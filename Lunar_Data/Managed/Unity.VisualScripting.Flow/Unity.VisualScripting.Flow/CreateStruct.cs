using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000019 RID: 25
	[SpecialUnit]
	public sealed class CreateStruct : Unit
	{
		// Token: 0x060000CD RID: 205 RVA: 0x000039E3 File Offset: 0x00001BE3
		[Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public CreateStruct()
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000039EB File Offset: 0x00001BEB
		public CreateStruct(Type type)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			if (!type.IsStruct())
			{
				throw new ArgumentException(string.Format("Type {0} must be a struct.", type), "type");
			}
			this.type = type;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003A28 File Offset: 0x00001C28
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00003A30 File Offset: 0x00001C30
		[Serialize]
		public Type type { get; internal set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003A39 File Offset: 0x00001C39
		public override bool canDefine
		{
			get
			{
				return this.type != null;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003A47 File Offset: 0x00001C47
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00003A4F File Offset: 0x00001C4F
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003A58 File Offset: 0x00001C58
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00003A60 File Offset: 0x00001C60
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003A69 File Offset: 0x00001C69
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003A71 File Offset: 0x00001C71
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput output { get; private set; }

		// Token: 0x060000D8 RID: 216 RVA: 0x00003A7C File Offset: 0x00001C7C
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Enter));
			this.exit = base.ControlOutput("exit");
			this.output = base.ValueOutput(this.type, "output", new Func<Flow, object>(this.Create));
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003AEC File Offset: 0x00001CEC
		private ControlOutput Enter(Flow flow)
		{
			flow.SetValue(this.output, Activator.CreateInstance(this.type));
			return this.exit;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003B0B File Offset: 0x00001D0B
		private object Create(Flow flow)
		{
			return Activator.CreateInstance(this.type);
		}
	}
}
