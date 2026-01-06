using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000032 RID: 50
	internal class ComponentRegistry : IComponentRegistry
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002ACD File Offset: 0x00000CCD
		[NotNull]
		internal Dictionary<int, IServiceComponent> ComponentTypeHashToInstance { get; }

		// Token: 0x060000D7 RID: 215 RVA: 0x00002AD5 File Offset: 0x00000CD5
		public ComponentRegistry([NotNull] Dictionary<int, IServiceComponent> componentTypeHashToInstance)
		{
			this.ComponentTypeHashToInstance = componentTypeHashToInstance;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public void RegisterServiceComponent<TComponent>(TComponent component) where TComponent : IServiceComponent
		{
			Type typeFromHandle = typeof(TComponent);
			if (component.GetType() == typeFromHandle)
			{
				throw new ArgumentException("Interface type of component not specified.");
			}
			int hashCode = typeFromHandle.GetHashCode();
			if (this.IsComponentTypeRegistered(hashCode))
			{
				throw new InvalidOperationException("A component with the type " + typeFromHandle.FullName + " has already been registered.");
			}
			this.ComponentTypeHashToInstance[hashCode] = component;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00002B5C File Offset: 0x00000D5C
		public TComponent GetServiceComponent<TComponent>() where TComponent : IServiceComponent
		{
			Type typeFromHandle = typeof(TComponent);
			IServiceComponent serviceComponent;
			if (!this.ComponentTypeHashToInstance.TryGetValue(typeFromHandle.GetHashCode(), out serviceComponent) || serviceComponent is MissingComponent)
			{
				throw new KeyNotFoundException("There is no component `" + typeFromHandle.Name + "` registered. Are you missing a package?");
			}
			return (TComponent)((object)serviceComponent);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00002BB4 File Offset: 0x00000DB4
		private bool IsComponentTypeRegistered(int componentTypeHash)
		{
			IServiceComponent serviceComponent;
			return this.ComponentTypeHashToInstance.TryGetValue(componentTypeHash, out serviceComponent) && serviceComponent != null && !(serviceComponent is MissingComponent);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public void ResetProvidedComponents(IDictionary<int, IServiceComponent> componentTypeHashToInstance)
		{
			this.ComponentTypeHashToInstance.Clear();
			this.ComponentTypeHashToInstance.MergeAllowOverride(componentTypeHashToInstance);
		}
	}
}
