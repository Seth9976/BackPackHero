using System;
using System.Threading;
using Unity;

namespace System.ComponentModel
{
	/// <summary>Tracks the lifetime of an asynchronous operation.</summary>
	// Token: 0x02000676 RID: 1654
	public sealed class AsyncOperation
	{
		// Token: 0x0600352E RID: 13614 RVA: 0x000BE5E5 File Offset: 0x000BC7E5
		private AsyncOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			this._userSuppliedState = userSuppliedState;
			this._syncContext = syncContext;
			this._alreadyCompleted = false;
			this._syncContext.OperationStarted();
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000BE610 File Offset: 0x000BC810
		~AsyncOperation()
		{
			if (!this._alreadyCompleted && this._syncContext != null)
			{
				this._syncContext.OperationCompleted();
			}
		}

		/// <summary>Gets or sets an object used to uniquely identify an asynchronous operation.</summary>
		/// <returns>The state object passed to the asynchronous method invocation.</returns>
		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06003530 RID: 13616 RVA: 0x000BE654 File Offset: 0x000BC854
		public object UserSuppliedState
		{
			get
			{
				return this._userSuppliedState;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</summary>
		/// <returns>The <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</returns>
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x000BE65C File Offset: 0x000BC85C
		public SynchronizationContext SynchronizationContext
		{
			get
			{
				return this._syncContext;
			}
		}

		/// <summary>Invokes a delegate on the thread or context appropriate for the application model.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends. </param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.ComponentModel.AsyncOperation.PostOperationCompleted(System.Threading.SendOrPostCallback,System.Object)" /> method has been called previously for this task. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x06003532 RID: 13618 RVA: 0x000BE664 File Offset: 0x000BC864
		public void Post(SendOrPostCallback d, object arg)
		{
			this.PostCore(d, arg, false);
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends. </param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x06003533 RID: 13619 RVA: 0x000BE66F File Offset: 0x000BC86F
		public void PostOperationCompleted(SendOrPostCallback d, object arg)
		{
			this.PostCore(d, arg, true);
			this.OperationCompletedCore();
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003534 RID: 13620 RVA: 0x000BE680 File Offset: 0x000BC880
		public void OperationCompleted()
		{
			this.VerifyNotCompleted();
			this._alreadyCompleted = true;
			this.OperationCompletedCore();
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000BE695 File Offset: 0x000BC895
		private void PostCore(SendOrPostCallback d, object arg, bool markCompleted)
		{
			this.VerifyNotCompleted();
			this.VerifyDelegateNotNull(d);
			if (markCompleted)
			{
				this._alreadyCompleted = true;
			}
			this._syncContext.Post(d, arg);
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000BE6BC File Offset: 0x000BC8BC
		private void OperationCompletedCore()
		{
			try
			{
				this._syncContext.OperationCompleted();
			}
			finally
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000BE6F0 File Offset: 0x000BC8F0
		private void VerifyNotCompleted()
		{
			if (this._alreadyCompleted)
			{
				throw new InvalidOperationException("This operation has already had OperationCompleted called on it and further calls are illegal.");
			}
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000BE705 File Offset: 0x000BC905
		private void VerifyDelegateNotNull(SendOrPostCallback d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "A non-null SendOrPostCallback must be supplied.");
			}
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000BE71A File Offset: 0x000BC91A
		internal static AsyncOperation CreateOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			return new AsyncOperation(userSuppliedState, syncContext);
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x00013B26 File Offset: 0x00011D26
		internal AsyncOperation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001FFC RID: 8188
		private readonly SynchronizationContext _syncContext;

		// Token: 0x04001FFD RID: 8189
		private readonly object _userSuppliedState;

		// Token: 0x04001FFE RID: 8190
		private bool _alreadyCompleted;
	}
}
