using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x0200084B RID: 2123
	internal class SerialPortStream : Stream, ISerialStream, IDisposable
	{
		// Token: 0x06004381 RID: 17281
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int open_serial(string portName);

		// Token: 0x06004382 RID: 17282 RVA: 0x000EA780 File Offset: 0x000E8980
		public SerialPortStream(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits, bool dtrEnable, bool rtsEnable, Handshake handshake, int readTimeout, int writeTimeout, int readBufferSize, int writeBufferSize)
		{
			this.fd = SerialPortStream.open_serial(portName);
			if (this.fd == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			this.TryBaudRate(baudRate);
			if (!SerialPortStream.set_attributes(this.fd, baudRate, parity, dataBits, stopBits, handshake))
			{
				SerialPortStream.ThrowIOException();
			}
			this.read_timeout = readTimeout;
			this.write_timeout = writeTimeout;
			this.SetSignal(SerialSignal.Dtr, dtrEnable);
			if (handshake != Handshake.RequestToSend && handshake != Handshake.RequestToSendXOnXOff)
			{
				this.SetSignal(SerialSignal.Rts, rtsEnable);
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06004383 RID: 17283 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06004384 RID: 17284 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06004385 RID: 17285 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06004386 RID: 17286 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06004387 RID: 17287 RVA: 0x000EA7FB File Offset: 0x000E89FB
		// (set) Token: 0x06004388 RID: 17288 RVA: 0x000EA803 File Offset: 0x000E8A03
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
				this.read_timeout = value;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06004389 RID: 17289 RVA: 0x000EA81F File Offset: 0x000E8A1F
		// (set) Token: 0x0600438A RID: 17290 RVA: 0x000EA827 File Offset: 0x000E8A27
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
				this.write_timeout = value;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x0600438B RID: 17291 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x0600438C RID: 17292 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x0600438D RID: 17293 RVA: 0x000044FA File Offset: 0x000026FA
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

		// Token: 0x0600438E RID: 17294 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x0600438F RID: 17295
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int read_serial(int fd, byte[] buffer, int offset, int count);

		// Token: 0x06004390 RID: 17296
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern bool poll_serial(int fd, out int error, int timeout);

		// Token: 0x06004391 RID: 17297 RVA: 0x000EA844 File Offset: 0x000E8A44
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
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
			bool flag = SerialPortStream.poll_serial(this.fd, out num, this.read_timeout);
			if (num == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			if (!flag)
			{
				throw new TimeoutException();
			}
			int num2 = SerialPortStream.read_serial(this.fd, buffer, offset, count);
			if (num2 == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			return num2;
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004394 RID: 17300
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int write_serial(int fd, byte[] buffer, int offset, int count, int timeout);

		// Token: 0x06004395 RID: 17301 RVA: 0x000EA8CC File Offset: 0x000E8ACC
		public override void Write(byte[] buffer, int offset, int count)
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
			if (SerialPortStream.write_serial(this.fd, buffer, offset, count, this.write_timeout) < 0)
			{
				throw new TimeoutException("The operation has timed-out");
			}
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x000EA935 File Offset: 0x000E8B35
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (SerialPortStream.close_serial(this.fd) != 0)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06004397 RID: 17303
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int close_serial(int fd);

		// Token: 0x06004398 RID: 17304 RVA: 0x0000BBD5 File Offset: 0x00009DD5
		public override void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x000EA959 File Offset: 0x000E8B59
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x000EA968 File Offset: 0x000E8B68
		~SerialPortStream()
		{
			try
			{
				this.Dispose(false);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000EA9A4 File Offset: 0x000E8BA4
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0600439C RID: 17308
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern bool set_attributes(int fd, int baudRate, Parity parity, int dataBits, StopBits stopBits, Handshake handshake);

		// Token: 0x0600439D RID: 17309 RVA: 0x000EA9BF File Offset: 0x000E8BBF
		public void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits sb, Handshake hs)
		{
			if (!SerialPortStream.set_attributes(this.fd, baud_rate, parity, data_bits, sb, hs))
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x0600439E RID: 17310
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int get_bytes_in_buffer(int fd, int input);

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x0600439F RID: 17311 RVA: 0x000EA9DA File Offset: 0x000E8BDA
		public int BytesToRead
		{
			get
			{
				int num = SerialPortStream.get_bytes_in_buffer(this.fd, 1);
				if (num == -1)
				{
					SerialPortStream.ThrowIOException();
				}
				return num;
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x060043A0 RID: 17312 RVA: 0x000EA9F1 File Offset: 0x000E8BF1
		public int BytesToWrite
		{
			get
			{
				int num = SerialPortStream.get_bytes_in_buffer(this.fd, 0);
				if (num == -1)
				{
					SerialPortStream.ThrowIOException();
				}
				return num;
			}
		}

		// Token: 0x060043A1 RID: 17313
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int discard_buffer(int fd, bool inputBuffer);

		// Token: 0x060043A2 RID: 17314 RVA: 0x000EAA08 File Offset: 0x000E8C08
		public void DiscardInBuffer()
		{
			if (SerialPortStream.discard_buffer(this.fd, true) != 0)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x000EAA1D File Offset: 0x000E8C1D
		public void DiscardOutBuffer()
		{
			if (SerialPortStream.discard_buffer(this.fd, false) != 0)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x060043A4 RID: 17316
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern SerialSignal get_signals(int fd, out int error);

		// Token: 0x060043A5 RID: 17317 RVA: 0x000EAA34 File Offset: 0x000E8C34
		public SerialSignal GetSignals()
		{
			int num;
			SerialSignal serialSignal = SerialPortStream.get_signals(this.fd, out num);
			if (num == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			return serialSignal;
		}

		// Token: 0x060043A6 RID: 17318
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int set_signal(int fd, SerialSignal signal, bool value);

		// Token: 0x060043A7 RID: 17319 RVA: 0x000EAA57 File Offset: 0x000E8C57
		public void SetSignal(SerialSignal signal, bool value)
		{
			if (signal < SerialSignal.Cd || signal > SerialSignal.Rts || signal == SerialSignal.Cd || signal == SerialSignal.Cts || signal == SerialSignal.Dsr)
			{
				throw new Exception("Invalid internal value");
			}
			if (SerialPortStream.set_signal(this.fd, signal, value) == -1)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x060043A8 RID: 17320
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int breakprop(int fd);

		// Token: 0x060043A9 RID: 17321 RVA: 0x000EAA8E File Offset: 0x000E8C8E
		public void SetBreakState(bool value)
		{
			if (value && SerialPortStream.breakprop(this.fd) == -1)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x060043AA RID: 17322
		[DllImport("libc")]
		private static extern IntPtr strerror(int errnum);

		// Token: 0x060043AB RID: 17323 RVA: 0x000EAAA6 File Offset: 0x000E8CA6
		private static void ThrowIOException()
		{
			throw new IOException(Marshal.PtrToStringAnsi(SerialPortStream.strerror(Marshal.GetLastWin32Error())));
		}

		// Token: 0x060043AC RID: 17324
		[DllImport("MonoPosixHelper")]
		private static extern bool is_baud_rate_legal(int baud_rate);

		// Token: 0x060043AD RID: 17325 RVA: 0x000EAABC File Offset: 0x000E8CBC
		private void TryBaudRate(int baudRate)
		{
			if (!SerialPortStream.is_baud_rate_legal(baudRate))
			{
				throw new ArgumentOutOfRangeException("baudRate", "Given baud rate is not supported on this platform.");
			}
		}

		// Token: 0x040028A6 RID: 10406
		private int fd;

		// Token: 0x040028A7 RID: 10407
		private int read_timeout;

		// Token: 0x040028A8 RID: 10408
		private int write_timeout;

		// Token: 0x040028A9 RID: 10409
		private bool disposed;
	}
}
