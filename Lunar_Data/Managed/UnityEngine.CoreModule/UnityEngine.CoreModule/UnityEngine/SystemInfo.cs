using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200024C RID: 588
	[NativeHeader("Runtime/Misc/SystemInfo.h")]
	[NativeHeader("Runtime/Shaders/GraphicsCapsScriptBindings.h")]
	[NativeHeader("Runtime/Camera/RenderLoops/MotionVectorRenderLoop.h")]
	[NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h")]
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	[NativeHeader("Runtime/Input/GetInput.h")]
	public sealed class SystemInfo
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x00028BA0 File Offset: 0x00026DA0
		[NativeProperty]
		public static float batteryLevel
		{
			get
			{
				return SystemInfo.GetBatteryLevel();
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x00028BB8 File Offset: 0x00026DB8
		public static BatteryStatus batteryStatus
		{
			get
			{
				return SystemInfo.GetBatteryStatus();
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x00028BD0 File Offset: 0x00026DD0
		public static string operatingSystem
		{
			get
			{
				return SystemInfo.GetOperatingSystem();
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x00028BE8 File Offset: 0x00026DE8
		public static OperatingSystemFamily operatingSystemFamily
		{
			get
			{
				return SystemInfo.GetOperatingSystemFamily();
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x00028C00 File Offset: 0x00026E00
		public static string processorType
		{
			get
			{
				return SystemInfo.GetProcessorType();
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x00028C18 File Offset: 0x00026E18
		public static int processorFrequency
		{
			get
			{
				return SystemInfo.GetProcessorFrequencyMHz();
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x00028C30 File Offset: 0x00026E30
		public static int processorCount
		{
			get
			{
				return SystemInfo.GetProcessorCount();
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00028C48 File Offset: 0x00026E48
		public static int systemMemorySize
		{
			get
			{
				return SystemInfo.GetPhysicalMemoryMB();
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x00028C60 File Offset: 0x00026E60
		public static string deviceUniqueIdentifier
		{
			get
			{
				return SystemInfo.GetDeviceUniqueIdentifier();
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00028C78 File Offset: 0x00026E78
		public static string deviceName
		{
			get
			{
				return SystemInfo.GetDeviceName();
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x00028C90 File Offset: 0x00026E90
		public static string deviceModel
		{
			get
			{
				return SystemInfo.GetDeviceModel();
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x00028CA8 File Offset: 0x00026EA8
		public static bool supportsAccelerometer
		{
			get
			{
				return SystemInfo.SupportsAccelerometer();
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00028CC0 File Offset: 0x00026EC0
		public static bool supportsGyroscope
		{
			get
			{
				return SystemInfo.IsGyroAvailable();
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00028CD8 File Offset: 0x00026ED8
		public static bool supportsLocationService
		{
			get
			{
				return SystemInfo.SupportsLocationService();
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00028CF0 File Offset: 0x00026EF0
		public static bool supportsVibration
		{
			get
			{
				return SystemInfo.SupportsVibration();
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x00028D08 File Offset: 0x00026F08
		public static bool supportsAudio
		{
			get
			{
				return SystemInfo.SupportsAudio();
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00028D20 File Offset: 0x00026F20
		public static DeviceType deviceType
		{
			get
			{
				return SystemInfo.GetDeviceType();
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x00028D38 File Offset: 0x00026F38
		public static int graphicsMemorySize
		{
			get
			{
				return SystemInfo.GetGraphicsMemorySize();
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x00028D50 File Offset: 0x00026F50
		public static string graphicsDeviceName
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceName();
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x00028D68 File Offset: 0x00026F68
		public static string graphicsDeviceVendor
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceVendor();
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00028D80 File Offset: 0x00026F80
		public static int graphicsDeviceID
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceID();
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x00028D98 File Offset: 0x00026F98
		public static int graphicsDeviceVendorID
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceVendorID();
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x00028DB0 File Offset: 0x00026FB0
		public static GraphicsDeviceType graphicsDeviceType
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceType();
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x00028DC8 File Offset: 0x00026FC8
		public static bool graphicsUVStartsAtTop
		{
			get
			{
				return SystemInfo.GetGraphicsUVStartsAtTop();
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x00028DE0 File Offset: 0x00026FE0
		public static string graphicsDeviceVersion
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceVersion();
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00028DF8 File Offset: 0x00026FF8
		public static int graphicsShaderLevel
		{
			get
			{
				return SystemInfo.GetGraphicsShaderLevel();
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x00028E10 File Offset: 0x00027010
		public static bool graphicsMultiThreaded
		{
			get
			{
				return SystemInfo.GetGraphicsMultiThreaded();
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x00028E28 File Offset: 0x00027028
		public static RenderingThreadingMode renderingThreadingMode
		{
			get
			{
				return SystemInfo.GetRenderingThreadingMode();
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x00028E40 File Offset: 0x00027040
		public static bool hasHiddenSurfaceRemovalOnGPU
		{
			get
			{
				return SystemInfo.HasHiddenSurfaceRemovalOnGPU();
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00028E58 File Offset: 0x00027058
		public static bool hasDynamicUniformArrayIndexingInFragmentShaders
		{
			get
			{
				return SystemInfo.HasDynamicUniformArrayIndexingInFragmentShaders();
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x00028E70 File Offset: 0x00027070
		public static bool supportsShadows
		{
			get
			{
				return SystemInfo.SupportsShadows();
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x00028E88 File Offset: 0x00027088
		public static bool supportsRawShadowDepthSampling
		{
			get
			{
				return SystemInfo.SupportsRawShadowDepthSampling();
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x00028EA0 File Offset: 0x000270A0
		[Obsolete("supportsRenderTextures always returns true, no need to call it")]
		public static bool supportsRenderTextures
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00028EB4 File Offset: 0x000270B4
		public static bool supportsMotionVectors
		{
			get
			{
				return SystemInfo.SupportsMotionVectors();
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x00028ECC File Offset: 0x000270CC
		[Obsolete("supportsRenderToCubemap always returns true, no need to call it")]
		public static bool supportsRenderToCubemap
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00028EE0 File Offset: 0x000270E0
		[Obsolete("supportsImageEffects always returns true, no need to call it")]
		public static bool supportsImageEffects
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x00028EF4 File Offset: 0x000270F4
		public static bool supports3DTextures
		{
			get
			{
				return SystemInfo.Supports3DTextures();
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00028F0C File Offset: 0x0002710C
		public static bool supportsCompressed3DTextures
		{
			get
			{
				return SystemInfo.SupportsCompressed3DTextures();
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x00028F24 File Offset: 0x00027124
		public static bool supports2DArrayTextures
		{
			get
			{
				return SystemInfo.Supports2DArrayTextures();
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x00028F3C File Offset: 0x0002713C
		public static bool supports3DRenderTextures
		{
			get
			{
				return SystemInfo.Supports3DRenderTextures();
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00028F54 File Offset: 0x00027154
		public static bool supportsCubemapArrayTextures
		{
			get
			{
				return SystemInfo.SupportsCubemapArrayTextures();
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x00028F6C File Offset: 0x0002716C
		public static bool supportsAnisotropicFilter
		{
			get
			{
				return SystemInfo.SupportsAnisotropicFilter();
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x00028F84 File Offset: 0x00027184
		public static CopyTextureSupport copyTextureSupport
		{
			get
			{
				return SystemInfo.GetCopyTextureSupport();
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x00028F9C File Offset: 0x0002719C
		public static bool supportsComputeShaders
		{
			get
			{
				return SystemInfo.SupportsComputeShaders();
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x00028FB4 File Offset: 0x000271B4
		public static bool supportsGeometryShaders
		{
			get
			{
				return SystemInfo.SupportsGeometryShaders();
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x00028FCC File Offset: 0x000271CC
		public static bool supportsTessellationShaders
		{
			get
			{
				return SystemInfo.SupportsTessellationShaders();
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x00028FE4 File Offset: 0x000271E4
		public static bool supportsRenderTargetArrayIndexFromVertexShader
		{
			get
			{
				return SystemInfo.SupportsRenderTargetArrayIndexFromVertexShader();
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x00028FFC File Offset: 0x000271FC
		public static bool supportsInstancing
		{
			get
			{
				return SystemInfo.SupportsInstancing();
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x00029014 File Offset: 0x00027214
		public static bool supportsHardwareQuadTopology
		{
			get
			{
				return SystemInfo.SupportsHardwareQuadTopology();
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0002902C File Offset: 0x0002722C
		public static bool supports32bitsIndexBuffer
		{
			get
			{
				return SystemInfo.Supports32bitsIndexBuffer();
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x00029044 File Offset: 0x00027244
		public static bool supportsSparseTextures
		{
			get
			{
				return SystemInfo.SupportsSparseTextures();
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0002905C File Offset: 0x0002725C
		public static int supportedRenderTargetCount
		{
			get
			{
				return SystemInfo.SupportedRenderTargetCount();
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x00029074 File Offset: 0x00027274
		public static bool supportsSeparatedRenderTargetsBlend
		{
			get
			{
				return SystemInfo.SupportsSeparatedRenderTargetsBlend();
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0002908C File Offset: 0x0002728C
		public static int supportedRandomWriteTargetCount
		{
			get
			{
				return SystemInfo.SupportedRandomWriteTargetCount();
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x000290A4 File Offset: 0x000272A4
		public static int supportsMultisampledTextures
		{
			get
			{
				return SystemInfo.SupportsMultisampledTextures();
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x000290BC File Offset: 0x000272BC
		public static bool supportsMultisampled2DArrayTextures
		{
			get
			{
				return SystemInfo.SupportsMultisampled2DArrayTextures();
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x000290D4 File Offset: 0x000272D4
		public static bool supportsMultisampleAutoResolve
		{
			get
			{
				return SystemInfo.SupportsMultisampleAutoResolve();
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x000290EC File Offset: 0x000272EC
		public static int supportsTextureWrapMirrorOnce
		{
			get
			{
				return SystemInfo.SupportsTextureWrapMirrorOnce();
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x00029104 File Offset: 0x00027304
		public static bool usesReversedZBuffer
		{
			get
			{
				return SystemInfo.UsesReversedZBuffer();
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0002911C File Offset: 0x0002731C
		[Obsolete("supportsStencil always returns true, no need to call it")]
		public static int supportsStencil
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00029130 File Offset: 0x00027330
		private static bool IsValidEnumValue(Enum value)
		{
			bool flag = !Enum.IsDefined(value.GetType(), value);
			return !flag;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0002915C File Offset: 0x0002735C
		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return SystemInfo.HasRenderTextureNative(format);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00029194 File Offset: 0x00027394
		public static bool SupportsBlendingOnRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsBlendingOnRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return SystemInfo.SupportsBlendingOnRenderTextureFormatNative(format);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000291CC File Offset: 0x000273CC
		public static bool SupportsRandomWriteOnRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsRandomWriteOnRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return SystemInfo.SupportsRandomWriteOnRenderTextureFormatNative(format);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x00029204 File Offset: 0x00027404
		public static bool SupportsTextureFormat(TextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsTextureFormat; format is not a valid TextureFormat");
			}
			return SystemInfo.SupportsTextureFormatNative(format);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0002923C File Offset: 0x0002743C
		public static bool SupportsVertexAttributeFormat(VertexAttributeFormat format, int dimension)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsVertexAttributeFormat; format is not a valid VertexAttributeFormat");
			}
			bool flag2 = dimension < 1 || dimension > 4;
			if (flag2)
			{
				throw new ArgumentException("Failed SupportsVertexAttributeFormat; dimension must be in 1..4 range");
			}
			return SystemInfo.SupportsVertexAttributeFormatNative(format, dimension);
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0002928C File Offset: 0x0002748C
		public static NPOTSupport npotSupport
		{
			get
			{
				return SystemInfo.GetNPOTSupport();
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x000292A4 File Offset: 0x000274A4
		public static int maxTextureSize
		{
			get
			{
				return SystemInfo.GetMaxTextureSize();
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x000292BC File Offset: 0x000274BC
		public static int maxTexture3DSize
		{
			get
			{
				return SystemInfo.GetMaxTexture3DSize();
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x000292D4 File Offset: 0x000274D4
		public static int maxTextureArraySlices
		{
			get
			{
				return SystemInfo.GetMaxTextureArraySlices();
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x000292EC File Offset: 0x000274EC
		public static int maxCubemapSize
		{
			get
			{
				return SystemInfo.GetMaxCubemapSize();
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x00029304 File Offset: 0x00027504
		public static int maxAnisotropyLevel
		{
			get
			{
				return SystemInfo.GetMaxAnisotropyLevel();
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0002931C File Offset: 0x0002751C
		internal static int maxRenderTextureSize
		{
			get
			{
				return SystemInfo.GetMaxRenderTextureSize();
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x00029334 File Offset: 0x00027534
		public static int maxComputeBufferInputsVertex
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsVertex();
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0002934C File Offset: 0x0002754C
		public static int maxComputeBufferInputsFragment
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsFragment();
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x00029364 File Offset: 0x00027564
		public static int maxComputeBufferInputsGeometry
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsGeometry();
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0002937C File Offset: 0x0002757C
		public static int maxComputeBufferInputsDomain
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsDomain();
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x00029394 File Offset: 0x00027594
		public static int maxComputeBufferInputsHull
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsHull();
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x000293AC File Offset: 0x000275AC
		public static int maxComputeBufferInputsCompute
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsCompute();
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x000293C4 File Offset: 0x000275C4
		public static int maxComputeWorkGroupSize
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSize();
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x000293DC File Offset: 0x000275DC
		public static int maxComputeWorkGroupSizeX
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSizeX();
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x000293F4 File Offset: 0x000275F4
		public static int maxComputeWorkGroupSizeY
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSizeY();
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x0002940C File Offset: 0x0002760C
		public static int maxComputeWorkGroupSizeZ
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSizeZ();
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x00029424 File Offset: 0x00027624
		public static int computeSubGroupSize
		{
			get
			{
				return SystemInfo.GetComputeSubGroupSize();
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x0002943C File Offset: 0x0002763C
		public static bool supportsAsyncCompute
		{
			get
			{
				return SystemInfo.SupportsAsyncCompute();
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x00029454 File Offset: 0x00027654
		public static bool supportsGpuRecorder
		{
			get
			{
				return SystemInfo.SupportsGpuRecorder();
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0002946C File Offset: 0x0002766C
		public static bool supportsGraphicsFence
		{
			get
			{
				return SystemInfo.SupportsGPUFence();
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x00029484 File Offset: 0x00027684
		public static bool supportsAsyncGPUReadback
		{
			get
			{
				return SystemInfo.SupportsAsyncGPUReadback();
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0002949C File Offset: 0x0002769C
		public static bool supportsRayTracing
		{
			get
			{
				return SystemInfo.SupportsRayTracing();
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x000294B4 File Offset: 0x000276B4
		public static bool supportsSetConstantBuffer
		{
			get
			{
				return SystemInfo.SupportsSetConstantBuffer();
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x000294CC File Offset: 0x000276CC
		public static int constantBufferOffsetAlignment
		{
			get
			{
				return SystemInfo.MinConstantBufferOffsetAlignment();
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x000294E4 File Offset: 0x000276E4
		public static long maxGraphicsBufferSize
		{
			get
			{
				return SystemInfo.MaxGraphicsBufferSize();
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x000294FC File Offset: 0x000276FC
		[Obsolete("Use SystemInfo.constantBufferOffsetAlignment instead.")]
		public static bool minConstantBufferOffsetAlignment
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x00029510 File Offset: 0x00027710
		public static bool hasMipMaxLevel
		{
			get
			{
				return SystemInfo.HasMipMaxLevel();
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x00029528 File Offset: 0x00027728
		public static bool supportsMipStreaming
		{
			get
			{
				return SystemInfo.SupportsMipStreaming();
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x00029540 File Offset: 0x00027740
		[Obsolete("graphicsPixelFillrate is no longer supported in Unity 5.0+.")]
		public static int graphicsPixelFillrate
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x00029554 File Offset: 0x00027754
		public static bool usesLoadStoreActions
		{
			get
			{
				return SystemInfo.UsesLoadStoreActions();
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x0002956C File Offset: 0x0002776C
		public static HDRDisplaySupportFlags hdrDisplaySupportFlags
		{
			get
			{
				return SystemInfo.GetHDRDisplaySupportFlags();
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x00029584 File Offset: 0x00027784
		public static bool supportsConservativeRaster
		{
			get
			{
				return SystemInfo.SupportsConservativeRaster();
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x0002959C File Offset: 0x0002779C
		public static bool supportsMultiview
		{
			get
			{
				return SystemInfo.SupportsMultiview();
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x000295B4 File Offset: 0x000277B4
		public static bool supportsStoreAndResolveAction
		{
			get
			{
				return SystemInfo.SupportsStoreAndResolveAction();
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x000295CC File Offset: 0x000277CC
		public static bool supportsMultisampleResolveDepth
		{
			get
			{
				return SystemInfo.SupportsMultisampleResolveDepth();
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x000295E4 File Offset: 0x000277E4
		[Obsolete("Vertex program support is required in Unity 5.0+")]
		public static bool supportsVertexPrograms
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001921 RID: 6433
		[FreeFunction("systeminfo::GetBatteryLevel")]
		[MethodImpl(4096)]
		private static extern float GetBatteryLevel();

		// Token: 0x06001922 RID: 6434
		[FreeFunction("systeminfo::GetBatteryStatus")]
		[MethodImpl(4096)]
		private static extern BatteryStatus GetBatteryStatus();

		// Token: 0x06001923 RID: 6435
		[FreeFunction("systeminfo::GetOperatingSystem")]
		[MethodImpl(4096)]
		private static extern string GetOperatingSystem();

		// Token: 0x06001924 RID: 6436
		[FreeFunction("systeminfo::GetOperatingSystemFamily")]
		[MethodImpl(4096)]
		private static extern OperatingSystemFamily GetOperatingSystemFamily();

		// Token: 0x06001925 RID: 6437
		[FreeFunction("systeminfo::GetProcessorType")]
		[MethodImpl(4096)]
		private static extern string GetProcessorType();

		// Token: 0x06001926 RID: 6438
		[FreeFunction("systeminfo::GetProcessorFrequencyMHz")]
		[MethodImpl(4096)]
		private static extern int GetProcessorFrequencyMHz();

		// Token: 0x06001927 RID: 6439
		[FreeFunction("systeminfo::GetProcessorCount")]
		[MethodImpl(4096)]
		private static extern int GetProcessorCount();

		// Token: 0x06001928 RID: 6440
		[FreeFunction("systeminfo::GetPhysicalMemoryMB")]
		[MethodImpl(4096)]
		private static extern int GetPhysicalMemoryMB();

		// Token: 0x06001929 RID: 6441
		[FreeFunction("systeminfo::GetDeviceUniqueIdentifier")]
		[MethodImpl(4096)]
		private static extern string GetDeviceUniqueIdentifier();

		// Token: 0x0600192A RID: 6442
		[FreeFunction("systeminfo::GetDeviceName")]
		[MethodImpl(4096)]
		private static extern string GetDeviceName();

		// Token: 0x0600192B RID: 6443
		[FreeFunction("systeminfo::GetDeviceModel")]
		[MethodImpl(4096)]
		private static extern string GetDeviceModel();

		// Token: 0x0600192C RID: 6444
		[FreeFunction("systeminfo::SupportsAccelerometer")]
		[MethodImpl(4096)]
		private static extern bool SupportsAccelerometer();

		// Token: 0x0600192D RID: 6445
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern bool IsGyroAvailable();

		// Token: 0x0600192E RID: 6446
		[FreeFunction("systeminfo::SupportsLocationService")]
		[MethodImpl(4096)]
		private static extern bool SupportsLocationService();

		// Token: 0x0600192F RID: 6447
		[FreeFunction("systeminfo::SupportsVibration")]
		[MethodImpl(4096)]
		private static extern bool SupportsVibration();

		// Token: 0x06001930 RID: 6448
		[FreeFunction("systeminfo::SupportsAudio")]
		[MethodImpl(4096)]
		private static extern bool SupportsAudio();

		// Token: 0x06001931 RID: 6449
		[FreeFunction("systeminfo::GetDeviceType")]
		[MethodImpl(4096)]
		private static extern DeviceType GetDeviceType();

		// Token: 0x06001932 RID: 6450
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsMemorySize")]
		[MethodImpl(4096)]
		private static extern int GetGraphicsMemorySize();

		// Token: 0x06001933 RID: 6451
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceName")]
		[MethodImpl(4096)]
		private static extern string GetGraphicsDeviceName();

		// Token: 0x06001934 RID: 6452
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendor")]
		[MethodImpl(4096)]
		private static extern string GetGraphicsDeviceVendor();

		// Token: 0x06001935 RID: 6453
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceID")]
		[MethodImpl(4096)]
		private static extern int GetGraphicsDeviceID();

		// Token: 0x06001936 RID: 6454
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendorID")]
		[MethodImpl(4096)]
		private static extern int GetGraphicsDeviceVendorID();

		// Token: 0x06001937 RID: 6455
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceType")]
		[MethodImpl(4096)]
		private static extern GraphicsDeviceType GetGraphicsDeviceType();

		// Token: 0x06001938 RID: 6456
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsUVStartsAtTop")]
		[MethodImpl(4096)]
		private static extern bool GetGraphicsUVStartsAtTop();

		// Token: 0x06001939 RID: 6457
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVersion")]
		[MethodImpl(4096)]
		private static extern string GetGraphicsDeviceVersion();

		// Token: 0x0600193A RID: 6458
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsShaderLevel")]
		[MethodImpl(4096)]
		private static extern int GetGraphicsShaderLevel();

		// Token: 0x0600193B RID: 6459
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsMultiThreaded")]
		[MethodImpl(4096)]
		private static extern bool GetGraphicsMultiThreaded();

		// Token: 0x0600193C RID: 6460
		[FreeFunction("ScriptingGraphicsCaps::GetRenderingThreadingMode")]
		[MethodImpl(4096)]
		private static extern RenderingThreadingMode GetRenderingThreadingMode();

		// Token: 0x0600193D RID: 6461
		[FreeFunction("ScriptingGraphicsCaps::HasHiddenSurfaceRemovalOnGPU")]
		[MethodImpl(4096)]
		private static extern bool HasHiddenSurfaceRemovalOnGPU();

		// Token: 0x0600193E RID: 6462
		[FreeFunction("ScriptingGraphicsCaps::HasDynamicUniformArrayIndexingInFragmentShaders")]
		[MethodImpl(4096)]
		private static extern bool HasDynamicUniformArrayIndexingInFragmentShaders();

		// Token: 0x0600193F RID: 6463
		[FreeFunction("ScriptingGraphicsCaps::SupportsShadows")]
		[MethodImpl(4096)]
		private static extern bool SupportsShadows();

		// Token: 0x06001940 RID: 6464
		[FreeFunction("ScriptingGraphicsCaps::SupportsRawShadowDepthSampling")]
		[MethodImpl(4096)]
		private static extern bool SupportsRawShadowDepthSampling();

		// Token: 0x06001941 RID: 6465
		[FreeFunction("SupportsMotionVectors")]
		[MethodImpl(4096)]
		private static extern bool SupportsMotionVectors();

		// Token: 0x06001942 RID: 6466
		[FreeFunction("ScriptingGraphicsCaps::Supports3DTextures")]
		[MethodImpl(4096)]
		private static extern bool Supports3DTextures();

		// Token: 0x06001943 RID: 6467
		[FreeFunction("ScriptingGraphicsCaps::SupportsCompressed3DTextures")]
		[MethodImpl(4096)]
		private static extern bool SupportsCompressed3DTextures();

		// Token: 0x06001944 RID: 6468
		[FreeFunction("ScriptingGraphicsCaps::Supports2DArrayTextures")]
		[MethodImpl(4096)]
		private static extern bool Supports2DArrayTextures();

		// Token: 0x06001945 RID: 6469
		[FreeFunction("ScriptingGraphicsCaps::Supports3DRenderTextures")]
		[MethodImpl(4096)]
		private static extern bool Supports3DRenderTextures();

		// Token: 0x06001946 RID: 6470
		[FreeFunction("ScriptingGraphicsCaps::SupportsCubemapArrayTextures")]
		[MethodImpl(4096)]
		private static extern bool SupportsCubemapArrayTextures();

		// Token: 0x06001947 RID: 6471
		[FreeFunction("ScriptingGraphicsCaps::SupportsAnisotropicFilter")]
		[MethodImpl(4096)]
		private static extern bool SupportsAnisotropicFilter();

		// Token: 0x06001948 RID: 6472
		[FreeFunction("ScriptingGraphicsCaps::GetCopyTextureSupport")]
		[MethodImpl(4096)]
		private static extern CopyTextureSupport GetCopyTextureSupport();

		// Token: 0x06001949 RID: 6473
		[FreeFunction("ScriptingGraphicsCaps::SupportsComputeShaders")]
		[MethodImpl(4096)]
		private static extern bool SupportsComputeShaders();

		// Token: 0x0600194A RID: 6474
		[FreeFunction("ScriptingGraphicsCaps::SupportsGeometryShaders")]
		[MethodImpl(4096)]
		private static extern bool SupportsGeometryShaders();

		// Token: 0x0600194B RID: 6475
		[FreeFunction("ScriptingGraphicsCaps::SupportsTessellationShaders")]
		[MethodImpl(4096)]
		private static extern bool SupportsTessellationShaders();

		// Token: 0x0600194C RID: 6476
		[FreeFunction("ScriptingGraphicsCaps::SupportsRenderTargetArrayIndexFromVertexShader")]
		[MethodImpl(4096)]
		private static extern bool SupportsRenderTargetArrayIndexFromVertexShader();

		// Token: 0x0600194D RID: 6477
		[FreeFunction("ScriptingGraphicsCaps::SupportsInstancing")]
		[MethodImpl(4096)]
		private static extern bool SupportsInstancing();

		// Token: 0x0600194E RID: 6478
		[FreeFunction("ScriptingGraphicsCaps::SupportsHardwareQuadTopology")]
		[MethodImpl(4096)]
		private static extern bool SupportsHardwareQuadTopology();

		// Token: 0x0600194F RID: 6479
		[FreeFunction("ScriptingGraphicsCaps::Supports32bitsIndexBuffer")]
		[MethodImpl(4096)]
		private static extern bool Supports32bitsIndexBuffer();

		// Token: 0x06001950 RID: 6480
		[FreeFunction("ScriptingGraphicsCaps::SupportsSparseTextures")]
		[MethodImpl(4096)]
		private static extern bool SupportsSparseTextures();

		// Token: 0x06001951 RID: 6481
		[FreeFunction("ScriptingGraphicsCaps::SupportedRenderTargetCount")]
		[MethodImpl(4096)]
		private static extern int SupportedRenderTargetCount();

		// Token: 0x06001952 RID: 6482
		[FreeFunction("ScriptingGraphicsCaps::SupportsSeparatedRenderTargetsBlend")]
		[MethodImpl(4096)]
		private static extern bool SupportsSeparatedRenderTargetsBlend();

		// Token: 0x06001953 RID: 6483
		[FreeFunction("ScriptingGraphicsCaps::SupportedRandomWriteTargetCount")]
		[MethodImpl(4096)]
		private static extern int SupportedRandomWriteTargetCount();

		// Token: 0x06001954 RID: 6484
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsVertex")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsVertex();

		// Token: 0x06001955 RID: 6485
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsFragment")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsFragment();

		// Token: 0x06001956 RID: 6486
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsGeometry")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsGeometry();

		// Token: 0x06001957 RID: 6487
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsDomain")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsDomain();

		// Token: 0x06001958 RID: 6488
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsHull")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsHull();

		// Token: 0x06001959 RID: 6489
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsCompute")]
		[MethodImpl(4096)]
		private static extern int MaxComputeBufferInputsCompute();

		// Token: 0x0600195A RID: 6490
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampledTextures")]
		[MethodImpl(4096)]
		private static extern int SupportsMultisampledTextures();

		// Token: 0x0600195B RID: 6491
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampled2DArrayTextures")]
		[MethodImpl(4096)]
		private static extern bool SupportsMultisampled2DArrayTextures();

		// Token: 0x0600195C RID: 6492
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampleAutoResolve")]
		[MethodImpl(4096)]
		private static extern bool SupportsMultisampleAutoResolve();

		// Token: 0x0600195D RID: 6493
		[FreeFunction("ScriptingGraphicsCaps::SupportsTextureWrapMirrorOnce")]
		[MethodImpl(4096)]
		private static extern int SupportsTextureWrapMirrorOnce();

		// Token: 0x0600195E RID: 6494
		[FreeFunction("ScriptingGraphicsCaps::UsesReversedZBuffer")]
		[MethodImpl(4096)]
		private static extern bool UsesReversedZBuffer();

		// Token: 0x0600195F RID: 6495
		[FreeFunction("ScriptingGraphicsCaps::HasRenderTexture")]
		[MethodImpl(4096)]
		private static extern bool HasRenderTextureNative(RenderTextureFormat format);

		// Token: 0x06001960 RID: 6496
		[FreeFunction("ScriptingGraphicsCaps::SupportsBlendingOnRenderTextureFormat")]
		[MethodImpl(4096)]
		private static extern bool SupportsBlendingOnRenderTextureFormatNative(RenderTextureFormat format);

		// Token: 0x06001961 RID: 6497
		[FreeFunction("ScriptingGraphicsCaps::SupportsRandomWriteOnRenderTextureFormat")]
		[MethodImpl(4096)]
		private static extern bool SupportsRandomWriteOnRenderTextureFormatNative(RenderTextureFormat format);

		// Token: 0x06001962 RID: 6498
		[FreeFunction("ScriptingGraphicsCaps::SupportsTextureFormat")]
		[MethodImpl(4096)]
		private static extern bool SupportsTextureFormatNative(TextureFormat format);

		// Token: 0x06001963 RID: 6499
		[FreeFunction("ScriptingGraphicsCaps::SupportsVertexAttributeFormat")]
		[MethodImpl(4096)]
		private static extern bool SupportsVertexAttributeFormatNative(VertexAttributeFormat format, int dimension);

		// Token: 0x06001964 RID: 6500
		[FreeFunction("ScriptingGraphicsCaps::GetNPOTSupport")]
		[MethodImpl(4096)]
		private static extern NPOTSupport GetNPOTSupport();

		// Token: 0x06001965 RID: 6501
		[FreeFunction("ScriptingGraphicsCaps::GetMaxTextureSize")]
		[MethodImpl(4096)]
		private static extern int GetMaxTextureSize();

		// Token: 0x06001966 RID: 6502
		[FreeFunction("ScriptingGraphicsCaps::GetMaxTexture3DSize")]
		[MethodImpl(4096)]
		private static extern int GetMaxTexture3DSize();

		// Token: 0x06001967 RID: 6503
		[FreeFunction("ScriptingGraphicsCaps::GetMaxTextureArraySlices")]
		[MethodImpl(4096)]
		private static extern int GetMaxTextureArraySlices();

		// Token: 0x06001968 RID: 6504
		[FreeFunction("ScriptingGraphicsCaps::GetMaxCubemapSize")]
		[MethodImpl(4096)]
		private static extern int GetMaxCubemapSize();

		// Token: 0x06001969 RID: 6505
		[FreeFunction("ScriptingGraphicsCaps::GetMaxAnisotropyLevel")]
		[MethodImpl(4096)]
		private static extern int GetMaxAnisotropyLevel();

		// Token: 0x0600196A RID: 6506
		[FreeFunction("ScriptingGraphicsCaps::GetMaxRenderTextureSize")]
		[MethodImpl(4096)]
		private static extern int GetMaxRenderTextureSize();

		// Token: 0x0600196B RID: 6507
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSize")]
		[MethodImpl(4096)]
		private static extern int GetMaxComputeWorkGroupSize();

		// Token: 0x0600196C RID: 6508
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeX")]
		[MethodImpl(4096)]
		private static extern int GetMaxComputeWorkGroupSizeX();

		// Token: 0x0600196D RID: 6509
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeY")]
		[MethodImpl(4096)]
		private static extern int GetMaxComputeWorkGroupSizeY();

		// Token: 0x0600196E RID: 6510
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeZ")]
		[MethodImpl(4096)]
		private static extern int GetMaxComputeWorkGroupSizeZ();

		// Token: 0x0600196F RID: 6511
		[FreeFunction("ScriptingGraphicsCaps::GetComputeSubGroupSize")]
		[MethodImpl(4096)]
		private static extern int GetComputeSubGroupSize();

		// Token: 0x06001970 RID: 6512
		[FreeFunction("ScriptingGraphicsCaps::SupportsAsyncCompute")]
		[MethodImpl(4096)]
		private static extern bool SupportsAsyncCompute();

		// Token: 0x06001971 RID: 6513
		[FreeFunction("ScriptingGraphicsCaps::SupportsGpuRecorder")]
		[MethodImpl(4096)]
		private static extern bool SupportsGpuRecorder();

		// Token: 0x06001972 RID: 6514
		[FreeFunction("ScriptingGraphicsCaps::SupportsGPUFence")]
		[MethodImpl(4096)]
		private static extern bool SupportsGPUFence();

		// Token: 0x06001973 RID: 6515
		[FreeFunction("ScriptingGraphicsCaps::SupportsAsyncGPUReadback")]
		[MethodImpl(4096)]
		private static extern bool SupportsAsyncGPUReadback();

		// Token: 0x06001974 RID: 6516
		[FreeFunction("ScriptingGraphicsCaps::SupportsRayTracing")]
		[MethodImpl(4096)]
		private static extern bool SupportsRayTracing();

		// Token: 0x06001975 RID: 6517
		[FreeFunction("ScriptingGraphicsCaps::SupportsSetConstantBuffer")]
		[MethodImpl(4096)]
		private static extern bool SupportsSetConstantBuffer();

		// Token: 0x06001976 RID: 6518
		[FreeFunction("ScriptingGraphicsCaps::MinConstantBufferOffsetAlignment")]
		[MethodImpl(4096)]
		private static extern int MinConstantBufferOffsetAlignment();

		// Token: 0x06001977 RID: 6519
		[FreeFunction("ScriptingGraphicsCaps::MaxGraphicsBufferSize")]
		[MethodImpl(4096)]
		private static extern long MaxGraphicsBufferSize();

		// Token: 0x06001978 RID: 6520
		[FreeFunction("ScriptingGraphicsCaps::HasMipMaxLevel")]
		[MethodImpl(4096)]
		private static extern bool HasMipMaxLevel();

		// Token: 0x06001979 RID: 6521
		[FreeFunction("ScriptingGraphicsCaps::SupportsMipStreaming")]
		[MethodImpl(4096)]
		private static extern bool SupportsMipStreaming();

		// Token: 0x0600197A RID: 6522
		[FreeFunction("ScriptingGraphicsCaps::IsFormatSupported")]
		[MethodImpl(4096)]
		public static extern bool IsFormatSupported(GraphicsFormat format, FormatUsage usage);

		// Token: 0x0600197B RID: 6523
		[FreeFunction("ScriptingGraphicsCaps::GetCompatibleFormat")]
		[MethodImpl(4096)]
		public static extern GraphicsFormat GetCompatibleFormat(GraphicsFormat format, FormatUsage usage);

		// Token: 0x0600197C RID: 6524
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsFormat")]
		[MethodImpl(4096)]
		public static extern GraphicsFormat GetGraphicsFormat(DefaultFormat format);

		// Token: 0x0600197D RID: 6525 RVA: 0x000295F7 File Offset: 0x000277F7
		[FreeFunction("ScriptingGraphicsCaps::GetRenderTextureSupportedMSAASampleCount")]
		public static int GetRenderTextureSupportedMSAASampleCount(RenderTextureDescriptor desc)
		{
			return SystemInfo.GetRenderTextureSupportedMSAASampleCount_Injected(ref desc);
		}

		// Token: 0x0600197E RID: 6526
		[FreeFunction("ScriptingGraphicsCaps::UsesLoadStoreActions")]
		[MethodImpl(4096)]
		private static extern bool UsesLoadStoreActions();

		// Token: 0x0600197F RID: 6527
		[FreeFunction("ScriptingGraphicsCaps::GetHDRDisplaySupportFlags")]
		[MethodImpl(4096)]
		private static extern HDRDisplaySupportFlags GetHDRDisplaySupportFlags();

		// Token: 0x06001980 RID: 6528
		[FreeFunction("ScriptingGraphicsCaps::SupportsConservativeRaster")]
		[MethodImpl(4096)]
		private static extern bool SupportsConservativeRaster();

		// Token: 0x06001981 RID: 6529
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultiview")]
		[MethodImpl(4096)]
		private static extern bool SupportsMultiview();

		// Token: 0x06001982 RID: 6530
		[FreeFunction("ScriptingGraphicsCaps::SupportsStoreAndResolveAction")]
		[MethodImpl(4096)]
		private static extern bool SupportsStoreAndResolveAction();

		// Token: 0x06001983 RID: 6531
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampleResolveDepth")]
		[MethodImpl(4096)]
		private static extern bool SupportsMultisampleResolveDepth();

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x00029600 File Offset: 0x00027800
		[Obsolete("SystemInfo.supportsGPUFence has been deprecated, use SystemInfo.supportsGraphicsFence instead (UnityUpgradable) ->  supportsGraphicsFence", true)]
		public static bool supportsGPUFence
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001986 RID: 6534
		[MethodImpl(4096)]
		private static extern int GetRenderTextureSupportedMSAASampleCount_Injected(ref RenderTextureDescriptor desc);

		// Token: 0x0400086F RID: 2159
		public const string unsupportedIdentifier = "n/a";
	}
}
