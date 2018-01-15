﻿using System;
using VoiceActions.NET.Converters;
using VoiceActions.NET.Recorders;
using Xunit;
using Xunit.Abstractions;

namespace VoiceActions.NET.Tests
{
    public class ManagersTests : BaseTests
    {
        public ManagersTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void AutoWinmmWitAiVoiceManagerTest() => BaseVoiceManagerTest(new VoiceManager
        {
            Recorder = new AutoStopRecorder(new WinmmRecorder(), 1000),
            Converter = new WitAiConverter("OQTI5VZ6JYDHYXTDKCDIYUODEUKH3ELS")
        }, PlatformID.Win32NT);

        [Fact]
        public void WinmmWitAiVoiceManagerTest() => BaseVoiceManagerTest(new VoiceManager
        {
            Recorder = new WinmmRecorder(),
            Converter = new WitAiConverter("OQTI5VZ6JYDHYXTDKCDIYUODEUKH3ELS")
        }, PlatformID.Win32NT);

        [Fact]
        public void VoiceManagerConstructorTest() => BaseVoiceManagerTest(
            new VoiceManager(
                new WinmmRecorder(), 
                new WitAiConverter("OQTI5VZ6JYDHYXTDKCDIYUODEUKH3ELS")), 
            PlatformID.Win32NT);

        [Fact]
        public void WinmmYandexVoiceManagerTest() => BaseVoiceManagerTest(new VoiceManager
        {
            Recorder = new WinmmRecorder(),
            Converter = new YandexConverter("1ce29818-0d15-4080-b6a1-ea5267c9fefd")
            {
                Lang = "ru-RU",
                Topic = "queries"
            }
        }, PlatformID.Win32NT);

        [Fact]
        public void AutoWinmmYandexVoiceManagerTest() => BaseVoiceManagerTest(new VoiceManager
        {
            Recorder = new AutoStopRecorder(new WinmmRecorder(), 1000),
            Converter = new YandexConverter("1ce29818-0d15-4080-b6a1-ea5267c9fefd")
            {
                Lang = "ru-RU",
                Topic = "queries"
            }
        }, PlatformID.Win32NT);
    }
}