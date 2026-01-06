using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200031A RID: 794
	internal static class Shaders
	{
		// Token: 0x060019BA RID: 6586 RVA: 0x0006AAB0 File Offset: 0x00068CB0
		static Shaders()
		{
			bool isUIEPackageLoaded = UIElementsPackageUtility.IsUIEPackageLoaded;
			if (isUIEPackageLoaded)
			{
				Shaders.k_AtlasBlit = "Hidden/UIE-AtlasBlit";
				Shaders.k_Editor = "Hidden/UIE-Editor";
				Shaders.k_Runtime = "Hidden/UIE-Runtime";
				Shaders.k_RuntimeWorld = "Hidden/UIE-RuntimeWorld";
				Shaders.k_GraphView = "Hidden/UIE-GraphView";
			}
			else
			{
				Shaders.k_AtlasBlit = "Hidden/Internal-UIRAtlasBlitCopy";
				Shaders.k_Editor = "Hidden/UIElements/EditorUIE";
				Shaders.k_Runtime = "Hidden/Internal-UIRDefault";
				Shaders.k_RuntimeWorld = "Hidden/Internal-UIRDefaultWorld";
				Shaders.k_GraphView = "Hidden/GraphView/GraphViewUIE";
			}
		}

		// Token: 0x04000BA0 RID: 2976
		public static readonly string k_AtlasBlit;

		// Token: 0x04000BA1 RID: 2977
		public static readonly string k_Editor;

		// Token: 0x04000BA2 RID: 2978
		public static readonly string k_Runtime;

		// Token: 0x04000BA3 RID: 2979
		public static readonly string k_RuntimeWorld;

		// Token: 0x04000BA4 RID: 2980
		public static readonly string k_GraphView;
	}
}
