using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

// Token: 0x02000109 RID: 265
public class LightFlicker : MonoBehaviour
{
	// Token: 0x06000919 RID: 2329 RVA: 0x0005EB88 File Offset: 0x0005CD88
	private void Start()
	{
		this.light2D = base.GetComponent<Light2D>();
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x0005EB98 File Offset: 0x0005CD98
	private void Update()
	{
		if (this.goingUp)
		{
			this.ct += Time.deltaTime;
		}
		else
		{
			this.ct -= Time.deltaTime;
		}
		if (this.ct > this.time)
		{
			this.ct = this.time;
			this.goingUp = false;
		}
		else if (this.ct <= 0f)
		{
			this.ct = 0f;
			this.goingUp = true;
		}
		base.transform.localRotation = Quaternion.Euler(Mathf.Lerp(this.flickAmountX.x, this.flickAmountX.y, this.ct / this.time), Mathf.Lerp(this.flickAmountY.x, this.flickAmountY.y, this.ct / this.time), 0f);
	}

	// Token: 0x0400072D RID: 1837
	private Light2D light2D;

	// Token: 0x0400072E RID: 1838
	[SerializeField]
	private Vector2 flickAmountX;

	// Token: 0x0400072F RID: 1839
	[SerializeField]
	private Vector2 flickAmountY;

	// Token: 0x04000730 RID: 1840
	[SerializeField]
	private float time = 1f;

	// Token: 0x04000731 RID: 1841
	private bool goingUp = true;

	// Token: 0x04000732 RID: 1842
	private float ct;
}
