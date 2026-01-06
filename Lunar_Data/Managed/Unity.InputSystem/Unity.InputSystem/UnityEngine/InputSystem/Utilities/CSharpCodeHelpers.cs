using System;
using System.Linq;
using System.Text;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000128 RID: 296
	internal static class CSharpCodeHelpers
	{
		// Token: 0x0600107A RID: 4218 RVA: 0x0004E7D8 File Offset: 0x0004C9D8
		public static bool IsProperIdentifier(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			if (char.IsDigit(name[0]))
			{
				return false;
			}
			foreach (char c in name)
			{
				if (!char.IsLetterOrDigit(c) && c != '_')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0004E828 File Offset: 0x0004CA28
		public static bool IsEmptyOrProperIdentifier(string name)
		{
			return string.IsNullOrEmpty(name) || CSharpCodeHelpers.IsProperIdentifier(name);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0004E83A File Offset: 0x0004CA3A
		public static bool IsEmptyOrProperNamespaceName(string name)
		{
			return string.IsNullOrEmpty(name) || name.Split('.', StringSplitOptions.None).All(new Func<string, bool>(CSharpCodeHelpers.IsProperIdentifier));
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0004E860 File Offset: 0x0004CA60
		public static string MakeIdentifier(string name, string suffix = "")
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (char.IsDigit(name[0]))
			{
				name = "_" + name;
			}
			bool flag = false;
			foreach (char c in name)
			{
				if (!char.IsLetterOrDigit(c) && c != '_')
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (char c2 in name)
				{
					if (char.IsLetterOrDigit(c2) || c2 == '_')
					{
						stringBuilder.Append(c2);
					}
				}
				name = stringBuilder.ToString();
			}
			return name + suffix;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0004E918 File Offset: 0x0004CB18
		public static string MakeTypeName(string name, string suffix = "")
		{
			string text = CSharpCodeHelpers.MakeIdentifier(name, suffix);
			if (char.IsLower(text[0]))
			{
				text = char.ToUpper(text[0]).ToString() + text.Substring(1);
			}
			return text;
		}
	}
}
