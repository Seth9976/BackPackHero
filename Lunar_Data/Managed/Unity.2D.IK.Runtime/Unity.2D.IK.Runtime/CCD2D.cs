using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.IK
{
	// Token: 0x0200000A RID: 10
	[MovedFrom("UnityEngine.Experimental.U2D.IK")]
	public static class CCD2D
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000030B4 File Offset: 0x000012B4
		public static bool Solve(Vector3 targetPosition, Vector3 forward, int solverLimit, float tolerance, float velocity, ref Vector3[] positions)
		{
			int num = positions.Length - 1;
			int num2 = 0;
			float num3 = tolerance * tolerance;
			float num4 = (targetPosition - positions[num]).sqrMagnitude;
			while (num4 > num3)
			{
				CCD2D.DoIteration(targetPosition, forward, num, velocity, ref positions);
				num4 = (targetPosition - positions[num]).sqrMagnitude;
				if (++num2 >= solverLimit)
				{
					break;
				}
			}
			return num2 != 0;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003120 File Offset: 0x00001320
		private static void DoIteration(Vector3 targetPosition, Vector3 forward, int last, float velocity, ref Vector3[] positions)
		{
			for (int i = last - 1; i >= 0; i--)
			{
				Vector3 vector = targetPosition - positions[i];
				float num = Vector3.SignedAngle(positions[last] - positions[i], vector, forward);
				num = Mathf.Lerp(0f, num, velocity);
				Quaternion quaternion = Quaternion.AngleAxis(num, forward);
				for (int j = last; j > i; j--)
				{
					positions[j] = CCD2D.RotatePositionFrom(positions[j], positions[i], quaternion);
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000031B4 File Offset: 0x000013B4
		private static Vector3 RotatePositionFrom(Vector3 position, Vector3 pivot, Quaternion rotation)
		{
			Vector3 vector = position - pivot;
			vector = rotation * vector;
			return pivot + vector;
		}
	}
}
