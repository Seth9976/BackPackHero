using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000142 RID: 322
	internal sealed class SavedStructState<T> : ISavedState where T : struct
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x00052BE0 File Offset: 0x00050DE0
		internal SavedStructState(ref T state, SavedStructState<T>.TypedRestore restoreAction, Action staticDisposeCurrentState = null)
		{
			this.m_State = state;
			this.m_RestoreAction = restoreAction;
			this.m_StaticDisposeCurrentState = staticDisposeCurrentState;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00052C02 File Offset: 0x00050E02
		public void StaticDisposeCurrentState()
		{
			if (this.m_StaticDisposeCurrentState != null)
			{
				this.m_StaticDisposeCurrentState();
				this.m_StaticDisposeCurrentState = null;
			}
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00052C1E File Offset: 0x00050E1E
		public void RestoreSavedState()
		{
			this.m_RestoreAction(ref this.m_State);
			this.m_RestoreAction = null;
		}

		// Token: 0x040006E5 RID: 1765
		private T m_State;

		// Token: 0x040006E6 RID: 1766
		private SavedStructState<T>.TypedRestore m_RestoreAction;

		// Token: 0x040006E7 RID: 1767
		private Action m_StaticDisposeCurrentState;

		// Token: 0x02000246 RID: 582
		// (Invoke) Token: 0x060015D7 RID: 5591
		public delegate void TypedRestore(ref T state);
	}
}
