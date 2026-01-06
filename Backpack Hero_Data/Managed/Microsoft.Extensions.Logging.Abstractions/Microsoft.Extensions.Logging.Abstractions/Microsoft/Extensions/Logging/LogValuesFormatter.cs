using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// Formatter to convert the named format items like {NamedformatItem} to <see cref="M:System.String.Format(System.IFormatProvider,System.String,System.Object)" /> format.
	/// </summary>
	// Token: 0x02000018 RID: 24
	internal class LogValuesFormatter
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00002ECC File Offset: 0x000010CC
		public LogValuesFormatter(string format)
		{
			this.OriginalFormat = format;
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			int length = format.Length;
			while (i < length)
			{
				int num = LogValuesFormatter.FindBraceIndex(format, '{', i, length);
				int num2 = LogValuesFormatter.FindBraceIndex(format, '}', num, length);
				if (num2 == length)
				{
					stringBuilder.Append(format, i, length - i);
					i = length;
				}
				else
				{
					int num3 = LogValuesFormatter.FindIndexOfAny(format, LogValuesFormatter.FormatDelimiters, num, num2);
					stringBuilder.Append(format, i, num - i + 1);
					stringBuilder.Append(this._valueNames.Count.ToString(CultureInfo.InvariantCulture));
					this._valueNames.Add(format.Substring(num + 1, num3 - num - 1));
					stringBuilder.Append(format, num3, num2 - num3 + 1);
					i = num2 + 1;
				}
			}
			this._format = stringBuilder.ToString();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002FB2 File Offset: 0x000011B2
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002FBA File Offset: 0x000011BA
		public string OriginalFormat { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002FC3 File Offset: 0x000011C3
		public List<string> ValueNames
		{
			get
			{
				return this._valueNames;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002FCC File Offset: 0x000011CC
		private static int FindBraceIndex(string format, char brace, int startIndex, int endIndex)
		{
			int num = endIndex;
			int i = startIndex;
			int num2 = 0;
			while (i < endIndex)
			{
				if (num2 > 0 && format.get_Chars(i) != brace)
				{
					if (num2 % 2 != 0)
					{
						break;
					}
					num2 = 0;
					num = endIndex;
				}
				else if (format.get_Chars(i) == brace)
				{
					if (brace == '}')
					{
						if (num2 == 0)
						{
							num = i;
						}
					}
					else
					{
						num = i;
					}
					num2++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003020 File Offset: 0x00001220
		private static int FindIndexOfAny(string format, char[] chars, int startIndex, int endIndex)
		{
			int num = format.IndexOfAny(chars, startIndex, endIndex - startIndex);
			if (num != -1)
			{
				return num;
			}
			return endIndex;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003040 File Offset: 0x00001240
		public string Format(object[] values)
		{
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i] = this.FormatArgument(values[i]);
				}
			}
			return string.Format(CultureInfo.InvariantCulture, this._format, values ?? Array.Empty<object>());
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003084 File Offset: 0x00001284
		internal string Format()
		{
			return this._format;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000308C File Offset: 0x0000128C
		internal string Format(object arg0)
		{
			return string.Format(CultureInfo.InvariantCulture, this._format, this.FormatArgument(arg0));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030A5 File Offset: 0x000012A5
		internal string Format(object arg0, object arg1)
		{
			return string.Format(CultureInfo.InvariantCulture, this._format, this.FormatArgument(arg0), this.FormatArgument(arg1));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000030C5 File Offset: 0x000012C5
		internal string Format(object arg0, object arg1, object arg2)
		{
			return string.Format(CultureInfo.InvariantCulture, this._format, this.FormatArgument(arg0), this.FormatArgument(arg1), this.FormatArgument(arg2));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000030EC File Offset: 0x000012EC
		public KeyValuePair<string, object> GetValue(object[] values, int index)
		{
			if (index < 0 || index > this._valueNames.Count)
			{
				throw new IndexOutOfRangeException("index");
			}
			if (this._valueNames.Count > index)
			{
				return new KeyValuePair<string, object>(this._valueNames[index], values[index]);
			}
			return new KeyValuePair<string, object>("{OriginalFormat}", this.OriginalFormat);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000314C File Offset: 0x0000134C
		public IEnumerable<KeyValuePair<string, object>> GetValues(object[] values)
		{
			KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[values.Length + 1];
			for (int num = 0; num != this._valueNames.Count; num++)
			{
				array[num] = new KeyValuePair<string, object>(this._valueNames[num], values[num]);
			}
			array[array.Length - 1] = new KeyValuePair<string, object>("{OriginalFormat}", this.OriginalFormat);
			return array;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000031B4 File Offset: 0x000013B4
		private object FormatArgument(object value)
		{
			if (value == null)
			{
				return "(null)";
			}
			if (value is string)
			{
				return value;
			}
			IEnumerable enumerable = value as IEnumerable;
			if (enumerable != null)
			{
				return string.Join<object>(", ", Enumerable.Select<object, object>(Enumerable.Cast<object>(enumerable), (object o) => o ?? "(null)"));
			}
			return value;
		}

		// Token: 0x0400001C RID: 28
		private const string NullValue = "(null)";

		// Token: 0x0400001D RID: 29
		private static readonly char[] FormatDelimiters = new char[] { ',', ':' };

		// Token: 0x0400001E RID: 30
		private readonly string _format;

		// Token: 0x0400001F RID: 31
		private readonly List<string> _valueNames = new List<string>();
	}
}
