using System;
using System.Collections.Generic;
using System.Text;

namespace ItemLib.Model
{
    public class Item
    {
        private int _id;
        private string _name;
        private string _quality;
        private double _quantity;


        public int ID
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Quality
        {
            get => _quality;
            set => _quality = value;
        }

        public double Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }

        public Item(int id, string name, string quality, double quantity)
        {
            ID = id;
            Name = name;
            Quality = quality;
            Quantity = quantity;
        }

        public Item()
        {
            
        }

        public override string ToString()
        {
            return ID + "" + Name + "" + Quality + "" + Quantity;
        }
    }
}
