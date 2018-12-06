using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;


namespace ATMCustomerCreationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CustApp : Window
    {

        DataSet ds;

        private void winCust_Load(object sender, EventArgs e)
        {
           

        }

        public void loadCustomersDel()
        {
            cboCustomersDel.Items.Clear();
            for (int i = 0; i < ds.Tables["ss"].Rows.Count; i++)
                cboCustomersDel.Items.Add(ds.Tables[0].Rows[i][0].ToString().PadLeft(3, '0') + " " + ds.Tables[0].Rows[i][1].ToString() + " " + ds.Tables[0].Rows[i][2].ToString());

        }

        public void loadCustomers()
        {
            DataSet ds = new DataSet();
            ds = Customer.getCustomersDate(ds);


            cboCustomers.Items.Clear();
            for (int i = 0; i < ds.Tables["ss"].Rows.Count; i++)
               cboCustomers.Items.Add(ds.Tables[0].Rows[i][0].ToString().PadLeft(3, '0') + " " + ds.Tables[0].Rows[i][1].ToString() + " " + ds.Tables[0].Rows[i][3].ToString());

           

        }

        public CustApp()
        {
            InitializeComponent();
            txtCustID.Text = Customer.nextCust().ToString("139");

            loadCustomers();
            loadCustomersDel();

            ds = new DataSet();
            ds = Customer.getAllCustomers(ds);

            dataCustomers.ItemsSource = getCustomers(ds).Tables["ss"].AsDataView();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
             
            Application.Current.Shutdown();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSurname_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCustID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void radM_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radF_Checked(object sender, RoutedEventArgs e)
        {

        }

        public bool IsPhoneNo(String txtphoneNo)
        {
            if (Regex.IsMatch(txtphoneNo, @"|^\s *\(?\s *\d{ 1,4}\s *\)?\s *[\d\s]{ 5,10}\s *$?|", RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text.Equals(""))
            {
                MessageBox.Show("No first name written! Please enter the first name!", "Customer Error", MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                txtFirstName.Focus();
                return;
            }

           else if (txtSurname.Text.Equals(""))
            {
                MessageBox.Show("No last name written! Please enter the last name!", "Customer Error", MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                txtSurname.Focus();
                return;
            }


            else if (txtAdd.Text.Equals(""))
            {
                MessageBox.Show("No address written! Please enter the address!", "Customer Error", MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                txtAdd.Focus();
                return;
            }


            else if (!IsPhoneNo(txtPhoneNo.Text))
            //else if (txtPhoneNo.Text.Equals(""))
            {
                MessageBox.Show("No phone number detected! This phone number must be entered!", "Missing Phone Number", MessageBoxButton.OK,
               MessageBoxImage.Error);
                txtPhoneNo.Focus();
                return;

            }


            Customer cust = new Customer();

            cust.setCustID(Convert.ToInt32(txtCustID.Text));
            cust.setFirstName(txtFirstName.Text);
            cust.setLastName(txtSurname.Text);
            cust.setGender(Convert.ToString(cboGender.Text.Substring(0,1)));
            cust.setAddress(txtAdd.Text);
            cust.setPhoneNo(Convert.ToString(txtPhoneNo.Text));
            cust.setDateTime(Convert.ToDateTime(dateDOB.SelectedDate.Value.ToString("dd-MM-yyyy")));


            try
            {
                cust.regCustomer();


                MessageBox.Show("CustomerID: " + txtCustID.Text + "\nFirst Name: " + txtFirstName.Text + "\nSurname: " + txtSurname.Text
                     + "\nGender: " + cboGender.SelectedItem.ToString() + "\nAddress: " + txtAdd.Text + "\nPhone No: " + txtPhoneNo.Text + "\nDate of birth: " + dateDOB.Text,
                     "Customer created", MessageBoxButton.OK, MessageBoxImage.Information);

                MessageBox.Show("Customer is now added to the ATM database! The account details will be given to the customer later", "Supplier Confirmation", MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            txtCustID.Text = Customer.nextCust().ToString("000");
           txtFirstName.Focus();
            txtFirstName.Clear();
            txtSurname.Clear();
            cboGender.SelectedIndex = -1;
            txtAdd.Clear();
            txtPhoneNo.Clear();
            dateDOB.SelectedDate = null;

        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstNameUdp.Text.Equals(""))
            {
                MessageBox.Show("No first name written! Please enter the first name!", "Customer Error", MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                txtFirstNameUdp.Focus();
                return;
            }

            else if (txtSurnameUdp.Text.Equals(""))
            {
                MessageBox.Show("No last name written! Please enter the last name!", "Customer Error", MessageBoxButton.OK,
                                 MessageBoxImage.Error);
               txtSurnameUdp.Focus();
                return;
            }


            else if (txtAddUpd.Text.Equals(""))
            {
                MessageBox.Show("No address written! Please enter the address!", "Customer Error", MessageBoxButton.OK,
                                 MessageBoxImage.Error);
                txtAddUpd.Focus();
                return;
            }


            else if (!IsPhoneNo(txtPhoneNoUpd.Text))
            //else if (txtPhoneNo.Text.Equals(""))
            {
                MessageBox.Show("No phone number detected! This phone number must be entered!", "Missing Phone Number", MessageBoxButton.OK,
               MessageBoxImage.Error);
                txtPhoneNoUpd.Focus();
                return;

            }


            Customer cust = new Customer();

            cust.setCustID(Convert.ToInt32(txtCustIDUpd.Text));
            cust.setFirstName(txtFirstNameUdp.Text);
            cust.setLastName(txtSurnameUdp.Text);
            cust.setGender(Convert.ToString(txtGender.Text));
            cust.setAddress(txtAddUpd.Text);
            cust.setPhoneNo(Convert.ToString(txtPhoneNoUpd.Text));
            cust.setDateTime(Convert.ToDateTime(txtDOB.Text));

            try
            {
                cust.updCustomer();


                MessageBox.Show("CustomerID: " + txtCustIDUpd.Text + "\nFirst Name: " + txtFirstNameUdp.Text + "\nSurname: " + txtSurnameUdp.Text
                     + "\nGender: " + txtGender.Text + "\nAddress: " + txtAddUpd.Text + "\nPhone No: " + txtPhoneNoUpd.Text + "\nDate of birth: " + txtDOB.Text,
                     "Customer updated", MessageBoxButton.OK, MessageBoxImage.Information);

                MessageBox.Show("Customer is now updated to the ATM database!", "Update Complete", MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            cboCustomers.SelectedIndex = -1;
            txtCustIDUpd.Clear();
            txtFirstNameUdp.Focus();
            txtFirstNameUdp.Clear();
            txtSurnameUdp.Clear();
            txtGender.Clear();
            txtAddUpd.Clear();
            txtPhoneNoUpd.Clear();
            txtDOB.Clear();
        }


        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res;

           res = MessageBox.Show("Are you sure you want to remove this customer? \nCustomer: " + cboCustomersDel.SelectedItem.ToString() + 
               "\n\nCustomer ID:" + txtCustIDDel.Text +
               "\nFirst Name: " + txtFirstNameDel.Text + "\nLast Name: " + txtSurnameDel.Text + "\nGender: " + txtGenderDel.Text + "\nAddress: " 
               + txtAddDel.Text + 
               "\nPhone Number " + txtPhoneNoDel.Text + 
               "\nDate of Birth:  " + txtDOBDel.Text, 
               " Confirm Removal", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (res == MessageBoxResult.Yes)
            {

                Customer.removeCust(Convert.ToInt32(txtCustIDDel.Text));

                MessageBox.Show("This customer has now been removed", "Customer deleted", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            }
            else {

                MessageBox.Show("This customer will not be removed", "Customer remains in database", MessageBoxButton.OK, MessageBoxImage.Information);

                cboCustomers.SelectedIndex = -1;
            txtCustIDUpd.Clear();
            txtFirstNameUdp.Focus();
            txtFirstNameUdp.Clear();
            txtSurnameUdp.Clear();
            txtGender.Clear();
            txtAddUpd.Clear();
            txtPhoneNoUpd.Clear();
            txtDOB.Clear();
            }
        }

        public static DataSet getCustomers(DataSet ds)
        {
            return ds;
        }

        private void cboGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RadView_Checked(object sender, RoutedEventArgs e)
        {
            dataCustomers.ItemsSource = null;
            ds = new DataSet();
           
            
            ds = Customer.getView(ds);


            dataCustomers.ItemsSource = getCustomers(ds).Tables["ss"].AsDataView();
        }

        private void RadCustomers_Checked(object sender, RoutedEventArgs e)
        {
            ds = new DataSet();


            ds = Customer.getAllCustomers(ds);


            dataCustomers.ItemsSource = getCustomers(ds).Tables["ss"].AsDataView();

        }

        private void cboCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboCustomers.SelectedIndex == -1)
            {
                return;
            }

            Customer updCust = new Customer();

            updCust.getCust(Convert.ToInt32(cboCustomers.SelectedIndex+1));

            if (updCust.getCustID().Equals(0))
            {
                MessageBox.Show("No details found", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtCustID.Focus();
                return;
            }


            txtCustIDUpd.Text = updCust.getCustID().ToString("000");
            txtFirstNameUdp.Text = updCust.getFirstName();
           txtSurnameUdp.Text = updCust.getLastName();
            txtGender.Text = updCust.getGender().ToString();
            txtAddUpd.Text = updCust.getAddress();
            txtPhoneNoUpd.Text = updCust.getPhoneNo().ToString();
            txtDOB.Text = updCust.getDOB().ToShortDateString();
                  
        }

        private void txtDOB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cboCustomersDel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboCustomersDel.SelectedIndex == -1)
            {
                return;
            }

            Customer delCust = new Customer();

            delCust.getCust(Convert.ToInt32(cboCustomersDel.SelectedIndex + 1));

            if (delCust.getCustID().Equals(0))
            {
                MessageBox.Show("No details found", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtCustID.Focus();
                return;
            }


            txtCustIDDel.Text = delCust.getCustID().ToString("000");
            txtFirstNameDel.Text = delCust.getFirstName();
            txtSurnameDel.Text = delCust.getLastName();
            txtGenderDel.Text = delCust.getGender().ToString();
            txtAddDel.Text = delCust.getAddress();
            txtPhoneNoDel.Text = delCust.getPhoneNo().ToString();
            txtDOBDel.Text = delCust.getDOB().ToShortDateString();
        }
    }
    }

