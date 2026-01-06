using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000F2 RID: 242
	public sealed class SubtractionHandler : BinaryOperatorHandler
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0001DABC File Offset: 0x0001BCBC
		public SubtractionHandler()
			: base("Subtraction", "Subtract", "-", "op_Subtraction")
		{
			base.Handle<byte, byte>((byte a, byte b) => (int)(a - b), false);
			base.Handle<byte, sbyte>((byte a, sbyte b) => (int)(a - (byte)b), false);
			base.Handle<byte, short>((byte a, short b) => (int)((short)a - b), false);
			base.Handle<byte, ushort>((byte a, ushort b) => (int)((ushort)a - b), false);
			base.Handle<byte, int>((byte a, int b) => (int)a - b, false);
			base.Handle<byte, uint>((byte a, uint b) => (uint)a - b, false);
			base.Handle<byte, long>((byte a, long b) => (long)((ulong)a - (ulong)b), false);
			base.Handle<byte, ulong>((byte a, ulong b) => (ulong)a - b, false);
			base.Handle<byte, float>((byte a, float b) => (float)a - b, false);
			base.Handle<byte, decimal>((byte a, decimal b) => a - b, false);
			base.Handle<byte, double>((byte a, double b) => (double)a - b, false);
			base.Handle<sbyte, byte>((sbyte a, byte b) => (int)(a - (sbyte)b), false);
			base.Handle<sbyte, sbyte>((sbyte a, sbyte b) => (int)(a - b), false);
			base.Handle<sbyte, short>((sbyte a, short b) => (int)((short)a - b), false);
			base.Handle<sbyte, ushort>((sbyte a, ushort b) => (int)((ushort)a - b), false);
			base.Handle<sbyte, int>((sbyte a, int b) => (int)a - b, false);
			base.Handle<sbyte, uint>((sbyte a, uint b) => (long)a - (long)((ulong)b), false);
			base.Handle<sbyte, long>((sbyte a, long b) => (long)a - b, false);
			base.Handle<sbyte, float>((sbyte a, float b) => (float)a - b, false);
			base.Handle<sbyte, decimal>((sbyte a, decimal b) => a - b, false);
			base.Handle<sbyte, double>((sbyte a, double b) => (double)a - b, false);
			base.Handle<short, byte>((short a, byte b) => (int)(a - (short)b), false);
			base.Handle<short, sbyte>((short a, sbyte b) => (int)(a - (short)b), false);
			base.Handle<short, short>((short a, short b) => (int)(a - b), false);
			base.Handle<short, ushort>((short a, ushort b) => (int)(a - (short)b), false);
			base.Handle<short, int>((short a, int b) => (int)a - b, false);
			base.Handle<short, uint>((short a, uint b) => (long)a - (long)((ulong)b), false);
			base.Handle<short, long>((short a, long b) => (long)a - b, false);
			base.Handle<short, float>((short a, float b) => (float)a - b, false);
			base.Handle<short, decimal>((short a, decimal b) => a - b, false);
			base.Handle<short, double>((short a, double b) => (double)a - b, false);
			base.Handle<ushort, byte>((ushort a, byte b) => (int)(a - (ushort)b), false);
			base.Handle<ushort, sbyte>((ushort a, sbyte b) => (int)(a - (ushort)b), false);
			base.Handle<ushort, short>((ushort a, short b) => (int)(a - (ushort)b), false);
			base.Handle<ushort, ushort>((ushort a, ushort b) => (int)(a - b), false);
			base.Handle<ushort, int>((ushort a, int b) => (int)a - b, false);
			base.Handle<ushort, uint>((ushort a, uint b) => (uint)a - b, false);
			base.Handle<ushort, long>((ushort a, long b) => (long)((ulong)a - (ulong)b), false);
			base.Handle<ushort, ulong>((ushort a, ulong b) => (ulong)a - b, false);
			base.Handle<ushort, float>((ushort a, float b) => (float)a - b, false);
			base.Handle<ushort, decimal>((ushort a, decimal b) => a - b, false);
			base.Handle<ushort, double>((ushort a, double b) => (double)a - b, false);
			base.Handle<int, byte>((int a, byte b) => a - (int)b, false);
			base.Handle<int, sbyte>((int a, sbyte b) => a - (int)b, false);
			base.Handle<int, short>((int a, short b) => a - (int)b, false);
			base.Handle<int, ushort>((int a, ushort b) => a - (int)b, false);
			base.Handle<int, int>((int a, int b) => a - b, false);
			base.Handle<int, uint>((int a, uint b) => (long)a - (long)((ulong)b), false);
			base.Handle<int, long>((int a, long b) => (long)a - b, false);
			base.Handle<int, float>((int a, float b) => (float)a - b, false);
			base.Handle<int, decimal>((int a, decimal b) => a - b, false);
			base.Handle<int, double>((int a, double b) => (double)a - b, false);
			base.Handle<uint, byte>((uint a, byte b) => a - (uint)b, false);
			base.Handle<uint, sbyte>((uint a, sbyte b) => (long)((ulong)a - (ulong)((long)b)), false);
			base.Handle<uint, short>((uint a, short b) => (long)((ulong)a - (ulong)((long)b)), false);
			base.Handle<uint, ushort>((uint a, ushort b) => a - (uint)b, false);
			base.Handle<uint, int>((uint a, int b) => (long)((ulong)a - (ulong)((long)b)), false);
			base.Handle<uint, uint>((uint a, uint b) => a - b, false);
			base.Handle<uint, long>((uint a, long b) => (long)((ulong)a - (ulong)b), false);
			base.Handle<uint, ulong>((uint a, ulong b) => (ulong)a - b, false);
			base.Handle<uint, float>((uint a, float b) => a - b, false);
			base.Handle<uint, decimal>((uint a, decimal b) => a - b, false);
			base.Handle<uint, double>((uint a, double b) => a - b, false);
			base.Handle<long, byte>((long a, byte b) => a - (long)((ulong)b), false);
			base.Handle<long, sbyte>((long a, sbyte b) => a - (long)b, false);
			base.Handle<long, short>((long a, short b) => a - (long)b, false);
			base.Handle<long, ushort>((long a, ushort b) => a - (long)((ulong)b), false);
			base.Handle<long, int>((long a, int b) => a - (long)b, false);
			base.Handle<long, uint>((long a, uint b) => a - (long)((ulong)b), false);
			base.Handle<long, long>((long a, long b) => a - b, false);
			base.Handle<long, float>((long a, float b) => (float)a - b, false);
			base.Handle<long, decimal>((long a, decimal b) => a - b, false);
			base.Handle<long, double>((long a, double b) => (double)a - b, false);
			base.Handle<ulong, byte>((ulong a, byte b) => a - (ulong)b, false);
			base.Handle<ulong, ushort>((ulong a, ushort b) => a - (ulong)b, false);
			base.Handle<ulong, uint>((ulong a, uint b) => a - (ulong)b, false);
			base.Handle<ulong, ulong>((ulong a, ulong b) => a - b, false);
			base.Handle<ulong, float>((ulong a, float b) => a - b, false);
			base.Handle<ulong, decimal>((ulong a, decimal b) => a - b, false);
			base.Handle<ulong, double>((ulong a, double b) => a - b, false);
			base.Handle<float, byte>((float a, byte b) => a - (float)b, false);
			base.Handle<float, sbyte>((float a, sbyte b) => a - (float)b, false);
			base.Handle<float, short>((float a, short b) => a - (float)b, false);
			base.Handle<float, ushort>((float a, ushort b) => a - (float)b, false);
			base.Handle<float, int>((float a, int b) => a - (float)b, false);
			base.Handle<float, uint>((float a, uint b) => a - b, false);
			base.Handle<float, long>((float a, long b) => a - (float)b, false);
			base.Handle<float, ulong>((float a, ulong b) => a - b, false);
			base.Handle<float, float>((float a, float b) => a - b, false);
			base.Handle<float, double>((float a, double b) => (double)a - b, false);
			base.Handle<decimal, byte>((decimal a, byte b) => a - b, false);
			base.Handle<decimal, sbyte>((decimal a, sbyte b) => a - b, false);
			base.Handle<decimal, short>((decimal a, short b) => a - b, false);
			base.Handle<decimal, ushort>((decimal a, ushort b) => a - b, false);
			base.Handle<decimal, int>((decimal a, int b) => a - b, false);
			base.Handle<decimal, uint>((decimal a, uint b) => a - b, false);
			base.Handle<decimal, long>((decimal a, long b) => a - b, false);
			base.Handle<decimal, ulong>((decimal a, ulong b) => a - b, false);
			base.Handle<decimal, decimal>((decimal a, decimal b) => a - b, false);
			base.Handle<double, byte>((double a, byte b) => a - (double)b, false);
			base.Handle<double, sbyte>((double a, sbyte b) => a - (double)b, false);
			base.Handle<double, short>((double a, short b) => a - (double)b, false);
			base.Handle<double, ushort>((double a, ushort b) => a - (double)b, false);
			base.Handle<double, int>((double a, int b) => a - (double)b, false);
			base.Handle<double, uint>((double a, uint b) => a - b, false);
			base.Handle<double, long>((double a, long b) => a - (double)b, false);
			base.Handle<double, ulong>((double a, ulong b) => a - b, false);
			base.Handle<double, float>((double a, float b) => a - (double)b, false);
			base.Handle<double, double>((double a, double b) => a - b, false);
		}
	}
}
