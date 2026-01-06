using System;
using System.ComponentModel;
using Unity;

namespace System.Net.NetworkInformation
{
	/// <summary>Allows applications to receive notification when the Internet Protocol (IP) address of a network interface, also called a network card or adapter, changes.</summary>
	// Token: 0x02000521 RID: 1313
	public sealed class NetworkChange
	{
		/// <summary>Occurs when the IP address of a network interface changes.</summary>
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06002A43 RID: 10819 RVA: 0x0009A318 File Offset: 0x00098518
		// (remove) Token: 0x06002A44 RID: 10820 RVA: 0x0009A370 File Offset: 0x00098570
		public static event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					NetworkChange.MaybeCreate();
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAddressChanged += value;
					}
				}
			}
			remove
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAddressChanged -= value;
						NetworkChange.MaybeDispose();
					}
				}
			}
		}

		/// <summary>Occurs when the availability of the network changes.</summary>
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06002A45 RID: 10821 RVA: 0x0009A3C8 File Offset: 0x000985C8
		// (remove) Token: 0x06002A46 RID: 10822 RVA: 0x0009A420 File Offset: 0x00098620
		public static event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					NetworkChange.MaybeCreate();
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAvailabilityChanged += value;
					}
				}
			}
			remove
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAvailabilityChanged -= value;
						NetworkChange.MaybeDispose();
					}
				}
			}
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x0009A478 File Offset: 0x00098678
		private static void MaybeCreate()
		{
			if (NetworkChange.networkChange != null)
			{
				return;
			}
			if (NetworkChange.IsWindows)
			{
				throw new PlatformNotSupportedException("NetworkInformation.NetworkChange is not supported on the current platform.");
			}
			try
			{
				NetworkChange.networkChange = new MacNetworkChange();
			}
			catch
			{
				NetworkChange.networkChange = new LinuxNetworkChange();
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x0009A4CC File Offset: 0x000986CC
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT || platform == PlatformID.WinCE;
			}
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x0009A4F6 File Offset: 0x000986F6
		private static void MaybeDispose()
		{
			if (NetworkChange.networkChange != null && NetworkChange.networkChange.HasRegisteredEvents)
			{
				NetworkChange.networkChange.Dispose();
				NetworkChange.networkChange = null;
			}
		}

		/// <summary>Registers a network change instance to receive network change events.</summary>
		/// <param name="nc">The instance to register. </param>
		// Token: 0x06002A4B RID: 10827 RVA: 0x00013B26 File Offset: 0x00011D26
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public static void RegisterNetworkChange(NetworkChange nc)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040018CF RID: 6351
		private static INetworkChange networkChange;
	}
}
