using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000D1 RID: 209
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Camera))]
	[ImageEffectAllowedInSceneView]
	public class UniversalAdditionalCameraData : MonoBehaviour, ISerializationCallbackReceiver, IAdditionalData
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00020B29 File Offset: 0x0001ED29
		public float version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00020B31 File Offset: 0x0001ED31
		internal static UniversalAdditionalCameraData defaultAdditionalCameraData
		{
			get
			{
				if (UniversalAdditionalCameraData.s_DefaultAdditionalCameraData == null)
				{
					UniversalAdditionalCameraData.s_DefaultAdditionalCameraData = new UniversalAdditionalCameraData();
				}
				return UniversalAdditionalCameraData.s_DefaultAdditionalCameraData;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00020B4F File Offset: 0x0001ED4F
		internal Camera camera
		{
			get
			{
				if (!this.m_Camera)
				{
					base.gameObject.TryGetComponent<Camera>(out this.m_Camera);
				}
				return this.m_Camera;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00020B76 File Offset: 0x0001ED76
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x00020B7E File Offset: 0x0001ED7E
		public bool renderShadows
		{
			get
			{
				return this.m_RenderShadows;
			}
			set
			{
				this.m_RenderShadows = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00020B87 File Offset: 0x0001ED87
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00020B8F File Offset: 0x0001ED8F
		public CameraOverrideOption requiresDepthOption
		{
			get
			{
				return this.m_RequiresDepthTextureOption;
			}
			set
			{
				this.m_RequiresDepthTextureOption = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00020B98 File Offset: 0x0001ED98
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x00020BA0 File Offset: 0x0001EDA0
		public CameraOverrideOption requiresColorOption
		{
			get
			{
				return this.m_RequiresOpaqueTextureOption;
			}
			set
			{
				this.m_RequiresOpaqueTextureOption = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00020BA9 File Offset: 0x0001EDA9
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x00020BB1 File Offset: 0x0001EDB1
		public CameraRenderType renderType
		{
			get
			{
				return this.m_CameraType;
			}
			set
			{
				this.m_CameraType = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00020BBC File Offset: 0x0001EDBC
		public List<Camera> cameraStack
		{
			get
			{
				if (this.renderType != CameraRenderType.Base)
				{
					Camera component = base.gameObject.GetComponent<Camera>();
					Debug.LogWarning(string.Format("{0}: This camera is of {1} type. Only Base cameras can have a camera stack.", component.name, this.renderType));
					return null;
				}
				if (!this.scriptableRenderer.SupportsCameraStackingType(CameraRenderType.Base))
				{
					Camera component2 = base.gameObject.GetComponent<Camera>();
					Debug.LogWarning(string.Format("{0}: This camera has a ScriptableRenderer that doesn't support camera stacking. Camera stack is null.", component2.name));
					return null;
				}
				return this.m_Cameras;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00020C38 File Offset: 0x0001EE38
		internal void UpdateCameraStack()
		{
			int count = this.m_Cameras.Count;
			this.m_Cameras.RemoveAll((Camera cam) => cam == null);
			int count2 = this.m_Cameras.Count;
			int num = count - count2;
			if (num != 0)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					base.name,
					": ",
					num.ToString(),
					" camera overlay",
					(num > 1) ? "s" : "",
					" no longer exists and will be removed from the camera stack."
				}));
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00020CDB File Offset: 0x0001EEDB
		public bool clearDepth
		{
			get
			{
				return this.m_ClearDepth;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00020CE3 File Offset: 0x0001EEE3
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x00020D02 File Offset: 0x0001EF02
		public bool requiresDepthTexture
		{
			get
			{
				if (this.m_RequiresDepthTextureOption == CameraOverrideOption.UsePipelineSettings)
				{
					return UniversalRenderPipeline.asset.supportsCameraDepthTexture;
				}
				return this.m_RequiresDepthTextureOption == CameraOverrideOption.On;
			}
			set
			{
				this.m_RequiresDepthTextureOption = (value ? CameraOverrideOption.On : CameraOverrideOption.Off);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00020D11 File Offset: 0x0001EF11
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x00020D30 File Offset: 0x0001EF30
		public bool requiresColorTexture
		{
			get
			{
				if (this.m_RequiresOpaqueTextureOption == CameraOverrideOption.UsePipelineSettings)
				{
					return UniversalRenderPipeline.asset.supportsCameraOpaqueTexture;
				}
				return this.m_RequiresOpaqueTextureOption == CameraOverrideOption.On;
			}
			set
			{
				this.m_RequiresOpaqueTextureOption = (value ? CameraOverrideOption.On : CameraOverrideOption.Off);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00020D40 File Offset: 0x0001EF40
		public ScriptableRenderer scriptableRenderer
		{
			get
			{
				if (UniversalRenderPipeline.asset == null)
				{
					return null;
				}
				if (!UniversalRenderPipeline.asset.ValidateRendererData(this.m_RendererIndex))
				{
					int defaultRendererIndex = UniversalRenderPipeline.asset.m_DefaultRendererIndex;
					Debug.LogWarning(string.Concat(new string[]
					{
						"Renderer at <b>index ",
						this.m_RendererIndex.ToString(),
						"</b> is missing for camera <b>",
						this.camera.name,
						"</b>, falling back to Default Renderer. <b>",
						UniversalRenderPipeline.asset.m_RendererDataList[defaultRendererIndex].name,
						"</b>"
					}), UniversalRenderPipeline.asset);
					return UniversalRenderPipeline.asset.GetRenderer(defaultRendererIndex);
				}
				return UniversalRenderPipeline.asset.GetRenderer(this.m_RendererIndex);
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00020DF4 File Offset: 0x0001EFF4
		public void SetRenderer(int index)
		{
			this.m_RendererIndex = index;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00020DFD File Offset: 0x0001EFFD
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x00020E05 File Offset: 0x0001F005
		public LayerMask volumeLayerMask
		{
			get
			{
				return this.m_VolumeLayerMask;
			}
			set
			{
				this.m_VolumeLayerMask = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00020E0E File Offset: 0x0001F00E
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00020E16 File Offset: 0x0001F016
		public Transform volumeTrigger
		{
			get
			{
				return this.m_VolumeTrigger;
			}
			set
			{
				this.m_VolumeTrigger = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00020E1F File Offset: 0x0001F01F
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x00020E27 File Offset: 0x0001F027
		internal VolumeFrameworkUpdateMode volumeFrameworkUpdateMode
		{
			get
			{
				return this.m_VolumeFrameworkUpdateModeOption;
			}
			set
			{
				this.m_VolumeFrameworkUpdateModeOption = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00020E30 File Offset: 0x0001F030
		public bool requiresVolumeFrameworkUpdate
		{
			get
			{
				if (this.m_VolumeFrameworkUpdateModeOption == VolumeFrameworkUpdateMode.UsePipelineSettings)
				{
					return UniversalRenderPipeline.asset.volumeFrameworkUpdateMode != VolumeFrameworkUpdateMode.ViaScripting;
				}
				return this.m_VolumeFrameworkUpdateModeOption == VolumeFrameworkUpdateMode.EveryFrame;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00020E55 File Offset: 0x0001F055
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00020E5D File Offset: 0x0001F05D
		public VolumeStack volumeStack
		{
			get
			{
				return this.m_VolumeStack;
			}
			set
			{
				this.m_VolumeStack = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00020E66 File Offset: 0x0001F066
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00020E6E File Offset: 0x0001F06E
		public bool renderPostProcessing
		{
			get
			{
				return this.m_RenderPostProcessing;
			}
			set
			{
				this.m_RenderPostProcessing = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00020E77 File Offset: 0x0001F077
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00020E7F File Offset: 0x0001F07F
		public AntialiasingMode antialiasing
		{
			get
			{
				return this.m_Antialiasing;
			}
			set
			{
				this.m_Antialiasing = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00020E88 File Offset: 0x0001F088
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x00020E90 File Offset: 0x0001F090
		public AntialiasingQuality antialiasingQuality
		{
			get
			{
				return this.m_AntialiasingQuality;
			}
			set
			{
				this.m_AntialiasingQuality = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00020E99 File Offset: 0x0001F099
		internal MotionVectorsPersistentData motionVectorsPersistentData
		{
			get
			{
				return this.m_MotionVectorsPersistentData;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00020EA1 File Offset: 0x0001F0A1
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x00020EA9 File Offset: 0x0001F0A9
		public bool stopNaN
		{
			get
			{
				return this.m_StopNaN;
			}
			set
			{
				this.m_StopNaN = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00020EB2 File Offset: 0x0001F0B2
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x00020EBA File Offset: 0x0001F0BA
		public bool dithering
		{
			get
			{
				return this.m_Dithering;
			}
			set
			{
				this.m_Dithering = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00020EC3 File Offset: 0x0001F0C3
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x00020ECB File Offset: 0x0001F0CB
		public bool allowXRRendering
		{
			get
			{
				return this.m_AllowXRRendering;
			}
			set
			{
				this.m_AllowXRRendering = value;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00020ED4 File Offset: 0x0001F0D4
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00020ED6 File Offset: 0x0001F0D6
		public void OnAfterDeserialize()
		{
			if (this.version <= 1f)
			{
				this.m_RequiresDepthTextureOption = (this.m_RequiresDepthTexture ? CameraOverrideOption.On : CameraOverrideOption.Off);
				this.m_RequiresOpaqueTextureOption = (this.m_RequiresColorTexture ? CameraOverrideOption.On : CameraOverrideOption.Off);
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00020F0C File Offset: 0x0001F10C
		public void OnDrawGizmos()
		{
			string text = "";
			Color white = Color.white;
			if (this.m_CameraType == CameraRenderType.Base)
			{
				text = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";
			}
			else if (this.m_CameraType == CameraRenderType.Overlay)
			{
				text = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";
			}
			if (!string.IsNullOrEmpty(text))
			{
				Gizmos.DrawIcon(base.transform.position, text, true, white);
			}
			if (this.renderPostProcessing)
			{
				Gizmos.DrawIcon(base.transform.position, "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_PostProcessing.png", true, white);
			}
		}

		// Token: 0x040004C4 RID: 1220
		private const string k_GizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/";

		// Token: 0x040004C5 RID: 1221
		private const string k_BaseCameraGizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";

		// Token: 0x040004C6 RID: 1222
		private const string k_OverlayCameraGizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";

		// Token: 0x040004C7 RID: 1223
		private const string k_PostProcessingGizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_PostProcessing.png";

		// Token: 0x040004C8 RID: 1224
		[FormerlySerializedAs("renderShadows")]
		[SerializeField]
		private bool m_RenderShadows = true;

		// Token: 0x040004C9 RID: 1225
		[SerializeField]
		private CameraOverrideOption m_RequiresDepthTextureOption = CameraOverrideOption.UsePipelineSettings;

		// Token: 0x040004CA RID: 1226
		[SerializeField]
		private CameraOverrideOption m_RequiresOpaqueTextureOption = CameraOverrideOption.UsePipelineSettings;

		// Token: 0x040004CB RID: 1227
		[SerializeField]
		private CameraRenderType m_CameraType;

		// Token: 0x040004CC RID: 1228
		[SerializeField]
		private List<Camera> m_Cameras = new List<Camera>();

		// Token: 0x040004CD RID: 1229
		[SerializeField]
		private int m_RendererIndex = -1;

		// Token: 0x040004CE RID: 1230
		[SerializeField]
		private LayerMask m_VolumeLayerMask = 1;

		// Token: 0x040004CF RID: 1231
		[SerializeField]
		private Transform m_VolumeTrigger;

		// Token: 0x040004D0 RID: 1232
		[SerializeField]
		private VolumeFrameworkUpdateMode m_VolumeFrameworkUpdateModeOption = VolumeFrameworkUpdateMode.UsePipelineSettings;

		// Token: 0x040004D1 RID: 1233
		[SerializeField]
		private bool m_RenderPostProcessing;

		// Token: 0x040004D2 RID: 1234
		[SerializeField]
		private AntialiasingMode m_Antialiasing;

		// Token: 0x040004D3 RID: 1235
		[SerializeField]
		private AntialiasingQuality m_AntialiasingQuality = AntialiasingQuality.High;

		// Token: 0x040004D4 RID: 1236
		[SerializeField]
		private bool m_StopNaN;

		// Token: 0x040004D5 RID: 1237
		[SerializeField]
		private bool m_Dithering;

		// Token: 0x040004D6 RID: 1238
		[SerializeField]
		private bool m_ClearDepth = true;

		// Token: 0x040004D7 RID: 1239
		[SerializeField]
		private bool m_AllowXRRendering = true;

		// Token: 0x040004D8 RID: 1240
		[NonSerialized]
		private Camera m_Camera;

		// Token: 0x040004D9 RID: 1241
		[FormerlySerializedAs("requiresDepthTexture")]
		[SerializeField]
		private bool m_RequiresDepthTexture;

		// Token: 0x040004DA RID: 1242
		[FormerlySerializedAs("requiresColorTexture")]
		[SerializeField]
		private bool m_RequiresColorTexture;

		// Token: 0x040004DB RID: 1243
		[HideInInspector]
		[SerializeField]
		private float m_Version = 2f;

		// Token: 0x040004DC RID: 1244
		[NonSerialized]
		private MotionVectorsPersistentData m_MotionVectorsPersistentData = new MotionVectorsPersistentData();

		// Token: 0x040004DD RID: 1245
		private static UniversalAdditionalCameraData s_DefaultAdditionalCameraData;

		// Token: 0x040004DE RID: 1246
		private VolumeStack m_VolumeStack;
	}
}
