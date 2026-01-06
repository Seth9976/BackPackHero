using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018E RID: 398
public class SymbolSpecificNavigation : MonoBehaviour
{
	// Token: 0x06001013 RID: 4115 RVA: 0x0009B628 File Offset: 0x00099828
	private void Awake()
	{
		Selectable component = base.GetComponent<Selectable>();
		if (component == null)
		{
			Debug.LogError("Missing Selectable on GameObject");
		}
		foreach (SymbolSpecificNavigation.NavigationOverride navigationOverride in this.navigationOverrides)
		{
			if (this.RequiredSymbolsDefined(navigationOverride))
			{
				this.SetFromOverride(component, navigationOverride);
			}
		}
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x0009B680 File Offset: 0x00099880
	private bool RequiredSymbolsDefined(SymbolSpecificNavigation.NavigationOverride navigationOverride)
	{
		foreach (string text in navigationOverride.requiredSymbols)
		{
			if (!SymbolSpecificNavigation.SYMBOLS.Contains(text))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x0009B6E0 File Offset: 0x000998E0
	private void SetFromOverride(Selectable selectable, SymbolSpecificNavigation.NavigationOverride navigationOverride)
	{
		Navigation navigation = selectable.navigation;
		navigation.selectOnUp = navigationOverride.selectOnUp;
		navigation.selectOnDown = navigationOverride.selectOnDown;
		navigation.selectOnLeft = navigationOverride.selectOnLeft;
		navigation.selectOnRight = navigationOverride.selectOnRight;
		selectable.navigation = navigation;
	}

	// Token: 0x04000D31 RID: 3377
	private static readonly List<string> SYMBOLS = new List<string>();

	// Token: 0x04000D32 RID: 3378
	[SerializeField]
	private SymbolSpecificNavigation.NavigationOverride[] navigationOverrides;

	// Token: 0x02000469 RID: 1129
	[Serializable]
	public struct NavigationOverride
	{
		// Token: 0x04001A1B RID: 6683
		public List<string> requiredSymbols;

		// Token: 0x04001A1C RID: 6684
		public Selectable selectOnUp;

		// Token: 0x04001A1D RID: 6685
		public Selectable selectOnDown;

		// Token: 0x04001A1E RID: 6686
		public Selectable selectOnLeft;

		// Token: 0x04001A1F RID: 6687
		public Selectable selectOnRight;
	}
}
