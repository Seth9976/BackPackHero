using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x020006FB RID: 1787
	internal sealed class ReflectEventDescriptor : EventDescriptor
	{
		// Token: 0x0600393E RID: 14654 RVA: 0x000C7C30 File Offset: 0x000C5E30
		public ReflectEventDescriptor(Type componentClass, string name, Type type, Attribute[] attributes)
			: base(name, attributes)
		{
			if (componentClass == null)
			{
				throw new ArgumentException(SR.Format("Null is not a valid value for {0}.", "componentClass"));
			}
			if (type == null || !typeof(Delegate).IsAssignableFrom(type))
			{
				throw new ArgumentException(SR.Format("Invalid type for the {0} event.", name));
			}
			this._componentClass = componentClass;
			this._type = type;
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000C7C9E File Offset: 0x000C5E9E
		public ReflectEventDescriptor(Type componentClass, EventInfo eventInfo)
			: base(eventInfo.Name, Array.Empty<Attribute>())
		{
			if (componentClass == null)
			{
				throw new ArgumentException(SR.Format("Null is not a valid value for {0}.", "componentClass"));
			}
			this._componentClass = componentClass;
			this._realEvent = eventInfo;
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000C7CE0 File Offset: 0x000C5EE0
		public ReflectEventDescriptor(Type componentType, EventDescriptor oldReflectEventDescriptor, Attribute[] attributes)
			: base(oldReflectEventDescriptor, attributes)
		{
			this._componentClass = componentType;
			this._type = oldReflectEventDescriptor.EventType;
			ReflectEventDescriptor reflectEventDescriptor = oldReflectEventDescriptor as ReflectEventDescriptor;
			if (reflectEventDescriptor != null)
			{
				this._addMethod = reflectEventDescriptor._addMethod;
				this._removeMethod = reflectEventDescriptor._removeMethod;
				this._filledMethods = true;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x000C7D31 File Offset: 0x000C5F31
		public override Type ComponentType
		{
			get
			{
				return this._componentClass;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06003942 RID: 14658 RVA: 0x000C7D39 File Offset: 0x000C5F39
		public override Type EventType
		{
			get
			{
				this.FillMethods();
				return this._type;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06003943 RID: 14659 RVA: 0x000C7D47 File Offset: 0x000C5F47
		public override bool IsMulticast
		{
			get
			{
				return typeof(MulticastDelegate).IsAssignableFrom(this.EventType);
			}
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000C7D60 File Offset: 0x000C5F60
		public override void AddEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
					componentChangeService.OnComponentChanging(component, this);
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					if (this.EventType != value.GetType())
					{
						throw new ArgumentException(SR.Format("Invalid event handler for the {0} event.", this.Name));
					}
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Combine(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					MethodBase addMethod = this._addMethod;
					object[] array = new Delegate[] { value };
					addMethod.Invoke(component, array);
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000C7E74 File Offset: 0x000C6074
		protected override void FillAttributes(IList attributes)
		{
			this.FillMethods();
			if (this._realEvent != null)
			{
				this.FillEventInfoAttribute(this._realEvent, attributes);
			}
			else
			{
				this.FillSingleMethodAttribute(this._removeMethod, attributes);
				this.FillSingleMethodAttribute(this._addMethod, attributes);
			}
			base.FillAttributes(attributes);
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000C7EC8 File Offset: 0x000C60C8
		private void FillEventInfoAttribute(EventInfo realEventInfo, IList attributes)
		{
			string name = realEventInfo.Name;
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realEventInfo.ReflectedType;
			int num = 0;
			while (type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realEventInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != typeof(object))
				{
					MemberInfo @event = type.GetEvent(name, bindingFlags);
					if (@event != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(@event);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute in array3)
						{
							attributes.Add(attribute);
						}
					}
				}
			}
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000C7FA4 File Offset: 0x000C61A4
		private void FillMethods()
		{
			if (this._filledMethods)
			{
				return;
			}
			if (this._realEvent != null)
			{
				this._addMethod = this._realEvent.GetAddMethod();
				this._removeMethod = this._realEvent.GetRemoveMethod();
				EventInfo eventInfo = null;
				if (this._addMethod == null || this._removeMethod == null)
				{
					Type baseType = this._componentClass.BaseType;
					while (baseType != null && baseType != typeof(object))
					{
						BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
						EventInfo @event = baseType.GetEvent(this._realEvent.Name, bindingFlags);
						if (@event.GetAddMethod() != null)
						{
							eventInfo = @event;
							break;
						}
					}
				}
				if (eventInfo != null)
				{
					this._addMethod = eventInfo.GetAddMethod();
					this._removeMethod = eventInfo.GetRemoveMethod();
					this._type = eventInfo.EventHandlerType;
				}
				else
				{
					this._type = this._realEvent.EventHandlerType;
				}
			}
			else
			{
				this._realEvent = this._componentClass.GetEvent(this.Name);
				if (this._realEvent != null)
				{
					this.FillMethods();
					return;
				}
				Type[] array = new Type[] { this._type };
				this._addMethod = MemberDescriptor.FindMethod(this._componentClass, "AddOn" + this.Name, array, typeof(void));
				this._removeMethod = MemberDescriptor.FindMethod(this._componentClass, "RemoveOn" + this.Name, array, typeof(void));
				if (this._addMethod == null || this._removeMethod == null)
				{
					throw new ArgumentException(SR.Format("Accessor methods for the {0} event are missing.", this.Name));
				}
			}
			this._filledMethods = true;
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x000C8174 File Offset: 0x000C6374
		private void FillSingleMethodAttribute(MethodInfo realMethodInfo, IList attributes)
		{
			string name = realMethodInfo.Name;
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realMethodInfo.ReflectedType;
			int num = 0;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realMethodInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != null && type != typeof(object))
				{
					MemberInfo method = type.GetMethod(name, bindingFlags);
					if (method != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(method);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute in array3)
						{
							attributes.Add(attribute);
						}
					}
				}
			}
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000C8264 File Offset: 0x000C6464
		public override void RemoveEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
					componentChangeService.OnComponentChanging(component, this);
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Remove(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					MethodBase removeMethod = this._removeMethod;
					object[] array = new Delegate[] { value };
					removeMethod.Invoke(component, array);
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x04002145 RID: 8517
		private Type _type;

		// Token: 0x04002146 RID: 8518
		private readonly Type _componentClass;

		// Token: 0x04002147 RID: 8519
		private MethodInfo _addMethod;

		// Token: 0x04002148 RID: 8520
		private MethodInfo _removeMethod;

		// Token: 0x04002149 RID: 8521
		private EventInfo _realEvent;

		// Token: 0x0400214A RID: 8522
		private bool _filledMethods;
	}
}
