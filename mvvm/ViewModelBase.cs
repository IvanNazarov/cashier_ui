using System.Collections.Generic;

namespace Mvvm
{
    public class ViewModelBase : ObservableObject
    {

        public static bool IsInDesignModeStatic
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
                    var prop = System.ComponentModel.DesignerProperties.IsInDesignModeProperty;
                    _isInDesignMode
                        = (bool)System.ComponentModel.DependencyPropertyDescriptor
                                        .FromProperty(prop, typeof(System.Windows.FrameworkElement))
                                        .Metadata.DefaultValue;
                }
                return _isInDesignMode.Value;
            }
        }

        public bool IsInDesignMode
        {
            get
            {
                return IsInDesignModeStatic;
            }
        }


        protected bool Set<T>(
            string propertyName,
            ref T field,
            T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            field = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }

       
        private static bool? _isInDesignMode;


    }
}