using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000032 RID: 50
	internal static class TextGeneratorUtilities
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00018274 File Offset: 0x00016474
		public static bool Approximately(float a, float b)
		{
			return b - 0.0001f < a && a < b + 0.0001f;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000182A0 File Offset: 0x000164A0
		public static Color32 HexCharsToColor(char[] hexChars, int tagCount)
		{
			bool flag = tagCount == 4;
			Color32 color;
			if (flag)
			{
				byte b = (byte)(TextGeneratorUtilities.HexToInt(hexChars[1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[1]));
				byte b2 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[2]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[2]));
				byte b3 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[3]));
				color = new Color32(b, b2, b3, byte.MaxValue);
			}
			else
			{
				bool flag2 = tagCount == 5;
				if (flag2)
				{
					byte b4 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[1]));
					byte b5 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[2]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[2]));
					byte b6 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[3]));
					byte b7 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[4]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[4]));
					color = new Color32(b4, b5, b6, b7);
				}
				else
				{
					bool flag3 = tagCount == 7;
					if (flag3)
					{
						byte b8 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[2]));
						byte b9 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[4]));
						byte b10 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[5]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[6]));
						color = new Color32(b8, b9, b10, byte.MaxValue);
					}
					else
					{
						bool flag4 = tagCount == 9;
						if (flag4)
						{
							byte b11 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[2]));
							byte b12 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[4]));
							byte b13 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[5]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[6]));
							byte b14 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[8]));
							color = new Color32(b11, b12, b13, b14);
						}
						else
						{
							bool flag5 = tagCount == 10;
							if (flag5)
							{
								byte b15 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[7]));
								byte b16 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[8]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[8]));
								byte b17 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[9]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[9]));
								color = new Color32(b15, b16, b17, byte.MaxValue);
							}
							else
							{
								bool flag6 = tagCount == 11;
								if (flag6)
								{
									byte b18 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[7]));
									byte b19 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[8]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[8]));
									byte b20 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[9]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[9]));
									byte b21 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[10]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[10]));
									color = new Color32(b18, b19, b20, b21);
								}
								else
								{
									bool flag7 = tagCount == 13;
									if (flag7)
									{
										byte b22 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[8]));
										byte b23 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[9]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[10]));
										byte b24 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[11]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[12]));
										color = new Color32(b22, b23, b24, byte.MaxValue);
									}
									else
									{
										bool flag8 = tagCount == 15;
										if (flag8)
										{
											byte b25 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[8]));
											byte b26 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[9]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[10]));
											byte b27 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[11]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[12]));
											byte b28 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[13]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[14]));
											color = new Color32(b25, b26, b27, b28);
										}
										else
										{
											color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
										}
									}
								}
							}
						}
					}
				}
			}
			return color;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00018660 File Offset: 0x00016860
		public static Color32 HexCharsToColor(char[] hexChars, int startIndex, int length)
		{
			bool flag = length == 7;
			Color32 color;
			if (flag)
			{
				byte b = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 2]));
				byte b2 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 4]));
				byte b3 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 5]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 6]));
				color = new Color32(b, b2, b3, byte.MaxValue);
			}
			else
			{
				bool flag2 = length == 9;
				if (flag2)
				{
					byte b4 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 1]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 2]));
					byte b5 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 3]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 4]));
					byte b6 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 5]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 6]));
					byte b7 = (byte)(TextGeneratorUtilities.HexToInt(hexChars[startIndex + 7]) * 16 + TextGeneratorUtilities.HexToInt(hexChars[startIndex + 8]));
					color = new Color32(b4, b5, b6, b7);
				}
				else
				{
					color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
				}
			}
			return color;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00018784 File Offset: 0x00016984
		public static int HexToInt(char hex)
		{
			switch (hex)
			{
			case '0':
				return 0;
			case '1':
				return 1;
			case '2':
				return 2;
			case '3':
				return 3;
			case '4':
				return 4;
			case '5':
				return 5;
			case '6':
				return 6;
			case '7':
				return 7;
			case '8':
				return 8;
			case '9':
				return 9;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				break;
			case 'A':
				return 10;
			case 'B':
				return 11;
			case 'C':
				return 12;
			case 'D':
				return 13;
			case 'E':
				return 14;
			case 'F':
				return 15;
			default:
				switch (hex)
				{
				case 'a':
					return 10;
				case 'b':
					return 11;
				case 'c':
					return 12;
				case 'd':
					return 13;
				case 'e':
					return 14;
				case 'f':
					return 15;
				}
				break;
			}
			return 15;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0001888C File Offset: 0x00016A8C
		public static float ConvertToFloat(char[] chars, int startIndex, int length)
		{
			int num;
			return TextGeneratorUtilities.ConvertToFloat(chars, startIndex, length, out num);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000188A8 File Offset: 0x00016AA8
		public static float ConvertToFloat(char[] chars, int startIndex, int length, out int lastIndex)
		{
			bool flag = startIndex == 0;
			float num;
			if (flag)
			{
				lastIndex = 0;
				num = -32767f;
			}
			else
			{
				int num2 = startIndex + length;
				bool flag2 = true;
				float num3 = 0f;
				int num4 = 1;
				bool flag3 = chars[startIndex] == '+';
				if (flag3)
				{
					num4 = 1;
					startIndex++;
				}
				else
				{
					bool flag4 = chars[startIndex] == '-';
					if (flag4)
					{
						num4 = -1;
						startIndex++;
					}
				}
				float num5 = 0f;
				int i = startIndex;
				while (i < num2)
				{
					uint num6 = (uint)chars[i];
					bool flag5 = (num6 >= 48U && num6 <= 57U) || num6 == 46U;
					if (flag5)
					{
						bool flag6 = num6 == 46U;
						if (flag6)
						{
							flag2 = false;
							num3 = 0.1f;
						}
						else
						{
							bool flag7 = flag2;
							if (flag7)
							{
								num5 = num5 * 10f + (float)((ulong)(num6 - 48U) * (ulong)((long)num4));
							}
							else
							{
								num5 += (num6 - 48U) * num3 * (float)num4;
								num3 *= 0.1f;
							}
						}
					}
					else
					{
						bool flag8 = num6 == 44U;
						if (flag8)
						{
							bool flag9 = i + 1 < num2 && chars[i + 1] == ' ';
							if (flag9)
							{
								lastIndex = i + 1;
							}
							else
							{
								lastIndex = i;
							}
							return num5;
						}
					}
					IL_0116:
					i++;
					continue;
					goto IL_0116;
				}
				lastIndex = num2;
				num = num5;
			}
			return num;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000189EC File Offset: 0x00016BEC
		public static Vector2 PackUV(float x, float y, float scale)
		{
			Vector2 vector;
			vector.x = (float)((int)(x * 511f));
			vector.y = (float)((int)(y * 511f));
			vector.x = vector.x * 4096f + vector.y;
			vector.y = scale;
			return vector;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00018A44 File Offset: 0x00016C44
		public static void StringToCharArray(string sourceText, ref int[] charBuffer, ref TextProcessingStack<int> styleStack, TextGenerationSettings generationSettings)
		{
			bool flag = sourceText == null;
			if (flag)
			{
				charBuffer[0] = 0;
			}
			else
			{
				bool flag2 = charBuffer == null;
				if (flag2)
				{
					charBuffer = new int[8];
				}
				styleStack.SetDefault(0);
				int num = 0;
				int i = 0;
				while (i < sourceText.Length)
				{
					bool flag3 = sourceText.get_Chars(i) == '\\' && sourceText.Length > i + 1;
					if (flag3)
					{
						int num2 = (int)sourceText.get_Chars(i + 1);
						int num3 = num2;
						if (num3 <= 92)
						{
							if (num3 != 85)
							{
								if (num3 == 92)
								{
									bool flag4 = !generationSettings.parseControlCharacters;
									if (!flag4)
									{
										bool flag5 = sourceText.Length <= i + 2;
										if (!flag5)
										{
											bool flag6 = num + 2 > charBuffer.Length;
											if (flag6)
											{
												TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
											}
											charBuffer[num] = (int)sourceText.get_Chars(i + 1);
											charBuffer[num + 1] = (int)sourceText.get_Chars(i + 2);
											i += 2;
											num += 2;
											goto IL_0385;
										}
									}
								}
							}
							else
							{
								bool flag7 = sourceText.Length > i + 9;
								if (flag7)
								{
									bool flag8 = num == charBuffer.Length;
									if (flag8)
									{
										TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
									}
									charBuffer[num] = TextGeneratorUtilities.GetUtf32(sourceText, i + 2);
									i += 9;
									num++;
									goto IL_0385;
								}
							}
						}
						else if (num3 != 110)
						{
							switch (num3)
							{
							case 114:
							{
								bool flag9 = !generationSettings.parseControlCharacters;
								if (!flag9)
								{
									bool flag10 = num == charBuffer.Length;
									if (flag10)
									{
										TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
									}
									charBuffer[num] = 13;
									i++;
									num++;
									goto IL_0385;
								}
								break;
							}
							case 116:
							{
								bool flag11 = !generationSettings.parseControlCharacters;
								if (!flag11)
								{
									bool flag12 = num == charBuffer.Length;
									if (flag12)
									{
										TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
									}
									charBuffer[num] = 9;
									i++;
									num++;
									goto IL_0385;
								}
								break;
							}
							case 117:
							{
								bool flag13 = sourceText.Length > i + 5;
								if (flag13)
								{
									bool flag14 = num == charBuffer.Length;
									if (flag14)
									{
										TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
									}
									charBuffer[num] = (int)((ushort)TextGeneratorUtilities.GetUtf16(sourceText, i + 2));
									i += 5;
									num++;
									goto IL_0385;
								}
								break;
							}
							}
						}
						else
						{
							bool flag15 = !generationSettings.parseControlCharacters;
							if (!flag15)
							{
								bool flag16 = num == charBuffer.Length;
								if (flag16)
								{
									TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
								}
								charBuffer[num] = 10;
								i++;
								num++;
								goto IL_0385;
							}
						}
						goto IL_0251;
					}
					goto IL_0251;
					IL_0385:
					i++;
					continue;
					IL_0251:
					bool flag17 = char.IsHighSurrogate(sourceText.get_Chars(i)) && char.IsLowSurrogate(sourceText.get_Chars(i + 1));
					if (flag17)
					{
						bool flag18 = num == charBuffer.Length;
						if (flag18)
						{
							TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
						}
						charBuffer[num] = char.ConvertToUtf32(sourceText.get_Chars(i), sourceText.get_Chars(i + 1));
						i++;
						num++;
						goto IL_0385;
					}
					bool flag19 = sourceText.get_Chars(i) == '<' && generationSettings.richText;
					if (flag19)
					{
						bool flag20 = TextGeneratorUtilities.IsTagName(ref sourceText, "<BR>", i);
						if (flag20)
						{
							bool flag21 = num == charBuffer.Length;
							if (flag21)
							{
								TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
							}
							charBuffer[num] = 10;
							num++;
							i += 3;
							goto IL_0385;
						}
						bool flag22 = TextGeneratorUtilities.IsTagName(ref sourceText, "<STYLE=", i);
						if (flag22)
						{
							int num4;
							bool flag23 = TextGeneratorUtilities.ReplaceOpeningStyleTag(ref sourceText, i, out num4, ref charBuffer, ref num, ref styleStack, ref generationSettings);
							if (flag23)
							{
								i = num4;
								goto IL_0385;
							}
						}
						else
						{
							bool flag24 = TextGeneratorUtilities.IsTagName(ref sourceText, "</STYLE>", i);
							if (flag24)
							{
								TextGeneratorUtilities.ReplaceClosingStyleTag(ref charBuffer, ref num, ref styleStack, ref generationSettings);
								i += 7;
								goto IL_0385;
							}
						}
					}
					bool flag25 = num == charBuffer.Length;
					if (flag25)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[num] = (int)sourceText.get_Chars(i);
					num++;
					goto IL_0385;
				}
				bool flag26 = num == charBuffer.Length;
				if (flag26)
				{
					TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
				}
				charBuffer[num] = 0;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00018E08 File Offset: 0x00017008
		private static void ResizeInternalArray<T>(ref T[] array)
		{
			int num = Mathf.NextPowerOfTwo(array.Length + 1);
			Array.Resize<T>(ref array, num);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00018E2C File Offset: 0x0001702C
		internal static void ResizeArray<T>(T[] array)
		{
			int num = array.Length * 2;
			bool flag = num == 0;
			if (flag)
			{
				num = 8;
			}
			Array.Resize<T>(ref array, num);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00018E54 File Offset: 0x00017054
		private static bool IsTagName(ref string text, string tag, int index)
		{
			bool flag = text.Length < index + tag.Length;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < tag.Length; i++)
				{
					bool flag3 = TextUtilities.ToUpperFast(text.get_Chars(index + i)) != tag.get_Chars(i);
					if (flag3)
					{
						return false;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00018EBC File Offset: 0x000170BC
		private static bool IsTagName(ref int[] text, string tag, int index)
		{
			bool flag = text.Length < index + tag.Length;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < tag.Length; i++)
				{
					bool flag3 = TextUtilities.ToUpperFast((char)text[index + i]) != tag.get_Chars(i);
					if (flag3)
					{
						return false;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00018F20 File Offset: 0x00017120
		private static bool ReplaceOpeningStyleTag(ref int[] sourceText, int srcIndex, out int srcOffset, ref int[] charBuffer, ref int writeIndex, ref TextProcessingStack<int> styleStack, ref TextGenerationSettings generationSettings)
		{
			int tagHashCode = TextGeneratorUtilities.GetTagHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TextStyle style = TextGeneratorUtilities.GetStyle(generationSettings, tagHashCode);
			bool flag = style == null || srcOffset == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				styleStack.Add(style.hashCode);
				int num = style.styleOpeningTagArray.Length;
				int[] styleOpeningTagArray = style.styleOpeningTagArray;
				int i = 0;
				while (i < num)
				{
					int num2 = styleOpeningTagArray[i];
					bool flag3 = num2 == 60;
					if (!flag3)
					{
						goto IL_0114;
					}
					bool flag4 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "<BR>", i);
					if (!flag4)
					{
						bool flag5 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "<STYLE=", i);
						if (flag5)
						{
							int num3;
							bool flag6 = TextGeneratorUtilities.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num3, ref charBuffer, ref writeIndex, ref styleStack, ref generationSettings);
							if (flag6)
							{
								i = num3;
								goto IL_013B;
							}
						}
						else
						{
							bool flag7 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "</STYLE>", i);
							if (flag7)
							{
								TextGeneratorUtilities.ReplaceClosingStyleTag(ref charBuffer, ref writeIndex, ref styleStack, ref generationSettings);
								i += 7;
								goto IL_013B;
							}
						}
						goto IL_0114;
					}
					bool flag8 = writeIndex == charBuffer.Length;
					if (flag8)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = 10;
					writeIndex++;
					i += 3;
					IL_013B:
					i++;
					continue;
					IL_0114:
					bool flag9 = writeIndex == charBuffer.Length;
					if (flag9)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = num2;
					writeIndex++;
					goto IL_013B;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00019084 File Offset: 0x00017284
		private static bool ReplaceOpeningStyleTag(ref string sourceText, int srcIndex, out int srcOffset, ref int[] charBuffer, ref int writeIndex, ref TextProcessingStack<int> styleStack, ref TextGenerationSettings generationSettings)
		{
			int tagHashCode = TextGeneratorUtilities.GetTagHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TextStyle style = TextGeneratorUtilities.GetStyle(generationSettings, tagHashCode);
			bool flag = style == null || srcOffset == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				styleStack.Add(style.hashCode);
				int num = style.styleOpeningTagArray.Length;
				int[] styleOpeningTagArray = style.styleOpeningTagArray;
				int i = 0;
				while (i < num)
				{
					int num2 = styleOpeningTagArray[i];
					bool flag3 = num2 == 60;
					if (!flag3)
					{
						goto IL_0114;
					}
					bool flag4 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "<BR>", i);
					if (!flag4)
					{
						bool flag5 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "<STYLE=", i);
						if (flag5)
						{
							int num3;
							bool flag6 = TextGeneratorUtilities.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num3, ref charBuffer, ref writeIndex, ref styleStack, ref generationSettings);
							if (flag6)
							{
								i = num3;
								goto IL_013B;
							}
						}
						else
						{
							bool flag7 = TextGeneratorUtilities.IsTagName(ref styleOpeningTagArray, "</STYLE>", i);
							if (flag7)
							{
								TextGeneratorUtilities.ReplaceClosingStyleTag(ref charBuffer, ref writeIndex, ref styleStack, ref generationSettings);
								i += 7;
								goto IL_013B;
							}
						}
						goto IL_0114;
					}
					bool flag8 = writeIndex == charBuffer.Length;
					if (flag8)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = 10;
					writeIndex++;
					i += 3;
					IL_013B:
					i++;
					continue;
					IL_0114:
					bool flag9 = writeIndex == charBuffer.Length;
					if (flag9)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = num2;
					writeIndex++;
					goto IL_013B;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000191E8 File Offset: 0x000173E8
		private static void ReplaceClosingStyleTag(ref int[] charBuffer, ref int writeIndex, ref TextProcessingStack<int> styleStack, ref TextGenerationSettings generationSettings)
		{
			int num = styleStack.CurrentItem();
			TextStyle style = TextGeneratorUtilities.GetStyle(generationSettings, num);
			styleStack.Remove();
			bool flag = style == null;
			if (!flag)
			{
				int num2 = style.styleClosingTagArray.Length;
				int[] styleClosingTagArray = style.styleClosingTagArray;
				int i = 0;
				while (i < num2)
				{
					int num3 = styleClosingTagArray[i];
					bool flag2 = num3 == 60;
					if (!flag2)
					{
						goto IL_00F1;
					}
					bool flag3 = TextGeneratorUtilities.IsTagName(ref styleClosingTagArray, "<BR>", i);
					if (!flag3)
					{
						bool flag4 = TextGeneratorUtilities.IsTagName(ref styleClosingTagArray, "<STYLE=", i);
						if (flag4)
						{
							int num4;
							bool flag5 = TextGeneratorUtilities.ReplaceOpeningStyleTag(ref styleClosingTagArray, i, out num4, ref charBuffer, ref writeIndex, ref styleStack, ref generationSettings);
							if (flag5)
							{
								i = num4;
								goto IL_0114;
							}
						}
						else
						{
							bool flag6 = TextGeneratorUtilities.IsTagName(ref styleClosingTagArray, "</STYLE>", i);
							if (flag6)
							{
								TextGeneratorUtilities.ReplaceClosingStyleTag(ref charBuffer, ref writeIndex, ref styleStack, ref generationSettings);
								i += 7;
								goto IL_0114;
							}
						}
						goto IL_00F1;
					}
					bool flag7 = writeIndex == charBuffer.Length;
					if (flag7)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = 10;
					writeIndex++;
					i += 3;
					IL_0114:
					i++;
					continue;
					IL_00F1:
					bool flag8 = writeIndex == charBuffer.Length;
					if (flag8)
					{
						TextGeneratorUtilities.ResizeInternalArray<int>(ref charBuffer);
					}
					charBuffer[writeIndex] = num3;
					writeIndex++;
					goto IL_0114;
				}
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00019320 File Offset: 0x00017520
		private static TextStyle GetStyle(TextGenerationSettings generationSetting, int hashCode)
		{
			TextStyle textStyle = null;
			TextStyleSheet textStyleSheet = generationSetting.styleSheet;
			bool flag = textStyleSheet != null;
			if (flag)
			{
				textStyle = textStyleSheet.GetStyle(hashCode);
				bool flag2 = textStyle != null;
				if (flag2)
				{
					return textStyle;
				}
			}
			textStyleSheet = generationSetting.textSettings.defaultStyleSheet;
			bool flag3 = textStyleSheet != null;
			if (flag3)
			{
				textStyle = textStyleSheet.GetStyle(hashCode);
			}
			return textStyle;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00019384 File Offset: 0x00017584
		private static int GetUtf32(string text, int i)
		{
			int num = 0;
			num += TextGeneratorUtilities.HexToInt(text.get_Chars(i)) << 30;
			num += TextGeneratorUtilities.HexToInt(text.get_Chars(i + 1)) << 24;
			num += TextGeneratorUtilities.HexToInt(text.get_Chars(i + 2)) << 20;
			num += TextGeneratorUtilities.HexToInt(text.get_Chars(i + 3)) << 16;
			num += TextGeneratorUtilities.HexToInt(text.get_Chars(i + 4)) << 12;
			num += TextGeneratorUtilities.HexToInt(text.get_Chars(i + 5)) << 8;
			num += TextGeneratorUtilities.HexToInt(text.get_Chars(i + 6)) << 4;
			return num + TextGeneratorUtilities.HexToInt(text.get_Chars(i + 7));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00019434 File Offset: 0x00017634
		private static int GetUtf16(string text, int i)
		{
			int num = 0;
			num += TextGeneratorUtilities.HexToInt(text.get_Chars(i)) << 12;
			num += TextGeneratorUtilities.HexToInt(text.get_Chars(i + 1)) << 8;
			num += TextGeneratorUtilities.HexToInt(text.get_Chars(i + 2)) << 4;
			return num + TextGeneratorUtilities.HexToInt(text.get_Chars(i + 3));
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00019494 File Offset: 0x00017694
		private static int GetTagHashCode(ref int[] text, int index, out int closeIndex)
		{
			int num = 0;
			closeIndex = 0;
			for (int i = index; i < text.Length; i++)
			{
				bool flag = text[i] == 34;
				if (!flag)
				{
					bool flag2 = text[i] == 62;
					if (flag2)
					{
						closeIndex = i;
						break;
					}
					num = ((num << 5) + num) ^ (int)TextUtilities.ToUpperASCIIFast((uint)((ushort)text[i]));
				}
			}
			return num;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000194F8 File Offset: 0x000176F8
		private static int GetTagHashCode(ref string text, int index, out int closeIndex)
		{
			int num = 0;
			closeIndex = 0;
			for (int i = index; i < text.Length; i++)
			{
				bool flag = text.get_Chars(i) == '"';
				if (!flag)
				{
					bool flag2 = text.get_Chars(i) == '>';
					if (flag2)
					{
						closeIndex = i;
						break;
					}
					num = ((num << 5) + num) ^ (int)TextUtilities.ToUpperASCIIFast((uint)text.get_Chars(i));
				}
			}
			return num;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00019568 File Offset: 0x00017768
		public static void FillCharacterVertexBuffers(int i, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			int materialReferenceIndex = textInfo.textElementInfo[i].materialReferenceIndex;
			int vertexCount = textInfo.meshInfo[materialReferenceIndex].vertexCount;
			bool flag = vertexCount >= textInfo.meshInfo[materialReferenceIndex].vertices.Length;
			if (flag)
			{
				textInfo.meshInfo[materialReferenceIndex].ResizeMeshInfo(Mathf.NextPowerOfTwo((vertexCount + 4) / 4));
			}
			TextElementInfo[] textElementInfo = textInfo.textElementInfo;
			textInfo.textElementInfo[i].vertexIndex = vertexCount;
			bool inverseYAxis = generationSettings.inverseYAxis;
			if (inverseYAxis)
			{
				Vector3 vector;
				vector.x = 0f;
				vector.y = generationSettings.screenRect.y + generationSettings.screenRect.height;
				vector.z = 0f;
				Vector3 vector2 = textElementInfo[i].vertexBottomLeft.position;
				vector2.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = vector2 + vector;
				vector2 = textElementInfo[i].vertexTopLeft.position;
				vector2.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = vector2 + vector;
				vector2 = textElementInfo[i].vertexTopRight.position;
				vector2.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = vector2 + vector;
				vector2 = textElementInfo[i].vertexBottomRight.position;
				vector2.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = vector2 + vector;
			}
			else
			{
				textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = textElementInfo[i].vertexBottomLeft.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = textElementInfo[i].vertexTopLeft.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = textElementInfo[i].vertexTopRight.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = textElementInfo[i].vertexBottomRight.position;
			}
			textInfo.meshInfo[materialReferenceIndex].uvs0[vertexCount] = textElementInfo[i].vertexBottomLeft.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs2[vertexCount] = textElementInfo[i].vertexBottomLeft.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv2;
			textInfo.meshInfo[materialReferenceIndex].colors32[vertexCount] = textElementInfo[i].vertexBottomLeft.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[1 + vertexCount] = textElementInfo[i].vertexTopLeft.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[2 + vertexCount] = textElementInfo[i].vertexTopRight.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[3 + vertexCount] = textElementInfo[i].vertexBottomRight.color;
			textInfo.meshInfo[materialReferenceIndex].vertexCount = vertexCount + 4;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000199F8 File Offset: 0x00017BF8
		public static void FillSpriteVertexBuffers(int i, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			int materialReferenceIndex = textInfo.textElementInfo[i].materialReferenceIndex;
			int vertexCount = textInfo.meshInfo[materialReferenceIndex].vertexCount;
			TextElementInfo[] textElementInfo = textInfo.textElementInfo;
			textInfo.textElementInfo[i].vertexIndex = vertexCount;
			bool inverseYAxis = generationSettings.inverseYAxis;
			if (inverseYAxis)
			{
				Vector3 vector;
				vector.x = 0f;
				vector.y = generationSettings.screenRect.y + generationSettings.screenRect.height;
				vector.z = 0f;
				Vector3 vector2 = textElementInfo[i].vertexBottomLeft.position;
				vector2.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = vector2 + vector;
				vector2 = textElementInfo[i].vertexTopLeft.position;
				vector2.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = vector2 + vector;
				vector2 = textElementInfo[i].vertexTopRight.position;
				vector2.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = vector2 + vector;
				vector2 = textElementInfo[i].vertexBottomRight.position;
				vector2.y *= -1f;
				textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = vector2 + vector;
			}
			else
			{
				textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = textElementInfo[i].vertexBottomLeft.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = textElementInfo[i].vertexTopLeft.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = textElementInfo[i].vertexTopRight.position;
				textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = textElementInfo[i].vertexBottomRight.position;
			}
			textInfo.meshInfo[materialReferenceIndex].uvs0[vertexCount] = textElementInfo[i].vertexBottomLeft.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs0[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv;
			textInfo.meshInfo[materialReferenceIndex].uvs2[vertexCount] = textElementInfo[i].vertexBottomLeft.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv2;
			textInfo.meshInfo[materialReferenceIndex].uvs2[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv2;
			textInfo.meshInfo[materialReferenceIndex].colors32[vertexCount] = textElementInfo[i].vertexBottomLeft.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[1 + vertexCount] = textElementInfo[i].vertexTopLeft.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[2 + vertexCount] = textElementInfo[i].vertexTopRight.color;
			textInfo.meshInfo[materialReferenceIndex].colors32[3 + vertexCount] = textElementInfo[i].vertexBottomRight.color;
			textInfo.meshInfo[materialReferenceIndex].vertexCount = vertexCount + 4;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00019E50 File Offset: 0x00018050
		public static void AdjustLineOffset(int startIndex, int endIndex, float offset, TextInfo textInfo)
		{
			Vector3 vector = new Vector3(0f, offset, 0f);
			for (int i = startIndex; i <= endIndex; i++)
			{
				TextElementInfo[] textElementInfo = textInfo.textElementInfo;
				int num = i;
				textElementInfo[num].bottomLeft = textElementInfo[num].bottomLeft - vector;
				TextElementInfo[] textElementInfo2 = textInfo.textElementInfo;
				int num2 = i;
				textElementInfo2[num2].topLeft = textElementInfo2[num2].topLeft - vector;
				TextElementInfo[] textElementInfo3 = textInfo.textElementInfo;
				int num3 = i;
				textElementInfo3[num3].topRight = textElementInfo3[num3].topRight - vector;
				TextElementInfo[] textElementInfo4 = textInfo.textElementInfo;
				int num4 = i;
				textElementInfo4[num4].bottomRight = textElementInfo4[num4].bottomRight - vector;
				TextElementInfo[] textElementInfo5 = textInfo.textElementInfo;
				int num5 = i;
				textElementInfo5[num5].ascender = textElementInfo5[num5].ascender - vector.y;
				TextElementInfo[] textElementInfo6 = textInfo.textElementInfo;
				int num6 = i;
				textElementInfo6[num6].baseLine = textElementInfo6[num6].baseLine - vector.y;
				TextElementInfo[] textElementInfo7 = textInfo.textElementInfo;
				int num7 = i;
				textElementInfo7[num7].descender = textElementInfo7[num7].descender - vector.y;
				bool isVisible = textInfo.textElementInfo[i].isVisible;
				if (isVisible)
				{
					TextElementInfo[] textElementInfo8 = textInfo.textElementInfo;
					int num8 = i;
					textElementInfo8[num8].vertexBottomLeft.position = textElementInfo8[num8].vertexBottomLeft.position - vector;
					TextElementInfo[] textElementInfo9 = textInfo.textElementInfo;
					int num9 = i;
					textElementInfo9[num9].vertexTopLeft.position = textElementInfo9[num9].vertexTopLeft.position - vector;
					TextElementInfo[] textElementInfo10 = textInfo.textElementInfo;
					int num10 = i;
					textElementInfo10[num10].vertexTopRight.position = textElementInfo10[num10].vertexTopRight.position - vector;
					TextElementInfo[] textElementInfo11 = textInfo.textElementInfo;
					int num11 = i;
					textElementInfo11[num11].vertexBottomRight.position = textElementInfo11[num11].vertexBottomRight.position - vector;
				}
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0001A01C File Offset: 0x0001821C
		public static void ResizeLineExtents(int size, TextInfo textInfo)
		{
			size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size + 1));
			LineInfo[] array = new LineInfo[size];
			for (int i = 0; i < size; i++)
			{
				bool flag = i < textInfo.lineInfo.Length;
				if (flag)
				{
					array[i] = textInfo.lineInfo[i];
				}
				else
				{
					array[i].lineExtents.min = TextGeneratorUtilities.largePositiveVector2;
					array[i].lineExtents.max = TextGeneratorUtilities.largeNegativeVector2;
					array[i].ascender = -32767f;
					array[i].descender = 32767f;
				}
			}
			textInfo.lineInfo = array;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0001A0DC File Offset: 0x000182DC
		public static FontStyles LegacyStyleToNewStyle(FontStyle fontStyle)
		{
			FontStyles fontStyles;
			switch (fontStyle)
			{
			case FontStyle.Bold:
				fontStyles = FontStyles.Bold;
				break;
			case FontStyle.Italic:
				fontStyles = FontStyles.Italic;
				break;
			case FontStyle.BoldAndItalic:
				fontStyles = FontStyles.Bold | FontStyles.Italic;
				break;
			default:
				fontStyles = FontStyles.Normal;
				break;
			}
			return fontStyles;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0001A118 File Offset: 0x00018318
		public static TextAlignment LegacyAlignmentToNewAlignment(TextAnchor anchor)
		{
			TextAlignment textAlignment;
			switch (anchor)
			{
			case TextAnchor.UpperLeft:
				textAlignment = TextAlignment.TopLeft;
				break;
			case TextAnchor.UpperCenter:
				textAlignment = TextAlignment.TopCenter;
				break;
			case TextAnchor.UpperRight:
				textAlignment = TextAlignment.TopRight;
				break;
			case TextAnchor.MiddleLeft:
				textAlignment = TextAlignment.MiddleLeft;
				break;
			case TextAnchor.MiddleCenter:
				textAlignment = TextAlignment.MiddleCenter;
				break;
			case TextAnchor.MiddleRight:
				textAlignment = TextAlignment.MiddleRight;
				break;
			case TextAnchor.LowerLeft:
				textAlignment = TextAlignment.BottomLeft;
				break;
			case TextAnchor.LowerCenter:
				textAlignment = TextAlignment.BottomCenter;
				break;
			case TextAnchor.LowerRight:
				textAlignment = TextAlignment.BottomRight;
				break;
			default:
				textAlignment = TextAlignment.TopLeft;
				break;
			}
			return textAlignment;
		}

		// Token: 0x04000255 RID: 597
		public static readonly Vector2 largePositiveVector2 = new Vector2(2.1474836E+09f, 2.1474836E+09f);

		// Token: 0x04000256 RID: 598
		public static readonly Vector2 largeNegativeVector2 = new Vector2(-214748370f, -214748370f);

		// Token: 0x04000257 RID: 599
		public const float largePositiveFloat = 32767f;

		// Token: 0x04000258 RID: 600
		public const float largeNegativeFloat = -32767f;
	}
}
