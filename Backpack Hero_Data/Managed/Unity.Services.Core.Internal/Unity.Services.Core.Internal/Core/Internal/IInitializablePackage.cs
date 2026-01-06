using System;
using System.Threading.Tasks;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200003F RID: 63
	public interface IInitializablePackage
	{
		// Token: 0x06000116 RID: 278
		Task Initialize(CoreRegistry registry);
	}
}
