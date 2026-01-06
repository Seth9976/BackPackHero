using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000103 RID: 259
	internal struct DeferredTiler
	{
		// Token: 0x060007D6 RID: 2006 RVA: 0x0002F398 File Offset: 0x0002D598
		public DeferredTiler(int tilePixelWidth, int tilePixelHeight, int avgLightPerTile, int tilerLevel)
		{
			this.m_TilePixelWidth = tilePixelWidth;
			this.m_TilePixelHeight = tilePixelHeight;
			this.m_TileXCount = 0;
			this.m_TileYCount = 0;
			this.m_TileHeaderSize = ((tilerLevel == 0) ? 4 : 2);
			this.m_AvgLightPerTile = avgLightPerTile;
			this.m_TilerLevel = tilerLevel;
			this.m_FrustumPlanes = new FrustumPlanes
			{
				left = 0f,
				right = 0f,
				bottom = 0f,
				top = 0f,
				zNear = 0f,
				zFar = 0f
			};
			this.m_IsOrthographic = false;
			this.m_Counters = default(NativeArray<int>);
			this.m_TileData = default(NativeArray<ushort>);
			this.m_TileHeaders = default(NativeArray<uint>);
			this.m_PreTiles = default(NativeArray<PreTile>);
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0002F46C File Offset: 0x0002D66C
		public int TilerLevel
		{
			get
			{
				return this.m_TilerLevel;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0002F474 File Offset: 0x0002D674
		public int TileXCount
		{
			get
			{
				return this.m_TileXCount;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0002F47C File Offset: 0x0002D67C
		public int TileYCount
		{
			get
			{
				return this.m_TileYCount;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0002F484 File Offset: 0x0002D684
		public int TilePixelWidth
		{
			get
			{
				return this.m_TilePixelWidth;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0002F48C File Offset: 0x0002D68C
		public int TilePixelHeight
		{
			get
			{
				return this.m_TilePixelHeight;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0002F494 File Offset: 0x0002D694
		public int TileHeaderSize
		{
			get
			{
				return this.m_TileHeaderSize;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0002F49C File Offset: 0x0002D69C
		public int MaxLightPerTile
		{
			get
			{
				if (!this.m_Counters.IsCreated)
				{
					return 0;
				}
				return this.m_Counters[0];
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0002F4B9 File Offset: 0x0002D6B9
		public int TileDataCapacity
		{
			get
			{
				if (!this.m_Counters.IsCreated)
				{
					return 0;
				}
				return this.m_Counters[2];
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0002F4D6 File Offset: 0x0002D6D6
		public NativeArray<ushort> Tiles
		{
			get
			{
				return this.m_TileData;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0002F4DE File Offset: 0x0002D6DE
		public NativeArray<uint> TileHeaders
		{
			get
			{
				return this.m_TileHeaders;
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0002F4E8 File Offset: 0x0002D6E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetTileOffsetAndCount(int i, int j, out int offset, out int count)
		{
			int tileHeaderOffset = this.GetTileHeaderOffset(i, j);
			offset = (int)this.m_TileHeaders[tileHeaderOffset];
			count = (int)this.m_TileHeaders[tileHeaderOffset + 1];
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0002F51D File Offset: 0x0002D71D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetTileHeaderOffset(int i, int j)
		{
			return (i + j * this.m_TileXCount) * this.m_TileHeaderSize;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0002F530 File Offset: 0x0002D730
		public void Setup(int tileDataCapacity)
		{
			if (tileDataCapacity <= 0)
			{
				tileDataCapacity = this.m_TileXCount * this.m_TileYCount * this.m_AvgLightPerTile;
			}
			this.m_Counters = new NativeArray<int>(3, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			this.m_TileData = new NativeArray<ushort>(tileDataCapacity, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			this.m_TileHeaders = new NativeArray<uint>(this.m_TileXCount * this.m_TileYCount * this.m_TileHeaderSize, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			this.m_Counters[0] = 0;
			this.m_Counters[1] = 0;
			this.m_Counters[2] = tileDataCapacity;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002F5BC File Offset: 0x0002D7BC
		public void OnCameraCleanup()
		{
			if (this.m_TileHeaders.IsCreated)
			{
				this.m_TileHeaders.Dispose();
			}
			if (this.m_TileData.IsCreated)
			{
				this.m_TileData.Dispose();
			}
			if (this.m_Counters.IsCreated)
			{
				this.m_Counters.Dispose();
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002F614 File Offset: 0x0002D814
		public void PrecomputeTiles(Matrix4x4 proj, bool isOrthographic, int renderWidth, int renderHeight)
		{
			this.m_TileXCount = (renderWidth + this.m_TilePixelWidth - 1) / this.m_TilePixelWidth;
			this.m_TileYCount = (renderHeight + this.m_TilePixelHeight - 1) / this.m_TilePixelHeight;
			this.m_PreTiles = DeferredShaderData.instance.GetPreTiles(this.m_TilerLevel, this.m_TileXCount * this.m_TileYCount);
			int num = DeferredTiler.Align(renderWidth, this.m_TilePixelWidth);
			int num2 = DeferredTiler.Align(renderHeight, this.m_TilePixelHeight);
			this.m_FrustumPlanes = proj.decomposeProjection;
			this.m_FrustumPlanes.right = this.m_FrustumPlanes.left + (this.m_FrustumPlanes.right - this.m_FrustumPlanes.left) * ((float)num / (float)renderWidth);
			this.m_FrustumPlanes.bottom = this.m_FrustumPlanes.top + (this.m_FrustumPlanes.bottom - this.m_FrustumPlanes.top) * ((float)num2 / (float)renderHeight);
			this.m_IsOrthographic = isOrthographic;
			float num3 = (this.m_FrustumPlanes.right - this.m_FrustumPlanes.left) / (float)this.m_TileXCount;
			float num4 = (this.m_FrustumPlanes.top - this.m_FrustumPlanes.bottom) / (float)this.m_TileYCount;
			if (!isOrthographic)
			{
				for (int i = 0; i < this.m_TileYCount; i++)
				{
					float num5 = this.m_FrustumPlanes.top - num4 * (float)i;
					float num6 = num5 - num4;
					for (int j = 0; j < this.m_TileXCount; j++)
					{
						float num7 = this.m_FrustumPlanes.left + num3 * (float)j;
						float num8 = num7 + num3;
						PreTile preTile;
						preTile.planeLeft = DeferredTiler.MakePlane(new float3(num7, num6, -this.m_FrustumPlanes.zNear), new float3(num7, num5, -this.m_FrustumPlanes.zNear));
						preTile.planeRight = DeferredTiler.MakePlane(new float3(num8, num5, -this.m_FrustumPlanes.zNear), new float3(num8, num6, -this.m_FrustumPlanes.zNear));
						preTile.planeBottom = DeferredTiler.MakePlane(new float3(num8, num6, -this.m_FrustumPlanes.zNear), new float3(num7, num6, -this.m_FrustumPlanes.zNear));
						preTile.planeTop = DeferredTiler.MakePlane(new float3(num7, num5, -this.m_FrustumPlanes.zNear), new float3(num8, num5, -this.m_FrustumPlanes.zNear));
						this.m_PreTiles[j + i * this.m_TileXCount] = preTile;
					}
				}
				return;
			}
			for (int k = 0; k < this.m_TileYCount; k++)
			{
				float num9 = this.m_FrustumPlanes.top - num4 * (float)k;
				float num10 = num9 - num4;
				for (int l = 0; l < this.m_TileXCount; l++)
				{
					float num11 = this.m_FrustumPlanes.left + num3 * (float)l;
					float num12 = num11 + num3;
					PreTile preTile2;
					preTile2.planeLeft = DeferredTiler.MakePlane(new float3(num11, num10, -this.m_FrustumPlanes.zNear), new float3(num11, num10, -this.m_FrustumPlanes.zNear - 1f), new float3(num11, num9, -this.m_FrustumPlanes.zNear));
					preTile2.planeRight = DeferredTiler.MakePlane(new float3(num12, num9, -this.m_FrustumPlanes.zNear), new float3(num12, num9, -this.m_FrustumPlanes.zNear - 1f), new float3(num12, num10, -this.m_FrustumPlanes.zNear));
					preTile2.planeBottom = DeferredTiler.MakePlane(new float3(num12, num10, -this.m_FrustumPlanes.zNear), new float3(num12, num10, -this.m_FrustumPlanes.zNear - 1f), new float3(num11, num10, -this.m_FrustumPlanes.zNear));
					preTile2.planeTop = DeferredTiler.MakePlane(new float3(num11, num9, -this.m_FrustumPlanes.zNear), new float3(num11, num9, -this.m_FrustumPlanes.zNear - 1f), new float3(num12, num9, -this.m_FrustumPlanes.zNear));
					this.m_PreTiles[l + k * this.m_TileXCount] = preTile2;
				}
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002FA70 File Offset: 0x0002DC70
		public unsafe void CullFinalLights(ref NativeArray<DeferredTiler.PrePunctualLight> punctualLights, ref NativeArray<ushort> lightIndices, int lightStartIndex, int lightCount, int istart, int iend, int jstart, int jend)
		{
			DeferredTiler.PrePunctualLight* unsafeBufferPointerWithoutChecks = (DeferredTiler.PrePunctualLight*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<DeferredTiler.PrePunctualLight>(punctualLights);
			ushort* unsafeBufferPointerWithoutChecks2 = (ushort*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<ushort>(lightIndices);
			uint* unsafeBufferPointerWithoutChecks3 = (uint*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<uint>(this.m_TileHeaders);
			if (lightCount == 0)
			{
				for (int i = jstart; i < jend; i++)
				{
					for (int j = istart; j < iend; j++)
					{
						int tileHeaderOffset = this.GetTileHeaderOffset(j, i);
						unsafeBufferPointerWithoutChecks3[tileHeaderOffset] = 0U;
						unsafeBufferPointerWithoutChecks3[tileHeaderOffset + 1] = 0U;
						unsafeBufferPointerWithoutChecks3[tileHeaderOffset + 2] = 0U;
						unsafeBufferPointerWithoutChecks3[tileHeaderOffset + 3] = 0U;
					}
				}
				return;
			}
			ushort* ptr;
			float2* ptr2;
			int num;
			checked
			{
				ptr = stackalloc ushort[unchecked((UIntPtr)(lightCount * 2)) * 2];
				ptr2 = stackalloc float2[unchecked((UIntPtr)lightCount) * (UIntPtr)sizeof(float2)];
				num = 0;
			}
			int num2 = lightStartIndex + lightCount;
			float2 @float = new float2((this.m_FrustumPlanes.right - this.m_FrustumPlanes.left) / (float)this.m_TileXCount, (this.m_FrustumPlanes.top - this.m_FrustumPlanes.bottom) / (float)this.m_TileYCount);
			float2 float2 = @float * 0.5f;
			float2 float3 = new float2(1f / float2.x, 1f / float2.y);
			for (int k = jstart; k < jend; k++)
			{
				float num3 = this.m_FrustumPlanes.top - (float2.y + (float)k * @float.y);
				for (int l = istart; l < iend; l++)
				{
					float num4 = this.m_FrustumPlanes.left + float2.x + (float)l * @float.x;
					PreTile preTile = this.m_PreTiles[l + k * this.m_TileXCount];
					int num5 = 0;
					float num6 = float.MaxValue;
					float num7 = float.MinValue;
					if (!this.m_IsOrthographic)
					{
						for (int m = lightStartIndex; m < num2; m++)
						{
							ushort num8 = unsafeBufferPointerWithoutChecks2[m];
							DeferredTiler.PrePunctualLight prePunctualLight = unsafeBufferPointerWithoutChecks[num8];
							float2 float4 = new float2(num4, num3);
							float2 float5 = prePunctualLight.screenPos - float4;
							float2 float6 = math.abs(float5 * float3);
							float num9 = 1f / DeferredTiler.max3(float6.x, float6.y, 1f);
							float3 float7 = new float3(float4.x + float5.x * num9, float4.y + float5.y * num9, -this.m_FrustumPlanes.zNear);
							float3 float8 = new float3(0f);
							float num10;
							float num11;
							if (DeferredTiler.IntersectionLineSphere(prePunctualLight.posVS, prePunctualLight.radius, float8, float7, out num10, out num11))
							{
								num6 = ((num6 < num10) ? num6 : num10);
								num7 = ((num7 > num11) ? num7 : num11);
								ptr2[num5] = new float2(num10, num11);
								ptr[num5] = prePunctualLight.visLightIndex;
								num5++;
							}
						}
					}
					else
					{
						for (int n = lightStartIndex; n < num2; n++)
						{
							ushort num12 = unsafeBufferPointerWithoutChecks2[n];
							DeferredTiler.PrePunctualLight prePunctualLight2 = unsafeBufferPointerWithoutChecks[num12];
							float2 float9 = new float2(num4, num3);
							float2 float10 = prePunctualLight2.screenPos - float9;
							float2 float11 = math.abs(float10 * float3);
							float num13 = 1f / DeferredTiler.max3(float11.x, float11.y, 1f);
							float3 float12 = new float3(0f, 0f, -this.m_FrustumPlanes.zNear);
							float3 float13 = new float3(float9.x + float10.x * num13, float9.y + float10.y * num13, 0f);
							float num14;
							float num15;
							if (DeferredTiler.IntersectionLineSphere(prePunctualLight2.posVS, prePunctualLight2.radius, float13, float12, out num14, out num15))
							{
								num6 = ((num6 < num14) ? num6 : num14);
								num7 = ((num7 > num15) ? num7 : num15);
								ptr2[num5] = new float2(num14, num15);
								ptr[num5] = prePunctualLight2.visLightIndex;
								num5++;
							}
						}
					}
					num6 = DeferredTiler.max2(num6 * this.m_FrustumPlanes.zNear, this.m_FrustumPlanes.zNear);
					num7 = DeferredTiler.min2(num7 * this.m_FrustumPlanes.zNear, this.m_FrustumPlanes.zFar);
					uint num16 = 0U;
					float num17 = 1f / (num7 - num6);
					for (int num18 = 0; num18 < num5; num18++)
					{
						float num19 = DeferredTiler.max2(ptr2[num18].x * this.m_FrustumPlanes.zNear, this.m_FrustumPlanes.zNear);
						float num20 = DeferredTiler.min2(ptr2[num18].y * this.m_FrustumPlanes.zNear, this.m_FrustumPlanes.zFar);
						int num21 = (int)((num19 - num6) * 32f * num17);
						int num22 = math.min((int)((num20 - num6) * 32f * num17) - num21 + 1, 32 - num21);
						num16 |= uint.MaxValue >> 32 - num22 << num21;
						ptr[num5 + num18] = (ushort)(num21 | (num22 << 8));
					}
					float num23 = 32f * num17;
					float num24 = -num6 * num23;
					int num25 = num5 * 2;
					int num26 = ((num5 > 0) ? this.AddTileData(ptr, ref num25) : 0);
					int tileHeaderOffset2 = this.GetTileHeaderOffset(l, k);
					unsafeBufferPointerWithoutChecks3[tileHeaderOffset2] = (uint)num26;
					unsafeBufferPointerWithoutChecks3[tileHeaderOffset2 + 1] = (uint)((num25 == 0) ? 0 : num5);
					unsafeBufferPointerWithoutChecks3[tileHeaderOffset2 + 2] = DeferredTiler._f32tof16(num23) | (DeferredTiler._f32tof16(num24) << 16);
					unsafeBufferPointerWithoutChecks3[tileHeaderOffset2 + 3] = num16;
					num = math.max(num, num5);
				}
			}
			this.m_Counters[0] = math.max(this.m_Counters[0], num);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00030064 File Offset: 0x0002E264
		public unsafe void CullIntermediateLights(ref NativeArray<DeferredTiler.PrePunctualLight> punctualLights, ref NativeArray<ushort> lightIndices, int lightStartIndex, int lightCount, int istart, int iend, int jstart, int jend)
		{
			DeferredTiler.PrePunctualLight* unsafeBufferPointerWithoutChecks = (DeferredTiler.PrePunctualLight*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<DeferredTiler.PrePunctualLight>(punctualLights);
			ushort* unsafeBufferPointerWithoutChecks2 = (ushort*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<ushort>(lightIndices);
			uint* unsafeBufferPointerWithoutChecks3 = (uint*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<uint>(this.m_TileHeaders);
			if (lightCount == 0)
			{
				for (int i = jstart; i < jend; i++)
				{
					for (int j = istart; j < iend; j++)
					{
						int tileHeaderOffset = this.GetTileHeaderOffset(j, i);
						unsafeBufferPointerWithoutChecks3[tileHeaderOffset] = 0U;
						unsafeBufferPointerWithoutChecks3[tileHeaderOffset + 1] = 0U;
					}
				}
				return;
			}
			ushort* ptr;
			checked
			{
				ptr = stackalloc ushort[unchecked((UIntPtr)lightCount) * 2];
			}
			int num = lightStartIndex + lightCount;
			for (int k = jstart; k < jend; k++)
			{
				for (int l = istart; l < iend; l++)
				{
					PreTile preTile = this.m_PreTiles[l + k * this.m_TileXCount];
					int num2 = 0;
					for (int m = lightStartIndex; m < num; m++)
					{
						ushort num3 = unsafeBufferPointerWithoutChecks2[m];
						DeferredTiler.PrePunctualLight prePunctualLight = unsafeBufferPointerWithoutChecks[num3];
						if (DeferredTiler.Clip(ref preTile, prePunctualLight.posVS, prePunctualLight.radius))
						{
							ptr[num2] = num3;
							num2++;
						}
					}
					int num4 = ((num2 > 0) ? this.AddTileData(ptr, ref num2) : 0);
					int tileHeaderOffset2 = this.GetTileHeaderOffset(l, k);
					unsafeBufferPointerWithoutChecks3[tileHeaderOffset2] = (uint)num4;
					unsafeBufferPointerWithoutChecks3[tileHeaderOffset2 + 1] = (uint)num2;
				}
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000301C4 File Offset: 0x0002E3C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe int AddTileData(ushort* lightData, ref int size)
		{
			int* unsafePtr = (int*)this.m_Counters.GetUnsafePtr<int>();
			int num = Interlocked.Add(ref unsafePtr[1], size);
			int num2 = num - size;
			if (num <= this.m_TileData.Length)
			{
				ushort* unsafePtr2 = (ushort*)this.m_TileData.GetUnsafePtr<ushort>();
				UnsafeUtility.MemCpy((void*)(unsafePtr2 + num2), (void*)lightData, (long)(size * 2));
				return num2;
			}
			this.m_Counters[2] = math.max(this.m_Counters[2], num);
			size = 0;
			return 0;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0003023C File Offset: 0x0002E43C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IntersectionLineSphere(float3 centre, float radius, float3 raySource, float3 rayDirection, out float t0, out float t1)
		{
			float num = math.dot(rayDirection, rayDirection);
			float num2 = math.dot(raySource - centre, rayDirection);
			float num3 = math.dot(raySource, raySource) + math.dot(centre, centre) - radius * radius - 2f * math.dot(raySource, centre);
			float num4 = num2 * num2 - num * num3;
			if (num4 > 0f)
			{
				float num5 = math.sqrt(num4);
				float num6 = 1f / num;
				t0 = (-num2 - num5) * num6;
				t1 = (-num2 + num5) * num6;
				return true;
			}
			t0 = 0f;
			t1 = 0f;
			return false;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000302CC File Offset: 0x0002E4CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool Clip(ref PreTile tile, float3 posVS, float radius)
		{
			float num = radius * radius;
			int num2 = 0;
			DeferredTiler.ClipResult clipResult = DeferredTiler.ClipPartial(tile.planeLeft, tile.planeBottom, tile.planeTop, posVS, radius, num, ref num2);
			if (clipResult != DeferredTiler.ClipResult.Unknown)
			{
				return clipResult == DeferredTiler.ClipResult.In;
			}
			clipResult = DeferredTiler.ClipPartial(tile.planeRight, tile.planeBottom, tile.planeTop, posVS, radius, num, ref num2);
			if (clipResult != DeferredTiler.ClipResult.Unknown)
			{
				return clipResult == DeferredTiler.ClipResult.In;
			}
			clipResult = DeferredTiler.ClipPartial(tile.planeTop, tile.planeLeft, tile.planeRight, posVS, radius, num, ref num2);
			if (clipResult != DeferredTiler.ClipResult.Unknown)
			{
				return clipResult == DeferredTiler.ClipResult.In;
			}
			clipResult = DeferredTiler.ClipPartial(tile.planeBottom, tile.planeLeft, tile.planeRight, posVS, radius, num, ref num2);
			if (clipResult != DeferredTiler.ClipResult.Unknown)
			{
				return clipResult == DeferredTiler.ClipResult.In;
			}
			return num2 == 4;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00030378 File Offset: 0x0002E578
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static DeferredTiler.ClipResult ClipPartial(float4 plane, float4 sidePlaneA, float4 sidePlaneB, float3 posVS, float radius, float radiusSq, ref int insideCount)
		{
			float num = DeferredTiler.DistanceToPlane(plane, posVS);
			if (num + radius <= 0f)
			{
				return DeferredTiler.ClipResult.Out;
			}
			if (num < 0f)
			{
				float3 @float = posVS - plane.xyz * num;
				float num2 = radiusSq - num * num;
				if (DeferredTiler.SignedSq(DeferredTiler.DistanceToPlane(sidePlaneA, @float)) >= -num2 && DeferredTiler.SignedSq(DeferredTiler.DistanceToPlane(sidePlaneB, @float)) >= -num2)
				{
					return DeferredTiler.ClipResult.In;
				}
			}
			else
			{
				insideCount++;
			}
			return DeferredTiler.ClipResult.Unknown;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000303E8 File Offset: 0x0002E5E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float4 MakePlane(float3 pb, float3 pc)
		{
			float3 @float = math.cross(pb, pc);
			@float = math.normalize(@float);
			return new float4(@float.x, @float.y, @float.z, 0f);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00030424 File Offset: 0x0002E624
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float4 MakePlane(float3 pa, float3 pb, float3 pc)
		{
			float3 @float = pb - pa;
			float3 float2 = pc - pa;
			float3 float3 = math.cross(@float, float2);
			float3 = math.normalize(float3);
			return new float4(float3.x, float3.y, float3.z, -math.dot(float3, pa));
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0003046D File Offset: 0x0002E66D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float DistanceToPlane(float4 plane, float3 p)
		{
			return plane.x * p.x + plane.y * p.y + plane.z * p.z + plane.w;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0003049F File Offset: 0x0002E69F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float SignedSq(float f)
		{
			return ((f < 0f) ? (-1f) : 1f) * (f * f);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000304B9 File Offset: 0x0002E6B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float min2(float a, float b)
		{
			if (a >= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000304C2 File Offset: 0x0002E6C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float max2(float a, float b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000304CB File Offset: 0x0002E6CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float max3(float a, float b, float c)
		{
			if (a <= b)
			{
				if (b <= c)
				{
					return c;
				}
				return b;
			}
			else
			{
				if (a <= c)
				{
					return c;
				}
				return a;
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000304E0 File Offset: 0x0002E6E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint _f32tof16(float x)
		{
			uint num = math.asuint(x);
			uint num2 = num & 2147479552U;
			return math.select(math.asuint(DeferredTiler.min2(math.asfloat(num2) * 1.92593E-34f, 260042750f)) + 4096U >> 13, math.select(31744U, 32256U, num2 > 2139095040U), num2 >= 2139095040U) | ((num & 2147487743U) >> 16);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00030552 File Offset: 0x0002E752
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int Align(int s, int alignment)
		{
			return (s + alignment - 1) / alignment * alignment;
		}

		// Token: 0x04000737 RID: 1847
		private int m_TilePixelWidth;

		// Token: 0x04000738 RID: 1848
		private int m_TilePixelHeight;

		// Token: 0x04000739 RID: 1849
		private int m_TileXCount;

		// Token: 0x0400073A RID: 1850
		private int m_TileYCount;

		// Token: 0x0400073B RID: 1851
		private int m_TileHeaderSize;

		// Token: 0x0400073C RID: 1852
		private int m_AvgLightPerTile;

		// Token: 0x0400073D RID: 1853
		private int m_TilerLevel;

		// Token: 0x0400073E RID: 1854
		private FrustumPlanes m_FrustumPlanes;

		// Token: 0x0400073F RID: 1855
		private bool m_IsOrthographic;

		// Token: 0x04000740 RID: 1856
		[NativeDisableContainerSafetyRestriction]
		private NativeArray<int> m_Counters;

		// Token: 0x04000741 RID: 1857
		[NativeDisableContainerSafetyRestriction]
		private NativeArray<ushort> m_TileData;

		// Token: 0x04000742 RID: 1858
		[NativeDisableContainerSafetyRestriction]
		private NativeArray<uint> m_TileHeaders;

		// Token: 0x04000743 RID: 1859
		[NativeDisableContainerSafetyRestriction]
		private NativeArray<PreTile> m_PreTiles;

		// Token: 0x020001A2 RID: 418
		internal struct PrePunctualLight
		{
			// Token: 0x04000A98 RID: 2712
			public float3 posVS;

			// Token: 0x04000A99 RID: 2713
			public float radius;

			// Token: 0x04000A9A RID: 2714
			public float minDist;

			// Token: 0x04000A9B RID: 2715
			public float2 screenPos;

			// Token: 0x04000A9C RID: 2716
			public ushort visLightIndex;
		}

		// Token: 0x020001A3 RID: 419
		private enum ClipResult
		{
			// Token: 0x04000A9E RID: 2718
			Unknown,
			// Token: 0x04000A9F RID: 2719
			In,
			// Token: 0x04000AA0 RID: 2720
			Out
		}
	}
}
