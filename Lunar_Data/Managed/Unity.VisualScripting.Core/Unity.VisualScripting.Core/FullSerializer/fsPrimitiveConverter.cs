using System;
using Unity.VisualScripting.FullSerializer.Internal;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000180 RID: 384
	public class fsPrimitiveConverter : fsConverter
	{
		// Token: 0x06000A3E RID: 2622 RVA: 0x0002ACAD File Offset: 0x00028EAD
		public override bool CanProcess(Type type)
		{
			return type.Resolve().IsPrimitive || type == typeof(string) || type == typeof(decimal);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002ACE0 File Offset: 0x00028EE0
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002ACE3 File Offset: 0x00028EE3
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002ACE8 File Offset: 0x00028EE8
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			Type type = instance.GetType();
			if (this.Serializer.Config.Serialize64BitIntegerAsString && (type == typeof(long) || type == typeof(ulong)))
			{
				serialized = new fsData((string)Convert.ChangeType(instance, typeof(string)));
				return fsResult.Success;
			}
			if (fsPrimitiveConverter.UseBool(type))
			{
				serialized = new fsData((bool)instance);
				return fsResult.Success;
			}
			if (fsPrimitiveConverter.UseInt64(type))
			{
				serialized = new fsData((long)Convert.ChangeType(instance, typeof(long)));
				return fsResult.Success;
			}
			if (fsPrimitiveConverter.UseDouble(type))
			{
				if (instance.GetType() == typeof(float) && (float)instance != -3.4028235E+38f && (float)instance != 3.4028235E+38f && !float.IsInfinity((float)instance) && !float.IsNaN((float)instance))
				{
					serialized = new fsData((double)((decimal)((float)instance)));
					return fsResult.Success;
				}
				serialized = new fsData((double)Convert.ChangeType(instance, typeof(double)));
				return fsResult.Success;
			}
			else
			{
				if (fsPrimitiveConverter.UseString(type))
				{
					serialized = new fsData((string)Convert.ChangeType(instance, typeof(string)));
					return fsResult.Success;
				}
				serialized = null;
				string text = "Unhandled primitive type ";
				Type type2 = instance.GetType();
				return fsResult.Fail(text + ((type2 != null) ? type2.ToString() : null));
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002AE80 File Offset: 0x00029080
		public override fsResult TryDeserialize(fsData storage, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			if (fsPrimitiveConverter.UseBool(storageType))
			{
				fsResult fsResult2;
				fsResult = (fsResult2 = fsResult + base.CheckType(storage, fsDataType.Boolean));
				if (fsResult2.Succeeded)
				{
					instance = storage.AsBool;
				}
				return fsResult;
			}
			if (fsPrimitiveConverter.UseDouble(storageType) || fsPrimitiveConverter.UseInt64(storageType))
			{
				if (storage.IsDouble)
				{
					instance = Convert.ChangeType(storage.AsDouble, storageType);
				}
				else if (storage.IsInt64)
				{
					instance = Convert.ChangeType(storage.AsInt64, storageType);
				}
				else
				{
					if (!this.Serializer.Config.Serialize64BitIntegerAsString || !storage.IsString || (!(storageType == typeof(long)) && !(storageType == typeof(ulong))))
					{
						return fsResult.Fail(string.Concat(new string[]
						{
							base.GetType().Name,
							" expected number but got ",
							storage.Type.ToString(),
							" in ",
							(storage != null) ? storage.ToString() : null
						}));
					}
					instance = Convert.ChangeType(storage.AsString, storageType);
				}
				return fsResult.Success;
			}
			if (fsPrimitiveConverter.UseString(storageType))
			{
				fsResult fsResult2;
				fsResult = (fsResult2 = fsResult + base.CheckType(storage, fsDataType.String));
				if (fsResult2.Succeeded)
				{
					string asString = storage.AsString;
					if (storageType == typeof(char))
					{
						if (storageType == typeof(char))
						{
							if (asString.Length == 1)
							{
								instance = asString[0];
							}
							else
							{
								instance = '\0';
							}
						}
					}
					else
					{
						instance = asString;
					}
				}
				return fsResult;
			}
			return fsResult.Fail(base.GetType().Name + ": Bad data; expected bool, number, string, but got " + ((storage != null) ? storage.ToString() : null));
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002B05D File Offset: 0x0002925D
		private static bool UseBool(Type type)
		{
			return type == typeof(bool);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002B070 File Offset: 0x00029270
		private static bool UseInt64(Type type)
		{
			return type == typeof(sbyte) || type == typeof(byte) || type == typeof(short) || type == typeof(ushort) || type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(ulong);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002B10D File Offset: 0x0002930D
		private static bool UseDouble(Type type)
		{
			return type == typeof(float) || type == typeof(double) || type == typeof(decimal);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002B145 File Offset: 0x00029345
		private static bool UseString(Type type)
		{
			return type == typeof(string) || type == typeof(char);
		}
	}
}
