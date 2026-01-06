using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class PetItem2 : MonoBehaviour
{
	// Token: 0x060002EE RID: 750 RVA: 0x00011296 File Offset: 0x0000F496
	private void Awake()
	{
		if (!PetItem2.allPets.Contains(this))
		{
			PetItem2.allPets.Add(this);
		}
	}

	// Token: 0x060002EF RID: 751 RVA: 0x000112B0 File Offset: 0x0000F4B0
	private void OnDestroy()
	{
		PetItem2.allPets.Remove(this);
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x000112BE File Offset: 0x0000F4BE
	private void Start()
	{
		this.MakeReferences();
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x000112C6 File Offset: 0x0000F4C6
	private void MakeReferences()
	{
		this.itemPouch = base.GetComponent<ItemPouch>();
		this.itemMovement = base.GetComponent<ItemMovement>();
		this.myItem = base.GetComponent<Item2>();
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x000112EC File Offset: 0x0000F4EC
	public static void RemoveAllPets()
	{
		PetItem2[] array = Object.FindObjectsOfType<PetItem2>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].RemoveCombatPet();
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x00011318 File Offset: 0x0000F518
	public void RemoveCombatPet()
	{
		if (!base.gameObject || !this.combatPet)
		{
			return;
		}
		if (this.setupCombatPet != null)
		{
			base.StopCoroutine(this.setupCombatPet);
		}
		if (this.petProxyPrefab)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.petProxyPrefab, this.combatPet.transform.position, Quaternion.identity);
			ItemAnimationProxy component = gameObject.GetComponent<ItemAnimationProxy>();
			component.CopySprite(this.combatPet.GetComponentInChildren<SpriteRenderer>());
			component.ChangeSettings(Vector3.one, Vector3.zero, Quaternion.identity, 0.4f);
			component.FlyTo(gameObject.transform.position, GameManager.main.player.transform.GetChild(0), new Vector3(-0.5f, 0.2f, 0f));
		}
		if (this.combatPet.stats)
		{
			this.myItem.RemoveModifiers(new List<Item2.Modifier.Length> { Item2.Modifier.Length.untilUnzombied }, -1);
		}
		Object.Destroy(this.combatPet.gameObject);
		this.combatPet = null;
		this.myItem.playerAnimation = Item2.PlayerAnimation.UseItem;
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x00011442 File Offset: 0x0000F642
	public static Status GetStatus(GameObject item)
	{
		return PetItem2.GetStatus(item.GetComponent<PetItem2>());
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00011450 File Offset: 0x0000F650
	public static Status GetStatus(PetItem2 pet)
	{
		if (pet == null)
		{
			return Player.main.stats;
		}
		if (pet.combatPet == null || pet.combatPet.stats == null)
		{
			return null;
		}
		return pet.combatPet.stats;
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x000114A0 File Offset: 0x0000F6A0
	public static void SetAllPetAP()
	{
		foreach (PetItem2 petItem in PetItem2.allPets)
		{
			petItem.SetAP(petItem.APperTurn);
		}
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x000114F8 File Offset: 0x0000F6F8
	public static void HealAllPets()
	{
		foreach (PetItem2 petItem in PetItem2.allPets)
		{
			petItem.health = petItem.maxHealth;
		}
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00011550 File Offset: 0x0000F750
	public void SetAP(int amount)
	{
		this.currentAP = amount;
		if (this.combatPet)
		{
			this.combatPet.ShowAP();
		}
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00011574 File Offset: 0x0000F774
	public void ChangeAP(int amount)
	{
		int num = this.currentAP;
		this.currentAP += amount;
		this.currentAP = Mathf.Clamp(this.currentAP, 0, 9999);
		if (this.combatPet && num != this.currentAP)
		{
			this.combatPet.ChangeAP(amount);
		}
	}

	// Token: 0x060002FA RID: 762 RVA: 0x000115D0 File Offset: 0x0000F7D0
	public static void ReviveAllDefeatedPets()
	{
		foreach (PetItem2 petItem in PetItem2.allPets)
		{
			petItem.ReviveDefeatedPet();
		}
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00011620 File Offset: 0x0000F820
	public void ReviveDefeatedPet()
	{
		if (this.health <= 0)
		{
			this.health = 1;
		}
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00011634 File Offset: 0x0000F834
	public void SummonPet()
	{
		if (this.combatPet != null)
		{
			return;
		}
		Player player = GameManager.main.player;
		GameObject gameObject = Object.Instantiate<GameObject>(this.combatPetPrefab, Vector3.zero, Quaternion.identity, player.transform.parent);
		this.combatPet = gameObject.GetComponent<CombatPet>();
		this.combatPet.transform.position = new Vector3(player.transform.position.x, this.combatPetPrefab.transform.position.y, player.transform.position.z);
		this.combatPet.petItem2 = this;
		GameObject gameObject2;
		if (this.statusPrefab)
		{
			gameObject2 = Object.Instantiate<GameObject>(this.statusPrefab, new Vector3(player.transform.position.x, this.combatPetPrefab.transform.position.y, player.transform.position.z), Quaternion.identity);
		}
		else
		{
			gameObject2 = Object.Instantiate<GameObject>(Object.FindObjectOfType<Status>().gameObject, new Vector3(player.transform.position.x, this.combatPetPrefab.transform.position.y, player.transform.position.z), Quaternion.identity);
		}
		Status component = gameObject2.GetComponent<Status>();
		this.combatPet.stats = component;
		this.combatPet.ShowAP();
		component.parent = this.combatPet.gameObject;
		component.SetMaxHP(this.maxHealth);
		component.SetHealth(this.health);
		if (this.petProxyPrefab)
		{
			GameObject gameObject3 = Object.Instantiate<GameObject>(this.petProxyPrefab, player.transform.position + new Vector3(-0.5f, 0.2f, 0f), Quaternion.identity);
			ItemAnimationProxy component2 = gameObject3.GetComponent<ItemAnimationProxy>();
			component2.ChangeSettings(Vector3.zero, Vector3.one, Quaternion.Euler(0f, 0f, 180f), 0.4f);
			component2.CopySprite(this.combatPet.GetComponentInChildren<SpriteRenderer>());
			component2.FlyTo(gameObject3.transform.position, this.combatPet.transform, Vector3.zero);
		}
		this.myItem.playerAnimation = Item2.PlayerAnimation.Command;
		GameFlowManager.main.CheckConstants();
		GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onSummonAnyPet, null, null, true, false);
		GameFlowManager.main.ConsiderAllEffectsPublic(Item2.Trigger.ActionTrigger.onSummonPet, null, new List<Status> { PetItem2.GetStatus(this) }, true, false);
	}

	// Token: 0x060002FD RID: 765 RVA: 0x000118C4 File Offset: 0x0000FAC4
	public List<Enemy.PossibleAttack> GetPetAttacks()
	{
		List<Enemy.PossibleAttack> list = new List<Enemy.PossibleAttack>();
		foreach (GameObject gameObject in this.itemPouch.itemsInside)
		{
			PetEffect component = gameObject.GetComponent<PetEffect>();
			if (component)
			{
				list.Add(component.possibleAttack);
			}
		}
		return list;
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00011938 File Offset: 0x0000FB38
	public static void SetupAllPetEffects()
	{
		foreach (PetItem2 petItem in PetItem2.allPets)
		{
			petItem.SetupPetEffects();
		}
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00011988 File Offset: 0x0000FB88
	public void SetupPetEffects()
	{
		if (!this.myItem)
		{
			this.MakeReferences();
			if (!this.myItem)
			{
				return;
			}
		}
		this.myItem.RemoveModifiers(new List<Item2.Modifier.Length> { Item2.Modifier.Length.whileItemIsInInventory }, -1);
		this.myItem.combatEffects = new List<Item2.CombattEffect>(this.summoningEffects);
		this.myItem.modifiers = this.myItem.modifiers.Where((Item2.Modifier x) => x.origin != "self").ToList<Item2.Modifier>();
		this.myItem.createEffects = new List<Item2.CreateEffect>();
		this.myItem.combatEffects = new List<Item2.CombattEffect>(this.defaultCombatEffects);
		foreach (GameObject gameObject in this.itemPouch.itemsInside)
		{
			if (gameObject)
			{
				Item2 component = gameObject.GetComponent<Item2>();
				if (component && component.itemType.Contains(Item2.ItemType.Treat))
				{
					foreach (Item2.CombattEffect combattEffect in component.combatEffects)
					{
						combattEffect.effect.originName = Item2.GetDisplayName(component.name);
					}
					this.myItem.combatEffects.AddRange(component.combatEffects);
					this.AddAllModifiers(component);
					this.myItem.activeItemStatusEffects.AddRange(component.activeItemStatusEffects.Where((Item2.ItemStatusEffect x) => x.length != Item2.ItemStatusEffect.Length.whileCoveredByHazards));
					this.myItem.createEffects.AddRange(component.createEffects);
				}
			}
		}
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00011B80 File Offset: 0x0000FD80
	private void AddAllModifiers(Item2 item)
	{
		foreach (Item2.Modifier modifier in item.modifiers)
		{
			this.AddModifier(modifier);
		}
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00011BD4 File Offset: 0x0000FDD4
	private void AddModifier(Item2.Modifier modifier)
	{
		foreach (Item2.Modifier modifier2 in this.myItem.modifiers)
		{
			Debug.Log(modifier2.origin);
			if (Item2.CheckIfSameTrigger(modifier2.trigger, modifier.trigger) && !(modifier2.name != modifier.name) && modifier2.effects.Count > 0 && modifier.effects.Count > 0)
			{
				Item2.Effect effect = modifier2.effects[0];
				Item2.Effect effect2 = modifier.effects[0];
				if (effect != null && effect2 != null && effect.type == effect2.type && effect.target == effect2.target && effect.mathematicalType == effect2.mathematicalType)
				{
					effect.value += effect2.value;
					if (effect.itemStatusEffect.Count > 0 && effect2.itemStatusEffect.Count > 0)
					{
						effect.itemStatusEffect[0].num += effect2.itemStatusEffect[0].num;
					}
					return;
				}
			}
		}
		Item2.Modifier modifier3 = modifier.Clone();
		this.myItem.modifiers.Add(modifier3);
	}

	// Token: 0x040001ED RID: 493
	public static List<PetItem2> allPets = new List<PetItem2>();

	// Token: 0x040001EE RID: 494
	[Header("Pet Properties")]
	public int maxHealth = 100;

	// Token: 0x040001EF RID: 495
	public int health = 100;

	// Token: 0x040001F0 RID: 496
	public int APperTurn = 1;

	// Token: 0x040001F1 RID: 497
	public int currentAP = 1;

	// Token: 0x040001F2 RID: 498
	public List<Item2.Cost> summoningCosts = new List<Item2.Cost>();

	// Token: 0x040001F3 RID: 499
	private List<Item2.CombattEffect> summoningEffects = new List<Item2.CombattEffect>();

	// Token: 0x040001F4 RID: 500
	public List<Item2.CombattEffect> defaultCombatEffects = new List<Item2.CombattEffect>();

	// Token: 0x040001F5 RID: 501
	[SerializeField]
	public GameObject petProxyPrefab;

	// Token: 0x040001F6 RID: 502
	[SerializeField]
	public GameObject statusPrefab;

	// Token: 0x040001F7 RID: 503
	public GameObject combatPetPrefab;

	// Token: 0x040001F8 RID: 504
	[NonSerialized]
	public CombatPet combatPet;

	// Token: 0x040001F9 RID: 505
	[NonSerialized]
	public ItemMovement itemMovement;

	// Token: 0x040001FA RID: 506
	[NonSerialized]
	public Item2 myItem;

	// Token: 0x040001FB RID: 507
	[NonSerialized]
	private ItemPouch itemPouch;

	// Token: 0x040001FC RID: 508
	private Coroutine setupCombatPet;
}
