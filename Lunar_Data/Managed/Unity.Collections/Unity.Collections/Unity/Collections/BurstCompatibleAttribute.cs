using System;

namespace Unity.Collections
{
	// Token: 0x0200003A RID: 58
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
	public class BurstCompatibleAttribute : Attribute
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000041C2 File Offset: 0x000023C2
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000041CA File Offset: 0x000023CA
		public Type[] GenericTypeArguments { get; set; }

		// Token: 0x04000073 RID: 115
		public string RequiredUnityDefine;

		// Token: 0x04000074 RID: 116
		public BurstCompatibleAttribute.BurstCompatibleCompileTarget CompileTarget;

		// Token: 0x0200003B RID: 59
		public enum BurstCompatibleCompileTarget
		{
			// Token: 0x04000076 RID: 118
			Player,
			// Token: 0x04000077 RID: 119
			Editor,
			// Token: 0x04000078 RID: 120
			PlayerAndEditor
		}
	}
}
