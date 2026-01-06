using System;
using System.Reflection;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000192 RID: 402
	public class fsConfig
	{
		// Token: 0x0400026F RID: 623
		public Type[] SerializeAttributes = new Type[]
		{
			typeof(SerializeField),
			typeof(fsPropertyAttribute),
			typeof(SerializeAttribute),
			typeof(SerializeAsAttribute)
		};

		// Token: 0x04000270 RID: 624
		public Type[] IgnoreSerializeAttributes = new Type[]
		{
			typeof(NonSerializedAttribute),
			typeof(fsIgnoreAttribute),
			typeof(DoNotSerializeAttribute)
		};

		// Token: 0x04000271 RID: 625
		public fsMemberSerialization DefaultMemberSerialization = fsMemberSerialization.Default;

		// Token: 0x04000272 RID: 626
		public Func<string, MemberInfo, string> GetJsonNameFromMemberName = (string name, MemberInfo info) => name;

		// Token: 0x04000273 RID: 627
		public bool EnablePropertySerialization = true;

		// Token: 0x04000274 RID: 628
		public bool SerializeNonAutoProperties;

		// Token: 0x04000275 RID: 629
		public bool SerializeNonPublicSetProperties = true;

		// Token: 0x04000276 RID: 630
		public string CustomDateTimeFormatString;

		// Token: 0x04000277 RID: 631
		public bool Serialize64BitIntegerAsString;

		// Token: 0x04000278 RID: 632
		public bool SerializeEnumsAsInteger;
	}
}
