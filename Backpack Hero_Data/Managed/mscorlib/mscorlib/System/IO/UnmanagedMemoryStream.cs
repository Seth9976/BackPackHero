using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Provides access to unmanaged blocks of memory from managed code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000B29 RID: 2857
	public class UnmanagedMemoryStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class.</summary>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		// Token: 0x06006695 RID: 26261 RVA: 0x0015E2AA File Offset: 0x0015C4AA
		protected UnmanagedMemoryStream()
		{
			this._mem = null;
			this._isOpen = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class in a safe buffer with a specified offset and length. </summary>
		/// <param name="buffer">The buffer to contain the unmanaged memory stream.</param>
		/// <param name="offset">The byte position in the buffer at which to start the unmanaged memory stream.</param>
		/// <param name="length">The length of the unmanaged memory stream.</param>
		// Token: 0x06006696 RID: 26262 RVA: 0x0015E2C1 File Offset: 0x0015C4C1
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length)
		{
			this.Initialize(buffer, offset, length, FileAccess.Read);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class in a safe buffer with a specified offset, length, and file access. </summary>
		/// <param name="buffer">The buffer to contain the unmanaged memory stream.</param>
		/// <param name="offset">The byte position in the buffer at which to start the unmanaged memory stream.</param>
		/// <param name="length">The length of the unmanaged memory stream.</param>
		/// <param name="access">The mode of file access to the unmanaged memory stream. </param>
		// Token: 0x06006697 RID: 26263 RVA: 0x0015E2D3 File Offset: 0x0015C4D3
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			this.Initialize(buffer, offset, length, access);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class in a safe buffer with a specified offset, length, and file access. </summary>
		/// <param name="buffer">The buffer to contain the unmanaged memory stream.</param>
		/// <param name="offset">The byte position in the buffer at which to start the unmanaged memory stream.</param>
		/// <param name="length">The length of the unmanaged memory stream.</param>
		/// <param name="access">The mode of file access to the unmanaged memory stream.</param>
		// Token: 0x06006698 RID: 26264 RVA: 0x0015E2E8 File Offset: 0x0015C4E8
		protected unsafe void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length", "Non-negative number required.");
			}
			if (buffer.ByteLength < (ulong)(offset + length))
			{
				throw new ArgumentException("Offset and length were greater than the size of the SafeBuffer.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException("The method cannot be called twice on the same instance.");
			}
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.AcquirePointer(ref ptr);
				if (ptr + offset + length < ptr)
				{
					throw new ArgumentException("The UnmanagedMemoryStream capacity would wrap around the high end of the address space.");
				}
			}
			finally
			{
				if (ptr != null)
				{
					buffer.ReleasePointer();
				}
			}
			this._offset = offset;
			this._buffer = buffer;
			this._length = length;
			this._capacity = length;
			this._access = access;
			this._isOpen = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class using the specified location and memory length.</summary>
		/// <param name="pointer">A pointer to an unmanaged memory location.</param>
		/// <param name="length">The length of the memory to use.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pointer" /> value is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="length" /> value is less than zero.- or -The <paramref name="length" /> is large enough to cause an overflow.</exception>
		// Token: 0x06006699 RID: 26265 RVA: 0x0015E3DC File Offset: 0x0015C5DC
		[CLSCompliant(false)]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length)
		{
			this.Initialize(pointer, length, length, FileAccess.Read);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class using the specified location, memory length, total amount of memory, and file access values.</summary>
		/// <param name="pointer">A pointer to an unmanaged memory location.</param>
		/// <param name="length">The length of the memory to use.</param>
		/// <param name="capacity">The total amount of memory assigned to the stream.</param>
		/// <param name="access">One of the <see cref="T:System.IO.FileAccess" /> values.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pointer" /> value is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="length" /> value is less than zero.- or - The <paramref name="capacity" /> value is less than zero.- or -The <paramref name="length" /> value is greater than the <paramref name="capacity" /> value.</exception>
		// Token: 0x0600669A RID: 26266 RVA: 0x0015E3EE File Offset: 0x0015C5EE
		[CLSCompliant(false)]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access)
		{
			this.Initialize(pointer, length, capacity, access);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class by using a pointer to an unmanaged memory location. </summary>
		/// <param name="pointer">A pointer to an unmanaged memory location.</param>
		/// <param name="length">The length of the memory to use.</param>
		/// <param name="capacity">The total amount of memory assigned to the stream.</param>
		/// <param name="access">One of the <see cref="T:System.IO.FileAccess" /> values. </param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pointer" /> value is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="length" /> value is less than zero.- or - The <paramref name="capacity" /> value is less than zero.- or -The <paramref name="length" /> value is large enough to cause an overflow.</exception>
		// Token: 0x0600669B RID: 26267 RVA: 0x0015E404 File Offset: 0x0015C604
		[CLSCompliant(false)]
		protected unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access)
		{
			if (pointer == null)
			{
				throw new ArgumentNullException("pointer");
			}
			if (length < 0L || capacity < 0L)
			{
				throw new ArgumentOutOfRangeException((length < 0L) ? "length" : "capacity", "Non-negative number required.");
			}
			if (length > capacity)
			{
				throw new ArgumentOutOfRangeException("length", "The length cannot be greater than the capacity.");
			}
			if (pointer + capacity < pointer)
			{
				throw new ArgumentOutOfRangeException("capacity", "The UnmanagedMemoryStream capacity would wrap around the high end of the address space.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", "Enum value was out of legal range.");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException("The method cannot be called twice on the same instance.");
			}
			this._mem = pointer;
			this._offset = 0L;
			this._length = length;
			this._capacity = capacity;
			this._access = access;
			this._isOpen = true;
		}

		/// <summary>Gets a value indicating whether a stream supports reading.</summary>
		/// <returns>false if the object was created by a constructor with an <paramref name="access" /> parameter that did not include reading the stream and if the stream is closed; otherwise, true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x0600669C RID: 26268 RVA: 0x0015E4CC File Offset: 0x0015C6CC
		public override bool CanRead
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Read) > (FileAccess)0;
			}
		}

		/// <summary>Gets a value indicating whether a stream supports seeking.</summary>
		/// <returns>false if the stream is closed; otherwise, true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x0600669D RID: 26269 RVA: 0x0015E4E3 File Offset: 0x0015C6E3
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		/// <summary>Gets a value indicating whether a stream supports writing.</summary>
		/// <returns>false if the object was created by a constructor with an <paramref name="access" /> parameter value that supports writing or was created by a constructor that had no parameters, or if the stream is closed; otherwise, true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x0600669E RID: 26270 RVA: 0x0015E4EB File Offset: 0x0015C6EB
		public override bool CanWrite
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Write) > (FileAccess)0;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.UnmanagedMemoryStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600669F RID: 26271 RVA: 0x0015E502 File Offset: 0x0015C702
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			this._mem = null;
			base.Dispose(disposing);
		}

		// Token: 0x060066A0 RID: 26272 RVA: 0x0015E51A File Offset: 0x0015C71A
		private void EnsureNotClosed()
		{
			if (!this._isOpen)
			{
				throw Error.GetStreamIsClosed();
			}
		}

		// Token: 0x060066A1 RID: 26273 RVA: 0x0015E52A File Offset: 0x0015C72A
		private void EnsureReadable()
		{
			if (!this.CanRead)
			{
				throw Error.GetReadNotSupported();
			}
		}

		// Token: 0x060066A2 RID: 26274 RVA: 0x00156D48 File Offset: 0x00154F48
		private void EnsureWriteable()
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
		}

		/// <summary>Overrides the <see cref="M:System.IO.Stream.Flush" /> method so that no action is performed.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060066A3 RID: 26275 RVA: 0x0015E53A File Offset: 0x0015C73A
		public override void Flush()
		{
			this.EnsureNotClosed();
		}

		// Token: 0x060066A4 RID: 26276 RVA: 0x0015E544 File Offset: 0x0015C744
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.Flush();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		/// <summary>Gets the length of the data in a stream.</summary>
		/// <returns>The length of the data in the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x060066A5 RID: 26277 RVA: 0x0015E58C File Offset: 0x0015C78C
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				return Interlocked.Read(ref this._length);
			}
		}

		/// <summary>Gets the stream length (size) or the total amount of memory assigned to a stream (capacity).</summary>
		/// <returns>The size or capacity of the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x060066A6 RID: 26278 RVA: 0x0015E59F File Offset: 0x0015C79F
		public long Capacity
		{
			get
			{
				this.EnsureNotClosed();
				return this._capacity;
			}
		}

		/// <summary>Gets or sets the current position in a stream.</summary>
		/// <returns>The current position in the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The position is set to a value that is less than zero, or the position is larger than <see cref="F:System.Int32.MaxValue" /> or results in overflow when added to the current pointer.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x060066A7 RID: 26279 RVA: 0x0015E5AD File Offset: 0x0015C7AD
		// (set) Token: 0x060066A8 RID: 26280 RVA: 0x0015E5C8 File Offset: 0x0015C7C8
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					throw Error.GetStreamIsClosed();
				}
				return Interlocked.Read(ref this._position);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
				}
				if (!this.CanSeek)
				{
					throw Error.GetStreamIsClosed();
				}
				Interlocked.Exchange(ref this._position, value);
			}
		}

		/// <summary>Gets or sets a byte pointer to a stream based on the current position in the stream.</summary>
		/// <returns>A byte pointer.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The current position is larger than the capacity of the stream.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The position is being set is not a valid position in the current stream.</exception>
		/// <exception cref="T:System.IO.IOException">The pointer is being set to a lower value than the starting position of the stream.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream was initialized for use with a <see cref="T:System.Runtime.InteropServices.SafeBuffer" />. The <see cref="P:System.IO.UnmanagedMemoryStream.PositionPointer" /> property is valid only for streams that are initialized with a <see cref="T:System.Byte" /> pointer. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x060066A9 RID: 26281 RVA: 0x0015E5FC File Offset: 0x0015C7FC
		// (set) Token: 0x060066AA RID: 26282 RVA: 0x0015E64C File Offset: 0x0015C84C
		[CLSCompliant(false)]
		public unsafe byte* PositionPointer
		{
			get
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer.");
				}
				this.EnsureNotClosed();
				long num = Interlocked.Read(ref this._position);
				if (num > this._capacity)
				{
					throw new IndexOutOfRangeException("Unmanaged memory stream position was beyond the capacity of the stream.");
				}
				return this._mem + num;
			}
			set
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer.");
				}
				this.EnsureNotClosed();
				if (value < this._mem)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				long num = (long)(value - this._mem);
				if (num < 0L)
				{
					throw new ArgumentOutOfRangeException("offset", "UnmanagedMemoryStream length must be non-negative and less than 2^63 - 1 - baseAddress.");
				}
				Interlocked.Exchange(ref this._position, num);
			}
		}

		/// <summary>Reads the specified number of bytes into the specified array.</summary>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
		/// <param name="buffer">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source. This parameter is passed uninitialized.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to read from the current stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support reading.- or - The <see cref="P:System.IO.UnmanagedMemoryStream.CanRead" /> property is set to false. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is set to null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero. - or - The <paramref name="count" /> parameter is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The length of the buffer array minus the <paramref name="offset" /> parameter is less than the <paramref name="count" /> parameter.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060066AB RID: 26283 RVA: 0x0015E6B4 File Offset: 0x0015C8B4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return this.ReadCore(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x060066AC RID: 26284 RVA: 0x0015E71D File Offset: 0x0015C91D
		public override int Read(Span<byte> buffer)
		{
			if (base.GetType() == typeof(UnmanagedMemoryStream))
			{
				return this.ReadCore(buffer);
			}
			return base.Read(buffer);
		}

		// Token: 0x060066AD RID: 26285 RVA: 0x0015E748 File Offset: 0x0015C948
		internal unsafe int ReadCore(Span<byte> buffer)
		{
			this.EnsureNotClosed();
			this.EnsureReadable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Math.Min(Interlocked.Read(ref this._length) - num, (long)buffer.Length);
			if (num2 <= 0L)
			{
				return 0;
			}
			int num3 = (int)num2;
			if (num3 < 0)
			{
				return 0;
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(buffer))
			{
				byte* ptr = reference;
				if (this._buffer != null)
				{
					byte* ptr2 = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this._buffer.AcquirePointer(ref ptr2);
						Buffer.Memcpy(ptr, ptr2 + num + this._offset, num3, true);
						goto IL_00A7;
					}
					finally
					{
						if (ptr2 != null)
						{
							this._buffer.ReleasePointer();
						}
					}
				}
				Buffer.Memcpy(ptr, this._mem + num, num3, true);
				IL_00A7:;
			}
			Interlocked.Exchange(ref this._position, num + num2);
			return num3;
		}

		// Token: 0x060066AE RID: 26286 RVA: 0x0015E820 File Offset: 0x0015CA20
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			Task<int> task;
			try
			{
				int num = this.Read(buffer, offset, count);
				Task<int> lastReadTask = this._lastReadTask;
				Task<int> task2;
				if (lastReadTask == null || lastReadTask.Result != num)
				{
					task = (this._lastReadTask = Task.FromResult<int>(num));
					task2 = task;
				}
				else
				{
					task2 = lastReadTask;
				}
				task = task2;
			}
			catch (Exception ex)
			{
				task = Task.FromException<int>(ex);
			}
			return task;
		}

		// Token: 0x060066AF RID: 26287 RVA: 0x0015E8D8 File Offset: 0x0015CAD8
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			ValueTask<int> valueTask;
			try
			{
				ArraySegment<byte> arraySegment;
				valueTask = new ValueTask<int>(MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment) ? this.Read(arraySegment.Array, arraySegment.Offset, arraySegment.Count) : this.Read(buffer.Span));
			}
			catch (Exception ex)
			{
				valueTask = new ValueTask<int>(Task.FromException<int>(ex));
			}
			return valueTask;
		}

		/// <summary>Reads a byte from a stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.</summary>
		/// <returns>The unsigned byte cast to an <see cref="T:System.Int32" /> object, or -1 if at the end of the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support reading.- or -The current position is at the end of the stream.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060066B0 RID: 26288 RVA: 0x0015E95C File Offset: 0x0015CB5C
		public unsafe override int ReadByte()
		{
			this.EnsureNotClosed();
			this.EnsureReadable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			if (num >= num2)
			{
				return -1;
			}
			Interlocked.Exchange(ref this._position, num + 1L);
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					return (int)(ptr + num)[this._offset];
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			return (int)this._mem[num];
		}

		/// <summary>Sets the current position of the current stream to the given value.</summary>
		/// <returns>The new position in the stream.</returns>
		/// <param name="offset">The point relative to <paramref name="origin" /> to begin seeking from. </param>
		/// <param name="loc">Specifies the beginning, the end, or the current position as a reference point for <paramref name="origin" />, using a value of type <see cref="T:System.IO.SeekOrigin" />. </param>
		/// <exception cref="T:System.IO.IOException">An attempt was made to seek before the beginning of the stream.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> value is larger than the maximum size of the stream.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="loc" /> is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060066B1 RID: 26289 RVA: 0x0015EA00 File Offset: 0x0015CC00
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.EnsureNotClosed();
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				Interlocked.Exchange(ref this._position, offset);
				break;
			case SeekOrigin.Current:
			{
				long num = Interlocked.Read(ref this._position);
				if (offset + num < 0L)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				Interlocked.Exchange(ref this._position, offset + num);
				break;
			}
			case SeekOrigin.End:
			{
				long num2 = Interlocked.Read(ref this._length);
				if (num2 + offset < 0L)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				Interlocked.Exchange(ref this._position, num2 + offset);
				break;
			}
			default:
				throw new ArgumentException("Invalid seek origin.");
			}
			return Interlocked.Read(ref this._position);
		}

		/// <summary>Sets the length of a stream to a specified value.</summary>
		/// <param name="value">The length of the stream.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support writing.- or -An attempt is made to write to the stream and the <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> property is false.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="value" /> exceeds the capacity of the stream.- or -The specified <paramref name="value" /> is negative.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060066B2 RID: 26290 RVA: 0x0015EABC File Offset: 0x0015CCBC
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
			}
			if (this._buffer != null)
			{
				throw new NotSupportedException("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer.");
			}
			this.EnsureNotClosed();
			this.EnsureWriteable();
			if (value > this._capacity)
			{
				throw new IOException("Unable to expand length of this stream beyond its capacity.");
			}
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			if (value > num2)
			{
				Buffer.ZeroMemory(this._mem + num2, value - num2);
			}
			Interlocked.Exchange(ref this._length, value);
			if (num > value)
			{
				Interlocked.Exchange(ref this._position, value);
			}
		}

		/// <summary>Writes a block of bytes to the current stream using data from a buffer.</summary>
		/// <param name="buffer">The byte array from which to copy bytes to the current stream.</param>
		/// <param name="offset">The offset in the buffer at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to write to the current stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support writing. - or -An attempt is made to write to the stream and the <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> property is false.- or -The <paramref name="count" /> value is greater than the capacity of the stream.- or -The position is at the end of the stream capacity.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One of the specified parameters is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="offset" /> parameter minus the length of the <paramref name="buffer" /> parameter is less than the <paramref name="count" /> parameter.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060066B3 RID: 26291 RVA: 0x0015EB5C File Offset: 0x0015CD5C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this.WriteCore(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x060066B4 RID: 26292 RVA: 0x0015EBCA File Offset: 0x0015CDCA
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			if (base.GetType() == typeof(UnmanagedMemoryStream))
			{
				this.WriteCore(buffer);
				return;
			}
			base.Write(buffer);
		}

		// Token: 0x060066B5 RID: 26293 RVA: 0x0015EBF4 File Offset: 0x0015CDF4
		internal unsafe void WriteCore(ReadOnlySpan<byte> buffer)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			long num3 = num + (long)buffer.Length;
			if (num3 < 0L)
			{
				throw new IOException("Stream was too long.");
			}
			if (num3 > this._capacity)
			{
				throw new NotSupportedException("Unable to expand length of this stream beyond its capacity.");
			}
			if (this._buffer == null)
			{
				if (num > num2)
				{
					Buffer.ZeroMemory(this._mem + num2, num - num2);
				}
				if (num3 > num2)
				{
					Interlocked.Exchange(ref this._length, num3);
				}
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(buffer))
			{
				byte* ptr = reference;
				if (this._buffer != null)
				{
					if (this._capacity - num < (long)buffer.Length)
					{
						throw new ArgumentException("Not enough space available in the buffer.");
					}
					byte* ptr2 = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this._buffer.AcquirePointer(ref ptr2);
						Buffer.Memcpy(ptr2 + num + this._offset, ptr, buffer.Length, true);
						goto IL_010E;
					}
					finally
					{
						if (ptr2 != null)
						{
							this._buffer.ReleasePointer();
						}
					}
				}
				Buffer.Memcpy(this._mem + num, ptr, buffer.Length, true);
				IL_010E:;
			}
			Interlocked.Exchange(ref this._position, num3);
		}

		// Token: 0x060066B6 RID: 26294 RVA: 0x0015ED30 File Offset: 0x0015CF30
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.Write(buffer, offset, count);
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x060066B7 RID: 26295 RVA: 0x0015EDC8 File Offset: 0x0015CFC8
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask(Task.FromCanceled(cancellationToken));
			}
			ValueTask valueTask;
			try
			{
				ArraySegment<byte> arraySegment;
				if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
				{
					this.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				}
				else
				{
					this.Write(buffer.Span);
				}
				valueTask = default(ValueTask);
				valueTask = valueTask;
			}
			catch (Exception ex)
			{
				valueTask = new ValueTask(Task.FromException(ex));
			}
			return valueTask;
		}

		/// <summary>Writes a byte to the current position in the file stream.</summary>
		/// <param name="value">A byte value written to the stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support writing.- or -An attempt is made to write to the stream and the <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> property is false.- or - The current position is at the end of the capacity of the stream.</exception>
		/// <exception cref="T:System.IO.IOException">The supplied <paramref name="value" /> causes the stream exceed its maximum capacity.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060066B8 RID: 26296 RVA: 0x0015EE4C File Offset: 0x0015D04C
		public unsafe override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			long num3 = num + 1L;
			if (num >= num2)
			{
				if (num3 < 0L)
				{
					throw new IOException("Stream was too long.");
				}
				if (num3 > this._capacity)
				{
					throw new NotSupportedException("Unable to expand length of this stream beyond its capacity.");
				}
				if (this._buffer == null)
				{
					if (num > num2)
					{
						Buffer.ZeroMemory(this._mem + num2, num - num2);
					}
					Interlocked.Exchange(ref this._length, num3);
				}
			}
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					(ptr + num)[this._offset] = value;
					goto IL_00C4;
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			this._mem[num] = value;
			IL_00C4:
			Interlocked.Exchange(ref this._position, num3);
		}

		// Token: 0x04003C14 RID: 15380
		private SafeBuffer _buffer;

		// Token: 0x04003C15 RID: 15381
		private unsafe byte* _mem;

		// Token: 0x04003C16 RID: 15382
		private long _length;

		// Token: 0x04003C17 RID: 15383
		private long _capacity;

		// Token: 0x04003C18 RID: 15384
		private long _position;

		// Token: 0x04003C19 RID: 15385
		private long _offset;

		// Token: 0x04003C1A RID: 15386
		private FileAccess _access;

		// Token: 0x04003C1B RID: 15387
		internal bool _isOpen;

		// Token: 0x04003C1C RID: 15388
		private Task<int> _lastReadTask;
	}
}
