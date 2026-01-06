using System;

namespace Unity.Services.Core.Environments
{
	// Token: 0x02000002 RID: 2
	public static class EnvironmentsOptionsExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static InitializationOptions SetEnvironmentName(this InitializationOptions self, string environmentName)
		{
			if (string.IsNullOrEmpty(environmentName))
			{
				throw new ArgumentException("Environment name cannot be null or empty.", "environmentName");
			}
			self.SetOption("com.unity.services.core.environment-name", environmentName);
			return self;
		}

		// Token: 0x04000001 RID: 1
		internal const string EnvironmentNameKey = "com.unity.services.core.environment-name";
	}
}
