using System;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.ProviderBase
{
	// Token: 0x02000308 RID: 776
	internal abstract class DbBuffer : SafeHandle
	{
		// Token: 0x06002339 RID: 9017 RVA: 0x000A28C0 File Offset: 0x000A0AC0
		protected DbBuffer(int initialSize)
			: base(IntPtr.Zero, true)
		{
			if (0 < initialSize)
			{
				this._bufferLength = initialSize;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this.handle = SafeNativeMethods.LocalAlloc((IntPtr)initialSize);
				}
				if (IntPtr.Zero == this.handle)
				{
					throw new OutOfMemoryException();
				}
			}
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000A2928 File Offset: 0x000A0B28
		protected DbBuffer(IntPtr invalidHandleValue, bool ownsHandle)
			: base(invalidHandleValue, ownsHandle)
		{
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x00005AE9 File Offset: 0x00003CE9
		private int BaseOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x0007DD20 File Offset: 0x0007BF20
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x000A2932 File Offset: 0x000A0B32
		internal int Length
		{
			get
			{
				return this._bufferLength;
			}
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000A293C File Offset: 0x000A0B3C
		internal string PtrToStringUni(int offset)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2);
			string text = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				text = Marshal.PtrToStringUni(ADP.IntPtrOffset(base.DangerousGetHandle(), offset));
				this.Validate(offset, 2 * (text.Length + 1));
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return text;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000A29AC File Offset: 0x000A0BAC
		internal string PtrToStringUni(int offset, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			string text = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				text = Marshal.PtrToStringUni(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return text;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000A2A10 File Offset: 0x000A0C10
		internal byte ReadByte(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			byte b;
			try
			{
				base.DangerousAddRef(ref flag);
				b = Marshal.ReadByte(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return b;
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x000A2A60 File Offset: 0x000A0C60
		internal byte[] ReadBytes(int offset, int length)
		{
			byte[] array = new byte[length];
			return this.ReadBytes(offset, array, 0, length);
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000A2A80 File Offset: 0x000A0C80
		internal byte[] ReadBytes(int offset, byte[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return destination;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000A2AE4 File Offset: 0x000A0CE4
		internal char ReadChar(int offset)
		{
			return (char)this.ReadInt16(offset);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000A2AF0 File Offset: 0x000A0CF0
		internal char[] ReadChars(int offset, char[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return destination;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000A2B54 File Offset: 0x000A0D54
		internal double ReadDouble(int offset)
		{
			return BitConverter.Int64BitsToDouble(this.ReadInt64(offset));
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000A2B64 File Offset: 0x000A0D64
		internal short ReadInt16(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			short num;
			try
			{
				base.DangerousAddRef(ref flag);
				num = Marshal.ReadInt16(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000A2BB4 File Offset: 0x000A0DB4
		internal void ReadInt16Array(int offset, short[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000A2C18 File Offset: 0x000A0E18
		internal int ReadInt32(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			int num;
			try
			{
				base.DangerousAddRef(ref flag);
				num = Marshal.ReadInt32(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000A2C68 File Offset: 0x000A0E68
		internal void ReadInt32Array(int offset, int[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000A2CCC File Offset: 0x000A0ECC
		internal long ReadInt64(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			long num;
			try
			{
				base.DangerousAddRef(ref flag);
				num = Marshal.ReadInt64(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000A2D1C File Offset: 0x000A0F1C
		internal IntPtr ReadIntPtr(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			IntPtr intPtr;
			try
			{
				base.DangerousAddRef(ref flag);
				intPtr = Marshal.ReadIntPtr(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return intPtr;
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000A2D6C File Offset: 0x000A0F6C
		internal unsafe float ReadSingle(int offset)
		{
			int num = this.ReadInt32(offset);
			return *(float*)(&num);
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000A2D88 File Offset: 0x000A0F88
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				SafeNativeMethods.LocalFree(handle);
			}
			return true;
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000A2DBC File Offset: 0x000A0FBC
		private void StructureToPtr(int offset, object structure)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.StructureToPtr(structure, intPtr, false);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000A2E14 File Offset: 0x000A1014
		internal void WriteByte(int offset, byte value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteByte(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000A2E64 File Offset: 0x000A1064
		internal void WriteBytes(int offset, byte[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000A2EC8 File Offset: 0x000A10C8
		internal void WriteCharArray(int offset, char[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000A2F2C File Offset: 0x000A112C
		internal void WriteDouble(int offset, double value)
		{
			this.WriteInt64(offset, BitConverter.DoubleToInt64Bits(value));
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000A2F3C File Offset: 0x000A113C
		internal void WriteInt16(int offset, short value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt16(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000A2F8C File Offset: 0x000A118C
		internal void WriteInt16Array(int offset, short[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000A2FF0 File Offset: 0x000A11F0
		internal void WriteInt32(int offset, int value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt32(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000A3040 File Offset: 0x000A1240
		internal void WriteInt32Array(int offset, int[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000A30A4 File Offset: 0x000A12A4
		internal void WriteInt64(int offset, long value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt64(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000A30F4 File Offset: 0x000A12F4
		internal void WriteIntPtr(int offset, IntPtr value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteIntPtr(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000A3144 File Offset: 0x000A1344
		internal unsafe void WriteSingle(int offset, float value)
		{
			this.WriteInt32(offset, *(int*)(&value));
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000A3154 File Offset: 0x000A1354
		internal void ZeroMemory()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				SafeNativeMethods.ZeroMemory(base.DangerousGetHandle(), this.Length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000A31A0 File Offset: 0x000A13A0
		internal Guid ReadGuid(int offset)
		{
			byte[] array = new byte[16];
			this.ReadBytes(offset, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000A31C7 File Offset: 0x000A13C7
		internal void WriteGuid(int offset, Guid value)
		{
			this.StructureToPtr(offset, value);
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000A31D8 File Offset: 0x000A13D8
		internal DateTime ReadDate(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000A3208 File Offset: 0x000A1408
		internal void WriteDate(int offset, DateTime value)
		{
			short[] array = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day
			};
			this.WriteInt16Array(offset, array, 0, 3);
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000A3248 File Offset: 0x000A1448
		internal TimeSpan ReadTime(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new TimeSpan((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000A3278 File Offset: 0x000A1478
		internal void WriteTime(int offset, TimeSpan value)
		{
			short[] array = new short[]
			{
				(short)value.Hours,
				(short)value.Minutes,
				(short)value.Seconds
			};
			this.WriteInt16Array(offset, array, 0, 3);
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000A32B8 File Offset: 0x000A14B8
		internal DateTime ReadDateTime(int offset)
		{
			short[] array = new short[6];
			this.ReadInt16Array(offset, array, 0, 6);
			int num = this.ReadInt32(offset + 12);
			DateTime dateTime = new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]), (int)((ushort)array[3]), (int)((ushort)array[4]), (int)((ushort)array[5]));
			return dateTime.AddTicks((long)(num / 100));
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000A330C File Offset: 0x000A150C
		internal void WriteDateTime(int offset, DateTime value)
		{
			int num = (int)(value.Ticks % 10000000L) * 100;
			short[] array = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day,
				(short)value.Hour,
				(short)value.Minute,
				(short)value.Second
			};
			this.WriteInt16Array(offset, array, 0, 6);
			this.WriteInt32(offset + 12, num);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000A338C File Offset: 0x000A158C
		internal decimal ReadNumeric(int offset)
		{
			byte[] array = new byte[20];
			this.ReadBytes(offset, array, 1, 19);
			int[] array2 = new int[]
			{
				0,
				0,
				0,
				(int)array[2] << 16
			};
			if (array[3] == 0)
			{
				array2[3] |= int.MinValue;
			}
			array2[0] = BitConverter.ToInt32(array, 4);
			array2[1] = BitConverter.ToInt32(array, 8);
			array2[2] = BitConverter.ToInt32(array, 12);
			if (BitConverter.ToInt32(array, 16) != 0)
			{
				throw ADP.NumericToDecimalOverflow();
			}
			return new decimal(array2);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000A3408 File Offset: 0x000A1608
		internal void WriteNumeric(int offset, decimal value, byte precision)
		{
			int[] bits = decimal.GetBits(value);
			byte[] array = new byte[20];
			array[1] = precision;
			Buffer.BlockCopy(bits, 14, array, 2, 2);
			array[3] = ((array[3] == 0) ? 1 : 0);
			Buffer.BlockCopy(bits, 0, array, 4, 12);
			array[16] = 0;
			array[17] = 0;
			array[18] = 0;
			array[19] = 0;
			this.WriteBytes(offset, array, 1, 19);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000A3468 File Offset: 0x000A1668
		[Conditional("DEBUG")]
		protected void ValidateCheck(int offset, int count)
		{
			this.Validate(offset, count);
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000A3472 File Offset: 0x000A1672
		protected void Validate(int offset, int count)
		{
			if (offset < 0 || count < 0 || this.Length < checked(offset + count))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer);
			}
		}

		// Token: 0x04001783 RID: 6019
		private readonly int _bufferLength;
	}
}
