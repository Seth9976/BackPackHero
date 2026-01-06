using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the value to pass to a property to cause the property to get its value from another source. This is known as ambience. This class cannot be inherited.</summary>
	// Token: 0x0200068F RID: 1679
	[AttributeUsage(AttributeTargets.All)]
	public sealed class AmbientValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given the value and its type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the <paramref name="value" /> parameter. </param>
		/// <param name="value">The value for this attribute. </param>
		// Token: 0x060035C8 RID: 13768 RVA: 0x000BF3AC File Offset: 0x000BD5AC
		public AmbientValueAttribute(Type type, string value)
		{
			try
			{
				this.Value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Unicode character for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060035C9 RID: 13769 RVA: 0x000BF3E8 File Offset: 0x000BD5E8
		public AmbientValueAttribute(char value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an 8-bit unsigned integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060035CA RID: 13770 RVA: 0x000BF3FC File Offset: 0x000BD5FC
		public AmbientValueAttribute(byte value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 16-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060035CB RID: 13771 RVA: 0x000BF410 File Offset: 0x000BD610
		public AmbientValueAttribute(short value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 32-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060035CC RID: 13772 RVA: 0x000BF424 File Offset: 0x000BD624
		public AmbientValueAttribute(int value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 64-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060035CD RID: 13773 RVA: 0x000BF438 File Offset: 0x000BD638
		public AmbientValueAttribute(long value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a single-precision floating point number for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060035CE RID: 13774 RVA: 0x000BF44C File Offset: 0x000BD64C
		public AmbientValueAttribute(float value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a double-precision floating-point number for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060035CF RID: 13775 RVA: 0x000BF460 File Offset: 0x000BD660
		public AmbientValueAttribute(double value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Boolean value for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060035D0 RID: 13776 RVA: 0x000BF474 File Offset: 0x000BD674
		public AmbientValueAttribute(bool value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a string for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060035D1 RID: 13777 RVA: 0x000BF488 File Offset: 0x000BD688
		public AmbientValueAttribute(string value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an object for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060035D2 RID: 13778 RVA: 0x000BF488 File Offset: 0x000BD688
		public AmbientValueAttribute(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets the object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>The object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x000BF497 File Offset: 0x000BD697
		public object Value { get; }

		/// <summary>Determines whether the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.AmbientValueAttribute" /> to compare with the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</param>
		// Token: 0x060035D4 RID: 13780 RVA: 0x000BF4A0 File Offset: 0x000BD6A0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			AmbientValueAttribute ambientValueAttribute = obj as AmbientValueAttribute;
			if (ambientValueAttribute == null)
			{
				return false;
			}
			if (this.Value == null)
			{
				return ambientValueAttribute.Value == null;
			}
			return this.Value.Equals(ambientValueAttribute.Value);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x060035D5 RID: 13781 RVA: 0x000BE516 File Offset: 0x000BC716
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
