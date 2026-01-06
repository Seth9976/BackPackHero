using System;
using System.Collections.Specialized;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides name/value-pair configuration information from a configuration section.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BA RID: 442
	public class NameValueSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <returns>A configuration object.</returns>
		/// <param name="parent">Parent object.</param>
		/// <param name="context">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BA2 RID: 2978 RVA: 0x00031410 File Offset: 0x0002F610
		public object Create(object parent, object context, XmlNode section)
		{
			return ConfigHelper.GetNameValueCollection(parent as NameValueCollection, section, this.KeyAttributeName, this.ValueAttributeName);
		}

		/// <summary>Gets the XML attribute name to use as the key in a key/value pair.</summary>
		/// <returns>A <see cref="T:System.String" /> value containing the name of the key attribute.</returns>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0003114B File Offset: 0x0002F34B
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		/// <summary>Gets the XML attribute name to use as the value in a key/value pair.</summary>
		/// <returns>A <see cref="T:System.String" /> value containing the name of the value attribute.</returns>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00031152 File Offset: 0x0002F352
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}
	}
}
