using System;
using TMPro;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class CramButton : MonoBehaviour
{
	// Token: 0x060008BB RID: 2235 RVA: 0x0005C068 File Offset: 0x0005A268
	private void Start()
	{
		this.gameFlowManager = GameFlowManager.main;
		this.animator = base.GetComponent<Animator>();
		this.satchel = Object.FindObjectOfType<Satchel>();
		this.energyNumberText = this.energyTransform.GetComponentInChildren<TextMeshPro>();
		this.energyTransform.transform.rotation = Quaternion.identity;
		this.UpdateCost();
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x0005C0C3 File Offset: 0x0005A2C3
	private void Update()
	{
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x0005C0C8 File Offset: 0x0005A2C8
	public static void ResetAllCramButtonCosts()
	{
		foreach (CramButton cramButton in Object.FindObjectsOfType<CramButton>())
		{
			cramButton.currentEnergyCost = 0;
			cramButton.UpdateCost();
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0005C0F8 File Offset: 0x0005A2F8
	public void UpdateCost()
	{
		if (this.currentEnergyCost == 0)
		{
			this.energyTransform.gameObject.SetActive(false);
			return;
		}
		this.energyTransform.gameObject.SetActive(true);
		this.energyNumberText.text = this.currentEnergyCost.ToString() ?? "";
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x0005C150 File Offset: 0x0005A350
	private void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.enemyTurn || this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerAction)
		{
			return;
		}
		if (this.gameFlowManager.battlePhase == GameFlowManager.BattlePhase.playerTurn)
		{
			Player main = Player.main;
			if (!main || main.AP < this.currentEnergyCost)
			{
				GameManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("gm15"));
				return;
			}
			main.ChangeAP(-1 * this.currentEnergyCost);
			this.currentEnergyCost++;
		}
		this.UpdateCost();
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0005C1ED File Offset: 0x0005A3ED
	private void OnMouseOver()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.ShowCramHightlight();
		this.animator.Play("cramWiggle");
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0005C212 File Offset: 0x0005A412
	public void ShowCramHightlight()
	{
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0005C214 File Offset: 0x0005A414
	private void OnMouseExit()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.satchel.EndCramHighlights();
		this.animator.Play("cramStatic");
	}

	// Token: 0x040006E4 RID: 1764
	private GameFlowManager gameFlowManager;

	// Token: 0x040006E5 RID: 1765
	private Animator animator;

	// Token: 0x040006E6 RID: 1766
	private Satchel satchel;

	// Token: 0x040006E7 RID: 1767
	[SerializeField]
	private Vector2 cramDirection;

	// Token: 0x040006E8 RID: 1768
	[SerializeField]
	private Transform energyTransform;

	// Token: 0x040006E9 RID: 1769
	private TextMeshPro energyNumberText;

	// Token: 0x040006EA RID: 1770
	public int currentEnergyCost;
}
