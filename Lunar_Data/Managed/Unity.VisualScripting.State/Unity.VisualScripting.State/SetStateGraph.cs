using System;
using JetBrains.Annotations;

namespace Unity.VisualScripting
{
	// Token: 0x0200001E RID: 30
	[TypeIcon(typeof(StateGraph))]
	public class SetStateGraph : SetGraph<StateGraph, StateGraphAsset, StateMachine>
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000034F4 File Offset: 0x000016F4
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000034FC File Offset: 0x000016FC
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable]
		[UsedImplicitly]
		public StateGraphContainerType containerType { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003505 File Offset: 0x00001705
		protected override bool isGameObject
		{
			get
			{
				return this.containerType == StateGraphContainerType.GameObject;
			}
		}
	}
}
