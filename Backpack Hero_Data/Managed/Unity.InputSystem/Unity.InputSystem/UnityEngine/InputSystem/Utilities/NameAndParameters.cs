using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000133 RID: 307
	public struct NameAndParameters
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00050EB0 File Offset: 0x0004F0B0
		// (set) Token: 0x060010F3 RID: 4339 RVA: 0x00050EB8 File Offset: 0x0004F0B8
		public string name { readonly get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00050EC1 File Offset: 0x0004F0C1
		// (set) Token: 0x060010F5 RID: 4341 RVA: 0x00050EC9 File Offset: 0x0004F0C9
		public ReadOnlyArray<NamedValue> parameters { readonly get; set; }

		// Token: 0x060010F6 RID: 4342 RVA: 0x00050ED4 File Offset: 0x0004F0D4
		public override string ToString()
		{
			if (this.parameters.Count == 0)
			{
				return this.name;
			}
			string text = string.Join(",", this.parameters.Select((NamedValue x) => x.ToString()).ToArray<string>());
			return this.name + "(" + text + ")";
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00050F50 File Offset: 0x0004F150
		public static IEnumerable<NameAndParameters> ParseMultiple(string text)
		{
			List<NameAndParameters> list = null;
			if (!NameAndParameters.ParseMultiple(text, ref list))
			{
				return Enumerable.Empty<NameAndParameters>();
			}
			return list;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00050F70 File Offset: 0x0004F170
		internal static bool ParseMultiple(string text, ref List<NameAndParameters> list)
		{
			text = text.Trim();
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			if (list == null)
			{
				list = new List<NameAndParameters>();
			}
			else
			{
				list.Clear();
			}
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				list.Add(NameAndParameters.ParseNameAndParameters(text, ref i, false));
			}
			return true;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00050FC4 File Offset: 0x0004F1C4
		internal static string ParseName(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int num = 0;
			return NameAndParameters.ParseNameAndParameters(text, ref num, true).name;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00050FF4 File Offset: 0x0004F1F4
		public static NameAndParameters Parse(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int num = 0;
			return NameAndParameters.ParseNameAndParameters(text, ref num, false);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0005101C File Offset: 0x0004F21C
		private static NameAndParameters ParseNameAndParameters(string text, ref int index, bool nameOnly = false)
		{
			int length = text.Length;
			while (index < length && char.IsWhiteSpace(text[index]))
			{
				index++;
			}
			int num = index;
			while (index < length)
			{
				char c = text[index];
				if (c == '(' || c == ","[0] || char.IsWhiteSpace(c))
				{
					break;
				}
				index++;
			}
			if (index - num == 0)
			{
				throw new ArgumentException(string.Format("Expecting name at position {0} in '{1}'", num, text), "text");
			}
			string text2 = text.Substring(num, index - num);
			if (nameOnly)
			{
				return new NameAndParameters
				{
					name = text2
				};
			}
			while (index < length && char.IsWhiteSpace(text[index]))
			{
				index++;
			}
			NamedValue[] array = null;
			if (index < length && text[index] == '(')
			{
				index++;
				int num2 = text.IndexOf(')', index);
				if (num2 == -1)
				{
					throw new ArgumentException(string.Format("Expecting ')' after '(' at position {0} in '{1}'", index, text), "text");
				}
				array = NamedValue.ParseMultiple(text.Substring(index, num2 - index));
				index = num2 + 1;
			}
			if (index < length && (text[index] == ',' || text[index] == ';'))
			{
				index++;
			}
			return new NameAndParameters
			{
				name = text2,
				parameters = new ReadOnlyArray<NamedValue>(array)
			};
		}
	}
}
