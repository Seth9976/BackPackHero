using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Specifies the attributes of a property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000107 RID: 263
	[Flags]
	[Obsolete("PropertyAttributes has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum PropertyAttributes
	{
		/// <summary>The property is not supported by the provider.</summary>
		// Token: 0x04000992 RID: 2450
		NotSupported = 0,
		/// <summary>The user must specify a value for this property before the data source is initialized.</summary>
		// Token: 0x04000993 RID: 2451
		Required = 1,
		/// <summary>The user does not need to specify a value for this property before the data source is initialized.</summary>
		// Token: 0x04000994 RID: 2452
		Optional = 2,
		/// <summary>The user can read the property.</summary>
		// Token: 0x04000995 RID: 2453
		Read = 512,
		/// <summary>The user can write to the property.</summary>
		// Token: 0x04000996 RID: 2454
		Write = 1024
	}
}
