using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IncomeExpenseTrackerManager
{
    class IncomeData
    {

        string stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\tharushi umesha\OneDrive - Edith Cowan University\Documents\expenseTracker.mdf"";Integrated Security=True;Connect Timeout=30";

        public int ID { get; set; }

        public string Category { get; set; }
        public string Item { get; set; }

        public double Income { get; set; }

        public string Description { get; set; }
        public string DateIncome { get; set; }
        public string DateInserted { get; set; }

        public List<IncomeData> IncomeListData()
        {
            List<IncomeData> listData = new List<IncomeData>();

            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string selectData = "SELECT * FROM income";
                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        IncomeData iData = new IncomeData();
                        iData.ID = (int)reader["id"];
                        iData.Category = reader["category"].ToString();
                        iData.Item = reader["item"].ToString();
                        iData.Income = Convert.ToDouble(reader["income"]);
                        iData.Description = reader["description"].ToString();

                        // Safely format date
                        iData.DateIncome = Convert.ToDateTime(reader["date_income"]).ToString("MM-dd-yyyy");
                        iData.DateInserted = Convert.ToDateTime(reader["date_inserted"]).ToString("MM-dd-yyyy");

                        listData.Add(iData);
                    }
                }
            }

            return listData;
        }
    }
}
