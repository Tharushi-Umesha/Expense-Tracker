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
            incomeThisMonthIncome();
            incomeYesterdayIncome();
            incomeThisYearIncome();
            expenseTodayExpense();
            expenseYesterdayExpense();
            expenseThisMonthExpense();
            expenseThisYearExpense();
            incomeTotalIncome();
            expenseTotalExpense();
        }

        public void refreshData() 
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            incomeTodayIncome();
            incomeThisMonthIncome();
            incomeYesterdayIncome();
            incomeThisYearIncome();
            expenseTodayExpense();
            expenseYesterdayExpense();
            expenseThisMonthExpense();
            expenseThisYearExpense();
            incomeTotalIncome();
            expenseTotalExpense();
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
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = "SELECT SUM(income) FROM income WHERE convert(DATE, date_income) = DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1)";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal yesterdayIncome = Convert.ToDecimal(result);
                        income_yesterday.Text = yesterdayIncome.ToString("C");
                    }
                    else
                    {
                        income_yesterday.Text = "$0.00";
                    }
                }
            }
        }

        public void incomeThisMonthIncome()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                DateTime today = DateTime.Today;
                DateTime startMonth = new DateTime(today.Year, today.Month, 1);
                DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);

                string query = $"SELECT SUM(income) FROM income WHERE date_income >= @startMonth AND date_income <= @endMonth";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@startMonth", startMonth);
                    cmd.Parameters.AddWithValue("@endMonth", endMonth);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal thisMonthIncome = Convert.ToDecimal(result);
                        income_monthly.Text = thisMonthIncome.ToString("C");
                    }
                    else
                    {
                        income_monthly.Text = "$0.00";
                    }
                }
            }
        }

        public void incomeThisYearIncome()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();
                DateTime today = DateTime.Today;
                DateTime startYear = new DateTime(today.Year, 1, 1);
                DateTime endYear = new DateTime(today.Year, 12, 31);
                string query = "SELECT SUM(income) FROM income WHERE date_income >= @startYear AND date_income <= @endYear";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@startYear", startYear);
                    cmd.Parameters.AddWithValue("@endYear", endYear);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal thisYearIncome = Convert.ToDecimal(result);
                        income_yearly.Text = thisYearIncome.ToString("C");
                    }
                    else
                    {
                        income_yearly.Text = "$0.00";
                    }
                }
            }

        }

        public void expenseTodayExpense()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();
                string query = "SELECT SUM(expense) FROM expenses WHERE CONVERT(DATE, date_expense) = @date_expense";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    DateTime today = DateTime.Today;
                    cmd.Parameters.AddWithValue("@date_expense", today);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        decimal todayExpense = Convert.ToDecimal(result);
                        expense_today.Text = todayExpense.ToString("C");
                    }
                    else
                    {
                        expense_today.Text = "$0.00";
                    }
                }
            }
        }

        public void expenseYesterdayExpense()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();
                string query = "SELECT SUM(expense) FROM expenses WHERE CONVERT(DATE, date_expense) = DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1)";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal yesterdayExpense = Convert.ToDecimal(result);
                        expense_yesterday.Text = yesterdayExpense.ToString("C");
                    }
                    else
                    {
                        expense_yesterday.Text = "$0.00";
                    }
                }
            }
        }

        public void expenseThisMonthExpense()
        {
            using(SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();
                DateTime today = DateTime.Today;
                DateTime startMonth = new DateTime(today.Year, today.Month, 1);
                DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);
                string query = "SELECT SUM(expense) FROM expenses WHERE date_expense >= @startMonth AND date_expense <= @endMonth";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@startMonth", startMonth);
                    cmd.Parameters.AddWithValue("@endMonth", endMonth);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal thisMonthExpense = Convert.ToDecimal(result);
                        expense_monthly.Text = thisMonthExpense.ToString("C");
                    }
                    else
                    {
                        expense_monthly.Text = "$0.00";
                    }
                }
            }
        }

        public void expenseThisYearExpense()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();
                DateTime today = DateTime.Today;
                DateTime startYear = new DateTime(today.Year, 1, 1);
                DateTime endYear = new DateTime(today.Year, 12, 31);
                string query = "SELECT SUM(expense) FROM expenses WHERE date_expense >= @startYear AND date_expense <= @endYear";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@startYear", startYear);
                    cmd.Parameters.AddWithValue("@endYear", endYear);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal thisYearExpense = Convert.ToDecimal(result);
                        expense_yearly.Text = thisYearExpense.ToString("C");
                    }
                    else
                    {
                        expense_yearly.Text = "$0.00";
                    }
                }
            }
        }

        public void incomeTotalIncome()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();
                string query = "SELECT SUM(income) FROM income";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal totalIncome = Convert.ToDecimal(result);
                        total_income.Text = totalIncome.ToString("C");
                    }
                    else
                    {
                        total_income.Text = "$0.00";
                    }
                }
            }
        }  
        
        public void expenseTotalExpense()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();
                string query = "SELECT SUM(expense) FROM expenses";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        decimal totalExpense = Convert.ToDecimal(result);
                        total_expense.Text = totalExpense.ToString("C");
                    }
                    else
                    {
                        total_expense.Text = "$0.00";
                    }
                }
            }
        }
    }
}
