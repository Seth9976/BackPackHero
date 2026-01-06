using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD QName type.</summary>
	// Token: 0x020005FB RID: 1531
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapQName : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class.</summary>
		// Token: 0x060039FB RID: 14843 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapQName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the local part of a qualified name.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains the local part of a qualified name. </param>
		// Token: 0x060039FC RID: 14844 RVA: 0x000CC03E File Offset: 0x000CA23E
		public SoapQName(string value)
		{
			this._name = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the namespace alias and the local part of a qualified name.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the namespace alias of a qualified name. </param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the local part of a qualified name. </param>
		// Token: 0x060039FD RID: 14845 RVA: 0x000CC04D File Offset: 0x000CA24D
		public SoapQName(string key, string name)
		{
			this._key = key;
			this._name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the namespace alias, the local part of a qualified name, and the namespace that is referenced by the alias.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the namespace alias of a qualified name. </param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the local part of a qualified name. </param>
		/// <param name="namespaceValue">A <see cref="T:System.String" /> that contains the namespace that is referenced by <paramref name="key" />. </param>
		// Token: 0x060039FE RID: 14846 RVA: 0x000CC063 File Offset: 0x000CA263
		public SoapQName(string key, string name, string namespaceValue)
		{
			this._key = key;
			this._name = name;
			this._namespace = namespaceValue;
		}

		/// <summary>Gets or sets the namespace alias of a qualified name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace alias of a qualified name.</returns>
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060039FF RID: 14847 RVA: 0x000CC080 File Offset: 0x000CA280
		// (set) Token: 0x06003A00 RID: 14848 RVA: 0x000CC088 File Offset: 0x000CA288
		public string Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		/// <summary>Gets or sets the name portion of a qualified name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name portion of a qualified name.</returns>
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06003A01 RID: 14849 RVA: 0x000CC091 File Offset: 0x000CA291
		// (set) Token: 0x06003A02 RID: 14850 RVA: 0x000CC099 File Offset: 0x000CA299
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets or sets the namespace that is referenced by <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace that is referenced by <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" />.</returns>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x000CC0A2 File Offset: 0x000CA2A2
		// (set) Token: 0x06003A04 RID: 14852 RVA: 0x000CC0AA File Offset: 0x000CA2AA
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x000CC0B3 File Offset: 0x000CA2B3
		public static string XsdType
		{
			get
			{
				return "QName";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the XSD of the current SOAP type.</returns>
		// Token: 0x06003A06 RID: 14854 RVA: 0x000CC0BA File Offset: 0x000CA2BA
		public string GetXsdType()
		{
			return SoapQName.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		// Token: 0x06003A07 RID: 14855 RVA: 0x000CC0C4 File Offset: 0x000CA2C4
		public static SoapQName Parse(string value)
		{
			SoapQName soapQName = new SoapQName();
			int num = value.IndexOf(':');
			if (num != -1)
			{
				soapQName.Key = value.Substring(0, num);
				soapQName.Name = value.Substring(num + 1);
			}
			else
			{
				soapQName.Name = value;
			}
			return soapQName;
		}

		/// <summary>Returns the qualified name as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> in the format " <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> : <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" /> ". If <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> is not specified, this method returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" />.</returns>
		// Token: 0x06003A08 RID: 14856 RVA: 0x000CC10B File Offset: 0x000CA30B
		public override string ToString()
		{
			if (this._key == null || this._key == "")
			{
				return this._name;
			}
			return this._key + ":" + this._name;
		}

		// Token: 0x0400263E RID: 9790
		private string _name;

		// Token: 0x0400263F RID: 9791
		private string _key;

		// Token: 0x04002640 RID: 9792
		private string _namespace;
	}
}
