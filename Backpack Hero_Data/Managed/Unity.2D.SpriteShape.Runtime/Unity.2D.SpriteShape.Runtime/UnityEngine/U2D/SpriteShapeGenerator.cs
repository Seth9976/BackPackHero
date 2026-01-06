using System;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Profiling;
using Unity.SpriteShape.External.LibTessDotNet;
using UnityEngine.U2D.Common.UTess;

namespace UnityEngine.U2D
{
	// Token: 0x02000020 RID: 32
	[BurstCompile]
	public struct SpriteShapeGenerator : IJob
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00008A2C File Offset: 0x00006C2C
		private int vertexDataCount
		{
			get
			{
				return this.m_VertexDataCount;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00008A34 File Offset: 0x00006C34
		private int vertexArrayCount
		{
			get
			{
				return this.m_VertexArrayCount;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00008A3C File Offset: 0x00006C3C
		private int indexDataCount
		{
			get
			{
				return this.m_IndexDataCount;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00008A44 File Offset: 0x00006C44
		private int spriteCount
		{
			get
			{
				return this.m_SpriteInfos.Length;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00008A51 File Offset: 0x00006C51
		private int cornerSpriteCount
		{
			get
			{
				return this.m_CornerSpriteInfos.Length;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00008A5E File Offset: 0x00006C5E
		private int angleRangeCount
		{
			get
			{
				return this.m_AngleRanges.Length;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00008A6B File Offset: 0x00006C6B
		private int controlPointCount
		{
			get
			{
				return this.m_ControlPointCount;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00008A73 File Offset: 0x00006C73
		private int contourPointCount
		{
			get
			{
				return this.m_ContourPointCount;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00008A7B File Offset: 0x00006C7B
		private int segmentCount
		{
			get
			{
				return this.m_SegmentCount;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00008A83 File Offset: 0x00006C83
		private bool hasCollider
		{
			get
			{
				return this.m_ShapeParams.splineData.w == 1;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00008A98 File Offset: 0x00006C98
		private float colliderPivot
		{
			get
			{
				return this.m_ShapeParams.curveData.x;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00008AAA File Offset: 0x00006CAA
		private float borderPivot
		{
			get
			{
				return this.m_ShapeParams.curveData.y;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00008ABC File Offset: 0x00006CBC
		private int splineDetail
		{
			get
			{
				return this.m_ShapeParams.splineData.y;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00008ACE File Offset: 0x00006CCE
		private bool isCarpet
		{
			get
			{
				return this.m_ShapeParams.shapeData.x == 1;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00008AE3 File Offset: 0x00006CE3
		private bool isAdaptive
		{
			get
			{
				return this.m_ShapeParams.shapeData.y == 1;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00008AF8 File Offset: 0x00006CF8
		private bool hasSpriteBorder
		{
			get
			{
				return this.m_ShapeParams.shapeData.z == 1;
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00008B0D File Offset: 0x00006D0D
		private SpriteShapeGenerator.JobSpriteInfo GetSpriteInfo(int index)
		{
			return this.m_SpriteInfos[index];
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00008B1C File Offset: 0x00006D1C
		private SpriteShapeGenerator.JobSpriteInfo GetCornerSpriteInfo(int index)
		{
			int num = index - 1;
			return this.m_CornerSpriteInfos[num];
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008B39 File Offset: 0x00006D39
		private SpriteShapeGenerator.JobAngleRange GetAngleRange(int index)
		{
			return this.m_AngleRanges[index];
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00008B47 File Offset: 0x00006D47
		private SpriteShapeGenerator.JobControlPoint GetControlPoint(int index)
		{
			return this.m_ControlPoints[index];
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00008B55 File Offset: 0x00006D55
		private SpriteShapeGenerator.JobContourPoint GetContourPoint(int index)
		{
			return this.m_ContourPoints[index];
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00008B63 File Offset: 0x00006D63
		private SpriteShapeGenerator.JobSegmentInfo GetSegmentInfo(int index)
		{
			return this.m_Segments[index];
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00008B71 File Offset: 0x00006D71
		private int GetContourIndex(int index)
		{
			return index * this.m_ShapeParams.splineData.y;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00008B85 File Offset: 0x00006D85
		private int GetEndContourIndexOfSegment(SpriteShapeGenerator.JobSegmentInfo isi)
		{
			return this.GetContourIndex(isi.sgInfo.y) - 1;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00008B9C File Offset: 0x00006D9C
		private static void CopyToNativeArray<T>(NativeArray<T> from, int length, ref NativeArray<T> to) where T : struct
		{
			to = new NativeArray<T>(length, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < length; i++)
			{
				to[i] = from[i];
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00008BD2 File Offset: 0x00006DD2
		private static void SafeDispose<T>(NativeArray<T> na) where T : struct
		{
			if (na.Length > 0)
			{
				na.Dispose();
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00008BE8 File Offset: 0x00006DE8
		private static bool IsPointOnLine(float epsilon, float2 a, float2 b, float2 c)
		{
			if (math.abs((c.y - a.y) * (b.x - a.x) - (c.x - a.x) * (b.y - a.y)) > epsilon)
			{
				return false;
			}
			float num = (c.x - a.x) * (b.x - a.x) + (c.y - a.y) * (b.y - a.y);
			if (num < 0f)
			{
				return false;
			}
			float num2 = (b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y);
			return num <= num2;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00008CB7 File Offset: 0x00006EB7
		private static bool IsPointOnLines(float epsilon, float2 p1, float2 p2, float2 p3, float2 p4, float2 r)
		{
			return SpriteShapeGenerator.IsPointOnLine(epsilon, p1, p2, r) && SpriteShapeGenerator.IsPointOnLine(epsilon, p3, p4, r);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00008CD4 File Offset: 0x00006ED4
		private static bool Colinear(float2 p, float2 q, float2 r)
		{
			return q.x <= math.max(p.x, r.x) && q.x >= math.min(p.x, r.x) && q.y <= math.max(p.y, r.y) && q.y >= math.min(p.y, r.y);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008D4C File Offset: 0x00006F4C
		private static int Det(float epsilon, float2 p, float2 q, float2 r)
		{
			float num = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);
			if (num > -epsilon && num < epsilon)
			{
				return 0;
			}
			if (num <= 0f)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00008DA8 File Offset: 0x00006FA8
		private static bool LineIntersectionTest(float epsilon, float2 p1, float2 q1, float2 p2, float2 q2)
		{
			int num = SpriteShapeGenerator.Det(epsilon, p1, q1, p2);
			int num2 = SpriteShapeGenerator.Det(epsilon, p1, q1, q2);
			int num3 = SpriteShapeGenerator.Det(epsilon, p2, q2, p1);
			int num4 = SpriteShapeGenerator.Det(epsilon, p2, q2, q1);
			return (num != num2 && num3 != num4) || (num == 0 && SpriteShapeGenerator.Colinear(p1, p2, q1)) || (num2 == 0 && SpriteShapeGenerator.Colinear(p1, q2, q1)) || (num3 == 0 && SpriteShapeGenerator.Colinear(p2, p1, q2)) || (num4 == 0 && SpriteShapeGenerator.Colinear(p2, q1, q2));
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00008E2C File Offset: 0x0000702C
		private static bool LineIntersection(float epsilon, float2 p1, float2 p2, float2 p3, float2 p4, ref float2 result)
		{
			if (!SpriteShapeGenerator.LineIntersectionTest(epsilon, p1, p2, p3, p4))
			{
				return false;
			}
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			if (math.abs(num5) < epsilon)
			{
				return false;
			}
			float num6 = p3.x - p1.x;
			float num7 = p3.y - p1.y;
			float num8 = (num6 * num4 - num7 * num3) / num5;
			if (num8 >= -epsilon && num8 <= 1f + epsilon)
			{
				result.x = p1.x + num8 * num;
				result.y = p1.y + num8 * num2;
				return true;
			}
			return false;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00008EF8 File Offset: 0x000070F8
		private static float AngleBetweenVector(float2 a, float2 b)
		{
			float num = math.dot(a, b);
			return math.atan2(a.x * b.y - b.x * a.y, num) * 57.29578f;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008F34 File Offset: 0x00007134
		private static bool GenerateColumnsBi(float2 a, float2 b, float2 whsize, bool flip, ref float2 rt, ref float2 rb, float cph, float pivot)
		{
			float2 @float = (flip ? (a - b) : (b - a));
			if (math.length(@float) < 1E-30f)
			{
				return false;
			}
			float2 float2 = new float2(-1f, 1f);
			float2 float3 = @float.yx * float2;
			float2 float4 = new float2(whsize.y * cph);
			float2 float5 = math.normalize(float3) * float4;
			rt = a - float5;
			rb = a + float5;
			float2 float6 = (rb - rt) * pivot;
			rt += float6;
			rb += float6;
			return true;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009004 File Offset: 0x00007204
		private static bool GenerateColumnsTri(float2 a, float2 b, float2 c, float2 whsize, bool flip, ref float2 rt, ref float2 rb, float cph, float pivot)
		{
			float2 @float = new float2(-1f, 1f);
			float2 float2 = b - a;
			float2 float3 = c - b;
			float2 = float2.yx * @float;
			float3 = float3.yx * @float;
			float2 float4 = math.normalize(float2) + math.normalize(float3);
			if (math.length(float4) < 1E-30f)
			{
				return false;
			}
			float4 = math.normalize(float4);
			float2 float5 = new float2(whsize.y * cph);
			float2 float6 = float4 * float5;
			rt = b - float6;
			rb = b + float6;
			float2 float7 = (rb - rt) * pivot;
			rt += float7;
			rb += float7;
			return true;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000090F8 File Offset: 0x000072F8
		private void AppendCornerCoordinates(ref NativeArray<float2> corners, ref int cornerCount, float2 a, float2 b, float2 c, float2 d)
		{
			int num = cornerCount;
			cornerCount = num + 1;
			corners[num] = a;
			num = cornerCount;
			cornerCount = num + 1;
			corners[num] = b;
			num = cornerCount;
			cornerCount = num + 1;
			corners[num] = c;
			num = cornerCount;
			cornerCount = num + 1;
			corners[num] = d;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00009148 File Offset: 0x00007348
		private void PrepareInput(SpriteShapeParameters shapeParams, int maxArrayCount, NativeArray<ShapeControlPoint> shapePoints, bool optimizeGeometry, bool updateCollider, bool optimizeCollider, float colliderOffset, float colliderDetail)
		{
			this.kModeLinear = 0;
			this.kModeContinous = 1;
			this.kModeBroken = 2;
			this.kCornerTypeOuterTopLeft = 1;
			this.kCornerTypeOuterTopRight = 2;
			this.kCornerTypeOuterBottomLeft = 3;
			this.kCornerTypeOuterBottomRight = 4;
			this.kCornerTypeInnerTopLeft = 5;
			this.kCornerTypeInnerTopRight = 6;
			this.kCornerTypeInnerBottomLeft = 7;
			this.kCornerTypeInnerBottomRight = 8;
			this.m_IndexDataCount = 0;
			this.m_VertexDataCount = 0;
			this.m_ColliderDataCount = 0;
			this.m_ActiveIndexCount = 0;
			this.m_ActiveVertexCount = 0;
			this.kEpsilon = 1E-05f;
			this.kEpsilonOrder = -0.0001f;
			this.kEpsilonRelaxed = 0.001f;
			this.kExtendSegment = 10000f;
			this.kLowestQualityTolerance = 4f;
			this.kHighestQualityTolerance = 16f;
			this.kColliderQuality = math.clamp(colliderDetail, this.kLowestQualityTolerance, this.kHighestQualityTolerance);
			this.kOptimizeCollider = (float)(optimizeCollider ? 1 : 0);
			this.kColliderQuality = (this.kHighestQualityTolerance - this.kColliderQuality + 2f) * 0.002f;
			colliderOffset = ((colliderOffset == 0f) ? this.kEpsilonRelaxed : (-colliderOffset));
			this.kOptimizeRender = (float)(optimizeGeometry ? 1 : 0);
			this.kRenderQuality = math.clamp(shapeParams.splineDetail, this.kLowestQualityTolerance, this.kHighestQualityTolerance);
			this.kRenderQuality = (this.kHighestQualityTolerance - this.kRenderQuality + 2f) * 0.0002f;
			this.m_ShapeParams.shapeData = new int4(shapeParams.carpet ? 1 : 0, shapeParams.adaptiveUV ? 1 : 0, shapeParams.spriteBorders ? 1 : 0, (shapeParams.fillTexture != null) ? 1 : 0);
			this.m_ShapeParams.splineData = new int4(shapeParams.stretchUV ? 1 : 0, (int)((shapeParams.splineDetail > 4U) ? shapeParams.splineDetail : 4U), 0, updateCollider ? 1 : 0);
			this.m_ShapeParams.curveData = new float4(colliderOffset, shapeParams.borderPivot, shapeParams.angleThreshold, 0f);
			float num = 0f;
			float num2 = 0f;
			if (shapeParams.fillTexture != null)
			{
				num = (float)shapeParams.fillTexture.width * (1f / shapeParams.fillScale);
				num2 = (float)shapeParams.fillTexture.height * (1f / shapeParams.fillScale);
			}
			this.m_ShapeParams.fillData = new float4(shapeParams.fillScale, num, num2, 0f);
			UnsafeUtility.MemClear(this.m_GeomArray.GetUnsafePtr<SpriteShapeSegment>(), (long)(this.m_GeomArray.Length * UnsafeUtility.SizeOf<SpriteShapeSegment>()));
			this.m_Transform = new float4x4(shapeParams.transform.m00, shapeParams.transform.m01, shapeParams.transform.m02, shapeParams.transform.m03, shapeParams.transform.m10, shapeParams.transform.m11, shapeParams.transform.m12, shapeParams.transform.m13, shapeParams.transform.m20, shapeParams.transform.m21, shapeParams.transform.m22, shapeParams.transform.m23, shapeParams.transform.m30, shapeParams.transform.m31, shapeParams.transform.m32, shapeParams.transform.m33);
			this.kControlPointCount = shapePoints.Length * (int)shapeParams.splineDetail * 32;
			this.m_Segments = new NativeArray<SpriteShapeGenerator.JobSegmentInfo>(shapePoints.Length * 2, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.m_ContourPoints = new NativeArray<SpriteShapeGenerator.JobContourPoint>(this.kControlPointCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.m_TessPoints = new NativeArray<float2>(shapePoints.Length * (int)shapeParams.splineDetail * 128, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.m_VertexData = new NativeArray<SpriteShapeGenerator.JobShapeVertex>(maxArrayCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.m_OutputVertexData = new NativeArray<SpriteShapeGenerator.JobShapeVertex>(maxArrayCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.m_CornerCoordinates = new NativeArray<float2>(32, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.m_Intersectors = new NativeArray<SpriteShapeGenerator.JobIntersectPoint>(this.kControlPointCount, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			this.m_TempPoints = new NativeArray<float2>(maxArrayCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.m_GeneratedControlPoints = new NativeArray<SpriteShapeGenerator.JobControlPoint>(this.kControlPointCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.m_SpriteIndices = new NativeArray<int2>(this.kControlPointCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			int num3 = 0;
			this.AppendCornerCoordinates(ref this.m_CornerCoordinates, ref num3, new float2(1f, 1f), new float2(0f, 1f), new float2(1f, 0f), new float2(0f, 0f));
			this.AppendCornerCoordinates(ref this.m_CornerCoordinates, ref num3, new float2(1f, 0f), new float2(1f, 1f), new float2(0f, 0f), new float2(0f, 1f));
			this.AppendCornerCoordinates(ref this.m_CornerCoordinates, ref num3, new float2(0f, 1f), new float2(0f, 0f), new float2(1f, 1f), new float2(1f, 0f));
			this.AppendCornerCoordinates(ref this.m_CornerCoordinates, ref num3, new float2(0f, 0f), new float2(1f, 0f), new float2(0f, 1f), new float2(1f, 1f));
			this.AppendCornerCoordinates(ref this.m_CornerCoordinates, ref num3, new float2(0f, 0f), new float2(0f, 1f), new float2(1f, 0f), new float2(1f, 1f));
			this.AppendCornerCoordinates(ref this.m_CornerCoordinates, ref num3, new float2(0f, 1f), new float2(1f, 1f), new float2(0f, 0f), new float2(1f, 0f));
			this.AppendCornerCoordinates(ref this.m_CornerCoordinates, ref num3, new float2(1f, 0f), new float2(0f, 0f), new float2(1f, 1f), new float2(0f, 1f));
			this.AppendCornerCoordinates(ref this.m_CornerCoordinates, ref num3, new float2(1f, 1f), new float2(1f, 0f), new float2(0f, 1f), new float2(0f, 0f));
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000097C4 File Offset: 0x000079C4
		private void TransferSprites(ref NativeArray<SpriteShapeGenerator.JobSpriteInfo> spriteInfos, Sprite[] sprites, int maxCount)
		{
			int num = 0;
			while (num < sprites.Length && num < maxCount)
			{
				SpriteShapeGenerator.JobSpriteInfo jobSpriteInfo = spriteInfos[num];
				Sprite sprite = sprites[num];
				if (sprite != null)
				{
					Texture2D texture = sprite.texture;
					jobSpriteInfo.texRect = new float4(sprite.textureRect.x, sprite.textureRect.y, sprite.textureRect.width, sprite.textureRect.height);
					jobSpriteInfo.texData = new float4((float)texture.width, (float)texture.height, texture.texelSize.x, texture.texelSize.y);
					jobSpriteInfo.border = new float4(sprite.border.x, sprite.border.y, sprite.border.z, sprite.border.w);
					jobSpriteInfo.uvInfo = new float4(jobSpriteInfo.texRect.x / jobSpriteInfo.texData.x, jobSpriteInfo.texRect.y / jobSpriteInfo.texData.y, jobSpriteInfo.texRect.z / jobSpriteInfo.texData.x, jobSpriteInfo.texRect.w / jobSpriteInfo.texData.y);
					jobSpriteInfo.metaInfo = new float4(sprite.pixelsPerUnit, sprite.pivot.y / sprite.textureRect.height, sprite.rect.width, sprite.rect.height);
					if (!math.any(jobSpriteInfo.texRect))
					{
						this.Cleanup();
					}
				}
				spriteInfos[num] = jobSpriteInfo;
				num++;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009988 File Offset: 0x00007B88
		private void PrepareSprites(Sprite[] edgeSprites, Sprite[] cornerSprites)
		{
			this.m_SpriteInfos = new NativeArray<SpriteShapeGenerator.JobSpriteInfo>(edgeSprites.Length, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			this.TransferSprites(ref this.m_SpriteInfos, edgeSprites, edgeSprites.Length);
			this.m_CornerSpriteInfos = new NativeArray<SpriteShapeGenerator.JobSpriteInfo>(this.kCornerTypeInnerBottomRight, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			this.TransferSprites(ref this.m_CornerSpriteInfos, cornerSprites, cornerSprites.Length);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000099D8 File Offset: 0x00007BD8
		private void PrepareAngleRanges(AngleRangeInfo[] angleRanges)
		{
			this.m_AngleRanges = new NativeArray<SpriteShapeGenerator.JobAngleRange>(angleRanges.Length, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < angleRanges.Length; i++)
			{
				SpriteShapeGenerator.JobAngleRange jobAngleRange = this.m_AngleRanges[i];
				AngleRangeInfo angleRangeInfo = angleRanges[i];
				int[] sprites = angleRangeInfo.sprites;
				if (angleRangeInfo.start > angleRangeInfo.end)
				{
					float start = angleRangeInfo.start;
					angleRangeInfo.start = angleRangeInfo.end;
					angleRangeInfo.end = start;
				}
				jobAngleRange.spriteAngles = new float4(angleRangeInfo.start + 90f, angleRangeInfo.end + 90f, 0f, 0f);
				jobAngleRange.spriteData = new int4((int)angleRangeInfo.order, sprites.Length, 32, 0);
				this.m_AngleRanges[i] = jobAngleRange;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009AA4 File Offset: 0x00007CA4
		private void PrepareControlPoints(NativeArray<ShapeControlPoint> shapePoints, NativeArray<SplinePointMetaData> metaData)
		{
			float2 @float = new float2(0f, 0f);
			this.m_ControlPoints = new NativeArray<SpriteShapeGenerator.JobControlPoint>(this.kControlPointCount, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < shapePoints.Length; i++)
			{
				SpriteShapeGenerator.JobControlPoint jobControlPoint = this.m_ControlPoints[i];
				ShapeControlPoint shapeControlPoint = shapePoints[i];
				SplinePointMetaData splinePointMetaData = metaData[i];
				jobControlPoint.position = new float2(shapeControlPoint.position.x, shapeControlPoint.position.y);
				jobControlPoint.tangentLt = ((shapeControlPoint.mode == this.kModeLinear) ? @float : new float2(shapeControlPoint.leftTangent.x, shapeControlPoint.leftTangent.y));
				jobControlPoint.tangentRt = ((shapeControlPoint.mode == this.kModeLinear) ? @float : new float2(shapeControlPoint.rightTangent.x, shapeControlPoint.rightTangent.y));
				jobControlPoint.cpInfo = new float2(splinePointMetaData.height, 0f);
				jobControlPoint.cpData = new int4((int)splinePointMetaData.spriteIndex, splinePointMetaData.cornerMode, shapeControlPoint.mode, 0);
				jobControlPoint.exData = new int4(-1, 0, 0, shapeControlPoint.mode);
				this.m_ControlPoints[i] = jobControlPoint;
			}
			this.m_ControlPointCount = shapePoints.Length;
			this.m_Corners = new NativeArray<SpriteShapeGenerator.JobCornerInfo>(shapePoints.Length, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			this.GenerateControlPoints();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009C18 File Offset: 0x00007E18
		private bool WithinRange(SpriteShapeGenerator.JobAngleRange angleRange, float inputAngle)
		{
			float num = angleRange.spriteAngles.y - angleRange.spriteAngles.x;
			float num2 = Mathf.Repeat(inputAngle - angleRange.spriteAngles.x, 360f);
			return num2 >= 0f && num2 <= num;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00009C66 File Offset: 0x00007E66
		private bool AngleWithinRange(float t, float a, float b)
		{
			return a != 0f && b != 0f && t >= a && t <= b;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00009C88 File Offset: 0x00007E88
		private static float2 BezierPoint(float2 st, float2 sp, float2 ep, float2 et, float t)
		{
			float2 @float = new float2(t);
			float2 float2 = new float2(1f - t);
			float2 float3 = new float2(3f);
			return sp * float2 * float2 * float2 + st * float2 * float2 * @float * float3 + et * float2 * @float * @float * float3 + ep * @float * @float * @float;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00009D20 File Offset: 0x00007F20
		private static float SlopeAngle(float2 dirNormalized)
		{
			float2 @float = new float2(0f, 1f);
			float2 float2 = new float2(1f, 0f);
			float num = math.dot(dirNormalized, float2);
			float num2 = math.dot(dirNormalized, @float);
			float num3 = math.acos(num2);
			float num4 = ((num >= 0f) ? 1f : (-1f));
			float num5 = num3 * 57.29578f * num4;
			num5 = ((num2 != 1f) ? num5 : 0f);
			return (num2 != -1f) ? num5 : (-180f);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00009DAA File Offset: 0x00007FAA
		private static float SlopeAngle(float2 start, float2 end)
		{
			return SpriteShapeGenerator.SlopeAngle(math.normalize(start - end));
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00009DC0 File Offset: 0x00007FC0
		private bool ResolveAngle(float angle, int activeIndex, ref float renderOrder, ref int spriteIndex, ref int firstSpriteIndex)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.m_AngleRanges.Length; i++)
			{
				if (this.WithinRange(this.m_AngleRanges[i], angle))
				{
					int num3 = ((activeIndex < this.m_AngleRanges[i].spriteData.y) ? activeIndex : 0);
					renderOrder = (float)(num + num3);
					spriteIndex = num2 + num3;
					firstSpriteIndex = num2;
					return true;
				}
				num += this.m_AngleRanges[i].spriteData.z;
				num2 += this.m_AngleRanges[i].spriteData.y;
			}
			return false;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009E64 File Offset: 0x00008064
		private int GetSpriteIndex(int index, int previousIndex, ref int resolved)
		{
			int num = (index + 1) % this.controlPointCount;
			int num2 = -1;
			int num3 = -1;
			float num4 = 0f;
			SpriteShapeGenerator.JobControlPoint controlPoint = this.GetControlPoint(index);
			float num5 = SpriteShapeGenerator.SlopeAngle(this.GetControlPoint(num).position, controlPoint.position);
			bool flag = this.ResolveAngle(num5, controlPoint.cpData.x, ref num4, ref num2, ref num3);
			resolved = (flag ? 1 : 0);
			if (!flag)
			{
				return previousIndex;
			}
			return num2;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00009ED8 File Offset: 0x000080D8
		private void GenerateSegments()
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			SpriteShapeGenerator.JobSegmentInfo jobSegmentInfo = this.m_Segments[0];
			jobSegmentInfo.sgInfo = int4.zero;
			jobSegmentInfo.spriteInfo = int4.zero;
			float num4 = 0f;
			int i = 0;
			while (i < this.controlPointCount)
			{
				int num5 = (i + 1) % this.controlPointCount;
				bool flag = false;
				if (num5 != 0)
				{
					goto IL_0064;
				}
				if (this.isCarpet)
				{
					num5 = 1;
					flag = true;
					goto IL_0064;
				}
				IL_035B:
				i++;
				continue;
				IL_0064:
				SpriteShapeGenerator.JobControlPoint controlPoint = this.GetControlPoint(i);
				SpriteShapeGenerator.JobControlPoint controlPoint2 = this.GetControlPoint(num5);
				if (controlPoint.exData.x > 0 && controlPoint.exData.x == controlPoint2.exData.x && controlPoint.exData.z == 2)
				{
					goto IL_035B;
				}
				int4 cpData = controlPoint.cpData;
				float2 cpInfo = controlPoint.cpInfo;
				int num6 = ((i < num5) ? i : num5);
				int num7 = ((i > num5) ? i : num5);
				bool flag2 = controlPoint.cpData.z == this.kModeContinous;
				bool flag3 = false;
				if (!flag2 || num2 == 0)
				{
					num4 = SpriteShapeGenerator.SlopeAngle(controlPoint2.position, controlPoint.position);
				}
				if (!this.ResolveAngle(num4, cpData.x, ref cpInfo.y, ref cpData.w, ref num3) && !flag)
				{
					cpData.w = num;
					controlPoint.cpData = cpData;
					this.m_ControlPoints[i] = controlPoint;
					jobSegmentInfo = this.m_Segments[num2];
					jobSegmentInfo.sgInfo.x = num6;
					jobSegmentInfo.sgInfo.y = num7;
					jobSegmentInfo.sgInfo.z = -1;
					this.m_Segments[num2] = jobSegmentInfo;
					num2++;
					goto IL_035B;
				}
				num = cpData.w;
				controlPoint.cpData = cpData;
				controlPoint.cpInfo = cpInfo;
				this.m_ControlPoints[i] = controlPoint;
				if (flag)
				{
					goto IL_035B;
				}
				if (num2 != 0)
				{
					flag2 = flag2 && this.m_SpriteIndices[jobSegmentInfo.sgInfo.x].y != 0 && num == jobSegmentInfo.sgInfo.z;
				}
				if (flag2 && i != this.controlPointCount - 1)
				{
					for (int j = 0; j < num2; j++)
					{
						jobSegmentInfo = this.m_Segments[j];
						if (jobSegmentInfo.sgInfo.x - num6 == 1)
						{
							flag3 = true;
							jobSegmentInfo.sgInfo.x = num6;
							this.m_Segments[j] = jobSegmentInfo;
							break;
						}
						if (num7 - jobSegmentInfo.sgInfo.y == 1)
						{
							flag3 = true;
							jobSegmentInfo.sgInfo.y = num7;
							this.m_Segments[j] = jobSegmentInfo;
							break;
						}
					}
				}
				if (!flag3)
				{
					jobSegmentInfo = this.m_Segments[num2];
					SpriteShapeGenerator.JobSpriteInfo spriteInfo = this.GetSpriteInfo(controlPoint.cpData.w);
					jobSegmentInfo.sgInfo.x = num6;
					jobSegmentInfo.sgInfo.y = num7;
					jobSegmentInfo.sgInfo.z = num;
					jobSegmentInfo.sgInfo.w = num3;
					jobSegmentInfo.spriteInfo.x = spriteInfo.texRect.z;
					jobSegmentInfo.spriteInfo.y = spriteInfo.texRect.w;
					jobSegmentInfo.spriteInfo.z = cpInfo.y;
					this.m_Segments[num2] = jobSegmentInfo;
					num2++;
					goto IL_035B;
				}
				goto IL_035B;
			}
			this.m_SegmentCount = num2;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A25C File Offset: 0x0000845C
		private void UpdateSegments()
		{
			for (int i = 0; i < this.segmentCount; i++)
			{
				SpriteShapeGenerator.JobSegmentInfo segmentInfo = this.GetSegmentInfo(i);
				if (segmentInfo.spriteInfo.z >= 0f)
				{
					segmentInfo.spriteInfo.w = this.SegmentDistance(segmentInfo);
					this.m_Segments[i] = segmentInfo;
				}
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A2B4 File Offset: 0x000084B4
		private bool GetSegmentBoundaryColumn(SpriteShapeGenerator.JobSegmentInfo segment, SpriteShapeGenerator.JobSpriteInfo sprInfo, float2 whsize, float2 startPos, float2 endPos, bool end, ref float2 top, ref float2 bottom)
		{
			float num = 0.5f - sprInfo.metaInfo.y;
			bool flag;
			if (!end)
			{
				SpriteShapeGenerator.JobControlPoint controlPoint = this.GetControlPoint(segment.sgInfo.x);
				if (math.any(controlPoint.tangentRt))
				{
					endPos = controlPoint.tangentRt + startPos;
				}
				flag = SpriteShapeGenerator.GenerateColumnsBi(startPos, endPos, whsize, end, ref top, ref bottom, controlPoint.cpInfo.x * 0.5f, num);
			}
			else
			{
				SpriteShapeGenerator.JobControlPoint controlPoint2 = this.GetControlPoint(segment.sgInfo.y);
				if (math.any(controlPoint2.tangentLt))
				{
					endPos = controlPoint2.tangentLt + startPos;
				}
				flag = SpriteShapeGenerator.GenerateColumnsBi(startPos, endPos, whsize, end, ref top, ref bottom, controlPoint2.cpInfo.x * 0.5f, num);
			}
			return flag;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A380 File Offset: 0x00008580
		private void GenerateControlPoints()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = this.controlPointCount;
			int controlPointCount = this.controlPointCount;
			int2 @int = new int2(0, 0);
			for (int i = 0; i < this.controlPointCount; i++)
			{
				int num5 = 0;
				int spriteIndex = this.GetSpriteIndex(i, num2, ref num5);
				num2 = (@int.x = spriteIndex);
				@int.y = num5;
				this.m_SpriteIndices[i] = @int;
			}
			if (!this.isCarpet)
			{
				SpriteShapeGenerator.JobControlPoint controlPoint = this.GetControlPoint(0);
				controlPoint.cpData.z = ((controlPoint.cpData.z == this.kModeContinous) ? this.kModeBroken : controlPoint.cpData.z);
				this.m_GeneratedControlPoints[num++] = controlPoint;
				num3 = 1;
				num4 = this.controlPointCount - 1;
			}
			for (int j = num3; j < num4; j++)
			{
				bool flag = false;
				if (!this.InsertCorner(j, ref this.m_SpriteIndices, ref this.m_GeneratedControlPoints, ref num, ref flag))
				{
					SpriteShapeGenerator.JobControlPoint controlPoint2 = this.GetControlPoint(j);
					controlPoint2.exData.z = ((flag && controlPoint2.cpData.y == 2) ? 1 : 0);
					this.m_GeneratedControlPoints[num++] = controlPoint2;
				}
			}
			if (!this.isCarpet)
			{
				SpriteShapeGenerator.JobControlPoint jobControlPoint = this.m_GeneratedControlPoints[0];
				jobControlPoint.exData.z = 1;
				this.m_GeneratedControlPoints[0] = jobControlPoint;
				SpriteShapeGenerator.JobControlPoint controlPoint3 = this.GetControlPoint(num4);
				controlPoint3.cpData.z = ((controlPoint3.cpData.z == this.kModeContinous) ? this.kModeBroken : controlPoint3.cpData.z);
				controlPoint3.exData.z = 1;
				this.m_GeneratedControlPoints[num++] = controlPoint3;
			}
			else
			{
				SpriteShapeGenerator.JobControlPoint jobControlPoint2 = this.m_GeneratedControlPoints[0];
				this.m_GeneratedControlPoints[num++] = jobControlPoint2;
			}
			for (int k = 0; k < num; k++)
			{
				this.m_ControlPoints[k] = this.m_GeneratedControlPoints[k];
			}
			this.m_ControlPointCount = num;
			for (int l = 0; l < this.controlPointCount; l++)
			{
				int num6 = 0;
				int spriteIndex2 = this.GetSpriteIndex(l, num2, ref num6);
				num2 = (@int.x = spriteIndex2);
				@int.y = num6;
				this.m_SpriteIndices[l] = @int;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A5EC File Offset: 0x000087EC
		private float SegmentDistance(SpriteShapeGenerator.JobSegmentInfo isi)
		{
			float num = 0f;
			int contourIndex = this.GetContourIndex(isi.sgInfo.x);
			int endContourIndexOfSegment = this.GetEndContourIndexOfSegment(isi);
			for (int i = contourIndex; i < endContourIndexOfSegment; i++)
			{
				int num2 = i + 1;
				SpriteShapeGenerator.JobContourPoint contourPoint = this.GetContourPoint(i);
				SpriteShapeGenerator.JobContourPoint contourPoint2 = this.GetContourPoint(num2);
				num += math.distance(contourPoint.position, contourPoint2.position);
			}
			return num;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A650 File Offset: 0x00008850
		private void GenerateContour()
		{
			int num = this.controlPointCount - 1;
			int num2 = 0;
			float num3 = (float)(this.splineDetail - 1);
			for (int i = 0; i < num; i++)
			{
				int num4 = i + 1;
				SpriteShapeGenerator.JobControlPoint controlPoint = this.GetControlPoint(i);
				SpriteShapeGenerator.JobControlPoint controlPoint2 = this.GetControlPoint(num4);
				bool flag = controlPoint.exData.w == this.kModeContinous || controlPoint2.exData.w == this.kModeContinous;
				float2 position = controlPoint.position;
				float2 position2 = controlPoint2.position;
				float2 @float = position;
				float2 float2 = position + controlPoint.tangentRt;
				float2 float3 = position2 + controlPoint2.tangentLt;
				int num5 = num2;
				float num6 = 0f;
				float num7 = 0f;
				for (int j = 0; j < this.splineDetail; j++)
				{
					SpriteShapeGenerator.JobContourPoint jobContourPoint = this.m_ContourPoints[num2];
					float num8 = (float)j / num3;
					float2 float4 = SpriteShapeGenerator.BezierPoint(float2, position, position2, float3, num8);
					jobContourPoint.position = float4;
					num6 += math.distance(float4, @float);
					this.m_ContourPoints[num2++] = jobContourPoint;
					@float = float4;
				}
				@float = position;
				for (int k = 0; k < this.splineDetail; k++)
				{
					SpriteShapeGenerator.JobContourPoint jobContourPoint2 = this.m_ContourPoints[num5];
					num7 += math.distance(jobContourPoint2.position, @float);
					jobContourPoint2.ptData.x = (flag ? this.InterpolateSmooth(controlPoint.cpInfo.x, controlPoint2.cpInfo.x, num7 / num6) : this.InterpolateLinear(controlPoint.cpInfo.x, controlPoint2.cpInfo.x, num7 / num6));
					this.m_ContourPoints[num5++] = jobContourPoint2;
					@float = jobContourPoint2.position;
				}
			}
			this.m_ContourPointCount = num2;
			int num9 = 0;
			for (int l = 0; l < this.contourPointCount; l++)
			{
				if ((l + 1) % this.splineDetail != 0)
				{
					int num10 = ((l == 0) ? (this.contourPointCount - 1) : (l - 1));
					int num11 = (l + 1) % this.contourPointCount;
					num10 = ((l % this.splineDetail == 0) ? (num10 - 1) : num10);
					SpriteShapeGenerator.JobContourPoint contourPoint = this.GetContourPoint(num10);
					SpriteShapeGenerator.JobContourPoint contourPoint2 = this.GetContourPoint(l);
					SpriteShapeGenerator.JobContourPoint contourPoint3 = this.GetContourPoint(num11);
					float2 float5 = contourPoint2.position - contourPoint.position;
					float2 float6 = contourPoint3.position - contourPoint2.position;
					if (math.length(float5) >= this.kEpsilon && math.length(float6) >= this.kEpsilon)
					{
						float2 float7 = math.normalize(float5);
						float2 float8 = math.normalize(float6);
						float7 = new float2(-float7.y, float7.x);
						float8 = new float2(-float8.y, float8.x);
						float2 float9 = math.normalize(float7) + math.normalize(float8);
						float2 float10 = math.normalize(float9);
						if (math.any(float9) && math.any(float10))
						{
							this.m_TessPoints[num9++] = contourPoint2.position + float10 * this.borderPivot;
						}
					}
				}
			}
			this.m_TessPointCount = num9;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		private void TessellateContour()
		{
			this.GenerateContour();
			SpriteShapeSegment spriteShapeSegment = this.m_GeomArray[0];
			spriteShapeSegment.vertexCount = 0;
			spriteShapeSegment.geomIndex = 0;
			spriteShapeSegment.indexCount = 0;
			spriteShapeSegment.spriteIndex = -1;
			if (math.all(this.m_ShapeParams.shapeData.xw) && this.m_TessPointCount > 0)
			{
				if (this.kOptimizeRender > 0f)
				{
					this.OptimizePoints(this.kRenderQuality, ref this.m_TessPoints, ref this.m_TessPointCount);
				}
				int tessPointCount = this.m_TessPointCount;
				NativeArray<int2> nativeArray = new NativeArray<int2>(tessPointCount - 1, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<float2> nativeArray2 = new NativeArray<float2>(tessPointCount - 1, Allocator.Temp, NativeArrayOptions.ClearMemory);
				for (int i = 0; i < nativeArray2.Length; i++)
				{
					nativeArray2[i] = this.m_TessPoints[i];
				}
				for (int j = 0; j < tessPointCount - 2; j++)
				{
					int2 @int = nativeArray[j];
					@int.x = j;
					@int.y = j + 1;
					nativeArray[j] = @int;
				}
				int2 int2 = nativeArray[tessPointCount - 2];
				int2.x = tessPointCount - 2;
				int2.y = 0;
				nativeArray[tessPointCount - 2] = int2;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				NativeArray<float2> nativeArray3 = new NativeArray<float2>(this.m_TessPointCount * this.m_TessPointCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<int> nativeArray4 = new NativeArray<int>(this.m_TessPointCount * this.m_TessPointCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<int2> nativeArray5 = new NativeArray<int2>(this.m_TessPointCount * this.m_TessPointCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
				ModuleHandle.Tessellate(Allocator.Temp, in nativeArray2, in nativeArray, ref nativeArray3, out num, ref nativeArray4, out num2, ref nativeArray5, out num3);
				if (num2 > 0)
				{
					this.m_ActiveIndexCount = 0;
					while (this.m_ActiveIndexCount < num2)
					{
						this.m_IndexArray[this.m_ActiveIndexCount] = (ushort)nativeArray4[this.m_ActiveIndexCount];
						this.m_ActiveIndexCount++;
					}
					this.m_ActiveVertexCount = 0;
					while (this.m_ActiveVertexCount < num)
					{
						this.m_PosArray[this.m_ActiveVertexCount] = new Vector3(nativeArray3[this.m_ActiveVertexCount].x, nativeArray3[this.m_ActiveVertexCount].y, 0f);
						this.m_ActiveVertexCount++;
					}
					this.m_IndexDataCount = (spriteShapeSegment.indexCount = this.m_ActiveIndexCount);
					this.m_VertexDataCount = (spriteShapeSegment.vertexCount = this.m_ActiveVertexCount);
				}
				nativeArray3.Dispose();
				nativeArray4.Dispose();
				nativeArray5.Dispose();
				nativeArray.Dispose();
				nativeArray2.Dispose();
			}
			if (this.m_TanArray.Length > 1)
			{
				for (int k = 0; k < this.m_ActiveVertexCount; k++)
				{
					this.m_TanArray[k] = new Vector4(1f, 0f, 0f, -1f);
				}
			}
			this.m_GeomArray[0] = spriteShapeSegment;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000AC94 File Offset: 0x00008E94
		private void TessellateContourMainThread()
		{
			this.GenerateContour();
			SpriteShapeSegment spriteShapeSegment = this.m_GeomArray[0];
			spriteShapeSegment.vertexCount = 0;
			spriteShapeSegment.geomIndex = 0;
			spriteShapeSegment.indexCount = 0;
			spriteShapeSegment.spriteIndex = -1;
			if (math.all(this.m_ShapeParams.shapeData.xw) && this.m_TessPointCount > 0)
			{
				if (this.kOptimizeRender > 0f)
				{
					this.OptimizePoints(this.kRenderQuality, ref this.m_TessPoints, ref this.m_TessPointCount);
				}
				ContourVertex[] array = new ContourVertex[this.m_TessPointCount];
				for (int k = 0; k < this.m_TessPointCount; k++)
				{
					array[k] = new ContourVertex
					{
						Position = new Vec3
						{
							X = this.m_TessPoints[k].x,
							Y = this.m_TessPoints[k].y
						}
					};
				}
				Tess tess = new Tess();
				tess.AddContour(array, ContourOrientation.Original);
				tess.Tessellate(WindingRule.NonZero, ElementType.Polygons, 3);
				ushort[] array2 = tess.Elements.Select((int i) => (ushort)i).ToArray<ushort>();
				Vector2[] array3 = tess.Vertices.Select((ContourVertex v) => new Vector2(v.Position.X, v.Position.Y)).ToArray<Vector2>();
				this.m_IndexDataCount = array2.Length;
				this.m_VertexDataCount = array3.Length;
				if (array3.Length != 0)
				{
					this.m_ActiveIndexCount = 0;
					while (this.m_ActiveIndexCount < this.m_IndexDataCount)
					{
						this.m_IndexArray[this.m_ActiveIndexCount] = array2[this.m_ActiveIndexCount];
						this.m_ActiveIndexCount++;
					}
					this.m_ActiveVertexCount = 0;
					while (this.m_ActiveVertexCount < this.m_VertexDataCount)
					{
						Vector3 vector = new Vector3(array3[this.m_ActiveVertexCount].x, array3[this.m_ActiveVertexCount].y, 0f);
						this.m_PosArray[this.m_ActiveVertexCount] = vector;
						this.m_ActiveVertexCount++;
					}
					spriteShapeSegment.indexCount = this.m_ActiveIndexCount;
					spriteShapeSegment.vertexCount = this.m_ActiveVertexCount;
				}
			}
			if (this.m_TanArray.Length > 1)
			{
				for (int j = 0; j < this.m_ActiveVertexCount; j++)
				{
					this.m_TanArray[j] = new Vector4(1f, 0f, 0f, -1f);
				}
			}
			this.m_GeomArray[0] = spriteShapeSegment;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000AF38 File Offset: 0x00009138
		private void CalculateBoundingBox()
		{
			if (this.vertexArrayCount == 0 && this.contourPointCount == 0)
			{
				return;
			}
			Bounds bounds = default(Bounds);
			float2 @float = ((this.vertexArrayCount != 0) ? new float2(this.m_PosArray[0].x, this.m_PosArray[0].y) : new float2(this.m_ContourPoints[0].position.x, this.m_ContourPoints[0].position.y));
			float2 float2 = @float;
			for (int i = 0; i < this.vertexArrayCount; i++)
			{
				float3 float3 = this.m_PosArray[i];
				@float = math.min(@float, float3.xy);
				float2 = math.max(float2, float3.xy);
			}
			for (int j = 0; j < this.contourPointCount; j++)
			{
				float2 float4 = new float2(this.m_ContourPoints[j].position.x, this.m_ContourPoints[j].position.y);
				@float = math.min(@float, float4);
				float2 = math.max(float2, float4);
			}
			bounds.SetMinMax(new Vector3(@float.x, @float.y, 0f), new Vector3(float2.x, float2.y, 0f));
			this.m_Bounds[0] = bounds;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000B0A0 File Offset: 0x000092A0
		private void CalculateTexCoords()
		{
			SpriteShapeSegment spriteShapeSegment = this.m_GeomArray[0];
			if (this.m_ShapeParams.splineData.x > 0)
			{
				float3 @float = this.m_Bounds[0].extents * 2f;
				float3 float2 = this.m_Bounds[0].center - this.m_Bounds[0].extents;
				for (int i = 0; i < spriteShapeSegment.vertexCount; i++)
				{
					Vector3 vector = this.m_PosArray[i];
					Vector2 vector2 = this.m_Uv0Array[i];
					float3 float3 = (new float3(vector.x, vector.y, vector.z) - float2) / @float * this.m_ShapeParams.fillData.x;
					vector2.x = float3.x;
					vector2.y = float3.y;
					this.m_Uv0Array[i] = vector2;
				}
				return;
			}
			for (int j = 0; j < spriteShapeSegment.vertexCount; j++)
			{
				Vector3 vector3 = this.m_PosArray[j];
				Vector2 vector4 = this.m_Uv0Array[j];
				float3 float4 = math.transform(this.m_Transform, new float3(vector3.x, vector3.y, vector3.z));
				vector4.x = float4.x / this.m_ShapeParams.fillData.y;
				vector4.y = float4.y / this.m_ShapeParams.fillData.z;
				this.m_Uv0Array[j] = vector4;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000B278 File Offset: 0x00009478
		private void CopyVertexData(ref NativeSlice<Vector3> outPos, ref NativeSlice<Vector2> outUV0, ref NativeSlice<Vector4> outTan, int outIndex, NativeArray<SpriteShapeGenerator.JobShapeVertex> inVertices, int inIndex, float sOrder)
		{
			Vector3 vector = outPos[outIndex];
			Vector2 vector2 = outUV0[outIndex];
			float3 @float = new float3(inVertices[inIndex].pos.x, inVertices[inIndex].pos.y, sOrder);
			float3 float2 = new float3(inVertices[inIndex + 1].pos.x, inVertices[inIndex + 1].pos.y, sOrder);
			float3 float3 = new float3(inVertices[inIndex + 2].pos.x, inVertices[inIndex + 2].pos.y, sOrder);
			float3 float4 = new float3(inVertices[inIndex + 3].pos.x, inVertices[inIndex + 3].pos.y, sOrder);
			outPos[outIndex] = @float;
			outUV0[outIndex] = inVertices[inIndex].uv;
			outPos[outIndex + 1] = float2;
			outUV0[outIndex + 1] = inVertices[inIndex + 1].uv;
			outPos[outIndex + 2] = float3;
			outUV0[outIndex + 2] = inVertices[inIndex + 2].uv;
			outPos[outIndex + 3] = float4;
			outUV0[outIndex + 3] = inVertices[inIndex + 3].uv;
			if (outTan.Length > 1)
			{
				outTan[outIndex] = inVertices[inIndex].tan;
				outTan[outIndex + 1] = inVertices[inIndex + 1].tan;
				outTan[outIndex + 2] = inVertices[inIndex + 2].tan;
				outTan[outIndex + 3] = inVertices[inIndex + 3].tan;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000B498 File Offset: 0x00009698
		private int CopySegmentRenderData(SpriteShapeGenerator.JobSpriteInfo ispr, ref NativeSlice<Vector3> outPos, ref NativeSlice<Vector2> outUV0, ref NativeSlice<Vector4> outTan, ref int outCount, ref NativeArray<ushort> indexData, ref int indexCount, NativeArray<SpriteShapeGenerator.JobShapeVertex> inVertices, int inCount, float sOrder)
		{
			if (inCount < 4)
			{
				return -1;
			}
			int num = 0;
			int num2 = inCount / 2;
			int i = 0;
			while (i < inCount)
			{
				this.CopyVertexData(ref outPos, ref outUV0, ref outTan, outCount, inVertices, i, sOrder);
				int num3 = indexCount;
				indexCount = num3 + 1;
				indexData[num3] = (ushort)num;
				num3 = indexCount;
				indexCount = num3 + 1;
				indexData[num3] = (ushort)(3 + num);
				num3 = indexCount;
				indexCount = num3 + 1;
				indexData[num3] = (ushort)(1 + num);
				num3 = indexCount;
				indexCount = num3 + 1;
				indexData[num3] = (ushort)num;
				num3 = indexCount;
				indexCount = num3 + 1;
				indexData[num3] = (ushort)(2 + num);
				num3 = indexCount;
				indexCount = num3 + 1;
				indexData[num3] = (ushort)(3 + num);
				i += 4;
				outCount += 4;
				num += 4;
			}
			return outCount;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000B568 File Offset: 0x00009768
		private void GetLineSegments(SpriteShapeGenerator.JobSpriteInfo sprInfo, SpriteShapeGenerator.JobSegmentInfo segment, float2 whsize, ref float2 vlt, ref float2 vlb, ref float2 vrt, ref float2 vrb)
		{
			SpriteShapeGenerator.JobControlPoint controlPoint = this.GetControlPoint(segment.sgInfo.x);
			SpriteShapeGenerator.JobControlPoint controlPoint2 = this.GetControlPoint(segment.sgInfo.y);
			this.GetSegmentBoundaryColumn(segment, sprInfo, whsize, controlPoint.position, controlPoint2.position, false, ref vlt, ref vlb);
			this.GetSegmentBoundaryColumn(segment, sprInfo, whsize, controlPoint2.position, controlPoint.position, true, ref vrt, ref vrb);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B5D0 File Offset: 0x000097D0
		private void TessellateSegment(int segmentIndex, SpriteShapeGenerator.JobSpriteInfo sprInfo, SpriteShapeGenerator.JobSegmentInfo segment, float2 whsize, float4 border, float pxlWidth, NativeArray<SpriteShapeGenerator.JobShapeVertex> vertices, int vertexCount, bool useClosure, bool validHead, bool validTail, bool firstSegment, bool finalSegment, ref NativeArray<SpriteShapeGenerator.JobShapeVertex> outputVertices, ref int outputCount)
		{
			int num = 0;
			float2 float4;
			float2 float3;
			float2 float2;
			float2 @float = (float2 = (float3 = (float4 = float2.zero)));
			float4 float5 = new float4(1f, 1f, 0f, 0f);
			SpriteShapeGenerator.JobShapeVertex jobShapeVertex = default(SpriteShapeGenerator.JobShapeVertex);
			SpriteShapeGenerator.JobShapeVertex jobShapeVertex2 = default(SpriteShapeGenerator.JobShapeVertex);
			SpriteShapeGenerator.JobShapeVertex jobShapeVertex3 = default(SpriteShapeGenerator.JobShapeVertex);
			SpriteShapeGenerator.JobShapeVertex jobShapeVertex4 = default(SpriteShapeGenerator.JobShapeVertex);
			int num2 = vertexCount - 1;
			int num3 = num2 - 1;
			int4 sprite = vertices[0].sprite;
			float num4 = 0f;
			float x = border.x;
			float num5 = whsize.x - border.z;
			float x2 = whsize.x;
			float num6 = num5 - x;
			float num7 = x / x2;
			float num8 = num6 / pxlWidth;
			float num9 = 0.5f - sprInfo.metaInfo.y;
			bool flag = false;
			if (math.abs(segment.sgInfo.x - segment.sgInfo.y) == 1 && this.segmentCount > 1)
			{
				flag = this.FetchStretcher(segmentIndex, sprInfo, segment, whsize, validHead, validTail, ref float5);
			}
			for (int i = 0; i < num2; i++)
			{
				bool flag2 = num2 > 1 && i == num3;
				bool flag3 = i != 0 && !flag2;
				SpriteShapeGenerator.JobShapeVertex jobShapeVertex5 = vertices[i];
				SpriteShapeGenerator.JobShapeVertex jobShapeVertex6 = vertices[i + 1];
				float2 float6 = (flag2 ? jobShapeVertex5.pos : vertices[i + 2].pos);
				float4 = jobShapeVertex2.pos;
				float3 = jobShapeVertex4.pos;
				if (flag3)
				{
					SpriteShapeGenerator.GenerateColumnsTri(jobShapeVertex5.pos, jobShapeVertex6.pos, float6, whsize, flag2, ref @float, ref float2, jobShapeVertex6.meta.x * 0.5f, num9);
				}
				else
				{
					if (!flag2)
					{
						this.GetSegmentBoundaryColumn(segment, sprInfo, whsize, jobShapeVertex5.pos, jobShapeVertex6.pos, false, ref float4, ref float3);
					}
					if (flag2 && useClosure)
					{
						float2 = this.m_FirstLB;
						@float = this.m_FirstLT;
					}
					else
					{
						this.GetSegmentBoundaryColumn(segment, sprInfo, whsize, jobShapeVertex6.pos, float6, flag2, ref @float, ref float2);
					}
				}
				if (i == 0 && segment.sgInfo.x == 0)
				{
					this.m_FirstLB = float3;
					this.m_FirstLT = float4;
				}
				if ((math.any(float4) || math.any(float3)) && (math.any(@float) || math.any(float2)))
				{
					float2 float7 = math.normalize(@float - float4);
					float4 float8 = new float4(float7.x, float7.y, 0f, -1f);
					jobShapeVertex.pos = float4;
					jobShapeVertex.meta = jobShapeVertex5.meta;
					jobShapeVertex.sprite = sprite;
					jobShapeVertex.tan = float8;
					jobShapeVertex2.pos = @float;
					jobShapeVertex2.meta = jobShapeVertex6.meta;
					jobShapeVertex2.sprite = sprite;
					jobShapeVertex2.tan = float8;
					jobShapeVertex3.pos = float3;
					jobShapeVertex3.meta = jobShapeVertex5.meta;
					jobShapeVertex3.sprite = sprite;
					jobShapeVertex3.tan = float8;
					jobShapeVertex4.pos = float2;
					jobShapeVertex4.meta = jobShapeVertex6.meta;
					jobShapeVertex4.sprite = sprite;
					jobShapeVertex4.tan = float8;
					if (validHead && i == 0)
					{
						jobShapeVertex.uv.x = (jobShapeVertex.uv.y = (jobShapeVertex2.uv.y = (jobShapeVertex3.uv.x = 0f)));
						jobShapeVertex2.uv.x = (jobShapeVertex4.uv.x = border.x / whsize.x);
						jobShapeVertex3.uv.y = (jobShapeVertex4.uv.y = 1f);
						jobShapeVertex.sprite.z = (jobShapeVertex3.sprite.z = (firstSegment ? 0 : 1));
					}
					else if (validTail && i == num3)
					{
						jobShapeVertex.uv.y = (jobShapeVertex2.uv.y = 0f);
						jobShapeVertex.uv.x = (jobShapeVertex3.uv.x = (whsize.x - border.z) / whsize.x);
						jobShapeVertex2.uv.x = (jobShapeVertex3.uv.y = (jobShapeVertex4.uv.x = (jobShapeVertex4.uv.y = 1f)));
						jobShapeVertex2.sprite.z = (jobShapeVertex4.sprite.z = (finalSegment ? 0 : 1));
					}
					else
					{
						if (num6 - num4 < this.kEpsilonRelaxed)
						{
							num7 = x / x2;
							num4 = 0f;
						}
						num4 += math.distance(jobShapeVertex6.pos, jobShapeVertex5.pos) * num8;
						float num10 = (num4 + x) / x2;
						if (num4 - num6 > this.kEpsilonRelaxed)
						{
							num10 = num5 / x2;
							num4 = num5;
						}
						jobShapeVertex.uv.y = (jobShapeVertex2.uv.y = 0f);
						jobShapeVertex.uv.x = (jobShapeVertex3.uv.x = num7);
						jobShapeVertex2.uv.x = (jobShapeVertex4.uv.x = num10);
						jobShapeVertex3.uv.y = (jobShapeVertex4.uv.y = 1f);
						num7 = num10;
					}
					jobShapeVertex.uv.x = jobShapeVertex.uv.x * sprInfo.uvInfo.z + sprInfo.uvInfo.x;
					jobShapeVertex.uv.y = jobShapeVertex.uv.y * sprInfo.uvInfo.w + sprInfo.uvInfo.y;
					outputVertices[num++] = jobShapeVertex;
					jobShapeVertex2.uv.x = jobShapeVertex2.uv.x * sprInfo.uvInfo.z + sprInfo.uvInfo.x;
					jobShapeVertex2.uv.y = jobShapeVertex2.uv.y * sprInfo.uvInfo.w + sprInfo.uvInfo.y;
					outputVertices[num++] = jobShapeVertex2;
					jobShapeVertex3.uv.x = jobShapeVertex3.uv.x * sprInfo.uvInfo.z + sprInfo.uvInfo.x;
					jobShapeVertex3.uv.y = jobShapeVertex3.uv.y * sprInfo.uvInfo.w + sprInfo.uvInfo.y;
					outputVertices[num++] = jobShapeVertex3;
					jobShapeVertex4.uv.x = jobShapeVertex4.uv.x * sprInfo.uvInfo.z + sprInfo.uvInfo.x;
					jobShapeVertex4.uv.y = jobShapeVertex4.uv.y * sprInfo.uvInfo.w + sprInfo.uvInfo.y;
					outputVertices[num++] = jobShapeVertex4;
				}
			}
			if (flag)
			{
				this.StretchCorners(segment, outputVertices, num, validHead, validTail, float5);
			}
			outputCount = num;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000BD40 File Offset: 0x00009F40
		private bool SkipSegment(SpriteShapeGenerator.JobSegmentInfo isi)
		{
			bool flag = isi.sgInfo.z < 0;
			if (!flag)
			{
				flag = !math.any(this.GetSpriteInfo(isi.sgInfo.z).uvInfo);
			}
			if (flag)
			{
				int i = this.GetContourIndex(isi.sgInfo.x);
				int endContourIndexOfSegment = this.GetEndContourIndexOfSegment(isi);
				while (i < endContourIndexOfSegment)
				{
					SpriteShapeGenerator.JobContourPoint contourPoint = this.GetContourPoint(i);
					int colliderDataCount = this.m_ColliderDataCount;
					this.m_ColliderDataCount = colliderDataCount + 1;
					this.m_ColliderPoints[colliderDataCount] = contourPoint.position;
					i++;
				}
			}
			return flag;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000BDD3 File Offset: 0x00009FD3
		private float InterpolateLinear(float a, float b, float t)
		{
			return math.lerp(a, b, t);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000BDE0 File Offset: 0x00009FE0
		private float InterpolateSmooth(float a, float b, float t)
		{
			float num = (1f - math.cos(t * 3.1415927f)) / 2f;
			return a * (1f - num) + b * num;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000BE14 File Offset: 0x0000A014
		private void TessellateSegments()
		{
			bool flag = this.GetControlPoint(0).cpData.z == this.kModeContinous && this.isCarpet;
			new float2(0f, 0f);
			for (int i = 0; i < this.segmentCount; i++)
			{
				SpriteShapeGenerator.JobSegmentInfo segmentInfo = this.GetSegmentInfo(i);
				if (!this.SkipSegment(segmentInfo))
				{
					SpriteShapeGenerator.JobShapeVertex jobShapeVertex = default(SpriteShapeGenerator.JobShapeVertex);
					SpriteShapeGenerator.JobSpriteInfo spriteInfo = this.GetSpriteInfo(segmentInfo.sgInfo.z);
					int num = 0;
					int z = segmentInfo.sgInfo.z;
					float num2 = 1f / spriteInfo.metaInfo.x;
					float2 @float = new float2(spriteInfo.metaInfo.z, spriteInfo.metaInfo.w) * num2;
					float4 float2 = spriteInfo.border * num2;
					SpriteShapeGenerator.JobControlPoint controlPoint = this.GetControlPoint(segmentInfo.sgInfo.x);
					SpriteShapeGenerator.JobControlPoint controlPoint2 = this.GetControlPoint(segmentInfo.sgInfo.y);
					bool flag2 = this.m_ControlPoints[0].cpData.z == this.kModeContinous && segmentInfo.sgInfo.y == this.controlPointCount - 1;
					bool flag3 = i == 0 && !this.isCarpet && !flag2;
					bool flag4 = this.hasSpriteBorder && float2.x > 0f && (controlPoint.exData.z == 0 || flag3);
					flag4 = ((this.m_ControlPoints[0].cpData.z == this.kModeContinous) ? (flag4 && !this.isCarpet) : flag4);
					bool flag5 = i == this.segmentCount - 1 && !this.isCarpet && !flag2;
					bool flag6 = this.hasSpriteBorder && float2.z > 0f && (controlPoint2.exData.z == 0 || flag5);
					flag6 = ((this.m_ControlPoints[0].cpData.z == this.kModeContinous) ? (flag6 && !this.isCarpet) : flag6);
					float num3 = 0f;
					float num4 = float2.x;
					float num5 = @float.x - float2.z - num4;
					float w = segmentInfo.spriteInfo.w;
					float num6 = math.floor(w / num5);
					num6 = ((num6 == 0f) ? 1f : num6);
					num5 = (this.isAdaptive ? (w / num6) : num5);
					if (num5 < this.kEpsilon)
					{
						this.Cleanup();
					}
					int contourIndex = this.GetContourIndex(segmentInfo.sgInfo.x);
					int endContourIndexOfSegment = this.GetEndContourIndexOfSegment(segmentInfo);
					if (contourIndex == 0)
					{
						flag4 = flag4 && !flag;
					}
					if (flag4)
					{
						SpriteShapeGenerator.JobContourPoint contourPoint = this.GetContourPoint(contourIndex);
						float2 position = contourPoint.position;
						float2 position2 = this.GetContourPoint(contourIndex + 1).position;
						jobShapeVertex.pos = position + math.normalize(position - position2) * float2.x;
						jobShapeVertex.meta.x = contourPoint.ptData.x;
						jobShapeVertex.sprite.x = z;
						this.m_VertexData[num++] = jobShapeVertex;
					}
					float num7 = 0f;
					int j = contourIndex;
					jobShapeVertex.sprite.z = 0;
					while (j < endContourIndexOfSegment)
					{
						int num8 = j + 1;
						SpriteShapeGenerator.JobContourPoint contourPoint2 = this.GetContourPoint(j);
						SpriteShapeGenerator.JobContourPoint contourPoint3 = this.GetContourPoint(num8);
						float2 float3 = contourPoint2.position;
						float2 float4 = contourPoint3.position - float3;
						float num9 = math.length(float4);
						if (num9 > this.kEpsilon)
						{
							float x = contourPoint2.ptData.x;
							float x2 = contourPoint3.ptData.x;
							float num10 = 0f;
							num7 += num9;
							bool flag7 = num == 0;
							float2 float5 = math.normalize(float4);
							jobShapeVertex.pos = contourPoint2.position;
							jobShapeVertex.meta.x = contourPoint2.ptData.x;
							jobShapeVertex.sprite.x = z;
							if (num > 0)
							{
								flag7 = math.length(this.m_VertexData[num - 1].pos - jobShapeVertex.pos) > this.kEpsilonRelaxed;
							}
							if (flag7)
							{
								this.m_VertexData[num++] = jobShapeVertex;
							}
							while (num7 > num5)
							{
								float num11 = num5 - num3;
								float2 float6 = new float2(num11);
								float2 float7 = float3 + float5 * float6;
								num10 += math.length(float7 - float3);
								jobShapeVertex.pos = float7;
								jobShapeVertex.meta.x = this.InterpolateLinear(x, x2, num10 / num9);
								jobShapeVertex.sprite.x = z;
								if (math.any(this.m_VertexData[num - 1].pos - jobShapeVertex.pos))
								{
									this.m_VertexData[num++] = jobShapeVertex;
								}
								num7 -= num5;
								float3 = float7;
								num3 = 0f;
							}
							num3 = num7;
						}
						j++;
					}
					if (num7 > this.kEpsilon)
					{
						SpriteShapeGenerator.JobContourPoint contourPoint4 = this.GetContourPoint(endContourIndexOfSegment);
						jobShapeVertex.pos = contourPoint4.position;
						jobShapeVertex.meta.x = contourPoint4.ptData.x;
						jobShapeVertex.sprite.x = z;
						this.m_VertexData[num++] = jobShapeVertex;
					}
					if (flag6)
					{
						SpriteShapeGenerator.JobContourPoint contourPoint5 = this.GetContourPoint(endContourIndexOfSegment);
						float2 position3 = contourPoint5.position;
						float2 position4 = this.GetContourPoint(endContourIndexOfSegment - 1).position;
						jobShapeVertex.pos = position3 + math.normalize(position3 - position4) * float2.z;
						jobShapeVertex.meta.x = contourPoint5.ptData.x;
						jobShapeVertex.sprite.x = z;
						this.m_VertexData[num++] = jobShapeVertex;
					}
					int num12 = 0;
					this.TessellateSegment(i, spriteInfo, segmentInfo, @float, float2, num5, this.m_VertexData, num, flag2, flag4, flag6, flag3, flag5, ref this.m_OutputVertexData, ref num12);
					if (num12 != 0)
					{
						float num13 = (float)(i + 1) * this.kEpsilonOrder + (float)segmentInfo.sgInfo.z * this.kEpsilonOrder * 0.001f;
						this.CopySegmentRenderData(spriteInfo, ref this.m_PosArray, ref this.m_Uv0Array, ref this.m_TanArray, ref this.m_VertexDataCount, ref this.m_IndexArray, ref this.m_IndexDataCount, this.m_OutputVertexData, num12, num13);
						if (this.hasCollider)
						{
							SpriteShapeGenerator.JobSpriteInfo jobSpriteInfo = ((spriteInfo.metaInfo.x == 0f) ? this.GetSpriteInfo(segmentInfo.sgInfo.w) : spriteInfo);
							num12 = 0;
							num2 = 1f / jobSpriteInfo.metaInfo.x;
							@float = new float2(jobSpriteInfo.metaInfo.z, jobSpriteInfo.metaInfo.w) * num2;
							float2 = jobSpriteInfo.border * num2;
							num4 = float2.x;
							num5 = @float.x - float2.z - num4;
							this.TessellateSegment(i, jobSpriteInfo, segmentInfo, @float, float2, num5, this.m_VertexData, num, flag2, flag4, flag6, flag3, flag5, ref this.m_OutputVertexData, ref num12);
							this.UpdateCollider(segmentInfo, jobSpriteInfo, this.m_OutputVertexData, num12, ref this.m_ColliderPoints, ref this.m_ColliderDataCount);
						}
						SpriteShapeSegment spriteShapeSegment = this.m_GeomArray[i + 1];
						spriteShapeSegment.geomIndex = i + 1;
						spriteShapeSegment.indexCount = this.m_IndexDataCount - this.m_ActiveIndexCount;
						spriteShapeSegment.vertexCount = this.m_VertexDataCount - this.m_ActiveVertexCount;
						spriteShapeSegment.spriteIndex = segmentInfo.sgInfo.z;
						this.m_GeomArray[i + 1] = spriteShapeSegment;
						this.m_ActiveIndexCount = this.m_IndexDataCount;
						this.m_ActiveVertexCount = this.m_VertexDataCount;
					}
				}
			}
			this.m_GeomArrayCount = this.segmentCount + 1;
			this.m_IndexArrayCount = this.m_IndexDataCount;
			this.m_VertexArrayCount = this.m_VertexDataCount;
			this.m_ColliderPointCount = this.m_ColliderDataCount;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C67C File Offset: 0x0000A87C
		private bool FetchStretcher(int segmentIndex, SpriteShapeGenerator.JobSpriteInfo sprInfo, SpriteShapeGenerator.JobSegmentInfo segment, float2 whsize, bool validHead, bool validTail, ref float4 stretcher)
		{
			bool flag = false;
			bool flag2 = false;
			int num = this.segmentCount - 1;
			int num2 = ((segmentIndex == 0) ? num : (segmentIndex - 1));
			int num3 = ((segmentIndex == num) ? 0 : (segmentIndex + 1));
			SpriteShapeGenerator.JobSegmentInfo segmentInfo = this.GetSegmentInfo(num2);
			SpriteShapeGenerator.JobSegmentInfo segmentInfo2 = this.GetSegmentInfo(num3);
			SpriteShapeGenerator.JobControlPoint controlPoint = this.GetControlPoint(segment.sgInfo.x);
			SpriteShapeGenerator.JobControlPoint controlPoint2 = this.GetControlPoint(segment.sgInfo.y);
			bool flag3 = controlPoint.cpData.y == 2 && math.abs(segmentInfo.sgInfo.x - segmentInfo.sgInfo.y) == 1;
			bool flag4 = controlPoint2.cpData.y == 2 && math.abs(segmentInfo2.sgInfo.x - segmentInfo2.sgInfo.y) == 1;
			int num4 = this.controlPointCount - 1;
			if (!this.isCarpet)
			{
				flag3 = flag3 && segment.sgInfo.x != 0;
				flag4 = flag4 && segment.sgInfo.y != num4;
			}
			if (flag3 || flag4)
			{
				float2 zero = float2.zero;
				float2 zero2 = float2.zero;
				float2 zero3 = float2.zero;
				float2 zero4 = float2.zero;
				this.GetLineSegments(sprInfo, segment, whsize, ref zero, ref zero2, ref zero3, ref zero4);
				float2 @float = zero;
				float2 float2 = zero2;
				float2 float3 = zero3;
				float2 float4 = zero4;
				float2 float5 = zero;
				float2 float6 = zero2;
				float2 float7 = zero3;
				float2 float8 = zero4;
				this.ExtendSegment(ref zero, ref zero3);
				this.ExtendSegment(ref zero2, ref zero4);
				if (flag3)
				{
					if (math.any(this.m_Intersectors[segment.sgInfo.x].top) && math.any(this.m_Intersectors[segment.sgInfo.x].bottom))
					{
						float5 = this.m_Intersectors[segment.sgInfo.x].top;
						float6 = this.m_Intersectors[segment.sgInfo.x].bottom;
						flag = true;
					}
					else
					{
						if (1 == controlPoint.exData.z)
						{
							float2 zero5 = float2.zero;
							float2 zero6 = float2.zero;
							float2 zero7 = float2.zero;
							float2 zero8 = float2.zero;
							this.GetLineSegments(sprInfo, segmentInfo, whsize, ref zero5, ref zero6, ref zero7, ref zero8);
							this.ExtendSegment(ref zero5, ref zero7);
							this.ExtendSegment(ref zero6, ref zero8);
							bool flag5 = SpriteShapeGenerator.LineIntersection(this.kEpsilon, zero5, zero7, zero, zero3, ref float5);
							bool flag6 = SpriteShapeGenerator.LineIntersection(this.kEpsilon, zero6, zero8, zero2, zero4, ref float6);
							flag = flag5 && flag6;
						}
						if (flag)
						{
							SpriteShapeGenerator.JobIntersectPoint jobIntersectPoint = this.m_Intersectors[segment.sgInfo.x];
							jobIntersectPoint.top = float5;
							jobIntersectPoint.bottom = float6;
							this.m_Intersectors[segment.sgInfo.x] = jobIntersectPoint;
						}
					}
				}
				if (flag4)
				{
					if (math.any(this.m_Intersectors[segment.sgInfo.y].top) && math.any(this.m_Intersectors[segment.sgInfo.y].bottom))
					{
						float7 = this.m_Intersectors[segment.sgInfo.y].top;
						float8 = this.m_Intersectors[segment.sgInfo.y].bottom;
						flag2 = true;
					}
					else
					{
						if (1 == controlPoint2.exData.z)
						{
							float2 zero9 = float2.zero;
							float2 zero10 = float2.zero;
							float2 zero11 = float2.zero;
							float2 zero12 = float2.zero;
							this.GetLineSegments(sprInfo, segmentInfo2, whsize, ref zero9, ref zero10, ref zero11, ref zero12);
							this.ExtendSegment(ref zero9, ref zero11);
							this.ExtendSegment(ref zero10, ref zero12);
							bool flag7 = SpriteShapeGenerator.LineIntersection(this.kEpsilon, zero, zero3, zero9, zero11, ref float7);
							bool flag8 = SpriteShapeGenerator.LineIntersection(this.kEpsilon, zero2, zero4, zero10, zero12, ref float8);
							flag2 = flag7 && flag8;
						}
						if (flag2)
						{
							SpriteShapeGenerator.JobIntersectPoint jobIntersectPoint2 = this.m_Intersectors[segment.sgInfo.y];
							jobIntersectPoint2.top = float7;
							jobIntersectPoint2.bottom = float8;
							this.m_Intersectors[segment.sgInfo.y] = jobIntersectPoint2;
						}
					}
				}
				if (flag || flag2)
				{
					float2 float9 = (@float + float2) * 0.5f;
					float2 float10 = (float3 + float4) * 0.5f;
					float num5 = math.length(float9 - float10);
					float num6 = math.length(float5 - float7);
					float num7 = math.length(float6 - float8);
					stretcher.x = num6 / num5;
					stretcher.y = num7 / num5;
					stretcher.z = (flag ? 1f : 0f);
					stretcher.w = (flag2 ? 1f : 0f);
				}
			}
			return flag || flag2;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000CB4C File Offset: 0x0000AD4C
		private void StretchCorners(SpriteShapeGenerator.JobSegmentInfo segment, NativeArray<SpriteShapeGenerator.JobShapeVertex> vertices, int vertexCount, bool validHead, bool validTail, float4 stretcher)
		{
			if (vertexCount > 0)
			{
				int num = (validHead ? 4 : 0);
				float2 @float = vertices[num].pos;
				float2 pos = vertices[num].pos;
				float2 float2 = vertices[vertexCount - 3].pos;
				SpriteShapeGenerator.JobShapeVertex jobShapeVertex = vertices[vertexCount - 3];
				float2 float3 = vertices[num + 2].pos;
				float2 pos2 = vertices[num + 2].pos;
				float2 float4 = vertices[vertexCount - 1].pos;
				SpriteShapeGenerator.JobShapeVertex jobShapeVertex2 = vertices[vertexCount - 1];
				if (math.any(this.m_Intersectors[segment.sgInfo.x].top) && math.any(this.m_Intersectors[segment.sgInfo.x].bottom))
				{
					@float = this.m_Intersectors[segment.sgInfo.x].top;
					float3 = this.m_Intersectors[segment.sgInfo.x].bottom;
				}
				if (math.any(this.m_Intersectors[segment.sgInfo.y].top) && math.any(this.m_Intersectors[segment.sgInfo.y].bottom))
				{
					float2 = this.m_Intersectors[segment.sgInfo.y].top;
					float4 = this.m_Intersectors[segment.sgInfo.y].bottom;
				}
				for (int i = num; i < vertexCount; i += 4)
				{
					SpriteShapeGenerator.JobShapeVertex jobShapeVertex3 = vertices[i];
					SpriteShapeGenerator.JobShapeVertex jobShapeVertex4 = vertices[i + 1];
					SpriteShapeGenerator.JobShapeVertex jobShapeVertex5 = vertices[i + 2];
					SpriteShapeGenerator.JobShapeVertex jobShapeVertex6 = vertices[i + 3];
					jobShapeVertex3.pos = @float + (vertices[i].pos - pos) * stretcher.x;
					jobShapeVertex4.pos = @float + (vertices[i + 1].pos - pos) * stretcher.x;
					jobShapeVertex5.pos = float3 + (vertices[i + 2].pos - pos2) * stretcher.y;
					jobShapeVertex6.pos = float3 + (vertices[i + 3].pos - pos2) * stretcher.y;
					vertices[i] = jobShapeVertex3;
					vertices[i + 1] = jobShapeVertex4;
					vertices[i + 2] = jobShapeVertex5;
					vertices[i + 3] = jobShapeVertex6;
				}
				SpriteShapeGenerator.JobShapeVertex jobShapeVertex7 = vertices[num];
				SpriteShapeGenerator.JobShapeVertex jobShapeVertex8 = vertices[num + 2];
				jobShapeVertex7.pos = @float;
				jobShapeVertex8.pos = float3;
				vertices[num] = jobShapeVertex7;
				vertices[num + 2] = jobShapeVertex8;
				SpriteShapeGenerator.JobShapeVertex jobShapeVertex9 = vertices[vertexCount - 3];
				SpriteShapeGenerator.JobShapeVertex jobShapeVertex10 = vertices[vertexCount - 1];
				jobShapeVertex9.pos = float2;
				jobShapeVertex10.pos = float4;
				vertices[vertexCount - 3] = jobShapeVertex9;
				vertices[vertexCount - 1] = jobShapeVertex10;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000CE90 File Offset: 0x0000B090
		private void ExtendSegment(ref float2 l0, ref float2 r0)
		{
			float2 @float = l0;
			float2 float2 = r0;
			float2 float3 = math.normalize(float2 - @float);
			r0 = float2 + float3 * this.kExtendSegment;
			l0 = @float + -float3 * this.kExtendSegment;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		private bool GetIntersection(int cp, int ct, SpriteShapeGenerator.JobSpriteInfo ispr, ref float2 lt0, ref float2 lb0, ref float2 rt0, ref float2 rb0, ref float2 lt1, ref float2 lb1, ref float2 rt1, ref float2 rb1, ref float2 tp, ref float2 bt)
		{
			new float2(0f, 0f);
			int num = ((cp == 0) ? (this.controlPointCount - 1) : (cp - 1));
			int num2 = (cp + 1) % this.controlPointCount;
			float num3 = 0.5f - ispr.metaInfo.y;
			SpriteShapeGenerator.JobControlPoint controlPoint = this.GetControlPoint(num);
			SpriteShapeGenerator.JobControlPoint controlPoint2 = this.GetControlPoint(cp);
			SpriteShapeGenerator.JobControlPoint controlPoint3 = this.GetControlPoint(num2);
			float num4 = 1f / ispr.metaInfo.x;
			float2 @float = new float2(ispr.texRect.z, ispr.texRect.w) * num4;
			float4 float2 = ispr.border * num4;
			float y = float2.y;
			float num5 = @float.y - float2.y;
			SpriteShapeGenerator.GenerateColumnsBi(controlPoint.position, controlPoint2.position, @float, false, ref lb0, ref lt0, controlPoint2.cpInfo.x * 0.5f, num3);
			SpriteShapeGenerator.GenerateColumnsBi(controlPoint2.position, controlPoint.position, @float, false, ref rt0, ref rb0, controlPoint2.cpInfo.x * 0.5f, num3);
			SpriteShapeGenerator.GenerateColumnsBi(controlPoint2.position, controlPoint3.position, @float, false, ref lb1, ref lt1, controlPoint2.cpInfo.x * 0.5f, num3);
			SpriteShapeGenerator.GenerateColumnsBi(controlPoint3.position, controlPoint2.position, @float, false, ref rt1, ref rb1, controlPoint2.cpInfo.x * 0.5f, num3);
			rt0 += math.normalize(rt0 - lt0) * this.kExtendSegment;
			rb0 += math.normalize(rb0 - lb0) * this.kExtendSegment;
			lt1 += math.normalize(lt1 - rt1) * this.kExtendSegment;
			lb1 += math.normalize(lb1 - rb1) * this.kExtendSegment;
			bool flag = SpriteShapeGenerator.LineIntersection(this.kEpsilon, lt0, rt0, lt1, rt1, ref tp);
			return SpriteShapeGenerator.LineIntersection(this.kEpsilon, lb0, rb0, lb1, rb1, ref bt) || flag;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		private bool AttachCorner(int cp, int ct, SpriteShapeGenerator.JobSpriteInfo ispr, ref NativeArray<SpriteShapeGenerator.JobControlPoint> newPoints, ref int activePoint)
		{
			float2 float10;
			float2 float9;
			float2 float8;
			float2 float7;
			float2 float6;
			float2 float5;
			float2 float4;
			float2 float3;
			float2 float2;
			float2 @float = (float2 = (float3 = (float4 = (float5 = (float6 = (float7 = (float8 = (float9 = (float10 = new float2(0f, 0f))))))))));
			float num = 0.5f - ispr.metaInfo.y;
			int num2 = ((cp == 0) ? (this.controlPointCount - 1) : (cp - 1));
			int num3 = (cp + 1) % this.controlPointCount;
			SpriteShapeGenerator.JobControlPoint controlPoint = this.GetControlPoint(num2);
			SpriteShapeGenerator.JobControlPoint controlPoint2 = this.GetControlPoint(cp);
			SpriteShapeGenerator.JobControlPoint controlPoint3 = this.GetControlPoint(num3);
			float num4 = 1f / ispr.metaInfo.x;
			float2 float11 = new float2(ispr.texRect.z, ispr.texRect.w) * num4;
			float4 float12 = ispr.border * num4;
			float y = float12.y;
			float num5 = float11.y - float12.y - y;
			if (!this.GetIntersection(cp, ct, ispr, ref float8, ref float7, ref float6, ref float5, ref float4, ref float3, ref @float, ref float2, ref float10, ref float9))
			{
				return false;
			}
			float2 position = controlPoint2.position;
			float2 float13 = controlPoint.position - position;
			float2 float14 = controlPoint3.position - position;
			float num6 = math.length(float13);
			float num7 = math.length(float14);
			if (num6 < num5 || num7 < num5)
			{
				return false;
			}
			float num8 = SpriteShapeGenerator.AngleBetweenVector(math.normalize(controlPoint.position - controlPoint2.position), math.normalize(controlPoint3.position - controlPoint2.position));
			float num9;
			float num10;
			if (num8 > 0f)
			{
				num9 = num6 - math.distance(float7, float9);
				num10 = num7 - math.distance(float9, float2);
			}
			else
			{
				num9 = num6 - math.distance(float8, float10);
				num10 = num7 - math.distance(float10, @float);
			}
			float2 float15 = position + math.normalize(float13) * num9;
			float2 float16 = position + math.normalize(float14) * num10;
			controlPoint2.exData.x = ct;
			controlPoint2.exData.z = 2;
			controlPoint2.position = float15;
			int num11 = activePoint;
			activePoint = num11 + 1;
			newPoints[num11] = controlPoint2;
			controlPoint2.exData.x = ct;
			controlPoint2.exData.z = 3;
			controlPoint2.position = float16;
			num11 = activePoint;
			activePoint = num11 + 1;
			newPoints[num11] = controlPoint2;
			SpriteShapeGenerator.JobCornerInfo jobCornerInfo = this.m_Corners[this.m_CornerCount];
			if (num8 > 0f)
			{
				jobCornerInfo.bottom = float9;
				jobCornerInfo.top = float10;
				SpriteShapeGenerator.GenerateColumnsBi(float15, controlPoint.position, float11, false, ref float8, ref float7, controlPoint2.cpInfo.x * ispr.metaInfo.y, num);
				SpriteShapeGenerator.GenerateColumnsBi(float16, controlPoint3.position, float11, false, ref float4, ref float3, controlPoint2.cpInfo.x * ispr.metaInfo.y, num);
				jobCornerInfo.left = float8;
				jobCornerInfo.right = float3;
			}
			else
			{
				jobCornerInfo.bottom = float10;
				jobCornerInfo.top = float9;
				SpriteShapeGenerator.GenerateColumnsBi(float15, controlPoint.position, float11, false, ref float8, ref float7, controlPoint2.cpInfo.x * ispr.metaInfo.y, num);
				SpriteShapeGenerator.GenerateColumnsBi(float16, controlPoint3.position, float11, false, ref float4, ref float3, controlPoint2.cpInfo.x * ispr.metaInfo.y, num);
				jobCornerInfo.left = float7;
				jobCornerInfo.right = float4;
			}
			jobCornerInfo.cornerData.x = ct;
			jobCornerInfo.cornerData.y = activePoint;
			this.m_Corners[this.m_CornerCount] = jobCornerInfo;
			this.m_CornerCount++;
			return true;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000D574 File Offset: 0x0000B774
		private float2 CornerTextureCoordinate(int cornerType, int index)
		{
			int num = (cornerType - 1) * 4;
			return this.m_CornerCoordinates[num + index];
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000D598 File Offset: 0x0000B798
		private int CalculateCorner(int index, float angle, float2 lt, float2 rt)
		{
			int num = 0;
			float num2 = SpriteShapeGenerator.SlopeAngle(lt);
			float2[] array = new float2[]
			{
				new float2(-135f, -35f),
				new float2(35f, 135f),
				new float2(-35f, 35f),
				new float2(-135f, 135f)
			};
			int2[] array2 = new int2[]
			{
				new int2(this.kCornerTypeInnerTopLeft, this.kCornerTypeOuterBottomLeft),
				new int2(this.kCornerTypeInnerBottomRight, this.kCornerTypeOuterTopRight),
				new int2(this.kCornerTypeInnerTopRight, this.kCornerTypeOuterTopLeft),
				new int2(this.kCornerTypeInnerBottomLeft, this.kCornerTypeOuterBottomRight)
			};
			for (int i = 0; i < 3; i++)
			{
				if (num2 > array[i].x && num2 < array[i].y)
				{
					num = ((angle > 0f) ? array2[i].x : array2[i].y);
					break;
				}
			}
			if (num == 0)
			{
				num = ((angle > 0f) ? this.kCornerTypeInnerBottomLeft : this.kCornerTypeOuterBottomRight);
			}
			return num;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000D6E8 File Offset: 0x0000B8E8
		private bool InsertCorner(int index, ref NativeArray<int2> cpSpriteIndices, ref NativeArray<SpriteShapeGenerator.JobControlPoint> newPoints, ref int activePoint, ref bool cornerConsidered)
		{
			int num = ((index == 0) ? (this.controlPointCount - 1) : (index - 1));
			int num2 = (index + 1) % this.controlPointCount;
			if (cpSpriteIndices[num].x >= this.spriteCount || cpSpriteIndices[index].x >= this.spriteCount)
			{
				return false;
			}
			if (cpSpriteIndices[num].y == 0 || cpSpriteIndices[index].y == 0)
			{
				return false;
			}
			SpriteShapeGenerator.JobControlPoint controlPoint = this.GetControlPoint(num);
			SpriteShapeGenerator.JobControlPoint controlPoint2 = this.GetControlPoint(index);
			SpriteShapeGenerator.JobControlPoint controlPoint3 = this.GetControlPoint(num2);
			if (controlPoint2.cpData.y == 0 || controlPoint.cpData.z != this.kModeLinear || controlPoint2.cpData.z != this.kModeLinear || controlPoint3.cpData.z != this.kModeLinear)
			{
				return false;
			}
			if (controlPoint.cpInfo.x != controlPoint2.cpInfo.x || controlPoint2.cpInfo.x != controlPoint3.cpInfo.x)
			{
				return false;
			}
			ref SpriteShapeGenerator.JobSpriteInfo spriteInfo = this.GetSpriteInfo(cpSpriteIndices[num].x);
			SpriteShapeGenerator.JobSpriteInfo spriteInfo2 = this.GetSpriteInfo(cpSpriteIndices[index].x);
			if (spriteInfo.metaInfo.y != spriteInfo2.metaInfo.y)
			{
				return false;
			}
			float2 @float = math.normalize(controlPoint3.position - controlPoint2.position);
			float2 float2 = math.normalize(controlPoint.position - controlPoint2.position);
			float num3 = SpriteShapeGenerator.AngleBetweenVector(@float, float2);
			float num4 = math.abs(num3);
			cornerConsidered = this.AngleWithinRange(num4, 90f - this.m_ShapeParams.curveData.z, 90f + this.m_ShapeParams.curveData.z) || this.m_ShapeParams.curveData.z == 90f;
			if (cornerConsidered && controlPoint2.cpData.y == 1)
			{
				float2 float3 = math.normalize(controlPoint2.position - controlPoint.position);
				int num5 = this.CalculateCorner(index, num3, float3, @float);
				if (num5 > 0)
				{
					SpriteShapeGenerator.JobSpriteInfo cornerSpriteInfo = this.GetCornerSpriteInfo(num5);
					return this.AttachCorner(index, num5, cornerSpriteInfo, ref newPoints, ref activePoint);
				}
			}
			return false;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000D920 File Offset: 0x0000BB20
		private void TessellateCorners()
		{
			for (int i = 1; i <= this.kCornerTypeInnerBottomRight; i++)
			{
				SpriteShapeGenerator.JobSpriteInfo cornerSpriteInfo = this.GetCornerSpriteInfo(i);
				if (cornerSpriteInfo.metaInfo.x != 0f)
				{
					int num = 0;
					int num2 = 0;
					Vector3 vector = this.m_PosArray[num];
					Vector2 vector2 = this.m_Uv0Array[num];
					bool flag = i <= this.kCornerTypeOuterBottomRight;
					int vertexArrayCount = this.m_VertexArrayCount;
					for (int j = 0; j < this.m_CornerCount; j++)
					{
						SpriteShapeGenerator.JobCornerInfo jobCornerInfo = this.m_Corners[j];
						if (jobCornerInfo.cornerData.x == i)
						{
							vector.x = jobCornerInfo.top.x;
							vector.y = jobCornerInfo.top.y;
							vector2.x = this.CornerTextureCoordinate(i, 1).x * cornerSpriteInfo.uvInfo.z + cornerSpriteInfo.uvInfo.x;
							vector2.y = this.CornerTextureCoordinate(i, 1).y * cornerSpriteInfo.uvInfo.w + cornerSpriteInfo.uvInfo.y;
							this.m_PosArray[this.m_VertexArrayCount] = vector;
							int num3 = this.m_VertexArrayCount;
							this.m_VertexArrayCount = num3 + 1;
							this.m_Uv0Array[num3] = vector2;
							vector.x = jobCornerInfo.right.x;
							vector.y = jobCornerInfo.right.y;
							vector2.x = this.CornerTextureCoordinate(i, 0).x * cornerSpriteInfo.uvInfo.z + cornerSpriteInfo.uvInfo.x;
							vector2.y = this.CornerTextureCoordinate(i, 0).y * cornerSpriteInfo.uvInfo.w + cornerSpriteInfo.uvInfo.y;
							this.m_PosArray[this.m_VertexArrayCount] = vector;
							num3 = this.m_VertexArrayCount;
							this.m_VertexArrayCount = num3 + 1;
							this.m_Uv0Array[num3] = vector2;
							vector.x = jobCornerInfo.left.x;
							vector.y = jobCornerInfo.left.y;
							vector2.x = this.CornerTextureCoordinate(i, 3).x * cornerSpriteInfo.uvInfo.z + cornerSpriteInfo.uvInfo.x;
							vector2.y = this.CornerTextureCoordinate(i, 3).y * cornerSpriteInfo.uvInfo.w + cornerSpriteInfo.uvInfo.y;
							this.m_PosArray[this.m_VertexArrayCount] = vector;
							num3 = this.m_VertexArrayCount;
							this.m_VertexArrayCount = num3 + 1;
							this.m_Uv0Array[num3] = vector2;
							vector.x = jobCornerInfo.bottom.x;
							vector.y = jobCornerInfo.bottom.y;
							vector2.x = this.CornerTextureCoordinate(i, 2).x * cornerSpriteInfo.uvInfo.z + cornerSpriteInfo.uvInfo.x;
							vector2.y = this.CornerTextureCoordinate(i, 2).y * cornerSpriteInfo.uvInfo.w + cornerSpriteInfo.uvInfo.y;
							this.m_PosArray[this.m_VertexArrayCount] = vector;
							num3 = this.m_VertexArrayCount;
							this.m_VertexArrayCount = num3 + 1;
							this.m_Uv0Array[num3] = vector2;
							num3 = this.m_IndexArrayCount;
							this.m_IndexArrayCount = num3 + 1;
							this.m_IndexArray[num3] = (ushort)num2;
							num3 = this.m_IndexArrayCount;
							this.m_IndexArrayCount = num3 + 1;
							this.m_IndexArray[num3] = (ushort)(num2 + (flag ? 1 : 3));
							num3 = this.m_IndexArrayCount;
							this.m_IndexArrayCount = num3 + 1;
							this.m_IndexArray[num3] = (ushort)(num2 + (flag ? 3 : 1));
							num3 = this.m_IndexArrayCount;
							this.m_IndexArrayCount = num3 + 1;
							this.m_IndexArray[num3] = (ushort)num2;
							num3 = this.m_IndexArrayCount;
							this.m_IndexArrayCount = num3 + 1;
							this.m_IndexArray[num3] = (ushort)(num2 + (flag ? 3 : 2));
							num3 = this.m_IndexArrayCount;
							this.m_IndexArrayCount = num3 + 1;
							this.m_IndexArray[num3] = (ushort)(num2 + (flag ? 2 : 3));
							num2 += 4;
							num += 6;
						}
					}
					if (this.m_TanArray.Length > 1)
					{
						for (int k = vertexArrayCount; k < this.m_VertexArrayCount; k++)
						{
							this.m_TanArray[k] = new Vector4(1f, 0f, 0f, -1f);
						}
					}
					if (num > 0 && num2 > 0)
					{
						SpriteShapeSegment spriteShapeSegment = this.m_GeomArray[this.m_GeomArrayCount];
						spriteShapeSegment.geomIndex = this.m_GeomArrayCount;
						spriteShapeSegment.indexCount = num;
						spriteShapeSegment.vertexCount = num2;
						spriteShapeSegment.spriteIndex = this.m_SpriteInfos.Length + (i - 1);
						int num3 = this.m_GeomArrayCount;
						this.m_GeomArrayCount = num3 + 1;
						this.m_GeomArray[num3] = spriteShapeSegment;
					}
				}
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000DE48 File Offset: 0x0000C048
		private bool AreCollinear(float2 a, float2 b, float2 c, float t)
		{
			float num = (a.y - b.y) * (a.x - c.x);
			float num2 = (a.y - c.y) * (a.x - b.x);
			return math.abs(num - num2) < t;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000DE98 File Offset: 0x0000C098
		private void OptimizePoints(float tolerance, ref NativeArray<float2> pointSet, ref int pointCount)
		{
			int num = 8;
			if (pointCount < num)
			{
				return;
			}
			int num2 = 0;
			int num3 = pointCount - 2;
			this.m_TempPoints[0] = pointSet[0];
			for (int i = 0; i < num3; i++)
			{
				int num4 = i;
				float2 @float = pointSet[i];
				float2 float2 = pointSet[i + 1];
				float2 float3 = pointSet[i + 2];
				bool flag = true;
				while (flag && num4 < num3)
				{
					flag = this.AreCollinear(@float, float2, float3, tolerance);
					if (!flag)
					{
						this.m_TempPoints[++num2] = float2;
						i = num4;
						break;
					}
					num4++;
					float2 = pointSet[num4 + 1];
					float3 = pointSet[num4 + 2];
				}
			}
			this.m_TempPoints[++num2] = pointSet[num3];
			this.m_TempPoints[++num2] = pointSet[num3 + 1];
			if (this.isCarpet)
			{
				this.m_TempPoints[++num2] = pointSet[0];
			}
			int num5 = num2 + 1;
			if (num5 > 0)
			{
				pointCount = 0;
				int num6 = pointCount;
				pointCount = num6 + 1;
				pointSet[num6] = this.m_TempPoints[0];
				for (int j = 1; j < num5; j++)
				{
					if (math.distance(pointSet[pointCount - 1], this.m_TempPoints[j]) > 0.0001f)
					{
						num6 = pointCount;
						pointCount = num6 + 1;
						pointSet[num6] = this.m_TempPoints[j];
					}
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000E024 File Offset: 0x0000C224
		private void AttachCornerToCollider(SpriteShapeGenerator.JobSegmentInfo isi, float pivot, ref NativeArray<float2> colliderPoints, ref int colliderPointCount)
		{
			float2 @float = new float2(0f, 0f);
			int num = isi.sgInfo.x + 1;
			for (int i = 0; i < this.m_CornerCount; i++)
			{
				SpriteShapeGenerator.JobCornerInfo jobCornerInfo = this.m_Corners[i];
				if (num == jobCornerInfo.cornerData.y)
				{
					float2 float2;
					if (jobCornerInfo.cornerData.x > this.kCornerTypeOuterBottomRight)
					{
						float2 = jobCornerInfo.top;
					}
					else
					{
						float2 = jobCornerInfo.bottom;
					}
					float2 float3;
					if (jobCornerInfo.cornerData.x > this.kCornerTypeOuterBottomRight)
					{
						float3 = jobCornerInfo.bottom;
					}
					else
					{
						float3 = jobCornerInfo.top;
					}
					float2 float4 = (float2 - float3) * pivot;
					float4 = (float3 + float4 + float2 + float4) * 0.5f;
					int num2 = colliderPointCount;
					colliderPointCount = num2 + 1;
					colliderPoints[num2] = float4;
					return;
				}
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000E124 File Offset: 0x0000C324
		private float2 UpdateCollider(SpriteShapeGenerator.JobSegmentInfo isi, SpriteShapeGenerator.JobSpriteInfo ispr, NativeArray<SpriteShapeGenerator.JobShapeVertex> vertices, int count, ref NativeArray<float2> colliderPoints, ref int colliderPointCount)
		{
			new float2(0f, 0f);
			float num = 0f;
			num += this.colliderPivot;
			this.AttachCornerToCollider(isi, num, ref colliderPoints, ref colliderPointCount);
			float2 @float;
			for (int i = 0; i < count; i += 4)
			{
				float2 pos = vertices[i].pos;
				float2 pos2 = vertices[i + 2].pos;
				@float = (pos - pos2) * num;
				if (vertices[i].sprite.z == 0)
				{
					int num2 = colliderPointCount;
					colliderPointCount = num2 + 1;
					colliderPoints[num2] = (pos2 + @float + pos + @float) * 0.5f;
				}
			}
			float2 pos3 = vertices[count - 1].pos;
			float2 pos4 = vertices[count - 3].pos;
			@float = (pos4 - pos3) * num;
			if (vertices[count - 1].sprite.z == 0)
			{
				int num2 = colliderPointCount;
				colliderPointCount = num2 + 1;
				colliderPoints[num2] = (pos3 + @float + pos4 + @float) * 0.5f;
			}
			return @float;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000E26C File Offset: 0x0000C46C
		private void TrimOverlaps(int cpCount)
		{
			int num = 4;
			if (this.m_ColliderPointCount < num)
			{
				return;
			}
			int num2 = 0;
			int i = 0;
			int num3 = this.m_ColliderPointCount / 2;
			int num4 = math.clamp(this.splineDetail * 3, 0, 8);
			int num5 = ((num4 > num3) ? num3 : num4);
			num5 = ((num5 > cpCount) ? cpCount : num5);
			if (!this.isCarpet)
			{
				this.m_TempPoints[num2++] = this.m_ColliderPoints[0];
			}
			while (i < this.m_ColliderPointCount)
			{
				int num6 = ((i > 0) ? (i - 1) : (this.m_ColliderPointCount - 1));
				bool flag = true;
				float2 @float = this.m_ColliderPoints[num6];
				float2 float2 = this.m_ColliderPoints[i];
				for (int j = num5; j > 1; j--)
				{
					int num7 = (i + j - 1) % this.m_ColliderPointCount;
					int num8 = (i + j) % this.m_ColliderPointCount;
					if (num8 != 0 && i != 0)
					{
						float2 float3 = this.m_ColliderPoints[num7];
						float2 float4 = this.m_ColliderPoints[num8];
						if (math.abs(math.length(@float - float4)) < this.kEpsilon)
						{
							break;
						}
						float2 float5 = @float;
						if (SpriteShapeGenerator.LineIntersection(this.kEpsilonRelaxed, @float, float2, float3, float4, ref float5) && SpriteShapeGenerator.IsPointOnLines(this.kEpsilonRelaxed, @float, float2, float3, float4, float5))
						{
							flag = false;
							this.m_TempPoints[num2++] = float5;
							i += j;
							break;
						}
					}
				}
				if (flag)
				{
					this.m_TempPoints[num2++] = float2;
					i++;
				}
			}
			while (i < this.m_ColliderPointCount)
			{
				this.m_TempPoints[num2++] = this.m_ColliderPoints[i];
				i++;
			}
			i = 0;
			this.m_ColliderPoints[i++] = this.m_TempPoints[0];
			float2 float6 = this.m_TempPoints[0];
			for (int k = 1; k < num2; k++)
			{
				if (math.length(this.m_TempPoints[k] - float6) > this.kEpsilon)
				{
					this.m_ColliderPoints[i++] = this.m_TempPoints[k];
				}
				float6 = this.m_TempPoints[k];
			}
			num2 = i;
			if (num2 > 4)
			{
				float2 float7 = this.m_ColliderPoints[0];
				if (SpriteShapeGenerator.LineIntersection(this.kEpsilonRelaxed, this.m_ColliderPoints[0], this.m_ColliderPoints[1], this.m_ColliderPoints[num2 - 1], this.m_ColliderPoints[num2 - 2], ref float7))
				{
					this.m_ColliderPoints[0] = (this.m_ColliderPoints[num2 - 1] = float7);
				}
			}
			this.m_ColliderPointCount = num2;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000E53C File Offset: 0x0000C73C
		private void OptimizeCollider()
		{
			if (this.hasCollider)
			{
				if (this.kColliderQuality > 0f)
				{
					this.OptimizePoints(this.kColliderQuality, ref this.m_ColliderPoints, ref this.m_ColliderPointCount);
					this.TrimOverlaps(this.m_ControlPointCount - 1);
					int num = this.m_ColliderPointCount;
					this.m_ColliderPointCount = num + 1;
					this.m_ColliderPoints[num] = new float2(0f, 0f);
					num = this.m_ColliderPointCount;
					this.m_ColliderPointCount = num + 1;
					this.m_ColliderPoints[num] = new float2(0f, 0f);
				}
				if (this.m_ColliderPointCount <= 4)
				{
					for (int i = 0; i < this.m_TessPointCount; i++)
					{
						this.m_ColliderPoints[i] = this.m_TessPoints[i];
					}
					this.m_ColliderPoints[this.m_TessPointCount] = new float2(0f, 0f);
					this.m_ColliderPoints[this.m_TessPointCount + 1] = new float2(0f, 0f);
					this.m_ColliderPointCount = this.m_TessPointCount + 2;
				}
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000E660 File Offset: 0x0000C860
		[Obsolete]
		public void Prepare(SpriteShapeController controller, SpriteShapeParameters shapeParams, int maxArrayCount, NativeArray<ShapeControlPoint> shapePoints, NativeArray<SpriteShapeMetaData> metaData, AngleRangeInfo[] angleRanges, Sprite[] segmentSprites, Sprite[] cornerSprites)
		{
			this.PrepareInput(shapeParams, maxArrayCount, shapePoints, controller.optimizeGeometry, controller.autoUpdateCollider, controller.optimizeCollider, controller.colliderOffset, (float)controller.colliderDetail);
			this.PrepareSprites(segmentSprites, cornerSprites);
			this.PrepareAngleRanges(angleRanges);
			NativeArray<SplinePointMetaData> nativeArray = new NativeArray<SplinePointMetaData>(metaData.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < metaData.Length; i++)
			{
				nativeArray[i] = new SplinePointMetaData
				{
					height = metaData[i].height,
					spriteIndex = metaData[i].spriteIndex,
					cornerMode = (metaData[i].corner ? 1 : 0)
				};
			}
			this.PrepareControlPoints(shapePoints, nativeArray);
			nativeArray.Dispose();
			this.kModeUTess = 0;
			this.TessellateContourMainThread();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000E73C File Offset: 0x0000C93C
		internal void Prepare(SpriteShapeController controller, SpriteShapeParameters shapeParams, int maxArrayCount, NativeArray<ShapeControlPoint> shapePoints, NativeArray<SplinePointMetaData> metaData, AngleRangeInfo[] angleRanges, Sprite[] segmentSprites, Sprite[] cornerSprites, bool UseUTess)
		{
			this.PrepareInput(shapeParams, maxArrayCount, shapePoints, controller.optimizeGeometry, controller.autoUpdateCollider, controller.optimizeCollider, controller.colliderOffset, (float)controller.colliderDetail);
			this.PrepareSprites(segmentSprites, cornerSprites);
			this.PrepareAngleRanges(angleRanges);
			this.PrepareControlPoints(shapePoints, metaData);
			this.kModeUTess = (UseUTess ? 1 : 0);
			if (this.kModeUTess == 0)
			{
				this.TessellateContourMainThread();
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		public void Execute()
		{
			if (this.kModeUTess != 0)
			{
				this.TessellateContour();
			}
			this.GenerateSegments();
			this.UpdateSegments();
			this.TessellateSegments();
			this.TessellateCorners();
			this.CalculateTexCoords();
			this.CalculateBoundingBox();
			this.OptimizeCollider();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
		public void Cleanup()
		{
			SpriteShapeGenerator.SafeDispose<SpriteShapeGenerator.JobCornerInfo>(this.m_Corners);
			SpriteShapeGenerator.SafeDispose<SpriteShapeGenerator.JobSpriteInfo>(this.m_CornerSpriteInfos);
			SpriteShapeGenerator.SafeDispose<SpriteShapeGenerator.JobSpriteInfo>(this.m_SpriteInfos);
			SpriteShapeGenerator.SafeDispose<SpriteShapeGenerator.JobAngleRange>(this.m_AngleRanges);
			SpriteShapeGenerator.SafeDispose<SpriteShapeGenerator.JobSegmentInfo>(this.m_Segments);
			SpriteShapeGenerator.SafeDispose<SpriteShapeGenerator.JobControlPoint>(this.m_ControlPoints);
			SpriteShapeGenerator.SafeDispose<SpriteShapeGenerator.JobContourPoint>(this.m_ContourPoints);
			SpriteShapeGenerator.SafeDispose<float2>(this.m_TempPoints);
			SpriteShapeGenerator.SafeDispose<SpriteShapeGenerator.JobControlPoint>(this.m_GeneratedControlPoints);
			SpriteShapeGenerator.SafeDispose<int2>(this.m_SpriteIndices);
			SpriteShapeGenerator.SafeDispose<SpriteShapeGenerator.JobIntersectPoint>(this.m_Intersectors);
			SpriteShapeGenerator.SafeDispose<float2>(this.m_TessPoints);
			SpriteShapeGenerator.SafeDispose<SpriteShapeGenerator.JobShapeVertex>(this.m_VertexData);
			SpriteShapeGenerator.SafeDispose<SpriteShapeGenerator.JobShapeVertex>(this.m_OutputVertexData);
			SpriteShapeGenerator.SafeDispose<float2>(this.m_CornerCoordinates);
		}

		// Token: 0x040000AB RID: 171
		public ProfilerMarker generateGeometry;

		// Token: 0x040000AC RID: 172
		public ProfilerMarker generateCollider;

		// Token: 0x040000AD RID: 173
		[ReadOnly]
		private SpriteShapeGenerator.JobParameters m_ShapeParams;

		// Token: 0x040000AE RID: 174
		[ReadOnly]
		[DeallocateOnJobCompletion]
		private NativeArray<SpriteShapeGenerator.JobSpriteInfo> m_SpriteInfos;

		// Token: 0x040000AF RID: 175
		[ReadOnly]
		[DeallocateOnJobCompletion]
		private NativeArray<SpriteShapeGenerator.JobSpriteInfo> m_CornerSpriteInfos;

		// Token: 0x040000B0 RID: 176
		[ReadOnly]
		[DeallocateOnJobCompletion]
		private NativeArray<SpriteShapeGenerator.JobAngleRange> m_AngleRanges;

		// Token: 0x040000B1 RID: 177
		[DeallocateOnJobCompletion]
		private NativeArray<SpriteShapeGenerator.JobSegmentInfo> m_Segments;

		// Token: 0x040000B2 RID: 178
		private int m_SegmentCount;

		// Token: 0x040000B3 RID: 179
		[DeallocateOnJobCompletion]
		private NativeArray<SpriteShapeGenerator.JobContourPoint> m_ContourPoints;

		// Token: 0x040000B4 RID: 180
		private int m_ContourPointCount;

		// Token: 0x040000B5 RID: 181
		[DeallocateOnJobCompletion]
		private NativeArray<SpriteShapeGenerator.JobCornerInfo> m_Corners;

		// Token: 0x040000B6 RID: 182
		private int m_CornerCount;

		// Token: 0x040000B7 RID: 183
		[DeallocateOnJobCompletion]
		private NativeArray<float2> m_TessPoints;

		// Token: 0x040000B8 RID: 184
		private int m_TessPointCount;

		// Token: 0x040000B9 RID: 185
		[DeallocateOnJobCompletion]
		private NativeArray<SpriteShapeGenerator.JobShapeVertex> m_VertexData;

		// Token: 0x040000BA RID: 186
		[DeallocateOnJobCompletion]
		private NativeArray<SpriteShapeGenerator.JobShapeVertex> m_OutputVertexData;

		// Token: 0x040000BB RID: 187
		[DeallocateOnJobCompletion]
		private NativeArray<SpriteShapeGenerator.JobControlPoint> m_ControlPoints;

		// Token: 0x040000BC RID: 188
		private int m_ControlPointCount;

		// Token: 0x040000BD RID: 189
		[DeallocateOnJobCompletion]
		private NativeArray<float2> m_CornerCoordinates;

		// Token: 0x040000BE RID: 190
		[DeallocateOnJobCompletion]
		private NativeArray<float2> m_TempPoints;

		// Token: 0x040000BF RID: 191
		[DeallocateOnJobCompletion]
		private NativeArray<SpriteShapeGenerator.JobControlPoint> m_GeneratedControlPoints;

		// Token: 0x040000C0 RID: 192
		[DeallocateOnJobCompletion]
		private NativeArray<int2> m_SpriteIndices;

		// Token: 0x040000C1 RID: 193
		[DeallocateOnJobCompletion]
		private NativeArray<SpriteShapeGenerator.JobIntersectPoint> m_Intersectors;

		// Token: 0x040000C2 RID: 194
		private int m_IndexArrayCount;

		// Token: 0x040000C3 RID: 195
		public NativeArray<ushort> m_IndexArray;

		// Token: 0x040000C4 RID: 196
		private int m_VertexArrayCount;

		// Token: 0x040000C5 RID: 197
		public NativeSlice<Vector3> m_PosArray;

		// Token: 0x040000C6 RID: 198
		public NativeSlice<Vector2> m_Uv0Array;

		// Token: 0x040000C7 RID: 199
		public NativeSlice<Vector4> m_TanArray;

		// Token: 0x040000C8 RID: 200
		private int m_GeomArrayCount;

		// Token: 0x040000C9 RID: 201
		public NativeArray<SpriteShapeSegment> m_GeomArray;

		// Token: 0x040000CA RID: 202
		private int m_ColliderPointCount;

		// Token: 0x040000CB RID: 203
		public NativeArray<float2> m_ColliderPoints;

		// Token: 0x040000CC RID: 204
		public NativeArray<Bounds> m_Bounds;

		// Token: 0x040000CD RID: 205
		private int m_IndexDataCount;

		// Token: 0x040000CE RID: 206
		private int m_VertexDataCount;

		// Token: 0x040000CF RID: 207
		private int m_ColliderDataCount;

		// Token: 0x040000D0 RID: 208
		private int m_ActiveIndexCount;

		// Token: 0x040000D1 RID: 209
		private int m_ActiveVertexCount;

		// Token: 0x040000D2 RID: 210
		private float2 m_FirstLT;

		// Token: 0x040000D3 RID: 211
		private float2 m_FirstLB;

		// Token: 0x040000D4 RID: 212
		private float4x4 m_Transform;

		// Token: 0x040000D5 RID: 213
		private int kModeLinear;

		// Token: 0x040000D6 RID: 214
		private int kModeContinous;

		// Token: 0x040000D7 RID: 215
		private int kModeBroken;

		// Token: 0x040000D8 RID: 216
		private int kModeUTess;

		// Token: 0x040000D9 RID: 217
		private int kCornerTypeOuterTopLeft;

		// Token: 0x040000DA RID: 218
		private int kCornerTypeOuterTopRight;

		// Token: 0x040000DB RID: 219
		private int kCornerTypeOuterBottomLeft;

		// Token: 0x040000DC RID: 220
		private int kCornerTypeOuterBottomRight;

		// Token: 0x040000DD RID: 221
		private int kCornerTypeInnerTopLeft;

		// Token: 0x040000DE RID: 222
		private int kCornerTypeInnerTopRight;

		// Token: 0x040000DF RID: 223
		private int kCornerTypeInnerBottomLeft;

		// Token: 0x040000E0 RID: 224
		private int kCornerTypeInnerBottomRight;

		// Token: 0x040000E1 RID: 225
		private int kControlPointCount;

		// Token: 0x040000E2 RID: 226
		private float kEpsilon;

		// Token: 0x040000E3 RID: 227
		private float kEpsilonOrder;

		// Token: 0x040000E4 RID: 228
		private float kEpsilonRelaxed;

		// Token: 0x040000E5 RID: 229
		private float kExtendSegment;

		// Token: 0x040000E6 RID: 230
		private float kRenderQuality;

		// Token: 0x040000E7 RID: 231
		private float kOptimizeRender;

		// Token: 0x040000E8 RID: 232
		private float kColliderQuality;

		// Token: 0x040000E9 RID: 233
		private float kOptimizeCollider;

		// Token: 0x040000EA RID: 234
		private float kLowestQualityTolerance;

		// Token: 0x040000EB RID: 235
		private float kHighestQualityTolerance;

		// Token: 0x0200002D RID: 45
		private struct JobParameters
		{
			// Token: 0x04000118 RID: 280
			public int4 shapeData;

			// Token: 0x04000119 RID: 281
			public int4 splineData;

			// Token: 0x0400011A RID: 282
			public float4 curveData;

			// Token: 0x0400011B RID: 283
			public float4 fillData;
		}

		// Token: 0x0200002E RID: 46
		private struct JobSpriteInfo
		{
			// Token: 0x0400011C RID: 284
			public float4 texRect;

			// Token: 0x0400011D RID: 285
			public float4 texData;

			// Token: 0x0400011E RID: 286
			public float4 uvInfo;

			// Token: 0x0400011F RID: 287
			public float4 metaInfo;

			// Token: 0x04000120 RID: 288
			public float4 border;
		}

		// Token: 0x0200002F RID: 47
		private struct JobAngleRange
		{
			// Token: 0x04000121 RID: 289
			public float4 spriteAngles;

			// Token: 0x04000122 RID: 290
			public int4 spriteData;
		}

		// Token: 0x02000030 RID: 48
		private struct JobControlPoint
		{
			// Token: 0x04000123 RID: 291
			public int4 cpData;

			// Token: 0x04000124 RID: 292
			public int4 exData;

			// Token: 0x04000125 RID: 293
			public float2 cpInfo;

			// Token: 0x04000126 RID: 294
			public float2 position;

			// Token: 0x04000127 RID: 295
			public float2 tangentLt;

			// Token: 0x04000128 RID: 296
			public float2 tangentRt;
		}

		// Token: 0x02000031 RID: 49
		private struct JobContourPoint
		{
			// Token: 0x04000129 RID: 297
			public float2 position;

			// Token: 0x0400012A RID: 298
			public float2 ptData;
		}

		// Token: 0x02000032 RID: 50
		private struct JobIntersectPoint
		{
			// Token: 0x0400012B RID: 299
			public float2 top;

			// Token: 0x0400012C RID: 300
			public float2 bottom;
		}

		// Token: 0x02000033 RID: 51
		private struct JobSegmentInfo
		{
			// Token: 0x0400012D RID: 301
			public int4 sgInfo;

			// Token: 0x0400012E RID: 302
			public float4 spriteInfo;
		}

		// Token: 0x02000034 RID: 52
		private struct JobCornerInfo
		{
			// Token: 0x0400012F RID: 303
			public float2 bottom;

			// Token: 0x04000130 RID: 304
			public float2 top;

			// Token: 0x04000131 RID: 305
			public float2 left;

			// Token: 0x04000132 RID: 306
			public float2 right;

			// Token: 0x04000133 RID: 307
			public int2 cornerData;
		}

		// Token: 0x02000035 RID: 53
		private struct JobShapeVertex
		{
			// Token: 0x04000134 RID: 308
			public float2 pos;

			// Token: 0x04000135 RID: 309
			public float2 uv;

			// Token: 0x04000136 RID: 310
			public float4 tan;

			// Token: 0x04000137 RID: 311
			public float2 meta;

			// Token: 0x04000138 RID: 312
			public int4 sprite;
		}
	}
}
