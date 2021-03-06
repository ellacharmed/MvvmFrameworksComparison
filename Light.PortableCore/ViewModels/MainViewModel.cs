using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace Light.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        
        private bool _canOpenChildViewModel;
        private string _parameter;
        private readonly RelayCommand _openChildViewModelCommand;

        #endregion

        #region Constructors

        public MainViewModel()
        {
            _openChildViewModelCommand = new RelayCommand(OpenChildViewModel, CanExecuteOpenChildViewModel);
            CanOpenChildViewModel = true;
        }

        #endregion

        #region Properties

        public string DisplayName
        {
            get { return "Main view model"; }
        }

        public bool CanOpenChildViewModel
        {
            get { return _canOpenChildViewModel; }
            set
            {
                if (value == _canOpenChildViewModel)
                {
                    return;
                }

                _canOpenChildViewModel = value;
                RaisePropertyChanged();
                _openChildViewModelCommand.RaiseCanExecuteChanged();
            }
        }

        public string Parameter
        {
            get { return _parameter; }
            set
            {
                if (value == _parameter)
                {
                    return;
                }

                _parameter = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand OpenChildViewModelCommand
        {
            get { return _openChildViewModelCommand; }
        }

        private void OpenChildViewModel()
        {
            var childViewModel = ServiceLocator.Current.GetInstance<ChildViewModel>();
            childViewModel.Initialize(Parameter);
            Messenger.Default.Send(new NotificationMessage(ViewModelLocator.ShowChildViewModelMessage));
        }

        private bool CanExecuteOpenChildViewModel()
        {
            return CanOpenChildViewModel;
        }

        #endregion
    }
}