using System;

namespace UnityEngine.Lumin
{
	// Token: 0x02000394 RID: 916
	[AttributeUsage(4, AllowMultiple = true)]
	public sealed class UsesLuminPrivilegeAttribute : Attribute
	{
		// Token: 0x06001EEE RID: 7918 RVA: 0x00032528 File Offset: 0x00030728
		public UsesLuminPrivilegeAttribute(string privilege)
		{
			this.m_Privilege = privilege;
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x0003253C File Offset: 0x0003073C
		public string privilege
		{
			get
			{
				return this.m_Privilege;
			}
		}

		// Token: 0x04000A2E RID: 2606
		private readonly string m_Privilege;
	}
}
