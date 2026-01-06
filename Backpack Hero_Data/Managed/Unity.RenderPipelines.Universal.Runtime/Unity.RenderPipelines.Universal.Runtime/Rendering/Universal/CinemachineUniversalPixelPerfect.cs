using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000006 RID: 6
	[AddComponentMenu("")]
	[MovedFrom("UnityEngine.Experimental.Rendering.Universal")]
	public class CinemachineUniversalPixelPerfect : MonoBehaviour
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002B1D File Offset: 0x00000D1D
		private void OnEnable()
		{
			Debug.LogError("CinemachineUniversalPixelPerfect is now deprecated and doesn't function properly. Instead, use the one from Cinemachine v2.4.0 or newer.");
		}
	}
}
