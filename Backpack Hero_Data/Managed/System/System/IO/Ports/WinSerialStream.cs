using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.IO.Ports
{
	// Token: 0x0200084F RID: 2127
	internal class WinSerialStream : Stream, ISerialStream, IDisposable
	{
		// Token: 0x060043B1 RID: 17329
		[DllImport("kernel32", SetLastError = true)]
		private static extern int CreateFile(string port_name, uint desired_access, uint share_mode, uint security_attrs, uint creation, uint flags, uint template);

		// Token: 0x060043B2 RID: 17330
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetupComm(int handle, int read_buffer_size, int write_buffer_size);

		// Token: 0x060043B3 RID: 17331
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool PurgeComm(int handle, uint flags);

		// Token: 0x060043B4 RID: 17332
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetCommTimeouts(int handle, Timeouts timeouts);

		// Token: 0x060043B5 RID: 17333 RVA: 0x000EAAF0 File Offset: 0x000E8CF0
		public WinSerialStream(string port_name, int baud_rate, int data_bits, Parity parity, StopBits sb, bool dtr_enable, bool rts_enable, Handshake hs, int read_timeout, int write_timeout, int read_buffer_size, int write_buffer_size)
		{
			this.handle = WinSerialStream.CreateFile((port_name != null && !port_name.StartsWith("\\\\.\\")) ? ("\\\\.\\" + port_name) : port_name, 3221225472U, 0U, 0U, 3U, 1073741824U, 0U);
			if (this.handle == -1)
			{
				this.ReportIOError(port_name);
			}
			this.SetAttributes(baud_rate, parity, data_bits, sb, hs);
			if (!WinSerialStream.PurgeComm(this.handle, 12U) || !WinSerialStream.SetupComm(this.handle, read_buffer_size, write_buffer_size))
			{
				this.ReportIOError(null);
			}
			this.read_timeout = read_timeout;
			this.write_timeout = write_timeout;
			this.timeouts = new Timeouts(read_timeout, write_timeout);
			if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
			{
				this.ReportIOError(null);
			}
			this.SetSignal(SerialSignal.Dtr, dtr_enable);
			if (hs != Handshake.RequestToSend && hs != Handshake.RequestToSendXOnXOff)
			{
				this.SetSignal(SerialSignal.Rts, rts_enable);
			}
			NativeOverlapped nativeOverlapped = default(NativeOverlapped);
			this.write_event = new ManualResetEvent(false);
			nativeOverlapped.EventHandle = this.write_event.Handle;
			this.write_overlapped = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			Marshal.StructureToPtr<NativeOverlapped>(nativeOverlapped, this.write_overlapped, true);
			NativeOverlapped nativeOverlapped2 = default(NativeOverlapped);
			this.read_event = new ManualResetEvent(false);
			nativeOverlapped2.EventHandle = this.read_event.Handle;
			this.read_overlapped = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			Marshal.StructureToPtr<NativeOverlapped>(nativeOverlapped2, this.read_overlapped, true);
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x060043B6 RID: 17334 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x060043B7 RID: 17335 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x060043B8 RID: 17336 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x060043BA RID: 17338 RVA: 0x000EAC6F File Offset: 0x000E8E6F
		// (set) Token: 0x060043BB RID: 17339 RVA: 0x000EAC78 File Offset: 0x000E8E78
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeouts.SetValues(value, this.write_timeout);
				if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
				{
					this.ReportIOError(null);
				}
				this.read_timeout = value;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x060043BC RID: 17340 RVA: 0x000EACCB File Offset: 0x000E8ECB
		// (set) Token: 0x060043BD RID: 17341 RVA: 0x000EACD4 File Offset: 0x000E8ED4
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeouts.SetValues(this.read_timeout, value);
				if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
				{
					this.ReportIOError(null);
				}
				this.write_timeout = value;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x060043BE RID: 17342 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x060043C0 RID: 17344 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060043C1 RID: 17345
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool CloseHandle(int handle);

		// Token: 0x060043C2 RID: 17346 RVA: 0x000EAD27 File Offset: 0x000E8F27
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			WinSerialStream.CloseHandle(this.handle);
			Marshal.FreeHGlobal(this.write_overlapped);
			Marshal.FreeHGlobal(this.read_overlapped);
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x000EA959 File Offset: 0x000E8B59
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x0000BBD5 File Offset: 0x00009DD5
		public override void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x000EAD5C File Offset: 0x000E8F5C
		~WinSerialStream()
		{
			this.Dispose(false);
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x000EAD8C File Offset: 0x000E8F8C
		public override void Flush()
		{
			this.CheckDisposed();
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043C9 RID: 17353
		[DllImport("kernel32", SetLastError = true)]
		private unsafe static extern bool ReadFile(int handle, byte* buffer, int bytes_to_read, out int bytes_read, IntPtr overlapped);

		// Token: 0x060043CA RID: 17354
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetOverlappedResult(int handle, IntPtr overlapped, ref int bytes_transfered, bool wait);

		// Token: 0x060043CB RID: 17355 RVA: 0x000EAD94 File Offset: 0x000E8F94
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (WinSerialStream.ReadFile(this.handle, ptr + offset, count, out num, this.read_overlapped))
				{
					return num;
				}
				if ((long)Marshal.GetLastWin32Error() != 997L)
				{
					this.ReportIOError(null);
				}
				if (!WinSerialStream.GetOverlappedResult(this.handle, this.read_overlapped, ref num, true))
				{
					this.ReportIOError(null);
				}
			}
			if (num == 0)
			{
				throw new TimeoutException();
			}
			return num;
		}

		// Token: 0x060043CC RID: 17356
		[DllImport("kernel32", SetLastError = true)]
		private unsafe static extern bool WriteFile(int handle, byte* buffer, int bytes_to_write, out int bytes_written, IntPtr overlapped);

		// Token: 0x060043CD RID: 17357 RVA: 0x000EAE54 File Offset: 0x000E9054
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			int num = 0;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (WinSerialStream.WriteFile(this.handle, ptr + offset, count, out num, this.write_overlapped))
				{
					return;
				}
				if ((long)Marshal.GetLastWin32Error() != 997L)
				{
					this.ReportIOError(null);
				}
				if (!WinSerialStream.GetOverlappedResult(this.handle, this.write_overlapped, ref num, true))
				{
					this.ReportIOError(null);
				}
			}
			if (num < count)
			{
				throw new TimeoutException();
			}
		}

		// Token: 0x060043CE RID: 17358
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetCommState(int handle, [Out] DCB dcb);

		// Token: 0x060043CF RID: 17359
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetCommState(int handle, DCB dcb);

		// Token: 0x060043D0 RID: 17360 RVA: 0x000EAF10 File Offset: 0x000E9110
		public void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits bits, Handshake hs)
		{
			DCB dcb = new DCB();
			if (!WinSerialStream.GetCommState(this.handle, dcb))
			{
				this.ReportIOError(null);
			}
			dcb.SetValues(baud_rate, parity, data_bits, bits, hs);
			if (!WinSerialStream.SetCommState(this.handle, dcb))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x000EAF5C File Offset: 0x000E915C
		private void ReportIOError(string optional_arg)
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			string text;
			if (lastWin32Error - 2 > 1)
			{
				if (lastWin32Error != 87)
				{
					text = new Win32Exception().Message;
				}
				else
				{
					text = "Parameter is incorrect.";
				}
			}
			else
			{
				text = "The port `" + optional_arg + "' does not exist.";
			}
			throw new IOException(text);
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x000EAFA8 File Offset: 0x000E91A8
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x000EAFC3 File Offset: 0x000E91C3
		public void DiscardInBuffer()
		{
			if (!WinSerialStream.PurgeComm(this.handle, 8U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x000EAFDA File Offset: 0x000E91DA
		public void DiscardOutBuffer()
		{
			if (!WinSerialStream.PurgeComm(this.handle, 4U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x060043D5 RID: 17365
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool ClearCommError(int handle, out uint errors, out CommStat stat);

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x060043D6 RID: 17366 RVA: 0x000EAFF4 File Offset: 0x000E91F4
		public int BytesToRead
		{
			get
			{
				uint num;
				CommStat commStat;
				if (!WinSerialStream.ClearCommError(this.handle, out num, out commStat))
				{
					this.ReportIOError(null);
				}
				return (int)commStat.BytesIn;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x060043D7 RID: 17367 RVA: 0x000EB020 File Offset: 0x000E9220
		public int BytesToWrite
		{
			get
			{
				uint num;
				CommStat commStat;
				if (!WinSerialStream.ClearCommError(this.handle, out num, out commStat))
				{
					this.ReportIOError(null);
				}
				return (int)commStat.BytesOut;
			}
		}

		// Token: 0x060043D8 RID: 17368
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetCommModemStatus(int handle, out uint flags);

		// Token: 0x060043D9 RID: 17369 RVA: 0x000EB04C File Offset: 0x000E924C
		public SerialSignal GetSignals()
		{
			uint num;
			if (!WinSerialStream.GetCommModemStatus(this.handle, out num))
			{
				this.ReportIOError(null);
			}
			SerialSignal serialSignal = SerialSignal.None;
			if ((num & 128U) != 0U)
			{
				serialSignal |= SerialSignal.Cd;
			}
			if ((num & 16U) != 0U)
			{
				serialSignal |= SerialSignal.Cts;
			}
			if ((num & 32U) != 0U)
			{
				serialSignal |= SerialSignal.Dsr;
			}
			return serialSignal;
		}

		// Token: 0x060043DA RID: 17370
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool EscapeCommFunction(int handle, uint flags);

		// Token: 0x060043DB RID: 17371 RVA: 0x000EB094 File Offset: 0x000E9294
		public void SetSignal(SerialSignal signal, bool value)
		{
			if (signal != SerialSignal.Rts && signal != SerialSignal.Dtr)
			{
				throw new Exception("Wrong internal value");
			}
			uint num;
			if (signal == SerialSignal.Rts)
			{
				if (value)
				{
					num = 3U;
				}
				else
				{
					num = 4U;
				}
			}
			else if (value)
			{
				num = 5U;
			}
			else
			{
				num = 6U;
			}
			if (!WinSerialStream.EscapeCommFunction(this.handle, num))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x000EB0E3 File Offset: 0x000E92E3
		public void SetBreakState(bool value)
		{
			if (!WinSerialStream.EscapeCommFunction(this.handle, value ? 8U : 9U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x040028B7 RID: 10423
		private const uint GenericRead = 2147483648U;

		// Token: 0x040028B8 RID: 10424
		private const uint GenericWrite = 1073741824U;

		// Token: 0x040028B9 RID: 10425
		private const uint OpenExisting = 3U;

		// Token: 0x040028BA RID: 10426
		private const uint FileFlagOverlapped = 1073741824U;

		// Token: 0x040028BB RID: 10427
		private const uint PurgeRxClear = 8U;

		// Token: 0x040028BC RID: 10428
		private const uint PurgeTxClear = 4U;

		// Token: 0x040028BD RID: 10429
		private const uint WinInfiniteTimeout = 4294967295U;

		// Token: 0x040028BE RID: 10430
		private const uint FileIOPending = 997U;

		// Token: 0x040028BF RID: 10431
		private const uint SetRts = 3U;

		// Token: 0x040028C0 RID: 10432
		private const uint ClearRts = 4U;

		// Token: 0x040028C1 RID: 10433
		private const uint SetDtr = 5U;

		// Token: 0x040028C2 RID: 10434
		private const uint ClearDtr = 6U;

		// Token: 0x040028C3 RID: 10435
		private const uint SetBreak = 8U;

		// Token: 0x040028C4 RID: 10436
		private const uint ClearBreak = 9U;

		// Token: 0x040028C5 RID: 10437
		private const uint CtsOn = 16U;

		// Token: 0x040028C6 RID: 10438
		private const uint DsrOn = 32U;

		// Token: 0x040028C7 RID: 10439
		private const uint RsldOn = 128U;

		// Token: 0x040028C8 RID: 10440
		private const uint EvRxChar = 1U;

		// Token: 0x040028C9 RID: 10441
		private const uint EvCts = 8U;

		// Token: 0x040028CA RID: 10442
		private const uint EvDsr = 16U;

		// Token: 0x040028CB RID: 10443
		private const uint EvRlsd = 32U;

		// Token: 0x040028CC RID: 10444
		private const uint EvBreak = 64U;

		// Token: 0x040028CD RID: 10445
		private const uint EvErr = 128U;

		// Token: 0x040028CE RID: 10446
		private const uint EvRing = 256U;

		// Token: 0x040028CF RID: 10447
		private int handle;

		// Token: 0x040028D0 RID: 10448
		private int read_timeout;

		// Token: 0x040028D1 RID: 10449
		private int write_timeout;

		// Token: 0x040028D2 RID: 10450
		private bool disposed;

		// Token: 0x040028D3 RID: 10451
		private IntPtr write_overlapped;

		// Token: 0x040028D4 RID: 10452
		private IntPtr read_overlapped;

		// Token: 0x040028D5 RID: 10453
		private ManualResetEvent read_event;

		// Token: 0x040028D6 RID: 10454
		private ManualResetEvent write_event;

		// Token: 0x040028D7 RID: 10455
		private Timeouts timeouts;
	}
}
