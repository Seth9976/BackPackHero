using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class AutoAchievement : MonoBehaviour
{
	// Token: 0x06000015 RID: 21 RVA: 0x000028EE File Offset: 0x00000AEE
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(5f);
		yield break;
	}
}
