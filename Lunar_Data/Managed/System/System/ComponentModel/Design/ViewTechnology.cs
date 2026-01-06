using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers for a set of technologies that designer hosts support.</summary>
	// Token: 0x0200078E RID: 1934
	public enum ViewTechnology
	{
		/// <summary>Represents a mode in which the view object is passed directly to the development environment. </summary>
		// Token: 0x040025F0 RID: 9712
		[Obsolete("This value has been deprecated. Use ViewTechnology.Default instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Passthrough,
		/// <summary>Represents a mode in which a Windows Forms control object provides the display for the root designer. </summary>
		// Token: 0x040025F1 RID: 9713
		[Obsolete("This value has been deprecated. Use ViewTechnology.Default instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		WindowsForms,
		/// <summary>Specifies the default view technology support. </summary>
		// Token: 0x040025F2 RID: 9714
		Default
	}
}
