﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using H.NET.Core.Utilities;

namespace H.NET.SearchDeskBand
{
    public partial class DeskBandControl : UserControl, IDisposable
    {
        #region Properties

        private DeskBandWindow Window { get; set; } = new DeskBandWindow();
        private Dictionary<string, Action<string>> ActionDictionary { get; } = new Dictionary<string, Action<string>>();

        #endregion

        #region Constructors

        public DeskBandControl()
        {
            InitializeComponent();

            AddAction("start", message => RecordButton.BackColor = Color.RoyalBlue);
            AddAction("stop", message => RecordButton.BackColor = Color.White);

            Window.VisibleChanged += (sender, args) => Label.Visible = !Window.Visible;

            IpcService.Message += OnNewMessage;
        }

        #endregion

        #region Event handlers

        private void OnNewMessage(object obj, string message)
        {
            var values = message.SplitOnlyFirst(' ');
            if (!ActionDictionary.TryGetValue(values[0], out var action))
            {
                return;
            }

            action?.Invoke(values[1]);
        }

        private void OnClick(object sender, EventArgs e)
        {
            Window.Visible = !Window.Visible;
            var location = PointToScreen(Point.Empty);
            Window.Location = location;
            Window.Top -= Window.Height;
            Window.Top += Height;
            Window.Left -= 1; // border
        }

        private void RecordButton_Click(object sender, EventArgs e) => Run("start-record");
        private void UiButton_Click(object sender, EventArgs e) => Run("show-ui");
        private void MenuButton_Click(object sender, EventArgs e) => Run("show-commands");
        private void SettingsButton_Click(object sender, EventArgs e) => Run("show-settings");

        #endregion

        #region IDisposable

        public new void Dispose()
        {
            Window?.Dispose();
            Window = null;

            base.Dispose();
        }

        #endregion

        #region Private methods

        public void Run(string message) => IpcService.SendMessage(message);

        private void AddAction(string key, Action<string> action)
        {
            ActionDictionary[key.ToLowerInvariant()] = action;
        }

        #endregion
    }
}
