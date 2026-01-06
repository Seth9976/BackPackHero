using System;

namespace System.Data.Common
{
	/// <summary>Specifies what types of Transact-SQL join statements are supported by the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200036F RID: 879
	[Flags]
	public enum SupportedJoinOperators
	{
		/// <summary>The data source does not support join queries.</summary>
		// Token: 0x040019D2 RID: 6610
		None = 0,
		/// <summary>The data source supports inner joins.</summary>
		// Token: 0x040019D3 RID: 6611
		Inner = 1,
		/// <summary>The data source supports left outer joins.</summary>
		// Token: 0x040019D4 RID: 6612
		LeftOuter = 2,
		/// <summary>The data source supports right outer joins.</summary>
		// Token: 0x040019D5 RID: 6613
		RightOuter = 4,
		/// <summary>The data source supports full outer joins.</summary>
		// Token: 0x040019D6 RID: 6614
		FullOuter = 8
	}
}
