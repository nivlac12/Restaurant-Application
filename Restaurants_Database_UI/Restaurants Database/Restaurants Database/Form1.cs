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
            

            if (!String.IsNullOrEmpty(cSupNameTextBox.Text))
            {
                Supplier supp = _suppliers.CreateSupplier(cSupNameTextBox.Text);
                cSupplierIdNumLabel.Text = supp.SuppliersID.ToString();
                cFoodSupplierComboBox.Items.Add(supp.SuppliersName);
                suppListBox.Items.Add(supp.SuppliersName);
            }
            else
            {
                MessageBox.Show("Please make sure to fill out the supplier name section.");
            }
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

            if (!String.IsNullOrEmpty(cFoodNameTextBox.Text) && !String.IsNullOrEmpty(cFoodSupplierComboBox.Text))
            {
                Supplier supp = _suppliers.GetSupplier(cFoodSupplierComboBox.Text);
                Food f = _food.CreateFood(supp.SuppliersID, cFoodNameTextBox.Text, Convert.ToDouble(cFoodSupPriceNumUpDownBox.Value), Convert.ToDouble(cFoodRetailNumUpDownBox.Value));
                cFoodIdNumLabel.Text = f.FoodID.ToString();
                foodListBox.Items.Add(f.FoodName);
                cInventoryFoodComboBox.Items.Add(f.FoodName);
            }
            else
            {
                MessageBox.Show("Please make sure to fill out the food name section, in addition, select a supplier from the list.");
            }
        }

        private void cFoodEditButton_Click(object sender, EventArgs e)
        {
            int foodID = Convert.ToInt32(cFoodIdNumLabel.Text);
            string originalName = _food.GetFoodByID();
        }

        private void cInventoryAddButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cInventoryFoodComboBox.Text) && !String.IsNullOrEmpty(cInventoryRestComboBox.Text))
            {
                Food f = _food.GetFood(cInventoryFoodComboBox.Text);
                Restaurant r = _rest.GetRestaurant(cInventoryRestComboBox.Text);
                StockItem si = _stock.CreateStockItems(f.FoodID, r.RestaurantID, Convert.ToInt32(cInventoryQuantityNumUpDownBox.Value));

                invListBox.Items.Add(r.RestaurantName + ": " + f.FoodName);
                cInventoryIdNumLabel.Text = si.InventoryID.ToString();
            }
            else
            {
                MessageBox.Show("select a food and a restaurant from their respective list.");
            }
                
        }

        private void cInvyEditButton_Click(object sender, EventArgs e)
        {
            int itemID = Convert.ToInt32(cInventoryIdNumLabel.Text);
            StockItem si = _stock.GetStockItemByID(itemID);
            int originalRestID = si.RestaurantID;
            int originalFoodID = si.FoodID;
            string originalRestName = _rest.GetRestaurantByID(originalRestID).RestaurantName;
            string originalFoodName = _food.GetFoodByID(originalFoodID).FoodName;
            string newRestName = cInventoryRestComboBox.Text;
            string newFoodName = cInventoryFoodComboBox.Text;
            int newRestID = _rest.GetRestaurant(newRestName).RestaurantID;
            int newFoodID = _food.GetFood(newFoodName).FoodID;

            _stock.UpdateStockItem(itemID, newFoodID, newRestID, Convert.ToInt32(cInventoryQuantityNumUpDownBox.Value));
            int len = invListBox.Items.Count;
            string temp;
            for(int i = 0; i<len; i++)
            {
                temp = cInventoryRestComboBox.Text + ": " + cInventoryFoodComboBox.Text;
                if (Equals(temp, invListBox.Items[i]))
                {
                    invListBox.Items[i] = temp;
                }
            }

        }

        private void cJobAddButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cJobNameTextBox.Text))
            {
                Jobs j = _jobs.CreateJobs(cJobNameTextBox.Text, Convert.ToDouble(cJobSalaryNumUpDownBox.Value));
                cJobIdNumLabel.Text = j.JobTitleID.ToString();
                jobsListBox.Items.Add(j.JobName);
                cEmployJobTitleComboBox.Items.Add(j.JobName);
            }
            else
            {
                MessageBox.Show("Please make sure to fill out the job name section");
            }
        }

        private void cJobsEditButton_Click(object sender, EventArgs e)
        {
            int jobID = Convert.ToInt32(cJobIdNumLabel.Text);
            string originalName = _jobs.GetJobByID(jobID).JobName;
            _jobs.UpdateJob(jobID, cJobNameTextBox.Text, Convert.ToDouble(cJobSalaryNumUpDownBox.Value));
            int len = jobsListBox.Items.Count;
            for(int i = 0; i<len; ++i)
            {
                if(Equals(originalName, jobsListBox.Items[i]))
                {
                    jobsListBox.Items[i] = cJobNameTextBox.Text;
                    cEmployJobTitleComboBox.Items[i] = cJobNameTextBox.Text;
                }
            }
        }

        private void cEmployeeAddButton_Click(object sender, EventArgs e)
        {
            

            if(!String.IsNullOrEmpty(cEmployRestComboBox.Text) && !String.IsNullOrEmpty(cEmployeeNameTextBox.Text) && !String.IsNullOrEmpty(cEmployJobTitleComboBox.Text))
            {
                Restaurant r = _rest.GetRestaurant(cEmployRestComboBox.Items[cEmployRestComboBox.SelectedIndex].ToString());
                Jobs j = _jobs.GetJobs(cEmployJobTitleComboBox.Items[cEmployJobTitleComboBox.SelectedIndex].ToString());
                Employee employee = _emp.CreateEmployee(r.RestaurantID, j.JobTitleID, cEmployeeNameTextBox.Text, Convert.ToInt32(seniorityUpDown.Value));

                cPersonIdNumLabel.Text = employee.PersonID.ToString();
                cEmployRestComboBox.Text = cEmployRestComboBox.Items[cEmployRestComboBox.SelectedIndex].ToString(); ;
                cEmployJobTitleComboBox.Text = cEmployJobTitleComboBox.Items[cEmployJobTitleComboBox.SelectedIndex].ToString();
                empListBox.Items.Add(employee.EmployeeName);
            }
            else
            {
                MessageBox.Show("Please make sure to fill out the emplyee's name section, in addition, select a restaurant and a job from their respective list.");
            }
        }

        private void cEmployeesEditButton_Click(object sender, EventArgs e)
        {
            int empID = Convert.ToInt32(cPersonIdNumLabel.Text);
            string originalName = _emp.GetEmployeeByID(empID).EmployeeName;
            _emp.UpdateEmployee(empID,
                                _rest.GetRestaurant(cEmployRestComboBox.Text).RestaurantID,
                                _jobs.GetJobs(cEmployJobTitleComboBox.Text).JobTitleID,
                                cEmployeeNameTextBox.Text,
                                Convert.ToInt32(seniorityUpDown.Value)
                                );
            int len = empListBox.Items.Count;
            for(int i = 0; i<len; i++)
            {
                if(Equals(originalName, empListBox.Items[i]))
                {
                    empListBox.Items[i] = cEmployeeNameTextBox.Text;
                }
            }
        }

        private void cRestAddButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cRestNameTextBox.Text) && !String.IsNullOrEmpty(cRestOpComboBox.Text))
            {
                Organization o = _org.GetOrganization(cRestOrgComboBox.Items[cRestOrgComboBox.SelectedIndex].ToString());
                bool isOp = string.Equals(cRestOpComboBox.Text, "Yes") ? true : false;
                Restaurant r = _rest.CreateRestaurant(o.OrganizationID, cRestNameTextBox.Text, isOp);
                restListBox.Items.Add(r.RestaurantName);
                cRestIdNumLabel.Text = r.RestaurantID.ToString();
                cInventoryRestComboBox.Items.Add(r.RestaurantName);
            }

            else
            {
                MessageBox.Show("Please make sure to fill out the restaurant name section, in addition, select an organization from the list of organizations, and select whether the organization is operational.");
            }
        }

        private void cRestEditButton_Click(object sender, EventArgs e)
        {
            string originalName = _rest.GetRestaurantByID(Convert.ToInt32(cRestIdNumLabel.Text)).RestaurantName;
            _rest.UpdateRestaurant(Convert.ToInt32(cRestIdNumLabel.Text),
                                   _org.GetOrganization(cRestOrgComboBox.Text).OrganizationID,
                                   cRestNameTextBox.Text,
                                   Equals(cRestOpComboBox.Text, "Yes") ? true : false
                                   );
            int len = restListBox.Items.Count;
            for(int i = 0; i<len; i++)
            {
                if(Equals(originalName, restListBox.Items[i]))
                {
                    restListBox.Items[i] = cRestNameTextBox.Text;
                    cEmployRestComboBox.Items[i] = cRestNameTextBox.Text;
                }
            }
        }

        private void cOrgEditButton_Click(object sender, EventArgs e)
        {
            string originalName = _org.GetOrganizationByID(Convert.ToInt32(cOrgIdNumLabel.Text)).OrganizationName;
            _org.UpdateOrganization(Convert.ToInt32(cOrgIdNumLabel.Text), cOrgNameTextBox.Text);
            int len = orgListBox.Items.Count;
            for(int i = 0; i<len; i++)
            {
                if(Equals(originalName, orgListBox.Items[i]))
                {
                    orgListBox.Items[i] = cOrgNameTextBox.Text;
                    cRestOrgComboBox.Items[i] = cOrgNameTextBox.Text;
                }
            }
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
            if (orgListBox.SelectedIndex != -1)
            {
                string orgName = orgListBox.Items[orgListBox.SelectedIndex].ToString();
                cOrgNameTextBox.Text = orgName;
                Organization o = _org.GetOrganization(orgName);
                cOrgIdNumLabel.Text = o.OrganizationID.ToString();
                cDateFoundedTextBox.Text = o.DateFounded;
            }
            
        }

        private void cDateFoundedTextBox_TextChanged(object sender, EventArgs e)
        {

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void restListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(restListBox.SelectedIndex != -1)
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
            if (empListBox.SelectedIndex != -1)
            {
                Employee emp = _emp.GetEmployee(empListBox.Items[empListBox.SelectedIndex].ToString());
                cPersonIdNumLabel.Text = emp.PersonID.ToString();
                cEmployeeNameTextBox.Text = emp.EmployeeName;
                cEmployRestComboBox.Text = _rest.GetRestaurantByID(emp.RestaurantID).RestaurantName;
                seniorityUpDown.Value = emp.Seniority;
                cEmployJobTitleComboBox.Text = _jobs.GetJobByID(emp.JobTitleID).JobName;
            }
        }

        private void jobsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (jobsListBox.SelectedIndex != -1)
            {
                Jobs j = _jobs.GetJobs(jobsListBox.Items[jobsListBox.SelectedIndex].ToString());
                cJobIdNumLabel.Text = j.JobTitleID.ToString();
                cJobNameTextBox.Text = j.JobName;
                cJobSalaryNumUpDownBox.Value = Convert.ToDecimal(j.Salary);
            }
            
        }

        private void suppListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suppListBox.SelectedIndex != -1)
            {
                Supplier supp = _suppliers.GetSupplier(suppListBox.Items[suppListBox.SelectedIndex].ToString());
                cSupNameTextBox.Text = supp.SuppliersName;
                cSupplierIdNumLabel.Text = supp.SuppliersID.ToString();
            }
            
        }

        private void foodListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (foodListBox.SelectedIndex != -1)
            {
                Food f = _food.GetFood(foodListBox.Items[foodListBox.SelectedIndex].ToString());
                Supplier supp = _suppliers.GetSupplierByID(f.SupplierID);
                cFoodSupplierComboBox.Text = supp.SuppliersName;
                cFoodIdNumLabel.Text = f.FoodID.ToString();
                cFoodNameTextBox.Text = f.FoodName;
                cFoodSupPriceNumUpDownBox.Value = Convert.ToDecimal(f.SupplierPrice);
                cFoodRetailNumUpDownBox.Value = Convert.ToDecimal(f.RetailPrice);
            }
            
        }

        private void invListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (invListBox.SelectedIndex != -1)
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

        private void cFoodSupPriceNumUpDownBox_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
