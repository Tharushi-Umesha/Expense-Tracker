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
    }
}
