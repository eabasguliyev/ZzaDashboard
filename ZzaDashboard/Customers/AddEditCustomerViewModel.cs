using System;
using Zza.Data;

namespace ZzaDashboard.Customers
{
    public class AddEditCustomerViewModel:BindableBase
    {
        private Customer _customer;
        public bool EditMode { get; set; }
        public Guid CustomerId => _customer?.Id ?? Guid.Empty;

        public void SetCustomer(Customer customer)
        {
            _customer = customer;
            //OnPropertyChanged(nameof(CustomerId));
        }
    }
}