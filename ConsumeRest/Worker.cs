using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ItemLib.Model;
using Newtonsoft.Json;

namespace ConsumeRest
{
    public class Worker
    {
        private const string URI = "http://andersbootstrapmakeover.azurewebsites.net/api/localItems/";
        public async void Start()
        {
            Console.WriteLine("Udskriver alle");
           IList<Item> list = await GetAllItemsAsync();

           foreach (Item item in list)
           {
               Console.WriteLine(item);
           }
           Console.WriteLine("Udskrive nr:2");
           //int no = int.Parse(Console.ReadLine());

            Item idItem = await GetOneItemsAsync(2);
           
           Console.WriteLine(idItem);

           //

           Item i1 = new Item(5, "name", "High", 23.5);
            Console.WriteLine("Opretter Item");
          await PostAsync(i1);

          //

          i1.Name = "Sam";
            Console.WriteLine("Opdatere Item");
         await PutAsync(i1);

         //

         Item i2 = new Item(1, "Su", "Low", 12.6);
            Console.WriteLine("Sletter Item");
         await DeleteAsync(i2);
         IList<Item> list2 = await GetAllItemsAsync();

         foreach (Item item in list2)
         {
             Console.WriteLine(item);
         }
        }

        public async Task<IList<Item>> GetAllItemsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(URI);
                List<Item> cList = JsonConvert.DeserializeObject<List<Item>>(content);
                return cList;
            }
        }

        public async Task<Item> GetOneItemsAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(URI + id);
                Item clist = JsonConvert.DeserializeObject<Item>(content);
                return clist;
            } 
        }

        public async Task PostAsync(Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"); 
                HttpResponseMessage rsp = await client.PostAsync(URI, content);
            }
        }

        public async Task PutAsync(Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                HttpResponseMessage rsp = await client.PutAsync(URI + item.ID, content);
            }
        }

        public async Task DeleteAsync(Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage rsp = await client.DeleteAsync(URI + item.ID);
            }
        }
    }
}
