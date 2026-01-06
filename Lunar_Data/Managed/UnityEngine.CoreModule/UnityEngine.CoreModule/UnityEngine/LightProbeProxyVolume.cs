using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000186 RID: 390
	[NativeHeader("Runtime/Camera/LightProbeProxyVolume.h")]
	public sealed class LightProbeProxyVolume : Behaviour
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000EA7 RID: 3751
		public static extern bool isFeatureSupported
		{
			[NativeName("IsFeatureSupported")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00012C20 File Offset: 0x00010E20
		[NativeName("GlobalAABB")]
		public Bounds boundsGlobal
		{
			get
			{
				Bounds bounds;
				this.get_boundsGlobal_Injected(out bounds);
				return bounds;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00012C38 File Offset: 0x00010E38
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x00012C4E File Offset: 0x00010E4E
		[NativeName("BoundingBoxSizeCustom")]
		public Vector3 sizeCustom
		{
			get
			{
				Vector3 vector;
				this.get_sizeCustom_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_sizeCustom_Injected(ref value);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00012C58 File Offset: 0x00010E58
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x00012C6E File Offset: 0x00010E6E
		[NativeName("BoundingBoxOriginCustom")]
		public Vector3 originCustom
		{
			get
			{
				Vector3 vector;
				this.get_originCustom_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_originCustom_Injected(ref value);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000EAD RID: 3757
		// (set) Token: 0x06000EAE RID: 3758
		public extern float probeDensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000EAF RID: 3759
		// (set) Token: 0x06000EB0 RID: 3760
		public extern int gridResolutionX
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000EB1 RID: 3761
		// (set) Token: 0x06000EB2 RID: 3762
		public extern int gridResolutionY
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000EB3 RID: 3763
		// (set) Token: 0x06000EB4 RID: 3764
		public extern int gridResolutionZ
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000EB5 RID: 3765
		// (set) Token: 0x06000EB6 RID: 3766
		public extern LightProbeProxyVolume.BoundingBoxMode boundingBoxMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000EB7 RID: 3767
		// (set) Token: 0x06000EB8 RID: 3768
		public extern LightProbeProxyVolume.ResolutionMode resolutionMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000EB9 RID: 3769
		// (set) Token: 0x06000EBA RID: 3770
		public extern LightProbeProxyVolume.ProbePositionMode probePositionMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000EBB RID: 3771
		// (set) Token: 0x06000EBC RID: 3772
		public extern LightProbeProxyVolume.RefreshMode refreshMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000EBD RID: 3773
		// (set) Token: 0x06000EBE RID: 3774
		public extern LightProbeProxyVolume.QualityMode qualityMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000EBF RID: 3775
		// (set) Token: 0x06000EC0 RID: 3776
		public extern LightProbeProxyVolume.DataFormat dataFormat
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00012C78 File Offset: 0x00010E78
		public void Update()
		{
			this.SetDirtyFlag(true);
		}

		// Token: 0x06000EC2 RID: 3778
		[MethodImpl(4096)]
		private extern void SetDirtyFlag(bool flag);

		// Token: 0x06000EC4 RID: 3780
		[MethodImpl(4096)]
		private extern void get_boundsGlobal_Injected(out Bounds ret);

		// Token: 0x06000EC5 RID: 3781
		[MethodImpl(4096)]
		private extern void get_sizeCustom_Injected(out Vector3 ret);

		// Token: 0x06000EC6 RID: 3782
		[MethodImpl(4096)]
		private extern void set_sizeCustom_Injected(ref Vector3 value);

		// Token: 0x06000EC7 RID: 3783
		[MethodImpl(4096)]
		private extern void get_originCustom_Injected(out Vector3 ret);

		// Token: 0x06000EC8 RID: 3784
		[MethodImpl(4096)]
		private extern void set_originCustom_Injected(ref Vector3 value);

		// Token: 0x02000187 RID: 391
		public enum ResolutionMode
		{
			// Token: 0x04000573 RID: 1395
			Automatic,
			// Token: 0x04000574 RID: 1396
			Custom
		}

		// Token: 0x02000188 RID: 392
		public enum BoundingBoxMode
		{
			// Token: 0x04000576 RID: 1398
			AutomaticLocal,
			// Token: 0x04000577 RID: 1399
			AutomaticWorld,
			// Token: 0x04000578 RID: 1400
			Custom
		}

		// Token: 0x02000189 RID: 393
		public enum ProbePositionMode
		{
			// Token: 0x0400057A RID: 1402
			CellCorner,
			// Token: 0x0400057B RID: 1403
			CellCenter
		}

		// Token: 0x0200018A RID: 394
		public enum RefreshMode
		{
			// Token: 0x0400057D RID: 1405
			Automatic,
			// Token: 0x0400057E RID: 1406
			EveryFrame,
			// Token: 0x0400057F RID: 1407
			ViaScripting
		}

		// Token: 0x0200018B RID: 395
		public enum QualityMode
		{
			// Token: 0x04000581 RID: 1409
			Low,
			// Token: 0x04000582 RID: 1410
			Normal
		}

		// Token: 0x0200018C RID: 396
		public enum DataFormat
		{
			// Token: 0x04000584 RID: 1412
			HalfFloat,
			// Token: 0x04000585 RID: 1413
			Float
		}
	}
}
