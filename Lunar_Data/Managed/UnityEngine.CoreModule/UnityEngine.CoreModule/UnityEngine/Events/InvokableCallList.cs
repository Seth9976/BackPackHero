using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x020002C0 RID: 704
	internal class InvokableCallList
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0002F7EC File Offset: 0x0002D9EC
		public int Count
		{
			get
			{
				return this.m_PersistentCalls.Count + this.m_RuntimeCalls.Count;
			}
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0002F815 File Offset: 0x0002DA15
		public void AddPersistentInvokableCall(BaseInvokableCall call)
		{
			this.m_PersistentCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0002F82C File Offset: 0x0002DA2C
		public void AddListener(BaseInvokableCall call)
		{
			this.m_RuntimeCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0002F844 File Offset: 0x0002DA44
		public void RemoveListener(object targetObj, MethodInfo method)
		{
			List<BaseInvokableCall> list = new List<BaseInvokableCall>();
			for (int i = 0; i < this.m_RuntimeCalls.Count; i++)
			{
				bool flag = this.m_RuntimeCalls[i].Find(targetObj, method);
				if (flag)
				{
					list.Add(this.m_RuntimeCalls[i]);
				}
			}
			this.m_RuntimeCalls.RemoveAll(new Predicate<BaseInvokableCall>(list.Contains));
			List<BaseInvokableCall> list2 = new List<BaseInvokableCall>(this.m_PersistentCalls.Count + this.m_RuntimeCalls.Count);
			list2.AddRange(this.m_PersistentCalls);
			list2.AddRange(this.m_RuntimeCalls);
			this.m_ExecutingCalls = list2;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0002F900 File Offset: 0x0002DB00
		public void Clear()
		{
			this.m_RuntimeCalls.Clear();
			List<BaseInvokableCall> list = new List<BaseInvokableCall>(this.m_PersistentCalls);
			this.m_ExecutingCalls = list;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0002F934 File Offset: 0x0002DB34
		public void ClearPersistent()
		{
			this.m_PersistentCalls.Clear();
			List<BaseInvokableCall> list = new List<BaseInvokableCall>(this.m_RuntimeCalls);
			this.m_ExecutingCalls = list;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0002F968 File Offset: 0x0002DB68
		public List<BaseInvokableCall> PrepareInvoke()
		{
			bool needsUpdate = this.m_NeedsUpdate;
			if (needsUpdate)
			{
				this.m_ExecutingCalls.Clear();
				this.m_ExecutingCalls.AddRange(this.m_PersistentCalls);
				this.m_ExecutingCalls.AddRange(this.m_RuntimeCalls);
				this.m_NeedsUpdate = false;
			}
			return this.m_ExecutingCalls;
		}

		// Token: 0x040009A2 RID: 2466
		private readonly List<BaseInvokableCall> m_PersistentCalls = new List<BaseInvokableCall>();

		// Token: 0x040009A3 RID: 2467
		private readonly List<BaseInvokableCall> m_RuntimeCalls = new List<BaseInvokableCall>();

		// Token: 0x040009A4 RID: 2468
		private List<BaseInvokableCall> m_ExecutingCalls = new List<BaseInvokableCall>();

		// Token: 0x040009A5 RID: 2469
		private bool m_NeedsUpdate = true;
	}
}
