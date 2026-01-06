using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting.FullSerializer.Internal
{
	// Token: 0x020001AF RID: 431
	public static class fsTypeExtensions
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x00030FD3 File Offset: 0x0002F1D3
		public static string CSharpName(this Type type)
		{
			return type.CSharpName(false);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00030FDC File Offset: 0x0002F1DC
		public static string CSharpName(this Type type, bool includeNamespace, bool ensureSafeDeclarationName)
		{
			string text = type.CSharpName(includeNamespace);
			if (ensureSafeDeclarationName)
			{
				text = text.Replace('>', '_').Replace('<', '_').Replace('.', '_');
			}
			return text;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00031014 File Offset: 0x0002F214
		public static string CSharpName(this Type type, bool includeNamespace)
		{
			if (type == typeof(void))
			{
				return "void";
			}
			if (type == typeof(int))
			{
				return "int";
			}
			if (type == typeof(float))
			{
				return "float";
			}
			if (type == typeof(bool))
			{
				return "bool";
			}
			if (type == typeof(double))
			{
				return "double";
			}
			if (type == typeof(string))
			{
				return "string";
			}
			if (type.IsGenericParameter)
			{
				return type.ToString();
			}
			string text = "";
			IEnumerable<Type> enumerable = type.GetGenericArguments();
			if (type.IsNested)
			{
				text = text + type.DeclaringType.CSharpName() + ".";
				if (type.DeclaringType.GetGenericArguments().Length != 0)
				{
					enumerable = enumerable.Skip(type.DeclaringType.GetGenericArguments().Length);
				}
			}
			if (!enumerable.Any<Type>())
			{
				text += type.Name;
			}
			else
			{
				int num = type.Name.IndexOf('`');
				if (num > 0)
				{
					text += type.Name.Substring(0, num);
				}
				text = text + "<" + string.Join(",", enumerable.Select((Type t) => t.CSharpName(includeNamespace)).ToArray<string>()) + ">";
			}
			if (includeNamespace && type.Namespace != null)
			{
				text = type.Namespace + "." + text;
			}
			return text;
		}
	}
}
