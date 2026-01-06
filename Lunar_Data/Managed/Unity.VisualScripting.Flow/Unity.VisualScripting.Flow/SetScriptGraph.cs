using System;
using JetBrains.Annotations;

namespace Unity.VisualScripting
{
	// Token: 0x020000B2 RID: 178
	[TypeIcon(typeof(FlowGraph))]
	public sealed class SetScriptGraph : SetGraph<FlowGraph, ScriptGraphAsset, ScriptMachine>
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000AE61 File Offset: 0x00009061
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0000AE69 File Offset: 0x00009069
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable]
		[UsedImplicitly]
		public ScriptGraphContainerType containerType { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000AE72 File Offset: 0x00009072
		protected override bool isGameObject
		{
			get
			{
				return this.containerType == ScriptGraphContainerType.GameObject;
			}
		}
	}
}
