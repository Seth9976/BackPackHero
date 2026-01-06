using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000E1 RID: 225
	[NativeContainer]
	[DebuggerDisplay("Length = {Length}")]
	[BurstCompatible]
	public struct NativeText : INativeList<byte>, IIndexable<byte>, INativeDisposable, IDisposable, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<NativeText>, IEquatable<NativeText>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x000191A8 File Offset: 0x000173A8
		[NotBurstCompatible]
		public NativeText(string source, Allocator allocator)
		{
			this = new NativeText(source, allocator);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000191B8 File Offset: 0x000173B8
		[NotBurstCompatible]
		public unsafe NativeText(string source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText(source.Length * 2, allocator);
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
					this.m_Data->Dispose();
					void* ptr2 = (ref allocator).Allocate(sizeof(UnsafeText), 16, 1);
					this.m_Data = (UnsafeText*)ptr2;
					*this.m_Data = default(UnsafeText);
				}
				this.Length = num;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00019248 File Offset: 0x00017448
		private unsafe NativeText(int capacity, AllocatorManager.AllocatorHandle allocator, int disposeSentinelStackDepth)
		{
			this = default(NativeText);
			void* ptr = (ref allocator).Allocate(sizeof(UnsafeText), 16, 1);
			this.m_Data = (UnsafeText*)ptr;
			*this.m_Data = new UnsafeText(capacity, allocator);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00019286 File Offset: 0x00017486
		public NativeText(int capacity, Allocator allocator)
		{
			this = new NativeText(capacity, allocator);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00019295 File Offset: 0x00017495
		public NativeText(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText(capacity, allocator, 2);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000192A0 File Offset: 0x000174A0
		public NativeText(Allocator allocator)
		{
			this = new NativeText(allocator);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000192AE File Offset: 0x000174AE
		public NativeText(AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText(512, allocator);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000192BC File Offset: 0x000174BC
		public unsafe NativeText(in FixedString32Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)ptr, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00019306 File Offset: 0x00017506
		public NativeText(in FixedString32Bytes source, Allocator allocator)
		{
			this = new NativeText(in source, allocator);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00019318 File Offset: 0x00017518
		public unsafe NativeText(in FixedString64Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)ptr, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00019362 File Offset: 0x00017562
		public NativeText(in FixedString64Bytes source, Allocator allocator)
		{
			this = new NativeText(in source, allocator);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00019374 File Offset: 0x00017574
		public unsafe NativeText(in FixedString128Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)ptr, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000193BE File Offset: 0x000175BE
		public NativeText(in FixedString128Bytes source, Allocator allocator)
		{
			this = new NativeText(in source, allocator);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000193D0 File Offset: 0x000175D0
		public unsafe NativeText(in FixedString512Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)ptr, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001941A File Offset: 0x0001761A
		public NativeText(in FixedString512Bytes source, Allocator allocator)
		{
			this = new NativeText(in source, allocator);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001942C File Offset: 0x0001762C
		public unsafe NativeText(in FixedString4096Bytes source, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeText((int)source.utf8LengthInBytes, allocator);
			this.Length = (int)source.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in source.bytes);
			UnsafeUtility.MemCpy((void*)this.m_Data->GetUnsafePtr(), (void*)ptr, (long)((ulong)source.utf8LengthInBytes));
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00019476 File Offset: 0x00017676
		public NativeText(in FixedString4096Bytes source, Allocator allocator)
		{
			this = new NativeText(in source, allocator);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00019485 File Offset: 0x00017685
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00019492 File Offset: 0x00017692
		public unsafe int Length
		{
			get
			{
				return this.m_Data->Length;
			}
			set
			{
				this.m_Data->Length = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x000194A0 File Offset: 0x000176A0
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x000194AD File Offset: 0x000176AD
		public unsafe int Capacity
		{
			get
			{
				return this.m_Data->Capacity;
			}
			set
			{
				this.m_Data->Capacity = value;
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000194BB File Offset: 0x000176BB
		public bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			this.Length = newLength;
			return true;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x000194C5 File Offset: 0x000176C5
		public unsafe bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.m_Data->IsEmpty;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000194DC File Offset: 0x000176DC
		public bool IsCreated
		{
			get
			{
				return this.m_Data != null;
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000194EB File Offset: 0x000176EB
		public unsafe byte* GetUnsafePtr()
		{
			return this.m_Data->GetUnsafePtr();
		}

		// Token: 0x170000E9 RID: 233
		public unsafe byte this[int index]
		{
			get
			{
				return *this.m_Data->ElementAt(index);
			}
			set
			{
				*this.m_Data->ElementAt(index) = value;
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00019517 File Offset: 0x00017717
		public unsafe ref byte ElementAt(int index)
		{
			return this.m_Data->ElementAt(index);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00019525 File Offset: 0x00017725
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00019530 File Offset: 0x00017730
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00019556 File Offset: 0x00017756
		public unsafe int CompareTo(NativeText other)
		{
			return (ref this).CompareTo(in *other.m_Data);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00019564 File Offset: 0x00017764
		public unsafe bool Equals(NativeText other)
		{
			return (ref this).Equals(in *other.m_Data);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00019572 File Offset: 0x00017772
		public int CompareTo(NativeText.ReadOnly other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001957C File Offset: 0x0001777C
		public unsafe bool Equals(NativeText.ReadOnly other)
		{
			return (ref this).Equals(in *other.m_Data);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001958A File Offset: 0x0001778A
		public unsafe void Dispose()
		{
			AllocatorManager.AllocatorHandle allocator = this.m_Data->m_UntypedListData.Allocator;
			this.m_Data->Dispose();
			AllocatorManager.Free<UnsafeText>(allocator, this.m_Data, 1);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000195B3 File Offset: 0x000177B3
		[NotBurstCompatible]
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_Data->Dispose(inputDeps);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000195C1 File Offset: 0x000177C1
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

		// Token: 0x06000861 RID: 2145 RVA: 0x000195CF File Offset: 0x000177CF
		public NativeText.Enumerator GetEnumerator()
		{
			return new NativeText.Enumerator(this);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000195DC File Offset: 0x000177DC
		[NotBurstCompatible]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000195F0 File Offset: 0x000177F0
		[NotBurstCompatible]
		public bool Equals(string other)
		{
			return this.ToString().Equals(other);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00019604 File Offset: 0x00017804
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00019610 File Offset: 0x00017810
		public unsafe static bool operator ==(in NativeText a, in FixedString32Bytes b)
		{
			NativeText nativeText = *UnsafeUtilityExtensions.AsRef<NativeText>(in a);
			int length = nativeText.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00019653 File Offset: 0x00017853
		public static bool operator !=(in NativeText a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001965F File Offset: 0x0001785F
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00019669 File Offset: 0x00017869
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00019674 File Offset: 0x00017874
		public unsafe static bool operator ==(in NativeText a, in FixedString64Bytes b)
		{
			NativeText nativeText = *UnsafeUtilityExtensions.AsRef<NativeText>(in a);
			int length = nativeText.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000196B7 File Offset: 0x000178B7
		public static bool operator !=(in NativeText a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000196C3 File Offset: 0x000178C3
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000196CD File Offset: 0x000178CD
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000196D8 File Offset: 0x000178D8
		public unsafe static bool operator ==(in NativeText a, in FixedString128Bytes b)
		{
			NativeText nativeText = *UnsafeUtilityExtensions.AsRef<NativeText>(in a);
			int length = nativeText.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001971B File Offset: 0x0001791B
		public static bool operator !=(in NativeText a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00019727 File Offset: 0x00017927
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00019731 File Offset: 0x00017931
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001973C File Offset: 0x0001793C
		public unsafe static bool operator ==(in NativeText a, in FixedString512Bytes b)
		{
			NativeText nativeText = *UnsafeUtilityExtensions.AsRef<NativeText>(in a);
			int length = nativeText.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001977F File Offset: 0x0001797F
		public static bool operator !=(in NativeText a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001978B File Offset: 0x0001798B
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00019795 File Offset: 0x00017995
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000197A0 File Offset: 0x000179A0
		public unsafe static bool operator ==(in NativeText a, in FixedString4096Bytes b)
		{
			NativeText nativeText = *UnsafeUtilityExtensions.AsRef<NativeText>(in a);
			int length = nativeText.Length;
			int utf8LengthInBytes = (int)b.utf8LengthInBytes;
			byte* unsafePtr = nativeText.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000197E3 File Offset: 0x000179E3
		public static bool operator !=(in NativeText a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000197EF File Offset: 0x000179EF
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000197F9 File Offset: 0x000179F9
		[NotBurstCompatible]
		public override string ToString()
		{
			if (this.m_Data == null)
			{
				return "";
			}
			return (ref this).ConvertToString<NativeText>();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00019811 File Offset: 0x00017A11
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<NativeText>();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001981C File Offset: 0x00017A1C
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
			if (other is NativeText)
			{
				NativeText nativeText = (NativeText)other;
				return this.Equals(nativeText);
			}
			if (other is NativeText.ReadOnly)
			{
				NativeText.ReadOnly readOnly = (NativeText.ReadOnly)other;
				return this.Equals(readOnly);
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

		// Token: 0x0600087B RID: 2171 RVA: 0x000198EA File Offset: 0x00017AEA
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal unsafe static void CheckNull(void* dataPtr)
		{
			if (dataPtr == null)
			{
				throw new Exception("NativeText has yet to be created or has been destroyed!");
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckRead()
		{
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWriteAndBumpSecondaryVersion()
		{
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000198FC File Offset: 0x00017AFC
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= this.Length)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in NativeText of {1} length.", index, this.Length));
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001994D File Offset: 0x00017B4D
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ThrowCopyError(CopyError error, string source)
		{
			throw new ArgumentException(string.Format("NativeText: {0} while copying \"{1}\"", error, source));
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00019965 File Offset: 0x00017B65
		public NativeText.ReadOnly AsReadOnly()
		{
			return new NativeText.ReadOnly(this.m_Data);
		}

		// Token: 0x040002D3 RID: 723
		[NativeDisableUnsafePtrRestriction]
		private unsafe UnsafeText* m_Data;

		// Token: 0x020000E2 RID: 226
		public struct Enumerator : IEnumerator<Unicode.Rune>, IEnumerator, IDisposable
		{
			// Token: 0x06000882 RID: 2178 RVA: 0x00019972 File Offset: 0x00017B72
			public Enumerator(NativeText source)
			{
				this.target = source.AsReadOnly();
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x06000883 RID: 2179 RVA: 0x00019994 File Offset: 0x00017B94
			public Enumerator(NativeText.ReadOnly source)
			{
				this.target = source;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x06000884 RID: 2180 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000885 RID: 2181 RVA: 0x000199B0 File Offset: 0x00017BB0
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x000199F0 File Offset: 0x00017BF0
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x06000887 RID: 2183 RVA: 0x00019A05 File Offset: 0x00017C05
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x06000888 RID: 2184 RVA: 0x00019A12 File Offset: 0x00017C12
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x040002D4 RID: 724
			private NativeText.ReadOnly target;

			// Token: 0x040002D5 RID: 725
			private int offset;

			// Token: 0x040002D6 RID: 726
			private Unicode.Rune current;
		}

		// Token: 0x020000E3 RID: 227
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct ReadOnly : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<NativeText>, IEquatable<NativeText>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
		{
			// Token: 0x06000889 RID: 2185 RVA: 0x00019A1A File Offset: 0x00017C1A
			internal unsafe ReadOnly(UnsafeText* text)
			{
				this.m_Data = text;
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600088A RID: 2186 RVA: 0x00019A23 File Offset: 0x00017C23
			// (set) Token: 0x0600088B RID: 2187 RVA: 0x00002C2B File Offset: 0x00000E2B
			public unsafe int Capacity
			{
				get
				{
					return this.m_Data->Capacity;
				}
				set
				{
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x0600088C RID: 2188 RVA: 0x00019A30 File Offset: 0x00017C30
			// (set) Token: 0x0600088D RID: 2189 RVA: 0x00002C2B File Offset: 0x00000E2B
			public unsafe bool IsEmpty
			{
				get
				{
					return this.m_Data == null || this.m_Data->IsEmpty;
				}
				set
				{
				}
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x0600088E RID: 2190 RVA: 0x00019A49 File Offset: 0x00017C49
			// (set) Token: 0x0600088F RID: 2191 RVA: 0x00002C2B File Offset: 0x00000E2B
			public unsafe int Length
			{
				get
				{
					return this.m_Data->Length;
				}
				set
				{
				}
			}

			// Token: 0x170000F0 RID: 240
			public unsafe byte this[int index]
			{
				get
				{
					return *this.m_Data->ElementAt(index);
				}
				set
				{
				}
			}

			// Token: 0x06000892 RID: 2194 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Clear()
			{
			}

			// Token: 0x06000893 RID: 2195 RVA: 0x00019A65 File Offset: 0x00017C65
			public ref byte ElementAt(int index)
			{
				throw new NotSupportedException("Trying to retrieve non-readonly ref to NativeText.ReadOnly data. This is not permitted.");
			}

			// Token: 0x06000894 RID: 2196 RVA: 0x00019A71 File Offset: 0x00017C71
			public unsafe byte* GetUnsafePtr()
			{
				return this.m_Data->GetUnsafePtr();
			}

			// Token: 0x06000895 RID: 2197 RVA: 0x00019A7E File Offset: 0x00017C7E
			public bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
			{
				return false;
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x00019A81 File Offset: 0x00017C81
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			internal unsafe static void CheckNull(void* dataPtr)
			{
				if (dataPtr == null)
				{
					throw new Exception("NativeText.ReadOnly has yet to be created or has been destroyed!");
				}
			}

			// Token: 0x06000897 RID: 2199 RVA: 0x00002C2B File Offset: 0x00000E2B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckRead()
			{
			}

			// Token: 0x06000898 RID: 2200 RVA: 0x00002C2B File Offset: 0x00000E2B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void ErrorWrite()
			{
			}

			// Token: 0x06000899 RID: 2201 RVA: 0x00019A93 File Offset: 0x00017C93
			[NotBurstCompatible]
			public unsafe int CompareTo(string other)
			{
				return this.m_Data->ToString().CompareTo(other);
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x00019AAC File Offset: 0x00017CAC
			[NotBurstCompatible]
			public unsafe bool Equals(string other)
			{
				return this.m_Data->ToString().Equals(other);
			}

			// Token: 0x0600089B RID: 2203 RVA: 0x00019AC5 File Offset: 0x00017CC5
			public unsafe int CompareTo(NativeText.ReadOnly other)
			{
				return (ref *this.m_Data).CompareTo(in *other.m_Data);
			}

			// Token: 0x0600089C RID: 2204 RVA: 0x00019AD8 File Offset: 0x00017CD8
			public unsafe bool Equals(NativeText.ReadOnly other)
			{
				return (ref *this.m_Data).Equals(in *other.m_Data);
			}

			// Token: 0x0600089D RID: 2205 RVA: 0x00019AEB File Offset: 0x00017CEB
			public unsafe int CompareTo(NativeText other)
			{
				return (ref this).CompareTo(in *other.m_Data);
			}

			// Token: 0x0600089E RID: 2206 RVA: 0x00019AF9 File Offset: 0x00017CF9
			public unsafe bool Equals(NativeText other)
			{
				return (ref this).Equals(in *other.m_Data);
			}

			// Token: 0x0600089F RID: 2207 RVA: 0x00019B07 File Offset: 0x00017D07
			public int CompareTo(FixedString32Bytes other)
			{
				return (ref this).CompareTo(in other);
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x00019B14 File Offset: 0x00017D14
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString32Bytes b)
			{
				UnsafeText unsafeText = *a.m_Data;
				int length = unsafeText.Length;
				int utf8LengthInBytes = (int)b.utf8LengthInBytes;
				byte* unsafePtr = unsafeText.GetUnsafePtr();
				byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x00019B57 File Offset: 0x00017D57
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString32Bytes b)
			{
				return !((in a) == (in b));
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x00019B63 File Offset: 0x00017D63
			public bool Equals(FixedString32Bytes other)
			{
				return (in this) == (in other);
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x00019B6D File Offset: 0x00017D6D
			public int CompareTo(FixedString64Bytes other)
			{
				return (ref this).CompareTo(in other);
			}

			// Token: 0x060008A4 RID: 2212 RVA: 0x00019B78 File Offset: 0x00017D78
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString64Bytes b)
			{
				UnsafeText unsafeText = *a.m_Data;
				int length = unsafeText.Length;
				int utf8LengthInBytes = (int)b.utf8LengthInBytes;
				byte* unsafePtr = unsafeText.GetUnsafePtr();
				byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
			}

			// Token: 0x060008A5 RID: 2213 RVA: 0x00019BBB File Offset: 0x00017DBB
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString64Bytes b)
			{
				return !((in a) == (in b));
			}

			// Token: 0x060008A6 RID: 2214 RVA: 0x00019BC7 File Offset: 0x00017DC7
			public bool Equals(FixedString64Bytes other)
			{
				return (in this) == (in other);
			}

			// Token: 0x060008A7 RID: 2215 RVA: 0x00019BD1 File Offset: 0x00017DD1
			public int CompareTo(FixedString128Bytes other)
			{
				return (ref this).CompareTo(in other);
			}

			// Token: 0x060008A8 RID: 2216 RVA: 0x00019BDC File Offset: 0x00017DDC
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString128Bytes b)
			{
				UnsafeText unsafeText = *a.m_Data;
				int length = unsafeText.Length;
				int utf8LengthInBytes = (int)b.utf8LengthInBytes;
				byte* unsafePtr = unsafeText.GetUnsafePtr();
				byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
			}

			// Token: 0x060008A9 RID: 2217 RVA: 0x00019C1F File Offset: 0x00017E1F
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString128Bytes b)
			{
				return !((in a) == (in b));
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x00019C2B File Offset: 0x00017E2B
			public bool Equals(FixedString128Bytes other)
			{
				return (in this) == (in other);
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x00019C35 File Offset: 0x00017E35
			public int CompareTo(FixedString512Bytes other)
			{
				return (ref this).CompareTo(in other);
			}

			// Token: 0x060008AC RID: 2220 RVA: 0x00019C40 File Offset: 0x00017E40
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString512Bytes b)
			{
				UnsafeText unsafeText = *a.m_Data;
				int length = unsafeText.Length;
				int utf8LengthInBytes = (int)b.utf8LengthInBytes;
				byte* unsafePtr = unsafeText.GetUnsafePtr();
				byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
			}

			// Token: 0x060008AD RID: 2221 RVA: 0x00019C83 File Offset: 0x00017E83
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString512Bytes b)
			{
				return !((in a) == (in b));
			}

			// Token: 0x060008AE RID: 2222 RVA: 0x00019C8F File Offset: 0x00017E8F
			public bool Equals(FixedString512Bytes other)
			{
				return (in this) == (in other);
			}

			// Token: 0x060008AF RID: 2223 RVA: 0x00019C99 File Offset: 0x00017E99
			public int CompareTo(FixedString4096Bytes other)
			{
				return (ref this).CompareTo(in other);
			}

			// Token: 0x060008B0 RID: 2224 RVA: 0x00019CA4 File Offset: 0x00017EA4
			public unsafe static bool operator ==(in NativeText.ReadOnly a, in FixedString4096Bytes b)
			{
				UnsafeText unsafeText = *a.m_Data;
				int length = unsafeText.Length;
				int utf8LengthInBytes = (int)b.utf8LengthInBytes;
				byte* unsafePtr = unsafeText.GetUnsafePtr();
				byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
				return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(unsafePtr, length, ptr, utf8LengthInBytes);
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x00019CE7 File Offset: 0x00017EE7
			public static bool operator !=(in NativeText.ReadOnly a, in FixedString4096Bytes b)
			{
				return !((in a) == (in b));
			}

			// Token: 0x060008B2 RID: 2226 RVA: 0x00019CF3 File Offset: 0x00017EF3
			public bool Equals(FixedString4096Bytes other)
			{
				return (in this) == (in other);
			}

			// Token: 0x060008B3 RID: 2227 RVA: 0x00019CFD File Offset: 0x00017EFD
			[NotBurstCompatible]
			public override string ToString()
			{
				if (this.m_Data == null)
				{
					return "";
				}
				return (ref this).ConvertToString<NativeText.ReadOnly>();
			}

			// Token: 0x060008B4 RID: 2228 RVA: 0x00019D15 File Offset: 0x00017F15
			public override int GetHashCode()
			{
				return (ref this).ComputeHashCode<NativeText.ReadOnly>();
			}

			// Token: 0x060008B5 RID: 2229 RVA: 0x00019D20 File Offset: 0x00017F20
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
				if (other is NativeText)
				{
					NativeText nativeText = (NativeText)other;
					return this.Equals(nativeText);
				}
				if (other is NativeText.ReadOnly)
				{
					NativeText.ReadOnly readOnly = (NativeText.ReadOnly)other;
					return this.Equals(readOnly);
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

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00019DEE File Offset: 0x00017FEE
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

			// Token: 0x060008B7 RID: 2231 RVA: 0x00019DFC File Offset: 0x00017FFC
			public NativeText.Enumerator GetEnumerator()
			{
				return new NativeText.Enumerator(this);
			}

			// Token: 0x040002D7 RID: 727
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeText* m_Data;
		}
	}
}
