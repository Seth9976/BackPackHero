using System;
using Unity.VisualScripting.FullSerializer.Internal;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200017F RID: 383
	public class fsNullableConverter : fsConverter
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x0002AC52 File Offset: 0x00028E52
		public override bool CanProcess(Type type)
		{
			return type.Resolve().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002AC78 File Offset: 0x00028E78
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			return this.Serializer.TrySerialize(Nullable.GetUnderlyingType(storageType), instance, out serialized);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002AC8D File Offset: 0x00028E8D
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			return this.Serializer.TryDeserialize(data, Nullable.GetUnderlyingType(storageType), ref instance);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002ACA2 File Offset: 0x00028EA2
		public override object CreateInstance(fsData data, Type storageType)
		{
			return storageType;
		}
	}
}
