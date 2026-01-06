using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A8 RID: 168
public class CenterDropDownOnButton : MonoBehaviour
{
	// Token: 0x060003EA RID: 1002 RVA: 0x0002760D File Offset: 0x0002580D
	private void Start()
	{
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0002760F File Offset: 0x0002580F
	private void Update()
	{
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00027614 File Offset: 0x00025814
	public void SetSliderMenu(Transform transform)
	{
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		RectTransform component = transform.GetComponent<RectTransform>();
		RectTransform component2 = transform.parent.GetComponent<RectTransform>();
		ScrollRect componentInParent = base.GetComponentInParent<ScrollRect>();
		if (!componentInParent)
		{
			return;
		}
		RectTransform component3 = componentInParent.GetComponent<RectTransform>();
		if (!component || !component2 || !componentInParent || !component3)
		{
			return;
		}
		if (componentInParent.vertical)
		{
			component2.anchoredPosition = new Vector2(0f, Mathf.Clamp(component.anchoredPosition.y * -1f - component3.rect.height / 2f, 0f, component2.rect.height - component3.rect.height));
		}
		if (componentInParent.horizontal)
		{
			Vector2 pivot = component2.pivot;
			component2.pivot = new Vector2(0f, 0.5f);
			component2.anchoredPosition = new Vector2(component.anchoredPosition.x * -1f + component3.rect.width / 2f, 0f);
			component2.anchoredPosition = new Vector2(Mathf.Clamp(component2.anchoredPosition.x, -(component2.sizeDelta.x / 2f), component2.sizeDelta.x / 2f), component2.anchoredPosition.y);
			component2.pivot = pivot;
		}
	}
}
