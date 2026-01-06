using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000138 RID: 312
	internal interface IGenericMenu
	{
		// Token: 0x06000A29 RID: 2601
		void AddItem(string itemName, bool isChecked, Action action);

		// Token: 0x06000A2A RID: 2602
		void AddItem(string itemName, bool isChecked, Action<object> action, object data);

		// Token: 0x06000A2B RID: 2603
		void AddDisabledItem(string itemName, bool isChecked);

		// Token: 0x06000A2C RID: 2604
		void AddSeparator(string path);

		// Token: 0x06000A2D RID: 2605
		void DropDown(Rect position, VisualElement targetElement = null, bool anchored = false);
	}
}
