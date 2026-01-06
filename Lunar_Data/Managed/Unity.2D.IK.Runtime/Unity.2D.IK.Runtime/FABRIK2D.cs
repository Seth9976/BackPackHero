using System;
using System.Collections.Generic;

namespace UnityEngine.U2D.IK
{
	// Token: 0x0200000C RID: 12
	public static class FABRIK2D
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000031F8 File Offset: 0x000013F8
		public static bool Solve(Vector2 targetPosition, int solverLimit, float tolerance, float[] lengths, ref Vector2[] positions)
		{
			int num = positions.Length - 1;
			int num2 = 0;
			float num3 = tolerance * tolerance;
			float num4 = (targetPosition - positions[num]).sqrMagnitude;
			Vector2 vector = positions[0];
			while (num4 > num3)
			{
				FABRIK2D.Forward(targetPosition, lengths, ref positions);
				FABRIK2D.Backward(vector, lengths, ref positions);
				num4 = (targetPosition - positions[num]).sqrMagnitude;
				if (++num2 >= solverLimit)
				{
					break;
				}
			}
			return num2 != 0;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003278 File Offset: 0x00001478
		public static bool SolveChain(int solverLimit, ref FABRIKChain2D[] chains)
		{
			if (FABRIK2D.ValidateChain(chains))
			{
				return false;
			}
			for (int i = 0; i < solverLimit; i++)
			{
				FABRIK2D.SolveForwardsChain(0, ref chains);
				if (!FABRIK2D.SolveBackwardsChain(0, ref chains))
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000032B0 File Offset: 0x000014B0
		private static bool ValidateChain(FABRIKChain2D[] chains)
		{
			foreach (FABRIKChain2D fabrikchain2D in chains)
			{
				if (fabrikchain2D.subChainIndices.Length == 0 && (fabrikchain2D.target - fabrikchain2D.last).sqrMagnitude > fabrikchain2D.sqrTolerance)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003304 File Offset: 0x00001504
		private static void SolveForwardsChain(int idx, ref FABRIKChain2D[] chains)
		{
			Vector2 vector = chains[idx].target;
			if (chains[idx].subChainIndices.Length != 0)
			{
				vector = Vector2.zero;
				for (int i = 0; i < chains[idx].subChainIndices.Length; i++)
				{
					int num = chains[idx].subChainIndices[i];
					FABRIK2D.SolveForwardsChain(num, ref chains);
					vector += chains[num].first;
				}
				vector /= (float)chains[idx].subChainIndices.Length;
			}
			FABRIK2D.Forward(vector, chains[idx].lengths, ref chains[idx].positions);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033B4 File Offset: 0x000015B4
		private static bool SolveBackwardsChain(int idx, ref FABRIKChain2D[] chains)
		{
			bool flag = false;
			FABRIK2D.Backward(chains[idx].origin, chains[idx].lengths, ref chains[idx].positions);
			for (int i = 0; i < chains[idx].subChainIndices.Length; i++)
			{
				int num = chains[idx].subChainIndices[i];
				chains[num].origin = chains[idx].last;
				flag |= FABRIK2D.SolveBackwardsChain(num, ref chains);
			}
			if (chains[idx].subChainIndices.Length == 0)
			{
				flag |= (chains[idx].target - chains[idx].last).sqrMagnitude > chains[idx].sqrTolerance;
			}
			return flag;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003488 File Offset: 0x00001688
		private static void Forward(Vector2 targetPosition, IList<float> lengths, ref Vector2[] positions)
		{
			int num = positions.Length - 1;
			positions[num] = targetPosition;
			for (int i = num - 1; i >= 0; i--)
			{
				Vector2 vector = positions[i + 1] - positions[i];
				float num2 = lengths[i] / vector.magnitude;
				Vector2 vector2 = (1f - num2) * positions[i + 1] + num2 * positions[i];
				positions[i] = vector2;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003510 File Offset: 0x00001710
		private static void Backward(Vector2 originPosition, IList<float> lengths, ref Vector2[] positions)
		{
			positions[0] = originPosition;
			int num = positions.Length - 1;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = positions[i + 1] - positions[i];
				float num2 = lengths[i] / vector.magnitude;
				Vector2 vector2 = (1f - num2) * positions[i] + num2 * positions[i + 1];
				positions[i + 1] = vector2;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003598 File Offset: 0x00001798
		private static Vector2 ValidateJoint(Vector2 endPosition, Vector2 startPosition, Vector2 right, float min, float max)
		{
			Vector2 vector = endPosition - startPosition;
			float num = Vector2.SignedAngle(right, vector);
			Vector2 vector2 = endPosition;
			if (num < min)
			{
				Quaternion quaternion = Quaternion.Euler(0f, 0f, min);
				vector2 = startPosition + quaternion * right * vector.magnitude;
			}
			else if (num > max)
			{
				Quaternion quaternion2 = Quaternion.Euler(0f, 0f, max);
				vector2 = startPosition + quaternion2 * right * vector.magnitude;
			}
			return vector2;
		}
	}
}
