using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Encapsulates an array and an offset within the specified array.</summary>
	// Token: 0x020006E1 RID: 1761
	[ComVisible(true)]
	[Serializable]
	public struct ArrayWithOffset
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> structure.</summary>
		/// <param name="array">A managed array. </param>
		/// <param name="offset">The offset in bytes, of the element to be passed through platform invoke. </param>
		/// <exception cref="T:System.ArgumentException">The array is larger than 2 gigabytes (GB).</exception>
		// Token: 0x06004052 RID: 16466 RVA: 0x000E0E1A File Offset: 0x000DF01A
		[SecuritySafeCritical]
		public ArrayWithOffset(object array, int offset)
		{
			this.m_array = array;
			this.m_offset = offset;
			this.m_count = 0;
			this.m_count = this.CalculateCount();
		}

		/// <summary>Returns the managed array referenced by this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.</summary>
		/// <returns>The managed array this instance references.</returns>
		// Token: 0x06004053 RID: 16467 RVA: 0x000E0E3D File Offset: 0x000DF03D
		public object GetArray()
		{
			return this.m_array;
		}

		/// <summary>Returns the offset provided when this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> was constructed.</summary>
		/// <returns>The offset for this instance.</returns>
		// Token: 0x06004054 RID: 16468 RVA: 0x000E0E45 File Offset: 0x000DF045
		public int GetOffset()
		{
			return this.m_offset;
		}

		/// <summary>Returns a hash code for this value type.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x06004055 RID: 16469 RVA: 0x000E0E4D File Offset: 0x000DF04D
		public override int GetHashCode()
		{
			return this.m_count + this.m_offset;
		}

		/// <summary>Indicates whether the specified object matches the current <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object.</summary>
		/// <returns>true if the object matches this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />; otherwise, false.</returns>
		/// <param name="obj">Object to compare with this instance. </param>
		// Token: 0x06004056 RID: 16470 RVA: 0x000E0E5C File Offset: 0x000DF05C
		public override bool Equals(object obj)
		{
			return obj is ArrayWithOffset && this.Equals((ArrayWithOffset)obj);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object matches the current instance.</summary>
		/// <returns>true if the specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object matches the current instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with this instance.</param>
		// Token: 0x06004057 RID: 16471 RVA: 0x000E0E74 File Offset: 0x000DF074
		public bool Equals(ArrayWithOffset obj)
		{
			return obj.m_array == this.m_array && obj.m_offset == this.m_offset && obj.m_count == this.m_count;
		}

		/// <summary>Determines whether two specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> objects have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is the same as the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="b" /> parameter. </param>
		/// <param name="b">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="a" /> parameter.</param>
		// Token: 0x06004058 RID: 16472 RVA: 0x000E0EA2 File Offset: 0x000DF0A2
		public static bool operator ==(ArrayWithOffset a, ArrayWithOffset b)
		{
			return a.Equals(b);
		}

		/// <summary>Determines whether two specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> objects no not have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is not the same as the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="b" /> parameter. </param>
		/// <param name="b">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="a" /> parameter.</param>
		// Token: 0x06004059 RID: 16473 RVA: 0x000E0EAC File Offset: 0x000DF0AC
		public static bool operator !=(ArrayWithOffset a, ArrayWithOffset b)
		{
			return !(a == b);
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x000E0EB8 File Offset: 0x000DF0B8
		private int CalculateCount()
		{
			Array array = this.m_array as Array;
			if (array == null)
			{
				throw new ArgumentException();
			}
			return array.Rank * array.Length - this.m_offset;
		}

		// Token: 0x04002A23 RID: 10787
		private object m_array;

		// Token: 0x04002A24 RID: 10788
		private int m_offset;

		// Token: 0x04002A25 RID: 10789
		private int m_count;
	}
}
