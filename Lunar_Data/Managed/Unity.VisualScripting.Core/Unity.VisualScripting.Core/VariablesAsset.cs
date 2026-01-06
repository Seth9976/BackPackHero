using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000174 RID: 372
	[IncludeInSettings(false)]
	public sealed class VariablesAsset : LudiqScriptableObject
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x000298F1 File Offset: 0x00027AF1
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x000298F9 File Offset: 0x00027AF9
		[Serialize]
		[Inspectable]
		[InspectorWide(true)]
		public VariableDeclarations declarations { get; internal set; } = new VariableDeclarations();

		// Token: 0x060009F6 RID: 2550 RVA: 0x00029902 File Offset: 0x00027B02
		[ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}
	}
}
