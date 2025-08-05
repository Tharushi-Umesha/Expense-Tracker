using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace IncomeExpenseTrackerManager
{
    class ExpenseData
    {

        string stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\tharushi umesha\OneDrive - Edith Cowan University\Documents\expenseTracker.mdf"";Integrated Security=True;Connect Timeout=30";

        public int ID { get; set; }

        public string Category { get; set; }
        public string Item { get; set; }

        public double Expense { get; set; }

        public string Description { get; set; }
        public string DateExpense { get; set; }
        public string DateInserted { get; set; }

        public List<ExpenseData> ExpenseListData()
        {
            List<ExpenseData> listData = new List<ExpenseData>();

            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string selectData = "SELECT * FROM expenses";
                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ExpenseData eData = new ExpenseData();
                        eData.ID = (int)reader["id"];
                        eData.Category = reader["category"].ToString();
                        eData.Item = reader["item"].ToString();
                        eData.Expense = Convert.ToDouble(reader["expense"]);
                        eData.Description = reader["description"].ToString();

                        // Safely format date
                        eData.DateExpense = Convert.ToDateTime(reader["date_expense"]).ToString("MM-dd-yyyy");
                        eData.DateInserted = Convert.ToDateTime(reader["date_insert"]).ToString("MM-dd-yyyy");

                        listData.Add(eData);
                    }
                }
            }

            return listData;
        }

    }
}
