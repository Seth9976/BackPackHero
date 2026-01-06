using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F3 RID: 243
	public struct InputStateBlock
	{
		// Token: 0x06000E33 RID: 3635 RVA: 0x00045194 File Offset: 0x00043394
		public static int GetSizeOfPrimitiveFormatInBits(FourCC type)
		{
			if (type == InputStateBlock.FormatBit || type == InputStateBlock.FormatSBit)
			{
				return 1;
			}
			if (type == InputStateBlock.FormatInt || type == InputStateBlock.FormatUInt)
			{
				return 32;
			}
			if (type == InputStateBlock.FormatShort || type == InputStateBlock.FormatUShort)
			{
				return 16;
			}
			if (type == InputStateBlock.FormatByte || type == InputStateBlock.FormatSByte)
			{
				return 8;
			}
			if (type == InputStateBlock.FormatLong || type == InputStateBlock.FormatULong)
			{
				return 64;
			}
			if (type == InputStateBlock.FormatFloat)
			{
				return 32;
			}
			if (type == InputStateBlock.FormatDouble)
			{
				return 64;
			}
			if (type == InputStateBlock.FormatVector2)
			{
				return 64;
			}
			if (type == InputStateBlock.FormatVector3)
			{
				return 96;
			}
			if (type == InputStateBlock.FormatQuaternion)
			{
				return 128;
			}
			if (type == InputStateBlock.FormatVector2Short)
			{
				return 32;
			}
			if (type == InputStateBlock.FormatVector3Short)
			{
				return 48;
			}
			if (type == InputStateBlock.FormatVector2Byte)
			{
				return 16;
			}
			if (type == InputStateBlock.FormatVector3Byte)
			{
				return 24;
			}
			return -1;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000452C4 File Offset: 0x000434C4
		public static FourCC GetPrimitiveFormatFromType(Type type)
		{
			if (type == typeof(int))
			{
				return InputStateBlock.FormatInt;
			}
			if (type == typeof(uint))
			{
				return InputStateBlock.FormatUInt;
			}
			if (type == typeof(short))
			{
				return InputStateBlock.FormatShort;
			}
			if (type == typeof(ushort))
			{
				return InputStateBlock.FormatUShort;
			}
			if (type == typeof(byte))
			{
				return InputStateBlock.FormatByte;
			}
			if (type == typeof(sbyte))
			{
				return InputStateBlock.FormatSByte;
			}
			if (type == typeof(long))
			{
				return InputStateBlock.FormatLong;
			}
			if (type == typeof(ulong))
			{
				return InputStateBlock.FormatULong;
			}
			if (type == typeof(float))
			{
				return InputStateBlock.FormatFloat;
			}
			if (type == typeof(double))
			{
				return InputStateBlock.FormatDouble;
			}
			if (type == typeof(Vector2))
			{
				return InputStateBlock.FormatVector2;
			}
			if (type == typeof(Vector3))
			{
				return InputStateBlock.FormatVector3;
			}
			if (type == typeof(Quaternion))
			{
				return InputStateBlock.FormatQuaternion;
			}
			return default(FourCC);
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x000453D1 File Offset: 0x000435D1
		// (set) Token: 0x06000E36 RID: 3638 RVA: 0x000453D9 File Offset: 0x000435D9
		public FourCC format { readonly get; set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x000453E2 File Offset: 0x000435E2
		// (set) Token: 0x06000E38 RID: 3640 RVA: 0x000453EA File Offset: 0x000435EA
		public uint byteOffset
		{
			get
			{
				return this.m_ByteOffset;
			}
			set
			{
				this.m_ByteOffset = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x000453F3 File Offset: 0x000435F3
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x000453FB File Offset: 0x000435FB
		public uint bitOffset { readonly get; set; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x00045404 File Offset: 0x00043604
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x0004540C File Offset: 0x0004360C
		public uint sizeInBits { readonly get; set; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x00045415 File Offset: 0x00043615
		internal uint alignedSizeInBytes
		{
			get
			{
				return this.sizeInBits + 7U >> 3;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00045421 File Offset: 0x00043621
		internal uint effectiveByteOffset
		{
			get
			{
				return this.byteOffset + (this.bitOffset >> 3);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x00045432 File Offset: 0x00043632
		internal uint effectiveBitOffset
		{
			get
			{
				return this.byteOffset * 8U + this.bitOffset;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00045444 File Offset: 0x00043644
		public unsafe int ReadInt(void* statePtr)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1396853076)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num != 1113150533)
						{
							goto IL_00FA;
						}
						return (int)(*ptr);
					}
					else
					{
						if (this.sizeInBits != 1U)
						{
							return (int)MemoryHelpers.ReadMultipleBitsAsUInt((void*)ptr, this.bitOffset, this.sizeInBits);
						}
						if (!MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset))
						{
							return 0;
						}
						return 1;
					}
				}
				else if (num != 1229870112)
				{
					if (num != 1396853076)
					{
						goto IL_00FA;
					}
					if (this.sizeInBits != 1U)
					{
						return MemoryHelpers.ReadExcessKMultipleBitsAsInt((void*)ptr, this.bitOffset, this.sizeInBits);
					}
					if (!MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset))
					{
						return -1;
					}
					return 1;
				}
			}
			else if (num <= 1397248596)
			{
				if (num == 1396857172)
				{
					return (int)(*(sbyte*)ptr);
				}
				if (num != 1397248596)
				{
					goto IL_00FA;
				}
				return (int)(*(short*)ptr);
			}
			else if (num != 1430867540)
			{
				if (num != 1431521364)
				{
					goto IL_00FA;
				}
				return (int)(*(ushort*)ptr);
			}
			return *(int*)ptr;
			IL_00FA:
			throw new InvalidOperationException(string.Format("State format '{0}' is not supported as integer format", this.format));
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00045568 File Offset: 0x00043768
		public unsafe void WriteInt(void* statePtr, int value)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1396853076)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num != 1113150533)
						{
							goto IL_00FB;
						}
						*ptr = (byte)value;
						return;
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value != 0);
							return;
						}
						MemoryHelpers.WriteUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, (uint)value);
						return;
					}
				}
				else if (num != 1229870112)
				{
					if (num != 1396853076)
					{
						goto IL_00FB;
					}
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value > 0);
						return;
					}
					MemoryHelpers.WriteIntAsExcessKMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, value);
					return;
				}
			}
			else if (num <= 1397248596)
			{
				if (num == 1396857172)
				{
					*ptr = (byte)((sbyte)value);
					return;
				}
				if (num != 1397248596)
				{
					goto IL_00FB;
				}
				*(short*)ptr = (short)value;
				return;
			}
			else if (num != 1430867540)
			{
				if (num != 1431521364)
				{
					goto IL_00FB;
				}
				*(short*)ptr = (short)((ushort)value);
				return;
			}
			*(int*)ptr = value;
			return;
			IL_00FB:
			throw new Exception(string.Format("State format '{0}' is not supported as integer format", this.format));
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0004568C File Offset: 0x0004388C
		public unsafe float ReadFloat(void* statePtr)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							return NumberHelpers.UIntToNormalizedFloat((uint)(*ptr), 0U, 255U);
						}
					}
					else
					{
						if (this.sizeInBits != 1U)
						{
							return MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)ptr, this.bitOffset, this.sizeInBits);
						}
						if (!MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset))
						{
							return 0f;
						}
						return 1f;
					}
				}
				else
				{
					if (num == 1145195552)
					{
						return (float)(*(double*)ptr);
					}
					if (num == 1179407392)
					{
						return *(float*)ptr;
					}
					if (num == 1229870112)
					{
						return NumberHelpers.IntToNormalizedFloat(*(int*)ptr, int.MinValue, int.MaxValue) * 2f - 1f;
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						return NumberHelpers.IntToNormalizedFloat((int)(*(sbyte*)ptr), -128, 127) * 2f - 1f;
					}
				}
				else
				{
					if (this.sizeInBits != 1U)
					{
						return MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)ptr, this.bitOffset, this.sizeInBits) * 2f - 1f;
					}
					if (!MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset))
					{
						return -1f;
					}
					return 1f;
				}
			}
			else
			{
				if (num == 1397248596)
				{
					return NumberHelpers.IntToNormalizedFloat((int)(*(short*)ptr), -32768, 32767) * 2f - 1f;
				}
				if (num == 1430867540)
				{
					return NumberHelpers.UIntToNormalizedFloat(*(uint*)ptr, 0U, uint.MaxValue);
				}
				if (num == 1431521364)
				{
					return NumberHelpers.UIntToNormalizedFloat((uint)(*(ushort*)ptr), 0U, 65535U);
				}
			}
			throw new InvalidOperationException(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00045858 File Offset: 0x00043A58
		public unsafe void WriteFloat(void* statePtr, float value)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							*ptr = (byte)NumberHelpers.NormalizedFloatToUInt(value, 0U, 255U);
							return;
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value >= 0.5f);
							return;
						}
						MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, value);
						return;
					}
				}
				else
				{
					if (num == 1145195552)
					{
						*(double*)ptr = (double)value;
						return;
					}
					if (num == 1179407392)
					{
						*(float*)ptr = value;
						return;
					}
					if (num == 1229870112)
					{
						*(int*)ptr = NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
						return;
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						*ptr = (byte)((sbyte)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -128, 127));
						return;
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value >= 0f);
						return;
					}
					MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, value * 0.5f + 0.5f);
					return;
				}
			}
			else
			{
				if (num == 1397248596)
				{
					*(short*)ptr = (short)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -32768, 32767);
					return;
				}
				if (num == 1430867540)
				{
					*(int*)ptr = (int)NumberHelpers.NormalizedFloatToUInt(value, 0U, uint.MaxValue);
					return;
				}
				if (num == 1431521364)
				{
					*(short*)ptr = (short)((ushort)NumberHelpers.NormalizedFloatToUInt(value, 0U, 65535U));
					return;
				}
			}
			throw new Exception(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00045A2C File Offset: 0x00043C2C
		internal PrimitiveValue FloatToPrimitiveValue(float value)
		{
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							return (byte)NumberHelpers.NormalizedFloatToUInt(value, 0U, 255U);
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							return value >= 0.5f;
						}
						return (int)NumberHelpers.NormalizedFloatToUInt(value, 0U, (uint)((1L << (int)this.sizeInBits) - 1L));
					}
				}
				else
				{
					if (num == 1145195552)
					{
						return value;
					}
					if (num == 1179407392)
					{
						return value;
					}
					if (num == 1229870112)
					{
						return NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						return (sbyte)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -128, 127);
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						return value >= 0f;
					}
					int num2 = (int)(-(int)((int)1L << (int)(this.sizeInBits - 1U)));
					int num3 = (int)((1L << (int)(this.sizeInBits - 1U)) - 1L);
					return NumberHelpers.NormalizedFloatToInt(value, num2, num3);
				}
			}
			else
			{
				if (num == 1397248596)
				{
					return (short)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -32768, 32767);
				}
				if (num == 1430867540)
				{
					return NumberHelpers.NormalizedFloatToUInt(value, 0U, uint.MaxValue);
				}
				if (num == 1431521364)
				{
					return (ushort)NumberHelpers.NormalizedFloatToUInt(value, 0U, 65535U);
				}
			}
			throw new Exception(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00045C1C File Offset: 0x00043E1C
		public unsafe double ReadDouble(void* statePtr)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							return (double)NumberHelpers.UIntToNormalizedFloat((uint)(*ptr), 0U, 255U);
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							return (double)(MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset) ? 1f : 0f);
						}
						return (double)MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)ptr, this.bitOffset, this.sizeInBits);
					}
				}
				else
				{
					if (num == 1145195552)
					{
						return *(double*)ptr;
					}
					if (num == 1179407392)
					{
						return (double)(*(float*)ptr);
					}
					if (num == 1229870112)
					{
						return (double)(NumberHelpers.IntToNormalizedFloat(*(int*)ptr, int.MinValue, int.MaxValue) * 2f - 1f);
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						return (double)(NumberHelpers.IntToNormalizedFloat((int)(*(sbyte*)ptr), -128, 127) * 2f - 1f);
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						return (double)(MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset) ? 1f : (-1f));
					}
					return (double)(MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)ptr, this.bitOffset, this.sizeInBits) * 2f - 1f);
				}
			}
			else
			{
				if (num == 1397248596)
				{
					return (double)(NumberHelpers.IntToNormalizedFloat((int)(*(short*)ptr), -32768, 32767) * 2f - 1f);
				}
				if (num == 1430867540)
				{
					return (double)NumberHelpers.UIntToNormalizedFloat(*(uint*)ptr, 0U, uint.MaxValue);
				}
				if (num == 1431521364)
				{
					return (double)NumberHelpers.UIntToNormalizedFloat((uint)(*(ushort*)ptr), 0U, 65535U);
				}
			}
			throw new Exception(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00045DF4 File Offset: 0x00043FF4
		public unsafe void WriteDouble(void* statePtr, double value)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							*ptr = (byte)NumberHelpers.NormalizedFloatToUInt((float)value, 0U, 255U);
							return;
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value >= 0.5);
							return;
						}
						MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, (float)value);
						return;
					}
				}
				else
				{
					if (num == 1145195552)
					{
						*(double*)ptr = value;
						return;
					}
					if (num == 1179407392)
					{
						*(float*)ptr = (float)value;
						return;
					}
					if (num == 1229870112)
					{
						*(int*)ptr = NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
						return;
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						*ptr = (byte)((sbyte)NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, -128, 127));
						return;
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value >= 0.0);
						return;
					}
					MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, (float)value * 0.5f + 0.5f);
					return;
				}
			}
			else
			{
				if (num == 1397248596)
				{
					*(short*)ptr = (short)NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, -32768, 32767);
					return;
				}
				if (num == 1430867540)
				{
					*(int*)ptr = (int)NumberHelpers.NormalizedFloatToUInt((float)value, 0U, uint.MaxValue);
					return;
				}
				if (num == 1431521364)
				{
					*(short*)ptr = (short)((ushort)NumberHelpers.NormalizedFloatToUInt((float)value, 0U, 65535U));
					return;
				}
			}
			throw new InvalidOperationException(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00045FD8 File Offset: 0x000441D8
		public unsafe void Write(void* statePtr, PrimitiveValue value)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							*ptr = value.ToByte(null);
							return;
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value.ToBoolean(null));
							return;
						}
						MemoryHelpers.WriteUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, value.ToUInt32(null));
						return;
					}
				}
				else
				{
					if (num == 1179407392)
					{
						*(float*)ptr = value.ToSingle(null);
						return;
					}
					if (num == 1229870112)
					{
						*(int*)ptr = value.ToInt32(null);
						return;
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						*ptr = (byte)value.ToSByte(null);
						return;
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value.ToBoolean(null));
						return;
					}
					MemoryHelpers.WriteIntAsExcessKMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, value.ToInt32(null));
					return;
				}
			}
			else
			{
				if (num == 1397248596)
				{
					*(short*)ptr = value.ToInt16(null);
					return;
				}
				if (num == 1430867540)
				{
					*(int*)ptr = (int)value.ToUInt32(null);
					return;
				}
				if (num == 1431521364)
				{
					*(short*)ptr = (short)value.ToUInt16(null);
					return;
				}
			}
			throw new NotImplementedException(string.Format("Writing primitive value of type '{0}' into state block with format '{1}'", value.type, this.format));
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00046168 File Offset: 0x00044368
		public unsafe void CopyToFrom(void* toStatePtr, void* fromStatePtr)
		{
			if (this.bitOffset != 0U || this.sizeInBits % 8U != 0U)
			{
				throw new NotImplementedException("Copying bitfields");
			}
			byte* ptr = (byte*)fromStatePtr + this.byteOffset;
			UnsafeUtility.MemCpy((void*)((byte*)toStatePtr + this.byteOffset), (void*)ptr, (long)((ulong)this.alignedSizeInBytes));
		}

		// Token: 0x040005B4 RID: 1460
		public const uint InvalidOffset = 4294967295U;

		// Token: 0x040005B5 RID: 1461
		public const uint AutomaticOffset = 4294967294U;

		// Token: 0x040005B6 RID: 1462
		public static readonly FourCC FormatInvalid = new FourCC(0);

		// Token: 0x040005B7 RID: 1463
		internal const int kFormatInvalid = 0;

		// Token: 0x040005B8 RID: 1464
		public static readonly FourCC FormatBit = new FourCC('B', 'I', 'T', ' ');

		// Token: 0x040005B9 RID: 1465
		internal const int kFormatBit = 1112101920;

		// Token: 0x040005BA RID: 1466
		public static readonly FourCC FormatSBit = new FourCC('S', 'B', 'I', 'T');

		// Token: 0x040005BB RID: 1467
		internal const int kFormatSBit = 1396853076;

		// Token: 0x040005BC RID: 1468
		public static readonly FourCC FormatInt = new FourCC('I', 'N', 'T', ' ');

		// Token: 0x040005BD RID: 1469
		internal const int kFormatInt = 1229870112;

		// Token: 0x040005BE RID: 1470
		public static readonly FourCC FormatUInt = new FourCC('U', 'I', 'N', 'T');

		// Token: 0x040005BF RID: 1471
		internal const int kFormatUInt = 1430867540;

		// Token: 0x040005C0 RID: 1472
		public static readonly FourCC FormatShort = new FourCC('S', 'H', 'R', 'T');

		// Token: 0x040005C1 RID: 1473
		internal const int kFormatShort = 1397248596;

		// Token: 0x040005C2 RID: 1474
		public static readonly FourCC FormatUShort = new FourCC('U', 'S', 'H', 'T');

		// Token: 0x040005C3 RID: 1475
		internal const int kFormatUShort = 1431521364;

		// Token: 0x040005C4 RID: 1476
		public static readonly FourCC FormatByte = new FourCC('B', 'Y', 'T', 'E');

		// Token: 0x040005C5 RID: 1477
		internal const int kFormatByte = 1113150533;

		// Token: 0x040005C6 RID: 1478
		public static readonly FourCC FormatSByte = new FourCC('S', 'B', 'Y', 'T');

		// Token: 0x040005C7 RID: 1479
		internal const int kFormatSByte = 1396857172;

		// Token: 0x040005C8 RID: 1480
		public static readonly FourCC FormatLong = new FourCC('L', 'N', 'G', ' ');

		// Token: 0x040005C9 RID: 1481
		internal const int kFormatLong = 1280198432;

		// Token: 0x040005CA RID: 1482
		public static readonly FourCC FormatULong = new FourCC('U', 'L', 'N', 'G');

		// Token: 0x040005CB RID: 1483
		internal const int kFormatULong = 1431064135;

		// Token: 0x040005CC RID: 1484
		public static readonly FourCC FormatFloat = new FourCC('F', 'L', 'T', ' ');

		// Token: 0x040005CD RID: 1485
		internal const int kFormatFloat = 1179407392;

		// Token: 0x040005CE RID: 1486
		public static readonly FourCC FormatDouble = new FourCC('D', 'B', 'L', ' ');

		// Token: 0x040005CF RID: 1487
		internal const int kFormatDouble = 1145195552;

		// Token: 0x040005D0 RID: 1488
		public static readonly FourCC FormatVector2 = new FourCC('V', 'E', 'C', '2');

		// Token: 0x040005D1 RID: 1489
		internal const int kFormatVector2 = 1447379762;

		// Token: 0x040005D2 RID: 1490
		public static readonly FourCC FormatVector3 = new FourCC('V', 'E', 'C', '3');

		// Token: 0x040005D3 RID: 1491
		internal const int kFormatVector3 = 1447379763;

		// Token: 0x040005D4 RID: 1492
		public static readonly FourCC FormatQuaternion = new FourCC('Q', 'U', 'A', 'T');

		// Token: 0x040005D5 RID: 1493
		internal const int kFormatQuaternion = 1364541780;

		// Token: 0x040005D6 RID: 1494
		public static readonly FourCC FormatVector2Short = new FourCC('V', 'C', '2', 'S');

		// Token: 0x040005D7 RID: 1495
		public static readonly FourCC FormatVector3Short = new FourCC('V', 'C', '3', 'S');

		// Token: 0x040005D8 RID: 1496
		public static readonly FourCC FormatVector2Byte = new FourCC('V', 'C', '2', 'B');

		// Token: 0x040005D9 RID: 1497
		public static readonly FourCC FormatVector3Byte = new FourCC('V', 'C', '3', 'B');

		// Token: 0x040005DA RID: 1498
		public static readonly FourCC FormatPose = new FourCC('P', 'o', 's', 'e');

		// Token: 0x040005DB RID: 1499
		internal const int kFormatPose = 1349481317;

		// Token: 0x040005DD RID: 1501
		internal uint m_ByteOffset;
	}
}
