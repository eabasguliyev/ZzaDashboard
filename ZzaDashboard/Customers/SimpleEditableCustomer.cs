using System;
using System.ComponentModel.DataAnnotations;
using Zza.Data.Annotations;

namespace ZzaDashboard.Customers
{
    public class SimpleEditableCustomer:ValidatableBindableBase
    {
        private Guid _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        

        public Guid Id
        {
            get => _id;
            set
            {
                _id = value;
                base.SetProperty(ref _id, value);
            }
        }


        [Required]
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                base.SetProperty(ref _firstName, value);
            }
        }

        [Required]
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                base.SetProperty(ref _lastName, value);
            }
        }

        [EmailAddress]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                base.SetProperty(ref _email, value);
            }
        }

        [Phone]
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                base.SetProperty(ref _phone, value);
            }
        }
    }
}