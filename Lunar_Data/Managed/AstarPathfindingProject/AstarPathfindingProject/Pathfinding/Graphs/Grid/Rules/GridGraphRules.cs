using System;
using System.Collections.Generic;
using Pathfinding.Jobs;
using Pathfinding.Serialization;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Rules
{
	// Token: 0x02000206 RID: 518
	[JsonOptIn]
	public class GridGraphRules
	{
		// Token: 0x06000CAA RID: 3242 RVA: 0x0004F533 File Offset: 0x0004D733
		public void AddRule(GridGraphRule rule)
		{
			this.rules.Add(rule);
			this.lastHash = -1L;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0004F549 File Offset: 0x0004D749
		public void RemoveRule(GridGraphRule rule)
		{
			this.rules.Remove(rule);
			this.lastHash = -1L;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0004F560 File Offset: 0x0004D760
		public IReadOnlyList<GridGraphRule> GetRules()
		{
			if (this.rules == null)
			{
				this.rules = new List<GridGraphRule>();
			}
			return this.rules.AsReadOnly();
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0004F580 File Offset: 0x0004D780
		private long Hash()
		{
			long num = 196613L;
			for (int i = 0; i < this.rules.Count; i++)
			{
				if (this.rules[i] != null && this.rules[i].enabled)
				{
					num = (num * 1572869L) ^ (long)this.rules[i].Hash;
				}
			}
			return num;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0004F5E8 File Offset: 0x0004D7E8
		public void RebuildIfNecessary()
		{
			long num = this.Hash();
			if (num == this.lastHash && this.jobSystemCallbacks != null && this.mainThreadCallbacks != null)
			{
				return;
			}
			this.lastHash = num;
			this.Rebuild();
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0004F624 File Offset: 0x0004D824
		public void Rebuild()
		{
			this.rules = this.rules ?? new List<GridGraphRule>();
			this.jobSystemCallbacks = this.jobSystemCallbacks ?? new List<Action<GridGraphRules.Context>>[6];
			for (int i = 0; i < this.jobSystemCallbacks.Length; i++)
			{
				if (this.jobSystemCallbacks[i] != null)
				{
					this.jobSystemCallbacks[i].Clear();
				}
			}
			this.mainThreadCallbacks = this.mainThreadCallbacks ?? new List<Action<GridGraphRules.Context>>[6];
			for (int j = 0; j < this.mainThreadCallbacks.Length; j++)
			{
				if (this.mainThreadCallbacks[j] != null)
				{
					this.mainThreadCallbacks[j].Clear();
				}
			}
			for (int k = 0; k < this.rules.Count; k++)
			{
				if (this.rules[k].enabled)
				{
					this.rules[k].Register(this);
				}
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0004F704 File Offset: 0x0004D904
		public void DisposeUnmanagedData()
		{
			if (this.rules != null)
			{
				for (int i = 0; i < this.rules.Count; i++)
				{
					if (this.rules[i] != null)
					{
						this.rules[i].DisposeUnmanagedData();
						this.rules[i].SetDirty();
					}
				}
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0004F760 File Offset: 0x0004D960
		private static void CallActions(List<Action<GridGraphRules.Context>> actions, GridGraphRules.Context context)
		{
			if (actions != null)
			{
				try
				{
					for (int i = 0; i < actions.Count; i++)
					{
						actions[i](context);
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0004F7A8 File Offset: 0x0004D9A8
		public IEnumerator<JobHandle> ExecuteRule(GridGraphRule.Pass rule, GridGraphRules.Context context)
		{
			if (this.jobSystemCallbacks == null)
			{
				this.Rebuild();
			}
			GridGraphRules.CallActions(this.jobSystemCallbacks[(int)rule], context);
			if (this.mainThreadCallbacks[(int)rule] != null && this.mainThreadCallbacks[(int)rule].Count > 0)
			{
				if (!context.tracker.forceLinearDependencies)
				{
					yield return context.tracker.AllWritesDependency;
				}
				GridGraphRules.CallActions(this.mainThreadCallbacks[(int)rule], context);
			}
			yield break;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0004F7C8 File Offset: 0x0004D9C8
		public void ExecuteRuleMainThread(GridGraphRule.Pass rule, GridGraphRules.Context context)
		{
			if (this.jobSystemCallbacks[(int)rule] != null && this.jobSystemCallbacks[(int)rule].Count > 0)
			{
				throw new Exception(string.Concat(new string[]
				{
					"A job system pass has been added for the ",
					rule.ToString(),
					" pass. ",
					rule.ToString(),
					" only supports main thread callbacks."
				}));
			}
			if (context.tracker != null)
			{
				context.tracker.AllWritesDependency.Complete();
			}
			GridGraphRules.CallActions(this.mainThreadCallbacks[(int)rule], context);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0004F864 File Offset: 0x0004DA64
		public void AddJobSystemPass(GridGraphRule.Pass pass, Action<GridGraphRules.Context> action)
		{
			if (this.jobSystemCallbacks[(int)pass] == null)
			{
				this.jobSystemCallbacks[(int)pass] = new List<Action<GridGraphRules.Context>>();
			}
			this.jobSystemCallbacks[(int)pass].Add(action);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0004F898 File Offset: 0x0004DA98
		public void AddMainThreadPass(GridGraphRule.Pass pass, Action<GridGraphRules.Context> action)
		{
			if (this.mainThreadCallbacks[(int)pass] == null)
			{
				this.mainThreadCallbacks[(int)pass] = new List<Action<GridGraphRules.Context>>();
			}
			this.mainThreadCallbacks[(int)pass].Add(action);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0004F8CC File Offset: 0x0004DACC
		[Obsolete("Use AddJobSystemPass or AddMainThreadPass instead")]
		public void Add(GridGraphRule.Pass pass, Action<GridGraphRules.Context> action)
		{
			this.AddJobSystemPass(pass, action);
		}

		// Token: 0x04000976 RID: 2422
		private List<Action<GridGraphRules.Context>>[] jobSystemCallbacks;

		// Token: 0x04000977 RID: 2423
		private List<Action<GridGraphRules.Context>>[] mainThreadCallbacks;

		// Token: 0x04000978 RID: 2424
		[JsonMember]
		private List<GridGraphRule> rules = new List<GridGraphRule>();

		// Token: 0x04000979 RID: 2425
		private long lastHash;

		// Token: 0x02000207 RID: 519
		public class Context
		{
			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0004F8E9 File Offset: 0x0004DAE9
			public JobDependencyTracker tracker
			{
				get
				{
					return this.data.dependencyTracker;
				}
			}

			// Token: 0x0400097A RID: 2426
			public GridGraph graph;

			// Token: 0x0400097B RID: 2427
			public GridGraphScanData data;
		}
	}
}
