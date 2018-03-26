﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using H.NET.Storages;
using H.NET.Utilities;

namespace H.NET.SearchDeskBand
{
    public partial class DeskBandWindow : Form
    {
        public DeskBandWindow()
        {
            InitializeComponent();

            #region Auto Complete

            var storage = new CommandsStorage();
            storage.Load();

            var collection = new AutoCompleteStringCollection();
            collection.AddRange(storage.Select(i => i.Key).ToArray());
            TextBox.AutoCompleteCustomSource = collection;

            #endregion

            UpdateHistory();
        }

        private void UpdateHistory()
        {
            historyListBox.Items.Clear();

            var history = CommandsHistory.Load();
            history.Reverse();
            foreach (var command in history)
            {
                historyListBox.Items.Add(command);
            }
        }

        private void DeskBandWindow_Deactivate(object sender, EventArgs e)
        {
            Hide();
        }

        private void DeskBandWindow_Activated(object sender, EventArgs e)
        {
            TextBox.Focus();
            //deskBandControl1.Focus();
        }

        private static async void SendCommand(string message)
        {
            try
            {
                await Client.Write(message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Run(string command)
        {
            SendCommand(command);
            Hide();

            await Task.Delay(1000); // TODO: fix

            UpdateHistory();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            Run(TextBox.Text);
            TextBox.Clear();
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            TextBox.Focus();
        }

        private void HistoryListBox_DoubleClick(object sender, EventArgs e)
        {
            var item = historyListBox.SelectedItem as string;

            Run(item);
        }

        private void ClearHistoryButton_Click(object sender, EventArgs e)
        {
            CommandsHistory.Clear();
            UpdateHistory();
        }
    }
}