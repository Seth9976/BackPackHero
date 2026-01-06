using System;
using System.Data;
using System.Diagnostics;

namespace System.Xml
{
	// Token: 0x02000029 RID: 41
	internal sealed class DataPointer : IXmlDataVirtualNode
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00005B75 File Offset: 0x00003D75
		internal DataPointer(XmlDataDocument doc, XmlNode node)
		{
			this._doc = doc;
			this._node = node;
			this._column = null;
			this._fOnValue = false;
			this._bNeedFoliate = false;
			this._isInUse = true;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005BA8 File Offset: 0x00003DA8
		internal DataPointer(DataPointer pointer)
		{
			this._doc = pointer._doc;
			this._node = pointer._node;
			this._column = pointer._column;
			this._fOnValue = pointer._fOnValue;
			this._bNeedFoliate = false;
			this._isInUse = true;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005BF9 File Offset: 0x00003DF9
		internal void AddPointer()
		{
			this._doc.AddPointer(this);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005C08 File Offset: 0x00003E08
		private XmlBoundElement GetRowElement()
		{
			XmlBoundElement xmlBoundElement;
			if (this._column != null)
			{
				xmlBoundElement = this._node as XmlBoundElement;
				return xmlBoundElement;
			}
			this._doc.Mapper.GetRegion(this._node, out xmlBoundElement);
			return xmlBoundElement;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005C48 File Offset: 0x00003E48
		private DataRow Row
		{
			get
			{
				XmlBoundElement rowElement = this.GetRowElement();
				if (rowElement == null)
				{
					return null;
				}
				return rowElement.Row;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005C67 File Offset: 0x00003E67
		private static bool IsFoliated(XmlNode node)
		{
			return node == null || !(node is XmlBoundElement) || ((XmlBoundElement)node).IsFoliated;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005C81 File Offset: 0x00003E81
		internal void MoveTo(DataPointer pointer)
		{
			this._doc = pointer._doc;
			this._node = pointer._node;
			this._column = pointer._column;
			this._fOnValue = pointer._fOnValue;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005CB3 File Offset: 0x00003EB3
		private void MoveTo(XmlNode node)
		{
			this._node = node;
			this._column = null;
			this._fOnValue = false;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005CCA File Offset: 0x00003ECA
		private void MoveTo(XmlNode node, DataColumn column, bool fOnValue)
		{
			this._node = node;
			this._column = column;
			this._fOnValue = fOnValue;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005CE4 File Offset: 0x00003EE4
		private DataColumn NextColumn(DataRow row, DataColumn col, bool fAttribute, bool fNulls)
		{
			if (row.RowState == DataRowState.Deleted)
			{
				return null;
			}
			DataColumnCollection columns = row.Table.Columns;
			int i = ((col != null) ? (col.Ordinal + 1) : 0);
			int count = columns.Count;
			DataRowVersion dataRowVersion = ((row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current);
			while (i < count)
			{
				DataColumn dataColumn = columns[i];
				if (!this._doc.IsNotMapped(dataColumn) && dataColumn.ColumnMapping == MappingType.Attribute == fAttribute && (fNulls || !Convert.IsDBNull(row[dataColumn, dataRowVersion])))
				{
					return dataColumn;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005D7C File Offset: 0x00003F7C
		private DataColumn NthColumn(DataRow row, bool fAttribute, int iColumn, bool fNulls)
		{
			DataColumn dataColumn = null;
			checked
			{
				while ((dataColumn = this.NextColumn(row, dataColumn, fAttribute, fNulls)) != null)
				{
					if (iColumn == 0)
					{
						return dataColumn;
					}
					iColumn--;
				}
				return null;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005DA8 File Offset: 0x00003FA8
		private int ColumnCount(DataRow row, bool fAttribute, bool fNulls)
		{
			DataColumn dataColumn = null;
			int num = 0;
			while ((dataColumn = this.NextColumn(row, dataColumn, fAttribute, fNulls)) != null)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005DD0 File Offset: 0x00003FD0
		internal bool MoveToFirstChild()
		{
			this.RealFoliate();
			if (this._node == null)
			{
				return false;
			}
			if (this._column != null)
			{
				if (this._fOnValue)
				{
					return false;
				}
				this._fOnValue = true;
				return true;
			}
			else
			{
				if (!DataPointer.IsFoliated(this._node))
				{
					DataColumn dataColumn = this.NextColumn(this.Row, null, false, false);
					if (dataColumn != null)
					{
						this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
						return true;
					}
				}
				XmlNode xmlNode = this._doc.SafeFirstChild(this._node);
				if (xmlNode != null)
				{
					this.MoveTo(xmlNode);
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005E64 File Offset: 0x00004064
		internal bool MoveToNextSibling()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue && !this._doc.IsTextOnly(this._column))
					{
						return false;
					}
					DataColumn dataColumn = this.NextColumn(this.Row, this._column, false, false);
					if (dataColumn != null)
					{
						this.MoveTo(this._node, dataColumn, false);
						return true;
					}
					XmlNode xmlNode = this._doc.SafeFirstChild(this._node);
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode);
						return true;
					}
				}
				else
				{
					XmlNode xmlNode2 = this._doc.SafeNextSibling(this._node);
					if (xmlNode2 != null)
					{
						this.MoveTo(xmlNode2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005F0C File Offset: 0x0000410C
		internal bool MoveToParent()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue && !this._doc.IsTextOnly(this._column))
					{
						this.MoveTo(this._node, this._column, false);
						return true;
					}
					if (this._column.ColumnMapping != MappingType.Attribute)
					{
						this.MoveTo(this._node, null, false);
						return true;
					}
				}
				else
				{
					XmlNode parentNode = this._node.ParentNode;
					if (parentNode != null)
					{
						this.MoveTo(parentNode);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005F98 File Offset: 0x00004198
		internal bool MoveToOwnerElement()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue || this._doc.IsTextOnly(this._column) || this._column.ColumnMapping != MappingType.Attribute)
					{
						return false;
					}
					this.MoveTo(this._node, null, false);
					return true;
				}
				else if (this._node.NodeType == XmlNodeType.Attribute)
				{
					XmlNode ownerElement = ((XmlAttribute)this._node).OwnerElement;
					if (ownerElement != null)
					{
						this.MoveTo(ownerElement, null, false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006024 File Offset: 0x00004224
		internal int AttributeCount
		{
			get
			{
				this.RealFoliate();
				if (this._node == null || this._column != null || this._node.NodeType != XmlNodeType.Element)
				{
					return 0;
				}
				if (!DataPointer.IsFoliated(this._node))
				{
					return this.ColumnCount(this.Row, true, false);
				}
				return this._node.Attributes.Count;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006084 File Offset: 0x00004284
		internal bool MoveToAttribute(int i)
		{
			this.RealFoliate();
			if (i < 0)
			{
				return false;
			}
			if (this._node != null && (this._column == null || this._column.ColumnMapping == MappingType.Attribute) && this._node.NodeType == XmlNodeType.Element)
			{
				if (!DataPointer.IsFoliated(this._node))
				{
					DataColumn dataColumn = this.NthColumn(this.Row, true, i, false);
					if (dataColumn != null)
					{
						this.MoveTo(this._node, dataColumn, false);
						return true;
					}
				}
				else
				{
					XmlNode xmlNode = this._node.Attributes.Item(i);
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode, null, false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000611C File Offset: 0x0000431C
		internal XmlNodeType NodeType
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return XmlNodeType.None;
				}
				if (this._column == null)
				{
					return this._node.NodeType;
				}
				if (this._fOnValue)
				{
					return XmlNodeType.Text;
				}
				if (this._column.ColumnMapping == MappingType.Attribute)
				{
					return XmlNodeType.Attribute;
				}
				return XmlNodeType.Element;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006168 File Offset: 0x00004368
		internal string LocalName
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					string localName = this._node.LocalName;
					if (this.IsLocalNameEmpty(this._node.NodeType))
					{
						return string.Empty;
					}
					return localName;
				}
				else
				{
					if (this._fOnValue)
					{
						return string.Empty;
					}
					return this._doc.NameTable.Add(this._column.EncodedColumnName);
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000061E4 File Offset: 0x000043E4
		internal string NamespaceURI
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					return this._node.NamespaceURI;
				}
				if (this._fOnValue)
				{
					return string.Empty;
				}
				return this._doc.NameTable.Add(this._column.Namespace);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00006244 File Offset: 0x00004444
		internal string Name
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					string name = this._node.Name;
					if (this.IsLocalNameEmpty(this._node.NodeType))
					{
						return string.Empty;
					}
					return name;
				}
				else
				{
					string prefix = this.Prefix;
					string localName = this.LocalName;
					if (prefix == null || prefix.Length <= 0)
					{
						return localName;
					}
					if (localName != null && localName.Length > 0)
					{
						return this._doc.NameTable.Add(prefix + ":" + localName);
					}
					return prefix;
				}
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000062DC File Offset: 0x000044DC
		private bool IsLocalNameEmpty(XmlNodeType nt)
		{
			switch (nt)
			{
			case XmlNodeType.None:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			case XmlNodeType.EndElement:
			case XmlNodeType.EndEntity:
				return true;
			case XmlNodeType.Element:
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.DocumentType:
			case XmlNodeType.Notation:
			case XmlNodeType.XmlDeclaration:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000633E File Offset: 0x0000453E
		internal string Prefix
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					return this._node.Prefix;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006370 File Offset: 0x00004570
		internal string Value
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return null;
				}
				if (this._column == null)
				{
					return this._node.Value;
				}
				if (this._column.ColumnMapping != MappingType.Attribute && !this._fOnValue)
				{
					return null;
				}
				DataRow row = this.Row;
				DataRowVersion dataRowVersion = ((row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current);
				object obj = row[this._column, dataRowVersion];
				if (!Convert.IsDBNull(obj))
				{
					return this._column.ConvertObjectToXml(obj);
				}
				return null;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000063F8 File Offset: 0x000045F8
		bool IXmlDataVirtualNode.IsOnNode(XmlNode nodeToCheck)
		{
			this.RealFoliate();
			return nodeToCheck == this._node;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006409 File Offset: 0x00004609
		bool IXmlDataVirtualNode.IsOnColumn(DataColumn col)
		{
			this.RealFoliate();
			return col == this._column;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000641A File Offset: 0x0000461A
		internal XmlNode GetNode()
		{
			return this._node;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006422 File Offset: 0x00004622
		internal bool IsEmptyElement
		{
			get
			{
				this.RealFoliate();
				return this._node != null && this._column == null && this._node.NodeType == XmlNodeType.Element && ((XmlElement)this._node).IsEmpty;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000645A File Offset: 0x0000465A
		internal bool IsDefault
		{
			get
			{
				this.RealFoliate();
				return this._node != null && this._column == null && this._node.NodeType == XmlNodeType.Attribute && !((XmlAttribute)this._node).Specified;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006495 File Offset: 0x00004695
		void IXmlDataVirtualNode.OnFoliated(XmlNode foliatedNode)
		{
			if (this._node == foliatedNode)
			{
				if (this._column == null)
				{
					return;
				}
				this._bNeedFoliate = true;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000064B0 File Offset: 0x000046B0
		internal void RealFoliate()
		{
			if (!this._bNeedFoliate)
			{
				return;
			}
			XmlNode xmlNode;
			if (this._doc.IsTextOnly(this._column))
			{
				xmlNode = this._node.FirstChild;
			}
			else
			{
				if (this._column.ColumnMapping == MappingType.Attribute)
				{
					xmlNode = this._node.Attributes.GetNamedItem(this._column.EncodedColumnName, this._column.Namespace);
				}
				else
				{
					xmlNode = this._node.FirstChild;
					while (xmlNode != null && (!(xmlNode.LocalName == this._column.EncodedColumnName) || !(xmlNode.NamespaceURI == this._column.Namespace)))
					{
						xmlNode = xmlNode.NextSibling;
					}
				}
				if (xmlNode != null && this._fOnValue)
				{
					xmlNode = xmlNode.FirstChild;
				}
			}
			if (xmlNode == null)
			{
				throw new InvalidOperationException("Invalid foliation.");
			}
			this._node = xmlNode;
			this._column = null;
			this._fOnValue = false;
			this._bNeedFoliate = false;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000065A8 File Offset: 0x000047A8
		internal string PublicId
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Entity)
				{
					return ((XmlEntity)this._node).PublicId;
				}
				if (nodeType == XmlNodeType.DocumentType)
				{
					return ((XmlDocumentType)this._node).PublicId;
				}
				if (nodeType != XmlNodeType.Notation)
				{
					return null;
				}
				return ((XmlNotation)this._node).PublicId;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006600 File Offset: 0x00004800
		internal string SystemId
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Entity)
				{
					return ((XmlEntity)this._node).SystemId;
				}
				if (nodeType == XmlNodeType.DocumentType)
				{
					return ((XmlDocumentType)this._node).SystemId;
				}
				if (nodeType != XmlNodeType.Notation)
				{
					return null;
				}
				return ((XmlNotation)this._node).SystemId;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006658 File Offset: 0x00004858
		internal string InternalSubset
		{
			get
			{
				if (this.NodeType == XmlNodeType.DocumentType)
				{
					return ((XmlDocumentType)this._node).InternalSubset;
				}
				return null;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006678 File Offset: 0x00004878
		internal XmlDeclaration Declaration
		{
			get
			{
				XmlNode xmlNode = this._doc.SafeFirstChild(this._doc);
				if (xmlNode != null && xmlNode.NodeType == XmlNodeType.XmlDeclaration)
				{
					return (XmlDeclaration)xmlNode;
				}
				return null;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000066AC File Offset: 0x000048AC
		internal string Encoding
		{
			get
			{
				if (this.NodeType == XmlNodeType.XmlDeclaration)
				{
					return ((XmlDeclaration)this._node).Encoding;
				}
				if (this.NodeType == XmlNodeType.Document)
				{
					XmlDeclaration declaration = this.Declaration;
					if (declaration != null)
					{
						return declaration.Encoding;
					}
				}
				return null;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000066F0 File Offset: 0x000048F0
		internal string Standalone
		{
			get
			{
				if (this.NodeType == XmlNodeType.XmlDeclaration)
				{
					return ((XmlDeclaration)this._node).Standalone;
				}
				if (this.NodeType == XmlNodeType.Document)
				{
					XmlDeclaration declaration = this.Declaration;
					if (declaration != null)
					{
						return declaration.Standalone;
					}
				}
				return null;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006734 File Offset: 0x00004934
		internal string Version
		{
			get
			{
				if (this.NodeType == XmlNodeType.XmlDeclaration)
				{
					return ((XmlDeclaration)this._node).Version;
				}
				if (this.NodeType == XmlNodeType.Document)
				{
					XmlDeclaration declaration = this.Declaration;
					if (declaration != null)
					{
						return declaration.Version;
					}
				}
				return null;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006778 File Offset: 0x00004978
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this._column != null)
			{
				XmlBoundElement xmlBoundElement = this._node as XmlBoundElement;
				DataRow row = xmlBoundElement.Row;
				ElementState elementState = xmlBoundElement.ElementState;
				DataRowState rowState = row.RowState;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000067AE File Offset: 0x000049AE
		bool IXmlDataVirtualNode.IsInUse()
		{
			return this._isInUse;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000067B6 File Offset: 0x000049B6
		internal void SetNoLongerUse()
		{
			this._node = null;
			this._column = null;
			this._fOnValue = false;
			this._bNeedFoliate = false;
			this._isInUse = false;
		}

		// Token: 0x0400042D RID: 1069
		private XmlDataDocument _doc;

		// Token: 0x0400042E RID: 1070
		private XmlNode _node;

		// Token: 0x0400042F RID: 1071
		private DataColumn _column;

		// Token: 0x04000430 RID: 1072
		private bool _fOnValue;

		// Token: 0x04000431 RID: 1073
		private bool _bNeedFoliate;

		// Token: 0x04000432 RID: 1074
		private bool _isInUse;
	}
}
