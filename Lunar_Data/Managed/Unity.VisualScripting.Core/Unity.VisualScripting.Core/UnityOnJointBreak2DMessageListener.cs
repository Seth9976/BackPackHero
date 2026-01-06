using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200008B RID: 139
	[AddComponentMenu("")]
	public sealed class UnityOnJointBreak2DMessageListener : MessageListener
	{
		// Token: 0x060003DF RID: 991 RVA: 0x000095ED File Offset: 0x000077ED
		private void OnJointBreak2D(Joint2D brokenJoint)
		{
			EventBus.Trigger<Joint2D>("OnJointBreak2D", base.gameObject, brokenJoint);
		}
	}
}
