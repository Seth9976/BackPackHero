using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000F0 RID: 240
	public sealed class PlusHandler : UnaryOperatorHandler
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0001D2E4 File Offset: 0x0001B4E4
		public PlusHandler()
			: base("Plus", "Plus", "+", "op_UnaryPlus")
		{
			base.Handle<byte>((byte a) => (int)a);
			base.Handle<sbyte>((sbyte a) => (int)a);
			base.Handle<short>((short a) => (int)a);
			base.Handle<ushort>((ushort a) => (int)a);
			base.Handle<int>((int a) => a);
			base.Handle<uint>((uint a) => a);
			base.Handle<long>((long a) => a);
			base.Handle<ulong>((ulong a) => a);
			base.Handle<float>((float a) => a);
			base.Handle<decimal>((decimal a) => a);
			base.Handle<double>((double a) => a);
		}
	}
}
