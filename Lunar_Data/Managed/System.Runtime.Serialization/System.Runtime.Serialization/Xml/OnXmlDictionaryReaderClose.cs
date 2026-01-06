using System;

namespace System.Xml
{
	/// <summary>delegate for a callback method when closing the reader.</summary>
	/// <param name="reader">The <see cref="T:System.Xml.XmlDictionaryReader" /> that fires the OnClose event.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005F RID: 95
	// (Invoke) Token: 0x06000473 RID: 1139
	public delegate void OnXmlDictionaryReaderClose(XmlDictionaryReader reader);
}
