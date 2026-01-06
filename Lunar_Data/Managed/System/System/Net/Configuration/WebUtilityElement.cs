using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the WebUtility element in the configuration file.</summary>
	// Token: 0x02000873 RID: 2163
	public sealed class WebUtilityElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebUtilityElement" /> class.</summary>
		// Token: 0x060044AC RID: 17580 RVA: 0x00013B26 File Offset: 0x00011D26
		public WebUtilityElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the default Unicode decoding conformance behavior used for an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.UnicodeDecodingConformance" />.The default Unicode decoding behavior.</returns>
		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x060044AD RID: 17581 RVA: 0x000ECF48 File Offset: 0x000EB148
		// (set) Token: 0x060044AE RID: 17582 RVA: 0x00013B26 File Offset: 0x00011D26
		public UnicodeDecodingConformance UnicodeDecodingConformance
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return UnicodeDecodingConformance.Auto;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the default Unicode encoding conformance behavior used for an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.UnicodeEncodingConformance" />.The default Unicode encoding behavior.</returns>
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x000ECF64 File Offset: 0x000EB164
		// (set) Token: 0x060044B0 RID: 17584 RVA: 0x00013B26 File Offset: 0x00011D26
		public UnicodeEncodingConformance UnicodeEncodingConformance
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return UnicodeEncodingConformance.Auto;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
