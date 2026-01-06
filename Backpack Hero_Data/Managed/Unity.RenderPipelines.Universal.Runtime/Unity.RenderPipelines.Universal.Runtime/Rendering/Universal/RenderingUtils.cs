using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B5 RID: 181
	public static class RenderingUtils
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001EB5B File Offset: 0x0001CD5B
		internal static AttachmentDescriptor emptyAttachment
		{
			get
			{
				return RenderingUtils.s_EmptyAttachment;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001EB64 File Offset: 0x0001CD64
		public static Mesh fullscreenMesh
		{
			get
			{
				if (RenderingUtils.s_FullscreenMesh != null)
				{
					return RenderingUtils.s_FullscreenMesh;
				}
				float num = 1f;
				float num2 = 0f;
				RenderingUtils.s_FullscreenMesh = new Mesh
				{
					name = "Fullscreen Quad"
				};
				RenderingUtils.s_FullscreenMesh.SetVertices(new List<Vector3>
				{
					new Vector3(-1f, -1f, 0f),
					new Vector3(-1f, 1f, 0f),
					new Vector3(1f, -1f, 0f),
					new Vector3(1f, 1f, 0f)
				});
				RenderingUtils.s_FullscreenMesh.SetUVs(0, new List<Vector2>
				{
					new Vector2(0f, num2),
					new Vector2(0f, num),
					new Vector2(1f, num2),
					new Vector2(1f, num)
				});
				RenderingUtils.s_FullscreenMesh.SetIndices(new int[] { 0, 1, 2, 2, 1, 3 }, MeshTopology.Triangles, 0, false);
				RenderingUtils.s_FullscreenMesh.UploadMeshData(true);
				return RenderingUtils.s_FullscreenMesh;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001EC9E File Offset: 0x0001CE9E
		internal static bool useStructuredBuffer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001ECA1 File Offset: 0x0001CEA1
		internal static bool SupportsLightLayers(GraphicsDeviceType type)
		{
			return type != GraphicsDeviceType.OpenGLES2;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0001ECAC File Offset: 0x0001CEAC
		private static Material errorMaterial
		{
			get
			{
				if (RenderingUtils.s_ErrorMaterial == null)
				{
					try
					{
						RenderingUtils.s_ErrorMaterial = new Material(Shader.Find("Hidden/Universal Render Pipeline/FallbackError"));
					}
					catch
					{
					}
				}
				return RenderingUtils.s_ErrorMaterial;
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001ECF4 File Offset: 0x0001CEF4
		public static void SetViewAndProjectionMatrices(CommandBuffer cmd, Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix, bool setInverseMatrices)
		{
			Matrix4x4 matrix4x = projectionMatrix * viewMatrix;
			cmd.SetGlobalMatrix(ShaderPropertyId.viewMatrix, viewMatrix);
			cmd.SetGlobalMatrix(ShaderPropertyId.projectionMatrix, projectionMatrix);
			cmd.SetGlobalMatrix(ShaderPropertyId.viewAndProjectionMatrix, matrix4x);
			if (setInverseMatrices)
			{
				Matrix4x4 matrix4x2 = Matrix4x4.Inverse(viewMatrix);
				Matrix4x4 matrix4x3 = Matrix4x4.Inverse(projectionMatrix);
				Matrix4x4 matrix4x4 = matrix4x2 * matrix4x3;
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseViewMatrix, matrix4x2);
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseProjectionMatrix, matrix4x3);
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseViewAndProjectionMatrix, matrix4x4);
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001ED6C File Offset: 0x0001CF6C
		internal static void SetStereoViewAndProjectionMatrices(CommandBuffer cmd, Matrix4x4[] viewMatrix, Matrix4x4[] projMatrix, Matrix4x4[] cameraProjMatrix, bool setInverseMatrices)
		{
			for (int i = 0; i < 2; i++)
			{
				RenderingUtils.stereoConstants.viewProjMatrix[i] = projMatrix[i] * viewMatrix[i];
				RenderingUtils.stereoConstants.invViewMatrix[i] = Matrix4x4.Inverse(viewMatrix[i]);
				RenderingUtils.stereoConstants.invProjMatrix[i] = Matrix4x4.Inverse(projMatrix[i]);
				RenderingUtils.stereoConstants.invViewProjMatrix[i] = Matrix4x4.Inverse(RenderingUtils.stereoConstants.viewProjMatrix[i]);
				RenderingUtils.stereoConstants.invCameraProjMatrix[i] = Matrix4x4.Inverse(cameraProjMatrix[i]);
				RenderingUtils.stereoConstants.worldSpaceCameraPos[i] = RenderingUtils.stereoConstants.invViewMatrix[i].GetColumn(3);
			}
			cmd.SetGlobalMatrixArray(RenderingUtils.UNITY_STEREO_MATRIX_V, viewMatrix);
			cmd.SetGlobalMatrixArray(RenderingUtils.UNITY_STEREO_MATRIX_P, projMatrix);
			cmd.SetGlobalMatrixArray(RenderingUtils.UNITY_STEREO_MATRIX_VP, RenderingUtils.stereoConstants.viewProjMatrix);
			cmd.SetGlobalMatrixArray(RenderingUtils.UNITY_STEREO_CAMERA_PROJECTION, cameraProjMatrix);
			if (setInverseMatrices)
			{
				cmd.SetGlobalMatrixArray(RenderingUtils.UNITY_STEREO_MATRIX_IV, RenderingUtils.stereoConstants.invViewMatrix);
				cmd.SetGlobalMatrixArray(RenderingUtils.UNITY_STEREO_MATRIX_IP, RenderingUtils.stereoConstants.invProjMatrix);
				cmd.SetGlobalMatrixArray(RenderingUtils.UNITY_STEREO_MATRIX_IVP, RenderingUtils.stereoConstants.invViewProjMatrix);
				cmd.SetGlobalMatrixArray(RenderingUtils.UNITY_STEREO_CAMERA_INV_PROJECTION, RenderingUtils.stereoConstants.invCameraProjMatrix);
			}
			cmd.SetGlobalVectorArray(RenderingUtils.UNITY_STEREO_VECTOR_CAMPOS, RenderingUtils.stereoConstants.worldSpaceCameraPos);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001EEF4 File Offset: 0x0001D0F4
		internal static void SetScaleBiasRt(CommandBuffer cmd, in RenderingData renderingData)
		{
			ScriptableRenderer renderer = renderingData.cameraData.renderer;
			CameraData cameraData = renderingData.cameraData;
			float num = ((cameraData.cameraType != CameraType.Game || !(renderer.cameraColorTarget == BuiltinRenderTextureType.CameraTarget) || !(cameraData.camera.targetTexture == null)) ? (-1f) : 1f);
			Vector4 vector = ((num < 0f) ? new Vector4(num, 1f, -1f, 1f) : new Vector4(num, 0f, 1f, 1f));
			cmd.SetGlobalVector(Shader.PropertyToID("_ScaleBiasRt"), vector);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001EF9C File Offset: 0x0001D19C
		internal static void Blit(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material, int passIndex = 0, bool useDrawProcedural = false, RenderBufferLoadAction colorLoadAction = RenderBufferLoadAction.Load, RenderBufferStoreAction colorStoreAction = RenderBufferStoreAction.Store, RenderBufferLoadAction depthLoadAction = RenderBufferLoadAction.Load, RenderBufferStoreAction depthStoreAction = RenderBufferStoreAction.Store)
		{
			cmd.SetGlobalTexture(ShaderPropertyId.sourceTex, source);
			if (useDrawProcedural)
			{
				Vector4 vector = new Vector4(1f, 1f, 0f, 0f);
				Vector4 vector2 = new Vector4(1f, 1f, 0f, 0f);
				cmd.SetGlobalVector(ShaderPropertyId.scaleBias, vector);
				cmd.SetGlobalVector(ShaderPropertyId.scaleBiasRt, vector2);
				cmd.SetRenderTarget(new RenderTargetIdentifier(destination, 0, CubemapFace.Unknown, -1), colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
				cmd.DrawProcedural(Matrix4x4.identity, material, passIndex, MeshTopology.Quads, 4, 1, null);
				return;
			}
			cmd.SetRenderTarget(destination, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
			cmd.Blit(source, BuiltinRenderTextureType.CurrentActive, material, passIndex);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001F050 File Offset: 0x0001D250
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal static void RenderObjectsWithError(ScriptableRenderContext context, ref CullingResults cullResults, Camera camera, FilteringSettings filterSettings, SortingCriteria sortFlags)
		{
			if (RenderingUtils.errorMaterial == null)
			{
				return;
			}
			SortingSettings sortingSettings = new SortingSettings(camera)
			{
				criteria = sortFlags
			};
			DrawingSettings drawingSettings = new DrawingSettings(RenderingUtils.m_LegacyShaderPassNames[0], sortingSettings)
			{
				perObjectData = PerObjectData.None,
				overrideMaterial = RenderingUtils.errorMaterial,
				overrideMaterialPassIndex = 0
			};
			for (int i = 1; i < RenderingUtils.m_LegacyShaderPassNames.Count; i++)
			{
				drawingSettings.SetShaderPassName(i, RenderingUtils.m_LegacyShaderPassNames[i]);
			}
			context.DrawRenderers(cullResults, ref drawingSettings, ref filterSettings);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001F0EE File Offset: 0x0001D2EE
		internal static void ClearSystemInfoCache()
		{
			RenderingUtils.m_RenderTextureFormatSupport.Clear();
			RenderingUtils.m_GraphicsFormatSupport.Clear();
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001F104 File Offset: 0x0001D304
		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag;
			if (!RenderingUtils.m_RenderTextureFormatSupport.TryGetValue(format, out flag))
			{
				flag = SystemInfo.SupportsRenderTextureFormat(format);
				RenderingUtils.m_RenderTextureFormatSupport.Add(format, flag);
			}
			return flag;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001F134 File Offset: 0x0001D334
		public static bool SupportsGraphicsFormat(GraphicsFormat format, FormatUsage usage)
		{
			bool flag = false;
			Dictionary<FormatUsage, bool> dictionary;
			if (!RenderingUtils.m_GraphicsFormatSupport.TryGetValue(format, out dictionary))
			{
				dictionary = new Dictionary<FormatUsage, bool>();
				flag = SystemInfo.IsFormatSupported(format, usage);
				dictionary.Add(usage, flag);
				RenderingUtils.m_GraphicsFormatSupport.Add(format, dictionary);
			}
			else if (!dictionary.TryGetValue(usage, out flag))
			{
				flag = SystemInfo.IsFormatSupported(format, usage);
				dictionary.Add(usage, flag);
			}
			return flag;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001F194 File Offset: 0x0001D394
		internal static int GetLastValidColorBufferIndex(RenderTargetIdentifier[] colorBuffers)
		{
			int num = colorBuffers.Length - 1;
			while (num >= 0 && !(colorBuffers[num] != 0))
			{
				num--;
			}
			return num;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
		internal static uint GetValidColorBufferCount(RenderTargetIdentifier[] colorBuffers)
		{
			uint num = 0U;
			if (colorBuffers != null)
			{
				for (int i = 0; i < colorBuffers.Length; i++)
				{
					if (colorBuffers[i] != 0)
					{
						num += 1U;
					}
				}
			}
			return num;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001F203 File Offset: 0x0001D403
		internal static bool IsMRT(RenderTargetIdentifier[] colorBuffers)
		{
			return RenderingUtils.GetValidColorBufferCount(colorBuffers) > 1U;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001F210 File Offset: 0x0001D410
		internal static bool Contains(RenderTargetIdentifier[] source, RenderTargetIdentifier value)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001F240 File Offset: 0x0001D440
		internal static int IndexOf(RenderTargetIdentifier[] source, RenderTargetIdentifier value)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001F270 File Offset: 0x0001D470
		internal static uint CountDistinct(RenderTargetIdentifier[] source, RenderTargetIdentifier value)
		{
			uint num = 0U;
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] != value && source[i] != 0)
				{
					num += 1U;
				}
			}
			return num;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		internal static int LastValid(RenderTargetIdentifier[] source)
		{
			for (int i = source.Length - 1; i >= 0; i--)
			{
				if (source[i] != 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		internal static bool Contains(ClearFlag a, ClearFlag b)
		{
			return (a & b) == b;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001F2F4 File Offset: 0x0001D4F4
		internal static bool SequenceEqual(RenderTargetIdentifier[] left, RenderTargetIdentifier[] right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400044C RID: 1100
		private static List<ShaderTagId> m_LegacyShaderPassNames = new List<ShaderTagId>
		{
			new ShaderTagId("Always"),
			new ShaderTagId("ForwardBase"),
			new ShaderTagId("PrepassBase"),
			new ShaderTagId("Vertex"),
			new ShaderTagId("VertexLMRGBM"),
			new ShaderTagId("VertexLM")
		};

		// Token: 0x0400044D RID: 1101
		private static AttachmentDescriptor s_EmptyAttachment = new AttachmentDescriptor(GraphicsFormat.None);

		// Token: 0x0400044E RID: 1102
		private static Mesh s_FullscreenMesh = null;

		// Token: 0x0400044F RID: 1103
		private static Material s_ErrorMaterial;

		// Token: 0x04000450 RID: 1104
		internal static readonly int UNITY_STEREO_MATRIX_V = Shader.PropertyToID("unity_StereoMatrixV");

		// Token: 0x04000451 RID: 1105
		internal static readonly int UNITY_STEREO_MATRIX_IV = Shader.PropertyToID("unity_StereoMatrixInvV");

		// Token: 0x04000452 RID: 1106
		internal static readonly int UNITY_STEREO_MATRIX_P = Shader.PropertyToID("unity_StereoMatrixP");

		// Token: 0x04000453 RID: 1107
		internal static readonly int UNITY_STEREO_MATRIX_IP = Shader.PropertyToID("unity_StereoMatrixInvP");

		// Token: 0x04000454 RID: 1108
		internal static readonly int UNITY_STEREO_MATRIX_VP = Shader.PropertyToID("unity_StereoMatrixVP");

		// Token: 0x04000455 RID: 1109
		internal static readonly int UNITY_STEREO_MATRIX_IVP = Shader.PropertyToID("unity_StereoMatrixInvVP");

		// Token: 0x04000456 RID: 1110
		internal static readonly int UNITY_STEREO_CAMERA_PROJECTION = Shader.PropertyToID("unity_StereoCameraProjection");

		// Token: 0x04000457 RID: 1111
		internal static readonly int UNITY_STEREO_CAMERA_INV_PROJECTION = Shader.PropertyToID("unity_StereoCameraInvProjection");

		// Token: 0x04000458 RID: 1112
		internal static readonly int UNITY_STEREO_VECTOR_CAMPOS = Shader.PropertyToID("unity_StereoWorldSpaceCameraPos");

		// Token: 0x04000459 RID: 1113
		private static readonly RenderingUtils.StereoConstants stereoConstants = new RenderingUtils.StereoConstants();

		// Token: 0x0400045A RID: 1114
		private static Dictionary<RenderTextureFormat, bool> m_RenderTextureFormatSupport = new Dictionary<RenderTextureFormat, bool>();

		// Token: 0x0400045B RID: 1115
		private static Dictionary<GraphicsFormat, Dictionary<FormatUsage, bool>> m_GraphicsFormatSupport = new Dictionary<GraphicsFormat, Dictionary<FormatUsage, bool>>();

		// Token: 0x02000181 RID: 385
		internal class StereoConstants
		{
			// Token: 0x040009DC RID: 2524
			public Matrix4x4[] viewProjMatrix = new Matrix4x4[2];

			// Token: 0x040009DD RID: 2525
			public Matrix4x4[] invViewMatrix = new Matrix4x4[2];

			// Token: 0x040009DE RID: 2526
			public Matrix4x4[] invProjMatrix = new Matrix4x4[2];

			// Token: 0x040009DF RID: 2527
			public Matrix4x4[] invViewProjMatrix = new Matrix4x4[2];

			// Token: 0x040009E0 RID: 2528
			public Matrix4x4[] invCameraProjMatrix = new Matrix4x4[2];

			// Token: 0x040009E1 RID: 2529
			public Vector4[] worldSpaceCameraPos = new Vector4[2];
		}
	}
}
