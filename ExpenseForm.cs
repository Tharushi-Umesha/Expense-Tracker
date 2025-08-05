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
    public partial class ExpenseForm : UserControl
    {
        string stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\tharushi umesha\OneDrive - Edith Cowan University\Documents\expenseTracker.mdf"";Integrated Security=True;Connect Timeout=30";


        public ExpenseForm()
        {
            InitializeComponent();
            displayCategoryList();
            displayExpenseData();
        }

        public void displayExpenseData()
        {
            ExpenseData eData = new ExpenseData();
            List<ExpenseData> listData = eData.ExpenseListData();

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

                    cmd.Parameters.AddWithValue("@type", "Expenses");
                    cmd.Parameters.AddWithValue("@status", "Active");

                    expense_category.Items.Clear();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        expense_category.Items.Add(reader["category"].ToString());
                    }
                }
                connect.Close();
            }

        }

        private void expense_addBtn_Click(object sender, EventArgs e)
        {
            if (expense_category.SelectedIndex == -1 || expense_item.Text == "" || expense_expense.Text == "" || expense_description.Text == "")
            {
                MessageBox.Show("Please fill all the fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                using (SqlConnection connect = new SqlConnection(stringConnection))
                {
                    connect.Open();

                    string insertData = "INSERT INTO expenses (category, item, expense, description, date_expense, date_insert) VALUES (@category, @item, @expense, @description, @date_expense, @date_insert)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@category", expense_category.SelectedItem);
                        cmd.Parameters.AddWithValue("@item", expense_item.Text);
                        cmd.Parameters.AddWithValue("@expense", Convert.ToDecimal(expense_expense.Text));
                        cmd.Parameters.AddWithValue("@description", expense_description.Text);
                        cmd.Parameters.AddWithValue("@date_expense", expense_date.Value);

                        DateTime today = DateTime.Now;
                        cmd.Parameters.AddWithValue("@date_insert", today);

                        cmd.ExecuteNonQuery();
                        clearFields();

                        MessageBox.Show("Added Successfully", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    connect.Close();
                }
            }
            displayExpenseData();
        }

        public void clearFields()
        {
            expense_category.SelectedIndex = -1;
            expense_item.Text = "";
            expense_expense.Text = "";
            expense_description.Text = "";
        }

        private void expense_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void expense_updateBtn_Click(object sender, EventArgs e)
        {
            if (expense_category.SelectedIndex == -1 || expense_item.Text == "" || expense_expense.Text == "" || expense_description.Text == "")
            {
                MessageBox.Show("Please select a field first .", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (MessageBox.Show("Are you sure you want to update ID: " + getID + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection connect = new SqlConnection(stringConnection))
                    {
                        connect.Open();

                        string updateData = "UPDATE expenses SET category = @category, item = @item, expense = @expense, description = @description, date_expense = @date_expense WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(updateData, connect))
                        {
                            cmd.Parameters.AddWithValue("@id", getID);
                            cmd.Parameters.AddWithValue("@category", expense_category.SelectedItem);
                            cmd.Parameters.AddWithValue("@item", expense_item.Text);
                            cmd.Parameters.AddWithValue("@expense", Convert.ToDecimal(expense_expense.Text));
                            cmd.Parameters.AddWithValue("@description", expense_description.Text);
                            cmd.Parameters.AddWithValue("@date_expense", expense_date.Value);



                            cmd.ExecuteNonQuery();
                            clearFields();

                            MessageBox.Show("Updated Successfully", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        connect.Close();
                    }
                }
            }
            displayExpenseData();
        }

        private int getID = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                getID = Convert.ToInt32(row.Cells[0].Value);  // ID
                expense_category.SelectedItem = row.Cells[1].Value.ToString(); // Category
                expense_item.Text = row.Cells[2].Value.ToString(); // Item
                expense_expense.Text = Convert.ToDecimal(row.Cells[3].Value).ToString(); // expense as string
                expense_description.Text = row.Cells[4].Value.ToString(); // Description
                expense_date.Value = Convert.ToDateTime(row.Cells[5].Value); // Date
            }
        }

        private void expense_deleteBtn_Click(object sender, EventArgs e)
        {
            if (expense_category.SelectedIndex == -1 || expense_item.Text == "" || expense_expense.Text == "" || expense_description.Text == "")
            {
                MessageBox.Show("Please select a field first .", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Delete ID: " + getID + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection connect = new SqlConnection(stringConnection))
                    {
                        connect.Open();

                        string deleteData = "DELETE FROM expenses WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(deleteData, connect))
                        {
                            cmd.Parameters.AddWithValue("@id", getID);
                            cmd.ExecuteNonQuery();
                            clearFields();

                            MessageBox.Show("Deleted Successfully", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        connect.Close();
                    }
                }
            }
            displayExpenseData();
        }
    }
}
