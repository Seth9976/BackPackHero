using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000093 RID: 147
	[AddComponentMenu("")]
	public sealed class UnityOnMouseUpMessageListener : MessageListener
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x000096BF File Offset: 0x000078BF
		private void OnMouseUp()
		{
			EventBus.Trigger("OnMouseUp", base.gameObject);
		}
	}
}
