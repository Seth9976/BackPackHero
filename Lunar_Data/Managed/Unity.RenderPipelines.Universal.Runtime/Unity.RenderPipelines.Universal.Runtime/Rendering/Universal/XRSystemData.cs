using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	public class XRSystemData : ScriptableObject
	{
		// Token: 0x04000220 RID: 544
		public XRSystemData.ShaderResources shaders;

		// Token: 0x02000152 RID: 338
		[ReloadGroup]
		[Serializable]
		public sealed class ShaderResources
		{
			// Token: 0x040008EB RID: 2283
			[Reload("Shaders/XR/XROcclusionMesh.shader", ReloadAttribute.Package.Root)]
			public Shader xrOcclusionMeshPS;

			// Token: 0x040008EC RID: 2284
			[Reload("Shaders/XR/XRMirrorView.shader", ReloadAttribute.Package.Root)]
			public Shader xrMirrorViewPS;
		}
	}
}
