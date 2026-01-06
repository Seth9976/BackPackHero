using System;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a byte range in a Range header value.</summary>
	// Token: 0x02000065 RID: 101
	public class RangeItemHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> class.</summary>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />-or- <paramref name="from" /> or <paramref name="to" /> is less than 0. </exception>
		// Token: 0x0600038B RID: 907 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		public RangeItemHeaderValue(long? from, long? to)
		{
			if (from == null && to == null)
			{
				throw new ArgumentException();
			}
			long? num2;
			if (from != null && to != null)
			{
				long? num = from;
				num2 = to;
				if ((num.GetValueOrDefault() > num2.GetValueOrDefault()) & ((num != null) & (num2 != null)))
				{
					throw new ArgumentOutOfRangeException("from");
				}
			}
			num2 = from;
			long num3 = 0L;
			if ((num2.GetValueOrDefault() < num3) & (num2 != null))
			{
				throw new ArgumentOutOfRangeException("from");
			}
			num2 = to;
			num3 = 0L;
			if ((num2.GetValueOrDefault() < num3) & (num2 != null))
			{
				throw new ArgumentOutOfRangeException("to");
			}
			this.From = from;
			this.To = to;
		}

		/// <summary>Gets the position at which to start sending data.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The position at which to start sending data.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000C670 File Offset: 0x0000A870
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000C678 File Offset: 0x0000A878
		public long? From { get; private set; }

		/// <summary>Gets the position at which to stop sending data. </summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The position at which to stop sending data. </returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000C681 File Offset: 0x0000A881
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000C689 File Offset: 0x0000A889
		public long? To { get; private set; }

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x06000390 RID: 912 RVA: 0x000069EA File Offset: 0x00004BEA
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x06000391 RID: 913 RVA: 0x0000C694 File Offset: 0x0000A894
		public override bool Equals(object obj)
		{
			RangeItemHeaderValue rangeItemHeaderValue = obj as RangeItemHeaderValue;
			if (rangeItemHeaderValue != null)
			{
				long? num = rangeItemHeaderValue.From;
				long? num2 = this.From;
				if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
				{
					num2 = rangeItemHeaderValue.To;
					num = this.To;
					return (num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null));
				}
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x06000392 RID: 914 RVA: 0x0000C710 File Offset: 0x0000A910
		public override int GetHashCode()
		{
			return this.From.GetHashCode() ^ this.To.GetHashCode();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x06000393 RID: 915 RVA: 0x0000C748 File Offset: 0x0000A948
		public override string ToString()
		{
			if (this.From == null)
			{
				return "-" + this.To.Value.ToString();
			}
			if (this.To == null)
			{
				return this.From.Value.ToString() + "-";
			}
			return this.From.Value.ToString() + "-" + this.To.Value.ToString();
		}
	}
}
