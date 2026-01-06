using System;
using TMPro;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class DestroyIfOffScreen : MonoBehaviour
{
	// Token: 0x060008D8 RID: 2264 RVA: 0x0005CAAE File Offset: 0x0005ACAE
	private void Start()
	{
		this.text = base.GetComponent<TextMeshProUGUI>();
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x0005CABC File Offset: 0x0005ACBC
	private void Update()
	{
		if (this.delay > 0f)
		{
			this.delay -= Time.deltaTime;
			return;
		}
		if (base.transform.position.y < -10f || base.transform.position.y > 10f)
		{
			Object.Destroy(base.gameObject);
		}
		if (this.text && this.text.alpha < 0.1f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040006FB RID: 1787
	private float delay = 0.2f;

	// Token: 0x040006FC RID: 1788
	private TextMeshProUGUI text;
}
