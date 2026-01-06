using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class PlayerStatsDisplay : MonoBehaviour
{
	// Token: 0x06000959 RID: 2393 RVA: 0x000608EF File Offset: 0x0005EAEF
	private void Start()
	{
		this.Setup(PlayerStatTracking.main.gamesSavedandLoaded, PlayerStatTracking.main.stats);
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x0006090B File Offset: 0x0005EB0B
	private void Update()
	{
		if (!this.eventBoxAnimator.gameObject.activeInHierarchy)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x0006092C File Offset: 0x0005EB2C
	private void Setup(List<string> savesAndLoads, List<PlayerStatTracking.Stat> stats)
	{
		foreach (string text in savesAndLoads)
		{
			TextMeshProUGUI textMeshProUGUI = this.gameSavesAndLoads;
			textMeshProUGUI.text = textMeshProUGUI.text + "\n" + text;
		}
		foreach (PlayerStatTracking.Stat stat in stats)
		{
			TextMeshProUGUI textMeshProUGUI2 = this.gameStats;
			textMeshProUGUI2.text = string.Concat(new string[]
			{
				textMeshProUGUI2.text,
				"\n",
				stat.name,
				": ",
				Mathf.Abs(stat.value).ToString()
			});
		}
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x00060A1C File Offset: 0x0005EC1C
	public void EndEvent()
	{
		if (this.finished)
		{
			return;
		}
		GameManager.main.viewingEvent = false;
		this.finished = true;
		this.eventBoxAnimator.Play("Out");
	}

	// Token: 0x04000776 RID: 1910
	[SerializeField]
	private TextMeshProUGUI gameSavesAndLoads;

	// Token: 0x04000777 RID: 1911
	[SerializeField]
	private TextMeshProUGUI gameStats;

	// Token: 0x04000778 RID: 1912
	private bool finished;

	// Token: 0x04000779 RID: 1913
	[SerializeField]
	private Animator eventBoxAnimator;
}
