using System;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class PlaySFX : MonoBehaviour
{
	// Token: 0x06000310 RID: 784 RVA: 0x00012169 File Offset: 0x00010369
	public void Play()
	{
		SoundManager.main.PlaySFX(this.sfxName);
	}

	// Token: 0x04000217 RID: 535
	[SerializeField]
	private string sfxName;
}
