using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MegaDesk_5
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddQuoteWindow : Page
    {
        private List<string> materials;
        private bool validated;
        public AddQuoteWindow()
        {
            this.InitializeComponent();
            materials = new List<string>();

            foreach (Desk.SurfaceMaterials surfaceMaterials in Enum.GetValues(typeof(Desk.SurfaceMaterials)))
            {
                materials.Add(Convert.ToString(surfaceMaterials));
            }

            variableMaterial.ItemsSource = materials;
        }

        private void addQuoteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            string FirstName = variableFirstName.Text;
            string LastName = variableLastName.Text;
            int width = Convert.ToInt32(variableWidth.Text);
            int depth = Convert.ToInt32(variableDepth.Text);
            int drawerCt = Convert.ToInt32(variableDrawer.SelectedItem.ToString());
            string MaterialSelectedText = variableMaterial.SelectedItem.ToString();
            Desk.SurfaceMaterials MaterialSelected = (Desk.SurfaceMaterials)Enum.Parse(typeof(Desk.SurfaceMaterials), MaterialSelectedText);
            int ShippingDays = int.Parse(variableShipping.SelectedItem.ToString());
            
                // Create the newDesk object
                Desk newDesk1 = new Desk(width, depth, drawerCt, MaterialSelected);

                //Calculate the deskCost and ShippingCost so we can create the DeskQuote object
                double deskCost = newDesk1.CalculateDeskCost();
                double ShippingCost = CalculateShipping(ShippingDays, newDesk1);

                // Create the DeskQuote object
                DeskQuote newDeskQuote1 = new DeskQuote(FirstName, LastName, deskCost, ShippingDays, ShippingCost, newDesk1);

                Frame.Navigate(typeof(DisplayQuote), newDeskQuote1);
            }
            catch (Exception)
            {
                var messageDialog = new MessageDialog("There is an error in your input. Please try again.");
                messageDialog.ShowAsync();
            }
        }

        //Calculator for shipping Costs
        double CalculateShipping(int inShippingDays, Desk inDesk)
        {
            int shippingDays = inShippingDays;
            Desk newDesk1 = inDesk;
            double DeskArea = inDesk.DeskAreaCalc();
            double ShippingCost;

            int i;
            if (DeskArea < 1000)
            {
                i = 0;
            }
            else if (DeskArea > 1000 && DeskArea < 2000)
            {
                i = 1;
            }
            else
            {
                i = 2;
            }

            switch (shippingDays)
            {
                case 3:
                    double[] ThreeDay = new double[3] { 60, 70, 80 };
                    ShippingCost = ThreeDay[i];
                    break;
                case 5:
                    double[] FiveDay = new double[3] { 40, 50, 60 };
                    ShippingCost = FiveDay[i];
                    break;
                case 7:
                    double[] SevenDay = new double[3] { 30, 35, 40 };
                    ShippingCost = SevenDay[i];
                    break;
                case 14:
                    double[] FourteenDay = new double[3] { 0, 0, 0 };
                    ShippingCost = FourteenDay[i];
                    break;
                default:
                    throw new Exception("Bad Input");
                    break;
            }

            return ShippingCost;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }


        private void variableDepth_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            int i = 0;
            if (variableDepth.Text != "")
            {
                if (!int.TryParse(variableDepth.Text, out i))
                {
                    var messageDialog = new MessageDialog("Please enter a number");
                    messageDialog.ShowAsync();
                    variableDepth.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                    variableDepth.Text = "";
                    validated = false;
                }
                else
                {
                    variableDepth.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
                    validated = true;
                }
            }
        }

        private void variableWidth_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            int i = 0;

            if (variableWidth.Text != "")
            {
                if (!int.TryParse(variableWidth.Text, out i))
                {
                    var messageDialog = new MessageDialog("Please enter a number");
                    messageDialog.ShowAsync();
                    variableWidth.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                    variableWidth.Text = "";
                    validated = false;
                }
                else
                {
                    variableWidth.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
                    validated = true;
                }
            }
        }

        
        
        private void variableDepth_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {

                if (validated)
                {
                    int depth = 0;
                    depth = Convert.ToInt32(variableDepth.Text);
                    if (!(depth > 11 && depth < 49))
                    {
                        var messageDialog = new MessageDialog("Please enter a number between 12 and 48");
                        messageDialog.ShowAsync();
                        variableDepth.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                        variableDepth.Text = "";
                    }
                    else
                    {
                        variableDepth.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
                    }
                }
            }
            catch (Exception)
            {}
        }

        private void variableWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validated)
                {
                    int width = 0;
                    width = Convert.ToInt32(variableWidth.Text);
                    if (!(width > 23 && width < 97))
                    {
                        var messageDialog = new MessageDialog("Please enter a number between 24 and 96");
                        messageDialog.ShowAsync();
                        variableWidth.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                        variableWidth.Text = "";
                    }
                    else
                    {
                        variableWidth.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
                    }
                }
            }
            catch (Exception)
            {}
        }
    }
}