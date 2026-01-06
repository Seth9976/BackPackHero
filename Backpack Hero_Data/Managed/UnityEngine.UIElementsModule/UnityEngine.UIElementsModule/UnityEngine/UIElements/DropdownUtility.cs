using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000026 RID: 38
	internal static class DropdownUtility
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x000053C8 File Offset: 0x000035C8
		internal static IGenericMenu CreateDropdown()
		{
			IGenericMenu genericMenu2;
			if (DropdownUtility.MakeDropdownFunc == null)
			{
				IGenericMenu genericMenu = new GenericDropdownMenu();
				genericMenu2 = genericMenu;
			}
			else
			{
				genericMenu2 = DropdownUtility.MakeDropdownFunc.Invoke();
			}
			return genericMenu2;
		}

		// Token: 0x04000068 RID: 104
		internal static Func<IGenericMenu> MakeDropdownFunc;
	}
}
