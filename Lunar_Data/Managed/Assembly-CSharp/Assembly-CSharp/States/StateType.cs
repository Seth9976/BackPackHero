using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SaveSystem.States
{
	// Token: 0x020000B4 RID: 180
	[JsonConverter(typeof(StringEnumConverter))]
	public enum StateType
	{
		// Token: 0x040003AC RID: 940
		Options,
		// Token: 0x040003AD RID: 941
		Progress
	}
}
