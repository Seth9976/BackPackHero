using System;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x02000072 RID: 114
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(ScrollRect))]
	[UnitOrder(7)]
	public sealed class OnScrollRectValueChanged : GameObjectEventUnit<Vector2>
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000091CF File Offset: 0x000073CF
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnScrollRectValueChangedMessageListener);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003DB RID: 987 RVA: 0x000091DB File Offset: 0x000073DB
		protected override string hookName
		{
			get
			{
				return "OnScrollRectValueChanged";
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003DC RID: 988 RVA: 0x000091E2 File Offset: 0x000073E2
		// (set) Token: 0x060003DD RID: 989 RVA: 0x000091EA File Offset: 0x000073EA
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput value { get; private set; }

		// Token: 0x060003DE RID: 990 RVA: 0x000091F3 File Offset: 0x000073F3
		protected override void Definition()
		{
			base.Definition();
			this.value = base.ValueOutput<Vector2>("value");
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000920C File Offset: 0x0000740C
		protected override void AssignArguments(Flow flow, Vector2 value)
		{
			flow.SetValue(this.value, value);
		}
	}
}
