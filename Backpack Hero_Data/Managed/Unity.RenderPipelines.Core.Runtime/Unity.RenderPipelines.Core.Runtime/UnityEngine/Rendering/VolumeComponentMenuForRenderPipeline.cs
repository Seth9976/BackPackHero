using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B8 RID: 184
	public class VolumeComponentMenuForRenderPipeline : VolumeComponentMenu
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001CCD3 File Offset: 0x0001AED3
		public Type[] pipelineTypes { get; }

		// Token: 0x06000619 RID: 1561 RVA: 0x0001CCDC File Offset: 0x0001AEDC
		public VolumeComponentMenuForRenderPipeline(string menu, params Type[] pipelineTypes)
			: base(menu)
		{
			if (pipelineTypes == null)
			{
				throw new Exception("Specify a list of supported pipeline");
			}
			foreach (Type type in pipelineTypes)
			{
				if (!typeof(RenderPipeline).IsAssignableFrom(type))
				{
					throw new Exception(string.Format("You can only specify types that inherit from {0}, please check {1}", typeof(RenderPipeline), type));
				}
			}
			this.pipelineTypes = pipelineTypes;
		}
	}
}
