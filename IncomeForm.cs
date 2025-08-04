using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IncomeExpenseTrackerManager
{
    public partial class IncomeForm : UserControl
    {
        string stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\tharushi umesha\OneDrive - Edith Cowan University\Documents\expenseTracker.mdf"";Integrated Security=True;Connect Timeout=30";

        public IncomeForm()
        {
            InitializeComponent();

            displayCategoryList();

            displayIncomeData();
        }

        public void displayIncomeData()
        {
            IncomeData iData = new IncomeData();
            List<IncomeData> listData = iData.IncomeListData();

            dataGridView1.DataSource = listData;
        }

        public void displayCategoryList()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string selectData = "SELECT category FROM categories WHERE type = @type AND status = @status";

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {

                    cmd.Parameters.AddWithValue("@type", "Income");
                    cmd.Parameters.AddWithValue("@status", "Active");

                    income_category.Items.Clear();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        income_category.Items.Add(reader["category"].ToString());
                    }
                }
                connect.Close();
            }
        }

        private void income_addBtn_Click(object sender, EventArgs e)
        {
            if (income_category.SelectedIndex == -1 || income_item.Text == "" || income_income.Text == "" || income_description.Text == "")
            {
                MessageBox.Show("Please fill all the fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                using (SqlConnection connect = new SqlConnection(stringConnection))
                {
                    connect.Open();

                    string insertData = "INSERT INTO income (category, item, income, description, date_income, date_inserted) VALUES (@category, @item, @income, @description, @date_income, @date_inserted)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@category", income_category.SelectedItem);
                        cmd.Parameters.AddWithValue("@item", income_item.Text);
                        cmd.Parameters.AddWithValue("@income", Convert.ToDecimal(income_income.Text));
                        cmd.Parameters.AddWithValue("@description", income_description.Text);
                        cmd.Parameters.AddWithValue("@date_income", income_date.Value);

                        DateTime today = DateTime.Now;
                        cmd.Parameters.AddWithValue("@date_inserted", today);

                        cmd.ExecuteNonQuery();
                        clearFields();

                        MessageBox.Show("Added Successfully", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    connect.Close();
                }

               
            }

        }

        public void clearFields()
        {
            income_category.SelectedIndex = -1;
            income_item.Text = "";
            income_income.Text = "";
            income_description.Text = "";
        }

        private void income_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void income_updateBtn_Click(object sender, EventArgs e)
        {
            if (income_category.SelectedIndex == -1 || income_item.Text == "" || income_income.Text == "" || income_description.Text == "")
            {
                MessageBox.Show("Please select a field first .", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                using (SqlConnection connect = new SqlConnection(stringConnection))
                {
                    connect.Open();

                    string updateData = "UPDATE income SET category = @category, item = @item, income = @income, description = @description, date_income = @date_income WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(updateData, connect))
                    {
                        cmd.Parameters.AddWithValue("@id", getID);
                        cmd.Parameters.AddWithValue("@category", income_category.SelectedItem);
                        cmd.Parameters.AddWithValue("@item", income_item.Text);
                        cmd.Parameters.AddWithValue("@income", Convert.ToDecimal(income_income.Text));
                        cmd.Parameters.AddWithValue("@description", income_description.Text);
                        cmd.Parameters.AddWithValue("@date_income", income_date.Value);

                        

                        cmd.ExecuteNonQuery();
                        clearFields();

                        MessageBox.Show("Updated Successfully", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    connect.Close();
                }


            }
            displayIncomeData();
        }

        private int getID = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                getID = Convert.ToInt32(row.Cells[0].Value);  // ID
                income_category.SelectedItem = row.Cells[1].Value.ToString(); // Category
                income_item.Text = row.Cells[2].Value.ToString(); // Item
                income_income.Text = Convert.ToDecimal(row.Cells[3].Value).ToString(); // Income as string
                income_description.Text = row.Cells[4].Value.ToString(); // Description
                income_date.Value = Convert.ToDateTime(row.Cells[5].Value); // Date
            }
        }
    }
}
