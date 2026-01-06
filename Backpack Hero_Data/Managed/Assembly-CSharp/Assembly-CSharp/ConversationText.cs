using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FB RID: 251
public class ConversationText : MonoBehaviour
{
	// Token: 0x060008B8 RID: 2232 RVA: 0x0005BF8C File Offset: 0x0005A18C
	private void Start()
	{
		this.master.SetParent(GameObject.FindGameObjectWithTag("PrimaryCanvas").GetComponent<Canvas>().transform);
		this.master.SetAsFirstSibling();
		this.master.localScale = Vector3.one;
		base.transform.parent.localScale = Vector3.one;
		base.transform.localScale = Vector3.one;
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0005BFF8 File Offset: 0x0005A1F8
	private void Update()
	{
		if (!this.frog)
		{
			Object.Destroy(base.transform.parent.gameObject);
			return;
		}
		this.canvasGroup.alpha = this.frog.transform.localScale.x;
		this.master.position = this.positonForText.position;
	}

	// Token: 0x040006DE RID: 1758
	[SerializeField]
	private Image textBoxImage;

	// Token: 0x040006DF RID: 1759
	[SerializeField]
	private Transform master;

	// Token: 0x040006E0 RID: 1760
	[SerializeField]
	private Sprite[] sprites;

	// Token: 0x040006E1 RID: 1761
	[SerializeField]
	public Transform positonForText;

	// Token: 0x040006E2 RID: 1762
	[SerializeField]
	public GameObject frog;

	// Token: 0x040006E3 RID: 1763
	[SerializeField]
	private CanvasGroup canvasGroup;
}
