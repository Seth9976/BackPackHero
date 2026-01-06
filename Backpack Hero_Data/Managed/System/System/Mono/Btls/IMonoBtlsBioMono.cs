using System;

namespace Mono.Btls
{
	// Token: 0x020000D0 RID: 208
	internal interface IMonoBtlsBioMono
	{
		// Token: 0x0600041B RID: 1051
		int Read(byte[] buffer, int offset, int size, out bool wantMore);

		// Token: 0x0600041C RID: 1052
		bool Write(byte[] buffer, int offset, int size);

		// Token: 0x0600041D RID: 1053
		void Flush();

		// Token: 0x0600041E RID: 1054
		void Close();
	}
}
