using AssemblyBrowserLib.Extensions;
using Commands;
using Microsoft.Win32;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
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


        private IEnumerable<AssemblyInfoTree> _assemblies;
        public IEnumerable<AssemblyInfoTree> Assemblies { get => _assemblies; set => SetProperty(ref _assemblies, value); }


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
                /*try
                {

                    Assemblies = new AssemblyInfoTree[] {new AssemblyInfoTree(typeof(AssemblyInfoTree).Assembly
                                .GetAssemblyInfo()) };
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }*/

                var isOpen = FileDialog.ShowDialog();
                if (isOpen != null && isOpen.Value)
                {
                    try
                    {
                        Assemblies = new AssemblyInfoTree[] {new AssemblyInfoTree(
                            Assembly.LoadFrom(FileDialog.FileName)
                                    .GetAssemblyInfo()) };
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            });
    }
}
