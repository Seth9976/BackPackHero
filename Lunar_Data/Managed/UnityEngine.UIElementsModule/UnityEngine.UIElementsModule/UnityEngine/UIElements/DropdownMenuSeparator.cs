using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000020 RID: 32
	public class DropdownMenuSeparator : DropdownMenuItem
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004F9B File Offset: 0x0000319B
		public string subMenuPath { get; }

		// Token: 0x060000D5 RID: 213 RVA: 0x00004FA3 File Offset: 0x000031A3
		public DropdownMenuSeparator(string subMenuPath)
		{
			this.subMenuPath = subMenuPath;
		}
	}
}
