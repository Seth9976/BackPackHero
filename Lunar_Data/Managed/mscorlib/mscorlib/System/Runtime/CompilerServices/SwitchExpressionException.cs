using System;
using System.Runtime.Serialization;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000857 RID: 2135
	[Serializable]
	public sealed class SwitchExpressionException : InvalidOperationException
	{
		// Token: 0x0600472A RID: 18218 RVA: 0x000E7F61 File Offset: 0x000E6161
		public SwitchExpressionException()
			: base("Non-exhaustive switch expression failed to match its input.")
		{
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x000E7F6E File Offset: 0x000E616E
		public SwitchExpressionException(Exception innerException)
			: base("Non-exhaustive switch expression failed to match its input.", innerException)
		{
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x000E7F7C File Offset: 0x000E617C
		public SwitchExpressionException(object unmatchedValue)
			: this()
		{
			this.UnmatchedValue = unmatchedValue;
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x000E7F8B File Offset: 0x000E618B
		private SwitchExpressionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.UnmatchedValue = info.GetValue("UnmatchedValue", typeof(object));
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x000E7FB0 File Offset: 0x000E61B0
		public SwitchExpressionException(string message)
			: base(message)
		{
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x000E7FB9 File Offset: 0x000E61B9
		public SwitchExpressionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06004730 RID: 18224 RVA: 0x000E7FC3 File Offset: 0x000E61C3
		public object UnmatchedValue { get; }

		// Token: 0x06004731 RID: 18225 RVA: 0x000E7FCB File Offset: 0x000E61CB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("UnmatchedValue", this.UnmatchedValue, typeof(object));
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06004732 RID: 18226 RVA: 0x000E7FF0 File Offset: 0x000E61F0
		public override string Message
		{
			get
			{
				if (this.UnmatchedValue == null)
				{
					return base.Message;
				}
				string text = SR.Format("Unmatched value was {0}.", this.UnmatchedValue.ToString());
				return base.Message + Environment.NewLine + text;
			}
		}
	}
}
