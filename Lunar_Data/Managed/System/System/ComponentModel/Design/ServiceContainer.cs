using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a simple implementation of the <see cref="T:System.ComponentModel.Design.IServiceContainer" /> interface. This class cannot be inherited.</summary>
	// Token: 0x02000787 RID: 1927
	public class ServiceContainer : IServiceContainer, IServiceProvider, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class.</summary>
		// Token: 0x06003D1F RID: 15647 RVA: 0x0000219B File Offset: 0x0000039B
		public ServiceContainer()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class using the specified parent service provider.</summary>
		/// <param name="parentProvider">A parent service provider. </param>
		// Token: 0x06003D20 RID: 15648 RVA: 0x000D8709 File Offset: 0x000D6909
		public ServiceContainer(IServiceProvider parentProvider)
		{
			this._parentProvider = parentProvider;
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x000D8718 File Offset: 0x000D6918
		private IServiceContainer Container
		{
			get
			{
				IServiceContainer serviceContainer = null;
				if (this._parentProvider != null)
				{
					serviceContainer = (IServiceContainer)this._parentProvider.GetService(typeof(IServiceContainer));
				}
				return serviceContainer;
			}
		}

		/// <summary>Gets the default services implemented directly by <see cref="T:System.ComponentModel.Design.ServiceContainer" />.</summary>
		/// <returns>The default services.</returns>
		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000D874B File Offset: 0x000D694B
		protected virtual Type[] DefaultServices
		{
			get
			{
				return ServiceContainer.s_defaultServices;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x000D8754 File Offset: 0x000D6954
		private ServiceContainer.ServiceCollection<object> Services
		{
			get
			{
				ServiceContainer.ServiceCollection<object> serviceCollection;
				if ((serviceCollection = this._services) == null)
				{
					serviceCollection = (this._services = new ServiceContainer.ServiceCollection<object>());
				}
				return serviceCollection;
			}
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06003D24 RID: 15652 RVA: 0x000D8779 File Offset: 0x000D6979
		public void AddService(Type serviceType, object serviceInstance)
		{
			this.AddService(serviceType, serviceInstance, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		/// <param name="promote">true if this service should be added to any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06003D25 RID: 15653 RVA: 0x000D8784 File Offset: 0x000D6984
		public virtual void AddService(Type serviceType, object serviceInstance, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, serviceInstance, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (serviceInstance == null)
			{
				throw new ArgumentNullException("serviceInstance");
			}
			if (!(serviceInstance is ServiceCreatorCallback) && !serviceInstance.GetType().IsCOMObject && !serviceType.IsInstanceOfType(serviceInstance))
			{
				throw new ArgumentException(SR.Format("The service instance must derive from or implement {0}.", serviceType.FullName));
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(SR.Format("The service {0} already exists in the service container.", serviceType.FullName), "serviceType");
			}
			this.Services[serviceType] = serviceInstance;
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06003D26 RID: 15654 RVA: 0x000D8834 File Offset: 0x000D6A34
		public void AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			this.AddService(serviceType, callback, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested. </param>
		/// <param name="promote">true if this service should be added to any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06003D27 RID: 15655 RVA: 0x000D8840 File Offset: 0x000D6A40
		public virtual void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, callback, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(SR.Format("The service {0} already exists in the service container.", serviceType.FullName), "serviceType");
			}
			this.Services[serviceType] = callback;
		}

		/// <summary>Disposes this service container.</summary>
		// Token: 0x06003D28 RID: 15656 RVA: 0x000D88BC File Offset: 0x000D6ABC
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Disposes this service container.</summary>
		/// <param name="disposing">true if the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> is in the process of being disposed of; otherwise, false.</param>
		// Token: 0x06003D29 RID: 15657 RVA: 0x000D88C8 File Offset: 0x000D6AC8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				ServiceContainer.ServiceCollection<object> services = this._services;
				this._services = null;
				if (services != null)
				{
					foreach (object obj in services.Values)
					{
						if (obj is IDisposable)
						{
							((IDisposable)obj).Dispose();
						}
					}
				}
			}
		}

		/// <summary>Gets the requested service.</summary>
		/// <returns>An instance of the service if it could be found, or null if it could not be found.</returns>
		/// <param name="serviceType">The type of service to retrieve. </param>
		// Token: 0x06003D2A RID: 15658 RVA: 0x000D893C File Offset: 0x000D6B3C
		public virtual object GetService(Type serviceType)
		{
			object obj = null;
			Type[] defaultServices = this.DefaultServices;
			for (int i = 0; i < defaultServices.Length; i++)
			{
				if (serviceType.IsEquivalentTo(defaultServices[i]))
				{
					obj = this;
					break;
				}
			}
			if (obj == null)
			{
				this.Services.TryGetValue(serviceType, out obj);
			}
			if (obj is ServiceCreatorCallback)
			{
				obj = ((ServiceCreatorCallback)obj)(this, serviceType);
				if (obj != null && !obj.GetType().IsCOMObject && !serviceType.IsInstanceOfType(obj))
				{
					obj = null;
				}
				this.Services[serviceType] = obj;
			}
			if (obj == null && this._parentProvider != null)
			{
				obj = this._parentProvider.GetService(serviceType);
			}
			return obj;
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is null.</exception>
		// Token: 0x06003D2B RID: 15659 RVA: 0x000D89D8 File Offset: 0x000D6BD8
		public void RemoveService(Type serviceType)
		{
			this.RemoveService(serviceType, false);
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		/// <param name="promote">true if this service should be removed from any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is null.</exception>
		// Token: 0x06003D2C RID: 15660 RVA: 0x000D89E4 File Offset: 0x000D6BE4
		public virtual void RemoveService(Type serviceType, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.RemoveService(serviceType, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			this.Services.Remove(serviceType);
		}

		// Token: 0x04002251 RID: 8785
		private ServiceContainer.ServiceCollection<object> _services;

		// Token: 0x04002252 RID: 8786
		private IServiceProvider _parentProvider;

		// Token: 0x04002253 RID: 8787
		private static Type[] s_defaultServices = new Type[]
		{
			typeof(IServiceContainer),
			typeof(ServiceContainer)
		};

		// Token: 0x04002254 RID: 8788
		private static TraceSwitch s_TRACESERVICE = new TraceSwitch("TRACESERVICE", "ServiceProvider: Trace service provider requests.");

		// Token: 0x02000788 RID: 1928
		private sealed class ServiceCollection<T> : Dictionary<Type, T>
		{
			// Token: 0x06003D2E RID: 15662 RVA: 0x000D8A63 File Offset: 0x000D6C63
			public ServiceCollection()
				: base(ServiceContainer.ServiceCollection<T>.s_serviceTypeComparer)
			{
			}

			// Token: 0x04002255 RID: 8789
			private static ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer s_serviceTypeComparer = new ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer();

			// Token: 0x02000789 RID: 1929
			private sealed class EmbeddedTypeAwareTypeComparer : IEqualityComparer<Type>
			{
				// Token: 0x06003D30 RID: 15664 RVA: 0x000D8A7C File Offset: 0x000D6C7C
				public bool Equals(Type x, Type y)
				{
					return x.IsEquivalentTo(y);
				}

				// Token: 0x06003D31 RID: 15665 RVA: 0x000D8A85 File Offset: 0x000D6C85
				public int GetHashCode(Type obj)
				{
					return obj.FullName.GetHashCode();
				}
			}
		}
	}
}
