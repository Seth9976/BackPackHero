using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200007C RID: 124
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Lens Flare (SRP)")]
	public sealed class LensFlareComponentSRP : MonoBehaviour
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000141FC File Offset: 0x000123FC
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x00014204 File Offset: 0x00012404
		public LensFlareDataSRP lensFlareData
		{
			get
			{
				return this.m_LensFlareData;
			}
			set
			{
				this.m_LensFlareData = value;
				this.OnValidate();
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00014214 File Offset: 0x00012414
		public float celestialProjectedOcclusionRadius(Camera mainCam)
		{
			float num = (float)Math.Tan((double)LensFlareComponentSRP.sCelestialAngularRadius) * mainCam.farClipPlane;
			return this.occlusionRadius * num;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001423D File Offset: 0x0001243D
		private void OnEnable()
		{
			if (this.lensFlareData)
			{
				LensFlareCommonSRP.Instance.AddData(this);
				return;
			}
			LensFlareCommonSRP.Instance.RemoveData(this);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00014263 File Offset: 0x00012463
		private void OnDisable()
		{
			LensFlareCommonSRP.Instance.RemoveData(this);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00014270 File Offset: 0x00012470
		private void OnValidate()
		{
			if (base.isActiveAndEnabled && this.lensFlareData != null)
			{
				LensFlareCommonSRP.Instance.AddData(this);
				return;
			}
			LensFlareCommonSRP.Instance.RemoveData(this);
		}

		// Token: 0x04000269 RID: 617
		[SerializeField]
		private LensFlareDataSRP m_LensFlareData;

		// Token: 0x0400026A RID: 618
		[Min(0f)]
		public float intensity = 1f;

		// Token: 0x0400026B RID: 619
		[Min(1E-05f)]
		public float maxAttenuationDistance = 100f;

		// Token: 0x0400026C RID: 620
		[Min(1E-05f)]
		public float maxAttenuationScale = 100f;

		// Token: 0x0400026D RID: 621
		public AnimationCurve distanceAttenuationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x0400026E RID: 622
		public AnimationCurve scaleByDistanceCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x0400026F RID: 623
		public bool attenuationByLightShape = true;

		// Token: 0x04000270 RID: 624
		public AnimationCurve radialScreenAttenuationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04000271 RID: 625
		public bool useOcclusion;

		// Token: 0x04000272 RID: 626
		[Min(0f)]
		public float occlusionRadius = 0.1f;

		// Token: 0x04000273 RID: 627
		[Range(1f, 64f)]
		public uint sampleCount = 32U;

		// Token: 0x04000274 RID: 628
		public float occlusionOffset = 0.05f;

		// Token: 0x04000275 RID: 629
		[Min(0f)]
		public float scale = 1f;

		// Token: 0x04000276 RID: 630
		public bool allowOffScreen;

		// Token: 0x04000277 RID: 631
		private static float sCelestialAngularRadius = 0.057595868f;
	}
}
