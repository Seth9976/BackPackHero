using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine.Device
{
	// Token: 0x02000450 RID: 1104
	public static class SystemInfo
	{
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x000410C9 File Offset: 0x0003F2C9
		public static float batteryLevel
		{
			get
			{
				return SystemInfo.batteryLevel;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002744 RID: 10052 RVA: 0x000410D0 File Offset: 0x0003F2D0
		public static BatteryStatus batteryStatus
		{
			get
			{
				return SystemInfo.batteryStatus;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x000410D7 File Offset: 0x0003F2D7
		public static string operatingSystem
		{
			get
			{
				return SystemInfo.operatingSystem;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x000410DE File Offset: 0x0003F2DE
		public static OperatingSystemFamily operatingSystemFamily
		{
			get
			{
				return SystemInfo.operatingSystemFamily;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x000410E5 File Offset: 0x0003F2E5
		public static string processorType
		{
			get
			{
				return SystemInfo.processorType;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x000410EC File Offset: 0x0003F2EC
		public static int processorFrequency
		{
			get
			{
				return SystemInfo.processorFrequency;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000410F3 File Offset: 0x0003F2F3
		public static int processorCount
		{
			get
			{
				return SystemInfo.processorCount;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x000410FA File Offset: 0x0003F2FA
		public static int systemMemorySize
		{
			get
			{
				return SystemInfo.systemMemorySize;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x00041101 File Offset: 0x0003F301
		public static string deviceUniqueIdentifier
		{
			get
			{
				return SystemInfo.deviceUniqueIdentifier;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x00041108 File Offset: 0x0003F308
		public static string deviceName
		{
			get
			{
				return SystemInfo.deviceName;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x0004110F File Offset: 0x0003F30F
		public static string deviceModel
		{
			get
			{
				return SystemInfo.deviceModel;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x00041116 File Offset: 0x0003F316
		public static bool supportsAccelerometer
		{
			get
			{
				return SystemInfo.supportsAccelerometer;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x0004111D File Offset: 0x0003F31D
		public static bool supportsGyroscope
		{
			get
			{
				return SystemInfo.supportsGyroscope;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x00041124 File Offset: 0x0003F324
		public static bool supportsLocationService
		{
			get
			{
				return SystemInfo.supportsLocationService;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x0004112B File Offset: 0x0003F32B
		public static bool supportsVibration
		{
			get
			{
				return SystemInfo.supportsVibration;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002752 RID: 10066 RVA: 0x00041132 File Offset: 0x0003F332
		public static bool supportsAudio
		{
			get
			{
				return SystemInfo.supportsAudio;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x00041139 File Offset: 0x0003F339
		public static DeviceType deviceType
		{
			get
			{
				return SystemInfo.deviceType;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002754 RID: 10068 RVA: 0x00041140 File Offset: 0x0003F340
		public static int graphicsMemorySize
		{
			get
			{
				return SystemInfo.graphicsMemorySize;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x00041147 File Offset: 0x0003F347
		public static string graphicsDeviceName
		{
			get
			{
				return SystemInfo.graphicsDeviceName;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x0004114E File Offset: 0x0003F34E
		public static string graphicsDeviceVendor
		{
			get
			{
				return SystemInfo.graphicsDeviceVendor;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x00041155 File Offset: 0x0003F355
		public static int graphicsDeviceID
		{
			get
			{
				return SystemInfo.graphicsDeviceID;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x0004115C File Offset: 0x0003F35C
		public static int graphicsDeviceVendorID
		{
			get
			{
				return SystemInfo.graphicsDeviceVendorID;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x00041163 File Offset: 0x0003F363
		public static GraphicsDeviceType graphicsDeviceType
		{
			get
			{
				return SystemInfo.graphicsDeviceType;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x0004116A File Offset: 0x0003F36A
		public static bool graphicsUVStartsAtTop
		{
			get
			{
				return SystemInfo.graphicsUVStartsAtTop;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x00041171 File Offset: 0x0003F371
		public static string graphicsDeviceVersion
		{
			get
			{
				return SystemInfo.graphicsDeviceVersion;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x00041178 File Offset: 0x0003F378
		public static int graphicsShaderLevel
		{
			get
			{
				return SystemInfo.graphicsShaderLevel;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x0004117F File Offset: 0x0003F37F
		public static bool graphicsMultiThreaded
		{
			get
			{
				return SystemInfo.graphicsMultiThreaded;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x00041186 File Offset: 0x0003F386
		public static RenderingThreadingMode renderingThreadingMode
		{
			get
			{
				return SystemInfo.renderingThreadingMode;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x0004118D File Offset: 0x0003F38D
		public static bool hasHiddenSurfaceRemovalOnGPU
		{
			get
			{
				return SystemInfo.hasHiddenSurfaceRemovalOnGPU;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x00041194 File Offset: 0x0003F394
		public static bool hasDynamicUniformArrayIndexingInFragmentShaders
		{
			get
			{
				return SystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x0004119B File Offset: 0x0003F39B
		public static bool supportsShadows
		{
			get
			{
				return SystemInfo.supportsShadows;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x000411A2 File Offset: 0x0003F3A2
		public static bool supportsRawShadowDepthSampling
		{
			get
			{
				return SystemInfo.supportsRawShadowDepthSampling;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x000411A9 File Offset: 0x0003F3A9
		public static bool supportsMotionVectors
		{
			get
			{
				return SystemInfo.supportsMotionVectors;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x000411B0 File Offset: 0x0003F3B0
		public static bool supports3DTextures
		{
			get
			{
				return SystemInfo.supports3DTextures;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x000411B7 File Offset: 0x0003F3B7
		public static bool supportsCompressed3DTextures
		{
			get
			{
				return SystemInfo.supportsCompressed3DTextures;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x000411BE File Offset: 0x0003F3BE
		public static bool supports2DArrayTextures
		{
			get
			{
				return SystemInfo.supports2DArrayTextures;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x000411C5 File Offset: 0x0003F3C5
		public static bool supports3DRenderTextures
		{
			get
			{
				return SystemInfo.supports3DRenderTextures;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x000411CC File Offset: 0x0003F3CC
		public static bool supportsCubemapArrayTextures
		{
			get
			{
				return SystemInfo.supportsCubemapArrayTextures;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x000411D3 File Offset: 0x0003F3D3
		public static bool supportsAnisotropicFilter
		{
			get
			{
				return SystemInfo.supportsAnisotropicFilter;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x000411DA File Offset: 0x0003F3DA
		public static CopyTextureSupport copyTextureSupport
		{
			get
			{
				return SystemInfo.copyTextureSupport;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x000411E1 File Offset: 0x0003F3E1
		public static bool supportsComputeShaders
		{
			get
			{
				return SystemInfo.supportsComputeShaders;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x000411E8 File Offset: 0x0003F3E8
		public static bool supportsGeometryShaders
		{
			get
			{
				return SystemInfo.supportsGeometryShaders;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x000411EF File Offset: 0x0003F3EF
		public static bool supportsTessellationShaders
		{
			get
			{
				return SystemInfo.supportsTessellationShaders;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x000411F6 File Offset: 0x0003F3F6
		public static bool supportsRenderTargetArrayIndexFromVertexShader
		{
			get
			{
				return SystemInfo.supportsRenderTargetArrayIndexFromVertexShader;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x000411FD File Offset: 0x0003F3FD
		public static bool supportsInstancing
		{
			get
			{
				return SystemInfo.supportsInstancing;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x00041204 File Offset: 0x0003F404
		public static bool supportsHardwareQuadTopology
		{
			get
			{
				return SystemInfo.supportsHardwareQuadTopology;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x0004120B File Offset: 0x0003F40B
		public static bool supports32bitsIndexBuffer
		{
			get
			{
				return SystemInfo.supports32bitsIndexBuffer;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x00041212 File Offset: 0x0003F412
		public static bool supportsSparseTextures
		{
			get
			{
				return SystemInfo.supportsSparseTextures;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x00041219 File Offset: 0x0003F419
		public static int supportedRenderTargetCount
		{
			get
			{
				return SystemInfo.supportedRenderTargetCount;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x00041220 File Offset: 0x0003F420
		public static bool supportsSeparatedRenderTargetsBlend
		{
			get
			{
				return SystemInfo.supportsSeparatedRenderTargetsBlend;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x00041227 File Offset: 0x0003F427
		public static int supportedRandomWriteTargetCount
		{
			get
			{
				return SystemInfo.supportedRandomWriteTargetCount;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x0004122E File Offset: 0x0003F42E
		public static int supportsMultisampledTextures
		{
			get
			{
				return SystemInfo.supportsMultisampledTextures;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x00041235 File Offset: 0x0003F435
		public static bool supportsMultisampled2DArrayTextures
		{
			get
			{
				return SystemInfo.supportsMultisampled2DArrayTextures;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x0004123C File Offset: 0x0003F43C
		public static bool supportsMultisampleAutoResolve
		{
			get
			{
				return SystemInfo.supportsMultisampleAutoResolve;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002779 RID: 10105 RVA: 0x00041243 File Offset: 0x0003F443
		public static int supportsTextureWrapMirrorOnce
		{
			get
			{
				return SystemInfo.supportsTextureWrapMirrorOnce;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x0004124A File Offset: 0x0003F44A
		public static bool usesReversedZBuffer
		{
			get
			{
				return SystemInfo.usesReversedZBuffer;
			}
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x00041254 File Offset: 0x0003F454
		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			return SystemInfo.SupportsRenderTextureFormat(format);
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x0004126C File Offset: 0x0003F46C
		public static bool SupportsBlendingOnRenderTextureFormat(RenderTextureFormat format)
		{
			return SystemInfo.SupportsBlendingOnRenderTextureFormat(format);
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x00041284 File Offset: 0x0003F484
		public static bool SupportsTextureFormat(TextureFormat format)
		{
			return SystemInfo.SupportsTextureFormat(format);
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0004129C File Offset: 0x0003F49C
		public static bool SupportsVertexAttributeFormat(VertexAttributeFormat format, int dimension)
		{
			return SystemInfo.SupportsVertexAttributeFormat(format, dimension);
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x000412B5 File Offset: 0x0003F4B5
		public static NPOTSupport npotSupport
		{
			get
			{
				return SystemInfo.npotSupport;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x000412BC File Offset: 0x0003F4BC
		public static int maxTextureSize
		{
			get
			{
				return SystemInfo.maxTextureSize;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x000412C3 File Offset: 0x0003F4C3
		public static int maxTexture3DSize
		{
			get
			{
				return SystemInfo.maxTexture3DSize;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x000412CA File Offset: 0x0003F4CA
		public static int maxTextureArraySlices
		{
			get
			{
				return SystemInfo.maxTextureArraySlices;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x000412D1 File Offset: 0x0003F4D1
		public static int maxCubemapSize
		{
			get
			{
				return SystemInfo.maxCubemapSize;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002784 RID: 10116 RVA: 0x000412D8 File Offset: 0x0003F4D8
		public static int maxAnisotropyLevel
		{
			get
			{
				return SystemInfo.maxAnisotropyLevel;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x000412DF File Offset: 0x0003F4DF
		public static int maxComputeBufferInputsVertex
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsVertex;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002786 RID: 10118 RVA: 0x000412E6 File Offset: 0x0003F4E6
		public static int maxComputeBufferInputsFragment
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsFragment;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x000412ED File Offset: 0x0003F4ED
		public static int maxComputeBufferInputsGeometry
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsGeometry;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002788 RID: 10120 RVA: 0x000412F4 File Offset: 0x0003F4F4
		public static int maxComputeBufferInputsDomain
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsDomain;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x000412FB File Offset: 0x0003F4FB
		public static int maxComputeBufferInputsHull
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsHull;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600278A RID: 10122 RVA: 0x00041302 File Offset: 0x0003F502
		public static int maxComputeBufferInputsCompute
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsCompute;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x00041309 File Offset: 0x0003F509
		public static int maxComputeWorkGroupSize
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSize;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x00041310 File Offset: 0x0003F510
		public static int maxComputeWorkGroupSizeX
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSizeX;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x00041317 File Offset: 0x0003F517
		public static int maxComputeWorkGroupSizeY
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSizeY;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600278E RID: 10126 RVA: 0x0004131E File Offset: 0x0003F51E
		public static int maxComputeWorkGroupSizeZ
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSizeZ;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600278F RID: 10127 RVA: 0x00041325 File Offset: 0x0003F525
		public static int computeSubGroupSize
		{
			get
			{
				return SystemInfo.computeSubGroupSize;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002790 RID: 10128 RVA: 0x0004132C File Offset: 0x0003F52C
		public static bool supportsAsyncCompute
		{
			get
			{
				return SystemInfo.supportsAsyncCompute;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x00041333 File Offset: 0x0003F533
		public static bool supportsGpuRecorder
		{
			get
			{
				return SystemInfo.supportsGpuRecorder;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x0004133A File Offset: 0x0003F53A
		public static bool supportsGraphicsFence
		{
			get
			{
				return SystemInfo.supportsGraphicsFence;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x00041341 File Offset: 0x0003F541
		public static bool supportsAsyncGPUReadback
		{
			get
			{
				return SystemInfo.supportsAsyncGPUReadback;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x00041348 File Offset: 0x0003F548
		public static bool supportsRayTracing
		{
			get
			{
				return SystemInfo.supportsRayTracing;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002795 RID: 10133 RVA: 0x0004134F File Offset: 0x0003F54F
		public static bool supportsSetConstantBuffer
		{
			get
			{
				return SystemInfo.supportsSetConstantBuffer;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x00041356 File Offset: 0x0003F556
		public static int constantBufferOffsetAlignment
		{
			get
			{
				return SystemInfo.constantBufferOffsetAlignment;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x0004135D File Offset: 0x0003F55D
		public static long maxGraphicsBufferSize
		{
			get
			{
				return SystemInfo.maxGraphicsBufferSize;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x00041364 File Offset: 0x0003F564
		public static bool hasMipMaxLevel
		{
			get
			{
				return SystemInfo.hasMipMaxLevel;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x0004136B File Offset: 0x0003F56B
		public static bool supportsMipStreaming
		{
			get
			{
				return SystemInfo.supportsMipStreaming;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x00041372 File Offset: 0x0003F572
		public static bool usesLoadStoreActions
		{
			get
			{
				return SystemInfo.usesLoadStoreActions;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x00041379 File Offset: 0x0003F579
		public static HDRDisplaySupportFlags hdrDisplaySupportFlags
		{
			get
			{
				return SystemInfo.hdrDisplaySupportFlags;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x00041380 File Offset: 0x0003F580
		public static bool supportsConservativeRaster
		{
			get
			{
				return SystemInfo.supportsConservativeRaster;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x00041387 File Offset: 0x0003F587
		public static bool supportsMultiview
		{
			get
			{
				return SystemInfo.supportsMultiview;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x0004138E File Offset: 0x0003F58E
		public static bool supportsStoreAndResolveAction
		{
			get
			{
				return SystemInfo.supportsStoreAndResolveAction;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x00041395 File Offset: 0x0003F595
		public static bool supportsMultisampleResolveDepth
		{
			get
			{
				return SystemInfo.supportsMultisampleResolveDepth;
			}
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x0004139C File Offset: 0x0003F59C
		public static bool IsFormatSupported(GraphicsFormat format, FormatUsage usage)
		{
			return SystemInfo.IsFormatSupported(format, usage);
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000413B8 File Offset: 0x0003F5B8
		public static GraphicsFormat GetCompatibleFormat(GraphicsFormat format, FormatUsage usage)
		{
			return SystemInfo.GetCompatibleFormat(format, usage);
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000413D4 File Offset: 0x0003F5D4
		public static GraphicsFormat GetGraphicsFormat(DefaultFormat format)
		{
			return SystemInfo.GetGraphicsFormat(format);
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000413EC File Offset: 0x0003F5EC
		public static int GetRenderTextureSupportedMSAASampleCount(RenderTextureDescriptor desc)
		{
			return SystemInfo.GetRenderTextureSupportedMSAASampleCount(desc);
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x00041404 File Offset: 0x0003F604
		public static bool SupportsRandomWriteOnRenderTextureFormat(RenderTextureFormat format)
		{
			return SystemInfo.SupportsRandomWriteOnRenderTextureFormat(format);
		}

		// Token: 0x04000E33 RID: 3635
		public const string unsupportedIdentifier = "n/a";
	}
}
