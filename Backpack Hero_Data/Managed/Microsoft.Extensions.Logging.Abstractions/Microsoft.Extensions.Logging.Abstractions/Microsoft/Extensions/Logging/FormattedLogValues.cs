using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Extensions.Logging
{
	/// <summary>
	/// LogValues to enable formatting options supported by <see cref="M:System.String.Format(System.IFormatProvider,System.String,System.Object)" />.
	/// This also enables using {NamedformatItem} in the format string.
	/// </summary>
	// Token: 0x0200000B RID: 11
	internal readonly struct FormattedLogValues : IReadOnlyList<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IReadOnlyCollection<KeyValuePair<string, object>>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002742 File Offset: 0x00000942
		internal LogValuesFormatter Formatter
		{
			get
			{
				return this._formatter;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000274C File Offset: 0x0000094C
		public FormattedLogValues(string format, params object[] values)
		{
			if (values != null && values.Length != 0 && format != null)
			{
				if (FormattedLogValues._count >= 1024)
				{
					if (!FormattedLogValues._formatters.TryGetValue(format, ref this._formatter))
					{
						this._formatter = new LogValuesFormatter(format);
					}
				}
				else
				{
					this._formatter = FormattedLogValues._formatters.GetOrAdd(format, delegate(string f)
					{
						Interlocked.Increment(ref FormattedLogValues._count);
						return new LogValuesFormatter(f);
					});
				}
			}
			else
			{
				this._formatter = null;
			}
			this._originalMessage = format ?? "[null]";
			this._values = values;
		}

		// Token: 0x17000006 RID: 6
		public KeyValuePair<string, object> this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new IndexOutOfRangeException("index");
				}
				if (index == this.Count - 1)
				{
					return new KeyValuePair<string, object>("{OriginalFormat}", this._originalMessage);
				}
				return this._formatter.GetValue(this._values, index);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002833 File Offset: 0x00000A33
		public int Count
		{
			get
			{
				if (this._formatter == null)
				{
					return 1;
				}
				return this._formatter.ValueNames.Count + 1;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002851 File Offset: 0x00000A51
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num)
			{
				yield return this[i];
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002865 File Offset: 0x00000A65
		public override string ToString()
		{
			if (this._formatter == null)
			{
				return this._originalMessage;
			}
			return this._formatter.Format(this._values);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002887 File Offset: 0x00000A87
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400000A RID: 10
		internal const int MaxCachedFormatters = 1024;

		// Token: 0x0400000B RID: 11
		private const string NullFormat = "[null]";

		// Token: 0x0400000C RID: 12
		private static int _count;

		// Token: 0x0400000D RID: 13
		private static ConcurrentDictionary<string, LogValuesFormatter> _formatters = new ConcurrentDictionary<string, LogValuesFormatter>();

		// Token: 0x0400000E RID: 14
		private readonly LogValuesFormatter _formatter;

		// Token: 0x0400000F RID: 15
		private readonly object[] _values;

		// Token: 0x04000010 RID: 16
		private readonly string _originalMessage;
	}
}
