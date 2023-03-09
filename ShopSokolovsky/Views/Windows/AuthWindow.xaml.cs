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
using System.Net.Http;
using ShopSokolovsky.Models;
using Newtonsoft.Json;

namespace ShopSokolovsky.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public static HttpClient httpClient = new HttpClient();
        public static Client client;
        public AuthWindow()
        {
            InitializeComponent();
            Enter();
        }
        private async void Enter()
        {
            loginTB.Text = Properties.Settings.Default.login;
            passPB.Password = Properties.Settings.Default.password;
        }

        private async void enterBTN_Click(object sender, RoutedEventArgs e)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("applicaton/json"));
            
            var contet = new UserData { login = loginTB.Text, password = passPB.Password };
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(contet), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:52488/api/Auth", httpContent);
            if(message.IsSuccessStatusCode)
            {
                var curContent = await message.Content.ReadAsStringAsync();
                client = JsonConvert.DeserializeObject<Client>(curContent);
                if ((bool)SaveChek.IsChecked)
                {
                    Properties.Settings.Default.login = loginTB.Text;
                    Properties.Settings.Default.password = loginTB.Text;
                    Properties.Settings.Default.Save();
                    
                }
                else
                {
                    Properties.Settings.Default.login = string.Empty;
                    Properties.Settings.Default.password = string.Empty;
                    Properties.Settings.Default.Save();
                }
                MainWindow m = new MainWindow();
                m.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка Входа");
            }
        }
        public class UserData
        {
            public string login { get; set; }
            public string password { get; set; }
        }
    }
}
