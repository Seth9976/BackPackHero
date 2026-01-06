using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000020 RID: 32
public class CopySprite : MonoBehaviour
{
	// Token: 0x06000102 RID: 258 RVA: 0x0000670D File Offset: 0x0000490D
	private void Update()
	{
		this.image.sprite = this.imageToCopy.sprite;
	}

	// Token: 0x040000BF RID: 191
	[SerializeField]
	private Image image;

	// Token: 0x040000C0 RID: 192
	[SerializeField]
	private Image imageToCopy;
}
