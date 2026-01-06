using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200001E RID: 30
	public sealed class SetMember : MemberUnit
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00004CDD File Offset: 0x00002EDD
		public SetMember()
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004CE5 File Offset: 0x00002EE5
		public SetMember(Member member)
			: base(member)
		{
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004CEE File Offset: 0x00002EEE
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00004CF6 File Offset: 0x00002EF6
		[Serialize]
		[InspectableIf("supportsChaining")]
		public bool chainable { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004CFF File Offset: 0x00002EFF
		[DoNotSerialize]
		public bool supportsChaining
		{
			get
			{
				return base.member.requiresTarget;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004D0C File Offset: 0x00002F0C
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00004D14 File Offset: 0x00002F14
		[DoNotSerialize]
		[MemberFilter(Fields = true, Properties = true, ReadOnly = false)]
		public Member setter
		{
			get
			{
				return base.member;
			}
			set
			{
				base.member = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004D1D File Offset: 0x00002F1D
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00004D25 File Offset: 0x00002F25
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput assign { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004D2E File Offset: 0x00002F2E
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00004D36 File Offset: 0x00002F36
		[DoNotSerialize]
		[PortLabel("Value")]
		[PortLabelHidden]
		public ValueInput input { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00004D3F File Offset: 0x00002F3F
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00004D47 File Offset: 0x00002F47
		[DoNotSerialize]
		[PortLabel("Value")]
		[PortLabelHidden]
		public ValueOutput output { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00004D50 File Offset: 0x00002F50
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00004D58 File Offset: 0x00002F58
		[DoNotSerialize]
		[PortLabel("Target")]
		[PortLabelHidden]
		public ValueOutput targetOutput { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00004D61 File Offset: 0x00002F61
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00004D69 File Offset: 0x00002F69
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput assigned { get; private set; }

		// Token: 0x0600012F RID: 303 RVA: 0x00004D74 File Offset: 0x00002F74
		protected override void Definition()
		{
			base.Definition();
			this.assign = base.ControlInput("assign", new Func<Flow, ControlOutput>(this.Assign));
			this.assigned = base.ControlOutput("assigned");
			base.Succession(this.assign, this.assigned);
			if (this.supportsChaining && this.chainable)
			{
				this.targetOutput = base.ValueOutput(base.member.targetType, "targetOutput");
				base.Assignment(this.assign, this.targetOutput);
			}
			this.output = base.ValueOutput(base.member.type, "output");
			base.Assignment(this.assign, this.output);
			if (base.member.requiresTarget)
			{
				base.Requirement(base.target, this.assign);
			}
			this.input = base.ValueInput(base.member.type, "input");
			base.Requirement(this.input, this.assign);
			if (base.member.allowsNull)
			{
				this.input.AllowsNull();
			}
			this.input.SetDefaultValue(base.member.type.PseudoDefault());
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004EB4 File Offset: 0x000030B4
		protected override bool IsMemberValid(Member member)
		{
			return member.isAccessor && member.isSettable;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004EC8 File Offset: 0x000030C8
		private object GetAndChainTarget(Flow flow)
		{
			if (base.member.requiresTarget)
			{
				object value = flow.GetValue(base.target, base.member.targetType);
				if (this.supportsChaining && this.chainable)
				{
					flow.SetValue(this.targetOutput, value);
				}
				return value;
			}
			return null;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004F1C File Offset: 0x0000311C
		private ControlOutput Assign(Flow flow)
		{
			object andChainTarget = this.GetAndChainTarget(flow);
			object convertedValue = flow.GetConvertedValue(this.input);
			flow.SetValue(this.output, base.member.Set(andChainTarget, convertedValue));
			return this.assigned;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004F60 File Offset: 0x00003160
		public override AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			AnalyticsIdentifier analyticsIdentifier = new AnalyticsIdentifier();
			analyticsIdentifier.Identifier = base.member.targetType.FullName + "." + base.member.name + "(Set)";
			analyticsIdentifier.Namespace = base.member.targetType.Namespace;
			analyticsIdentifier.Hashcode = analyticsIdentifier.Identifier.GetHashCode();
			return analyticsIdentifier;
		}
	}
}
