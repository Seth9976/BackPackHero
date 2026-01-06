using System;
using TMPro;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class ExplanationGroup : MonoBehaviour
{
	// Token: 0x06000182 RID: 386 RVA: 0x00009E03 File Offset: 0x00008003
	private void Start()
	{
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00009E05 File Offset: 0x00008005
	private void Update()
	{
	}

	// Token: 0x040000F8 RID: 248
	[SerializeField]
	public TextMeshProUGUI currentNumberText;

	// Token: 0x040000F9 RID: 249
	[SerializeField]
	public Animation currentNumberTextAnimation;

	// Token: 0x040000FA RID: 250
	[SerializeField]
	public TextMeshProUGUI explanationText;
}
