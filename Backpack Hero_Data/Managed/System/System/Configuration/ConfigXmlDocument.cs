using System;
using System.Configuration.Internal;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Wraps the corresponding <see cref="T:System.Xml.XmlDocument" /> type and also carries the necessary information for reporting file-name and line numbers. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200019F RID: 415
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class ConfigXmlDocument : XmlDocument, IConfigXmlNode, IConfigErrorInfo
	{
		/// <summary>Creates a configuration element attribute.</summary>
		/// <returns>The <see cref="P:System.Xml.Serialization.XmlAttributes.XmlAttribute" /> attribute.</returns>
		/// <param name="prefix">The prefix definition.</param>
		/// <param name="localName">The name that is used locally.</param>
		/// <param name="namespaceUri">The URL that is assigned to the namespace.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002ED8F File Offset: 0x0002CF8F
		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlDocument.ConfigXmlAttribute(this, prefix, localName, namespaceUri);
		}

		/// <summary>Creates an XML CData section.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlCDataSection" /> value.</returns>
		/// <param name="data">The data to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002ED9A File Offset: 0x0002CF9A
		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new ConfigXmlDocument.ConfigXmlCDataSection(this, data);
		}

		/// <summary>Create an XML comment.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlComment" /> value.</returns>
		/// <param name="data">The comment data.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002EDA3 File Offset: 0x0002CFA3
		public override XmlComment CreateComment(string data)
		{
			return new ConfigXmlDocument.ConfigXmlComment(this, data);
		}

		/// <summary>Creates a configuration element.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlElement" /> value.</returns>
		/// <param name="prefix">The prefix definition.</param>
		/// <param name="localName">The name used locally.</param>
		/// <param name="namespaceUri">The namespace for the URL.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002EDAC File Offset: 0x0002CFAC
		public override XmlElement CreateElement(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlDocument.ConfigXmlElement(this, prefix, localName, namespaceUri);
		}

		/// <summary>Creates white spaces.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlSignificantWhitespace" /> value.</returns>
		/// <param name="data">The data to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002EDB7 File Offset: 0x0002CFB7
		public override XmlSignificantWhitespace CreateSignificantWhitespace(string data)
		{
			return base.CreateSignificantWhitespace(data);
		}

		/// <summary>Create a text node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlText" /> value.</returns>
		/// <param name="text">The text to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002EDC0 File Offset: 0x0002CFC0
		public override XmlText CreateTextNode(string text)
		{
			return new ConfigXmlDocument.ConfigXmlText(this, text);
		}

		/// <summary>Creates white space.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlWhitespace" /> value.</returns>
		/// <param name="data">The data to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002EDC9 File Offset: 0x0002CFC9
		public override XmlWhitespace CreateWhitespace(string data)
		{
			return base.CreateWhitespace(data);
		}

		/// <summary>Loads the configuration file.</summary>
		/// <param name="filename">The name of the file.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002EDD4 File Offset: 0x0002CFD4
		public override void Load(string filename)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(filename);
			try
			{
				xmlTextReader.MoveToContent();
				this.LoadSingleElement(filename, xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Loads a single configuration element.</summary>
		/// <param name="filename">The name of the file.</param>
		/// <param name="sourceReader">The source for the reader.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AEA RID: 2794 RVA: 0x0002EE10 File Offset: 0x0002D010
		public void LoadSingleElement(string filename, XmlTextReader sourceReader)
		{
			this.fileName = filename;
			this.lineNumber = sourceReader.LineNumber;
			string text = sourceReader.ReadOuterXml();
			this.reader = new XmlTextReader(new StringReader(text), sourceReader.NameTable);
			this.Load(this.reader);
			this.reader.Close();
		}

		/// <summary>Gets the configuration file name.</summary>
		/// <returns>The configuration file name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0002EE65 File Offset: 0x0002D065
		public string Filename
		{
			get
			{
				if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
				}
				return this.fileName;
			}
		}

		/// <summary>Gets the current node line number.</summary>
		/// <returns>The line number for the current node.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0002EE9B File Offset: 0x0002D09B
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the configuration file name.</summary>
		/// <returns>The file name.</returns>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0002EEA3 File Offset: 0x0002D0A3
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this.Filename;
			}
		}

		/// <summary>Gets the configuration line number.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0002EEAB File Offset: 0x0002D0AB
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0002EEA3 File Offset: 0x0002D0A3
		string IConfigXmlNode.Filename
		{
			get
			{
				return this.Filename;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0002EEAB File Offset: 0x0002D0AB
		int IConfigXmlNode.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		// Token: 0x04000732 RID: 1842
		private XmlTextReader reader;

		// Token: 0x04000733 RID: 1843
		private string fileName;

		// Token: 0x04000734 RID: 1844
		private int lineNumber;

		// Token: 0x020001A0 RID: 416
		private class ConfigXmlAttribute : XmlAttribute, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000AF2 RID: 2802 RVA: 0x0002EEBB File Offset: 0x0002D0BB
			public ConfigXmlAttribute(ConfigXmlDocument document, string prefix, string localName, string namespaceUri)
				: base(prefix, localName, namespaceUri, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0002EEE0 File Offset: 0x0002D0E0
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0002EF16 File Offset: 0x0002D116
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x04000735 RID: 1845
			private string fileName;

			// Token: 0x04000736 RID: 1846
			private int lineNumber;
		}

		// Token: 0x020001A1 RID: 417
		private class ConfigXmlCDataSection : XmlCDataSection, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000AF5 RID: 2805 RVA: 0x0002EF1E File Offset: 0x0002D11E
			public ConfigXmlCDataSection(ConfigXmlDocument document, string data)
				: base(data, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0002EF40 File Offset: 0x0002D140
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0002EF76 File Offset: 0x0002D176
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x04000737 RID: 1847
			private string fileName;

			// Token: 0x04000738 RID: 1848
			private int lineNumber;
		}

		// Token: 0x020001A2 RID: 418
		private class ConfigXmlComment : XmlComment, IConfigXmlNode
		{
			// Token: 0x06000AF8 RID: 2808 RVA: 0x0002EF7E File Offset: 0x0002D17E
			public ConfigXmlComment(ConfigXmlDocument document, string comment)
				: base(comment, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0002EFA0 File Offset: 0x0002D1A0
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0002EFD6 File Offset: 0x0002D1D6
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x04000739 RID: 1849
			private string fileName;

			// Token: 0x0400073A RID: 1850
			private int lineNumber;
		}

		// Token: 0x020001A3 RID: 419
		private class ConfigXmlElement : XmlElement, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000AFB RID: 2811 RVA: 0x0002EFDE File Offset: 0x0002D1DE
			public ConfigXmlElement(ConfigXmlDocument document, string prefix, string localName, string namespaceUri)
				: base(prefix, localName, namespaceUri, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0002F003 File Offset: 0x0002D203
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0002F039 File Offset: 0x0002D239
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x0400073B RID: 1851
			private string fileName;

			// Token: 0x0400073C RID: 1852
			private int lineNumber;
		}

		// Token: 0x020001A4 RID: 420
		private class ConfigXmlText : XmlText, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000AFE RID: 2814 RVA: 0x0002F041 File Offset: 0x0002D241
			public ConfigXmlText(ConfigXmlDocument document, string data)
				: base(data, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0002F063 File Offset: 0x0002D263
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0002F099 File Offset: 0x0002D299
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x0400073D RID: 1853
			private string fileName;

			// Token: 0x0400073E RID: 1854
			private int lineNumber;
		}
	}
}
