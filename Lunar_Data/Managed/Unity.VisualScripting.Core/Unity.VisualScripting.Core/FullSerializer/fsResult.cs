using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x020001A5 RID: 421
	public struct fsResult
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x0002E878 File Offset: 0x0002CA78
		public void AddMessage(string message)
		{
			if (this._messages == null)
			{
				this._messages = new List<string>();
			}
			this._messages.Add(message);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002E899 File Offset: 0x0002CA99
		public void AddMessages(fsResult result)
		{
			if (result._messages == null)
			{
				return;
			}
			if (this._messages == null)
			{
				this._messages = new List<string>();
			}
			this._messages.AddRange(result._messages);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002E8C8 File Offset: 0x0002CAC8
		public fsResult Merge(fsResult other)
		{
			this._success = this._success && other._success;
			if (other._messages != null)
			{
				if (this._messages == null)
				{
					this._messages = new List<string>(other._messages);
				}
				else
				{
					this._messages.AddRange(other._messages);
				}
			}
			return this;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002E928 File Offset: 0x0002CB28
		public static fsResult Warn(string warning)
		{
			return new fsResult
			{
				_success = true,
				_messages = new List<string> { warning }
			};
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002E95C File Offset: 0x0002CB5C
		public static fsResult Fail(string warning)
		{
			return new fsResult
			{
				_success = false,
				_messages = new List<string> { warning }
			};
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002E98D File Offset: 0x0002CB8D
		public static fsResult operator +(fsResult a, fsResult b)
		{
			return a.Merge(b);
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0002E997 File Offset: 0x0002CB97
		public bool Failed
		{
			get
			{
				return !this._success;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0002E9A2 File Offset: 0x0002CBA2
		public bool Succeeded
		{
			get
			{
				return this._success;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0002E9AA File Offset: 0x0002CBAA
		public bool HasWarnings
		{
			get
			{
				return this._messages != null && this._messages.Any<string>();
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002E9C1 File Offset: 0x0002CBC1
		public fsResult AssertSuccess()
		{
			if (this.Failed)
			{
				throw this.AsException;
			}
			return this;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002E9D8 File Offset: 0x0002CBD8
		public fsResult AssertSuccessWithoutWarnings()
		{
			if (this.Failed || this.RawMessages.Any<string>())
			{
				throw this.AsException;
			}
			return this;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0002E9FC File Offset: 0x0002CBFC
		public Exception AsException
		{
			get
			{
				if (!this.Failed && !this.RawMessages.Any<string>())
				{
					throw new Exception("Only a failed result can be converted to an exception");
				}
				return new Exception(this.FormattedMessages);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0002EA29 File Offset: 0x0002CC29
		public IEnumerable<string> RawMessages
		{
			get
			{
				if (this._messages != null)
				{
					return this._messages;
				}
				return fsResult.EmptyStringArray;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0002EA3F File Offset: 0x0002CC3F
		public string FormattedMessages
		{
			get
			{
				return string.Join(",\n", this.RawMessages.ToArray<string>());
			}
		}

		// Token: 0x04000294 RID: 660
		private static readonly string[] EmptyStringArray = new string[0];

		// Token: 0x04000295 RID: 661
		private bool _success;

		// Token: 0x04000296 RID: 662
		private List<string> _messages;

		// Token: 0x04000297 RID: 663
		public static fsResult Success = new fsResult
		{
			_success = true
		};
	}
}
