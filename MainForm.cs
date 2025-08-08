using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IncomeExpenseTrackerManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            displayUserName();
        }

        public void displayUserName() { 
            string getUserName = Form1.username;
            if (getUserName != null)
            {
                user_name.Text = getUserName;
            }
            else
            {
                user_name.Text = "Guest";
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want to Exit from The App?? ", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want to LogOut?? ", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();

                this.Hide();
            }
        }

       

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = true;
            categoryForm1.Visible = false;
            incomeForm1.Visible = false;
            expenseForm1.Visible = false;

            DashboardForm dForm = dashboardForm1 as DashboardForm;

            if (dForm != null)
            {
                dForm.refreshData();
            }

        }

        private void addCategory_btn_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = false;
            categoryForm1.Visible = true;
            incomeForm1.Visible = false;
            expenseForm1.Visible = false;

            CategoryForm cForm = categoryForm1 as CategoryForm;
            if (cForm != null)
            {
                cForm.refreshData();
            }

        }

        private void income_btn_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = false;
            categoryForm1.Visible = false;
            incomeForm1.Visible = true;
            expenseForm1.Visible = false;

            IncomeForm iForm = incomeForm1 as IncomeForm;
            if (iForm != null)
            {
                iForm.refreshData();
            }
        }

        private void expenses_btn_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = false;
            categoryForm1.Visible = false;
            incomeForm1.Visible = false;
            expenseForm1.Visible = true;

            ExpenseForm eForm = expenseForm1 as ExpenseForm;
            if (eForm != null)
            {
                eForm.refreshData();
            }
        }

        
    }
}
