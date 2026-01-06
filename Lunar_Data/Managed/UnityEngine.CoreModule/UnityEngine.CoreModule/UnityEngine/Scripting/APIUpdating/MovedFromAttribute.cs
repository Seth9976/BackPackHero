using System;

namespace UnityEngine.Scripting.APIUpdating
{
	// Token: 0x020002DE RID: 734
	[AttributeUsage(5148)]
	public class MovedFromAttribute : Attribute
	{
		// Token: 0x06001DF7 RID: 7671 RVA: 0x00030AE0 File Offset: 0x0002ECE0
		public MovedFromAttribute(bool autoUpdateAPI, string sourceNamespace = null, string sourceAssembly = null, string sourceClassName = null)
		{
			this.data.Set(autoUpdateAPI, sourceNamespace, sourceAssembly, sourceClassName);
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00030AFB File Offset: 0x0002ECFB
		public MovedFromAttribute(string sourceNamespace)
		{
			this.data.Set(true, sourceNamespace, null, null);
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x00030B18 File Offset: 0x0002ED18
		internal bool AffectsAPIUpdater
		{
			get
			{
				return !this.data.classHasChanged && !this.data.assemblyHasChanged;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x00030B48 File Offset: 0x0002ED48
		public bool IsInDifferentAssembly
		{
			get
			{
				return this.data.assemblyHasChanged;
			}
		}

		// Token: 0x040009D7 RID: 2519
		internal MovedFromAttributeData data;
	}
}
