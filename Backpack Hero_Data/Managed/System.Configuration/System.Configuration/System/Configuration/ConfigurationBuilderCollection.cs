using System;
using System.Configuration.Provider;
using System.Reflection;
using Unity;

namespace System.Configuration
{
	// Token: 0x0200008D RID: 141
	[DefaultMember("Item")]
	public class ConfigurationBuilderCollection : ProviderCollection
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00003518 File Offset: 0x00001718
		public ConfigurationBuilderCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
