using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000109 RID: 265
	public class InstancePropertyAccessor<TTarget, TProperty> : IOptimizedAccessor
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x0001FEEC File Offset: 0x0001E0EC
		public InstancePropertyAccessor(PropertyInfo propertyInfo)
		{
			if (OptimizedReflection.safeMode)
			{
				Ensure.That("propertyInfo").IsNotNull<PropertyInfo>(propertyInfo);
				if (propertyInfo.DeclaringType != typeof(TTarget))
				{
					throw new ArgumentException("The declaring type of the property info doesn't match the generic type.", "propertyInfo");
				}
				if (propertyInfo.PropertyType != typeof(TProperty))
				{
					throw new ArgumentException("The property type of the property info doesn't match the generic type.", "propertyInfo");
				}
				if (propertyInfo.IsStatic())
				{
					throw new ArgumentException("The property is static.", "propertyInfo");
				}
			}
			this.propertyInfo = propertyInfo;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001FF84 File Offset: 0x0001E184
		public void Compile()
		{
			MethodInfo getMethod = this.propertyInfo.GetGetMethod(true);
			MethodInfo setMethod = this.propertyInfo.GetSetMethod(true);
			if (OptimizedReflection.useJit)
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(TTarget), "target");
				if (getMethod != null)
				{
					MemberExpression memberExpression = Expression.Property(parameterExpression, this.propertyInfo);
					this.getter = Expression.Lambda<Func<TTarget, TProperty>>(memberExpression, new ParameterExpression[] { parameterExpression }).Compile();
				}
				if (setMethod != null)
				{
					this.setter = (Action<TTarget, TProperty>)setMethod.CreateDelegate(typeof(Action<TTarget, TProperty>));
					return;
				}
			}
			else
			{
				if (getMethod != null)
				{
					this.getter = (Func<TTarget, TProperty>)getMethod.CreateDelegate(typeof(Func<TTarget, TProperty>));
				}
				if (setMethod != null)
				{
					this.setter = (Action<TTarget, TProperty>)setMethod.CreateDelegate(typeof(Action<TTarget, TProperty>));
				}
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00020068 File Offset: 0x0001E268
		public object GetValue(object target)
		{
			if (OptimizedReflection.safeMode)
			{
				OptimizedReflection.VerifyInstanceTarget<TTarget>(target);
				if (this.getter == null)
				{
					throw new TargetException(string.Format("The property '{0}.{1}' has no get accessor.", typeof(TTarget), this.propertyInfo.Name));
				}
				try
				{
					return this.GetValueUnsafe(target);
				}
				catch (TargetInvocationException)
				{
					throw;
				}
				catch (Exception ex)
				{
					throw new TargetInvocationException(ex);
				}
			}
			return this.GetValueUnsafe(target);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000200E8 File Offset: 0x0001E2E8
		private object GetValueUnsafe(object target)
		{
			return this.getter((TTarget)((object)target));
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00020100 File Offset: 0x0001E300
		public void SetValue(object target, object value)
		{
			if (OptimizedReflection.safeMode)
			{
				OptimizedReflection.VerifyInstanceTarget<TTarget>(target);
				if (this.setter == null)
				{
					throw new TargetException(string.Format("The property '{0}.{1}' has no set accessor.", typeof(TTarget), this.propertyInfo.Name));
				}
				if (!typeof(TProperty).IsAssignableFrom(value))
				{
					string text = "The provided value for '{0}.{1}' does not match the property type.\nProvided: {2}\nExpected: {3}";
					object[] array = new object[4];
					array[0] = typeof(TTarget);
					array[1] = this.propertyInfo.Name;
					int num = 2;
					object obj;
					if (value == null)
					{
						obj = null;
					}
					else
					{
						Type type = value.GetType();
						obj = ((type != null) ? type.ToString() : null);
					}
					array[num] = obj ?? "null";
					array[3] = typeof(TProperty);
					throw new ArgumentException(string.Format(text, array));
				}
				try
				{
					this.SetValueUnsafe(target, value);
					return;
				}
				catch (TargetInvocationException)
				{
					throw;
				}
				catch (Exception ex)
				{
					throw new TargetInvocationException(ex);
				}
			}
			this.SetValueUnsafe(target, value);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000201F8 File Offset: 0x0001E3F8
		private void SetValueUnsafe(object target, object value)
		{
			this.setter((TTarget)((object)target), (TProperty)((object)value));
		}

		// Token: 0x040001A4 RID: 420
		private readonly PropertyInfo propertyInfo;

		// Token: 0x040001A5 RID: 421
		private Func<TTarget, TProperty> getter;

		// Token: 0x040001A6 RID: 422
		private Action<TTarget, TProperty> setter;
	}
}
