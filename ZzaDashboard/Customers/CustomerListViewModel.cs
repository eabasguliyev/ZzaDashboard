using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel:BindableBase
    {
        private ICustomersRepository _repo;

        private ObservableCollection<Customer> _customers;

        private List<Customer> _allCustomers;
        private string _searchInput;


        public CustomerListViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        private void OnClearSearch()
        {
            SearchInput = null;
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => base.SetProperty(ref _customers, value);
        }

        public string SearchInput
        {
            get => _searchInput;
            set
            {
                base.SetProperty(ref _searchInput, value);

                FilterCustomers(_searchInput);
            }
        }

        private void FilterCustomers(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                Customers = new ObservableCollection<Customer>(_allCustomers);
            }
            else
            {
                Customers = new ObservableCollection<Customer>(_allCustomers.Where(c =>
                    c.FullName.ToLower().Contains(searchInput.ToLower())));

            }
        }

        public ICommand PlaceOrderCommand { get; private set; }
        public ICommand AddCustomerCommand { get; set; }
        public ICommand EditCustomerCommand { get; set; }

        public ICommand ClearSearchCommand { get; private set; }

        public event Action<Guid> PlaceOrderRequested;
        public event Action<Customer> AddCustomerRequested;
        public event Action<Customer> EditCustomerRequested;

        public async void LoadCustomers()
        {
            _allCustomers = await _repo.GetCustomersAsync();

            Customers = new ObservableCollection<Customer>(_allCustomers);
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