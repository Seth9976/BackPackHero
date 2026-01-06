using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F3 RID: 243
	public struct InputStateBlock
	{
		// Token: 0x06000E38 RID: 3640 RVA: 0x000451E0 File Offset: 0x000433E0
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

		// Token: 0x06000E39 RID: 3641 RVA: 0x00045310 File Offset: 0x00043510
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

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0004541D File Offset: 0x0004361D
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x00045425 File Offset: 0x00043625
		public FourCC format { readonly get; set; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0004542E File Offset: 0x0004362E
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x00045436 File Offset: 0x00043636
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

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0004543F File Offset: 0x0004363F
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00045447 File Offset: 0x00043647
		public uint bitOffset { readonly get; set; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00045450 File Offset: 0x00043650
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x00045458 File Offset: 0x00043658
		public uint sizeInBits { readonly get; set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00045461 File Offset: 0x00043661
		internal uint alignedSizeInBytes
		{
			get
			{
				return this.sizeInBits + 7U >> 3;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0004546D File Offset: 0x0004366D
		internal uint effectiveByteOffset
		{
			get
			{
				return this.byteOffset + (this.bitOffset >> 3);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0004547E File Offset: 0x0004367E
		internal uint effectiveBitOffset
		{
			get
			{
				return this.byteOffset * 8U + this.bitOffset;
			}
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00045490 File Offset: 0x00043690
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

		// Token: 0x06000E46 RID: 3654 RVA: 0x000455B4 File Offset: 0x000437B4
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

		// Token: 0x06000E47 RID: 3655 RVA: 0x000456D8 File Offset: 0x000438D8
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

		// Token: 0x06000E48 RID: 3656 RVA: 0x000458A4 File Offset: 0x00043AA4
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

		// Token: 0x06000E49 RID: 3657 RVA: 0x00045A78 File Offset: 0x00043C78
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

		// Token: 0x06000E4A RID: 3658 RVA: 0x00045C68 File Offset: 0x00043E68
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

		// Token: 0x06000E4B RID: 3659 RVA: 0x00045E40 File Offset: 0x00044040
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

		// Token: 0x06000E4C RID: 3660 RVA: 0x00046024 File Offset: 0x00044224
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

		// Token: 0x06000E4D RID: 3661 RVA: 0x000461B4 File Offset: 0x000443B4
		public unsafe void CopyToFrom(void* toStatePtr, void* fromStatePtr)
		{
			if (this.bitOffset != 0U || this.sizeInBits % 8U != 0U)
			{
				throw new NotImplementedException("Copying bitfields");
			}
			byte* ptr = (byte*)fromStatePtr + this.byteOffset;
			UnsafeUtility.MemCpy((void*)((byte*)toStatePtr + this.byteOffset), (void*)ptr, (long)((ulong)this.alignedSizeInBytes));
		}

		// Token: 0x040005B5 RID: 1461
		public const uint InvalidOffset = 4294967295U;

		// Token: 0x040005B6 RID: 1462
		public const uint AutomaticOffset = 4294967294U;

		// Token: 0x040005B7 RID: 1463
		public static readonly FourCC FormatInvalid = new FourCC(0);

		// Token: 0x040005B8 RID: 1464
		internal const int kFormatInvalid = 0;

		// Token: 0x040005B9 RID: 1465
		public static readonly FourCC FormatBit = new FourCC('B', 'I', 'T', ' ');

		// Token: 0x040005BA RID: 1466
		internal const int kFormatBit = 1112101920;

		// Token: 0x040005BB RID: 1467
		public static readonly FourCC FormatSBit = new FourCC('S', 'B', 'I', 'T');

		// Token: 0x040005BC RID: 1468
		internal const int kFormatSBit = 1396853076;

		// Token: 0x040005BD RID: 1469
		public static readonly FourCC FormatInt = new FourCC('I', 'N', 'T', ' ');

		// Token: 0x040005BE RID: 1470
		internal const int kFormatInt = 1229870112;

		// Token: 0x040005BF RID: 1471
		public static readonly FourCC FormatUInt = new FourCC('U', 'I', 'N', 'T');

		// Token: 0x040005C0 RID: 1472
		internal const int kFormatUInt = 1430867540;

		// Token: 0x040005C1 RID: 1473
		public static readonly FourCC FormatShort = new FourCC('S', 'H', 'R', 'T');

		// Token: 0x040005C2 RID: 1474
		internal const int kFormatShort = 1397248596;

		// Token: 0x040005C3 RID: 1475
		public static readonly FourCC FormatUShort = new FourCC('U', 'S', 'H', 'T');

		// Token: 0x040005C4 RID: 1476
		internal const int kFormatUShort = 1431521364;

		// Token: 0x040005C5 RID: 1477
		public static readonly FourCC FormatByte = new FourCC('B', 'Y', 'T', 'E');

		// Token: 0x040005C6 RID: 1478
		internal const int kFormatByte = 1113150533;

		// Token: 0x040005C7 RID: 1479
		public static readonly FourCC FormatSByte = new FourCC('S', 'B', 'Y', 'T');

		// Token: 0x040005C8 RID: 1480
		internal const int kFormatSByte = 1396857172;

		// Token: 0x040005C9 RID: 1481
		public static readonly FourCC FormatLong = new FourCC('L', 'N', 'G', ' ');

		// Token: 0x040005CA RID: 1482
		internal const int kFormatLong = 1280198432;

		// Token: 0x040005CB RID: 1483
		public static readonly FourCC FormatULong = new FourCC('U', 'L', 'N', 'G');

		// Token: 0x040005CC RID: 1484
		internal const int kFormatULong = 1431064135;

		// Token: 0x040005CD RID: 1485
		public static readonly FourCC FormatFloat = new FourCC('F', 'L', 'T', ' ');

		// Token: 0x040005CE RID: 1486
		internal const int kFormatFloat = 1179407392;

		// Token: 0x040005CF RID: 1487
		public static readonly FourCC FormatDouble = new FourCC('D', 'B', 'L', ' ');

		// Token: 0x040005D0 RID: 1488
		internal const int kFormatDouble = 1145195552;

		// Token: 0x040005D1 RID: 1489
		public static readonly FourCC FormatVector2 = new FourCC('V', 'E', 'C', '2');

		// Token: 0x040005D2 RID: 1490
		internal const int kFormatVector2 = 1447379762;

		// Token: 0x040005D3 RID: 1491
		public static readonly FourCC FormatVector3 = new FourCC('V', 'E', 'C', '3');

		// Token: 0x040005D4 RID: 1492
		internal const int kFormatVector3 = 1447379763;

		// Token: 0x040005D5 RID: 1493
		public static readonly FourCC FormatQuaternion = new FourCC('Q', 'U', 'A', 'T');

		// Token: 0x040005D6 RID: 1494
		internal const int kFormatQuaternion = 1364541780;

		// Token: 0x040005D7 RID: 1495
		public static readonly FourCC FormatVector2Short = new FourCC('V', 'C', '2', 'S');

		// Token: 0x040005D8 RID: 1496
		public static readonly FourCC FormatVector3Short = new FourCC('V', 'C', '3', 'S');

		// Token: 0x040005D9 RID: 1497
		public static readonly FourCC FormatVector2Byte = new FourCC('V', 'C', '2', 'B');

		// Token: 0x040005DA RID: 1498
		public static readonly FourCC FormatVector3Byte = new FourCC('V', 'C', '3', 'B');

		// Token: 0x040005DB RID: 1499
		public static readonly FourCC FormatPose = new FourCC('P', 'o', 's', 'e');

		// Token: 0x040005DC RID: 1500
		internal const int kFormatPose = 1349481317;

		// Token: 0x040005DE RID: 1502
		internal uint m_ByteOffset;
	}
}
