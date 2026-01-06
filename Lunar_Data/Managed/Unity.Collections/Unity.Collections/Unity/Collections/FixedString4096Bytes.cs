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
	// Token: 0x0200009F RID: 159
	[BurstCompatible]
	[Serializable]
	[StructLayout(0, Size = 4096)]
	public struct FixedString4096Bytes : INativeList<byte>, IIndexable<byte>, IUTF8Bytes, IComparable<string>, IEquatable<string>, IComparable<FixedString32Bytes>, IEquatable<FixedString32Bytes>, IComparable<FixedString64Bytes>, IEquatable<FixedString64Bytes>, IComparable<FixedString128Bytes>, IEquatable<FixedString128Bytes>, IComparable<FixedString512Bytes>, IEquatable<FixedString512Bytes>, IComparable<FixedString4096Bytes>, IEquatable<FixedString4096Bytes>
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000B1CB File Offset: 0x000093CB
		public static int UTF8MaxLengthInBytes
		{
			get
			{
				return 4093;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000B1D2 File Offset: 0x000093D2
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

		// Token: 0x06000478 RID: 1144 RVA: 0x0000B1E0 File Offset: 0x000093E0
		[MethodImpl(256)]
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)UnsafeUtility.AddressOf<FixedBytes4094>(ref this.bytes);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000B1ED File Offset: 0x000093ED
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000B1F5 File Offset: 0x000093F5
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000B1CB File Offset: 0x000093CB
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return 4093;
			}
			set
			{
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000B210 File Offset: 0x00009410
		public unsafe bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			if (newLength < 0 || newLength > 4093)
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000B28E File Offset: 0x0000948E
		public bool IsEmpty
		{
			get
			{
				return this.utf8LengthInBytes == 0;
			}
		}

		// Token: 0x170000A5 RID: 165
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

		// Token: 0x06000481 RID: 1153 RVA: 0x0000B2B0 File Offset: 0x000094B0
		public unsafe ref byte ElementAt(int index)
		{
			return ref this.GetUnsafePtr()[index];
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000B2BA File Offset: 0x000094BA
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public void Add(in byte value)
		{
			int length = this.Length;
			this.Length = length + 1;
			this[length] = value;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000B2EA File Offset: 0x000094EA
		public FixedString4096Bytes.Enumerator GetEnumerator()
		{
			return new FixedString4096Bytes.Enumerator(this);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000B2F7 File Offset: 0x000094F7
		[NotBurstCompatible]
		public int CompareTo(string other)
		{
			return this.ToString().CompareTo(other);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000B30C File Offset: 0x0000950C
		[NotBurstCompatible]
		public unsafe bool Equals(string other)
		{
			int num = (int)this.utf8LengthInBytes;
			int length = other.Length;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in this.bytes);
			char* ptr2 = other;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UTF8ArrayUnsafeUtility.StrCmp(ptr, num, ptr2, length) == 0;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000B351 File Offset: 0x00009551
		public ref FixedList4096Bytes<byte> AsFixedList()
		{
			return UnsafeUtility.AsRef<FixedList4096Bytes<byte>>(UnsafeUtility.AddressOf<FixedString4096Bytes>(ref this));
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000B35E File Offset: 0x0000955E
		[NotBurstCompatible]
		public FixedString4096Bytes(string source)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(source);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000B370 File Offset: 0x00009570
		[NotBurstCompatible]
		internal unsafe int Initialize(string source)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				CopyError copyError = UTF8ArrayUnsafeUtility.Copy(this.GetUnsafePtr(), out this.utf8LengthInBytes, 4093, ptr, source.Length);
				if (copyError != CopyError.None)
				{
					return (int)copyError;
				}
				this.Length = (int)this.utf8LengthInBytes;
			}
			return 0;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000B3D2 File Offset: 0x000095D2
		public FixedString4096Bytes(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(rune, count);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal int Initialize(Unicode.Rune rune, int count = 1)
		{
			this = default(FixedString4096Bytes);
			return (int)(ref this).Append(rune, count);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000B3F5 File Offset: 0x000095F5
		public int CompareTo(FixedString32Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000B3FF File Offset: 0x000095FF
		public FixedString4096Bytes(in FixedString32Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(in other);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000B410 File Offset: 0x00009610
		internal unsafe int Initialize(in FixedString32Bytes other)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 4093, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000B468 File Offset: 0x00009668
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString32Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes30>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000B4A4 File Offset: 0x000096A4
		public static bool operator !=(in FixedString4096Bytes a, in FixedString32Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000B4B0 File Offset: 0x000096B0
		public bool Equals(FixedString32Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000B4BA File Offset: 0x000096BA
		public int CompareTo(FixedString64Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000B4C4 File Offset: 0x000096C4
		public FixedString4096Bytes(in FixedString64Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(in other);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000B4D8 File Offset: 0x000096D8
		internal unsafe int Initialize(in FixedString64Bytes other)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 4093, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000B530 File Offset: 0x00009730
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString64Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes62>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000B56C File Offset: 0x0000976C
		public static bool operator !=(in FixedString4096Bytes a, in FixedString64Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000B578 File Offset: 0x00009778
		public bool Equals(FixedString64Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000B582 File Offset: 0x00009782
		public int CompareTo(FixedString128Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000B58C File Offset: 0x0000978C
		public FixedString4096Bytes(in FixedString128Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(in other);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000B5A0 File Offset: 0x000097A0
		internal unsafe int Initialize(in FixedString128Bytes other)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 4093, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000B5F8 File Offset: 0x000097F8
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString128Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes126>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000B634 File Offset: 0x00009834
		public static bool operator !=(in FixedString4096Bytes a, in FixedString128Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000B640 File Offset: 0x00009840
		public bool Equals(FixedString128Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000B64A File Offset: 0x0000984A
		public int CompareTo(FixedString512Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000B654 File Offset: 0x00009854
		public FixedString4096Bytes(in FixedString512Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000B668 File Offset: 0x00009868
		internal unsafe int Initialize(in FixedString512Bytes other)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 4093, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000B6C0 File Offset: 0x000098C0
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString512Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes510>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000B6FC File Offset: 0x000098FC
		public static bool operator !=(in FixedString4096Bytes a, in FixedString512Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000B708 File Offset: 0x00009908
		public bool Equals(FixedString512Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000B712 File Offset: 0x00009912
		public int CompareTo(FixedString4096Bytes other)
		{
			return (ref this).CompareTo(in other);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000B71C File Offset: 0x0000991C
		public FixedString4096Bytes(in FixedString4096Bytes other)
		{
			this = default(FixedString4096Bytes);
			this.Initialize(in other);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000B730 File Offset: 0x00009930
		internal unsafe int Initialize(in FixedString4096Bytes other)
		{
			this.bytes = default(FixedBytes4094);
			this.utf8LengthInBytes = 0;
			int num = 0;
			byte* unsafePtr = this.GetUnsafePtr();
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in other.bytes);
			ushort num2 = other.utf8LengthInBytes;
			FormatError formatError = UTF8ArrayUnsafeUtility.AppendUTF8Bytes(unsafePtr, ref num, 4093, ptr, (int)num2);
			if (formatError != FormatError.None)
			{
				return (int)formatError;
			}
			this.Length = num;
			return 0;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000B788 File Offset: 0x00009988
		public unsafe static bool operator ==(in FixedString4096Bytes a, in FixedString4096Bytes b)
		{
			int num = (int)a.utf8LengthInBytes;
			int num2 = (int)b.utf8LengthInBytes;
			byte* ptr = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in a.bytes);
			byte* ptr2 = (byte*)UnsafeUtilityExtensions.AddressOf<FixedBytes4094>(in b.bytes);
			return UTF8ArrayUnsafeUtility.EqualsUTF8Bytes(ptr, num, ptr2, num2);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000B7C4 File Offset: 0x000099C4
		public static bool operator !=(in FixedString4096Bytes a, in FixedString4096Bytes b)
		{
			return !((in a) == (in b));
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000B7D0 File Offset: 0x000099D0
		public bool Equals(FixedString4096Bytes other)
		{
			return (in this) == (in other);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000B7DA File Offset: 0x000099DA
		[NotBurstCompatible]
		public static implicit operator FixedString4096Bytes(string b)
		{
			return new FixedString4096Bytes(b);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000B7E2 File Offset: 0x000099E2
		[NotBurstCompatible]
		public override string ToString()
		{
			return (ref this).ConvertToString<FixedString4096Bytes>();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000B7EA File Offset: 0x000099EA
		public override int GetHashCode()
		{
			return (ref this).ComputeHashCode<FixedString4096Bytes>();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000B7F4 File Offset: 0x000099F4
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

		// Token: 0x060004AE RID: 1198 RVA: 0x0000B890 File Offset: 0x00009A90
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= (int)this.utf8LengthInBytes)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in FixedString4096Bytes of '{1}' Length.", index, this.utf8LengthInBytes));
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckLengthInRange(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} must be positive.", length));
			}
			if (length > 4093)
			{
				throw new ArgumentOutOfRangeException(string.Format("Length {0} is out of range in FixedString4096Bytes of '{1}' Capacity.", length, 4093));
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000B933 File Offset: 0x00009B33
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckCapacityInRange(int capacity)
		{
			if (capacity > 4093)
			{
				throw new ArgumentOutOfRangeException(string.Format("Capacity {0} must be lower than {1}.", capacity, 4093));
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000B95D File Offset: 0x00009B5D
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyError(CopyError error, string source)
		{
			if (error != CopyError.None)
			{
				throw new ArgumentException(string.Format("FixedString4096Bytes: {0} while copying \"{1}\"", error, source));
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000098D3 File Offset: 0x00007AD3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckFormatError(FormatError error)
		{
			if (error != FormatError.None)
			{
				throw new ArgumentException("Source is too long to fit into fixed string of this size");
			}
		}

		// Token: 0x04000270 RID: 624
		internal const ushort utf8MaxLengthInBytes = 4093;

		// Token: 0x04000271 RID: 625
		[SerializeField]
		internal ushort utf8LengthInBytes;

		// Token: 0x04000272 RID: 626
		[SerializeField]
		internal FixedBytes4094 bytes;

		// Token: 0x020000A0 RID: 160
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060004B3 RID: 1203 RVA: 0x0000B979 File Offset: 0x00009B79
			public Enumerator(FixedString4096Bytes other)
			{
				this.target = other;
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x060004B4 RID: 1204 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x060004B5 RID: 1205 RVA: 0x0000B995 File Offset: 0x00009B95
			public bool MoveNext()
			{
				if (this.offset >= this.target.Length)
				{
					return false;
				}
				Unicode.Utf8ToUcs(out this.current, this.target.GetUnsafePtr(), ref this.offset, this.target.Length);
				return true;
			}

			// Token: 0x060004B6 RID: 1206 RVA: 0x0000B9D5 File Offset: 0x00009BD5
			public void Reset()
			{
				this.offset = 0;
				this.current = default(Unicode.Rune);
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000B9EA File Offset: 0x00009BEA
			public Unicode.Rune Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000B9F2 File Offset: 0x00009BF2
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000273 RID: 627
			private FixedString4096Bytes target;

			// Token: 0x04000274 RID: 628
			private int offset;

			// Token: 0x04000275 RID: 629
			private Unicode.Rune current;
		}
	}
}
