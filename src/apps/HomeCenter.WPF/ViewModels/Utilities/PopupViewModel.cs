﻿using System;
using System.Timers;
using Caliburn.Micro;

namespace HomeCenter.NET.ViewModels.Utilities
{
    public sealed class PopupViewModel : Screen, IDisposable
    {
        #region Properties

        private string _text = string.Empty;
        public string Text {
            get => _text;
            set
            {
                _text = value;
                NotifyOfPropertyChange(nameof(Text));
            }
        }

        private bool _isVisible;
        public bool IsVisible {
            get => _isVisible;
            set {
                _isVisible = value;
                NotifyOfPropertyChange(nameof(IsVisible));
            }
        }

        private bool _isWarning;
        public bool IsWarning {
            get => _isWarning;
            set {
                _isWarning = value;
                NotifyOfPropertyChange(nameof(IsWarning));
            }
        }

        public int Delay { get; set; }
        private Timer Timer { get; set; } = new Timer(100);

        #endregion

        #region Constructors

        public PopupViewModel()
        {
            Timer.Elapsed += OnElapsed;
        }

        #endregion

        #region Public methods

        public void Show(string text, int delay, bool isWarning)
        {
            IsWarning = isWarning;
            IsVisible = true;
            Text = text;
            Delay = delay;

            Timer.Start();
        }

        public void Dispose()
        {
            Timer.Dispose();
        }

        #endregion

        #region Event Handlers

        private void OnElapsed(object sender, ElapsedEventArgs args)
        {
            Delay -= 100;
            if (Delay > 0)
            {
                return;
            }

            Timer.Stop();
            IsVisible = false;
        }

        #endregion
    }
}
