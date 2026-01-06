using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DF RID: 223
public class DraggableCombat : CustomInputHandler
{
	// Token: 0x060006E4 RID: 1764 RVA: 0x00043476 File Offset: 0x00041676
	private void Awake()
	{
		DraggableCombat.all.Add(this);
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00043483 File Offset: 0x00041683
	private void OnDestroy()
	{
		DraggableCombat.all.Remove(this);
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00043494 File Offset: 0x00041694
	public static DraggableCombat GetDragging()
	{
		foreach (DraggableCombat draggableCombat in DraggableCombat.all)
		{
			if (draggableCombat.isDragging)
			{
				return draggableCombat;
			}
		}
		return null;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x000434F0 File Offset: 0x000416F0
	public static DraggableCombat GetClosestNonDragging(Vector2 start, Vector2 direction)
	{
		float num = 999f;
		DraggableCombat draggableCombat = null;
		foreach (DraggableCombat draggableCombat2 in DraggableCombat.all)
		{
			if (!draggableCombat2.isDragging && Vector2.Dot(draggableCombat2.transform.position - start, direction) >= 0f)
			{
				float num2 = Vector2.Distance(start, draggableCombat2.transform.position);
				if (num2 < num)
				{
					num = num2;
					draggableCombat = draggableCombat2;
				}
			}
		}
		return draggableCombat;
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00043594 File Offset: 0x00041794
	private void Start()
	{
		this.gameFlowManager = GameFlowManager.main;
		this.player = Player.main;
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x000435AC File Offset: 0x000417AC
	private void Update()
	{
		if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerTurn && this.player.characterName == Character.CharacterName.Pochette)
		{
			base.EnableInterface();
		}
		else
		{
			base.DisableInterface();
		}
		if (!this.mySpacerLocation)
		{
			CombatPet component = base.GetComponent<CombatPet>();
			if (component)
			{
				this.mySpacerLocation = component.mySpacerLocation;
			}
			else if (this.player)
			{
				this.mySpacerLocation = this.player.mySpacerLocation;
			}
		}
		if (this.isDragging)
		{
			DigitalCursor.main.EnterFreeMoveOnly(base.transform);
			Vector2 vector = DigitalCursor.main.transform.position;
			base.transform.position = new Vector3(Mathf.Clamp(vector.x, -9.5f, 0f), base.transform.position.y, base.transform.position.z);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -1f);
			if ((!Input.GetMouseButton(0) && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor) || DigitalCursor.main.GetInputDown("cancel"))
			{
				this.isDragging = false;
				this.StopDrag();
			}
		}
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00043708 File Offset: 0x00041908
	public void StopDrag()
	{
		int siblingIndex = this.mySpacerLocation.GetSiblingIndex();
		this.mySpacerLocation.SetParent(null);
		GameObject gameObject = GameObject.FindGameObjectWithTag("PlayerSpacerParent");
		bool flag = false;
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			if (gameObject.transform.GetChild(i).transform.position.x > base.transform.position.x)
			{
				this.mySpacerLocation.SetParent(gameObject.transform);
				this.mySpacerLocation.SetSiblingIndex(i);
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			this.mySpacerLocation.SetParent(gameObject.transform);
			this.mySpacerLocation.SetAsLastSibling();
		}
		if (this.mySpacerLocation.GetSiblingIndex() != siblingIndex)
		{
			Item2.GetAllEffectTotals();
			SoundManager.main.PlaySFX("wheelSFX");
			return;
		}
		SoundManager.main.PlaySFX("putdown");
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x000437EF File Offset: 0x000419EF
	private void OnMouseDown()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.StartDrag();
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x00043804 File Offset: 0x00041A04
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName != "confirm" || overrideKeyName)
		{
			return;
		}
		this.StartDrag();
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0004381C File Offset: 0x00041A1C
	private void StartDrag()
	{
		if (this.player.characterName != Character.CharacterName.Pochette || this.gameFlowManager.battlePhase != GameFlowManager.BattlePhase.playerTurn)
		{
			return;
		}
		CombatPet component = base.GetComponent<CombatPet>();
		if (component && component.stats && component.stats.health <= 0)
		{
			return;
		}
		this.isDragging = true;
	}

	// Token: 0x04000590 RID: 1424
	public static List<DraggableCombat> all = new List<DraggableCombat>();

	// Token: 0x04000591 RID: 1425
	[SerializeField]
	public bool isDragging;

	// Token: 0x04000592 RID: 1426
	private Transform mySpacerLocation;

	// Token: 0x04000593 RID: 1427
	private GameFlowManager gameFlowManager;

	// Token: 0x04000594 RID: 1428
	private Player player;
}
