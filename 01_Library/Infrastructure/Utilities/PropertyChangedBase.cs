using Infrastructure.Entities;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    /// <summary>
    /// Base class for models that implement INotifyPropertyChanged, INotifyPropertyChanging, and INotifyDataErrorInfo
    /// </summary>
    public abstract class PropertyChangedBase : INotifyPropertyChanged, INotifyPropertyChanging, INotifyDataErrorInfo
    {
        #region Implementation of INotifyPropertyChanged

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Implementation of INotifyDataErrorInfo

        /// <inheritdoc />
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return null;
            _errors.TryGetValue(propertyName, out List<string> errorsForName);
            return errorsForName;
        }

        /// <inheritdoc />
        public bool HasErrors => _errors.Any(b => b.Value != null && b.Value.Count > 0);

        /// <inheritdoc />
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        #endregion

        #region Implementation of INotifyPropertyChanging

        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        #region Implementation of PropertyChangedBase

        private readonly ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();
        private bool _isDirty;

        /// <summary>
        /// Dirty flag to tell if needs saving
        /// </summary>
        [NotMapped]
        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                if (_isDirty == value)
                    return;
                _isDirty = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Action to be executed after a property is set.
        /// </summary>
        public Action<string> PostSetAction = propertyName => { };

        /// <summary>
        /// Raises the PropertyChanged event for the specified property name.
        /// Validates the model asynchronously before raising the event.
        /// </summary>
        /// <param name="propertyName"></param>
        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            ValidateAsync();
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Sets the field only if the new value is different from old value, and raises a notify of property change
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <typeparam name="T"></typeparam>
        /// <usage>
        ///  private string name;  public string Name
        ///  {
        ///     get { return name; }
        ///     set { SetField(ref name, value); }
        ///  }
        /// </usage>
        /// <returns>true if value is set and false if new value is same as old value</returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            OnPropertyChanging(propertyName);
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            if (typeof(ComboBoxDropDownItem).IsAssignableFrom(typeof(T)) &&
                string.IsNullOrWhiteSpace((value as ComboBoxDropDownItem)?.Name))
                value = default;
            field = value;
            PostSetAction?.Invoke(propertyName);
            IsDirty = true;
            RaisePropertyChanged(propertyName);
            return true;
        }


        /// <summary>
        /// Sets the field only if the new value is different from old value, and raises a notify of property change
        /// Useful if you intend to set the member of another object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="setValue"></param>
        /// <param name="propertyName"></param>
        /// <usage>
        /// public class Person
        /// {
        ///     public string Name {get; set;}
        ///     public int Id {get; set;}
        /// };
        /// private Person person;
        /// public string Name
        /// {
        ///     get { return person.Name; }
        ///     set { Setter(person.Name, value, b => person.Name = b); }
        /// }
        /// </usage>
        /// <returns></returns>
        protected bool Setter<T>(T field, T value, Action<T> setValue, [CallerMemberName] string propertyName = "")
        {
            OnPropertyChanging(propertyName);
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            setValue(value);
            PostSetAction?.Invoke(propertyName);
            IsDirty = true;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanging event for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the ErrorsChanged event for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        public void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Runs the validation asynchronously
        /// </summary>
        /// <returns></returns>
        public Task ValidateAsync()
        {
            return Task.Run(() => Validate());
        }

        private readonly object _lock = new object();

        /// <summary>
        /// Run the Validation rules on the model
        /// </summary>
        public void Validate()
        {
            lock (_lock)
            {
                ValidationContext validationContext = new ValidationContext(this, null, null);
                List<ValidationResult> validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                foreach (KeyValuePair<string, List<string>> keyValuePair in _errors.ToList())
                {
                    if (validationResults.All(b => b.MemberNames.All(m => m != keyValuePair.Key)))
                    {
                        _errors.TryRemove(keyValuePair.Key, out List<string> result);
                        RaiseErrorsChanged(keyValuePair.Key);
                    }
                }

                IEnumerable<IGrouping<string, ValidationResult>> groupings =
                    from validationResult in validationResults
                    from name in validationResult.MemberNames
                    group validationResult by name into grouping
                    select grouping;

                foreach (IGrouping<string, ValidationResult> grouping in groupings)
                {
                    List<string> errorMessages = grouping.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(grouping.Key))
                    {
                        _errors.TryRemove(grouping.Key, out List<string> result);
                        RaiseErrorsChanged(grouping.Key);
                    }
                    _errors.TryAdd(grouping.Key, errorMessages);
                    RaiseErrorsChanged(grouping.Key);
                }
            }
        }

        /// <summary>
        /// Adds list of errors for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errorMessages"></param>
        public void AddErrors(string propertyName, List<string> errorMessages)
        {
            _errors.TryAdd(propertyName, errorMessages);
            RaiseErrorsChanged(propertyName);
        }

        /// <summary>
        /// Clears all errors for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        public void ClearErrors(string propertyName)
        {
            _errors.TryRemove(propertyName, out List<string> result);
            RaiseErrorsChanged(propertyName);
        }


        #endregion

    }
}
