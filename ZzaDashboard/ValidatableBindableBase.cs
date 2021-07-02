using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ZzaDashboard
{
    public class ValidatableBindableBase:BindableBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errors;

        public ValidatableBindableBase()
        {
            _errors = new Dictionary<string, List<string>>();
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        public bool HasErrors => _errors.Count > 0;

        protected override void SetProperty<T>(ref T member, T val,[CallerMemberName] string propertyName = null)
        {
            base.SetProperty(ref member, val, propertyName);
            ValidateProperty(propertyName, val);
        }

        private void ValidateProperty<T>(string propertyName, T val)
        {
            if (String.IsNullOrEmpty(propertyName))
                return;

            List<ValidationResult> results = new List<ValidationResult>();

            ValidationContext context = new ValidationContext(this);

            context.MemberName = propertyName;

            Validator.TryValidateProperty(val, context, results);

            if (results.Any())
            {
                _errors[propertyName] = results.Select(c => c.ErrorMessage).ToList();
            }
            else
            {
                _errors.Remove(propertyName);
            }
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
} 