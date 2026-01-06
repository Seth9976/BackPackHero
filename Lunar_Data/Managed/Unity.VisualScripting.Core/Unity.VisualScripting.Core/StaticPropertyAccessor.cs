using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000121 RID: 289
	public class StaticPropertyAccessor<TProperty> : IOptimizedAccessor
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x00022404 File Offset: 0x00020604
		public StaticPropertyAccessor(PropertyInfo propertyInfo)
		{
			if (OptimizedReflection.safeMode)
			{
				if (propertyInfo == null)
				{
					throw new ArgumentNullException("propertyInfo");
				}
				if (propertyInfo.PropertyType != typeof(TProperty))
				{
					throw new ArgumentException("The property type of the property info doesn't match the generic type.", "propertyInfo");
				}
				if (!propertyInfo.IsStatic())
				{
					throw new ArgumentException("The property isn't static.", "propertyInfo");
				}
			}
			this.propertyInfo = propertyInfo;
			this.targetType = propertyInfo.DeclaringType;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00022484 File Offset: 0x00020684
		public void Compile()
		{
			MethodInfo getMethod = this.propertyInfo.GetGetMethod(true);
			MethodInfo setMethod = this.propertyInfo.GetSetMethod(true);
			if (OptimizedReflection.useJit)
			{
				if (getMethod != null)
				{
					MemberExpression memberExpression = Expression.Property(null, this.propertyInfo);
					this.getter = Expression.Lambda<Func<TProperty>>(memberExpression, Array.Empty<ParameterExpression>()).Compile();
				}
				if (setMethod != null)
				{
					this.setter = (Action<TProperty>)setMethod.CreateDelegate(typeof(Action<TProperty>));
					return;
				}
			}
			else
			{
				if (getMethod != null)
				{
					this.getter = (Func<TProperty>)getMethod.CreateDelegate(typeof(Func<TProperty>));
				}
				if (setMethod != null)
				{
					this.setter = (Action<TProperty>)setMethod.CreateDelegate(typeof(Action<TProperty>));
				}
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0002254C File Offset: 0x0002074C
		public object GetValue(object target)
		{
			if (OptimizedReflection.safeMode)
			{
				OptimizedReflection.VerifyStaticTarget(this.targetType, target);
				if (this.getter == null)
				{
					throw new TargetException(string.Format("The property '{0}.{1}' has no get accessor.", this.targetType, this.propertyInfo.Name));
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

		// Token: 0x0600079F RID: 1951 RVA: 0x000225D0 File Offset: 0x000207D0
		private object GetValueUnsafe(object target)
		{
			return this.getter();
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000225E4 File Offset: 0x000207E4
		public void SetValue(object target, object value)
		{
			if (OptimizedReflection.safeMode)
			{
				OptimizedReflection.VerifyStaticTarget(this.targetType, target);
				if (this.setter == null)
				{
					throw new TargetException(string.Format("The property '{0}.{1}' has no set accessor.", this.targetType, this.propertyInfo.Name));
				}
				if (!typeof(TProperty).IsAssignableFrom(value))
				{
					string text = "The provided value for '{0}.{1}' does not match the property type.\nProvided: {2}\nExpected: {3}";
					object[] array = new object[4];
					array[0] = this.targetType;
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

		// Token: 0x060007A1 RID: 1953 RVA: 0x000226D8 File Offset: 0x000208D8
		private void SetValueUnsafe(object target, object value)
		{
			this.setter((TProperty)((object)value));
		}

		// Token: 0x040001C3 RID: 451
		private readonly PropertyInfo propertyInfo;

		// Token: 0x040001C4 RID: 452
		private Func<TProperty> getter;

		// Token: 0x040001C5 RID: 453
		private Action<TProperty> setter;

		// Token: 0x040001C6 RID: 454
		private Type targetType;
	}
}
