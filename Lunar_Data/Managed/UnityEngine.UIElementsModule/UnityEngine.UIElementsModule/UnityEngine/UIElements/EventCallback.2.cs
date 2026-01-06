using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CF RID: 463
	// (Invoke) Token: 0x06000E9D RID: 3741
	public delegate void EventCallback<in TEventType, in TCallbackArgs>(TEventType evt, TCallbackArgs userArgs);
}
