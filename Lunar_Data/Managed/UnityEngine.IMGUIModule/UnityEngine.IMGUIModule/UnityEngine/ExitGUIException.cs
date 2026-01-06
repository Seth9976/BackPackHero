using System;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	public sealed class ExitGUIException : Exception
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000C998 File Offset: 0x0000AB98
		public ExitGUIException()
		{
			GUIUtility.guiIsExiting = true;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000C9A9 File Offset: 0x0000ABA9
		internal ExitGUIException(string message)
			: base(message)
		{
			GUIUtility.guiIsExiting = true;
		}
	}
}
