using System;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200003C RID: 60
	internal class HashException : Exception
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000356E File Offset: 0x0000176E
		public int Hash { get; }

		// Token: 0x0600010D RID: 269 RVA: 0x00003576 File Offset: 0x00001776
		public HashException(int hash)
		{
			this.Hash = hash;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003585 File Offset: 0x00001785
		public HashException(int hash, string message)
		{
			this.Hash = hash;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003594 File Offset: 0x00001794
		public HashException(int hash, string message, Exception inner)
			: base(message, inner)
		{
			this.Hash = hash;
		}
	}
}
