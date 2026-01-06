using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.ComponentModel
{
	/// <summary>Executes an operation on a separate thread.</summary>
	// Token: 0x02000678 RID: 1656
	[DefaultEvent("DoWork")]
	public class BackgroundWorker : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BackgroundWorker" /> class.</summary>
		// Token: 0x0600353E RID: 13630 RVA: 0x000BE750 File Offset: 0x000BC950
		public BackgroundWorker()
		{
			this._operationCompleted = new SendOrPostCallback(this.AsyncOperationCompleted);
			this._progressReporter = new SendOrPostCallback(this.ProgressReporter);
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000BE77C File Offset: 0x000BC97C
		private void AsyncOperationCompleted(object arg)
		{
			this._isRunning = false;
			this._cancellationPending = false;
			this.OnRunWorkerCompleted((RunWorkerCompletedEventArgs)arg);
		}

		/// <summary>Gets a value indicating whether the application has requested cancellation of a background operation.</summary>
		/// <returns>true if the application has requested cancellation of a background operation; otherwise, false. The default is false.</returns>
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000BE798 File Offset: 0x000BC998
		public bool CancellationPending
		{
			get
			{
				return this._cancellationPending;
			}
		}

		/// <summary>Requests cancellation of a pending background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.WorkerSupportsCancellation" /> is false. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003541 RID: 13633 RVA: 0x000BE7A0 File Offset: 0x000BC9A0
		public void CancelAsync()
		{
			if (!this.WorkerSupportsCancellation)
			{
				throw new InvalidOperationException("This BackgroundWorker states that it doesn't support cancellation. Modify WorkerSupportsCancellation to state that it does support cancellation.");
			}
			this._cancellationPending = true;
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync" /> is called.</summary>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06003542 RID: 13634 RVA: 0x000BE7BC File Offset: 0x000BC9BC
		// (remove) Token: 0x06003543 RID: 13635 RVA: 0x000BE7F4 File Offset: 0x000BC9F4
		public event DoWorkEventHandler DoWork;

		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation.</summary>
		/// <returns>true, if the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation; otherwise, false.</returns>
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000BE829 File Offset: 0x000BCA29
		public bool IsBusy
		{
			get
			{
				return this._isRunning;
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003545 RID: 13637 RVA: 0x000BE834 File Offset: 0x000BCA34
		protected virtual void OnDoWork(DoWorkEventArgs e)
		{
			DoWorkEventHandler doWork = this.DoWork;
			if (doWork != null)
			{
				doWork(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.RunWorkerCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003546 RID: 13638 RVA: 0x000BE854 File Offset: 0x000BCA54
		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler runWorkerCompleted = this.RunWorkerCompleted;
			if (runWorkerCompleted != null)
			{
				runWorkerCompleted(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003547 RID: 13639 RVA: 0x000BE874 File Offset: 0x000BCA74
		protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChanged = this.ProgressChanged;
			if (progressChanged != null)
			{
				progressChanged(this, e);
			}
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.ReportProgress(System.Int32)" /> is called.</summary>
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06003548 RID: 13640 RVA: 0x000BE894 File Offset: 0x000BCA94
		// (remove) Token: 0x06003549 RID: 13641 RVA: 0x000BE8CC File Offset: 0x000BCACC
		public event ProgressChangedEventHandler ProgressChanged;

		// Token: 0x0600354A RID: 13642 RVA: 0x000BE901 File Offset: 0x000BCB01
		private void ProgressReporter(object arg)
		{
			this.OnProgressChanged((ProgressChangedEventArgs)arg);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to false. </exception>
		// Token: 0x0600354B RID: 13643 RVA: 0x000BE90F File Offset: 0x000BCB0F
		public void ReportProgress(int percentProgress)
		{
			this.ReportProgress(percentProgress, null);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete.</param>
		/// <param name="userState">The state object passed to <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to false. </exception>
		// Token: 0x0600354C RID: 13644 RVA: 0x000BE91C File Offset: 0x000BCB1C
		public void ReportProgress(int percentProgress, object userState)
		{
			if (!this.WorkerReportsProgress)
			{
				throw new InvalidOperationException("This BackgroundWorker states that it doesn't report progress. Modify WorkerReportsProgress to state that it does report progress.");
			}
			ProgressChangedEventArgs progressChangedEventArgs = new ProgressChangedEventArgs(percentProgress, userState);
			if (this._asyncOperation != null)
			{
				this._asyncOperation.Post(this._progressReporter, progressChangedEventArgs);
				return;
			}
			this._progressReporter(progressChangedEventArgs);
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600354D RID: 13645 RVA: 0x000BE96B File Offset: 0x000BCB6B
		public void RunWorkerAsync()
		{
			this.RunWorkerAsync(null);
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <param name="argument">A parameter for use by the background operation to be executed in the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true. </exception>
		// Token: 0x0600354E RID: 13646 RVA: 0x000BE974 File Offset: 0x000BCB74
		public void RunWorkerAsync(object argument)
		{
			if (this._isRunning)
			{
				throw new InvalidOperationException("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently.");
			}
			this._isRunning = true;
			this._cancellationPending = false;
			this._asyncOperation = AsyncOperationManager.CreateOperation(null);
			Task.Factory.StartNew(delegate(object arg)
			{
				this.WorkerThreadStart(arg);
			}, argument, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Occurs when the background operation has completed, has been canceled, or has raised an exception.</summary>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600354F RID: 13647 RVA: 0x000BE9D4 File Offset: 0x000BCBD4
		// (remove) Token: 0x06003550 RID: 13648 RVA: 0x000BEA0C File Offset: 0x000BCC0C
		public event RunWorkerCompletedEventHandler RunWorkerCompleted;

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> can report progress updates.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports progress updates; otherwise false. The default is false.</returns>
		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x000BEA41 File Offset: 0x000BCC41
		// (set) Token: 0x06003552 RID: 13650 RVA: 0x000BEA49 File Offset: 0x000BCC49
		public bool WorkerReportsProgress
		{
			get
			{
				return this._workerReportsProgress;
			}
			set
			{
				this._workerReportsProgress = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports asynchronous cancellation.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports cancellation; otherwise false. The default is false.</returns>
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x000BEA52 File Offset: 0x000BCC52
		// (set) Token: 0x06003554 RID: 13652 RVA: 0x000BEA5A File Offset: 0x000BCC5A
		public bool WorkerSupportsCancellation
		{
			get
			{
				return this._canCancelWorker;
			}
			set
			{
				this._canCancelWorker = value;
			}
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000BEA64 File Offset: 0x000BCC64
		private void WorkerThreadStart(object argument)
		{
			object obj = null;
			Exception ex = null;
			bool flag = false;
			try
			{
				DoWorkEventArgs doWorkEventArgs = new DoWorkEventArgs(argument);
				this.OnDoWork(doWorkEventArgs);
				if (doWorkEventArgs.Cancel)
				{
					flag = true;
				}
				else
				{
					obj = doWorkEventArgs.Result;
				}
			}
			catch (Exception ex)
			{
			}
			RunWorkerCompletedEventArgs runWorkerCompletedEventArgs = new RunWorkerCompletedEventArgs(obj, ex, flag);
			this._asyncOperation.PostOperationCompleted(this._operationCompleted, runWorkerCompletedEventArgs);
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x04001FFF RID: 8191
		private bool _canCancelWorker;

		// Token: 0x04002000 RID: 8192
		private bool _workerReportsProgress;

		// Token: 0x04002001 RID: 8193
		private bool _cancellationPending;

		// Token: 0x04002002 RID: 8194
		private bool _isRunning;

		// Token: 0x04002003 RID: 8195
		private AsyncOperation _asyncOperation;

		// Token: 0x04002004 RID: 8196
		private readonly SendOrPostCallback _operationCompleted;

		// Token: 0x04002005 RID: 8197
		private readonly SendOrPostCallback _progressReporter;
	}
}
