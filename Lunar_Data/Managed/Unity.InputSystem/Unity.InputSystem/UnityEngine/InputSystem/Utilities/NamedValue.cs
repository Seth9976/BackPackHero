using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000134 RID: 308
	public struct NamedValue : IEquatable<NamedValue>
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00050F70 File Offset: 0x0004F170
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x00050F78 File Offset: 0x0004F178
		public string name { readonly get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00050F81 File Offset: 0x0004F181
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x00050F89 File Offset: 0x0004F189
		public PrimitiveValue value { readonly get; set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00050F94 File Offset: 0x0004F194
		public TypeCode type
		{
			get
			{
				return this.value.type;
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00050FB0 File Offset: 0x0004F1B0
		public NamedValue ConvertTo(TypeCode type)
		{
			return new NamedValue
			{
				name = this.name,
				value = this.value.ConvertTo(type)
			};
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00050FEC File Offset: 0x0004F1EC
		public static NamedValue From<TValue>(string name, TValue value) where TValue : struct
		{
			return new NamedValue
			{
				name = name,
				value = PrimitiveValue.From<TValue>(value)
			};
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00051017 File Offset: 0x0004F217
		public override string ToString()
		{
			return string.Format("{0}={1}", this.name, this.value);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00051034 File Offset: 0x0004F234
		public bool Equals(NamedValue other)
		{
			return string.Equals(this.name, other.name, StringComparison.InvariantCultureIgnoreCase) && this.value == other.value;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00051060 File Offset: 0x0004F260
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is NamedValue)
			{
				NamedValue namedValue = (NamedValue)obj;
				return this.Equals(namedValue);
			}
			return false;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0005108C File Offset: 0x0004F28C
		public override int GetHashCode()
		{
			return (((this.name != null) ? this.name.GetHashCode() : 0) * 397) ^ this.value.GetHashCode();
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000510CA File Offset: 0x0004F2CA
		public static bool operator ==(NamedValue left, NamedValue right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000510D4 File Offset: 0x0004F2D4
		public static bool operator !=(NamedValue left, NamedValue right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000510E4 File Offset: 0x0004F2E4
		public static NamedValue[] ParseMultiple(string parameterString)
		{
			if (parameterString == null)
			{
				throw new ArgumentNullException("parameterString");
			}
			parameterString = parameterString.Trim();
			if (string.IsNullOrEmpty(parameterString))
			{
				return null;
			}
			int num = parameterString.CountOccurrences(","[0]) + 1;
			NamedValue[] array = new NamedValue[num];
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				NamedValue namedValue = NamedValue.ParseParameter(parameterString, ref num2);
				array[i] = namedValue;
			}
			return array;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00051150 File Offset: 0x0004F350
		public static NamedValue Parse(string str)
		{
			int num = 0;
			return NamedValue.ParseParameter(str, ref num);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00051168 File Offset: 0x0004F368
		private static NamedValue ParseParameter(string parameterString, ref int index)
		{
			NamedValue namedValue = default(NamedValue);
			int length = parameterString.Length;
			while (index < length && char.IsWhiteSpace(parameterString[index]))
			{
				index++;
			}
			int num = index;
			while (index < length)
			{
				char c = parameterString[index];
				if (c == '=' || c == ","[0] || char.IsWhiteSpace(c))
				{
					break;
				}
				index++;
			}
			namedValue.name = parameterString.Substring(num, index - num);
			while (index < length && char.IsWhiteSpace(parameterString[index]))
			{
				index++;
			}
			if (index == length || parameterString[index] != '=')
			{
				namedValue.value = true;
			}
			else
			{
				index++;
				while (index < length && char.IsWhiteSpace(parameterString[index]))
				{
					index++;
				}
				int num2 = index;
				while (index < length && parameterString[index] != ","[0] && !char.IsWhiteSpace(parameterString[index]))
				{
					index++;
				}
				string text = parameterString.Substring(num2, index - num2);
				namedValue.value = PrimitiveValue.FromString(text);
			}
			if (index < length && parameterString[index] == ","[0])
			{
				index++;
			}
			return namedValue;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000512B8 File Offset: 0x0004F4B8
		public void ApplyToObject(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			Type type = instance.GetType();
			FieldInfo field = type.GetField(this.name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new ArgumentException(string.Concat(new string[] { "Cannot find public field '", this.name, "' in '", type.Name, "' (while trying to apply parameter)" }), "instance");
			}
			TypeCode typeCode = Type.GetTypeCode(field.FieldType);
			field.SetValue(instance, this.value.ConvertTo(typeCode).ToObject());
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0005135C File Offset: 0x0004F55C
		public static void ApplyAllToObject<TParameterList>(object instance, TParameterList parameters) where TParameterList : IEnumerable<NamedValue>
		{
			foreach (NamedValue namedValue in parameters)
			{
				namedValue.ApplyToObject(instance);
			}
		}

		// Token: 0x040006C3 RID: 1731
		public const string Separator = ",";
	}
}
