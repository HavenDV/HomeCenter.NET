﻿using System;
using System.Windows;
using System.Windows.Media;

namespace HomeCenter.NET.Controls
{
    public partial class InstanceControl
    {
        #region Properties

        public string ObjectName { get; set; }
        public string ObjectDescription { get; set; }

        private bool _objectIsEnabled;
        public bool ObjectIsEnabled
        {
            get => _objectIsEnabled;
            set
            {
                _objectIsEnabled = value;
                EnableButtton.Content = value ? "On" : "Off";
                EnableButtton.Background = new SolidColorBrush(value ? Colors.LightGreen : Colors.Bisque);
            }
        }

        private Color _color;
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                Background = new SolidColorBrush(value);
            }
        }

        public bool EnableName {
            get => NameLabel.Visibility == Visibility.Visible;
            set => NameLabel.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool EnableEnabling {
            get => EnableButtton.Visibility == Visibility.Visible;
            set => EnableButtton.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool EnableEditing
        {
            get => EditButtton.Visibility == Visibility.Visible;
            set => EditButtton.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool EnableDeleting {
            get => DeleteButtton.Visibility == Visibility.Visible;
            set => DeleteButtton.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion

        #region Events

        public delegate void EnabledChangedDelegate(bool isEnabled);
        public event EnabledChangedDelegate EnabledChanged;

        public event EventHandler Edited;
        public event EventHandler Deleted;

        #endregion

        #region Constructors

        public InstanceControl(string name, string description)
        {
            ObjectName = name ?? throw new ArgumentNullException(nameof(name));
            ObjectDescription = description ?? throw new ArgumentNullException(nameof(description));

            InitializeComponent();
        }

        public InstanceControl(string name) : this(string.Empty, name)
        {
            EnableName = false;
        }

        #endregion

        #region Event handlers

        private void EnableButton_Click(object sender, RoutedEventArgs e)
        {
            ObjectIsEnabled = !ObjectIsEnabled;

            EnabledChanged?.Invoke(ObjectIsEnabled);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) =>
            Edited?.Invoke(this, EventArgs.Empty);

        private void DeleteButton_Click(object sender, RoutedEventArgs e) =>
            Deleted?.Invoke(this, EventArgs.Empty);

        #endregion
    }
}