using System;
using System.ComponentModel;
using System.Windows.Input;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public class AddEditCustomerViewModel:BindableBase
    {
        private Customer _editingCustomer;
        private bool _editMode;
        private SimpleEditableCustomer _customer;
        private ICustomersRepository _repo;

        public AddEditCustomerViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        public bool EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;
                base.SetProperty(ref _editMode, value);
            }
        }

        public SimpleEditableCustomer Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                base.SetProperty(ref _customer, value);
            }
        }

        public void SetCustomer(Customer customer)
        {
            _editingCustomer = customer;
            if (Customer != null) Customer.ErrorsChanged -= CustomerOnErrorsChanged;
            Customer = new SimpleEditableCustomer();
            Customer.ErrorsChanged += CustomerOnErrorsChanged;
            CopyCustomer(customer, Customer);
        }

        private void CustomerOnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            (SaveCommand as RelayCommand)?.OnCanExecuteChanged();
        }

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public event Action Done;

        private void CopyCustomer(Customer source, SimpleEditableCustomer target)
        {
            target.Id = source.Id;

            if (!EditMode)
                return;

            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Phone = source.Phone;
            target.Email = source.Email;
        }


        private bool CanSave()
        {
            return !Customer.HasErrors;
        }

        private async void OnSave()
        {
            UpdateCustomer(Customer, _editingCustomer);

            if (EditMode)
                await _repo.UpdateCustomerAsync(_editingCustomer);
            else
                await _repo.AddCustomerAsync(_editingCustomer);

            Done?.Invoke();
        }

        private void UpdateCustomer(SimpleEditableCustomer source, Customer target)
        {
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Email = source.Email;
            target.Phone = source.Phone;
        }

        private void OnCancel()
        {
            Done?.Invoke();
        }

    }
}