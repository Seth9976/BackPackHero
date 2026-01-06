using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class EnergyBarMaster : MonoBehaviour
{
	// Token: 0x06000194 RID: 404 RVA: 0x00009173 File Offset: 0x00007373
	private void OnEnable()
	{
		EnergyBarMaster.instance = this;
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000917B File Offset: 0x0000737B
	private void OnDisable()
	{
		if (EnergyBarMaster.instance == this)
		{
			EnergyBarMaster.instance = null;
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00009190 File Offset: 0x00007390
	private void Start()
	{
		for (int i = 0; i < 4; i++)
		{
			this.CreateEnergyCapsule();
		}
		this.AddEnergyAfterDelay(400f);
	}

	// Token: 0x06000197 RID: 407 RVA: 0x000091BC File Offset: 0x000073BC
	private void Update()
	{
		if (!Player.instance)
		{
			return;
		}
		float num = 100f;
		if (Player.instance.isWaiting)
		{
			num *= ModifierManager.instance.GetModifierPercentage(Modifier.ModifierEffect.Type.GainEnergyFasterWhenStill);
		}
		float num2 = num * TimeManager.instance.currentUnscaledTimeScale;
		this.AddEnergy(num2 * Time.deltaTime);
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00009214 File Offset: 0x00007414
	public void RemoveAllEnergyCapsules()
	{
		for (int i = this.energyContainer.transform.childCount - 1; i >= 0; i--)
		{
			Object.Destroy(this.energyContainer.transform.GetChild(i).gameObject);
		}
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00009259 File Offset: 0x00007459
	public void CreateEmptyEnergyCapsule()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.energyCapsulePrefab, this.energyContainer);
		gameObject.transform.SetAsFirstSibling();
		gameObject.GetComponent<EnergyCapsule>().SetEnergy(0f);
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00009286 File Offset: 0x00007486
	public void CreateEnergyCapsule()
	{
		Object.Instantiate<GameObject>(this.energyCapsulePrefab, this.energyContainer).transform.SetAsFirstSibling();
	}

	// Token: 0x0600019B RID: 411 RVA: 0x000092A4 File Offset: 0x000074A4
	public int GetEnergy()
	{
		int num = 0;
		for (int i = 0; i < this.energyContainer.transform.childCount; i++)
		{
			EnergyCapsule component = this.energyContainer.transform.GetChild(i).gameObject.GetComponent<EnergyCapsule>();
			if (component && component.IsFull())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00009300 File Offset: 0x00007500
	public void PulseEnergies(int capsulesToPulse)
	{
		for (int i = 0; i < this.energyContainer.transform.childCount; i++)
		{
			EnergyCapsule component = this.energyContainer.transform.GetChild(i).gameObject.GetComponent<EnergyCapsule>();
			if (component)
			{
				component.PulseNotEnough();
				capsulesToPulse--;
				if (capsulesToPulse <= 0)
				{
					break;
				}
			}
		}
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0000935C File Offset: 0x0000755C
	public bool HasAFullEnergy()
	{
		for (int i = 0; i < this.energyContainer.transform.childCount; i++)
		{
			EnergyCapsule component = this.energyContainer.transform.GetChild(i).gameObject.GetComponent<EnergyCapsule>();
			if (component && component.IsFull())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x000093B4 File Offset: 0x000075B4
	public void UseEnergyCapsules(int amountToUse)
	{
		for (int i = 0; i < amountToUse; i++)
		{
			this.UseEnergyCapsule();
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x000093D4 File Offset: 0x000075D4
	public void UseEnergyCapsule()
	{
		float num = (float)EnergyBarMaster.capsuleCapacity;
		for (int i = this.energyContainer.transform.childCount - 1; i >= 0; i--)
		{
			EnergyCapsule component = this.energyContainer.transform.GetChild(i).gameObject.GetComponent<EnergyCapsule>();
			if (component && component.energyValue > 0f)
			{
				float energyValue = component.energyValue;
				component.FillEnergy(-num);
				num -= energyValue - component.energyValue;
				if (num <= 0f)
				{
					break;
				}
			}
		}
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00009458 File Offset: 0x00007658
	public void AddEnergy(float amountToAdd)
	{
		for (int i = 0; i < this.energyContainer.transform.childCount; i++)
		{
			EnergyCapsule component = this.energyContainer.transform.GetChild(i).gameObject.GetComponent<EnergyCapsule>();
			if (component && !component.IsFull())
			{
				float energyValue = component.energyValue;
				component.FillEnergy(amountToAdd);
				amountToAdd -= component.energyValue - energyValue;
				if (amountToAdd <= 0f)
				{
					break;
				}
			}
		}
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x000094CE File Offset: 0x000076CE
	public void AddEnergyAfterDelay(float num)
	{
		base.StartCoroutine(this.AddEnergyRoutine(num));
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000094DE File Offset: 0x000076DE
	private IEnumerator AddEnergyRoutine(float num)
	{
		yield return new WaitForSeconds(0.5f);
		EnergyBarMaster.instance.AddEnergy(num);
		yield break;
	}

	// Token: 0x04000143 RID: 323
	public static EnergyBarMaster instance;

	// Token: 0x04000144 RID: 324
	[SerializeField]
	private Transform energyContainer;

	// Token: 0x04000145 RID: 325
	[SerializeField]
	private GameObject energyCapsulePrefab;

	// Token: 0x04000146 RID: 326
	public static int capsuleCapacity = 100;
}
