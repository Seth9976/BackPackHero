using System;

namespace Febucci.UI.Core
{
	// Token: 0x02000041 RID: 65
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00006EA3 File Offset: 0x000050A3
		public DefaultValueAttribute(string variableName, float variableValue)
		{
			this.variableName = variableName;
			this.variableValue = variableValue;
		}

		// Token: 0x040000FC RID: 252
		public readonly string variableName;

		// Token: 0x040000FD RID: 253
		public readonly float variableValue;
	}
}
