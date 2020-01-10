﻿using System;
using System.Threading;
using System.Threading.Tasks;
using H.Pipes;

namespace H.NET.SearchDeskBand
{
    public class IpcService
    {
        #region Properties

        private PipeClient<string> PipeClient { get; } = new PipeClient<string>("H.MainApplication");

        #endregion

        #region Events

        public event EventHandler<string> MessageReceived;

        private void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(null, message);
        }

        #endregion

        #region Constructors

        public IpcService()
        {
            PipeClient.MessageReceived += (sender, args) => OnMessageReceived(args.Message);
        }

        #endregion

        #region Public methods

        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            await PipeClient.ConnectAsync(cancellationToken);
        }

        public async Task WriteAsync(string message, CancellationToken cancellationToken = default)
        {
            await PipeClient.WriteAsync(message, cancellationToken);
        }

        #endregion
    }
}