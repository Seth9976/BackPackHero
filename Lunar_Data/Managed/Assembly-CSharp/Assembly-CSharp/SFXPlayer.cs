using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class SFXPlayer : MonoBehaviour
{
	// Token: 0x060003B7 RID: 951 RVA: 0x0001268F File Offset: 0x0001088F
	public void PlaySFX(string name)
	{
		SoundManager.instance.PlaySFX(name, double.PositiveInfinity);
	}
}
