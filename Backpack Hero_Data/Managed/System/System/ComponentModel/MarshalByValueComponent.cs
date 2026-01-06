using System;
using System.ComponentModel.Design;

namespace System.ComponentModel
{
	/// <summary>Implements <see cref="T:System.ComponentModel.IComponent" /> and provides the base implementation for remotable components that are marshaled by value (a copy of the serialized object is passed).</summary>
	// Token: 0x020006E9 RID: 1769
	[DesignerCategory("Component")]
	[TypeConverter(typeof(ComponentConverter))]
	[Designer("System.Windows.Forms.Design.ComponentDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	public class MarshalByValueComponent : IComponent, IDisposable, IServiceProvider
	{
		// Token: 0x06003823 RID: 14371 RVA: 0x000C43BC File Offset: 0x000C25BC
		~MarshalByValueComponent()
		{
			this.Dispose(false);
		}

		/// <summary>Adds an event handler to listen to the <see cref="E:System.ComponentModel.MarshalByValueComponent.Disposed" /> event on the component.</summary>
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06003824 RID: 14372 RVA: 0x000C43EC File Offset: 0x000C25EC
		// (remove) Token: 0x06003825 RID: 14373 RVA: 0x000C43FF File Offset: 0x000C25FF
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(MarshalByValueComponent.s_eventDisposed, value);
			}
			remove
			{
				this.Events.RemoveHandler(MarshalByValueComponent.s_eventDisposed, value);
			}
		}

		/// <summary>Gets the list of event handlers that are attached to this component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that provides the delegates for this component.</returns>
		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06003826 RID: 14374 RVA: 0x000C4414 File Offset: 0x000C2614
		protected EventHandlerList Events
		{
			get
			{
				EventHandlerList eventHandlerList;
				if ((eventHandlerList = this._events) == null)
				{
					eventHandlerList = (this._events = new EventHandlerList());
				}
				return eventHandlerList;
			}
		}

		/// <summary>Gets or sets the site of the component.</summary>
		/// <returns>An object implementing the <see cref="T:System.ComponentModel.ISite" /> interface that represents the site of the component.</returns>
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06003827 RID: 14375 RVA: 0x000C4439 File Offset: 0x000C2639
		// (set) Token: 0x06003828 RID: 14376 RVA: 0x000C4441 File Offset: 0x000C2641
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ISite Site
		{
			get
			{
				return this._site;
			}
			set
			{
				this._site = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" />.</summary>
		// Token: 0x06003829 RID: 14377 RVA: 0x000C444A File Offset: 0x000C264A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600382A RID: 14378 RVA: 0x000C445C File Offset: 0x000C265C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					ISite site = this._site;
					if (site != null)
					{
						IContainer container = site.Container;
						if (container != null)
						{
							container.Remove(this);
						}
					}
					EventHandlerList events = this._events;
					EventHandler eventHandler = (EventHandler)((events != null) ? events[MarshalByValueComponent.s_eventDisposed] : null);
					if (eventHandler != null)
					{
						eventHandler(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets the container for the component.</summary>
		/// <returns>An object implementing the <see cref="T:System.ComponentModel.IContainer" /> interface that represents the component's container, or null if the component does not have a site.</returns>
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x0600382B RID: 14379 RVA: 0x000C44E0 File Offset: 0x000C26E0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IContainer Container
		{
			get
			{
				ISite site = this._site;
				if (site == null)
				{
					return null;
				}
				return site.Container;
			}
		}

		/// <summary>Gets the implementer of the <see cref="T:System.IServiceProvider" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the implementer of the <see cref="T:System.IServiceProvider" />.</returns>
		/// <param name="service">A <see cref="T:System.Type" /> that represents the type of service you want. </param>
		// Token: 0x0600382C RID: 14380 RVA: 0x000C44F3 File Offset: 0x000C26F3
		public virtual object GetService(Type service)
		{
			ISite site = this._site;
			if (site == null)
			{
				return null;
			}
			return site.GetService(service);
		}

		/// <summary>Gets a value indicating whether the component is currently in design mode.</summary>
		/// <returns>true if the component is in design mode; otherwise, false.</returns>
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x0600382D RID: 14381 RVA: 0x000C4507 File Offset: 0x000C2707
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool DesignMode
		{
			get
			{
				ISite site = this._site;
				return site != null && site.DesignMode;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any.null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x0600382E RID: 14382 RVA: 0x000C451C File Offset: 0x000C271C
		public override string ToString()
		{
			ISite site = this._site;
			if (site != null)
			{
				return site.Name + " [" + base.GetType().FullName + "]";
			}
			return base.GetType().FullName;
		}

		// Token: 0x040020E1 RID: 8417
		private static readonly object s_eventDisposed = new object();

		// Token: 0x040020E2 RID: 8418
		private ISite _site;

		// Token: 0x040020E3 RID: 8419
		private EventHandlerList _events;
	}
}
