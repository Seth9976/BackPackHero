using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200008C RID: 140
	[AddComponentMenu("")]
	public sealed class UnityOnJointBreakMessageListener : MessageListener
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x00009608 File Offset: 0x00007808
		private void OnJointBreak(float breakForce)
		{
			EventBus.Trigger<float>("OnJointBreak", base.gameObject, breakForce);
		}
	}
}
