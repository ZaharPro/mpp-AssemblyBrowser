using AssemblyBrowserLib.Extensions;
using Commands;
using Microsoft.Win32;
using Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModels
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
                Filter = "Assembly | *.dll",
                Title = "Select assembly",
                Multiselect = false
            };


        private ICommand _openFileCommand;
        public ICommand OpenFileCommand =>
            _openFileCommand ??= new RelayCommand(obj =>
            {
                try
                {

                    Tree = new AssemblyInfoTree(typeof(Tree).Assembly
                                .GetAssemblyInfo());
                } catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                /*var isOpen = FileDialog.ShowDialog();
                if (isOpen != null && isOpen.Value)
                {
                    try
                    {
                        Tree = new AssemblyInfoTree(
                            Assembly.LoadFrom(FileDialog.FileName)
                                    .GetAssemblyInfo());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }*/
            });
    }
}
