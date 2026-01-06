using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000437 RID: 1079
	internal static class HttpDateParse
	{
		// Token: 0x0600223D RID: 8765 RVA: 0x0007DC35 File Offset: 0x0007BE35
		private static char MAKE_UPPER(char c)
		{
			return char.ToUpper(c, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0007DC44 File Offset: 0x0007BE44
		private static int MapDayMonthToDword(char[] lpszDay, int index)
		{
			switch (HttpDateParse.MAKE_UPPER(lpszDay[index]))
			{
			case 'A':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'P')
				{
					return 4;
				}
				if (c != 'U')
				{
					return -999;
				}
				return 8;
			}
			case 'D':
				return 12;
			case 'F':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'E')
				{
					return 2;
				}
				if (c == 'R')
				{
					return 5;
				}
				return -999;
			}
			case 'G':
				return -1000;
			case 'J':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c != 'A')
				{
					if (c == 'U')
					{
						char c2 = HttpDateParse.MAKE_UPPER(lpszDay[index + 2]);
						if (c2 == 'L')
						{
							return 7;
						}
						if (c2 == 'N')
						{
							return 6;
						}
					}
					return -999;
				}
				return 1;
			}
			case 'M':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c != 'A')
				{
					if (c == 'O')
					{
						return 1;
					}
				}
				else
				{
					char c2 = HttpDateParse.MAKE_UPPER(lpszDay[index + 2]);
					if (c2 == 'R')
					{
						return 3;
					}
					if (c2 == 'Y')
					{
						return 5;
					}
				}
				return -999;
			}
			case 'N':
				return 11;
			case 'O':
				return 10;
			case 'S':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'A')
				{
					return 6;
				}
				if (c == 'E')
				{
					return 9;
				}
				if (c != 'U')
				{
					return -999;
				}
				return 0;
			}
			case 'T':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'H')
				{
					return 4;
				}
				if (c == 'U')
				{
					return 2;
				}
				return -999;
			}
			case 'U':
				return -1000;
			case 'W':
				return 3;
			}
			return -999;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0007DDD8 File Offset: 0x0007BFD8
		public static bool ParseHttpDate(string DateString, out DateTime dtOut)
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			bool flag = false;
			int[] array = new int[8];
			bool flag2 = true;
			char[] array2 = DateString.ToCharArray();
			dtOut = default(DateTime);
			while (num < DateString.Length && num2 < 8)
			{
				if (array2[num] >= '0' && array2[num] <= '9')
				{
					array[num2] = 0;
					do
					{
						array[num2] *= 10;
						array[num2] += (int)(array2[num] - '0');
						num++;
					}
					while (num < DateString.Length && array2[num] >= '0' && array2[num] <= '9');
					num2++;
				}
				else if ((array2[num] >= 'A' && array2[num] <= 'Z') || (array2[num] >= 'a' && array2[num] <= 'z'))
				{
					array[num2] = HttpDateParse.MapDayMonthToDword(array2, num);
					num3 = num2;
					if (array[num2] == -999 && (!flag || num2 != 6))
					{
						flag2 = false;
						return flag2;
					}
					if (num2 == 1)
					{
						flag = true;
					}
					do
					{
						num++;
					}
					while (num < DateString.Length && ((array2[num] >= 'A' && array2[num] <= 'Z') || (array2[num] >= 'a' && array2[num] <= 'z')));
					num2++;
				}
				else
				{
					num++;
				}
			}
			int num4 = 0;
			int num5;
			int num6;
			int num7;
			int num8;
			int num9;
			int num10;
			if (flag)
			{
				num5 = array[2];
				num6 = array[1];
				num7 = array[3];
				num8 = array[4];
				num9 = array[5];
				if (num3 != 6)
				{
					num10 = array[6];
				}
				else
				{
					num10 = array[7];
				}
			}
			else
			{
				num5 = array[1];
				num6 = array[2];
				num10 = array[3];
				num7 = array[4];
				num8 = array[5];
				num9 = array[6];
			}
			if (num10 < 100)
			{
				num10 += ((num10 < 80) ? 2000 : 1900);
			}
			if (num2 < 4 || num5 > 31 || num7 > 23 || num8 > 59 || num9 > 59)
			{
				return false;
			}
			dtOut = new DateTime(num10, num6, num5, num7, num8, num9, num4);
			if (num3 == 6)
			{
				dtOut = dtOut.ToUniversalTime();
			}
			if (num2 > 7 && array[7] != -1000)
			{
				double num11 = (double)array[7];
				dtOut.AddHours(num11);
			}
			dtOut = dtOut.ToLocalTime();
			return flag2;
		}

		// Token: 0x040013C7 RID: 5063
		private const int BASE_DEC = 10;

		// Token: 0x040013C8 RID: 5064
		private const int DATE_INDEX_DAY_OF_WEEK = 0;

		// Token: 0x040013C9 RID: 5065
		private const int DATE_1123_INDEX_DAY = 1;

		// Token: 0x040013CA RID: 5066
		private const int DATE_1123_INDEX_MONTH = 2;

		// Token: 0x040013CB RID: 5067
		private const int DATE_1123_INDEX_YEAR = 3;

		// Token: 0x040013CC RID: 5068
		private const int DATE_1123_INDEX_HRS = 4;

		// Token: 0x040013CD RID: 5069
		private const int DATE_1123_INDEX_MINS = 5;

		// Token: 0x040013CE RID: 5070
		private const int DATE_1123_INDEX_SECS = 6;

		// Token: 0x040013CF RID: 5071
		private const int DATE_ANSI_INDEX_MONTH = 1;

		// Token: 0x040013D0 RID: 5072
		private const int DATE_ANSI_INDEX_DAY = 2;

		// Token: 0x040013D1 RID: 5073
		private const int DATE_ANSI_INDEX_HRS = 3;

		// Token: 0x040013D2 RID: 5074
		private const int DATE_ANSI_INDEX_MINS = 4;

		// Token: 0x040013D3 RID: 5075
		private const int DATE_ANSI_INDEX_SECS = 5;

		// Token: 0x040013D4 RID: 5076
		private const int DATE_ANSI_INDEX_YEAR = 6;

		// Token: 0x040013D5 RID: 5077
		private const int DATE_INDEX_TZ = 7;

		// Token: 0x040013D6 RID: 5078
		private const int DATE_INDEX_LAST = 7;

		// Token: 0x040013D7 RID: 5079
		private const int MAX_FIELD_DATE_ENTRIES = 8;

		// Token: 0x040013D8 RID: 5080
		private const int DATE_TOKEN_JANUARY = 1;

		// Token: 0x040013D9 RID: 5081
		private const int DATE_TOKEN_FEBRUARY = 2;

		// Token: 0x040013DA RID: 5082
		private const int DATE_TOKEN_Microsoft = 3;

		// Token: 0x040013DB RID: 5083
		private const int DATE_TOKEN_APRIL = 4;

		// Token: 0x040013DC RID: 5084
		private const int DATE_TOKEN_MAY = 5;

		// Token: 0x040013DD RID: 5085
		private const int DATE_TOKEN_JUNE = 6;

		// Token: 0x040013DE RID: 5086
		private const int DATE_TOKEN_JULY = 7;

		// Token: 0x040013DF RID: 5087
		private const int DATE_TOKEN_AUGUST = 8;

		// Token: 0x040013E0 RID: 5088
		private const int DATE_TOKEN_SEPTEMBER = 9;

		// Token: 0x040013E1 RID: 5089
		private const int DATE_TOKEN_OCTOBER = 10;

		// Token: 0x040013E2 RID: 5090
		private const int DATE_TOKEN_NOVEMBER = 11;

		// Token: 0x040013E3 RID: 5091
		private const int DATE_TOKEN_DECEMBER = 12;

		// Token: 0x040013E4 RID: 5092
		private const int DATE_TOKEN_LAST_MONTH = 13;

		// Token: 0x040013E5 RID: 5093
		private const int DATE_TOKEN_SUNDAY = 0;

		// Token: 0x040013E6 RID: 5094
		private const int DATE_TOKEN_MONDAY = 1;

		// Token: 0x040013E7 RID: 5095
		private const int DATE_TOKEN_TUESDAY = 2;

		// Token: 0x040013E8 RID: 5096
		private const int DATE_TOKEN_WEDNESDAY = 3;

		// Token: 0x040013E9 RID: 5097
		private const int DATE_TOKEN_THURSDAY = 4;

		// Token: 0x040013EA RID: 5098
		private const int DATE_TOKEN_FRIDAY = 5;

		// Token: 0x040013EB RID: 5099
		private const int DATE_TOKEN_SATURDAY = 6;

		// Token: 0x040013EC RID: 5100
		private const int DATE_TOKEN_LAST_DAY = 7;

		// Token: 0x040013ED RID: 5101
		private const int DATE_TOKEN_GMT = -1000;

		// Token: 0x040013EE RID: 5102
		private const int DATE_TOKEN_LAST = -1000;

		// Token: 0x040013EF RID: 5103
		private const int DATE_TOKEN_ERROR = -999;
	}
}
