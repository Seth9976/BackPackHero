using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000142 RID: 322
	internal sealed class SavedStructState<T> : ISavedState where T : struct
	{
		// Token: 0x0600118C RID: 4492 RVA: 0x00052DF4 File Offset: 0x00050FF4
		internal SavedStructState(ref T state, SavedStructState<T>.TypedRestore restoreAction, Action staticDisposeCurrentState = null)
		{
			this.m_State = state;
			this.m_RestoreAction = restoreAction;
			this.m_StaticDisposeCurrentState = staticDisposeCurrentState;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00052E16 File Offset: 0x00051016
		public void StaticDisposeCurrentState()
		{
			if (this.m_StaticDisposeCurrentState != null)
			{
				this.m_StaticDisposeCurrentState();
				this.m_StaticDisposeCurrentState = null;
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00052E32 File Offset: 0x00051032
		public void RestoreSavedState()
		{
			this.m_RestoreAction(ref this.m_State);
			this.m_RestoreAction = null;
		}

		// Token: 0x040006E6 RID: 1766
		private T m_State;

		// Token: 0x040006E7 RID: 1767
		private SavedStructState<T>.TypedRestore m_RestoreAction;

		// Token: 0x040006E8 RID: 1768
		private Action m_StaticDisposeCurrentState;

		// Token: 0x02000246 RID: 582
		// (Invoke) Token: 0x060015DE RID: 5598
		public delegate void TypedRestore(ref T state);
	}
}
