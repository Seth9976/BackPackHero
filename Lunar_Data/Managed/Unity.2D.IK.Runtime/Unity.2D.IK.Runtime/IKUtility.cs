using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.IK
{
	// Token: 0x02000006 RID: 6
	[MovedFrom("UnityEngine.Experimental.U2D.IK")]
	public class IKUtility
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDescendentOf(Transform transform, Transform ancestor)
		{
			Transform transform2 = transform.parent;
			while (transform2)
			{
				if (transform2 == ancestor)
				{
					return true;
				}
				transform2 = transform2.parent;
			}
			return false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A00 File Offset: 0x00000C00
		public static int GetAncestorCount(Transform transform)
		{
			int num = 0;
			while (transform.parent)
			{
				num++;
				transform = transform.parent;
			}
			return num;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A2C File Offset: 0x00000C2C
		public static int GetMaxChainCount(IKChain2D chain)
		{
			int num = 0;
			if (chain.effector)
			{
				num = IKUtility.GetAncestorCount(chain.effector) + 1;
			}
			return num;
		}
	}
}
