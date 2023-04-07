using StoreHouse.Model.Commands;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.ViewModels
{
    internal class WriteOffsUCViewModel : INotifyPropertyChanged
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public WriteOffsUCViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

        //Properties
        // ChosenRemainsItem Field
        private static OutputWriteOff _ChoosenWriteOffItem;
        public OutputWriteOff ChoosenWriteOffItem
        {
            get => _ChoosenWriteOffItem;
            set
            {
                _ChoosenWriteOffItem = value;
                OnPropertyChanged();
            }
        }
        public static OutputWriteOff GetChoosenWriteOffItem()
        {
            return _ChoosenWriteOffItem;
        }
        //Commands
        private RelayCommand _LoadAddWriteOffCommand;
        public RelayCommand LoadAddWriteOffCommand
        {
            get
            {
                return _LoadAddWriteOffCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.AddWriteOff);
                });
            }
        }
        /// <summary>
        /// Сообщение пользователю
        /// </summary>
        private RelayCommand _ShowMessageCommand;
        public RelayCommand ShowMessageCommand
        {
            get
            {
                return _ShowMessageCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.ShowMessage("WriteOffsUC");
                });
            }
        }

        private RelayCommand _LoadEditWriteOffCommand;
        public RelayCommand LoadEditWriteOffCommand
        {
            get
            {
                return _LoadEditWriteOffCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.EditWriteOff);
                });
            }
        }
        private RelayCommand _LoadDetailsWriteOffCommand;
        public RelayCommand LoadDetailsWriteOffCommand
        {
            get
            {
                return _LoadDetailsWriteOffCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.DetailsWriteOff);
                });
            }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
