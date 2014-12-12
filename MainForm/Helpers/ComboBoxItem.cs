using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;


namespace SharepointListImport.Helpers
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public FieldCollection Fields { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
