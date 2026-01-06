using System;

namespace System.Threading
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000180 RID: 384
	public class ThreadExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadExceptionEventArgs" /> class.</summary>
		/// <param name="t">The <see cref="T:System.Exception" /> that occurred. </param>
		// Token: 0x06000A51 RID: 2641 RVA: 0x0002D061 File Offset: 0x0002B261
		public ThreadExceptionEventArgs(Exception t)
		{
			this.exception = t;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that occurred.</summary>
		/// <returns>The <see cref="T:System.Exception" /> that occurred.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0002D070 File Offset: 0x0002B270
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040006DB RID: 1755
		private Exception exception;
	}
}
