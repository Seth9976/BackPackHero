using System;
using Unity.Mathematics;

namespace Pathfinding.PID
{
	// Token: 0x02000241 RID: 577
	public static class AnglePIDController
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x00056448 File Offset: 0x00054648
		public static float ApproximateTurningRadius(float followingStrength)
		{
			float num = 2f * math.sqrt(math.abs(followingStrength)) * 1f;
			return 1f / (num * 1.5707964f);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0005647C File Offset: 0x0005467C
		public static float RotationSpeedToFollowingStrength(float speed, float maxRotationSpeed)
		{
			float num = maxRotationSpeed / (6.2831855f * speed * 1f);
			return num * num;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0005648F File Offset: 0x0005468F
		public static float FollowingStrengthToRotationSpeed(float followingStrength)
		{
			return 1f / (AnglePIDController.ApproximateTurningRadius(followingStrength) * 0.5f);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000564A4 File Offset: 0x000546A4
		public static AnglePIDControlOutput2D Control(ref PIDMovement settings, float followingStrength, float angle, float curveAngle, float curveCurvature, float curveDistanceSigned, float speed, float remainingDistance, float minRotationSpeed, bool isStationary, float dt)
		{
			float num = 2f * math.sqrt(math.abs(followingStrength)) * 1f;
			float num2 = 1f;
			float num3 = AstarMath.DeltaAngle(angle, curveAngle);
			float num4 = curveAngle + math.sign(curveDistanceSigned) * 3.1415927f * 0.5f;
			float num5 = AstarMath.DeltaAngle(angle, num4);
			float num6 = followingStrength * math.abs(curveDistanceSigned) * num5;
			float num7 = num6 * speed * dt;
			float num8 = num * num3;
			float num9 = num + followingStrength * math.abs(curveDistanceSigned);
			float num10 = ((num9 > 1.1754944E-38f) ? ((num8 + num6) / num9) : 0f);
			float.IsFinite(num10);
			isStationary = settings.allowRotatingOnSpot && (math.abs(num10) > 2.0941856f || (isStationary && math.abs(num10) > 0.1f));
			if (!isStationary)
			{
				speed = math.min(settings.Speed(remainingDistance), settings.Accelerate(speed, settings.slowdownTime, dt));
				if (math.abs(num3) > 1.5707964f)
				{
					num7 = 0f;
				}
				if (math.abs(num8) > 0.0001f)
				{
					num8 = math.max(math.abs(num8), minRotationSpeed) * math.sign(num8);
				}
				float num11 = num8 * speed * dt;
				float num12 = math.abs(num7 / num5);
				float num13 = math.abs(num11 / num3);
				float num14 = 1f;
				float num15 = math.max(0f, math.cos(num3));
				float num16 = 1f;
				float num17 = speed * num16 * dt;
				float num18 = curveCurvature * num17;
				float num19 = num2 * num18 * num15;
				float num20 = math.max(1f, math.max(num12, math.max(num13, num14)));
				float num21 = (num19 + num11 + num7) / num20;
				float num22 = math.radians(settings.maxRotationSpeed);
				float num23 = math.max(0.1f, math.min(1f, num22 * dt / math.abs(num21)));
				return new AnglePIDControlOutput2D(angle, angle + num10, num21 * num23, num17 * num23);
			}
			float num24 = settings.Accelerate(speed, settings.slowdownTimeWhenTurningOnSpot, -dt);
			float num25 = math.radians(settings.maxOnSpotRotationSpeed);
			bool flag = num25 * dt > math.abs(num10);
			if (num24 > 0f && !flag)
			{
				return AnglePIDControlOutput2D.WithMovementAtEnd(angle, angle, 0f, num24 * dt);
			}
			return AnglePIDControlOutput2D.WithMovementAtEnd(angle, angle + num10, math.clamp(num10, -num25 * dt, num25 * dt), flag ? (speed * dt) : 0f);
		}

		// Token: 0x04000AA7 RID: 2727
		private const float DampingRatio = 1f;
	}
}
