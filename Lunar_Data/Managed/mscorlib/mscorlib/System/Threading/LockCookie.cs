using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Defines the lock that implements single-writer/multiple-reader semantics. This is a value type.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002F1 RID: 753
	[ComVisible(true)]
	public struct LockCookie
	{
		// Token: 0x060020CE RID: 8398 RVA: 0x00076A7A File Offset: 0x00074C7A
		internal LockCookie(int thread_id)
		{
			this.ThreadId = thread_id;
			this.ReaderLocks = 0;
			this.WriterLocks = 0;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x00076A91 File Offset: 0x00074C91
		internal LockCookie(int thread_id, int reader_locks, int writer_locks)
		{
			this.ThreadId = thread_id;
			this.ReaderLocks = reader_locks;
			this.WriterLocks = writer_locks;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060020D0 RID: 8400 RVA: 0x00076AA8 File Offset: 0x00074CA8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Threading.LockCookie" />.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Threading.LockCookie" /> to compare to the current instance.</param>
		// Token: 0x060020D1 RID: 8401 RVA: 0x00076ABA File Offset: 0x00074CBA
		public bool Equals(LockCookie obj)
		{
			return this.ThreadId == obj.ThreadId && this.ReaderLocks == obj.ReaderLocks && this.WriterLocks == obj.WriterLocks;
		}

		/// <summary>Indicates whether a specified object is a <see cref="T:System.Threading.LockCookie" /> and is equal to the current instance.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current instance.</param>
		// Token: 0x060020D2 RID: 8402 RVA: 0x00076AE9 File Offset: 0x00074CE9
		public override bool Equals(object obj)
		{
			return obj is LockCookie && obj.Equals(this);
		}

		/// <summary>Indicates whether two <see cref="T:System.Threading.LockCookie" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060020D3 RID: 8403 RVA: 0x00076B06 File Offset: 0x00074D06
		public static bool operator ==(LockCookie a, LockCookie b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Threading.LockCookie" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060020D4 RID: 8404 RVA: 0x00076B10 File Offset: 0x00074D10
		public static bool operator !=(LockCookie a, LockCookie b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04001B6D RID: 7021
		internal int ThreadId;

		// Token: 0x04001B6E RID: 7022
		internal int ReaderLocks;

		// Token: 0x04001B6F RID: 7023
		internal int WriterLocks;
	}
}
