using System;
using System.Collections;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	/// <summary>Exposes the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method, which supports a simple iteration over a collection by a .NET Framework data provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000342 RID: 834
	public class DbEnumerator : IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbEnumerator" /> class using the specified DataReader.</summary>
		/// <param name="reader">The DataReader through which to iterate. </param>
		// Token: 0x0600288B RID: 10379 RVA: 0x000B20CB File Offset: 0x000B02CB
		public DbEnumerator(IDataReader reader)
		{
			if (reader == null)
			{
				throw ADP.ArgumentNull("reader");
			}
			this._reader = reader;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbEnumerator" /> class using the specified DataReader, and indicates whether to automatically close the DataReader after iterating through its data.</summary>
		/// <param name="reader">The DataReader through which to iterate. </param>
		/// <param name="closeReader">true to automatically close the DataReader after iterating through its data; otherwise, false. </param>
		// Token: 0x0600288C RID: 10380 RVA: 0x000B20E8 File Offset: 0x000B02E8
		public DbEnumerator(IDataReader reader, bool closeReader)
		{
			if (reader == null)
			{
				throw ADP.ArgumentNull("reader");
			}
			this._reader = reader;
			this._closeReader = closeReader;
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000B210C File Offset: 0x000B030C
		public DbEnumerator(DbDataReader reader)
			: this(reader)
		{
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000B2115 File Offset: 0x000B0315
		public DbEnumerator(DbDataReader reader, bool closeReader)
			: this(reader, closeReader)
		{
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x000B211F File Offset: 0x000B031F
		public object Current
		{
			get
			{
				return this._current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002890 RID: 10384 RVA: 0x000B2128 File Offset: 0x000B0328
		public bool MoveNext()
		{
			if (this._schemaInfo == null)
			{
				this.BuildSchemaInfo();
			}
			this._current = null;
			if (this._reader.Read())
			{
				object[] array = new object[this._schemaInfo.Length];
				this._reader.GetValues(array);
				this._current = new DataRecordInternal(this._schemaInfo, array, this._descriptors, this._fieldNameLookup);
				return true;
			}
			if (this._closeReader)
			{
				this._reader.Close();
			}
			return false;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002891 RID: 10385 RVA: 0x00060F32 File Offset: 0x0005F132
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Reset()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000B21A8 File Offset: 0x000B03A8
		private void BuildSchemaInfo()
		{
			int fieldCount = this._reader.FieldCount;
			string[] array = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = this._reader.GetName(i);
			}
			ADP.BuildSchemaTableInfoTableNames(array);
			SchemaInfo[] array2 = new SchemaInfo[fieldCount];
			PropertyDescriptor[] array3 = new PropertyDescriptor[this._reader.FieldCount];
			for (int j = 0; j < array2.Length; j++)
			{
				SchemaInfo schemaInfo = default(SchemaInfo);
				schemaInfo.name = this._reader.GetName(j);
				schemaInfo.type = this._reader.GetFieldType(j);
				schemaInfo.typeName = this._reader.GetDataTypeName(j);
				array3[j] = new DbEnumerator.DbColumnDescriptor(j, array[j], schemaInfo.type);
				array2[j] = schemaInfo;
			}
			this._schemaInfo = array2;
			this._fieldNameLookup = new FieldNameLookup(this._reader, -1);
			this._descriptors = new PropertyDescriptorCollection(array3);
		}

		// Token: 0x04001933 RID: 6451
		internal IDataReader _reader;

		// Token: 0x04001934 RID: 6452
		internal DbDataRecord _current;

		// Token: 0x04001935 RID: 6453
		internal SchemaInfo[] _schemaInfo;

		// Token: 0x04001936 RID: 6454
		internal PropertyDescriptorCollection _descriptors;

		// Token: 0x04001937 RID: 6455
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04001938 RID: 6456
		private bool _closeReader;

		// Token: 0x02000343 RID: 835
		private sealed class DbColumnDescriptor : PropertyDescriptor
		{
			// Token: 0x06002893 RID: 10387 RVA: 0x000B22A2 File Offset: 0x000B04A2
			internal DbColumnDescriptor(int ordinal, string name, Type type)
				: base(name, null)
			{
				this._ordinal = ordinal;
				this._type = type;
			}

			// Token: 0x170006F1 RID: 1777
			// (get) Token: 0x06002894 RID: 10388 RVA: 0x000B22BA File Offset: 0x000B04BA
			public override Type ComponentType
			{
				get
				{
					return typeof(IDataRecord);
				}
			}

			// Token: 0x170006F2 RID: 1778
			// (get) Token: 0x06002895 RID: 10389 RVA: 0x0000CD07 File Offset: 0x0000AF07
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170006F3 RID: 1779
			// (get) Token: 0x06002896 RID: 10390 RVA: 0x000B22C6 File Offset: 0x000B04C6
			public override Type PropertyType
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x06002897 RID: 10391 RVA: 0x00005AE9 File Offset: 0x00003CE9
			public override bool CanResetValue(object component)
			{
				return false;
			}

			// Token: 0x06002898 RID: 10392 RVA: 0x000B22CE File Offset: 0x000B04CE
			public override object GetValue(object component)
			{
				return ((IDataRecord)component)[this._ordinal];
			}

			// Token: 0x06002899 RID: 10393 RVA: 0x00060F32 File Offset: 0x0005F132
			public override void ResetValue(object component)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x0600289A RID: 10394 RVA: 0x00060F32 File Offset: 0x0005F132
			public override void SetValue(object component, object value)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x0600289B RID: 10395 RVA: 0x00005AE9 File Offset: 0x00003CE9
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x04001939 RID: 6457
			private int _ordinal;

			// Token: 0x0400193A RID: 6458
			private Type _type;
		}
	}
}
