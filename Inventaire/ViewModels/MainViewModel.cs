using BillingManagement.Models;
using BillingManagement.UI.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingManagement.UI.ViewModels
{
	class MainViewModel : BaseViewModel
	{
		private BaseViewModel _vm;

		public BaseViewModel VM
		{
			get { return _vm; }
			set {
				_vm = value;
				OnPropertyChanged();
			}
		}

		CustomerViewModel customerViewModel;
		InvoiceViewModel invoiceViewModel;

		public ChangeViewCommand ChangeViewCommand { get; set; }
		public RelayCommand NewCustomerCommand { get; private set; }
		public RelayCommand DisplayInvoiceCommand { get; private set; }
		public RelayCommand DisplayCustomerCommant { get; private set; }
		public RelayCommand NewInvoiceCommand { get; private set; }

		public MainViewModel()
		{
			ChangeViewCommand = new ChangeViewCommand(ChangeView);

			NewCustomerCommand = new RelayCommand(NewCustomer);
			DisplayInvoiceCommand = new RelayCommand(DisplayInvoice);
			NewInvoiceCommand = new RelayCommand(NewInvoice,CanExecuteNewInvoice);


			customerViewModel = new CustomerViewModel();
			invoiceViewModel = new InvoiceViewModel(customerViewModel.Customers);

			VM = customerViewModel;

		}

		private void ChangeView(string vm)
		{
			switch (vm)
			{
				case "customers":
					VM = customerViewModel;
					break;
				case "invoices":
					VM = invoiceViewModel;
					break;
			}
		}

		private void NewCustomer(object c)
		{
			Customer customer = new Customer();
			customerViewModel.Customers.Add(customer);
			customerViewModel.SelectedCustomer = customer;
		}
		private void DisplayInvoice(object i)
		{
			Invoice invoice = i as Invoice;
			invoiceViewModel.SelectedInvoice = invoice;
			VM = invoiceViewModel;
		}
		private void NewInvoice(object c)
		{
			Customer customer = c as Customer;
			Invoice invoice = new Invoice(customer);
			customer.Invoices.Add(invoice);
			DisplayInvoice(invoice);
		}
		private bool CanExecuteNewInvoice(object c)
		{
			return c==null?false:true;

		}
	}
}
