using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200001D RID: 29
	[SpecialUnit]
	public abstract class MemberUnit : Unit, IAotStubbable
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00004BBE File Offset: 0x00002DBE
		protected MemberUnit()
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004BC6 File Offset: 0x00002DC6
		protected MemberUnit(Member member)
			: this()
		{
			this.member = member;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004BD5 File Offset: 0x00002DD5
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00004BDD File Offset: 0x00002DDD
		[Serialize]
		[MemberFilter(Fields = true, Properties = true, Methods = true, Constructors = true)]
		public Member member { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00004BE6 File Offset: 0x00002DE6
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00004BEE File Offset: 0x00002DEE
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput target { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00004BF7 File Offset: 0x00002DF7
		public override bool canDefine
		{
			get
			{
				return this.member != null;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004C08 File Offset: 0x00002E08
		protected override void Definition()
		{
			this.member.EnsureReflected();
			if (!this.IsMemberValid(this.member))
			{
				throw new NotSupportedException("The member type is not valid for this unit.");
			}
			if (this.member.requiresTarget)
			{
				this.target = base.ValueInput(this.member.targetType, "target");
				this.target.SetDefaultValue(this.member.targetType.PseudoDefault());
				if (typeof(Object).IsAssignableFrom(this.member.targetType))
				{
					this.target.NullMeansSelf();
				}
			}
		}

		// Token: 0x0600011B RID: 283
		protected abstract bool IsMemberValid(Member member);

		// Token: 0x0600011C RID: 284 RVA: 0x00004CA5 File Offset: 0x00002EA5
		public override void Prewarm()
		{
			if (this.member != null && this.member.isReflected)
			{
				this.member.Prewarm();
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004CCD File Offset: 0x00002ECD
		public override IEnumerable<object> GetAotStubs(HashSet<object> visited)
		{
			if (this.member != null && this.member.isReflected)
			{
				yield return this.member.info;
			}
			yield break;
		}
	}
}
