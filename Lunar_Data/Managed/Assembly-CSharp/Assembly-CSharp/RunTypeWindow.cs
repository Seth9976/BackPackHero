using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008B RID: 139
public class RunTypeWindow : MonoBehaviour
{
	// Token: 0x060003AA RID: 938 RVA: 0x000123EC File Offset: 0x000105EC
	private void OnEnable()
	{
		RunTypeWindow.instance = this;
	}

	// Token: 0x060003AB RID: 939 RVA: 0x000123F4 File Offset: 0x000105F4
	private void OnDisable()
	{
		RunTypeWindow.instance = null;
	}

	// Token: 0x060003AC RID: 940 RVA: 0x000123FC File Offset: 0x000105FC
	public void SetupRunTypes()
	{
		foreach (object obj in this.runTypesParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		List<RunType> list = (from runType in this.runTypes.Where((RunType runType) => UnlockManager.instance.IsUnlocked(runType)).ToList<RunType>()
			where runType.characterName == Singleton.instance.selectedCharacter.characterName
			select runType).ToList<RunType>();
		bool flag = true;
		foreach (RunType runType2 in list)
		{
			RunTypeButton component = Object.Instantiate<GameObject>(this.runTypePrefab, this.runTypesParent).GetComponent<RunTypeButton>();
			component.SetRunType(runType2);
			if (flag)
			{
				component.OnClick();
				foreach (Selectable selectable in this.characters.GetComponentsInChildren<Selectable>())
				{
					Navigation navigation = selectable.navigation;
					navigation.selectOnDown = component.GetComponent<Button>();
					selectable.navigation = navigation;
				}
				flag = false;
			}
		}
	}

	// Token: 0x040002CD RID: 717
	public static RunTypeWindow instance;

	// Token: 0x040002CE RID: 718
	[SerializeField]
	private List<RunType> runTypes = new List<RunType>();

	// Token: 0x040002CF RID: 719
	[SerializeField]
	private GameObject runTypePrefab;

	// Token: 0x040002D0 RID: 720
	[SerializeField]
	private Transform runTypesParent;

	// Token: 0x040002D1 RID: 721
	[SerializeField]
	private Transform characters;
}
