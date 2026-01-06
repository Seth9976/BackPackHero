using System;

namespace UnityEngine.Assertions
{
	// Token: 0x02000484 RID: 1156
	public class AssertionException : Exception
	{
		// Token: 0x060028FE RID: 10494 RVA: 0x00043AC7 File Offset: 0x00041CC7
		public AssertionException(string message, string userMessage)
			: base(message)
		{
			this.m_UserMessage = userMessage;
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x00043ADC File Offset: 0x00041CDC
		public override string Message
		{
			get
			{
				string text = base.Message;
				bool flag = this.m_UserMessage != null;
				if (flag)
				{
					text = this.m_UserMessage + "\n" + text;
				}
				return text;
			}
		}

		// Token: 0x04000F94 RID: 3988
		private string m_UserMessage;
	}
}
