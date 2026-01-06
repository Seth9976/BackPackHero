using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML language type.</summary>
	// Token: 0x020005EE RID: 1518
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapLanguage : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> class.</summary>
		// Token: 0x06003991 RID: 14737 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapLanguage()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> class with the language identifier value of language attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains the language identifier value of a language attribute. </param>
		// Token: 0x06003992 RID: 14738 RVA: 0x000CBC06 File Offset: 0x000C9E06
		public SoapLanguage(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets the language identifier of a language attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the language identifier of a language attribute.</returns>
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000CBC1A File Offset: 0x000C9E1A
		// (set) Token: 0x06003994 RID: 14740 RVA: 0x000CBC22 File Offset: 0x000C9E22
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06003995 RID: 14741 RVA: 0x000CBC2B File Offset: 0x000C9E2B
		public static string XsdType
		{
			get
			{
				return "language";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003996 RID: 14742 RVA: 0x000CBC32 File Offset: 0x000C9E32
		public string GetXsdType()
		{
			return SoapLanguage.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06003997 RID: 14743 RVA: 0x000CBC39 File Offset: 0x000C9E39
		public static SoapLanguage Parse(string value)
		{
			return new SoapLanguage(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage" /> object that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapLanguage.Value" />.</returns>
		// Token: 0x06003998 RID: 14744 RVA: 0x000CBC1A File Offset: 0x000C9E1A
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x0400262F RID: 9775
		private string _value;
	}
}
