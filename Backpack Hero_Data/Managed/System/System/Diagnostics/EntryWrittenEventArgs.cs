using System;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000256 RID: 598
	public class EntryWrittenEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class.</summary>
		// Token: 0x06001270 RID: 4720 RVA: 0x0004FB40 File Offset: 0x0004DD40
		public EntryWrittenEventArgs()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class with the specified event log entry.</summary>
		/// <param name="entry">An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written. </param>
		// Token: 0x06001271 RID: 4721 RVA: 0x0004FB49 File Offset: 0x0004DD49
		public EntryWrittenEventArgs(EventLogEntry entry)
		{
			this.entry = entry;
		}

		/// <summary>Gets the event log entry that was written to the log.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written to the event log.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0004FB58 File Offset: 0x0004DD58
		public EventLogEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x04000AA6 RID: 2726
		private EventLogEntry entry;
	}
}
