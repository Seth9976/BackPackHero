using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The FieldToken struct is an object representation of a token that represents a field.</summary>
	// Token: 0x02000926 RID: 2342
	[ComVisible(true)]
	[Serializable]
	public readonly struct FieldToken : IEquatable<FieldToken>
	{
		// Token: 0x0600503D RID: 20541 RVA: 0x000FAE8A File Offset: 0x000F908A
		internal FieldToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Determines if an object is an instance of FieldToken and is equal to this instance.</summary>
		/// <returns>Returns true if <paramref name="obj" /> is an instance of FieldToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this FieldToken. </param>
		// Token: 0x0600503E RID: 20542 RVA: 0x000FAE94 File Offset: 0x000F9094
		public override bool Equals(object obj)
		{
			bool flag = obj is FieldToken;
			if (flag)
			{
				FieldToken fieldToken = (FieldToken)obj;
				flag = this.tokValue == fieldToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.FieldToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to the current instance.</param>
		// Token: 0x0600503F RID: 20543 RVA: 0x000FAEC5 File Offset: 0x000F90C5
		public bool Equals(FieldToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.FieldToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06005040 RID: 20544 RVA: 0x000FAED5 File Offset: 0x000F90D5
		public static bool operator ==(FieldToken a, FieldToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.FieldToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06005041 RID: 20545 RVA: 0x000FAEE8 File Offset: 0x000F90E8
		public static bool operator !=(FieldToken a, FieldToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this field.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x06005042 RID: 20546 RVA: 0x000FAEFE File Offset: 0x000F90FE
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this field.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this field.</returns>
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06005043 RID: 20547 RVA: 0x000FAEFE File Offset: 0x000F90FE
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x0400316B RID: 12651
		internal readonly int tokValue;

		/// <summary>The default FieldToken with <see cref="P:System.Reflection.Emit.FieldToken.Token" /> value 0.</summary>
		// Token: 0x0400316C RID: 12652
		public static readonly FieldToken Empty;
	}
}
