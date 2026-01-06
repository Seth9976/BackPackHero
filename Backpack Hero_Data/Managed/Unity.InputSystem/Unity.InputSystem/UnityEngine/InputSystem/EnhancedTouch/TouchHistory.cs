using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x02000099 RID: 153
	public struct TouchHistory : IReadOnlyList<Touch>, IEnumerable<Touch>, IEnumerable, IReadOnlyCollection<Touch>
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x0003FAA8 File Offset: 0x0003DCA8
		internal TouchHistory(Finger finger, InputStateHistory<TouchState> history, int startIndex = -1, int count = -1)
		{
			this.m_Finger = finger;
			this.m_History = history;
			this.m_Version = history.version;
			this.m_Count = ((count >= 0) ? count : this.m_History.Count);
			this.m_StartIndex = ((startIndex >= 0) ? startIndex : (this.m_History.Count - 1));
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0003FB03 File Offset: 0x0003DD03
		public IEnumerator<Touch> GetEnumerator()
		{
			return new TouchHistory.Enumerator(this);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0003FB10 File Offset: 0x0003DD10
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0003FB18 File Offset: 0x0003DD18
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000332 RID: 818
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

		// Token: 0x06000BFF RID: 3071 RVA: 0x0003FB84 File Offset: 0x0003DD84
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
			// Token: 0x06001463 RID: 5219 RVA: 0x0005F023 File Offset: 0x0005D223
			internal Enumerator(TouchHistory owner)
			{
				this.m_Owner = owner;
				this.m_Index = -1;
			}

			// Token: 0x06001464 RID: 5220 RVA: 0x0005F03C File Offset: 0x0005D23C
			public bool MoveNext()
			{
				if (this.m_Index >= this.m_Owner.Count - 1)
				{
					return false;
				}
				this.m_Index++;
				return true;
			}

			// Token: 0x06001465 RID: 5221 RVA: 0x0005F072 File Offset: 0x0005D272
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x06001466 RID: 5222 RVA: 0x0005F07C File Offset: 0x0005D27C
			public Touch Current
			{
				get
				{
					return this.m_Owner[this.m_Index];
				}
			}

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x06001467 RID: 5223 RVA: 0x0005F09D File Offset: 0x0005D29D
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001468 RID: 5224 RVA: 0x0005F0AA File Offset: 0x0005D2AA
			public void Dispose()
			{
			}

			// Token: 0x04000AD3 RID: 2771
			private readonly TouchHistory m_Owner;

			// Token: 0x04000AD4 RID: 2772
			private int m_Index;
		}
	}
}
