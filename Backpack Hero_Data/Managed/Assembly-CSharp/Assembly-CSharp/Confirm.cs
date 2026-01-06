using System;
using TMPro;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class Confirm : MonoBehaviour
{
	// Token: 0x0600007E RID: 126 RVA: 0x000053BA File Offset: 0x000035BA
	private void Start()
	{
	}

	// Token: 0x0600007F RID: 127 RVA: 0x000053BC File Offset: 0x000035BC
	public void UpdateText(string x, string description)
	{
		LangaugeManager.main.SetFont(base.transform);
		this.text.text = x;
		this.descriptionText.transform.parent.gameObject.SetActive(true);
		this.descriptionText.text = description;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x0000540C File Offset: 0x0000360C
	public void UpdateText(string x)
	{
		this.descriptionText.transform.parent.gameObject.SetActive(false);
		LangaugeManager.main.SetFont(base.transform);
		this.text.text = x;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00005445 File Offset: 0x00003645
	public void AddCardToPanel(GameObject card)
	{
		this.cardArea.gameObject.SetActive(true);
		card.transform.SetParent(this.cardArea.transform);
		card.transform.localScale = Vector3.one;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0000547E File Offset: 0x0000367E
	public void ReceiveYes()
	{
		this.DelegateFunction();
		this.Close();
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00005492 File Offset: 0x00003692
	public void ReceiveNo()
	{
		this.Close();
	}

	// Token: 0x06000084 RID: 132 RVA: 0x0000549A File Offset: 0x0000369A
	private void Close()
	{
		base.GetComponent<SingleUI>().CloseAndDestroy();
	}

	// Token: 0x04000049 RID: 73
	[SerializeField]
	private TextMeshProUGUI text;

	// Token: 0x0400004A RID: 74
	[SerializeField]
	private TextMeshProUGUI descriptionText;

	// Token: 0x0400004B RID: 75
	[SerializeField]
	private GameObject cardArea;

	// Token: 0x0400004C RID: 76
	public Func<Action> DelegateFunction;
}
