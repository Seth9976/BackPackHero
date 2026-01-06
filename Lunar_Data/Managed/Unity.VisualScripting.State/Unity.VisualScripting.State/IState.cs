using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000008 RID: 8
	public interface IState : IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IGraphElementWithData
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29
		StateGraph graph { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30
		// (set) Token: 0x0600001F RID: 31
		bool isStart { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32
		bool canBeSource { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000021 RID: 33
		bool canBeDestination { get; }

		// Token: 0x06000022 RID: 34
		void OnBranchTo(Flow flow, IState destination);

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000023 RID: 35
		IEnumerable<IStateTransition> outgoingTransitions { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36
		IEnumerable<IStateTransition> incomingTransitions { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37
		IEnumerable<IStateTransition> transitions { get; }

		// Token: 0x06000026 RID: 38
		void OnEnter(Flow flow, StateEnterReason reason);

		// Token: 0x06000027 RID: 39
		void OnExit(Flow flow, StateExitReason reason);

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40
		// (set) Token: 0x06000029 RID: 41
		Vector2 position { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42
		// (set) Token: 0x0600002B RID: 43
		float width { get; set; }
	}
}
