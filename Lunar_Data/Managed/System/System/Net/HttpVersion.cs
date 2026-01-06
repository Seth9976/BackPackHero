using System;

namespace System.Net
{
	/// <summary>Defines the HTTP version numbers that are supported by the <see cref="T:System.Net.HttpWebRequest" /> and <see cref="T:System.Net.HttpWebResponse" /> classes.</summary>
	// Token: 0x0200038C RID: 908
	public class HttpVersion
	{
		// Token: 0x04000FAF RID: 4015
		public static readonly Version Unknown = new Version(0, 0);

		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.0.</summary>
		// Token: 0x04000FB0 RID: 4016
		public static readonly Version Version10 = new Version(1, 0);

		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.1.</summary>
		// Token: 0x04000FB1 RID: 4017
		public static readonly Version Version11 = new Version(1, 1);

		// Token: 0x04000FB2 RID: 4018
		public static readonly Version Version20 = new Version(2, 0);
	}
}
