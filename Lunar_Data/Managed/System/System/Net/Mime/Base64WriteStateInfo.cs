using System;

namespace System.Net.Mime
{
	// Token: 0x02000603 RID: 1539
	internal class Base64WriteStateInfo : WriteStateInfoBase
	{
		// Token: 0x06003165 RID: 12645 RVA: 0x000B0FAD File Offset: 0x000AF1AD
		internal Base64WriteStateInfo()
		{
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000B0FB5 File Offset: 0x000AF1B5
		internal Base64WriteStateInfo(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength)
			: base(bufferSize, header, footer, maxLineLength, mimeHeaderLength)
		{
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000B0FC4 File Offset: 0x000AF1C4
		// (set) Token: 0x06003168 RID: 12648 RVA: 0x000B0FCC File Offset: 0x000AF1CC
		internal int Padding { get; set; }

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000B0FD5 File Offset: 0x000AF1D5
		// (set) Token: 0x0600316A RID: 12650 RVA: 0x000B0FDD File Offset: 0x000AF1DD
		internal byte LastBits { get; set; }
	}
}
