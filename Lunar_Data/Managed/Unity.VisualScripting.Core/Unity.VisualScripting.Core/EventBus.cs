using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200004F RID: 79
	public static class EventBus
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00005F68 File Offset: 0x00004168
		public static void Register<TArgs>(EventHook hook, Action<TArgs> handler)
		{
			HashSet<Delegate> hashSet;
			if (!EventBus.events.TryGetValue(hook, out hashSet))
			{
				hashSet = new HashSet<Delegate>();
				EventBus.events.Add(hook, hashSet);
			}
			hashSet.Add(handler);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00005FA0 File Offset: 0x000041A0
		public static void Unregister(EventHook hook, Delegate handler)
		{
			HashSet<Delegate> hashSet;
			if (EventBus.events.TryGetValue(hook, out hashSet) && hashSet.Remove(handler) && hashSet.Count == 0)
			{
				EventBus.events.Remove(hook);
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00005FDC File Offset: 0x000041DC
		public static void Trigger<TArgs>(EventHook hook, TArgs args)
		{
			HashSet<Action<TArgs>> hashSet = null;
			HashSet<Delegate> hashSet2;
			if (EventBus.events.TryGetValue(hook, out hashSet2))
			{
				foreach (Delegate @delegate in hashSet2)
				{
					Action<TArgs> action = @delegate as Action<TArgs>;
					if (action != null)
					{
						if (hashSet == null)
						{
							hashSet = HashSetPool<Action<TArgs>>.New();
						}
						hashSet.Add(action);
					}
				}
			}
			if (hashSet != null)
			{
				foreach (Action<TArgs> action2 in hashSet)
				{
					if (hashSet2.Contains(action2))
					{
						action2(args);
					}
				}
				hashSet.Free<Action<TArgs>>();
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000060A0 File Offset: 0x000042A0
		public static void Trigger<TArgs>(string name, GameObject target, TArgs args)
		{
			EventBus.Trigger<TArgs>(new EventHook(name, target, null), args);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000060B0 File Offset: 0x000042B0
		public static void Trigger(EventHook hook)
		{
			EventBus.Trigger<EmptyEventArgs>(hook, default(EmptyEventArgs));
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000060CC File Offset: 0x000042CC
		public static void Trigger(string name, GameObject target)
		{
			EventBus.Trigger(new EventHook(name, target, null));
		}

		// Token: 0x0400006E RID: 110
		private static readonly Dictionary<EventHook, HashSet<Delegate>> events = new Dictionary<EventHook, HashSet<Delegate>>(new EventHookComparer());
	}
}
