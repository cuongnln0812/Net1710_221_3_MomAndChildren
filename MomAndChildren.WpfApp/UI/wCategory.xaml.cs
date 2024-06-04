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

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void grdCategory_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void grdCategory_MouseDouble_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void LoadGrdCategories()
        {
            var result = await _business.GetCategoriesAsync();

            if (result.Status > 0 && result.Data != null)
            {
                grdCurrency.ItemsSource = result.Data as List<Category>;
            }
            else
            {
                grdCurrency.ItemsSource = new List<Category>();
            }
        }

    }
}
