using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000A6 RID: 166
	[BurstCompatible]
	[Obsolete("HeapString has been removed and replaced with NativeText (RemovedAfter 2021-07-21) (UnityUpgradable) -> NativeText", false)]
	public struct HeapString : INativeList<byte>, IIndexable<byte>, IDisposable, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<HeapString>, IEquatable<HeapString>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00014BA5 File Offset: 0x00012DA5
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00014BB4 File Offset: 0x00012DB4
		public int Length
		{
			get
			{
				return this.m_Data.Length - 1;
			}
			set
			{
				this.m_Data.Resize(value + 1, NativeArrayOptions.UninitializedMemory);
				this.m_Data[value] = 0;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00014BD2 File Offset: 0x00012DD2
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00014BE1 File Offset: 0x00012DE1
		public int Capacity
		{
			get
			{
				return this.m_Data.Capacity - 1;
			}
			set
			{
				this.m_Data.Capacity = value + 1;
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00014BF1 File Offset: 0x00012DF1
		public bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			this.Length = newLength;
			return true;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00014BFB File Offset: 0x00012DFB
		public bool IsEmpty
		{
			get
			{
				return this.m_Data.Length == 1;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00014C0B File Offset: 0x00012E0B
		public bool IsCreated
		{
			get
			{
				return this.m_Data.IsCreated;
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00014C18 File Offset: 0x00012E18
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)this.m_Data.GetUnsafePtr<byte>();
		}

		// Token: 0x170000AD RID: 173
		public byte this[int index]
		{
			get
			{
				return this.m_Data[index];
			}
			set
			{
				this.m_Data[index] = value;
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00014C42 File Offset: 0x00012E42
		public ref byte ElementAt(int index)
		{
			return this.m_Data.ElementAt(index);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00014C50 File Offset: 0x00012E50
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00014C5C File Offset: 0x00012E5C
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00014C82 File Offset: 0x00012E82
		public int CompareTo(HeapString other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00014C8C File Offset: 0x00012E8C
		public bool Equals(HeapString other)
		{
			return (ref this).Equals(in other);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00014C96 File Offset: 0x00012E96
		public void Dispose()
		{
			this.m_Data.Dispose();
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00014CA3 File Offset: 0x00012EA3
		[CreateProperty]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[NotBurstCompatible]
		public string Value
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00014CB1 File Offset: 0x00012EB1
		public HeapString.Enumerator GetEnumerator()
		{
			return new HeapString.Enumerator(this);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00014CBE File Offset: 0x00012EBE
		[NotBurstCompatible]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00014CD2 File Offset: 0x00012ED2
		[NotBurstCompatible]
		public bool Equals(string other)
		{
			return this.ToString().Equals(other);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00014CE8 File Offset: 0x00012EE8
		[NotBurstCompatible]
		public unsafe HeapString(string source, Allocator allocator)
		{
			this.m_Data = new NativeList<byte>(source.Length * 2 + 1, allocator);
			this.Length = source.Length * 2;
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num;
				if (UTF8ArrayUnsafeUtility.Copy(this.GetUnsafePtr(), out num, this.Capacity, ptr, source.Length) != CopyError.None)
				{
					this.m_Data.Dispose();
					this.m_Data = default(NativeList<byte>);
				}
				this.Length = num;
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00014D6A File Offset: 0x00012F6A
		public HeapString(int capacity, Allocator allocator)
		{
			this.m_Data = new NativeList<byte>(capacity + 1, allocator);
			this.Length = 0;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00014D87 File Offset: 0x00012F87
		public HeapString(Allocator allocator)
		{
			this.m_Data = new NativeList<byte>(129, allocator);
			this.Length = 0;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00014DA6 File Offset: 0x00012FA6
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00014DB0 File Offset: 0x00012FB0
		public unsafe HeapString(in FixedString32Bytes source, Allocator allocator)
		{
			this.m_Data = new NativeList<byte>((int)(source.utf8LengthInBytes + 1), allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in source.bytes);
			byte* unsafePtr = (byte*)this.m_Data.GetUnsafePtr<byte>();
			UnsafeUtility.MemCpy((void*)unsafePtr, (void*)ptr, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00014E08 File Offset: 0x00013008
		public unsafe static bool operator ==(in HeapString a, in FixedString32Bytes b)
		{
			HeapString heapString = *UnsafeUtilityExtensions.AsRef<HeapString>(in a);
			int length = heapString.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = heapString.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00014E4B File Offset: 0x0001304B
		public static bool operator !=(in HeapString a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00014E57 File Offset: 0x00013057
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00014E61 File Offset: 0x00013061
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00014E6C File Offset: 0x0001306C
		public unsafe HeapString(in FixedString64Bytes source, Allocator allocator)
		{
			this.m_Data = new NativeList<byte>((int)(source.utf8LengthInBytes + 1), allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in source.bytes);
			byte* unsafePtr = (byte*)this.m_Data.GetUnsafePtr<byte>();
			UnsafeUtility.MemCpy((void*)unsafePtr, (void*)ptr, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00014EC4 File Offset: 0x000130C4
		public unsafe static bool operator ==(in HeapString a, in FixedString64Bytes b)
		{
			HeapString heapString = *UnsafeUtilityExtensions.AsRef<HeapString>(in a);
			int length = heapString.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = heapString.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00014F07 File Offset: 0x00013107
		public static bool operator !=(in HeapString a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00014F13 File Offset: 0x00013113
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00014F1D File Offset: 0x0001311D
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00014F28 File Offset: 0x00013128
		public unsafe HeapString(in FixedString128Bytes source, Allocator allocator)
		{
			this.m_Data = new NativeList<byte>((int)(source.utf8LengthInBytes + 1), allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in source.bytes);
			byte* unsafePtr = (byte*)this.m_Data.GetUnsafePtr<byte>();
			UnsafeUtility.MemCpy((void*)unsafePtr, (void*)ptr, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00014F80 File Offset: 0x00013180
		public unsafe static bool operator ==(in HeapString a, in FixedString128Bytes b)
		{
			HeapString heapString = *UnsafeUtilityExtensions.AsRef<HeapString>(in a);
			int length = heapString.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = heapString.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00014FC3 File Offset: 0x000131C3
		public static bool operator !=(in HeapString a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00014FCF File Offset: 0x000131CF
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00014FD9 File Offset: 0x000131D9
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00014FE4 File Offset: 0x000131E4
		public unsafe HeapString(in FixedString512Bytes source, Allocator allocator)
		{
			this.m_Data = new NativeList<byte>((int)(source.utf8LengthInBytes + 1), allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in source.bytes);
			byte* unsafePtr = (byte*)this.m_Data.GetUnsafePtr<byte>();
			UnsafeUtility.MemCpy((void*)unsafePtr, (void*)ptr, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001503C File Offset: 0x0001323C
		public unsafe static bool operator ==(in HeapString a, in FixedString512Bytes b)
		{
			HeapString heapString = *UnsafeUtilityExtensions.AsRef<HeapString>(in a);
			int length = heapString.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = heapString.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001507F File Offset: 0x0001327F
		public static bool operator !=(in HeapString a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001508B File Offset: 0x0001328B
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00015095 File Offset: 0x00013295
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000150A0 File Offset: 0x000132A0
		public unsafe HeapString(in FixedString4096Bytes source, Allocator allocator)
		{
			this.m_Data = new NativeList<byte>((int)(source.utf8LengthInBytes + 1), allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in source.bytes);
			byte* unsafePtr = (byte*)this.m_Data.GetUnsafePtr<byte>();
			UnsafeUtility.MemCpy((void*)unsafePtr, (void*)ptr, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000150F8 File Offset: 0x000132F8
		public unsafe static bool operator ==(in HeapString a, in FixedString4096Bytes b)
		{
			HeapString heapString = *UnsafeUtilityExtensions.AsRef<HeapString>(in a);
			int length = heapString.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = heapString.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001513B File Offset: 0x0001333B
		public static bool operator !=(in HeapString a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00015147 File Offset: 0x00013347
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00015151 File Offset: 0x00013351
		[NotBurstCompatible]
		public override string ToString()
		{
			if (!this.m_Data.IsCreated)
			{
				return "";
			}
			return (ref this).ConvertToString<HeapString>();
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001516C File Offset: 0x0001336C
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<HeapString>();
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00015174 File Offset: 0x00013374
		[NotBurstCompatible]
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			string text = other as string;
			if (text != null)
			{
				return this.Equals(text);
			}
			if (other is HeapString)
			{
				HeapString heapString = (HeapString)other;
				return this.Equals(heapString);
			}
			if (other is FixedString32Bytes)
			{
				FixedString32Bytes fixedString32Bytes = (FixedString32Bytes)other;
				return this.Equals(fixedString32Bytes);
			}
			if (other is FixedString64Bytes)
			{
				FixedString64Bytes fixedString64Bytes = (FixedString64Bytes)other;
				return this.Equals(fixedString64Bytes);
			}
			if (other is FixedString128Bytes)
			{
				FixedString128Bytes fixedString128Bytes = (FixedString128Bytes)other;
				return this.Equals(fixedString128Bytes);
			}
			if (other is FixedString512Bytes)
			{
				FixedString512Bytes fixedString512Bytes = (FixedString512Bytes)other;
				return this.Equals(fixedString512Bytes);
			}
			if (other is FixedString4096Bytes)
			{
				FixedString4096Bytes fixedString4096Bytes = (FixedString4096Bytes)other;
				return this.Equals(fixedString4096Bytes);
			}
			return false;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001522C File Offset: 0x0001342C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= this.Length)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in HeapString of {1} length.", index, this.Length));
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001527D File Offset: 0x0001347D
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ThrowCopyError(CopyError error, string source)
		{
			throw new ArgumentException(string.Format("HeapString: {0} while copying \"{1}\"", error, source));
		}

		// Token: 0x04000278 RID: 632
		private NativeList<byte> m_Data;

		// Token: 0x020000A7 RID: 167
		public struct Enumerator : IEnumerator<Unicode.Rune>, IEnumerator, IDisposable
		{
			// Token: 0x0600067D RID: 1661 RVA: 0x00015295 File Offset: 0x00013495
			public Enumerator(HeapString source)
			{
				this.target = source;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x0600067E RID: 1662 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x000152B1 File Offset: 0x000134B1
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x06000680 RID: 1664 RVA: 0x000152F1 File Offset: 0x000134F1
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x06000681 RID: 1665 RVA: 0x00015306 File Offset: 0x00013506
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000682 RID: 1666 RVA: 0x00015313 File Offset: 0x00013513
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x04000279 RID: 633
			private HeapString target;

			// Token: 0x0400027A RID: 634
			private int offset;

			// Token: 0x0400027B RID: 635
			private Unicode.Rune current;
		}
	}
}
