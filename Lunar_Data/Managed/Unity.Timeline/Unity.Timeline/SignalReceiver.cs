using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002C RID: 44
	public class SignalReceiver : MonoBehaviour, INotificationReceiver
	{
		// Token: 0x0600024F RID: 591 RVA: 0x000086D0 File Offset: 0x000068D0
		public void OnNotify(Playable origin, INotification notification, object context)
		{
			SignalEmitter signalEmitter = notification as SignalEmitter;
			UnityEvent unityEvent;
			if (signalEmitter != null && signalEmitter.asset != null && this.m_Events.TryGetValue(signalEmitter.asset, out unityEvent) && unityEvent != null)
			{
				unityEvent.Invoke();
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000871C File Offset: 0x0000691C
		public void AddReaction(SignalAsset asset, UnityEvent reaction)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (this.m_Events.signals.Contains(asset))
			{
				throw new ArgumentException("SignalAsset already used.");
			}
			this.m_Events.Append(asset, reaction);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008768 File Offset: 0x00006968
		public int AddEmptyReaction(UnityEvent reaction)
		{
			this.m_Events.Append(null, reaction);
			return this.m_Events.events.Count - 1;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008789 File Offset: 0x00006989
		public void Remove(SignalAsset asset)
		{
			if (!this.m_Events.signals.Contains(asset))
			{
				throw new ArgumentException("The SignalAsset is not registered with this receiver.");
			}
			this.m_Events.Remove(asset);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000087B5 File Offset: 0x000069B5
		public IEnumerable<SignalAsset> GetRegisteredSignals()
		{
			return this.m_Events.signals;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000087C4 File Offset: 0x000069C4
		public UnityEvent GetReaction(SignalAsset key)
		{
			UnityEvent unityEvent;
			if (this.m_Events.TryGetValue(key, out unityEvent))
			{
				return unityEvent;
			}
			return null;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000087E4 File Offset: 0x000069E4
		public int Count()
		{
			return this.m_Events.signals.Count;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000087F8 File Offset: 0x000069F8
		public void ChangeSignalAtIndex(int idx, SignalAsset newKey)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			if (this.m_Events.signals[idx] == newKey)
			{
				return;
			}
			bool flag = this.m_Events.signals.Contains(newKey);
			if (newKey == null || this.m_Events.signals[idx] == null || !flag)
			{
				this.m_Events.signals[idx] = newKey;
			}
			if (newKey != null && flag)
			{
				throw new ArgumentException("SignalAsset already used.");
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000889D File Offset: 0x00006A9D
		public void RemoveAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			this.m_Events.Remove(idx);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000088CA File Offset: 0x00006ACA
		public void ChangeReactionAtIndex(int idx, UnityEvent reaction)
		{
			if (idx < 0 || idx > this.m_Events.events.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			this.m_Events.events[idx] = reaction;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000088FD File Offset: 0x00006AFD
		public UnityEvent GetReactionAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.events.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.m_Events.events[idx];
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000892F File Offset: 0x00006B2F
		public SignalAsset GetSignalAssetAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.m_Events.signals[idx];
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008961 File Offset: 0x00006B61
		private void OnEnable()
		{
		}

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		private SignalReceiver.EventKeyValue m_Events = new SignalReceiver.EventKeyValue();

		// Token: 0x02000070 RID: 112
		[Serializable]
		private class EventKeyValue
		{
			// Token: 0x06000354 RID: 852 RVA: 0x0000BA34 File Offset: 0x00009C34
			public bool TryGetValue(SignalAsset key, out UnityEvent value)
			{
				int num = this.m_Signals.IndexOf(key);
				if (num != -1)
				{
					value = this.m_Events[num];
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x06000355 RID: 853 RVA: 0x0000BA66 File Offset: 0x00009C66
			public void Append(SignalAsset key, UnityEvent value)
			{
				this.m_Signals.Add(key);
				this.m_Events.Add(value);
			}

			// Token: 0x06000356 RID: 854 RVA: 0x0000BA80 File Offset: 0x00009C80
			public void Remove(int idx)
			{
				if (idx != -1)
				{
					this.m_Signals.RemoveAt(idx);
					this.m_Events.RemoveAt(idx);
				}
			}

			// Token: 0x06000357 RID: 855 RVA: 0x0000BAA0 File Offset: 0x00009CA0
			public void Remove(SignalAsset key)
			{
				int num = this.m_Signals.IndexOf(key);
				if (num != -1)
				{
					this.m_Signals.RemoveAt(num);
					this.m_Events.RemoveAt(num);
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000358 RID: 856 RVA: 0x0000BAD6 File Offset: 0x00009CD6
			public List<SignalAsset> signals
			{
				get
				{
					return this.m_Signals;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000359 RID: 857 RVA: 0x0000BADE File Offset: 0x00009CDE
			public List<UnityEvent> events
			{
				get
				{
					return this.m_Events;
				}
			}

			// Token: 0x04000166 RID: 358
			[SerializeField]
			private List<SignalAsset> m_Signals = new List<SignalAsset>();

			// Token: 0x04000167 RID: 359
			[SerializeField]
			[CustomSignalEventDrawer]
			private List<UnityEvent> m_Events = new List<UnityEvent>();
		}
	}
}
