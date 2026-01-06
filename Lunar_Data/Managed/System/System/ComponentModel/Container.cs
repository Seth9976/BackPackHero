using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Encapsulates zero or more components.</summary>
	// Token: 0x02000724 RID: 1828
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Container : IContainer, IDisposable
	{
		// Token: 0x06003A12 RID: 14866 RVA: 0x000C9700 File Offset: 0x000C7900
		~Container()
		{
			this.Dispose(false);
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" />. The component is unnamed.</summary>
		/// <param name="component">The component to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003A13 RID: 14867 RVA: 0x000C9730 File Offset: 0x000C7930
		public virtual void Add(IComponent component)
		{
			this.Add(component, null);
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" /> and assigns it a name.</summary>
		/// <param name="component">The component to add. </param>
		/// <param name="name">The unique, case-insensitive name to assign to the component.-or- null, which leaves the component unnamed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x06003A14 RID: 14868 RVA: 0x000C973C File Offset: 0x000C793C
		public virtual void Add(IComponent component, string name)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site == null || site.Container != this)
					{
						if (this.sites == null)
						{
							this.sites = new ISite[4];
						}
						else
						{
							this.ValidateName(component, name);
							if (this.sites.Length == this.siteCount)
							{
								ISite[] array = new ISite[this.siteCount * 2];
								Array.Copy(this.sites, 0, array, 0, this.siteCount);
								this.sites = array;
							}
						}
						if (site != null)
						{
							site.Container.Remove(component);
						}
						ISite site2 = this.CreateSite(component, name);
						ISite[] array2 = this.sites;
						int num = this.siteCount;
						this.siteCount = num + 1;
						array2[num] = site2;
						component.Site = site2;
						this.components = null;
					}
				}
			}
		}

		/// <summary>Creates a site <see cref="T:System.ComponentModel.ISite" /> for the given <see cref="T:System.ComponentModel.IComponent" /> and assigns the given name to the site.</summary>
		/// <returns>The newly created site.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to create a site for. </param>
		/// <param name="name">The name to assign to <paramref name="component" />, or null to skip the name assignment. </param>
		// Token: 0x06003A15 RID: 14869 RVA: 0x000C9834 File Offset: 0x000C7A34
		protected virtual ISite CreateSite(IComponent component, string name)
		{
			return new Container.Site(component, this, name);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Container" />.</summary>
		// Token: 0x06003A16 RID: 14870 RVA: 0x000C983E File Offset: 0x000C7A3E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Container" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003A17 RID: 14871 RVA: 0x000C9850 File Offset: 0x000C7A50
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				object obj = this.syncObj;
				lock (obj)
				{
					while (this.siteCount > 0)
					{
						ISite[] array = this.sites;
						int num = this.siteCount - 1;
						this.siteCount = num;
						object obj2 = array[num];
						((ISite)obj2).Component.Site = null;
						((ISite)obj2).Component.Dispose();
					}
					this.sites = null;
					this.components = null;
				}
			}
		}

		/// <summary>Gets the service object of the specified type, if it is available.</summary>
		/// <returns>An <see cref="T:System.Object" /> implementing the requested service, or null if the service cannot be resolved.</returns>
		/// <param name="service">The <see cref="T:System.Type" /> of the service to retrieve. </param>
		// Token: 0x06003A18 RID: 14872 RVA: 0x000C98D4 File Offset: 0x000C7AD4
		protected virtual object GetService(Type service)
		{
			if (!(service == typeof(IContainer)))
			{
				return null;
			}
			return this;
		}

		/// <summary>Gets all the components in the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <returns>A collection that contains the components in the <see cref="T:System.ComponentModel.Container" />.</returns>
		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06003A19 RID: 14873 RVA: 0x000C98EC File Offset: 0x000C7AEC
		public virtual ComponentCollection Components
		{
			get
			{
				object obj = this.syncObj;
				ComponentCollection componentCollection2;
				lock (obj)
				{
					if (this.components == null)
					{
						IComponent[] array = new IComponent[this.siteCount];
						for (int i = 0; i < this.siteCount; i++)
						{
							array[i] = this.sites[i].Component;
						}
						this.components = new ComponentCollection(array);
						if (this.filter == null && this.checkedFilter)
						{
							this.checkedFilter = false;
						}
					}
					if (!this.checkedFilter)
					{
						this.filter = this.GetService(typeof(ContainerFilterService)) as ContainerFilterService;
						this.checkedFilter = true;
					}
					if (this.filter != null)
					{
						ComponentCollection componentCollection = this.filter.FilterComponents(this.components);
						if (componentCollection != null)
						{
							this.components = componentCollection;
						}
					}
					componentCollection2 = this.components;
				}
				return componentCollection2;
			}
		}

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <param name="component">The component to remove. </param>
		// Token: 0x06003A1A RID: 14874 RVA: 0x000C99DC File Offset: 0x000C7BDC
		public virtual void Remove(IComponent component)
		{
			this.Remove(component, false);
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000C99E8 File Offset: 0x000C7BE8
		private void Remove(IComponent component, bool preserveSite)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null && site.Container == this)
					{
						if (!preserveSite)
						{
							component.Site = null;
						}
						for (int i = 0; i < this.siteCount; i++)
						{
							if (this.sites[i] == site)
							{
								this.siteCount--;
								Array.Copy(this.sites, i + 1, this.sites, i, this.siteCount - i);
								this.sites[this.siteCount] = null;
								this.components = null;
								break;
							}
						}
					}
				}
			}
		}

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.Container" /> without setting <see cref="P:System.ComponentModel.IComponent.Site" /> to null.</summary>
		/// <param name="component">The component to remove.</param>
		// Token: 0x06003A1C RID: 14876 RVA: 0x000C9AA8 File Offset: 0x000C7CA8
		protected void RemoveWithoutUnsiting(IComponent component)
		{
			this.Remove(component, true);
		}

		/// <summary>Determines whether the component name is unique for this container.</summary>
		/// <param name="component">The named component.</param>
		/// <param name="name">The component name to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x06003A1D RID: 14877 RVA: 0x000C9AB4 File Offset: 0x000C7CB4
		protected virtual void ValidateName(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (name != null)
			{
				for (int i = 0; i < Math.Min(this.siteCount, this.sites.Length); i++)
				{
					ISite site = this.sites[i];
					if (site != null && site.Name != null && string.Equals(site.Name, name, StringComparison.OrdinalIgnoreCase) && site.Component != component && ((InheritanceAttribute)TypeDescriptor.GetAttributes(site.Component)[typeof(InheritanceAttribute)]).InheritanceLevel != InheritanceLevel.InheritedReadOnly)
					{
						throw new ArgumentException(SR.GetString("Duplicate component name '{0}'.  Component names must be unique and case-insensitive.", new object[] { name }));
					}
				}
			}
		}

		// Token: 0x04002184 RID: 8580
		private ISite[] sites;

		// Token: 0x04002185 RID: 8581
		private int siteCount;

		// Token: 0x04002186 RID: 8582
		private ComponentCollection components;

		// Token: 0x04002187 RID: 8583
		private ContainerFilterService filter;

		// Token: 0x04002188 RID: 8584
		private bool checkedFilter;

		// Token: 0x04002189 RID: 8585
		private object syncObj = new object();

		// Token: 0x02000725 RID: 1829
		private class Site : ISite, IServiceProvider
		{
			// Token: 0x06003A1F RID: 14879 RVA: 0x000C9B76 File Offset: 0x000C7D76
			internal Site(IComponent component, Container container, string name)
			{
				this.component = component;
				this.container = container;
				this.name = name;
			}

			// Token: 0x17000D70 RID: 3440
			// (get) Token: 0x06003A20 RID: 14880 RVA: 0x000C9B93 File Offset: 0x000C7D93
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x17000D71 RID: 3441
			// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000C9B9B File Offset: 0x000C7D9B
			public IContainer Container
			{
				get
				{
					return this.container;
				}
			}

			// Token: 0x06003A22 RID: 14882 RVA: 0x000C9BA3 File Offset: 0x000C7DA3
			public object GetService(Type service)
			{
				if (!(service == typeof(ISite)))
				{
					return this.container.GetService(service);
				}
				return this;
			}

			// Token: 0x17000D72 RID: 3442
			// (get) Token: 0x06003A23 RID: 14883 RVA: 0x00003062 File Offset: 0x00001262
			public bool DesignMode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000D73 RID: 3443
			// (get) Token: 0x06003A24 RID: 14884 RVA: 0x000C9BC5 File Offset: 0x000C7DC5
			// (set) Token: 0x06003A25 RID: 14885 RVA: 0x000C9BCD File Offset: 0x000C7DCD
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					if (value == null || this.name == null || !value.Equals(this.name))
					{
						this.container.ValidateName(this.component, value);
						this.name = value;
					}
				}
			}

			// Token: 0x0400218A RID: 8586
			private IComponent component;

			// Token: 0x0400218B RID: 8587
			private Container container;

			// Token: 0x0400218C RID: 8588
			private string name;
		}
	}
}
