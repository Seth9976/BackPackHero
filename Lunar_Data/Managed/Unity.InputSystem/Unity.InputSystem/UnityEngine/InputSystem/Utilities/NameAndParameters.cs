using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000133 RID: 307
	public struct NameAndParameters
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x00050C9C File Offset: 0x0004EE9C
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x00050CA4 File Offset: 0x0004EEA4
		public string name { readonly get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00050CAD File Offset: 0x0004EEAD
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x00050CB5 File Offset: 0x0004EEB5
		public ReadOnlyArray<NamedValue> parameters { readonly get; set; }

		// Token: 0x060010EF RID: 4335 RVA: 0x00050CC0 File Offset: 0x0004EEC0
		public override string ToString()
		{
			if (this.parameters.Count == 0)
			{
				return this.name;
			}
			string text = string.Join(",", this.parameters.Select((NamedValue x) => x.ToString()).ToArray<string>());
			return this.name + "(" + text + ")";
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00050D3C File Offset: 0x0004EF3C
		public static IEnumerable<NameAndParameters> ParseMultiple(string text)
		{
			List<NameAndParameters> list = null;
			if (!NameAndParameters.ParseMultiple(text, ref list))
			{
				return Enumerable.Empty<NameAndParameters>();
			}
			return list;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00050D5C File Offset: 0x0004EF5C
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

		// Token: 0x060010F2 RID: 4338 RVA: 0x00050DB0 File Offset: 0x0004EFB0
		internal static string ParseName(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int num = 0;
			return NameAndParameters.ParseNameAndParameters(text, ref num, true).name;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00050DE0 File Offset: 0x0004EFE0
		public static NameAndParameters Parse(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int num = 0;
			return NameAndParameters.ParseNameAndParameters(text, ref num, false);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00050E08 File Offset: 0x0004F008
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
