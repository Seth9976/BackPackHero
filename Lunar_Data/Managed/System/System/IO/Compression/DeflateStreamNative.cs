using System;
using System.Runtime.InteropServices;
using System.Threading;
using Mono.Util;

namespace System.IO.Compression
{
	// Token: 0x0200085E RID: 2142
	internal class DeflateStreamNative
	{
		// Token: 0x06004487 RID: 17543 RVA: 0x0000219B File Offset: 0x0000039B
		private DeflateStreamNative()
		{
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x000ECA4C File Offset: 0x000EAC4C
		public static DeflateStreamNative Create(Stream compressedStream, CompressionMode mode, bool gzip)
		{
			DeflateStreamNative deflateStreamNative = new DeflateStreamNative();
			deflateStreamNative.data = GCHandle.Alloc(deflateStreamNative);
			deflateStreamNative.feeder = ((mode == CompressionMode.Compress) ? new DeflateStreamNative.UnmanagedReadOrWrite(DeflateStreamNative.UnmanagedWrite) : new DeflateStreamNative.UnmanagedReadOrWrite(DeflateStreamNative.UnmanagedRead));
			deflateStreamNative.z_stream = DeflateStreamNative.CreateZStream(mode, gzip, deflateStreamNative.feeder, GCHandle.ToIntPtr(deflateStreamNative.data));
			if (deflateStreamNative.z_stream.IsInvalid)
			{
				deflateStreamNative.Dispose(true);
				return null;
			}
			deflateStreamNative.base_stream = compressedStream;
			return deflateStreamNative;
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x000ECACC File Offset: 0x000EACCC
		~DeflateStreamNative()
		{
			this.Dispose(false);
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x000ECAFC File Offset: 0x000EACFC
		public void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				GC.SuppressFinalize(this);
			}
			else
			{
				this.base_stream = Stream.Null;
			}
			this.io_buffer = null;
			if (this.z_stream != null && !this.z_stream.IsInvalid)
			{
				this.z_stream.Dispose();
			}
			GCHandle gchandle = this.data;
			if (this.data.IsAllocated)
			{
				this.data.Free();
			}
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x000ECB74 File Offset: 0x000EAD74
		public void Flush()
		{
			int num = DeflateStreamNative.Flush(this.z_stream);
			this.CheckResult(num, "Flush");
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x000ECB9C File Offset: 0x000EAD9C
		public int ReadZStream(IntPtr buffer, int length)
		{
			int num = DeflateStreamNative.ReadZStream(this.z_stream, buffer, length);
			this.CheckResult(num, "ReadInternal");
			return num;
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x000ECBC4 File Offset: 0x000EADC4
		public void WriteZStream(IntPtr buffer, int length)
		{
			int num = DeflateStreamNative.WriteZStream(this.z_stream, buffer, length);
			this.CheckResult(num, "WriteInternal");
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x000ECBEC File Offset: 0x000EADEC
		[MonoPInvokeCallback(typeof(DeflateStreamNative.UnmanagedReadOrWrite))]
		private static int UnmanagedRead(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStreamNative deflateStreamNative = GCHandle.FromIntPtr(data).Target as DeflateStreamNative;
			if (deflateStreamNative == null)
			{
				return -1;
			}
			return deflateStreamNative.UnmanagedRead(buffer, length);
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x000ECC1C File Offset: 0x000EAE1C
		private int UnmanagedRead(IntPtr buffer, int length)
		{
			if (this.io_buffer == null)
			{
				this.io_buffer = new byte[4096];
			}
			int num = Math.Min(length, this.io_buffer.Length);
			int num2;
			try
			{
				num2 = this.base_stream.Read(this.io_buffer, 0, num);
			}
			catch (Exception ex)
			{
				this.last_error = ex;
				return -12;
			}
			if (num2 > 0)
			{
				Marshal.Copy(this.io_buffer, 0, buffer, num2);
			}
			return num2;
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x000ECC98 File Offset: 0x000EAE98
		[MonoPInvokeCallback(typeof(DeflateStreamNative.UnmanagedReadOrWrite))]
		private static int UnmanagedWrite(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStreamNative deflateStreamNative = GCHandle.FromIntPtr(data).Target as DeflateStreamNative;
			if (deflateStreamNative == null)
			{
				return -1;
			}
			return deflateStreamNative.UnmanagedWrite(buffer, length);
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x000ECCC8 File Offset: 0x000EAEC8
		private unsafe int UnmanagedWrite(IntPtr buffer, int length)
		{
			int num = 0;
			while (length > 0)
			{
				if (this.io_buffer == null)
				{
					this.io_buffer = new byte[4096];
				}
				int num2 = Math.Min(length, this.io_buffer.Length);
				Marshal.Copy(buffer, this.io_buffer, 0, num2);
				try
				{
					this.base_stream.Write(this.io_buffer, 0, num2);
				}
				catch (Exception ex)
				{
					this.last_error = ex;
					return -12;
				}
				buffer = new IntPtr((void*)((byte*)buffer.ToPointer() + num2));
				length -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x000ECD60 File Offset: 0x000EAF60
		private void CheckResult(int result, string where)
		{
			if (result >= 0)
			{
				return;
			}
			Exception ex = Interlocked.Exchange<Exception>(ref this.last_error, null);
			if (ex != null)
			{
				throw ex;
			}
			string text;
			switch (result)
			{
			case -11:
				text = "IO error";
				goto IL_0094;
			case -10:
				text = "Invalid argument(s)";
				goto IL_0094;
			case -6:
				text = "Invalid version";
				goto IL_0094;
			case -5:
				text = "Internal error (no progress possible)";
				goto IL_0094;
			case -4:
				text = "Not enough memory";
				goto IL_0094;
			case -3:
				text = "Corrupted data";
				goto IL_0094;
			case -2:
				text = "Internal error";
				goto IL_0094;
			case -1:
				text = "Unknown error";
				goto IL_0094;
			}
			text = "Unknown error";
			IL_0094:
			throw new IOException(text + " " + where);
		}

		// Token: 0x06004493 RID: 17555
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern DeflateStreamNative.SafeDeflateStreamHandle CreateZStream(CompressionMode compress, bool gzip, DeflateStreamNative.UnmanagedReadOrWrite feeder, IntPtr data);

		// Token: 0x06004494 RID: 17556
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int CloseZStream(IntPtr stream);

		// Token: 0x06004495 RID: 17557
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int Flush(DeflateStreamNative.SafeDeflateStreamHandle stream);

		// Token: 0x06004496 RID: 17558
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int ReadZStream(DeflateStreamNative.SafeDeflateStreamHandle stream, IntPtr buffer, int length);

		// Token: 0x06004497 RID: 17559
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern int WriteZStream(DeflateStreamNative.SafeDeflateStreamHandle stream, IntPtr buffer, int length);

		// Token: 0x0400291C RID: 10524
		private const int BufferSize = 4096;

		// Token: 0x0400291D RID: 10525
		private DeflateStreamNative.UnmanagedReadOrWrite feeder;

		// Token: 0x0400291E RID: 10526
		private Stream base_stream;

		// Token: 0x0400291F RID: 10527
		private DeflateStreamNative.SafeDeflateStreamHandle z_stream;

		// Token: 0x04002920 RID: 10528
		private GCHandle data;

		// Token: 0x04002921 RID: 10529
		private bool disposed;

		// Token: 0x04002922 RID: 10530
		private byte[] io_buffer;

		// Token: 0x04002923 RID: 10531
		private Exception last_error;

		// Token: 0x0200085F RID: 2143
		// (Invoke) Token: 0x06004499 RID: 17561
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int UnmanagedReadOrWrite(IntPtr buffer, int length, IntPtr data);

		// Token: 0x02000860 RID: 2144
		private sealed class SafeDeflateStreamHandle : SafeHandle
		{
			// Token: 0x17000F7C RID: 3964
			// (get) Token: 0x0600449C RID: 17564 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}

			// Token: 0x0600449D RID: 17565 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
			private SafeDeflateStreamHandle()
				: base(IntPtr.Zero, true)
			{
			}

			// Token: 0x0600449E RID: 17566 RVA: 0x000ECE12 File Offset: 0x000EB012
			internal SafeDeflateStreamHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x0600449F RID: 17567 RVA: 0x000ECE1C File Offset: 0x000EB01C
			protected override bool ReleaseHandle()
			{
				try
				{
					DeflateStreamNative.CloseZStream(this.handle);
				}
				catch
				{
				}
				return true;
			}
		}
	}
}
