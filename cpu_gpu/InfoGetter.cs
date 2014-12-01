using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Management;
using System.Web;
using System.IO;
using Daramkun.Blockar.Json;


namespace cpu_gpu
{
    enum DeviceType
    {
        CPU,
        GPU,
    }

    class InfoGetter
    {
        private string GetKey(DeviceType deviceType)
        {
            switch (deviceType)
            {
                case DeviceType.CPU: return "Win32_Processor";
                case DeviceType.GPU: return "Win32_VideoController";
                default: return null;
            }
        }

        public IEnumerable<string> GetName( DeviceType deviceType )
        {
            
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select name from " + GetKey(deviceType));
            foreach (ManagementObject obj in searcher.Get())
            {
                yield return obj["Name"] as string;
            }
            yield return null;
        }

        public string GetThisCpuPerform()//자신의 컴퓨터 cpu 이름 보내서 정보 받기(메인실행시 실행)
        {
            string CPU_NAME = GetName(DeviceType.CPU).First<string>();
            string serverUrl = "http://cpungpu.daram.pe.kr/client/itemdetail/cpu/";

            StringBuilder dataparams = new StringBuilder();
            dataparams.Append(CPU_NAME);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverUrl + HttpUtility.UrlEncode(dataparams.ToString()));
            
            //응답 받기
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            //stream읽기
            Stream resStream = res.GetResponseStream();
            StreamReader srReadData = new StreamReader(resStream, Encoding.Default);
            
            //Stream을 string변환
            string strResult = srReadData.ReadToEnd();

            resStream.Close();
            res.Close();

            return strResult;
        }

        public string GetThisGpuPerform()
        {
            string GPU_NAME = GetName(DeviceType.GPU).First<string>();
            string serverUrl = "http://cpungpu.daram.pe.kr/client/itemdetail/gpu/";

            StringBuilder dataparams = new StringBuilder();
            dataparams.Append(GPU_NAME);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverUrl + HttpUtility.UrlEncode(dataparams.ToString()));

            //응답 받기
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            //stream읽기
            Stream resStream = res.GetResponseStream();
            StreamReader srReadData = new StreamReader(resStream, Encoding.Default);

            //Stream을 string변환
            string strResult = srReadData.ReadToEnd();

            resStream.Close();
            res.Close();
            
            return strResult;
        }

        

       

       

       

        /*
             String JsonS = GetBenchScoreCPU();

             JsonContainer ThisScores = new JsonContainer(JsonS)["scores"] as JsonContainer;
             JsonContainer ThisS_0 = ThisScores[0] as JsonContainer;
             string ThisPurpose_0 = ThisS_0["purpose"] as string;

             foreach (KeyValuePair<object, object> i in ThisS_0.GetDictionaryEnumerable())//딕셔너리 (중괄호)
                 ;
             foreach (JsonContainer i in ThisS_0.GetListEnumerable())//배열 (대괄호)
                 ;
              * */

    }
}
