using System;
using Steamworks;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class OpenKeyboard : MonoBehaviour
{
	// Token: 0x0600026D RID: 621 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
	public void OpenTouchscreenKeyboard()
	{
		if (SteamManager.Initialized)
		{
			RectTransform component = base.GetComponent<RectTransform>();
			Vector2 min = component.rect.min;
			SteamUtils.ShowFloatingGamepadTextInput(EFloatingGamepadTextInputMode.k_EFloatingGamepadTextInputModeModeSingleLine, (int)min.x, (int)min.y, (int)component.rect.width, (int)component.rect.height);
		}
	}
}
