using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000082 RID: 130
	public abstract class RenderPipelineResources : ScriptableObject
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00014780 File Offset: 0x00012980
		protected virtual string packagePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00014783 File Offset: 0x00012983
		internal string packagePath_Internal
		{
			get
			{
				return this.packagePath;
			}
		}
	}
}
