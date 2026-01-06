using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000051 RID: 81
	public class DynamicResolutionHandler
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000E080 File Offset: 0x0000C280
		private void Reset()
		{
			this.m_Enabled = false;
			this.m_UseMipBias = false;
			this.m_MinScreenFraction = 1f;
			this.m_MaxScreenFraction = 1f;
			this.m_CurrentFraction = 1f;
			this.m_ForcingRes = false;
			this.m_CurrentCameraRequest = true;
			this.m_PrevFraction = -1f;
			this.m_ForceSoftwareFallback = false;
			this.m_RunUpscalerFilterOnFullResolution = false;
			this.m_PrevHWScaleWidth = 1f;
			this.m_PrevHWScaleHeight = 1f;
			this.m_LastScaledSize = new Vector2Int(0, 0);
			this.filter = DynamicResUpscaleFilter.CatmullRom;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000E10D File Offset: 0x0000C30D
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000E115 File Offset: 0x0000C315
		public DynamicResUpscaleFilter filter { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000E11E File Offset: 0x0000C31E
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000E126 File Offset: 0x0000C326
		public Vector2Int finalViewport { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000E138 File Offset: 0x0000C338
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000E12F File Offset: 0x0000C32F
		public bool runUpscalerFilterOnFullResolution
		{
			get
			{
				return this.m_RunUpscalerFilterOnFullResolution || this.filter == DynamicResUpscaleFilter.EdgeAdaptiveScalingUpres;
			}
			set
			{
				this.m_RunUpscalerFilterOnFullResolution = value;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000E150 File Offset: 0x0000C350
		private bool FlushScalableBufferManagerState()
		{
			if (DynamicResolutionHandler.s_GlobalHwUpresActive == this.HardwareDynamicResIsEnabled() && DynamicResolutionHandler.s_GlobalHwFraction == this.m_CurrentFraction)
			{
				return false;
			}
			DynamicResolutionHandler.s_GlobalHwUpresActive = this.HardwareDynamicResIsEnabled();
			DynamicResolutionHandler.s_GlobalHwFraction = this.m_CurrentFraction;
			float num = (DynamicResolutionHandler.s_GlobalHwUpresActive ? DynamicResolutionHandler.s_GlobalHwFraction : 1f);
			ScalableBufferManager.ResizeBuffers(num, num);
			return true;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000E1AC File Offset: 0x0000C3AC
		private static DynamicResolutionHandler GetOrCreateDrsInstanceHandler(Camera camera)
		{
			if (camera == null)
			{
				return null;
			}
			DynamicResolutionHandler dynamicResolutionHandler = null;
			int instanceID = camera.GetInstanceID();
			if (!DynamicResolutionHandler.s_CameraInstances.TryGetValue(instanceID, out dynamicResolutionHandler))
			{
				if (DynamicResolutionHandler.s_CameraInstances.Count >= 32)
				{
					int num = 0;
					DynamicResolutionHandler dynamicResolutionHandler2 = null;
					foreach (KeyValuePair<int, DynamicResolutionHandler> keyValuePair in DynamicResolutionHandler.s_CameraInstances)
					{
						if (keyValuePair.Value.m_OwnerCameraWeakRef == null || !keyValuePair.Value.m_OwnerCameraWeakRef.IsAlive)
						{
							dynamicResolutionHandler2 = keyValuePair.Value;
							num = keyValuePair.Key;
							break;
						}
					}
					if (dynamicResolutionHandler2 != null)
					{
						dynamicResolutionHandler = dynamicResolutionHandler2;
						DynamicResolutionHandler.s_CameraInstances.Remove(num);
						DynamicResolutionHandler.s_CameraUpscaleFilters.Remove(num);
					}
				}
				if (dynamicResolutionHandler == null)
				{
					dynamicResolutionHandler = new DynamicResolutionHandler();
					dynamicResolutionHandler.m_OwnerCameraWeakRef = new WeakReference(camera);
				}
				else
				{
					dynamicResolutionHandler.Reset();
					dynamicResolutionHandler.m_OwnerCameraWeakRef.Target = camera;
				}
				DynamicResolutionHandler.s_CameraInstances.Add(instanceID, dynamicResolutionHandler);
			}
			return dynamicResolutionHandler;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000E2C5 File Offset: 0x0000C4C5
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		public DynamicResolutionHandler.UpsamplerScheduleType upsamplerSchedule
		{
			get
			{
				return this.m_UpsamplerSchedule;
			}
			set
			{
				this.m_UpsamplerSchedule = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000E2CD File Offset: 0x0000C4CD
		public static DynamicResolutionHandler instance
		{
			get
			{
				return DynamicResolutionHandler.s_ActiveInstance;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000E2D4 File Offset: 0x0000C4D4
		private DynamicResolutionHandler()
		{
			this.Reset();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		private static float DefaultDynamicResMethod()
		{
			return 1f;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		private void ProcessSettings(GlobalDynamicResolutionSettings settings)
		{
			this.m_Enabled = settings.enabled && (Application.isPlaying || settings.forceResolution);
			if (!this.m_Enabled)
			{
				this.m_CurrentFraction = 1f;
			}
			else
			{
				this.type = settings.dynResType;
				this.m_UseMipBias = settings.useMipBias;
				float num = Mathf.Clamp(settings.minPercentage / 100f, 0.1f, 1f);
				this.m_MinScreenFraction = num;
				float num2 = Mathf.Clamp(settings.maxPercentage / 100f, this.m_MinScreenFraction, 3f);
				this.m_MaxScreenFraction = num2;
				DynamicResUpscaleFilter dynamicResUpscaleFilter;
				this.filter = (DynamicResolutionHandler.s_CameraUpscaleFilters.TryGetValue(DynamicResolutionHandler.s_ActiveCameraId, out dynamicResUpscaleFilter) ? dynamicResUpscaleFilter : settings.upsampleFilter);
				this.m_ForcingRes = settings.forceResolution;
				if (this.m_ForcingRes)
				{
					float num3 = Mathf.Clamp(settings.forcedPercentage / 100f, 0.1f, 1.5f);
					this.m_CurrentFraction = num3;
				}
			}
			this.m_CachedSettings = settings;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000E408 File Offset: 0x0000C608
		public Vector2 GetResolvedScale()
		{
			if (!this.m_Enabled || !this.m_CurrentCameraRequest)
			{
				return new Vector2(1f, 1f);
			}
			float num = this.m_CurrentFraction;
			float num2 = this.m_CurrentFraction;
			if (!this.m_ForceSoftwareFallback && this.type == DynamicResolutionType.Hardware)
			{
				num = ScalableBufferManager.widthScaleFactor;
				num2 = ScalableBufferManager.heightScaleFactor;
			}
			return new Vector2(num, num2);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000E467 File Offset: 0x0000C667
		public float CalculateMipBias(Vector2Int inputResolution, Vector2Int outputResolution, bool forceApply = false)
		{
			if (!this.m_UseMipBias && !forceApply)
			{
				return 0f;
			}
			return (float)Math.Log((double)inputResolution.x / (double)outputResolution.x, 2.0);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000E49C File Offset: 0x0000C69C
		public static void SetDynamicResScaler(PerformDynamicRes scaler, DynamicResScalePolicyType scalerType = DynamicResScalePolicyType.ReturnsMinMaxLerpFactor)
		{
			DynamicResolutionHandler.s_ScalerContainers[0] = new DynamicResolutionHandler.ScalerContainer
			{
				type = scalerType,
				method = scaler
			};
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		public static void SetSystemDynamicResScaler(PerformDynamicRes scaler, DynamicResScalePolicyType scalerType = DynamicResScalePolicyType.ReturnsMinMaxLerpFactor)
		{
			DynamicResolutionHandler.s_ScalerContainers[1] = new DynamicResolutionHandler.ScalerContainer
			{
				type = scalerType,
				method = scaler
			};
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000E501 File Offset: 0x0000C701
		public static void SetActiveDynamicScalerSlot(DynamicResScalerSlot slot)
		{
			DynamicResolutionHandler.s_ActiveScalerSlot = slot;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000E509 File Offset: 0x0000C709
		public static void ClearSelectedCamera()
		{
			DynamicResolutionHandler.s_ActiveInstance = DynamicResolutionHandler.s_DefaultInstance;
			DynamicResolutionHandler.s_ActiveCameraId = 0;
			DynamicResolutionHandler.s_ActiveInstanceDirty = true;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000E524 File Offset: 0x0000C724
		public static void SetUpscaleFilter(Camera camera, DynamicResUpscaleFilter filter)
		{
			int instanceID = camera.GetInstanceID();
			if (DynamicResolutionHandler.s_CameraUpscaleFilters.ContainsKey(instanceID))
			{
				DynamicResolutionHandler.s_CameraUpscaleFilters[instanceID] = filter;
				return;
			}
			DynamicResolutionHandler.s_CameraUpscaleFilters.Add(instanceID, filter);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000E55E File Offset: 0x0000C75E
		public void SetCurrentCameraRequest(bool cameraRequest)
		{
			this.m_CurrentCameraRequest = cameraRequest;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000E568 File Offset: 0x0000C768
		public static void UpdateAndUseCamera(Camera camera, GlobalDynamicResolutionSettings? settings = null, Action OnResolutionChange = null)
		{
			int num;
			if (camera == null)
			{
				DynamicResolutionHandler.s_ActiveInstance = DynamicResolutionHandler.s_DefaultInstance;
				num = 0;
			}
			else
			{
				DynamicResolutionHandler.s_ActiveInstance = DynamicResolutionHandler.GetOrCreateDrsInstanceHandler(camera);
				num = camera.GetInstanceID();
			}
			DynamicResolutionHandler.s_ActiveInstanceDirty = num != DynamicResolutionHandler.s_ActiveCameraId;
			DynamicResolutionHandler.s_ActiveCameraId = num;
			DynamicResolutionHandler.s_ActiveInstance.Update((settings != null) ? settings.Value : DynamicResolutionHandler.s_ActiveInstance.m_CachedSettings, OnResolutionChange);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		public void Update(GlobalDynamicResolutionSettings settings, Action OnResolutionChange = null)
		{
			this.ProcessSettings(settings);
			if (!this.m_Enabled || !DynamicResolutionHandler.s_ActiveInstanceDirty)
			{
				this.FlushScalableBufferManagerState();
				DynamicResolutionHandler.s_ActiveInstanceDirty = false;
				return;
			}
			if (!this.m_ForcingRes)
			{
				ref DynamicResolutionHandler.ScalerContainer ptr = ref DynamicResolutionHandler.s_ScalerContainers[(int)DynamicResolutionHandler.s_ActiveScalerSlot];
				if (ptr.type == DynamicResScalePolicyType.ReturnsMinMaxLerpFactor)
				{
					float num = Mathf.Clamp(ptr.method(), 0f, 1f);
					this.m_CurrentFraction = Mathf.Lerp(this.m_MinScreenFraction, this.m_MaxScreenFraction, num);
				}
				else if (ptr.type == DynamicResScalePolicyType.ReturnsPercentage)
				{
					float num2 = Mathf.Max(ptr.method(), 5f);
					this.m_CurrentFraction = Mathf.Clamp(num2 / 100f, this.m_MinScreenFraction, this.m_MaxScreenFraction);
				}
			}
			bool flag = false;
			bool flag2 = this.m_CurrentFraction != this.m_PrevFraction;
			this.m_PrevFraction = this.m_CurrentFraction;
			if (!this.m_ForceSoftwareFallback && this.type == DynamicResolutionType.Hardware)
			{
				flag = this.FlushScalableBufferManagerState();
				if (ScalableBufferManager.widthScaleFactor != this.m_PrevHWScaleWidth || ScalableBufferManager.heightScaleFactor != this.m_PrevHWScaleHeight)
				{
					flag = true;
				}
			}
			if ((flag2 || flag) && OnResolutionChange != null)
			{
				OnResolutionChange();
			}
			DynamicResolutionHandler.s_ActiveInstanceDirty = false;
			this.m_PrevHWScaleWidth = ScalableBufferManager.widthScaleFactor;
			this.m_PrevHWScaleHeight = ScalableBufferManager.heightScaleFactor;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000E71E File Offset: 0x0000C91E
		public bool SoftwareDynamicResIsEnabled()
		{
			return this.m_CurrentCameraRequest && this.m_Enabled && (this.m_CurrentFraction != 1f || this.runUpscalerFilterOnFullResolution) && (this.m_ForceSoftwareFallback || this.type == DynamicResolutionType.Software);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000E75A File Offset: 0x0000C95A
		public bool HardwareDynamicResIsEnabled()
		{
			return !this.m_ForceSoftwareFallback && this.m_CurrentCameraRequest && this.m_Enabled && this.type == DynamicResolutionType.Hardware;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E77F File Offset: 0x0000C97F
		public bool RequestsHardwareDynamicResolution()
		{
			return !this.m_ForceSoftwareFallback && this.type == DynamicResolutionType.Hardware;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000E794 File Offset: 0x0000C994
		public bool DynamicResolutionEnabled()
		{
			return this.m_CurrentCameraRequest && this.m_Enabled && (this.m_CurrentFraction != 1f || this.runUpscalerFilterOnFullResolution);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000E7BD File Offset: 0x0000C9BD
		public void ForceSoftwareFallback()
		{
			this.m_ForceSoftwareFallback = true;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		public Vector2Int GetScaledSize(Vector2Int size)
		{
			this.cachedOriginalSize = size;
			if (!this.m_Enabled || !this.m_CurrentCameraRequest)
			{
				return size;
			}
			Vector2Int vector2Int = this.ApplyScalesOnSize(size);
			this.m_LastScaledSize = vector2Int;
			return vector2Int;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E7FE File Offset: 0x0000C9FE
		public Vector2Int ApplyScalesOnSize(Vector2Int size)
		{
			return this.ApplyScalesOnSize(size, this.GetResolvedScale());
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000E810 File Offset: 0x0000CA10
		internal Vector2Int ApplyScalesOnSize(Vector2Int size, Vector2 scales)
		{
			Vector2Int vector2Int = new Vector2Int(Mathf.CeilToInt((float)size.x * scales.x), Mathf.CeilToInt((float)size.y * scales.y));
			if (this.m_ForceSoftwareFallback || this.type != DynamicResolutionType.Hardware)
			{
				vector2Int.x += 1 & vector2Int.x;
				vector2Int.y += 1 & vector2Int.y;
			}
			vector2Int.x = Math.Min(vector2Int.x, size.x);
			vector2Int.y = Math.Min(vector2Int.y, size.y);
			return vector2Int;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		public float GetCurrentScale()
		{
			if (!this.m_Enabled || !this.m_CurrentCameraRequest)
			{
				return 1f;
			}
			return this.m_CurrentFraction;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000E8DE File Offset: 0x0000CADE
		public Vector2Int GetLastScaledSize()
		{
			return this.m_LastScaledSize;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		public float GetLowResMultiplier(float targetLowRes)
		{
			if (!this.m_Enabled)
			{
				return targetLowRes;
			}
			float num = Math.Min(this.m_CachedSettings.lowResTransparencyMinimumThreshold / 100f, targetLowRes);
			if (targetLowRes * this.m_CurrentFraction >= num)
			{
				return targetLowRes;
			}
			return Mathf.Clamp(num / this.m_CurrentFraction, 0f, 1f);
		}

		// Token: 0x040001BC RID: 444
		private bool m_Enabled;

		// Token: 0x040001BD RID: 445
		private bool m_UseMipBias;

		// Token: 0x040001BE RID: 446
		private float m_MinScreenFraction;

		// Token: 0x040001BF RID: 447
		private float m_MaxScreenFraction;

		// Token: 0x040001C0 RID: 448
		private float m_CurrentFraction;

		// Token: 0x040001C1 RID: 449
		private bool m_ForcingRes;

		// Token: 0x040001C2 RID: 450
		private bool m_CurrentCameraRequest;

		// Token: 0x040001C3 RID: 451
		private float m_PrevFraction;

		// Token: 0x040001C4 RID: 452
		private bool m_ForceSoftwareFallback;

		// Token: 0x040001C5 RID: 453
		private bool m_RunUpscalerFilterOnFullResolution;

		// Token: 0x040001C6 RID: 454
		private float m_PrevHWScaleWidth;

		// Token: 0x040001C7 RID: 455
		private float m_PrevHWScaleHeight;

		// Token: 0x040001C8 RID: 456
		private Vector2Int m_LastScaledSize;

		// Token: 0x040001C9 RID: 457
		private static DynamicResScalerSlot s_ActiveScalerSlot = DynamicResScalerSlot.User;

		// Token: 0x040001CA RID: 458
		private static DynamicResolutionHandler.ScalerContainer[] s_ScalerContainers = new DynamicResolutionHandler.ScalerContainer[]
		{
			new DynamicResolutionHandler.ScalerContainer
			{
				type = DynamicResScalePolicyType.ReturnsMinMaxLerpFactor,
				method = new PerformDynamicRes(DynamicResolutionHandler.DefaultDynamicResMethod)
			},
			new DynamicResolutionHandler.ScalerContainer
			{
				type = DynamicResScalePolicyType.ReturnsMinMaxLerpFactor,
				method = new PerformDynamicRes(DynamicResolutionHandler.DefaultDynamicResMethod)
			}
		};

		// Token: 0x040001CB RID: 459
		private Vector2Int cachedOriginalSize;

		// Token: 0x040001CD RID: 461
		private static Dictionary<int, DynamicResUpscaleFilter> s_CameraUpscaleFilters = new Dictionary<int, DynamicResUpscaleFilter>();

		// Token: 0x040001CF RID: 463
		private DynamicResolutionType type;

		// Token: 0x040001D0 RID: 464
		private GlobalDynamicResolutionSettings m_CachedSettings = GlobalDynamicResolutionSettings.NewDefault();

		// Token: 0x040001D1 RID: 465
		private const int CameraDictionaryMaxcCapacity = 32;

		// Token: 0x040001D2 RID: 466
		private WeakReference m_OwnerCameraWeakRef;

		// Token: 0x040001D3 RID: 467
		private static Dictionary<int, DynamicResolutionHandler> s_CameraInstances = new Dictionary<int, DynamicResolutionHandler>(32);

		// Token: 0x040001D4 RID: 468
		private static DynamicResolutionHandler s_DefaultInstance = new DynamicResolutionHandler();

		// Token: 0x040001D5 RID: 469
		private static int s_ActiveCameraId = 0;

		// Token: 0x040001D6 RID: 470
		private static DynamicResolutionHandler s_ActiveInstance = DynamicResolutionHandler.s_DefaultInstance;

		// Token: 0x040001D7 RID: 471
		private static bool s_ActiveInstanceDirty = true;

		// Token: 0x040001D8 RID: 472
		private static float s_GlobalHwFraction = 1f;

		// Token: 0x040001D9 RID: 473
		private static bool s_GlobalHwUpresActive = false;

		// Token: 0x040001DA RID: 474
		private DynamicResolutionHandler.UpsamplerScheduleType m_UpsamplerSchedule = DynamicResolutionHandler.UpsamplerScheduleType.AfterPost;

		// Token: 0x0200013D RID: 317
		private struct ScalerContainer
		{
			// Token: 0x040004FB RID: 1275
			public DynamicResScalePolicyType type;

			// Token: 0x040004FC RID: 1276
			public PerformDynamicRes method;
		}

		// Token: 0x0200013E RID: 318
		public enum UpsamplerScheduleType
		{
			// Token: 0x040004FE RID: 1278
			BeforePost,
			// Token: 0x040004FF RID: 1279
			AfterPost
		}
	}
}
