﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Daramkun.Blockar.Common;

namespace Daramkun.Blockar.Json
{
	partial class JsonContainer
	{
		private enum ParseState
		{
			None,

			Object,
			Array,

			Key,
			Value,
		}

		private enum BSONType
		{
			EndDoc = 0,
			Double = 0x01,
			String = 0x02,
			Document = 0x03,
			Array = 0x04,
			BinaryData = 0x05,
			Boolean = 0x08,
			UTCTime = 0x09,
			Null = 0x0A,
			Regexp = 0x0B,
			JavascriptCode = 0x0D,
			JavascriptCodeWScope = 0x0F,
			Integer = 0x10,
			Integer64 = 0x12,
		}

		private static readonly char [] TRUE_ARRAY = new char [] { 'r', 'u', 'e' };
		private static readonly char [] FALSE_ARRAY = new char [] { 'a', 'l', 's', 'e' };
		private static readonly char [] NULL_ARRAY = new char [] { 'u', 'l', 'l' };

		private JsonContainer ParseString ( MyStringReader jsonString )
		{
			try
			{
				ParseState parseMode = ParseState.None;
				ParseState parseState = ParseState.None;
				Queue<object> tokenStack = new Queue<object> ();

				while ( true )
				{
					char rc = '\0';
					do
					{
						rc = jsonString.ReadChar ();
					} while ( rc == ' ' || rc == '	' || rc == '　' || rc == '\n' || rc == '\r' );

					if ( rc == ':' )
					{
						if ( !( parseMode == ParseState.Object && parseState == ParseState.Key ) )
							throw new Exception ( "Invalid JSON document." );
						parseState = ParseState.Value;
					}
					else if ( rc == ',' )
					{
						if ( parseState != ParseState.Value ) throw new Exception ( "Invalid JSON document." );
						parseState = ( parseMode == ParseState.Object ) ? ParseState.Key : ParseState.Value;
					}
					else if ( rc == '"' ) tokenStack.Enqueue ( GetStringFromString ( jsonString ) );
					else if ( rc == 't' || rc == 'f' || rc == 'n' ) tokenStack.Enqueue ( GetKeywordFromString ( jsonString, rc ) );
					else if ( ( rc >= '0' && rc <= '9' ) || rc == '-' || rc == '+' ) tokenStack.Enqueue ( GetNumberFromString ( jsonString, rc ) );
					else if ( rc == '{' || rc == '[' )
					{
						if ( parseState == ParseState.Value )
						{
							jsonString.Position -= 1;
							tokenStack.Enqueue ( ParseString ( jsonString ) );
						}
						else
						{
							switch ( rc )
							{
								case '{': parseMode = ParseState.Object; parseState = ParseState.Key; break;
								case '[': parseMode = ParseState.Array; parseState = ParseState.Value; break;
							}
						}
					}
					else if ( ( rc == '}' && parseMode == ParseState.Object ) || ( rc == ']' && parseMode == ParseState.Array ) ) break;
					else throw new ArgumentException ( "Invalid JSON document." );
				}

				return Build ( parseMode, tokenStack );
			}
			catch { throw new ArgumentException ( "Invalid JSON document." ); }
		}

		private JsonContainer ParseBinary ( BinaryReader jsonBinary, ParseState parseMode = ParseState.Object )
		{
			try
			{
				Queue<object> tokenStack = new Queue<object> ();
				bool isParsing = true;
				int dataSize = jsonBinary.ReadInt32 ();
				int currentPosition = ( int ) jsonBinary.BaseStream.Position;
				while ( isParsing && ( jsonBinary.BaseStream.Position - currentPosition ) != dataSize )
				{
					BSONType rb = ( BSONType ) jsonBinary.ReadByte ();
					if ( rb == BSONType.EndDoc ) break;

					if ( parseMode == ParseState.Object ) tokenStack.Enqueue ( GetKeyFromBinary ( jsonBinary ) );
					else jsonBinary.ReadByte ();

					switch ( rb )
					{
						case BSONType.EndDoc: isParsing = false; break;
						case BSONType.Double: tokenStack.Enqueue ( jsonBinary.ReadDouble () ); break;
						case BSONType.String: tokenStack.Enqueue ( GetStringFromBinary ( jsonBinary ) ); break;
						case BSONType.Document: tokenStack.Enqueue ( ParseBinary ( jsonBinary, ParseState.Object ) ); break;
						case BSONType.Array: tokenStack.Enqueue ( ParseBinary ( jsonBinary, ParseState.Array ) ); break;
						case BSONType.BinaryData: tokenStack.Enqueue ( GetBinaryFromBinary ( jsonBinary ) ); break;
						case BSONType.Boolean: tokenStack.Enqueue ( jsonBinary.ReadByte () == 0 ? false : true ); break;
						case BSONType.UTCTime: tokenStack.Enqueue ( DateTime.FromFileTimeUtc ( jsonBinary.ReadInt64 () ) ); break;
						case BSONType.Null: tokenStack.Enqueue ( null ); break;
						case BSONType.Regexp: tokenStack.Enqueue ( new Regex ( GetStringFromBinary ( jsonBinary ) ) ); break;
						case BSONType.JavascriptCode: tokenStack.Enqueue ( GetStringFromBinary ( jsonBinary ) ); break;
						case BSONType.Integer: tokenStack.Enqueue ( jsonBinary.ReadInt32 () ); break;
						case BSONType.Integer64: tokenStack.Enqueue ( jsonBinary.ReadInt64 () ); break;

						default: throw new Exception ( "There is unsupport Data type." );
					}
				}

				return Build ( parseMode, tokenStack );
			}
			catch { throw new ArgumentException ( "Invalid JSON document." ); }
		}

