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
	// Token: 0x02000093 RID: 147
	[BurstCompatible]
	[Serializable]
	[StructLayout(0, Size = 64)]
	public struct FixedString64Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00009969 File Offset: 0x00007B69
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000996D File Offset: 0x00007B6D
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

		// Token: 0x060003A9 RID: 937 RVA: 0x0000997B File Offset: 0x00007B7B
		[MethodImpl(256)]
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)UnsafeUtility.AddressOf<FixedBytes62>(ref this.bytes);
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00009988 File Offset: 0x00007B88
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00009990 File Offset: 0x00007B90
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00009969 File Offset: 0x00007B69
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return 61;
			}
			set
			{
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000099AC File Offset: 0x00007BAC
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 61)
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00009A27 File Offset: 0x00007C27
		public bool IsEmpty
		{
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x1700008D RID: 141
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

		// Token: 0x060003B2 RID: 946 RVA: 0x00009A49 File Offset: 0x00007C49
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00009A53 File Offset: 0x00007C53
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00009A5C File Offset: 0x00007C5C
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00009A82 File Offset: 0x00007C82
		public FixedString64Bytes.Enumerator GetEnumerator()
		{
			return new FixedString64Bytes.Enumerator(this);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00009A8F File Offset: 0x00007C8F
		[NotBurstCompatible]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00009AA4 File Offset: 0x00007CA4
		[NotBurstCompatible]
		public unsafe bool Equals(string other)
		{
			int num = (int)this.utf8LengthInBytes;
			int length = other.Length;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in this.bytes);
			char* ptr2 = other;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(ptr, num, ptr2, length) == 0;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00009AE9 File Offset: 0x00007CE9
		public ref FixedList64Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList64Bytes<byte>>(UnsafeUtility.AddressOf<FixedString64Bytes>(ref this));
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00009AF6 File Offset: 0x00007CF6
		[NotBurstCompatible]
		public FixedString64Bytes(string source)
		{
			this = default(FixedString64Bytes);
			this.Initialize(source);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00009B08 File Offset: 0x00007D08
		[NotBurstCompatible]
		internal unsafe int Initialize(string source)
		{
			this.bytes = default(FixedBytes62);
			this.utf8LengthInBytes = 0;
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				CopyError copyError = UTF8ArrayUnsafeUtility.Copy(this.GetUnsafePtr(), out this.utf8LengthInBytes, 61, ptr, source.Length);
				if (copyError != CopyError.None)
				{
					return (int)copyError;
				}
				this.Length = (int)this.utf8LengthInBytes;
			}
			return 0;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00009B67 File Offset: 0x00007D67
		public FixedString64Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString64Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00009B79 File Offset: 0x00007D79
		internal int Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString64Bytes);
			return (int)(ref this).Append(rune, count);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00009B8A File Offset: 0x00007D8A
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00009B94 File Offset: 0x00007D94
		public FixedString64Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString64Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00009BA8 File Offset: 0x00007DA8
		internal unsafe int Initialize(in FixedString32Bytes other)
		{
			this.bytes = default(FixedBytes62);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 61, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00009BFC File Offset: 0x00007DFC
		public unsafe static bool operator ==(in FixedString64Bytes a, in FixedString32Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00009C38 File Offset: 0x00007E38
		public static bool operator !=(in FixedString64Bytes a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00009C44 File Offset: 0x00007E44
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00009C4E File Offset: 0x00007E4E
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00009C58 File Offset: 0x00007E58
		public FixedString64Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString64Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00009C6C File Offset: 0x00007E6C
		internal unsafe int Initialize(in FixedString64Bytes other)
		{
			this.bytes = default(FixedBytes62);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 61, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00009CC0 File Offset: 0x00007EC0
		public unsafe static bool operator ==(in FixedString64Bytes a, in FixedString64Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00009CFC File Offset: 0x00007EFC
		public static bool operator !=(in FixedString64Bytes a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00009D08 File Offset: 0x00007F08
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00009D12 File Offset: 0x00007F12
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00009D1C File Offset: 0x00007F1C
		public FixedString64Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString64Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00009D30 File Offset: 0x00007F30
		internal unsafe int Initialize(in FixedString128Bytes other)
		{
			this.bytes = default(FixedBytes62);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 61, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00009D84 File Offset: 0x00007F84
		public unsafe static bool operator ==(in FixedString64Bytes a, in FixedString128Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00009DC0 File Offset: 0x00007FC0
		public static bool operator !=(in FixedString64Bytes a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00009DCC File Offset: 0x00007FCC
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00009DD6 File Offset: 0x00007FD6
		public static implicit operator FixedString128Bytes(in FixedString64Bytes fs)
		{
			return new FixedString128Bytes(in fs);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00009DDE File Offset: 0x00007FDE
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00009DE8 File Offset: 0x00007FE8
		public FixedString64Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString64Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00009DFC File Offset: 0x00007FFC
		internal unsafe int Initialize(in FixedString512Bytes other)
		{
			this.bytes = default(FixedBytes62);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 61, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00009E50 File Offset: 0x00008050
		public unsafe static bool operator ==(in FixedString64Bytes a, in FixedString512Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00009E8C File Offset: 0x0000808C
		public static bool operator !=(in FixedString64Bytes a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00009E98 File Offset: 0x00008098
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00009EA2 File Offset: 0x000080A2
		public static implicit operator FixedString512Bytes(in FixedString64Bytes fs)
		{
			return new FixedString512Bytes(in fs);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00009EAA File Offset: 0x000080AA
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00009EB4 File Offset: 0x000080B4
		public FixedString64Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString64Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00009EC8 File Offset: 0x000080C8
		internal unsafe int Initialize(in FixedString4096Bytes other)
		{
			this.bytes = default(FixedBytes62);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 61, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00009F1C File Offset: 0x0000811C
		public unsafe static bool operator ==(in FixedString64Bytes a, in FixedString4096Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00009F58 File Offset: 0x00008158
		public static bool operator !=(in FixedString64Bytes a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00009F64 File Offset: 0x00008164
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00009F6E File Offset: 0x0000816E
		public static implicit operator FixedString4096Bytes(in FixedString64Bytes fs)
		{
			return new FixedString4096Bytes(in fs);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00009F76 File Offset: 0x00008176
		[NotBurstCompatible]
		public static implicit operator FixedString64Bytes(string b)
		{
			return new FixedString64Bytes(b);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00009F7E File Offset: 0x0000817E
		[NotBurstCompatible]
		public override string ToString()
		{
			return (ref this).ConvertToString<FixedString64Bytes>();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00009F86 File Offset: 0x00008186
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<FixedString64Bytes>();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00009F90 File Offset: 0x00008190
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

		// Token: 0x060003E2 RID: 994 RVA: 0x0000A02C File Offset: 0x0000822C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= (int)this.utf8LengthInBytes)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString64Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000A07D File Offset: 0x0000827D
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 61)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString64Bytes of '{1}' Capacity.", length, 61));
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000A0BB File Offset: 0x000082BB
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 61)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 61));
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000A0DF File Offset: 0x000082DF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString64Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000098D3 File Offset: 0x00007AD3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x0400010F RID: 271
		internal const ushort utf8MaxLengthInBytes = 61;

		// Token: 0x04000110 RID: 272
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x04000111 RID: 273
		[SerializeField]
		internal FixedBytes62 bytes;

		// Token: 0x02000094 RID: 148
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060003E7 RID: 999 RVA: 0x0000A0FB File Offset: 0x000082FB
			public Enumerator(FixedString64Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x060003E8 RID: 1000 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x0000A117 File Offset: 0x00008317
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x0000A157 File Offset: 0x00008357
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000A16C File Offset: 0x0000836C
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000A174 File Offset: 0x00008374
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000112 RID: 274
			private FixedString64Bytes target;

			// Token: 0x04000113 RID: 275
			private int offset;

			// Token: 0x04000114 RID: 276
			private Unicode.Rune current;
		}
	}
}
