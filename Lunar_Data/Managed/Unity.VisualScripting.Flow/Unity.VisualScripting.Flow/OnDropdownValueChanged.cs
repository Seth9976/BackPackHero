using System;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x02000065 RID: 101
	[UnitCategory("Events/GUI")]
	[TypeIcon(typeof(Dropdown))]
	[UnitOrder(4)]
	public sealed class OnDropdownValueChanged : GameObjectEventUnit<int>
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00008F0B File Offset: 0x0000710B
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnDropdownValueChangedMessageListener);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00008F17 File Offset: 0x00007117
		protected override string hookName
		{
			get
			{
				return "OnDropdownValueChanged";
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00008F1E File Offset: 0x0000711E
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00008F26 File Offset: 0x00007126
		[DoNotSerialize]
		public ValueOutput index { get; private set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00008F2F File Offset: 0x0000712F
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00008F37 File Offset: 0x00007137
		[DoNotSerialize]
		public ValueOutput text { get; private set; }

		// Token: 0x060003A4 RID: 932 RVA: 0x00008F40 File Offset: 0x00007140
		protected override void Definition()
		{
			base.Definition();
			this.index = base.ValueOutput<int>("index");
			this.text = base.ValueOutput<string>("text");
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00008F6A File Offset: 0x0000716A
		protected override void AssignArguments(Flow flow, int index)
		{
			flow.SetValue(this.index, index);
			flow.SetValue(this.text, flow.GetValue<Dropdown>(base.target).options[index].text);
		}
	}
}
