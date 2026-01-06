using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000E5 RID: 229
	public class LeftShiftHandler : BinaryOperatorHandler
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x000176F0 File Offset: 0x000158F0
		public LeftShiftHandler()
			: base("Left Shift", "Left Shift", "<<", "op_LeftShift")
		{
			base.Handle<byte, byte>((byte a, byte b) => (int)a << (int)b, false);
			base.Handle<byte, sbyte>((byte a, sbyte b) => (int)a << (int)b, false);
			base.Handle<byte, short>((byte a, short b) => (int)a << (int)b, false);
			base.Handle<byte, ushort>((byte a, ushort b) => (int)a << (int)b, false);
			base.Handle<byte, int>((byte a, int b) => (int)a << b, false);
			base.Handle<sbyte, byte>((sbyte a, byte b) => (int)a << (int)b, false);
			base.Handle<sbyte, sbyte>((sbyte a, sbyte b) => (int)a << (int)b, false);
			base.Handle<sbyte, short>((sbyte a, short b) => (int)a << (int)b, false);
			base.Handle<sbyte, ushort>((sbyte a, ushort b) => (int)a << (int)b, false);
			base.Handle<sbyte, int>((sbyte a, int b) => (int)a << b, false);
			base.Handle<short, byte>((short a, byte b) => (int)a << (int)b, false);
			base.Handle<short, sbyte>((short a, sbyte b) => (int)a << (int)b, false);
			base.Handle<short, short>((short a, short b) => (int)a << (int)b, false);
			base.Handle<short, ushort>((short a, ushort b) => (int)a << (int)b, false);
			base.Handle<short, int>((short a, int b) => (int)a << b, false);
			base.Handle<ushort, byte>((ushort a, byte b) => (int)a << (int)b, false);
			base.Handle<ushort, sbyte>((ushort a, sbyte b) => (int)a << (int)b, false);
			base.Handle<ushort, short>((ushort a, short b) => (int)a << (int)b, false);
			base.Handle<ushort, ushort>((ushort a, ushort b) => (int)a << (int)b, false);
			base.Handle<ushort, int>((ushort a, int b) => (int)a << b, false);
			base.Handle<int, byte>((int a, byte b) => a << (int)b, false);
			base.Handle<int, sbyte>((int a, sbyte b) => a << (int)b, false);
			base.Handle<int, short>((int a, short b) => a << (int)b, false);
			base.Handle<int, ushort>((int a, ushort b) => a << (int)b, false);
			base.Handle<int, int>((int a, int b) => a << b, false);
			base.Handle<uint, byte>((uint a, byte b) => a << (int)b, false);
			base.Handle<uint, sbyte>((uint a, sbyte b) => a << (int)b, false);
			base.Handle<uint, short>((uint a, short b) => a << (int)b, false);
			base.Handle<uint, ushort>((uint a, ushort b) => a << (int)b, false);
			base.Handle<uint, int>((uint a, int b) => a << b, false);
			base.Handle<long, byte>((long a, byte b) => a << (int)b, false);
			base.Handle<long, sbyte>((long a, sbyte b) => a << (int)b, false);
			base.Handle<long, short>((long a, short b) => a << (int)b, false);
			base.Handle<long, ushort>((long a, ushort b) => a << (int)b, false);
			base.Handle<long, int>((long a, int b) => a << b, false);
			base.Handle<ulong, byte>((ulong a, byte b) => a << (int)b, false);
			base.Handle<ulong, sbyte>((ulong a, sbyte b) => a << (int)b, false);
			base.Handle<ulong, short>((ulong a, short b) => a << (int)b, false);
			base.Handle<ulong, ushort>((ulong a, ushort b) => a << (int)b, false);
			base.Handle<ulong, int>((ulong a, int b) => a << b, false);
		}
	}
}
