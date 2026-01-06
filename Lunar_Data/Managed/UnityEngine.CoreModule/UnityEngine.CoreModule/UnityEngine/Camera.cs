using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000F3 RID: 243
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Camera/Camera.h")]
	[NativeHeader("Runtime/Camera/RenderManager.h")]
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	[NativeHeader("Runtime/Shaders/Shader.h")]
	[NativeHeader("Runtime/Misc/GameObjectUtility.h")]
	[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
	[UsedByNativeCode]
	public sealed class Camera : Behaviour
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004A7 RID: 1191
		// (set) Token: 0x060004A8 RID: 1192
		[NativeProperty("Near")]
		public extern float nearClipPlane
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004A9 RID: 1193
		// (set) Token: 0x060004AA RID: 1194
		[NativeProperty("Far")]
		public extern float farClipPlane
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004AB RID: 1195
		// (set) Token: 0x060004AC RID: 1196
		[NativeProperty("VerticalFieldOfView")]
		public extern float fieldOfView
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004AD RID: 1197
		// (set) Token: 0x060004AE RID: 1198
		public extern RenderingPath renderingPath
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004AF RID: 1199
		public extern RenderingPath actualRenderingPath
		{
			[NativeName("CalculateRenderingPath")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060004B0 RID: 1200
		[MethodImpl(4096)]
		public extern void Reset();

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004B1 RID: 1201
		// (set) Token: 0x060004B2 RID: 1202
		public extern bool allowHDR
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004B3 RID: 1203
		// (set) Token: 0x060004B4 RID: 1204
		public extern bool allowMSAA
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004B5 RID: 1205
		// (set) Token: 0x060004B6 RID: 1206
		public extern bool allowDynamicResolution
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004B7 RID: 1207
		// (set) Token: 0x060004B8 RID: 1208
		[NativeProperty("ForceIntoRT")]
		public extern bool forceIntoRenderTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004B9 RID: 1209
		// (set) Token: 0x060004BA RID: 1210
		public extern float orthographicSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004BB RID: 1211
		// (set) Token: 0x060004BC RID: 1212
		public extern bool orthographic
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004BD RID: 1213
		// (set) Token: 0x060004BE RID: 1214
		public extern OpaqueSortMode opaqueSortMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004BF RID: 1215
		// (set) Token: 0x060004C0 RID: 1216
		public extern TransparencySortMode transparencySortMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000761C File Offset: 0x0000581C
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00007632 File Offset: 0x00005832
		public Vector3 transparencySortAxis
		{
			get
			{
				Vector3 vector;
				this.get_transparencySortAxis_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_transparencySortAxis_Injected(ref value);
			}
		}

		// Token: 0x060004C3 RID: 1219
		[MethodImpl(4096)]
		public extern void ResetTransparencySortSettings();

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004C4 RID: 1220
		// (set) Token: 0x060004C5 RID: 1221
		public extern float depth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004C6 RID: 1222
		// (set) Token: 0x060004C7 RID: 1223
		public extern float aspect
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060004C8 RID: 1224
		[MethodImpl(4096)]
		public extern void ResetAspect();

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000763C File Offset: 0x0000583C
		public Vector3 velocity
		{
			get
			{
				Vector3 vector;
				this.get_velocity_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004CA RID: 1226
		// (set) Token: 0x060004CB RID: 1227
		public extern int cullingMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004CC RID: 1228
		// (set) Token: 0x060004CD RID: 1229
		public extern int eventMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004CE RID: 1230
		// (set) Token: 0x060004CF RID: 1231
		public extern bool layerCullSpherical
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004D0 RID: 1232
		// (set) Token: 0x060004D1 RID: 1233
		public extern CameraType cameraType
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004D2 RID: 1234
		internal extern Material skyboxMaterial
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004D3 RID: 1235
		// (set) Token: 0x060004D4 RID: 1236
		[NativeConditional("UNITY_EDITOR")]
		public extern ulong overrideSceneCullingMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004D5 RID: 1237
		[NativeConditional("UNITY_EDITOR")]
		internal extern ulong sceneCullingMask
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060004D6 RID: 1238
		[FreeFunction("CameraScripting::GetLayerCullDistances", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern float[] GetLayerCullDistances();

		// Token: 0x060004D7 RID: 1239
		[FreeFunction("CameraScripting::SetLayerCullDistances", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetLayerCullDistances([NotNull("ArgumentNullException")] float[] d);

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00007654 File Offset: 0x00005854
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0000766C File Offset: 0x0000586C
		public float[] layerCullDistances
		{
			get
			{
				return this.GetLayerCullDistances();
			}
			set
			{
				bool flag = value.Length != 32;
				if (flag)
				{
					throw new UnityException("Array needs to contain exactly 32 floats for layerCullDistances.");
				}
				this.SetLayerCullDistances(value);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000769C File Offset: 0x0000589C
		[Obsolete("PreviewCullingLayer is obsolete. Use scene culling masks instead.", false)]
		internal static int PreviewCullingLayer
		{
			get
			{
				return 31;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004DB RID: 1243
		// (set) Token: 0x060004DC RID: 1244
		public extern bool useOcclusionCulling
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x000076B0 File Offset: 0x000058B0
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x000076C6 File Offset: 0x000058C6
		public Matrix4x4 cullingMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_cullingMatrix_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.set_cullingMatrix_Injected(ref value);
			}
		}

		// Token: 0x060004DF RID: 1247
		[MethodImpl(4096)]
		public extern void ResetCullingMatrix();

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x000076D0 File Offset: 0x000058D0
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x000076E6 File Offset: 0x000058E6
		public Color backgroundColor
		{
			get
			{
				Color color;
				this.get_backgroundColor_Injected(out color);
				return color;
			}
			set
			{
				this.set_backgroundColor_Injected(ref value);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004E2 RID: 1250
		// (set) Token: 0x060004E3 RID: 1251
		public extern CameraClearFlags clearFlags
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004E4 RID: 1252
		// (set) Token: 0x060004E5 RID: 1253
		public extern DepthTextureMode depthTextureMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004E6 RID: 1254
		// (set) Token: 0x060004E7 RID: 1255
		public extern bool clearStencilAfterLightingPass
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060004E8 RID: 1256
		[MethodImpl(4096)]
		public extern void SetReplacementShader(Shader shader, string replacementTag);

		// Token: 0x060004E9 RID: 1257
		[MethodImpl(4096)]
		public extern void ResetReplacementShader();

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004EA RID: 1258
		internal extern Camera.ProjectionMatrixMode projectionMatrixMode
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004EB RID: 1259
		// (set) Token: 0x060004EC RID: 1260
		public extern bool usePhysicalProperties
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x000076F0 File Offset: 0x000058F0
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00007706 File Offset: 0x00005906
		public Vector2 sensorSize
		{
			get
			{
				Vector2 vector;
				this.get_sensorSize_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_sensorSize_Injected(ref value);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00007710 File Offset: 0x00005910
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00007726 File Offset: 0x00005926
		public Vector2 lensShift
		{
			get
			{
				Vector2 vector;
				this.get_lensShift_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_lensShift_Injected(ref value);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004F1 RID: 1265
		// (set) Token: 0x060004F2 RID: 1266
		public extern float focalLength
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004F3 RID: 1267
		// (set) Token: 0x060004F4 RID: 1268
		public extern Camera.GateFitMode gateFit
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060004F5 RID: 1269
		[MethodImpl(4096)]
		public extern float GetGateFittedFieldOfView();

		// Token: 0x060004F6 RID: 1270 RVA: 0x00007730 File Offset: 0x00005930
		public Vector2 GetGateFittedLensShift()
		{
			Vector2 vector;
			this.GetGateFittedLensShift_Injected(out vector);
			return vector;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00007748 File Offset: 0x00005948
		internal Vector3 GetLocalSpaceAim()
		{
			Vector3 vector;
			this.GetLocalSpaceAim_Injected(out vector);
			return vector;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00007760 File Offset: 0x00005960
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00007776 File Offset: 0x00005976
		[NativeProperty("NormalizedViewportRect")]
		public Rect rect
		{
			get
			{
				Rect rect;
				this.get_rect_Injected(out rect);
				return rect;
			}
			set
			{
				this.set_rect_Injected(ref value);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00007780 File Offset: 0x00005980
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x00007796 File Offset: 0x00005996
		[NativeProperty("ScreenViewportRect")]
		public Rect pixelRect
		{
			get
			{
				Rect rect;
				this.get_pixelRect_Injected(out rect);
				return rect;
			}
			set
			{
				this.set_pixelRect_Injected(ref value);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004FC RID: 1276
		public extern int pixelWidth
		{
			[FreeFunction("CameraScripting::GetPixelWidth", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004FD RID: 1277
		public extern int pixelHeight
		{
			[FreeFunction("CameraScripting::GetPixelHeight", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004FE RID: 1278
		public extern int scaledPixelWidth
		{
			[FreeFunction("CameraScripting::GetScaledPixelWidth", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004FF RID: 1279
		public extern int scaledPixelHeight
		{
			[FreeFunction("CameraScripting::GetScaledPixelHeight", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000500 RID: 1280
		// (set) Token: 0x06000501 RID: 1281
		public extern RenderTexture targetTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000502 RID: 1282
		public extern RenderTexture activeTexture
		{
			[NativeName("GetCurrentTargetTexture")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000503 RID: 1283
		// (set) Token: 0x06000504 RID: 1284
		public extern int targetDisplay
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000077A0 File Offset: 0x000059A0
		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private void SetTargetBuffersImpl(RenderBuffer color, RenderBuffer depth)
		{
			this.SetTargetBuffersImpl_Injected(ref color, ref depth);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000077AC File Offset: 0x000059AC
		public void SetTargetBuffers(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersImpl(colorBuffer, depthBuffer);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000077B8 File Offset: 0x000059B8
		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private void SetTargetBuffersMRTImpl(RenderBuffer[] color, RenderBuffer depth)
		{
			this.SetTargetBuffersMRTImpl_Injected(color, ref depth);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000077C3 File Offset: 0x000059C3
		public void SetTargetBuffers(RenderBuffer[] colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersMRTImpl(colorBuffer, depthBuffer);
		}

		// Token: 0x06000509 RID: 1289
		[MethodImpl(4096)]
		internal extern string[] GetCameraBufferWarnings();

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x000077D0 File Offset: 0x000059D0
		public Matrix4x4 cameraToWorldMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_cameraToWorldMatrix_Injected(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x000077E8 File Offset: 0x000059E8
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x000077FE File Offset: 0x000059FE
		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_worldToCameraMatrix_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.set_worldToCameraMatrix_Injected(ref value);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00007808 File Offset: 0x00005A08
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0000781E File Offset: 0x00005A1E
		public Matrix4x4 projectionMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_projectionMatrix_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.set_projectionMatrix_Injected(ref value);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00007828 File Offset: 0x00005A28
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x0000783E File Offset: 0x00005A3E
		public Matrix4x4 nonJitteredProjectionMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_nonJitteredProjectionMatrix_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.set_nonJitteredProjectionMatrix_Injected(ref value);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000511 RID: 1297
		// (set) Token: 0x06000512 RID: 1298
		[NativeProperty("UseJitteredProjectionMatrixForTransparent")]
		public extern bool useJitteredProjectionMatrixForTransparentRendering
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00007848 File Offset: 0x00005A48
		public Matrix4x4 previousViewProjectionMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_previousViewProjectionMatrix_Injected(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x06000514 RID: 1300
		[MethodImpl(4096)]
		public extern void ResetWorldToCameraMatrix();

		// Token: 0x06000515 RID: 1301
		[MethodImpl(4096)]
		public extern void ResetProjectionMatrix();

		// Token: 0x06000516 RID: 1302 RVA: 0x00007860 File Offset: 0x00005A60
		[FreeFunction("CameraScripting::CalculateObliqueMatrix", HasExplicitThis = true)]
		public Matrix4x4 CalculateObliqueMatrix(Vector4 clipPlane)
		{
			Matrix4x4 matrix4x;
			this.CalculateObliqueMatrix_Injected(ref clipPlane, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00007878 File Offset: 0x00005A78
		public Vector3 WorldToScreenPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 vector;
			this.WorldToScreenPoint_Injected(ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00007894 File Offset: 0x00005A94
		public Vector3 WorldToViewportPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 vector;
			this.WorldToViewportPoint_Injected(ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000078B0 File Offset: 0x00005AB0
		public Vector3 ViewportToWorldPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 vector;
			this.ViewportToWorldPoint_Injected(ref position, eye, out vector);
			return vector;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000078CC File Offset: 0x00005ACC
		public Vector3 ScreenToWorldPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 vector;
			this.ScreenToWorldPoint_Injected(ref position, eye, out vector);
			return vector;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000078E8 File Offset: 0x00005AE8
		public Vector3 WorldToScreenPoint(Vector3 position)
		{
			return this.WorldToScreenPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00007904 File Offset: 0x00005B04
		public Vector3 WorldToViewportPoint(Vector3 position)
		{
			return this.WorldToViewportPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00007920 File Offset: 0x00005B20
		public Vector3 ViewportToWorldPoint(Vector3 position)
		{
			return this.ViewportToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000793C File Offset: 0x00005B3C
		public Vector3 ScreenToWorldPoint(Vector3 position)
		{
			return this.ScreenToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00007958 File Offset: 0x00005B58
		public Vector3 ScreenToViewportPoint(Vector3 position)
		{
			Vector3 vector;
			this.ScreenToViewportPoint_Injected(ref position, out vector);
			return vector;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00007970 File Offset: 0x00005B70
		public Vector3 ViewportToScreenPoint(Vector3 position)
		{
			Vector3 vector;
			this.ViewportToScreenPoint_Injected(ref position, out vector);
			return vector;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00007988 File Offset: 0x00005B88
		internal Vector2 GetFrustumPlaneSizeAt(float distance)
		{
			Vector2 vector;
			this.GetFrustumPlaneSizeAt_Injected(distance, out vector);
			return vector;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000079A0 File Offset: 0x00005BA0
		private Ray ViewportPointToRay(Vector2 pos, Camera.MonoOrStereoscopicEye eye)
		{
			Ray ray;
			this.ViewportPointToRay_Injected(ref pos, eye, out ray);
			return ray;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000079BC File Offset: 0x00005BBC
		public Ray ViewportPointToRay(Vector3 pos, Camera.MonoOrStereoscopicEye eye)
		{
			return this.ViewportPointToRay(pos, eye);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000079DC File Offset: 0x00005BDC
		public Ray ViewportPointToRay(Vector3 pos)
		{
			return this.ViewportPointToRay(pos, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000079F8 File Offset: 0x00005BF8
		private Ray ScreenPointToRay(Vector2 pos, Camera.MonoOrStereoscopicEye eye)
		{
			Ray ray;
			this.ScreenPointToRay_Injected(ref pos, eye, out ray);
			return ray;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00007A14 File Offset: 0x00005C14
		public Ray ScreenPointToRay(Vector3 pos, Camera.MonoOrStereoscopicEye eye)
		{
			return this.ScreenPointToRay(pos, eye);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00007A34 File Offset: 0x00005C34
		public Ray ScreenPointToRay(Vector3 pos)
		{
			return this.ScreenPointToRay(pos, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00007A4E File Offset: 0x00005C4E
		[FreeFunction("CameraScripting::CalculateViewportRayVectors", HasExplicitThis = true)]
		private void CalculateFrustumCornersInternal(Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, [Out] Vector3[] outCorners)
		{
			this.CalculateFrustumCornersInternal_Injected(ref viewport, z, eye, outCorners);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00007A5C File Offset: 0x00005C5C
		public void CalculateFrustumCorners(Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, Vector3[] outCorners)
		{
			bool flag = outCorners == null;
			if (flag)
			{
				throw new ArgumentNullException("outCorners");
			}
			bool flag2 = outCorners.Length < 4;
			if (flag2)
			{
				throw new ArgumentException("outCorners minimum size is 4", "outCorners");
			}
			this.CalculateFrustumCornersInternal(viewport, z, eye, outCorners);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00007AA5 File Offset: 0x00005CA5
		[NativeName("CalculateProjectionMatrixFromPhysicalProperties")]
		private static void CalculateProjectionMatrixFromPhysicalPropertiesInternal(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, float gateAspect, Camera.GateFitMode gateFitMode)
		{
			Camera.CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out output, focalLength, ref sensorSize, ref lensShift, nearClip, farClip, gateAspect, gateFitMode);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00007ABA File Offset: 0x00005CBA
		public static void CalculateProjectionMatrixFromPhysicalProperties(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, Camera.GateFitParameters gateFitParameters = default(Camera.GateFitParameters))
		{
			Camera.CalculateProjectionMatrixFromPhysicalPropertiesInternal(out output, focalLength, sensorSize, lensShift, nearClip, farClip, gateFitParameters.aspect, gateFitParameters.mode);
		}

		// Token: 0x0600052C RID: 1324
		[NativeName("FocalLengthToFieldOfView_Safe")]
		[MethodImpl(4096)]
		public static extern float FocalLengthToFieldOfView(float focalLength, float sensorSize);

		// Token: 0x0600052D RID: 1325
		[NativeName("FieldOfViewToFocalLength_Safe")]
		[MethodImpl(4096)]
		public static extern float FieldOfViewToFocalLength(float fieldOfView, float sensorSize);

		// Token: 0x0600052E RID: 1326
		[NativeName("HorizontalToVerticalFieldOfView_Safe")]
		[MethodImpl(4096)]
		public static extern float HorizontalToVerticalFieldOfView(float horizontalFieldOfView, float aspectRatio);

		// Token: 0x0600052F RID: 1327
		[MethodImpl(4096)]
		public static extern float VerticalToHorizontalFieldOfView(float verticalFieldOfView, float aspectRatio);

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000530 RID: 1328
		public static extern Camera main
		{
			[FreeFunction("FindMainCamera")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000531 RID: 1329
		public static extern Camera current
		{
			[FreeFunction("GetCurrentCameraPPtr")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00007ADC File Offset: 0x00005CDC
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00007AF2 File Offset: 0x00005CF2
		public Scene scene
		{
			[FreeFunction("CameraScripting::GetScene", HasExplicitThis = true)]
			get
			{
				Scene scene;
				this.get_scene_Injected(out scene);
				return scene;
			}
			[FreeFunction("CameraScripting::SetScene", HasExplicitThis = true)]
			set
			{
				this.set_scene_Injected(ref value);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000534 RID: 1332
		public extern bool stereoEnabled
		{
			[NativeMethod("GetStereoEnabledForBuiltInOrSRP")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000535 RID: 1333
		// (set) Token: 0x06000536 RID: 1334
		public extern float stereoSeparation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000537 RID: 1335
		// (set) Token: 0x06000538 RID: 1336
		public extern float stereoConvergence
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000539 RID: 1337
		public extern bool areVRStereoViewMatricesWithinSingleCullTolerance
		{
			[NativeName("AreVRStereoViewMatricesWithinSingleCullTolerance")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600053A RID: 1338
		// (set) Token: 0x0600053B RID: 1339
		public extern StereoTargetEyeMask stereoTargetEye
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600053C RID: 1340
		public extern Camera.MonoOrStereoscopicEye stereoActiveEye
		{
			[FreeFunction("CameraScripting::GetStereoActiveEye", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00007AFC File Offset: 0x00005CFC
		public Matrix4x4 GetStereoNonJitteredProjectionMatrix(Camera.StereoscopicEye eye)
		{
			Matrix4x4 matrix4x;
			this.GetStereoNonJitteredProjectionMatrix_Injected(eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00007B14 File Offset: 0x00005D14
		[FreeFunction("CameraScripting::GetStereoViewMatrix", HasExplicitThis = true)]
		public Matrix4x4 GetStereoViewMatrix(Camera.StereoscopicEye eye)
		{
			Matrix4x4 matrix4x;
			this.GetStereoViewMatrix_Injected(eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600053F RID: 1343
		[MethodImpl(4096)]
		public extern void CopyStereoDeviceProjectionMatrixToNonJittered(Camera.StereoscopicEye eye);

		// Token: 0x06000540 RID: 1344 RVA: 0x00007B2C File Offset: 0x00005D2C
		[FreeFunction("CameraScripting::GetStereoProjectionMatrix", HasExplicitThis = true)]
		public Matrix4x4 GetStereoProjectionMatrix(Camera.StereoscopicEye eye)
		{
			Matrix4x4 matrix4x;
			this.GetStereoProjectionMatrix_Injected(eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00007B43 File Offset: 0x00005D43
		public void SetStereoProjectionMatrix(Camera.StereoscopicEye eye, Matrix4x4 matrix)
		{
			this.SetStereoProjectionMatrix_Injected(eye, ref matrix);
		}

		// Token: 0x06000542 RID: 1346
		[MethodImpl(4096)]
		public extern void ResetStereoProjectionMatrices();

		// Token: 0x06000543 RID: 1347 RVA: 0x00007B4E File Offset: 0x00005D4E
		public void SetStereoViewMatrix(Camera.StereoscopicEye eye, Matrix4x4 matrix)
		{
			this.SetStereoViewMatrix_Injected(eye, ref matrix);
		}

		// Token: 0x06000544 RID: 1348
		[MethodImpl(4096)]
		public extern void ResetStereoViewMatrices();

		// Token: 0x06000545 RID: 1349
		[FreeFunction("CameraScripting::GetAllCamerasCount")]
		[MethodImpl(4096)]
		private static extern int GetAllCamerasCount();

		// Token: 0x06000546 RID: 1350
		[FreeFunction("CameraScripting::GetAllCameras")]
		[MethodImpl(4096)]
		private static extern int GetAllCamerasImpl([NotNull("ArgumentNullException")] [Out] Camera[] cam);

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00007B5C File Offset: 0x00005D5C
		public static int allCamerasCount
		{
			get
			{
				return Camera.GetAllCamerasCount();
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00007B74 File Offset: 0x00005D74
		public static Camera[] allCameras
		{
			get
			{
				Camera[] array = new Camera[Camera.allCamerasCount];
				Camera.GetAllCamerasImpl(array);
				return array;
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00007B9C File Offset: 0x00005D9C
		public static int GetAllCameras(Camera[] cameras)
		{
			bool flag = cameras == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = cameras.Length < Camera.allCamerasCount;
			if (flag2)
			{
				throw new ArgumentException("Passed in array to fill with cameras is to small to hold the number of cameras. Use Camera.allCamerasCount to get the needed size.");
			}
			return Camera.GetAllCamerasImpl(cameras);
		}

		// Token: 0x0600054A RID: 1354
		[FreeFunction("CameraScripting::RenderToCubemap", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern bool RenderToCubemapImpl(Texture tex, [DefaultValue("63")] int faceMask);

		// Token: 0x0600054B RID: 1355 RVA: 0x00007BDC File Offset: 0x00005DDC
		public bool RenderToCubemap(Cubemap cubemap, int faceMask)
		{
			return this.RenderToCubemapImpl(cubemap, faceMask);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00007BF8 File Offset: 0x00005DF8
		public bool RenderToCubemap(Cubemap cubemap)
		{
			return this.RenderToCubemapImpl(cubemap, 63);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00007C14 File Offset: 0x00005E14
		public bool RenderToCubemap(RenderTexture cubemap, int faceMask)
		{
			return this.RenderToCubemapImpl(cubemap, faceMask);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00007C30 File Offset: 0x00005E30
		public bool RenderToCubemap(RenderTexture cubemap)
		{
			return this.RenderToCubemapImpl(cubemap, 63);
		}

		// Token: 0x0600054F RID: 1359
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(4096)]
		private extern int GetFilterMode();

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00007C4C File Offset: 0x00005E4C
		[NativeConditional("UNITY_EDITOR")]
		public Camera.SceneViewFilterMode sceneViewFilterMode
		{
			get
			{
				return (Camera.SceneViewFilterMode)this.GetFilterMode();
			}
		}

		// Token: 0x06000551 RID: 1361
		[NativeName("RenderToCubemap")]
		[MethodImpl(4096)]
		private extern bool RenderToCubemapEyeImpl(RenderTexture cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye);

		// Token: 0x06000552 RID: 1362 RVA: 0x00007C64 File Offset: 0x00005E64
		public bool RenderToCubemap(RenderTexture cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye)
		{
			return this.RenderToCubemapEyeImpl(cubemap, faceMask, stereoEye);
		}

		// Token: 0x06000553 RID: 1363
		[FreeFunction("CameraScripting::Render", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void Render();

		// Token: 0x06000554 RID: 1364
		[FreeFunction("CameraScripting::RenderWithShader", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void RenderWithShader(Shader shader, string replacementTag);

		// Token: 0x06000555 RID: 1365
		[FreeFunction("CameraScripting::RenderDontRestore", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void RenderDontRestore();

		// Token: 0x06000556 RID: 1366 RVA: 0x00007C80 File Offset: 0x00005E80
		public void SubmitRenderRequests(List<Camera.RenderRequest> renderRequests)
		{
			bool flag = renderRequests == null || renderRequests.Count == 0;
			if (flag)
			{
				throw new ArgumentException("SubmitRenderRequests has been invoked with invalid renderRequests");
			}
			bool flag2 = GraphicsSettings.currentRenderPipeline == null;
			if (flag2)
			{
				Debug.LogWarning("Trying to invoke 'SubmitRenderRequests' when no SRP is set. A scriptable render pipeline is needed for this function call");
			}
			else
			{
				this.SubmitRenderRequestsInternal(renderRequests);
			}
		}

		// Token: 0x06000557 RID: 1367
		[FreeFunction("CameraScripting::SubmitRenderRequests", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SubmitRenderRequestsInternal(object requests);

		// Token: 0x06000558 RID: 1368
		[FreeFunction("CameraScripting::SetupCurrent")]
		[MethodImpl(4096)]
		public static extern void SetupCurrent(Camera cur);

		// Token: 0x06000559 RID: 1369
		[FreeFunction("CameraScripting::CopyFrom", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void CopyFrom(Camera other);

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600055A RID: 1370
		public extern int commandBufferCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600055B RID: 1371
		[MethodImpl(4096)]
		public extern void RemoveCommandBuffers(CameraEvent evt);

		// Token: 0x0600055C RID: 1372
		[MethodImpl(4096)]
		public extern void RemoveAllCommandBuffers();

		// Token: 0x0600055D RID: 1373
		[NativeName("AddCommandBuffer")]
		[MethodImpl(4096)]
		private extern void AddCommandBufferImpl(CameraEvent evt, [NotNull("ArgumentNullException")] CommandBuffer buffer);

		// Token: 0x0600055E RID: 1374
		[NativeName("AddCommandBufferAsync")]
		[MethodImpl(4096)]
		private extern void AddCommandBufferAsyncImpl(CameraEvent evt, [NotNull("ArgumentNullException")] CommandBuffer buffer, ComputeQueueType queueType);

		// Token: 0x0600055F RID: 1375
		[NativeName("RemoveCommandBuffer")]
		[MethodImpl(4096)]
		private extern void RemoveCommandBufferImpl(CameraEvent evt, [NotNull("ArgumentNullException")] CommandBuffer buffer);

		// Token: 0x06000560 RID: 1376 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public void AddCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			this.AddCommandBufferImpl(evt, buffer);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00007D28 File Offset: 0x00005F28
		public void AddCommandBufferAsync(CameraEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			this.AddCommandBufferAsyncImpl(evt, buffer, queueType);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00007D7C File Offset: 0x00005F7C
		public void RemoveCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			this.RemoveCommandBufferImpl(evt, buffer);
		}

		// Token: 0x06000563 RID: 1379
		[FreeFunction("CameraScripting::GetCommandBuffers", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern CommandBuffer[] GetCommandBuffers(CameraEvent evt);

		// Token: 0x06000564 RID: 1380 RVA: 0x00007DD0 File Offset: 0x00005FD0
		[RequiredByNativeCode]
		private static void FireOnPreCull(Camera cam)
		{
			bool flag = Camera.onPreCull != null;
			if (flag)
			{
				Camera.onPreCull(cam);
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00007DF8 File Offset: 0x00005FF8
		[RequiredByNativeCode]
		private static void FireOnPreRender(Camera cam)
		{
			bool flag = Camera.onPreRender != null;
			if (flag)
			{
				Camera.onPreRender(cam);
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00007E20 File Offset: 0x00006020
		[RequiredByNativeCode]
		private static void FireOnPostRender(Camera cam)
		{
			bool flag = Camera.onPostRender != null;
			if (flag)
			{
				Camera.onPostRender(cam);
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00004557 File Offset: 0x00002757
		internal void OnlyUsedForTesting1()
		{
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00004557 File Offset: 0x00002757
		internal void OnlyUsedForTesting2()
		{
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00007E48 File Offset: 0x00006048
		public unsafe bool TryGetCullingParameters(out ScriptableCullingParameters cullingParameters)
		{
			return Camera.GetCullingParameters_Internal(this, false, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00007E68 File Offset: 0x00006068
		public unsafe bool TryGetCullingParameters(bool stereoAware, out ScriptableCullingParameters cullingParameters)
		{
			return Camera.GetCullingParameters_Internal(this, stereoAware, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		// Token: 0x0600056B RID: 1387
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetCullingParameters_Internal")]
		[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
		[MethodImpl(4096)]
		private static extern bool GetCullingParameters_Internal(Camera camera, bool stereoAware, out ScriptableCullingParameters cullingParameters, int managedCullingParametersSize);

		// Token: 0x0600056C RID: 1388
		[MethodImpl(4096)]
		private extern void get_transparencySortAxis_Injected(out Vector3 ret);

		// Token: 0x0600056D RID: 1389
		[MethodImpl(4096)]
		private extern void set_transparencySortAxis_Injected(ref Vector3 value);

		// Token: 0x0600056E RID: 1390
		[MethodImpl(4096)]
		private extern void get_velocity_Injected(out Vector3 ret);

		// Token: 0x0600056F RID: 1391
		[MethodImpl(4096)]
		private extern void get_cullingMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000570 RID: 1392
		[MethodImpl(4096)]
		private extern void set_cullingMatrix_Injected(ref Matrix4x4 value);

		// Token: 0x06000571 RID: 1393
		[MethodImpl(4096)]
		private extern void get_backgroundColor_Injected(out Color ret);

		// Token: 0x06000572 RID: 1394
		[MethodImpl(4096)]
		private extern void set_backgroundColor_Injected(ref Color value);

		// Token: 0x06000573 RID: 1395
		[MethodImpl(4096)]
		private extern void get_sensorSize_Injected(out Vector2 ret);

		// Token: 0x06000574 RID: 1396
		[MethodImpl(4096)]
		private extern void set_sensorSize_Injected(ref Vector2 value);

		// Token: 0x06000575 RID: 1397
		[MethodImpl(4096)]
		private extern void get_lensShift_Injected(out Vector2 ret);

		// Token: 0x06000576 RID: 1398
		[MethodImpl(4096)]
		private extern void set_lensShift_Injected(ref Vector2 value);

		// Token: 0x06000577 RID: 1399
		[MethodImpl(4096)]
		private extern void GetGateFittedLensShift_Injected(out Vector2 ret);

		// Token: 0x06000578 RID: 1400
		[MethodImpl(4096)]
		private extern void GetLocalSpaceAim_Injected(out Vector3 ret);

		// Token: 0x06000579 RID: 1401
		[MethodImpl(4096)]
		private extern void get_rect_Injected(out Rect ret);

		// Token: 0x0600057A RID: 1402
		[MethodImpl(4096)]
		private extern void set_rect_Injected(ref Rect value);

		// Token: 0x0600057B RID: 1403
		[MethodImpl(4096)]
		private extern void get_pixelRect_Injected(out Rect ret);

		// Token: 0x0600057C RID: 1404
		[MethodImpl(4096)]
		private extern void set_pixelRect_Injected(ref Rect value);

		// Token: 0x0600057D RID: 1405
		[MethodImpl(4096)]
		private extern void SetTargetBuffersImpl_Injected(ref RenderBuffer color, ref RenderBuffer depth);

		// Token: 0x0600057E RID: 1406
		[MethodImpl(4096)]
		private extern void SetTargetBuffersMRTImpl_Injected(RenderBuffer[] color, ref RenderBuffer depth);

		// Token: 0x0600057F RID: 1407
		[MethodImpl(4096)]
		private extern void get_cameraToWorldMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000580 RID: 1408
		[MethodImpl(4096)]
		private extern void get_worldToCameraMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000581 RID: 1409
		[MethodImpl(4096)]
		private extern void set_worldToCameraMatrix_Injected(ref Matrix4x4 value);

		// Token: 0x06000582 RID: 1410
		[MethodImpl(4096)]
		private extern void get_projectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000583 RID: 1411
		[MethodImpl(4096)]
		private extern void set_projectionMatrix_Injected(ref Matrix4x4 value);

		// Token: 0x06000584 RID: 1412
		[MethodImpl(4096)]
		private extern void get_nonJitteredProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000585 RID: 1413
		[MethodImpl(4096)]
		private extern void set_nonJitteredProjectionMatrix_Injected(ref Matrix4x4 value);

		// Token: 0x06000586 RID: 1414
		[MethodImpl(4096)]
		private extern void get_previousViewProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000587 RID: 1415
		[MethodImpl(4096)]
		private extern void CalculateObliqueMatrix_Injected(ref Vector4 clipPlane, out Matrix4x4 ret);

		// Token: 0x06000588 RID: 1416
		[MethodImpl(4096)]
		private extern void WorldToScreenPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x06000589 RID: 1417
		[MethodImpl(4096)]
		private extern void WorldToViewportPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x0600058A RID: 1418
		[MethodImpl(4096)]
		private extern void ViewportToWorldPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x0600058B RID: 1419
		[MethodImpl(4096)]
		private extern void ScreenToWorldPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x0600058C RID: 1420
		[MethodImpl(4096)]
		private extern void ScreenToViewportPoint_Injected(ref Vector3 position, out Vector3 ret);

		// Token: 0x0600058D RID: 1421
		[MethodImpl(4096)]
		private extern void ViewportToScreenPoint_Injected(ref Vector3 position, out Vector3 ret);

		// Token: 0x0600058E RID: 1422
		[MethodImpl(4096)]
		private extern void GetFrustumPlaneSizeAt_Injected(float distance, out Vector2 ret);

		// Token: 0x0600058F RID: 1423
		[MethodImpl(4096)]
		private extern void ViewportPointToRay_Injected(ref Vector2 pos, Camera.MonoOrStereoscopicEye eye, out Ray ret);

		// Token: 0x06000590 RID: 1424
		[MethodImpl(4096)]
		private extern void ScreenPointToRay_Injected(ref Vector2 pos, Camera.MonoOrStereoscopicEye eye, out Ray ret);

		// Token: 0x06000591 RID: 1425
		[MethodImpl(4096)]
		private extern void CalculateFrustumCornersInternal_Injected(ref Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, [Out] Vector3[] outCorners);

		// Token: 0x06000592 RID: 1426
		[MethodImpl(4096)]
		private static extern void CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out Matrix4x4 output, float focalLength, ref Vector2 sensorSize, ref Vector2 lensShift, float nearClip, float farClip, float gateAspect, Camera.GateFitMode gateFitMode);

		// Token: 0x06000593 RID: 1427
		[MethodImpl(4096)]
		private extern void get_scene_Injected(out Scene ret);

		// Token: 0x06000594 RID: 1428
		[MethodImpl(4096)]
		private extern void set_scene_Injected(ref Scene value);

		// Token: 0x06000595 RID: 1429
		[MethodImpl(4096)]
		private extern void GetStereoNonJitteredProjectionMatrix_Injected(Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x06000596 RID: 1430
		[MethodImpl(4096)]
		private extern void GetStereoViewMatrix_Injected(Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x06000597 RID: 1431
		[MethodImpl(4096)]
		private extern void GetStereoProjectionMatrix_Injected(Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x06000598 RID: 1432
		[MethodImpl(4096)]
		private extern void SetStereoProjectionMatrix_Injected(Camera.StereoscopicEye eye, ref Matrix4x4 matrix);

		// Token: 0x06000599 RID: 1433
		[MethodImpl(4096)]
		private extern void SetStereoViewMatrix_Injected(Camera.StereoscopicEye eye, ref Matrix4x4 matrix);

		// Token: 0x0400032A RID: 810
		public static Camera.CameraCallback onPreCull;

		// Token: 0x0400032B RID: 811
		public static Camera.CameraCallback onPreRender;

		// Token: 0x0400032C RID: 812
		public static Camera.CameraCallback onPostRender;

		// Token: 0x020000F4 RID: 244
		internal enum ProjectionMatrixMode
		{
			// Token: 0x0400032E RID: 814
			Explicit,
			// Token: 0x0400032F RID: 815
			Implicit,
			// Token: 0x04000330 RID: 816
			PhysicalPropertiesBased
		}

		// Token: 0x020000F5 RID: 245
		public enum GateFitMode
		{
			// Token: 0x04000332 RID: 818
			Vertical = 1,
			// Token: 0x04000333 RID: 819
			Horizontal,
			// Token: 0x04000334 RID: 820
			Fill,
			// Token: 0x04000335 RID: 821
			Overscan,
			// Token: 0x04000336 RID: 822
			None = 0
		}

		// Token: 0x020000F6 RID: 246
		public enum FieldOfViewAxis
		{
			// Token: 0x04000338 RID: 824
			Vertical,
			// Token: 0x04000339 RID: 825
			Horizontal
		}

		// Token: 0x020000F7 RID: 247
		public struct GateFitParameters
		{
			// Token: 0x1700012A RID: 298
			// (get) Token: 0x0600059A RID: 1434 RVA: 0x00007E88 File Offset: 0x00006088
			// (set) Token: 0x0600059B RID: 1435 RVA: 0x00007E90 File Offset: 0x00006090
			public Camera.GateFitMode mode { readonly get; set; }

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x0600059C RID: 1436 RVA: 0x00007E99 File Offset: 0x00006099
			// (set) Token: 0x0600059D RID: 1437 RVA: 0x00007EA1 File Offset: 0x000060A1
			public float aspect { readonly get; set; }

			// Token: 0x0600059E RID: 1438 RVA: 0x00007EAA File Offset: 0x000060AA
			public GateFitParameters(Camera.GateFitMode mode, float aspect)
			{
				this.mode = mode;
				this.aspect = aspect;
			}
		}

		// Token: 0x020000F8 RID: 248
		public enum StereoscopicEye
		{
			// Token: 0x0400033D RID: 829
			Left,
			// Token: 0x0400033E RID: 830
			Right
		}

		// Token: 0x020000F9 RID: 249
		public enum MonoOrStereoscopicEye
		{
			// Token: 0x04000340 RID: 832
			Left,
			// Token: 0x04000341 RID: 833
			Right,
			// Token: 0x04000342 RID: 834
			Mono
		}

		// Token: 0x020000FA RID: 250
		public enum SceneViewFilterMode
		{
			// Token: 0x04000344 RID: 836
			Off,
			// Token: 0x04000345 RID: 837
			ShowFiltered
		}

		// Token: 0x020000FB RID: 251
		public enum RenderRequestMode
		{
			// Token: 0x04000347 RID: 839
			None,
			// Token: 0x04000348 RID: 840
			ObjectId,
			// Token: 0x04000349 RID: 841
			Depth,
			// Token: 0x0400034A RID: 842
			VertexNormal,
			// Token: 0x0400034B RID: 843
			WorldPosition,
			// Token: 0x0400034C RID: 844
			EntityId,
			// Token: 0x0400034D RID: 845
			BaseColor,
			// Token: 0x0400034E RID: 846
			SpecularColor,
			// Token: 0x0400034F RID: 847
			Metallic,
			// Token: 0x04000350 RID: 848
			Emission,
			// Token: 0x04000351 RID: 849
			Normal,
			// Token: 0x04000352 RID: 850
			Smoothness,
			// Token: 0x04000353 RID: 851
			Occlusion,
			// Token: 0x04000354 RID: 852
			DiffuseColor
		}

		// Token: 0x020000FC RID: 252
		public enum RenderRequestOutputSpace
		{
			// Token: 0x04000356 RID: 854
			ScreenSpace = -1,
			// Token: 0x04000357 RID: 855
			UV0,
			// Token: 0x04000358 RID: 856
			UV1,
			// Token: 0x04000359 RID: 857
			UV2,
			// Token: 0x0400035A RID: 858
			UV3,
			// Token: 0x0400035B RID: 859
			UV4,
			// Token: 0x0400035C RID: 860
			UV5,
			// Token: 0x0400035D RID: 861
			UV6,
			// Token: 0x0400035E RID: 862
			UV7,
			// Token: 0x0400035F RID: 863
			UV8
		}

		// Token: 0x020000FD RID: 253
		public struct RenderRequest
		{
			// Token: 0x0600059F RID: 1439 RVA: 0x00007EBD File Offset: 0x000060BD
			public RenderRequest(Camera.RenderRequestMode mode, RenderTexture rt)
			{
				this.m_CameraRenderMode = mode;
				this.m_ResultRT = rt;
				this.m_OutputSpace = Camera.RenderRequestOutputSpace.ScreenSpace;
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x00007ED5 File Offset: 0x000060D5
			public RenderRequest(Camera.RenderRequestMode mode, Camera.RenderRequestOutputSpace space, RenderTexture rt)
			{
				this.m_CameraRenderMode = mode;
				this.m_ResultRT = rt;
				this.m_OutputSpace = space;
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00007EED File Offset: 0x000060ED
			public bool isValid
			{
				get
				{
					return this.m_CameraRenderMode != Camera.RenderRequestMode.None && this.m_ResultRT != null;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00007F06 File Offset: 0x00006106
			public Camera.RenderRequestMode mode
			{
				get
				{
					return this.m_CameraRenderMode;
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00007F0E File Offset: 0x0000610E
			public RenderTexture result
			{
				get
				{
					return this.m_ResultRT;
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00007F16 File Offset: 0x00006116
			public Camera.RenderRequestOutputSpace outputSpace
			{
				get
				{
					return this.m_OutputSpace;
				}
			}

			// Token: 0x04000360 RID: 864
			private readonly Camera.RenderRequestMode m_CameraRenderMode;

			// Token: 0x04000361 RID: 865
			private readonly RenderTexture m_ResultRT;

			// Token: 0x04000362 RID: 866
			private readonly Camera.RenderRequestOutputSpace m_OutputSpace;
		}

		// Token: 0x020000FE RID: 254
		// (Invoke) Token: 0x060005A6 RID: 1446
		public delegate void CameraCallback(Camera cam);
	}
}
