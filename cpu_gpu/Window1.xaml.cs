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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        int num = 0;
        string UUse;
        int seton = 0;
        string DDvice;
        string name_1,name_2,name_3,name_4,name_5;
        public Window1(string used, string Divice)
        {
            InitializeComponent();
            UUse = used;
            DDvice = Divice;
            PostB.IsEnabled = false;
            if (Divice == "cpu")
            {
                GetCPUListParser(used);
            }
            else if (Divice == "gpu")
            {
                GetGPUListParser(used);
            }
            

        }

        private void Close_Act(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CButton_1(object sender, RoutedEventArgs e)
        {
            new Window2(name_1,DDvice).Show();
        }
        private void CButton_2(object sender, RoutedEventArgs e)
        {
            new Window2(name_2, DDvice).Show();
        }
        private void CButton_3(object sender, RoutedEventArgs e)
        {
            new Window2(name_3, DDvice).Show();
        }
        private void CButton_4(object sender, RoutedEventArgs e)
        {
            new Window2(name_4, DDvice).Show();
        }
        private void CButton_5(object sender, RoutedEventArgs e)
        {
            new Window2(name_5, DDvice).Show();
        }

        private void left_click(object sender, RoutedEventArgs e)
        {
            seton = 1;
            num--;
            if (num == 0)
            {
                PostB.IsEnabled = false;
            }
            else if (num != 5)
            {
                NextB.IsEnabled = true;
            }
            if (DDvice == "cpu")
                GetCPUListParser(UUse);
            else if (DDvice == "gpu")
                GetGPUListParser(UUse);
            
        }

        private void right_click(object sender, RoutedEventArgs e)
        {
            seton = 1;
            num++;
            if (num == 5)
            {
                NextB.IsEnabled = false;
            }
            else if( num != 0)
            {
                PostB.IsEnabled = true;
            }
            if (DDvice == "cpu")
            {
                GetCPUListParser(UUse);
            }
            else if (DDvice == "gpu")
            {
                GetGPUListParser(UUse);
            }
        }

        //private void right_click()


        public void GetCPUListParser(string used)
        {

            String JsonS = GetCPUList(used);

            JsonContainer items = new JsonContainer(JsonS)["items"] as JsonContainer;
            /*
            if (seton == 0)
            {
                JsonContainer temp, temp_2, temp_3;
                int[] totalS = new int[100];
                int[] totalN = new int[100];
                int j = 0;
                foreach (JsonContainer i in items.GetListEnumerable())
                {
                    temp = items[i] as JsonContainer;
                    temp_2 = temp["benchmarks"] as JsonContainer;
                    temp_3 = temp_2[0] as JsonContainer;
                    totalS[j] = int.Parse(temp_3["score"] as string);//밴치마크 점수저장
                    totalN[j] = j;// 걍 숫자 저장
                    j++;
                }
                int max, tempI;
                for (int i = 0; i < j - 1; i++)
                {
                    max = totalS[i];
                    for (int k = i + 1; k < j; k++)
                    {
                        if (max < totalS[k])
                        {
                            tempI = max;
                            max = totalS[k];
                            totalS[k] = tempI;
                            totalN[i] = k;//위치 바뀜이 일어날때 마다 숫자를 바꿔줌으로서
                            totalN[k] = i;//본래위치에서 어디로 옮겨졌는지 파악
                        }
                    }
                }
            }
            */
            JsonContainer itemsJ_0 = items[num * 5] as JsonContainer;
            JsonContainer itemsJ_1 = items[num * 5 + 1] as JsonContainer;
            JsonContainer itemsJ_2 = items[num * 5 + 2] as JsonContainer;
            JsonContainer itemsJ_3 = items[num * 5 + 2] as JsonContainer;
            JsonContainer itemsJ_4 = items[num * 5 + 3] as JsonContainer;

            name_1 = itemsJ_0["name"] as string;
            name_2 = itemsJ_1["name"] as string;
            name_3 = itemsJ_2["name"] as string;
            name_4 = itemsJ_3["name"] as string;
            name_5 = itemsJ_4["name"] as string;


            bu_1.Content = "제품명 : " + itemsJ_0["name"] as string +
                           " 밴치마크 점수 : " + ((itemsJ_0["benchmarks"] as JsonContainer)[0] as JsonContainer)["score"] as string +
                           " 가격 : " + itemsJ_0["price"] as string;
            bu_2.Content = "제품명 : " + itemsJ_1["name"] as string +
                           " 밴치마크 점수 : " + ((itemsJ_1["benchmarks"] as JsonContainer)[0] as JsonContainer)["score"] as string +
                           " 가격 : " + itemsJ_1["price"] as string;
            bu_3.Content = "제품명 : " + itemsJ_2["name"] as string +
                           " 밴치마크 점수 : " + ((itemsJ_2["benchmarks"] as JsonContainer)[0] as JsonContainer)["score"] as string +
                           " 가격 : " + itemsJ_2["price"] as string;
            bu_4.Content = "제품명 : " + itemsJ_3["name"] as string +
                           " 밴치마크 점수 : " + ((itemsJ_3["benchmarks"] as JsonContainer)[0] as JsonContainer)["score"] as string +
                           " 가격 : " + itemsJ_3["price"] as string;
            bu_5.Content = "제품명 : " + itemsJ_4["name"] as string +
                           " 밴치마크 점수 : " + ((itemsJ_4["benchmarks"] as JsonContainer)[0] as JsonContainer)["score"] as string +
                           " 가격 : " + itemsJ_4["price"] as string;

        }
        public void GetGPUListParser(string used)
        {
            String JsonS = GetGPUList(used);

            JsonContainer items = new JsonContainer(JsonS)["items"] as JsonContainer;
            /*
            if (seton == 0)
            {
                JsonContainer temp, temp_2, temp_3;
                int[] totalS = new int[100];
                int[] totalN = new int[100];
                int j = 0;
                foreach (JsonContainer i in items.GetListEnumerable())
                {
                    temp = items[i] as JsonContainer;
                    temp_2 = temp["benchmarks"] as JsonContainer;
                    temp_3 = temp_2[0] as JsonContainer;
                    totalS[j] = int.Parse(temp_3["score"] as string);//밴치마크 점수저장
                    totalN[j] = j;// 걍 숫자 저장
                    j++;
                }
                int max, tempI;
                for (int i = 0; i < j - 1; i++)
                {
                    max = totalS[i];
                    for (int k = i + 1; k < j; k++)
                    {
                        if (max < totalS[k])
                        {
                            tempI = max;
                            max = totalS[k];
                            totalS[k] = tempI;
                            totalN[i] = k;//위치 바뀜이 일어날때 마다 숫자를 바꿔줌으로서
                            totalN[k] = i;//본래위치에서 어디로 옮겨졌는지 파악
                        }
                    }
                }
            }
            */
            JsonContainer itemsJ_0 = items[num * 5] as JsonContainer;
            JsonContainer itemsJ_1 = items[num * 5 + 1] as JsonContainer;
            JsonContainer itemsJ_2 = items[num * 5 + 2] as JsonContainer;
            JsonContainer itemsJ_3 = items[num * 5 + 2] as JsonContainer;
            JsonContainer itemsJ_4 = items[num * 5 + 3] as JsonContainer;

            name_1 = itemsJ_0["name"] as string;
            name_2 = itemsJ_1["name"] as string;
            name_3 = itemsJ_2["name"] as string;
            name_4 = itemsJ_3["name"] as string;
            name_5 = itemsJ_4["name"] as string;


            bu_1.Content = "제품명 : " + itemsJ_0["name"] as string +
                           " 밴치마크 점수 : " + ((itemsJ_0["benchmarks"] as JsonContainer)[0] as JsonContainer)["score"] as string +
                           " 가격 : " + itemsJ_0["price"] as string;
            bu_2.Content = "제품명 : " + itemsJ_1["name"] as string +
                           " 밴치마크 점수 : " + ((itemsJ_1["benchmarks"] as JsonContainer)[0] as JsonContainer)["score"] as string +
                           " 가격 : " + itemsJ_1["price"] as string;
            bu_3.Content = "제품명 : " + itemsJ_2["name"] as string +
                           " 밴치마크 점수 : " + ((itemsJ_2["benchmarks"] as JsonContainer)[0] as JsonContainer)["score"] as string +
                           " 가격 : " + itemsJ_2["price"] as string;
            bu_4.Content = "제품명 : " + itemsJ_3["name"] as string +
                           " 밴치마크 점수 : " + ((itemsJ_3["benchmarks"] as JsonContainer)[0] as JsonContainer)["score"] as string +
                           " 가격 : " + itemsJ_3["price"] as string;
            bu_5.Content = "제품명 : " + itemsJ_4["name"] as string +
                           " 밴치마크 점수 : " + ((itemsJ_4["benchmarks"] as JsonContainer)[0] as JsonContainer)["score"] as string +
                           " 가격 : " + itemsJ_4["price"] as string;
        }

        public string GetCPUList(string used)// cpu리스트 받아오기 (입력값 용도)
        {
            string serverUrl = "http://cpungpu.daram.pe.kr/client/recommend/cpu/";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverUrl + used);

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

        public string GetGPUList(string used)//used에 용도가 들어옵니다.
        {
            string serverUrl = "http://cpungpu.daram.pe.kr/client/recommend/gpu/";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverUrl + used);

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

        

        
    }
}
