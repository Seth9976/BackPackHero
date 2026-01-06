using System;

namespace System.Xml
{
	// Token: 0x0200002E RID: 46
	internal sealed class TreeIterator : BaseTreeIterator
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00006F35 File Offset: 0x00005135
		internal TreeIterator(XmlNode nodeTop)
			: base(((XmlDataDocument)nodeTop.OwnerDocument).Mapper)
		{
			this._nodeTop = nodeTop;
			this._currentNode = nodeTop;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006F5B File Offset: 0x0000515B
		internal override XmlNode CurrentNode
		{
			get
			{
				return this._currentNode;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006F64 File Offset: 0x00005164
		internal override bool Next()
		{
			XmlNode firstChild = this._currentNode.FirstChild;
			if (firstChild != null)
			{
				this._currentNode = firstChild;
				return true;
			}
			return this.NextRight();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006F90 File Offset: 0x00005190
		internal override bool NextRight()
		{
			if (this._currentNode == this._nodeTop)
			{
				this._currentNode = null;
				return false;
			}
			XmlNode xmlNode = this._currentNode.NextSibling;
			if (xmlNode != null)
			{
				this._currentNode = xmlNode;
				return true;
			}
			xmlNode = this._currentNode;
			while (xmlNode != this._nodeTop && xmlNode.NextSibling == null)
			{
				xmlNode = xmlNode.ParentNode;
			}
			if (xmlNode == this._nodeTop)
			{
				this._currentNode = null;
				return false;
			}
			this._currentNode = xmlNode.NextSibling;
			return true;
		}

		// Token: 0x0400043A RID: 1082
		private readonly XmlNode _nodeTop;

		// Token: 0x0400043B RID: 1083
		private XmlNode _currentNode;
	}
}
