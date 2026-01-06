using System;

namespace UnityEngine.U2D
{
	// Token: 0x02000003 RID: 3
	[DisallowMultipleComponent]
	[AddComponentMenu("")]
	[RequireComponent(typeof(Camera))]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.pixel-perfect@latest/index.html?subfolder=/manual/index.html%23properties")]
	public class PixelPerfectCamera : MonoBehaviour, IPixelPerfectCamera
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000206C File Offset: 0x0000026C
		public int assetsPPU
		{
			get
			{
				return this.m_AssetsPPU;
			}
			set
			{
				this.m_AssetsPPU = ((value > 0) ? value : 1);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000207C File Offset: 0x0000027C
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002084 File Offset: 0x00000284
		public int refResolutionX
		{
			get
			{
				return this.m_RefResolutionX;
			}
			set
			{
				this.m_RefResolutionX = ((value > 0) ? value : 1);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002094 File Offset: 0x00000294
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000209C File Offset: 0x0000029C
		public int refResolutionY
		{
			get
			{
				return this.m_RefResolutionY;
			}
			set
			{
				this.m_RefResolutionY = ((value > 0) ? value : 1);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020AC File Offset: 0x000002AC
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020B4 File Offset: 0x000002B4
		public bool upscaleRT
		{
			get
			{
				return this.m_UpscaleRT;
			}
			set
			{
				this.m_UpscaleRT = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020BD File Offset: 0x000002BD
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020C5 File Offset: 0x000002C5
		public bool pixelSnapping
		{
			get
			{
				return this.m_PixelSnapping;
			}
			set
			{
				this.m_PixelSnapping = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020CE File Offset: 0x000002CE
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020D6 File Offset: 0x000002D6
		public bool cropFrameX
		{
			get
			{
				return this.m_CropFrameX;
			}
			set
			{
				this.m_CropFrameX = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020DF File Offset: 0x000002DF
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020E7 File Offset: 0x000002E7
		public bool cropFrameY
		{
			get
			{
				return this.m_CropFrameY;
			}
			set
			{
				this.m_CropFrameY = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020F0 File Offset: 0x000002F0
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000020F8 File Offset: 0x000002F8
		public bool stretchFill
		{
			get
			{
				return this.m_StretchFill;
			}
			set
			{
				this.m_StretchFill = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002104 File Offset: 0x00000304
		public int pixelRatio
		{
			get
			{
				if (!this.m_CinemachineCompatibilityMode)
				{
					return this.m_Internal.zoom;
				}
				if (this.m_UpscaleRT)
				{
					return this.m_Internal.zoom * this.m_Internal.cinemachineVCamZoom;
				}
				return this.m_Internal.cinemachineVCamZoom;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002150 File Offset: 0x00000350
		public Vector3 RoundToPixel(Vector3 position)
		{
			float unitsPerPixel = this.m_Internal.unitsPerPixel;
			if (unitsPerPixel == 0f)
			{
				return position;
			}
			Vector3 vector;
			vector.x = Mathf.Round(position.x / unitsPerPixel) * unitsPerPixel;
			vector.y = Mathf.Round(position.y / unitsPerPixel) * unitsPerPixel;
			vector.z = Mathf.Round(position.z / unitsPerPixel) * unitsPerPixel;
			return vector;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021B6 File Offset: 0x000003B6
		public float CorrectCinemachineOrthoSize(float targetOrthoSize)
		{
			this.m_CinemachineCompatibilityMode = true;
			if (this.m_Internal == null)
			{
				return targetOrthoSize;
			}
			return this.m_Internal.CorrectCinemachineOrthoSize(targetOrthoSize);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021D8 File Offset: 0x000003D8
		private void PixelSnap()
		{
			Vector3 position = this.m_Camera.transform.position;
			Vector3 vector = this.RoundToPixel(position) - position;
			vector.z = -vector.z;
			Matrix4x4 matrix4x = Matrix4x4.TRS(-vector, Quaternion.identity, new Vector3(1f, 1f, -1f));
			this.m_Camera.worldToCameraMatrix = matrix4x * this.m_Camera.transform.worldToLocalMatrix;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002258 File Offset: 0x00000458
		private void Awake()
		{
			this.m_Camera = base.GetComponent<Camera>();
			this.m_Internal = new PixelPerfectCameraInternal(this);
			this.m_Internal.originalOrthoSize = this.m_Camera.orthographicSize;
			this.m_Internal.hasPostProcessLayer = base.GetComponent("PostProcessLayer") != null;
			if (this.m_Camera.targetTexture != null)
			{
				Debug.LogWarning("Render to texture is not supported by Pixel Perfect Camera.", this.m_Camera);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022D2 File Offset: 0x000004D2
		private void LateUpdate()
		{
			this.m_Internal.CalculateCameraProperties(Screen.width, Screen.height);
			this.m_Camera.forceIntoRenderTexture = this.m_Internal.hasPostProcessLayer || this.m_Internal.useOffscreenRT;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002310 File Offset: 0x00000510
		private void OnPreCull()
		{
			this.PixelSnap();
			if (this.m_Internal.pixelRect != Rect.zero)
			{
				this.m_Camera.pixelRect = this.m_Internal.pixelRect;
			}
			else
			{
				this.m_Camera.rect = new Rect(0f, 0f, 1f, 1f);
			}
			if (!this.m_CinemachineCompatibilityMode)
			{
				this.m_Camera.orthographicSize = this.m_Internal.orthoSize;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002394 File Offset: 0x00000594
		private void OnPreRender()
		{
			if (this.m_Internal.cropFrameXOrY)
			{
				GL.Clear(false, true, Color.black);
			}
			PixelPerfectRendering.pixelSnapSpacing = this.m_Internal.unitsPerPixel;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023C0 File Offset: 0x000005C0
		private void OnPostRender()
		{
			PixelPerfectRendering.pixelSnapSpacing = 0f;
			if (!this.m_Internal.useOffscreenRT)
			{
				return;
			}
			RenderTexture activeTexture = this.m_Camera.activeTexture;
			if (activeTexture != null)
			{
				activeTexture.filterMode = (this.m_Internal.useStretchFill ? FilterMode.Bilinear : FilterMode.Point);
			}
			this.m_Camera.pixelRect = this.m_Internal.CalculatePostRenderPixelRect(this.m_Camera.aspect, Screen.width, Screen.height);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000243C File Offset: 0x0000063C
		private void OnEnable()
		{
			this.m_CinemachineCompatibilityMode = false;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002448 File Offset: 0x00000648
		internal void OnDisable()
		{
			this.m_Camera.rect = new Rect(0f, 0f, 1f, 1f);
			this.m_Camera.orthographicSize = this.m_Internal.originalOrthoSize;
			this.m_Camera.forceIntoRenderTexture = this.m_Internal.hasPostProcessLayer;
			this.m_Camera.ResetAspect();
			this.m_Camera.ResetWorldToCameraMatrix();
		}

		// Token: 0x04000001 RID: 1
		[SerializeField]
		private int m_AssetsPPU = 100;

		// Token: 0x04000002 RID: 2
		[SerializeField]
		private int m_RefResolutionX = 320;

		// Token: 0x04000003 RID: 3
		[SerializeField]
		private int m_RefResolutionY = 180;

		// Token: 0x04000004 RID: 4
		[SerializeField]
		private bool m_UpscaleRT;

		// Token: 0x04000005 RID: 5
		[SerializeField]
		private bool m_PixelSnapping;

		// Token: 0x04000006 RID: 6
		[SerializeField]
		private bool m_CropFrameX;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		private bool m_CropFrameY;

		// Token: 0x04000008 RID: 8
		[SerializeField]
		private bool m_StretchFill;

		// Token: 0x04000009 RID: 9
		private Camera m_Camera;

		// Token: 0x0400000A RID: 10
		private PixelPerfectCameraInternal m_Internal;

		// Token: 0x0400000B RID: 11
		private bool m_CinemachineCompatibilityMode;
	}
}
