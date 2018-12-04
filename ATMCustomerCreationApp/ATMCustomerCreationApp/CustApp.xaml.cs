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

        public CustApp()
        {
            InitializeComponent();
            txtCustID.Text = Customer.nextCust().ToString("135");



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
                txtSurname.Focus();
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
            cust.setGender(Convert.ToChar(cboGender.Text.Substring(0,1)));
            cust.setAddress(txtAdd.Text);
            cust.setPhoneNo(Convert.ToInt32(txtPhoneNo.Text));
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
    }
}
