using System;

namespace DevPunksSaveGame.Interfaces
{
	// Token: 0x02000233 RID: 563
	public class SaveFileEntry
	{
		// Token: 0x06001275 RID: 4725 RVA: 0x000ADEF6 File Offset: 0x000AC0F6
		public SaveFileEntry(string slot, string path)
		{
			this.Slot = slot;
			this.Path = path;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x000ADF0C File Offset: 0x000AC10C
		public SaveFileEntry(string slot, string path, byte[] value)
		{
			this.Slot = slot;
			this.Path = path;
			this.Value = value;
		}

		// Token: 0x04000E83 RID: 3715
		public string Slot;

		// Token: 0x04000E84 RID: 3716
		public string Path;

		// Token: 0x04000E85 RID: 3717
		public byte[] Value;
	}
}
