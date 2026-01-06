using System;
using System.Collections.Generic;
using System.Text;

namespace Unity.Burst
{
	// Token: 0x02000016 RID: 22
	internal static class SafeStringArrayHelper
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00005428 File Offset: 0x00003628
		public static string SerialiseStringArraySafe(string[] array)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in array)
			{
				stringBuilder.Append(string.Format("{0}]", Encoding.UTF8.GetByteCount(text)));
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005480 File Offset: 0x00003680
		public static string[] DeserialiseStringArraySafe(string input)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(input);
			List<string> list = new List<string>();
			int i = 0;
			int num = bytes.Length;
			IL_0097:
			while (i < num)
			{
				int num2 = 0;
				while (i < num)
				{
					byte b = bytes[i];
					if (b == 93)
					{
						i++;
						list.Add(Encoding.UTF8.GetString(bytes, i, num2));
						i += num2;
						goto IL_0097;
					}
					if (b < 48 || b > 57)
					{
						throw new FormatException(string.Format("Invalid input `{0}` at {1}: Got non-digit character while reading length", input, i));
					}
					num2 = num2 * 10 + (int)(b - 48);
					i++;
				}
				throw new FormatException("Invalid input `" + input + "`: reached end while reading length");
			}
			return list.ToArray();
		}
	}
}
