using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data
{
	/// <summary>Represents an in-memory cache of data.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000071 RID: 113
	[DefaultProperty("DataSetName")]
	[XmlRoot("DataSet")]
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.DataSetToolboxItem, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[XmlSchemaProvider("GetDataSetSchema")]
	[Serializable]
	public class DataSet : MarshalByValueComponent, IListSource, IXmlSerializable, ISupportInitializeNotification, ISupportInitialize, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSet" /> class.</summary>
		// Token: 0x06000654 RID: 1620 RVA: 0x00017E48 File Offset: 0x00016048
		public DataSet()
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int>("<ds.DataSet.DataSet|API> {0}", this.ObjectID);
			this._tableCollection = new DataTableCollection(this);
			this._relationCollection = new DataRelationCollection.DataSetRelationCollection(this);
			this._culture = CultureInfo.CurrentCulture;
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataSet" /> class with the given name.</summary>
		/// <param name="dataSetName">The name of the <see cref="T:System.Data.DataSet" />.</param>
		// Token: 0x06000655 RID: 1621 RVA: 0x00017EEE File Offset: 0x000160EE
		public DataSet(string dataSetName)
			: this()
		{
			this.DataSetName = dataSetName;
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SerializationFormat" /> for the <see cref="T:System.Data.DataSet" /> used during remoting.</summary>
		/// <returns>A <see cref="T:System.Data.SerializationFormat" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00017EFD File Offset: 0x000160FD
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x00017F08 File Offset: 0x00016108
		[DefaultValue(SerializationFormat.Xml)]
		public SerializationFormat RemotingFormat
		{
			get
			{
				return this._remotingFormat;
			}
			set
			{
				if (value != SerializationFormat.Binary && value != SerializationFormat.Xml)
				{
					throw ExceptionBuilder.InvalidRemotingFormat(value);
				}
				this._remotingFormat = value;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].RemotingFormat = value;
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>Gets or sets a <see cref="T:System.Data.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0000CD07 File Offset: 0x0000AF07
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x00017F52 File Offset: 0x00016152
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual SchemaSerializationMode SchemaSerializationMode
		{
			get
			{
				return SchemaSerializationMode.IncludeSchema;
			}
			set
			{
				if (value != SchemaSerializationMode.IncludeSchema)
				{
					throw ExceptionBuilder.CannotChangeSchemaSerializationMode();
				}
			}
		}

		/// <summary>Inspects the format of the serialized representation of the DataSet.</summary>
		/// <returns>true if the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> represents a DataSet serialized in its binary format, false otherwise.</returns>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x0600065A RID: 1626 RVA: 0x00017F60 File Offset: 0x00016160
		protected bool IsBinarySerialized(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "DataSet.RemotingFormat")
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
					break;
				}
			}
			return serializationFormat == SerializationFormat.Binary;
		}

		/// <summary>Determines the <see cref="P:System.Data.DataSet.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>An <see cref="T:System.Data.SchemaSerializationMode" /> enumeration indicating whether schema information has been omitted from the payload.</returns>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that a DataSet’s protected constructor <see cref="M:System.Data.DataSet.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> is invoked with during deserialization in remoting scenarios. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that a DataSet’s protected constructor <see cref="M:System.Data.DataSet.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> is invoked with during deserialization in remoting scenarios.</param>
		// Token: 0x0600065B RID: 1627 RVA: 0x00017FA4 File Offset: 0x000161A4
		protected SchemaSerializationMode DetermineSchemaSerializationMode(SerializationInfo info, StreamingContext context)
		{
			SchemaSerializationMode schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "SchemaSerializationMode.DataSet")
				{
					schemaSerializationMode = (SchemaSerializationMode)enumerator.Value;
					break;
				}
			}
			return schemaSerializationMode;
		}

		/// <summary>Determines the <see cref="P:System.Data.DataSet.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>An <see cref="T:System.Data.SchemaSerializationMode" /> enumeration indicating whether schema information has been omitted from the payload.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> instance that is passed during deserialization of the <see cref="T:System.Data.DataSet" />.</param>
		// Token: 0x0600065C RID: 1628 RVA: 0x00017FE8 File Offset: 0x000161E8
		protected SchemaSerializationMode DetermineSchemaSerializationMode(XmlReader reader)
		{
			SchemaSerializationMode schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			reader.MoveToContent();
			if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes)
			{
				string attribute = reader.GetAttribute("SchemaSerializationMode", "urn:schemas-microsoft-com:xml-msdata");
				if (string.Equals(attribute, "ExcludeSchema", StringComparison.OrdinalIgnoreCase))
				{
					schemaSerializationMode = SchemaSerializationMode.ExcludeSchema;
				}
				else if (string.Equals(attribute, "IncludeSchema", StringComparison.OrdinalIgnoreCase))
				{
					schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
				}
				else if (attribute != null)
				{
					throw ExceptionBuilder.InvalidSchemaSerializationMode(typeof(SchemaSerializationMode), attribute);
				}
			}
			return schemaSerializationMode;
		}

		/// <summary>Deserializes the table data from the binary or XML stream.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance. </param>
		/// <param name="context">The streaming context. </param>
		// Token: 0x0600065D RID: 1629 RVA: 0x0001805C File Offset: 0x0001625C
		protected void GetSerializationData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "DataSet.RemotingFormat")
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
					break;
				}
			}
			this.DeserializeDataSetData(info, context, serializationFormat);
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataSet" /> class that has the given serialization information and context.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a given serialized stream.</param>
		// Token: 0x0600065E RID: 1630 RVA: 0x000180A5 File Offset: 0x000162A5
		protected DataSet(SerializationInfo info, StreamingContext context)
			: this(info, context, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSet" /> class.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		/// <param name="ConstructSchema">The boolean value.</param>
		// Token: 0x0600065F RID: 1631 RVA: 0x000180B0 File Offset: 0x000162B0
		protected DataSet(SerializationInfo info, StreamingContext context, bool ConstructSchema)
			: this()
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SchemaSerializationMode schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "DataSet.RemotingFormat"))
				{
					if (name == "SchemaSerializationMode.DataSet")
					{
						schemaSerializationMode = (SchemaSerializationMode)enumerator.Value;
					}
				}
				else
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
				}
			}
			if (schemaSerializationMode == SchemaSerializationMode.ExcludeSchema)
			{
				this.InitializeDerivedDataSet();
			}
			if (serializationFormat == SerializationFormat.Xml && !ConstructSchema)
			{
				return;
			}
			this.DeserializeDataSet(info, context, serializationFormat, schemaSerializationMode);
		}

		/// <summary>Populates a serialization information object with the data needed to serialize the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data associated with the <see cref="T:System.Data.DataSet" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the <see cref="T:System.Data.DataSet" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x06000660 RID: 1632 RVA: 0x00018130 File Offset: 0x00016330
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat remotingFormat = this.RemotingFormat;
			this.SerializeDataSet(info, context, remotingFormat);
		}

		/// <summary>Deserialize all of the tables data of the DataSet from the binary or XML stream.</summary>
		// Token: 0x06000661 RID: 1633 RVA: 0x000094D4 File Offset: 0x000076D4
		protected virtual void InitializeDerivedDataSet()
		{
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00018150 File Offset: 0x00016350
		private void SerializeDataSet(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat)
		{
			info.AddValue("DataSet.RemotingVersion", new Version(2, 0));
			if (remotingFormat != SerializationFormat.Xml)
			{
				info.AddValue("DataSet.RemotingFormat", remotingFormat);
			}
			if (SchemaSerializationMode.IncludeSchema != this.SchemaSerializationMode)
			{
				info.AddValue("SchemaSerializationMode.DataSet", this.SchemaSerializationMode);
			}
			if (remotingFormat != SerializationFormat.Xml)
			{
				if (this.SchemaSerializationMode == SchemaSerializationMode.IncludeSchema)
				{
					this.SerializeDataSetProperties(info, context);
					info.AddValue("DataSet.Tables.Count", this.Tables.Count);
					for (int i = 0; i < this.Tables.Count; i++)
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(context.State, false));
						MemoryStream memoryStream = new MemoryStream();
						binaryFormatter.Serialize(memoryStream, this.Tables[i]);
						memoryStream.Position = 0L;
						info.AddValue(string.Format(CultureInfo.InvariantCulture, "DataSet.Tables_{0}", i), memoryStream.GetBuffer());
					}
					for (int j = 0; j < this.Tables.Count; j++)
					{
						this.Tables[j].SerializeConstraints(info, context, j, true);
					}
					this.SerializeRelations(info, context);
					for (int k = 0; k < this.Tables.Count; k++)
					{
						this.Tables[k].SerializeExpressionColumns(info, context, k);
					}
				}
				else
				{
					this.SerializeDataSetProperties(info, context);
				}
				for (int l = 0; l < this.Tables.Count; l++)
				{
					this.Tables[l].SerializeTableData(info, context, l);
				}
				return;
			}
			string xmlSchemaForRemoting = this.GetXmlSchemaForRemoting(null);
			info.AddValue("XmlSchema", xmlSchemaForRemoting);
			StringWriter stringWriter = new StringWriter(new StringBuilder(this.EstimatedXmlStringSize() * 2), CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			this.WriteXml(xmlTextWriter, XmlWriteMode.DiffGram);
			string text = stringWriter.ToString();
			info.AddValue("XmlDiffGram", text);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001832E File Offset: 0x0001652E
		internal void DeserializeDataSet(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat, SchemaSerializationMode schemaSerializationMode)
		{
			this.DeserializeDataSetSchema(info, context, remotingFormat, schemaSerializationMode);
			this.DeserializeDataSetData(info, context, remotingFormat);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00018344 File Offset: 0x00016544
		private void DeserializeDataSetSchema(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat, SchemaSerializationMode schemaSerializationMode)
		{
			if (remotingFormat == SerializationFormat.Xml)
			{
				string text = (string)info.GetValue("XmlSchema", typeof(string));
				if (text != null)
				{
					this.ReadXmlSchema(new XmlTextReader(new StringReader(text)), true);
				}
				return;
			}
			if (schemaSerializationMode == SchemaSerializationMode.IncludeSchema)
			{
				this.DeserializeDataSetProperties(info, context);
				int @int = info.GetInt32("DataSet.Tables.Count");
				for (int i = 0; i < @int; i++)
				{
					MemoryStream memoryStream = new MemoryStream((byte[])info.GetValue(string.Format(CultureInfo.InvariantCulture, "DataSet.Tables_{0}", i), typeof(byte[])));
					memoryStream.Position = 0L;
					DataTable dataTable = (DataTable)new BinaryFormatter(null, new StreamingContext(context.State, false)).Deserialize(memoryStream);
					this.Tables.Add(dataTable);
				}
				for (int j = 0; j < @int; j++)
				{
					this.Tables[j].DeserializeConstraints(info, context, j, true);
				}
				this.DeserializeRelations(info, context);
				for (int k = 0; k < @int; k++)
				{
					this.Tables[k].DeserializeExpressionColumns(info, context, k);
				}
				return;
			}
			this.DeserializeDataSetProperties(info, context);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001847C File Offset: 0x0001667C
		private void DeserializeDataSetData(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat)
		{
			if (remotingFormat != SerializationFormat.Xml)
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].DeserializeTableData(info, context, i);
				}
				return;
			}
			string text = (string)info.GetValue("XmlDiffGram", typeof(string));
			if (text != null)
			{
				this.ReadXml(new XmlTextReader(new StringReader(text)), XmlReadMode.DiffGram);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000184E8 File Offset: 0x000166E8
		private void SerializeDataSetProperties(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("DataSet.DataSetName", this.DataSetName);
			info.AddValue("DataSet.Namespace", this.Namespace);
			info.AddValue("DataSet.Prefix", this.Prefix);
			info.AddValue("DataSet.CaseSensitive", this.CaseSensitive);
			info.AddValue("DataSet.LocaleLCID", this.Locale.LCID);
			info.AddValue("DataSet.EnforceConstraints", this.EnforceConstraints);
			info.AddValue("DataSet.ExtendedProperties", this.ExtendedProperties);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00018574 File Offset: 0x00016774
		private void DeserializeDataSetProperties(SerializationInfo info, StreamingContext context)
		{
			this._dataSetName = info.GetString("DataSet.DataSetName");
			this._namespaceURI = info.GetString("DataSet.Namespace");
			this._datasetPrefix = info.GetString("DataSet.Prefix");
			this._caseSensitive = info.GetBoolean("DataSet.CaseSensitive");
			int num = (int)info.GetValue("DataSet.LocaleLCID", typeof(int));
			this._culture = new CultureInfo(num);
			this._cultureUserSet = true;
			this._enforceConstraints = info.GetBoolean("DataSet.EnforceConstraints");
			this._extendedProperties = (PropertyCollection)info.GetValue("DataSet.ExtendedProperties", typeof(PropertyCollection));
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00018624 File Offset: 0x00016824
		private void SerializeRelations(SerializationInfo info, StreamingContext context)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.Relations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				int[] array = new int[dataRelation.ParentColumns.Length + 1];
				array[0] = this.Tables.IndexOf(dataRelation.ParentTable);
				for (int i = 1; i < array.Length; i++)
				{
					array[i] = dataRelation.ParentColumns[i - 1].Ordinal;
				}
				int[] array2 = new int[dataRelation.ChildColumns.Length + 1];
				array2[0] = this.Tables.IndexOf(dataRelation.ChildTable);
				for (int j = 1; j < array2.Length; j++)
				{
					array2[j] = dataRelation.ChildColumns[j - 1].Ordinal;
				}
				arrayList.Add(new ArrayList { dataRelation.RelationName, array, array2, dataRelation.Nested, dataRelation._extendedProperties });
			}
			info.AddValue("DataSet.Relations", arrayList);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00018788 File Offset: 0x00016988
		private void DeserializeRelations(SerializationInfo info, StreamingContext context)
		{
			foreach (object obj in ((ArrayList)info.GetValue("DataSet.Relations", typeof(ArrayList))))
			{
				ArrayList arrayList = (ArrayList)obj;
				string text = (string)arrayList[0];
				int[] array = (int[])arrayList[1];
				int[] array2 = (int[])arrayList[2];
				bool flag = (bool)arrayList[3];
				PropertyCollection propertyCollection = (PropertyCollection)arrayList[4];
				DataColumn[] array3 = new DataColumn[array.Length - 1];
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i] = this.Tables[array[0]].Columns[array[i + 1]];
				}
				DataColumn[] array4 = new DataColumn[array2.Length - 1];
				for (int j = 0; j < array4.Length; j++)
				{
					array4[j] = this.Tables[array2[0]].Columns[array2[j + 1]];
				}
				DataRelation dataRelation = new DataRelation(text, array3, array4, false);
				dataRelation.CheckMultipleNested = false;
				dataRelation.Nested = flag;
				dataRelation._extendedProperties = propertyCollection;
				this.Relations.Add(dataRelation);
				dataRelation.CheckMultipleNested = true;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00018904 File Offset: 0x00016B04
		internal void FailedEnableConstraints()
		{
			this.EnforceConstraints = false;
			throw ExceptionBuilder.EnforceConstraint();
		}

		/// <summary>Gets or sets a value indicating whether string comparisons within <see cref="T:System.Data.DataTable" /> objects are case-sensitive.</summary>
		/// <returns>true if string comparisons are case-sensitive; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00018912 File Offset: 0x00016B12
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x0001891C File Offset: 0x00016B1C
		[DefaultValue(false)]
		public bool CaseSensitive
		{
			get
			{
				return this._caseSensitive;
			}
			set
			{
				if (this._caseSensitive != value)
				{
					bool caseSensitive = this._caseSensitive;
					this._caseSensitive = value;
					if (!this.ValidateCaseConstraint())
					{
						this._caseSensitive = caseSensitive;
						throw ExceptionBuilder.CannotChangeCaseLocale();
					}
					foreach (object obj in this.Tables)
					{
						((DataTable)obj).SetCaseSensitiveValue(value, false, true);
					}
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IListSource.ContainsListCollection" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IListSource.ContainsListCollection" />.</returns>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0000CD07 File Offset: 0x0000AF07
		bool IListSource.ContainsListCollection
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a custom view of the data contained in the <see cref="T:System.Data.DataSet" /> to allow filtering, searching, and navigating using a custom <see cref="T:System.Data.DataViewManager" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewManager" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x000189A4 File Offset: 0x00016BA4
		[Browsable(false)]
		public DataViewManager DefaultViewManager
		{
			get
			{
				if (this._defaultViewManager == null)
				{
					object defaultViewManagerLock = this._defaultViewManagerLock;
					lock (defaultViewManagerLock)
					{
						if (this._defaultViewManager == null)
						{
							this._defaultViewManager = new DataViewManager(this, true);
						}
					}
				}
				return this._defaultViewManager;
			}
		}

		/// <summary>Gets or sets a value indicating whether constraint rules are followed when attempting any update operation.</summary>
		/// <returns>true if rules are enforced; otherwise false. The default is true.</returns>
		/// <exception cref="T:System.Data.ConstraintException">One or more constraints cannot be enforced. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00018A04 File Offset: 0x00016C04
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x00018A0C File Offset: 0x00016C0C
		[DefaultValue(true)]
		public bool EnforceConstraints
		{
			get
			{
				return this._enforceConstraints;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.set_EnforceConstraints|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._enforceConstraints != value)
					{
						if (value)
						{
							this.EnableConstraints();
						}
						this._enforceConstraints = value;
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00018A68 File Offset: 0x00016C68
		internal void RestoreEnforceConstraints(bool value)
		{
			this._enforceConstraints = value;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00018A74 File Offset: 0x00016C74
		internal void EnableConstraints()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.EnableConstraints|INFO> {0}", this.ObjectID);
			try
			{
				bool flag = false;
				ConstraintEnumerator constraintEnumerator = new ConstraintEnumerator(this);
				while (constraintEnumerator.GetNext())
				{
					Constraint constraint = constraintEnumerator.GetConstraint();
					flag |= constraint.IsConstraintViolated();
				}
				foreach (object obj in this.Tables)
				{
					foreach (object obj2 in ((DataTable)obj).Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (!dataColumn.AllowDBNull)
						{
							flag |= dataColumn.IsNotAllowDBNullViolated();
						}
						if (dataColumn.MaxLength >= 0)
						{
							flag |= dataColumn.IsMaxLengthViolated();
						}
					}
				}
				if (flag)
				{
					this.FailedEnableConstraints();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Gets or sets the name of the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00018B98 File Offset: 0x00016D98
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x00018BA0 File Offset: 0x00016DA0
		[DefaultValue("")]
		public string DataSetName
		{
			get
			{
				return this._dataSetName;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataSet.set_DataSetName|API> {0}, '{1}'", this.ObjectID, value);
				if (value != this._dataSetName)
				{
					if (value == null || value.Length == 0)
					{
						throw ExceptionBuilder.SetDataSetNameToEmpty();
					}
					DataTable dataTable = this.Tables[value, this.Namespace];
					if (dataTable != null && !dataTable._fNestedInDataset)
					{
						throw ExceptionBuilder.SetDataSetNameConflicting(value);
					}
					this.RaisePropertyChanging("DataSetName");
					this._dataSetName = value;
				}
			}
		}

		/// <summary>Gets or sets the namespace of the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Data.DataSet" />.</returns>
		/// <exception cref="T:System.ArgumentException">The namespace already has data. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00018C19 File Offset: 0x00016E19
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x00018C24 File Offset: 0x00016E24
		[DefaultValue("")]
		public string Namespace
		{
			get
			{
				return this._namespaceURI;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataSet.set_Namespace|API> {0}, '{1}'", this.ObjectID, value);
				if (value == null)
				{
					value = string.Empty;
				}
				if (value != this._namespaceURI)
				{
					this.RaisePropertyChanging("Namespace");
					foreach (object obj in this.Tables)
					{
						DataTable dataTable = (DataTable)obj;
						if (dataTable._tableNamespace == null && (dataTable.NestedParentRelations.Length == 0 || (dataTable.NestedParentRelations.Length == 1 && dataTable.NestedParentRelations[0].ChildTable == dataTable)))
						{
							if (this.Tables.Contains(dataTable.TableName, value, false, true))
							{
								throw ExceptionBuilder.DuplicateTableName2(dataTable.TableName, value);
							}
							dataTable.CheckCascadingNamespaceConflict(value);
							dataTable.DoRaiseNamespaceChange();
						}
					}
					this._namespaceURI = value;
					if (string.IsNullOrEmpty(value))
					{
						this._datasetPrefix = string.Empty;
					}
				}
			}
		}

		/// <summary>Gets or sets an XML prefix that aliases the namespace of the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The XML prefix for the <see cref="T:System.Data.DataSet" /> namespace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00018D28 File Offset: 0x00016F28
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x00018D30 File Offset: 0x00016F30
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return this._datasetPrefix;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (XmlConvert.DecodeName(value) == value && XmlConvert.EncodeName(value) != value)
				{
					throw ExceptionBuilder.InvalidPrefix(value);
				}
				if (value != this._datasetPrefix)
				{
					this.RaisePropertyChanging("Prefix");
					this._datasetPrefix = value;
				}
			}
		}

		/// <summary>Gets the collection of customized user information associated with the DataSet.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> with all custom user information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00018D8C File Offset: 0x00016F8C
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				PropertyCollection propertyCollection;
				if ((propertyCollection = this._extendedProperties) == null)
				{
					propertyCollection = (this._extendedProperties = new PropertyCollection());
				}
				return propertyCollection;
			}
		}

		/// <summary>Gets a value indicating whether there are errors in any of the <see cref="T:System.Data.DataTable" /> objects within this <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>true if any table contains an error;otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00018DB4 File Offset: 0x00016FB4
		[Browsable(false)]
		public bool HasErrors
		{
			get
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					if (this.Tables[i].HasErrors)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataSet" /> is initialized.</summary>
		/// <returns>true to indicate the component has completed initialization; otherwise false.</returns>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00018DED File Offset: 0x00016FED
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return !this._fInitInProgress;
			}
		}

		/// <summary>Gets or sets the locale information used to compare strings within the table.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that contains data about the user's machine locale. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00018DF8 File Offset: 0x00016FF8
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x00018E00 File Offset: 0x00017000
		public CultureInfo Locale
		{
			get
			{
				return this._culture;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.set_Locale|API> {0}", this.ObjectID);
				try
				{
					if (value != null)
					{
						if (!this._culture.Equals(value))
						{
							this.SetLocaleValue(value, true);
						}
						this._cultureUserSet = true;
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00018E64 File Offset: 0x00017064
		internal void SetLocaleValue(CultureInfo value, bool userSet)
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			CultureInfo culture = this._culture;
			bool cultureUserSet = this._cultureUserSet;
			try
			{
				this._culture = value;
				this._cultureUserSet = userSet;
				foreach (object obj in this.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					if (!dataTable.ShouldSerializeLocale())
					{
						dataTable.SetLocaleValue(value, false, false);
					}
				}
				flag = this.ValidateLocaleConstraint();
				if (flag)
				{
					flag = false;
					foreach (object obj2 in this.Tables)
					{
						DataTable dataTable2 = (DataTable)obj2;
						num++;
						if (!dataTable2.ShouldSerializeLocale())
						{
							dataTable2.SetLocaleValue(value, false, true);
						}
					}
					flag = true;
				}
			}
			catch
			{
				flag2 = true;
				throw;
			}
			finally
			{
				if (!flag)
				{
					this._culture = culture;
					this._cultureUserSet = cultureUserSet;
					foreach (object obj3 in this.Tables)
					{
						DataTable dataTable3 = (DataTable)obj3;
						if (!dataTable3.ShouldSerializeLocale())
						{
							dataTable3.SetLocaleValue(culture, false, false);
						}
					}
					try
					{
						for (int i = 0; i < num; i++)
						{
							if (!this.Tables[i].ShouldSerializeLocale())
							{
								this.Tables[i].SetLocaleValue(culture, false, true);
							}
						}
					}
					catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
					{
						ADP.TraceExceptionWithoutRethrow(ex);
					}
					if (!flag2)
					{
						throw ExceptionBuilder.CannotChangeCaseLocale(null);
					}
				}
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00019070 File Offset: 0x00017270
		internal bool ShouldSerializeLocale()
		{
			return this._cultureUserSet;
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00019078 File Offset: 0x00017278
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x00019080 File Offset: 0x00017280
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				ISite site = this.Site;
				if (value == null && site != null)
				{
					IContainer container = site.Container;
					if (container != null)
					{
						for (int i = 0; i < this.Tables.Count; i++)
						{
							if (this.Tables[i].Site != null)
							{
								container.Remove(this.Tables[i]);
							}
						}
					}
				}
				base.Site = value;
			}
		}

		/// <summary>Get the collection of relations that link tables and allow navigation from parent tables to child tables.</summary>
		/// <returns>A <see cref="T:System.Data.DataRelationCollection" /> that contains a collection of <see cref="T:System.Data.DataRelation" /> objects. An empty collection is returned if no <see cref="T:System.Data.DataRelation" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000190E6 File Offset: 0x000172E6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataRelationCollection Relations
		{
			get
			{
				return this._relationCollection;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="P:System.Data.DataSet.Relations" /> property should be persisted.</summary>
		/// <returns>true if the property value has been changed from its default; otherwise false.</returns>
		// Token: 0x06000683 RID: 1667 RVA: 0x0000CD07 File Offset: 0x0000AF07
		protected virtual bool ShouldSerializeRelations()
		{
			return true;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000190EE File Offset: 0x000172EE
		private void ResetRelations()
		{
			this.Relations.Clear();
		}

		/// <summary>Gets the collection of tables contained in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The <see cref="T:System.Data.DataTableCollection" /> contained by this <see cref="T:System.Data.DataSet" />. An empty collection is returned if no <see cref="T:System.Data.DataTable" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x000190FB File Offset: 0x000172FB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataTableCollection Tables
		{
			get
			{
				return this._tableCollection;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="P:System.Data.DataSet.Tables" /> property should be persisted.</summary>
		/// <returns>true if the property value has been changed from its default; otherwise false.</returns>
		// Token: 0x06000686 RID: 1670 RVA: 0x0000CD07 File Offset: 0x0000AF07
		protected virtual bool ShouldSerializeTables()
		{
			return true;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00019103 File Offset: 0x00017303
		private void ResetTables()
		{
			this.Tables.Clear();
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00019110 File Offset: 0x00017310
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x00019118 File Offset: 0x00017318
		internal bool FBoundToDocument
		{
			get
			{
				return this._fBoundToDocument;
			}
			set
			{
				this._fBoundToDocument = value;
			}
		}

		/// <summary>Commits all the changes made to this <see cref="T:System.Data.DataSet" /> since it was loaded or since the last time <see cref="M:System.Data.DataSet.AcceptChanges" /> was called.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600068A RID: 1674 RVA: 0x00019124 File Offset: 0x00017324
		public void AcceptChanges()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.AcceptChanges|API> {0}", this.ObjectID);
			try
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].AcceptChanges();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600068B RID: 1675 RVA: 0x0001918C File Offset: 0x0001738C
		// (remove) Token: 0x0600068C RID: 1676 RVA: 0x000191C4 File Offset: 0x000173C4
		internal event PropertyChangedEventHandler PropertyChanging;

		/// <summary>Occurs when a target and source <see cref="T:System.Data.DataRow" /> have the same primary key value, and <see cref="P:System.Data.DataSet.EnforceConstraints" /> is set to true.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600068D RID: 1677 RVA: 0x000191FC File Offset: 0x000173FC
		// (remove) Token: 0x0600068E RID: 1678 RVA: 0x00019234 File Offset: 0x00017434
		public event MergeFailedEventHandler MergeFailed;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600068F RID: 1679 RVA: 0x0001926C File Offset: 0x0001746C
		// (remove) Token: 0x06000690 RID: 1680 RVA: 0x000192A4 File Offset: 0x000174A4
		internal event DataRowCreatedEventHandler DataRowCreated;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000691 RID: 1681 RVA: 0x000192DC File Offset: 0x000174DC
		// (remove) Token: 0x06000692 RID: 1682 RVA: 0x00019314 File Offset: 0x00017514
		internal event DataSetClearEventhandler ClearFunctionCalled;

		/// <summary>Occurs after the <see cref="T:System.Data.DataSet" /> is initialized.</summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000693 RID: 1683 RVA: 0x0001934C File Offset: 0x0001754C
		// (remove) Token: 0x06000694 RID: 1684 RVA: 0x00019384 File Offset: 0x00017584
		public event EventHandler Initialized;

		/// <summary>Begins the initialization of a <see cref="T:System.Data.DataSet" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000695 RID: 1685 RVA: 0x000193B9 File Offset: 0x000175B9
		public void BeginInit()
		{
			this._fInitInProgress = true;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Data.DataSet" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000696 RID: 1686 RVA: 0x000193C4 File Offset: 0x000175C4
		public void EndInit()
		{
			this.Tables.FinishInitCollection();
			for (int i = 0; i < this.Tables.Count; i++)
			{
				this.Tables[i].Columns.FinishInitCollection();
			}
			for (int j = 0; j < this.Tables.Count; j++)
			{
				this.Tables[j].Constraints.FinishInitConstraints();
			}
			((DataRelationCollection.DataSetRelationCollection)this.Relations).FinishInitRelations();
			this._fInitInProgress = false;
			this.OnInitialized();
		}

		/// <summary>Clears the <see cref="T:System.Data.DataSet" /> of any data by removing all rows in all tables.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000697 RID: 1687 RVA: 0x00019454 File Offset: 0x00017654
		public void Clear()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Clear|API> {0}", this.ObjectID);
			try
			{
				this.OnClearFunctionCalled(null);
				bool enforceConstraints = this.EnforceConstraints;
				this.EnforceConstraints = false;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].Clear();
				}
				this.EnforceConstraints = enforceConstraints;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Copies the structure of the <see cref="T:System.Data.DataSet" />, including all <see cref="T:System.Data.DataTable" /> schemas, relations, and constraints. Does not copy any data.</summary>
		/// <returns>A new <see cref="T:System.Data.DataSet" /> with the same schema as the current <see cref="T:System.Data.DataSet" />, but none of the data.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000698 RID: 1688 RVA: 0x000194D8 File Offset: 0x000176D8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual DataSet Clone()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Clone|API> {0}", this.ObjectID);
			DataSet dataSet2;
			try
			{
				DataSet dataSet = (DataSet)Activator.CreateInstance(base.GetType(), true);
				if (dataSet.Tables.Count > 0)
				{
					dataSet.Reset();
				}
				dataSet.DataSetName = this.DataSetName;
				dataSet.CaseSensitive = this.CaseSensitive;
				dataSet._culture = this._culture;
				dataSet._cultureUserSet = this._cultureUserSet;
				dataSet.EnforceConstraints = this.EnforceConstraints;
				dataSet.Namespace = this.Namespace;
				dataSet.Prefix = this.Prefix;
				dataSet.RemotingFormat = this.RemotingFormat;
				dataSet._fIsSchemaLoading = true;
				DataTableCollection tables = this.Tables;
				for (int i = 0; i < tables.Count; i++)
				{
					DataTable dataTable = tables[i].Clone(dataSet);
					dataTable._tableNamespace = tables[i].Namespace;
					dataSet.Tables.Add(dataTable);
				}
				for (int j = 0; j < tables.Count; j++)
				{
					ConstraintCollection constraints = tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (!(constraints[k] is UniqueConstraint))
						{
							ForeignKeyConstraint foreignKeyConstraint = constraints[k] as ForeignKeyConstraint;
							if (foreignKeyConstraint.Table != foreignKeyConstraint.RelatedTable)
							{
								dataSet.Tables[j].Constraints.Add(constraints[k].Clone(dataSet));
							}
						}
					}
				}
				DataRelationCollection relations = this.Relations;
				for (int l = 0; l < relations.Count; l++)
				{
					DataRelation dataRelation = relations[l].Clone(dataSet);
					dataRelation.CheckMultipleNested = false;
					dataSet.Relations.Add(dataRelation);
					dataRelation.CheckMultipleNested = true;
				}
				if (this._extendedProperties != null)
				{
					foreach (object obj in this._extendedProperties.Keys)
					{
						dataSet.ExtendedProperties[obj] = this._extendedProperties[obj];
					}
				}
				foreach (object obj2 in this.Tables)
				{
					DataTable dataTable2 = (DataTable)obj2;
					foreach (object obj3 in dataTable2.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj3;
						if (dataColumn.Expression.Length != 0)
						{
							dataSet.Tables[dataTable2.TableName, dataTable2.Namespace].Columns[dataColumn.ColumnName].Expression = dataColumn.Expression;
						}
					}
				}
				for (int m = 0; m < tables.Count; m++)
				{
					dataSet.Tables[m]._tableNamespace = tables[m]._tableNamespace;
				}
				dataSet._fIsSchemaLoading = false;
				dataSet2 = dataSet;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataSet2;
		}

		/// <summary>Copies both the structure and data for this <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataSet" /> with the same structure (table schemas, relations, and constraints) and data as this <see cref="T:System.Data.DataSet" />.NoteIf these classes have been subclassed, the copy will also be of the same subclasses.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000699 RID: 1689 RVA: 0x0001988C File Offset: 0x00017A8C
		public DataSet Copy()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Copy|API> {0}", this.ObjectID);
			DataSet dataSet2;
			try
			{
				DataSet dataSet = this.Clone();
				bool enforceConstraints = dataSet.EnforceConstraints;
				dataSet.EnforceConstraints = false;
				foreach (object obj in this.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					DataTable dataTable2 = dataSet.Tables[dataTable.TableName, dataTable.Namespace];
					foreach (object obj2 in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						dataTable.CopyRow(dataTable2, dataRow);
					}
				}
				dataSet.EnforceConstraints = enforceConstraints;
				dataSet2 = dataSet;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataSet2;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000199A4 File Offset: 0x00017BA4
		internal int EstimatedXmlStringSize()
		{
			int num = 100;
			for (int i = 0; i < this.Tables.Count; i++)
			{
				int num2 = this.Tables[i].TableName.Length + 4 << 2;
				DataTable dataTable = this.Tables[i];
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					num2 += dataTable.Columns[j].ColumnName.Length + 4 << 2;
					num2 += 20;
				}
				num += dataTable.Rows.Count * num2;
			}
			return num;
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataSet" /> that contains all changes made to it since it was loaded or since <see cref="M:System.Data.DataSet.AcceptChanges" /> was last called.</summary>
		/// <returns>A copy of the changes from this <see cref="T:System.Data.DataSet" /> that can have actions performed on it and later be merged back in using <see cref="M:System.Data.DataSet.Merge(System.Data.DataSet)" />. If no changed rows are found, the method returns null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600069B RID: 1691 RVA: 0x00019A43 File Offset: 0x00017C43
		public DataSet GetChanges()
		{
			return this.GetChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataSet" /> containing all changes made to it since it was last loaded, or since <see cref="M:System.Data.DataSet.AcceptChanges" /> was called, filtered by <see cref="T:System.Data.DataRowState" />.</summary>
		/// <returns>A filtered copy of the <see cref="T:System.Data.DataSet" /> that can have actions performed on it, and subsequently be merged back in using <see cref="M:System.Data.DataSet.Merge(System.Data.DataSet)" />. If no rows of the desired <see cref="T:System.Data.DataRowState" /> are found, the method returns null.</returns>
		/// <param name="rowStates">One of the <see cref="T:System.Data.DataRowState" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600069C RID: 1692 RVA: 0x00019A50 File Offset: 0x00017C50
		public DataSet GetChanges(DataRowState rowStates)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, DataRowState>("<ds.DataSet.GetChanges|API> {0}, rowStates={1}", this.ObjectID, rowStates);
			DataSet dataSet2;
			try
			{
				DataSet dataSet = null;
				bool flag = false;
				if ((rowStates & ~(DataRowState.Unchanged | DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					throw ExceptionBuilder.InvalidRowState(rowStates);
				}
				DataSet.TableChanges[] array = new DataSet.TableChanges[this.Tables.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new DataSet.TableChanges(this.Tables[i].Rows.Count);
				}
				this.MarkModifiedRows(array, rowStates);
				for (int j = 0; j < array.Length; j++)
				{
					if (0 < array[j].HasChanges)
					{
						if (dataSet == null)
						{
							dataSet = this.Clone();
							flag = dataSet.EnforceConstraints;
							dataSet.EnforceConstraints = false;
						}
						DataTable dataTable = this.Tables[j];
						DataTable dataTable2 = dataSet.Tables[dataTable.TableName, dataTable.Namespace];
						int num2 = 0;
						while (0 < array[j].HasChanges)
						{
							if (array[j][num2])
							{
								dataTable.CopyRow(dataTable2, dataTable.Rows[num2]);
								DataSet.TableChanges[] array2 = array;
								int num3 = j;
								int hasChanges = array2[num3].HasChanges;
								array2[num3].HasChanges = hasChanges - 1;
							}
							num2++;
						}
					}
				}
				if (dataSet != null)
				{
					dataSet.EnforceConstraints = flag;
				}
				dataSet2 = dataSet;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataSet2;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00019BD8 File Offset: 0x00017DD8
		private void MarkModifiedRows(DataSet.TableChanges[] bitMatrix, DataRowState rowStates)
		{
			for (int i = 0; i < bitMatrix.Length; i++)
			{
				DataRowCollection rows = this.Tables[i].Rows;
				int count = rows.Count;
				for (int j = 0; j < count; j++)
				{
					DataRow dataRow = rows[j];
					DataRowState rowState = dataRow.RowState;
					if ((rowStates & rowState) != (DataRowState)0 && !bitMatrix[i][j])
					{
						bitMatrix[i][j] = true;
						if (DataRowState.Deleted != rowState)
						{
							this.MarkRelatedRowsAsModified(bitMatrix, dataRow);
						}
					}
				}
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00019C5C File Offset: 0x00017E5C
		private void MarkRelatedRowsAsModified(DataSet.TableChanges[] bitMatrix, DataRow row)
		{
			DataRelationCollection parentRelations = row.Table.ParentRelations;
			int count = parentRelations.Count;
			for (int i = 0; i < count; i++)
			{
				foreach (DataRow dataRow in row.GetParentRows(parentRelations[i], DataRowVersion.Current))
				{
					int num = this.Tables.IndexOf(dataRow.Table);
					int num2 = dataRow.Table.Rows.IndexOf(dataRow);
					if (!bitMatrix[num][num2])
					{
						bitMatrix[num][num2] = true;
						if (DataRowState.Deleted != dataRow.RowState)
						{
							this.MarkRelatedRowsAsModified(bitMatrix, dataRow);
						}
					}
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</returns>
		// Token: 0x0600069F RID: 1695 RVA: 0x00019D16 File Offset: 0x00017F16
		IList IListSource.GetList()
		{
			return this.DefaultViewManager;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00019D20 File Offset: 0x00017F20
		internal string GetRemotingDiffGram(DataTable table)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			if (stringWriter != null)
			{
				new NewDiffgramGen(table, false).Save(xmlTextWriter, table);
			}
			return stringWriter.ToString();
		}

		/// <summary>Returns the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A string that is a representation of the data stored in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006A1 RID: 1697 RVA: 0x00019D5C File Offset: 0x00017F5C
		public string GetXml()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.GetXml|API> {0}", this.ObjectID);
			string text;
			try
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				if (stringWriter != null)
				{
					XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
					xmlTextWriter.Formatting = Formatting.Indented;
					new XmlDataTreeWriter(this).Save(xmlTextWriter, false);
				}
				text = stringWriter.ToString();
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return text;
		}

		/// <summary>Returns the XML Schema for the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>String that is the XML Schema for the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006A2 RID: 1698 RVA: 0x00019DD0 File Offset: 0x00017FD0
		public string GetXmlSchema()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.GetXmlSchema|API> {0}", this.ObjectID);
			string text;
			try
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.Formatting = Formatting.Indented;
				if (stringWriter != null)
				{
					new XmlTreeGen(SchemaFormat.Public).Save(this, xmlTextWriter);
				}
				text = stringWriter.ToString();
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return text;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00019E40 File Offset: 0x00018040
		internal string GetXmlSchemaForRemoting(DataTable table)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			if (stringWriter != null)
			{
				if (table == null)
				{
					if (this.SchemaSerializationMode == SchemaSerializationMode.ExcludeSchema)
					{
						new XmlTreeGen(SchemaFormat.RemotingSkipSchema).Save(this, xmlTextWriter);
					}
					else
					{
						new XmlTreeGen(SchemaFormat.Remoting).Save(this, xmlTextWriter);
					}
				}
				else
				{
					new XmlTreeGen(SchemaFormat.Remoting).Save(table, xmlTextWriter);
				}
			}
			return stringWriter.ToString();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.DataSet" /> has changes, including new, deleted, or modified rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataSet" /> has changes; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060006A4 RID: 1700 RVA: 0x00019EA4 File Offset: 0x000180A4
		public bool HasChanges()
		{
			return this.HasChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.DataSet" /> has changes, including new, deleted, or modified rows, filtered by <see cref="T:System.Data.DataRowState" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataSet" /> has changes; otherwise false.</returns>
		/// <param name="rowStates">One of the <see cref="T:System.Data.DataRowState" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060006A5 RID: 1701 RVA: 0x00019EB0 File Offset: 0x000180B0
		public bool HasChanges(DataRowState rowStates)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataSet.HasChanges|API> {0}, rowStates={1}", this.ObjectID, (int)rowStates);
			bool flag;
			try
			{
				if ((rowStates & ~(DataRowState.Detached | DataRowState.Unchanged | DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					throw ExceptionBuilder.ArgumentOutOfRange("rowState");
				}
				for (int i = 0; i < this.Tables.Count; i++)
				{
					DataTable dataTable = this.Tables[i];
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						if ((dataTable.Rows[j].RowState & rowStates) != (DataRowState)0)
						{
							return true;
						}
					}
				}
				flag = false;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return flag;
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.Xml.XmlReader" /> to the <see cref="T:System.Data.DataSet" />. </summary>
		/// <param name="reader">The XMLReader from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006A6 RID: 1702 RVA: 0x00019F5C File Offset: 0x0001815C
		public void InferXmlSchema(XmlReader reader, string[] nsArray)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.InferXmlSchema|API> {0}", this.ObjectID);
			try
			{
				if (reader != null)
				{
					XmlDocument xmlDocument = new XmlDocument();
					if (reader.NodeType == XmlNodeType.Element)
					{
						XmlNode xmlNode = xmlDocument.ReadNode(reader);
						xmlDocument.AppendChild(xmlNode);
					}
					else
					{
						xmlDocument.Load(reader);
					}
					if (xmlDocument.DocumentElement != null)
					{
						this.InferSchema(xmlDocument, nsArray, XmlReadMode.InferSchema);
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.IO.Stream" /> to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="stream">The Stream from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006A7 RID: 1703 RVA: 0x00019FE0 File Offset: 0x000181E0
		public void InferXmlSchema(Stream stream, string[] nsArray)
		{
			if (stream == null)
			{
				return;
			}
			this.InferXmlSchema(new XmlTextReader(stream), nsArray);
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.IO.TextReader" /> to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The TextReader from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006A8 RID: 1704 RVA: 0x00019FF3 File Offset: 0x000181F3
		public void InferXmlSchema(TextReader reader, string[] nsArray)
		{
			if (reader == null)
			{
				return;
			}
			this.InferXmlSchema(new XmlTextReader(reader), nsArray);
		}

		/// <summary>Applies the XML schema from the specified file to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="fileName">The name of the file (including the path) from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006A9 RID: 1705 RVA: 0x0001A008 File Offset: 0x00018208
		public void InferXmlSchema(string fileName, string[] nsArray)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			try
			{
				this.InferXmlSchema(xmlTextReader, nsArray);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.Xml.XmlReader" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006AA RID: 1706 RVA: 0x0001A040 File Offset: 0x00018240
		public void ReadXmlSchema(XmlReader reader)
		{
			this.ReadXmlSchema(reader, false);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001A04C File Offset: 0x0001824C
		internal void ReadXmlSchema(XmlReader reader, bool denyResolving)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.ReadXmlSchema|INFO> {0}, reader, denyResolving={1}", this.ObjectID, denyResolving);
			try
			{
				int num2 = -1;
				if (reader != null)
				{
					if (reader is XmlTextReader)
					{
						((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.None;
					}
					XmlDocument xmlDocument = new XmlDocument();
					if (reader.NodeType == XmlNodeType.Element)
					{
						num2 = reader.Depth;
					}
					reader.MoveToContent();
					if (reader.NodeType == XmlNodeType.Element)
					{
						if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
						{
							this.ReadXDRSchema(reader);
						}
						else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
						{
							this.ReadXSDSchema(reader, denyResolving);
						}
						else
						{
							if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
							{
								throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
							}
							XmlElement xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							if (reader.HasAttributes)
							{
								int attributeCount = reader.AttributeCount;
								for (int i = 0; i < attributeCount; i++)
								{
									reader.MoveToAttribute(i);
									if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
									{
										xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
									}
									else
									{
										XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
										xmlAttribute.Prefix = reader.Prefix;
										xmlAttribute.Value = reader.GetAttribute(i);
									}
								}
							}
							reader.Read();
							while (this.MoveToElement(reader, num2))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									this.ReadXDRSchema(reader);
									return;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									this.ReadXSDSchema(reader, denyResolving);
									return;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
									throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
								}
								XmlNode xmlNode = xmlDocument.ReadNode(reader);
								xmlElement.AppendChild(xmlNode);
							}
							this.ReadEndElement(reader);
							xmlDocument.AppendChild(xmlElement);
							this.InferSchema(xmlDocument, null, XmlReadMode.Auto);
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001A2D8 File Offset: 0x000184D8
		internal bool MoveToElement(XmlReader reader, int depth)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element && reader.Depth > depth)
			{
				reader.Read();
			}
			return reader.NodeType == XmlNodeType.Element;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001A310 File Offset: 0x00018510
		private static void MoveToElement(XmlReader reader)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element)
			{
				reader.Read();
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001A336 File Offset: 0x00018536
		internal void ReadEndElement(XmlReader reader)
		{
			while (reader.NodeType == XmlNodeType.Whitespace)
			{
				reader.Skip();
			}
			if (reader.NodeType == XmlNodeType.None)
			{
				reader.Skip();
				return;
			}
			if (reader.NodeType == XmlNodeType.EndElement)
			{
				reader.ReadEndElement();
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001A36C File Offset: 0x0001856C
		internal void ReadXSDSchema(XmlReader reader, bool denyResolving)
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			int num = 1;
			if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema" && reader.HasAttributes)
			{
				string attribute = reader.GetAttribute("schemafragmentcount", "urn:schemas-microsoft-com:xml-msdata");
				if (!string.IsNullOrEmpty(attribute))
				{
					num = int.Parse(attribute, null);
				}
			}
			while (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
			{
				XmlSchema xmlSchema = XmlSchema.Read(reader, null);
				xmlSchemaSet.Add(xmlSchema);
				this.ReadEndElement(reader);
				if (--num > 0)
				{
					DataSet.MoveToElement(reader);
				}
				while (reader.NodeType == XmlNodeType.Whitespace)
				{
					reader.Skip();
				}
			}
			xmlSchemaSet.Compile();
			new XSDSchema().LoadSchema(xmlSchemaSet, this);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001A440 File Offset: 0x00018640
		internal void ReadXDRSchema(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.ReadNode(reader);
			xmlDocument.AppendChild(xmlNode);
			XDRSchema xdrschema = new XDRSchema(this, false);
			this.DataSetName = xmlDocument.DocumentElement.LocalName;
			xdrschema.LoadSchema((XmlElement)xmlNode, this);
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.IO.Stream" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006B1 RID: 1713 RVA: 0x0001A487 File Offset: 0x00018687
		public void ReadXmlSchema(Stream stream)
		{
			if (stream == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(stream), false);
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.IO.TextReader" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006B2 RID: 1714 RVA: 0x0001A49A File Offset: 0x0001869A
		public void ReadXmlSchema(TextReader reader)
		{
			if (reader == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(reader), false);
		}

		/// <summary>Reads the XML schema from the specified file into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="fileName">The file name (including the path) from which to read. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006B3 RID: 1715 RVA: 0x0001A4B0 File Offset: 0x000186B0
		public void ReadXmlSchema(string fileName)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			try
			{
				this.ReadXmlSchema(xmlTextReader, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006B4 RID: 1716 RVA: 0x0001A4E8 File Offset: 0x000186E8
		public void WriteXmlSchema(Stream stream)
		{
			this.WriteXmlSchema(stream, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object to write to. </param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x060006B5 RID: 1717 RVA: 0x0001A4F3 File Offset: 0x000186F3
		public void WriteXmlSchema(Stream stream, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(stream, SchemaFormat.Public, multipleTargetConverter);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to a file.</summary>
		/// <param name="fileName">The file name (including the path) to which to write. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006B6 RID: 1718 RVA: 0x0001A509 File Offset: 0x00018709
		public void WriteXmlSchema(string fileName)
		{
			this.WriteXmlSchema(fileName, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to a file.</summary>
		/// <param name="fileName">The name of the file to write to. </param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x060006B7 RID: 1719 RVA: 0x0001A514 File Offset: 0x00018714
		public void WriteXmlSchema(string fileName, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(fileName, SchemaFormat.Public, multipleTargetConverter);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> object with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006B8 RID: 1720 RVA: 0x0001A52A File Offset: 0x0001872A
		public void WriteXmlSchema(TextWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object to write to. </param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x060006B9 RID: 1721 RVA: 0x0001A535 File Offset: 0x00018735
		public void WriteXmlSchema(TextWriter writer, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(writer, SchemaFormat.Public, multipleTargetConverter);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to an <see cref="T:System.Xml.XmlWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to write to. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006BA RID: 1722 RVA: 0x0001A54B File Offset: 0x0001874B
		public void WriteXmlSchema(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" /> object to write to. </param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x060006BB RID: 1723 RVA: 0x0001A556 File Offset: 0x00018756
		public void WriteXmlSchema(XmlWriter writer, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(writer, SchemaFormat.Public, multipleTargetConverter);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001A56C File Offset: 0x0001876C
		private void WriteXmlSchema(string fileName, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				this.WriteXmlSchema(xmlTextWriter, schemaFormat, multipleTargetConverter);
				xmlTextWriter.WriteEndDocument();
			}
			finally
			{
				xmlTextWriter.Close();
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001A5B8 File Offset: 0x000187B8
		private void WriteXmlSchema(Stream stream, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			if (stream == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			}, schemaFormat, multipleTargetConverter);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001A5E4 File Offset: 0x000187E4
		private void WriteXmlSchema(TextWriter writer, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			if (writer == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			}, schemaFormat, multipleTargetConverter);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001A60C File Offset: 0x0001880C
		private void WriteXmlSchema(XmlWriter writer, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, SchemaFormat>("<ds.DataSet.WriteXmlSchema|INFO> {0}, schemaFormat={1}", this.ObjectID, schemaFormat);
			try
			{
				if (writer != null)
				{
					XmlTreeGen xmlTreeGen;
					if (schemaFormat == SchemaFormat.WebService && this.SchemaSerializationMode == SchemaSerializationMode.ExcludeSchema && writer.WriteState == WriteState.Element)
					{
						xmlTreeGen = new XmlTreeGen(SchemaFormat.WebServiceSkipSchema);
					}
					else
					{
						xmlTreeGen = new XmlTreeGen(schemaFormat);
					}
					xmlTreeGen.Save(this, null, writer, false, multipleTargetConverter);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006C0 RID: 1728 RVA: 0x0001A684 File Offset: 0x00018884
		public XmlReadMode ReadXml(XmlReader reader)
		{
			return this.ReadXml(reader, false);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001A690 File Offset: 0x00018890
		internal XmlReadMode ReadXml(XmlReader reader, bool denyResolving)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.ReadXml|INFO> {0}, denyResolving={1}", this.ObjectID, denyResolving);
			XmlReadMode xmlReadMode2;
			try
			{
				DataTable.DSRowDiffIdUsageSection dsrowDiffIdUsageSection = default(DataTable.DSRowDiffIdUsageSection);
				try
				{
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					int num2 = -1;
					XmlReadMode xmlReadMode = XmlReadMode.Auto;
					bool flag5 = false;
					bool flag6 = false;
					dsrowDiffIdUsageSection.Prepare(this);
					if (reader == null)
					{
						xmlReadMode2 = xmlReadMode;
					}
					else
					{
						if (this.Tables.Count == 0)
						{
							flag5 = true;
						}
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						XmlDataLoader xmlDataLoader = null;
						reader.MoveToContent();
						if (reader.NodeType == XmlNodeType.Element)
						{
							num2 = reader.Depth;
						}
						if (reader.NodeType == XmlNodeType.Element)
						{
							if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
							{
								this.ReadXmlDiffgram(reader);
								this.ReadEndElement(reader);
								return XmlReadMode.DiffGram;
							}
							if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
							{
								this.ReadXDRSchema(reader);
								return XmlReadMode.ReadSchema;
							}
							if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
							{
								this.ReadXSDSchema(reader, denyResolving);
								return XmlReadMode.ReadSchema;
							}
							if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
							{
								throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
							}
							XmlElement xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							if (reader.HasAttributes)
							{
								int attributeCount = reader.AttributeCount;
								for (int i = 0; i < attributeCount; i++)
								{
									reader.MoveToAttribute(i);
									if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
									{
										xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
									}
									else
									{
										XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
										xmlAttribute.Prefix = reader.Prefix;
										xmlAttribute.Value = reader.GetAttribute(i);
									}
								}
							}
							reader.Read();
							string value = reader.Value;
							while (this.MoveToElement(reader, num2))
							{
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									this.ReadXmlDiffgram(reader);
									xmlReadMode = XmlReadMode.DiffGram;
								}
								if (!flag2 && !flag && reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									this.ReadXDRSchema(reader);
									flag2 = true;
									flag4 = true;
								}
								else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									this.ReadXSDSchema(reader, denyResolving);
									flag2 = true;
								}
								else
								{
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
									{
										this.ReadXmlDiffgram(reader);
										flag3 = true;
										xmlReadMode = XmlReadMode.DiffGram;
									}
									else
									{
										while (!reader.EOF && reader.NodeType == XmlNodeType.Whitespace)
										{
											reader.Read();
										}
										if (reader.NodeType == XmlNodeType.Element)
										{
											flag = true;
											if (!flag2 && this.Tables.Count == 0)
											{
												XmlNode xmlNode = xmlDocument.ReadNode(reader);
												xmlElement.AppendChild(xmlNode);
											}
											else
											{
												if (xmlDataLoader == null)
												{
													xmlDataLoader = new XmlDataLoader(this, flag4, xmlElement, false);
												}
												xmlDataLoader.LoadData(reader);
												flag6 = true;
												if (flag2)
												{
													xmlReadMode = XmlReadMode.ReadSchema;
												}
												else
												{
													xmlReadMode = XmlReadMode.IgnoreSchema;
												}
											}
										}
									}
								}
							}
							this.ReadEndElement(reader);
							bool flag7 = false;
							bool fTopLevelTable = this._fTopLevelTable;
							if (!flag2 && this.Tables.Count == 0 && !xmlElement.HasChildNodes)
							{
								this._fTopLevelTable = true;
								flag7 = true;
								if (value != null && value.Length > 0)
								{
									xmlElement.InnerText = value;
								}
							}
							if (!flag5 && value != null && value.Length > 0)
							{
								xmlElement.InnerText = value;
							}
							xmlDocument.AppendChild(xmlElement);
							if (xmlDataLoader == null)
							{
								xmlDataLoader = new XmlDataLoader(this, flag4, xmlElement, false);
							}
							if (!flag5 && !flag6)
							{
								XmlElement documentElement = xmlDocument.DocumentElement;
								if (documentElement.ChildNodes.Count == 0 || (documentElement.ChildNodes.Count == 1 && documentElement.FirstChild.GetType() == typeof(XmlText)))
								{
									bool fTopLevelTable2 = this._fTopLevelTable;
									if (this.DataSetName != documentElement.Name && this._namespaceURI != documentElement.NamespaceURI && this.Tables.Contains(documentElement.Name, (documentElement.NamespaceURI.Length == 0) ? null : documentElement.NamespaceURI, false, true))
									{
										this._fTopLevelTable = true;
									}
									try
									{
										xmlDataLoader.LoadData(xmlDocument);
									}
									finally
									{
										this._fTopLevelTable = fTopLevelTable2;
									}
								}
							}
							if (!flag3)
							{
								if (!flag2 && this.Tables.Count == 0)
								{
									this.InferSchema(xmlDocument, null, XmlReadMode.Auto);
									xmlReadMode = XmlReadMode.InferSchema;
									xmlDataLoader.FromInference = true;
									try
									{
										xmlDataLoader.LoadData(xmlDocument);
									}
									finally
									{
										xmlDataLoader.FromInference = false;
									}
								}
								if (flag7)
								{
									this._fTopLevelTable = fTopLevelTable;
								}
							}
						}
						xmlReadMode2 = xmlReadMode;
					}
				}
				finally
				{
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return xmlReadMode2;
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="stream">An object that derives from <see cref="T:System.IO.Stream" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006C2 RID: 1730 RVA: 0x0001AC48 File Offset: 0x00018E48
		public XmlReadMode ReadXml(Stream stream)
		{
			if (stream == null)
			{
				return XmlReadMode.Auto;
			}
			return this.ReadXml(new XmlTextReader(stream)
			{
				XmlResolver = null
			}, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="reader">The TextReader from which to read the schema and data. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006C3 RID: 1731 RVA: 0x0001AC70 File Offset: 0x00018E70
		public XmlReadMode ReadXml(TextReader reader)
		{
			if (reader == null)
			{
				return XmlReadMode.Auto;
			}
			return this.ReadXml(new XmlTextReader(reader)
			{
				XmlResolver = null
			}, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified file.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="fileName">The filename (including the path) from which to read. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006C4 RID: 1732 RVA: 0x0001AC98 File Offset: 0x00018E98
		public XmlReadMode ReadXml(string fileName)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			xmlTextReader.XmlResolver = null;
			XmlReadMode xmlReadMode;
			try
			{
				xmlReadMode = this.ReadXml(xmlTextReader, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return xmlReadMode;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		internal void InferSchema(XmlDocument xdoc, string[] excludedNamespaces, XmlReadMode mode)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, XmlReadMode>("<ds.DataSet.InferSchema|INFO> {0}, mode={1}", this.ObjectID, mode);
			try
			{
				string namespaceURI = xdoc.DocumentElement.NamespaceURI;
				if (excludedNamespaces == null)
				{
					excludedNamespaces = Array.Empty<string>();
				}
				XmlNodeReader xmlNodeReader = new XmlIgnoreNamespaceReader(xdoc, excludedNamespaces);
				XmlSchemaSet xmlSchemaSet = new XmlSchemaInference
				{
					Occurrence = XmlSchemaInference.InferenceOption.Relaxed,
					TypeInference = ((mode == XmlReadMode.InferTypedSchema) ? XmlSchemaInference.InferenceOption.Restricted : XmlSchemaInference.InferenceOption.Relaxed)
				}.InferSchema(xmlNodeReader);
				xmlSchemaSet.Compile();
				XSDSchema xsdschema = new XSDSchema();
				xsdschema.FromInference = true;
				try
				{
					xsdschema.LoadSchema(xmlSchemaSet, this);
				}
				finally
				{
					xsdschema.FromInference = false;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001AD88 File Offset: 0x00018F88
		private bool IsEmpty()
		{
			using (IEnumerator enumerator = this.Tables.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((DataTable)enumerator.Current).Rows.Count > 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		private void ReadXmlDiffgram(XmlReader reader)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ReadXmlDiffgram|INFO> {0}", this.ObjectID);
			try
			{
				int depth = reader.Depth;
				bool enforceConstraints = this.EnforceConstraints;
				this.EnforceConstraints = false;
				bool flag = this.IsEmpty();
				DataSet dataSet;
				if (flag)
				{
					dataSet = this;
				}
				else
				{
					dataSet = this.Clone();
					dataSet.EnforceConstraints = false;
				}
				foreach (object obj in dataSet.Tables)
				{
					((DataTable)obj).Rows._nullInList = 0;
				}
				reader.MoveToContent();
				if (!(reader.LocalName != "diffgram") || !(reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					reader.Read();
					if (reader.NodeType == XmlNodeType.Whitespace)
					{
						this.MoveToElement(reader, reader.Depth - 1);
					}
					dataSet._fInLoadDiffgram = true;
					if (reader.Depth > depth)
					{
						if (reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1" && reader.NamespaceURI != "urn:schemas-microsoft-com:xml-msdata")
						{
							XmlElement xmlElement = new XmlDocument().CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							reader.Read();
							if (reader.NodeType == XmlNodeType.Whitespace)
							{
								this.MoveToElement(reader, reader.Depth - 1);
							}
							if (reader.Depth - 1 > depth)
							{
								new XmlDataLoader(dataSet, false, xmlElement, false)
								{
									_isDiffgram = true
								}.LoadData(reader);
							}
							this.ReadEndElement(reader);
							if (reader.NodeType == XmlNodeType.Whitespace)
							{
								this.MoveToElement(reader, reader.Depth - 1);
							}
						}
						if ((reader.LocalName == "before" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1") || (reader.LocalName == "errors" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
						{
							new XMLDiffLoader().LoadDiffGram(dataSet, reader);
						}
						while (reader.Depth > depth)
						{
							reader.Read();
						}
						this.ReadEndElement(reader);
					}
					foreach (object obj2 in dataSet.Tables)
					{
						DataTable dataTable = (DataTable)obj2;
						if (dataTable.Rows._nullInList > 0)
						{
							throw ExceptionBuilder.RowInsertMissing(dataTable.TableName);
						}
					}
					dataSet._fInLoadDiffgram = false;
					foreach (object obj3 in dataSet.Tables)
					{
						DataTable dataTable2 = (DataTable)obj3;
						DataRelation[] nestedParentRelations = dataTable2.NestedParentRelations;
						DataRelation[] array = nestedParentRelations;
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].ParentTable == dataTable2)
							{
								foreach (object obj4 in dataTable2.Rows)
								{
									DataRow dataRow = (DataRow)obj4;
									foreach (DataRelation dataRelation in nestedParentRelations)
									{
										dataRow.CheckForLoops(dataRelation);
									}
								}
							}
						}
					}
					if (!flag)
					{
						this.Merge(dataSet);
						if (this._dataSetName == "NewDataSet")
						{
							this._dataSetName = dataSet._dataSetName;
						}
						dataSet.EnforceConstraints = enforceConstraints;
					}
					this.EnforceConstraints = enforceConstraints;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlReader" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006C8 RID: 1736 RVA: 0x0001B204 File Offset: 0x00019404
		public XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode)
		{
			return this.ReadXml(reader, mode, false);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001B210 File Offset: 0x00019410
		internal XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode, bool denyResolving)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, XmlReadMode, bool>("<ds.DataSet.ReadXml|INFO> {0}, mode={1}, denyResolving={2}", this.ObjectID, mode, denyResolving);
			XmlReadMode xmlReadMode2;
			try
			{
				XmlReadMode xmlReadMode = mode;
				if (reader == null)
				{
					xmlReadMode2 = xmlReadMode;
				}
				else if (mode == XmlReadMode.Auto)
				{
					xmlReadMode2 = this.ReadXml(reader);
				}
				else
				{
					DataTable.DSRowDiffIdUsageSection dsrowDiffIdUsageSection = default(DataTable.DSRowDiffIdUsageSection);
					try
					{
						bool flag = false;
						bool flag2 = false;
						bool flag3 = false;
						int num2 = -1;
						dsrowDiffIdUsageSection.Prepare(this);
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						if (mode != XmlReadMode.Fragment && reader.NodeType == XmlNodeType.Element)
						{
							num2 = reader.Depth;
						}
						reader.MoveToContent();
						XmlDataLoader xmlDataLoader = null;
						if (reader.NodeType == XmlNodeType.Element)
						{
							XmlElement xmlElement;
							if (mode == XmlReadMode.Fragment)
							{
								xmlDocument.AppendChild(xmlDocument.CreateElement("ds_sqlXmlWraPPeR"));
								xmlElement = xmlDocument.DocumentElement;
							}
							else
							{
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										this.ReadXmlDiffgram(reader);
										this.ReadEndElement(reader);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXDRSchema(reader);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXSDSchema(reader, denyResolving);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
									throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
								}
								xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
								if (reader.HasAttributes)
								{
									int attributeCount = reader.AttributeCount;
									for (int i = 0; i < attributeCount; i++)
									{
										reader.MoveToAttribute(i);
										if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
										{
											xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
										}
										else
										{
											XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
											xmlAttribute.Prefix = reader.Prefix;
											xmlAttribute.Value = reader.GetAttribute(i);
										}
									}
								}
								reader.Read();
							}
							while (this.MoveToElement(reader, num2))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (!flag && !flag2 && mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXDRSchema(reader);
										flag = true;
										flag3 = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXSDSchema(reader, denyResolving);
										flag = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										this.ReadXmlDiffgram(reader);
										xmlReadMode = XmlReadMode.DiffGram;
									}
									else
									{
										reader.Skip();
									}
								}
								else
								{
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (mode == XmlReadMode.DiffGram)
									{
										reader.Skip();
									}
									else
									{
										flag2 = true;
										if (mode == XmlReadMode.InferSchema || mode == XmlReadMode.InferTypedSchema)
										{
											XmlNode xmlNode = xmlDocument.ReadNode(reader);
											xmlElement.AppendChild(xmlNode);
										}
										else
										{
											if (xmlDataLoader == null)
											{
												xmlDataLoader = new XmlDataLoader(this, flag3, xmlElement, mode == XmlReadMode.IgnoreSchema);
											}
											xmlDataLoader.LoadData(reader);
										}
									}
								}
							}
							this.ReadEndElement(reader);
							xmlDocument.AppendChild(xmlElement);
							if (xmlDataLoader == null)
							{
								xmlDataLoader = new XmlDataLoader(this, flag3, mode == XmlReadMode.IgnoreSchema);
							}
							if (mode == XmlReadMode.DiffGram)
							{
								return xmlReadMode;
							}
							if (mode == XmlReadMode.InferSchema || mode == XmlReadMode.InferTypedSchema)
							{
								this.InferSchema(xmlDocument, null, mode);
								xmlReadMode = XmlReadMode.InferSchema;
								xmlDataLoader.FromInference = true;
								try
								{
									xmlDataLoader.LoadData(xmlDocument);
								}
								finally
								{
									xmlDataLoader.FromInference = false;
								}
							}
						}
						xmlReadMode2 = xmlReadMode;
					}
					finally
					{
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return xmlReadMode2;
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006CA RID: 1738 RVA: 0x0001B6AC File Offset: 0x000198AC
		public XmlReadMode ReadXml(Stream stream, XmlReadMode mode)
		{
			if (stream == null)
			{
				return XmlReadMode.Auto;
			}
			XmlTextReader xmlTextReader = ((mode == XmlReadMode.Fragment) ? new XmlTextReader(stream, XmlNodeType.Element, null) : new XmlTextReader(stream));
			xmlTextReader.XmlResolver = null;
			return this.ReadXml(xmlTextReader, mode, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextReader" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006CB RID: 1739 RVA: 0x0001B6E4 File Offset: 0x000198E4
		public XmlReadMode ReadXml(TextReader reader, XmlReadMode mode)
		{
			if (reader == null)
			{
				return XmlReadMode.Auto;
			}
			XmlTextReader xmlTextReader = ((mode == XmlReadMode.Fragment) ? new XmlTextReader(reader.ReadToEnd(), XmlNodeType.Element, null) : new XmlTextReader(reader));
			xmlTextReader.XmlResolver = null;
			return this.ReadXml(xmlTextReader, mode, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified file and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="fileName">The filename (including the path) from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006CC RID: 1740 RVA: 0x0001B720 File Offset: 0x00019920
		public XmlReadMode ReadXml(string fileName, XmlReadMode mode)
		{
			XmlTextReader xmlTextReader = null;
			if (mode == XmlReadMode.Fragment)
			{
				xmlTextReader = new XmlTextReader(new FileStream(fileName, FileMode.Open), XmlNodeType.Element, null);
			}
			else
			{
				xmlTextReader = new XmlTextReader(fileName);
			}
			xmlTextReader.XmlResolver = null;
			XmlReadMode xmlReadMode;
			try
			{
				xmlReadMode = this.ReadXml(xmlTextReader, mode, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return xmlReadMode;
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006CD RID: 1741 RVA: 0x0001B778 File Offset: 0x00019978
		public void WriteXml(Stream stream)
		{
			this.WriteXml(stream, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> object with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006CE RID: 1742 RVA: 0x0001B782 File Offset: 0x00019982
		public void WriteXml(TextWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006CF RID: 1743 RVA: 0x0001B78C File Offset: 0x0001998C
		public void WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> to the specified file.</summary>
		/// <param name="fileName">The file name (including the path) to which to write. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006D0 RID: 1744 RVA: 0x0001B796 File Offset: 0x00019996
		public void WriteXml(string fileName)
		{
			this.WriteXml(fileName, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006D1 RID: 1745 RVA: 0x0001B7A0 File Offset: 0x000199A0
		public void WriteXml(Stream stream, XmlWriteMode mode)
		{
			if (stream != null)
			{
				this.WriteXml(new XmlTextWriter(stream, null)
				{
					Formatting = Formatting.Indented
				}, mode);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object used to write the document. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006D2 RID: 1746 RVA: 0x0001B7C8 File Offset: 0x000199C8
		public void WriteXml(TextWriter writer, XmlWriteMode mode)
		{
			if (writer != null)
			{
				this.WriteXml(new XmlTextWriter(writer)
				{
					Formatting = Formatting.Indented
				}, mode);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006D3 RID: 1747 RVA: 0x0001B7F0 File Offset: 0x000199F0
		public void WriteXml(XmlWriter writer, XmlWriteMode mode)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, XmlWriteMode>("<ds.DataSet.WriteXml|API> {0}, mode={1}", this.ObjectID, mode);
			try
			{
				if (writer != null)
				{
					if (mode == XmlWriteMode.DiffGram)
					{
						new NewDiffgramGen(this).Save(writer);
					}
					else
					{
						new XmlDataTreeWriter(this).Save(writer, mode == XmlWriteMode.WriteSchema);
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> to the specified file using the specified <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="fileName">The file name (including the path) to which to write. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006D4 RID: 1748 RVA: 0x0001B858 File Offset: 0x00019A58
		public void WriteXml(string fileName, XmlWriteMode mode)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, string, int>("<ds.DataSet.WriteXml|API> {0}, fileName='{1}', mode={2}", this.ObjectID, fileName, (int)mode);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				if (xmlTextWriter != null)
				{
					if (mode == XmlWriteMode.DiffGram)
					{
						new NewDiffgramGen(this).Save(xmlTextWriter);
					}
					else
					{
						new XmlDataTreeWriter(this).Save(xmlTextWriter, mode == XmlWriteMode.WriteSchema);
					}
				}
				xmlTextWriter.WriteEndDocument();
			}
			finally
			{
				xmlTextWriter.Close();
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001B8E4 File Offset: 0x00019AE4
		internal DataRelationCollection GetParentRelations(DataTable table)
		{
			return table.ParentRelations;
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema into the current DataSet.</summary>
		/// <param name="dataSet">The DataSet whose data and schema will be merged. </param>
		/// <exception cref="T:System.Data.ConstraintException">One or more constraints cannot be enabled. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006D6 RID: 1750 RVA: 0x0001B8EC File Offset: 0x00019AEC
		public void Merge(DataSet dataSet)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataSet.Merge|API> {0}, dataSet={1}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0);
			try
			{
				this.Merge(dataSet, false, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema into the current DataSet, preserving or discarding any changes in this DataSet according to the given argument.</summary>
		/// <param name="dataSet">The DataSet whose data and schema will be merged. </param>
		/// <param name="preserveChanges">true to preserve changes in the current DataSet; otherwise false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006D7 RID: 1751 RVA: 0x0001B944 File Offset: 0x00019B44
		public void Merge(DataSet dataSet, bool preserveChanges)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, bool>("<ds.DataSet.Merge|API> {0}, dataSet={1}, preserveChanges={2}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0, preserveChanges);
			try
			{
				this.Merge(dataSet, preserveChanges, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema with the current DataSet, preserving or discarding changes in the current DataSet and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="dataSet">The DataSet whose data and schema will be merged. </param>
		/// <param name="preserveChanges">true to preserve changes in the current DataSet; otherwise false. </param>
		/// <param name="missingSchemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006D8 RID: 1752 RVA: 0x0001B99C File Offset: 0x00019B9C
		public void Merge(DataSet dataSet, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, bool, MissingSchemaAction>("<ds.DataSet.Merge|API> {0}, dataSet={1}, preserveChanges={2}, missingSchemaAction={3}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0, preserveChanges, missingSchemaAction);
			try
			{
				if (dataSet == null)
				{
					throw ExceptionBuilder.ArgumentNull("dataSet");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				new Merger(this, preserveChanges, missingSchemaAction).MergeDataSet(dataSet);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataTable" /> and its schema into the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> whose data and schema will be merged. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="table" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006D9 RID: 1753 RVA: 0x0001BA14 File Offset: 0x00019C14
		public void Merge(DataTable table)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataSet.Merge|API> {0}, table={1}", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.Merge(table, false, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataTable" /> and its schema into the current DataSet, preserving or discarding changes in the DataSet and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="table">The DataTable whose data and schema will be merged. </param>
		/// <param name="preserveChanges">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <param name="missingSchemaAction">true to preserve changes in the DataSet; otherwise false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006DA RID: 1754 RVA: 0x0001BA6C File Offset: 0x00019C6C
		public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, bool, MissingSchemaAction>("<ds.DataSet.Merge|API> {0}, table={1}, preserveChanges={2}, missingSchemaAction={3}", this.ObjectID, (table != null) ? table.ObjectID : 0, preserveChanges, missingSchemaAction);
			try
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				new Merger(this, preserveChanges, missingSchemaAction).MergeTable(table);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges an array of <see cref="T:System.Data.DataRow" /> objects into the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="rows">The array of DataRow objects to be merged into the DataSet. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006DB RID: 1755 RVA: 0x0001BAE4 File Offset: 0x00019CE4
		public void Merge(DataRow[] rows)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Merge|API> {0}, rows", this.ObjectID);
			try
			{
				this.Merge(rows, false, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges an array of <see cref="T:System.Data.DataRow" /> objects into the current <see cref="T:System.Data.DataSet" />, preserving or discarding changes in the DataSet and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="rows">The array of <see cref="T:System.Data.DataRow" /> objects to be merged into the DataSet. </param>
		/// <param name="preserveChanges">true to preserve changes in the DataSet; otherwise false. </param>
		/// <param name="missingSchemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006DC RID: 1756 RVA: 0x0001BB30 File Offset: 0x00019D30
		public void Merge(DataRow[] rows, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool, MissingSchemaAction>("<ds.DataSet.Merge|API> {0}, preserveChanges={1}, missingSchemaAction={2}", this.ObjectID, preserveChanges, missingSchemaAction);
			try
			{
				if (rows == null)
				{
					throw ExceptionBuilder.ArgumentNull("rows");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				new Merger(this, preserveChanges, missingSchemaAction).MergeRows(rows);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Raises the <see cref="M:System.Data.DataSet.OnPropertyChanging(System.ComponentModel.PropertyChangedEventArgs)" /> event.</summary>
		/// <param name="pcevent">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060006DD RID: 1757 RVA: 0x0001BB9C File Offset: 0x00019D9C
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			PropertyChangedEventHandler propertyChanging = this.PropertyChanging;
			if (propertyChanging == null)
			{
				return;
			}
			propertyChanging(this, pcevent);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001BBB0 File Offset: 0x00019DB0
		internal void OnMergeFailed(MergeFailedEventArgs mfevent)
		{
			if (this.MergeFailed != null)
			{
				this.MergeFailed(this, mfevent);
				return;
			}
			throw ExceptionBuilder.MergeFailed(mfevent.Conflict);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001BBD3 File Offset: 0x00019DD3
		internal void RaiseMergeFailed(DataTable table, string conflict, MissingSchemaAction missingSchemaAction)
		{
			if (MissingSchemaAction.Error == missingSchemaAction)
			{
				throw ExceptionBuilder.MergeFailed(conflict);
			}
			this.OnMergeFailed(new MergeFailedEventArgs(table, conflict));
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001BBED File Offset: 0x00019DED
		internal void OnDataRowCreated(DataRow row)
		{
			DataRowCreatedEventHandler dataRowCreated = this.DataRowCreated;
			if (dataRowCreated == null)
			{
				return;
			}
			dataRowCreated(this, row);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001BC01 File Offset: 0x00019E01
		internal void OnClearFunctionCalled(DataTable table)
		{
			DataSetClearEventhandler clearFunctionCalled = this.ClearFunctionCalled;
			if (clearFunctionCalled == null)
			{
				return;
			}
			clearFunctionCalled(this, table);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001BC15 File Offset: 0x00019E15
		private void OnInitialized()
		{
			EventHandler initialized = this.Initialized;
			if (initialized == null)
			{
				return;
			}
			initialized(this, EventArgs.Empty);
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataTable" /> is removed from a <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> being removed. </param>
		// Token: 0x060006E3 RID: 1763 RVA: 0x000094D4 File Offset: 0x000076D4
		protected internal virtual void OnRemoveTable(DataTable table)
		{
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001BC30 File Offset: 0x00019E30
		internal void OnRemovedTable(DataTable table)
		{
			DataViewManager defaultViewManager = this._defaultViewManager;
			if (defaultViewManager != null)
			{
				defaultViewManager.DataViewSettings.Remove(table);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataRelation" /> object is removed from a <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> being removed. </param>
		// Token: 0x060006E5 RID: 1765 RVA: 0x000094D4 File Offset: 0x000076D4
		protected virtual void OnRemoveRelation(DataRelation relation)
		{
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001BC53 File Offset: 0x00019E53
		internal void OnRemoveRelationHack(DataRelation relation)
		{
			this.OnRemoveRelation(relation);
		}

		/// <summary>Sends a notification that the specified <see cref="T:System.Data.DataSet" /> property is about to change.</summary>
		/// <param name="name">The name of the property that is about to change. </param>
		// Token: 0x060006E7 RID: 1767 RVA: 0x0001BC5C File Offset: 0x00019E5C
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001BC6A File Offset: 0x00019E6A
		internal DataTable[] TopLevelTables()
		{
			return this.TopLevelTables(false);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001BC74 File Offset: 0x00019E74
		internal DataTable[] TopLevelTables(bool forSchema)
		{
			List<DataTable> list = new List<DataTable>();
			if (forSchema)
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					DataTable dataTable = this.Tables[i];
					if (dataTable.NestedParentsCount > 1 || dataTable.SelfNested)
					{
						list.Add(dataTable);
					}
				}
			}
			for (int j = 0; j < this.Tables.Count; j++)
			{
				DataTable dataTable2 = this.Tables[j];
				if (dataTable2.NestedParentsCount == 0 && !list.Contains(dataTable2))
				{
					list.Add(dataTable2);
				}
			}
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return Array.Empty<DataTable>();
		}

		/// <summary>Rolls back all the changes made to the <see cref="T:System.Data.DataSet" /> since it was created, or since the last time <see cref="M:System.Data.DataSet.AcceptChanges" /> was called.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060006EA RID: 1770 RVA: 0x0001BD18 File Offset: 0x00019F18
		public virtual void RejectChanges()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.RejectChanges|API> {0}", this.ObjectID);
			try
			{
				bool enforceConstraints = this.EnforceConstraints;
				this.EnforceConstraints = false;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].RejectChanges();
				}
				this.EnforceConstraints = enforceConstraints;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Clears all tables and removes all relations, foreign constraints, and tables from the <see cref="T:System.Data.DataSet" />. Subclasses should override <see cref="M:System.Data.DataSet.Reset" /> to restore a <see cref="T:System.Data.DataSet" /> to its original state.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060006EB RID: 1771 RVA: 0x0001BD98 File Offset: 0x00019F98
		public virtual void Reset()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Reset|API> {0}", this.ObjectID);
			try
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					ConstraintCollection constraints = this.Tables[i].Constraints;
					int j = 0;
					while (j < constraints.Count)
					{
						if (constraints[j] is ForeignKeyConstraint)
						{
							constraints.Remove(constraints[j]);
						}
						else
						{
							j++;
						}
					}
				}
				this.Clear();
				this.Relations.Clear();
				this.Tables.Clear();
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001BE4C File Offset: 0x0001A04C
		internal bool ValidateCaseConstraint()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ValidateCaseConstraint|INFO> {0}", this.ObjectID);
			bool flag;
			try
			{
				for (int i = 0; i < this.Relations.Count; i++)
				{
					DataRelation dataRelation = this.Relations[i];
					if (dataRelation.ChildTable.CaseSensitive != dataRelation.ParentTable.CaseSensitive)
					{
						return false;
					}
				}
				for (int j = 0; j < this.Tables.Count; j++)
				{
					ConstraintCollection constraints = this.Tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (constraints[k] is ForeignKeyConstraint)
						{
							ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraints[k];
							if (foreignKeyConstraint.Table.CaseSensitive != foreignKeyConstraint.RelatedTable.CaseSensitive)
							{
								return false;
							}
						}
					}
				}
				flag = true;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return flag;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001BF5C File Offset: 0x0001A15C
		internal bool ValidateLocaleConstraint()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ValidateLocaleConstraint|INFO> {0}", this.ObjectID);
			bool flag;
			try
			{
				for (int i = 0; i < this.Relations.Count; i++)
				{
					DataRelation dataRelation = this.Relations[i];
					if (dataRelation.ChildTable.Locale.LCID != dataRelation.ParentTable.Locale.LCID)
					{
						return false;
					}
				}
				for (int j = 0; j < this.Tables.Count; j++)
				{
					ConstraintCollection constraints = this.Tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (constraints[k] is ForeignKeyConstraint)
						{
							ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraints[k];
							if (foreignKeyConstraint.Table.Locale.LCID != foreignKeyConstraint.RelatedTable.Locale.LCID)
							{
								return false;
							}
						}
					}
				}
				flag = true;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return flag;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001C084 File Offset: 0x0001A284
		internal DataTable FindTable(DataTable baseTable, PropertyDescriptor[] props, int propStart)
		{
			if (props.Length < propStart + 1)
			{
				return baseTable;
			}
			PropertyDescriptor propertyDescriptor = props[propStart];
			if (baseTable == null)
			{
				if (propertyDescriptor is DataTablePropertyDescriptor)
				{
					return this.FindTable(((DataTablePropertyDescriptor)propertyDescriptor).Table, props, propStart + 1);
				}
				return null;
			}
			else
			{
				if (propertyDescriptor is DataRelationPropertyDescriptor)
				{
					return this.FindTable(((DataRelationPropertyDescriptor)propertyDescriptor).Relation.ChildTable, props, propStart + 1);
				}
				return null;
			}
		}

		/// <summary>Ignores attributes and returns an empty DataSet.</summary>
		/// <param name="reader">The specified XML reader. </param>
		// Token: 0x060006EF RID: 1775 RVA: 0x0001C0E8 File Offset: 0x0001A2E8
		protected virtual void ReadXmlSerializable(XmlReader reader)
		{
			this._useDataSetSchemaOnly = false;
			this._udtIsWrapped = false;
			if (reader.HasAttributes)
			{
				if (reader.MoveToAttribute("xsi:nil") && string.Equals(reader.GetAttribute("xsi:nil"), "true", StringComparison.Ordinal))
				{
					this.MoveToElement(reader, 1);
					return;
				}
				if (reader.MoveToAttribute("msdata:UseDataSetSchemaOnly"))
				{
					string attribute = reader.GetAttribute("msdata:UseDataSetSchemaOnly");
					if (string.Equals(attribute, "true", StringComparison.Ordinal) || string.Equals(attribute, "1", StringComparison.Ordinal))
					{
						this._useDataSetSchemaOnly = true;
					}
					else if (!string.Equals(attribute, "false", StringComparison.Ordinal) && !string.Equals(attribute, "0", StringComparison.Ordinal))
					{
						throw ExceptionBuilder.InvalidAttributeValue("UseDataSetSchemaOnly", attribute);
					}
				}
				if (reader.MoveToAttribute("msdata:UDTColumnValueWrapped"))
				{
					string attribute2 = reader.GetAttribute("msdata:UDTColumnValueWrapped");
					if (string.Equals(attribute2, "true", StringComparison.Ordinal) || string.Equals(attribute2, "1", StringComparison.Ordinal))
					{
						this._udtIsWrapped = true;
					}
					else if (!string.Equals(attribute2, "false", StringComparison.Ordinal) && !string.Equals(attribute2, "0", StringComparison.Ordinal))
					{
						throw ExceptionBuilder.InvalidAttributeValue("UDTColumnValueWrapped", attribute2);
					}
				}
			}
			this.ReadXml(reader, XmlReadMode.DiffGram, true);
		}

		/// <summary>Returns a serializable <see cref="T:System.Xml.Schema.XMLSchema" /> instance.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XMLSchema" /> instance.</returns>
		// Token: 0x060006F0 RID: 1776 RVA: 0x00003DF6 File Offset: 0x00001FF6
		protected virtual XmlSchema GetSchemaSerializable()
		{
			return null;
		}

		/// <summary>Gets a copy of <see cref="T:System.Xml.Schema.XmlSchemaSet" /> for the DataSet.</summary>
		/// <returns>A copy of <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">The specified schema set. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006F1 RID: 1777 RVA: 0x0001C214 File Offset: 0x0001A414
		public static XmlSchemaComplexType GetDataSetSchema(XmlSchemaSet schemaSet)
		{
			if (DataSet.s_schemaTypeForWSDL == null)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				xmlSchemaSequence.MaxOccurs = decimal.MaxValue;
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				DataSet.s_schemaTypeForWSDL = xmlSchemaComplexType;
			}
			return DataSet.s_schemaTypeForWSDL;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00005AE9 File Offset: 0x00003CE9
		private static bool PublishLegacyWSDL()
		{
			return false;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</returns>
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
		XmlSchema IXmlSerializable.GetSchema()
		{
			if (base.GetType() == typeof(DataSet))
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = new XmlTextWriter(memoryStream, null);
			if (xmlWriter != null)
			{
				new XmlTreeGen(SchemaFormat.WebService).Save(this, xmlWriter);
			}
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" />.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x060006F4 RID: 1780 RVA: 0x0001C310 File Offset: 0x0001A510
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			bool flag = true;
			XmlTextReader xmlTextReader = null;
			IXmlTextParser xmlTextParser = reader as IXmlTextParser;
			if (xmlTextParser != null)
			{
				flag = xmlTextParser.Normalized;
				xmlTextParser.Normalized = false;
			}
			else
			{
				xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					flag = xmlTextReader.Normalization;
					xmlTextReader.Normalization = false;
				}
			}
			this.ReadXmlSerializable(reader);
			if (xmlTextParser != null)
			{
				xmlTextParser.Normalized = flag;
				return;
			}
			if (xmlTextReader != null)
			{
				xmlTextReader.Normalization = flag;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" />.</param>
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001C36F File Offset: 0x0001A56F
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.WebService, null);
			this.WriteXml(writer, XmlWriteMode.DiffGram);
		}

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of <see cref="T:System.Data.DataTable" /> instances to supply the schema and namespace information.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the <see cref="T:System.Data.DataSet" /> will be combined with incoming rows that share the same primary key. </param>
		/// <param name="errorHandler">A <see cref="T:System.Data.FillErrorEventHandler" /> delegate to call when an error occurs while loading data.</param>
		/// <param name="tables">An array of <see cref="T:System.Data.DataTable" /> instances, from which the <see cref="M:System.Data.DataSet.Load(System.Data.IDataReader,System.Data.LoadOption,System.Data.FillErrorEventHandler,System.Data.DataTable[])" /> method retrieves name and namespace information.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006F6 RID: 1782 RVA: 0x0001C384 File Offset: 0x0001A584
		public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler, params DataTable[] tables)
		{
			long num = DataCommonEventSource.Log.EnterScope<LoadOption>("<ds.DataSet.Load|API> reader, loadOption={0}", loadOption);
			try
			{
				foreach (DataTable dataTable in tables)
				{
					ADP.CheckArgumentNull(dataTable, "tables");
					if (dataTable.DataSet != this)
					{
						throw ExceptionBuilder.TableNotInTheDataSet(dataTable.TableName);
					}
				}
				LoadAdapter loadAdapter = new LoadAdapter();
				loadAdapter.FillLoadOption = loadOption;
				loadAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				if (errorHandler != null)
				{
					loadAdapter.FillError += errorHandler;
				}
				loadAdapter.FillFromReader(tables, reader, 0, 0);
				if (!reader.IsClosed && !reader.NextResult())
				{
					reader.Close();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of <see cref="T:System.Data.DataTable" /> instances to supply the schema and namespace information.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets. </param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the <see cref="T:System.Data.DataSet" /> will be combined with incoming rows that share the same primary key. </param>
		/// <param name="tables">An array of <see cref="T:System.Data.DataTable" /> instances, from which the <see cref="M:System.Data.DataSet.Load(System.Data.IDataReader,System.Data.LoadOption,System.Data.DataTable[])" /> method retrieves name and namespace information. Each of these tables must be a member of the <see cref="T:System.Data.DataTableCollection" /> contained by this <see cref="T:System.Data.DataSet" />.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006F7 RID: 1783 RVA: 0x0001C43C File Offset: 0x0001A63C
		public void Load(IDataReader reader, LoadOption loadOption, params DataTable[] tables)
		{
			this.Load(reader, loadOption, null, tables);
		}

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of strings to supply the names for the tables within the DataSet.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the DataSet will be combined with incoming rows that share the same primary key. </param>
		/// <param name="tables">An array of strings, from which the Load method retrieves table name information.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006F8 RID: 1784 RVA: 0x0001C448 File Offset: 0x0001A648
		public void Load(IDataReader reader, LoadOption loadOption, params string[] tables)
		{
			ADP.CheckArgumentNull(tables, "tables");
			DataTable[] array = new DataTable[tables.Length];
			for (int i = 0; i < tables.Length; i++)
			{
				DataTable dataTable = this.Tables[tables[i]];
				if (dataTable == null)
				{
					dataTable = new DataTable(tables[i]);
					this.Tables.Add(dataTable);
				}
				array[i] = dataTable;
			}
			this.Load(reader, loadOption, null, array);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTableReader" /> with one result set per <see cref="T:System.Data.DataTable" />, in the same sequence as the tables appear in the <see cref="P:System.Data.DataSet.Tables" /> collection.</summary>
		/// <returns>A <see cref="T:System.Data.DataTableReader" /> containing one or more result sets, corresponding to the <see cref="T:System.Data.DataTable" /> instances contained within the source <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x060006F9 RID: 1785 RVA: 0x0001C4AC File Offset: 0x0001A6AC
		public DataTableReader CreateDataReader()
		{
			if (this.Tables.Count == 0)
			{
				throw ExceptionBuilder.CannotCreateDataReaderOnEmptyDataSet();
			}
			DataTable[] array = new DataTable[this.Tables.Count];
			for (int i = 0; i < this.Tables.Count; i++)
			{
				array[i] = this.Tables[i];
			}
			return this.CreateDataReader(array);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTableReader" /> with one result set per <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTableReader" /> containing one or more result sets, corresponding to the <see cref="T:System.Data.DataTable" /> instances contained within the source <see cref="T:System.Data.DataSet" />. The returned result sets are in the order specified by the <paramref name="dataTables" /> parameter.</returns>
		/// <param name="dataTables">An array of DataTables providing the order of the result sets to be returned in the <see cref="T:System.Data.DataTableReader" />.</param>
		// Token: 0x060006FA RID: 1786 RVA: 0x0001C50C File Offset: 0x0001A70C
		public DataTableReader CreateDataReader(params DataTable[] dataTables)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.GetDataReader|API> {0}", this.ObjectID);
			DataTableReader dataTableReader;
			try
			{
				if (dataTables.Length == 0)
				{
					throw ExceptionBuilder.DataTableReaderArgumentIsEmpty();
				}
				for (int i = 0; i < dataTables.Length; i++)
				{
					if (dataTables[i] == null)
					{
						throw ExceptionBuilder.ArgumentContainsNullValue();
					}
				}
				dataTableReader = new DataTableReader(dataTables);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTableReader;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001C578 File Offset: 0x0001A778
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0001C580 File Offset: 0x0001A780
		internal string MainTableName
		{
			get
			{
				return this._mainTableName;
			}
			set
			{
				this._mainTableName = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001C589 File Offset: 0x0001A789
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x0400053F RID: 1343
		private const string KEY_XMLSCHEMA = "XmlSchema";

		// Token: 0x04000540 RID: 1344
		private const string KEY_XMLDIFFGRAM = "XmlDiffGram";

		// Token: 0x04000541 RID: 1345
		private DataViewManager _defaultViewManager;

		// Token: 0x04000542 RID: 1346
		private readonly DataTableCollection _tableCollection;

		// Token: 0x04000543 RID: 1347
		private readonly DataRelationCollection _relationCollection;

		// Token: 0x04000544 RID: 1348
		internal PropertyCollection _extendedProperties;

		// Token: 0x04000545 RID: 1349
		private string _dataSetName = "NewDataSet";

		// Token: 0x04000546 RID: 1350
		private string _datasetPrefix = string.Empty;

		// Token: 0x04000547 RID: 1351
		internal string _namespaceURI = string.Empty;

		// Token: 0x04000548 RID: 1352
		private bool _enforceConstraints = true;

		// Token: 0x04000549 RID: 1353
		private bool _caseSensitive;

		// Token: 0x0400054A RID: 1354
		private CultureInfo _culture;

		// Token: 0x0400054B RID: 1355
		private bool _cultureUserSet;

		// Token: 0x0400054C RID: 1356
		internal bool _fInReadXml;

		// Token: 0x0400054D RID: 1357
		internal bool _fInLoadDiffgram;

		// Token: 0x0400054E RID: 1358
		internal bool _fTopLevelTable;

		// Token: 0x0400054F RID: 1359
		internal bool _fInitInProgress;

		// Token: 0x04000550 RID: 1360
		internal bool _fEnableCascading = true;

		// Token: 0x04000551 RID: 1361
		internal bool _fIsSchemaLoading;

		// Token: 0x04000552 RID: 1362
		private bool _fBoundToDocument;

		// Token: 0x04000553 RID: 1363
		internal string _mainTableName = string.Empty;

		// Token: 0x04000554 RID: 1364
		private SerializationFormat _remotingFormat;

		// Token: 0x04000555 RID: 1365
		private object _defaultViewManagerLock = new object();

		// Token: 0x04000556 RID: 1366
		private static int s_objectTypeCount;

		// Token: 0x04000557 RID: 1367
		private readonly int _objectID = Interlocked.Increment(ref DataSet.s_objectTypeCount);

		// Token: 0x04000558 RID: 1368
		private static XmlSchemaComplexType s_schemaTypeForWSDL;

		// Token: 0x04000559 RID: 1369
		internal bool _useDataSetSchemaOnly;

		// Token: 0x0400055A RID: 1370
		internal bool _udtIsWrapped;

		// Token: 0x02000072 RID: 114
		private struct TableChanges
		{
			// Token: 0x060006FE RID: 1790 RVA: 0x0001C591 File Offset: 0x0001A791
			internal TableChanges(int rowCount)
			{
				this._rowChanges = new BitArray(rowCount);
				this.HasChanges = 0;
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001C5A6 File Offset: 0x0001A7A6
			// (set) Token: 0x06000700 RID: 1792 RVA: 0x0001C5AE File Offset: 0x0001A7AE
			internal int HasChanges { readonly get; set; }

			// Token: 0x17000145 RID: 325
			internal bool this[int index]
			{
				get
				{
					return this._rowChanges[index];
				}
				set
				{
					this._rowChanges[index] = value;
					int hasChanges = this.HasChanges;
					this.HasChanges = hasChanges + 1;
				}
			}

			// Token: 0x04000560 RID: 1376
			private BitArray _rowChanges;
		}
	}
}
