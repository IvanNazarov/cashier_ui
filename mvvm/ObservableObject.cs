using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mvvm
{
    public class ObservableObject : INotifyPropertyChanged
    {
        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        #endregion

        protected void RaisePropertyChanged([CallerMemberName] string
            propertyName = null)
        {
            VerifyPropertyName(propertyName);
            //invoke ProperyChanged event if handler isn't null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void VerifyPropertyName(string propertyName)
        {
            var myType = GetType();


            if (!string.IsNullOrEmpty(propertyName) &&
                myType.GetProperty(propertyName) == null)
            {

                if (this is ICustomTypeDescriptor descriptor)
                {
                    if (descriptor.GetProperties()
                        .Cast<PropertyDescriptor>()
                        .Any(property => property.Name == propertyName))
                    {
                        return;
                    }
                    throw new ArgumentException("Property not found",
                                        propertyName);
                }
            }
        }
    }
}
