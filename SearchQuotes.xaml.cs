using Newtonsoft.Json;
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
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MegaDesk_5
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchQuotes : Page
    {
        private List<string> materials;

        public DateTime dateSet { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Double DeskCost { get; set; }
        public int ShippingDays { get; set; }
        public double ShippingCost { get; set; }
        //public Desk Desk { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public int DrawerCount { get; set; }
        public double Surface { get; set; }

        public SearchQuotes()
        {
            this.InitializeComponent();

            materials = new List<string>();
            foreach (Desk.SurfaceMaterials surfaceMaterials in Enum.GetValues(typeof(Desk.SurfaceMaterials)))
            { materials.Add(Convert.ToString(surfaceMaterials)); }

            variableMaterial.ItemsSource = materials;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
                //Clear searchbox
                ViewAQuotes.Blocks.Clear();

                Paragraph deskQuoteObjects = new Paragraph();
                Run deskQuoteObjectRun = new Run();

                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile file = await storageFolder.GetFileAsync("quotes.json");
                using (var inputStream = await file.OpenReadAsync())
                using (var classicStream = inputStream.AsStreamForRead())
                using (var streamReader = new StreamReader(classicStream))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        string json = streamReader.ReadLine();
                        DeskQuote printQuote = JsonConvert.DeserializeObject<DeskQuote>(json);


                        if (variableMaterial.SelectedItem.ToString() == System.Convert.ToString(printQuote.newDesk1.surface))
                        {
                            //Desk.SurfaceMaterials MaterialSelected = (Desk.SurfaceMaterials)Enum.Parse(typeof(Desk.SurfaceMaterials), MaterialSelectedText);
                            //Print the matching quotes

                            deskQuoteObjectRun.Text +=
                                "Date: \t" + printQuote.date +
                                "\n First Name: \t" + printQuote.FirstName +
                                "\n Last Name: \t" + printQuote.LastName +
                                "\n Desk Specs:" +
                                "\n Width: \t\t" + System.Convert.ToString(printQuote.newDesk1.width) +
                                "\n Depth: \t\t" + System.Convert.ToString(printQuote.newDesk1.depth) +
                                "\n Drawers: \t" + System.Convert.ToString(printQuote.newDesk1.drawerCount) +
                                "\n Material: \t\t" + System.Convert.ToString(printQuote.newDesk1.surface) +
                                "\n Shipping: \t" + System.Convert.ToString(printQuote.ShippingDays) + " Day" +
                                "\n Total Cost: \t" + "$" + System.Convert.ToString(printQuote.DeskCost + printQuote.ShippingCost) +
                                "\n" +
                                "==============================" +
                                "\n";
                        }
                    }
                deskQuoteObjects.Inlines.Add(deskQuoteObjectRun);
                ViewAQuotes.Blocks.Add(deskQuoteObjects);
            }
        }
    }
}