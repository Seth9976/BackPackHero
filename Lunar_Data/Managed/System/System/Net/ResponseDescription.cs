using System;
using System.Text;

namespace System.Net
{
	// Token: 0x02000395 RID: 917
	internal class ResponseDescription
	{
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0006E132 File Offset: 0x0006C332
		internal bool PositiveIntermediate
		{
			get
			{
				return this.Status >= 100 && this.Status <= 199;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x0006E150 File Offset: 0x0006C350
		internal bool PositiveCompletion
		{
			get
			{
				return this.Status >= 200 && this.Status <= 299;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x0006E171 File Offset: 0x0006C371
		internal bool TransientFailure
		{
			get
			{
				return this.Status >= 400 && this.Status <= 499;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x0006E192 File Offset: 0x0006C392
		internal bool PermanentFailure
		{
			get
			{
				return this.Status >= 500 && this.Status <= 599;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x0006E1B3 File Offset: 0x0006C3B3
		internal bool InvalidStatusCode
		{
			get
			{
				return this.Status < 100 || this.Status > 599;
			}
		}

		// Token: 0x04000FE6 RID: 4070
		internal const int NoStatus = -1;

		// Token: 0x04000FE7 RID: 4071
		internal bool Multiline;

		// Token: 0x04000FE8 RID: 4072
		internal int Status = -1;

		// Token: 0x04000FE9 RID: 4073
		internal string StatusDescription;

		// Token: 0x04000FEA RID: 4074
		internal StringBuilder StatusBuffer = new StringBuilder();

		// Token: 0x04000FEB RID: 4075
		internal string StatusCodeString;
	}
}
