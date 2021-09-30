using AssemblyBrowserLib.Extensions;
using AssemblyBrowserLib.Nodes;
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


        private Tree _tree;
        public Tree Tree { get => _tree; set => SetProperty(ref _tree, value); }


        private FileDialog _fileDialog;
        private FileDialog FileDialog =>
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
                        Tree = new AssemblyInfoTree(
                            Assembly.Load(FileDialog.FileName)
                                    .GetAssemblyInfo());
                    }
                    catch
                    {
                    }
                }
            });
    }
}
