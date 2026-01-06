using System;
using System.Runtime.InteropServices;
using Mono.Util;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000522 RID: 1314
	internal sealed class MacNetworkChange : INetworkChange, IDisposable
	{
		// Token: 0x06002A4C RID: 10828
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern IntPtr dlopen(string path, int mode);

		// Token: 0x06002A4D RID: 10829
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern IntPtr dlsym(IntPtr handle, string symbol);

		// Token: 0x06002A4E RID: 10830
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern int dlclose(IntPtr handle);

		// Token: 0x06002A4F RID: 10831
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRelease(IntPtr handle);

		// Token: 0x06002A50 RID: 10832
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFRunLoopGetMain();

		// Token: 0x06002A51 RID: 10833
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern IntPtr SCNetworkReachabilityCreateWithAddress(IntPtr allocator, ref MacNetworkChange.sockaddr_in sockaddr);

		// Token: 0x06002A52 RID: 10834
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityGetFlags(IntPtr reachability, out MacNetworkChange.NetworkReachabilityFlags flags);

		// Token: 0x06002A53 RID: 10835
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilitySetCallback(IntPtr reachability, MacNetworkChange.SCNetworkReachabilityCallback callback, ref MacNetworkChange.SCNetworkReachabilityContext context);

		// Token: 0x06002A54 RID: 10836
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityScheduleWithRunLoop(IntPtr reachability, IntPtr runLoop, IntPtr runLoopMode);

		// Token: 0x06002A55 RID: 10837
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityUnscheduleFromRunLoop(IntPtr reachability, IntPtr runLoop, IntPtr runLoopMode);

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06002A56 RID: 10838 RVA: 0x0009A51C File Offset: 0x0009871C
		// (remove) Token: 0x06002A57 RID: 10839 RVA: 0x0009A554 File Offset: 0x00098754
		private event NetworkAddressChangedEventHandler networkAddressChanged;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06002A58 RID: 10840 RVA: 0x0009A58C File Offset: 0x0009878C
		// (remove) Token: 0x06002A59 RID: 10841 RVA: 0x0009A5C4 File Offset: 0x000987C4
		private event NetworkAvailabilityChangedEventHandler networkAvailabilityChanged;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06002A5A RID: 10842 RVA: 0x0009A5F9 File Offset: 0x000987F9
		// (remove) Token: 0x06002A5B RID: 10843 RVA: 0x0009A60E File Offset: 0x0009880E
		public event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				value(null, EventArgs.Empty);
				this.networkAddressChanged += value;
			}
			remove
			{
				this.networkAddressChanged -= value;
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06002A5C RID: 10844 RVA: 0x0009A617 File Offset: 0x00098817
		// (remove) Token: 0x06002A5D RID: 10845 RVA: 0x0009A632 File Offset: 0x00098832
		public event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				value(null, new NetworkAvailabilityEventArgs(this.IsAvailable));
				this.networkAvailabilityChanged += value;
			}
			remove
			{
				this.networkAvailabilityChanged -= value;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x0009A63B File Offset: 0x0009883B
		private bool IsAvailable
		{
			get
			{
				return (this.flags & MacNetworkChange.NetworkReachabilityFlags.Reachable) != MacNetworkChange.NetworkReachabilityFlags.None && (this.flags & MacNetworkChange.NetworkReachabilityFlags.ConnectionRequired) == MacNetworkChange.NetworkReachabilityFlags.None;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x0009A654 File Offset: 0x00098854
		public bool HasRegisteredEvents
		{
			get
			{
				return this.networkAddressChanged != null || this.networkAvailabilityChanged != null;
			}
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x0009A66C File Offset: 0x0009886C
		public MacNetworkChange()
		{
			MacNetworkChange.sockaddr_in sockaddr_in = MacNetworkChange.sockaddr_in.Create();
			this.handle = MacNetworkChange.SCNetworkReachabilityCreateWithAddress(IntPtr.Zero, ref sockaddr_in);
			if (this.handle == IntPtr.Zero)
			{
				throw new Exception("SCNetworkReachabilityCreateWithAddress returned NULL");
			}
			this.callback = new MacNetworkChange.SCNetworkReachabilityCallback(MacNetworkChange.HandleCallback);
			MacNetworkChange.SCNetworkReachabilityContext scnetworkReachabilityContext = new MacNetworkChange.SCNetworkReachabilityContext
			{
				info = GCHandle.ToIntPtr(GCHandle.Alloc(this))
			};
			MacNetworkChange.SCNetworkReachabilitySetCallback(this.handle, this.callback, ref scnetworkReachabilityContext);
			this.scheduledWithRunLoop = this.LoadRunLoopMode() && MacNetworkChange.SCNetworkReachabilityScheduleWithRunLoop(this.handle, MacNetworkChange.CFRunLoopGetMain(), this.runLoopMode);
			MacNetworkChange.SCNetworkReachabilityGetFlags(this.handle, out this.flags);
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x0009A730 File Offset: 0x00098930
		private bool LoadRunLoopMode()
		{
			IntPtr intPtr = MacNetworkChange.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (intPtr == IntPtr.Zero)
			{
				return false;
			}
			try
			{
				this.runLoopMode = MacNetworkChange.dlsym(intPtr, "kCFRunLoopDefaultMode");
				if (this.runLoopMode != IntPtr.Zero)
				{
					this.runLoopMode = Marshal.ReadIntPtr(this.runLoopMode);
					return this.runLoopMode != IntPtr.Zero;
				}
			}
			finally
			{
				MacNetworkChange.dlclose(intPtr);
			}
			return false;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x0009A7C0 File Offset: 0x000989C0
		public void Dispose()
		{
			lock (this)
			{
				if (!(this.handle == IntPtr.Zero))
				{
					if (this.scheduledWithRunLoop)
					{
						MacNetworkChange.SCNetworkReachabilityUnscheduleFromRunLoop(this.handle, MacNetworkChange.CFRunLoopGetMain(), this.runLoopMode);
					}
					MacNetworkChange.CFRelease(this.handle);
					this.handle = IntPtr.Zero;
					this.callback = null;
					this.flags = MacNetworkChange.NetworkReachabilityFlags.None;
					this.scheduledWithRunLoop = false;
				}
			}
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x0009A854 File Offset: 0x00098A54
		[MonoPInvokeCallback(typeof(MacNetworkChange.SCNetworkReachabilityCallback))]
		private static void HandleCallback(IntPtr reachability, MacNetworkChange.NetworkReachabilityFlags flags, IntPtr info)
		{
			if (info == IntPtr.Zero)
			{
				return;
			}
			MacNetworkChange macNetworkChange = GCHandle.FromIntPtr(info).Target as MacNetworkChange;
			if (macNetworkChange == null || macNetworkChange.flags == flags)
			{
				return;
			}
			macNetworkChange.flags = flags;
			NetworkAddressChangedEventHandler networkAddressChangedEventHandler = macNetworkChange.networkAddressChanged;
			if (networkAddressChangedEventHandler != null)
			{
				networkAddressChangedEventHandler(null, EventArgs.Empty);
			}
			NetworkAvailabilityChangedEventHandler networkAvailabilityChangedEventHandler = macNetworkChange.networkAvailabilityChanged;
			if (networkAvailabilityChangedEventHandler != null)
			{
				networkAvailabilityChangedEventHandler(null, new NetworkAvailabilityEventArgs(macNetworkChange.IsAvailable));
			}
		}

		// Token: 0x040018D0 RID: 6352
		private const string DL_LIB = "/usr/lib/libSystem.dylib";

		// Token: 0x040018D1 RID: 6353
		private const string CORE_SERVICES_LIB = "/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration";

		// Token: 0x040018D2 RID: 6354
		private const string CORE_FOUNDATION_LIB = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		// Token: 0x040018D3 RID: 6355
		private IntPtr handle;

		// Token: 0x040018D4 RID: 6356
		private IntPtr runLoopMode;

		// Token: 0x040018D5 RID: 6357
		private MacNetworkChange.SCNetworkReachabilityCallback callback;

		// Token: 0x040018D6 RID: 6358
		private bool scheduledWithRunLoop;

		// Token: 0x040018D7 RID: 6359
		private MacNetworkChange.NetworkReachabilityFlags flags;

		// Token: 0x02000523 RID: 1315
		// (Invoke) Token: 0x06002A65 RID: 10853
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void SCNetworkReachabilityCallback(IntPtr target, MacNetworkChange.NetworkReachabilityFlags flags, IntPtr info);

		// Token: 0x02000524 RID: 1316
		[StructLayout(LayoutKind.Explicit, Size = 28)]
		private struct sockaddr_in
		{
			// Token: 0x06002A68 RID: 10856 RVA: 0x0009A8CC File Offset: 0x00098ACC
			public static MacNetworkChange.sockaddr_in Create()
			{
				return new MacNetworkChange.sockaddr_in
				{
					sin_len = 28,
					sin_family = 2
				};
			}

			// Token: 0x040018DA RID: 6362
			[FieldOffset(0)]
			public byte sin_len;

			// Token: 0x040018DB RID: 6363
			[FieldOffset(1)]
			public byte sin_family;
		}

		// Token: 0x02000525 RID: 1317
		private struct SCNetworkReachabilityContext
		{
			// Token: 0x040018DC RID: 6364
			public IntPtr version;

			// Token: 0x040018DD RID: 6365
			public IntPtr info;

			// Token: 0x040018DE RID: 6366
			public IntPtr retain;

			// Token: 0x040018DF RID: 6367
			public IntPtr release;

			// Token: 0x040018E0 RID: 6368
			public IntPtr copyDescription;
		}

		// Token: 0x02000526 RID: 1318
		[Flags]
		private enum NetworkReachabilityFlags
		{
			// Token: 0x040018E2 RID: 6370
			None = 0,
			// Token: 0x040018E3 RID: 6371
			TransientConnection = 1,
			// Token: 0x040018E4 RID: 6372
			Reachable = 2,
			// Token: 0x040018E5 RID: 6373
			ConnectionRequired = 4,
			// Token: 0x040018E6 RID: 6374
			ConnectionOnTraffic = 8,
			// Token: 0x040018E7 RID: 6375
			InterventionRequired = 16,
			// Token: 0x040018E8 RID: 6376
			ConnectionOnDemand = 32,
			// Token: 0x040018E9 RID: 6377
			IsLocalAddress = 65536,
			// Token: 0x040018EA RID: 6378
			IsDirect = 131072,
			// Token: 0x040018EB RID: 6379
			IsWWAN = 262144,
			// Token: 0x040018EC RID: 6380
			ConnectionAutomatic = 8
		}
	}
}
