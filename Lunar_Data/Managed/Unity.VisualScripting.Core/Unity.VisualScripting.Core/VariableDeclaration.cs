using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200016D RID: 365
	[SerializationVersion("A", new Type[] { })]
	public sealed class VariableDeclaration
	{
		// Token: 0x060009BC RID: 2492 RVA: 0x000294E3 File Offset: 0x000276E3
		[Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public VariableDeclaration()
		{
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000294EB File Offset: 0x000276EB
		public VariableDeclaration(string name, object value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00029501 File Offset: 0x00027701
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x00029509 File Offset: 0x00027709
		[Serialize]
		public string name { get; private set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00029512 File Offset: 0x00027712
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x0002951A File Offset: 0x0002771A
		[Serialize]
		[Value]
		public object value { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00029523 File Offset: 0x00027723
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x0002952B File Offset: 0x0002772B
		[Serialize]
		public SerializableType typeHandle { get; set; }
	}
}
