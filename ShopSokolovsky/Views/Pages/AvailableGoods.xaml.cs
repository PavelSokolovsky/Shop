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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using ShopSokolovsky.Models;
using Newtonsoft.Json;

namespace ShopSokolovsky.Views.Pages
{ public partial class ResponceProductInStock
    {
        public ResponceProductInStock()
        {
         

        }
        public Product Product { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Current { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для AvailableGoods.xaml
    /// </summary>
    public partial class AvailableGoods : Page
    {
       public static HttpClient httpClient = new HttpClient();
        public static List<ResponceProductInStock> productInStock;
        
            public AvailableGoods()
        {
            //new ShopEntiti().ProductInStock.FirstOrDefault().Product.name
            InitializeComponent();
            GetGoodsInformation();
        }
        private async void GetGoodsInformation()
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(productInStock), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await httpClient.GetAsync("http://localhost:52488/api/ProductInStocks/1");
            if (message.IsSuccessStatusCode)
            {
                var curContent = await message.Content.ReadAsStringAsync();
                productInStock = JsonConvert.DeserializeObject<List<ResponceProductInStock>>(curContent);
                DG.ItemsSource = productInStock;
            }

        }

        private  void scanBTN_Click(object sender, RoutedEventArgs e)
        {

            ScanImitate();

        }
        private async void ScanImitate()
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(productInStock), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:52488/api/ProductInStocks/1");
            if(message.IsSuccessStatusCode)
            {
                var curContent = await message.Content.ReadAsStringAsync();
                productInStock = JsonConvert.DeserializeObject<List<ResponceProductInStock>>(curContent);
                DG.ItemsSource = productInStock;
            }
        }
    }
}
