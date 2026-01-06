using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200002D RID: 45
	internal sealed class RegionIterator : BaseRegionIterator
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00006D0B File Offset: 0x00004F0B
		internal RegionIterator(XmlBoundElement rowElement)
			: base(((XmlDataDocument)rowElement.OwnerDocument).Mapper)
		{
			this._rowElement = rowElement;
			this._currentNode = rowElement;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006D31 File Offset: 0x00004F31
		internal override XmlNode CurrentNode
		{
			get
			{
				return this._currentNode;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006D3C File Offset: 0x00004F3C
		internal override bool Next()
		{
			ElementState elementState = this._rowElement.ElementState;
			XmlNode firstChild = this._currentNode.FirstChild;
			if (firstChild != null)
			{
				this._currentNode = firstChild;
				this._rowElement.ElementState = elementState;
				return true;
			}
			return this.NextRight();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006D80 File Offset: 0x00004F80
		internal override bool NextRight()
		{
			if (this._currentNode == this._rowElement)
			{
				this._currentNode = null;
				return false;
			}
			ElementState elementState = this._rowElement.ElementState;
			XmlNode xmlNode = this._currentNode.NextSibling;
			if (xmlNode != null)
			{
				this._currentNode = xmlNode;
				this._rowElement.ElementState = elementState;
				return true;
			}
			xmlNode = this._currentNode;
			while (xmlNode != this._rowElement && xmlNode.NextSibling == null)
			{
				xmlNode = xmlNode.ParentNode;
			}
			if (xmlNode == this._rowElement)
			{
				this._currentNode = null;
				this._rowElement.ElementState = elementState;
				return false;
			}
			this._currentNode = xmlNode.NextSibling;
			this._rowElement.ElementState = elementState;
			return true;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006E2C File Offset: 0x0000502C
		internal bool NextInitialTextLikeNodes(out string value)
		{
			ElementState elementState = this._rowElement.ElementState;
			XmlNode firstChild = this.CurrentNode.FirstChild;
			value = RegionIterator.GetInitialTextFromNodes(ref firstChild);
			if (firstChild == null)
			{
				this._rowElement.ElementState = elementState;
				return this.NextRight();
			}
			this._currentNode = firstChild;
			this._rowElement.ElementState = elementState;
			return true;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006E84 File Offset: 0x00005084
		private static string GetInitialTextFromNodes(ref XmlNode n)
		{
			string text = null;
			if (n != null)
			{
				while (n.NodeType == XmlNodeType.Whitespace)
				{
					n = n.NextSibling;
					if (n == null)
					{
						return string.Empty;
					}
				}
				if (XmlDataDocument.IsTextLikeNode(n) && (n.NextSibling == null || !XmlDataDocument.IsTextLikeNode(n.NextSibling)))
				{
					text = n.Value;
					n = n.NextSibling;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (n != null && XmlDataDocument.IsTextLikeNode(n))
					{
						if (n.NodeType != XmlNodeType.Whitespace)
						{
							stringBuilder.Append(n.Value);
						}
						n = n.NextSibling;
					}
					text = stringBuilder.ToString();
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x04000438 RID: 1080
		private XmlBoundElement _rowElement;

		// Token: 0x04000439 RID: 1081
		private XmlNode _currentNode;
	}
}
