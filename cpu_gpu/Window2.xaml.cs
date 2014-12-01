using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Net;
using System.Management;
using System.Web;
using System.IO;
using Daramkun.Blockar.Json;

namespace cpu_gpu
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2(string name,string div)
        {
            InitializeComponent();

            if (div == "cpu")
            {
                GetThisCPUParser(name);
            }
            else if (div == "gpu")
            {
                GetThisGPUParser(name);
            }

        }
        public void GetThisCPUParser(string name)//자기 cpu정보 파서
        {
            String JsonS = GetThisCpuPerform(name);
            JsonContainer detail = new JsonContainer(JsonS)["detail"] as JsonContainer;
            JsonContainer details = detail["details"] as JsonContainer;

            cpu_no.Text = "cpu_no : " + detail["cpu_no"] as string;
            cpu_idle.Text = "cpu_idle : " + detail["cpu_idle"] as string;
            cpu_full.Text = "cpu_full : " + detail["cpu_full"] as string;
            cpu_cores.Text = "cpu_cores : " + detail["cpu_cores"] as string;
            cpu_threads.Text = "cpu_threads : " + detail["cpu_threads"] as string;
            cpu_l2.Text = "cpu_l2 : " + detail["cpu_l2"] as string;
            cpu_l3.Text = "cpu_l3 : " + detail["cpu_l3"] as string;
            cpu_name.Text = "cpu_name : " + detail["cpu_name"] as string;
            cpu_tdp.Text = "cpu_tdp : " + detail["cpu_tdp"] as string;
            cpu_price.Text = "cpu_price : " + detail["cpu_price"] as string;
            cpu_memory_type.Text = "cpu_memory_type : " + detail["cpu_memory_type"] as string;
            cpu_score.Text = "cpu_score : " + (details[0] as JsonContainer)["score"] as string;
        }

        public void GetThisGPUParser(string name)//자기 cpu정보 파서
        {
            String JsonS = GetThisGpuPerform(name);
            JsonContainer detail = new JsonContainer(JsonS)["detail"] as JsonContainer;
            JsonContainer details = detail["details"] as JsonContainer;

            cpu_no.Text = "gpu_no : " + detail["gpu_no"] as string;
            cpu_idle.Text = "gpu_processors : " + detail["gpu_processors"] as string;
            cpu_full.Text = "gpu_memory_type : " + detail["gpu_memory_type"] as string;
            cpu_cores.Text = "gpu_memory_size : " + detail["gpu_memory_size"] as string;
            cpu_threads.Text = "gpu_name : " + detail["gpu_name"] as string;
            cpu_l2.Text = "gpu_consume_power : " + detail["gpu_consume_power"] as string;
            cpu_l3.Text = "gpu_recommend_power : " + detail["gpu_recommend_power"] as string;
            cpu_name.Text = "gpu_price : " + detail["gpu_price"] as string;
            cpu_tdp.Text = "gpu_score : " + (details[0] as JsonContainer)["score"] as string;
            cpu_price.Visibility = Visibility.Hidden;
            cpu_memory_type.Visibility = Visibility.Hidden;
            cpu_score.Visibility = Visibility.Hidden;

        }

        public string GetThisGpuPerform(string name)
        {
            string GPU_NAME = name;
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

        public string GetThisCpuPerform(string name)//자신의 컴퓨터 cpu 이름 보내서 정보 받기(메인실행시 실행)
        {
            string CPU_NAME = name;
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
    }
}
