using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003F3 RID: 1011
	[UsedByNativeCode]
	public struct ScriptableCullingParameters : IEquatable<ScriptableCullingParameters>
	{
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x00039258 File Offset: 0x00037458
		// (set) Token: 0x0600223B RID: 8763 RVA: 0x00039270 File Offset: 0x00037470
		public int maximumVisibleLights
		{
			get
			{
				return this.m_maximumVisibleLights;
			}
			set
			{
				this.m_maximumVisibleLights = value;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0003927C File Offset: 0x0003747C
		// (set) Token: 0x0600223D RID: 8765 RVA: 0x00039294 File Offset: 0x00037494
		public bool conservativeEnclosingSphere
		{
			get
			{
				return this.m_ConservativeEnclosingSphere;
			}
			set
			{
				this.m_ConservativeEnclosingSphere = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x000392A0 File Offset: 0x000374A0
		// (set) Token: 0x0600223F RID: 8767 RVA: 0x000392B8 File Offset: 0x000374B8
		public int numIterationsEnclosingSphere
		{
			get
			{
				return this.m_NumIterationsEnclosingSphere;
			}
			set
			{
				this.m_NumIterationsEnclosingSphere = value;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x000392C4 File Offset: 0x000374C4
		// (set) Token: 0x06002241 RID: 8769 RVA: 0x000392DC File Offset: 0x000374DC
		public int cullingPlaneCount
		{
			get
			{
				return this.m_CullingPlaneCount;
			}
			set
			{
				bool flag = value < 0 || value > 10;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "value", value, 10));
				}
				this.m_CullingPlaneCount = value;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x00039324 File Offset: 0x00037524
		// (set) Token: 0x06002243 RID: 8771 RVA: 0x00039341 File Offset: 0x00037541
		public bool isOrthographic
		{
			get
			{
				return Convert.ToBoolean(this.m_IsOrthographic);
			}
			set
			{
				this.m_IsOrthographic = Convert.ToInt32(value);
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x00039350 File Offset: 0x00037550
		// (set) Token: 0x06002245 RID: 8773 RVA: 0x00039368 File Offset: 0x00037568
		public LODParameters lodParameters
		{
			get
			{
				return this.m_LODParameters;
			}
			set
			{
				this.m_LODParameters = value;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x00039374 File Offset: 0x00037574
		// (set) Token: 0x06002247 RID: 8775 RVA: 0x0003938C File Offset: 0x0003758C
		public uint cullingMask
		{
			get
			{
				return this.m_CullingMask;
			}
			set
			{
				this.m_CullingMask = value;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x00039398 File Offset: 0x00037598
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x000393B0 File Offset: 0x000375B0
		public Matrix4x4 cullingMatrix
		{
			get
			{
				return this.m_CullingMatrix;
			}
			set
			{
				this.m_CullingMatrix = value;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x000393BC File Offset: 0x000375BC
		// (set) Token: 0x0600224B RID: 8779 RVA: 0x000393D4 File Offset: 0x000375D4
		public Vector3 origin
		{
			get
			{
				return this.m_Origin;
			}
			set
			{
				this.m_Origin = value;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x000393E0 File Offset: 0x000375E0
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x000393F8 File Offset: 0x000375F8
		public float shadowDistance
		{
			get
			{
				return this.m_ShadowDistance;
			}
			set
			{
				this.m_ShadowDistance = value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x00039404 File Offset: 0x00037604
		// (set) Token: 0x0600224F RID: 8783 RVA: 0x0003941C File Offset: 0x0003761C
		public float shadowNearPlaneOffset
		{
			get
			{
				return this.m_ShadowNearPlaneOffset;
			}
			set
			{
				this.m_ShadowNearPlaneOffset = value;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x00039428 File Offset: 0x00037628
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x00039440 File Offset: 0x00037640
		public CullingOptions cullingOptions
		{
			get
			{
				return this.m_CullingOptions;
			}
			set
			{
				this.m_CullingOptions = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x0003944C File Offset: 0x0003764C
		// (set) Token: 0x06002253 RID: 8787 RVA: 0x00039464 File Offset: 0x00037664
		public ReflectionProbeSortingCriteria reflectionProbeSortingCriteria
		{
			get
			{
				return this.m_ReflectionProbeSortingCriteria;
			}
			set
			{
				this.m_ReflectionProbeSortingCriteria = value;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x00039470 File Offset: 0x00037670
		// (set) Token: 0x06002255 RID: 8789 RVA: 0x00039488 File Offset: 0x00037688
		public CameraProperties cameraProperties
		{
			get
			{
				return this.m_CameraProperties;
			}
			set
			{
				this.m_CameraProperties = value;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x00039494 File Offset: 0x00037694
		// (set) Token: 0x06002257 RID: 8791 RVA: 0x000394AC File Offset: 0x000376AC
		public Matrix4x4 stereoViewMatrix
		{
			get
			{
				return this.m_StereoViewMatrix;
			}
			set
			{
				this.m_StereoViewMatrix = value;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x000394B8 File Offset: 0x000376B8
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x000394D0 File Offset: 0x000376D0
		public Matrix4x4 stereoProjectionMatrix
		{
			get
			{
				return this.m_StereoProjectionMatrix;
			}
			set
			{
				this.m_StereoProjectionMatrix = value;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x000394DC File Offset: 0x000376DC
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x000394F4 File Offset: 0x000376F4
		public float stereoSeparationDistance
		{
			get
			{
				return this.m_StereoSeparationDistance;
			}
			set
			{
				this.m_StereoSeparationDistance = value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x00039500 File Offset: 0x00037700
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x00039518 File Offset: 0x00037718
		public float accurateOcclusionThreshold
		{
			get
			{
				return this.m_AccurateOcclusionThreshold;
			}
			set
			{
				this.m_AccurateOcclusionThreshold = Mathf.Max(-1f, value);
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x0003952C File Offset: 0x0003772C
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x00039544 File Offset: 0x00037744
		public int maximumPortalCullingJobs
		{
			get
			{
				return this.m_MaximumPortalCullingJobs;
			}
			set
			{
				bool flag = value < 1 || value > 16;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be in range {2} to {3}", new object[] { "maximumPortalCullingJobs", this.maximumPortalCullingJobs, 1, 16 }));
				}
				this.m_MaximumPortalCullingJobs = value;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x000395A8 File Offset: 0x000377A8
		public static int cullingJobsLowerLimit
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x000395BC File Offset: 0x000377BC
		public static int cullingJobsUpperLimit
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000395D0 File Offset: 0x000377D0
		public unsafe float GetLayerCullingDistance(int layerIndex)
		{
			bool flag = layerIndex < 0 || layerIndex >= 32;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "layerIndex", layerIndex, 32));
			}
			fixed (float* ptr = &this.m_LayerFarCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				return ptr2[layerIndex];
			}
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x00039630 File Offset: 0x00037830
		public unsafe void SetLayerCullingDistance(int layerIndex, float distance)
		{
			bool flag = layerIndex < 0 || layerIndex >= 32;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "layerIndex", layerIndex, 32));
			}
			fixed (float* ptr = &this.m_LayerFarCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				ptr2[layerIndex] = distance;
			}
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x00039690 File Offset: 0x00037890
		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, this.cullingPlaneCount));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x00039704 File Offset: 0x00037904
		public unsafe void SetCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, this.cullingPlaneCount));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x00039778 File Offset: 0x00037978
		public bool Equals(ScriptableCullingParameters other)
		{
			for (int i = 0; i < 32; i++)
			{
				bool flag = !this.GetLayerCullingDistance(i).Equals(other.GetLayerCullingDistance(i));
				if (flag)
				{
					return false;
				}
			}
			for (int j = 0; j < this.cullingPlaneCount; j++)
			{
				bool flag2 = !this.GetCullingPlane(j).Equals(other.GetCullingPlane(j));
				if (flag2)
				{
					return false;
				}
			}
			return this.m_IsOrthographic == other.m_IsOrthographic && this.m_LODParameters.Equals(other.m_LODParameters) && this.m_CullingPlaneCount == other.m_CullingPlaneCount && this.m_CullingMask == other.m_CullingMask && this.m_SceneMask == other.m_SceneMask && this.m_LayerCull == other.m_LayerCull && this.m_CullingMatrix.Equals(other.m_CullingMatrix) && this.m_Origin.Equals(other.m_Origin) && this.m_ShadowDistance.Equals(other.m_ShadowDistance) && this.m_ShadowNearPlaneOffset.Equals(other.m_ShadowNearPlaneOffset) && this.m_CullingOptions == other.m_CullingOptions && this.m_ReflectionProbeSortingCriteria == other.m_ReflectionProbeSortingCriteria && this.m_CameraProperties.Equals(other.m_CameraProperties) && this.m_AccurateOcclusionThreshold.Equals(other.m_AccurateOcclusionThreshold) && this.m_StereoViewMatrix.Equals(other.m_StereoViewMatrix) && this.m_StereoProjectionMatrix.Equals(other.m_StereoProjectionMatrix) && this.m_StereoSeparationDistance.Equals(other.m_StereoSeparationDistance) && this.m_maximumVisibleLights == other.m_maximumVisibleLights && this.m_ConservativeEnclosingSphere == other.m_ConservativeEnclosingSphere && this.m_NumIterationsEnclosingSphere == other.m_NumIterationsEnclosingSphere;
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x00039988 File Offset: 0x00037B88
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableCullingParameters && this.Equals((ScriptableCullingParameters)obj);
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000399C0 File Offset: 0x00037BC0
		public override int GetHashCode()
		{
			int num = this.m_IsOrthographic;
			num = (num * 397) ^ this.m_LODParameters.GetHashCode();
			num = (num * 397) ^ this.m_CullingPlaneCount;
			num = (num * 397) ^ (int)this.m_CullingMask;
			num = (num * 397) ^ this.m_SceneMask.GetHashCode();
			num = (num * 397) ^ this.m_LayerCull;
			num = (num * 397) ^ this.m_CullingMatrix.GetHashCode();
			num = (num * 397) ^ this.m_Origin.GetHashCode();
			num = (num * 397) ^ this.m_ShadowDistance.GetHashCode();
			num = (num * 397) ^ this.m_ShadowNearPlaneOffset.GetHashCode();
			num = (num * 397) ^ (int)this.m_CullingOptions;
			num = (num * 397) ^ (int)this.m_ReflectionProbeSortingCriteria;
			num = (num * 397) ^ this.m_CameraProperties.GetHashCode();
			num = (num * 397) ^ this.m_AccurateOcclusionThreshold.GetHashCode();
			num = (num * 397) ^ this.m_MaximumPortalCullingJobs.GetHashCode();
			num = (num * 397) ^ this.m_StereoViewMatrix.GetHashCode();
			num = (num * 397) ^ this.m_StereoProjectionMatrix.GetHashCode();
			num = (num * 397) ^ this.m_StereoSeparationDistance.GetHashCode();
			num = (num * 397) ^ this.m_maximumVisibleLights;
			num = (num * 397) ^ this.m_ConservativeEnclosingSphere.GetHashCode();
			return (num * 397) ^ this.m_NumIterationsEnclosingSphere.GetHashCode();
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x00039B74 File Offset: 0x00037D74
		public static bool operator ==(ScriptableCullingParameters left, ScriptableCullingParameters right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x00039B90 File Offset: 0x00037D90
		public static bool operator !=(ScriptableCullingParameters left, ScriptableCullingParameters right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000CA4 RID: 3236
		private int m_IsOrthographic;

		// Token: 0x04000CA5 RID: 3237
		private LODParameters m_LODParameters;

		// Token: 0x04000CA6 RID: 3238
		private const int k_MaximumCullingPlaneCount = 10;

		// Token: 0x04000CA7 RID: 3239
		public static readonly int maximumCullingPlaneCount = 10;

		// Token: 0x04000CA8 RID: 3240
		[FixedBuffer(typeof(byte), 160)]
		internal ScriptableCullingParameters.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		// Token: 0x04000CA9 RID: 3241
		private int m_CullingPlaneCount;

		// Token: 0x04000CAA RID: 3242
		private uint m_CullingMask;

		// Token: 0x04000CAB RID: 3243
		private ulong m_SceneMask;

		// Token: 0x04000CAC RID: 3244
		private const int k_LayerCount = 32;

		// Token: 0x04000CAD RID: 3245
		public static readonly int layerCount = 32;

		// Token: 0x04000CAE RID: 3246
		[FixedBuffer(typeof(float), 32)]
		internal ScriptableCullingParameters.<m_LayerFarCullDistances>e__FixedBuffer m_LayerFarCullDistances;

		// Token: 0x04000CAF RID: 3247
		private int m_LayerCull;

		// Token: 0x04000CB0 RID: 3248
		private Matrix4x4 m_CullingMatrix;

		// Token: 0x04000CB1 RID: 3249
		private Vector3 m_Origin;

		// Token: 0x04000CB2 RID: 3250
		private float m_ShadowDistance;

		// Token: 0x04000CB3 RID: 3251
		private float m_ShadowNearPlaneOffset;

		// Token: 0x04000CB4 RID: 3252
		private CullingOptions m_CullingOptions;

		// Token: 0x04000CB5 RID: 3253
		private ReflectionProbeSortingCriteria m_ReflectionProbeSortingCriteria;

		// Token: 0x04000CB6 RID: 3254
		private CameraProperties m_CameraProperties;

		// Token: 0x04000CB7 RID: 3255
		private float m_AccurateOcclusionThreshold;

		// Token: 0x04000CB8 RID: 3256
		private int m_MaximumPortalCullingJobs;

		// Token: 0x04000CB9 RID: 3257
		private const int k_CullingJobCountLowerLimit = 1;

		// Token: 0x04000CBA RID: 3258
		private const int k_CullingJobCountUpperLimit = 16;

		// Token: 0x04000CBB RID: 3259
		private Matrix4x4 m_StereoViewMatrix;

		// Token: 0x04000CBC RID: 3260
		private Matrix4x4 m_StereoProjectionMatrix;

		// Token: 0x04000CBD RID: 3261
		private float m_StereoSeparationDistance;

		// Token: 0x04000CBE RID: 3262
		private int m_maximumVisibleLights;

		// Token: 0x04000CBF RID: 3263
		private bool m_ConservativeEnclosingSphere;

		// Token: 0x04000CC0 RID: 3264
		private int m_NumIterationsEnclosingSphere;

		// Token: 0x020003F4 RID: 1012
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			// Token: 0x04000CC1 RID: 3265
			public byte FixedElementField;
		}

		// Token: 0x020003F5 RID: 1013
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(0, Size = 128)]
		public struct <m_LayerFarCullDistances>e__FixedBuffer
		{
			// Token: 0x04000CC2 RID: 3266
			public float FixedElementField;
		}
	}
}
