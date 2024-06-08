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
    /// Interaction logic for wBrand.xaml
    /// </summary>
    public partial class wBrand : Window
    {
        private readonly BrandBusiness _business;

        public wBrand()
        {
            InitializeComponent();
            this._business ??= new BrandBusiness();
            this.LoadGrdBrands();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

            int idTmp = -1;
            int.TryParse(txtBrandCode.Text, out idTmp);
            try
            {
                var item = await _business.GetBrandByIdAsync(idTmp);

                int status = 0;
                if (chkIsActive.IsChecked == true) { status = 1; }
                else { status = 0; }
                if (item.Data == null)
                {

                    var brand = new Brand()
                    {
                        BrandName = txtBrandName.Text,
                        Status = status
                    };

                    var result = await _business.CreateBrand(brand);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var brand = item.Data as Brand;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    brand.BrandName = txtBrandName.Text;
                    brand.Status = status;

                    var result = await _business.UpdateBrand(brand);
                    MessageBox.Show(result.Message, "Update");
                }

                txtBrandCode.Text = string.Empty;
                txtBrandName.Text = string.Empty;
                chkIsActive.IsChecked = false;
                this.LoadGrdBrands();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }



        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Hide();
            }

        }

        private async void grdBrand_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string categoryId = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(categoryId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteBrand(int.Parse(categoryId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdBrands();
                }
            }
        }

        private async void grdBrand_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Brand;
                    if (item != null)
                    {
                        var brandResult = await _business.GetBrandByIdAsync(item.BrandId);

                        if (brandResult.Status > 0 && brandResult.Data != null)
                        {
                            item = brandResult.Data as Brand;
                            txtBrandCode.Text = item.BrandId.ToString();
                            txtBrandName.Text = item.BrandName;
                            chkIsActive.IsChecked = Convert.ToBoolean(item.Status);
                        }
                    }
                }
            }
        }

        private async void LoadGrdBrands()
        {
            var result = await _business.GetBrandsAsync();

            if (result.Status > 0 && result.Data != null)
            {
                grdBrand.ItemsSource = result.Data as List<Brand>;
            }
            else
            {
                grdBrand.ItemsSource = new List<Brand>();
            }
        }

    }
}

