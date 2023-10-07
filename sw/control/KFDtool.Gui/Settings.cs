﻿using KFDtool.Container;
using KFDtool.P25.TransferConstructs;
using System.Diagnostics;
using System.Reflection;
using System;

namespace KFDtool.Gui
{
    class Settings
    {
        public static string AssemblyVersion { get; private set; }

        public static string AssemblyInformationalVersion { get; private set; }

        public static string ScreenCurrent { get; set; }

        public static bool ScreenInProgress { get; set; }

        public static bool ContainerOpen { get; set; }

        public static bool ContainerSaved { get; set; }

        public static string ContainerPath { get; set; }

        public static byte[] ContainerKey { get; set; }

        public static OuterContainer ContainerOuter { get; set; }

        public static InnerContainer ContainerInner { get; set; }

        public static BaseDevice SelectedDevice { get; set; }

        static Settings()
        {
            AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AssemblyInformationalVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            ScreenCurrent = string.Empty;
            ScreenInProgress = false;
            ContainerOpen = false;
            ContainerSaved = false;
            ContainerPath = string.Empty;
            ContainerKey = null;
            ContainerInner = null;
            ContainerOuter = null;

            SelectedDevice = new BaseDevice();

            SelectedDevice.TwiKfdtoolDevice = new TwiKfdtoolDevice();
            SelectedDevice.DliIpDevice = new DliIpDevice();
            SelectedDevice.DliIpDevice.Protocol = DliIpDevice.ProtocolOptions.UDP;

            //LoadSettings();
        }

        public static void InitSettings()
        {
            Properties.Settings.Default.TwiComPort = "";
            Properties.Settings.Default.DliHostname = "192.168.128.1";
            Properties.Settings.Default.DliPort = 0;
            Properties.Settings.Default.DliVariant = "Standard";
            Properties.Settings.Default.DeviceType = "TwiKfdDevice";
            Properties.Settings.Default.KfdDeviceType = "Kfdshield";
            Properties.Settings.Default.Save();
        }

        public static void SaveSettings()
        {
            Properties.Settings.Default.TwiComPort = SelectedDevice.TwiKfdtoolDevice.ComPort;
            Properties.Settings.Default.DliHostname = SelectedDevice.DliIpDevice.Hostname;
            Properties.Settings.Default.DliPort = SelectedDevice.DliIpDevice.Port;
            Properties.Settings.Default.DliVariant = SelectedDevice.DliIpDevice.Variant.ToString();
            Properties.Settings.Default.DeviceType = SelectedDevice.DeviceType.ToString();
            Properties.Settings.Default.KfdDeviceType = SelectedDevice.KfdDeviceType.ToString();
            Properties.Settings.Default.Save();
        }

        public static void LoadSettings()
        {
            SelectedDevice.TwiKfdtoolDevice.ComPort = Properties.Settings.Default.TwiComPort;
            SelectedDevice.DliIpDevice.Hostname = Properties.Settings.Default.DliHostname;
            SelectedDevice.DliIpDevice.Port = Properties.Settings.Default.DliPort;
            SelectedDevice.DliIpDevice.Variant = (DliIpDevice.VariantOptions)Enum.Parse(typeof(DliIpDevice.VariantOptions), Properties.Settings.Default.DliVariant);
            SelectedDevice.DeviceType = (BaseDevice.DeviceTypeOptions)Enum.Parse(typeof(BaseDevice.DeviceTypeOptions), Properties.Settings.Default.DeviceType);
            SelectedDevice.KfdDeviceType = (Adapter.Device.TwiKfdDevice)Enum.Parse(typeof(Adapter.Device.TwiKfdDevice), Properties.Settings.Default.KfdDeviceType);
        }
    }
}
