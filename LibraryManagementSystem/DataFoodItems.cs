using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FoodTicketingSystem
{
    class DataFoodItems
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\FoodD.mdf;Integrated Security=True;Connect Timeout=30";

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool ContainsAllergens { get; set; }
        public string Category { get; set; }
        public string image { get; set; }
        public DateTime DateAdded { get; set; }


        public List<DataFoodItems> GetAllFoodItems()
        {
            List<DataFoodItems> foodItems = new List<DataFoodItems>();

            try
            {
                using (SqlConnection connect = new SqlConnection(ConnectionString))
                {
                    connect.Open();
                    string query = "SELECT * FROM FoodItems";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataFoodItems food = new DataFoodItems
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = reader["name"].ToString(),
                                Description = reader["description"].ToString(),
                                Price = Convert.ToDecimal(reader["price"]),
                                ContainsAllergens = Convert.ToBoolean(reader["contains_allergens"]),
                                Category = reader["category"].ToString(),
                                image = reader["image"].ToString(),
                                DateAdded = Convert.ToDateTime(reader["date_added"])
                            };

                            foodItems.Add(food);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving food items: " + ex.Message);
            }

            return foodItems;
        }
    }
}
