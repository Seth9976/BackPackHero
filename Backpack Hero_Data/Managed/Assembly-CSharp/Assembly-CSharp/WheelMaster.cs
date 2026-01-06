using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class WheelMaster : MonoBehaviour
{
	// Token: 0x06000622 RID: 1570 RVA: 0x0003C698 File Offset: 0x0003A898
	private void Start()
	{
		this.randomEventMaster = base.GetComponent<RandomEventMaster>();
		this.spinButtonText.text = "Spin - free";
		this.gameManager = GameManager.main;
		base.StartCoroutine(this.Setup());
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0003C6CE File Offset: 0x0003A8CE
	private IEnumerator Setup()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		IEnumerable<GameObject> enumerable = new List<GameObject>();
		List<Item2> list = this.randomEventMaster.dungeonEvent.GetItem2s();
		new List<GameObject>(enumerable);
		if (list.Count == 0)
		{
			list.AddRange(ItemSpawner.GetItems(2, new List<Item2.Rarity> { Item2.Rarity.Uncommon }, new List<Item2.ItemType> { Item2.ItemType.Any }, true, false));
			list.AddRange(ItemSpawner.GetItems(2, new List<Item2.Rarity> { Item2.Rarity.Rare }, new List<Item2.ItemType> { Item2.ItemType.Any }, true, false));
			list.AddRange(ItemSpawner.GetItems(1, new List<Item2.Rarity> { Item2.Rarity.Legendary }, new List<Item2.ItemType> { Item2.ItemType.Any }, true, false));
			list.AddRange(ItemSpawner.GetItems(1, new List<Item2.ItemType> { Item2.ItemType.Curse }, true, true));
			list = list.OrderBy((Item2 x) => Random.value).ToList<Item2>();
		}
		UICarvingIndicator[] componentsInChildren = base.GetComponentsInChildren<UICarvingIndicator>();
		int num = 0;
		foreach (UICarvingIndicator uicarvingIndicator in componentsInChildren)
		{
			if (num >= list.Count)
			{
				break;
			}
			uicarvingIndicator.Setup(list[num]);
			num++;
		}
		this.UpdateText();
		yield break;
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0003C6E0 File Offset: 0x0003A8E0
	private void Update()
	{
		if (this.time < 1f)
		{
			this.time += Time.deltaTime;
		}
		float num = 999f;
		Transform transform = null;
		foreach (object obj in this.wheelParent)
		{
			Transform transform2 = (Transform)obj;
			if (transform2.GetComponent<WheelSlice>())
			{
				float num2 = transform2.rotation.eulerAngles.z % 360f;
				float num3 = Mathf.Min(Mathf.Abs(0f - num2), Mathf.Abs(360f - num2));
				if (num3 < num)
				{
					transform = transform2;
					num = num3;
				}
			}
		}
		if (!this.lastClosest)
		{
			this.lastClosest = transform;
		}
		else if (transform != this.lastClosest && this.time > 0.12f)
		{
			SoundManager.main.PlaySFX("wheelSFX");
			this.time = 0f;
			this.lastClosest = transform;
		}
		if (Mathf.Abs(this.power) > 1f)
		{
			this.wheelParent.transform.rotation *= Quaternion.Euler(0f, 0f, this.power * Time.deltaTime);
			if (this.power > 0f)
			{
				this.power -= Mathf.Max(this.power / 1.5f, 100f) * Time.deltaTime;
			}
			else
			{
				this.power -= Mathf.Min(this.power / 1.5f, -100f) * Time.deltaTime;
			}
			if (this.power < 10f && num > 25f)
			{
				this.power = -50f;
				return;
			}
		}
		else if (this.spinning)
		{
			this.randomEventMaster.dungeonEvent.AddEventProperty(DungeonEvent.EventProperty.Type.cost, 3);
			this.UpdateText();
			if (transform)
			{
				WheelSlice component = transform.GetComponent<WheelSlice>();
				component.StartCoroutine(component.Flash());
			}
			this.spinning = false;
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0003C910 File Offset: 0x0003AB10
	public void SetAllWheeltemsToLayer(int layer)
	{
		WheelSlice[] array = Object.FindObjectsOfType<WheelSlice>();
		for (int i = 0; i < array.Length; i++)
		{
			SpriteRenderer componentInChildren = array[i].GetComponentInChildren<SpriteRenderer>();
			if (componentInChildren)
			{
				componentInChildren.sortingOrder = layer;
			}
		}
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0003C94C File Offset: 0x0003AB4C
	private void UpdateText()
	{
		int eventPropertyValue = this.randomEventMaster.dungeonEvent.GetEventPropertyValue(DungeonEvent.EventProperty.Type.cost);
		if (eventPropertyValue != -1)
		{
			this.spinButtonText.text = LangaugeManager.main.GetTextByKey("mini1").Replace("/x", eventPropertyValue.ToString() ?? "");
			return;
		}
		this.spinButtonText.text = LangaugeManager.main.GetTextByKey("mini2").Replace("/x", eventPropertyValue.ToString() ?? "");
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0003C9D8 File Offset: 0x0003ABD8
	public void Spin()
	{
		if (this.spinning)
		{
			return;
		}
		SoundManager.main.PlaySFX("menuBlip");
		if (this.randomEventMaster.dungeonEvent.GetEventPropertyValue(DungeonEvent.EventProperty.Type.cost) > 0)
		{
			if (this.gameManager.GetCurrentGold() < this.randomEventMaster.dungeonEvent.GetEventProperty(DungeonEvent.EventProperty.Type.cost).value)
			{
				this.gameManager.CreatePopUp(LangaugeManager.main.GetTextByKey("gm27"));
				return;
			}
			this.gameManager.ChangeGold(this.randomEventMaster.dungeonEvent.GetEventProperty(DungeonEvent.EventProperty.Type.cost).value * -1);
		}
		this.power = Random.Range(1000f, 2000f);
		this.spinning = true;
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0003CA90 File Offset: 0x0003AC90
	public void EndEvent()
	{
		if (this.spinning)
		{
			return;
		}
		if (this.randomEventMaster.finished)
		{
			return;
		}
		SoundManager.main.PlaySFX("menuBlip");
		this.gameManager.viewingEvent = false;
		this.randomEventMaster.finished = true;
		this.eventBoxAnimator.Play("Out");
		base.GetComponent<RandomEventMaster>().npc.isOpen = false;
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		this.gameManager.SetAllItemColliders(true);
		Item2[] componentsInChildren = base.GetComponentsInChildren<Item2>();
		List<GameObject> list = new List<GameObject>();
		foreach (Item2 item in componentsInChildren)
		{
			list.Add(item.gameObject);
		}
		this.randomEventMaster.dungeonEvent.StoreItems(list);
	}

	// Token: 0x040004ED RID: 1261
	[SerializeField]
	private Transform wheelParent;

	// Token: 0x040004EE RID: 1262
	private float power;

	// Token: 0x040004EF RID: 1263
	private bool spinning;

	// Token: 0x040004F0 RID: 1264
	[SerializeField]
	private Animator eventBoxAnimator;

	// Token: 0x040004F1 RID: 1265
	[SerializeField]
	private Transform pointer;

	// Token: 0x040004F2 RID: 1266
	[SerializeField]
	private Transform wheelSliceParent;

	// Token: 0x040004F3 RID: 1267
	[SerializeField]
	private TextMeshProUGUI spinButtonText;

	// Token: 0x040004F4 RID: 1268
	private RandomEventMaster randomEventMaster;

	// Token: 0x040004F5 RID: 1269
	private GameManager gameManager;

	// Token: 0x040004F6 RID: 1270
	private Transform lastClosest;

	// Token: 0x040004F7 RID: 1271
	private float time;
}
