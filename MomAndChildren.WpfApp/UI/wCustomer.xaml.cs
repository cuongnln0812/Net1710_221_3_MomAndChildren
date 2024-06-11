using MomAndChildren.Business;
using MomAndChildren.Data.Models;
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
using System.Windows.Shapes;

namespace MomAndChildren.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wCustomer.xaml
    /// </summary>
    public partial class wCustomer : Window
    {
        private readonly CustomerBusiness _business;
        public wCustomer()
        {
            InitializeComponent();
            this._business ??= new CustomerBusiness();
            this.LoadGrdCustomers();
        }




        private async void LoadGrdCustomers()
        {
            var result = await _business.GetCustomersAsync();

            if (result.Status > 0 && result.Data != null)
            {
                grdCustomer.ItemsSource = result.Data as List<Customer>;
            }
            else
            {
                grdCustomer.ItemsSource = new List<Customer>();
            }
        }

        //private async void grdOrder_MouseDouble_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

            int idTmp = -1;
            int.TryParse(txtCustomerId.Text, out idTmp);
            try
            {
                var item = await _business.GetCustomerByIdAsync(idTmp);

                if (item.Data == null)
                {
                    var customer = new Customer()
                    {
                        Name = txtName.Text,
                        Address = txtAddress.Text,
                        PhoneNumber = txtPhoneNumber.Text,
                        Dob = DateTime.Parse(txtDob.Text),
                        Gender = int.Parse(txtGender.Text),
                        //CustomerId = 1
                    };

                    var result = await _business.CreateCustomer(customer);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var customer = item.Data as Customer;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    customer.Name = txtName.Text;
                    customer.Address = txtAddress.Text;
                    customer.PhoneNumber = txtPhoneNumber.Text;
                    customer.Dob = DateTime.Parse(txtDob.Text);
                    customer.Gender = int.Parse(txtGender.Text);
                    //order.CustomerId = 1;

                    var result = await _business.UpdateCustomer(customer);
                    MessageBox.Show(result.Message, "Update");
                }

                txtName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtPhoneNumber.Text = string.Empty;
                txtDob.Text = string.Empty;
                txtGender.Text = string.Empty;
                this.LoadGrdCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }


        }

        private async void grdCustomer_MouseDouble_Click(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Customer;
                    if (item != null)
                    {
                        var customerResult = await _business.GetCustomerByIdAsync(item.CustomerId);

                        if (customerResult.Status > 0 && customerResult.Data != null)
                        {
                            item = customerResult.Data as Customer;
                            txtCustomerId.Text = item.CustomerId.ToString();
                            txtName.Text = item.Name.ToString();
                            txtAddress.Text = item.Address.ToString();
                            txtAddress.Text = item.Address.ToString();
                            txtPhoneNumber.Text = item.PhoneNumber.ToString();
                            txtDob.SelectedDate = item.Dob;
                            txtGender.Text = item.Gender.ToString();
                            //item.CustomerId = 1;

                        }
                    }
                }
            }
        }

        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string customerId = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(customerId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteCustomer(int.Parse(customerId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdCustomers();
                }
            }
        }



        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
