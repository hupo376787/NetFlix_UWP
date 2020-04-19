using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NetFlix
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<StoreModel> stores = new List<StoreModel>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < 100; i++)
            {
                products.Add(
                    new Product {
                    text = i.ToString(),
                    image = "ms-appx:///Assets/v2.jpg"
                    }
                    ) ;
            }

            for (int i = 0; i < 100; i++)
            {
                stores.Add(
                    new StoreModel
                    {
                        title = "River" + i.ToString(),
                        products = products
                    }
                    );
            }

            //listView.ItemsSource = stores;
            NetFlixItemsRepeater.ItemsSource = stores;
        }
    }
}
