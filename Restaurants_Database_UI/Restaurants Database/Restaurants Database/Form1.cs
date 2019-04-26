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
            Init_Tables init = new Init_Tables();

            InitializeComponent();

            cRestOrgComboBox.Items.Clear();

            init.initOrgs(_org);
            init.initRests(_rest);
            init.initJobs(_jobs);
            init.initSupps(_suppliers);
            init.initFoods(_food);

            IReadOnlyList<Organization> orgs = _org.RetrieveOrganizations();
            IReadOnlyList<Restaurant> rests = _rest.RetrieveRestaurants();
            IReadOnlyList<Jobs> jobs = _jobs.RetrieveJobs();
            init.initEmp(_emp, rests, jobs);
            IReadOnlyList<Employee> emps = _emp.RetrieveEmployee();
            IReadOnlyList<Supplier> supps = _suppliers.RetrieveSuppliers();
            IReadOnlyList<Food> foods = _food.RetrieveFood();

            foreach (var org in orgs)
            {
                cRestOrgComboBox.Items.Add(org.OrganizationName);
                orgListBox.Items.Add(org.OrganizationName);
            }
            foreach (var rest in rests)
            {
                cEmployRestComboBox.Items.Add(rest.RestaurantName);
                restListBox.Items.Add(rest.RestaurantName);
                cInventoryRestComboBox.Items.Add(rest.RestaurantName);
            }
            foreach (var emp in emps)
            {
                empListBox.Items.Add(emp.EmployeeName);
            }
            foreach (var job in jobs)
            {
                jobsListBox.Items.Add(job.JobName);
                cEmployJobTitleComboBox.Items.Add(job.JobName);
            }
            foreach (var supp in supps)
            {
                suppListBox.Items.Add(supp.SuppliersName);
                cFoodSupplierComboBox.Items.Add(supp.SuppliersName);
            }
            foreach (var food in foods)
            {
                foodListBox.Items.Add(food.FoodName);
                cInventoryFoodComboBox.Items.Add(food.FoodName);
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
            Supplier supp = _suppliers.CreateSupplier(cSupNameTextBox.Text);
            cSupplierIdNumLabel.Text = supp.SuppliersID.ToString();
            cFoodSupplierComboBox.Items.Add(supp.SuppliersName);
            suppListBox.Items.Add(supp.SuppliersName);
        }

        private void cSupEditButton_Click(object sender, EventArgs e)
        {
            Supplier supp = _suppliers.CreateSupplier(cSupNameTextBox.Text);
            cSupplierIdNumLabel.Text = supp.SuppliersID.ToString();
            suppListBox.Items.Add(supp.SuppliersName);
            cFoodSupplierComboBox.Items.Add(supp.SuppliersName);
        }

        private void cFoodAddButton_Click(object sender, EventArgs e)
        {
            Supplier supp = _suppliers.GetSupplier(cFoodSupplierComboBox.Text);
            Food f = _food.CreateFood(supp.SuppliersID, cFoodNameTextBox.Text, cFoodSupPriceNumUpDownBox.Value, cFoodRetailNumUpDownBox.Value);
            cFoodIdNumLabel.Text = f.FoodID.ToString();
            foodListBox.Items.Add(f.FoodName);
            cInventoryFoodComboBox.Items.Add(f.FoodName);
        }

        private void cFoodEditButton_Click(object sender, EventArgs e)
        {

        }

        private void cInventoryAddButton_Click(object sender, EventArgs e)
        {
            Food f = _food.GetFood(cInventoryFoodComboBox.Text);
            Restaurant r = _rest.GetRestaurant(cInventoryRestComboBox.Text);
            StockItem si = _stock.CreateStockItems(f.FoodID, r.RestaurantID, Convert.ToInt32(cInventoryQuantityNumUpDownBox.Value));

            invListBox.Items.Add(r.RestaurantName + ": " + f.FoodName);
            cInventoryIdNumLabel.Text = si.InventoryID.ToString();
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
            empListBox.Items.Add(employee.EmployeeName);
        }

        private void cEmployeesEditButton_Click(object sender, EventArgs e)
        {

        }

        private void cRestAddButton_Click(object sender, EventArgs e)
        {
            Organization o = _org.GetOrganization(cRestOrgComboBox.Items[cRestOrgComboBox.SelectedIndex].ToString());
            bool isOp = string.Equals(cRestOpComboBox.Items[cRestOpComboBox.SelectedIndex].ToString(), "Yes") ? true : false;
            Restaurant r = _rest.CreateRestaurant(o.OrganizationID, cRestNameTextBox.Text, isOp);
            restListBox.Items.Add(r.RestaurantName);
            cRestIdNumLabel.Text = r.RestaurantID.ToString();
            cInventoryRestComboBox.Items.Add(r.RestaurantName);
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

        private void cCalcSupplierSalesButton_Click(object sender, EventArgs e)
        {

        }

        private void cCalcOrgExpendButton_Click(object sender, EventArgs e)
        {

        }

        private void cCalcRestExpendButton_Click(object sender, EventArgs e)
        {

        }

        private void cRestOpLabel_Click(object sender, EventArgs e)
        {

        }

        private void cDateFoundedLabel_Click(object sender, EventArgs e)
        {

        }

        private void cOrgIdLabel_Click(object sender, EventArgs e)
        {

        }

        private void cRestaurantIdLabel_Click(object sender, EventArgs e)
        {

        }

        private void empListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employee emp = _emp.GetEmployee(empListBox.Items[empListBox.SelectedIndex].ToString());
            cPersonIdNumLabel.Text = emp.PersonID.ToString();
            cEmployeeNameTextBox.Text = emp.EmployeeName;
            cEmployRestComboBox.Text = _rest.GetRestaurantByID(emp.RestaurantID).RestaurantName;
            seniorityUpDown.Value = emp.Seniority;
            cEmployJobTitleComboBox.Text = _jobs.GetJobByID(emp.JobTitleID).JobName;
        }

        private void jobsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Jobs j = _jobs.GetJobs(jobsListBox.Items[jobsListBox.SelectedIndex].ToString());
            cJobIdNumLabel.Text = j.JobTitleID.ToString();
            cJobNameTextBox.Text = j.JobName;
            cJobSalaryNumUpDownBox.Value = j.Salary;
        }

        private void suppListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Supplier supp = _suppliers.GetSupplier(suppListBox.Items[suppListBox.SelectedIndex].ToString());
            cSupNameTextBox.Text = supp.SuppliersName;
            cSupplierIdNumLabel.Text = supp.SuppliersID.ToString();
        }

        private void foodListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Food f = _food.GetFood(foodListBox.Items[foodListBox.SelectedIndex].ToString());
            Supplier supp = _suppliers.GetSupplierByID(f.SupplierID);
            cFoodSupplierComboBox.Text = supp.SuppliersName;
            cFoodIdNumLabel.Text = f.FoodID.ToString();
            cFoodNameTextBox.Text = f.FoodName;
            cFoodSupPriceNumUpDownBox.Value = f.SupplierPrice;
            cFoodRetailNumUpDownBox.Value = f.RetailPrice;
        }

        private void invListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] restAndFood = invListBox.Items[invListBox.SelectedIndex].ToString().Split(':');
            Restaurant r = _rest.GetRestaurant(restAndFood[0]);
            Food f = _food.GetFood(restAndFood[1].Substring(1));
            cInventoryFoodComboBox.Text = f.FoodName;
            cInventoryRestComboBox.Text = r.RestaurantName;
            StockItem si = _stock.GetStockItem(f.FoodName, r.RestaurantName);
            cInventoryIdNumLabel.Text = si.InventoryID.ToString();
            cInventoryQuantityNumUpDownBox.Value = si.Quantity;
        }
    }
}
