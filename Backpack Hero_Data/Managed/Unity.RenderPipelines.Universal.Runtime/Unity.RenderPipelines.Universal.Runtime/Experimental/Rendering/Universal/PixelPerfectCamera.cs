using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.U2D;

namespace UnityEngine.Experimental.Rendering.Universal
{
	// Token: 0x02000002 RID: 2
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("Rendering/2D/Pixel Perfect Camera")]
	[RequireComponent(typeof(Camera))]
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest/index.html?subfolder=/manual/2d-pixelperfect.html%23properties")]
	public class PixelPerfectCamera : MonoBehaviour, IPixelPerfectCamera, ISerializationCallbackReceiver
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public PixelPerfectCamera.CropFrame cropFrame
		{
			get
			{
				return this.m_CropFrame;
			}
			set
			{
				this.m_CropFrame = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public PixelPerfectCamera.GridSnapping gridSnapping
		{
			get
			{
				return this.m_GridSnapping;
			}
			set
			{
				this.m_GridSnapping = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		public float orthographicSize
		{
			get
			{
				return this.m_Internal.orthoSize;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000207F File Offset: 0x0000027F
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002087 File Offset: 0x00000287
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002097 File Offset: 0x00000297
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000209F File Offset: 0x0000029F
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020AF File Offset: 0x000002AF
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020B7 File Offset: 0x000002B7
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020C7 File Offset: 0x000002C7
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020D2 File Offset: 0x000002D2
		[Obsolete("Use gridSnapping instead", false)]
		public bool upscaleRT
		{
			get
			{
				return this.m_GridSnapping == PixelPerfectCamera.GridSnapping.UpscaleRenderTexture;
			}
			set
			{
				this.m_GridSnapping = (value ? PixelPerfectCamera.GridSnapping.UpscaleRenderTexture : PixelPerfectCamera.GridSnapping.None);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020E1 File Offset: 0x000002E1
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020EC File Offset: 0x000002EC
		[Obsolete("Use gridSnapping instead", false)]
		public bool pixelSnapping
		{
			get
			{
				return this.m_GridSnapping == PixelPerfectCamera.GridSnapping.PixelSnapping;
			}
			set
			{
				this.m_GridSnapping = (value ? PixelPerfectCamera.GridSnapping.PixelSnapping : PixelPerfectCamera.GridSnapping.None);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020FB File Offset: 0x000002FB
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000211C File Offset: 0x0000031C
		[Obsolete("Use cropFrame instead", false)]
		public bool cropFrameX
		{
			get
			{
				return this.m_CropFrame == PixelPerfectCamera.CropFrame.StretchFill || this.m_CropFrame == PixelPerfectCamera.CropFrame.Windowbox || this.m_CropFrame == PixelPerfectCamera.CropFrame.Pillarbox;
			}
			set
			{
				if (value)
				{
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.None)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Pillarbox;
						return;
					}
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Letterbox)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Windowbox;
						return;
					}
				}
				else
				{
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Pillarbox)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.None;
						return;
					}
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Windowbox || this.m_CropFrame == PixelPerfectCamera.CropFrame.StretchFill)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Letterbox;
					}
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002177 File Offset: 0x00000377
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002198 File Offset: 0x00000398
		[Obsolete("Use cropFrame instead", false)]
		public bool cropFrameY
		{
			get
			{
				return this.m_CropFrame == PixelPerfectCamera.CropFrame.StretchFill || this.m_CropFrame == PixelPerfectCamera.CropFrame.Windowbox || this.m_CropFrame == PixelPerfectCamera.CropFrame.Letterbox;
			}
			set
			{
				if (value)
				{
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.None)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Letterbox;
						return;
					}
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Pillarbox)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Windowbox;
						return;
					}
				}
				else
				{
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Letterbox)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.None;
						return;
					}
					if (this.m_CropFrame == PixelPerfectCamera.CropFrame.Windowbox || this.m_CropFrame == PixelPerfectCamera.CropFrame.StretchFill)
					{
						this.m_CropFrame = PixelPerfectCamera.CropFrame.Pillarbox;
					}
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021F3 File Offset: 0x000003F3
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000021FE File Offset: 0x000003FE
		[Obsolete("Use cropFrame instead", false)]
		public bool stretchFill
		{
			get
			{
				return this.m_CropFrame == PixelPerfectCamera.CropFrame.StretchFill;
			}
			set
			{
				if (value)
				{
					this.m_CropFrame = PixelPerfectCamera.CropFrame.StretchFill;
					return;
				}
				this.m_CropFrame = PixelPerfectCamera.CropFrame.Windowbox;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002214 File Offset: 0x00000414
		public int pixelRatio
		{
			get
			{
				if (!this.m_CinemachineCompatibilityMode)
				{
					return this.m_Internal.zoom;
				}
				if (this.m_GridSnapping == PixelPerfectCamera.GridSnapping.UpscaleRenderTexture)
				{
					return this.m_Internal.zoom * this.m_Internal.cinemachineVCamZoom;
				}
				return this.m_Internal.cinemachineVCamZoom;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002264 File Offset: 0x00000464
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

		// Token: 0x06000018 RID: 24 RVA: 0x000022CA File Offset: 0x000004CA
		public float CorrectCinemachineOrthoSize(float targetOrthoSize)
		{
			this.m_CinemachineCompatibilityMode = true;
			if (this.m_Internal == null)
			{
				return targetOrthoSize;
			}
			return this.m_Internal.CorrectCinemachineOrthoSize(targetOrthoSize);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022E9 File Offset: 0x000004E9
		internal FilterMode finalBlitFilterMode
		{
			get
			{
				if (!this.m_Internal.useStretchFill)
				{
					return FilterMode.Point;
				}
				return FilterMode.Bilinear;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022FB File Offset: 0x000004FB
		internal Vector2Int offscreenRTSize
		{
			get
			{
				return new Vector2Int(this.m_Internal.offscreenRTWidth, this.m_Internal.offscreenRTHeight);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002318 File Offset: 0x00000518
		private Vector2Int cameraRTSize
		{
			get
			{
				RenderTexture targetTexture = this.m_Camera.targetTexture;
				if (!(targetTexture == null))
				{
					return new Vector2Int(targetTexture.width, targetTexture.height);
				}
				return new Vector2Int(Screen.width, Screen.height);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000235C File Offset: 0x0000055C
		private void PixelSnap()
		{
			Vector3 position = this.m_Camera.transform.position;
			Vector3 vector = this.RoundToPixel(position) - position;
			vector.z = -vector.z;
			Matrix4x4 matrix4x = Matrix4x4.TRS(-vector, Quaternion.identity, new Vector3(1f, 1f, -1f));
			this.m_Camera.worldToCameraMatrix = matrix4x * this.m_Camera.transform.worldToLocalMatrix;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023DC File Offset: 0x000005DC
		private void Awake()
		{
			this.m_Camera = base.GetComponent<Camera>();
			this.m_Internal = new PixelPerfectCameraInternal(this);
			this.UpdateCameraProperties();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023FC File Offset: 0x000005FC
		private void UpdateCameraProperties()
		{
			Vector2Int cameraRTSize = this.cameraRTSize;
			this.m_Internal.CalculateCameraProperties(cameraRTSize.x, cameraRTSize.y);
			if (this.m_Internal.useOffscreenRT)
			{
				this.m_Camera.pixelRect = this.m_Internal.CalculateFinalBlitPixelRect(cameraRTSize.x, cameraRTSize.y);
				return;
			}
			this.m_Camera.rect = new Rect(0f, 0f, 1f, 1f);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002480 File Offset: 0x00000680
		private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			if (camera == this.m_Camera)
			{
				this.UpdateCameraProperties();
				this.PixelSnap();
				if (!this.m_CinemachineCompatibilityMode)
				{
					this.m_Camera.orthographicSize = this.m_Internal.orthoSize;
				}
				PixelPerfectRendering.pixelSnapSpacing = this.m_Internal.unitsPerPixel;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024D5 File Offset: 0x000006D5
		private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			if (camera == this.m_Camera)
			{
				PixelPerfectRendering.pixelSnapSpacing = 0f;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024EF File Offset: 0x000006EF
		private void OnEnable()
		{
			this.m_CinemachineCompatibilityMode = false;
			RenderPipelineManager.beginCameraRendering += this.OnBeginCameraRendering;
			RenderPipelineManager.endCameraRendering += this.OnEndCameraRendering;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000251C File Offset: 0x0000071C
		internal void OnDisable()
		{
			RenderPipelineManager.beginCameraRendering -= this.OnBeginCameraRendering;
			RenderPipelineManager.endCameraRendering -= this.OnEndCameraRendering;
			this.m_Camera.rect = new Rect(0f, 0f, 1f, 1f);
			this.m_Camera.ResetWorldToCameraMatrix();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000257A File Offset: 0x0000077A
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000257C File Offset: 0x0000077C
		public void OnAfterDeserialize()
		{
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
		private PixelPerfectCamera.CropFrame m_CropFrame;

		// Token: 0x04000005 RID: 5
		[SerializeField]
		private PixelPerfectCamera.GridSnapping m_GridSnapping;

		// Token: 0x04000006 RID: 6
		private Camera m_Camera;

		// Token: 0x04000007 RID: 7
		private PixelPerfectCameraInternal m_Internal;

		// Token: 0x04000008 RID: 8
		private bool m_CinemachineCompatibilityMode;

		// Token: 0x02000132 RID: 306
		public enum CropFrame
		{
			// Token: 0x04000865 RID: 2149
			None,
			// Token: 0x04000866 RID: 2150
			Pillarbox,
			// Token: 0x04000867 RID: 2151
			Letterbox,
			// Token: 0x04000868 RID: 2152
			Windowbox,
			// Token: 0x04000869 RID: 2153
			StretchFill
		}

		// Token: 0x02000133 RID: 307
		public enum GridSnapping
		{
			// Token: 0x0400086B RID: 2155
			None,
			// Token: 0x0400086C RID: 2156
			PixelSnapping,
			// Token: 0x0400086D RID: 2157
			UpscaleRenderTexture
		}

		// Token: 0x02000134 RID: 308
		private enum ComponentVersions
		{
			// Token: 0x0400086F RID: 2159
			Version_Unserialized,
			// Token: 0x04000870 RID: 2160
			Version_1
		}
	}
}
