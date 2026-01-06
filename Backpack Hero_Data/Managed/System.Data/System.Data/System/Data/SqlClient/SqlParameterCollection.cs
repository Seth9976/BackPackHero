using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.SqlClient
{
	/// <summary>Represents a collection of parameters associated with a <see cref="T:System.Data.SqlClient.SqlCommand" /> and their respective mappings to columns in a <see cref="T:System.Data.DataSet" />. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CB RID: 459
	public sealed class SqlParameterCollection : DbParameterCollection, ICollection, IEnumerable, IList, IDataParameterCollection
	{
		// Token: 0x0600163A RID: 5690 RVA: 0x0004F1FC File Offset: 0x0004D3FC
		internal SqlParameterCollection()
		{
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x0006DFCF File Offset: 0x0006C1CF
		// (set) Token: 0x0600163C RID: 5692 RVA: 0x0006DFD7 File Offset: 0x0006C1D7
		internal bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
			set
			{
				this._isDirty = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> has a fixed size. </summary>
		/// <returns>Returns true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> has a fixed size; otherwise false.</returns>
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x0006DFE0 File Offset: 0x0006C1E0
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> is read-only. </summary>
		/// <returns>Returns true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> is read only; otherwise false.</returns>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0006DFED File Offset: 0x0006C1ED
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlParameter" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlParameter" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the parameter to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700044C RID: 1100
		public SqlParameter this[int index]
		{
			get
			{
				return (SqlParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified name.</returns>
		/// <param name="parameterName">The name of the parameter to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified <paramref name="parameterName" /> is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700044D RID: 1101
		public SqlParameter this[string parameterName]
		{
			get
			{
				return (SqlParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlParameter" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.SqlClient.SqlParameter" /> specified in the <paramref name="value" /> parameter is already added to this or another <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The parameter passed was not a <see cref="T:System.Data.SqlClient.SqlParameter" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001643 RID: 5699 RVA: 0x0006E02A File Offset: 0x0006C22A
		public SqlParameter Add(SqlParameter value)
		{
			this.Add(value);
			return value;
		}

		/// <summary>Adds a value to the end of the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="value">The value to be added. Use <see cref="F:System.DBNull.Value" /> instead of null, to indicate a null value.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001644 RID: 5700 RVA: 0x0006E035 File Offset: 0x0006C235
		public SqlParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new SqlParameter(parameterName, value));
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlClient.SqlParameter" /> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> given the parameter name and the data type.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="sqlDbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001645 RID: 5701 RVA: 0x0006E044 File Offset: 0x0006C244
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType));
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlClient.SqlParameter" /> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />, given the specified parameter name, <see cref="T:System.Data.SqlDbType" /> and size.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="sqlDbType">The <see cref="T:System.Data.SqlDbType" /> of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to add to the collection. </param>
		/// <param name="size">The size as an <see cref="T:System.Int32" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001646 RID: 5702 RVA: 0x0006E053 File Offset: 0x0006C253
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size));
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlClient.SqlParameter" /> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> with the parameter name, the data type, and the column length.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="sqlDbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <param name="size">The column length.</param>
		/// <param name="sourceColumn">The name of the source column (<see cref="P:System.Data.SqlClient.SqlParameter.SourceColumn" />) if this <see cref="T:System.Data.SqlClient.SqlParameter" /> is used in a call to <see cref="Overload:System.Data.Common.DbDataAdapter.Update" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001647 RID: 5703 RVA: 0x0006E063 File Offset: 0x0006C263
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size, string sourceColumn)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size, sourceColumn));
		}

		/// <summary>Adds an array of <see cref="T:System.Data.SqlClient.SqlParameter" /> values to the end of the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Data.SqlClient.SqlParameter" /> values to add.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001648 RID: 5704 RVA: 0x0006E075 File Offset: 0x0006C275
		public void AddRange(SqlParameter[] values)
		{
			this.AddRange(values);
		}

		/// <summary>Determines whether the specified parameter name is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> contains the value; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.String" /> value.</param>
		// Token: 0x06001649 RID: 5705 RVA: 0x0006E07E File Offset: 0x0006C27E
		public override bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> contains the value; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlParameter" /> value.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600164A RID: 5706 RVA: 0x0006E08D File Offset: 0x0006C28D
		public bool Contains(SqlParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> to the specified <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> starting at the specified destination index.</summary>
		/// <param name="array">The <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> that is the destination of the elements copied from the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at which copying starts.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600164B RID: 5707 RVA: 0x0006E09C File Offset: 0x0006C29C
		public void CopyTo(SqlParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> that is a <see cref="T:System.Data.SqlClient.SqlParameter" /> within the collection. Returns -1 when the object does not exist in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlParameter" /> to find. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600164C RID: 5708 RVA: 0x0006E0A6 File Offset: 0x0006C2A6
		public int IndexOf(SqlParameter value)
		{
			return this.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Data.SqlClient.SqlParameter" /> object into the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Data.SqlClient.SqlParameter" /> object to be inserted in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600164D RID: 5709 RVA: 0x0006E0AF File Offset: 0x0006C2AF
		public void Insert(int index, SqlParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0006E0B9 File Offset: 0x0006C2B9
		private void OnChange()
		{
			this.IsDirty = true;
		}

		/// <summary>Removes the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> from the collection.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlClient.SqlParameter" /> object to remove from the collection. </param>
		/// <exception cref="T:System.InvalidCastException">The parameter is not a <see cref="T:System.Data.SqlClient.SqlParameter" />. </exception>
		/// <exception cref="T:System.SystemException">The parameter does not exist in the collection. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600164F RID: 5711 RVA: 0x0006E0C2 File Offset: 0x0006C2C2
		public void Remove(SqlParameter value)
		{
			this.Remove(value);
		}

		/// <summary>Returns an Integer that contains the number of elements in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. Read-only. </summary>
		/// <returns>The number of elements in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> as an Integer.</returns>
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0006E0CB File Offset: 0x0006C2CB
		public override int Count
		{
			get
			{
				if (this._items == null)
				{
					return 0;
				}
				return this._items.Count;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0006E0E4 File Offset: 0x0006C2E4
		private List<SqlParameter> InnerList
		{
			get
			{
				List<SqlParameter> list = this._items;
				if (list == null)
				{
					list = new List<SqlParameter>();
					this._items = list;
				}
				return list;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. </summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0006E109 File Offset: 0x0006C309
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="value">An <see cref="T:System.Object" />.</param>
		// Token: 0x06001653 RID: 5715 RVA: 0x0006E116 File Offset: 0x0006C316
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((SqlParameter)value);
			return this.Count - 1;
		}

		/// <summary>Adds an array of values to the end of the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Array" /> values to add.</param>
		// Token: 0x06001654 RID: 5716 RVA: 0x0006E148 File Offset: 0x0006C348
		public override void AddRange(Array values)
		{
			this.OnChange();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			foreach (object obj in values)
			{
				this.ValidateType(obj);
			}
			foreach (object obj2 in values)
			{
				SqlParameter sqlParameter = (SqlParameter)obj2;
				this.Validate(-1, sqlParameter);
				this.InnerList.Add(sqlParameter);
			}
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0006E1FC File Offset: 0x0006C3FC
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, SqlParameterCollection.s_itemType);
			}
			return num;
		}

		/// <summary>Removes all the <see cref="T:System.Data.SqlClient.SqlParameter" /> objects from the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		// Token: 0x06001656 RID: 5718 RVA: 0x0006E218 File Offset: 0x0006C418
		public override void Clear()
		{
			this.OnChange();
			List<SqlParameter> innerList = this.InnerList;
			if (innerList != null)
			{
				foreach (SqlParameter sqlParameter in innerList)
				{
					sqlParameter.ResetParent();
				}
				innerList.Clear();
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> contains the value; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> value.</param>
		// Token: 0x06001657 RID: 5719 RVA: 0x0006E27C File Offset: 0x0006C47C
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Array" /> at which copying starts.</param>
		// Token: 0x06001658 RID: 5720 RVA: 0x0006E28B File Offset: 0x0006C48B
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. </returns>
		// Token: 0x06001659 RID: 5721 RVA: 0x0006E29A File Offset: 0x0006C49A
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0006E2A7 File Offset: 0x0006C4A7
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0006E2BC File Offset: 0x0006C4BC
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, SqlParameterCollection.s_itemType);
			}
			return this.InnerList[num];
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0006E2F0 File Offset: 0x0006C4F0
		private static int IndexOf(IEnumerable items, string parameterName)
		{
			if (items != null)
			{
				int num = 0;
				foreach (object obj in items)
				{
					SqlParameter sqlParameter = (SqlParameter)obj;
					if (parameterName == sqlParameter.ParameterName)
					{
						return num;
					}
					num++;
				}
				num = 0;
				foreach (object obj2 in items)
				{
					SqlParameter sqlParameter2 = (SqlParameter)obj2;
					if (ADP.DstCompare(parameterName, sqlParameter2.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				return -1;
			}
			return -1;
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified name.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified case-sensitive name. Returns -1 when the object does not exist in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		/// <param name="parameterName">The case-sensitive name of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to find.</param>
		// Token: 0x0600165D RID: 5725 RVA: 0x0006E3BC File Offset: 0x0006C5BC
		public override int IndexOf(string parameterName)
		{
			return SqlParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Object" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.SqlClient.SqlParameter" /> within the collection. Returns -1 when the object does not exist in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to find. </param>
		// Token: 0x0600165E RID: 5726 RVA: 0x0006E3CC File Offset: 0x0006C5CC
		public override int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				List<SqlParameter> innerList = this.InnerList;
				if (innerList != null)
				{
					int count = innerList.Count;
					for (int i = 0; i < count; i++)
					{
						if (value == innerList[i])
						{
							return i;
						}
					}
				}
			}
			return -1;
		}

		/// <summary>Inserts an <see cref="T:System.Object" /> into the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">An <see cref="T:System.Object" /> to be inserted in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		// Token: 0x0600165F RID: 5727 RVA: 0x0006E40D File Offset: 0x0006C60D
		public override void Insert(int index, object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, (SqlParameter)value);
			this.InnerList.Insert(index, (SqlParameter)value);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0006E43B File Offset: 0x0006C63B
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ParametersMappingIndex(index, this);
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> from the collection.</summary>
		/// <param name="value">The object to remove from the collection. </param>
		// Token: 0x06001661 RID: 5729 RVA: 0x0006E454 File Offset: 0x0006C654
		public override void Remove(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			if (this != ((SqlParameter)value).CompareExchangeParent(null, this))
			{
				throw ADP.CollectionRemoveInvalidObject(SqlParameterCollection.s_itemType, this);
			}
		}

		/// <summary>Removes the <see cref="T:System.Data.SqlClient.SqlParameter" /> from the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.SqlClient.SqlParameter" /> object to remove.</param>
		// Token: 0x06001662 RID: 5730 RVA: 0x0006E49E File Offset: 0x0006C69E
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		/// <summary>Removes the <see cref="T:System.Data.SqlClient.SqlParameter" /> from the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified parameter name.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to remove.</param>
		// Token: 0x06001663 RID: 5731 RVA: 0x0006E4B4 File Offset: 0x0006C6B4
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int num = this.CheckName(parameterName);
			this.RemoveIndex(num);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0006E4D8 File Offset: 0x0006C6D8
		private void RemoveIndex(int index)
		{
			List<SqlParameter> innerList = this.InnerList;
			SqlParameter sqlParameter = innerList[index];
			innerList.RemoveAt(index);
			sqlParameter.ResetParent();
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0006E500 File Offset: 0x0006C700
		private void Replace(int index, object newValue)
		{
			List<SqlParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			SqlParameter sqlParameter = innerList[index];
			innerList[index] = (SqlParameter)newValue;
			sqlParameter.ResetParent();
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0006E53C File Offset: 0x0006C73C
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0006E554 File Offset: 0x0006C754
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.OnChange();
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, SqlParameterCollection.s_itemType);
			}
			this.Replace(num, value);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0006E588 File Offset: 0x0006C788
		private void Validate(int index, object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, SqlParameterCollection.s_itemType);
			}
			object obj = ((SqlParameter)value).CompareExchangeParent(this, null);
			if (obj != null)
			{
				if (this != obj)
				{
					throw ADP.ParametersIsNotParent(SqlParameterCollection.s_itemType, this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.ParametersIsParent(SqlParameterCollection.s_itemType, this);
				}
			}
			string text = ((SqlParameter)value).ParameterName;
			if (text.Length == 0)
			{
				index = 1;
				do
				{
					text = "Parameter" + index.ToString(CultureInfo.CurrentCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				((SqlParameter)value).ParameterName = text;
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0006E629 File Offset: 0x0006C829
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, SqlParameterCollection.s_itemType);
			}
			if (!SqlParameterCollection.s_itemType.IsInstanceOfType(value))
			{
				throw ADP.InvalidParameterType(this, SqlParameterCollection.s_itemType, value);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.Use caution when you are using this overload of the SqlParameterCollection.Add method to specify integer parameter values. Because this overload takes a <paramref name="value" /> of type <see cref="T:System.Object" />, you must convert the integral value to an <see cref="T:System.Object" /> type when the value is zero, as the following C# example demonstrates. Copy Codeparameters.Add("@pname", Convert.ToInt32(0));If you do not perform this conversion, the compiler assumes that you are trying to call the SqlParameterCollection.Add (string, SqlDbType) overload.</returns>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to add to the collection.</param>
		/// <param name="value">A <see cref="T:System.Object" />.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.SqlClient.SqlParameter" /> specified in the <paramref name="value" /> parameter is already added to this or another <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600166A RID: 5738 RVA: 0x0006E035 File Offset: 0x0006C235
		public SqlParameter Add(string parameterName, object value)
		{
			return this.Add(new SqlParameter(parameterName, value));
		}

		// Token: 0x04000EDE RID: 3806
		private bool _isDirty;

		// Token: 0x04000EDF RID: 3807
		private static Type s_itemType = typeof(SqlParameter);

		// Token: 0x04000EE0 RID: 3808
		private List<SqlParameter> _items;
	}
}
