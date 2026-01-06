using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200008F RID: 143
	[VolumeComponentMenuForRenderPipeline("Post-processing/Lift, Gamma, Gain", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class LiftGammaGain : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x0001D454 File Offset: 0x0001B654
		public bool IsActive()
		{
			Vector4 vector = new Vector4(1f, 1f, 1f, 0f);
			return this.lift != vector || this.gamma != vector || this.gain != vector;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001D4A6 File Offset: 0x0001B6A6
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x040003AE RID: 942
		[Tooltip("Use this to control and apply a hue to the dark tones. This has a more exaggerated effect on shadows.")]
		public Vector4Parameter lift = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x040003AF RID: 943
		[Tooltip("Use this to control and apply a hue to the mid-range tones with a power function.")]
		public Vector4Parameter gamma = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x040003B0 RID: 944
		[Tooltip("Use this to increase and apply a hue to the signal and make highlights brighter.")]
		public Vector4Parameter gain = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);
	}
}
