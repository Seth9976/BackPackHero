using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000F1 RID: 241
	public class RightShiftHandler : BinaryOperatorHandler
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
		public RightShiftHandler()
			: base("Right Shift", "Right Shift", ">>", "op_RightShift")
		{
			base.Handle<byte, byte>((byte a, byte b) => a >> (int)b, false);
			base.Handle<byte, sbyte>((byte a, sbyte b) => a >> (int)b, false);
			base.Handle<byte, short>((byte a, short b) => a >> (int)b, false);
			base.Handle<byte, ushort>((byte a, ushort b) => a >> (int)b, false);
			base.Handle<byte, int>((byte a, int b) => a >> b, false);
			base.Handle<sbyte, byte>((sbyte a, byte b) => a >> (int)b, false);
			base.Handle<sbyte, sbyte>((sbyte a, sbyte b) => a >> (int)b, false);
			base.Handle<sbyte, short>((sbyte a, short b) => a >> (int)b, false);
			base.Handle<sbyte, ushort>((sbyte a, ushort b) => a >> (int)b, false);
			base.Handle<sbyte, int>((sbyte a, int b) => a >> b, false);
			base.Handle<short, byte>((short a, byte b) => a >> (int)b, false);
			base.Handle<short, sbyte>((short a, sbyte b) => a >> (int)b, false);
			base.Handle<short, short>((short a, short b) => a >> (int)b, false);
			base.Handle<short, ushort>((short a, ushort b) => a >> (int)b, false);
			base.Handle<short, int>((short a, int b) => a >> b, false);
			base.Handle<ushort, byte>((ushort a, byte b) => a >> (int)b, false);
			base.Handle<ushort, sbyte>((ushort a, sbyte b) => a >> (int)b, false);
			base.Handle<ushort, short>((ushort a, short b) => a >> (int)b, false);
			base.Handle<ushort, ushort>((ushort a, ushort b) => a >> (int)b, false);
			base.Handle<ushort, int>((ushort a, int b) => a >> b, false);
			base.Handle<int, byte>((int a, byte b) => a >> (int)b, false);
			base.Handle<int, sbyte>((int a, sbyte b) => a >> (int)b, false);
			base.Handle<int, short>((int a, short b) => a >> (int)b, false);
			base.Handle<int, ushort>((int a, ushort b) => a >> (int)b, false);
			base.Handle<int, int>((int a, int b) => a >> b, false);
			base.Handle<uint, byte>((uint a, byte b) => a >> (int)b, false);
			base.Handle<uint, sbyte>((uint a, sbyte b) => a >> (int)b, false);
			base.Handle<uint, short>((uint a, short b) => a >> (int)b, false);
			base.Handle<uint, ushort>((uint a, ushort b) => a >> (int)b, false);
			base.Handle<uint, int>((uint a, int b) => a >> b, false);
			base.Handle<long, byte>((long a, byte b) => a >> (int)b, false);
			base.Handle<long, sbyte>((long a, sbyte b) => a >> (int)b, false);
			base.Handle<long, short>((long a, short b) => a >> (int)b, false);
			base.Handle<long, ushort>((long a, ushort b) => a >> (int)b, false);
			base.Handle<long, int>((long a, int b) => a >> b, false);
			base.Handle<ulong, byte>((ulong a, byte b) => a >> (int)b, false);
			base.Handle<ulong, sbyte>((ulong a, sbyte b) => a >> (int)b, false);
			base.Handle<ulong, short>((ulong a, short b) => a >> (int)b, false);
			base.Handle<ulong, ushort>((ulong a, ushort b) => a >> (int)b, false);
			base.Handle<ulong, int>((ulong a, int b) => a >> b, false);
		}
	}
}
