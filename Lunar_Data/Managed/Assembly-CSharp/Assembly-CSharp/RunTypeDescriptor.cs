using System;
using UnityEngine;

// Token: 0x02000089 RID: 137
public class RunTypeDescriptor : MonoBehaviour
{
	// Token: 0x060003A1 RID: 929 RVA: 0x00012158 File Offset: 0x00010358
	private void Start()
	{
		if (this.imagesParent.childCount == 0)
		{
			this.imagesParent.gameObject.SetActive(false);
		}
	}

	// Token: 0x040002CB RID: 715
	[SerializeField]
	public Transform imagesParent;
}
