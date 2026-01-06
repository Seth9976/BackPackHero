using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Internal
{
	// Token: 0x02000009 RID: 9
	internal static class TypeNameHelper
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000022C0 File Offset: 0x000004C0
		public static string GetTypeDisplayName(object item, bool fullName = true)
		{
			if (item != null)
			{
				return TypeNameHelper.GetTypeDisplayName(item.GetType(), fullName, false, true, '+');
			}
			return null;
		}

		/// <summary>
		/// Pretty print a type name.
		/// </summary>
		/// <param name="type">The <see cref="T:System.Type" />.</param>
		/// <param name="fullName"><c>true</c> to print a fully qualified name.</param>
		/// <param name="includeGenericParameterNames"><c>true</c> to include generic parameter names.</param>
		/// <param name="includeGenericParameters"><c>true</c> to include generic parameters.</param>
		/// <param name="nestedTypeDelimiter">Character to use as a delimiter in nested type names</param>
		/// <returns>The pretty printed type name.</returns>
		// Token: 0x06000015 RID: 21 RVA: 0x000022D8 File Offset: 0x000004D8
		public static string GetTypeDisplayName(Type type, bool fullName = true, bool includeGenericParameterNames = false, bool includeGenericParameters = true, char nestedTypeDelimiter = '+')
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = stringBuilder;
			TypeNameHelper.DisplayNameOptions displayNameOptions = new TypeNameHelper.DisplayNameOptions(fullName, includeGenericParameterNames, includeGenericParameters, nestedTypeDelimiter);
			TypeNameHelper.ProcessType(stringBuilder2, type, in displayNameOptions);
			return stringBuilder.ToString();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002308 File Offset: 0x00000508
		private static void ProcessType(StringBuilder builder, Type type, in TypeNameHelper.DisplayNameOptions options)
		{
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				TypeNameHelper.ProcessGenericType(builder, type, genericArguments, genericArguments.Length, in options);
				return;
			}
			if (type.IsArray)
			{
				TypeNameHelper.ProcessArrayType(builder, type, in options);
				return;
			}
			string text;
			if (TypeNameHelper._builtInTypeNames.TryGetValue(type, ref text))
			{
				builder.Append(text);
				return;
			}
			if (type.IsGenericParameter)
			{
				if (options.IncludeGenericParameterNames)
				{
					builder.Append(type.Name);
					return;
				}
			}
			else
			{
				string text2 = (options.FullName ? type.FullName : type.Name);
				builder.Append(text2);
				if (options.NestedTypeDelimiter != '+')
				{
					builder.Replace('+', options.NestedTypeDelimiter, builder.Length - text2.Length, text2.Length);
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023C4 File Offset: 0x000005C4
		private static void ProcessArrayType(StringBuilder builder, Type type, in TypeNameHelper.DisplayNameOptions options)
		{
			Type type2 = type;
			while (type2.IsArray)
			{
				type2 = type2.GetElementType();
			}
			TypeNameHelper.ProcessType(builder, type2, in options);
			while (type.IsArray)
			{
				builder.Append('[');
				builder.Append(',', type.GetArrayRank() - 1);
				builder.Append(']');
				type = type.GetElementType();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002424 File Offset: 0x00000624
		private static void ProcessGenericType(StringBuilder builder, Type type, Type[] genericArguments, int length, in TypeNameHelper.DisplayNameOptions options)
		{
			int num = 0;
			if (type.IsNested)
			{
				num = type.DeclaringType.GetGenericArguments().Length;
			}
			if (options.FullName)
			{
				if (type.IsNested)
				{
					TypeNameHelper.ProcessGenericType(builder, type.DeclaringType, genericArguments, num, in options);
					builder.Append(options.NestedTypeDelimiter);
				}
				else if (!string.IsNullOrEmpty(type.Namespace))
				{
					builder.Append(type.Namespace);
					builder.Append('.');
				}
			}
			int num2 = type.Name.IndexOf('`');
			if (num2 <= 0)
			{
				builder.Append(type.Name);
				return;
			}
			builder.Append(type.Name, 0, num2);
			if (options.IncludeGenericParameters)
			{
				builder.Append('<');
				for (int i = num; i < length; i++)
				{
					TypeNameHelper.ProcessType(builder, genericArguments[i], in options);
					if (i + 1 != length)
					{
						builder.Append(',');
						if (options.IncludeGenericParameterNames || !genericArguments[i + 1].IsGenericParameter)
						{
							builder.Append(' ');
						}
					}
				}
				builder.Append('>');
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000252C File Offset: 0x0000072C
		// Note: this type is marked as 'beforefieldinit'.
		static TypeNameHelper()
		{
			Dictionary<Type, string> dictionary = new Dictionary<Type, string>();
			dictionary.Add(typeof(void), "void");
			dictionary.Add(typeof(bool), "bool");
			dictionary.Add(typeof(byte), "byte");
			dictionary.Add(typeof(char), "char");
			dictionary.Add(typeof(decimal), "decimal");
			dictionary.Add(typeof(double), "double");
			dictionary.Add(typeof(float), "float");
			dictionary.Add(typeof(int), "int");
			dictionary.Add(typeof(long), "long");
			dictionary.Add(typeof(object), "object");
			dictionary.Add(typeof(sbyte), "sbyte");
			dictionary.Add(typeof(short), "short");
			dictionary.Add(typeof(string), "string");
			dictionary.Add(typeof(uint), "uint");
			dictionary.Add(typeof(ulong), "ulong");
			dictionary.Add(typeof(ushort), "ushort");
			TypeNameHelper._builtInTypeNames = dictionary;
		}

		// Token: 0x04000006 RID: 6
		private const char DefaultNestedTypeDelimiter = '+';

		// Token: 0x04000007 RID: 7
		private static readonly Dictionary<Type, string> _builtInTypeNames;

		// Token: 0x02000020 RID: 32
		private readonly struct DisplayNameOptions
		{
			// Token: 0x060000A1 RID: 161 RVA: 0x00003355 File Offset: 0x00001555
			public DisplayNameOptions(bool fullName, bool includeGenericParameterNames, bool includeGenericParameters, char nestedTypeDelimiter)
			{
				this.FullName = fullName;
				this.IncludeGenericParameters = includeGenericParameters;
				this.IncludeGenericParameterNames = includeGenericParameterNames;
				this.NestedTypeDelimiter = nestedTypeDelimiter;
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003374 File Offset: 0x00001574
			public bool FullName { get; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000337C File Offset: 0x0000157C
			public bool IncludeGenericParameters { get; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003384 File Offset: 0x00001584
			public bool IncludeGenericParameterNames { get; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000338C File Offset: 0x0000158C
			public char NestedTypeDelimiter { get; }
		}
	}
}
