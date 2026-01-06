using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.VisualScripting.FullSerializer.Internal;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000179 RID: 377
	public class fsEnumConverter : fsConverter
	{
		// Token: 0x06000A12 RID: 2578 RVA: 0x0002A23D File Offset: 0x0002843D
		public override bool CanProcess(Type type)
		{
			return type.Resolve().IsEnum;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002A24A File Offset: 0x0002844A
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002A24D File Offset: 0x0002844D
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002A250 File Offset: 0x00028450
		public override object CreateInstance(fsData data, Type storageType)
		{
			return Enum.ToObject(storageType, 0);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002A260 File Offset: 0x00028460
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			if (this.Serializer.Config.SerializeEnumsAsInteger)
			{
				serialized = new fsData(Convert.ToInt64(instance));
			}
			else if (fsPortableReflection.GetAttribute<FlagsAttribute>(storageType) != null)
			{
				long num = Convert.ToInt64(instance);
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (object obj in Enum.GetValues(storageType))
				{
					long num2 = Convert.ToInt64(obj);
					if (num2 != 0L && (num & num2) == num2)
					{
						if (!flag)
						{
							stringBuilder.Append(",");
						}
						flag = false;
						stringBuilder.Append(obj.ToString());
					}
				}
				serialized = new fsData(stringBuilder.ToString());
			}
			else
			{
				serialized = new fsData(Enum.GetName(storageType, instance));
			}
			return fsResult.Success;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002A348 File Offset: 0x00028548
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (data.IsString)
			{
				string[] array = data.AsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (!fsEnumConverter.ArrayContains<string>(Enum.GetNames(storageType), text))
					{
						Enum @enum;
						if (!Enum.GetValues(storageType).Cast<Enum>().SelectMany((Enum x) => from attr in x.GetAttributeOfEnumMember<RenamedFromAttribute>()
							select new ValueTuple<Enum, string>(x, attr.previousName))
							.ToDictionary(([TupleElementNames(new string[] { "enumMember", "previousName" })] ValueTuple<Enum, string> x) => x.Item2, ([TupleElementNames(new string[] { "enumMember", "previousName" })] ValueTuple<Enum, string> x) => x.Item1)
							.TryGetValue(text, out @enum))
						{
							return fsResult.Fail("Cannot find enum name " + text + " on type " + ((storageType != null) ? storageType.ToString() : null));
						}
						array[i] = @enum.ToString();
					}
				}
				if (Enum.GetUnderlyingType(storageType) == typeof(ulong))
				{
					ulong num = 0UL;
					foreach (string text2 in array)
					{
						ulong num2 = (ulong)Convert.ChangeType(Enum.Parse(storageType, text2), typeof(ulong));
						num |= num2;
					}
					instance = Enum.ToObject(storageType, num);
				}
				else
				{
					long num3 = 0L;
					foreach (string text3 in array)
					{
						long num4 = (long)Convert.ChangeType(Enum.Parse(storageType, text3), typeof(long));
						num3 |= num4;
					}
					instance = Enum.ToObject(storageType, num3);
				}
				return fsResult.Success;
			}
			if (data.IsInt64)
			{
				int num5 = (int)data.AsInt64;
				instance = Enum.ToObject(storageType, num5);
				return fsResult.Success;
			}
			return fsResult.Fail(string.Format("EnumConverter encountered an unknown JSON data type for {0}: {1}", storageType, data.Type));
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002A54C File Offset: 0x0002874C
		private static bool ArrayContains<T>(T[] values, T value)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (EqualityComparer<T>.Default.Equals(values[i], value))
				{
					return true;
				}
			}
			return false;
		}
	}
}
