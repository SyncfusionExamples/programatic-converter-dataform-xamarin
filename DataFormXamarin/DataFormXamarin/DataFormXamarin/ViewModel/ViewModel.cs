using System;
using System.Collections.Generic;
using System.Text;

namespace DataFormXamarin
{
    public class ViewModel
    {
        private Contacts contactDetails;
        public Contacts ContactDetails
        {
            get { return contactDetails; }
            set { contactDetails = value; }
        }
        public ViewModel()
        {
            contactDetails = new Contacts();
        }
    }
}
