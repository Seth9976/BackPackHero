using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000125 RID: 293
	internal struct CallbackArray<TDelegate> where TDelegate : Delegate
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0004E627 File Offset: 0x0004C827
		public int length
		{
			get
			{
				return this.m_Callbacks.length;
			}
		}

		// Token: 0x170004B0 RID: 1200
		public TDelegate this[int index]
		{
			get
			{
				return this.m_Callbacks[index];
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0004E642 File Offset: 0x0004C842
		public void Clear()
		{
			this.m_Callbacks.Clear();
			this.m_CallbacksToAdd.Clear();
			this.m_CallbacksToRemove.Clear();
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0004E668 File Offset: 0x0004C868
		public void AddCallback(TDelegate dlg)
		{
			if (!this.m_CannotMutateCallbacksArray)
			{
				if (!this.m_Callbacks.Contains(dlg))
				{
					this.m_Callbacks.AppendWithCapacity(dlg, 4);
				}
				return;
			}
			if (this.m_CallbacksToAdd.Contains(dlg))
			{
				return;
			}
			int num = this.m_CallbacksToRemove.IndexOf(dlg);
			if (num != -1)
			{
				this.m_CallbacksToRemove.RemoveAtByMovingTailWithCapacity(num);
			}
			this.m_CallbacksToAdd.AppendWithCapacity(dlg, 10);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0004E6D8 File Offset: 0x0004C8D8
		public void RemoveCallback(TDelegate dlg)
		{
			if (!this.m_CannotMutateCallbacksArray)
			{
				int num = this.m_Callbacks.IndexOf(dlg);
				if (num >= 0)
				{
					this.m_Callbacks.RemoveAtWithCapacity(num);
				}
				return;
			}
			if (this.m_CallbacksToRemove.Contains(dlg))
			{
				return;
			}
			int num2 = this.m_CallbacksToAdd.IndexOf(dlg);
			if (num2 != -1)
			{
				this.m_CallbacksToAdd.RemoveAtByMovingTailWithCapacity(num2);
			}
			this.m_CallbacksToRemove.AppendWithCapacity(dlg, 10);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0004E746 File Offset: 0x0004C946
		public void LockForChanges()
		{
			this.m_CannotMutateCallbacksArray = true;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0004E750 File Offset: 0x0004C950
		public void UnlockForChanges()
		{
			this.m_CannotMutateCallbacksArray = false;
			for (int i = 0; i < this.m_CallbacksToRemove.length; i++)
			{
				this.RemoveCallback(this.m_CallbacksToRemove[i]);
			}
			for (int j = 0; j < this.m_CallbacksToAdd.length; j++)
			{
				this.AddCallback(this.m_CallbacksToAdd[j]);
			}
			this.m_CallbacksToAdd.Clear();
			this.m_CallbacksToRemove.Clear();
		}

		// Token: 0x040006B2 RID: 1714
		private bool m_CannotMutateCallbacksArray;

		// Token: 0x040006B3 RID: 1715
		private InlinedArray<TDelegate> m_Callbacks;

		// Token: 0x040006B4 RID: 1716
		private InlinedArray<TDelegate> m_CallbacksToAdd;

		// Token: 0x040006B5 RID: 1717
		private InlinedArray<TDelegate> m_CallbacksToRemove;
	}
}
