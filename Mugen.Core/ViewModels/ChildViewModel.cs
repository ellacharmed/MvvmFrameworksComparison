﻿using System.Threading.Tasks;
using System.Windows.Input;
using MugenMvvmToolkit;
using MugenMvvmToolkit.Interfaces.Models;
using MugenMvvmToolkit.Interfaces.Presenters;
using MugenMvvmToolkit.Models;
using MugenMvvmToolkit.ViewModels;

namespace Mugen.ViewModels
{
    public class ChildViewModel : CloseableViewModel, IHasDisplayName, IHasOperationResult
    {
        #region Fields

        private readonly IMessagePresenter _messagePresenter;

        #endregion

        #region Constructors

        public ChildViewModel(IMessagePresenter messagePresenter)
        {
            Should.NotBeNull(messagePresenter, nameof(messagePresenter));
            _messagePresenter = messagePresenter;
            ApplyCommand = new RelayCommand(Apply);
        }

        #endregion

        #region Properties

        public string DisplayName => "Child view model";

        public string Parameter { get; set; }

        public bool? OperationResult { get; private set; }

        #endregion

        #region Commands

        public ICommand ApplyCommand { get; }

        private void Apply()
        {
            OperationResult = true;
            CloseAsync();
        }

        #endregion

        #region Methods

        protected override async Task<bool> OnClosing(object parameter)
        {
            var result = await _messagePresenter.ShowAsync("Are you sure you want to close window?", "Question",
                MessageButton.YesNo);
            await DoWorkAsync();
            return result == MessageResult.Yes;
        }

        private Task DoWorkAsync()
        {
            return Task.Delay(2000).WithBusyIndicator(this, "Long running process emulation");
        }

        #endregion
    }
}