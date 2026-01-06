using System;
using Unity;

namespace System.Timers
{
	/// <summary>Provides data for the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
	// Token: 0x02000195 RID: 405
	public class ElapsedEventArgs : EventArgs
	{
		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002DB70 File Offset: 0x0002BD70
		internal ElapsedEventArgs(DateTime time)
		{
			this.time = time;
		}

		/// <summary>Gets the time the <see cref="E:System.Timers.Timer.Elapsed" /> event was raised.</summary>
		/// <returns>The time the <see cref="E:System.Timers.Timer.Elapsed" /> event was raised.</returns>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0002DB7F File Offset: 0x0002BD7F
		public DateTime SignalTime
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00013B26 File Offset: 0x00011D26
		internal ElapsedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000720 RID: 1824
		private DateTime time;
	}
}
