using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Threading.Internal
{
	// Token: 0x0200000E RID: 14
	public interface IUnityThreadUtils : IServiceComponent
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23
		bool IsRunningOnUnityThread { get; }

		// Token: 0x06000018 RID: 24
		Task PostAsync([NotNull] Action action);

		// Token: 0x06000019 RID: 25
		Task PostAsync([NotNull] Action<object> action, object state);

		// Token: 0x0600001A RID: 26
		Task<T> PostAsync<T>([NotNull] Func<T> action);

		// Token: 0x0600001B RID: 27
		Task<T> PostAsync<T>([NotNull] Func<object, T> action, object state);

		// Token: 0x0600001C RID: 28
		void Send([NotNull] Action action);

		// Token: 0x0600001D RID: 29
		void Send([NotNull] Action<object> action, object state);

		// Token: 0x0600001E RID: 30
		T Send<T>([NotNull] Func<T> action);

		// Token: 0x0600001F RID: 31
		T Send<T>([NotNull] Func<object, T> action, object state);
	}
}
