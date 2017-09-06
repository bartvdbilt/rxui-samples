using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MvvmValidation;

namespace Hippocrate.ViewModel
{
    public abstract partial class ValidatableViewModelBase : ViewModelBase, IValidatable
    {
        protected ValidationHelper Validator { get; private set; }

        private DataErrorInfoAdapter DataErrorInfoAdapter { get; set; }

        public ValidatableViewModelBase()
        {
            Validator = new ValidationHelper();
            DataErrorInfoAdapter = new DataErrorInfoAdapter(Validator);
            OnCreated();
        }

        partial void OnCreated();

        Task<ValidationResult> IValidatable.Validate()
        {
            return Validator.ValidateAllAsync();
        }
    }
}