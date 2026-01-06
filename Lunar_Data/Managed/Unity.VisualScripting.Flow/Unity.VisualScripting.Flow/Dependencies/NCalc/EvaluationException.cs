using System;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x0200018C RID: 396
	public sealed class EvaluationException : ApplicationException
	{
		// Token: 0x06000A83 RID: 2691 RVA: 0x0001302B File Offset: 0x0001122B
		public EvaluationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00013034 File Offset: 0x00011234
		public EvaluationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
