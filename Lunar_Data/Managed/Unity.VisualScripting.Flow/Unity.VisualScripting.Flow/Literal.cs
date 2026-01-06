using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000B3 RID: 179
	[SpecialUnit]
	public sealed class Literal : Unit
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0000AE85 File Offset: 0x00009085
		[Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public Literal()
		{
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000AE8D File Offset: 0x0000908D
		public Literal(Type type)
			: this(type, type.PseudoDefault())
		{
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000AE9C File Offset: 0x0000909C
		public Literal(Type type, object value)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			Ensure.That("value").IsOfType<object>(value, type);
			this.type = type;
			this.value = value;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000AED3 File Offset: 0x000090D3
		public override bool canDefine
		{
			get
			{
				return this.type != null;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000AEE1 File Offset: 0x000090E1
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0000AEE9 File Offset: 0x000090E9
		[Serialize]
		public Type type { get; internal set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000AEF2 File Offset: 0x000090F2
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0000AEFA File Offset: 0x000090FA
		[DoNotSerialize]
		public object value
		{
			get
			{
				return this._value;
			}
			set
			{
				Ensure.That("value").IsOfType<object>(value, this.type);
				this._value = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000AF19 File Offset: 0x00009119
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0000AF21 File Offset: 0x00009121
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput output { get; private set; }

		// Token: 0x06000539 RID: 1337 RVA: 0x0000AF2A File Offset: 0x0000912A
		protected override void Definition()
		{
			this.output = base.ValueOutput(this.type, "output", (Flow flow) => this.value).Predictable();
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000AF54 File Offset: 0x00009154
		public override AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			AnalyticsIdentifier analyticsIdentifier = new AnalyticsIdentifier();
			analyticsIdentifier.Identifier = base.GetType().FullName + "(" + this.type.Name + ")";
			analyticsIdentifier.Namespace = this.type.Namespace;
			analyticsIdentifier.Hashcode = analyticsIdentifier.Identifier.GetHashCode();
			return analyticsIdentifier;
		}

		// Token: 0x04000151 RID: 337
		[SerializeAs("value")]
		private object _value;
	}
}
