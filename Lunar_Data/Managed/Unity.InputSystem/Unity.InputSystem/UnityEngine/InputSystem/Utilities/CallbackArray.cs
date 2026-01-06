using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000125 RID: 293
	internal struct CallbackArray<TDelegate> where TDelegate : Delegate
	{
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0004E5DB File Offset: 0x0004C7DB
		public int length
		{
			get
			{
				return this.m_Callbacks.length;
			}
		}

		// Token: 0x170004AE RID: 1198
		public TDelegate this[int index]
		{
			get
			{
				return this.m_Callbacks[index];
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0004E5F6 File Offset: 0x0004C7F6
		public void Clear()
		{
			this.m_Callbacks.Clear();
			this.m_CallbacksToAdd.Clear();
			this.m_CallbacksToRemove.Clear();
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0004E61C File Offset: 0x0004C81C
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

		// Token: 0x06001075 RID: 4213 RVA: 0x0004E68C File Offset: 0x0004C88C
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

		// Token: 0x06001076 RID: 4214 RVA: 0x0004E6FA File Offset: 0x0004C8FA
		public void LockForChanges()
		{
			this.m_CannotMutateCallbacksArray = true;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0004E704 File Offset: 0x0004C904
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

		// Token: 0x040006B1 RID: 1713
		private bool m_CannotMutateCallbacksArray;

		// Token: 0x040006B2 RID: 1714
		private InlinedArray<TDelegate> m_Callbacks;

		// Token: 0x040006B3 RID: 1715
		private InlinedArray<TDelegate> m_CallbacksToAdd;

		// Token: 0x040006B4 RID: 1716
		private InlinedArray<TDelegate> m_CallbacksToRemove;
	}
}
