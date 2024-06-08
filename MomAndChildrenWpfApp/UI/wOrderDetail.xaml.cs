using MomAndChildren.Business;
using MomAndChildren.Data;
using MomAndChildren.Data.Models;
using MomAndChildren.Data.Models.DTO;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace MomAndChildrenWpfApp.UI
{
    /// <summary>
    /// Interaction logic for wOrderDetail.xaml
    /// </summary>
    public partial class wOrderDetail : Window
    {
        private readonly OrderDetailBusiness _orderDetailBusiness;
        private readonly ProductBusiness _productBusiness;
        private readonly UnitOfWork _unitOfWork;


        public wOrderDetail()
        {
            InitializeComponent();
            this._orderDetailBusiness ??= new OrderDetailBusiness();
            this._productBusiness ??= new ProductBusiness();
            _unitOfWork ??= new UnitOfWork();
            this.LoadGrdOrderDetails();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            int idTmp = -1;
            int.TryParse(txtOrderDetailId.Text, out idTmp);
            try
            {
                var item = await _orderDetailBusiness.GetOrderDetailByIdAsync(idTmp);

                if (item.Data == null)
                {
                    int orderId = int.Parse(txtOrderId.Text);

                    List<CartItem> cartItems = new List<CartItem>();

                    var product = _unitOfWork.ProductRepository.GetById(1);

                    cartItems.Add(new CartItem { ProductId = int.Parse(txtProductId.Text), Product = product, Quantity = int.Parse(txtQuantity.Text) });

                    var result = await _orderDetailBusiness.CreateOrderDetail(orderId, cartItems);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var orderDetail = item.Data as OrderDetail;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    var result = await _orderDetailBusiness.UpdateOrderDetailQuantity(orderDetail.OrderDetailId, orderDetail.Quantity);
                    MessageBox.Show(result.Message, "Update");
                }

                txtOrderId.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtProductId.Text = string.Empty;
                txtOrderDetailId.Text = string.Empty;
                this.LoadGrdOrderDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void grdCategory_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string currencyCode = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(currencyCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _orderDetailBusiness.DeleteOrderDetail(int.Parse(currencyCode));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdOrderDetails();
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
                    var item = row.Item as OrderDetail;
                    if (item != null)
                    {
                        var currencyResult = await _orderDetailBusiness.GetOrderDetailByIdAsync(item.OrderDetailId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as OrderDetail;
                            txtOrderDetailId.Text = item.OrderDetailId.ToString();
                            txtOrderId.Text = item.OrderId.ToString();
                            txtProductId.Text = item.ProductId.ToString();
                            txtQuantity.Text = item.Quantity.ToString();
                        }
                    }
                }
            }
        }

        private async void LoadGrdOrderDetails()
        {
            var result = await _orderDetailBusiness.GetOrderDetailsAsync();

            if (result.Status > 0 && result.Data != null)
            {
                grdOrderDetails.ItemsSource = result.Data as List<OrderDetail>;
            }
            else
            {
                grdOrderDetails.ItemsSource = new List<OrderDetail>();
            }
        }

    }
}