		public JsonContainer Parse ( Stream stream )
		{
			byte [] temp = new byte [ 1 ];
			stream.Read ( temp, 0, 1 );
			stream.Position = 0;

			if ( temp [ 0 ] == ( byte ) '{' || temp [ 0 ] == ( byte ) '[' || temp [ 0 ] == ( byte ) ' ' || temp [ 0 ] == ( byte ) '	' ||
				temp [ 0 ] == ( byte ) '\r' || temp [ 0 ] == ( byte ) '\n' )
			{
				return ParseString ( new MyStringReader ( stream ) );
			}
			else
			{
				int skipByte;
				Encoding encoding = EncodingChecker.Check ( stream, out skipByte );
				if ( skipByte == 0 ) return ParseBinary ( new BinaryReader ( stream ) );
				else { stream.Position += skipByte; return ParseString ( new MyStringReader ( stream, encoding ) ); }
			}
		}

		private JsonContainer Build ( ParseState parseMode, Queue<object> tokenStack )
		{
			JsonContainer container = new JsonContainer ( parseMode == ParseState.Object ? ContainType.Object : ContainType.Array );

			int index = 0;
			while ( tokenStack.Count > 0 )
			{
				object key;
				if ( parseMode == ParseState.Object )
					key = tokenStack.Dequeue ();
				else key = index++;
				object value = tokenStack.Dequeue ();
				container.Add ( value, key );
			}

			return container;
		}

		#region Get Data From String
		private string GetStringFromString ( MyStringReader jsonString )
		{
			char ch;
			StringBuilder sb = new StringBuilder ();
			bool backslashMode = false;
			while ( true )
			{
				ch = jsonString.ReadChar ();
				if ( backslashMode )
				{
					switch ( ch )
					{
						case 'n': sb [ sb.Length - 1 ] = '\n'; break;
						case 'r': sb [ sb.Length - 1 ] = '\r'; break;
						case 't': sb [ sb.Length - 1 ] = '\t'; break;
						case '\\': sb [ sb.Length - 1 ] = '\\'; break;
						case '"': sb [ sb.Length - 1 ] = '"'; break;
						case '/': sb [ sb.Length - 1 ] = '/'; break;
						case 'b': sb [ sb.Length - 1 ] = '\b'; break;
						case 'f': sb [ sb.Length - 1 ] = '\f'; break;
						case 'u':
							sb.Remove ( sb.Length - 1, 1 );
							sb.Append ( ToNumberFromHexa ( jsonString.ReadChars ( 2 ) ) );
							sb.Append ( ToNumberFromHexa ( jsonString.ReadChars ( 2 ) ) );
							break;

						default: throw new Exception ();
					}
					backslashMode = false;
				}
				else if ( ch == '"' ) break;
				else
				{
					sb.Append ( ch );
					if ( ch == '\\' ) backslashMode = true;
				}
			}
			return sb.ToString ();
		}

		private byte ToNumberFromHexa ( char [] p )
		{
			return byte.Parse ( string.Format ( "0x{0}{1}", p [ 0 ], p [ 1 ] ) );
		}

		private bool CheckArray ( char [] v1, char [] v2 )
		{
			for ( int i = 0; i < v1.Length; ++i )
				if ( v1 [ i ] != v2 [ i ] ) return false;
			return true;
		}

		private object GetKeywordFromString ( MyStringReader jsonString, char ch )
		{
			switch(ch)
			{
				case 't':
					if ( CheckArray ( jsonString.ReadChars ( 3 ), TRUE_ARRAY ) ) return true;
					goto default;
				case 'f':
					if ( CheckArray ( jsonString.ReadChars ( 4 ), FALSE_ARRAY ) ) return false;
					goto default;
				case 'n':
					if ( CheckArray ( jsonString.ReadChars ( 3 ), NULL_ARRAY ) ) return null;
					goto default;
				default:
					throw new Exception ( "Invalid JSON document." );
			}
		}

		private object GetNumberFromString ( MyStringReader jsonString, char ch )
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append ( ch );

			bool isFloatingPoint = false;

			while ( true )
			{
				ch = jsonString.ReadChar ();
				if ( !( ( ch >= '0' && ch <= '9' ) || ch == '.' || ( isFloatingPoint && ( ch == 'e' || ch == 'E' || ch == '-' || ch == '+' ) ) ) )
				{
					jsonString.Position -= 1;
					break;
				}

				sb.Append ( ch );

				if ( ch == '.' && !isFloatingPoint ) isFloatingPoint = true;
				else if ( ch == '.' && isFloatingPoint ) throw new Exception ( "Invalid JSON document." );
			}

			if ( !isFloatingPoint )
			{
				int temp;
				if ( int.TryParse ( sb.ToString (), out temp ) ) return temp;
				else throw new Exception ( "Invalid JSON document." );
			}
			else
			{
				double temp;
				if ( double.TryParse ( sb.ToString (), out temp ) ) return temp;
				else throw new Exception ( "Invalid JSON document." );
			}
		}
		#endregion

		#region Get Data From Binary
		private string GetKeyFromBinary ( BinaryReader jsonBinary )
		{
			StringBuilder sb = new StringBuilder ();
			char ch;
			while ( ( ch = jsonBinary.ReadChar () ) != '\0' )
				sb.Append ( ch );
			return sb.ToString ();
		}

		private string GetStringFromBinary ( BinaryReader jsonBinary )
		{
			int length = jsonBinary.ReadInt32 ();
			return Encoding.UTF8.GetString ( jsonBinary.ReadBytes ( length ), 0, length - 1 );
		}

		private byte [] GetBinaryFromBinary ( BinaryReader jsonBinary )
		{
			int length = jsonBinary.ReadInt32 ();
			return jsonBinary.ReadBytes ( length );
		}
		#endregion
	}
}
