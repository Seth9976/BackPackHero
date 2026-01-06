using System;
using Pathfinding.Drawing;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;

namespace Pathfinding.PID
{
	// Token: 0x0200023C RID: 572
	[BurstCompile]
	[Serializable]
	public struct PIDMovement
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00054D7C File Offset: 0x00052F7C
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x00054D87 File Offset: 0x00052F87
		public bool allowRotatingOnSpot
		{
			get
			{
				return this.allowRotatingOnSpotBacking > 0;
			}
			set
			{
				this.allowRotatingOnSpotBacking = (value ? 1 : 0);
			}
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00054D97 File Offset: 0x00052F97
		public void ScaleByAgentScale(float agentScale)
		{
			this.speed *= agentScale;
			this.leadInRadiusWhenApproachingDestination *= agentScale;
			this.desiredWallDistance *= agentScale;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00054DC4 File Offset: 0x00052FC4
		public float Speed(float remainingDistance)
		{
			if (this.speed <= 0f)
			{
				return 0f;
			}
			if (this.slowdownTime > 0f)
			{
				float num = Mathf.Min(1f, Mathf.Sqrt(2f * remainingDistance / (this.speed * this.slowdownTime)));
				return this.speed * num;
			}
			if (remainingDistance > 0.0001f)
			{
				return this.speed;
			}
			return 0f;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00054E34 File Offset: 0x00053034
		public float Accelerate(float speed, float timeToReachMaxSpeed, float dt)
		{
			if (timeToReachMaxSpeed > 0.001f)
			{
				float num = this.speed / timeToReachMaxSpeed;
				return math.clamp(speed + dt * num, 0f, this.speed);
			}
			if (dt <= 0f)
			{
				return 0f;
			}
			return this.speed;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00054E7C File Offset: 0x0005307C
		public float CurveFollowingStrength(float signedDistToClearArea, float radiusToWall, float remainingDistance)
		{
			float num = math.max(1E-05f, this.speed);
			float num2 = math.max(AnglePIDController.RotationSpeedToFollowingStrength(num, math.radians(this.rotationSpeed)), 40f * math.pow(math.abs(signedDistToClearArea) / math.max(0.0001f, radiusToWall), 1f));
			float num3 = remainingDistance / num;
			return math.max(num2, math.min(80f, math.pow(1f / math.max(0f, num3 - 0.2f), 3f)));
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00054F08 File Offset: 0x00053108
		private static bool ClipLineByHalfPlaneX(ref float2 a, ref float2 b, float x, float side)
		{
			bool flag = (a.x - x) * side < 0f;
			bool flag2 = (b.x - x) * side < 0f;
			if (flag && flag2)
			{
				return false;
			}
			if (flag != flag2)
			{
				float num = math.unlerp(a.x, b.x, x);
				float2 @float = math.lerp(a, b, num);
				if (flag)
				{
					a = @float;
				}
				else
				{
					b = @float;
				}
			}
			return true;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00054F80 File Offset: 0x00053180
		private static void ClipLineByHalfPlaneYt(float2 a, float2 b, float y, float side, ref float mnT, ref float mxT)
		{
			bool flag = (a.y - y) * side < 0f;
			bool flag2 = (b.y - y) * side < 0f;
			if (flag && flag2)
			{
				mnT = 1f;
				mxT = 0f;
				return;
			}
			if (flag != flag2)
			{
				float num = math.unlerp(a.y, b.y, y);
				if (flag)
				{
					mnT = math.max(mnT, num);
					return;
				}
				mxT = math.min(mxT, num);
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00054FFA File Offset: 0x000531FA
		private static float2 MaxAngle(float2 a, float2 b, float2 c, bool clockwise)
		{
			a = math.select(a, b, VectorMath.Determinant(a, b) < 0f == clockwise);
			a = math.select(a, c, VectorMath.Determinant(a, c) < 0f == clockwise);
			return a;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00055031 File Offset: 0x00053231
		private static float2 MaxAngle(float2 a, float2 b, bool clockwise)
		{
			return math.select(a, b, VectorMath.Determinant(a, b) < 0f == clockwise);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0005504C File Offset: 0x0005324C
		private static void DrawChisel(float2 start, float2 direction, float pointiness, float length, float width, CommandBuilder draw, Color col)
		{
			draw.PushColor(col);
			float2 @float = start + (direction * pointiness + new float2(-direction.y, direction.x)) * width;
			float2 float2 = start + (direction * pointiness - new float2(-direction.y, direction.x)) * width;
			draw.xz.Line(start, @float, col);
			draw.xz.Line(start, float2, col);
			float num = length - pointiness * width;
			if (num > 0f)
			{
				draw.xz.Ray(@float, direction * num, col);
				draw.xz.Ray(float2, direction * num, col);
			}
			draw.PopColor();
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00055128 File Offset: 0x00053328
		private static void SplitSegment(float2 e1, float2 e2, float desiredRadius, float length, float pointiness, ref PIDMovement.EdgeBuffers buffers)
		{
			float num = desiredRadius * 2f;
			if ((e1.y < -num && e2.y < -num) || (e1.y > num && e2.y > num))
			{
				return;
			}
			if (!PIDMovement.ClipLineByHalfPlaneX(ref e1, ref e2, 0f, 1f))
			{
				return;
			}
			float num2;
			float num3;
			if (!VectorMath.SegmentCircleIntersectionFactors(e1, e2, length * length, out num2, out num3))
			{
				return;
			}
			float num4 = desiredRadius * 0.01f;
			float num5;
			float num6;
			if (VectorMath.SegmentCircleIntersectionFactors(e1, e2, num4 * num4, out num5, out num6) && num5 < num3 && num6 > num2)
			{
				if (num5 > num2 && num5 < num3)
				{
					PIDMovement.SplitSegment2(math.lerp(e1, e2, num2), math.lerp(e1, e2, num5), desiredRadius, pointiness, ref buffers);
				}
				if (num6 > num2 && num6 < num3)
				{
					PIDMovement.SplitSegment2(math.lerp(e1, e2, num6), math.lerp(e1, e2, num3), desiredRadius, pointiness, ref buffers);
					return;
				}
			}
			else
			{
				PIDMovement.SplitSegment2(math.lerp(e1, e2, num2), math.lerp(e1, e2, num3), desiredRadius, pointiness, ref buffers);
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00055214 File Offset: 0x00053414
		private static void SplitSegment2(float2 e1, float2 e2, float desiredRadius, float pointiness, ref PIDMovement.EdgeBuffers buffers)
		{
			float num;
			float num2;
			if (!VectorMath.SegmentCircleIntersectionFactors(e1, e2, (pointiness * pointiness + 1f) * desiredRadius * desiredRadius, out num, out num2))
			{
				PIDMovement.SplitSegment3(e1, e2, desiredRadius, false, ref buffers);
				return;
			}
			if (num > 0f && num2 < 1f)
			{
				PIDMovement.SplitSegment3(e1, math.lerp(e1, e2, num), desiredRadius, false, ref buffers);
				PIDMovement.SplitSegment3(math.lerp(e1, e2, num), math.lerp(e1, e2, num2), desiredRadius, true, ref buffers);
				PIDMovement.SplitSegment3(math.lerp(e1, e2, num2), e2, desiredRadius, false, ref buffers);
				return;
			}
			if (num > 0f)
			{
				PIDMovement.SplitSegment3(e1, math.lerp(e1, e2, num), desiredRadius, false, ref buffers);
				PIDMovement.SplitSegment3(math.lerp(e1, e2, num), e2, desiredRadius, true, ref buffers);
				return;
			}
			if (num2 < 1f)
			{
				PIDMovement.SplitSegment3(e1, math.lerp(e1, e2, num2), desiredRadius, true, ref buffers);
				PIDMovement.SplitSegment3(math.lerp(e1, e2, num2), e2, desiredRadius, false, ref buffers);
				return;
			}
			PIDMovement.SplitSegment3(e1, e2, desiredRadius, true, ref buffers);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00055300 File Offset: 0x00053500
		private static void SplitSegment3(float2 e1, float2 e2, float desiredRadius, bool inTriangularRegion, ref PIDMovement.EdgeBuffers buffers)
		{
			float2 @float = e1;
			float2 float2 = e2;
			if (float2.x < @float.x)
			{
				@float.y -= 0.01f;
				float2.y -= 0.01f;
			}
			else
			{
				@float.y += 0.01f;
				float2.y += 0.01f;
			}
			bool flag = @float.y > 0f;
			if (!flag)
			{
				Memory.Swap<float2>(ref e1, ref e2);
				Memory.Swap<float2>(ref @float, ref float2);
			}
			float num = math.unlerp(@float.y, float2.y, 0f);
			bool flag2 = math.isfinite(num);
			if (num <= 0f || num >= 1f || !flag2)
			{
				PIDMovement.SplitSegment4(e1, e2, inTriangularRegion, flag, ref buffers);
				return;
			}
			float2 float3 = e1 + num * (e2 - e1);
			float num2 = math.lengthsq(e1 - float3);
			float num3 = math.lengthsq(e2 - float3);
			float num4 = desiredRadius * 0.1f;
			float num5 = num4 * num4;
			if (num2 > num5 || num2 >= num3)
			{
				PIDMovement.SplitSegment4(e1, float3, inTriangularRegion, true, ref buffers);
			}
			if (num3 > num5 || num3 >= num2)
			{
				PIDMovement.SplitSegment4(float3, e2, inTriangularRegion, false, ref buffers);
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00055434 File Offset: 0x00053634
		private static void SplitSegment4(float2 e1, float2 e2, bool inTriangularRegion, bool left, ref PIDMovement.EdgeBuffers buffers)
		{
			if (math.all(math.abs(e1 - e2) < 0.01f))
			{
				return;
			}
			ref FixedList512Bytes<float2> ptr = ref buffers.triangleRegionEdgesL;
			if (inTriangularRegion)
			{
				if (!left)
				{
					ptr = ref buffers.triangleRegionEdgesR;
				}
			}
			else if (left)
			{
				ptr = ref buffers.straightRegionEdgesL;
			}
			else
			{
				ptr = ref buffers.straightRegionEdgesR;
			}
			if (ptr.Length + 2 > ptr.Capacity)
			{
				return;
			}
			ptr.AddNoResize(in e1);
			ptr.AddNoResize(in e2);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000554B0 File Offset: 0x000536B0
		public static float2 OptimizeDirection(float2 start, float2 end, float desiredRadius, float remainingDistance, float pointiness, NativeArray<float2> edges, CommandBuilder draw, PIDMovement.DebugFlags debugFlags)
		{
			float num = math.length(end - start);
			float2 @float = math.normalizesafe(end - start, default(float2));
			num *= 0.999f;
			num = math.min(0.9f * remainingDistance, num);
			if (desiredRadius <= 0.0001f)
			{
				return @float;
			}
			float num2 = num;
			float num3 = 1f / num2;
			PIDMovement.EdgeBuffers edgeBuffers = default(PIDMovement.EdgeBuffers);
			for (int i = 0; i < edges.Length; i += 2)
			{
				float2 float2 = VectorMath.ComplexMultiplyConjugate(edges[i] - start, @float);
				float2 float3 = VectorMath.ComplexMultiplyConjugate(edges[i + 1] - start, @float);
				PIDMovement.SplitSegment(float2, float3, desiredRadius, num, pointiness, ref edgeBuffers);
			}
			float2 float4 = new float2(1f, 0f);
			for (int j = 0; j < 8; j++)
			{
				if ((debugFlags & PIDMovement.DebugFlags.ForwardClearance) != PIDMovement.DebugFlags.Nothing)
				{
					Color blue = Palette.Colorbrewer.Set1.Blue;
					blue.a = 0.5f;
					float2 float5 = VectorMath.ComplexMultiply(float4, @float);
					PIDMovement.DrawChisel(start, float5, pointiness, num, desiredRadius, draw, blue);
					draw.xz.Ray(start, float5 * num, Palette.Colorbrewer.Set1.Purple);
					draw.xz.Circle(start, remainingDistance, blue);
				}
				float2 float6 = new float2(0f, desiredRadius);
				float2 float7 = new float2(0f, -desiredRadius);
				float2 float8 = new float2(num, 0f);
				float2 float9 = new float2(num, 0f);
				for (int k = 0; k < edgeBuffers.straightRegionEdgesL.Length; k += 2)
				{
					float2 float10 = VectorMath.ComplexMultiplyConjugate(edgeBuffers.straightRegionEdgesL[k], float4);
					float2 float11 = VectorMath.ComplexMultiplyConjugate(edgeBuffers.straightRegionEdgesL[k + 1], float4);
					float8 = PIDMovement.MaxAngle(float8, float10 - float6, float11 - float6, true);
				}
				for (int l = 0; l < edgeBuffers.straightRegionEdgesR.Length; l += 2)
				{
					float2 float12 = VectorMath.ComplexMultiplyConjugate(edgeBuffers.straightRegionEdgesR[l], float4);
					float2 float13 = VectorMath.ComplexMultiplyConjugate(edgeBuffers.straightRegionEdgesR[l + 1], float4);
					float9 = PIDMovement.MaxAngle(float9, float12 - float7, float13 - float7, false);
				}
				float2 float14 = math.normalizesafe(VectorMath.ComplexMultiply(new float2(pointiness * desiredRadius, desiredRadius), float4), default(float2));
				float2 float15 = math.normalizesafe(VectorMath.ComplexMultiply(new float2(pointiness * desiredRadius, -desiredRadius), float4), default(float2));
				for (int m = 0; m < edgeBuffers.triangleRegionEdgesL.Length; m += 2)
				{
					float2 float16 = VectorMath.ComplexMultiplyConjugate(edgeBuffers.triangleRegionEdgesL[m], float14);
					float2 float17 = VectorMath.ComplexMultiplyConjugate(edgeBuffers.triangleRegionEdgesL[m + 1], float14);
					float2 float18 = ((float17.y < float16.y) ? float17 : float16);
					if (float18.y < 0f)
					{
						float8 = PIDMovement.MaxAngle(float8, float18, true);
					}
				}
				for (int n = 0; n < edgeBuffers.triangleRegionEdgesR.Length; n += 2)
				{
					float2 float19 = VectorMath.ComplexMultiplyConjugate(edgeBuffers.triangleRegionEdgesR[n], float15);
					float2 float20 = VectorMath.ComplexMultiplyConjugate(edgeBuffers.triangleRegionEdgesR[n + 1], float15);
					float2 float21 = ((float20.y > float19.y) ? float20 : float19);
					if (float21.y > 0f)
					{
						float9 = PIDMovement.MaxAngle(float9, float21, false);
					}
				}
				float num4 = 1f / math.max(1E-06f, num2 - float8.x * float8.x) - num3;
				float num5 = 1f / math.max(1E-06f, num2 - float9.x * float9.x) - num3;
				float2 float22 = math.normalizesafe(float8 * num5 + float9 * num4, default(float2));
				float2 float23 = math.lerp(new float2(1f, 0f), float22, 1f);
				float4 = math.normalizesafe(VectorMath.ComplexMultiply(float4, float23), default(float2));
				if (float8.y == 0f && float9.y == 0f)
				{
					num = math.min(remainingDistance * 0.9f, math.min(num * 1.1f, num2 * 1.2f));
				}
				else
				{
					num = math.min(num, math.max(desiredRadius * 2f, math.min(float8.x, float9.x) * 2f));
				}
			}
			float4 = VectorMath.ComplexMultiply(float4, @float);
			if ((debugFlags & PIDMovement.DebugFlags.ForwardClearance) != PIDMovement.DebugFlags.Nothing)
			{
				PIDMovement.DrawChisel(start, float4, pointiness, num, desiredRadius, draw, Color.black);
			}
			return float4;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0005596C File Offset: 0x00053B6C
		public static float SmallestDistanceWithinWedge(float2 point, float2 dir1, float2 dir2, float shrinkAmount, NativeArray<float2> edges)
		{
			dir1 = math.normalizesafe(dir1, default(float2));
			dir2 = math.normalizesafe(dir2, default(float2));
			if (math.dot(dir1, dir2) > 0.999f)
			{
				return float.PositiveInfinity;
			}
			float num = math.sign(VectorMath.Determinant(dir1, dir2));
			shrinkAmount *= num;
			float num2 = float.PositiveInfinity;
			for (int i = 0; i < edges.Length; i += 2)
			{
				float2 @float = edges[i] - point;
				float2 float2 = edges[i + 1] - point;
				float2 float3 = VectorMath.ComplexMultiplyConjugate(@float, dir1);
				float2 float4 = VectorMath.ComplexMultiplyConjugate(float2, dir1);
				float2 float5 = VectorMath.ComplexMultiplyConjugate(@float, dir2);
				float2 float6 = VectorMath.ComplexMultiplyConjugate(float2, dir2);
				float num3 = 0f;
				float num4 = 1f;
				PIDMovement.ClipLineByHalfPlaneYt(float3, float4, shrinkAmount, num, ref num3, ref num4);
				if (num3 <= num4)
				{
					PIDMovement.ClipLineByHalfPlaneYt(float5, float6, -shrinkAmount, -num, ref num3, ref num4);
					if (num3 <= num4)
					{
						float num5 = math.lengthsq(float2 - @float);
						float num6 = math.clamp(math.dot(@float, @float - float2) * math.rcp(num5), num3, num4);
						float num7 = math.lengthsq(math.lerp(@float, float2, num6));
						num2 = math.select(num2, math.min(num2, num7), num5 > 1.1754944E-38f);
					}
				}
			}
			return math.sqrt(num2);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00055AC4 File Offset: 0x00053CC4
		public static float2 Linecast(float2 a, float2 b, NativeArray<float2> edges)
		{
			float num = 1f;
			for (int i = 0; i < edges.Length; i += 2)
			{
				float2 @float = edges[i];
				float2 float2 = edges[i + 1];
				float num2;
				float num3;
				VectorMath.LineLineIntersectionFactors(a, b - a, @float, float2 - @float, out num2, out num3);
				if (num3 >= 0f && num3 <= 1f && num2 > 0f)
				{
					num = math.min(num, num2);
				}
			}
			return a + (b - a) * num;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00055B50 File Offset: 0x00053D50
		public static Bounds InterestingEdgeBounds(ref PIDMovement settings, float3 position, float3 nextCorner, float height, NativeMovementPlane plane)
		{
			float3 @float = math.mul(math.conjugate(plane.rotation), position);
			float3 float2 = math.mul(math.conjugate(plane.rotation), nextCorner);
			Bounds bounds = new Bounds(@float + new float3(0f, height * 0.25f, 0f), new Vector3(0f, 1.5f * height, 0f));
			float2.y = @float.y;
			bounds.Encapsulate(float2);
			if (settings.rotationSpeed > 0f)
			{
				float num = settings.speed / math.radians(settings.rotationSpeed);
				bounds.Expand(new Vector3(1f, 0f, 1f) * math.max(num, settings.desiredWallDistance * 8f * 1f));
			}
			return bounds;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00055C34 File Offset: 0x00053E34
		private static float2 OffsetCornerForApproach(float2 position2D, float2 endOfPath2D, float2 facingDir2D, ref PIDMovement settings, float2 nextCorner2D, ref float gammaAngle, ref float gammaAngleWeight, PIDMovement.DebugFlags debugFlags, ref CommandBuilder draw, NativeArray<float2> edges)
		{
			float2 @float = endOfPath2D - position2D;
			if (math.dot(math.normalizesafe(@float, default(float2)), facingDir2D) < -0.2f)
			{
				return nextCorner2D;
			}
			float2 float2 = new float2(-@float.y, @float.x);
			float2 float3 = new float2(-facingDir2D.y, facingDir2D.x);
			float2 float4 = (position2D + endOfPath2D) * 0.5f;
			bool flag;
			float2 float5 = VectorMath.LineIntersectionPoint(float4, float4 + float2, endOfPath2D, endOfPath2D + float3, out flag);
			if (!flag)
			{
				return nextCorner2D;
			}
			float num = PIDMovement.SmallestDistanceWithinWedge(endOfPath2D - 0.01f * facingDir2D, float3 - 0.1f * facingDir2D, -float3 - 0.1f * facingDir2D, 0.001f, edges);
			float num2 = settings.leadInRadiusWhenApproachingDestination;
			num2 = math.min(num2, num * 0.9f);
			float num3 = math.length(float5 - endOfPath2D);
			float num4 = math.abs(math.dot(math.normalizesafe(@float, default(float2)), float3));
			float num5 = 1f / math.sqrt(1f - num4 * num4) * math.length(@float) * 0.5f;
			num5 /= math.min(num2, num3);
			num5 = math.tanh(num5);
			num5 *= math.min(num2, num3);
			float2 float6 = nextCorner2D - facingDir2D * num5;
			if ((debugFlags & PIDMovement.DebugFlags.ApproachWithOrientation) != PIDMovement.DebugFlags.Nothing)
			{
				draw.xz.Circle(float5, num3, Color.blue);
				draw.xz.Arrow(position2D, float6, Palette.Colorbrewer.Set1.Orange);
			}
			if (math.lengthsq(PIDMovement.Linecast(position2D, float6, edges) - float6) > 0.01f)
			{
				return nextCorner2D;
			}
			return float6;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00055E20 File Offset: 0x00054020
		public static AnglePIDControlOutput2D Control(ref PIDMovement settings, float dt, ref PIDMovement.ControlParams controlParams, ref CommandBuilder draw, out float maxDesiredWallDistance)
		{
			if (dt <= 0f)
			{
				maxDesiredWallDistance = controlParams.maxDesiredWallDistance;
				AnglePIDControlOutput2D anglePIDControlOutput2D = new AnglePIDControlOutput2D
				{
					rotationDelta = 0f,
					positionDelta = float2.zero
				};
				return anglePIDControlOutput2D;
			}
			NativeMovementPlane movementPlane = controlParams.movementPlane;
			float num;
			float2 @float = movementPlane.ToPlane(controlParams.p, out num);
			if (controlParams.debugFlags != PIDMovement.DebugFlags.Nothing)
			{
				draw.PushMatrix(math.mul(new float4x4(movementPlane.rotation, float3.zero), float4x4.Translate(new float3(0f, num, 0f))));
			}
			if ((controlParams.debugFlags & PIDMovement.DebugFlags.Position) != PIDMovement.DebugFlags.Nothing)
			{
				draw.xz.Cross(controlParams.closestOnNavmesh, 0.05f, Color.red);
			}
			NativeArray<float2> edges = controlParams.edges;
			if ((controlParams.debugFlags & PIDMovement.DebugFlags.Obstacles) != PIDMovement.DebugFlags.Nothing)
			{
				draw.PushLineWidth(2f, true);
				draw.PushColor(Color.red);
				for (int i = 0; i < edges.Length; i += 2)
				{
					draw.xz.Line(edges[i], edges[i + 1]);
				}
				draw.PopColor();
				draw.PopLineWidth();
			}
			float2 float2 = movementPlane.ToPlane(controlParams.nextCorner);
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = controlParams.rotation + 1.5707964f;
			float2 float3 = math.normalizesafe(movementPlane.ToPlane(controlParams.facingDirectionAtEndOfPath), default(float2));
			bool flag = controlParams.remainingDistance < controlParams.agentRadius * 0.1f;
			if (!flag && settings.leadInRadiusWhenApproachingDestination > 0f && math.any(float3 != 0f))
			{
				float2 float4 = movementPlane.ToPlane(controlParams.endOfPath);
				if (math.lengthsq(float4 - float2) <= 0.1f)
				{
					float2 float5 = PIDMovement.OffsetCornerForApproach(@float, float4, float3, ref settings, float2, ref num3, ref num4, controlParams.debugFlags, ref draw, edges);
					float2 = float5;
					float num6 = settings.speed * 0.1f;
					if (num6 > 0.001f)
					{
						float num7;
						float num8;
						math.sincos(num5, out num7, out num8);
						float2 float6 = new float2(num8, num7);
						float2 float7 = PIDMovement.OffsetCornerForApproach(@float + float6 * num6, float4, float3, ref settings, float2, ref num3, ref num4, PIDMovement.DebugFlags.Nothing, ref draw, edges);
						num2 = math.asin(VectorMath.Determinant(math.normalizesafe(float5 - @float, default(float2)), math.normalizesafe(float7 - @float, default(float2)))) / num6;
					}
				}
			}
			float num9 = settings.desiredWallDistance;
			num9 = math.max(0f, math.min(num9, (controlParams.remainingDistance - num9) / 4f));
			float2 = PIDMovement.Linecast(@float, float2, edges);
			float2 float8 = PIDMovement.OptimizeDirection(@float, float2, num9, controlParams.remainingDistance, 2f, edges, draw, controlParams.debugFlags);
			maxDesiredWallDistance = controlParams.maxDesiredWallDistance + settings.speed * 0.1f * dt;
			float num10 = maxDesiredWallDistance;
			float num11 = 0f;
			float num12 = 0f;
			maxDesiredWallDistance = math.min(maxDesiredWallDistance, num10);
			if ((controlParams.debugFlags & PIDMovement.DebugFlags.Tangent) != PIDMovement.DebugFlags.Nothing)
			{
				draw.Arrow(controlParams.p, controlParams.p + new Vector3(float8.x, 0f, float8.y), Palette.Colorbrewer.Set1.Orange);
			}
			AnglePIDControlOutput2D anglePIDControlOutput2D2;
			if (flag)
			{
				float num13 = math.min(settings.Speed(controlParams.remainingDistance), settings.Accelerate(controlParams.speed, settings.slowdownTime, dt));
				float2 float9 = float2 - @float;
				float num14 = math.length(float9);
				if (math.any(float3 != 0f))
				{
					float num15 = math.atan2(float3.y, float3.x);
					float num16 = dt * math.radians(settings.maxRotationSpeed);
					AnglePIDControlOutput2D anglePIDControlOutput2D = new AnglePIDControlOutput2D
					{
						rotationDelta = math.clamp(AstarMath.DeltaAngle(num5, num15), -num16, num16),
						targetRotation = num15 - 1.5707964f,
						positionDelta = ((num14 > 1.1754944E-38f) ? (float9 * (dt * num13 / num14)) : float9)
					};
					anglePIDControlOutput2D2 = anglePIDControlOutput2D;
				}
				else
				{
					AnglePIDControlOutput2D anglePIDControlOutput2D = new AnglePIDControlOutput2D
					{
						rotationDelta = 0f,
						targetRotation = num5 - 1.5707964f,
						positionDelta = ((num14 > 1.1754944E-38f) ? (float9 * (dt * num13 / num14)) : float9)
					};
					anglePIDControlOutput2D2 = anglePIDControlOutput2D;
				}
			}
			else
			{
				float num17 = settings.CurveFollowingStrength(num12, num10, controlParams.remainingDistance);
				float num18 = math.atan2(float8.y, float8.x);
				float num19 = 0f;
				if (math.abs(AstarMath.DeltaAngle(num18, num5)) > 0.003141593f)
				{
					float num20;
					float num21;
					math.sincos(num5, out num20, out num21);
					float2 float10 = new float2(num21, num20);
					float num22 = PIDMovement.SmallestDistanceWithinWedge(@float, float8, float10, controlParams.agentRadius * 0.1f, edges);
					if ((controlParams.debugFlags & PIDMovement.DebugFlags.ForwardClearance) != PIDMovement.DebugFlags.Nothing && float.IsFinite(num22))
					{
						draw.xz.Arc(@float, @float + float10 * num22, @float + float8, Palette.Colorbrewer.Set1.Purple);
					}
					if (num22 > 0.001f && num22 * 1.01f < controlParams.remainingDistance)
					{
						num19 = math.rcp(num22) * 2f;
					}
				}
				anglePIDControlOutput2D2 = AnglePIDController.Control(ref settings, num17, num5, num18 + AstarMath.DeltaAngle(num18, num3) * num4, num2, num11, controlParams.speed, controlParams.remainingDistance, num19, controlParams.speed < settings.speed * 0.1f, dt);
				anglePIDControlOutput2D2.targetRotation -= 1.5707964f;
			}
			if (controlParams.debugFlags != PIDMovement.DebugFlags.Nothing)
			{
				draw.PopMatrix();
			}
			return anglePIDControlOutput2D2;
		}

		// Token: 0x04000A77 RID: 2679
		public float rotationSpeed;

		// Token: 0x04000A78 RID: 2680
		public float speed;

		// Token: 0x04000A79 RID: 2681
		public float maxRotationSpeed;

		// Token: 0x04000A7A RID: 2682
		public float maxOnSpotRotationSpeed;

		// Token: 0x04000A7B RID: 2683
		public float slowdownTime;

		// Token: 0x04000A7C RID: 2684
		public float slowdownTimeWhenTurningOnSpot;

		// Token: 0x04000A7D RID: 2685
		public float desiredWallDistance;

		// Token: 0x04000A7E RID: 2686
		public float leadInRadiusWhenApproachingDestination;

		// Token: 0x04000A7F RID: 2687
		[SerializeField]
		private byte allowRotatingOnSpotBacking;

		// Token: 0x04000A80 RID: 2688
		public const float DESTINATION_CLEARANCE_FACTOR = 4f;

		// Token: 0x04000A81 RID: 2689
		private static readonly ProfilerMarker MarkerSidewaysAvoidance = new ProfilerMarker("SidewaysAvoidance");

		// Token: 0x04000A82 RID: 2690
		private static readonly ProfilerMarker MarkerPID = new ProfilerMarker("PID");

		// Token: 0x04000A83 RID: 2691
		private static readonly ProfilerMarker MarkerOptimizeDirection = new ProfilerMarker("OptimizeDirection");

		// Token: 0x04000A84 RID: 2692
		private static readonly ProfilerMarker MarkerSmallestDistance = new ProfilerMarker("ClosestDistance");

		// Token: 0x04000A85 RID: 2693
		private static readonly ProfilerMarker MarkerConvertObstacles = new ProfilerMarker("ConvertObstacles");

		// Token: 0x04000A86 RID: 2694
		private const float ALLOWED_OVERLAP_FACTOR = 0.1f;

		// Token: 0x04000A87 RID: 2695
		private const float STEP_MULTIPLIER = 1f;

		// Token: 0x04000A88 RID: 2696
		private const float MAX_FRACTION_OF_REMAINING_DISTANCE = 0.9f;

		// Token: 0x04000A89 RID: 2697
		private const int OPTIMIZATION_ITERATIONS = 8;

		// Token: 0x0200023D RID: 573
		public struct PersistentState
		{
			// Token: 0x04000A8A RID: 2698
			public float maxDesiredWallDistance;
		}

		// Token: 0x0200023E RID: 574
		[Flags]
		public enum DebugFlags
		{
			// Token: 0x04000A8C RID: 2700
			Nothing = 0,
			// Token: 0x04000A8D RID: 2701
			Position = 1,
			// Token: 0x04000A8E RID: 2702
			Tangent = 2,
			// Token: 0x04000A8F RID: 2703
			SidewaysClearance = 4,
			// Token: 0x04000A90 RID: 2704
			ForwardClearance = 8,
			// Token: 0x04000A91 RID: 2705
			Obstacles = 16,
			// Token: 0x04000A92 RID: 2706
			Funnel = 32,
			// Token: 0x04000A93 RID: 2707
			Path = 64,
			// Token: 0x04000A94 RID: 2708
			ApproachWithOrientation = 128,
			// Token: 0x04000A95 RID: 2709
			Rotation = 256
		}

		// Token: 0x0200023F RID: 575
		private struct EdgeBuffers
		{
			// Token: 0x04000A96 RID: 2710
			public FixedList512Bytes<float2> triangleRegionEdgesL;

			// Token: 0x04000A97 RID: 2711
			public FixedList512Bytes<float2> triangleRegionEdgesR;

			// Token: 0x04000A98 RID: 2712
			public FixedList512Bytes<float2> straightRegionEdgesL;

			// Token: 0x04000A99 RID: 2713
			public FixedList512Bytes<float2> straightRegionEdgesR;
		}

		// Token: 0x02000240 RID: 576
		public struct ControlParams
		{
			// Token: 0x04000A9A RID: 2714
			public Vector3 p;

			// Token: 0x04000A9B RID: 2715
			public float speed;

			// Token: 0x04000A9C RID: 2716
			public float rotation;

			// Token: 0x04000A9D RID: 2717
			public float maxDesiredWallDistance;

			// Token: 0x04000A9E RID: 2718
			public float3 endOfPath;

			// Token: 0x04000A9F RID: 2719
			public float3 facingDirectionAtEndOfPath;

			// Token: 0x04000AA0 RID: 2720
			public NativeArray<float2> edges;

			// Token: 0x04000AA1 RID: 2721
			public float3 nextCorner;

			// Token: 0x04000AA2 RID: 2722
			public float agentRadius;

			// Token: 0x04000AA3 RID: 2723
			public float remainingDistance;

			// Token: 0x04000AA4 RID: 2724
			public float3 closestOnNavmesh;

			// Token: 0x04000AA5 RID: 2725
			public PIDMovement.DebugFlags debugFlags;

			// Token: 0x04000AA6 RID: 2726
			public NativeMovementPlane movementPlane;
		}
	}
}
