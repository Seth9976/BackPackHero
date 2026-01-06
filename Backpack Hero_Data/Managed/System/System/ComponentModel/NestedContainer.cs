using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides the base implementation for the <see cref="T:System.ComponentModel.INestedContainer" /> interface, which enables containers to have an owning component.</summary>
	// Token: 0x020006F0 RID: 1776
	public class NestedContainer : Container, INestedContainer, IContainer, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NestedContainer" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.ComponentModel.IComponent" /> that owns this nested container.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x060038A7 RID: 14503 RVA: 0x000C6372 File Offset: 0x000C4572
		public NestedContainer(IComponent owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.Owner = owner;
			this.Owner.Disposed += this.OnOwnerDisposed;
		}

		/// <summary>Gets the owning component for this nested container.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that owns this nested container.</returns>
		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x060038A8 RID: 14504 RVA: 0x000C63A6 File Offset: 0x000C45A6
		public IComponent Owner { get; }

		/// <summary>Gets the name of the owning component.</summary>
		/// <returns>The name of the owning component.</returns>
		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x000C63B0 File Offset: 0x000C45B0
		protected virtual string OwnerName
		{
			get
			{
				string text = null;
				if (this.Owner != null && this.Owner.Site != null)
				{
					INestedSite nestedSite = this.Owner.Site as INestedSite;
					if (nestedSite != null)
					{
						text = nestedSite.FullName;
					}
					else
					{
						text = this.Owner.Site.Name;
					}
				}
				return text;
			}
		}

		/// <summary>Creates a site for the component within the container.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.ISite" />.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to create a site for.</param>
		/// <param name="name">The name to assign to <paramref name="component" />, or null to skip the name assignment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		// Token: 0x060038AA RID: 14506 RVA: 0x000C6403 File Offset: 0x000C4603
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return new NestedContainer.Site(component, this, name);
		}

		/// <summary>Releases the resources used by the nested container.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060038AB RID: 14507 RVA: 0x000C641B File Offset: 0x000C461B
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Owner.Disposed -= this.OnOwnerDisposed;
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets the service object of the specified type, if it is available.</summary>
		/// <returns>An <see cref="T:System.Object" /> that implements the requested service, or null if the service cannot be resolved.</returns>
		/// <param name="service">The <see cref="T:System.Type" /> of the service to retrieve.</param>
		// Token: 0x060038AC RID: 14508 RVA: 0x000C643E File Offset: 0x000C463E
		protected override object GetService(Type service)
		{
			if (service == typeof(INestedContainer))
			{
				return this;
			}
			return base.GetService(service);
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000C645B File Offset: 0x000C465B
		private void OnOwnerDisposed(object sender, EventArgs e)
		{
			base.Dispose();
		}

		// Token: 0x020006F1 RID: 1777
		private class Site : INestedSite, ISite, IServiceProvider
		{
			// Token: 0x060038AE RID: 14510 RVA: 0x000C6463 File Offset: 0x000C4663
			internal Site(IComponent component, NestedContainer container, string name)
			{
				this.Component = component;
				this.Container = container;
				this._name = name;
			}

			// Token: 0x17000D14 RID: 3348
			// (get) Token: 0x060038AF RID: 14511 RVA: 0x000C6480 File Offset: 0x000C4680
			public IComponent Component { get; }

			// Token: 0x17000D15 RID: 3349
			// (get) Token: 0x060038B0 RID: 14512 RVA: 0x000C6488 File Offset: 0x000C4688
			public IContainer Container { get; }

			// Token: 0x060038B1 RID: 14513 RVA: 0x000C6490 File Offset: 0x000C4690
			public object GetService(Type service)
			{
				if (!(service == typeof(ISite)))
				{
					return ((NestedContainer)this.Container).GetService(service);
				}
				return this;
			}

			// Token: 0x17000D16 RID: 3350
			// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000C64B8 File Offset: 0x000C46B8
			public bool DesignMode
			{
				get
				{
					IComponent owner = ((NestedContainer)this.Container).Owner;
					return owner != null && owner.Site != null && owner.Site.DesignMode;
				}
			}

			// Token: 0x17000D17 RID: 3351
			// (get) Token: 0x060038B3 RID: 14515 RVA: 0x000C64F0 File Offset: 0x000C46F0
			public string FullName
			{
				get
				{
					if (this._name != null)
					{
						string ownerName = ((NestedContainer)this.Container).OwnerName;
						string text = this._name;
						if (ownerName != null)
						{
							text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", ownerName, text);
						}
						return text;
					}
					return this._name;
				}
			}

			// Token: 0x17000D18 RID: 3352
			// (get) Token: 0x060038B4 RID: 14516 RVA: 0x000C653A File Offset: 0x000C473A
			// (set) Token: 0x060038B5 RID: 14517 RVA: 0x000C6542 File Offset: 0x000C4742
			public string Name
			{
				get
				{
					return this._name;
				}
				set
				{
					if (value == null || this._name == null || !value.Equals(this._name))
					{
						((NestedContainer)this.Container).ValidateName(this.Component, value);
						this._name = value;
					}
				}
			}

			// Token: 0x04002120 RID: 8480
			private string _name;
		}
	}
}
