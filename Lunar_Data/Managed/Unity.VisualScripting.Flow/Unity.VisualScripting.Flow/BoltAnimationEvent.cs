using System;
using System.ComponentModel;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200004D RID: 77
	[UnitCategory("Events/Animation")]
	[UnitShortTitle("Animation Event")]
	[UnitTitle("Animation Event")]
	[TypeIcon(typeof(Animation))]
	[DisplayName("Visual Scripting Animation Event")]
	public sealed class BoltAnimationEvent : MachineEventUnit<AnimationEvent>
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00008575 File Offset: 0x00006775
		protected override string hookName
		{
			get
			{
				return "AnimationEvent";
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000857C File Offset: 0x0000677C
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00008584 File Offset: 0x00006784
		[DoNotSerialize]
		[PortLabel("String")]
		public ValueOutput stringParameter { get; private set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000858D File Offset: 0x0000678D
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00008595 File Offset: 0x00006795
		[DoNotSerialize]
		[PortLabel("Float")]
		public ValueOutput floatParameter { get; private set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000859E File Offset: 0x0000679E
		// (set) Token: 0x06000324 RID: 804 RVA: 0x000085A6 File Offset: 0x000067A6
		[DoNotSerialize]
		[PortLabel("Integer")]
		public ValueOutput intParameter { get; private set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000085AF File Offset: 0x000067AF
		// (set) Token: 0x06000326 RID: 806 RVA: 0x000085B7 File Offset: 0x000067B7
		[DoNotSerialize]
		[PortLabel("Object")]
		public ValueOutput objectReferenceParameter { get; private set; }

		// Token: 0x06000327 RID: 807 RVA: 0x000085C0 File Offset: 0x000067C0
		protected override void Definition()
		{
			base.Definition();
			this.stringParameter = base.ValueOutput<string>("stringParameter");
			this.floatParameter = base.ValueOutput<float>("floatParameter");
			this.intParameter = base.ValueOutput<int>("intParameter");
			this.objectReferenceParameter = base.ValueOutput<Object>("objectReferenceParameter");
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00008618 File Offset: 0x00006818
		protected override void AssignArguments(Flow flow, AnimationEvent args)
		{
			flow.SetValue(this.stringParameter, args.stringParameter);
			flow.SetValue(this.floatParameter, args.floatParameter);
			flow.SetValue(this.intParameter, args.intParameter);
			flow.SetValue(this.objectReferenceParameter, args.objectReferenceParameter);
		}
	}
}
