using System;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	// Token: 0x0200032D RID: 813
	internal sealed class DataRecordInternal : DbDataRecord, ICustomTypeDescriptor
	{
		// Token: 0x06002651 RID: 9809 RVA: 0x000AC961 File Offset: 0x000AAB61
		internal DataRecordInternal(SchemaInfo[] schemaInfo, object[] values, PropertyDescriptorCollection descriptors, FieldNameLookup fieldNameLookup)
		{
			this._schemaInfo = schemaInfo;
			this._values = values;
			this._propertyDescriptors = descriptors;
			this._fieldNameLookup = fieldNameLookup;
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x000AC986 File Offset: 0x000AAB86
		public override int FieldCount
		{
			get
			{
				return this._schemaInfo.Length;
			}
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000AC990 File Offset: 0x000AAB90
		public override int GetValues(object[] values)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length < this._schemaInfo.Length) ? values.Length : this._schemaInfo.Length);
			for (int i = 0; i < num; i++)
			{
				values[i] = this._values[i];
			}
			return num;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000AC9DE File Offset: 0x000AABDE
		public override string GetName(int i)
		{
			return this._schemaInfo[i].name;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000AC9F1 File Offset: 0x000AABF1
		public override object GetValue(int i)
		{
			return this._values[i];
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000AC9FB File Offset: 0x000AABFB
		public override string GetDataTypeName(int i)
		{
			return this._schemaInfo[i].typeName;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000ACA0E File Offset: 0x000AAC0E
		public override Type GetFieldType(int i)
		{
			return this._schemaInfo[i].type;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000ACA21 File Offset: 0x000AAC21
		public override int GetOrdinal(string name)
		{
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x17000684 RID: 1668
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x17000685 RID: 1669
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000ACA47 File Offset: 0x000AAC47
		public override bool GetBoolean(int i)
		{
			return (bool)this._values[i];
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000ACA56 File Offset: 0x000AAC56
		public override byte GetByte(int i)
		{
			return (byte)this._values[i];
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000ACA68 File Offset: 0x000AAC68
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			int num = 0;
			byte[] array = (byte[])this._values[i];
			num = array.Length;
			if (dataIndex > 2147483647L)
			{
				throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
			}
			int num2 = (int)dataIndex;
			if (buffer == null)
			{
				return (long)num;
			}
			try
			{
				if (num2 < num)
				{
					if (num2 + length > num)
					{
						num -= num2;
					}
					else
					{
						num = length;
					}
				}
				Array.Copy(array, num2, buffer, bufferIndex, num);
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				num = array.Length;
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				if (bufferIndex < 0 || bufferIndex >= buffer.Length)
				{
					throw ADP.InvalidDestinationBufferIndex(length, bufferIndex, "bufferIndex");
				}
				if (dataIndex < 0L || dataIndex >= (long)num)
				{
					throw ADP.InvalidSourceBufferIndex(length, dataIndex, "dataIndex");
				}
				if (num + bufferIndex > buffer.Length)
				{
					throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
				}
			}
			return (long)num;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000ACB4C File Offset: 0x000AAD4C
		public override char GetChar(int i)
		{
			return ((string)this._values[i])[0];
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000ACB64 File Offset: 0x000AAD64
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			char[] array = ((string)this._values[i]).ToCharArray();
			int num = array.Length;
			if (dataIndex > 2147483647L)
			{
				throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
			}
			int num2 = (int)dataIndex;
			if (buffer == null)
			{
				return (long)num;
			}
			try
			{
				if (num2 < num)
				{
					if (num2 + length > num)
					{
						num -= num2;
					}
					else
					{
						num = length;
					}
				}
				Array.Copy(array, num2, buffer, bufferIndex, num);
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				num = array.Length;
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				if (bufferIndex < 0 || bufferIndex >= buffer.Length)
				{
					throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
				}
				if (num2 < 0 || num2 >= num)
				{
					throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
				}
				if (num + bufferIndex > buffer.Length)
				{
					throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
				}
			}
			return (long)num;
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000ACC4C File Offset: 0x000AAE4C
		public override Guid GetGuid(int i)
		{
			return (Guid)this._values[i];
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000ACC5B File Offset: 0x000AAE5B
		public override short GetInt16(int i)
		{
			return (short)this._values[i];
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000ACC6A File Offset: 0x000AAE6A
		public override int GetInt32(int i)
		{
			return (int)this._values[i];
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000ACC79 File Offset: 0x000AAE79
		public override long GetInt64(int i)
		{
			return (long)this._values[i];
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000ACC88 File Offset: 0x000AAE88
		public override float GetFloat(int i)
		{
			return (float)this._values[i];
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000ACC97 File Offset: 0x000AAE97
		public override double GetDouble(int i)
		{
			return (double)this._values[i];
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000ACCA6 File Offset: 0x000AAEA6
		public override string GetString(int i)
		{
			return (string)this._values[i];
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000ACCB5 File Offset: 0x000AAEB5
		public override decimal GetDecimal(int i)
		{
			return (decimal)this._values[i];
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000ACCC4 File Offset: 0x000AAEC4
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this._values[i];
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000ACCD4 File Offset: 0x000AAED4
		public override bool IsDBNull(int i)
		{
			object obj = this._values[i];
			return obj == null || Convert.IsDBNull(obj);
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x00017DFA File Offset: 0x00015FFA
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x00003DF6 File Offset: 0x00001FF6
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x00003DF6 File Offset: 0x00001FF6
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x00003DF6 File Offset: 0x00001FF6
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x00003DF6 File Offset: 0x00001FF6
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x00003DF6 File Offset: 0x00001FF6
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x00003DF6 File Offset: 0x00001FF6
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x00017E02 File Offset: 0x00016002
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x00017E02 File Offset: 0x00016002
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x00017E0A File Offset: 0x0001600A
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000ACCF5 File Offset: 0x000AAEF5
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._propertyDescriptors == null)
			{
				this._propertyDescriptors = new PropertyDescriptorCollection(null);
			}
			return this._propertyDescriptors;
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0000565A File Offset: 0x0000385A
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x040018B4 RID: 6324
		private SchemaInfo[] _schemaInfo;

		// Token: 0x040018B5 RID: 6325
		private object[] _values;

		// Token: 0x040018B6 RID: 6326
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x040018B7 RID: 6327
		private FieldNameLookup _fieldNameLookup;
	}
}
