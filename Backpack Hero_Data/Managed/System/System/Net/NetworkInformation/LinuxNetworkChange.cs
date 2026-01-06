using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000527 RID: 1319
	internal sealed class LinuxNetworkChange : INetworkChange, IDisposable
	{
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06002A69 RID: 10857 RVA: 0x0009A8F3 File Offset: 0x00098AF3
		// (remove) Token: 0x06002A6A RID: 10858 RVA: 0x0009A8FC File Offset: 0x00098AFC
		public event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				this.Register(value);
			}
			remove
			{
				this.Unregister(value);
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06002A6B RID: 10859 RVA: 0x0009A905 File Offset: 0x00098B05
		// (remove) Token: 0x06002A6C RID: 10860 RVA: 0x0009A90E File Offset: 0x00098B0E
		public event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				this.Register(value);
			}
			remove
			{
				this.Unregister(value);
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002A6D RID: 10861 RVA: 0x0009A917 File Offset: 0x00098B17
		public bool HasRegisteredEvents
		{
			get
			{
				return this.AddressChanged != null || this.AvailabilityChanged != null;
			}
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose()
		{
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x0009A92C File Offset: 0x00098B2C
		private bool EnsureSocket()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this.nl_sock != null)
				{
					return true;
				}
				IntPtr intPtr = LinuxNetworkChange.CreateNLSocket();
				if (intPtr.ToInt64() == -1L)
				{
					return false;
				}
				SafeSocketHandle safeSocketHandle = new SafeSocketHandle(intPtr, true);
				this.nl_sock = new Socket(AddressFamily.Unspecified, SocketType.Raw, ProtocolType.Udp, safeSocketHandle);
				this.nl_args = new SocketAsyncEventArgs();
				this.nl_args.SetBuffer(new byte[8192], 0, 8192);
				this.nl_args.Completed += this.OnDataAvailable;
				this.nl_sock.ReceiveAsync(this.nl_args);
			}
			return true;
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x0009A9F8 File Offset: 0x00098BF8
		private void MaybeCloseSocket()
		{
			if (this.nl_sock == null || this.AvailabilityChanged != null || this.AddressChanged != null)
			{
				return;
			}
			LinuxNetworkChange.CloseNLSocket(this.nl_sock.Handle);
			GC.SuppressFinalize(this.nl_sock);
			this.nl_sock = null;
			this.nl_args = null;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x0009AA48 File Offset: 0x00098C48
		private bool GetAvailability()
		{
			foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback && networkInterface.OperationalStatus == OperationalStatus.Up)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x0009AA84 File Offset: 0x00098C84
		private void OnAvailabilityChanged(object unused)
		{
			NetworkAvailabilityChangedEventHandler availabilityChanged = this.AvailabilityChanged;
			if (availabilityChanged != null)
			{
				availabilityChanged(null, new NetworkAvailabilityEventArgs(this.GetAvailability()));
			}
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x0009AAB0 File Offset: 0x00098CB0
		private void OnAddressChanged(object unused)
		{
			NetworkAddressChangedEventHandler addressChanged = this.AddressChanged;
			if (addressChanged != null)
			{
				addressChanged(null, EventArgs.Empty);
			}
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x0009AAD4 File Offset: 0x00098CD4
		private void OnEventDue(object unused)
		{
			object @lock = this._lock;
			LinuxNetworkChange.EventType eventType;
			lock (@lock)
			{
				eventType = this.pending_events;
				this.pending_events = (LinuxNetworkChange.EventType)0;
				this.timer.Change(-1, -1);
			}
			if ((eventType & LinuxNetworkChange.EventType.Availability) != (LinuxNetworkChange.EventType)0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.OnAvailabilityChanged));
			}
			if ((eventType & LinuxNetworkChange.EventType.Address) != (LinuxNetworkChange.EventType)0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.OnAddressChanged));
			}
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0009AB58 File Offset: 0x00098D58
		private void QueueEvent(LinuxNetworkChange.EventType type)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this.timer == null)
				{
					this.timer = new Timer(new TimerCallback(this.OnEventDue));
				}
				if (this.pending_events == (LinuxNetworkChange.EventType)0)
				{
					this.timer.Change(150, -1);
				}
				this.pending_events |= type;
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0009ABDC File Offset: 0x00098DDC
		private unsafe void OnDataAvailable(object sender, SocketAsyncEventArgs args)
		{
			if (this.nl_sock == null)
			{
				return;
			}
			byte[] array;
			byte* ptr;
			if ((array = args.Buffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			LinuxNetworkChange.EventType eventType = LinuxNetworkChange.ReadEvents(this.nl_sock.Handle, new IntPtr((void*)ptr), args.BytesTransferred, 8192);
			array = null;
			this.nl_sock.ReceiveAsync(this.nl_args);
			if (eventType != (LinuxNetworkChange.EventType)0)
			{
				this.QueueEvent(eventType);
			}
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x0009AC4F File Offset: 0x00098E4F
		private void Register(NetworkAddressChangedEventHandler d)
		{
			this.EnsureSocket();
			this.AddressChanged = (NetworkAddressChangedEventHandler)Delegate.Combine(this.AddressChanged, d);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x0009AC6F File Offset: 0x00098E6F
		private void Register(NetworkAvailabilityChangedEventHandler d)
		{
			this.EnsureSocket();
			this.AvailabilityChanged = (NetworkAvailabilityChangedEventHandler)Delegate.Combine(this.AvailabilityChanged, d);
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x0009AC90 File Offset: 0x00098E90
		private void Unregister(NetworkAddressChangedEventHandler d)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.AddressChanged = (NetworkAddressChangedEventHandler)Delegate.Remove(this.AddressChanged, d);
				this.MaybeCloseSocket();
			}
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x0009ACE8 File Offset: 0x00098EE8
		private void Unregister(NetworkAvailabilityChangedEventHandler d)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.AvailabilityChanged = (NetworkAvailabilityChangedEventHandler)Delegate.Remove(this.AvailabilityChanged, d);
				this.MaybeCloseSocket();
			}
		}

		// Token: 0x06002A7B RID: 10875
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CreateNLSocket();

		// Token: 0x06002A7C RID: 10876
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern LinuxNetworkChange.EventType ReadEvents(IntPtr sock, IntPtr buffer, int count, int size);

		// Token: 0x06002A7D RID: 10877
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CloseNLSocket(IntPtr sock);

		// Token: 0x040018ED RID: 6381
		private object _lock = new object();

		// Token: 0x040018EE RID: 6382
		private Socket nl_sock;

		// Token: 0x040018EF RID: 6383
		private SocketAsyncEventArgs nl_args;

		// Token: 0x040018F0 RID: 6384
		private LinuxNetworkChange.EventType pending_events;

		// Token: 0x040018F1 RID: 6385
		private Timer timer;

		// Token: 0x040018F2 RID: 6386
		private NetworkAddressChangedEventHandler AddressChanged;

		// Token: 0x040018F3 RID: 6387
		private NetworkAvailabilityChangedEventHandler AvailabilityChanged;

		// Token: 0x02000528 RID: 1320
		[Flags]
		private enum EventType
		{
			// Token: 0x040018F5 RID: 6389
			Availability = 1,
			// Token: 0x040018F6 RID: 6390
			Address = 2
		}
	}
}
