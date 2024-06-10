﻿using MomAndChildren.Business;
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
        private readonly OrderBusiness _orderBusiness;
        private readonly ProductBusiness _productBusiness;
        private readonly UnitOfWork _unitOfWork;
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }


        public wOrderDetail()
        {
            InitializeComponent();
            this._orderDetailBusiness ??= new OrderDetailBusiness();
            this._orderBusiness ??= new OrderBusiness();
            this._productBusiness ??= new ProductBusiness();
            _unitOfWork ??= new UnitOfWork();
            this.LoadGrdOrderDetails();
            this.LoadOrdersAndProducts();
        }

        private async void LoadOrdersAndProducts()
        {
            Orders = (List<Order>)(await _orderBusiness.GetOrdersAsync()).Data;
            Products = (List<Product>)(await _productBusiness.GetProductsAsync()).Data;

            cmbOrderId.ItemsSource = Orders;
            cmbProductId.ItemsSource = Products;
            cmbProductId.IsEnabled = true;
            cmbOrderId.IsEnabled = true;
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
                    if (cmbProductId.Text == null || int.Parse(cmbProductId.Text) == 0)
                    {
                        MessageBox.Show("Please select ProductId and OrderId!", "OK");
                    }
                    else
                    {
                        int orderId = int.Parse(cmbOrderId.Text);

                    List<CartItem> cartItems = new List<CartItem>();

                    var product = _unitOfWork.ProductRepository.GetById(1);

                        cartItems.Add(new CartItem { ProductId = int.Parse(cmbProductId.Text), Product = product, Quantity = int.Parse(txtQuantity.Text) });

                    var result = await _orderDetailBusiness.CreateOrderDetail(orderId, cartItems);
                    MessageBox.Show(result.Message, "Save");
                        cmbProductId.IsEnabled = true;
                        cmbOrderId.IsEnabled = true;
                    }                   
                }
                else
                {
                    var orderDetail = item.Data as OrderDetail;
                    orderDetail.OrderDetailId = int.Parse(txtOrderDetailId.Text);
                    orderDetail.Quantity = int.Parse(txtQuantity.Text);
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    var result = await _orderDetailBusiness.UpdateOrderDetailQuantity(orderDetail.OrderDetailId, orderDetail.Quantity);
                    MessageBox.Show(result.Message, "Update");
                    cmbProductId.IsEnabled = true;
                    cmbOrderId.IsEnabled = true;

                }

                cmbOrderId.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                cmbProductId.Text = string.Empty;
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
            this.Close();
        }

        private async void grdOrderDetail_ButtonDelete_Click(object sender, RoutedEventArgs e)
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

        private async void grdOrderDetail_MouseDouble_Click(object sender, RoutedEventArgs e)
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
                            cmbOrderId.Text = item.OrderId.ToString();
                            cmbProductId.Text = item.ProductId.ToString();
                            txtQuantity.Text = item.Quantity.ToString();
                            cmbProductId.IsEnabled = false;
                            cmbOrderId.IsEnabled = false;
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
