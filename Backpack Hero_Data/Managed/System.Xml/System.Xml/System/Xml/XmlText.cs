using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents the text content of an element or attribute.</summary>
	// Token: 0x020001DB RID: 475
	public class XmlText : XmlCharacterData
	{
		// Token: 0x060012E3 RID: 4835 RVA: 0x00070892 File Offset: 0x0006EA92
		internal XmlText(string strData)
			: this(strData, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlText" /> class.</summary>
		/// <param name="strData">The content of the node; see the <see cref="P:System.Xml.XmlText.Value" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x060012E4 RID: 4836 RVA: 0x0006734B File Offset: 0x0006554B
		protected internal XmlText(string strData, XmlDocument doc)
			: base(strData, doc)
		{
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For text nodes, this property returns #text.</returns>
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x0007089C File Offset: 0x0006EA9C
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strTextName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For text nodes, this property returns #text.</returns>
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0007089C File Offset: 0x0006EA9C
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strTextName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For text nodes, this value is XmlNodeType.Text.</returns>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Text;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x000708AC File Offset: 0x0006EAAC
		public override XmlNode ParentNode
		{
			get
			{
				XmlNodeType nodeType = this.parentNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.Document)
					{
						return null;
					}
					if (nodeType - XmlNodeType.Whitespace > 1)
					{
						return this.parentNode;
					}
				}
				XmlNode xmlNode = this.parentNode.parentNode;
				while (xmlNode.IsText)
				{
					xmlNode = xmlNode.parentNode;
				}
				return xmlNode;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		// Token: 0x060012E9 RID: 4841 RVA: 0x00070900 File Offset: 0x0006EB00
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateTextNode(this.Data);
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The content of the text node.</returns>
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x00067404 File Offset: 0x00065604
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x00070914 File Offset: 0x0006EB14
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
				XmlNode parentNode = this.parentNode;
				if (parentNode != null && parentNode.NodeType == XmlNodeType.Attribute)
				{
					XmlUnspecifiedAttribute xmlUnspecifiedAttribute = parentNode as XmlUnspecifiedAttribute;
					if (xmlUnspecifiedAttribute != null && !xmlUnspecifiedAttribute.Specified)
					{
						xmlUnspecifiedAttribute.SetSpecified(true);
					}
				}
			}
		}

		/// <summary>Splits the node into two nodes at the specified offset, keeping both in the tree as siblings.</summary>
		/// <returns>The new node.</returns>
		/// <param name="offset">The offset at which to split the node. </param>
		// Token: 0x060012EC RID: 4844 RVA: 0x00070954 File Offset: 0x0006EB54
		public virtual XmlText SplitText(int offset)
		{
			XmlNode parentNode = this.ParentNode;
			int length = this.Length;
			if (offset > length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (parentNode == null)
			{
				throw new InvalidOperationException(Res.GetString("The 'Text' node is not connected in the DOM live tree. No 'SplitText' operation could be performed."));
			}
			int num = length - offset;
			string text = this.Substring(offset, num);
			this.DeleteData(offset, num);
			XmlText xmlText = this.OwnerDocument.CreateTextNode(text);
			parentNode.InsertAfter(xmlText, this);
			return xmlText;
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060012ED RID: 4845 RVA: 0x0007086A File Offset: 0x0006EA6A
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. XmlText nodes do not have children, so this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060012EE RID: 4846 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x00067362 File Offset: 0x00065562
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x000673DD File Offset: 0x000655DD
		public override XmlNode PreviousText
		{
			get
			{
				if (this.parentNode.IsText)
				{
					return this.parentNode;
				}
				return null;
			}
		}
	}
}
