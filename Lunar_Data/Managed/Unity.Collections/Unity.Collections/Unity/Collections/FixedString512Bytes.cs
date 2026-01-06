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
	// Token: 0x0200009B RID: 155
	[BurstCompatible]
	[Serializable]
	[StructLayout(0, Size = 512)]
	public struct FixedString512Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000A991 File Offset: 0x00008B91
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 509;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000A998 File Offset: 0x00008B98
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

		// Token: 0x06000434 RID: 1076 RVA: 0x0000A9A6 File Offset: 0x00008BA6
		[MethodImpl(256)]
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)UnsafeUtility.AddressOf<FixedBytes510>(ref this.bytes);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000A9B3 File Offset: 0x00008BB3
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000A9BB File Offset: 0x00008BBB
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000A991 File Offset: 0x00008B91
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return 509;
			}
			set
			{
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000A9D4 File Offset: 0x00008BD4
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 509)
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000AA52 File Offset: 0x00008C52
		public bool IsEmpty
		{
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x1700009D RID: 157
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

		// Token: 0x0600043D RID: 1085 RVA: 0x0000AA74 File Offset: 0x00008C74
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000AA7E File Offset: 0x00008C7E
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000AA88 File Offset: 0x00008C88
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000AAAE File Offset: 0x00008CAE
		public FixedString512Bytes.Enumerator GetEnumerator()
		{
			return new FixedString512Bytes.Enumerator(this);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000AABB File Offset: 0x00008CBB
		[NotBurstCompatible]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		[NotBurstCompatible]
		public unsafe bool Equals(string other)
		{
			int num = (int)this.utf8LengthInBytes;
			int length = other.Length;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in this.bytes);
			char* ptr2 = other;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(ptr, num, ptr2, length) == 0;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000AB15 File Offset: 0x00008D15
		public ref FixedList512Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList512Bytes<byte>>(UnsafeUtility.AddressOf<FixedString512Bytes>(ref this));
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000AB22 File Offset: 0x00008D22
		[NotBurstCompatible]
		public FixedString512Bytes(string source)
		{
			this = default(FixedString512Bytes);
			this.Initialize(source);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000AB34 File Offset: 0x00008D34
		[NotBurstCompatible]
		internal unsafe int Initialize(string source)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				CopyError copyError = UTF8ArrayUnsafeUtility.Copy(this.GetUnsafePtr(), out this.utf8LengthInBytes, 509, ptr, source.Length);
				if (copyError != CopyError.None)
				{
					return (int)copyError;
				}
				this.Length = (int)this.utf8LengthInBytes;
			}
			return 0;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000AB96 File Offset: 0x00008D96
		public FixedString512Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString512Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		internal int Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString512Bytes);
			return (int)(ref this).Append(rune, count);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000ABB9 File Offset: 0x00008DB9
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000ABC3 File Offset: 0x00008DC3
		public FixedString512Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(in other);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		internal unsafe int Initialize(in FixedString32Bytes other)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 509, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000AC2C File Offset: 0x00008E2C
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString32Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000AC68 File Offset: 0x00008E68
		public static bool operator !=(in FixedString512Bytes a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000AC74 File Offset: 0x00008E74
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000AC7E File Offset: 0x00008E7E
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000AC88 File Offset: 0x00008E88
		public FixedString512Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000AC9C File Offset: 0x00008E9C
		internal unsafe int Initialize(in FixedString64Bytes other)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 509, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString64Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000AD30 File Offset: 0x00008F30
		public static bool operator !=(in FixedString512Bytes a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000AD3C File Offset: 0x00008F3C
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000AD46 File Offset: 0x00008F46
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000AD50 File Offset: 0x00008F50
		public FixedString512Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000AD64 File Offset: 0x00008F64
		internal unsafe int Initialize(in FixedString128Bytes other)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 509, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000ADBC File Offset: 0x00008FBC
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString128Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public static bool operator !=(in FixedString512Bytes a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000AE04 File Offset: 0x00009004
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000AE0E File Offset: 0x0000900E
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000AE18 File Offset: 0x00009018
		public FixedString512Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(in other);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000AE2C File Offset: 0x0000902C
		internal unsafe int Initialize(in FixedString512Bytes other)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 509, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000AE84 File Offset: 0x00009084
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString512Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000AEC0 File Offset: 0x000090C0
		public static bool operator !=(in FixedString512Bytes a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000AECC File Offset: 0x000090CC
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000AED6 File Offset: 0x000090D6
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000AEE0 File Offset: 0x000090E0
		public FixedString512Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString512Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000AEF4 File Offset: 0x000090F4
		internal unsafe int Initialize(in FixedString4096Bytes other)
		{
			this.bytes = default(FixedBytes510);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 509, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000AF4C File Offset: 0x0000914C
		public unsafe static bool operator ==(in FixedString512Bytes a, in FixedString4096Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000AF88 File Offset: 0x00009188
		public static bool operator !=(in FixedString512Bytes a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000AF94 File Offset: 0x00009194
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000AF9E File Offset: 0x0000919E
		public static implicit operator FixedString4096Bytes(in FixedString512Bytes fs)
		{
			return new FixedString4096Bytes(in fs);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000AFA6 File Offset: 0x000091A6
		[NotBurstCompatible]
		public static implicit operator FixedString512Bytes(string b)
		{
			return new FixedString512Bytes(b);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000AFAE File Offset: 0x000091AE
		[NotBurstCompatible]
		public override string ToString()
		{
			return (ref this).ConvertToString<FixedString512Bytes>();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000AFB6 File Offset: 0x000091B6
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<FixedString512Bytes>();
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000AFC0 File Offset: 0x000091C0
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

		// Token: 0x0600046B RID: 1131 RVA: 0x0000B05C File Offset: 0x0000925C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= (int)this.utf8LengthInBytes)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString512Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000B0B0 File Offset: 0x000092B0
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 509)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString512Bytes of '{1}' Capacity.", length, 509));
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000B0FF File Offset: 0x000092FF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 509)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 509));
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000B129 File Offset: 0x00009329
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString512Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000098D3 File Offset: 0x00007AD3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x0400015D RID: 349
		internal const ushort utf8MaxLengthInBytes = 509;

		// Token: 0x0400015E RID: 350
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x0400015F RID: 351
		[SerializeField]
		internal FixedBytes510 bytes;

		// Token: 0x0200009C RID: 156
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06000470 RID: 1136 RVA: 0x0000B145 File Offset: 0x00009345
			public Enumerator(FixedString512Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x06000471 RID: 1137 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000472 RID: 1138 RVA: 0x0000B161 File Offset: 0x00009361
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x06000473 RID: 1139 RVA: 0x0000B1A1 File Offset: 0x000093A1
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000B1B6 File Offset: 0x000093B6
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000B1BE File Offset: 0x000093BE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000160 RID: 352
			private FixedString512Bytes target;

			// Token: 0x04000161 RID: 353
			private int offset;

			// Token: 0x04000162 RID: 354
			private Unicode.Rune current;
		}
	}
}
