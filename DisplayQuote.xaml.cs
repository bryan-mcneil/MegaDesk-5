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
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MegaDesk_5
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DisplayQuote : Page
    {
        DeskQuote CurrentDeskQuote { get; set; }
        public JsonSerializer JsonSerializer { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CurrentDeskQuote = e.Parameter as DeskQuote;
        }
        public DisplayQuote()
        {
            this.InitializeComponent();

            FirstNameValue.Text = CurrentDeskQuote.FirstName;
            LastNameValue.Text = CurrentDeskQuote.LastName;

            WidthValue.Text = System.Convert.ToString(CurrentDeskQuote.newDesk1.width);
            DepthValue.Text = System.Convert.ToString(CurrentDeskQuote.newDesk1.depth);
            DeskAreaCost.Text = "$" + System.Convert.ToString(CurrentDeskQuote.newDesk1.DeskAreaCostCalc());

            DrawerCtValue.Text = System.Convert.ToString(CurrentDeskQuote.newDesk1.drawerCount);
            DrawerCtCost.Text = "$" + System.Convert.ToString(CurrentDeskQuote.newDesk1.drawerCount * 50);

            MaterialValue.Text = System.Convert.ToString(CurrentDeskQuote.newDesk1.surface);
            MaterialCost.Text = "$" + System.Convert.ToString((double)CurrentDeskQuote.newDesk1.surface);

            ShippingOpValue.Text = System.Convert.ToString(CurrentDeskQuote.ShippingDays) + " Day";
            ShippingOpCost.Text = "$" + System.Convert.ToString(CurrentDeskQuote.ShippingCost);

            DeskCost.Text = "$" + System.Convert.ToString(CurrentDeskQuote.DeskCost + CurrentDeskQuote.ShippingCost);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void SaveQuit_Click(object sender, RoutedEventArgs e)
        {
            File.AppendAllText("quotes.json", JsonConvert.SerializeObject(CurrentDeskQuote) + "\r\n");
            Frame.Navigate(typeof(MainPage));
        }
    }
}
