using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class OpenURL : MonoBehaviour
{
	// Token: 0x0600026F RID: 623 RVA: 0x0000EC2D File Offset: 0x0000CE2D
	private void Start()
	{
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0000EC2F File Offset: 0x0000CE2F
	private void Update()
	{
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0000EC31 File Offset: 0x0000CE31
	public void OpenMyURL()
	{
		Application.OpenURL(this.url);
	}

	// Token: 0x0400019D RID: 413
	[SerializeField]
	private string url = "https://www.google.com";
}
