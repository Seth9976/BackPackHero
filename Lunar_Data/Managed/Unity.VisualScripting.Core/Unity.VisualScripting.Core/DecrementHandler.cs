using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000DC RID: 220
	public sealed class DecrementHandler : UnaryOperatorHandler
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00011814 File Offset: 0x0000FA14
		public DecrementHandler()
			: base("Decrement", "Decrement", "--", "op_Decrement")
		{
			base.Handle<byte>((byte a) => a -= 1);
			base.Handle<sbyte>((sbyte a) => a -= 1);
			base.Handle<short>((short a) => a -= 1);
			base.Handle<ushort>((ushort a) => a -= 1);
			base.Handle<int>((int a) => --a);
			base.Handle<uint>((uint a) => a -= 1U);
			base.Handle<long>((long a) => a -= 1L);
			base.Handle<ulong>((ulong a) => a -= 1UL);
			base.Handle<float>((float a) => a -= 1f);
			base.Handle<decimal>((decimal a) => a -= 1m);
			base.Handle<double>((double a) => a -= 1.0);
		}
	}
}
