using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000134 RID: 308
	public struct NamedValue : IEquatable<NamedValue>
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00051184 File Offset: 0x0004F384
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x0005118C File Offset: 0x0004F38C
		public string name { readonly get; set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00051195 File Offset: 0x0004F395
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x0005119D File Offset: 0x0004F39D
		public PrimitiveValue value { readonly get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x000511A8 File Offset: 0x0004F3A8
		public TypeCode type
		{
			get
			{
				return this.value.type;
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000511C4 File Offset: 0x0004F3C4
		public NamedValue ConvertTo(TypeCode type)
		{
			return new NamedValue
			{
				name = this.name,
				value = this.value.ConvertTo(type)
			};
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00051200 File Offset: 0x0004F400
		public static NamedValue From<TValue>(string name, TValue value) where TValue : struct
		{
			return new NamedValue
			{
				name = name,
				value = PrimitiveValue.From<TValue>(value)
			};
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0005122B File Offset: 0x0004F42B
		public override string ToString()
		{
			return string.Format("{0}={1}", this.name, this.value);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00051248 File Offset: 0x0004F448
		public bool Equals(NamedValue other)
		{
			return string.Equals(this.name, other.name, StringComparison.InvariantCultureIgnoreCase) && this.value == other.value;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00051274 File Offset: 0x0004F474
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

		// Token: 0x06001106 RID: 4358 RVA: 0x000512A0 File Offset: 0x0004F4A0
		public override int GetHashCode()
		{
			return (((this.name != null) ? this.name.GetHashCode() : 0) * 397) ^ this.value.GetHashCode();
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x000512DE File Offset: 0x0004F4DE
		public static bool operator ==(NamedValue left, NamedValue right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000512E8 File Offset: 0x0004F4E8
		public static bool operator !=(NamedValue left, NamedValue right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000512F8 File Offset: 0x0004F4F8
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

		// Token: 0x0600110A RID: 4362 RVA: 0x00051364 File Offset: 0x0004F564
		public static NamedValue Parse(string str)
		{
			int num = 0;
			return NamedValue.ParseParameter(str, ref num);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0005137C File Offset: 0x0004F57C
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

		// Token: 0x0600110C RID: 4364 RVA: 0x000514CC File Offset: 0x0004F6CC
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

		// Token: 0x0600110D RID: 4365 RVA: 0x00051570 File Offset: 0x0004F770
		public static void ApplyAllToObject<TParameterList>(object instance, TParameterList parameters) where TParameterList : IEnumerable<NamedValue>
		{
			foreach (NamedValue namedValue in parameters)
			{
				namedValue.ApplyToObject(instance);
			}
		}

		// Token: 0x040006C4 RID: 1732
		public const string Separator = ",";
	}
}
