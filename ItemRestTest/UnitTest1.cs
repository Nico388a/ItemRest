using System.Collections.Generic;
using ItemLib.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestItemService.Controllers;

namespace ItemRestTest
{
    [TestClass]
    public class UnitTest1
    {
        private ItemsController ctr;
        private Item _item;
        [TestInitialize]
        public void Init()
        {
            ctr = new ItemsController(); 
            _item = new Item(6, "Bread", "Low", 33.5);
        }

        [TestMethod]
        public void PostTest()
        {
            int arrange = ItemsController.Items.Count; 
            //Act
            ctr.Post(_item);


            Assert.AreEqual(arrange + 1, ItemsController.Items.Count);

        }


        [TestMethod]
        public void PutTest()
        {

        }

    }
}
