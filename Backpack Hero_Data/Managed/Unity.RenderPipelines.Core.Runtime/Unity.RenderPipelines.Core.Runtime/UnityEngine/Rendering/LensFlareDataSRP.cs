using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000081 RID: 129
	[Serializable]
	public sealed class LensFlareDataSRP : ScriptableObject
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x00014771 File Offset: 0x00012971
		public LensFlareDataSRP()
		{
			this.elements = null;
		}

		// Token: 0x040002AD RID: 685
		public LensFlareDataElementSRP[] elements;
	}
}
