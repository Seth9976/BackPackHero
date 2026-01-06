using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200001B RID: 27
	public sealed class GetMember : MemberUnit
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00003E9C File Offset: 0x0000209C
		public GetMember()
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003EA4 File Offset: 0x000020A4
		public GetMember(Member member)
			: base(member)
		{
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003EAD File Offset: 0x000020AD
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00003EB5 File Offset: 0x000020B5
		[DoNotSerialize]
		[MemberFilter(Fields = true, Properties = true, WriteOnly = false)]
		public Member getter
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003EBE File Offset: 0x000020BE
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00003EC6 File Offset: 0x000020C6
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput value { get; private set; }

		// Token: 0x060000F3 RID: 243 RVA: 0x00003ED0 File Offset: 0x000020D0
		protected override void Definition()
		{
			base.Definition();
			this.value = base.ValueOutput(base.member.type, "value", new Func<Flow, object>(this.Value));
			if (base.member.isPredictable)
			{
				this.value.Predictable();
			}
			if (base.member.requiresTarget)
			{
				base.Requirement(base.target, this.value);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003F43 File Offset: 0x00002143
		protected override bool IsMemberValid(Member member)
		{
			return member.isAccessor && member.isGettable;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003F58 File Offset: 0x00002158
		private object Value(Flow flow)
		{
			object obj = (base.member.requiresTarget ? flow.GetValue(base.target, base.member.targetType) : null);
			return base.member.Get(obj);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003F9C File Offset: 0x0000219C
		public override AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			AnalyticsIdentifier analyticsIdentifier = new AnalyticsIdentifier();
			analyticsIdentifier.Identifier = base.member.targetType.FullName + "." + base.member.name + "(Get)";
			analyticsIdentifier.Namespace = base.member.targetType.Namespace;
			analyticsIdentifier.Hashcode = analyticsIdentifier.Identifier.GetHashCode();
			return analyticsIdentifier;
		}
	}
}
