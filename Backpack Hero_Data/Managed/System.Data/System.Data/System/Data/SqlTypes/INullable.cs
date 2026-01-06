using System;

namespace System.Data.SqlTypes
{
	/// <summary>All the <see cref="N:System.Data.SqlTypes" /> objects and structures implement the INullable interface. </summary>
	// Token: 0x020002B3 RID: 691
	public interface INullable
	{
		/// <summary>Indicates whether a structure is null. This property is read-only.</summary>
		/// <returns>
		///   <see cref="T:System.Data.SqlTypes.SqlBoolean" />true if the value of this object is null. Otherwise, false.</returns>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001E46 RID: 7750
		bool IsNull { get; }
	}
}
