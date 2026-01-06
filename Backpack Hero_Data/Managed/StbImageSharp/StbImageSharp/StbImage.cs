using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Hebron.Runtime;

namespace StbImageSharp
{
	// Token: 0x0200000C RID: 12
	public static class StbImage
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000029A0 File Offset: 0x00000BA0
		public static int NativeAllocations
		{
			get
			{
				return MemoryStats.Allocations;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000029A7 File Offset: 0x00000BA7
		private static int stbi__err(string str)
		{
			StbImage.stbi__g_failure_reason = str;
			return 0;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029B0 File Offset: 0x00000BB0
		public static byte stbi__get8(StbImage.stbi__context s)
		{
			int num = s.Stream.ReadByte();
			if (num == -1)
			{
				return 0;
			}
			return (byte)num;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029D1 File Offset: 0x00000BD1
		public static void stbi__skip(StbImage.stbi__context s, int skip)
		{
			s.Stream.Seek((long)skip, 1);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000029E2 File Offset: 0x00000BE2
		public static void stbi__rewind(StbImage.stbi__context s)
		{
			s.Stream.Seek(0L, 0);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029F3 File Offset: 0x00000BF3
		public static int stbi__at_eof(StbImage.stbi__context s)
		{
			if (s.Stream.Position != s.Stream.Length)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A10 File Offset: 0x00000C10
		public unsafe static int stbi__getn(StbImage.stbi__context s, byte* buf, int size)
		{
			if (s._tempBuffer == null || s._tempBuffer.Length < size)
			{
				s._tempBuffer = new byte[size * 2];
			}
			int num = s.Stream.Read(s._tempBuffer, 0, size);
			Marshal.Copy(s._tempBuffer, 0, new IntPtr((void*)buf), num);
			return num;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A66 File Offset: 0x00000C66
		public static int stbi__bmp_test(StbImage.stbi__context s)
		{
			int num = StbImage.stbi__bmp_test_raw(s);
			StbImage.stbi__rewind(s);
			return num;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A74 File Offset: 0x00000C74
		public unsafe static void* stbi__bmp_load(StbImage.stbi__context s, int* x, int* y, int* comp, int req_comp, StbImage.stbi__result_info* ri)
		{
			byte[][] array = Utility.CreateArray<byte>(256, 4);
			int num = 0;
			StbImage.stbi__bmp_data stbi__bmp_data = default(StbImage.stbi__bmp_data);
			stbi__bmp_data.all_a = 255U;
			if (StbImage.stbi__bmp_parse_header(s, &stbi__bmp_data) == null)
			{
				return null;
			}
			int num2 = ((s.img_y > 0U) ? 1 : 0);
			s.img_y = (uint)CRuntime.abs((int)s.img_y);
			if (s.img_y > 16777216U)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			if (s.img_x > 16777216U)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			uint mr = stbi__bmp_data.mr;
			uint mg = stbi__bmp_data.mg;
			uint mb = stbi__bmp_data.mb;
			uint ma = stbi__bmp_data.ma;
			uint num3 = stbi__bmp_data.all_a;
			if (stbi__bmp_data.hsz == 12)
			{
				if (stbi__bmp_data.bpp < 24)
				{
					num = (stbi__bmp_data.offset - stbi__bmp_data.extra_read - 24) / 3;
				}
			}
			else if (stbi__bmp_data.bpp < 16)
			{
				num = stbi__bmp_data.offset - stbi__bmp_data.extra_read - stbi__bmp_data.hsz >> 2;
			}
			if (stbi__bmp_data.bpp == 24 && ma == 4278190080U)
			{
				s.img_n = 3;
			}
			else
			{
				s.img_n = ((ma != 0U) ? 4 : 3);
			}
			int num4;
			if (req_comp != 0 && req_comp >= 3)
			{
				num4 = req_comp;
			}
			else
			{
				num4 = s.img_n;
			}
			if (StbImage.stbi__mad3sizes_valid(num4, (int)s.img_x, (int)s.img_y, 0) == 0)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			byte* ptr = (byte*)StbImage.stbi__malloc_mad3(num4, (int)s.img_x, (int)s.img_y, 0);
			if (ptr == null)
			{
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			if (stbi__bmp_data.bpp < 16)
			{
				int num5 = 0;
				if (num == 0 || num > 256)
				{
					CRuntime.free((void*)ptr);
					return (StbImage.stbi__err("invalid") != 0) ? null : null;
				}
				for (int i = 0; i < num; i++)
				{
					array[i][2] = StbImage.stbi__get8(s);
					array[i][1] = StbImage.stbi__get8(s);
					array[i][0] = StbImage.stbi__get8(s);
					if (stbi__bmp_data.hsz != 12)
					{
						StbImage.stbi__get8(s);
					}
					array[i][3] = byte.MaxValue;
				}
				StbImage.stbi__skip(s, stbi__bmp_data.offset - stbi__bmp_data.extra_read - stbi__bmp_data.hsz - num * ((stbi__bmp_data.hsz == 12) ? 3 : 4));
				int num6;
				if (stbi__bmp_data.bpp == 1)
				{
					num6 = (int)(s.img_x + 7U >> 3);
				}
				else if (stbi__bmp_data.bpp == 4)
				{
					num6 = (int)(s.img_x + 1U >> 1);
				}
				else
				{
					if (stbi__bmp_data.bpp != 8)
					{
						CRuntime.free((void*)ptr);
						return (StbImage.stbi__err("bad bpp") != 0) ? null : null;
					}
					num6 = (int)s.img_x;
				}
				int num7 = -num6 & 3;
				if (stbi__bmp_data.bpp == 1)
				{
					for (int j = 0; j < (int)s.img_y; j++)
					{
						int num8 = 7;
						int num9 = (int)StbImage.stbi__get8(s);
						for (int i = 0; i < (int)s.img_x; i++)
						{
							int num10 = (num9 >> num8) & 1;
							ptr[num5++] = array[num10][0];
							ptr[num5++] = array[num10][1];
							ptr[num5++] = array[num10][2];
							if (num4 == 4)
							{
								ptr[num5++] = byte.MaxValue;
							}
							if (i + 1 == (int)s.img_x)
							{
								break;
							}
							if (--num8 < 0)
							{
								num8 = 7;
								num9 = (int)StbImage.stbi__get8(s);
							}
						}
						StbImage.stbi__skip(s, num7);
					}
				}
				else
				{
					for (int j = 0; j < (int)s.img_y; j++)
					{
						for (int i = 0; i < (int)s.img_x; i += 2)
						{
							int num11 = (int)StbImage.stbi__get8(s);
							int num12 = 0;
							if (stbi__bmp_data.bpp == 4)
							{
								num12 = num11 & 15;
								num11 >>= 4;
							}
							ptr[num5++] = array[num11][0];
							ptr[num5++] = array[num11][1];
							ptr[num5++] = array[num11][2];
							if (num4 == 4)
							{
								ptr[num5++] = byte.MaxValue;
							}
							if (i + 1 == (int)s.img_x)
							{
								break;
							}
							num11 = ((stbi__bmp_data.bpp == 8) ? ((int)StbImage.stbi__get8(s)) : num12);
							ptr[num5++] = array[num11][0];
							ptr[num5++] = array[num11][1];
							ptr[num5++] = array[num11][2];
							if (num4 == 4)
							{
								ptr[num5++] = byte.MaxValue;
							}
						}
						StbImage.stbi__skip(s, num7);
					}
				}
			}
			else
			{
				int num13 = 0;
				int num14 = 0;
				int num15 = 0;
				int num16 = 0;
				int num17 = 0;
				int num18 = 0;
				int num19 = 0;
				int num20 = 0;
				int num21 = 0;
				int num22 = 0;
				StbImage.stbi__skip(s, stbi__bmp_data.offset - stbi__bmp_data.extra_read - stbi__bmp_data.hsz);
				int num6;
				if (stbi__bmp_data.bpp == 24)
				{
					num6 = (int)(3U * s.img_x);
				}
				else if (stbi__bmp_data.bpp == 16)
				{
					num6 = (int)(2U * s.img_x);
				}
				else
				{
					num6 = 0;
				}
				int num7 = -num6 & 3;
				if (stbi__bmp_data.bpp == 24)
				{
					num22 = 1;
				}
				else if (stbi__bmp_data.bpp == 32 && mb == 255U && mg == 65280U && mr == 16711680U && ma == 4278190080U)
				{
					num22 = 2;
				}
				if (num22 == 0)
				{
					if (mr == 0U || mg == 0U || mb == 0U)
					{
						CRuntime.free((void*)ptr);
						return (StbImage.stbi__err("bad masks") != 0) ? null : null;
					}
					num13 = StbImage.stbi__high_bit(mr) - 7;
					num17 = StbImage.stbi__bitcount(mr);
					num14 = StbImage.stbi__high_bit(mg) - 7;
					num18 = StbImage.stbi__bitcount(mg);
					num15 = StbImage.stbi__high_bit(mb) - 7;
					num19 = StbImage.stbi__bitcount(mb);
					num16 = StbImage.stbi__high_bit(ma) - 7;
					num20 = StbImage.stbi__bitcount(ma);
					if (num17 > 8 || num18 > 8 || num19 > 8 || num20 > 8)
					{
						CRuntime.free((void*)ptr);
						return (StbImage.stbi__err("bad masks") != 0) ? null : null;
					}
				}
				for (int j = 0; j < (int)s.img_y; j++)
				{
					if (num22 != 0)
					{
						for (int i = 0; i < (int)s.img_x; i++)
						{
							ptr[num21 + 2] = StbImage.stbi__get8(s);
							ptr[num21 + 1] = StbImage.stbi__get8(s);
							ptr[num21] = StbImage.stbi__get8(s);
							num21 += 3;
							byte b = ((num22 == 2) ? StbImage.stbi__get8(s) : byte.MaxValue);
							num3 |= (uint)b;
							if (num4 == 4)
							{
								ptr[num21++] = b;
							}
						}
					}
					else
					{
						int bpp = stbi__bmp_data.bpp;
						for (int i = 0; i < (int)s.img_x; i++)
						{
							uint num23 = (uint)((bpp == 16) ? StbImage.stbi__get16le(s) : ((int)StbImage.stbi__get32le(s)));
							ptr[num21++] = (byte)(StbImage.stbi__shiftsigned(num23 & mr, num13, num17) & 255);
							ptr[num21++] = (byte)(StbImage.stbi__shiftsigned(num23 & mg, num14, num18) & 255);
							ptr[num21++] = (byte)(StbImage.stbi__shiftsigned(num23 & mb, num15, num19) & 255);
							uint num24 = (uint)((ma != 0U) ? StbImage.stbi__shiftsigned(num23 & ma, num16, num20) : 255);
							num3 |= num24;
							if (num4 == 4)
							{
								ptr[num21++] = (byte)(num24 & 255U);
							}
						}
					}
					StbImage.stbi__skip(s, num7);
				}
			}
			if (num4 == 4 && num3 == 0U)
			{
				for (int i = (int)(4U * s.img_x * s.img_y - 1U); i >= 0; i -= 4)
				{
					ptr[i] = byte.MaxValue;
				}
			}
			if (num2 != 0)
			{
				for (int j = 0; j < (int)s.img_y >> 1; j++)
				{
					byte* ptr2 = ptr + (long)j * (long)((ulong)s.img_x) * (long)num4;
					byte* ptr3 = ptr + ((ulong)(s.img_y - 1U) - (ulong)((long)j)) * (ulong)s.img_x * (ulong)((long)num4);
					for (int i = 0; i < (int)(s.img_x * (uint)num4); i++)
					{
						byte b2 = ptr2[i];
						ptr2[i] = ptr3[i];
						ptr3[i] = b2;
					}
				}
			}
			if (req_comp != 0 && req_comp != num4)
			{
				ptr = StbImage.stbi__convert_format(ptr, num4, req_comp, s.img_x, s.img_y);
				if (ptr == null)
				{
					return (void*)ptr;
				}
			}
			*x = (int)s.img_x;
			*y = (int)s.img_y;
			if (comp != null)
			{
				*comp = s.img_n;
			}
			return (void*)ptr;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000330C File Offset: 0x0000150C
		public unsafe static int stbi__bmp_info(StbImage.stbi__context s, int* x, int* y, int* comp)
		{
			StbImage.stbi__bmp_data stbi__bmp_data = default(StbImage.stbi__bmp_data);
			stbi__bmp_data.all_a = 255U;
			if (StbImage.stbi__bmp_parse_header(s, &stbi__bmp_data) == null)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			if (x != null)
			{
				*x = (int)s.img_x;
			}
			if (y != null)
			{
				*y = (int)s.img_y;
			}
			if (comp != null)
			{
				if (stbi__bmp_data.bpp == 24 && stbi__bmp_data.ma == 4278190080U)
				{
					*comp = 3;
				}
				else
				{
					*comp = ((stbi__bmp_data.ma != 0U) ? 4 : 3);
				}
			}
			return 1;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000338C File Offset: 0x0000158C
		public static int stbi__bmp_test_raw(StbImage.stbi__context s)
		{
			if (StbImage.stbi__get8(s) != 66)
			{
				return 0;
			}
			if (StbImage.stbi__get8(s) != 77)
			{
				return 0;
			}
			StbImage.stbi__get32le(s);
			StbImage.stbi__get16le(s);
			StbImage.stbi__get16le(s);
			StbImage.stbi__get32le(s);
			int num = (int)StbImage.stbi__get32le(s);
			if (num != 12 && num != 40 && num != 56 && num != 108 && num != 124)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000033F4 File Offset: 0x000015F4
		public unsafe static int stbi__bmp_set_mask_defaults(StbImage.stbi__bmp_data* info, int compress)
		{
			if (compress == 3)
			{
				return 1;
			}
			if (compress == 0)
			{
				if (info->bpp == 16)
				{
					info->mr = 31744U;
					info->mg = 992U;
					info->mb = 31U;
				}
				else if (info->bpp == 32)
				{
					info->mr = 16711680U;
					info->mg = 65280U;
					info->mb = 255U;
					info->ma = 4278190080U;
					info->all_a = 0U;
				}
				else
				{
					info->mr = (info->mg = (info->mb = (info->ma = 0U)));
				}
				return 1;
			}
			return 0;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000349C File Offset: 0x0000169C
		public unsafe static void* stbi__bmp_parse_header(StbImage.stbi__context s, StbImage.stbi__bmp_data* info)
		{
			if (StbImage.stbi__get8(s) != 66 || StbImage.stbi__get8(s) != 77)
			{
				return (StbImage.stbi__err("not BMP") != 0) ? null : null;
			}
			StbImage.stbi__get32le(s);
			StbImage.stbi__get16le(s);
			StbImage.stbi__get16le(s);
			info->offset = (int)StbImage.stbi__get32le(s);
			int num = (info->hsz = (int)StbImage.stbi__get32le(s));
			info->mr = (info->mg = (info->mb = (info->ma = 0U)));
			info->extra_read = 14;
			if (info->offset < 0)
			{
				return (StbImage.stbi__err("bad BMP") != 0) ? null : null;
			}
			if (num != 12 && num != 40 && num != 56 && num != 108 && num != 124)
			{
				return (StbImage.stbi__err("unknown BMP") != 0) ? null : null;
			}
			if (num == 12)
			{
				s.img_x = (uint)StbImage.stbi__get16le(s);
				s.img_y = (uint)StbImage.stbi__get16le(s);
			}
			else
			{
				s.img_x = StbImage.stbi__get32le(s);
				s.img_y = StbImage.stbi__get32le(s);
			}
			if (StbImage.stbi__get16le(s) != 1)
			{
				return (StbImage.stbi__err("bad BMP") != 0) ? null : null;
			}
			info->bpp = StbImage.stbi__get16le(s);
			if (num != 12)
			{
				int num2 = (int)StbImage.stbi__get32le(s);
				if (num2 == 1 || num2 == 2)
				{
					return (StbImage.stbi__err("BMP RLE") != 0) ? null : null;
				}
				if (num2 >= 4)
				{
					return (StbImage.stbi__err("BMP JPEG/PNG") != 0) ? null : null;
				}
				if (num2 == 3 && info->bpp != 16 && info->bpp != 32)
				{
					return (StbImage.stbi__err("bad BMP") != 0) ? null : null;
				}
				StbImage.stbi__get32le(s);
				StbImage.stbi__get32le(s);
				StbImage.stbi__get32le(s);
				StbImage.stbi__get32le(s);
				StbImage.stbi__get32le(s);
				if (num == 40 || num == 56)
				{
					if (num == 56)
					{
						StbImage.stbi__get32le(s);
						StbImage.stbi__get32le(s);
						StbImage.stbi__get32le(s);
						StbImage.stbi__get32le(s);
					}
					if (info->bpp == 16 || info->bpp == 32)
					{
						if (num2 == 0)
						{
							StbImage.stbi__bmp_set_mask_defaults(info, num2);
						}
						else
						{
							if (num2 != 3)
							{
								return (StbImage.stbi__err("bad BMP") != 0) ? null : null;
							}
							info->mr = StbImage.stbi__get32le(s);
							info->mg = StbImage.stbi__get32le(s);
							info->mb = StbImage.stbi__get32le(s);
							info->extra_read = info->extra_read + 12;
							if (info->mr == info->mg && info->mg == info->mb)
							{
								return (StbImage.stbi__err("bad BMP") != 0) ? null : null;
							}
						}
					}
				}
				else
				{
					if (num != 108 && num != 124)
					{
						return (StbImage.stbi__err("bad BMP") != 0) ? null : null;
					}
					info->mr = StbImage.stbi__get32le(s);
					info->mg = StbImage.stbi__get32le(s);
					info->mb = StbImage.stbi__get32le(s);
					info->ma = StbImage.stbi__get32le(s);
					if (num2 != 3)
					{
						StbImage.stbi__bmp_set_mask_defaults(info, num2);
					}
					StbImage.stbi__get32le(s);
					for (int i = 0; i < 12; i++)
					{
						StbImage.stbi__get32le(s);
					}
					if (num == 124)
					{
						StbImage.stbi__get32le(s);
						StbImage.stbi__get32le(s);
						StbImage.stbi__get32le(s);
						StbImage.stbi__get32le(s);
					}
				}
			}
			return 1;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000037CB File Offset: 0x000019CB
		public static void stbi_hdr_to_ldr_gamma(float gamma)
		{
			StbImage.stbi__h2l_gamma_i = 1f / gamma;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000037D9 File Offset: 0x000019D9
		public static void stbi_hdr_to_ldr_scale(float scale)
		{
			StbImage.stbi__h2l_scale_i = 1f / scale;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000037E7 File Offset: 0x000019E7
		public static void stbi_ldr_to_hdr_gamma(float gamma)
		{
			StbImage.stbi__l2h_gamma = gamma;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000037EF File Offset: 0x000019EF
		public static void stbi_ldr_to_hdr_scale(float scale)
		{
			StbImage.stbi__l2h_scale = scale;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000037F7 File Offset: 0x000019F7
		public static void stbi_set_unpremultiply_on_load(int flag_true_if_should_unpremultiply)
		{
			StbImage.stbi__unpremultiply_on_load_global = flag_true_if_should_unpremultiply;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000037FF File Offset: 0x000019FF
		public static void stbi_convert_iphone_png_to_rgb(int flag_true_if_should_convert)
		{
			StbImage.stbi__de_iphone_flag_global = flag_true_if_should_convert;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003807 File Offset: 0x00001A07
		public static void stbi_set_flip_vertically_on_load(int flag_true_if_should_flip)
		{
			StbImage.stbi__vertically_flip_on_load_global = flag_true_if_should_flip;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000380F File Offset: 0x00001A0F
		public static void stbi_convert_iphone_png_to_rgb_thread(int flag_true_if_should_convert)
		{
			StbImage.stbi__de_iphone_flag_local = flag_true_if_should_convert;
			StbImage.stbi__de_iphone_flag_set = 1;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000381D File Offset: 0x00001A1D
		public static void stbi_set_flip_vertically_on_load_thread(int flag_true_if_should_flip)
		{
			StbImage.stbi__vertically_flip_on_load_local = flag_true_if_should_flip;
			StbImage.stbi__vertically_flip_on_load_set = 1;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000382B File Offset: 0x00001A2B
		public unsafe static void* stbi__malloc(ulong size)
		{
			return CRuntime.malloc(size);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003833 File Offset: 0x00001A33
		public static int stbi__addsizes_valid(int a, int b)
		{
			if (b < 0)
			{
				return 0;
			}
			if (a > 2147483647 - b)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003848 File Offset: 0x00001A48
		public static int stbi__mul2sizes_valid(int a, int b)
		{
			if (a < 0 || b < 0)
			{
				return 0;
			}
			if (b == 0)
			{
				return 1;
			}
			if (a > 2147483647 / b)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003866 File Offset: 0x00001A66
		public static int stbi__mad2sizes_valid(int a, int b, int add)
		{
			if (StbImage.stbi__mul2sizes_valid(a, b) == 0 || StbImage.stbi__addsizes_valid(a * b, add) == 0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000387F File Offset: 0x00001A7F
		public static int stbi__mad3sizes_valid(int a, int b, int c, int add)
		{
			if (StbImage.stbi__mul2sizes_valid(a, b) == 0 || StbImage.stbi__mul2sizes_valid(a * b, c) == 0 || StbImage.stbi__addsizes_valid(a * b * c, add) == 0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000038A5 File Offset: 0x00001AA5
		public static int stbi__mad4sizes_valid(int a, int b, int c, int d, int add)
		{
			if (StbImage.stbi__mul2sizes_valid(a, b) == 0 || StbImage.stbi__mul2sizes_valid(a * b, c) == 0 || StbImage.stbi__mul2sizes_valid(a * b * c, d) == 0 || StbImage.stbi__addsizes_valid(a * b * c * d, add) == 0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000038DB File Offset: 0x00001ADB
		public unsafe static void* stbi__malloc_mad2(int a, int b, int add)
		{
			if (StbImage.stbi__mad2sizes_valid(a, b, add) == 0)
			{
				return null;
			}
			return StbImage.stbi__malloc((ulong)((long)(a * b + add)));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000038F5 File Offset: 0x00001AF5
		public unsafe static void* stbi__malloc_mad3(int a, int b, int c, int add)
		{
			if (StbImage.stbi__mad3sizes_valid(a, b, c, add) == 0)
			{
				return null;
			}
			return StbImage.stbi__malloc((ulong)((long)(a * b * c + add)));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003912 File Offset: 0x00001B12
		public unsafe static void* stbi__malloc_mad4(int a, int b, int c, int d, int add)
		{
			if (StbImage.stbi__mad4sizes_valid(a, b, c, d, add) == 0)
			{
				return null;
			}
			return StbImage.stbi__malloc((ulong)((long)(a * b * c * d + add)));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003934 File Offset: 0x00001B34
		public unsafe static float* stbi__ldr_to_hdr(byte* data, int x, int y, int comp)
		{
			if (data == null)
			{
				return null;
			}
			float* ptr = (float*)StbImage.stbi__malloc_mad4(x, y, comp, 4, 0);
			if (ptr == null)
			{
				CRuntime.free((void*)data);
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			int num;
			if ((comp & 1) != 0)
			{
				num = comp;
			}
			else
			{
				num = comp - 1;
			}
			for (int i = 0; i < x * y; i++)
			{
				for (int j = 0; j < num; j++)
				{
					ptr[i * comp + j] = (float)(CRuntime.pow((double)((float)data[i * comp + j] / 255f), (double)StbImage.stbi__l2h_gamma) * (double)StbImage.stbi__l2h_scale);
				}
			}
			if (num < comp)
			{
				for (int i = 0; i < x * y; i++)
				{
					ptr[i * comp + num] = (float)data[i * comp + num] / 255f;
				}
			}
			CRuntime.free((void*)data);
			return ptr;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A00 File Offset: 0x00001C00
		public unsafe static void* stbi__load_main(StbImage.stbi__context s, int* x, int* y, int* comp, int req_comp, StbImage.stbi__result_info* ri, int bpc)
		{
			CRuntime.memset((void*)ri, 0, (ulong)((long)sizeof(StbImage.stbi__result_info)));
			ri->bits_per_channel = 8;
			ri->channel_order = 0;
			ri->num_channels = 0;
			if (StbImage.stbi__png_test(s) != 0)
			{
				return StbImage.stbi__png_load(s, x, y, comp, req_comp, ri);
			}
			if (StbImage.stbi__bmp_test(s) != 0)
			{
				return StbImage.stbi__bmp_load(s, x, y, comp, req_comp, ri);
			}
			if (StbImage.stbi__gif_test(s) != 0)
			{
				return StbImage.stbi__gif_load(s, x, y, comp, req_comp, ri);
			}
			if (StbImage.stbi__psd_test(s) != 0)
			{
				return StbImage.stbi__psd_load(s, x, y, comp, req_comp, ri, bpc);
			}
			if (StbImage.stbi__jpeg_test(s) != 0)
			{
				return StbImage.stbi__jpeg_load(s, x, y, comp, req_comp, ri);
			}
			if (StbImage.stbi__hdr_test(s) != 0)
			{
				return (void*)StbImage.stbi__hdr_to_ldr(StbImage.stbi__hdr_load(s, x, y, comp, req_comp, ri), *x, *y, (req_comp != 0) ? req_comp : (*comp));
			}
			if (StbImage.stbi__tga_test(s) != 0)
			{
				return StbImage.stbi__tga_load(s, x, y, comp, req_comp, ri);
			}
			return (StbImage.stbi__err("unknown image type") != 0) ? null : null;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public unsafe static byte* stbi__convert_16_to_8(ushort* orig, int w, int h, int channels)
		{
			int num = w * h * channels;
			byte* ptr = (byte*)StbImage.stbi__malloc((ulong)((long)num));
			if (ptr == null)
			{
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			for (int i = 0; i < num; i++)
			{
				ptr[i] = (byte)((orig[i] >> 8) & 255);
			}
			CRuntime.free((void*)orig);
			return ptr;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003B54 File Offset: 0x00001D54
		public unsafe static ushort* stbi__convert_8_to_16(byte* orig, int w, int h, int channels)
		{
			int num = w * h * channels;
			ushort* ptr = (ushort*)StbImage.stbi__malloc((ulong)((long)(num * 2)));
			if (ptr == null)
			{
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			for (int i = 0; i < num; i++)
			{
				ptr[i] = (ushort)(((int)orig[i] << 8) + (int)orig[i]);
			}
			CRuntime.free((void*)orig);
			return ptr;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public unsafe static void stbi__vertical_flip(void* image, int w, int h, int bytes_per_pixel)
		{
			int num = w * bytes_per_pixel;
			byte* ptr = stackalloc byte[(UIntPtr)2048];
			for (int i = 0; i < h >> 1; i++)
			{
				byte* ptr2 = (byte*)image + i * num;
				byte* ptr3 = (byte*)image + (h - i - 1) * num;
				ulong num3;
				for (ulong num2 = (ulong)((long)num); num2 != 0UL; num2 -= num3)
				{
					num3 = ((num2 < 2048UL) ? num2 : 2048UL);
					CRuntime.memcpy((void*)ptr, (void*)ptr2, num3);
					CRuntime.memcpy((void*)ptr2, (void*)ptr3, num3);
					CRuntime.memcpy((void*)ptr3, (void*)ptr, num3);
					ptr2 += num3;
					ptr3 += num3;
				}
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003C48 File Offset: 0x00001E48
		public unsafe static void stbi__vertical_flip_slices(void* image, int w, int h, int z, int bytes_per_pixel)
		{
			int num = w * h * bytes_per_pixel;
			byte* ptr = (byte*)image;
			for (int i = 0; i < z; i++)
			{
				StbImage.stbi__vertical_flip((void*)ptr, w, h, bytes_per_pixel);
				ptr += num;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003C7C File Offset: 0x00001E7C
		public unsafe static byte* stbi__load_and_postprocess_8bit(StbImage.stbi__context s, int* x, int* y, int* comp, int req_comp)
		{
			StbImage.stbi__result_info stbi__result_info = default(StbImage.stbi__result_info);
			void* ptr = StbImage.stbi__load_main(s, x, y, comp, req_comp, &stbi__result_info, 8);
			if (ptr == null)
			{
				return null;
			}
			if (stbi__result_info.bits_per_channel != 8)
			{
				ptr = (void*)StbImage.stbi__convert_16_to_8((ushort*)ptr, *x, *y, (req_comp == 0) ? (*comp) : req_comp);
				stbi__result_info.bits_per_channel = 8;
			}
			if (((StbImage.stbi__vertically_flip_on_load_set != 0) ? StbImage.stbi__vertically_flip_on_load_local : StbImage.stbi__vertically_flip_on_load_global) != 0)
			{
				int num = ((req_comp != 0) ? req_comp : (*comp));
				StbImage.stbi__vertical_flip(ptr, *x, *y, num);
			}
			return (byte*)ptr;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003CFC File Offset: 0x00001EFC
		public unsafe static ushort* stbi__load_and_postprocess_16bit(StbImage.stbi__context s, int* x, int* y, int* comp, int req_comp)
		{
			StbImage.stbi__result_info stbi__result_info = default(StbImage.stbi__result_info);
			void* ptr = StbImage.stbi__load_main(s, x, y, comp, req_comp, &stbi__result_info, 16);
			if (ptr == null)
			{
				return null;
			}
			if (stbi__result_info.bits_per_channel != 16)
			{
				ptr = (void*)StbImage.stbi__convert_8_to_16((byte*)ptr, *x, *y, (req_comp == 0) ? (*comp) : req_comp);
				stbi__result_info.bits_per_channel = 16;
			}
			if (((StbImage.stbi__vertically_flip_on_load_set != 0) ? StbImage.stbi__vertically_flip_on_load_local : StbImage.stbi__vertically_flip_on_load_global) != 0)
			{
				int num = ((req_comp != 0) ? req_comp : (*comp));
				StbImage.stbi__vertical_flip(ptr, *x, *y, num * 2);
			}
			return (ushort*)ptr;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003D80 File Offset: 0x00001F80
		public unsafe static void stbi__float_postprocess(float* result, int* x, int* y, int* comp, int req_comp)
		{
			if (((StbImage.stbi__vertically_flip_on_load_set != 0) ? StbImage.stbi__vertically_flip_on_load_local : StbImage.stbi__vertically_flip_on_load_global) != 0 && result != null)
			{
				int num = ((req_comp != 0) ? req_comp : (*comp));
				StbImage.stbi__vertical_flip((void*)result, *x, *y, num * 4);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public unsafe static float* stbi__loadf_main(StbImage.stbi__context s, int* x, int* y, int* comp, int req_comp)
		{
			if (StbImage.stbi__hdr_test(s) != 0)
			{
				StbImage.stbi__result_info stbi__result_info = default(StbImage.stbi__result_info);
				float* ptr = StbImage.stbi__hdr_load(s, x, y, comp, req_comp, &stbi__result_info);
				if (ptr != null)
				{
					StbImage.stbi__float_postprocess(ptr, x, y, comp, req_comp);
				}
				return ptr;
			}
			byte* ptr2 = StbImage.stbi__load_and_postprocess_8bit(s, x, y, comp, req_comp);
			if (ptr2 != null)
			{
				return StbImage.stbi__ldr_to_hdr(ptr2, *x, *y, (req_comp != 0) ? req_comp : (*comp));
			}
			return (StbImage.stbi__err("unknown image type") != 0) ? null : null;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003E36 File Offset: 0x00002036
		public static int stbi__get16be(StbImage.stbi__context s)
		{
			return ((int)StbImage.stbi__get8(s) << 8) + (int)StbImage.stbi__get8(s);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E47 File Offset: 0x00002047
		public static uint stbi__get32be(StbImage.stbi__context s)
		{
			return (uint)((ulong)((ulong)StbImage.stbi__get16be(s) << 16) + (ulong)((long)StbImage.stbi__get16be(s)));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003E5C File Offset: 0x0000205C
		public static int stbi__get16le(StbImage.stbi__context s)
		{
			return (int)StbImage.stbi__get8(s) + ((int)StbImage.stbi__get8(s) << 8);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003E6D File Offset: 0x0000206D
		public static uint stbi__get32le(StbImage.stbi__context s)
		{
			return (uint)(StbImage.stbi__get16le(s) + (StbImage.stbi__get16le(s) << 16));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003E7F File Offset: 0x0000207F
		public static byte stbi__compute_y(int r, int g, int b)
		{
			return (byte)(r * 77 + g * 150 + 29 * b >> 8);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003E98 File Offset: 0x00002098
		public unsafe static byte* stbi__convert_format(byte* data, int img_n, int req_comp, uint x, uint y)
		{
			if (req_comp == img_n)
			{
				return data;
			}
			byte* ptr = (byte*)StbImage.stbi__malloc_mad3(req_comp, (int)x, (int)y, 0);
			if (ptr == null)
			{
				CRuntime.free((void*)data);
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			int i = 0;
			while (i < (int)y)
			{
				byte* ptr2 = data + (long)i * (long)((ulong)x) * (long)img_n;
				byte* ptr3 = ptr + (long)i * (long)((ulong)x) * (long)req_comp;
				int num = img_n * 8 + req_comp;
				switch (num)
				{
				case 10:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = *ptr2;
						ptr3[1] = byte.MaxValue;
						j--;
						ptr2++;
						ptr3 += 2;
					}
					break;
				}
				case 11:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = (ptr3[1] = (ptr3[2] = *ptr2));
						j--;
						ptr2++;
						ptr3 += 3;
					}
					break;
				}
				case 12:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = (ptr3[1] = (ptr3[2] = *ptr2));
						ptr3[3] = byte.MaxValue;
						j--;
						ptr2++;
						ptr3 += 4;
					}
					break;
				}
				case 13:
				case 14:
				case 15:
				case 16:
				case 18:
					goto IL_033F;
				case 17:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = *ptr2;
						j--;
						ptr2 += 2;
						ptr3++;
					}
					break;
				}
				case 19:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = (ptr3[1] = (ptr3[2] = *ptr2));
						j--;
						ptr2 += 2;
						ptr3 += 3;
					}
					break;
				}
				case 20:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = (ptr3[1] = (ptr3[2] = *ptr2));
						ptr3[3] = ptr2[1];
						j--;
						ptr2 += 2;
						ptr3 += 4;
					}
					break;
				}
				default:
					switch (num)
					{
					case 25:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = StbImage.stbi__compute_y((int)(*ptr2), (int)ptr2[1], (int)ptr2[2]);
							j--;
							ptr2 += 3;
							ptr3++;
						}
						break;
					}
					case 26:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = StbImage.stbi__compute_y((int)(*ptr2), (int)ptr2[1], (int)ptr2[2]);
							ptr3[1] = byte.MaxValue;
							j--;
							ptr2 += 3;
							ptr3 += 2;
						}
						break;
					}
					case 27:
					case 29:
					case 30:
					case 31:
					case 32:
						goto IL_033F;
					case 28:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = *ptr2;
							ptr3[1] = ptr2[1];
							ptr3[2] = ptr2[2];
							ptr3[3] = byte.MaxValue;
							j--;
							ptr2 += 3;
							ptr3 += 4;
						}
						break;
					}
					case 33:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = StbImage.stbi__compute_y((int)(*ptr2), (int)ptr2[1], (int)ptr2[2]);
							j--;
							ptr2 += 4;
							ptr3++;
						}
						break;
					}
					case 34:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = StbImage.stbi__compute_y((int)(*ptr2), (int)ptr2[1], (int)ptr2[2]);
							ptr3[1] = ptr2[3];
							j--;
							ptr2 += 4;
							ptr3 += 2;
						}
						break;
					}
					case 35:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = *ptr2;
							ptr3[1] = ptr2[1];
							ptr3[2] = ptr2[2];
							j--;
							ptr2 += 4;
							ptr3 += 3;
						}
						break;
					}
					default:
						goto IL_033F;
					}
					break;
				}
				i++;
				continue;
				IL_033F:
				CRuntime.free((void*)data);
				CRuntime.free((void*)ptr);
				return (StbImage.stbi__err("unsupported") != 0) ? null : null;
			}
			CRuntime.free((void*)data);
			return ptr;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004216 File Offset: 0x00002416
		public static ushort stbi__compute_y_16(int r, int g, int b)
		{
			return (ushort)(r * 77 + g * 150 + 29 * b >> 8);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000422C File Offset: 0x0000242C
		public unsafe static ushort* stbi__convert_format16(ushort* data, int img_n, int req_comp, uint x, uint y)
		{
			if (req_comp == img_n)
			{
				return data;
			}
			ushort* ptr = (ushort*)StbImage.stbi__malloc((ulong)((long)req_comp * (long)((ulong)x) * (long)((ulong)y) * 2L));
			if (ptr == null)
			{
				CRuntime.free((void*)data);
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			int i = 0;
			while (i < (int)y)
			{
				ushort* ptr2 = data + (long)i * (long)((ulong)x) * (long)img_n * 2L / 2L;
				ushort* ptr3 = ptr + (long)i * (long)((ulong)x) * (long)req_comp * 2L / 2L;
				int num = img_n * 8 + req_comp;
				switch (num)
				{
				case 10:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = *ptr2;
						ptr3[1] = ushort.MaxValue;
						j--;
						ptr2++;
						ptr3 += 2;
					}
					break;
				}
				case 11:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = (ptr3[1] = (ptr3[2] = *ptr2));
						j--;
						ptr2++;
						ptr3 += 3;
					}
					break;
				}
				case 12:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = (ptr3[1] = (ptr3[2] = *ptr2));
						ptr3[3] = ushort.MaxValue;
						j--;
						ptr2++;
						ptr3 += 4;
					}
					break;
				}
				case 13:
				case 14:
				case 15:
				case 16:
				case 18:
					goto IL_03B2;
				case 17:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = *ptr2;
						j--;
						ptr2 += 2;
						ptr3++;
					}
					break;
				}
				case 19:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = (ptr3[1] = (ptr3[2] = *ptr2));
						j--;
						ptr2 += 2;
						ptr3 += 3;
					}
					break;
				}
				case 20:
				{
					int j = (int)(x - 1U);
					while (j >= 0)
					{
						*ptr3 = (ptr3[1] = (ptr3[2] = *ptr2));
						ptr3[3] = ptr2[1];
						j--;
						ptr2 += 2;
						ptr3 += 4;
					}
					break;
				}
				default:
					switch (num)
					{
					case 25:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = StbImage.stbi__compute_y_16((int)(*ptr2), (int)ptr2[1], (int)ptr2[2]);
							j--;
							ptr2 += 3;
							ptr3++;
						}
						break;
					}
					case 26:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = StbImage.stbi__compute_y_16((int)(*ptr2), (int)ptr2[1], (int)ptr2[2]);
							ptr3[1] = ushort.MaxValue;
							j--;
							ptr2 += 3;
							ptr3 += 2;
						}
						break;
					}
					case 27:
					case 29:
					case 30:
					case 31:
					case 32:
						goto IL_03B2;
					case 28:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = *ptr2;
							ptr3[1] = ptr2[1];
							ptr3[2] = ptr2[2];
							ptr3[3] = ushort.MaxValue;
							j--;
							ptr2 += 3;
							ptr3 += 4;
						}
						break;
					}
					case 33:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = StbImage.stbi__compute_y_16((int)(*ptr2), (int)ptr2[1], (int)ptr2[2]);
							j--;
							ptr2 += 4;
							ptr3++;
						}
						break;
					}
					case 34:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = StbImage.stbi__compute_y_16((int)(*ptr2), (int)ptr2[1], (int)ptr2[2]);
							ptr3[1] = ptr2[3];
							j--;
							ptr2 += 4;
							ptr3 += 2;
						}
						break;
					}
					case 35:
					{
						int j = (int)(x - 1U);
						while (j >= 0)
						{
							*ptr3 = *ptr2;
							ptr3[1] = ptr2[1];
							ptr3[2] = ptr2[2];
							j--;
							ptr2 += 4;
							ptr3 += 3;
						}
						break;
					}
					default:
						goto IL_03B2;
					}
					break;
				}
				i++;
				continue;
				IL_03B2:
				CRuntime.free((void*)data);
				CRuntime.free((void*)ptr);
				return (StbImage.stbi__err("unsupported") != 0) ? null : null;
			}
			CRuntime.free((void*)data);
			return ptr;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000461D File Offset: 0x0000281D
		public static byte stbi__clamp(int x)
		{
			if (x > 255)
			{
				if (x < 0)
				{
					return 0;
				}
				if (x > 255)
				{
					return byte.MaxValue;
				}
			}
			return (byte)x;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000463D File Offset: 0x0000283D
		public static byte stbi__blinn_8x8(byte x, byte y)
		{
			byte b = x * y + 128;
			return (byte)((uint)b + ((uint)b >> 8) >> 8);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004650 File Offset: 0x00002850
		public static int stbi__bitreverse16(int n)
		{
			n = ((n & 43690) >> 1) | ((n & 21845) << 1);
			n = ((n & 52428) >> 2) | ((n & 13107) << 2);
			n = ((n & 61680) >> 4) | ((n & 3855) << 4);
			n = ((n & 65280) >> 8) | ((n & 255) << 8);
			return n;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000046B2 File Offset: 0x000028B2
		public static int stbi__bit_reverse(int v, int bits)
		{
			return StbImage.stbi__bitreverse16(v) >> 16 - bits;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000046C4 File Offset: 0x000028C4
		public static int stbi__paeth(int a, int b, int c)
		{
			int num = a + b - c;
			int num2 = CRuntime.abs(num - a);
			int num3 = CRuntime.abs(num - b);
			int num4 = CRuntime.abs(num - c);
			if (num2 <= num3 && num2 <= num4)
			{
				return a;
			}
			if (num3 <= num4)
			{
				return b;
			}
			return c;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004701 File Offset: 0x00002901
		public static void stbi__unpremultiply_on_load_thread(int flag_true_if_should_unpremultiply)
		{
			StbImage.stbi__unpremultiply_on_load_local = flag_true_if_should_unpremultiply;
			StbImage.stbi__unpremultiply_on_load_set = 1;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004710 File Offset: 0x00002910
		public static int stbi__high_bit(uint z)
		{
			int num = 0;
			if (z == 0U)
			{
				return -1;
			}
			if (z >= 65536U)
			{
				num += 16;
				z >>= 16;
			}
			if (z >= 256U)
			{
				num += 8;
				z >>= 8;
			}
			if (z >= 16U)
			{
				num += 4;
				z >>= 4;
			}
			if (z >= 4U)
			{
				num += 2;
				z >>= 2;
			}
			if (z >= 2U)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000476C File Offset: 0x0000296C
		public static int stbi__bitcount(uint a)
		{
			a = (a & 1431655765U) + ((a >> 1) & 1431655765U);
			a = (a & 858993459U) + ((a >> 2) & 858993459U);
			a = (a + (a >> 4)) & 252645135U;
			a += a >> 8;
			a += a >> 16;
			return (int)(a & 255U);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000047C2 File Offset: 0x000029C2
		public static int stbi__shiftsigned(uint v, int shift, int bits)
		{
			if (shift < 0)
			{
				v <<= -shift;
			}
			else
			{
				v >>= shift;
			}
			v >>= 8 - bits;
			return (int)((ulong)v * (ulong)((long)StbImage.stbi__shiftsigned_mul_table[bits])) >> StbImage.stbi__shiftsigned_shift_table[bits];
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000047FC File Offset: 0x000029FC
		public unsafe static int stbi__info_main(StbImage.stbi__context s, int* x, int* y, int* comp)
		{
			if (StbImage.stbi__jpeg_info(s, x, y, comp) != 0)
			{
				return 1;
			}
			if (StbImage.stbi__png_info(s, x, y, comp) != 0)
			{
				return 1;
			}
			if (StbImage.stbi__gif_info(s, x, y, comp) != 0)
			{
				return 1;
			}
			if (StbImage.stbi__bmp_info(s, x, y, comp) != 0)
			{
				return 1;
			}
			if (StbImage.stbi__psd_info(s, x, y, comp) != 0)
			{
				return 1;
			}
			if (StbImage.stbi__hdr_info(s, x, y, comp) != 0)
			{
				return 1;
			}
			if (StbImage.stbi__tga_info(s, x, y, comp) != 0)
			{
				return 1;
			}
			return StbImage.stbi__err("unknown image type");
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000486E File Offset: 0x00002A6E
		public static int stbi__is_16_main(StbImage.stbi__context s)
		{
			if (StbImage.stbi__png_is16(s) != 0)
			{
				return 1;
			}
			if (StbImage.stbi__psd_is16(s) != 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004885 File Offset: 0x00002A85
		public static int stbi__gif_test(StbImage.stbi__context s)
		{
			int num = StbImage.stbi__gif_test_raw(s);
			StbImage.stbi__rewind(s);
			return num;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004894 File Offset: 0x00002A94
		public unsafe static void* stbi__gif_load(StbImage.stbi__context s, int* x, int* y, int* comp, int req_comp, StbImage.stbi__result_info* ri)
		{
			byte* ptr = null;
			StbImage.stbi__gif stbi__gif = new StbImage.stbi__gif();
			ptr = StbImage.stbi__gif_load_next(s, stbi__gif, comp, req_comp, null);
			if (ptr != null)
			{
				*x = stbi__gif.w;
				*y = stbi__gif.h;
				if (req_comp != 0 && req_comp != 4)
				{
					ptr = StbImage.stbi__convert_format(ptr, 4, req_comp, (uint)stbi__gif.w, (uint)stbi__gif.h);
				}
			}
			else if (stbi__gif._out_ != null)
			{
				CRuntime.free((void*)stbi__gif._out_);
			}
			CRuntime.free((void*)stbi__gif.history);
			CRuntime.free((void*)stbi__gif.background);
			return (void*)ptr;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000491C File Offset: 0x00002B1C
		public unsafe static void* stbi__load_gif_main(StbImage.stbi__context s, int** delays, int* x, int* y, int* z, int* comp, int req_comp)
		{
			if (StbImage.stbi__gif_test(s) != 0)
			{
				int num = 0;
				byte* ptr = null;
				byte* ptr2 = null;
				byte* ptr3 = null;
				StbImage.stbi__gif stbi__gif = new StbImage.stbi__gif();
				if (delays != null)
				{
					*(IntPtr*)delays = (IntPtr)((UIntPtr)0);
				}
				for (;;)
				{
					ptr = StbImage.stbi__gif_load_next(s, stbi__gif, comp, req_comp, ptr3);
					if (ptr != null)
					{
						*x = stbi__gif.w;
						*y = stbi__gif.h;
						num++;
						int num2 = stbi__gif.w * stbi__gif.h * 4;
						if (ptr2 != null)
						{
							void* ptr4 = CRuntime.realloc((void*)ptr2, (ulong)((long)(num * num2)));
							if (ptr4 == null)
							{
								break;
							}
							ptr2 = (byte*)ptr4;
							if (delays != null)
							{
								int* ptr5 = (int*)CRuntime.realloc(*(IntPtr*)delays, (ulong)((long)(4 * num)));
								if (ptr5 == null)
								{
									goto Block_7;
								}
								*(IntPtr*)delays = ptr5;
							}
						}
						else
						{
							ptr2 = (byte*)StbImage.stbi__malloc((ulong)((long)(num * num2)));
							if (ptr2 == null)
							{
								goto Block_8;
							}
							if (delays != null)
							{
								*(IntPtr*)delays = StbImage.stbi__malloc((ulong)((long)(num * 4)));
								if (*(IntPtr*)delays == (IntPtr)((UIntPtr)0))
								{
									goto Block_10;
								}
							}
						}
						CRuntime.memcpy((void*)(ptr2 + (num - 1) * num2), (void*)ptr, (ulong)((long)num2));
						if (num >= 2)
						{
							ptr3 = ptr2 - 2 * num2;
						}
						if (delays != null)
						{
							*(*(IntPtr*)delays + (IntPtr)(((long)num - 1L) * 4L)) = stbi__gif.delay;
						}
					}
					if (ptr == null)
					{
						goto Block_13;
					}
				}
				return StbImage.stbi__load_gif_main_outofmem(stbi__gif, ptr2, delays);
				Block_7:
				return StbImage.stbi__load_gif_main_outofmem(stbi__gif, ptr2, delays);
				Block_8:
				return StbImage.stbi__load_gif_main_outofmem(stbi__gif, ptr2, delays);
				Block_10:
				return StbImage.stbi__load_gif_main_outofmem(stbi__gif, ptr2, delays);
				Block_13:
				CRuntime.free((void*)stbi__gif._out_);
				CRuntime.free((void*)stbi__gif.history);
				CRuntime.free((void*)stbi__gif.background);
				if (req_comp != 0 && req_comp != 4)
				{
					ptr2 = StbImage.stbi__convert_format(ptr2, 4, req_comp, (uint)(num * stbi__gif.w), (uint)stbi__gif.h);
				}
				*z = num;
				return (void*)ptr2;
			}
			return (StbImage.stbi__err("not GIF") != 0) ? null : null;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public unsafe static int stbi__gif_info(StbImage.stbi__context s, int* x, int* y, int* comp)
		{
			return StbImage.stbi__gif_info_raw(s, x, y, comp);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public static int stbi__gif_test_raw(StbImage.stbi__context s)
		{
			if (StbImage.stbi__get8(s) != 71 || StbImage.stbi__get8(s) != 73 || StbImage.stbi__get8(s) != 70 || StbImage.stbi__get8(s) != 56)
			{
				return 0;
			}
			int num = (int)StbImage.stbi__get8(s);
			if (num != 57 && num != 55)
			{
				return 0;
			}
			if (StbImage.stbi__get8(s) != 97)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004B1C File Offset: 0x00002D1C
		public static void stbi__gif_parse_colortable(StbImage.stbi__context s, byte[][] pal, int num_entries, int transp)
		{
			for (int i = 0; i < num_entries; i++)
			{
				pal[i][2] = StbImage.stbi__get8(s);
				pal[i][1] = StbImage.stbi__get8(s);
				pal[i][0] = StbImage.stbi__get8(s);
				pal[i][3] = ((transp == i) ? 0 : byte.MaxValue);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004B6C File Offset: 0x00002D6C
		public unsafe static int stbi__gif_header(StbImage.stbi__context s, StbImage.stbi__gif g, int* comp, int is_info)
		{
			if (StbImage.stbi__get8(s) != 71 || StbImage.stbi__get8(s) != 73 || StbImage.stbi__get8(s) != 70 || StbImage.stbi__get8(s) != 56)
			{
				return StbImage.stbi__err("not GIF");
			}
			byte b = StbImage.stbi__get8(s);
			if (b != 55 && b != 57)
			{
				return StbImage.stbi__err("not GIF");
			}
			if (StbImage.stbi__get8(s) != 97)
			{
				return StbImage.stbi__err("not GIF");
			}
			StbImage.stbi__g_failure_reason = "";
			g.w = StbImage.stbi__get16le(s);
			g.h = StbImage.stbi__get16le(s);
			g.flags = (int)StbImage.stbi__get8(s);
			g.bgindex = (int)StbImage.stbi__get8(s);
			g.ratio = (int)StbImage.stbi__get8(s);
			g.transparent = -1;
			if (g.w > 16777216)
			{
				return StbImage.stbi__err("too large");
			}
			if (g.h > 16777216)
			{
				return StbImage.stbi__err("too large");
			}
			if (comp != null)
			{
				*comp = 4;
			}
			if (is_info != 0)
			{
				return 1;
			}
			if ((g.flags & 128) != 0)
			{
				StbImage.stbi__gif_parse_colortable(s, g.pal, 2 << (g.flags & 7), -1);
			}
			return 1;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004C94 File Offset: 0x00002E94
		public unsafe static int stbi__gif_info_raw(StbImage.stbi__context s, int* x, int* y, int* comp)
		{
			StbImage.stbi__gif stbi__gif = new StbImage.stbi__gif();
			if (stbi__gif == null)
			{
				return StbImage.stbi__err("outofmem");
			}
			if (StbImage.stbi__gif_header(s, stbi__gif, comp, 1) == 0)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			if (x != null)
			{
				*x = stbi__gif.w;
			}
			if (y != null)
			{
				*y = stbi__gif.h;
			}
			return 1;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004CE4 File Offset: 0x00002EE4
		public unsafe static void stbi__out_gif_code(StbImage.stbi__gif g, ushort code)
		{
			if (g.codes[(int)code].prefix >= 0)
			{
				StbImage.stbi__out_gif_code(g, (ushort)g.codes[(int)code].prefix);
			}
			if (g.cur_y >= g.max_y)
			{
				return;
			}
			int num = g.cur_x + g.cur_y;
			byte* ptr = g._out_ + num;
			g.history[num / 4] = 1;
			byte[] array = g.color_table[(int)g.codes[(int)code].suffix];
			if (array[3] > 128)
			{
				*ptr = array[2];
				ptr[1] = array[1];
				ptr[2] = array[0];
				ptr[3] = array[3];
			}
			g.cur_x += 4;
			if (g.cur_x >= g.max_x)
			{
				g.cur_x = g.start_x;
				g.cur_y += g.step;
				while (g.cur_y >= g.max_y && g.parse > 0)
				{
					g.step = (1 << g.parse) * g.line_size;
					g.cur_y = g.start_y + (g.step >> 1);
					g.parse--;
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004E20 File Offset: 0x00003020
		public unsafe static byte* stbi__process_gif_raster(StbImage.stbi__context s, StbImage.stbi__gif g)
		{
			byte b = StbImage.stbi__get8(s);
			if (b > 12)
			{
				return null;
			}
			int num = 1 << (int)b;
			uint num2 = 1U;
			int num3 = (int)(b + 1);
			int num4 = (1 << num3) - 1;
			int num5 = 0;
			int num6 = 0;
			for (int i = 0; i < num; i++)
			{
				g.codes[i].prefix = -1;
				g.codes[i].first = (byte)i;
				g.codes[i].suffix = (byte)i;
			}
			int num7 = num + 2;
			int num8 = -1;
			int num9 = 0;
			for (;;)
			{
				if (num6 < num3)
				{
					if (num9 == 0)
					{
						num9 = (int)StbImage.stbi__get8(s);
						if (num9 == 0)
						{
							break;
						}
					}
					num9--;
					num5 |= (int)StbImage.stbi__get8(s) << num6;
					num6 += 8;
				}
				else
				{
					int num10 = num5 & num4;
					num5 >>= num3;
					num6 -= num3;
					if (num10 == num)
					{
						num3 = (int)(b + 1);
						num4 = (1 << num3) - 1;
						num7 = num + 2;
						num8 = -1;
						num2 = 0U;
					}
					else
					{
						if (num10 == num + 1)
						{
							goto Block_7;
						}
						if (num10 > num7)
						{
							goto IL_0231;
						}
						if (num2 != 0U)
						{
							goto Block_10;
						}
						if (num8 >= 0)
						{
							fixed (StbImage.stbi__gif_lzw* ptr = &g.codes[num7++])
							{
								StbImage.stbi__gif_lzw* ptr2 = ptr;
								if (num7 > 8192)
								{
									goto Block_13;
								}
								ptr2->prefix = (short)num8;
								ptr2->first = g.codes[num8].first;
								ptr2->suffix = ((num10 == num7) ? ptr2->first : g.codes[num10].first);
							}
						}
						else if (num10 == num7)
						{
							goto Block_16;
						}
						StbImage.stbi__out_gif_code(g, (ushort)num10);
						if ((num7 & num4) == 0 && num7 <= 4095)
						{
							num3++;
							num4 = (1 << num3) - 1;
						}
						num8 = num10;
					}
				}
			}
			return g._out_;
			Block_7:
			StbImage.stbi__skip(s, num9);
			while ((num9 = (int)StbImage.stbi__get8(s)) > 0)
			{
				StbImage.stbi__skip(s, num9);
			}
			return g._out_;
			Block_10:
			return (StbImage.stbi__err("no clear code") != 0) ? null : null;
			Block_13:
			return (StbImage.stbi__err("too many codes") != 0) ? null : null;
			Block_16:
			return (StbImage.stbi__err("illegal code in raster") != 0) ? null : null;
			IL_0231:
			return (StbImage.stbi__err("illegal code in raster") != 0) ? null : null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005070 File Offset: 0x00003270
		public unsafe static byte* stbi__gif_load_next(StbImage.stbi__context s, StbImage.stbi__gif g, int* comp, int req_comp, byte* two_back)
		{
			int num = 0;
			if (g._out_ == null)
			{
				if (StbImage.stbi__gif_header(s, g, comp, 0) == 0)
				{
					return null;
				}
				if (StbImage.stbi__mad3sizes_valid(4, g.w, g.h, 0) == 0)
				{
					return (StbImage.stbi__err("too large") != 0) ? null : null;
				}
				int num2 = g.w * g.h;
				g._out_ = (byte*)StbImage.stbi__malloc((ulong)((long)(4 * num2)));
				g.background = (byte*)StbImage.stbi__malloc((ulong)((long)(4 * num2)));
				g.history = (byte*)StbImage.stbi__malloc((ulong)((long)num2));
				if (g._out_ == null || g.background == null || g.history == null)
				{
					return (StbImage.stbi__err("outofmem") != 0) ? null : null;
				}
				CRuntime.memset((void*)g._out_, 0, (ulong)((long)(4 * num2)));
				CRuntime.memset((void*)g.background, 0, (ulong)((long)(4 * num2)));
				CRuntime.memset((void*)g.history, 0, (ulong)((long)num2));
				num = 1;
			}
			else
			{
				int num3 = (g.eflags & 28) >> 2;
				int num2 = g.w * g.h;
				if (num3 == 3 && two_back == null)
				{
					num3 = 2;
				}
				if (num3 == 3)
				{
					for (int i = 0; i < num2; i++)
					{
						if (g.history[i] != 0)
						{
							CRuntime.memcpy((void*)(g._out_ + i * 4), (void*)(two_back + i * 4), 4UL);
						}
					}
				}
				else if (num3 == 2)
				{
					for (int i = 0; i < num2; i++)
					{
						if (g.history[i] != 0)
						{
							CRuntime.memcpy((void*)(g._out_ + i * 4), (void*)(g.background + i * 4), 4UL);
						}
					}
				}
				CRuntime.memcpy((void*)g.background, (void*)g._out_, (ulong)((long)(4 * g.w * g.h)));
			}
			CRuntime.memset((void*)g.history, 0, (ulong)((long)(g.w * g.h)));
			int num4;
			for (;;)
			{
				num4 = (int)StbImage.stbi__get8(s);
				if (num4 != 33)
				{
					break;
				}
				int num5;
				if (StbImage.stbi__get8(s) == 249)
				{
					num5 = (int)StbImage.stbi__get8(s);
					if (num5 != 4)
					{
						StbImage.stbi__skip(s, num5);
						continue;
					}
					g.eflags = (int)StbImage.stbi__get8(s);
					g.delay = 10 * StbImage.stbi__get16le(s);
					if (g.transparent >= 0)
					{
						g.pal[g.transparent][3] = byte.MaxValue;
					}
					if ((g.eflags & 1) != 0)
					{
						g.transparent = (int)StbImage.stbi__get8(s);
						if (g.transparent >= 0)
						{
							g.pal[g.transparent][3] = 0;
						}
					}
					else
					{
						StbImage.stbi__skip(s, 1);
						g.transparent = -1;
					}
				}
				while ((num5 = (int)StbImage.stbi__get8(s)) != 0)
				{
					StbImage.stbi__skip(s, num5);
				}
			}
			if (num4 != 44)
			{
				if (num4 != 59)
				{
					return (StbImage.stbi__err("unknown code") != 0) ? null : null;
				}
				return null;
			}
			else
			{
				int num6 = StbImage.stbi__get16le(s);
				int num7 = StbImage.stbi__get16le(s);
				int num8 = StbImage.stbi__get16le(s);
				int num9 = StbImage.stbi__get16le(s);
				if (num6 + num8 > g.w || num7 + num9 > g.h)
				{
					return (StbImage.stbi__err("bad Image Descriptor") != 0) ? null : null;
				}
				g.line_size = g.w * 4;
				g.start_x = num6 * 4;
				g.start_y = num7 * g.line_size;
				g.max_x = g.start_x + num8 * 4;
				g.max_y = g.start_y + num9 * g.line_size;
				g.cur_x = g.start_x;
				g.cur_y = g.start_y;
				if (num8 == 0)
				{
					g.cur_y = g.max_y;
				}
				g.lflags = (int)StbImage.stbi__get8(s);
				if ((g.lflags & 64) != 0)
				{
					g.step = 8 * g.line_size;
					g.parse = 3;
				}
				else
				{
					g.step = g.line_size;
					g.parse = 0;
				}
				if ((g.lflags & 128) != 0)
				{
					StbImage.stbi__gif_parse_colortable(s, g.lpal, 2 << (g.lflags & 7), ((g.eflags & 1) != 0) ? g.transparent : (-1));
					g.color_table = g.lpal;
				}
				else
				{
					if ((g.flags & 128) == 0)
					{
						return (StbImage.stbi__err("missing color table") != 0) ? null : null;
					}
					g.color_table = g.pal;
				}
				byte* ptr = StbImage.stbi__process_gif_raster(s, g);
				if (ptr == null)
				{
					return null;
				}
				int num2 = g.w * g.h;
				if (num != 0 && g.bgindex > 0)
				{
					for (int i = 0; i < num2; i++)
					{
						if (g.history[i] == 0)
						{
							g.pal[g.bgindex][3] = byte.MaxValue;
							fixed (byte* ptr2 = &g.pal[g.bgindex][0])
							{
								byte* ptr3 = ptr2;
								CRuntime.memcpy((void*)(g._out_ + i * 4), (void*)ptr3, 4UL);
							}
						}
					}
				}
				return ptr;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005544 File Offset: 0x00003744
		public unsafe static void* stbi__load_gif_main_outofmem(StbImage.stbi__gif g, byte* _out_, int** delays)
		{
			CRuntime.free((void*)g._out_);
			CRuntime.free((void*)g.history);
			CRuntime.free((void*)g.background);
			if (_out_ != null)
			{
				CRuntime.free((void*)_out_);
			}
			if (delays != null && *(IntPtr*)delays != (IntPtr)((UIntPtr)0))
			{
				CRuntime.free(*(IntPtr*)delays);
			}
			return (StbImage.stbi__err("outofmem") != 0) ? null : null;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000055A4 File Offset: 0x000037A4
		public static int stbi__hdr_test(StbImage.stbi__context s)
		{
			int num = StbImage.stbi__hdr_test_core(s, "#?RADIANCE\n");
			StbImage.stbi__rewind(s);
			if (num == 0)
			{
				num = StbImage.stbi__hdr_test_core(s, "#?RGBE\n");
				StbImage.stbi__rewind(s);
			}
			return num;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000055DC File Offset: 0x000037DC
		public unsafe static float* stbi__hdr_load(StbImage.stbi__context s, int* x, int* y, int* comp, int req_comp, StbImage.stbi__result_info* ri)
		{
			sbyte* ptr = stackalloc sbyte[(UIntPtr)1024];
			int num = 0;
			int i = 0;
			int j = 0;
			sbyte* ptr2 = StbImage.stbi__hdr_gettoken(s, ptr);
			if (CRuntime.strcmp(ptr2, "#?RADIANCE") != 0 && CRuntime.strcmp(ptr2, "#?RGBE") != 0)
			{
				return (StbImage.stbi__err("not HDR") != 0) ? null : null;
			}
			sbyte* ptr3;
			for (;;)
			{
				ptr3 = StbImage.stbi__hdr_gettoken(s, ptr);
				if (*ptr3 == 0)
				{
					break;
				}
				if (CRuntime.strcmp(ptr3, "FORMAT=32-bit_rle_rgbe") == 0)
				{
					num = 1;
				}
			}
			if (num == 0)
			{
				return (StbImage.stbi__err("unsupported format") != 0) ? null : null;
			}
			ptr3 = StbImage.stbi__hdr_gettoken(s, ptr);
			if (CRuntime.strncmp(ptr3, "-Y ", 3UL) != 0)
			{
				return (StbImage.stbi__err("unsupported data layout") != 0) ? null : null;
			}
			ptr3 += 3;
			int num2 = (int)CRuntime.strtol(ptr3, &ptr3, 10);
			while (*ptr3 == 32)
			{
				ptr3++;
			}
			if (CRuntime.strncmp(ptr3, "+X ", 3UL) != 0)
			{
				return (StbImage.stbi__err("unsupported data layout") != 0) ? null : null;
			}
			ptr3 += 3;
			int num3 = (int)CRuntime.strtol(ptr3, null, 10);
			if (num2 > 16777216)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			if (num3 > 16777216)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			*x = num3;
			*y = num2;
			if (comp != null)
			{
				*comp = 3;
			}
			if (req_comp == 0)
			{
				req_comp = 3;
			}
			if (StbImage.stbi__mad4sizes_valid(num3, num2, req_comp, 4, 0) == 0)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			float* ptr4 = (float*)StbImage.stbi__malloc_mad4(num3, num2, req_comp, 4, 0);
			if (ptr4 == null)
			{
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			IL_01A5:
			while (!false)
			{
				if (num3 >= 8 && num3 < 32768)
				{
					byte* ptr5 = null;
					for (j = 0; j < num2; j++)
					{
						int num4 = (int)StbImage.stbi__get8(s);
						int num5 = (int)StbImage.stbi__get8(s);
						int num6 = (int)StbImage.stbi__get8(s);
						if (num4 != 2 || num5 != 2 || (num6 & 128) != 0)
						{
							byte* ptr6 = stackalloc byte[(UIntPtr)4];
							*ptr6 = (byte)num4;
							ptr6[1] = (byte)num5;
							ptr6[2] = (byte)num6;
							ptr6[3] = StbImage.stbi__get8(s);
							StbImage.stbi__hdr_convert(ptr4, ptr6, req_comp);
							i = 1;
							j = 0;
							CRuntime.free((void*)ptr5);
							goto IL_01A5;
						}
						num6 <<= 8;
						num6 |= (int)StbImage.stbi__get8(s);
						if (num6 != num3)
						{
							CRuntime.free((void*)ptr4);
							CRuntime.free((void*)ptr5);
							return (StbImage.stbi__err("invalid decoded scanline length") != 0) ? null : null;
						}
						if (ptr5 == null)
						{
							ptr5 = (byte*)StbImage.stbi__malloc_mad2(num3, 4, 0);
							if (ptr5 == null)
							{
								CRuntime.free((void*)ptr4);
								return (StbImage.stbi__err("outofmem") != 0) ? null : null;
							}
						}
						for (int k = 0; k < 4; k++)
						{
							i = 0;
							int num7;
							while ((num7 = num3 - i) > 0)
							{
								byte b = StbImage.stbi__get8(s);
								if (b > 128)
								{
									byte b2 = StbImage.stbi__get8(s);
									b -= 128;
									if ((int)b > num7)
									{
										CRuntime.free((void*)ptr4);
										CRuntime.free((void*)ptr5);
										return (StbImage.stbi__err("corrupt") != 0) ? null : null;
									}
									for (int l = 0; l < (int)b; l++)
									{
										ptr5[i++ * 4 + k] = b2;
									}
								}
								else
								{
									if ((int)b > num7)
									{
										CRuntime.free((void*)ptr4);
										CRuntime.free((void*)ptr5);
										return (StbImage.stbi__err("corrupt") != 0) ? null : null;
									}
									for (int l = 0; l < (int)b; l++)
									{
										ptr5[i++ * 4 + k] = StbImage.stbi__get8(s);
									}
								}
							}
						}
						for (i = 0; i < num3; i++)
						{
							StbImage.stbi__hdr_convert(ptr4 + (j * num3 + i) * req_comp, ptr5 + i * 4, req_comp);
						}
					}
					if (ptr5 != null)
					{
						CRuntime.free((void*)ptr5);
					}
					return ptr4;
				}
				j = (i = 0);
			}
			while (j < num2)
			{
				while (i < num3)
				{
					byte* ptr7 = stackalloc byte[(UIntPtr)4];
					StbImage.stbi__getn(s, ptr7, 4);
					StbImage.stbi__hdr_convert(ptr4 + j * num3 * req_comp + i * req_comp, ptr7, req_comp);
					i++;
				}
				j++;
			}
			return ptr4;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005A10 File Offset: 0x00003C10
		public unsafe static int stbi__hdr_info(StbImage.stbi__context s, int* x, int* y, int* comp)
		{
			sbyte* ptr = stackalloc sbyte[(UIntPtr)1024];
			int num = 0;
			int num2 = 0;
			if (x == null)
			{
				x = &num2;
			}
			if (y == null)
			{
				y = &num2;
			}
			if (comp == null)
			{
				comp = &num2;
			}
			if (StbImage.stbi__hdr_test(s) == 0)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			sbyte* ptr2;
			for (;;)
			{
				ptr2 = StbImage.stbi__hdr_gettoken(s, ptr);
				if (*ptr2 == 0)
				{
					break;
				}
				if (CRuntime.strcmp(ptr2, "FORMAT=32-bit_rle_rgbe") == 0)
				{
					num = 1;
				}
			}
			if (num == 0)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			ptr2 = StbImage.stbi__hdr_gettoken(s, ptr);
			if (CRuntime.strncmp(ptr2, "-Y ", 3UL) != 0)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			ptr2 += 3;
			*y = (int)CRuntime.strtol(ptr2, &ptr2, 10);
			while (*ptr2 == 32)
			{
				ptr2++;
			}
			if (CRuntime.strncmp(ptr2, "+X ", 3UL) != 0)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			ptr2 += 3;
			*x = (int)CRuntime.strtol(ptr2, null, 10);
			*comp = 3;
			return 1;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005AEC File Offset: 0x00003CEC
		public unsafe static byte* stbi__hdr_to_ldr(float* data, int x, int y, int comp)
		{
			if (data == null)
			{
				return null;
			}
			byte* ptr = (byte*)StbImage.stbi__malloc_mad3(x, y, comp, 0);
			if (ptr == null)
			{
				CRuntime.free((void*)data);
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			int num;
			if ((comp & 1) != 0)
			{
				num = comp;
			}
			else
			{
				num = comp - 1;
			}
			for (int i = 0; i < x * y; i++)
			{
				int j;
				for (j = 0; j < num; j++)
				{
					float num2 = (float)CRuntime.pow((double)(data[i * comp + j] * StbImage.stbi__h2l_scale_i), (double)StbImage.stbi__h2l_gamma_i) * 255f + 0.5f;
					if (num2 < 0f)
					{
						num2 = 0f;
					}
					if (num2 > 255f)
					{
						num2 = 255f;
					}
					ptr[i * comp + j] = (byte)((int)num2);
				}
				if (j < comp)
				{
					float num3 = data[i * comp + j] * 255f + 0.5f;
					if (num3 < 0f)
					{
						num3 = 0f;
					}
					if (num3 > 255f)
					{
						num3 = 255f;
					}
					ptr[i * comp + j] = (byte)((int)num3);
				}
			}
			CRuntime.free((void*)data);
			return ptr;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005C04 File Offset: 0x00003E04
		public static int stbi__hdr_test_core(StbImage.stbi__context s, string signature)
		{
			for (int i = 0; i < signature.Length; i++)
			{
				if ((char)StbImage.stbi__get8(s) != signature.get_Chars(i))
				{
					return 0;
				}
			}
			StbImage.stbi__rewind(s);
			return 1;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005C3C File Offset: 0x00003E3C
		public unsafe static sbyte* stbi__hdr_gettoken(StbImage.stbi__context z, sbyte* buffer)
		{
			int num = 0;
			sbyte b = (sbyte)StbImage.stbi__get8(z);
			while (StbImage.stbi__at_eof(z) == 0 && b != 10)
			{
				buffer[num++] = b;
				if (num == 1023)
				{
					while (StbImage.stbi__at_eof(z) == 0)
					{
						if (StbImage.stbi__get8(z) == 10)
						{
							break;
						}
					}
					break;
				}
				b = (sbyte)StbImage.stbi__get8(z);
			}
			buffer[num] = 0;
			return buffer;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005C98 File Offset: 0x00003E98
		public unsafe static void stbi__hdr_convert(float* output, byte* input, int req_comp)
		{
			if (input[3] != 0)
			{
				float num = (float)CRuntime.ldexp(1.0, (int)(input[3] - 136));
				if (req_comp <= 2)
				{
					*output = (float)(*input + input[1] + input[2]) * num / 3f;
				}
				else
				{
					*output = (float)(*input) * num;
					output[1] = (float)input[1] * num;
					output[2] = (float)input[2] * num;
				}
				if (req_comp == 2)
				{
					output[1] = 1f;
				}
				if (req_comp == 4)
				{
					output[3] = 1f;
					return;
				}
			}
			else if (req_comp - 1 > 1)
			{
				if (req_comp - 3 <= 1)
				{
					if (req_comp == 4)
					{
						output[3] = 1f;
					}
					*output = (output[1] = (output[2] = 0f));
					return;
				}
			}
			else
			{
				if (req_comp == 2)
				{
					output[1] = 1f;
				}
				*output = 0f;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005D70 File Offset: 0x00003F70
		public static int stbi__jpeg_test(StbImage.stbi__context s)
		{
			StbImage.stbi__jpeg stbi__jpeg = new StbImage.stbi__jpeg();
			if (stbi__jpeg == null)
			{
				return StbImage.stbi__err("outofmem");
			}
			stbi__jpeg.s = s;
			StbImage.stbi__setup_jpeg(stbi__jpeg);
			int num = StbImage.stbi__decode_jpeg_header(stbi__jpeg, 1);
			StbImage.stbi__rewind(s);
			return num;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005DAC File Offset: 0x00003FAC
		public unsafe static void* stbi__jpeg_load(StbImage.stbi__context s, int* x, int* y, int* comp, int req_comp, StbImage.stbi__result_info* ri)
		{
			StbImage.stbi__jpeg stbi__jpeg = new StbImage.stbi__jpeg();
			if (stbi__jpeg == null)
			{
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			stbi__jpeg.s = s;
			StbImage.stbi__setup_jpeg(stbi__jpeg);
			return (void*)StbImage.load_jpeg_image(stbi__jpeg, x, y, comp, req_comp);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005DF0 File Offset: 0x00003FF0
		public unsafe static int stbi__jpeg_info(StbImage.stbi__context s, int* x, int* y, int* comp)
		{
			StbImage.stbi__jpeg stbi__jpeg = new StbImage.stbi__jpeg();
			if (stbi__jpeg == null)
			{
				return StbImage.stbi__err("outofmem");
			}
			stbi__jpeg.s = s;
			return StbImage.stbi__jpeg_info_raw(stbi__jpeg, x, y, comp);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005E24 File Offset: 0x00004024
		public unsafe static int stbi__build_huffman(StbImage.stbi__huffman* h, int* count)
		{
			int num = 0;
			int j;
			for (int i = 0; i < 16; i++)
			{
				for (j = 0; j < count[i]; j++)
				{
					*((ref h->size.FixedElementField) + num++) = (byte)(i + 1);
				}
			}
			*((ref h->size.FixedElementField) + num) = 0;
			uint num2 = 0U;
			num = 0;
			for (j = 1; j <= 16; j++)
			{
				*((ref h->delta.FixedElementField) + (IntPtr)j * 4) = (int)((long)num - (long)((ulong)num2));
				if ((int)(*((ref h->size.FixedElementField) + num)) == j)
				{
					while ((int)(*((ref h->size.FixedElementField) + num)) == j)
					{
						*((ref h->code.FixedElementField) + (IntPtr)(num++) * 2) = (ushort)num2++;
					}
					if (num2 - 1U >= 1U << j)
					{
						return StbImage.stbi__err("bad code lengths");
					}
				}
				*((ref h->maxcode.FixedElementField) + (IntPtr)j * 4) = num2 << 16 - j;
				num2 <<= 1;
			}
			*((ref h->maxcode.FixedElementField) + (IntPtr)j * 4) = uint.MaxValue;
			CRuntime.memset((void*)(&h->fast.FixedElementField), 255, 512UL);
			for (int i = 0; i < num; i++)
			{
				int num3 = (int)(*((ref h->size.FixedElementField) + i));
				if (num3 <= 9)
				{
					int num4 = (int)(*((ref h->code.FixedElementField) + (IntPtr)i * 2)) << 9 - num3;
					int num5 = 1 << 9 - num3;
					for (j = 0; j < num5; j++)
					{
						*((ref h->fast.FixedElementField) + (num4 + j)) = (byte)i;
					}
				}
			}
			return 1;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005FB8 File Offset: 0x000041B8
		public unsafe static void stbi__build_fast_ac(short[] fast_ac, StbImage.stbi__huffman* h)
		{
			for (int i = 0; i < 512; i++)
			{
				byte b = *((ref h->fast.FixedElementField) + i);
				fast_ac[i] = 0;
				if (b < 255)
				{
					byte b2 = *((ref h->values.FixedElementField) + b);
					int num = (b2 >> 4) & 15;
					int num2 = (int)(b2 & 15);
					int num3 = (int)(*((ref h->size.FixedElementField) + b));
					if (num2 != 0 && num3 + num2 <= 9)
					{
						int num4 = ((i << num3) & 511) >> 9 - num2;
						int num5 = 1 << num2 - 1;
						if (num4 < num5)
						{
							num4 += (-1 << num2) + 1;
						}
						if (num4 >= -128 && num4 <= 127)
						{
							fast_ac[i] = (short)(num4 * 256 + num * 16 + num3 + num2);
						}
					}
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006088 File Offset: 0x00004288
		public static void stbi__grow_buffer_unsafe(StbImage.stbi__jpeg j)
		{
			int num2;
			for (;;)
			{
				uint num = (uint)((j.nomore != 0) ? 0 : StbImage.stbi__get8(j.s));
				if (num == 255U)
				{
					for (num2 = (int)StbImage.stbi__get8(j.s); num2 == 255; num2 = (int)StbImage.stbi__get8(j.s))
					{
					}
					if (num2 != 0)
					{
						break;
					}
				}
				j.code_buffer |= num << 24 - j.code_bits;
				j.code_bits += 8;
				if (j.code_bits > 24)
				{
					return;
				}
			}
			j.marker = (byte)num2;
			j.nomore = 1;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006120 File Offset: 0x00004320
		public unsafe static int stbi__jpeg_huff_decode(StbImage.stbi__jpeg j, StbImage.stbi__huffman* h)
		{
			if (j.code_bits < 16)
			{
				StbImage.stbi__grow_buffer_unsafe(j);
			}
			int num = (int)((j.code_buffer >> 23) & 511U);
			int num2 = (int)(*((ref h->fast.FixedElementField) + num));
			if (num2 < 255)
			{
				int num3 = (int)(*((ref h->size.FixedElementField) + num2));
				if (num3 > j.code_bits)
				{
					return -1;
				}
				j.code_buffer <<= num3;
				j.code_bits -= num3;
				return (int)(*((ref h->values.FixedElementField) + num2));
			}
			else
			{
				uint num4 = j.code_buffer >> 16;
				num2 = 10;
				while (num4 >= *((ref h->maxcode.FixedElementField) + (IntPtr)num2 * 4))
				{
					num2++;
				}
				if (num2 == 17)
				{
					j.code_bits -= 16;
					return -1;
				}
				if (num2 > j.code_bits)
				{
					return -1;
				}
				num = (int)((ulong)((j.code_buffer >> 32 - num2) & StbImage.stbi__bmask[num2]) + (ulong)((long)(*((ref h->delta.FixedElementField) + (IntPtr)num2 * 4))));
				j.code_bits -= num2;
				j.code_buffer <<= num2;
				return (int)(*((ref h->values.FixedElementField) + num));
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006254 File Offset: 0x00004454
		public static int stbi__extend_receive(StbImage.stbi__jpeg j, int n)
		{
			if (j.code_bits < n)
			{
				StbImage.stbi__grow_buffer_unsafe(j);
			}
			int num = (int)(j.code_buffer >> 31);
			uint num2 = CRuntime._lrotl(j.code_buffer, n);
			j.code_buffer = num2 & ~StbImage.stbi__bmask[n];
			num2 &= StbImage.stbi__bmask[n];
			j.code_bits -= n;
			return (int)((ulong)num2 + (ulong)((long)(StbImage.stbi__jbias[n] & (num - 1))));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000062C4 File Offset: 0x000044C4
		public static int stbi__jpeg_get_bits(StbImage.stbi__jpeg j, int n)
		{
			if (j.code_bits < n)
			{
				StbImage.stbi__grow_buffer_unsafe(j);
			}
			uint num = CRuntime._lrotl(j.code_buffer, n);
			j.code_buffer = num & ~StbImage.stbi__bmask[n];
			num &= StbImage.stbi__bmask[n];
			j.code_bits -= n;
			return (int)num;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006318 File Offset: 0x00004518
		public static int stbi__jpeg_get_bit(StbImage.stbi__jpeg j)
		{
			if (j.code_bits < 1)
			{
				StbImage.stbi__grow_buffer_unsafe(j);
			}
			int code_buffer = (int)j.code_buffer;
			j.code_buffer <<= 1;
			j.code_bits--;
			return code_buffer & int.MinValue;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006354 File Offset: 0x00004554
		public unsafe static int stbi__jpeg_decode_block(StbImage.stbi__jpeg j, short* data, StbImage.stbi__huffman* hdc, StbImage.stbi__huffman* hac, short[] fac, int b, ushort[] dequant)
		{
			if (j.code_bits < 16)
			{
				StbImage.stbi__grow_buffer_unsafe(j);
			}
			int num = StbImage.stbi__jpeg_huff_decode(j, hdc);
			if (num < 0 || num > 15)
			{
				return StbImage.stbi__err("bad huffman code");
			}
			CRuntime.memset((void*)data, 0, 128UL);
			int num2 = ((num != 0) ? StbImage.stbi__extend_receive(j, num) : 0);
			int num3 = j.img_comp[b].dc_pred + num2;
			j.img_comp[b].dc_pred = num3;
			*data = (short)(num3 * (int)dequant[0]);
			int num4 = 1;
			for (;;)
			{
				if (j.code_bits < 16)
				{
					StbImage.stbi__grow_buffer_unsafe(j);
				}
				int num5 = (int)((j.code_buffer >> 23) & 511U);
				int num6 = (int)fac[num5];
				if (num6 != 0)
				{
					num4 += (num6 >> 4) & 15;
					int num7 = num6 & 15;
					j.code_buffer <<= num7;
					j.code_bits -= num7;
					uint num8 = (uint)StbImage.stbi__jpeg_dezigzag[num4++];
					data[(ulong)num8 * 2UL / 2UL] = (short)((num6 >> 8) * (int)dequant[(int)num8]);
				}
				else
				{
					int num9 = StbImage.stbi__jpeg_huff_decode(j, hac);
					if (num9 < 0)
					{
						break;
					}
					int num7 = num9 & 15;
					num6 = num9 >> 4;
					if (num7 == 0)
					{
						if (num9 != 240)
						{
							return 1;
						}
						num4 += 16;
					}
					else
					{
						num4 += num6;
						uint num8 = (uint)StbImage.stbi__jpeg_dezigzag[num4++];
						data[(ulong)num8 * 2UL / 2UL] = (short)(StbImage.stbi__extend_receive(j, num7) * (int)dequant[(int)num8]);
					}
				}
				if (num4 >= 64)
				{
					return 1;
				}
			}
			return StbImage.stbi__err("bad huffman code");
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000064E0 File Offset: 0x000046E0
		public unsafe static int stbi__jpeg_decode_block_prog_dc(StbImage.stbi__jpeg j, short* data, StbImage.stbi__huffman* hdc, int b)
		{
			if (j.spec_end != 0)
			{
				return StbImage.stbi__err("can't merge dc and ac");
			}
			if (j.code_bits < 16)
			{
				StbImage.stbi__grow_buffer_unsafe(j);
			}
			if (j.succ_high == 0)
			{
				CRuntime.memset((void*)data, 0, 128UL);
				int num = StbImage.stbi__jpeg_huff_decode(j, hdc);
				if (num < 0 || num > 15)
				{
					return StbImage.stbi__err("can't merge dc and ac");
				}
				int num2 = ((num != 0) ? StbImage.stbi__extend_receive(j, num) : 0);
				int num3 = j.img_comp[b].dc_pred + num2;
				j.img_comp[b].dc_pred = num3;
				*data = (short)(num3 * (1 << j.succ_low));
			}
			else if (StbImage.stbi__jpeg_get_bit(j) != 0)
			{
				*data += (short)(1 << j.succ_low);
			}
			return 1;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000065A8 File Offset: 0x000047A8
		public unsafe static int stbi__jpeg_decode_block_prog_ac(StbImage.stbi__jpeg j, short* data, StbImage.stbi__huffman* hac, short[] fac)
		{
			if (j.spec_start == 0)
			{
				return StbImage.stbi__err("can't merge dc and ac");
			}
			if (j.succ_high == 0)
			{
				int succ_low = j.succ_low;
				if (j.eob_run != 0)
				{
					j.eob_run--;
					return 1;
				}
				int i = j.spec_start;
				int num2;
				for (;;)
				{
					if (j.code_bits < 16)
					{
						StbImage.stbi__grow_buffer_unsafe(j);
					}
					int num = (int)((j.code_buffer >> 23) & 511U);
					num2 = (int)fac[num];
					if (num2 != 0)
					{
						i += (num2 >> 4) & 15;
						int num3 = num2 & 15;
						j.code_buffer <<= num3;
						j.code_bits -= num3;
						uint num4 = (uint)StbImage.stbi__jpeg_dezigzag[i++];
						data[(ulong)num4 * 2UL / 2UL] = (short)((num2 >> 8) * (1 << succ_low));
					}
					else
					{
						int num5 = StbImage.stbi__jpeg_huff_decode(j, hac);
						if (num5 < 0)
						{
							break;
						}
						int num3 = num5 & 15;
						num2 = num5 >> 4;
						if (num3 == 0)
						{
							if (num2 < 15)
							{
								goto Block_8;
							}
							i += 16;
						}
						else
						{
							i += num2;
							uint num4 = (uint)StbImage.stbi__jpeg_dezigzag[i++];
							data[(ulong)num4 * 2UL / 2UL] = (short)(StbImage.stbi__extend_receive(j, num3) * (1 << succ_low));
						}
					}
					if (i > j.spec_end)
					{
						goto Block_10;
					}
				}
				return StbImage.stbi__err("bad huffman code");
				Block_8:
				j.eob_run = 1 << num2;
				if (num2 != 0)
				{
					j.eob_run += StbImage.stbi__jpeg_get_bits(j, num2);
				}
				j.eob_run--;
				Block_10:;
			}
			else
			{
				short num6 = (short)(1 << j.succ_low);
				if (j.eob_run == 0)
				{
					int i = j.spec_start;
					for (;;)
					{
						int num7 = StbImage.stbi__jpeg_huff_decode(j, hac);
						if (num7 < 0)
						{
							break;
						}
						int num8 = num7 & 15;
						int num9 = num7 >> 4;
						if (num8 == 0)
						{
							if (num9 < 15)
							{
								j.eob_run = (1 << num9) - 1;
								if (num9 != 0)
								{
									j.eob_run += StbImage.stbi__jpeg_get_bits(j, num9);
								}
								num9 = 64;
							}
						}
						else
						{
							if (num8 != 1)
							{
								goto Block_21;
							}
							if (StbImage.stbi__jpeg_get_bit(j) != 0)
							{
								num8 = (int)num6;
							}
							else
							{
								num8 = (int)(-(int)num6);
							}
						}
						while (i <= j.spec_end)
						{
							short* ptr = data + (IntPtr)StbImage.stbi__jpeg_dezigzag[i++];
							if (*ptr != 0)
							{
								if (StbImage.stbi__jpeg_get_bit(j) != 0 && (*ptr & num6) == 0)
								{
									if (*ptr > 0)
									{
										short* ptr2 = ptr;
										*ptr2 += num6;
									}
									else
									{
										short* ptr3 = ptr;
										*ptr3 -= num6;
									}
								}
							}
							else
							{
								if (num9 == 0)
								{
									*ptr = (short)num8;
									break;
								}
								num9--;
							}
						}
						if (i > j.spec_end)
						{
							return 1;
						}
					}
					return StbImage.stbi__err("bad huffman code");
					Block_21:
					return StbImage.stbi__err("bad huffman code");
				}
				j.eob_run--;
				for (int i = j.spec_start; i <= j.spec_end; i++)
				{
					short* ptr4 = data + StbImage.stbi__jpeg_dezigzag[i];
					if (*ptr4 != 0 && StbImage.stbi__jpeg_get_bit(j) != 0 && (*ptr4 & num6) == 0)
					{
						if (*ptr4 > 0)
						{
							short* ptr5 = ptr4;
							*ptr5 += num6;
						}
						else
						{
							short* ptr6 = ptr4;
							*ptr6 -= num6;
						}
					}
				}
			}
			return 1;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000068B0 File Offset: 0x00004AB0
		public unsafe static void stbi__idct_block(byte* _out_, int out_stride, short* data)
		{
			int* ptr = stackalloc int[(UIntPtr)256];
			int* ptr2 = ptr;
			short* ptr3 = data;
			int i = 0;
			while (i < 8)
			{
				if (ptr3[8] == 0 && ptr3[16] == 0 && ptr3[24] == 0 && ptr3[32] == 0 && ptr3[40] == 0 && ptr3[48] == 0 && ptr3[56] == 0)
				{
					int num = (int)(*ptr3 * 4);
					*ptr2 = (ptr2[8] = (ptr2[16] = (ptr2[24] = (ptr2[32] = (ptr2[40] = (ptr2[48] = (ptr2[56] = num)))))));
				}
				else
				{
					int num2 = (int)ptr3[16];
					int num3 = (int)ptr3[48];
					int num4 = (num2 + num3) * 2217;
					int num5 = num4 + num3 * -7567;
					int num6 = num4 + num2 * 3135;
					num2 = (int)(*ptr3);
					num3 = (int)ptr3[32];
					int num7 = (num2 + num3) * 4096;
					int num8 = (num2 - num3) * 4096;
					int num9 = num7 + num6;
					int num10 = num7 - num6;
					int num11 = num8 + num5;
					int num12 = num8 - num5;
					num7 = (int)ptr3[56];
					num8 = (int)ptr3[40];
					num5 = (int)ptr3[24];
					num6 = (int)ptr3[8];
					num3 = num7 + num5;
					int num13 = num8 + num6;
					num4 = num7 + num6;
					num2 = num8 + num5;
					int num14 = (num3 + num13) * 4816;
					num7 *= 1223;
					num8 *= 8410;
					num5 *= 12586;
					num6 *= 6149;
					num4 = num14 + num4 * -3685;
					num2 = num14 + num2 * -10497;
					num3 *= -8034;
					num13 *= -1597;
					num6 += num4 + num13;
					num5 += num2 + num3;
					num8 += num2 + num13;
					num7 += num4 + num3;
					num9 += 512;
					num11 += 512;
					num12 += 512;
					num10 += 512;
					*ptr2 = num9 + num6 >> 10;
					ptr2[56] = num9 - num6 >> 10;
					ptr2[8] = num11 + num5 >> 10;
					ptr2[48] = num11 - num5 >> 10;
					ptr2[16] = num12 + num8 >> 10;
					ptr2[40] = num12 - num8 >> 10;
					ptr2[24] = num10 + num7 >> 10;
					ptr2[32] = num10 - num7 >> 10;
				}
				i++;
				ptr3++;
				ptr2++;
			}
			i = 0;
			ptr2 = ptr;
			byte* ptr4 = _out_;
			while (i < 8)
			{
				int num15 = ptr2[2];
				int num16 = ptr2[6];
				int num17 = (num15 + num16) * 2217;
				int num18 = num17 + num16 * -7567;
				int num19 = num17 + num15 * 3135;
				num15 = *ptr2;
				num16 = ptr2[4];
				int num20 = (num15 + num16) * 4096;
				int num21 = (num15 - num16) * 4096;
				int num22 = num20 + num19;
				int num23 = num20 - num19;
				int num24 = num21 + num18;
				int num25 = num21 - num18;
				num20 = ptr2[7];
				num21 = ptr2[5];
				num18 = ptr2[3];
				num19 = ptr2[1];
				num16 = num20 + num18;
				int num26 = num21 + num19;
				num17 = num20 + num19;
				num15 = num21 + num18;
				int num27 = (num16 + num26) * 4816;
				num20 *= 1223;
				num21 *= 8410;
				num18 *= 12586;
				num19 *= 6149;
				num17 = num27 + num17 * -3685;
				num15 = num27 + num15 * -10497;
				num16 *= -8034;
				num26 *= -1597;
				num19 += num17 + num26;
				num18 += num15 + num16;
				num21 += num15 + num26;
				num20 += num17 + num16;
				num22 += 16842752;
				num24 += 16842752;
				num25 += 16842752;
				num23 += 16842752;
				*ptr4 = StbImage.stbi__clamp(num22 + num19 >> 17);
				ptr4[7] = StbImage.stbi__clamp(num22 - num19 >> 17);
				ptr4[1] = StbImage.stbi__clamp(num24 + num18 >> 17);
				ptr4[6] = StbImage.stbi__clamp(num24 - num18 >> 17);
				ptr4[2] = StbImage.stbi__clamp(num25 + num21 >> 17);
				ptr4[5] = StbImage.stbi__clamp(num25 - num21 >> 17);
				ptr4[3] = StbImage.stbi__clamp(num23 + num20 >> 17);
				ptr4[4] = StbImage.stbi__clamp(num23 - num20 >> 17);
				i++;
				ptr2 += 8;
				ptr4 += out_stride;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006DF8 File Offset: 0x00004FF8
		public static byte stbi__get_marker(StbImage.stbi__jpeg j)
		{
			byte b;
			if (j.marker != 255)
			{
				b = j.marker;
				j.marker = byte.MaxValue;
				return b;
			}
			b = StbImage.stbi__get8(j.s);
			if (b != 255)
			{
				return byte.MaxValue;
			}
			while (b == 255)
			{
				b = StbImage.stbi__get8(j.s);
			}
			return b;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006E58 File Offset: 0x00005058
		public static void stbi__jpeg_reset(StbImage.stbi__jpeg j)
		{
			j.code_bits = 0;
			j.code_buffer = 0U;
			j.nomore = 0;
			j.img_comp[0].dc_pred = (j.img_comp[1].dc_pred = (j.img_comp[2].dc_pred = (j.img_comp[3].dc_pred = 0)));
			j.marker = byte.MaxValue;
			j.todo = ((j.restart_interval != 0) ? j.restart_interval : int.MaxValue);
			j.eob_run = 0;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006EF8 File Offset: 0x000050F8
		public unsafe static int stbi__parse_entropy_coded_data(StbImage.stbi__jpeg z)
		{
			StbImage.stbi__jpeg_reset(z);
			if (z.progressive == 0)
			{
				if (z.scan_n == 1)
				{
					short* ptr = stackalloc short[(UIntPtr)128];
					int num = z.order[0];
					int num2 = z.img_comp[num].x + 7 >> 3;
					int num3 = z.img_comp[num].y + 7 >> 3;
					for (int i = 0; i < num3; i++)
					{
						for (int j = 0; j < num2; j++)
						{
							int ha = z.img_comp[num].ha;
							fixed (StbImage.stbi__huffman* ptr2 = &z.huff_dc[z.img_comp[num].hd])
							{
								StbImage.stbi__huffman* ptr3 = ptr2;
								fixed (StbImage.stbi__huffman* ptr4 = &z.huff_ac[ha])
								{
									StbImage.stbi__huffman* ptr5 = ptr4;
									if (StbImage.stbi__jpeg_decode_block(z, ptr, ptr3, ptr5, z.fast_ac[ha], num, z.dequant[z.img_comp[num].tq]) == 0)
									{
										return 0;
									}
								}
							}
							z.idct_block_kernel(z.img_comp[num].data + z.img_comp[num].w2 * i * 8 + j * 8, z.img_comp[num].w2, ptr);
							int num4 = z.todo - 1;
							z.todo = num4;
							if (num4 <= 0)
							{
								if (z.code_bits < 24)
								{
									StbImage.stbi__grow_buffer_unsafe(z);
								}
								if (z.marker < 208 || z.marker > 215)
								{
									return 1;
								}
								StbImage.stbi__jpeg_reset(z);
							}
						}
					}
					return 1;
				}
				short* ptr6 = stackalloc short[(UIntPtr)128];
				for (int k = 0; k < z.img_mcu_y; k++)
				{
					for (int l = 0; l < z.img_mcu_x; l++)
					{
						for (int m = 0; m < z.scan_n; m++)
						{
							int num5 = z.order[m];
							for (int n = 0; n < z.img_comp[num5].v; n++)
							{
								for (int num6 = 0; num6 < z.img_comp[num5].h; num6++)
								{
									int num7 = (l * z.img_comp[num5].h + num6) * 8;
									int num8 = (k * z.img_comp[num5].v + n) * 8;
									int ha2 = z.img_comp[num5].ha;
									fixed (StbImage.stbi__huffman* ptr2 = &z.huff_dc[z.img_comp[num5].hd])
									{
										StbImage.stbi__huffman* ptr7 = ptr2;
										fixed (StbImage.stbi__huffman* ptr4 = &z.huff_ac[ha2])
										{
											StbImage.stbi__huffman* ptr8 = ptr4;
											if (StbImage.stbi__jpeg_decode_block(z, ptr6, ptr7, ptr8, z.fast_ac[ha2], num5, z.dequant[z.img_comp[num5].tq]) == 0)
											{
												return 0;
											}
										}
									}
									z.idct_block_kernel(z.img_comp[num5].data + z.img_comp[num5].w2 * num8 + num7, z.img_comp[num5].w2, ptr6);
								}
							}
						}
						int num4 = z.todo - 1;
						z.todo = num4;
						if (num4 <= 0)
						{
							if (z.code_bits < 24)
							{
								StbImage.stbi__grow_buffer_unsafe(z);
							}
							if (z.marker < 208 || z.marker > 215)
							{
								return 1;
							}
							StbImage.stbi__jpeg_reset(z);
						}
					}
				}
				return 1;
			}
			else
			{
				if (z.scan_n == 1)
				{
					int num9 = z.order[0];
					int num10 = z.img_comp[num9].x + 7 >> 3;
					int num11 = z.img_comp[num9].y + 7 >> 3;
					for (int num12 = 0; num12 < num11; num12++)
					{
						for (int num13 = 0; num13 < num10; num13++)
						{
							short* ptr9 = z.img_comp[num9].coeff + 64 * (num13 + num12 * z.img_comp[num9].coeff_w);
							if (z.spec_start == 0)
							{
								fixed (StbImage.stbi__huffman* ptr2 = &z.huff_dc[z.img_comp[num9].hd])
								{
									StbImage.stbi__huffman* ptr10 = ptr2;
									if (StbImage.stbi__jpeg_decode_block_prog_dc(z, ptr9, ptr10, num9) == 0)
									{
										return 0;
									}
								}
							}
							else
							{
								int ha3 = z.img_comp[num9].ha;
								fixed (StbImage.stbi__huffman* ptr2 = &z.huff_ac[ha3])
								{
									StbImage.stbi__huffman* ptr11 = ptr2;
									if (StbImage.stbi__jpeg_decode_block_prog_ac(z, ptr9, ptr11, z.fast_ac[ha3]) == 0)
									{
										return 0;
									}
								}
							}
							int num4 = z.todo - 1;
							z.todo = num4;
							if (num4 <= 0)
							{
								if (z.code_bits < 24)
								{
									StbImage.stbi__grow_buffer_unsafe(z);
								}
								if (z.marker < 208 || z.marker > 215)
								{
									return 1;
								}
								StbImage.stbi__jpeg_reset(z);
							}
						}
					}
					return 1;
				}
				for (int num14 = 0; num14 < z.img_mcu_y; num14++)
				{
					for (int num15 = 0; num15 < z.img_mcu_x; num15++)
					{
						for (int num16 = 0; num16 < z.scan_n; num16++)
						{
							int num17 = z.order[num16];
							for (int num18 = 0; num18 < z.img_comp[num17].v; num18++)
							{
								for (int num19 = 0; num19 < z.img_comp[num17].h; num19++)
								{
									int num20 = num15 * z.img_comp[num17].h + num19;
									int num21 = num14 * z.img_comp[num17].v + num18;
									short* ptr12 = z.img_comp[num17].coeff + 64 * (num20 + num21 * z.img_comp[num17].coeff_w);
									fixed (StbImage.stbi__huffman* ptr2 = &z.huff_dc[z.img_comp[num17].hd])
									{
										StbImage.stbi__huffman* ptr13 = ptr2;
										if (StbImage.stbi__jpeg_decode_block_prog_dc(z, ptr12, ptr13, num17) == 0)
										{
											return 0;
										}
									}
								}
							}
						}
						int num4 = z.todo - 1;
						z.todo = num4;
						if (num4 <= 0)
						{
							if (z.code_bits < 24)
							{
								StbImage.stbi__grow_buffer_unsafe(z);
							}
							if (z.marker < 208 || z.marker > 215)
							{
								return 1;
							}
							StbImage.stbi__jpeg_reset(z);
						}
					}
				}
				return 1;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000075DC File Offset: 0x000057DC
		public unsafe static void stbi__jpeg_dequantize(short* data, ushort[] dequant)
		{
			for (int i = 0; i < 64; i++)
			{
				short* ptr = data + i;
				*ptr *= (short)dequant[i];
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007608 File Offset: 0x00005808
		public unsafe static void stbi__jpeg_finish(StbImage.stbi__jpeg z)
		{
			if (z.progressive != 0)
			{
				for (int i = 0; i < z.s.img_n; i++)
				{
					int num = z.img_comp[i].x + 7 >> 3;
					int num2 = z.img_comp[i].y + 7 >> 3;
					for (int j = 0; j < num2; j++)
					{
						for (int k = 0; k < num; k++)
						{
							short* ptr = z.img_comp[i].coeff + 64 * (k + j * z.img_comp[i].coeff_w);
							StbImage.stbi__jpeg_dequantize(ptr, z.dequant[z.img_comp[i].tq]);
							z.idct_block_kernel(z.img_comp[i].data + z.img_comp[i].w2 * j * 8 + k * 8, z.img_comp[i].w2, ptr);
						}
					}
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000772C File Offset: 0x0000592C
		public unsafe static int stbi__process_marker(StbImage.stbi__jpeg z, int m)
		{
			int i;
			if (m <= 219)
			{
				if (m != 196)
				{
					if (m == 219)
					{
						int num2;
						for (i = StbImage.stbi__get16be(z.s) - 2; i > 0; i -= ((num2 != 0) ? 129 : 65))
						{
							byte b = StbImage.stbi__get8(z.s);
							int num = b >> 4;
							num2 = ((num != 0) ? 1 : 0);
							int num3 = (int)(b & 15);
							if (num != 0 && num != 1)
							{
								return StbImage.stbi__err("bad DQT type");
							}
							if (num3 > 3)
							{
								return StbImage.stbi__err("bad DQT table");
							}
							for (int j = 0; j < 64; j++)
							{
								z.dequant[num3][(int)StbImage.stbi__jpeg_dezigzag[j]] = (ushort)((num2 != 0) ? StbImage.stbi__get16be(z.s) : ((int)StbImage.stbi__get8(z.s)));
							}
						}
						if (i != 0)
						{
							return 0;
						}
						return 1;
					}
				}
				else
				{
					int num4;
					for (i = StbImage.stbi__get16be(z.s) - 2; i > 0; i -= num4)
					{
						int* ptr = stackalloc int[(UIntPtr)64];
						num4 = 0;
						byte b2 = StbImage.stbi__get8(z.s);
						int num5 = b2 >> 4;
						int num6 = (int)(b2 & 15);
						if (num5 > 1 || num6 > 3)
						{
							return StbImage.stbi__err("bad DHT header");
						}
						for (int k = 0; k < 16; k++)
						{
							ptr[k] = (int)StbImage.stbi__get8(z.s);
							num4 += ptr[k];
						}
						i -= 17;
						if (num5 == 0)
						{
							fixed (StbImage.stbi__huffman* ptr2 = &z.huff_dc[num6])
							{
								StbImage.stbi__huffman* ptr3 = ptr2;
								if (StbImage.stbi__build_huffman(ptr3, ptr) == 0)
								{
									return 0;
								}
								byte* ptr4 = &ptr3->values.FixedElementField;
								for (int k = 0; k < num4; k++)
								{
									ptr4[k] = StbImage.stbi__get8(z.s);
								}
							}
						}
						else
						{
							fixed (StbImage.stbi__huffman* ptr2 = &z.huff_ac[num6])
							{
								StbImage.stbi__huffman* ptr5 = ptr2;
								if (StbImage.stbi__build_huffman(ptr5, ptr) == 0)
								{
									return 0;
								}
								byte* ptr6 = &ptr5->values.FixedElementField;
								for (int k = 0; k < num4; k++)
								{
									ptr6[k] = StbImage.stbi__get8(z.s);
								}
							}
						}
						if (num5 != 0)
						{
							fixed (StbImage.stbi__huffman* ptr2 = &z.huff_ac[num6])
							{
								StbImage.stbi__huffman* ptr7 = ptr2;
								StbImage.stbi__build_fast_ac(z.fast_ac[num6], ptr7);
							}
						}
					}
					if (i != 0)
					{
						return 0;
					}
					return 1;
				}
			}
			else if (m != 221)
			{
				if (m == 255)
				{
					return StbImage.stbi__err("expected marker");
				}
			}
			else
			{
				if (StbImage.stbi__get16be(z.s) != 4)
				{
					return StbImage.stbi__err("bad DRI len");
				}
				z.restart_interval = StbImage.stbi__get16be(z.s);
				return 1;
			}
			if ((m < 224 || m > 239) && m != 254)
			{
				return StbImage.stbi__err("unknown marker");
			}
			i = StbImage.stbi__get16be(z.s);
			if (i >= 2)
			{
				i -= 2;
				if (m == 224 && i >= 5)
				{
					int num7 = 1;
					for (int l = 0; l < 5; l++)
					{
						if (StbImage.stbi__get8(z.s) != StbImage.stbi__process_marker_tag[l])
						{
							num7 = 0;
						}
					}
					i -= 5;
					if (num7 != 0)
					{
						z.jfif = 1;
					}
				}
				else if (m == 238 && i >= 12)
				{
					int num8 = 1;
					for (int n = 0; n < 6; n++)
					{
						if (StbImage.stbi__get8(z.s) != StbImage.stbi__process_marker_tag[n])
						{
							num8 = 0;
						}
					}
					i -= 6;
					if (num8 != 0)
					{
						StbImage.stbi__get8(z.s);
						StbImage.stbi__get16be(z.s);
						StbImage.stbi__get16be(z.s);
						z.app14_color_transform = (int)StbImage.stbi__get8(z.s);
						i -= 6;
					}
				}
				StbImage.stbi__skip(z.s, i);
				return 1;
			}
			if (m == 254)
			{
				return StbImage.stbi__err("bad COM len");
			}
			return StbImage.stbi__err("bad APP len");
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007AF4 File Offset: 0x00005CF4
		public static int stbi__process_scan_header(StbImage.stbi__jpeg z)
		{
			int num = StbImage.stbi__get16be(z.s);
			z.scan_n = (int)StbImage.stbi__get8(z.s);
			if (z.scan_n < 1 || z.scan_n > 4 || z.scan_n > z.s.img_n)
			{
				return StbImage.stbi__err("bad SOS component count");
			}
			if (num != 6 + 2 * z.scan_n)
			{
				return StbImage.stbi__err("bad SOS len");
			}
			for (int i = 0; i < z.scan_n; i++)
			{
				int num2 = (int)StbImage.stbi__get8(z.s);
				int num3 = (int)StbImage.stbi__get8(z.s);
				int num4 = 0;
				while (num4 < z.s.img_n && z.img_comp[num4].id != num2)
				{
					num4++;
				}
				if (num4 == z.s.img_n)
				{
					return 0;
				}
				z.img_comp[num4].hd = num3 >> 4;
				if (z.img_comp[num4].hd > 3)
				{
					return StbImage.stbi__err("bad DC huff");
				}
				z.img_comp[num4].ha = num3 & 15;
				if (z.img_comp[num4].ha > 3)
				{
					return StbImage.stbi__err("bad AC huff");
				}
				z.order[i] = num4;
			}
			z.spec_start = (int)StbImage.stbi__get8(z.s);
			z.spec_end = (int)StbImage.stbi__get8(z.s);
			int num5 = (int)StbImage.stbi__get8(z.s);
			z.succ_high = num5 >> 4;
			z.succ_low = num5 & 15;
			if (z.progressive != 0)
			{
				if (z.spec_start > 63 || z.spec_end > 63 || z.spec_start > z.spec_end || z.succ_high > 13 || z.succ_low > 13)
				{
					return StbImage.stbi__err("bad SOS");
				}
			}
			else
			{
				if (z.spec_start != 0)
				{
					return StbImage.stbi__err("bad SOS");
				}
				if (z.succ_high != 0 || z.succ_low != 0)
				{
					return StbImage.stbi__err("bad SOS");
				}
				z.spec_end = 63;
			}
			return 1;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007D10 File Offset: 0x00005F10
		public unsafe static int stbi__free_jpeg_components(StbImage.stbi__jpeg z, int ncomp, int why)
		{
			for (int i = 0; i < ncomp; i++)
			{
				if (z.img_comp[i].raw_data != null)
				{
					CRuntime.free(z.img_comp[i].raw_data);
					z.img_comp[i].raw_data = null;
					z.img_comp[i].data = null;
				}
				if (z.img_comp[i].raw_coeff != null)
				{
					CRuntime.free(z.img_comp[i].raw_coeff);
					z.img_comp[i].raw_coeff = null;
					z.img_comp[i].coeff = null;
				}
				if (z.img_comp[i].linebuf != null)
				{
					CRuntime.free((void*)z.img_comp[i].linebuf);
					z.img_comp[i].linebuf = null;
				}
			}
			return why;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007E14 File Offset: 0x00006014
		public unsafe static int stbi__process_frame_header(StbImage.stbi__jpeg z, int scan)
		{
			StbImage.stbi__context s = z.s;
			int num = 1;
			int num2 = 1;
			int num3 = StbImage.stbi__get16be(s);
			if (num3 < 11)
			{
				return StbImage.stbi__err("bad SOF len");
			}
			if (StbImage.stbi__get8(s) != 8)
			{
				return StbImage.stbi__err("only 8-bit");
			}
			s.img_y = (uint)StbImage.stbi__get16be(s);
			if (s.img_y == 0U)
			{
				return StbImage.stbi__err("no header height");
			}
			s.img_x = (uint)StbImage.stbi__get16be(s);
			if (s.img_x == 0U)
			{
				return StbImage.stbi__err("0 width");
			}
			if (s.img_y > 16777216U)
			{
				return StbImage.stbi__err("too large");
			}
			if (s.img_x > 16777216U)
			{
				return StbImage.stbi__err("too large");
			}
			int num4 = (int)StbImage.stbi__get8(s);
			if (num4 != 3 && num4 != 1 && num4 != 4)
			{
				return StbImage.stbi__err("bad component count");
			}
			s.img_n = num4;
			for (int i = 0; i < num4; i++)
			{
				z.img_comp[i].data = null;
				z.img_comp[i].linebuf = null;
			}
			if (num3 != 8 + 3 * s.img_n)
			{
				return StbImage.stbi__err("bad SOF len");
			}
			z.rgb = 0;
			for (int i = 0; i < s.img_n; i++)
			{
				z.img_comp[i].id = (int)StbImage.stbi__get8(s);
				if (s.img_n == 3 && z.img_comp[i].id == (int)StbImage.stbi__process_frame_header_rgb[i])
				{
					z.rgb++;
				}
				int num5 = (int)StbImage.stbi__get8(s);
				z.img_comp[i].h = num5 >> 4;
				if (z.img_comp[i].h == 0 || z.img_comp[i].h > 4)
				{
					return StbImage.stbi__err("bad H");
				}
				z.img_comp[i].v = num5 & 15;
				if (z.img_comp[i].v == 0 || z.img_comp[i].v > 4)
				{
					return StbImage.stbi__err("bad V");
				}
				z.img_comp[i].tq = (int)StbImage.stbi__get8(s);
				if (z.img_comp[i].tq > 3)
				{
					return StbImage.stbi__err("bad TQ");
				}
			}
			if (scan != 0)
			{
				return 1;
			}
			if (StbImage.stbi__mad3sizes_valid((int)s.img_x, (int)s.img_y, s.img_n, 0) == 0)
			{
				return StbImage.stbi__err("too large");
			}
			for (int i = 0; i < s.img_n; i++)
			{
				if (z.img_comp[i].h > num)
				{
					num = z.img_comp[i].h;
				}
				if (z.img_comp[i].v > num2)
				{
					num2 = z.img_comp[i].v;
				}
			}
			for (int i = 0; i < s.img_n; i++)
			{
				if (num % z.img_comp[i].h != 0)
				{
					return StbImage.stbi__err("bad H");
				}
				if (num2 % z.img_comp[i].v != 0)
				{
					return StbImage.stbi__err("bad V");
				}
			}
			z.img_h_max = num;
			z.img_v_max = num2;
			z.img_mcu_w = num * 8;
			z.img_mcu_h = num2 * 8;
			z.img_mcu_x = (int)(((ulong)s.img_x + (ulong)((long)z.img_mcu_w) - 1UL) / (ulong)((long)z.img_mcu_w));
			z.img_mcu_y = (int)(((ulong)s.img_y + (ulong)((long)z.img_mcu_h) - 1UL) / (ulong)((long)z.img_mcu_h));
			for (int i = 0; i < s.img_n; i++)
			{
				z.img_comp[i].x = (int)(((ulong)s.img_x * (ulong)((long)z.img_comp[i].h) + (ulong)((long)num) - 1UL) / (ulong)((long)num));
				z.img_comp[i].y = (int)(((ulong)s.img_y * (ulong)((long)z.img_comp[i].v) + (ulong)((long)num2) - 1UL) / (ulong)((long)num2));
				z.img_comp[i].w2 = z.img_mcu_x * z.img_comp[i].h * 8;
				z.img_comp[i].h2 = z.img_mcu_y * z.img_comp[i].v * 8;
				z.img_comp[i].coeff = null;
				z.img_comp[i].raw_coeff = null;
				z.img_comp[i].linebuf = null;
				z.img_comp[i].raw_data = StbImage.stbi__malloc_mad2(z.img_comp[i].w2, z.img_comp[i].h2, 15);
				if (z.img_comp[i].raw_data == null)
				{
					return StbImage.stbi__free_jpeg_components(z, i + 1, StbImage.stbi__err("outofmem"));
				}
				z.img_comp[i].data = ((byte*)z.img_comp[i].raw_data + 15L) & -16L;
				if (z.progressive != 0)
				{
					z.img_comp[i].coeff_w = z.img_comp[i].w2 / 8;
					z.img_comp[i].coeff_h = z.img_comp[i].h2 / 8;
					z.img_comp[i].raw_coeff = StbImage.stbi__malloc_mad3(z.img_comp[i].w2, z.img_comp[i].h2, 2, 15);
					if (z.img_comp[i].raw_coeff == null)
					{
						return StbImage.stbi__free_jpeg_components(z, i + 1, StbImage.stbi__err("outofmem"));
					}
					z.img_comp[i].coeff = ((short*)z.img_comp[i].raw_coeff + 15L / 2L) & -16L;
				}
			}
			return 1;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00008440 File Offset: 0x00006640
		public static int stbi__decode_jpeg_header(StbImage.stbi__jpeg z, int scan)
		{
			z.jfif = 0;
			z.app14_color_transform = -1;
			z.marker = byte.MaxValue;
			int num = (int)StbImage.stbi__get_marker(z);
			if (num != 216)
			{
				return StbImage.stbi__err("no SOI");
			}
			if (scan == 1)
			{
				return 1;
			}
			num = (int)StbImage.stbi__get_marker(z);
			while (num != 192 && num != 193 && num != 194)
			{
				if (StbImage.stbi__process_marker(z, num) == 0)
				{
					return 0;
				}
				for (num = (int)StbImage.stbi__get_marker(z); num == 255; num = (int)StbImage.stbi__get_marker(z))
				{
					if (StbImage.stbi__at_eof(z.s) != 0)
					{
						return StbImage.stbi__err("no SOF");
					}
				}
			}
			z.progressive = ((num == 194) ? 1 : 0);
			if (StbImage.stbi__process_frame_header(z, scan) == 0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00008504 File Offset: 0x00006704
		public static int stbi__decode_jpeg_image(StbImage.stbi__jpeg j)
		{
			for (int i = 0; i < 4; i++)
			{
				j.img_comp[i].raw_data = null;
				j.img_comp[i].raw_coeff = null;
			}
			j.restart_interval = 0;
			if (StbImage.stbi__decode_jpeg_header(j, 0) == 0)
			{
				return 0;
			}
			for (int i = (int)StbImage.stbi__get_marker(j); i != 217; i = (int)StbImage.stbi__get_marker(j))
			{
				if (i == 218)
				{
					if (StbImage.stbi__process_scan_header(j) == 0)
					{
						return 0;
					}
					if (StbImage.stbi__parse_entropy_coded_data(j) == 0)
					{
						return 0;
					}
					if (j.marker == 255)
					{
						while (StbImage.stbi__at_eof(j.s) == 0)
						{
							if (StbImage.stbi__get8(j.s) == 255)
							{
								j.marker = StbImage.stbi__get8(j.s);
								break;
							}
						}
					}
				}
				else if (i == 220)
				{
					int num = StbImage.stbi__get16be(j.s);
					uint num2 = (uint)StbImage.stbi__get16be(j.s);
					if (num != 4)
					{
						return StbImage.stbi__err("bad DNL len");
					}
					if (num2 != j.s.img_y)
					{
						return StbImage.stbi__err("bad DNL height");
					}
				}
				else if (StbImage.stbi__process_marker(j, i) == 0)
				{
					return 0;
				}
			}
			if (j.progressive != 0)
			{
				StbImage.stbi__jpeg_finish(j);
			}
			return 1;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00008637 File Offset: 0x00006837
		public unsafe static byte* resample_row_1(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
		{
			return in_near;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000863C File Offset: 0x0000683C
		public unsafe static byte* stbi__resample_row_v_2(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
		{
			for (int i = 0; i < w; i++)
			{
				_out_[i] = (byte)(3 * in_near[i] + in_far[i] + 2 >> 2);
			}
			return _out_;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000866C File Offset: 0x0000686C
		public unsafe static byte* stbi__resample_row_h_2(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
		{
			if (w == 1)
			{
				*_out_ = (_out_[1] = *in_near);
				return _out_;
			}
			*_out_ = *in_near;
			_out_[1] = (byte)(*in_near * 3 + in_near[1] + 2 >> 2);
			int i;
			for (i = 1; i < w - 1; i++)
			{
				int num = (int)(3 * in_near[i] + 2);
				_out_[i * 2] = (byte)(num + (int)in_near[i - 1] >> 2);
				_out_[i * 2 + 1] = (byte)(num + (int)in_near[i + 1] >> 2);
			}
			_out_[i * 2] = (byte)(in_near[w - 2] * 3 + in_near[w - 1] + 2 >> 2);
			_out_[i * 2 + 1] = in_near[w - 1];
			return _out_;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00008708 File Offset: 0x00006908
		public unsafe static byte* stbi__resample_row_hv_2(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
		{
			if (w == 1)
			{
				*_out_ = (_out_[1] = (byte)(3 * *in_near + *in_far + 2 >> 2));
				return _out_;
			}
			int num = (int)(3 * *in_near + *in_far);
			*_out_ = (byte)(num + 2 >> 2);
			for (int i = 1; i < w; i++)
			{
				int num2 = num;
				num = (int)(3 * in_near[i] + in_far[i]);
				_out_[i * 2 - 1] = (byte)(3 * num2 + num + 8 >> 4);
				_out_[i * 2] = (byte)(3 * num + num2 + 8 >> 4);
			}
			_out_[w * 2 - 1] = (byte)(num + 2 >> 2);
			return _out_;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00008794 File Offset: 0x00006994
		public unsafe static byte* stbi__resample_row_generic(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
		{
			for (int i = 0; i < w; i++)
			{
				for (int j = 0; j < hs; j++)
				{
					_out_[i * hs + j] = in_near[i];
				}
			}
			return _out_;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000087CC File Offset: 0x000069CC
		public unsafe static void stbi__YCbCr_to_RGB_row(byte* _out_, byte* y, byte* pcb, byte* pcr, int count, int step)
		{
			for (int i = 0; i < count; i++)
			{
				int num = ((int)y[i] << 20) + 524288;
				int num2 = (int)(pcr[i] - 128);
				int num3 = (int)(pcb[i] - 128);
				int num4 = num + num2 * 1470208;
				int num5 = (int)((long)(num + num2 * -748800) + ((long)(num3 * -360960) & (long)((ulong)(-65536))));
				int num6 = num + num3 * 1858048;
				num4 >>= 20;
				num5 >>= 20;
				num6 >>= 20;
				if (num4 > 255)
				{
					if (num4 < 0)
					{
						num4 = 0;
					}
					else
					{
						num4 = 255;
					}
				}
				if (num5 > 255)
				{
					if (num5 < 0)
					{
						num5 = 0;
					}
					else
					{
						num5 = 255;
					}
				}
				if (num6 > 255)
				{
					if (num6 < 0)
					{
						num6 = 0;
					}
					else
					{
						num6 = 255;
					}
				}
				*_out_ = (byte)num4;
				_out_[1] = (byte)num5;
				_out_[2] = (byte)num6;
				_out_[3] = byte.MaxValue;
				_out_ += step;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000088BC File Offset: 0x00006ABC
		public static void stbi__setup_jpeg(StbImage.stbi__jpeg j)
		{
			j.idct_block_kernel = new StbImage.delegate0(StbImage.stbi__idct_block);
			j.YCbCr_to_RGB_kernel = new StbImage.delegate1(StbImage.stbi__YCbCr_to_RGB_row);
			j.resample_row_hv_2_kernel = new StbImage.delegate2(StbImage.stbi__resample_row_hv_2);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000088F4 File Offset: 0x00006AF4
		public static void stbi__cleanup_jpeg(StbImage.stbi__jpeg j)
		{
			StbImage.stbi__free_jpeg_components(j, j.s.img_n, 0);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000890C File Offset: 0x00006B0C
		public unsafe static byte* load_jpeg_image(StbImage.stbi__jpeg z, int* out_x, int* out_y, int* comp, int req_comp)
		{
			z.s.img_n = 0;
			if (req_comp < 0 || req_comp > 4)
			{
				return (StbImage.stbi__err("bad req_comp") != 0) ? null : null;
			}
			if (StbImage.stbi__decode_jpeg_image(z) == 0)
			{
				StbImage.stbi__cleanup_jpeg(z);
				return null;
			}
			int num = ((req_comp != 0) ? req_comp : ((z.s.img_n >= 3) ? 3 : 1));
			int num2 = ((z.s.img_n == 3 && (z.rgb == 3 || (z.app14_color_transform == 0 && z.jfif == 0))) ? 1 : 0);
			int num3;
			if (z.s.img_n == 3 && num < 3 && num2 == 0)
			{
				num3 = 1;
			}
			else
			{
				num3 = z.s.img_n;
			}
			if (num3 <= 0)
			{
				StbImage.stbi__cleanup_jpeg(z);
				return null;
			}
			IntPtr intPtr;
			checked
			{
				intPtr = stackalloc byte[unchecked((UIntPtr)4) * (UIntPtr)sizeof(byte*)];
			}
			*intPtr = (IntPtr)((UIntPtr)0);
			*(intPtr + (IntPtr)sizeof(byte*)) = (IntPtr)((UIntPtr)0);
			*(intPtr + (IntPtr)2 * (IntPtr)sizeof(byte*)) = (IntPtr)((UIntPtr)0);
			*(intPtr + (IntPtr)3 * (IntPtr)sizeof(byte*)) = (IntPtr)((UIntPtr)0);
			byte** ptr = intPtr;
			StbImage.stbi__resample[] array = new StbImage.stbi__resample[]
			{
				new StbImage.stbi__resample(),
				new StbImage.stbi__resample(),
				new StbImage.stbi__resample(),
				new StbImage.stbi__resample()
			};
			for (int i = 0; i < num3; i++)
			{
				StbImage.stbi__resample stbi__resample = array[i];
				z.img_comp[i].linebuf = (byte*)StbImage.stbi__malloc((ulong)(z.s.img_x + 3U));
				if (z.img_comp[i].linebuf == null)
				{
					StbImage.stbi__cleanup_jpeg(z);
					return (StbImage.stbi__err("outofmem") != 0) ? null : null;
				}
				stbi__resample.hs = z.img_h_max / z.img_comp[i].h;
				stbi__resample.vs = z.img_v_max / z.img_comp[i].v;
				stbi__resample.ystep = stbi__resample.vs >> 1;
				stbi__resample.w_lores = (int)(((ulong)z.s.img_x + (ulong)((long)stbi__resample.hs) - 1UL) / (ulong)((long)stbi__resample.hs));
				stbi__resample.ypos = 0;
				stbi__resample.line0 = (stbi__resample.line1 = z.img_comp[i].data);
				if (stbi__resample.hs == 1 && stbi__resample.vs == 1)
				{
					stbi__resample.resample = new StbImage.delegate2(StbImage.resample_row_1);
				}
				else if (stbi__resample.hs == 1 && stbi__resample.vs == 2)
				{
					stbi__resample.resample = new StbImage.delegate2(StbImage.stbi__resample_row_v_2);
				}
				else if (stbi__resample.hs == 2 && stbi__resample.vs == 1)
				{
					stbi__resample.resample = new StbImage.delegate2(StbImage.stbi__resample_row_h_2);
				}
				else if (stbi__resample.hs == 2 && stbi__resample.vs == 2)
				{
					stbi__resample.resample = z.resample_row_hv_2_kernel;
				}
				else
				{
					stbi__resample.resample = new StbImage.delegate2(StbImage.stbi__resample_row_generic);
				}
			}
			byte* ptr2 = (byte*)StbImage.stbi__malloc_mad3(num, (int)z.s.img_x, (int)z.s.img_y, 1);
			if (ptr2 == null)
			{
				StbImage.stbi__cleanup_jpeg(z);
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			for (uint num4 = 0U; num4 < z.s.img_y; num4 += 1U)
			{
				byte* ptr3 = ptr2 + (long)num * (long)((ulong)z.s.img_x) * (long)((ulong)num4);
				for (int i = 0; i < num3; i++)
				{
					StbImage.stbi__resample stbi__resample2 = array[i];
					int num5 = ((stbi__resample2.ystep >= stbi__resample2.vs >> 1) ? 1 : 0);
					*(IntPtr*)(ptr + (IntPtr)i * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = stbi__resample2.resample(z.img_comp[i].linebuf, (num5 != 0) ? stbi__resample2.line1 : stbi__resample2.line0, (num5 != 0) ? stbi__resample2.line0 : stbi__resample2.line1, stbi__resample2.w_lores, stbi__resample2.hs);
					StbImage.stbi__resample stbi__resample3 = stbi__resample2;
					int num6 = stbi__resample3.ystep + 1;
					stbi__resample3.ystep = num6;
					if (num6 >= stbi__resample2.vs)
					{
						stbi__resample2.ystep = 0;
						stbi__resample2.line0 = stbi__resample2.line1;
						StbImage.stbi__resample stbi__resample4 = stbi__resample2;
						num6 = stbi__resample4.ypos + 1;
						stbi__resample4.ypos = num6;
						if (num6 < z.img_comp[i].y)
						{
							stbi__resample2.line1 += z.img_comp[i].w2;
						}
					}
				}
				if (num >= 3)
				{
					byte* ptr4 = *(IntPtr*)ptr;
					if (z.s.img_n == 3)
					{
						if (num2 != 0)
						{
							for (uint num7 = 0U; num7 < z.s.img_x; num7 += 1U)
							{
								*ptr3 = ptr4[num7];
								ptr3[1] = *(*(IntPtr*)(ptr + sizeof(byte*) / sizeof(byte*)) + (IntPtr)((UIntPtr)num7));
								ptr3[2] = *(*(IntPtr*)(ptr + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) + (IntPtr)((UIntPtr)num7));
								ptr3[3] = byte.MaxValue;
								ptr3 += num;
							}
						}
						else
						{
							z.YCbCr_to_RGB_kernel(ptr3, ptr4, *(IntPtr*)(ptr + sizeof(byte*) / sizeof(byte*)), *(IntPtr*)(ptr + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)), (int)z.s.img_x, num);
						}
					}
					else if (z.s.img_n == 4)
					{
						if (z.app14_color_transform == 0)
						{
							for (uint num7 = 0U; num7 < z.s.img_x; num7 += 1U)
							{
								byte b = *(*(IntPtr*)(ptr + (IntPtr)3 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) + (IntPtr)((UIntPtr)num7));
								*ptr3 = StbImage.stbi__blinn_8x8(*(*(IntPtr*)ptr + (IntPtr)((UIntPtr)num7)), b);
								ptr3[1] = StbImage.stbi__blinn_8x8(*(*(IntPtr*)(ptr + sizeof(byte*) / sizeof(byte*)) + (IntPtr)((UIntPtr)num7)), b);
								ptr3[2] = StbImage.stbi__blinn_8x8(*(*(IntPtr*)(ptr + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) + (IntPtr)((UIntPtr)num7)), b);
								ptr3[3] = byte.MaxValue;
								ptr3 += num;
							}
						}
						else if (z.app14_color_transform == 2)
						{
							z.YCbCr_to_RGB_kernel(ptr3, ptr4, *(IntPtr*)(ptr + sizeof(byte*) / sizeof(byte*)), *(IntPtr*)(ptr + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)), (int)z.s.img_x, num);
							for (uint num7 = 0U; num7 < z.s.img_x; num7 += 1U)
							{
								byte b2 = *(*(IntPtr*)(ptr + (IntPtr)3 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) + (IntPtr)((UIntPtr)num7));
								*ptr3 = StbImage.stbi__blinn_8x8(byte.MaxValue - *ptr3, b2);
								ptr3[1] = StbImage.stbi__blinn_8x8(byte.MaxValue - ptr3[1], b2);
								ptr3[2] = StbImage.stbi__blinn_8x8(byte.MaxValue - ptr3[2], b2);
								ptr3 += num;
							}
						}
						else
						{
							z.YCbCr_to_RGB_kernel(ptr3, ptr4, *(IntPtr*)(ptr + sizeof(byte*) / sizeof(byte*)), *(IntPtr*)(ptr + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)), (int)z.s.img_x, num);
						}
					}
					else
					{
						for (uint num7 = 0U; num7 < z.s.img_x; num7 += 1U)
						{
							*ptr3 = (ptr3[1] = (ptr3[2] = ptr4[num7]));
							ptr3[3] = byte.MaxValue;
							ptr3 += num;
						}
					}
				}
				else if (num2 != 0)
				{
					if (num == 1)
					{
						for (uint num7 = 0U; num7 < z.s.img_x; num7 += 1U)
						{
							*(ptr3++) = StbImage.stbi__compute_y((int)(*(*(IntPtr*)ptr + (IntPtr)((UIntPtr)num7))), (int)(*(*(IntPtr*)(ptr + sizeof(byte*) / sizeof(byte*)) + (IntPtr)((UIntPtr)num7))), (int)(*(*(IntPtr*)(ptr + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) + (IntPtr)((UIntPtr)num7))));
						}
					}
					else
					{
						uint num7 = 0U;
						while (num7 < z.s.img_x)
						{
							*ptr3 = StbImage.stbi__compute_y((int)(*(*(IntPtr*)ptr + (IntPtr)((UIntPtr)num7))), (int)(*(*(IntPtr*)(ptr + sizeof(byte*) / sizeof(byte*)) + (IntPtr)((UIntPtr)num7))), (int)(*(*(IntPtr*)(ptr + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) + (IntPtr)((UIntPtr)num7))));
							ptr3[1] = byte.MaxValue;
							num7 += 1U;
							ptr3 += 2;
						}
					}
				}
				else if (z.s.img_n == 4 && z.app14_color_transform == 0)
				{
					for (uint num7 = 0U; num7 < z.s.img_x; num7 += 1U)
					{
						byte b3 = *(*(IntPtr*)(ptr + (IntPtr)3 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) + (IntPtr)((UIntPtr)num7));
						byte b4 = StbImage.stbi__blinn_8x8(*(*(IntPtr*)ptr + (IntPtr)((UIntPtr)num7)), b3);
						byte b5 = StbImage.stbi__blinn_8x8(*(*(IntPtr*)(ptr + sizeof(byte*) / sizeof(byte*)) + (IntPtr)((UIntPtr)num7)), b3);
						byte b6 = StbImage.stbi__blinn_8x8(*(*(IntPtr*)(ptr + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) + (IntPtr)((UIntPtr)num7)), b3);
						*ptr3 = StbImage.stbi__compute_y((int)b4, (int)b5, (int)b6);
						ptr3[1] = byte.MaxValue;
						ptr3 += num;
					}
				}
				else if (z.s.img_n == 4 && z.app14_color_transform == 2)
				{
					for (uint num7 = 0U; num7 < z.s.img_x; num7 += 1U)
					{
						*ptr3 = StbImage.stbi__blinn_8x8(byte.MaxValue - *(*(IntPtr*)ptr + (IntPtr)((UIntPtr)num7)), *(*(IntPtr*)(ptr + (IntPtr)3 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) + (IntPtr)((UIntPtr)num7)));
						ptr3[1] = byte.MaxValue;
						ptr3 += num;
					}
				}
				else
				{
					byte* ptr5 = *(IntPtr*)ptr;
					if (num == 1)
					{
						for (uint num7 = 0U; num7 < z.s.img_x; num7 += 1U)
						{
							ptr3[num7] = ptr5[num7];
						}
					}
					else
					{
						for (uint num7 = 0U; num7 < z.s.img_x; num7 += 1U)
						{
							*(ptr3++) = ptr5[num7];
							*(ptr3++) = byte.MaxValue;
						}
					}
				}
			}
			StbImage.stbi__cleanup_jpeg(z);
			*out_x = (int)z.s.img_x;
			*out_y = (int)z.s.img_y;
			if (comp != null)
			{
				*comp = ((z.s.img_n >= 3) ? 3 : 1);
			}
			return ptr2;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000092AC File Offset: 0x000074AC
		public unsafe static int stbi__jpeg_info_raw(StbImage.stbi__jpeg j, int* x, int* y, int* comp)
		{
			if (StbImage.stbi__decode_jpeg_header(j, 2) == 0)
			{
				StbImage.stbi__rewind(j.s);
				return 0;
			}
			if (x != null)
			{
				*x = (int)j.s.img_x;
			}
			if (y != null)
			{
				*y = (int)j.s.img_y;
			}
			if (comp != null)
			{
				*comp = ((j.s.img_n >= 3) ? 3 : 1);
			}
			return 1;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000930D File Offset: 0x0000750D
		public static int stbi__png_test(StbImage.stbi__context s)
		{
			int num = StbImage.stbi__check_png_header(s);
			StbImage.stbi__rewind(s);
			return num;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000931B File Offset: 0x0000751B
		public unsafe static void* stbi__png_load(StbImage.stbi__context s, int* x, int* y, int* comp, int req_comp, StbImage.stbi__result_info* ri)
		{
			return StbImage.stbi__do_png(new StbImage.stbi__png
			{
				s = s
			}, x, y, comp, req_comp, ri);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00009335 File Offset: 0x00007535
		public unsafe static int stbi__png_info(StbImage.stbi__context s, int* x, int* y, int* comp)
		{
			return StbImage.stbi__png_info_raw(new StbImage.stbi__png
			{
				s = s
			}, x, y, comp);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000934C File Offset: 0x0000754C
		public static int stbi__png_is16(StbImage.stbi__context s)
		{
			StbImage.stbi__png stbi__png = new StbImage.stbi__png();
			stbi__png.s = s;
			if (StbImage.stbi__png_info_raw(stbi__png, null, null, null) == 0)
			{
				return 0;
			}
			if (stbi__png.depth != 16)
			{
				StbImage.stbi__rewind(stbi__png.s);
				return 0;
			}
			return 1;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00009390 File Offset: 0x00007590
		public static StbImage.stbi__pngchunk stbi__get_chunk_header(StbImage.stbi__context s)
		{
			return new StbImage.stbi__pngchunk
			{
				length = StbImage.stbi__get32be(s),
				type = StbImage.stbi__get32be(s)
			};
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000093C0 File Offset: 0x000075C0
		public static int stbi__check_png_header(StbImage.stbi__context s)
		{
			for (int i = 0; i < 8; i++)
			{
				if (StbImage.stbi__get8(s) != StbImage.stbi__check_png_header_png_sig[i])
				{
					return StbImage.stbi__err("bad png sig");
				}
			}
			return 1;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000093F8 File Offset: 0x000075F8
		public unsafe static int stbi__create_png_image_raw(StbImage.stbi__png a, byte* raw, uint raw_len, int out_n, uint x, uint y, int depth, int color)
		{
			int num = ((depth == 16) ? 2 : 1);
			StbImage.stbi__context s = a.s;
			uint num2 = (uint)((ulong)x * (ulong)((long)out_n) * (ulong)((long)num));
			int img_n = s.img_n;
			int num3 = out_n * num;
			int num4 = img_n * num;
			int num5 = (int)x;
			a._out_ = (byte*)StbImage.stbi__malloc_mad3((int)x, (int)y, num3, 0);
			if (a._out_ == null)
			{
				return StbImage.stbi__err("outofmem");
			}
			if (StbImage.stbi__mad3sizes_valid(img_n, (int)x, depth, 7) == 0)
			{
				return StbImage.stbi__err("too large");
			}
			uint num6 = (uint)((long)img_n * (long)((ulong)x) * (long)depth + 7L >> 3);
			uint num7 = (num6 + 1U) * y;
			if (raw_len < num7)
			{
				return StbImage.stbi__err("not enough pixels");
			}
			for (uint num8 = 0U; num8 < y; num8 += 1U)
			{
				byte* ptr = a._out_ + num2 * num8;
				int num9 = (int)(*(raw++));
				if (num9 > 4)
				{
					return StbImage.stbi__err("invalid filter");
				}
				if (depth < 8)
				{
					if (num6 > x)
					{
						return StbImage.stbi__err("invalid width");
					}
					ptr += (ulong)x * (ulong)((long)out_n) - (ulong)num6;
					num4 = 1;
					num5 = (int)num6;
				}
				byte* ptr2 = ptr - num2;
				if (num8 == 0U)
				{
					num9 = (int)StbImage.first_row_filter[num9];
				}
				for (int i = 0; i < num4; i++)
				{
					switch (num9)
					{
					case 0:
						ptr[i] = raw[i];
						break;
					case 1:
						ptr[i] = raw[i];
						break;
					case 2:
						ptr[i] = (raw[i] + ptr2[i]) & byte.MaxValue;
						break;
					case 3:
						ptr[i] = (byte)(((int)raw[i] + (ptr2[i] >> 1)) & 255);
						break;
					case 4:
						ptr[i] = (byte)(((int)raw[i] + StbImage.stbi__paeth(0, (int)ptr2[i], 0)) & 255);
						break;
					case 5:
						ptr[i] = raw[i];
						break;
					case 6:
						ptr[i] = raw[i];
						break;
					}
				}
				if (depth == 8)
				{
					if (img_n != out_n)
					{
						ptr[img_n] = byte.MaxValue;
					}
					raw += img_n;
					ptr += out_n;
					ptr2 += out_n;
				}
				else if (depth == 16)
				{
					if (img_n != out_n)
					{
						ptr[num4] = byte.MaxValue;
						ptr[num4 + 1] = byte.MaxValue;
					}
					raw += num4;
					ptr += num3;
					ptr2 += num3;
				}
				else
				{
					raw++;
					ptr++;
					ptr2++;
				}
				if (depth < 8 || img_n == out_n)
				{
					int num10 = (num5 - 1) * num4;
					switch (num9)
					{
					case 0:
						CRuntime.memcpy((void*)ptr, (void*)raw, (ulong)((long)num10));
						break;
					case 1:
					{
						for (int i = 0; i < num10; i++)
						{
							ptr[i] = (raw[i] + ptr[i - num4]) & byte.MaxValue;
						}
						break;
					}
					case 2:
					{
						for (int i = 0; i < num10; i++)
						{
							ptr[i] = (raw[i] + ptr2[i]) & byte.MaxValue;
						}
						break;
					}
					case 3:
					{
						for (int i = 0; i < num10; i++)
						{
							ptr[i] = (byte)(((int)raw[i] + (ptr2[i] + ptr[i - num4] >> 1)) & 255);
						}
						break;
					}
					case 4:
					{
						for (int i = 0; i < num10; i++)
						{
							ptr[i] = (byte)(((int)raw[i] + StbImage.stbi__paeth((int)ptr[i - num4], (int)ptr2[i], (int)ptr2[i - num4])) & 255);
						}
						break;
					}
					case 5:
					{
						for (int i = 0; i < num10; i++)
						{
							ptr[i] = (byte)(((int)raw[i] + (ptr[i - num4] >> 1)) & 255);
						}
						break;
					}
					case 6:
					{
						for (int i = 0; i < num10; i++)
						{
							ptr[i] = (byte)(((int)raw[i] + StbImage.stbi__paeth((int)ptr[i - num4], 0, 0)) & 255);
						}
						break;
					}
					}
					raw += num10;
				}
				else
				{
					switch (num9)
					{
					case 0:
					{
						uint num11 = x - 1U;
						while (num11 >= 1U)
						{
							for (int i = 0; i < num4; i++)
							{
								ptr[i] = raw[i];
							}
							num11 -= 1U;
							ptr[num4] = byte.MaxValue;
							raw += num4;
							ptr += num3;
							ptr2 += num3;
						}
						break;
					}
					case 1:
					{
						uint num11 = x - 1U;
						while (num11 >= 1U)
						{
							for (int i = 0; i < num4; i++)
							{
								ptr[i] = (raw[i] + ptr[i - num3]) & byte.MaxValue;
							}
							num11 -= 1U;
							ptr[num4] = byte.MaxValue;
							raw += num4;
							ptr += num3;
							ptr2 += num3;
						}
						break;
					}
					case 2:
					{
						uint num11 = x - 1U;
						while (num11 >= 1U)
						{
							for (int i = 0; i < num4; i++)
							{
								ptr[i] = (raw[i] + ptr2[i]) & byte.MaxValue;
							}
							num11 -= 1U;
							ptr[num4] = byte.MaxValue;
							raw += num4;
							ptr += num3;
							ptr2 += num3;
						}
						break;
					}
					case 3:
					{
						uint num11 = x - 1U;
						while (num11 >= 1U)
						{
							for (int i = 0; i < num4; i++)
							{
								ptr[i] = (byte)(((int)raw[i] + (ptr2[i] + ptr[i - num3] >> 1)) & 255);
							}
							num11 -= 1U;
							ptr[num4] = byte.MaxValue;
							raw += num4;
							ptr += num3;
							ptr2 += num3;
						}
						break;
					}
					case 4:
					{
						uint num11 = x - 1U;
						while (num11 >= 1U)
						{
							for (int i = 0; i < num4; i++)
							{
								ptr[i] = (byte)(((int)raw[i] + StbImage.stbi__paeth((int)ptr[i - num3], (int)ptr2[i], (int)ptr2[i - num3])) & 255);
							}
							num11 -= 1U;
							ptr[num4] = byte.MaxValue;
							raw += num4;
							ptr += num3;
							ptr2 += num3;
						}
						break;
					}
					case 5:
					{
						uint num11 = x - 1U;
						while (num11 >= 1U)
						{
							for (int i = 0; i < num4; i++)
							{
								ptr[i] = (byte)(((int)raw[i] + (ptr[i - num3] >> 1)) & 255);
							}
							num11 -= 1U;
							ptr[num4] = byte.MaxValue;
							raw += num4;
							ptr += num3;
							ptr2 += num3;
						}
						break;
					}
					case 6:
					{
						uint num11 = x - 1U;
						while (num11 >= 1U)
						{
							for (int i = 0; i < num4; i++)
							{
								ptr[i] = (byte)(((int)raw[i] + StbImage.stbi__paeth((int)ptr[i - num3], 0, 0)) & 255);
							}
							num11 -= 1U;
							ptr[num4] = byte.MaxValue;
							raw += num4;
							ptr += num3;
							ptr2 += num3;
						}
						break;
					}
					}
					if (depth == 16)
					{
						ptr = a._out_ + num2 * num8;
						uint num11 = 0U;
						while (num11 < x)
						{
							ptr[num4 + 1] = byte.MaxValue;
							num11 += 1U;
							ptr += num3;
						}
					}
				}
			}
			if (depth < 8)
			{
				for (uint num8 = 0U; num8 < y; num8 += 1U)
				{
					byte* ptr3 = a._out_ + num2 * num8;
					byte* ptr4 = a._out_ + num2 * num8 + (ulong)x * (ulong)((long)out_n) - num6;
					byte b = ((color == 0) ? StbImage.stbi__depth_scale_table[depth] : 1);
					if (depth == 4)
					{
						int i = (int)((ulong)x * (ulong)((long)img_n));
						while (i >= 2)
						{
							*(ptr3++) = (byte)((int)b * (*ptr4 >> 4));
							*(ptr3++) = b * (*ptr4 & 15);
							i -= 2;
							ptr4++;
						}
						if (i > 0)
						{
							*(ptr3++) = (byte)((int)b * (*ptr4 >> 4));
						}
					}
					else if (depth == 2)
					{
						int i = (int)((ulong)x * (ulong)((long)img_n));
						while (i >= 4)
						{
							*(ptr3++) = (byte)((int)b * (*ptr4 >> 6));
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 4) & 3));
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 2) & 3));
							*(ptr3++) = b * (*ptr4 & 3);
							i -= 4;
							ptr4++;
						}
						if (i > 0)
						{
							*(ptr3++) = (byte)((int)b * (*ptr4 >> 6));
						}
						if (i > 1)
						{
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 4) & 3));
						}
						if (i > 2)
						{
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 2) & 3));
						}
					}
					else if (depth == 1)
					{
						int i = (int)((ulong)x * (ulong)((long)img_n));
						while (i >= 8)
						{
							*(ptr3++) = (byte)((int)b * (*ptr4 >> 7));
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 6) & 1));
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 5) & 1));
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 4) & 1));
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 3) & 1));
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 2) & 1));
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 1) & 1));
							*(ptr3++) = b * (*ptr4 & 1);
							i -= 8;
							ptr4++;
						}
						if (i > 0)
						{
							*(ptr3++) = (byte)((int)b * (*ptr4 >> 7));
						}
						if (i > 1)
						{
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 6) & 1));
						}
						if (i > 2)
						{
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 5) & 1));
						}
						if (i > 3)
						{
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 4) & 1));
						}
						if (i > 4)
						{
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 3) & 1));
						}
						if (i > 5)
						{
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 2) & 1));
						}
						if (i > 6)
						{
							*(ptr3++) = (byte)((int)b * ((*ptr4 >> 1) & 1));
						}
					}
					if (img_n != out_n)
					{
						ptr3 = a._out_ + num2 * num8;
						if (img_n == 1)
						{
							for (int j = (int)(x - 1U); j >= 0; j--)
							{
								ptr3[j * 2 + 1] = byte.MaxValue;
								ptr3[j * 2] = ptr3[j];
							}
						}
						else
						{
							for (int j = (int)(x - 1U); j >= 0; j--)
							{
								ptr3[j * 4 + 3] = byte.MaxValue;
								ptr3[j * 4 + 2] = ptr3[j * 3 + 2];
								ptr3[j * 4 + 1] = ptr3[j * 3 + 1];
								ptr3[j * 4] = ptr3[j * 3];
							}
						}
					}
				}
			}
			else if (depth == 16)
			{
				byte* ptr5 = a._out_;
				ushort* ptr6 = (ushort*)ptr5;
				uint num11 = 0U;
				while ((ulong)num11 < (ulong)(x * y) * (ulong)((long)out_n))
				{
					*ptr6 = (ushort)(((int)(*ptr5) << 8) | (int)ptr5[1]);
					num11 += 1U;
					ptr6++;
					ptr5 += 2;
				}
			}
			return 1;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00009ED0 File Offset: 0x000080D0
		public unsafe static int stbi__create_png_image(StbImage.stbi__png a, byte* image_data, uint image_data_len, int out_n, int depth, int color, int interlaced)
		{
			int num = ((depth == 16) ? 2 : 1);
			int num2 = out_n * num;
			if (interlaced == 0)
			{
				return StbImage.stbi__create_png_image_raw(a, image_data, image_data_len, out_n, a.s.img_x, a.s.img_y, depth, color);
			}
			byte* ptr = (byte*)StbImage.stbi__malloc_mad3((int)a.s.img_x, (int)a.s.img_y, num2, 0);
			if (ptr == null)
			{
				return StbImage.stbi__err("outofmem");
			}
			for (int i = 0; i < 7; i++)
			{
				IntPtr intPtr = stackalloc byte[(UIntPtr)28];
				*intPtr = 0;
				*(intPtr + 4) = 4;
				*(intPtr + (IntPtr)2 * 4) = 0;
				*(intPtr + (IntPtr)3 * 4) = 2;
				*(intPtr + (IntPtr)4 * 4) = 0;
				*(intPtr + (IntPtr)5 * 4) = 1;
				*(intPtr + (IntPtr)6 * 4) = 0;
				int* ptr2 = intPtr;
				IntPtr intPtr2 = stackalloc byte[(UIntPtr)28];
				*intPtr2 = 0;
				*(intPtr2 + 4) = 0;
				*(intPtr2 + (IntPtr)2 * 4) = 4;
				*(intPtr2 + (IntPtr)3 * 4) = 0;
				*(intPtr2 + (IntPtr)4 * 4) = 2;
				*(intPtr2 + (IntPtr)5 * 4) = 0;
				*(intPtr2 + (IntPtr)6 * 4) = 1;
				int* ptr3 = intPtr2;
				IntPtr intPtr3 = stackalloc byte[(UIntPtr)28];
				*intPtr3 = 8;
				*(intPtr3 + 4) = 8;
				*(intPtr3 + (IntPtr)2 * 4) = 4;
				*(intPtr3 + (IntPtr)3 * 4) = 4;
				*(intPtr3 + (IntPtr)4 * 4) = 2;
				*(intPtr3 + (IntPtr)5 * 4) = 2;
				*(intPtr3 + (IntPtr)6 * 4) = 1;
				int* ptr4 = intPtr3;
				IntPtr intPtr4 = stackalloc byte[(UIntPtr)28];
				*intPtr4 = 8;
				*(intPtr4 + 4) = 8;
				*(intPtr4 + (IntPtr)2 * 4) = 8;
				*(intPtr4 + (IntPtr)3 * 4) = 4;
				*(intPtr4 + (IntPtr)4 * 4) = 4;
				*(intPtr4 + (IntPtr)5 * 4) = 2;
				*(intPtr4 + (IntPtr)6 * 4) = 2;
				int* ptr5 = intPtr4;
				int num3 = (int)(((ulong)a.s.img_x - (ulong)((long)ptr2[i]) + (ulong)((long)ptr4[i]) - 1UL) / (ulong)((long)ptr4[i]));
				int num4 = (int)(((ulong)a.s.img_y - (ulong)((long)ptr3[i]) + (ulong)((long)ptr5[i]) - 1UL) / (ulong)((long)ptr5[i]));
				if (num3 != 0 && num4 != 0)
				{
					uint num5 = (uint)(((a.s.img_n * num3 * depth + 7 >> 3) + 1) * num4);
					if (StbImage.stbi__create_png_image_raw(a, image_data, image_data_len, out_n, (uint)num3, (uint)num4, depth, color) == 0)
					{
						CRuntime.free((void*)ptr);
						return 0;
					}
					for (int j = 0; j < num4; j++)
					{
						for (int k = 0; k < num3; k++)
						{
							int num6 = j * ptr5[i] + ptr3[i];
							int num7 = k * ptr4[i] + ptr2[i];
							CRuntime.memcpy((void*)(ptr + (long)num6 * (long)((ulong)a.s.img_x) * (long)num2 + num7 * num2), (void*)(a._out_ + (j * num3 + k) * num2), (ulong)((long)num2));
						}
					}
					CRuntime.free((void*)a._out_);
					image_data += num5;
					image_data_len -= num5;
				}
			}
			a._out_ = ptr;
			return 1;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000A184 File Offset: 0x00008384
		public unsafe static int stbi__compute_transparency(StbImage.stbi__png z, byte* tc, int out_n)
		{
			StbImage.stbi__context s = z.s;
			uint num = s.img_x * s.img_y;
			byte* ptr = z._out_;
			if (out_n == 2)
			{
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					ptr[1] = ((*ptr == *tc) ? 0 : byte.MaxValue);
					ptr += 2;
				}
			}
			else
			{
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					if (*ptr == *tc && ptr[1] == tc[1] && ptr[2] == tc[2])
					{
						ptr[3] = 0;
					}
					ptr += 4;
				}
			}
			return 1;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000A208 File Offset: 0x00008408
		public unsafe static int stbi__compute_transparency16(StbImage.stbi__png z, ushort* tc, int out_n)
		{
			StbImage.stbi__context s = z.s;
			uint num = s.img_x * s.img_y;
			ushort* ptr = (ushort*)z._out_;
			if (out_n == 2)
			{
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					ptr[1] = ((*ptr == *tc) ? 0 : ushort.MaxValue);
					ptr += 2;
				}
			}
			else
			{
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					if (*ptr == *tc && ptr[1] == tc[1] && ptr[2] == tc[2])
					{
						ptr[3] = 0;
					}
					ptr += 4;
				}
			}
			return 1;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000A29C File Offset: 0x0000849C
		public unsafe static int stbi__expand_png_palette(StbImage.stbi__png a, byte* palette, int len, int pal_img_n)
		{
			uint num = a.s.img_x * a.s.img_y;
			byte* out_ = a._out_;
			byte* ptr = (byte*)StbImage.stbi__malloc_mad2((int)num, pal_img_n, 0);
			if (ptr == null)
			{
				return StbImage.stbi__err("outofmem");
			}
			byte* ptr2 = ptr;
			if (pal_img_n == 3)
			{
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					int num3 = (int)(out_[num2] * 4);
					*ptr = palette[num3];
					ptr[1] = palette[num3 + 1];
					ptr[2] = palette[num3 + 2];
					ptr += 3;
				}
			}
			else
			{
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					int num4 = (int)(out_[num2] * 4);
					*ptr = palette[num4];
					ptr[1] = palette[num4 + 1];
					ptr[2] = palette[num4 + 2];
					ptr[3] = palette[num4 + 3];
					ptr += 4;
				}
			}
			CRuntime.free((void*)a._out_);
			a._out_ = ptr2;
			return 1;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000A378 File Offset: 0x00008578
		public unsafe static void stbi__de_iphone(StbImage.stbi__png z)
		{
			StbImage.stbi__context s = z.s;
			uint num = s.img_x * s.img_y;
			byte* ptr = z._out_;
			if (s.img_out_n == 3)
			{
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					byte b = *ptr;
					*ptr = ptr[2];
					ptr[2] = b;
					ptr += 3;
				}
				return;
			}
			if (((StbImage.stbi__unpremultiply_on_load_set != 0) ? StbImage.stbi__unpremultiply_on_load_local : StbImage.stbi__unpremultiply_on_load_global) != 0)
			{
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					byte b2 = ptr[3];
					byte b3 = *ptr;
					if (b2 != 0)
					{
						byte b4 = b2 / 2;
						*ptr = (ptr[2] * byte.MaxValue + b4) / b2;
						ptr[1] = (ptr[1] * byte.MaxValue + b4) / b2;
						ptr[2] = (b3 * byte.MaxValue + b4) / b2;
					}
					else
					{
						*ptr = ptr[2];
						ptr[2] = b3;
					}
					ptr += 4;
				}
				return;
			}
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				byte b5 = *ptr;
				*ptr = ptr[2];
				ptr[2] = b5;
				ptr += 4;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000A474 File Offset: 0x00008674
		public unsafe static int stbi__parse_png_file(StbImage.stbi__png z, int scan, int req_comp)
		{
			byte* ptr = stackalloc byte[(UIntPtr)1024];
			byte b = 0;
			byte b2 = 0;
			IntPtr intPtr = stackalloc byte[(UIntPtr)3];
			initblk(intPtr, 0, 3);
			byte* ptr2 = intPtr;
			ushort* ptr3 = stackalloc ushort[(UIntPtr)6];
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			int num4 = 1;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			StbImage.stbi__context s = z.s;
			z.expanded = null;
			z.idata = null;
			z._out_ = null;
			if (StbImage.stbi__check_png_header(s) == 0)
			{
				return 0;
			}
			if (scan == 1)
			{
				return 1;
			}
			StbImage.stbi__pngchunk stbi__pngchunk;
			for (;;)
			{
				stbi__pngchunk = StbImage.stbi__get_chunk_header(s);
				uint type = stbi__pngchunk.type;
				if (type <= 1229278788U)
				{
					if (type != 1130840649U)
					{
						if (type != 1229209940U)
						{
							if (type != 1229278788U)
							{
								goto IL_0739;
							}
							goto IL_0583;
						}
						else
						{
							if (num4 != 0)
							{
								goto Block_50;
							}
							if (b != 0 && num3 == 0U)
							{
								goto Block_52;
							}
							if (scan == 2)
							{
								goto Block_53;
							}
							if (num + stbi__pngchunk.length < num)
							{
								return 0;
							}
							if (num + stbi__pngchunk.length > num2)
							{
								if (num2 == 0U)
								{
									num2 = ((stbi__pngchunk.length > 4096U) ? stbi__pngchunk.length : 4096U);
								}
								while (num + stbi__pngchunk.length > num2)
								{
									num2 *= 2U;
								}
								byte* ptr4 = (byte*)CRuntime.realloc((void*)z.idata, (ulong)num2);
								if (ptr4 == null)
								{
									goto Block_59;
								}
								z.idata = ptr4;
							}
							if (StbImage.stbi__getn(s, z.idata + num, (int)stbi__pngchunk.length) == 0)
							{
								goto Block_60;
							}
							num += stbi__pngchunk.length;
						}
					}
					else
					{
						num7 = 1;
						StbImage.stbi__skip(s, (int)stbi__pngchunk.length);
					}
				}
				else if (type != 1229472850U)
				{
					if (type != 1347179589U)
					{
						if (type != 1951551059U)
						{
							goto IL_0739;
						}
						if (num4 != 0)
						{
							goto Block_38;
						}
						if (z.idata != null)
						{
							goto Block_39;
						}
						if (b != 0)
						{
							if (scan == 2)
							{
								goto Block_41;
							}
							if (num3 == 0U)
							{
								goto Block_42;
							}
							if (stbi__pngchunk.length > num3)
							{
								goto Block_43;
							}
							b = 4;
							for (uint num8 = 0U; num8 < stbi__pngchunk.length; num8 += 1U)
							{
								ptr[num8 * 4U + 3U] = StbImage.stbi__get8(s);
							}
						}
						else
						{
							if ((s.img_n & 1) == 0)
							{
								goto Block_45;
							}
							if (stbi__pngchunk.length != (uint)(s.img_n * 2))
							{
								goto Block_46;
							}
							b2 = 1;
							if (z.depth == 16)
							{
								for (int i = 0; i < s.img_n; i++)
								{
									ptr3[i] = (ushort)StbImage.stbi__get16be(s);
								}
							}
							else
							{
								for (int i = 0; i < s.img_n; i++)
								{
									ptr2[i] = (byte)(StbImage.stbi__get16be(s) & 255) * StbImage.stbi__depth_scale_table[z.depth];
								}
							}
						}
					}
					else
					{
						if (num4 != 0)
						{
							goto Block_34;
						}
						if (stbi__pngchunk.length > 768U)
						{
							goto Block_35;
						}
						num3 = stbi__pngchunk.length / 3U;
						if (num3 * 3U != stbi__pngchunk.length)
						{
							goto Block_36;
						}
						for (uint num8 = 0U; num8 < num3; num8 += 1U)
						{
							ptr[num8 * 4U] = StbImage.stbi__get8(s);
							ptr[num8 * 4U + 1U] = StbImage.stbi__get8(s);
							ptr[num8 * 4U + 2U] = StbImage.stbi__get8(s);
							ptr[num8 * 4U + 3U] = byte.MaxValue;
						}
					}
				}
				else
				{
					if (num4 == 0)
					{
						break;
					}
					num4 = 0;
					if (stbi__pngchunk.length != 13U)
					{
						goto Block_11;
					}
					s.img_x = StbImage.stbi__get32be(s);
					s.img_y = StbImage.stbi__get32be(s);
					if (s.img_y > 16777216U)
					{
						goto Block_12;
					}
					if (s.img_x > 16777216U)
					{
						goto Block_13;
					}
					z.depth = (int)StbImage.stbi__get8(s);
					if (z.depth != 1 && z.depth != 2 && z.depth != 4 && z.depth != 8 && z.depth != 16)
					{
						goto Block_18;
					}
					num6 = (int)StbImage.stbi__get8(s);
					if (num6 > 6)
					{
						goto Block_19;
					}
					if (num6 == 3 && z.depth == 16)
					{
						goto Block_21;
					}
					if (num6 == 3)
					{
						b = 3;
					}
					else if ((num6 & 1) != 0)
					{
						goto Block_23;
					}
					if (StbImage.stbi__get8(s) != 0)
					{
						goto Block_24;
					}
					if (StbImage.stbi__get8(s) != 0)
					{
						goto Block_25;
					}
					num5 = (int)StbImage.stbi__get8(s);
					if (num5 > 1)
					{
						goto Block_26;
					}
					if (s.img_x == 0U || s.img_y == 0U)
					{
						goto IL_0242;
					}
					if (b == 0)
					{
						s.img_n = (((num6 & 2) != 0) ? 3 : 1) + (((num6 & 4) != 0) ? 1 : 0);
						if ((ulong)(1073741824U / s.img_x) / (ulong)((long)s.img_n) < (ulong)s.img_y)
						{
							goto Block_31;
						}
						if (scan == 2)
						{
							return 1;
						}
					}
					else
					{
						s.img_n = 1;
						if (1073741824U / s.img_x / 4U < s.img_y)
						{
							goto Block_33;
						}
					}
				}
				IL_07D1:
				StbImage.stbi__get32be(s);
				continue;
				IL_0739:
				if (num4 != 0)
				{
					goto Block_81;
				}
				if ((stbi__pngchunk.type & 536870912U) == 0U)
				{
					goto Block_82;
				}
				StbImage.stbi__skip(s, (int)stbi__pngchunk.length);
				goto IL_07D1;
			}
			return StbImage.stbi__err("multiple IHDR");
			Block_11:
			return StbImage.stbi__err("bad IHDR len");
			Block_12:
			return StbImage.stbi__err("too large");
			Block_13:
			return StbImage.stbi__err("too large");
			Block_18:
			return StbImage.stbi__err("1/2/4/8/16-bit only");
			Block_19:
			return StbImage.stbi__err("bad ctype");
			Block_21:
			return StbImage.stbi__err("bad ctype");
			Block_23:
			return StbImage.stbi__err("bad ctype");
			Block_24:
			return StbImage.stbi__err("bad comp method");
			Block_25:
			return StbImage.stbi__err("bad filter method");
			Block_26:
			return StbImage.stbi__err("bad interlace method");
			IL_0242:
			return StbImage.stbi__err("0-pixel image");
			Block_31:
			return StbImage.stbi__err("too large");
			Block_33:
			return StbImage.stbi__err("too large");
			Block_34:
			return StbImage.stbi__err("first not IHDR");
			Block_35:
			return StbImage.stbi__err("invalid PLTE");
			Block_36:
			return StbImage.stbi__err("invalid PLTE");
			Block_38:
			return StbImage.stbi__err("first not IHDR");
			Block_39:
			return StbImage.stbi__err("tRNS after IDAT");
			Block_41:
			s.img_n = 4;
			return 1;
			Block_42:
			return StbImage.stbi__err("tRNS before PLTE");
			Block_43:
			return StbImage.stbi__err("bad tRNS len");
			Block_45:
			return StbImage.stbi__err("tRNS with alpha");
			Block_46:
			return StbImage.stbi__err("bad tRNS len");
			Block_50:
			return StbImage.stbi__err("first not IHDR");
			Block_52:
			return StbImage.stbi__err("no PLTE");
			Block_53:
			s.img_n = (int)b;
			return 1;
			Block_59:
			return StbImage.stbi__err("outofmem");
			Block_60:
			return StbImage.stbi__err("outofdata");
			IL_0583:
			uint num9 = 0U;
			if (num4 != 0)
			{
				return StbImage.stbi__err("first not IHDR");
			}
			if (scan != 0)
			{
				return 1;
			}
			if (z.idata == null)
			{
				return StbImage.stbi__err("no IDAT");
			}
			num9 = (uint)((ulong)((uint)(((ulong)s.img_x * (ulong)((long)z.depth) + 7UL) / 8UL) * s.img_y) * (ulong)((long)s.img_n) + (ulong)s.img_y);
			z.expanded = (byte*)StbImage.stbi_zlib_decode_malloc_guesssize_headerflag((sbyte*)z.idata, (int)num, (int)num9, (int*)(&num9), (num7 == 0) ? 1 : 0);
			if (z.expanded == null)
			{
				return 0;
			}
			CRuntime.free((void*)z.idata);
			z.idata = null;
			if ((req_comp == s.img_n + 1 && req_comp != 3 && b == 0) || b2 != 0)
			{
				s.img_out_n = s.img_n + 1;
			}
			else
			{
				s.img_out_n = s.img_n;
			}
			if (StbImage.stbi__create_png_image(z, z.expanded, num9, s.img_out_n, z.depth, num6, num5) == 0)
			{
				return 0;
			}
			if (b2 != 0)
			{
				if (z.depth == 16)
				{
					if (StbImage.stbi__compute_transparency16(z, ptr3, s.img_out_n) == 0)
					{
						return 0;
					}
				}
				else if (StbImage.stbi__compute_transparency(z, ptr2, s.img_out_n) == 0)
				{
					return 0;
				}
			}
			if (num7 != 0 && ((StbImage.stbi__de_iphone_flag_set != 0) ? StbImage.stbi__de_iphone_flag_local : StbImage.stbi__de_iphone_flag_global) != 0 && s.img_out_n > 2)
			{
				StbImage.stbi__de_iphone(z);
			}
			if (b != 0)
			{
				s.img_n = (int)b;
				s.img_out_n = (int)b;
				if (req_comp >= 3)
				{
					s.img_out_n = req_comp;
				}
				if (StbImage.stbi__expand_png_palette(z, ptr, (int)num3, s.img_out_n) == 0)
				{
					return 0;
				}
			}
			else if (b2 != 0)
			{
				s.img_n++;
			}
			CRuntime.free((void*)z.expanded);
			z.expanded = null;
			StbImage.stbi__get32be(s);
			return 1;
			Block_81:
			return StbImage.stbi__err("first not IHDR");
			Block_82:
			StbImage.stbi__parse_png_file_invalid_chunk[0] = (char)((stbi__pngchunk.type >> 24) & 255U);
			StbImage.stbi__parse_png_file_invalid_chunk[1] = (char)((stbi__pngchunk.type >> 16) & 255U);
			StbImage.stbi__parse_png_file_invalid_chunk[2] = (char)((stbi__pngchunk.type >> 8) & 255U);
			StbImage.stbi__parse_png_file_invalid_chunk[3] = (char)(stbi__pngchunk.type & 255U);
			return StbImage.stbi__err(new string(StbImage.stbi__parse_png_file_invalid_chunk));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000AC60 File Offset: 0x00008E60
		public unsafe static void* stbi__do_png(StbImage.stbi__png p, int* x, int* y, int* n, int req_comp, StbImage.stbi__result_info* ri)
		{
			void* ptr = null;
			if (req_comp < 0 || req_comp > 4)
			{
				return (StbImage.stbi__err("bad req_comp") != 0) ? null : null;
			}
			if (StbImage.stbi__parse_png_file(p, 0, req_comp) != 0)
			{
				if (p.depth <= 8)
				{
					ri->bits_per_channel = 8;
				}
				else
				{
					if (p.depth != 16)
					{
						return (StbImage.stbi__err("bad bits_per_channel") != 0) ? null : null;
					}
					ri->bits_per_channel = 16;
				}
				ptr = (void*)p._out_;
				p._out_ = null;
				if (req_comp != 0 && req_comp != p.s.img_out_n)
				{
					if (ri->bits_per_channel == 8)
					{
						ptr = (void*)StbImage.stbi__convert_format((byte*)ptr, p.s.img_out_n, req_comp, p.s.img_x, p.s.img_y);
					}
					else
					{
						ptr = (void*)StbImage.stbi__convert_format16((ushort*)ptr, p.s.img_out_n, req_comp, p.s.img_x, p.s.img_y);
					}
					p.s.img_out_n = req_comp;
					if (ptr == null)
					{
						return ptr;
					}
				}
				*x = (int)p.s.img_x;
				*y = (int)p.s.img_y;
				if (n != null)
				{
					*n = p.s.img_n;
				}
			}
			CRuntime.free((void*)p._out_);
			p._out_ = null;
			CRuntime.free((void*)p.expanded);
			p.expanded = null;
			CRuntime.free((void*)p.idata);
			p.idata = null;
			return ptr;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public unsafe static int stbi__png_info_raw(StbImage.stbi__png p, int* x, int* y, int* comp)
		{
			if (StbImage.stbi__parse_png_file(p, 2, 0) == 0)
			{
				StbImage.stbi__rewind(p.s);
				return 0;
			}
			if (x != null)
			{
				*x = (int)p.s.img_x;
			}
			if (y != null)
			{
				*y = (int)p.s.img_y;
			}
			if (comp != null)
			{
				*comp = p.s.img_n;
			}
			return 1;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000AE33 File Offset: 0x00009033
		public static int stbi__psd_test(StbImage.stbi__context s)
		{
			int num = ((StbImage.stbi__get32be(s) == 943870035U) ? 1 : 0);
			StbImage.stbi__rewind(s);
			return num;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000AE4C File Offset: 0x0000904C
		public unsafe static void* stbi__psd_load(StbImage.stbi__context s, int* x, int* y, int* comp, int req_comp, StbImage.stbi__result_info* ri, int bpc)
		{
			if (StbImage.stbi__get32be(s) != 943870035U)
			{
				return (StbImage.stbi__err("not PSD") != 0) ? null : null;
			}
			if (StbImage.stbi__get16be(s) != 1)
			{
				return (StbImage.stbi__err("wrong version") != 0) ? null : null;
			}
			StbImage.stbi__skip(s, 6);
			int num = StbImage.stbi__get16be(s);
			if (num < 0 || num > 16)
			{
				return (StbImage.stbi__err("wrong channel count") != 0) ? null : null;
			}
			int num2 = (int)StbImage.stbi__get32be(s);
			int num3 = (int)StbImage.stbi__get32be(s);
			if (num2 > 16777216)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			if (num3 > 16777216)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			int num4 = StbImage.stbi__get16be(s);
			if (num4 != 8 && num4 != 16)
			{
				return (StbImage.stbi__err("unsupported bit depth") != 0) ? null : null;
			}
			if (StbImage.stbi__get16be(s) != 3)
			{
				return (StbImage.stbi__err("wrong color format") != 0) ? null : null;
			}
			StbImage.stbi__skip(s, (int)StbImage.stbi__get32be(s));
			StbImage.stbi__skip(s, (int)StbImage.stbi__get32be(s));
			StbImage.stbi__skip(s, (int)StbImage.stbi__get32be(s));
			int num5 = StbImage.stbi__get16be(s);
			if (num5 > 1)
			{
				return (StbImage.stbi__err("bad compression") != 0) ? null : null;
			}
			if (StbImage.stbi__mad3sizes_valid(4, num3, num2, 0) == 0)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			byte* ptr;
			if (num5 == 0 && num4 == 16 && bpc == 16)
			{
				ptr = (byte*)StbImage.stbi__malloc_mad3(8, num3, num2, 0);
				ri->bits_per_channel = 16;
			}
			else
			{
				ptr = (byte*)StbImage.stbi__malloc((ulong)((long)(4 * num3 * num2)));
			}
			if (ptr == null)
			{
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			int num6 = num3 * num2;
			if (num5 != 0)
			{
				StbImage.stbi__skip(s, num2 * num * 2);
				for (int i = 0; i < 4; i++)
				{
					byte* ptr2 = ptr + i;
					if (i >= num)
					{
						int j = 0;
						while (j < num6)
						{
							*ptr2 = ((i == 3) ? byte.MaxValue : 0);
							j++;
							ptr2 += 4;
						}
					}
					else if (StbImage.stbi__psd_decode_rle(s, ptr2, num6) == 0)
					{
						CRuntime.free((void*)ptr);
						return (StbImage.stbi__err("corrupt") != 0) ? null : null;
					}
				}
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					if (i >= num)
					{
						if (num4 == 16 && bpc == 16)
						{
							ushort* ptr3 = (ushort*)(ptr + (IntPtr)i * 2);
							ushort num7 = ((i == 3) ? ushort.MaxValue : 0);
							int j = 0;
							while (j < num6)
							{
								*ptr3 = num7;
								j++;
								ptr3 += 4;
							}
						}
						else
						{
							byte* ptr4 = ptr + i;
							byte b = ((i == 3) ? byte.MaxValue : 0);
							int j = 0;
							while (j < num6)
							{
								*ptr4 = b;
								j++;
								ptr4 += 4;
							}
						}
					}
					else if (ri->bits_per_channel == 16)
					{
						ushort* ptr5 = (ushort*)(ptr + (IntPtr)i * 2);
						int j = 0;
						while (j < num6)
						{
							*ptr5 = (ushort)StbImage.stbi__get16be(s);
							j++;
							ptr5 += 4;
						}
					}
					else
					{
						byte* ptr6 = ptr + i;
						if (num4 == 16)
						{
							int j = 0;
							while (j < num6)
							{
								*ptr6 = (byte)(StbImage.stbi__get16be(s) >> 8);
								j++;
								ptr6 += 4;
							}
						}
						else
						{
							int j = 0;
							while (j < num6)
							{
								*ptr6 = StbImage.stbi__get8(s);
								j++;
								ptr6 += 4;
							}
						}
					}
				}
			}
			if (num >= 4)
			{
				if (ri->bits_per_channel == 16)
				{
					for (int j = 0; j < num3 * num2; j++)
					{
						ushort* ptr7 = (ushort*)(ptr + (IntPtr)(4 * j) * 2);
						if (ptr7[3] != 0 && ptr7[3] != 65535)
						{
							float num8 = (float)ptr7[3] / 65535f;
							float num9 = 1f / num8;
							float num10 = 65535f * (1f - num9);
							*ptr7 = (ushort)((float)(*ptr7) * num9 + num10);
							ptr7[1] = (ushort)((float)ptr7[1] * num9 + num10);
							ptr7[2] = (ushort)((float)ptr7[2] * num9 + num10);
						}
					}
				}
				else
				{
					for (int j = 0; j < num3 * num2; j++)
					{
						byte* ptr8 = ptr + 4 * j;
						if (ptr8[3] != 0 && ptr8[3] != 255)
						{
							float num11 = (float)ptr8[3] / 255f;
							float num12 = 1f / num11;
							float num13 = 255f * (1f - num12);
							*ptr8 = (byte)((float)(*ptr8) * num12 + num13);
							ptr8[1] = (byte)((float)ptr8[1] * num12 + num13);
							ptr8[2] = (byte)((float)ptr8[2] * num12 + num13);
						}
					}
				}
			}
			if (req_comp != 0 && req_comp != 4)
			{
				if (ri->bits_per_channel == 16)
				{
					ptr = (byte*)StbImage.stbi__convert_format16((ushort*)ptr, 4, req_comp, (uint)num3, (uint)num2);
				}
				else
				{
					ptr = StbImage.stbi__convert_format(ptr, 4, req_comp, (uint)num3, (uint)num2);
				}
				if (ptr == null)
				{
					return (void*)ptr;
				}
			}
			if (comp != null)
			{
				*comp = 4;
			}
			*y = num2;
			*x = num3;
			return (void*)ptr;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000B340 File Offset: 0x00009540
		public unsafe static int stbi__psd_info(StbImage.stbi__context s, int* x, int* y, int* comp)
		{
			int num = 0;
			if (x == null)
			{
				x = &num;
			}
			if (y == null)
			{
				y = &num;
			}
			if (comp == null)
			{
				comp = &num;
			}
			if (StbImage.stbi__get32be(s) != 943870035U)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			if (StbImage.stbi__get16be(s) != 1)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			StbImage.stbi__skip(s, 6);
			int num2 = StbImage.stbi__get16be(s);
			if (num2 < 0 || num2 > 16)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			*y = (int)StbImage.stbi__get32be(s);
			*x = (int)StbImage.stbi__get32be(s);
			int num3 = StbImage.stbi__get16be(s);
			if (num3 != 8 && num3 != 16)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			if (StbImage.stbi__get16be(s) != 3)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			*comp = 4;
			return 1;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000B3F4 File Offset: 0x000095F4
		public static int stbi__psd_is16(StbImage.stbi__context s)
		{
			if (StbImage.stbi__get32be(s) != 943870035U)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			if (StbImage.stbi__get16be(s) != 1)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			StbImage.stbi__skip(s, 6);
			int num = StbImage.stbi__get16be(s);
			if (num < 0 || num > 16)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			if (StbImage.stbi__get16be(s) != 16)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			return 1;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000B45C File Offset: 0x0000965C
		public unsafe static int stbi__psd_decode_rle(StbImage.stbi__context s, byte* p, int pixelCount)
		{
			int num = 0;
			int num2;
			while ((num2 = pixelCount - num) > 0)
			{
				int num3 = (int)StbImage.stbi__get8(s);
				if (num3 != 128)
				{
					if (num3 < 128)
					{
						num3++;
						if (num3 > num2)
						{
							return 0;
						}
						num += num3;
						while (num3 != 0)
						{
							*p = StbImage.stbi__get8(s);
							p += 4;
							num3--;
						}
					}
					else if (num3 > 128)
					{
						num3 = 257 - num3;
						if (num3 > num2)
						{
							return 0;
						}
						byte b = StbImage.stbi__get8(s);
						num += num3;
						while (num3 != 0)
						{
							*p = b;
							p += 4;
							num3--;
						}
					}
				}
			}
			return 1;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000B4F0 File Offset: 0x000096F0
		public static int stbi__tga_test(StbImage.stbi__context s)
		{
			int num = 0;
			StbImage.stbi__get8(s);
			int num2 = (int)StbImage.stbi__get8(s);
			if (num2 <= 1)
			{
				int num3 = (int)StbImage.stbi__get8(s);
				if (num2 == 1)
				{
					if (num3 != 1 && num3 != 9)
					{
						goto IL_00BB;
					}
					StbImage.stbi__skip(s, 4);
					num3 = (int)StbImage.stbi__get8(s);
					if (num3 != 8 && num3 != 15 && num3 != 16 && num3 != 24 && num3 != 32)
					{
						goto IL_00BB;
					}
					StbImage.stbi__skip(s, 4);
				}
				else
				{
					if (num3 != 2 && num3 != 3 && num3 != 10 && num3 != 11)
					{
						goto IL_00BB;
					}
					StbImage.stbi__skip(s, 9);
				}
				if (StbImage.stbi__get16le(s) >= 1 && StbImage.stbi__get16le(s) >= 1)
				{
					num3 = (int)StbImage.stbi__get8(s);
					if ((num2 != 1 || num3 == 8 || num3 == 16) && (num3 == 8 || num3 == 15 || num3 == 16 || num3 == 24 || num3 == 32))
					{
						num = 1;
					}
				}
			}
			IL_00BB:
			StbImage.stbi__rewind(s);
			return num;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000B5C0 File Offset: 0x000097C0
		public unsafe static void* stbi__tga_load(StbImage.stbi__context s, int* x, int* y, int* comp, int req_comp, StbImage.stbi__result_info* ri)
		{
			int num = (int)StbImage.stbi__get8(s);
			int num2 = (int)StbImage.stbi__get8(s);
			int num3 = (int)StbImage.stbi__get8(s);
			int num4 = 0;
			int num5 = StbImage.stbi__get16le(s);
			int num6 = StbImage.stbi__get16le(s);
			int num7 = (int)StbImage.stbi__get8(s);
			StbImage.stbi__get16le(s);
			StbImage.stbi__get16le(s);
			int num8 = StbImage.stbi__get16le(s);
			int num9 = StbImage.stbi__get16le(s);
			int num10 = (int)StbImage.stbi__get8(s);
			int num11 = 0;
			int num12 = (int)StbImage.stbi__get8(s);
			byte* ptr = null;
			IntPtr intPtr = stackalloc byte[(UIntPtr)4];
			initblk(intPtr, 0, 4);
			byte* ptr2 = intPtr;
			int num13 = 0;
			int num14 = 0;
			int num15 = 1;
			if (num9 > 16777216)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			if (num8 > 16777216)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			if (num3 >= 8)
			{
				num3 -= 8;
				num4 = 1;
			}
			num12 = 1 - ((num12 >> 5) & 1);
			int num16;
			if (num2 != 0)
			{
				num16 = StbImage.stbi__tga_get_comp(num7, 0, &num11);
			}
			else
			{
				num16 = StbImage.stbi__tga_get_comp(num10, (num3 == 3) ? 1 : 0, &num11);
			}
			if (num16 == 0)
			{
				return (StbImage.stbi__err("bad format") != 0) ? null : null;
			}
			*x = num8;
			*y = num9;
			if (comp != null)
			{
				*comp = num16;
			}
			if (StbImage.stbi__mad3sizes_valid(num8, num9, num16, 0) == 0)
			{
				return (StbImage.stbi__err("too large") != 0) ? null : null;
			}
			byte* ptr3 = (byte*)StbImage.stbi__malloc_mad3(num8, num9, num16, 0);
			if (ptr3 == null)
			{
				return (StbImage.stbi__err("outofmem") != 0) ? null : null;
			}
			StbImage.stbi__skip(s, num);
			if (num2 == 0 && num4 == 0 && num11 == 0)
			{
				for (int i = 0; i < num9; i++)
				{
					int num17 = ((num12 != 0) ? (num9 - i - 1) : i);
					byte* ptr4 = ptr3 + num17 * num8 * num16;
					StbImage.stbi__getn(s, ptr4, num8 * num16);
				}
			}
			else
			{
				if (num2 != 0)
				{
					if (num6 == 0)
					{
						CRuntime.free((void*)ptr3);
						return (StbImage.stbi__err("bad palette") != 0) ? null : null;
					}
					StbImage.stbi__skip(s, num5);
					ptr = (byte*)StbImage.stbi__malloc_mad2(num6, num16, 0);
					if (ptr == null)
					{
						CRuntime.free((void*)ptr3);
						return (StbImage.stbi__err("outofmem") != 0) ? null : null;
					}
					if (num11 != 0)
					{
						byte* ptr5 = ptr;
						for (int i = 0; i < num6; i++)
						{
							StbImage.stbi__tga_read_rgb16(s, ptr5);
							ptr5 += num16;
						}
					}
					else if (StbImage.stbi__getn(s, ptr, num6 * num16) == 0)
					{
						CRuntime.free((void*)ptr3);
						CRuntime.free((void*)ptr);
						return (StbImage.stbi__err("bad palette") != 0) ? null : null;
					}
				}
				for (int i = 0; i < num8 * num9; i++)
				{
					if (num4 != 0)
					{
						if (num13 == 0)
						{
							int num18 = (int)StbImage.stbi__get8(s);
							num13 = 1 + (num18 & 127);
							num14 = num18 >> 7;
							num15 = 1;
						}
						else if (num14 == 0)
						{
							num15 = 1;
						}
					}
					else
					{
						num15 = 1;
					}
					if (num15 != 0)
					{
						if (num2 != 0)
						{
							int num19 = ((num10 == 8) ? ((int)StbImage.stbi__get8(s)) : StbImage.stbi__get16le(s));
							if (num19 >= num6)
							{
								num19 = 0;
							}
							num19 *= num16;
							for (int j = 0; j < num16; j++)
							{
								ptr2[j] = ptr[num19 + j];
							}
						}
						else if (num11 != 0)
						{
							StbImage.stbi__tga_read_rgb16(s, ptr2);
						}
						else
						{
							for (int j = 0; j < num16; j++)
							{
								ptr2[j] = StbImage.stbi__get8(s);
							}
						}
						num15 = 0;
					}
					for (int j = 0; j < num16; j++)
					{
						ptr3[i * num16 + j] = ptr2[j];
					}
					num13--;
				}
				if (num12 != 0)
				{
					int j = 0;
					while (j * 2 < num9)
					{
						int num20 = j * num8 * num16;
						int num21 = (num9 - 1 - j) * num8 * num16;
						for (int i = num8 * num16; i > 0; i--)
						{
							byte b = ptr3[num20];
							ptr3[num20] = ptr3[num21];
							ptr3[num21] = b;
							num20++;
							num21++;
						}
						j++;
					}
				}
				if (ptr != null)
				{
					CRuntime.free((void*)ptr);
				}
			}
			if (num16 >= 3 && num11 == 0)
			{
				byte* ptr6 = ptr3;
				for (int i = 0; i < num8 * num9; i++)
				{
					byte b2 = *ptr6;
					*ptr6 = ptr6[2];
					ptr6[2] = b2;
					ptr6 += num16;
				}
			}
			if (req_comp != 0 && req_comp != num16)
			{
				ptr3 = StbImage.stbi__convert_format(ptr3, num16, req_comp, (uint)num8, (uint)num9);
			}
			return (void*)ptr3;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000BA00 File Offset: 0x00009C00
		public unsafe static int stbi__tga_info(StbImage.stbi__context s, int* x, int* y, int* comp)
		{
			StbImage.stbi__get8(s);
			int num = (int)StbImage.stbi__get8(s);
			if (num > 1)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			int num2 = (int)StbImage.stbi__get8(s);
			int num4;
			if (num == 1)
			{
				if (num2 != 1 && num2 != 9)
				{
					StbImage.stbi__rewind(s);
					return 0;
				}
				StbImage.stbi__skip(s, 4);
				int num3 = (int)StbImage.stbi__get8(s);
				if (num3 != 8 && num3 != 15 && num3 != 16 && num3 != 24 && num3 != 32)
				{
					StbImage.stbi__rewind(s);
					return 0;
				}
				StbImage.stbi__skip(s, 4);
				num4 = num3;
			}
			else
			{
				if (num2 != 2 && num2 != 3 && num2 != 10 && num2 != 11)
				{
					StbImage.stbi__rewind(s);
					return 0;
				}
				StbImage.stbi__skip(s, 9);
				num4 = 0;
			}
			int num5 = StbImage.stbi__get16le(s);
			if (num5 < 1)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			int num6 = StbImage.stbi__get16le(s);
			if (num6 < 1)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			int num7 = (int)StbImage.stbi__get8(s);
			StbImage.stbi__get8(s);
			int num8;
			if (num4 != 0)
			{
				if (num7 != 8 && num7 != 16)
				{
					StbImage.stbi__rewind(s);
					return 0;
				}
				num8 = StbImage.stbi__tga_get_comp(num4, 0, null);
			}
			else
			{
				num8 = StbImage.stbi__tga_get_comp(num7, (num2 == 3 || num2 == 11) ? 1 : 0, null);
			}
			if (num8 == 0)
			{
				StbImage.stbi__rewind(s);
				return 0;
			}
			if (x != null)
			{
				*x = num5;
			}
			if (y != null)
			{
				*y = num6;
			}
			if (comp != null)
			{
				*comp = num8;
			}
			return 1;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000BB54 File Offset: 0x00009D54
		public unsafe static int stbi__tga_get_comp(int bits_per_pixel, int is_grey, int* is_rgb16)
		{
			if (is_rgb16 != null)
			{
				*is_rgb16 = 0;
			}
			if (bits_per_pixel <= 16)
			{
				if (bits_per_pixel == 8)
				{
					return 1;
				}
				if (bits_per_pixel - 15 <= 1)
				{
					if (bits_per_pixel == 16 && is_grey != 0)
					{
						return 2;
					}
					if (is_rgb16 != null)
					{
						*is_rgb16 = 1;
					}
					return 3;
				}
			}
			else if (bits_per_pixel == 24 || bits_per_pixel == 32)
			{
				return bits_per_pixel / 8;
			}
			return 0;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		public unsafe static void stbi__tga_read_rgb16(StbImage.stbi__context s, byte* _out_)
		{
			ushort num = (ushort)StbImage.stbi__get16le(s);
			ushort num2 = 31;
			int num3 = (num >> 10) & (int)num2;
			int num4 = (num >> 5) & (int)num2;
			int num5 = (int)(num & num2);
			*_out_ = (byte)(num3 * 255 / 31);
			_out_[1] = (byte)(num4 * 255 / 31);
			_out_[2] = (byte)(num5 * 255 / 31);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		public unsafe static sbyte* stbi_zlib_decode_malloc_guesssize(sbyte* buffer, int len, int initial_size, int* outlen)
		{
			StbImage.stbi__zbuf stbi__zbuf = default(StbImage.stbi__zbuf);
			sbyte* ptr = (sbyte*)StbImage.stbi__malloc((ulong)((long)initial_size));
			if (ptr == null)
			{
				return null;
			}
			stbi__zbuf.zbuffer = (byte*)buffer;
			stbi__zbuf.zbuffer_end = (byte*)(buffer + len);
			if (StbImage.stbi__do_zlib(&stbi__zbuf, ptr, initial_size, 1, 1) != 0)
			{
				if (outlen != null)
				{
					*outlen = (int)((long)(stbi__zbuf.zout - stbi__zbuf.zout_start));
				}
				return stbi__zbuf.zout_start;
			}
			CRuntime.free((void*)stbi__zbuf.zout_start);
			return null;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000BC6C File Offset: 0x00009E6C
		public unsafe static sbyte* stbi_zlib_decode_malloc_guesssize_headerflag(sbyte* buffer, int len, int initial_size, int* outlen, int parse_header)
		{
			StbImage.stbi__zbuf stbi__zbuf = default(StbImage.stbi__zbuf);
			sbyte* ptr = (sbyte*)StbImage.stbi__malloc((ulong)((long)initial_size));
			if (ptr == null)
			{
				return null;
			}
			stbi__zbuf.zbuffer = (byte*)buffer;
			stbi__zbuf.zbuffer_end = (byte*)(buffer + len);
			if (StbImage.stbi__do_zlib(&stbi__zbuf, ptr, initial_size, 1, parse_header) != 0)
			{
				if (outlen != null)
				{
					*outlen = (int)((long)(stbi__zbuf.zout - stbi__zbuf.zout_start));
				}
				return stbi__zbuf.zout_start;
			}
			CRuntime.free((void*)stbi__zbuf.zout_start);
			return null;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000BCDE File Offset: 0x00009EDE
		public unsafe static sbyte* stbi_zlib_decode_malloc(sbyte* buffer, int len, int* outlen)
		{
			return StbImage.stbi_zlib_decode_malloc_guesssize(buffer, len, 16384, outlen);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		public unsafe static int stbi_zlib_decode_buffer(sbyte* obuffer, int olen, sbyte* ibuffer, int ilen)
		{
			StbImage.stbi__zbuf stbi__zbuf = default(StbImage.stbi__zbuf);
			stbi__zbuf.zbuffer = (byte*)ibuffer;
			stbi__zbuf.zbuffer_end = (byte*)(ibuffer + ilen);
			if (StbImage.stbi__do_zlib(&stbi__zbuf, obuffer, olen, 0, 1) != 0)
			{
				return (int)((long)(stbi__zbuf.zout - stbi__zbuf.zout_start));
			}
			return -1;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000BD38 File Offset: 0x00009F38
		public unsafe static sbyte* stbi_zlib_decode_noheader_malloc(sbyte* buffer, int len, int* outlen)
		{
			StbImage.stbi__zbuf stbi__zbuf = default(StbImage.stbi__zbuf);
			sbyte* ptr = (sbyte*)StbImage.stbi__malloc(16384UL);
			if (ptr == null)
			{
				return null;
			}
			stbi__zbuf.zbuffer = (byte*)buffer;
			stbi__zbuf.zbuffer_end = (byte*)(buffer + len);
			if (StbImage.stbi__do_zlib(&stbi__zbuf, ptr, 16384, 1, 0) != 0)
			{
				if (outlen != null)
				{
					*outlen = (int)((long)(stbi__zbuf.zout - stbi__zbuf.zout_start));
				}
				return stbi__zbuf.zout_start;
			}
			CRuntime.free((void*)stbi__zbuf.zout_start);
			return null;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000BDB4 File Offset: 0x00009FB4
		public unsafe static int stbi_zlib_decode_noheader_buffer(sbyte* obuffer, int olen, sbyte* ibuffer, int ilen)
		{
			StbImage.stbi__zbuf stbi__zbuf = default(StbImage.stbi__zbuf);
			stbi__zbuf.zbuffer = (byte*)ibuffer;
			stbi__zbuf.zbuffer_end = (byte*)(ibuffer + ilen);
			if (StbImage.stbi__do_zlib(&stbi__zbuf, obuffer, olen, 0, 0) != 0)
			{
				return (int)((long)(stbi__zbuf.zout - stbi__zbuf.zout_start));
			}
			return -1;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000BDFC File Offset: 0x00009FFC
		public unsafe static int stbi__zbuild_huffman(StbImage.stbi__zhuffman* z, byte* sizelist, int num)
		{
			int num2 = 0;
			int* ptr = stackalloc int[(UIntPtr)64];
			int* ptr2 = stackalloc int[(UIntPtr)68];
			CRuntime.memset((void*)ptr2, 0, 68UL);
			CRuntime.memset((void*)(&z->fast.FixedElementField), 0, 1024UL);
			for (int i = 0; i < num; i++)
			{
				ptr2[sizelist[i]]++;
			}
			*ptr2 = 0;
			for (int i = 1; i < 16; i++)
			{
				if (ptr2[i] > 1 << i)
				{
					return StbImage.stbi__err("bad sizes");
				}
			}
			int num3 = 0;
			for (int i = 1; i < 16; i++)
			{
				ptr[i] = num3;
				*((ref z->firstcode.FixedElementField) + (IntPtr)i * 2) = (ushort)num3;
				*((ref z->firstsymbol.FixedElementField) + (IntPtr)i * 2) = (ushort)num2;
				num3 += ptr2[i];
				if (ptr2[i] != 0 && num3 - 1 >= 1 << i)
				{
					return StbImage.stbi__err("bad codelengths");
				}
				*((ref z->maxcode.FixedElementField) + (IntPtr)i * 4) = num3 << 16 - i;
				num3 <<= 1;
				num2 += ptr2[i];
			}
			*((ref z->maxcode.FixedElementField) + (IntPtr)16 * 4) = 65536;
			for (int i = 0; i < num; i++)
			{
				int num4 = (int)sizelist[i];
				if (num4 != 0)
				{
					int num5 = ptr[num4] - (int)(*((ref z->firstcode.FixedElementField) + (IntPtr)num4 * 2)) + (int)(*((ref z->firstsymbol.FixedElementField) + (IntPtr)num4 * 2));
					ushort num6 = (ushort)((num4 << 9) | i);
					*((ref z->size.FixedElementField) + num5) = (byte)num4;
					*((ref z->value.FixedElementField) + (IntPtr)num5 * 2) = (ushort)i;
					if (num4 <= 9)
					{
						for (int j = StbImage.stbi__bit_reverse(ptr[num4], num4); j < 512; j += 1 << num4)
						{
							*((ref z->fast.FixedElementField) + (IntPtr)j * 2) = num6;
						}
					}
					ptr[num4]++;
				}
			}
			return 1;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000C002 File Offset: 0x0000A202
		public unsafe static int stbi__zeof(StbImage.stbi__zbuf* z)
		{
			if (z->zbuffer < z->zbuffer_end)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000C018 File Offset: 0x0000A218
		public unsafe static byte stbi__zget8(StbImage.stbi__zbuf* z)
		{
			byte b;
			if (StbImage.stbi__zeof(z) == 0)
			{
				byte* zbuffer = z->zbuffer;
				z->zbuffer = zbuffer + 1;
				b = *zbuffer;
			}
			else
			{
				b = 0;
			}
			return b;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000C040 File Offset: 0x0000A240
		public unsafe static void stbi__fill_bits(StbImage.stbi__zbuf* z)
		{
			while (z->code_buffer < 1U << z->num_bits)
			{
				z->code_buffer = z->code_buffer | (uint)((uint)StbImage.stbi__zget8(z) << z->num_bits);
				z->num_bits = z->num_bits + 8;
				if (z->num_bits > 24)
				{
					return;
				}
			}
			z->zbuffer = z->zbuffer_end;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000C09C File Offset: 0x0000A29C
		public unsafe static uint stbi__zreceive(StbImage.stbi__zbuf* z, int n)
		{
			if (z->num_bits < n)
			{
				StbImage.stbi__fill_bits(z);
			}
			uint num = (uint)((ulong)z->code_buffer & (ulong)((long)((1 << n) - 1)));
			z->code_buffer = z->code_buffer >> n;
			z->num_bits = z->num_bits - n;
			return num;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000C0D8 File Offset: 0x0000A2D8
		public unsafe static int stbi__zhuffman_decode_slowpath(StbImage.stbi__zbuf* a, StbImage.stbi__zhuffman* z)
		{
			int i = StbImage.stbi__bit_reverse((int)a->code_buffer, 16);
			int num = 10;
			while (i >= *((ref z->maxcode.FixedElementField) + (IntPtr)num * 4))
			{
				num++;
			}
			if (num >= 16)
			{
				return -1;
			}
			int num2 = (i >> 16 - num) - (int)(*((ref z->firstcode.FixedElementField) + (IntPtr)num * 2)) + (int)(*((ref z->firstsymbol.FixedElementField) + (IntPtr)num * 2));
			if (num2 >= 288)
			{
				return -1;
			}
			if ((int)(*((ref z->size.FixedElementField) + num2)) != num)
			{
				return -1;
			}
			a->code_buffer = a->code_buffer >> num;
			a->num_bits = a->num_bits - num;
			return (int)(*((ref z->value.FixedElementField) + (IntPtr)num2 * 2));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000C194 File Offset: 0x0000A394
		public unsafe static int stbi__zhuffman_decode(StbImage.stbi__zbuf* a, StbImage.stbi__zhuffman* z)
		{
			if (a->num_bits < 16)
			{
				if (StbImage.stbi__zeof(a) != 0)
				{
					return -1;
				}
				StbImage.stbi__fill_bits(a);
			}
			int num = (int)(*((ref z->fast.FixedElementField) + (IntPtr)((ulong)(a->code_buffer & 511U) * 2UL)));
			if (num != 0)
			{
				int num2 = num >> 9;
				a->code_buffer = a->code_buffer >> num2;
				a->num_bits = a->num_bits - num2;
				return num & 511;
			}
			return StbImage.stbi__zhuffman_decode_slowpath(a, z);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000C210 File Offset: 0x0000A410
		public unsafe static int stbi__zexpand(StbImage.stbi__zbuf* z, sbyte* zout, int n)
		{
			z->zout = zout;
			if (z->z_expandable == 0)
			{
				return StbImage.stbi__err("output buffer limit");
			}
			uint num = (uint)((long)(z->zout - z->zout_start));
			uint num2 = (uint)((long)(z->zout_end - z->zout_start));
			if (4294967295U - num < (uint)n)
			{
				return StbImage.stbi__err("outofmem");
			}
			while ((ulong)num + (ulong)((long)n) > (ulong)num2)
			{
				if (num2 > 2147483647U)
				{
					return StbImage.stbi__err("outofmem");
				}
				num2 *= 2U;
			}
			sbyte* ptr = (sbyte*)CRuntime.realloc((void*)z->zout_start, (ulong)num2);
			if (ptr == null)
			{
				return StbImage.stbi__err("outofmem");
			}
			z->zout_start = ptr;
			z->zout = ptr + num;
			z->zout_end = ptr + num2;
			return 1;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000C2CC File Offset: 0x0000A4CC
		public unsafe static int stbi__parse_huffman_block(StbImage.stbi__zbuf* a)
		{
			sbyte* ptr = a->zout;
			for (;;)
			{
				int num = StbImage.stbi__zhuffman_decode(a, &a->z_length);
				if (num < 256)
				{
					if (num < 0)
					{
						break;
					}
					if (ptr >= a->zout_end)
					{
						if (StbImage.stbi__zexpand(a, ptr, 1) == 0)
						{
							return 0;
						}
						ptr = a->zout;
					}
					*(ptr++) = (sbyte)num;
				}
				else
				{
					if (num == 256)
					{
						goto Block_5;
					}
					num -= 257;
					int num2 = StbImage.stbi__zlength_base[num];
					if (StbImage.stbi__zlength_extra[num] != 0)
					{
						num2 += (int)StbImage.stbi__zreceive(a, StbImage.stbi__zlength_extra[num]);
					}
					num = StbImage.stbi__zhuffman_decode(a, &a->z_distance);
					if (num < 0)
					{
						goto Block_7;
					}
					int num3 = StbImage.stbi__zdist_base[num];
					if (StbImage.stbi__zdist_extra[num] != 0)
					{
						num3 += (int)StbImage.stbi__zreceive(a, StbImage.stbi__zdist_extra[num]);
					}
					if ((long)(ptr - a->zout_start) < (long)num3)
					{
						goto Block_9;
					}
					if (ptr + num2 != a->zout_end)
					{
						if (StbImage.stbi__zexpand(a, ptr, num2) == 0)
						{
							return 0;
						}
						ptr = a->zout;
					}
					byte* ptr2 = (byte*)(ptr - num3);
					if (num3 == 1)
					{
						byte b = *ptr2;
						if (num2 != 0)
						{
							do
							{
								*(ptr++) = (sbyte)b;
							}
							while (--num2 != 0);
						}
					}
					else if (num2 != 0)
					{
						do
						{
							*(ptr++) = (sbyte)(*(ptr2++));
						}
						while (--num2 != 0);
					}
				}
			}
			return StbImage.stbi__err("bad huffman code");
			Block_5:
			a->zout = ptr;
			return 1;
			Block_7:
			return StbImage.stbi__err("bad huffman code");
			Block_9:
			return StbImage.stbi__err("bad dist");
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000C42C File Offset: 0x0000A62C
		public unsafe static int stbi__compute_huffman_codes(StbImage.stbi__zbuf* a)
		{
			StbImage.stbi__zhuffman stbi__zhuffman = default(StbImage.stbi__zhuffman);
			byte* ptr = stackalloc byte[(UIntPtr)455];
			byte* ptr2 = stackalloc byte[(UIntPtr)19];
			int num = (int)(StbImage.stbi__zreceive(a, 5) + 257U);
			int num2 = (int)(StbImage.stbi__zreceive(a, 5) + 1U);
			int num3 = (int)(StbImage.stbi__zreceive(a, 4) + 4U);
			int num4 = num + num2;
			CRuntime.memset((void*)ptr2, 0, 19UL);
			for (int i = 0; i < num3; i++)
			{
				int num5 = (int)StbImage.stbi__zreceive(a, 3);
				ptr2[StbImage.stbi__compute_huffman_codes_length_dezigzag[i]] = (byte)num5;
			}
			if (StbImage.stbi__zbuild_huffman(&stbi__zhuffman, ptr2, 19) == 0)
			{
				return 0;
			}
			int j = 0;
			while (j < num4)
			{
				int num6 = StbImage.stbi__zhuffman_decode(a, &stbi__zhuffman);
				if (num6 < 0 || num6 >= 19)
				{
					return StbImage.stbi__err("bad codelengths");
				}
				if (num6 < 16)
				{
					ptr[j++] = (byte)num6;
				}
				else
				{
					byte b = 0;
					if (num6 == 16)
					{
						num6 = (int)(StbImage.stbi__zreceive(a, 2) + 3U);
						if (j == 0)
						{
							return StbImage.stbi__err("bad codelengths");
						}
						b = ptr[j - 1];
					}
					else if (num6 == 17)
					{
						num6 = (int)(StbImage.stbi__zreceive(a, 3) + 3U);
					}
					else
					{
						if (num6 != 18)
						{
							return StbImage.stbi__err("bad codelengths");
						}
						num6 = (int)(StbImage.stbi__zreceive(a, 7) + 11U);
					}
					if (num4 - j < num6)
					{
						return StbImage.stbi__err("bad codelengths");
					}
					CRuntime.memset((void*)(ptr + j), (int)b, (ulong)((long)num6));
					j += num6;
				}
			}
			if (j != num4)
			{
				return StbImage.stbi__err("bad codelengths");
			}
			if (StbImage.stbi__zbuild_huffman(&a->z_length, ptr, num) == 0)
			{
				return 0;
			}
			if (StbImage.stbi__zbuild_huffman(&a->z_distance, ptr + num, num2) == 0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		public unsafe static int stbi__parse_uncompressed_block(StbImage.stbi__zbuf* a)
		{
			byte* ptr = stackalloc byte[(UIntPtr)4];
			if ((a->num_bits & 7) != 0)
			{
				StbImage.stbi__zreceive(a, a->num_bits & 7);
			}
			int i = 0;
			while (a->num_bits > 0)
			{
				ptr[i++] = (byte)(a->code_buffer & 255U);
				a->code_buffer = a->code_buffer >> 8;
				a->num_bits = a->num_bits - 8;
			}
			if (a->num_bits < 0)
			{
				return StbImage.stbi__err("zlib corrupt");
			}
			while (i < 4)
			{
				ptr[i++] = StbImage.stbi__zget8(a);
			}
			int num = (int)ptr[1] * 256 + (int)(*ptr);
			if ((int)ptr[3] * 256 + (int)ptr[2] != (num ^ 65535))
			{
				return StbImage.stbi__err("zlib corrupt");
			}
			if (a->zbuffer + num != a->zbuffer_end)
			{
				return StbImage.stbi__err("read past buffer");
			}
			if (a->zout + num != a->zout_end && StbImage.stbi__zexpand(a, a->zout, num) == 0)
			{
				return 0;
			}
			CRuntime.memcpy((void*)a->zout, (void*)a->zbuffer, (ulong)((long)num));
			a->zbuffer = a->zbuffer + num;
			a->zout = a->zout + num;
			return 1;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		public unsafe static int stbi__parse_zlib_header(StbImage.stbi__zbuf* a)
		{
			int num = (int)StbImage.stbi__zget8(a);
			int num2 = num & 15;
			int num3 = (int)StbImage.stbi__zget8(a);
			if (StbImage.stbi__zeof(a) != 0)
			{
				return StbImage.stbi__err("bad zlib header");
			}
			if ((num * 256 + num3) % 31 != 0)
			{
				return StbImage.stbi__err("bad zlib header");
			}
			if ((num3 & 32) != 0)
			{
				return StbImage.stbi__err("no preset dict");
			}
			if (num2 != 8)
			{
				return StbImage.stbi__err("bad compression");
			}
			return 1;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000C760 File Offset: 0x0000A960
		public unsafe static int stbi__parse_zlib(StbImage.stbi__zbuf* a, int parse_header)
		{
			if (parse_header != 0 && StbImage.stbi__parse_zlib_header(a) == 0)
			{
				return 0;
			}
			a->num_bits = 0;
			a->code_buffer = 0U;
			for (;;)
			{
				int num = (int)StbImage.stbi__zreceive(a, 1);
				int num2 = (int)StbImage.stbi__zreceive(a, 2);
				if (num2 == 0)
				{
					if (StbImage.stbi__parse_uncompressed_block(a) == 0)
					{
						break;
					}
				}
				else
				{
					if (num2 == 3)
					{
						return 0;
					}
					if (num2 == 1)
					{
						byte[] array;
						byte* ptr;
						if ((array = StbImage.stbi__zdefault_length) == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						if (StbImage.stbi__zbuild_huffman(&a->z_length, ptr, 288) == 0)
						{
							return 0;
						}
						array = null;
						byte* ptr2;
						if ((array = StbImage.stbi__zdefault_distance) == null || array.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array[0];
						}
						if (StbImage.stbi__zbuild_huffman(&a->z_distance, ptr2, 32) == 0)
						{
							return 0;
						}
						array = null;
					}
					else if (StbImage.stbi__compute_huffman_codes(a) == 0)
					{
						return 0;
					}
					if (StbImage.stbi__parse_huffman_block(a) == 0)
					{
						return 0;
					}
				}
				if (num != 0)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000C83B File Offset: 0x0000AA3B
		public unsafe static int stbi__do_zlib(StbImage.stbi__zbuf* a, sbyte* obuf, int olen, int exp, int parse_header)
		{
			a->zout_start = obuf;
			a->zout = obuf;
			a->zout_end = obuf + olen;
			a->z_expandable = exp;
			return StbImage.stbi__parse_zlib(a, parse_header);
		}

		// Token: 0x0400001E RID: 30
		public static string stbi__g_failure_reason;

		// Token: 0x0400001F RID: 31
		public static readonly char[] stbi__parse_png_file_invalid_chunk = new char[25];

		// Token: 0x04000020 RID: 32
		public const int STBI__SCAN_header = 2;

		// Token: 0x04000021 RID: 33
		public const int STBI__SCAN_load = 0;

		// Token: 0x04000022 RID: 34
		public const int STBI__SCAN_type = 1;

		// Token: 0x04000023 RID: 35
		public const int STBI_default = 0;

		// Token: 0x04000024 RID: 36
		public const int STBI_grey = 1;

		// Token: 0x04000025 RID: 37
		public const int STBI_grey_alpha = 2;

		// Token: 0x04000026 RID: 38
		public const int STBI_ORDER_BGR = 1;

		// Token: 0x04000027 RID: 39
		public const int STBI_ORDER_RGB = 0;

		// Token: 0x04000028 RID: 40
		public const int STBI_rgb = 3;

		// Token: 0x04000029 RID: 41
		public const int STBI_rgb_alpha = 4;

		// Token: 0x0400002A RID: 42
		public static byte[] stbi__compute_huffman_codes_length_dezigzag = new byte[]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		// Token: 0x0400002B RID: 43
		public static int stbi__de_iphone_flag_global;

		// Token: 0x0400002C RID: 44
		public static int stbi__de_iphone_flag_local;

		// Token: 0x0400002D RID: 45
		public static int stbi__de_iphone_flag_set;

		// Token: 0x0400002E RID: 46
		public static float stbi__h2l_gamma_i = 0.45454544f;

		// Token: 0x0400002F RID: 47
		public static float stbi__h2l_scale_i = 1f;

		// Token: 0x04000030 RID: 48
		public static float stbi__l2h_gamma = 2.2f;

		// Token: 0x04000031 RID: 49
		public static float stbi__l2h_scale = 1f;

		// Token: 0x04000032 RID: 50
		public static byte[] stbi__process_frame_header_rgb = new byte[] { 82, 71, 66 };

		// Token: 0x04000033 RID: 51
		public static byte[] stbi__process_marker_tag = new byte[] { 65, 100, 111, 98, 101, 0 };

		// Token: 0x04000034 RID: 52
		public static int[] stbi__shiftsigned_mul_table = new int[] { 0, 255, 85, 73, 17, 33, 65, 129, 1 };

		// Token: 0x04000035 RID: 53
		public static int[] stbi__shiftsigned_shift_table = new int[] { 0, 0, 0, 1, 0, 2, 4, 6, 0 };

		// Token: 0x04000036 RID: 54
		public static int stbi__unpremultiply_on_load_global;

		// Token: 0x04000037 RID: 55
		public static int stbi__unpremultiply_on_load_local;

		// Token: 0x04000038 RID: 56
		public static int stbi__unpremultiply_on_load_set;

		// Token: 0x04000039 RID: 57
		public static int stbi__vertically_flip_on_load_global;

		// Token: 0x0400003A RID: 58
		public static int stbi__vertically_flip_on_load_local;

		// Token: 0x0400003B RID: 59
		public static int stbi__vertically_flip_on_load_set;

		// Token: 0x0400003C RID: 60
		public static uint[] stbi__bmask = new uint[]
		{
			0U, 1U, 3U, 7U, 15U, 31U, 63U, 127U, 255U, 511U,
			1023U, 2047U, 4095U, 8191U, 16383U, 32767U, 65535U
		};

		// Token: 0x0400003D RID: 61
		public static int[] stbi__jbias = new int[]
		{
			0, -1, -3, -7, -15, -31, -63, -127, -255, -511,
			-1023, -2047, -4095, -8191, -16383, -32767
		};

		// Token: 0x0400003E RID: 62
		public static byte[] stbi__jpeg_dezigzag = new byte[]
		{
			0, 1, 8, 16, 9, 2, 3, 10, 17, 24,
			32, 25, 18, 11, 4, 5, 12, 19, 26, 33,
			40, 48, 41, 34, 27, 20, 13, 6, 7, 14,
			21, 28, 35, 42, 49, 56, 57, 50, 43, 36,
			29, 22, 15, 23, 30, 37, 44, 51, 58, 59,
			52, 45, 38, 31, 39, 46, 53, 60, 61, 54,
			47, 55, 62, 63, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63
		};

		// Token: 0x0400003F RID: 63
		public const int STBI__F_avg = 3;

		// Token: 0x04000040 RID: 64
		public const int STBI__F_avg_first = 5;

		// Token: 0x04000041 RID: 65
		public const int STBI__F_none = 0;

		// Token: 0x04000042 RID: 66
		public const int STBI__F_paeth = 4;

		// Token: 0x04000043 RID: 67
		public const int STBI__F_paeth_first = 6;

		// Token: 0x04000044 RID: 68
		public const int STBI__F_sub = 1;

		// Token: 0x04000045 RID: 69
		public const int STBI__F_up = 2;

		// Token: 0x04000046 RID: 70
		public static byte[] first_row_filter = new byte[] { 0, 1, 0, 5, 6 };

		// Token: 0x04000047 RID: 71
		public static byte[] stbi__check_png_header_png_sig = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };

		// Token: 0x04000048 RID: 72
		public static byte[] stbi__depth_scale_table = new byte[] { 0, byte.MaxValue, 85, 0, 17, 0, 0, 0, 1 };

		// Token: 0x04000049 RID: 73
		public static byte[] stbi__zdefault_distance = new byte[]
		{
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5
		};

		// Token: 0x0400004A RID: 74
		public static byte[] stbi__zdefault_length = new byte[]
		{
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			8, 8, 8, 8, 8, 8, 8, 8
		};

		// Token: 0x0400004B RID: 75
		public static int[] stbi__zdist_base = new int[]
		{
			1, 2, 3, 4, 5, 7, 9, 13, 17, 25,
			33, 49, 65, 97, 129, 193, 257, 385, 513, 769,
			1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577,
			0, 0
		};

		// Token: 0x0400004C RID: 76
		public static int[] stbi__zdist_extra = new int[]
		{
			0, 0, 0, 0, 1, 1, 2, 2, 3, 3,
			4, 4, 5, 5, 6, 6, 7, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 13, 13,
			0, 0
		};

		// Token: 0x0400004D RID: 77
		public static int[] stbi__zlength_base = new int[]
		{
			3, 4, 5, 6, 7, 8, 9, 10, 11, 13,
			15, 17, 19, 23, 27, 31, 35, 43, 51, 59,
			67, 83, 99, 115, 131, 163, 195, 227, 258, 0,
			0
		};

		// Token: 0x0400004E RID: 78
		public static int[] stbi__zlength_extra = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0, 0,
			0
		};

		// Token: 0x0200000E RID: 14
		public class stbi__context
		{
			// Token: 0x060000EC RID: 236 RVA: 0x0000CA2B File Offset: 0x0000AC2B
			public stbi__context(Stream stream)
			{
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				this.Stream = stream;
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000ED RID: 237 RVA: 0x0000CA48 File Offset: 0x0000AC48
			public Stream Stream { get; }

			// Token: 0x04000060 RID: 96
			public byte[] _tempBuffer;

			// Token: 0x04000061 RID: 97
			public int img_n;

			// Token: 0x04000062 RID: 98
			public int img_out_n;

			// Token: 0x04000063 RID: 99
			public uint img_x;

			// Token: 0x04000064 RID: 100
			public uint img_y;
		}

		// Token: 0x0200000F RID: 15
		public struct stbi__bmp_data
		{
			// Token: 0x04000066 RID: 102
			public int bpp;

			// Token: 0x04000067 RID: 103
			public int offset;

			// Token: 0x04000068 RID: 104
			public int hsz;

			// Token: 0x04000069 RID: 105
			public uint mr;

			// Token: 0x0400006A RID: 106
			public uint mg;

			// Token: 0x0400006B RID: 107
			public uint mb;

			// Token: 0x0400006C RID: 108
			public uint ma;

			// Token: 0x0400006D RID: 109
			public uint all_a;

			// Token: 0x0400006E RID: 110
			public int extra_read;
		}

		// Token: 0x02000010 RID: 16
		public struct stbi__result_info
		{
			// Token: 0x0400006F RID: 111
			public int bits_per_channel;

			// Token: 0x04000070 RID: 112
			public int num_channels;

			// Token: 0x04000071 RID: 113
			public int channel_order;
		}

		// Token: 0x02000011 RID: 17
		public class stbi__gif
		{
			// Token: 0x04000072 RID: 114
			public unsafe byte* _out_;

			// Token: 0x04000073 RID: 115
			public unsafe byte* background;

			// Token: 0x04000074 RID: 116
			public int bgindex;

			// Token: 0x04000075 RID: 117
			public StbImage.stbi__gif_lzw[] codes = new StbImage.stbi__gif_lzw[8192];

			// Token: 0x04000076 RID: 118
			public byte[][] color_table;

			// Token: 0x04000077 RID: 119
			public int cur_x;

			// Token: 0x04000078 RID: 120
			public int cur_y;

			// Token: 0x04000079 RID: 121
			public int delay;

			// Token: 0x0400007A RID: 122
			public int eflags;

			// Token: 0x0400007B RID: 123
			public int flags;

			// Token: 0x0400007C RID: 124
			public int h;

			// Token: 0x0400007D RID: 125
			public unsafe byte* history;

			// Token: 0x0400007E RID: 126
			public int lflags;

			// Token: 0x0400007F RID: 127
			public int line_size;

			// Token: 0x04000080 RID: 128
			public byte[][] lpal = Utility.CreateArray<byte>(256, 4);

			// Token: 0x04000081 RID: 129
			public int max_x;

			// Token: 0x04000082 RID: 130
			public int max_y;

			// Token: 0x04000083 RID: 131
			public byte[][] pal = Utility.CreateArray<byte>(256, 4);

			// Token: 0x04000084 RID: 132
			public int parse;

			// Token: 0x04000085 RID: 133
			public int ratio;

			// Token: 0x04000086 RID: 134
			public int start_x;

			// Token: 0x04000087 RID: 135
			public int start_y;

			// Token: 0x04000088 RID: 136
			public int step;

			// Token: 0x04000089 RID: 137
			public int transparent;

			// Token: 0x0400008A RID: 138
			public int w;
		}

		// Token: 0x02000012 RID: 18
		public struct stbi__gif_lzw
		{
			// Token: 0x0400008B RID: 139
			public short prefix;

			// Token: 0x0400008C RID: 140
			public byte first;

			// Token: 0x0400008D RID: 141
			public byte suffix;
		}

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x060000F0 RID: 240
		public unsafe delegate void delegate0(byte* arg0, int arg1, short* arg2);

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x060000F4 RID: 244
		public unsafe delegate void delegate1(byte* arg0, byte* arg1, byte* arg2, byte* arg3, int arg4, int arg5);

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x060000F8 RID: 248
		public unsafe delegate byte* delegate2(byte* arg0, byte* arg1, byte* arg2, int arg3, int arg4);

		// Token: 0x02000016 RID: 22
		public struct stbi__huffman
		{
			// Token: 0x0400008E RID: 142
			[FixedBuffer(typeof(byte), 512)]
			public StbImage.stbi__huffman.<fast>e__FixedBuffer fast;

			// Token: 0x0400008F RID: 143
			[FixedBuffer(typeof(ushort), 256)]
			public StbImage.stbi__huffman.<code>e__FixedBuffer code;

			// Token: 0x04000090 RID: 144
			[FixedBuffer(typeof(byte), 256)]
			public StbImage.stbi__huffman.<values>e__FixedBuffer values;

			// Token: 0x04000091 RID: 145
			[FixedBuffer(typeof(byte), 257)]
			public StbImage.stbi__huffman.<size>e__FixedBuffer size;

			// Token: 0x04000092 RID: 146
			[FixedBuffer(typeof(uint), 18)]
			public StbImage.stbi__huffman.<maxcode>e__FixedBuffer maxcode;

			// Token: 0x04000093 RID: 147
			[FixedBuffer(typeof(int), 17)]
			public StbImage.stbi__huffman.<delta>e__FixedBuffer delta;

			// Token: 0x0200002A RID: 42
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 257)]
			public struct <size>e__FixedBuffer
			{
				// Token: 0x040000D3 RID: 211
				public byte FixedElementField;
			}

			// Token: 0x0200002B RID: 43
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 68)]
			public struct <delta>e__FixedBuffer
			{
				// Token: 0x040000D4 RID: 212
				public int FixedElementField;
			}

			// Token: 0x0200002C RID: 44
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 512)]
			public struct <code>e__FixedBuffer
			{
				// Token: 0x040000D5 RID: 213
				public ushort FixedElementField;
			}

			// Token: 0x0200002D RID: 45
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 72)]
			public struct <maxcode>e__FixedBuffer
			{
				// Token: 0x040000D6 RID: 214
				public uint FixedElementField;
			}

			// Token: 0x0200002E RID: 46
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 512)]
			public struct <fast>e__FixedBuffer
			{
				// Token: 0x040000D7 RID: 215
				public byte FixedElementField;
			}

			// Token: 0x0200002F RID: 47
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 256)]
			public struct <values>e__FixedBuffer
			{
				// Token: 0x040000D8 RID: 216
				public byte FixedElementField;
			}
		}

		// Token: 0x02000017 RID: 23
		public class stbi__jpeg
		{
			// Token: 0x04000094 RID: 148
			public int app14_color_transform;

			// Token: 0x04000095 RID: 149
			public int code_bits;

			// Token: 0x04000096 RID: 150
			public uint code_buffer;

			// Token: 0x04000097 RID: 151
			public ushort[][] dequant = Utility.CreateArray<ushort>(4, 64);

			// Token: 0x04000098 RID: 152
			public int eob_run;

			// Token: 0x04000099 RID: 153
			public short[][] fast_ac = Utility.CreateArray<short>(4, 512);

			// Token: 0x0400009A RID: 154
			public StbImage.stbi__huffman[] huff_ac = new StbImage.stbi__huffman[4];

			// Token: 0x0400009B RID: 155
			public StbImage.stbi__huffman[] huff_dc = new StbImage.stbi__huffman[4];

			// Token: 0x0400009C RID: 156
			public StbImage.delegate0 idct_block_kernel;

			// Token: 0x0400009D RID: 157
			public StbImage.stbi__jpeg.unnamed1[] img_comp = new StbImage.stbi__jpeg.unnamed1[4];

			// Token: 0x0400009E RID: 158
			public int img_h_max;

			// Token: 0x0400009F RID: 159
			public int img_mcu_h;

			// Token: 0x040000A0 RID: 160
			public int img_mcu_w;

			// Token: 0x040000A1 RID: 161
			public int img_mcu_x;

			// Token: 0x040000A2 RID: 162
			public int img_mcu_y;

			// Token: 0x040000A3 RID: 163
			public int img_v_max;

			// Token: 0x040000A4 RID: 164
			public int jfif;

			// Token: 0x040000A5 RID: 165
			public byte marker;

			// Token: 0x040000A6 RID: 166
			public int nomore;

			// Token: 0x040000A7 RID: 167
			public int[] order = new int[4];

			// Token: 0x040000A8 RID: 168
			public int progressive;

			// Token: 0x040000A9 RID: 169
			public StbImage.delegate2 resample_row_hv_2_kernel;

			// Token: 0x040000AA RID: 170
			public int restart_interval;

			// Token: 0x040000AB RID: 171
			public int rgb;

			// Token: 0x040000AC RID: 172
			public StbImage.stbi__context s;

			// Token: 0x040000AD RID: 173
			public int scan_n;

			// Token: 0x040000AE RID: 174
			public int spec_end;

			// Token: 0x040000AF RID: 175
			public int spec_start;

			// Token: 0x040000B0 RID: 176
			public int succ_high;

			// Token: 0x040000B1 RID: 177
			public int succ_low;

			// Token: 0x040000B2 RID: 178
			public int todo;

			// Token: 0x040000B3 RID: 179
			public StbImage.delegate1 YCbCr_to_RGB_kernel;

			// Token: 0x02000030 RID: 48
			public struct unnamed1
			{
				// Token: 0x040000D9 RID: 217
				public int id;

				// Token: 0x040000DA RID: 218
				public int h;

				// Token: 0x040000DB RID: 219
				public int v;

				// Token: 0x040000DC RID: 220
				public int tq;

				// Token: 0x040000DD RID: 221
				public int hd;

				// Token: 0x040000DE RID: 222
				public int ha;

				// Token: 0x040000DF RID: 223
				public int dc_pred;

				// Token: 0x040000E0 RID: 224
				public int x;

				// Token: 0x040000E1 RID: 225
				public int y;

				// Token: 0x040000E2 RID: 226
				public int w2;

				// Token: 0x040000E3 RID: 227
				public int h2;

				// Token: 0x040000E4 RID: 228
				public unsafe byte* data;

				// Token: 0x040000E5 RID: 229
				public unsafe void* raw_data;

				// Token: 0x040000E6 RID: 230
				public unsafe void* raw_coeff;

				// Token: 0x040000E7 RID: 231
				public unsafe byte* linebuf;

				// Token: 0x040000E8 RID: 232
				public unsafe short* coeff;

				// Token: 0x040000E9 RID: 233
				public int coeff_w;

				// Token: 0x040000EA RID: 234
				public int coeff_h;
			}
		}

		// Token: 0x02000018 RID: 24
		public class stbi__resample
		{
			// Token: 0x040000B4 RID: 180
			public int hs;

			// Token: 0x040000B5 RID: 181
			public unsafe byte* line0;

			// Token: 0x040000B6 RID: 182
			public unsafe byte* line1;

			// Token: 0x040000B7 RID: 183
			public StbImage.delegate2 resample;

			// Token: 0x040000B8 RID: 184
			public int vs;

			// Token: 0x040000B9 RID: 185
			public int w_lores;

			// Token: 0x040000BA RID: 186
			public int ypos;

			// Token: 0x040000BB RID: 187
			public int ystep;
		}

		// Token: 0x02000019 RID: 25
		public class stbi__png
		{
			// Token: 0x040000BC RID: 188
			public unsafe byte* _out_;

			// Token: 0x040000BD RID: 189
			public int depth;

			// Token: 0x040000BE RID: 190
			public unsafe byte* expanded;

			// Token: 0x040000BF RID: 191
			public unsafe byte* idata;

			// Token: 0x040000C0 RID: 192
			public StbImage.stbi__context s;
		}

		// Token: 0x0200001A RID: 26
		public struct stbi__pngchunk
		{
			// Token: 0x040000C1 RID: 193
			public uint length;

			// Token: 0x040000C2 RID: 194
			public uint type;
		}

		// Token: 0x0200001B RID: 27
		public struct stbi__zbuf
		{
			// Token: 0x040000C3 RID: 195
			public unsafe byte* zbuffer;

			// Token: 0x040000C4 RID: 196
			public unsafe byte* zbuffer_end;

			// Token: 0x040000C5 RID: 197
			public int num_bits;

			// Token: 0x040000C6 RID: 198
			public uint code_buffer;

			// Token: 0x040000C7 RID: 199
			public unsafe sbyte* zout;

			// Token: 0x040000C8 RID: 200
			public unsafe sbyte* zout_start;

			// Token: 0x040000C9 RID: 201
			public unsafe sbyte* zout_end;

			// Token: 0x040000CA RID: 202
			public int z_expandable;

			// Token: 0x040000CB RID: 203
			public StbImage.stbi__zhuffman z_length;

			// Token: 0x040000CC RID: 204
			public StbImage.stbi__zhuffman z_distance;
		}

		// Token: 0x0200001C RID: 28
		public struct stbi__zhuffman
		{
			// Token: 0x040000CD RID: 205
			[FixedBuffer(typeof(ushort), 512)]
			public StbImage.stbi__zhuffman.<fast>e__FixedBuffer fast;

			// Token: 0x040000CE RID: 206
			[FixedBuffer(typeof(ushort), 16)]
			public StbImage.stbi__zhuffman.<firstcode>e__FixedBuffer firstcode;

			// Token: 0x040000CF RID: 207
			[FixedBuffer(typeof(int), 17)]
			public StbImage.stbi__zhuffman.<maxcode>e__FixedBuffer maxcode;

			// Token: 0x040000D0 RID: 208
			[FixedBuffer(typeof(ushort), 16)]
			public StbImage.stbi__zhuffman.<firstsymbol>e__FixedBuffer firstsymbol;

			// Token: 0x040000D1 RID: 209
			[FixedBuffer(typeof(byte), 288)]
			public StbImage.stbi__zhuffman.<size>e__FixedBuffer size;

			// Token: 0x040000D2 RID: 210
			[FixedBuffer(typeof(ushort), 288)]
			public StbImage.stbi__zhuffman.<value>e__FixedBuffer value;

			// Token: 0x02000031 RID: 49
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 1024)]
			public struct <fast>e__FixedBuffer
			{
				// Token: 0x040000EB RID: 235
				public ushort FixedElementField;
			}

			// Token: 0x02000032 RID: 50
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 32)]
			public struct <firstcode>e__FixedBuffer
			{
				// Token: 0x040000EC RID: 236
				public ushort FixedElementField;
			}

			// Token: 0x02000033 RID: 51
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 32)]
			public struct <firstsymbol>e__FixedBuffer
			{
				// Token: 0x040000ED RID: 237
				public ushort FixedElementField;
			}

			// Token: 0x02000034 RID: 52
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 68)]
			public struct <maxcode>e__FixedBuffer
			{
				// Token: 0x040000EE RID: 238
				public int FixedElementField;
			}

			// Token: 0x02000035 RID: 53
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 288)]
			public struct <size>e__FixedBuffer
			{
				// Token: 0x040000EF RID: 239
				public byte FixedElementField;
			}

			// Token: 0x02000036 RID: 54
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(0, Size = 576)]
			public struct <value>e__FixedBuffer
			{
				// Token: 0x040000F0 RID: 240
				public ushort FixedElementField;
			}
		}
	}
}
