using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000033 RID: 51
	internal interface IComponentRegistry
	{
		// Token: 0x060000DC RID: 220
		void RegisterServiceComponent<TComponent>([NotNull] TComponent component) where TComponent : IServiceComponent;

		// Token: 0x060000DD RID: 221
		TComponent GetServiceComponent<TComponent>() where TComponent : IServiceComponent;

		// Token: 0x060000DE RID: 222
		void ResetProvidedComponents(IDictionary<int, IServiceComponent> componentTypeHashToInstance);
	}
}
