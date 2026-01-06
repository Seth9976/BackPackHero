using System;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000D3 RID: 211
	internal enum QueryOperator
	{
		// Token: 0x040003CF RID: 975
		None,
		// Token: 0x040003D0 RID: 976
		Equals,
		// Token: 0x040003D1 RID: 977
		NotEquals,
		// Token: 0x040003D2 RID: 978
		Exists,
		// Token: 0x040003D3 RID: 979
		LessThan,
		// Token: 0x040003D4 RID: 980
		LessThanOrEquals,
		// Token: 0x040003D5 RID: 981
		GreaterThan,
		// Token: 0x040003D6 RID: 982
		GreaterThanOrEquals,
		// Token: 0x040003D7 RID: 983
		And,
		// Token: 0x040003D8 RID: 984
		Or,
		// Token: 0x040003D9 RID: 985
		RegexEquals,
		// Token: 0x040003DA RID: 986
		StrictEquals,
		// Token: 0x040003DB RID: 987
		StrictNotEquals
	}
}
