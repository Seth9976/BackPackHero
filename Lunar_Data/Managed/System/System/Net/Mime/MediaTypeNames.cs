using System;

namespace System.Net.Mime
{
	/// <summary>Specifies the media type information for an e-mail message attachment.</summary>
	// Token: 0x0200060D RID: 1549
	public static class MediaTypeNames
	{
		/// <summary>Specifies the type of text data in an e-mail message attachment.</summary>
		// Token: 0x0200060E RID: 1550
		public static class Text
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in plain text format.</summary>
			// Token: 0x04001E5E RID: 7774
			public const string Plain = "text/plain";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in HTML format.</summary>
			// Token: 0x04001E5F RID: 7775
			public const string Html = "text/html";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in XML format.</summary>
			// Token: 0x04001E60 RID: 7776
			public const string Xml = "text/xml";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x04001E61 RID: 7777
			public const string RichText = "text/richtext";
		}

		/// <summary>Specifies the kind of application data in an e-mail message attachment.</summary>
		// Token: 0x0200060F RID: 1551
		public static class Application
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is a SOAP document.</summary>
			// Token: 0x04001E62 RID: 7778
			public const string Soap = "application/soap+xml";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is not interpreted.</summary>
			// Token: 0x04001E63 RID: 7779
			public const string Octet = "application/octet-stream";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x04001E64 RID: 7780
			public const string Rtf = "application/rtf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Portable Document Format (PDF).</summary>
			// Token: 0x04001E65 RID: 7781
			public const string Pdf = "application/pdf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is compressed.</summary>
			// Token: 0x04001E66 RID: 7782
			public const string Zip = "application/zip";

			// Token: 0x04001E67 RID: 7783
			public const string Json = "application/json";

			// Token: 0x04001E68 RID: 7784
			public const string Xml = "application/xml";
		}

		/// <summary>Specifies the type of image data in an e-mail message attachment.</summary>
		// Token: 0x02000610 RID: 1552
		public static class Image
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Graphics Interchange Format (GIF).</summary>
			// Token: 0x04001E69 RID: 7785
			public const string Gif = "image/gif";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Tagged Image File Format (TIFF).</summary>
			// Token: 0x04001E6A RID: 7786
			public const string Tiff = "image/tiff";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Joint Photographic Experts Group (JPEG) format.</summary>
			// Token: 0x04001E6B RID: 7787
			public const string Jpeg = "image/jpeg";
		}
	}
}
