using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000067 RID: 103
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(GUI))]
	[UnitOrder(0)]
	public sealed class OnGUI : GlobalEventUnit<EmptyEventArgs>
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00008FC9 File Offset: 0x000071C9
		protected override string hookName
		{
			get
			{
				return "OnGUI";
			}
		}
	}
}
