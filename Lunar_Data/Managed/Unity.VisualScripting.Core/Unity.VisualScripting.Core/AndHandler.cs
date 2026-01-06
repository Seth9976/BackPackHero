using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000D9 RID: 217
	public class AndHandler : BinaryOperatorHandler
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x00010BA8 File Offset: 0x0000EDA8
		public AndHandler()
			: base("And", "And", "&", "op_BitwiseAnd")
		{
			base.Handle<bool, bool>((bool a, bool b) => a && b, false);
			base.Handle<byte, byte>((byte a, byte b) => (int)(a & b), false);
			base.Handle<byte, sbyte>((byte a, sbyte b) => (int)(a & (byte)b), false);
			base.Handle<byte, short>((byte a, short b) => (int)((short)a & b), false);
			base.Handle<byte, ushort>((byte a, ushort b) => (int)((ushort)a & b), false);
			base.Handle<byte, int>((byte a, int b) => (int)a & b, false);
			base.Handle<byte, uint>((byte a, uint b) => (uint)a & b, false);
			base.Handle<byte, long>((byte a, long b) => (long)((ulong)a & (ulong)b), false);
			base.Handle<byte, ulong>((byte a, ulong b) => (ulong)a & b, false);
			base.Handle<sbyte, byte>((sbyte a, byte b) => (int)(a & (sbyte)b), false);
			base.Handle<sbyte, sbyte>((sbyte a, sbyte b) => (int)(a & b), false);
			base.Handle<sbyte, short>((sbyte a, short b) => (int)((short)a & b), false);
			base.Handle<sbyte, ushort>((sbyte a, ushort b) => (int)((ushort)a & b), false);
			base.Handle<sbyte, int>((sbyte a, int b) => (int)a & b, false);
			base.Handle<sbyte, uint>((sbyte a, uint b) => (long)a & (long)((ulong)b), false);
			base.Handle<sbyte, long>((sbyte a, long b) => (long)a & b, false);
			base.Handle<short, byte>((short a, byte b) => (int)(a & (short)b), false);
			base.Handle<short, sbyte>((short a, sbyte b) => (int)(a & (short)b), false);
			base.Handle<short, short>((short a, short b) => (int)(a & b), false);
			base.Handle<short, ushort>((short a, ushort b) => (int)(a & (short)b), false);
			base.Handle<short, int>((short a, int b) => (int)a & b, false);
			base.Handle<short, uint>((short a, uint b) => (long)a & (long)((ulong)b), false);
			base.Handle<short, long>((short a, long b) => (long)a & b, false);
			base.Handle<ushort, byte>((ushort a, byte b) => (int)(a & (ushort)b), false);
			base.Handle<ushort, sbyte>((ushort a, sbyte b) => (int)(a & (ushort)b), false);
			base.Handle<ushort, short>((ushort a, short b) => (int)(a & (ushort)b), false);
			base.Handle<ushort, ushort>((ushort a, ushort b) => (int)(a & b), false);
			base.Handle<ushort, int>((ushort a, int b) => (int)a & b, false);
			base.Handle<ushort, uint>((ushort a, uint b) => (uint)a & b, false);
			base.Handle<ushort, long>((ushort a, long b) => (long)((ulong)a & (ulong)b), false);
			base.Handle<ushort, ulong>((ushort a, ulong b) => (ulong)a & b, false);
			base.Handle<int, byte>((int a, byte b) => a & (int)b, false);
			base.Handle<int, sbyte>((int a, sbyte b) => a & (int)b, false);
			base.Handle<int, short>((int a, short b) => a & (int)b, false);
			base.Handle<int, ushort>((int a, ushort b) => a & (int)b, false);
			base.Handle<int, int>((int a, int b) => a & b, false);
			base.Handle<int, uint>((int a, uint b) => (long)a & (long)((ulong)b), false);
			base.Handle<int, long>((int a, long b) => (long)a & b, false);
			base.Handle<uint, byte>((uint a, byte b) => a & (uint)b, false);
			base.Handle<uint, sbyte>((uint a, sbyte b) => (long)((ulong)a & (ulong)((long)b)), false);
			base.Handle<uint, short>((uint a, short b) => (long)((ulong)a & (ulong)((long)b)), false);
			base.Handle<uint, ushort>((uint a, ushort b) => a & (uint)b, false);
			base.Handle<uint, int>((uint a, int b) => (long)((ulong)a & (ulong)((long)b)), false);
			base.Handle<uint, uint>((uint a, uint b) => a & b, false);
			base.Handle<uint, long>((uint a, long b) => (long)((ulong)a & (ulong)b), false);
			base.Handle<uint, ulong>((uint a, ulong b) => (ulong)a & b, false);
			base.Handle<long, byte>((long a, byte b) => a & (long)((ulong)b), false);
			base.Handle<long, sbyte>((long a, sbyte b) => a & (long)b, false);
			base.Handle<long, short>((long a, short b) => a & (long)b, false);
			base.Handle<long, ushort>((long a, ushort b) => a & (long)((ulong)b), false);
			base.Handle<long, int>((long a, int b) => a & (long)b, false);
			base.Handle<long, uint>((long a, uint b) => a & (long)((ulong)b), false);
			base.Handle<long, long>((long a, long b) => a & b, false);
			base.Handle<ulong, byte>((ulong a, byte b) => a & (ulong)b, false);
			base.Handle<ulong, ushort>((ulong a, ushort b) => a & (ulong)b, false);
			base.Handle<ulong, uint>((ulong a, uint b) => a & (ulong)b, false);
			base.Handle<ulong, ulong>((ulong a, ulong b) => a & b, false);
		}
	}
}
