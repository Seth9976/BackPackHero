using System;
using System.ComponentModel;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200004E RID: 78
	[UnitCategory("Events/Animation")]
	[UnitShortTitle("Animation Event")]
	[UnitTitle("Named Animation Event")]
	[TypeIcon(typeof(Animation))]
	[DisplayName("Visual Scripting Named Animation Event")]
	public sealed class BoltNamedAnimationEvent : MachineEventUnit<AnimationEvent>
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000867F File Offset: 0x0000687F
		protected override string hookName
		{
			get
			{
				return "AnimationEvent";
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00008686 File Offset: 0x00006886
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000868E File Offset: 0x0000688E
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput name { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00008697 File Offset: 0x00006897
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000869F File Offset: 0x0000689F
		[DoNotSerialize]
		[PortLabel("Float")]
		public ValueOutput floatParameter { get; private set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600032F RID: 815 RVA: 0x000086A8 File Offset: 0x000068A8
		// (set) Token: 0x06000330 RID: 816 RVA: 0x000086B0 File Offset: 0x000068B0
		[DoNotSerialize]
		[PortLabel("Integer")]
		public ValueOutput intParameter { get; private set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000331 RID: 817 RVA: 0x000086B9 File Offset: 0x000068B9
		// (set) Token: 0x06000332 RID: 818 RVA: 0x000086C1 File Offset: 0x000068C1
		[DoNotSerialize]
		[PortLabel("Object")]
		public ValueOutput objectReferenceParameter { get; private set; }

		// Token: 0x06000333 RID: 819 RVA: 0x000086CC File Offset: 0x000068CC
		protected override void Definition()
		{
			base.Definition();
			this.name = base.ValueInput<string>("name", string.Empty);
			this.floatParameter = base.ValueOutput<float>("floatParameter");
			this.intParameter = base.ValueOutput<int>("intParameter");
			this.objectReferenceParameter = base.ValueOutput<GameObject>("objectReferenceParameter");
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00008728 File Offset: 0x00006928
		protected override bool ShouldTrigger(Flow flow, AnimationEvent animationEvent)
		{
			return EventUnit<AnimationEvent>.CompareNames(flow, this.name, animationEvent.stringParameter);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000873C File Offset: 0x0000693C
		protected override void AssignArguments(Flow flow, AnimationEvent animationEvent)
		{
			flow.SetValue(this.floatParameter, animationEvent.floatParameter);
			flow.SetValue(this.intParameter, animationEvent.intParameter);
			flow.SetValue(this.objectReferenceParameter, animationEvent.objectReferenceParameter);
		}
	}
}
