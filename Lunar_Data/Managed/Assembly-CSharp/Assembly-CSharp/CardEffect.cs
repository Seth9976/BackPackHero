using System;
using System.Collections;
using System.Collections.Generic;
using SaveSystem.States;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000014 RID: 20
public class CardEffect : MonoBehaviour
{
	// Token: 0x06000073 RID: 115 RVA: 0x00003C2A File Offset: 0x00001E2A
	private void Start()
	{
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003C2C File Offset: 0x00001E2C
	private void Update()
	{
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00003C2E File Offset: 0x00001E2E
	public int GetNecessaryEnergy()
	{
		if (this.modifierTypes.Contains(Modifier.ModifierEffect.Type.GarlicCosts0) && ModifierManager.instance.GetModifierExists(Modifier.ModifierEffect.Type.GarlicCosts0))
		{
			return 0;
		}
		if (ModifierManager.instance.GetModifierExists(Modifier.ModifierEffect.Type.NextCardCosts0))
		{
			return 0;
		}
		return this.necessaryEnergy;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003C64 File Offset: 0x00001E64
	public bool CanActivate()
	{
		if (EnergyBarMaster.instance.GetEnergy() < this.GetNecessaryEnergy())
		{
			SoundManager.instance.PlaySFX("noBlip", double.PositiveInfinity);
			EnergyBarMaster.instance.PulseEnergies(this.GetNecessaryEnergy());
			if (CardDescriptor.instance != null)
			{
				CardDescriptor.instance.PulseEnergyRequirement();
			}
			return false;
		}
		return true;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003CC8 File Offset: 0x00001EC8
	public bool ConsiderActivate()
	{
		if (EnergyBarMaster.instance.GetEnergy() < this.GetNecessaryEnergy())
		{
			EnergyBarMaster.instance.PulseEnergies(this.GetNecessaryEnergy());
			return false;
		}
		if (this.playConditions.Contains(CardEffect.PlayConditions.OpenSpace) && !this.SpaceIsOpen(InputManager.instance.GetCardPosition()))
		{
			SoundManager.instance.PlaySFX("noBlip", double.PositiveInfinity);
			return false;
		}
		EnergyBarMaster.instance.UseEnergyCapsules(this.GetNecessaryEnergy());
		this.Activate();
		return true;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00003D4C File Offset: 0x00001F4C
	public void Activate()
	{
		Singleton.instance.AddAccomplishment(ProgressState.Accomplishment.CardsPlayed, 1);
		switch (this.classType)
		{
		case CardEffect.ClassType.Attack:
			Singleton.instance.AddAccomplishment(ProgressState.Accomplishment.AttacksUsed, 1);
			break;
		case CardEffect.ClassType.Utility:
			Singleton.instance.AddAccomplishment(ProgressState.Accomplishment.UtilitiesUsed, 1);
			break;
		case CardEffect.ClassType.Movement:
			Singleton.instance.AddAccomplishment(ProgressState.Accomplishment.MovementsUsed, 1);
			break;
		}
		foreach (ProgressState.Accomplishment accomplishment in this.accomplishmentToAddOnPlay)
		{
			Singleton.instance.AddAccomplishment(accomplishment);
		}
		SoundManager.instance.PlaySFX("playCard", double.PositiveInfinity);
		if (OperatorPanel.instance)
		{
			OperatorPanel.instance.hasPlayedCard = true;
		}
		GameEventHandler.CallEvent(GameEventHandler.GameEvent.EventType.onCardPlayed);
		this.onActivate.Invoke();
		this.cardPlacement.Discard();
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00003E40 File Offset: 0x00002040
	public float GetRangeOfEffect()
	{
		float num = this.rangeOfEffect;
		if (this.modifierTypes.Contains(Modifier.ModifierEffect.Type.TossablesIncreasedArea))
		{
			num += ModifierManager.instance.GetModifierValue(Modifier.ModifierEffect.Type.TossablesIncreasedArea);
		}
		return num;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003E73 File Offset: 0x00002073
	private int GetNumToSpawn()
	{
		if (this.modifierTypes.Contains(Modifier.ModifierEffect.Type.NumberOfStakes))
		{
			return this.numToSpawn + (int)ModifierManager.instance.GetModifierValue(Modifier.ModifierEffect.Type.NumberOfStakes);
		}
		return this.numToSpawn;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00003EA0 File Offset: 0x000020A0
	public void DrawCards(int num)
	{
		for (int i = 0; i < num; i++)
		{
			CardManager.instance.DrawCard();
		}
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00003EC3 File Offset: 0x000020C3
	public void DiscardHand()
	{
		CardManager.instance.DiscardAll();
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003ECF File Offset: 0x000020CF
	public void CreatePassiveEffectWithThisPrefab(GameObject prefab)
	{
		PassiveManager.instance.CreatePassiveEffectFromPrefab(prefab, base.GetComponentInChildren<Image>().sprite, this.cardDescription.cardName, this.lengthOfEffect, null, null);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003EFA File Offset: 0x000020FA
	public void CreatePassiveEffect()
	{
		PassiveManager.instance.CreatePassiveEffectFromPrefab(this.prefabToSpawn, base.GetComponentInChildren<Image>().sprite, this.cardDescription.cardName, this.lengthOfEffect, null, null);
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00003F2C File Offset: 0x0000212C
	public bool SpaceIsOpen(Vector2 pos)
	{
		Collider2D collider2D = Physics2D.OverlapCircle(pos, this.GetRangeOfEffect(), LayerMask.GetMask(new string[] { "Level" }));
		if (collider2D)
		{
			Debug.Log(collider2D.gameObject.name);
			return false;
		}
		return true;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00003F74 File Offset: 0x00002174
	public void CreatePrefabAtAllFlamingEnemies()
	{
		foreach (Status status in Status.GetStatusesWithEffect(Status.StatusEffect.Type.Burn))
		{
			Object.Instantiate<GameObject>(this.prefabToSpawn, status.transform.position, Quaternion.identity);
		}
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00003FDC File Offset: 0x000021DC
	public void Dash()
	{
		SoundManager.instance.PlaySFX("dash", double.PositiveInfinity);
		GameObject gameObject = this.prefabToSpawn;
		int num = Mathf.CeilToInt(Vector2.Distance(InputManager.instance.GetCardPosition(), Player.instance.transform.position));
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, Player.instance.transform.position, Quaternion.identity);
			Vector2 vector = Vector2.Lerp(Player.instance.transform.position, InputManager.instance.GetCardPosition(), (float)i / 3f);
			gameObject2.transform.position = vector;
			gameObject2.GetComponent<PlayerBlur>().StartWithAlpha(Mathf.Clamp((float)i / (float)num, 0.25f, 1f));
			foreach (GameObject gameObject3 in this.otherPrefabsToSpawn)
			{
				Object.Instantiate<GameObject>(gameObject3, vector, Quaternion.identity, RoomManager.instance.roomContents);
			}
		}
		Player.instance.transform.position = InputManager.instance.GetCardPosition();
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00004130 File Offset: 0x00002330
	private Transform GetParentTransform()
	{
		if (this.parent == CardEffect.Parent.Player)
		{
			return Player.instance.transform;
		}
		return RoomManager.instance.roomContents;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00004150 File Offset: 0x00002350
	public void CreatePrefab()
	{
		Object.Instantiate<GameObject>(this.prefabToSpawn, Player.instance.transform.position, Quaternion.identity);
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00004172 File Offset: 0x00002372
	public void CreatePrefab(GameObject prefab)
	{
		Object.Instantiate<GameObject>(prefab, Player.instance.transform.position, Quaternion.identity);
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00004190 File Offset: 0x00002390
	public void CreatePrefabRotated()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.prefabToSpawn, Player.instance.transform.position, Quaternion.identity, this.GetParentTransform());
		Vector2 vector = InputManager.instance.GetCardPosition() - Player.instance.transform.position;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		gameObject.transform.rotation = Quaternion.Euler(0f, 0f, num);
		gameObject.transform.localPosition += gameObject.transform.rotation * this.placementOffset;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00004246 File Offset: 0x00002446
	public void CreateTossable()
	{
		Object.Instantiate<GameObject>(this.prefabToSpawn, Player.instance.transform.position, Quaternion.identity, RoomManager.instance.roomContents).GetComponent<Grenade>().destination = InputManager.instance.GetCardPosition();
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00004288 File Offset: 0x00002488
	public void FireOrbital()
	{
		int num = this.numToSpawn;
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < num; i++)
		{
			float num2 = (float)(i * 360 / num);
			GameObject gameObject = Object.Instantiate<GameObject>(this.prefabToSpawn, RoomManager.instance.roomContents);
			gameObject.transform.position = Player.instance.transform.position + new Vector3(Mathf.Cos(num2 * 0.017453292f), Mathf.Sin(num2 * 0.017453292f), 0f);
			list.Add(gameObject);
		}
		PassiveManager.instance.CreatePassiveEffect(base.GetComponentInChildren<Image>().sprite, "Orbital", this.lengthOfEffect, list, null);
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0000433C File Offset: 0x0000253C
	public void Exhaust()
	{
		this.isExhausted = true;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00004345 File Offset: 0x00002545
	public void FireShotsAtArc()
	{
		if (this.coroutine != null)
		{
			base.StopCoroutine(this.coroutine);
		}
		this.coroutine = base.StartCoroutine(this.FireOverTime());
	}

	// Token: 0x0600008A RID: 138 RVA: 0x0000436D File Offset: 0x0000256D
	private IEnumerator FireOverTime()
	{
		float delayBetweenShots = 0.025f;
		int num = this.GetNumToSpawn();
		Vector3[] aimPositions = InputManager.instance.GetAimLocations();
		int num2;
		for (int i = 0; i < num; i = num2 + 1)
		{
			SoundManager.instance.PlaySFX("shoot", double.PositiveInfinity);
			Bullet component = Object.Instantiate<GameObject>(this.prefabToSpawn, Player.instance.transform.position, Quaternion.identity).GetComponent<Bullet>();
			if (this.placementType == CardEffect.PlacementType.Line)
			{
				component.SetVelocity(aimPositions[1] - Player.instance.transform.position);
			}
			else
			{
				Vector3 vector = aimPositions[Mathf.FloorToInt((float)(i * aimPositions.Length / num))];
				component.SetVelocity(vector - Player.instance.transform.position);
			}
			yield return new WaitForSeconds(delayBetweenShots);
			num2 = i;
		}
		this.coroutine = null;
		yield break;
	}

	// Token: 0x04000053 RID: 83
	[Header("-----Basic Stats-----")]
	[SerializeField]
	public List<PlayableCharacter.CharacterName> validForCharacters = new List<PlayableCharacter.CharacterName>();

	// Token: 0x04000054 RID: 84
	[SerializeField]
	public bool canBeFoundInNormalDrops = true;

	// Token: 0x04000055 RID: 85
	[SerializeField]
	public CardEffect.UseType useType;

	// Token: 0x04000056 RID: 86
	[SerializeField]
	public CardEffect.ClassType classType;

	// Token: 0x04000057 RID: 87
	[SerializeField]
	public int necessaryEnergy;

	// Token: 0x04000058 RID: 88
	[SerializeField]
	public List<CardEffect.PlayConditions> playConditions;

	// Token: 0x04000059 RID: 89
	[SerializeField]
	public List<ProgressState.Accomplishment> accomplishmentToAddOnPlay = new List<ProgressState.Accomplishment>();

	// Token: 0x0400005A RID: 90
	[SerializeField]
	public List<Modifier.ModifierEffect.Type> modifierTypes = new List<Modifier.ModifierEffect.Type>();

	// Token: 0x0400005B RID: 91
	[Header("-----Placement Stats-----")]
	[SerializeField]
	public CardEffect.Parent parent;

	// Token: 0x0400005C RID: 92
	[SerializeField]
	public CardEffect.PlacementType placementType;

	// Token: 0x0400005D RID: 93
	public float rangeOfEffect = 1f;

	// Token: 0x0400005E RID: 94
	[SerializeField]
	public bool showRange;

	// Token: 0x0400005F RID: 95
	public float rangeOfPlacement = 3f;

	// Token: 0x04000060 RID: 96
	[SerializeField]
	public int actionIndicatorSortingOrder;

	// Token: 0x04000061 RID: 97
	[SerializeField]
	private Vector3 placementOffset;

	// Token: 0x04000062 RID: 98
	[Header("-----Effect Stats-----")]
	[SerializeField]
	private UnityEvent onActivate;

	// Token: 0x04000063 RID: 99
	[SerializeField]
	public GameObject prefabToSpawn;

	// Token: 0x04000064 RID: 100
	[SerializeField]
	public List<GameObject> otherPrefabsToSpawn = new List<GameObject>();

	// Token: 0x04000065 RID: 101
	[SerializeField]
	public int numToSpawn;

	// Token: 0x04000066 RID: 102
	[SerializeField]
	public float lengthOfEffect;

	// Token: 0x04000067 RID: 103
	[Header("-----References-----")]
	[SerializeField]
	private CardDescription cardDescription;

	// Token: 0x04000068 RID: 104
	[SerializeField]
	private CardPlacement cardPlacement;

	// Token: 0x04000069 RID: 105
	private Coroutine coroutine;

	// Token: 0x0400006A RID: 106
	[NonSerialized]
	public bool isExhausted;

	// Token: 0x020000BF RID: 191
	public enum PlacementType
	{
		// Token: 0x040003D7 RID: 983
		NoLocation,
		// Token: 0x040003D8 RID: 984
		Local,
		// Token: 0x040003D9 RID: 985
		Arc,
		// Token: 0x040003DA RID: 986
		PositionWithinRange,
		// Token: 0x040003DB RID: 987
		Line
	}

	// Token: 0x020000C0 RID: 192
	public enum UseType
	{
		// Token: 0x040003DD RID: 989
		None,
		// Token: 0x040003DE RID: 990
		Active,
		// Token: 0x040003DF RID: 991
		Passive,
		// Token: 0x040003E0 RID: 992
		Equipment
	}

	// Token: 0x020000C1 RID: 193
	public enum ClassType
	{
		// Token: 0x040003E2 RID: 994
		Attack,
		// Token: 0x040003E3 RID: 995
		Utility,
		// Token: 0x040003E4 RID: 996
		Movement
	}

	// Token: 0x020000C2 RID: 194
	public enum PlayConditions
	{
		// Token: 0x040003E6 RID: 998
		OpenSpace
	}

	// Token: 0x020000C3 RID: 195
	public enum Parent
	{
		// Token: 0x040003E8 RID: 1000
		None,
		// Token: 0x040003E9 RID: 1001
		Player
	}
}
