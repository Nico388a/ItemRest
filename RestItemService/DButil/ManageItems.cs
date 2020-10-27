using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ItemLib.Model;

namespace RestItemService.DButil
{
    public class ManageItems
    {

        private const string connStr =
            "Server=tcp:nicolaiserver.database.windows.net,1433;Initial Catalog=NicolaiDataBase;Persist Security Info=False;User ID=NicolaiAdmin;Password=Seacret1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private const string Get_All = "SELECT * FROM ITEM";
        private const string Get_One = "SELECT * FROM ITEM WHERE Item_ID = @ID";
        private const string Insert = "INSERT into ITEM VALUES(@ID, @Name, @Quality, @Quantity)";
        private const string Update = "Update ITEM set Item_ID = @ID, Name =@Name, Quality = @Quality, Quantity = @Quantity";
        private const string Delete = "Delete * FROM ITEM WHERE Item_ID = @ID";
        public IEnumerable<Item> Get()
        {
            List<Item> liste = new List<Item>();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(Get_All, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Item item = ReadNextElement(reader);
                    liste.Add(item);
                }

                reader.Close();
            }

            return liste;
        }

        private Item ReadNextElement(SqlDataReader reader)
        {
            Item item = new Item();
            item.ID = reader.GetInt32(0);
            item.Name = reader.GetString(1);
            item.Quality = reader.GetString(2);
            item.Quantity = reader.GetDouble(3);
            return item;
        }

        public Item Get(int id)
        {
            Item item = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(Get_One, conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    item = ReadNextElement(reader);
                }
                reader.Close();
            }

            return item;
        }
        //
        public void Post(Item value)
        {
            
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(Insert, conn))
            {
                cmd.Parameters.AddWithValue("@ID", value.ID);
                cmd.Parameters.AddWithValue("@Name", value.Name);
                cmd.Parameters.AddWithValue("@Qality", value.Quality);
                cmd.Parameters.AddWithValue("@Quantity", value.Quantity);
                conn.Open();
                cmd.ExecuteNonQuery();
                //Mangler noget 
                
            }
        }

        public void Put(int id, Item value)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(Update, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@ItemId", value.ID);
                cmd.Parameters.AddWithValue("@Name", value.Name);
                cmd.Parameters.AddWithValue("@Quality", value.Quality);
                cmd.Parameters.AddWithValue("@Quantity", value.Quantity);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();

            }
        }

        public void DeleteItem(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(Delete, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@ID", id);

                cmd.ExecuteNonQuery();
            }
            
        }
    }
}
