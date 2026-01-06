using System;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
	// Token: 0x02000008 RID: 8
	public class ExtensionSlot
	{
		// Token: 0x0600003F RID: 63 RVA: 0x0000225C File Offset: 0x0000045C
		public ExtensionSlot(ExtensionType type, string slot, UserExtensionState userExtensionState)
		{
			this.Type = type;
			this.Slot = slot;
			this.UserExtensionState = userExtensionState;
		}

		// Token: 0x0400001D RID: 29
		public ExtensionType Type;

		// Token: 0x0400001E RID: 30
		public string Slot;

		// Token: 0x0400001F RID: 31
		public UserExtensionState UserExtensionState;
	}
}
