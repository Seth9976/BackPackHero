using System;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class URLOpener : MonoBehaviour
{
	// Token: 0x06000481 RID: 1153 RVA: 0x0001634C File Offset: 0x0001454C
	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}
}
