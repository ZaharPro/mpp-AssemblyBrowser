
using AssemblyBrowserLib;
using Microsoft.Win32;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AssemblyBrowserApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, newValue))
            {
                return false;
            }
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }


        private AssemblyInfo assemblyInfo;
        public AssemblyInfo AssemblyInfo { get => assemblyInfo; set => SetProperty(ref assemblyInfo, value); }


        private OpenFileDialog _fileDialog;
        private OpenFileDialog FileDialog =>
            _fileDialog ??= new OpenFileDialog
            {
                Filter = "Assemblies|*.dll;*.exe",
                Title = "Select assembly",
                Multiselect = false
            };


        private ICommand _openFileCommand;
        public ICommand OpenFileCommand =>
            _openFileCommand ??= new Command(obj =>
            {
                var isOpen = FileDialog.ShowDialog();
                if (isOpen != null && isOpen.Value)
                {
                    try
                    {
                        AssemblyInfo = Assembly.Load(FileDialog.FileName)?.GetAssemblyInfo();
                    }
                    catch
                    {
                    }
                }
            });
    }
}
