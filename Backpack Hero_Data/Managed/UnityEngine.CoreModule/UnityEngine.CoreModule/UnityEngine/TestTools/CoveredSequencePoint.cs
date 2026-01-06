using System;
using System.Reflection;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	// Token: 0x0200048B RID: 1163
	[NativeType(CodegenOptions.Custom, "ManagedCoveredSequencePoint", Header = "Runtime/Scripting/ScriptingCoverage.bindings.h")]
	public struct CoveredSequencePoint
	{
		// Token: 0x04000F9E RID: 3998
		public MethodBase method;

		// Token: 0x04000F9F RID: 3999
		public uint ilOffset;

		// Token: 0x04000FA0 RID: 4000
		public uint hitCount;

		// Token: 0x04000FA1 RID: 4001
		public string filename;

		// Token: 0x04000FA2 RID: 4002
		public uint line;

		// Token: 0x04000FA3 RID: 4003
		public uint column;
	}
}
