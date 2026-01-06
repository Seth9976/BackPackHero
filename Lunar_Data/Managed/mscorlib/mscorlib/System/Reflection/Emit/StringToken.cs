using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents a token that represents a string.</summary>
	// Token: 0x02000946 RID: 2374
	[ComVisible(true)]
	[Serializable]
	public readonly struct StringToken : IEquatable<StringToken>
	{
		// Token: 0x0600527D RID: 21117 RVA: 0x00102E1E File Offset: 0x0010101E
		internal StringToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of StringToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of StringToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this StringToken. </param>
		// Token: 0x0600527E RID: 21118 RVA: 0x00102E28 File Offset: 0x00101028
		public override bool Equals(object obj)
		{
			bool flag = obj is StringToken;
			if (flag)
			{
				StringToken stringToken = (StringToken)obj;
				flag = this.tokValue == stringToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.StringToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to the current instance.</param>
		// Token: 0x0600527F RID: 21119 RVA: 0x00102E59 File Offset: 0x00101059
		public bool Equals(StringToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.StringToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06005280 RID: 21120 RVA: 0x00102E69 File Offset: 0x00101069
		public static bool operator ==(StringToken a, StringToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.StringToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06005281 RID: 21121 RVA: 0x00102E7C File Offset: 0x0010107C
		public static bool operator !=(StringToken a, StringToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Returns the hash code for this string.</summary>
		/// <returns>Returns the underlying string token.</returns>
		// Token: 0x06005282 RID: 21122 RVA: 0x00102E92 File Offset: 0x00101092
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this string.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this string.</returns>
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06005283 RID: 21123 RVA: 0x00102E92 File Offset: 0x00101092
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x0400331A RID: 13082
		internal readonly int tokValue;
	}
}
