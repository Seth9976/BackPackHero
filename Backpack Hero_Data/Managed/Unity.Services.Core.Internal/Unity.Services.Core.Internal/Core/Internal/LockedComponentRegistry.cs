using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000034 RID: 52
	internal class LockedComponentRegistry : IComponentRegistry
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00002BFC File Offset: 0x00000DFC
		[NotNull]
		internal IComponentRegistry Registry { get; }

		// Token: 0x060000E0 RID: 224 RVA: 0x00002C04 File Offset: 0x00000E04
		public LockedComponentRegistry([NotNull] IComponentRegistry registryToLock)
		{
			this.Registry = registryToLock;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00002C13 File Offset: 0x00000E13
		public void RegisterServiceComponent<TComponent>(TComponent component) where TComponent : IServiceComponent
		{
			throw new InvalidOperationException("Component registration has been locked. Make sure to register service components before all packages have finished initializing.");
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002C1F File Offset: 0x00000E1F
		public TComponent GetServiceComponent<TComponent>() where TComponent : IServiceComponent
		{
			return this.Registry.GetServiceComponent<TComponent>();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002C2C File Offset: 0x00000E2C
		public void ResetProvidedComponents(IDictionary<int, IServiceComponent> componentTypeHashToInstance)
		{
			throw new InvalidOperationException("Component registration has been locked. Make sure to register service components before all packages have finished initializing.");
		}

		// Token: 0x04000036 RID: 54
		private const string k_ErrorMessage = "Component registration has been locked. Make sure to register service components before all packages have finished initializing.";
	}
}
