using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001DA RID: 474
	internal sealed class SqlStatistics
	{
		// Token: 0x060016BA RID: 5818 RVA: 0x0006F495 File Offset: 0x0006D695
		internal static SqlStatistics StartTimer(SqlStatistics statistics)
		{
			if (statistics != null && !statistics.RequestExecutionTimer())
			{
				statistics = null;
			}
			return statistics;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0006F4A6 File Offset: 0x0006D6A6
		internal static void StopTimer(SqlStatistics statistics)
		{
			if (statistics != null)
			{
				statistics.ReleaseAndUpdateExecutionTimer();
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x0006F4B1 File Offset: 0x0006D6B1
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x0006F4B9 File Offset: 0x0006D6B9
		internal bool WaitForDoneAfterRow
		{
			get
			{
				return this._waitForDoneAfterRow;
			}
			set
			{
				this._waitForDoneAfterRow = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x0006F4C2 File Offset: 0x0006D6C2
		internal bool WaitForReply
		{
			get
			{
				return this._waitForReply;
			}
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00003D55 File Offset: 0x00001F55
		internal SqlStatistics()
		{
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0006F4CA File Offset: 0x0006D6CA
		internal void ContinueOnNewConnection()
		{
			this._startExecutionTimestamp = 0L;
			this._startFetchTimestamp = 0L;
			this._waitForDoneAfterRow = false;
			this._waitForReply = false;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0006F4EC File Offset: 0x0006D6EC
		internal IDictionary GetDictionary()
		{
			return new SqlStatistics.StatisticsDictionary(18)
			{
				{ "BuffersReceived", this._buffersReceived },
				{ "BuffersSent", this._buffersSent },
				{ "BytesReceived", this._bytesReceived },
				{ "BytesSent", this._bytesSent },
				{ "CursorOpens", this._cursorOpens },
				{ "IduCount", this._iduCount },
				{ "IduRows", this._iduRows },
				{ "PreparedExecs", this._preparedExecs },
				{ "Prepares", this._prepares },
				{ "SelectCount", this._selectCount },
				{ "SelectRows", this._selectRows },
				{ "ServerRoundtrips", this._serverRoundtrips },
				{ "SumResultSets", this._sumResultSets },
				{ "Transactions", this._transactions },
				{ "UnpreparedExecs", this._unpreparedExecs },
				{
					"ConnectionTime",
					ADP.TimerToMilliseconds(this._connectionTime)
				},
				{
					"ExecutionTime",
					ADP.TimerToMilliseconds(this._executionTime)
				},
				{
					"NetworkServerTime",
					ADP.TimerToMilliseconds(this._networkServerTime)
				}
			};
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0006F69B File Offset: 0x0006D89B
		internal bool RequestExecutionTimer()
		{
			if (this._startExecutionTimestamp == 0L)
			{
				ADP.TimerCurrent(out this._startExecutionTimestamp);
				return true;
			}
			return false;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0006F6B3 File Offset: 0x0006D8B3
		internal void RequestNetworkServerTimer()
		{
			if (this._startNetworkServerTimestamp == 0L)
			{
				ADP.TimerCurrent(out this._startNetworkServerTimestamp);
			}
			this._waitForReply = true;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0006F6CF File Offset: 0x0006D8CF
		internal void ReleaseAndUpdateExecutionTimer()
		{
			if (this._startExecutionTimestamp > 0L)
			{
				this._executionTime += ADP.TimerCurrent() - this._startExecutionTimestamp;
				this._startExecutionTimestamp = 0L;
			}
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0006F6FC File Offset: 0x0006D8FC
		internal void ReleaseAndUpdateNetworkServerTimer()
		{
			if (this._waitForReply && this._startNetworkServerTimestamp > 0L)
			{
				this._networkServerTime += ADP.TimerCurrent() - this._startNetworkServerTimestamp;
				this._startNetworkServerTimestamp = 0L;
			}
			this._waitForReply = false;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0006F738 File Offset: 0x0006D938
		internal void Reset()
		{
			this._buffersReceived = 0L;
			this._buffersSent = 0L;
			this._bytesReceived = 0L;
			this._bytesSent = 0L;
			this._connectionTime = 0L;
			this._cursorOpens = 0L;
			this._executionTime = 0L;
			this._iduCount = 0L;
			this._iduRows = 0L;
			this._networkServerTime = 0L;
			this._preparedExecs = 0L;
			this._prepares = 0L;
			this._selectCount = 0L;
			this._selectRows = 0L;
			this._serverRoundtrips = 0L;
			this._sumResultSets = 0L;
			this._transactions = 0L;
			this._unpreparedExecs = 0L;
			this._waitForDoneAfterRow = false;
			this._waitForReply = false;
			this._startExecutionTimestamp = 0L;
			this._startNetworkServerTimestamp = 0L;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0006F7F3 File Offset: 0x0006D9F3
		internal void SafeAdd(ref long value, long summand)
		{
			if (9223372036854775807L - value > summand)
			{
				value += summand;
				return;
			}
			value = long.MaxValue;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0006F816 File Offset: 0x0006DA16
		internal long SafeIncrement(ref long value)
		{
			if (value < 9223372036854775807L)
			{
				value += 1L;
			}
			return value;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0006F82E File Offset: 0x0006DA2E
		internal void UpdateStatistics()
		{
			if (this._closeTimestamp >= this._openTimestamp)
			{
				this.SafeAdd(ref this._connectionTime, this._closeTimestamp - this._openTimestamp);
				return;
			}
			this._connectionTime = long.MaxValue;
		}

		// Token: 0x04000F01 RID: 3841
		internal long _closeTimestamp;

		// Token: 0x04000F02 RID: 3842
		internal long _openTimestamp;

		// Token: 0x04000F03 RID: 3843
		internal long _startExecutionTimestamp;

		// Token: 0x04000F04 RID: 3844
		internal long _startFetchTimestamp;

		// Token: 0x04000F05 RID: 3845
		internal long _startNetworkServerTimestamp;

		// Token: 0x04000F06 RID: 3846
		internal long _buffersReceived;

		// Token: 0x04000F07 RID: 3847
		internal long _buffersSent;

		// Token: 0x04000F08 RID: 3848
		internal long _bytesReceived;

		// Token: 0x04000F09 RID: 3849
		internal long _bytesSent;

		// Token: 0x04000F0A RID: 3850
		internal long _connectionTime;

		// Token: 0x04000F0B RID: 3851
		internal long _cursorOpens;

		// Token: 0x04000F0C RID: 3852
		internal long _executionTime;

		// Token: 0x04000F0D RID: 3853
		internal long _iduCount;

		// Token: 0x04000F0E RID: 3854
		internal long _iduRows;

		// Token: 0x04000F0F RID: 3855
		internal long _networkServerTime;

		// Token: 0x04000F10 RID: 3856
		internal long _preparedExecs;

		// Token: 0x04000F11 RID: 3857
		internal long _prepares;

		// Token: 0x04000F12 RID: 3858
		internal long _selectCount;

		// Token: 0x04000F13 RID: 3859
		internal long _selectRows;

		// Token: 0x04000F14 RID: 3860
		internal long _serverRoundtrips;

		// Token: 0x04000F15 RID: 3861
		internal long _sumResultSets;

		// Token: 0x04000F16 RID: 3862
		internal long _transactions;

		// Token: 0x04000F17 RID: 3863
		internal long _unpreparedExecs;

		// Token: 0x04000F18 RID: 3864
		private bool _waitForDoneAfterRow;

		// Token: 0x04000F19 RID: 3865
		private bool _waitForReply;

		// Token: 0x020001DB RID: 475
		private sealed class StatisticsDictionary : Dictionary<object, object>, IDictionary, ICollection, IEnumerable
		{
			// Token: 0x060016CA RID: 5834 RVA: 0x0006F867 File Offset: 0x0006DA67
			public StatisticsDictionary(int capacity)
				: base(capacity)
			{
			}

			// Token: 0x17000462 RID: 1122
			// (get) Token: 0x060016CB RID: 5835 RVA: 0x0006F870 File Offset: 0x0006DA70
			ICollection IDictionary.Keys
			{
				get
				{
					SqlStatistics.StatisticsDictionary.Collection collection;
					if ((collection = this._keys) == null)
					{
						collection = (this._keys = new SqlStatistics.StatisticsDictionary.Collection(this, base.Keys));
					}
					return collection;
				}
			}

			// Token: 0x17000463 RID: 1123
			// (get) Token: 0x060016CC RID: 5836 RVA: 0x0006F89C File Offset: 0x0006DA9C
			ICollection IDictionary.Values
			{
				get
				{
					SqlStatistics.StatisticsDictionary.Collection collection;
					if ((collection = this._values) == null)
					{
						collection = (this._values = new SqlStatistics.StatisticsDictionary.Collection(this, base.Values));
					}
					return collection;
				}
			}

			// Token: 0x060016CD RID: 5837 RVA: 0x0006F8C8 File Offset: 0x0006DAC8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IDictionary)this).GetEnumerator();
			}

			// Token: 0x060016CE RID: 5838 RVA: 0x0006F8D0 File Offset: 0x0006DAD0
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					DictionaryEntry dictionaryEntry = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					array.SetValue(dictionaryEntry, arrayIndex++);
				}
			}

			// Token: 0x060016CF RID: 5839 RVA: 0x0006F948 File Offset: 0x0006DB48
			private void CopyKeys(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					array.SetValue(keyValuePair.Key, arrayIndex++);
				}
			}

			// Token: 0x060016D0 RID: 5840 RVA: 0x0006F9AC File Offset: 0x0006DBAC
			private void CopyValues(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					array.SetValue(keyValuePair.Value, arrayIndex++);
				}
			}

			// Token: 0x060016D1 RID: 5841 RVA: 0x0006FA10 File Offset: 0x0006DC10
			private void ValidateCopyToArguments(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < base.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
			}

			// Token: 0x04000F1A RID: 3866
			private SqlStatistics.StatisticsDictionary.Collection _keys;

			// Token: 0x04000F1B RID: 3867
			private SqlStatistics.StatisticsDictionary.Collection _values;

			// Token: 0x020001DC RID: 476
			private sealed class Collection : ICollection, IEnumerable
			{
				// Token: 0x060016D2 RID: 5842 RVA: 0x0006FA6E File Offset: 0x0006DC6E
				public Collection(SqlStatistics.StatisticsDictionary dictionary, ICollection collection)
				{
					this._dictionary = dictionary;
					this._collection = collection;
				}

				// Token: 0x17000464 RID: 1124
				// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0006FA84 File Offset: 0x0006DC84
				int ICollection.Count
				{
					get
					{
						return this._collection.Count;
					}
				}

				// Token: 0x17000465 RID: 1125
				// (get) Token: 0x060016D4 RID: 5844 RVA: 0x0006FA91 File Offset: 0x0006DC91
				bool ICollection.IsSynchronized
				{
					get
					{
						return this._collection.IsSynchronized;
					}
				}

				// Token: 0x17000466 RID: 1126
				// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0006FA9E File Offset: 0x0006DC9E
				object ICollection.SyncRoot
				{
					get
					{
						return this._collection.SyncRoot;
					}
				}

				// Token: 0x060016D6 RID: 5846 RVA: 0x0006FAAB File Offset: 0x0006DCAB
				void ICollection.CopyTo(Array array, int arrayIndex)
				{
					if (this._collection is Dictionary<object, object>.KeyCollection)
					{
						this._dictionary.CopyKeys(array, arrayIndex);
						return;
					}
					this._dictionary.CopyValues(array, arrayIndex);
				}

				// Token: 0x060016D7 RID: 5847 RVA: 0x0006FAD5 File Offset: 0x0006DCD5
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this._collection.GetEnumerator();
				}

				// Token: 0x04000F1C RID: 3868
				private readonly SqlStatistics.StatisticsDictionary _dictionary;

				// Token: 0x04000F1D RID: 3869
				private readonly ICollection _collection;
			}
		}
	}
}
