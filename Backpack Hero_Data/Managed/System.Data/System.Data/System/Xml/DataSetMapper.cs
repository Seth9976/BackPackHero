using System;
using System.Collections;
using System.Data;

namespace System.Xml
{
	// Token: 0x0200002A RID: 42
	internal sealed class DataSetMapper
	{
		// Token: 0x06000139 RID: 313 RVA: 0x000067DB File Offset: 0x000049DB
		internal DataSetMapper()
		{
			this._tableSchemaMap = new Hashtable();
			this._columnSchemaMap = new Hashtable();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000067FC File Offset: 0x000049FC
		internal void SetupMapping(XmlDataDocument xd, DataSet ds)
		{
			if (this.IsMapped())
			{
				this._tableSchemaMap = new Hashtable();
				this._columnSchemaMap = new Hashtable();
			}
			this._doc = xd;
			this._dataSet = ds;
			foreach (object obj in this._dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				this.AddTableSchema(dataTable);
				foreach (object obj2 in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj2;
					if (!DataSetMapper.IsNotMapped(dataColumn))
					{
						this.AddColumnSchema(dataColumn);
					}
				}
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000068DC File Offset: 0x00004ADC
		internal bool IsMapped()
		{
			return this._dataSet != null;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000068E8 File Offset: 0x00004AE8
		internal DataTable SearchMatchingTableSchema(string localName, string namespaceURI)
		{
			object identity = DataSetMapper.GetIdentity(localName, namespaceURI);
			return (DataTable)this._tableSchemaMap[identity];
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006910 File Offset: 0x00004B10
		internal DataTable SearchMatchingTableSchema(XmlBoundElement rowElem, XmlBoundElement elem)
		{
			DataTable dataTable = this.SearchMatchingTableSchema(elem.LocalName, elem.NamespaceURI);
			if (dataTable == null)
			{
				return null;
			}
			if (rowElem == null)
			{
				return dataTable;
			}
			if (this.GetColumnSchemaForNode(rowElem, elem) == null)
			{
				return dataTable;
			}
			using (IEnumerator enumerator = elem.Attributes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((XmlAttribute)enumerator.Current).NamespaceURI != "http://www.w3.org/2000/xmlns/")
					{
						return dataTable;
					}
				}
			}
			for (XmlNode xmlNode = elem.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000069C0 File Offset: 0x00004BC0
		internal DataColumn GetColumnSchemaForNode(XmlBoundElement rowElem, XmlNode node)
		{
			object identity = DataSetMapper.GetIdentity(rowElem.LocalName, rowElem.NamespaceURI);
			object identity2 = DataSetMapper.GetIdentity(node.LocalName, node.NamespaceURI);
			Hashtable hashtable = (Hashtable)this._columnSchemaMap[identity];
			if (hashtable == null)
			{
				return null;
			}
			DataColumn dataColumn = (DataColumn)hashtable[identity2];
			if (dataColumn == null)
			{
				return null;
			}
			MappingType columnMapping = dataColumn.ColumnMapping;
			if (node.NodeType == XmlNodeType.Attribute && columnMapping == MappingType.Attribute)
			{
				return dataColumn;
			}
			if (node.NodeType == XmlNodeType.Element && columnMapping == MappingType.Element)
			{
				return dataColumn;
			}
			return null;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006A44 File Offset: 0x00004C44
		internal DataTable GetTableSchemaForElement(XmlElement elem)
		{
			XmlBoundElement xmlBoundElement = elem as XmlBoundElement;
			if (xmlBoundElement == null)
			{
				return null;
			}
			return this.GetTableSchemaForElement(xmlBoundElement);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006A64 File Offset: 0x00004C64
		internal DataTable GetTableSchemaForElement(XmlBoundElement be)
		{
			DataRow row = be.Row;
			if (row == null)
			{
				return null;
			}
			return row.Table;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006A77 File Offset: 0x00004C77
		internal static bool IsNotMapped(DataColumn c)
		{
			return c.ColumnMapping == MappingType.Hidden;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006A82 File Offset: 0x00004C82
		internal DataRow GetRowFromElement(XmlElement e)
		{
			XmlBoundElement xmlBoundElement = e as XmlBoundElement;
			if (xmlBoundElement == null)
			{
				return null;
			}
			return xmlBoundElement.Row;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006A95 File Offset: 0x00004C95
		internal DataRow GetRowFromElement(XmlBoundElement be)
		{
			return be.Row;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006AA0 File Offset: 0x00004CA0
		internal bool GetRegion(XmlNode node, out XmlBoundElement rowElem)
		{
			while (node != null)
			{
				XmlBoundElement xmlBoundElement = node as XmlBoundElement;
				if (xmlBoundElement != null && this.GetRowFromElement(xmlBoundElement) != null)
				{
					rowElem = xmlBoundElement;
					return true;
				}
				if (node.NodeType == XmlNodeType.Attribute)
				{
					node = ((XmlAttribute)node).OwnerElement;
				}
				else
				{
					node = node.ParentNode;
				}
			}
			rowElem = null;
			return false;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006AF0 File Offset: 0x00004CF0
		internal bool IsRegionRadical(XmlBoundElement rowElem)
		{
			if (rowElem.ElementState == ElementState.Defoliated)
			{
				return true;
			}
			DataColumnCollection columns = this.GetTableSchemaForElement(rowElem).Columns;
			int num = 0;
			int count = rowElem.Attributes.Count;
			for (int i = 0; i < count; i++)
			{
				XmlAttribute xmlAttribute = rowElem.Attributes[i];
				if (!xmlAttribute.Specified)
				{
					return false;
				}
				DataColumn columnSchemaForNode = this.GetColumnSchemaForNode(rowElem, xmlAttribute);
				if (columnSchemaForNode == null)
				{
					return false;
				}
				if (!this.IsNextColumn(columns, ref num, columnSchemaForNode))
				{
					return false;
				}
				XmlNode firstChild = xmlAttribute.FirstChild;
				if (firstChild == null || firstChild.NodeType != XmlNodeType.Text || firstChild.NextSibling != null)
				{
					return false;
				}
			}
			num = 0;
			for (XmlNode xmlNode = rowElem.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType != XmlNodeType.Element)
				{
					return false;
				}
				XmlElement xmlElement = xmlNode as XmlElement;
				if (this.GetRowFromElement(xmlElement) != null)
				{
					IL_0135:
					while (xmlNode != null)
					{
						if (xmlNode.NodeType != XmlNodeType.Element)
						{
							return false;
						}
						if (this.GetRowFromElement((XmlElement)xmlNode) == null)
						{
							return false;
						}
						xmlNode = xmlNode.NextSibling;
					}
					return true;
				}
				DataColumn columnSchemaForNode2 = this.GetColumnSchemaForNode(rowElem, xmlElement);
				if (columnSchemaForNode2 == null)
				{
					return false;
				}
				if (!this.IsNextColumn(columns, ref num, columnSchemaForNode2))
				{
					return false;
				}
				if (xmlElement.HasAttributes)
				{
					return false;
				}
				XmlNode firstChild2 = xmlElement.FirstChild;
				if (firstChild2 == null || firstChild2.NodeType != XmlNodeType.Text || firstChild2.NextSibling != null)
				{
					return false;
				}
			}
			goto IL_0135;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006C38 File Offset: 0x00004E38
		private void AddTableSchema(DataTable table)
		{
			object identity = DataSetMapper.GetIdentity(table.EncodedTableName, table.Namespace);
			this._tableSchemaMap[identity] = table;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006C64 File Offset: 0x00004E64
		private void AddColumnSchema(DataColumn col)
		{
			DataTable table = col.Table;
			object identity = DataSetMapper.GetIdentity(table.EncodedTableName, table.Namespace);
			object identity2 = DataSetMapper.GetIdentity(col.EncodedColumnName, col.Namespace);
			Hashtable hashtable = (Hashtable)this._columnSchemaMap[identity];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this._columnSchemaMap[identity] = hashtable;
			}
			hashtable[identity2] = col;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006CCC File Offset: 0x00004ECC
		private static object GetIdentity(string localName, string namespaceURI)
		{
			return localName + ":" + namespaceURI;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006CDA File Offset: 0x00004EDA
		private bool IsNextColumn(DataColumnCollection columns, ref int iColumn, DataColumn col)
		{
			while (iColumn < columns.Count)
			{
				if (columns[iColumn] == col)
				{
					iColumn++;
					return true;
				}
				iColumn++;
			}
			return false;
		}

		// Token: 0x04000433 RID: 1075
		private Hashtable _tableSchemaMap;

		// Token: 0x04000434 RID: 1076
		private Hashtable _columnSchemaMap;

		// Token: 0x04000435 RID: 1077
		private XmlDataDocument _doc;

		// Token: 0x04000436 RID: 1078
		private DataSet _dataSet;

		// Token: 0x04000437 RID: 1079
		internal const string strReservedXmlns = "http://www.w3.org/2000/xmlns/";
	}
}
