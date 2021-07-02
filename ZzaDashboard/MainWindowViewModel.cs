using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Windows.Input;
using Unity;
using Zza.Data;
using ZzaDashboard.Customers;
using ZzaDashboard.OrderPrep;
using ZzaDashboard.Orders;
using ZzaDashboard.Services;

namespace ZzaDashboard
{
    public class MainWindowViewModel:BindableBase
    {
        private CustomerListViewModel _customerListViewModel;
        private OrderViewModel _orderViewModel;
        private OrderPrepViewModel _orderPrepViewModel;
        private AddEditCustomerViewModel _addEditCustomerViewModel;
        private BindableBase _currentViewModel;
        public MainWindowViewModel()
        {
            _customerListViewModel = ContainerHelper.Container.Resolve<CustomerListViewModel>();
            _orderViewModel = new OrderViewModel();
            _orderPrepViewModel = new OrderPrepViewModel();
            _addEditCustomerViewModel = ContainerHelper.Container.Resolve<AddEditCustomerViewModel>();
            _addEditCustomerViewModel.Done += NavToCustomerList;

            _customerListViewModel.PlaceOrderRequested += NavToOrder;
            _customerListViewModel.AddCustomerRequested += NavToAddCustomer;
            _customerListViewModel.EditCustomerRequested += NavToEditCustomer;

            NavCommand = new RelayCommand<string>(OnNav);
        }

        private void NavToCustomerList()
        {
            CurrentViewModel = _customerListViewModel;
        }

        public ICommand NavCommand { get; private set; }

        public BindableBase CurrentViewModel
        {
            get => _currentViewModel;
            set => base.SetProperty(ref _currentViewModel, value);
        }

        private void OnNav(string navigation)
        {
            switch (navigation)
            {
                case "order":
                {
                    CurrentViewModel = _orderPrepViewModel;
                }
                    break;
                case "orderPrep":
                {
                    CurrentViewModel = _orderViewModel;
                }
                    break;
                default:
                {
                    CurrentViewModel = _customerListViewModel;
                }
                    break;
            }
        }

        private void NavToOrder(Guid customerId)
        {
            _orderViewModel.CustomerId = customerId;

            CurrentViewModel = _orderViewModel;
        }


        private void NavToEditCustomer(Customer customer)
        {
            _addEditCustomerViewModel.EditMode = true;
            _addEditCustomerViewModel.SetCustomer(customer);

            CurrentViewModel = _addEditCustomerViewModel;
        }

        private void NavToAddCustomer(Customer customer)
        {
            _addEditCustomerViewModel.EditMode = false;
            _addEditCustomerViewModel.SetCustomer(customer);

            CurrentViewModel = _addEditCustomerViewModel;
        }

    }
}