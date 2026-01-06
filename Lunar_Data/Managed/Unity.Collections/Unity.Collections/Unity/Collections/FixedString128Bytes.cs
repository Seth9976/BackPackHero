using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x02000097 RID: 151
	[BurstCompatible]
	[Serializable]
	[StructLayout(0, Size = 128)]
	public struct FixedString128Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000A181 File Offset: 0x00008381
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 125;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000A185 File Offset: 0x00008385
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

		// Token: 0x060003EF RID: 1007 RVA: 0x0000A193 File Offset: 0x00008393
		[MethodImpl(256)]
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)UnsafeUtility.AddressOf<FixedBytes126>(ref this.bytes);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000A1A0 File Offset: 0x000083A0
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000A1A8 File Offset: 0x000083A8
		public unsafe int Length
		{
			get
			{
				return (int)this.utf8LengthInBytes;
			}
			set
			{
				this.utf8LengthInBytes = (ushort)value;
				this.GetUnsafePtr()[this.utf8LengthInBytes] = 0;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000A181 File Offset: 0x00008381
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return 125;
			}
			set
			{
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000A1C4 File Offset: 0x000083C4
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 125)
			{
				return false;
			}
			if (newLength == (int)this.utf8LengthInBytes)
			{
				return true;
			}
			if (clearOptions == NativeArrayOptions.ClearMemory)
			{
				if (newLength > (int)this.utf8LengthInBytes)
				{
					UnsafeUtility.MemClear((void*)(this.GetUnsafePtr() + this.utf8LengthInBytes), (long)(newLength - (int)this.utf8LengthInBytes));
				}
				else
				{
					UnsafeUtility.MemClear((void*)(this.GetUnsafePtr() + newLength), (long)((int)this.utf8LengthInBytes - newLength));
				}
			}
			this.utf8LengthInBytes = (ushort)newLength;
			this.GetUnsafePtr()[this.utf8LengthInBytes] = 0;
			return true;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000A23F File Offset: 0x0000843F
		public bool IsEmpty
		{
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x17000095 RID: 149
		public unsafe byte this[int index]
		{
			get
			{
				return this.GetUnsafePtr()[index];
			}
			set
			{
				this.GetUnsafePtr()[index] = value;
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000A261 File Offset: 0x00008461
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000A26B File Offset: 0x0000846B
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000A274 File Offset: 0x00008474
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000A29A File Offset: 0x0000849A
		public FixedString128Bytes.Enumerator GetEnumerator()
		{
			return new FixedString128Bytes.Enumerator(this);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000A2A7 File Offset: 0x000084A7
		[NotBurstCompatible]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000A2BC File Offset: 0x000084BC
		[NotBurstCompatible]
		public unsafe bool Equals(string other)
		{
			int num = (int)this.utf8LengthInBytes;
			int length = other.Length;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in this.bytes);
			char* ptr2 = other;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(ptr, num, ptr2, length) == 0;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000A301 File Offset: 0x00008501
		public ref FixedList128Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList128Bytes<byte>>(UnsafeUtility.AddressOf<FixedString128Bytes>(ref this));
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000A30E File Offset: 0x0000850E
		[NotBurstCompatible]
		public FixedString128Bytes(string source)
		{
			this = default(FixedString128Bytes);
			this.Initialize(source);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000A320 File Offset: 0x00008520
		[NotBurstCompatible]
		internal unsafe int Initialize(string source)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				CopyError copyError = UTF8ArrayUnsafeUtility.Copy(this.GetUnsafePtr(), out this.utf8LengthInBytes, 125, ptr, source.Length);
				if (copyError != CopyError.None)
				{
					return (int)copyError;
				}
				this.Length = (int)this.utf8LengthInBytes;
			}
			return 0;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000A37F File Offset: 0x0000857F
		public FixedString128Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString128Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000A391 File Offset: 0x00008591
		internal int Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString128Bytes);
			return (int)(ref this).Append(rune, count);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000A3A2 File Offset: 0x000085A2
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000A3AC File Offset: 0x000085AC
		public FixedString128Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000A3C0 File Offset: 0x000085C0
		internal unsafe int Initialize(in FixedString32Bytes other)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 125, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000A414 File Offset: 0x00008614
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString32Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000A450 File Offset: 0x00008650
		public static bool operator !=(in FixedString128Bytes a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000A45C File Offset: 0x0000865C
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000A466 File Offset: 0x00008666
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000A470 File Offset: 0x00008670
		public FixedString128Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(in other);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000A484 File Offset: 0x00008684
		internal unsafe int Initialize(in FixedString64Bytes other)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 125, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000A4D8 File Offset: 0x000086D8
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString64Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000A514 File Offset: 0x00008714
		public static bool operator !=(in FixedString128Bytes a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000A520 File Offset: 0x00008720
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000A52A File Offset: 0x0000872A
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000A534 File Offset: 0x00008734
		public FixedString128Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000A548 File Offset: 0x00008748
		internal unsafe int Initialize(in FixedString128Bytes other)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 125, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000A59C File Offset: 0x0000879C
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString128Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public static bool operator !=(in FixedString128Bytes a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000A5E4 File Offset: 0x000087E4
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000A5EE File Offset: 0x000087EE
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000A5F8 File Offset: 0x000087F8
		public FixedString128Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000A60C File Offset: 0x0000880C
		internal unsafe int Initialize(in FixedString512Bytes other)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 125, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000A660 File Offset: 0x00008860
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString512Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000A69C File Offset: 0x0000889C
		public static bool operator !=(in FixedString128Bytes a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000A6A8 File Offset: 0x000088A8
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000A6B2 File Offset: 0x000088B2
		public static implicit operator FixedString512Bytes(in FixedString128Bytes fs)
		{
			return new FixedString512Bytes(in fs);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000A6BA File Offset: 0x000088BA
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000A6C4 File Offset: 0x000088C4
		public FixedString128Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString128Bytes);
			this.Initialize(in other);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000A6D8 File Offset: 0x000088D8
		internal unsafe int Initialize(in FixedString4096Bytes other)
		{
			this.bytes = default(FixedBytes126);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 125, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000A72C File Offset: 0x0000892C
		public unsafe static bool operator ==(in FixedString128Bytes a, in FixedString4096Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000A768 File Offset: 0x00008968
		public static bool operator !=(in FixedString128Bytes a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000A774 File Offset: 0x00008974
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000A77E File Offset: 0x0000897E
		public static implicit operator FixedString4096Bytes(in FixedString128Bytes fs)
		{
			return new FixedString4096Bytes(in fs);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000A786 File Offset: 0x00008986
		[NotBurstCompatible]
		public static implicit operator FixedString128Bytes(string b)
		{
			return new FixedString128Bytes(b);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000A78E File Offset: 0x0000898E
		[NotBurstCompatible]
		public override string ToString()
		{
			return (ref this).ConvertToString<FixedString128Bytes>();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000A796 File Offset: 0x00008996
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<FixedString128Bytes>();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000A7A0 File Offset: 0x000089A0
		[NotBurstCompatible]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			string text = obj as string;
			if (text != null)
			{
				return this.Equals(text);
			}
			if (obj is FixedString32Bytes)
			{
				FixedString32Bytes fixedString32Bytes = (FixedString32Bytes)obj;
				return this.Equals(fixedString32Bytes);
			}
			if (obj is FixedString64Bytes)
			{
				FixedString64Bytes fixedString64Bytes = (FixedString64Bytes)obj;
				return this.Equals(fixedString64Bytes);
			}
			if (obj is FixedString128Bytes)
			{
				FixedString128Bytes fixedString128Bytes = (FixedString128Bytes)obj;
				return this.Equals(fixedString128Bytes);
			}
			if (obj is FixedString512Bytes)
			{
				FixedString512Bytes fixedString512Bytes = (FixedString512Bytes)obj;
				return this.Equals(fixedString512Bytes);
			}
			if (obj is FixedString4096Bytes)
			{
				FixedString4096Bytes fixedString4096Bytes = (FixedString4096Bytes)obj;
				return this.Equals(fixedString4096Bytes);
			}
			return false;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000A83C File Offset: 0x00008A3C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= (int)this.utf8LengthInBytes)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString128Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000A88D File Offset: 0x00008A8D
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 125)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString128Bytes of '{1}' Capacity.", length, 125));
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000A8CB File Offset: 0x00008ACB
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 125)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 125));
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000A8EF File Offset: 0x00008AEF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString128Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000098D3 File Offset: 0x00007AD3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x0400012A RID: 298
		internal const ushort utf8MaxLengthInBytes = 125;

		// Token: 0x0400012B RID: 299
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x0400012C RID: 300
		[SerializeField]
		internal FixedBytes126 bytes;

		// Token: 0x02000098 RID: 152
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600042C RID: 1068 RVA: 0x0000A90B File Offset: 0x00008B0B
			public Enumerator(FixedString128Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x0000A927 File Offset: 0x00008B27
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x0000A967 File Offset: 0x00008B67
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000A97C File Offset: 0x00008B7C
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000A984 File Offset: 0x00008B84
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400012D RID: 301
			private FixedString128Bytes target;

			// Token: 0x0400012E RID: 302
			private int offset;

			// Token: 0x0400012F RID: 303
			private Unicode.Rune current;
		}
	}
}
