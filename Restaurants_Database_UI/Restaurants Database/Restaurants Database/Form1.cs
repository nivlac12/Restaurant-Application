using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurants_Database
{
    public partial class cDataBaseForm : Form
    {
        public cDataBaseForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string shellName = "U:\\CIS 560\\Project\\Restaurant - Application\\Restaurants_Database_UI\\Restaurants Database\\Restaurants Database\\BuildDatabase.ps1";
            //string shellName = "..\\..\\BuildDatabase.ps1";
            //System.Diagnostics.Process.Start("C:\\windows\\system32\\windowspowershell\\v1.0\\powershell.exe ", "-noexit " + shellName);
            OrganizationRepo o = new OrganizationRepo();

            o.CreateOrganization("Buffet incorporated");
            IReadOnlyList<Organization> l = o.RetrieveOrganizations();
            Organization org = o.GetOrganization(13);
            MessageBox.Show(org.OrganizationName);
            MessageBox.Show(l.ToString());

            cRestOrgComboBox.Items.Add(cOrgNameTextBox.Text);

        }

        private void cRestNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void cRestDateFoundLabel_Click(object sender, EventArgs e)
        {

        }

        private void cJobsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cSupplierAddButton_Click(object sender, EventArgs e)
        {

        }

        private void cSupEditButton_Click(object sender, EventArgs e)
        {

        }

        private void cFoodAddButton_Click(object sender, EventArgs e)
        {

        }

        private void cFoodEditButton_Click(object sender, EventArgs e)
        {

        }

        private void cInventoryAddButton_Click(object sender, EventArgs e)
        {

        }

        private void cInvyEditButton_Click(object sender, EventArgs e)
        {

        }

        private void cJobAddButton_Click(object sender, EventArgs e)
        {

        }

        private void cJobsEditButton_Click(object sender, EventArgs e)
        {

        }

        private void cEmployeeAddButton_Click(object sender, EventArgs e)
        {

        }

        private void cEmployeesEditButton_Click(object sender, EventArgs e)
        {

        }

        private void cRestAddButton_Click(object sender, EventArgs e)
        {
            
        }

        private void cRestEditButton_Click(object sender, EventArgs e)
        {

        }

        private void cOrgEditButton_Click(object sender, EventArgs e)
        {

        }

        private void cRestDateFoundedTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void cOrgPage_Click(object sender, EventArgs e)
        {

        }

        private void cRestOrgComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cRestOpComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cRestNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
