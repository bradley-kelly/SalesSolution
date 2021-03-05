using DataProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationProject
{
    public partial class CRUD : Form
    {
        SalesContext salesContext = new SalesContext();

        public CRUD()
        {
            InitializeComponent();
        }
        void GetCustomers()
        {
            dataGridView1.DataSource = salesContext.Customers.OrderBy(a => a.LastName).ToList();            
        }
        private void CRUD_Load(object sender, EventArgs e)
        {
            GetCustomers();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                Customer delete = new Customer();
                delete = salesContext.Customers.SingleOrDefault(a => a.Id == Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value));
                salesContext.Customers.Remove(delete);
                salesContext.SaveChanges();
                GetCustomers();
            }
            catch (Exception deleteException)
            {
                MessageBox.Show(deleteException.Message, "exception has occurred");
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if ((firstNameBox.Text.ToString() == null || firstNameBox.Text.ToString() == "")
                    || (lastNameBox.Text.ToString() == null || lastNameBox.Text.ToString() == ""))
                {
                    MessageBox.Show("Enter a first and last name.");
                }
                else
                {
                    Customer customer = new Customer();
                    customer.FirstName = firstNameBox.Text.ToString();
                    customer.LastName = lastNameBox.Text.ToString();
                    customer.City = cityBox.Text.ToString();
                    customer.Country = countryBox.Text.ToString();
                    customer.Phone = phoneBox.Text.ToString();
                    salesContext.Add(customer);
                    salesContext.SaveChanges();
                    GetCustomers();
                }
            }
            catch (Exception addException)
            {
                MessageBox.Show(addException.Message, "exception has occurred");
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                salesContext.SaveChanges();
                GetCustomers();
            }
            catch (Exception updateException)
            {
                MessageBox.Show(updateException.Message, "exception has occurred");
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (lastNameFilter.Checked)
            {
                dataGridView1.DataSource = salesContext.Customers.Where(a => a.LastName == searchBox.Text.ToString()).ToList();
            }
            else if (cityFilter.Checked)
            {
                dataGridView1.DataSource = salesContext.Customers.Where(a => a.City == searchBox.Text.ToString()).ToList();
            }
            else if (startingLetterFilter.Checked && searchBox.TextLength > 0)
            {
                dataGridView1.DataSource = salesContext.Customers.Where(a => a.LastName.StartsWith(searchBox.Text.First().ToString())).ToList();
            }
            else
            {
                GetCustomers();
            }
        }
    }
}