using System;
using System.Linq;
using System.Text;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000128 RID: 296
	internal static class CSharpCodeHelpers
	{
		// Token: 0x0600107F RID: 4223 RVA: 0x0004E824 File Offset: 0x0004CA24
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

		// Token: 0x06001080 RID: 4224 RVA: 0x0004E874 File Offset: 0x0004CA74
		public static bool IsEmptyOrProperIdentifier(string name)
		{
			return string.IsNullOrEmpty(name) || CSharpCodeHelpers.IsProperIdentifier(name);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0004E886 File Offset: 0x0004CA86
		public static bool IsEmptyOrProperNamespaceName(string name)
		{
			return string.IsNullOrEmpty(name) || name.Split('.', StringSplitOptions.None).All(new Func<string, bool>(CSharpCodeHelpers.IsProperIdentifier));
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0004E8AC File Offset: 0x0004CAAC
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

		// Token: 0x06001083 RID: 4227 RVA: 0x0004E964 File Offset: 0x0004CB64
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
