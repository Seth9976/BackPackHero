using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000195 RID: 405
[ExecuteInEditMode]
public class SetupScrollExplicit : MonoBehaviour
{
	// Token: 0x06001038 RID: 4152 RVA: 0x0009C1EC File Offset: 0x0009A3EC
	private void Start()
	{
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x0009C1EE File Offset: 0x0009A3EE
	private void Update()
	{
		if (this.setupScroll)
		{
			this.setupScroll = false;
			this.SetupScroll();
		}
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x0009C205 File Offset: 0x0009A405
	public void SetupScrollPublic()
	{
		this.SetupScroll();
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x0009C210 File Offset: 0x0009A410
	private void SetupScroll()
	{
		ScrollRect component = base.GetComponent<ScrollRect>();
		Transform transform = base.transform;
		if (component)
		{
			transform = component.content;
		}
		if (this.scrollType == SetupScrollExplicit.ScrollType.Both)
		{
			foreach (Selectable selectable in transform.GetComponentsInChildren<Selectable>())
			{
				Navigation navigation = new Navigation
				{
					mode = Navigation.Mode.Explicit
				};
				selectable.navigation = navigation;
			}
			VerticalLayoutGroup[] componentsInChildren2 = transform.GetComponentsInChildren<VerticalLayoutGroup>();
			HorizontalLayoutGroup[] componentsInChildren3 = transform.GetComponentsInChildren<HorizontalLayoutGroup>();
			foreach (VerticalLayoutGroup verticalLayoutGroup in componentsInChildren2)
			{
				for (int j = 0; j < verticalLayoutGroup.transform.childCount; j++)
				{
					Selectable componentInChildren = verticalLayoutGroup.transform.GetChild(j).GetComponentInChildren<Selectable>();
					if (componentInChildren)
					{
						Selectable selectable2 = null;
						Selectable selectable3 = null;
						for (int k = j + 1; k < verticalLayoutGroup.transform.childCount; k++)
						{
							if (verticalLayoutGroup.transform.GetChild(k).gameObject.activeSelf)
							{
								selectable3 = verticalLayoutGroup.transform.GetChild(k).GetComponentInChildren<Selectable>();
								break;
							}
						}
						for (int l = j - 1; l >= 0; l--)
						{
							if (verticalLayoutGroup.transform.GetChild(l).gameObject.activeSelf)
							{
								selectable2 = verticalLayoutGroup.transform.GetChild(l).GetComponentInChildren<Selectable>();
								break;
							}
						}
						Selectable selectable4 = componentInChildren;
						Navigation navigation = new Navigation
						{
							mode = Navigation.Mode.Explicit,
							selectOnUp = selectable2,
							selectOnDown = selectable3,
							selectOnLeft = null,
							selectOnRight = null
						};
						selectable4.navigation = navigation;
					}
				}
			}
			foreach (HorizontalLayoutGroup horizontalLayoutGroup in componentsInChildren3)
			{
				for (int m = 0; m < horizontalLayoutGroup.transform.childCount; m++)
				{
					Selectable componentInChildren2 = horizontalLayoutGroup.transform.GetChild(m).GetComponentInChildren<Selectable>();
					if (componentInChildren2)
					{
						Selectable selectable5 = componentInChildren2;
						Navigation navigation = new Navigation
						{
							mode = Navigation.Mode.Explicit,
							selectOnUp = componentInChildren2.navigation.selectOnUp,
							selectOnDown = componentInChildren2.navigation.selectOnDown,
							selectOnLeft = ((m > 0) ? horizontalLayoutGroup.transform.GetChild(m - 1).GetComponentInChildren<Selectable>() : null),
							selectOnRight = ((m < horizontalLayoutGroup.transform.childCount - 1) ? horizontalLayoutGroup.transform.GetChild(m + 1).GetComponentInChildren<Selectable>() : null)
						};
						selectable5.navigation = navigation;
					}
				}
			}
			return;
		}
		for (int n = 0; n < transform.childCount; n++)
		{
			Selectable componentInChildren3 = transform.GetChild(n).GetComponentInChildren<Selectable>();
			Selectable selectable6 = this.GetSelectable(transform, n, -1);
			Selectable selectable7 = this.GetSelectable(transform, n, 1);
			if (componentInChildren3)
			{
				Selectable selectable8 = componentInChildren3;
				Navigation navigation = new Navigation
				{
					mode = Navigation.Mode.Explicit,
					selectOnUp = ((this.scrollType == SetupScrollExplicit.ScrollType.Vertical && selectable6) ? selectable6 : null),
					selectOnDown = ((this.scrollType == SetupScrollExplicit.ScrollType.Vertical && selectable7) ? selectable7 : null),
					selectOnLeft = ((this.scrollType == SetupScrollExplicit.ScrollType.Horizontal && selectable6) ? selectable6 : null),
					selectOnRight = ((this.scrollType == SetupScrollExplicit.ScrollType.Horizontal && selectable7) ? selectable7 : null)
				};
				selectable8.navigation = navigation;
			}
		}
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x0009C5A0 File Offset: 0x0009A7A0
	private Selectable GetSelectable(Transform t, int i, int dir)
	{
		i += dir;
		while (i >= 0 && i < t.childCount - 1)
		{
			Selectable componentInChildren = t.GetChild(i).GetComponentInChildren<Selectable>();
			if (componentInChildren)
			{
				return componentInChildren;
			}
			i += dir;
		}
		return null;
	}

	// Token: 0x04000D50 RID: 3408
	[SerializeField]
	private bool setupScroll;

	// Token: 0x04000D51 RID: 3409
	[SerializeField]
	private SetupScrollExplicit.ScrollType scrollType;

	// Token: 0x0200046D RID: 1133
	[SerializeField]
	private enum ScrollType
	{
		// Token: 0x04001A29 RID: 6697
		Vertical,
		// Token: 0x04001A2A RID: 6698
		Horizontal,
		// Token: 0x04001A2B RID: 6699
		Both
	}
}
