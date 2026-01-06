using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x02000119 RID: 281
public class SimpleClickItem : CustomInputHandler
{
	// Token: 0x06000985 RID: 2437 RVA: 0x00061660 File Offset: 0x0005F860
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		List<GameObject> list = null;
		bool flag = false;
		if (this.dungeonEvent)
		{
			flag = this.dungeonEvent.GetItems(out list);
		}
		foreach (SimpleClickItem.Effect effect in this.effects)
		{
			SimpleClickItem.Type type = effect.type;
			if (type == SimpleClickItem.Type.freeItem)
			{
				if (flag && list.Count > 0)
				{
					effect.assignedObject = list[0];
					list.RemoveAt(0);
				}
				else
				{
					effect.assignedObject = this.gameManager.SpawnItem(new List<Item2.ItemType>(), new List<Item2.Rarity> { Item2.Rarity.Legendary }, false, null);
				}
				effect.assignedObject.GetComponent<ItemMovement>().parentAltar = base.gameObject;
				effect.assignedObject.transform.position = base.transform.position + Vector3.back * 2f + Vector3.up * 0.25f;
			}
			else if (type == SimpleClickItem.Type.heal && effect.num < 0)
			{
				base.GetComponentInChildren<TextMeshPro>().text = LangaugeManager.main.GetTextByKey("e1").Replace("/x", Mathf.Abs(effect.num).ToString() ?? "");
			}
		}
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x000617F4 File Offset: 0x0005F9F4
	private void Update()
	{
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x000617F8 File Offset: 0x0005F9F8
	public void StoreItems()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (SimpleClickItem.Effect effect in this.effects)
		{
			if (effect.type == SimpleClickItem.Type.freeItem)
			{
				list.Add(effect.assignedObject);
			}
		}
		if (this.dungeonEvent)
		{
			this.dungeonEvent.StoreItems(list);
		}
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x00061878 File Offset: 0x0005FA78
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName == "confirm" || overrideKeyName)
		{
			this.Click();
		}
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0006188F File Offset: 0x0005FA8F
	private void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.Click();
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x000618A4 File Offset: 0x0005FAA4
	public void Click()
	{
		if (this.dungeonEvent && this.dungeonEvent.IsFinished())
		{
			return;
		}
		if (this.gameManager.travelling || this.isOpen)
		{
			return;
		}
		SoundManager.main.PlaySFX("use" + Random.Range(1, 3).ToString());
		DigitalInputSelectOnButton componentInParent = base.GetComponentInParent<DigitalInputSelectOnButton>();
		if (componentInParent)
		{
			componentInParent.RemoveSymbol();
		}
		Player main = Player.main;
		Animator componentInChildren = main.GetComponentInChildren<Animator>();
		if (!this.animationSpriteOverride)
		{
			main.itemToInteractWith.sprite = base.GetComponentInChildren<SpriteRenderer>().sprite;
		}
		else
		{
			main.itemToInteractWith.sprite = this.animationSpriteOverride;
		}
		if (this.effects.Where((SimpleClickItem.Effect x) => x.type == SimpleClickItem.Type.getBackpack).Count<SimpleClickItem.Effect>() > 0)
		{
			componentInChildren.speed = 0.08f;
			SoundManager.main.PlaySongSudden("backpack_get_fanfare", 0f, 0f, false);
			GameManager.main.startMusicOnMap = true;
			GameManager.main.StartCoroutine(SimpleClickItem.StopSong());
		}
		componentInChildren.Play("Player_UseItem");
		Animator componentInChildren2 = base.GetComponentInChildren<Animator>();
		if (componentInChildren2)
		{
			componentInChildren2.Play("dungeonEventDespawnAndDontDestroy", 0, 0f);
		}
		if (this.dungeonEvent)
		{
			this.dungeonEvent.FinishEvent();
		}
		base.StartCoroutine(this.TakeAction());
		this.isOpen = true;
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x00061A27 File Offset: 0x0005FC27
	private static IEnumerator StopSong()
	{
		yield return new WaitForSeconds(4.5f);
		yield break;
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x00061A2F File Offset: 0x0005FC2F
	private IEnumerator TakeAction()
	{
		Player player = Player.main;
		Animator animator = player.GetComponentInChildren<Animator>();
		if (this.effects.Where((SimpleClickItem.Effect x) => x.type == SimpleClickItem.Type.freeItem || x.type == SimpleClickItem.Type.createCurse).Count<SimpleClickItem.Effect>() > 0)
		{
			GameManager.main.ShowInventory();
		}
		yield return new WaitForSeconds(0.4f / animator.speed);
		if (this.effects.Where((SimpleClickItem.Effect x) => x.type == SimpleClickItem.Type.getBackpack).Count<SimpleClickItem.Effect>() > 0)
		{
			animator.speed = 1f;
		}
		using (List<SimpleClickItem.Effect>.Enumerator enumerator = this.effects.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SimpleClickItem.Effect effect = enumerator.Current;
				SimpleClickItem.Type type = effect.type;
				int num = effect.num;
				if (type == SimpleClickItem.Type.heal)
				{
					player.stats.ChangeHealth(num, null, false);
				}
				else if (type == SimpleClickItem.Type.xp)
				{
					SoundManager.main.PlaySFX("levelUp1");
					player.AddExperience(num, base.transform.position);
				}
				else if (type == SimpleClickItem.Type.gold)
				{
					this.gameManager.ChangeGold(num);
				}
				else if (type == SimpleClickItem.Type.createEventNextFloor)
				{
					Object.FindObjectOfType<DungeonSpawner>().AddDungeonProperty(this.dungeonProperty);
				}
				else if (type != SimpleClickItem.Type.freeItem)
				{
					if (type == SimpleClickItem.Type.getBackpack)
					{
						MetaProgressSaveManager.main.AddMetaProgressMarker(MetaProgressSaveManager.MetaProgressMarker.backpackCollected);
						player.SetCostumeToBackpack();
						this.gameManager.MoveInventoryDown();
					}
					else if (type == SimpleClickItem.Type.createCurse)
					{
						ItemSpawner.InstantiateItemsFree(ItemSpawner.GetItems(1, new List<Item2.ItemType> { Item2.ItemType.Curse }, false, true), true, base.transform.position);
					}
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x04000798 RID: 1944
	private GameManager gameManager;

	// Token: 0x04000799 RID: 1945
	private GameFlowManager gameFlowManager;

	// Token: 0x0400079A RID: 1946
	private bool isOpen;

	// Token: 0x0400079B RID: 1947
	[SerializeField]
	private Sprite animationSpriteOverride;

	// Token: 0x0400079C RID: 1948
	[SerializeField]
	private List<SimpleClickItem.Effect> effects;

	// Token: 0x0400079D RID: 1949
	[HideInInspector]
	public DungeonEvent dungeonEvent;

	// Token: 0x0400079E RID: 1950
	[SerializeField]
	private DungeonSpawner.DungeonProperty dungeonProperty;

	// Token: 0x0200038A RID: 906
	public enum Type
	{
		// Token: 0x04001549 RID: 5449
		heal,
		// Token: 0x0400154A RID: 5450
		xp,
		// Token: 0x0400154B RID: 5451
		gold,
		// Token: 0x0400154C RID: 5452
		createEventNextFloor,
		// Token: 0x0400154D RID: 5453
		freeItem,
		// Token: 0x0400154E RID: 5454
		getBackpack,
		// Token: 0x0400154F RID: 5455
		createCurse
	}

	// Token: 0x0200038B RID: 907
	[Serializable]
	private class Effect
	{
		// Token: 0x04001550 RID: 5456
		public SimpleClickItem.Type type;

		// Token: 0x04001551 RID: 5457
		public int num;

		// Token: 0x04001552 RID: 5458
		public GameObject assignedObject;
	}
}
