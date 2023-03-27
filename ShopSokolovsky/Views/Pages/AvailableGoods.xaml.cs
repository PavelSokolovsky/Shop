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
    public partial class ResponceOrder
    {
        public ResponceOrder()
        {

        }
        public Stock Stock { get; set; }
        public DateTime Date { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для AvailableGoods.xaml
    /// </summary>
    public partial class AvailableGoods : Page
    {
       public static HttpClient httpClient = new HttpClient();
        public static List<ResponceProductInStock> productInStock;
        public static List<ResponceOrder> responceOrder;
        
            public AvailableGoods()
        {
            //new ShopEntiti().ProductInStock.FirstOrDefault().Product.name
            InitializeComponent();
            GetGoodsInformation();
            
        }
        private async void AddOrder()
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(responceOrder), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:52488/api/Orders/"+ Views.Windows.AuthWindow.client.id, httpContent);
            if (message.IsSuccessStatusCode)
            {

            }
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
            HttpResponseMessage message = await httpClient.PutAsync("http://localhost:52488/api/ProductInStocks/1?barcode="+ barcodeTBX.Text, httpContent );
            if(message.IsSuccessStatusCode)
            {
                GetGoodsInformation();
            }
        }

        private void barcodeBTN_Click(object sender, RoutedEventArgs e)
        {
            Views.Windows.MainWindow.mainfrm.frm.Navigate(new ProductsInBusket());
           
        }

        private void addgoodScanBTN_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private async void AddProductInStok()
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(productInStock), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:52488/api/ProductInStocks", httpContent);
            if(message.IsSuccessStatusCode)
            {

            }
        }
    }
    
}
