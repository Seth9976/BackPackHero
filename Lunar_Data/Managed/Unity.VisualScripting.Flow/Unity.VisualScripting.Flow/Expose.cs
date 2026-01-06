using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200001A RID: 26
	[SpecialUnit]
	public sealed class Expose : Unit, IAotStubbable
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00003B18 File Offset: 0x00001D18
		public Expose()
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003B2E File Offset: 0x00001D2E
		public Expose(Type type)
		{
			this.type = type;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003B4B File Offset: 0x00001D4B
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00003B53 File Offset: 0x00001D53
		[Serialize]
		[Inspectable]
		[TypeFilter(new Type[] { }, Enums = false)]
		public Type type { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003B5C File Offset: 0x00001D5C
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00003B64 File Offset: 0x00001D64
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable("Instance")]
		[InspectorToggleLeft]
		public bool instance { get; set; } = true;

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003B6D File Offset: 0x00001D6D
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00003B75 File Offset: 0x00001D75
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable("Static")]
		[InspectorToggleLeft]
		public bool @static { get; set; } = true;

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003B7E File Offset: 0x00001D7E
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00003B86 File Offset: 0x00001D86
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput target { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003B8F File Offset: 0x00001D8F
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00003B97 File Offset: 0x00001D97
		[DoNotSerialize]
		public Dictionary<ValueOutput, Member> members { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public override bool canDefine
		{
			get
			{
				return this.type != null;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003BAE File Offset: 0x00001DAE
		public override IEnumerable<object> GetAotStubs(HashSet<object> visited)
		{
			if (this.members != null)
			{
				foreach (Member member in this.members.Values)
				{
					if (member != null && member.isReflected)
					{
						yield return member.info;
					}
				}
				Dictionary<ValueOutput, Member>.ValueCollection.Enumerator enumerator = default(Dictionary<ValueOutput, Member>.ValueCollection.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003BC0 File Offset: 0x00001DC0
		protected override void Definition()
		{
			this.members = new Dictionary<ValueOutput, Member>();
			bool flag = false;
			using (IEnumerator<Member> enumerator = (from m in this.type.GetMembers()
				where m is FieldInfo || m is PropertyInfo
				select m.ToManipulator(this.type)).DistinctBy((Member m) => m.name).Where(new Func<Member, bool>(this.Include)).OrderBy(delegate(Member m)
			{
				if (!m.requiresTarget)
				{
					return 1;
				}
				return 0;
			})
				.ThenBy((Member m) => m.order)
				.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Member member = enumerator.Current;
					ValueOutput valueOutput = base.ValueOutput(member.type, member.name, (Flow flow) => this.GetValue(flow, member));
					if (member.isPredictable)
					{
						valueOutput.Predictable();
					}
					this.members.Add(valueOutput, member);
					if (member.requiresTarget)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.target = base.ValueInput(this.type, "target").NullMeansSelf();
				this.target.SetDefaultValue(this.type.PseudoDefault());
				foreach (ValueOutput valueOutput2 in this.members.Keys)
				{
					if (this.members[valueOutput2].requiresTarget)
					{
						base.Requirement(this.target, valueOutput2);
					}
				}
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003DD8 File Offset: 0x00001FD8
		private bool Include(Member member)
		{
			return (this.instance || !member.requiresTarget) && (this.@static || member.requiresTarget) && member.isPubliclyGettable && !member.info.HasAttribute(true) && !member.isIndexer && (!(member.name == "runInEditMode") || !(member.declaringType == typeof(MonoBehaviour)));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003E5C File Offset: 0x0000205C
		private object GetValue(Flow flow, Member member)
		{
			object obj = (member.requiresTarget ? flow.GetValue(this.target, member.targetType) : null);
			return member.Get(obj);
		}
	}
}
