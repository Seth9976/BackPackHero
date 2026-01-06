using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x0200011A RID: 282
public class SimpleHoverText : CustomInputHandler
{
	// Token: 0x0600098E RID: 2446 RVA: 0x00061A46 File Offset: 0x0005FC46
	private void Start()
	{
		this.timeToDisplayCard = 0f;
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00061A53 File Offset: 0x0005FC53
	private void Update()
	{
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x00061A55 File Offset: 0x0005FC55
	public void SetText(string x)
	{
		this.textToDisplay[0] = x;
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x00061A64 File Offset: 0x0005FC64
	public override void OnCursorHold()
	{
		if (SingleUI.IsViewingPopUp() && !base.transform.IsChildOf(SingleUI.GetPopUp().transform))
		{
			return;
		}
		if (this.text)
		{
			this.num = int.Parse(this.text.text);
		}
		this.timeToDisplayCard += Time.deltaTime;
		if (this.timeToDisplayCard > 0.3f && !this.previewCard)
		{
			this.previewCard = Object.Instantiate<GameObject>(this.cardPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("SecondaryCanvas").GetComponent<Canvas>().transform);
			Card component = this.previewCard.GetComponent<Card>();
			List<string> list = new List<string>();
			foreach (string text in this.textToDisplay)
			{
				if (text == "bd1")
				{
					int scratchPower = GameFlowManager.GetScratchPower(3);
					list.Add(LangaugeManager.main.GetTextByKey(text).Replace("/x", scratchPower.ToString() ?? "").Replace("/y", 1.ToString() ?? ""));
				}
				else if (text == "bd14")
				{
					list.Add(LangaugeManager.main.GetTextByKey(text).Replace("/x", 3.ToString() ?? "").Replace("/y", 1.ToString() ?? ""));
				}
				else if (text == "bd2")
				{
					int num = 3;
					if (Player.main.characterName == Character.CharacterName.Pochette)
					{
						num = 2;
					}
					list.Add(LangaugeManager.main.GetTextByKey(text).Replace("/x", (num + Item2.GetEffectValues(Item2.Effect.Type.ChangeCostOfReorganize)).ToString() ?? ""));
				}
				else if (text == "bd16")
				{
					int num2 = Object.FindObjectOfType<Tote>().CalculateClearTotalDamage();
					string text2 = LangaugeManager.main.GetTextByKey(text).Replace("/y", num2.ToString() ?? "");
					text2 = text2.Replace("/x", (1 + Item2.GetEffectValues(Item2.Effect.Type.ChangeCostOfClear)).ToString() ?? "");
					list.Add(text2);
				}
				else if (text == "se16dd")
				{
					string text3 = LangaugeManager.main.GetTextByKey(text).Replace("/x", CurseManager.Instance.CursesStored().ToString() ?? "");
					list.Add(text3);
				}
				else if (text == "se16ddgold")
				{
					string text4 = LangaugeManager.main.GetTextByKey(text).Replace("/x", GameManager.main.goldAmount.ToString() ?? "");
					list.Add(text4);
				}
				else if (text == "gm49")
				{
					int num3 = 1;
					foreach (Card card in Object.FindObjectsOfType<Card>())
					{
						if (card.forgeCount)
						{
							num3 = int.Parse(card.forgeCount.text);
						}
					}
					string text5 = LangaugeManager.main.GetTextByKey(text).Replace("/x", num3.ToString() ?? "");
					list.Add(text5);
				}
				else if (this.num == -999)
				{
					list.Add(LangaugeManager.main.GetTextByKey(text));
				}
				else
				{
					list.Add(LangaugeManager.main.GetTextByKey(text).Replace("/x", this.num.ToString() ?? ""));
				}
			}
			component.GetDescriptionsSimple(list, base.gameObject);
			if (this.cardSize != Vector2.zero)
			{
				this.previewCard.GetComponent<RectTransform>().sizeDelta = this.cardSize;
			}
			if (this.centered)
			{
				TextMeshProUGUI[] componentsInChildren = this.previewCard.GetComponentsInChildren<TextMeshProUGUI>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].alignment = TextAlignmentOptions.Center;
				}
			}
		}
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x00061EDC File Offset: 0x000600DC
	public override void OnCursorEnd()
	{
		this.RemoveCard();
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x00061EE4 File Offset: 0x000600E4
	private void RemoveCard()
	{
		if (this.previewCard)
		{
			Object.Destroy(this.previewCard);
		}
		this.timeToDisplayCard = 0f;
	}

	// Token: 0x0400079F RID: 1951
	private float timeToDisplayCard;

	// Token: 0x040007A0 RID: 1952
	[SerializeField]
	private GameObject cardPrefab;

	// Token: 0x040007A1 RID: 1953
	[SerializeField]
	private Vector2 cardSize;

	// Token: 0x040007A2 RID: 1954
	[SerializeField]
	private List<string> textToDisplay;

	// Token: 0x040007A3 RID: 1955
	private GameObject previewCard;

	// Token: 0x040007A4 RID: 1956
	[SerializeField]
	private bool centered;

	// Token: 0x040007A5 RID: 1957
	[SerializeField]
	public int num = -999;

	// Token: 0x040007A6 RID: 1958
	[SerializeField]
	private TextMeshProUGUI text;
}
