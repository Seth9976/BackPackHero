using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000A9 RID: 169
	public interface ISerializationSurrogateProvider
	{
		// Token: 0x06000905 RID: 2309
		Type GetSurrogateType(Type type);

		// Token: 0x06000906 RID: 2310
		object GetObjectToSerialize(object obj, Type targetType);

		// Token: 0x06000907 RID: 2311
		object GetDeserializedObject(object obj, Type targetType);
	}
}
