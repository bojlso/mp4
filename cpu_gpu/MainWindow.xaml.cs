using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Management;
using System.Web;
using System.IO;
using Daramkun.Blockar.Json;

namespace cpu_gpu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
     
        public MainWindow()
        {
            InitializeComponent();
            GetThisCPUParser();
            GetThisGPUParser();
        }


        public void GetThisCPUParser()//자기 cpu정보 파서
        {
            String JsonS = GetThisCpuPerform();
            JsonContainer detail = new JsonContainer(JsonS)["detail"] as JsonContainer;
            JsonContainer details = detail["details"] as JsonContainer;
            int score = int.Parse((details[0] as JsonContainer)["score"] as string);

            Button Docu = (Button)this.FindName("Docu_1");
            Button WebSerf = (Button)this.FindName("WebSerf_1");
            Button WebBoard = (Button)this.FindName("WebBoard_1");
            Button Low = (Button)this.FindName("Low_1");
            Button Online = (Button)this.FindName("Online_1");
            Button High = (Button)this.FindName("High_1");
            Button HighEnd = (Button)this.FindName("HighEnd_1");
            Button Sound = (Button)this.FindName("Sound_1");
            Button Grap = (Button)this.FindName("Grap_1");
           
            Docu.Content = score + " /\n";//이 뒷부분에 기준되는 점수 받아와 넣으면 됨
            WebSerf.Content = score + " /\n";
            WebBoard.Content = score + " /\n";
            Low.Content = score + " /\n";
            Online.Content = score + " /\n";
            High.Content = score + " /\n";
            HighEnd.Content = score + " /\n";
            Sound.Content = score + " /\n";
            Grap.Content = score + " /\n";

            Docu.Click += (object sender, RoutedEventArgs e) => { new Window1("documentation", "cpu").Show(); };
            WebSerf.Click += (object sender, RoutedEventArgs e) => { new Window1("websurfing", "cpu").Show(); };
            WebBoard.Click += (object sender, RoutedEventArgs e) => { new Window1("web_board_game", "cpu").Show(); };
            Low.Click += (object sender, RoutedEventArgs e) => { new Window1("lowperf_game", "cpu").Show(); };
            Online.Click += (object sender, RoutedEventArgs e) => { new Window1("online_game", "cpu").Show(); };
            High.Click += (object sender, RoutedEventArgs e) => { new Window1("highperf_game", "cpu").Show(); };
            HighEnd.Click += (object sender, RoutedEventArgs e) => { new Window1("highend_game", "cpu").Show(); };
            Sound.Click += (object sender, RoutedEventArgs e) => { new Window1("edit_audio", "cpu").Show(); };
            Grap.Click += (object sender, RoutedEventArgs e) => { new Window1("rendering", "cpu").Show(); };

        }
        public void GetThisGPUParser()
        {
            String JsonS = GetThisGpuPerform();
            JsonContainer detail = new JsonContainer(JsonS)["detail"] as JsonContainer;
            JsonContainer details = detail["details"] as JsonContainer;
            int score = int.Parse((details[0] as JsonContainer)["score"] as string);

            Button Docu = (Button)this.FindName("Docu_2");
            Button WebSerf = (Button)this.FindName("WebSerf_2");
            Button WebBoard = (Button)this.FindName("WebBoard_2");
            Button Low = (Button)this.FindName("Low_2");
            Button Online = (Button)this.FindName("Online_2");
            Button High = (Button)this.FindName("High_2");
            Button HighEnd = (Button)this.FindName("HighEnd_2");
            Button Sound = (Button)this.FindName("Sound_2");
            Button Grap = (Button)this.FindName("Grap_2");

            Docu.Content = score + " /\n";
            WebSerf.Content = score + " /\n";
            WebBoard.Content = score + " /\n";
            Low.Content = score + " /\n";
            Online.Content = score + " /\n";
            High.Content = score + " /\n";
            HighEnd.Content = score + " /\n";
            Sound.Content = score + " /\n";
            Grap.Content = score + " /\n";

            Docu.Click += (object sender, RoutedEventArgs e) => { new Window1("documentation", "gpu").Show(); };
            WebSerf.Click += (object sender, RoutedEventArgs e) => { new Window1("websurfing", "gpu").Show(); };
            WebBoard.Click += (object sender, RoutedEventArgs e) => { new Window1("web_board_game", "gpu").Show(); };
            Low.Click += (object sender, RoutedEventArgs e) => { new Window1("lowperf_game", "gpu").Show(); };
            Online.Click += (object sender, RoutedEventArgs e) => { new Window1("online_game", "gpu").Show(); };
            High.Click += (object sender, RoutedEventArgs e) => { new Window1("highperf_game", "gpu").Show(); };
            HighEnd.Click += (object sender, RoutedEventArgs e) => { new Window1("highend_game", "gpu").Show(); };
            Sound.Click += (object sender, RoutedEventArgs e) => { new Window1("edit_audio", "gpu").Show(); };
            Grap.Click += (object sender, RoutedEventArgs e) => { new Window1("rendering", "gpu").Show(); };
        }

         public void GetBenchCPUParser()// 목적에 따른 cpu 밴치마크 점수 파서
        {
            String JsonS = GetBenchScoreCPU();

            JsonContainer scores = new JsonContainer(JsonS)["scores"] as JsonContainer;

            JsonContainer temp;

            String purpose;
            int score;

            foreach (JsonContainer i in scores.GetListEnumerable())
            {
                temp = scores[i] as JsonContainer;
                purpose = temp["purpose"] as string;
                score = int.Parse(temp["score"] as string);
            }
        }
         public void GetBenchGPUParser()
         {
             String JsonS = GetBenchScoreGPU();

             JsonContainer scores = new JsonContainer(JsonS)["scores"] as JsonContainer;

             JsonContainer temp;

             String purpose;
             int score;

             foreach (JsonContainer i in scores.GetListEnumerable())
             {
                 temp = scores[i] as JsonContainer;
                 purpose = temp["purpose"] as string;
                 score = int.Parse(temp["score"] as string);
             }
         }


        //파서 끝


         public string GetBenchScoreCPU() // 용도별 밴치 마크 점수 가져오기(고정값)
         {
             string serverUrl = "http://cpungpu.daram.pe.kr/client/benchscore/cpu/";

             HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverUrl);

             //응답 받기
             HttpWebResponse res = (HttpWebResponse)req.GetResponse();

             //stream읽기
             Stream resStream = res.GetResponseStream();
             StreamReader srReadData = new StreamReader(resStream, Encoding.Default);

             //Stream을 string변환
             string strResult = srReadData.ReadToEnd();


             resStream.Close();
             res.Close();

             return strResult;//스트링 반환 
         }
         public string GetBenchScoreGPU()
         {
             string serverUrl = "http://cpungpu.daram.pe.kr/client/benchscore/gpu/";

             HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverUrl);

             //응답 받기
             HttpWebResponse res = (HttpWebResponse)req.GetResponse();

             //stream읽기
             Stream resStream = res.GetResponseStream();
             StreamReader srReadData = new StreamReader(resStream, Encoding.Default);

             //Stream을 string변환
             string strResult = srReadData.ReadToEnd();


             resStream.Close();
             res.Close();

             return strResult;//스트링 반환 
         }


         

         


         public string GetThisCpuPerform()//자신의 컴퓨터 cpu 이름 보내서 정보 받기(메인실행시 실행)
         {
             //string CPU_NAME = GetName("CPU").First<string>();
             string CPU_NAME = "Intel(R) Core(TM) i5-3570";
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
             //string GPU_NAME = GetName("GPU").First<string>();
             string GPU_NAME = "GeForce GTX 660 Ti"; 
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



         private string GetKey(string deviceType)
         {
             switch (deviceType)
             {
                 case "CPU": return "Win32_Processor";
                 case "GPU": return "Win32_VideoController";
                 default: return null;
             }
         }
         public IEnumerable<string> GetName(string deviceType)
         {

             ManagementObjectSearcher searcher = new ManagementObjectSearcher("select name from " + GetKey(deviceType));
             foreach (ManagementObject obj in searcher.Get())
             {
                 yield return obj["Name"] as string;
             }
             yield return null;
         }
    }

    
}
