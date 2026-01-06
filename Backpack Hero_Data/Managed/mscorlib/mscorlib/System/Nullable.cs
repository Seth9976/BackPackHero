using System;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Represents a value type that can be assigned null.</summary>
	/// <typeparam name="T">The underlying value type of the <see cref="T:System.Nullable`1" /> generic type.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000163 RID: 355
	[NonVersionable]
	[Serializable]
	public struct Nullable<T> where T : struct
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Nullable`1" /> structure to the specified value. </summary>
		/// <param name="value">A value type.</param>
		// Token: 0x06000DE8 RID: 3560 RVA: 0x00036019 File Offset: 0x00034219
		[NonVersionable]
		public Nullable(T value)
		{
			this.value = value;
			this.hasValue = true;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Nullable`1" /> object has a value.</summary>
		/// <returns>true if the current <see cref="T:System.Nullable`1" /> object has a value; false if the current <see cref="T:System.Nullable`1" /> object has no value.</returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00036029 File Offset: 0x00034229
		public bool HasValue
		{
			[NonVersionable]
			get
			{
				return this.hasValue;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Nullable`1" /> value.</summary>
		/// <returns>The value of the current <see cref="T:System.Nullable`1" /> object if the <see cref="P:System.Nullable`1.HasValue" /> property is true. An exception is thrown if the <see cref="P:System.Nullable`1.HasValue" /> property is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Nullable`1.HasValue" /> property is false.</exception>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00036031 File Offset: 0x00034231
		public T Value
		{
			get
			{
				if (!this.hasValue)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_NoValue();
				}
				return this.value;
			}
		}

		/// <summary>Retrieves the value of the current <see cref="T:System.Nullable`1" /> object, or the object's default value.</summary>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property if the  <see cref="P:System.Nullable`1.HasValue" /> property is true; otherwise, the default value of the current <see cref="T:System.Nullable`1" /> object. The type of the default value is the type argument of the current <see cref="T:System.Nullable`1" /> object, and the value of the default value consists solely of binary zeroes.</returns>
		// Token: 0x06000DEB RID: 3563 RVA: 0x00036046 File Offset: 0x00034246
		[NonVersionable]
		public T GetValueOrDefault()
		{
			return this.value;
		}

		/// <summary>Retrieves the value of the current <see cref="T:System.Nullable`1" /> object, or the specified default value.</summary>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property if the <see cref="P:System.Nullable`1.HasValue" /> property is true; otherwise, the <paramref name="defaultValue" /> parameter.</returns>
		/// <param name="defaultValue">A value to return if the <see cref="P:System.Nullable`1.HasValue" /> property is false.</param>
		// Token: 0x06000DEC RID: 3564 RVA: 0x0003604E File Offset: 0x0003424E
		[NonVersionable]
		public T GetValueOrDefault(T defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		/// <summary>Indicates whether the current <see cref="T:System.Nullable`1" /> object is equal to a specified object.</summary>
		/// <returns>true if the <paramref name="other" /> parameter is equal to the current <see cref="T:System.Nullable`1" /> object; otherwise, false. This table describes how equality is defined for the compared values: Return ValueDescriptiontrueThe <see cref="P:System.Nullable`1.HasValue" /> property is false, and the <paramref name="other" /> parameter is null. That is, two null values are equal by definition.-or-The <see cref="P:System.Nullable`1.HasValue" /> property is true, and the value returned by the <see cref="P:System.Nullable`1.Value" /> property is equal to the <paramref name="other" /> parameter.falseThe <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is true, and the <paramref name="other" /> parameter is null.-or-The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is false, and the <paramref name="other" /> parameter is not null.-or-The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is true, and the value returned by the <see cref="P:System.Nullable`1.Value" /> property is not equal to the <paramref name="other" /> parameter.</returns>
		/// <param name="other">An object.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DED RID: 3565 RVA: 0x00036060 File Offset: 0x00034260
		public override bool Equals(object other)
		{
			if (!this.hasValue)
			{
				return other == null;
			}
			return other != null && this.value.Equals(other);
		}

		/// <summary>Retrieves the hash code of the object returned by the <see cref="P:System.Nullable`1.Value" /> property.</summary>
		/// <returns>The hash code of the object returned by the <see cref="P:System.Nullable`1.Value" /> property if the <see cref="P:System.Nullable`1.HasValue" /> property is true, or zero if the <see cref="P:System.Nullable`1.HasValue" /> property is false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DEE RID: 3566 RVA: 0x00036086 File Offset: 0x00034286
		public override int GetHashCode()
		{
			if (!this.hasValue)
			{
				return 0;
			}
			return this.value.GetHashCode();
		}

		/// <summary>Returns the text representation of the value of the current <see cref="T:System.Nullable`1" /> object.</summary>
		/// <returns>The text representation of the value of the current <see cref="T:System.Nullable`1" /> object if the <see cref="P:System.Nullable`1.HasValue" /> property is true, or an empty string ("") if the <see cref="P:System.Nullable`1.HasValue" /> property is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DEF RID: 3567 RVA: 0x000360A3 File Offset: 0x000342A3
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString();
		}

		/// <summary>Creates a new <see cref="T:System.Nullable`1" /> object initialized to a specified value. </summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> object whose <see cref="P:System.Nullable`1.Value" /> property is initialized with the <paramref name="value" /> parameter.</returns>
		/// <param name="value">A value type.</param>
		// Token: 0x06000DF0 RID: 3568 RVA: 0x000360C4 File Offset: 0x000342C4
		[NonVersionable]
		public static implicit operator T?(T value)
		{
			return new T?(value);
		}

		/// <summary>Returns the value of a specified <see cref="T:System.Nullable`1" /> value.</summary>
		/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property for the <paramref name="value" /> parameter.</returns>
		/// <param name="value">A <see cref="T:System.Nullable`1" /> value.</param>
		// Token: 0x06000DF1 RID: 3569 RVA: 0x000360CC File Offset: 0x000342CC
		[NonVersionable]
		public static explicit operator T(T? value)
		{
			return value.Value;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x000360D5 File Offset: 0x000342D5
		private static object Box(T? o)
		{
			if (!o.hasValue)
			{
				return null;
			}
			return o.value;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000360EC File Offset: 0x000342EC
		private static T? Unbox(object o)
		{
			if (o == null)
			{
				return null;
			}
			return new T?((T)((object)o));
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00036114 File Offset: 0x00034314
		private static T? UnboxExact(object o)
		{
			if (o == null)
			{
				return null;
			}
			if (o.GetType() != typeof(T))
			{
				throw new InvalidCastException();
			}
			return new T?((T)((object)o));
		}

		// Token: 0x04001285 RID: 4741
		private readonly bool hasValue;

		// Token: 0x04001286 RID: 4742
		internal T value;
	}
}
