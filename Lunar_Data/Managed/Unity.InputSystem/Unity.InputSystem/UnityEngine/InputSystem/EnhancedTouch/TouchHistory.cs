using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x02000099 RID: 153
	public struct TouchHistory : IReadOnlyList<Touch>, IEnumerable<Touch>, IEnumerable, IReadOnlyCollection<Touch>
	{
		// Token: 0x06000BF7 RID: 3063 RVA: 0x0003FA60 File Offset: 0x0003DC60
		internal TouchHistory(Finger finger, InputStateHistory<TouchState> history, int startIndex = -1, int count = -1)
		{
			this.m_Finger = finger;
			this.m_History = history;
			this.m_Version = history.version;
			this.m_Count = ((count >= 0) ? count : this.m_History.Count);
			this.m_StartIndex = ((startIndex >= 0) ? startIndex : (this.m_History.Count - 1));
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0003FABB File Offset: 0x0003DCBB
		public IEnumerator<Touch> GetEnumerator()
		{
			return new TouchHistory.Enumerator(this);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0003FAC8 File Offset: 0x0003DCC8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0003FAD0 File Offset: 0x0003DCD0
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000330 RID: 816
		public Touch this[int index]
		{
			get
			{
				this.CheckValid();
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, this.Count), "index");
				}
				return new Touch(this.m_Finger, this.m_History[this.m_StartIndex - index]);
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0003FB3C File Offset: 0x0003DD3C
		internal void CheckValid()
		{
			if (this.m_Finger == null || this.m_History == null)
			{
				throw new InvalidOperationException("Touch history not initialized");
			}
			if (this.m_History.version != this.m_Version)
			{
				throw new InvalidOperationException("Touch history is no longer valid; the recorded history has been changed");
			}
		}

		// Token: 0x04000439 RID: 1081
		private readonly InputStateHistory<TouchState> m_History;

		// Token: 0x0400043A RID: 1082
		private readonly Finger m_Finger;

		// Token: 0x0400043B RID: 1083
		private readonly int m_Count;

		// Token: 0x0400043C RID: 1084
		private readonly int m_StartIndex;

		// Token: 0x0400043D RID: 1085
		private readonly uint m_Version;

		// Token: 0x020001EF RID: 495
		private class Enumerator : IEnumerator<Touch>, IEnumerator, IDisposable
		{
			// Token: 0x0600145C RID: 5212 RVA: 0x0005EE0F File Offset: 0x0005D00F
			internal Enumerator(TouchHistory owner)
			{
				this.m_Owner = owner;
				this.m_Index = -1;
			}

			// Token: 0x0600145D RID: 5213 RVA: 0x0005EE28 File Offset: 0x0005D028
			public bool MoveNext()
			{
				if (this.m_Index >= this.m_Owner.Count - 1)
				{
					return false;
				}
				this.m_Index++;
				return true;
			}

			// Token: 0x0600145E RID: 5214 RVA: 0x0005EE5E File Offset: 0x0005D05E
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x0600145F RID: 5215 RVA: 0x0005EE68 File Offset: 0x0005D068
			public Touch Current
			{
				get
				{
					return this.m_Owner[this.m_Index];
				}
			}

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x06001460 RID: 5216 RVA: 0x0005EE89 File Offset: 0x0005D089
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001461 RID: 5217 RVA: 0x0005EE96 File Offset: 0x0005D096
			public void Dispose()
			{
			}

			// Token: 0x04000AD2 RID: 2770
			private readonly TouchHistory m_Owner;

			// Token: 0x04000AD3 RID: 2771
			private int m_Index;
		}
	}
}
