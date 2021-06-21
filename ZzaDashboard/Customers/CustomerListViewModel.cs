using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel:BindableBase
    {
        private ICustomersRepository _repo;

        private ObservableCollection<Customer> _customers;


        public CustomerListViewModel()
        {
            _repo = new CustomersRepository();

            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => base.SetProperty(ref _customers, value);
        }

        public ICommand PlaceOrderCommand { get; private set; }
        public ICommand AddCustomerCommand { get; set; }
        public ICommand EditCustomerCommand { get; set; }

        public event Action<Guid> PlaceOrderRequested;
        public event Action<Customer> AddCustomerRequested;
        public event Action<Customer> EditCustomerRequested;

        public async void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(await _repo.GetCustomersAsync());
        }

        private void OnPlaceOrder(Customer customer)
        {
            PlaceOrderRequested?.Invoke(customer.Id);
        }


        private void OnEditCustomer(Customer customer)
        {
            EditCustomerRequested?.Invoke(customer);
        }

        private void OnAddCustomer()
        {
            AddCustomerRequested?.Invoke(new Customer()
            {
                Id = Guid.NewGuid(),
            });
        }

    }
}