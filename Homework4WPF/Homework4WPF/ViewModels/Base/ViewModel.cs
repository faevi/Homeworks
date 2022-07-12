using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Calculator.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        //~ViewModel()
        //{
        //    Dispose(false);
        //}

        protected virtual void Dispose()
        {
            Dispose(true);
        }

        private bool _disposed; 
        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _disposed) return;
            _disposed = true;
            // Освобождение управляемых ресурсов
        }
    }
}
 