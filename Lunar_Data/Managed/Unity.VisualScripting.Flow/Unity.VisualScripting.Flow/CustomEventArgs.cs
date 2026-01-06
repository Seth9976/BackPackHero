using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000058 RID: 88
	public struct CustomEventArgs
	{
		// Token: 0x0600035D RID: 861 RVA: 0x000089B7 File Offset: 0x00006BB7
		public CustomEventArgs(string name, params object[] arguments)
		{
			this.name = name;
			this.arguments = arguments;
		}

		// Token: 0x040000FE RID: 254
		public readonly string name;

		// Token: 0x040000FF RID: 255
		public readonly object[] arguments;
	}
}
