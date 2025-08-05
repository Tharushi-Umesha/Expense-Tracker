using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace IncomeExpenseTrackerManager
{
    public partial class DashboardForm : UserControl
    {
        string stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\tharushi umesha\OneDrive - Edith Cowan University\Documents\expenseTracker.mdf"";Integrated Security=True;Connect Timeout=30";
        public DashboardForm()
        {
            InitializeComponent();
            incomeTodayIncome();
        }

        public void incomeTodayIncome()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = "SELECT SUM(income) FROM income WHERE date_income = @date_income";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    DateTime today = DateTime.Today;
                    cmd.Parameters.AddWithValue("@date_income", today);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal todayIncome = Convert.ToDecimal(result);
                        income_today.Text = todayIncome.ToString("C");
                    }
                    else
                    {
                        income_today.Text = "$0.00";
                    }
                }
            }
        }

        public void incomeYesterdayIncome()
        {
            using(SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = "SELECT SUM(income) FROM income WHERE convert(DATE, date_income) = DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1)"; 

                using(SqlCommand cmd = new SqlCommand(query, connect))
                {
                    object result = cmd.ExecuteScalar() ;

                    if (result != DBNull.Value)
                    {
                        decimal yesterdayIncome = Convert.ToDecimal(result);
                    }
                }
            }
        }

    }
}
