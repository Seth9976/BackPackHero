using System;

namespace System.Threading
{
	/// <summary>Specifies the scheduling priority of a <see cref="T:System.Threading.Thread" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000296 RID: 662
	public enum ThreadPriority
	{
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with any other priority.</summary>
		// Token: 0x04001A3F RID: 6719
		Lowest,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with Normal priority and before those with Lowest priority.</summary>
		// Token: 0x04001A40 RID: 6720
		BelowNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with AboveNormal priority and before those with BelowNormal priority. Threads have Normal priority by default.</summary>
		// Token: 0x04001A41 RID: 6721
		Normal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with Highest priority and before those with Normal priority.</summary>
		// Token: 0x04001A42 RID: 6722
		AboveNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled before threads with any other priority.</summary>
		// Token: 0x04001A43 RID: 6723
		Highest
	}
}
