using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018C RID: 396
public class PlatformSpecificNavigation : MonoBehaviour
{
	// Token: 0x06001007 RID: 4103 RVA: 0x0009B3E0 File Offset: 0x000995E0
	private void Awake()
	{
		base.GetComponent<Selectable>();
		PlatformSpecificNavigation.NavigationOverride[] array = this.navigationOverrides;
		for (int i = 0; i < array.Length; i++)
		{
		}
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x0009B414 File Offset: 0x00099614
	private void SetFromOverride(Selectable selectable, PlatformSpecificNavigation.NavigationOverride navigationOverride)
	{
		Navigation navigation = selectable.navigation;
		navigation.selectOnUp = navigationOverride.selectOnUp;
		navigation.selectOnDown = navigationOverride.selectOnDown;
		navigation.selectOnLeft = navigationOverride.selectOnLeft;
		navigation.selectOnRight = navigationOverride.selectOnRight;
		selectable.navigation = navigation;
	}

	// Token: 0x04000D2B RID: 3371
	[SerializeField]
	private PlatformSpecificNavigation.NavigationOverride[] navigationOverrides;

	// Token: 0x02000466 RID: 1126
	[Flags]
	public enum Platform
	{
		// Token: 0x04001A11 RID: 6673
		Switch = 1,
		// Token: 0x04001A12 RID: 6674
		Playstation = 2,
		// Token: 0x04001A13 RID: 6675
		Xbox = 3
	}

	// Token: 0x02000467 RID: 1127
	[Serializable]
	public struct NavigationOverride
	{
		// Token: 0x04001A14 RID: 6676
		public PlatformSpecificNavigation.Platform platform;

		// Token: 0x04001A15 RID: 6677
		public Selectable selectOnUp;

		// Token: 0x04001A16 RID: 6678
		public Selectable selectOnDown;

		// Token: 0x04001A17 RID: 6679
		public Selectable selectOnLeft;

		// Token: 0x04001A18 RID: 6680
		public Selectable selectOnRight;
	}
}
