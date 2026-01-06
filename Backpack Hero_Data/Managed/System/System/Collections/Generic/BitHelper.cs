using System;

namespace System.Collections.Generic
{
	// Token: 0x020007DA RID: 2010
	internal sealed class BitHelper
	{
		// Token: 0x06004013 RID: 16403 RVA: 0x000DFCC9 File Offset: 0x000DDEC9
		internal unsafe BitHelper(int* bitArrayPtr, int length)
		{
			this._arrayPtr = bitArrayPtr;
			this._length = length;
			this._useStackAlloc = true;
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x000DFCE6 File Offset: 0x000DDEE6
		internal BitHelper(int[] bitArray, int length)
		{
			this._array = bitArray;
			this._length = length;
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x000DFCFC File Offset: 0x000DDEFC
		internal unsafe void MarkBit(int bitPosition)
		{
			int num = bitPosition / 32;
			if (num < this._length && num >= 0)
			{
				int num2 = 1 << bitPosition % 32;
				if (this._useStackAlloc)
				{
					this._arrayPtr[num] |= num2;
					return;
				}
				this._array[num] |= num2;
			}
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x000DFD50 File Offset: 0x000DDF50
		internal unsafe bool IsMarked(int bitPosition)
		{
			int num = bitPosition / 32;
			if (num >= this._length || num < 0)
			{
				return false;
			}
			int num2 = 1 << bitPosition % 32;
			if (this._useStackAlloc)
			{
				return (this._arrayPtr[num] & num2) != 0;
			}
			return (this._array[num] & num2) != 0;
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x000DFDA2 File Offset: 0x000DDFA2
		internal static int ToIntArrayLength(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / 32 + 1;
		}

		// Token: 0x040026BD RID: 9917
		private const byte MarkedBitFlag = 1;

		// Token: 0x040026BE RID: 9918
		private const byte IntSize = 32;

		// Token: 0x040026BF RID: 9919
		private readonly int _length;

		// Token: 0x040026C0 RID: 9920
		private unsafe readonly int* _arrayPtr;

		// Token: 0x040026C1 RID: 9921
		private readonly int[] _array;

		// Token: 0x040026C2 RID: 9922
		private readonly bool _useStackAlloc;
	}
}
