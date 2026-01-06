using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000A1 RID: 161
public class AnalyticsMenuManager : MonoBehaviour
{
	// Token: 0x06000390 RID: 912 RVA: 0x00015029 File Offset: 0x00013229
	private void Start()
	{
	}

	// Token: 0x06000391 RID: 913 RVA: 0x0001502B File Offset: 0x0001322B
	private void Update()
	{
	}

	// Token: 0x06000392 RID: 914 RVA: 0x0001502D File Offset: 0x0001322D
	public void EnableAnalytics()
	{
		Singleton.Instance.analyticsActive = true;
		SceneManager.LoadScene("Early Access");
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00015044 File Offset: 0x00013244
	public void DontEnableAnalytics()
	{
		Singleton.Instance.analyticsActive = false;
		SceneManager.LoadScene("Early Access");
	}
}
