using System;
using JetBrains.Annotations;

namespace Unity.VisualScripting
{
	// Token: 0x02000005 RID: 5
	[TypeIcon(typeof(StateGraph))]
	[UnitCategory("Graphs/Graph Nodes")]
	public sealed class HasStateGraph : HasGraph<StateGraph, StateGraphAsset, StateMachine>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024FD File Offset: 0x000006FD
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002505 File Offset: 0x00000705
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable]
		[UsedImplicitly]
		public StateGraphContainerType containerType { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000250E File Offset: 0x0000070E
		protected override bool isGameObject
		{
			get
			{
				return this.containerType == StateGraphContainerType.GameObject;
			}
		}
	}
}
