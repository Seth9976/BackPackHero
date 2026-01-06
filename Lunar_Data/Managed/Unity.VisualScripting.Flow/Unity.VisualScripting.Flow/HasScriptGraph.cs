using System;
using JetBrains.Annotations;

namespace Unity.VisualScripting
{
	// Token: 0x020000AF RID: 175
	[TypeIcon(typeof(FlowGraph))]
	[UnitCategory("Graphs/Graph Nodes")]
	public sealed class HasScriptGraph : HasGraph<FlowGraph, ScriptGraphAsset, ScriptMachine>
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000AC74 File Offset: 0x00008E74
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0000AC7C File Offset: 0x00008E7C
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable]
		[UsedImplicitly]
		public ScriptGraphContainerType containerType { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000AC85 File Offset: 0x00008E85
		protected override bool isGameObject
		{
			get
			{
				return this.containerType == ScriptGraphContainerType.GameObject;
			}
		}
	}
}
