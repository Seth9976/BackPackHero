using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler.</summary>
	// Token: 0x02000728 RID: 1832
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DoWorkEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DoWorkEventArgs" /> class.</summary>
		/// <param name="argument">Specifies an argument for an asynchronous operation.</param>
		// Token: 0x06003A35 RID: 14901 RVA: 0x000CA0D0 File Offset: 0x000C82D0
		public DoWorkEventArgs(object argument)
		{
			this.argument = argument;
		}

		/// <summary>Gets a value that represents the argument of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the argument of an asynchronous operation.</returns>
		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000CA0DF File Offset: 0x000C82DF
		[SRDescription("Argument passed into the worker handler from BackgroundWorker.RunWorkerAsync.")]
		public object Argument
		{
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets or sets a value that represents the result of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the result of an asynchronous operation.</returns>
		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x000CA0E7 File Offset: 0x000C82E7
		// (set) Token: 0x06003A38 RID: 14904 RVA: 0x000CA0EF File Offset: 0x000C82EF
		[SRDescription("Result from the worker function.")]
		public object Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x04002190 RID: 8592
		private object result;

		// Token: 0x04002191 RID: 8593
		private object argument;
	}
}
