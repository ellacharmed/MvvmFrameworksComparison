﻿using System.Threading.Tasks;
using System.Windows.Input;
using MugenMvvmToolkit;
using MugenMvvmToolkit.Interfaces.Models;
using MugenMvvmToolkit.Interfaces.Navigation;
using MugenMvvmToolkit.Interfaces.Presenters;
using MugenMvvmToolkit.Interfaces.ViewModels;
using MugenMvvmToolkit.Models;
using MugenMvvmToolkit.ViewModels;

namespace Mugen.ViewModels
{
    public class ParentViewModel : CloseableViewModel, IHasDisplayName, INavigableViewModel
    {
        #region Fields

        private readonly IMessagePresenter _messagePresenter;
        private bool _canOpenChildViewModel;
        private string _parameter;
        private readonly ICommand _openChildViewModelCommand;

        #endregion

        #region Constructors

        public ParentViewModel(IMessagePresenter messagePresenter)
        {
            Should.NotBeNull(messagePresenter, "messagePresenter");
            _messagePresenter = messagePresenter;
            CanOpenChildViewModel = true;
            _openChildViewModelCommand = new RelayCommand(OpenChildViewModel, CanExecuteOpenChildViewModel, this);
        }

        #endregion
        
        #region Properties

        public string DisplayName
        {
            get { return "Parent view model"; }
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand OpenChildViewModelCommand
        {
            get { return _openChildViewModelCommand; }
        }

        private async void OpenChildViewModel()
        {
            using (var viewModel = GetViewModel<ChildViewModel>(ObservationMode.None))
            {
                viewModel.Initialize(Parameter);
                
                if (!await viewModel.ShowAsync())
                {
                    return;
                }

                Parameter = viewModel.Parameter;
            }
        }

        private bool CanExecuteOpenChildViewModel()
        {
            return CanOpenChildViewModel;
        }

        #endregion
        
        #region Implementation of INavigableViewModel

        void INavigableViewModel.OnNavigatedTo(INavigationContext context)
        {
            this.TraceNavigation(context, _messagePresenter);
        }

        Task<bool> INavigableViewModel.OnNavigatingFromAsync(INavigationContext context)
        {
            this.TraceNavigation(context, _messagePresenter);
            return Empty.TrueTask;
        }

        void INavigableViewModel.OnNavigatedFrom(INavigationContext context)
        {
            this.TraceNavigation(context, _messagePresenter);
        }

        #endregion
    }
}
