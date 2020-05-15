using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataFormXamarin
{
    public class Contacts
    {
        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private double? amount;
        public double? Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
        }
    }
}
