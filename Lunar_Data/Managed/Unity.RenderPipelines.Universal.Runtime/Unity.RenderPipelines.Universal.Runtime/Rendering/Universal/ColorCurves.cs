using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000086 RID: 134
	[VolumeComponentMenuForRenderPipeline("Post-processing/Color Curves", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class ColorCurves : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x0001CDD5 File Offset: 0x0001AFD5
		public bool IsActive()
		{
			return true;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001CDD8 File Offset: 0x0001AFD8
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001CDDC File Offset: 0x0001AFDC
		public ColorCurves()
		{
			Keyframe[] array = new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			};
			float num = 0f;
			bool flag = false;
			Vector2 vector = new Vector2(0f, 1f);
			this.master = new TextureCurveParameter(new TextureCurve(array, num, flag, in vector), false);
			Keyframe[] array2 = new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			};
			float num2 = 0f;
			bool flag2 = false;
			vector = new Vector2(0f, 1f);
			this.red = new TextureCurveParameter(new TextureCurve(array2, num2, flag2, in vector), false);
			Keyframe[] array3 = new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			};
			float num3 = 0f;
			bool flag3 = false;
			vector = new Vector2(0f, 1f);
			this.green = new TextureCurveParameter(new TextureCurve(array3, num3, flag3, in vector), false);
			Keyframe[] array4 = new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			};
			float num4 = 0f;
			bool flag4 = false;
			vector = new Vector2(0f, 1f);
			this.blue = new TextureCurveParameter(new TextureCurve(array4, num4, flag4, in vector), false);
			Keyframe[] array5 = new Keyframe[0];
			float num5 = 0.5f;
			bool flag5 = true;
			vector = new Vector2(0f, 1f);
			this.hueVsHue = new TextureCurveParameter(new TextureCurve(array5, num5, flag5, in vector), false);
			Keyframe[] array6 = new Keyframe[0];
			float num6 = 0.5f;
			bool flag6 = true;
			vector = new Vector2(0f, 1f);
			this.hueVsSat = new TextureCurveParameter(new TextureCurve(array6, num6, flag6, in vector), false);
			Keyframe[] array7 = new Keyframe[0];
			float num7 = 0.5f;
			bool flag7 = false;
			vector = new Vector2(0f, 1f);
			this.satVsSat = new TextureCurveParameter(new TextureCurve(array7, num7, flag7, in vector), false);
			Keyframe[] array8 = new Keyframe[0];
			float num8 = 0.5f;
			bool flag8 = false;
			vector = new Vector2(0f, 1f);
			this.lumVsSat = new TextureCurveParameter(new TextureCurve(array8, num8, flag8, in vector), false);
			base..ctor();
		}

		// Token: 0x04000380 RID: 896
		public TextureCurveParameter master;

		// Token: 0x04000381 RID: 897
		public TextureCurveParameter red;

		// Token: 0x04000382 RID: 898
		public TextureCurveParameter green;

		// Token: 0x04000383 RID: 899
		public TextureCurveParameter blue;

		// Token: 0x04000384 RID: 900
		public TextureCurveParameter hueVsHue;

		// Token: 0x04000385 RID: 901
		public TextureCurveParameter hueVsSat;

		// Token: 0x04000386 RID: 902
		public TextureCurveParameter satVsSat;

		// Token: 0x04000387 RID: 903
		public TextureCurveParameter lumVsSat;
	}
}
