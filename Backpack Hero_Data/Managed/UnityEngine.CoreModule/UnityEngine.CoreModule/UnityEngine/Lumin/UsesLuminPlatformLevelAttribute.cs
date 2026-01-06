using System;

namespace UnityEngine.Lumin
{
	// Token: 0x02000393 RID: 915
	[AttributeUsage(4, AllowMultiple = false)]
	public sealed class UsesLuminPlatformLevelAttribute : Attribute
	{
		// Token: 0x06001EEC RID: 7916 RVA: 0x000324FF File Offset: 0x000306FF
		public UsesLuminPlatformLevelAttribute(uint platformLevel)
		{
			this.m_PlatformLevel = platformLevel;
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x00032510 File Offset: 0x00030710
		public uint platformLevel
		{
			get
			{
				return this.m_PlatformLevel;
			}
		}

		// Token: 0x04000A2D RID: 2605
		private readonly uint m_PlatformLevel;
	}
}
