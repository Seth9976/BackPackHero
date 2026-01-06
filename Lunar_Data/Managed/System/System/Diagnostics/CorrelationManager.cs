using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Diagnostics
{
	/// <summary>Correlates traces that are part of a logical transaction.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000214 RID: 532
	public class CorrelationManager
	{
		// Token: 0x06000F4E RID: 3918 RVA: 0x0000219B File Offset: 0x0000039B
		internal CorrelationManager()
		{
		}

		/// <summary>Gets or sets the identity for a global activity.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that identifies the global activity.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00044AC0 File Offset: 0x00042CC0
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x00044AE7 File Offset: 0x00042CE7
		public Guid ActivityId
		{
			get
			{
				object obj = CallContext.LogicalGetData("E2ETrace.ActivityID");
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				CallContext.LogicalSetData("E2ETrace.ActivityID", value);
			}
		}

		/// <summary>Gets the logical operation stack from the call context.</summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> object that represents the logical operation stack for the call context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00044AF9 File Offset: 0x00042CF9
		public Stack LogicalOperationStack
		{
			get
			{
				return this.GetLogicalOperationStack();
			}
		}

		/// <summary>Starts a logical operation with the specified identity on a thread.</summary>
		/// <param name="operationId">An object identifying the operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="operationId" /> parameter is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F52 RID: 3922 RVA: 0x00044B01 File Offset: 0x00042D01
		public void StartLogicalOperation(object operationId)
		{
			if (operationId == null)
			{
				throw new ArgumentNullException("operationId");
			}
			this.GetLogicalOperationStack().Push(operationId);
		}

		/// <summary>Starts a logical operation on a thread.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F53 RID: 3923 RVA: 0x00044B1D File Offset: 0x00042D1D
		public void StartLogicalOperation()
		{
			this.StartLogicalOperation(Guid.NewGuid());
		}

		/// <summary>Stops the current logical operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.CorrelationManager.LogicalOperationStack" /> property is an empty stack.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F54 RID: 3924 RVA: 0x00044B2F File Offset: 0x00042D2F
		public void StopLogicalOperation()
		{
			this.GetLogicalOperationStack().Pop();
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00044B40 File Offset: 0x00042D40
		private Stack GetLogicalOperationStack()
		{
			Stack stack = CallContext.LogicalGetData("System.Diagnostics.Trace.CorrelationManagerSlot") as Stack;
			if (stack == null)
			{
				stack = new Stack();
				CallContext.LogicalSetData("System.Diagnostics.Trace.CorrelationManagerSlot", stack);
			}
			return stack;
		}

		// Token: 0x0400098E RID: 2446
		private const string transactionSlotName = "System.Diagnostics.Trace.CorrelationManagerSlot";

		// Token: 0x0400098F RID: 2447
		private const string activityIdSlotName = "E2ETrace.ActivityID";
	}
}
