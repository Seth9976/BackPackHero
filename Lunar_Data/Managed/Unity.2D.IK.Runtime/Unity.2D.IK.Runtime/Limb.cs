using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.IK
{
	// Token: 0x0200000D RID: 13
	[MovedFrom("UnityEngine.Experimental.U2D.IK")]
	public static class Limb
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003630 File Offset: 0x00001830
		public static bool Solve(Vector3 targetPosition, float[] lengths, Vector3[] positions, ref float[] outAngles)
		{
			outAngles[0] = 0f;
			outAngles[1] = 0f;
			if (lengths[0] == 0f || lengths[1] == 0f)
			{
				return false;
			}
			Vector3 vector = targetPosition - positions[0];
			float magnitude = vector.magnitude;
			float sqrMagnitude = vector.sqrMagnitude;
			float num = lengths[0] * lengths[0];
			float num2 = lengths[1] * lengths[1];
			float num3 = (sqrMagnitude + num - num2) / (2f * lengths[0] * magnitude);
			float num4 = (sqrMagnitude - num - num2) / (2f * lengths[0] * lengths[1]);
			if (num3 >= -1f && num3 <= 1f && num4 >= -1f && num4 <= 1f)
			{
				outAngles[0] = Mathf.Acos(num3) * 57.29578f;
				outAngles[1] = Mathf.Acos(num4) * 57.29578f;
			}
			return true;
		}
	}
}
