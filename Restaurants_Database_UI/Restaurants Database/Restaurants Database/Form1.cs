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
        OrganizationRepo _org;
        RestaurantRepository _rest;
        FoodRepo _food;
        JobsRepo _jobs;
        StockItemsRepo _stock;
        EmployeeRepo _emp;
        SuppliersRepo _suppliers;

        public cDataBaseForm()
        {
            _org = new OrganizationRepo();
            _rest = new RestaurantRepository();
            _food = new FoodRepo();
            _jobs = new JobsRepo();
            _stock = new StockItemsRepo();
            _emp = new EmployeeRepo();
            _suppliers = new SuppliersRepo();

            InitializeComponent();

            cRestOrgComboBox.Items.Clear();



            IReadOnlyList<Organization> orgs = _org.RetrieveOrganizations();
            IReadOnlyList<Restaurant> rests = _rest.RetrieveRestaurants();
            IReadOnlyList<Employee> emps = _emp.RetrieveEmployee();
            IReadOnlyList<Jobs> jobs = _jobs.RetrieveJobs();

            foreach (var org in orgs)
            {
                cRestOrgComboBox.Items.Add(org.OrganizationName);
                orgListBox.Items.Add(org.OrganizationName);
            }
            foreach (var rest in rests)
            {
                cEmployRestComboBox.Items.Add(rest.RestaurantName);
                restListBox.Items.Add(rest.RestaurantName);
            }
            foreach (var emp in emps)
            {
                cEmployRestComboBox.Items.Add(emp.EmployeeName);
                restListBox.Items.Add(emp.EmployeeName);
            }
            foreach (var job in jobs)
            {
                jobsListBox.Items.Add(job.JobName);
                cEmployJobTitleComboBox.Items.Add(job.JobName);
            }
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
            if (!String.IsNullOrEmpty(cOrgNameTextBox.Text))
            {
                OrganizationRepo or = new OrganizationRepo();

                Organization o = or.CreateOrganization(cOrgNameTextBox.Text);

                IReadOnlyList<Organization> l = or.RetrieveOrganizations();
                //Organization org = o.GetOrganization(13);

                cOrgIdNumLabel.Text = o.OrganizationID.ToString();
                cRestOrgComboBox.Items.Add(cOrgNameTextBox.Text);
                orgListBox.Items.Add(cOrgNameTextBox.Text);
            }
            else
            {
                MessageBox.Show("Organization name cannot be empty");
            }

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
            Jobs j = _jobs.CreateJobs(cJobNameTextBox.Text, cJobSalaryNumUpDownBox.Value);
            cJobIdNumLabel.Text = j.JobTitleID.ToString();
            jobsListBox.Items.Add(j.JobName);
            cEmployJobTitleComboBox.Items.Add(j.JobName);
        }

        private void cJobsEditButton_Click(object sender, EventArgs e)
        {

        }

        private void cEmployeeAddButton_Click(object sender, EventArgs e)
        {
            Restaurant r = _rest.GetRestaurant(cEmployRestComboBox.Items[cEmployRestComboBox.SelectedIndex].ToString());
            Jobs j = _jobs.GetJobs(cEmployJobTitleComboBox.Items[cEmployJobTitleComboBox.SelectedIndex].ToString());
            Employee employee = _emp.CreateEmployee(r.RestaurantID, j.JobTitleID, cEmployeeNameTextBox.Text, Convert.ToInt32(seniorityUpDown.Value));

            cPersonIdNumLabel.Text = employee.PersonID.ToString();
            cEmployRestComboBox.Text = cEmployRestComboBox.Items[cEmployRestComboBox.SelectedIndex].ToString(); ;
            cEmployJobTitleComboBox.Text = cEmployJobTitleComboBox.Items[cEmployJobTitleComboBox.SelectedIndex].ToString();

            restListBox.Items.Add(r.RestaurantName);
        }

        private void cEmployeesEditButton_Click(object sender, EventArgs e)
        {

        }

        private void cRestAddButton_Click(object sender, EventArgs e)
        {
            Organization o = _org.GetOrganization(cRestOrgComboBox.Items[cRestOrgComboBox.SelectedIndex].ToString());
            bool isOp = string.Equals(cRestOpComboBox.Items[cRestOpComboBox.SelectedIndex].ToString(), "Yes") ? true : false;
            Restaurant r = _rest.CreateRestaurant(o.OrganizationID, cRestNameTextBox.Text, cDateFoundedTextBox.Text, isOp);
            restListBox.Items.Add(r.RestaurantName);
            cRestIdNumLabel.Text = r.RestaurantID.ToString();
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

        private void orgListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgName = orgListBox.Items[orgListBox.SelectedIndex].ToString();
            cOrgNameTextBox.Text = orgName;
            Organization o = _org.GetOrganization(orgName);
            cOrgIdNumLabel.Text = o.OrganizationID.ToString();
            cDateFoundedTextBox.Text = o.DateFounded;
        }

        private void cDateFoundedTextBox_TextChanged(object sender, EventArgs e)
        {

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void restListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string restName = restListBox.Items[restListBox.SelectedIndex].ToString();
            Restaurant r = _rest.GetRestaurant(restName);
            cRestIdNumLabel.Text = r.RestaurantID.ToString();
            cRestNameTextBox.Text = r.RestaurantName;
            cRestDateFoundedTextBox.Text = r.DateFounded;
            Organization o = _org.GetOrganizationByID(r.OrganizationID);
            cRestOrgComboBox.Text = o.OrganizationName;
            cRestOpComboBox.Text = r.IsOperational ? "Yes" : "No";
        }

        private void cEmployRestComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cEmploySeniorityLabel_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cRestaurantPage_Click(object sender, EventArgs e)
        {

        }
    }
}
