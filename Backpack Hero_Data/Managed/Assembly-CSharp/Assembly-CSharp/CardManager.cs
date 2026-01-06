using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020000A7 RID: 167
public class CardManager : MonoBehaviour
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x060003DF RID: 991 RVA: 0x00027305 File Offset: 0x00025505
	public static CardManager main
	{
		get
		{
			return CardManager._instance;
		}
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0002730C File Offset: 0x0002550C
	private void Awake()
	{
		if (CardManager._instance != null && CardManager._instance != this)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		CardManager._instance = this;
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0002733A File Offset: 0x0002553A
	private void OnDestroy()
	{
		if (CardManager._instance == this)
		{
			CardManager._instance = null;
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0002734F File Offset: 0x0002554F
	private void Start()
	{
		if (LangaugeManager.main && LangaugeManager.main.loadedLanguage)
		{
			this.SetUpDescriptions();
		}
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00027370 File Offset: 0x00025570
	public void SetUpDescriptions()
	{
		this.stringsDict = new Dictionary<string, string>();
		foreach (string text in this.strings)
		{
			this.stringsDict.Add(LangaugeManager.main.GetTextByKey(text).ToLower(), LangaugeManager.main.GetTextByKey(text + "d"));
		}
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x000273F8 File Offset: 0x000255F8
	private void GetCurrentCardSiblingIndex()
	{
		List<RaycastResult> list = new List<RaycastResult>();
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position);
		EventSystem.current.RaycastAll(pointerEventData, list);
		foreach (RaycastResult raycastResult in list)
		{
			Card componentInParent = raycastResult.gameObject.GetComponentInParent<Card>();
			if (componentInParent)
			{
				HighlightText[] componentsInChildren = componentInParent.GetComponentsInChildren<HighlightText>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					string word = componentsInChildren[i].GetWord();
					if (word != "")
					{
						if (this.savedText != word)
						{
							this.CreateCard(word, base.gameObject);
							this.savedText = word;
						}
						return;
					}
				}
			}
		}
		this.DeleteCard();
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x00027500 File Offset: 0x00025700
	private void Update()
	{
		if (EventSystem.current)
		{
			this.GetCurrentCardSiblingIndex();
		}
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00027514 File Offset: 0x00025714
	public void DeleteCard()
	{
		this.savedText = "";
		if (this.currentCard)
		{
			Object.Destroy(this.currentCard);
		}
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00027539 File Offset: 0x00025739
	public void CreateCardIfNotAlready(string text, GameObject callingObject)
	{
		if (this.currentCard)
		{
			return;
		}
		this.CreateCard(text, callingObject);
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00027554 File Offset: 0x00025754
	public void CreateCard(string text, GameObject callingObject)
	{
		this.DeleteCard();
		text = text.ToLower();
		if (this.stringsDict.ContainsKey(text))
		{
			string text2 = this.stringsDict[text];
			this.currentCard = Object.Instantiate<GameObject>(this.simpleCardPrefab, new Vector3(0f, 999f, 0f), Quaternion.identity, base.transform);
			this.currentCard.GetComponent<Card>().GetDescriptionsSimple(new List<string> { text2.Replace("/x", "x") }, callingObject);
			return;
		}
	}

	// Token: 0x040002D0 RID: 720
	[SerializeField]
	private GameObject simpleCardPrefab;

	// Token: 0x040002D1 RID: 721
	private static CardManager _instance;

	// Token: 0x040002D2 RID: 722
	[SerializeField]
	public List<string> strings;

	// Token: 0x040002D3 RID: 723
	public Dictionary<string, string> stringsDict = new Dictionary<string, string>();

	// Token: 0x040002D4 RID: 724
	private string savedText = "";

	// Token: 0x040002D5 RID: 725
	private GameObject currentCard;
}
