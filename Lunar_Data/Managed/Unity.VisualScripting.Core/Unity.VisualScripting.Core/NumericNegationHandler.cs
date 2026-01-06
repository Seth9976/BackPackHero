using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000EB RID: 235
	public sealed class NumericNegationHandler : UnaryOperatorHandler
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public NumericNegationHandler()
			: base("Numeric Negation", "Negate", "-", "op_UnaryNegation")
		{
			base.Handle<byte>((byte a) => (int)(-(int)a));
			base.Handle<sbyte>((sbyte a) => (int)(-(int)a));
			base.Handle<short>((short a) => (int)(-(int)a));
			base.Handle<ushort>((ushort a) => (int)(-(int)a));
			base.Handle<int>((int a) => -a);
			base.Handle<uint>((uint a) => (long)(-(long)((ulong)a)));
			base.Handle<long>((long a) => -a);
			base.Handle<float>((float a) => -a);
			base.Handle<decimal>((decimal a) => -a);
			base.Handle<double>((double a) => -a);
		}
	}
}
