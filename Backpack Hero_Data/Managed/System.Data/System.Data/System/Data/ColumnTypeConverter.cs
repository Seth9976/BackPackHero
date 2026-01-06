using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.SqlTypes;
using System.Globalization;
using System.Reflection;

namespace System.Data
{
	// Token: 0x0200003A RID: 58
	internal sealed class ColumnTypeConverter : TypeConverter
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000CB09 File Offset: 0x0000AD09
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000CB28 File Offset: 0x0000AD28
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				if (value != null && destinationType == typeof(InstanceDescriptor))
				{
					object obj = value;
					if (value is string)
					{
						for (int i = 0; i < ColumnTypeConverter.s_types.Length; i++)
						{
							if (ColumnTypeConverter.s_types[i].ToString().Equals(value))
							{
								obj = ColumnTypeConverter.s_types[i];
							}
						}
					}
					if (value is Type || value is string)
					{
						MethodInfo method = typeof(Type).GetMethod("GetType", new Type[] { typeof(string) });
						if (method != null)
						{
							return new InstanceDescriptor(method, new object[] { ((Type)obj).AssemblyQualifiedName });
						}
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			return value.ToString();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000CC28 File Offset: 0x0000AE28
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000CC48 File Offset: 0x0000AE48
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value != null && value.GetType() == typeof(string))
			{
				for (int i = 0; i < ColumnTypeConverter.s_types.Length; i++)
				{
					if (ColumnTypeConverter.s_types[i].ToString().Equals(value))
					{
						return ColumnTypeConverter.s_types[i];
					}
				}
				return typeof(string);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this._values == null)
			{
				object[] array;
				if (ColumnTypeConverter.s_types != null)
				{
					array = new object[ColumnTypeConverter.s_types.Length];
					Array.Copy(ColumnTypeConverter.s_types, 0, array, 0, ColumnTypeConverter.s_types.Length);
				}
				else
				{
					array = null;
				}
				this._values = new TypeConverter.StandardValuesCollection(array);
			}
			return this._values;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04000479 RID: 1145
		private static readonly Type[] s_types = new Type[]
		{
			typeof(bool),
			typeof(byte),
			typeof(byte[]),
			typeof(char),
			typeof(DateTime),
			typeof(decimal),
			typeof(double),
			typeof(Guid),
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(object),
			typeof(sbyte),
			typeof(float),
			typeof(string),
			typeof(TimeSpan),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(SqlInt16),
			typeof(SqlInt32),
			typeof(SqlInt64),
			typeof(SqlDecimal),
			typeof(SqlSingle),
			typeof(SqlDouble),
			typeof(SqlString),
			typeof(SqlBoolean),
			typeof(SqlBinary),
			typeof(SqlByte),
			typeof(SqlDateTime),
			typeof(SqlGuid),
			typeof(SqlMoney),
			typeof(SqlBytes),
			typeof(SqlChars),
			typeof(SqlXml)
		};

		// Token: 0x0400047A RID: 1146
		private TypeConverter.StandardValuesCollection _values;
	}
}
