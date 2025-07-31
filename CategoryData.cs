using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace IncomeExpenseTrackerManager
{
    class CategoryData
    {
        string stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\tharushi umesha\OneDrive - Edith Cowan University\Documents\expenseTracker.mdf"";Integrated Security=True;Connect Timeout=30";

        public int id { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string status { get; set; }

        public List<CategoryData> CategoryListData() 
        {
            List<CategoryData> listData = new List<CategoryData>();

            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();
                string selectData = "SELECT * FROM categories";
                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CategoryData cdata = new CategoryData();
                        cdata.id = (int)reader["id"];
                        cdata.category = reader["category"].ToString();
                        cdata.type = reader["type"].ToString();
                        cdata.status = reader["status"].ToString();
                        listData.Add(cdata);
                    }      
                }
            }
            return listData;
        }
    }
}
