using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000086 RID: 134
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
	public class GenerateHLSL : Attribute
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x00014794 File Offset: 0x00012994
		public GenerateHLSL(PackingRules rules = PackingRules.Exact, bool needAccessors = true, bool needSetters = false, bool needParamDebug = false, int paramDefinesStart = 1, bool omitStructDeclaration = false, bool containsPackedFields = false, bool generateCBuffer = false, int constantRegister = -1, [CallerFilePath] string sourcePath = null)
		{
			this.sourcePath = sourcePath;
			this.packingRules = rules;
			this.needAccessors = needAccessors;
			this.needSetters = needSetters;
			this.needParamDebug = needParamDebug;
			this.paramDefinesStart = paramDefinesStart;
			this.omitStructDeclaration = omitStructDeclaration;
			this.containsPackedFields = containsPackedFields;
			this.generateCBuffer = generateCBuffer;
			this.constantRegister = constantRegister;
		}

		// Token: 0x040002BA RID: 698
		public PackingRules packingRules;

		// Token: 0x040002BB RID: 699
		public bool containsPackedFields;

		// Token: 0x040002BC RID: 700
		public bool needAccessors;

		// Token: 0x040002BD RID: 701
		public bool needSetters;

		// Token: 0x040002BE RID: 702
		public bool needParamDebug;

		// Token: 0x040002BF RID: 703
		public int paramDefinesStart;

		// Token: 0x040002C0 RID: 704
		public bool omitStructDeclaration;

		// Token: 0x040002C1 RID: 705
		public bool generateCBuffer;

		// Token: 0x040002C2 RID: 706
		public int constantRegister;

		// Token: 0x040002C3 RID: 707
		public string sourcePath;
	}
}
