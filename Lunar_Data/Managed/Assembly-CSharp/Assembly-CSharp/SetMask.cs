using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008E RID: 142
public class SetMask : MonoBehaviour
{
	// Token: 0x060003B5 RID: 949 RVA: 0x00012679 File Offset: 0x00010879
	private void Start()
	{
		this.mask.enabled = true;
	}

	// Token: 0x040002D5 RID: 725
	[SerializeField]
	private Mask mask;
}
