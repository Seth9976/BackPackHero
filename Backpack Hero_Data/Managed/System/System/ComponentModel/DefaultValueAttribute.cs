using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Specifies the default value for a property.</summary>
	// Token: 0x02000673 RID: 1651
	[AttributeUsage(AttributeTargets.All)]
	public class DefaultValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class, converting the specified value to the specified type, and using an invariant culture as the translation context.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to convert the value to. </param>
		/// <param name="value">A <see cref="T:System.String" /> that can be converted to the type using the <see cref="T:System.ComponentModel.TypeConverter" /> for the type and the U.S. English culture. </param>
		// Token: 0x06003515 RID: 13589 RVA: 0x000BE334 File Offset: 0x000BC534
		public DefaultValueAttribute(Type type, string value)
		{
			try
			{
				object obj;
				if (DefaultValueAttribute.<.ctor>g__TryConvertFromInvariantString|2_0(type, value, out obj))
				{
					this._value = obj;
				}
				else if (type.IsSubclassOf(typeof(Enum)))
				{
					this._value = Enum.Parse(type, value, true);
				}
				else if (type == typeof(TimeSpan))
				{
					this._value = TimeSpan.Parse(value);
				}
				else
				{
					this._value = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
				}
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a Unicode character.</summary>
		/// <param name="value">A Unicode character that is the default value. </param>
		// Token: 0x06003516 RID: 13590 RVA: 0x000BE3CC File Offset: 0x000BC5CC
		public DefaultValueAttribute(char value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using an 8-bit unsigned integer.</summary>
		/// <param name="value">An 8-bit unsigned integer that is the default value. </param>
		// Token: 0x06003517 RID: 13591 RVA: 0x000BE3E0 File Offset: 0x000BC5E0
		public DefaultValueAttribute(byte value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 16-bit signed integer.</summary>
		/// <param name="value">A 16-bit signed integer that is the default value. </param>
		// Token: 0x06003518 RID: 13592 RVA: 0x000BE3F4 File Offset: 0x000BC5F4
		public DefaultValueAttribute(short value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 32-bit signed integer.</summary>
		/// <param name="value">A 32-bit signed integer that is the default value. </param>
		// Token: 0x06003519 RID: 13593 RVA: 0x000BE408 File Offset: 0x000BC608
		public DefaultValueAttribute(int value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 64-bit signed integer.</summary>
		/// <param name="value">A 64-bit signed integer that is the default value. </param>
		// Token: 0x0600351A RID: 13594 RVA: 0x000BE41C File Offset: 0x000BC61C
		public DefaultValueAttribute(long value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a single-precision floating point number.</summary>
		/// <param name="value">A single-precision floating point number that is the default value. </param>
		// Token: 0x0600351B RID: 13595 RVA: 0x000BE430 File Offset: 0x000BC630
		public DefaultValueAttribute(float value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a double-precision floating point number.</summary>
		/// <param name="value">A double-precision floating point number that is the default value. </param>
		// Token: 0x0600351C RID: 13596 RVA: 0x000BE444 File Offset: 0x000BC644
		public DefaultValueAttribute(double value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Boolean" /> that is the default value. </param>
		// Token: 0x0600351D RID: 13597 RVA: 0x000BE458 File Offset: 0x000BC658
		public DefaultValueAttribute(bool value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.String" />.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that is the default value. </param>
		// Token: 0x0600351E RID: 13598 RVA: 0x000BE46C File Offset: 0x000BC66C
		public DefaultValueAttribute(string value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that represents the default value. </param>
		// Token: 0x0600351F RID: 13599 RVA: 0x000BE46C File Offset: 0x000BC66C
		public DefaultValueAttribute(object value)
		{
			this._value = value;
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000BE47B File Offset: 0x000BC67B
		[CLSCompliant(false)]
		public DefaultValueAttribute(sbyte value)
		{
			this._value = value;
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000BE48F File Offset: 0x000BC68F
		[CLSCompliant(false)]
		public DefaultValueAttribute(ushort value)
		{
			this._value = value;
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000BE4A3 File Offset: 0x000BC6A3
		[CLSCompliant(false)]
		public DefaultValueAttribute(uint value)
		{
			this._value = value;
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000BE4B7 File Offset: 0x000BC6B7
		[CLSCompliant(false)]
		public DefaultValueAttribute(ulong value)
		{
			this._value = value;
		}

		/// <summary>Gets the default value of the property this attribute is bound to.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the default value of the property this attribute is bound to.</returns>
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x000BE4CB File Offset: 0x000BC6CB
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultValueAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06003525 RID: 13605 RVA: 0x000BE4D4 File Offset: 0x000BC6D4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.Value != null)
			{
				return this.Value.Equals(defaultValueAttribute.Value);
			}
			return defaultValueAttribute.Value == null;
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000BE516 File Offset: 0x000BC716
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Sets the default value for the property to which this attribute is bound.</summary>
		/// <param name="value">The default value.</param>
		// Token: 0x06003527 RID: 13607 RVA: 0x000BE51E File Offset: 0x000BC71E
		protected void SetValue(object value)
		{
			this._value = value;
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000BE528 File Offset: 0x000BC728
		[CompilerGenerated]
		internal static bool <.ctor>g__TryConvertFromInvariantString|2_0(Type typeToConvert, string stringValue, out object conversionResult)
		{
			conversionResult = null;
			if (DefaultValueAttribute.s_convertFromInvariantString == null)
			{
				Type type = Type.GetType("System.ComponentModel.TypeDescriptor, System.ComponentModel.TypeConverter", false);
				Volatile.Write<object>(ref DefaultValueAttribute.s_convertFromInvariantString, (type == null) ? new object() : Delegate.CreateDelegate(typeof(Func<Type, string, object>), type, "ConvertFromInvariantString", false));
			}
			Func<Type, string, object> func = DefaultValueAttribute.s_convertFromInvariantString as Func<Type, string, object>;
			if (func == null)
			{
				return false;
			}
			conversionResult = func(typeToConvert, stringValue);
			return true;
		}

		// Token: 0x04001FF5 RID: 8181
		private object _value;

		// Token: 0x04001FF6 RID: 8182
		private static object s_convertFromInvariantString;
	}
}
