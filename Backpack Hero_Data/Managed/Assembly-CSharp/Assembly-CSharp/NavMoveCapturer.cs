using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000E0 RID: 224
public class NavMoveCapturer : CustomInputHandler, IMoveHandler, IEventSystemHandler
{
	// Token: 0x060006F0 RID: 1776 RVA: 0x0004388C File Offset: 0x00041A8C
	public void OnMove(AxisEventData eventData)
	{
		DigitalCursorInterface digitalCursorInterface = this.cursorInterface;
		if (digitalCursorInterface == null)
		{
			return;
		}
		digitalCursorInterface.OnMove(eventData, base.GetComponent<Selectable>());
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x000438A8 File Offset: 0x00041AA8
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName == "contextualaction")
		{
			foreach (Selectable selectable in base.gameObject.GetComponentsInChildren<Selectable>())
			{
				if (selectable.navigation.mode == Navigation.Mode.None && selectable.IsInteractable())
				{
					Button component = selectable.gameObject.GetComponent<Button>();
					if (component != null)
					{
						component.onClick.Invoke();
					}
				}
			}
		}
		base.OnPressStart(keyName, overrideKeyName);
	}

	// Token: 0x04000595 RID: 1429
	public DigitalCursorInterface cursorInterface;
}
