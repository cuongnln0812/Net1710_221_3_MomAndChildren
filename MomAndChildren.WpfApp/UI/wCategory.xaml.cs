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
    /// Interaction logic for wCategory.xaml
    /// </summary>
    public partial class wCategory : Window


    {
        private readonly CategoryBusiness _business;
    
        public wCategory()
        {
            InitializeComponent();
            this._business ??= new CategoryBusiness();
            this.LoadGrdCategories();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
 
                int idTmp = -1;
                int.TryParse(txtCategoryCode.Text, out idTmp);
                try
                {
                    var item = await _business.GetCategoryByIdAsync(idTmp);

                    int status = 0;
                    if (chkIsActive.IsChecked == true) { status = 1; }
                    else { status = 0; }
                    if (item.Data == null)
                    {
                        
                         var category = new Category()
                    {
                        CategoryName = txtCategoryName.Text,
                        Status = status
                        };

                        var result = await _business.CreateCategory(category);
                        MessageBox.Show(result.Message, "Save");
                    }
                    else
                    {
                        var category = item.Data as Category;
                        //currency.CurrencyCode = txtCurrencyCode.Text;
                        category.CategoryName = txtCategoryName.Text;
                        category.Status = status;

                        var result = await _business.UpdateCategory(category);
                        MessageBox.Show(result.Message, "Update");
                    }

                    txtCategoryCode.Text = string.Empty;
                    txtCategoryName.Text = string.Empty;
                    chkIsActive.IsChecked = false;
                    this.LoadGrdCategories();
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

        private async void grdCategory_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string categoryId = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(categoryId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteCategory(int.Parse(categoryId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdCategories();
                }
            }
        }

        private async void grdCategory_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Category;
                    if (item != null)
                    {
                        var categoryResult = await _business.GetCategoryByIdAsync(item.CategoryId);

                        if (categoryResult.Status > 0 && categoryResult.Data != null)
                        {
                            item = categoryResult.Data as Category;
                            txtCategoryCode.Text = item.CategoryId.ToString();
                            txtCategoryName.Text = item.CategoryName;
                            chkIsActive.IsChecked = Convert.ToBoolean(item.Status);
                        }
                    }
                }
            }
        }

        private async void LoadGrdCategories()
        {
            var result = await _business.GetCategoriesAsync();

            if (result.Status > 0 && result.Data != null)
            {
                grdCategory.ItemsSource = result.Data as List<Category>;
            }
            else
            {
                grdCategory.ItemsSource = new List<Category>();
            }
        }

    }
}
