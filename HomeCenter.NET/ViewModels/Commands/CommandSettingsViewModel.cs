﻿using System;
using System.Linq;
using Caliburn.Micro;
using H.NET.Storages;
using HomeCenter.NET.Windows;

// ReSharper disable UnusedMember.Global

namespace HomeCenter.NET.ViewModels.Commands
{
    // TODO: EditCommandViewModel ?
    public class CommandSettingsViewModel : SaveCancelViewModel
    {
        #region Properties

        public Command Command { get; }
        public BindableCollection<SingleKeyViewModel> Keys { get; }
        public BindableCollection<SingleCommandViewModel> Commands { get; }

        public string HotKey
        {
            get => Command.HotKey;
            set
            {
                Command.HotKey = value;
                NotifyOfPropertyChange(nameof(HotKey));
            }
        }

        private bool _editHotKeyIsEnabled = true;
        public bool EditHotKeyIsEnabled {
            get => _editHotKeyIsEnabled;
            set {
                _editHotKeyIsEnabled = value;
                NotifyOfPropertyChange(nameof(EditHotKeyIsEnabled));
            }
        }

        #endregion

        #region Constructors

        public CommandSettingsViewModel(Command command)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));

            Keys = new BindableCollection<SingleKeyViewModel>(Command.Keys.Select(i => new SingleKeyViewModel(i)));
            Commands = new BindableCollection<SingleCommandViewModel>(Command.Lines.Select(i => new SingleCommandViewModel(i)));
            HotKey = command.HotKey;
        }

        #endregion

        #region Public methods

        #region Key methods

        public void AddKey()
        {
            var key = new SingleKey(string.Empty);

            Command.Keys.Add(key);
            Keys.Add(new SingleKeyViewModel(key));

            /* TODO: Focus last element
            var control = DataPanel.Children.OfType<TextControl>().LastOrDefault();
            if (control == null)
            {
                return;
            }

            control.TextBox.Focusable = true;
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(control.TextBox), control.TextBox);
            Keyboard.Focus(control.TextBox);
            control.TextBox.Focus();*/
        }

        public void DeleteKey(SingleKeyViewModel viewModel)
        {
            Keys.Remove(viewModel);
            Command.Keys.Remove(viewModel.Key);
        }

        #endregion

        #region Command methods

        public void AddCommand()
        {
            var command = new SingleCommand(string.Empty);

            Command.Lines.Add(command);
            Commands.Add(new SingleCommandViewModel(command));
        }

        public void DeleteCommand(SingleCommandViewModel viewModel)
        {
            Commands.Remove(viewModel);
            Command.Lines.Remove(viewModel.Command);
        }

        public void RunCommand(SingleCommandViewModel viewModel)
        {
            MainWindow.GlobalRun(viewModel.Description);
        }

        #endregion

        #region HotKey methods

        public async void EditHotKey()
        {
            EditHotKeyIsEnabled = false;

            var combination = await MainWindow.CatchKey();
            HotKey = combination?.ToString() ?? string.Empty;

            EditHotKeyIsEnabled = true;
        }

        #endregion

        #endregion

    }
}
