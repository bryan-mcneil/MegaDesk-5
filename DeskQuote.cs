using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDesk_5
{
    class DeskQuote
    {
        // Prop + tab + tab
        public string date { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Double DeskCost { get; set; }
        public int ShippingDays { get; set; }
        public Double ShippingCost { get; set; }
        public Desk newDesk1 { get; set; }

        public DeskQuote(String inFirstName, String inLastName, Double inDeskCost, int inShippingDays, Double inShippingCost, Desk inDesk)
        {
            FirstName = inFirstName;
            LastName = inLastName;
            date = Convert.ToString(DateTime.Now);
            DeskCost = inDeskCost;
            ShippingDays = inShippingDays;
            ShippingCost = inShippingCost;
            newDesk1 = inDesk;
        }
    }
}
