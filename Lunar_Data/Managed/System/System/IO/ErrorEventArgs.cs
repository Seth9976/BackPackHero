using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200081F RID: 2079
	public class ErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.ErrorEventArgs" /> class.</summary>
		/// <param name="exception">An <see cref="T:System.Exception" /> that represents the error that occurred. </param>
		// Token: 0x0600426B RID: 17003 RVA: 0x000E6E7A File Offset: 0x000E507A
		public ErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that represents the error that occurred.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the error that occurred.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600426C RID: 17004 RVA: 0x000E6E89 File Offset: 0x000E5089
		public virtual Exception GetException()
		{
			return this.exception;
		}

		// Token: 0x040027A2 RID: 10146
		private Exception exception;
	}
}
