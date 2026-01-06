using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x020000DE RID: 222
	[Serializable]
	internal sealed class SimpleType : ISerializable
	{
		// Token: 0x06000C8F RID: 3215 RVA: 0x00039748 File Offset: 0x00037948
		internal SimpleType(string baseType)
		{
			this._baseType = baseType;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000397D0 File Offset: 0x000379D0
		internal SimpleType(XmlSchemaSimpleType node)
		{
			this._name = node.Name;
			this._ns = ((node.QualifiedName != null) ? node.QualifiedName.Namespace : "");
			this.LoadTypeValues(node);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00039889 File Offset: 0x00037A89
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00039890 File Offset: 0x00037A90
		internal void LoadTypeValues(XmlSchemaSimpleType node)
		{
			if (node.Content is XmlSchemaSimpleTypeList || node.Content is XmlSchemaSimpleTypeUnion)
			{
				throw ExceptionBuilder.SimpleTypeNotSupported();
			}
			if (node.Content is XmlSchemaSimpleTypeRestriction)
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)node.Content;
				XmlSchemaSimpleType xmlSchemaSimpleType = node.BaseXmlSchemaType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null && xmlSchemaSimpleType.QualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					this._baseSimpleType = new SimpleType(node.BaseXmlSchemaType as XmlSchemaSimpleType);
				}
				if (xmlSchemaSimpleTypeRestriction.BaseTypeName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseTypeName.Name;
				}
				else
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseTypeName.ToString();
				}
				if (this._baseSimpleType != null && this._baseSimpleType.Name != null && this._baseSimpleType.Name.Length > 0)
				{
					this._xmlBaseType = this._baseSimpleType.XmlBaseType;
				}
				else
				{
					this._xmlBaseType = xmlSchemaSimpleTypeRestriction.BaseTypeName;
				}
				if (this._baseType == null || this._baseType.Length == 0)
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseType.Name;
					this._xmlBaseType = null;
				}
				if (this._baseType == "NOTATION")
				{
					this._baseType = "string";
				}
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaSimpleTypeRestriction.Facets)
				{
					XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)xmlSchemaObject;
					if (xmlSchemaFacet is XmlSchemaLengthFacet)
					{
						this._length = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaMinLengthFacet)
					{
						this._minLength = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaMaxLengthFacet)
					{
						this._maxLength = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaPatternFacet)
					{
						this._pattern = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaEnumerationFacet)
					{
						this._enumeration = ((!string.IsNullOrEmpty(this._enumeration)) ? (this._enumeration + " " + xmlSchemaFacet.Value) : xmlSchemaFacet.Value);
					}
					if (xmlSchemaFacet is XmlSchemaMinExclusiveFacet)
					{
						this._minExclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMinInclusiveFacet)
					{
						this._minInclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMaxExclusiveFacet)
					{
						this._maxExclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMaxInclusiveFacet)
					{
						this._maxInclusive = xmlSchemaFacet.Value;
					}
				}
			}
			string msdataAttribute = XSDSchema.GetMsdataAttribute(node, "targetNamespace");
			if (msdataAttribute != null)
			{
				this._ns = msdataAttribute;
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00039B50 File Offset: 0x00037D50
		internal bool IsPlainString()
		{
			return XSDSchema.QualifiedName(this._baseType) == XSDSchema.QualifiedName("string") && string.IsNullOrEmpty(this._name) && this._length == -1 && this._minLength == -1 && this._maxLength == -1 && string.IsNullOrEmpty(this._pattern) && string.IsNullOrEmpty(this._maxExclusive) && string.IsNullOrEmpty(this._maxInclusive) && string.IsNullOrEmpty(this._minExclusive) && string.IsNullOrEmpty(this._minInclusive) && string.IsNullOrEmpty(this._enumeration);
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00039BEF File Offset: 0x00037DEF
		internal string BaseType
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00039BF7 File Offset: 0x00037DF7
		internal XmlQualifiedName XmlBaseType
		{
			get
			{
				return this._xmlBaseType;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00039BFF File Offset: 0x00037DFF
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00039C07 File Offset: 0x00037E07
		internal string Namespace
		{
			get
			{
				return this._ns;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00039C0F File Offset: 0x00037E0F
		internal int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00039C17 File Offset: 0x00037E17
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x00039C1F File Offset: 0x00037E1F
		internal int MaxLength
		{
			get
			{
				return this._maxLength;
			}
			set
			{
				this._maxLength = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00039C28 File Offset: 0x00037E28
		internal SimpleType BaseSimpleType
		{
			get
			{
				return this._baseSimpleType;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00039C30 File Offset: 0x00037E30
		public string SimpleTypeQualifiedName
		{
			get
			{
				if (this._ns.Length == 0)
				{
					return this._name;
				}
				return this._ns + ":" + this._name;
			}
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00039C5C File Offset: 0x00037E5C
		internal string QualifiedName(string name)
		{
			if (name.IndexOf(':') == -1)
			{
				return "xs:" + name;
			}
			return name;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00039C78 File Offset: 0x00037E78
		internal XmlNode ToNode(XmlDocument dc, Hashtable prefixes, bool inRemoting)
		{
			XmlElement xmlElement = dc.CreateElement("xs", "simpleType", "http://www.w3.org/2001/XMLSchema");
			if (this._name != null && this._name.Length != 0)
			{
				xmlElement.SetAttribute("name", this._name);
				if (inRemoting)
				{
					xmlElement.SetAttribute("targetNamespace", "urn:schemas-microsoft-com:xml-msdata", this.Namespace);
				}
			}
			XmlElement xmlElement2 = dc.CreateElement("xs", "restriction", "http://www.w3.org/2001/XMLSchema");
			if (!inRemoting)
			{
				if (this._baseSimpleType != null)
				{
					if (this._baseSimpleType.Namespace != null && this._baseSimpleType.Namespace.Length > 0)
					{
						string text = ((prefixes != null) ? ((string)prefixes[this._baseSimpleType.Namespace]) : null);
						if (text != null)
						{
							xmlElement2.SetAttribute("base", text + ":" + this._baseSimpleType.Name);
						}
						else
						{
							xmlElement2.SetAttribute("base", this._baseSimpleType.Name);
						}
					}
					else
					{
						xmlElement2.SetAttribute("base", this._baseSimpleType.Name);
					}
				}
				else
				{
					xmlElement2.SetAttribute("base", this.QualifiedName(this._baseType));
				}
			}
			else
			{
				xmlElement2.SetAttribute("base", (this._baseSimpleType != null) ? this._baseSimpleType.Name : this.QualifiedName(this._baseType));
			}
			if (this._length >= 0)
			{
				XmlElement xmlElement3 = dc.CreateElement("xs", "length", "http://www.w3.org/2001/XMLSchema");
				xmlElement3.SetAttribute("value", this._length.ToString(CultureInfo.InvariantCulture));
				xmlElement2.AppendChild(xmlElement3);
			}
			if (this._maxLength >= 0)
			{
				XmlElement xmlElement3 = dc.CreateElement("xs", "maxLength", "http://www.w3.org/2001/XMLSchema");
				xmlElement3.SetAttribute("value", this._maxLength.ToString(CultureInfo.InvariantCulture));
				xmlElement2.AppendChild(xmlElement3);
			}
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00039E67 File Offset: 0x00038067
		internal static SimpleType CreateEnumeratedType(string values)
		{
			return new SimpleType("string")
			{
				_enumeration = values
			};
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00039E7A File Offset: 0x0003807A
		internal static SimpleType CreateByteArrayType(string encoding)
		{
			return new SimpleType("base64Binary");
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00039E86 File Offset: 0x00038086
		internal static SimpleType CreateLimitedStringType(int length)
		{
			return new SimpleType("string")
			{
				_maxLength = length
			};
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00039E99 File Offset: 0x00038099
		internal static SimpleType CreateSimpleType(StorageType typeCode, Type type)
		{
			if (typeCode == StorageType.Char && type == typeof(char))
			{
				return new SimpleType("string")
				{
					_length = 1
				};
			}
			return null;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00039EC4 File Offset: 0x000380C4
		internal string HasConflictingDefinition(SimpleType otherSimpleType)
		{
			if (otherSimpleType == null)
			{
				return "otherSimpleType";
			}
			if (this.MaxLength != otherSimpleType.MaxLength)
			{
				return "MaxLength";
			}
			if (!string.Equals(this.BaseType, otherSimpleType.BaseType, StringComparison.Ordinal))
			{
				return "BaseType";
			}
			if (this.BaseSimpleType != null && otherSimpleType.BaseSimpleType != null && this.BaseSimpleType.HasConflictingDefinition(otherSimpleType.BaseSimpleType).Length != 0)
			{
				return "BaseSimpleType";
			}
			return string.Empty;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00039F3C File Offset: 0x0003813C
		internal bool CanHaveMaxLength()
		{
			SimpleType simpleType = this;
			while (simpleType.BaseSimpleType != null)
			{
				simpleType = simpleType.BaseSimpleType;
			}
			return string.Equals(simpleType.BaseType, "string", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00039F70 File Offset: 0x00038170
		internal void ConvertToAnnonymousSimpleType()
		{
			this._name = null;
			this._ns = string.Empty;
			SimpleType simpleType = this;
			while (simpleType._baseSimpleType != null)
			{
				simpleType = simpleType._baseSimpleType;
			}
			this._baseType = simpleType._baseType;
			this._baseSimpleType = simpleType._baseSimpleType;
			this._xmlBaseType = simpleType._xmlBaseType;
		}

		// Token: 0x04000803 RID: 2051
		private string _baseType;

		// Token: 0x04000804 RID: 2052
		private SimpleType _baseSimpleType;

		// Token: 0x04000805 RID: 2053
		private XmlQualifiedName _xmlBaseType;

		// Token: 0x04000806 RID: 2054
		private string _name = string.Empty;

		// Token: 0x04000807 RID: 2055
		private int _length = -1;

		// Token: 0x04000808 RID: 2056
		private int _minLength = -1;

		// Token: 0x04000809 RID: 2057
		private int _maxLength = -1;

		// Token: 0x0400080A RID: 2058
		private string _pattern = string.Empty;

		// Token: 0x0400080B RID: 2059
		private string _ns = string.Empty;

		// Token: 0x0400080C RID: 2060
		private string _maxExclusive = string.Empty;

		// Token: 0x0400080D RID: 2061
		private string _maxInclusive = string.Empty;

		// Token: 0x0400080E RID: 2062
		private string _minExclusive = string.Empty;

		// Token: 0x0400080F RID: 2063
		private string _minInclusive = string.Empty;

		// Token: 0x04000810 RID: 2064
		internal string _enumeration = string.Empty;
	}
}
