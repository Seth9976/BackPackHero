using System;

namespace System.IO
{
	/// <summary>Contains information on the change that occurred.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200080F RID: 2063
	public struct WaitForChangedResult
	{
		// Token: 0x0600422B RID: 16939 RVA: 0x000E5DED File Offset: 0x000E3FED
		internal WaitForChangedResult(WatcherChangeTypes changeType, string name, string oldName, bool timedOut)
		{
			this.ChangeType = changeType;
			this.Name = name;
			this.OldName = oldName;
			this.TimedOut = timedOut;
		}

		/// <summary>Gets or sets the type of change that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x000E5E0C File Offset: 0x000E400C
		// (set) Token: 0x0600422D RID: 16941 RVA: 0x000E5E14 File Offset: 0x000E4014
		public WatcherChangeTypes ChangeType { readonly get; set; }

		/// <summary>Gets or sets the name of the file or directory that changed.</summary>
		/// <returns>The name of the file or directory that changed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x0600422E RID: 16942 RVA: 0x000E5E1D File Offset: 0x000E401D
		// (set) Token: 0x0600422F RID: 16943 RVA: 0x000E5E25 File Offset: 0x000E4025
		public string Name { readonly get; set; }

		/// <summary>Gets or sets the original name of the file or directory that was renamed.</summary>
		/// <returns>The original name of the file or directory that was renamed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06004230 RID: 16944 RVA: 0x000E5E2E File Offset: 0x000E402E
		// (set) Token: 0x06004231 RID: 16945 RVA: 0x000E5E36 File Offset: 0x000E4036
		public string OldName { readonly get; set; }

		/// <summary>Gets or sets a value indicating whether the wait operation timed out.</summary>
		/// <returns>true if the <see cref="M:System.IO.FileSystemWatcher.WaitForChanged(System.IO.WatcherChangeTypes)" /> method timed out; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06004232 RID: 16946 RVA: 0x000E5E3F File Offset: 0x000E403F
		// (set) Token: 0x06004233 RID: 16947 RVA: 0x000E5E47 File Offset: 0x000E4047
		public bool TimedOut { readonly get; set; }

		// Token: 0x04002765 RID: 10085
		internal static readonly WaitForChangedResult TimedOutResult = new WaitForChangedResult((WatcherChangeTypes)0, null, null, true);
	}
}
