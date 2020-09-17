using System;
using System.Collections.Generic;
using System.Text;

namespace ItemLib.Model
{
    public class FilterItem
    {
        private double _lowQuantity;

        public double LowQuantity
        {
            get { return _lowQuantity; }
            set { _lowQuantity = value; }
        }

        private double _highQuantity;

        public double HighQuantity
        {
            get { return _highQuantity; }
            set { _highQuantity = value; }
        }

        public FilterItem(double lowQuantity, double highQuantity)
        {
            LowQuantity = lowQuantity;
            HighQuantity = highQuantity;
        }

        public FilterItem()
        {
            
        }



    }
}
