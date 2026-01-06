using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000118 RID: 280
	public class StaticFieldAccessor<TField> : IOptimizedAccessor
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x000218F0 File Offset: 0x0001FAF0
		public StaticFieldAccessor(FieldInfo fieldInfo)
		{
			if (OptimizedReflection.safeMode)
			{
				if (fieldInfo == null)
				{
					throw new ArgumentNullException("fieldInfo");
				}
				if (fieldInfo.FieldType != typeof(TField))
				{
					throw new ArgumentException("Field type of field info doesn't match generic type.", "fieldInfo");
				}
				if (!fieldInfo.IsStatic)
				{
					throw new ArgumentException("The field isn't static.", "fieldInfo");
				}
			}
			this.fieldInfo = fieldInfo;
			this.targetType = fieldInfo.DeclaringType;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00021970 File Offset: 0x0001FB70
		public void Compile()
		{
			if (this.fieldInfo.IsLiteral)
			{
				TField constant = (TField)((object)this.fieldInfo.GetValue(null));
				this.getter = () => constant;
				return;
			}
			if (OptimizedReflection.useJit)
			{
				MemberExpression memberExpression = Expression.Field(null, this.fieldInfo);
				this.getter = Expression.Lambda<Func<TField>>(memberExpression, Array.Empty<ParameterExpression>()).Compile();
				if (this.fieldInfo.CanWrite())
				{
					ParameterExpression parameterExpression = Expression.Parameter(typeof(TField));
					BinaryExpression binaryExpression = Expression.Assign(memberExpression, parameterExpression);
					this.setter = Expression.Lambda<Action<TField>>(binaryExpression, new ParameterExpression[] { parameterExpression }).Compile();
					return;
				}
			}
			else
			{
				this.getter = () => (TField)((object)this.fieldInfo.GetValue(null));
				if (this.fieldInfo.CanWrite())
				{
					this.setter = delegate(TField value)
					{
						this.fieldInfo.SetValue(null, value);
					};
				}
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00021A58 File Offset: 0x0001FC58
		public object GetValue(object target)
		{
			if (OptimizedReflection.safeMode)
			{
				OptimizedReflection.VerifyStaticTarget(this.targetType, target);
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

		// Token: 0x06000762 RID: 1890 RVA: 0x00021AB0 File Offset: 0x0001FCB0
		private object GetValueUnsafe(object target)
		{
			return this.getter();
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00021AC4 File Offset: 0x0001FCC4
		public void SetValue(object target, object value)
		{
			if (OptimizedReflection.safeMode)
			{
				OptimizedReflection.VerifyStaticTarget(this.targetType, target);
				if (this.setter == null)
				{
					throw new TargetException(string.Format("The field '{0}.{1}' cannot be assigned.", this.targetType, this.fieldInfo.Name));
				}
				if (!typeof(TField).IsAssignableFrom(value))
				{
					string text = "The provided value for '{0}.{1}' does not match the field type.\nProvided: {2}\nExpected: {3}";
					object[] array = new object[4];
					array[0] = this.targetType;
					array[1] = this.fieldInfo.Name;
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
					array[3] = typeof(TField);
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

		// Token: 0x06000764 RID: 1892 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		private void SetValueUnsafe(object target, object value)
		{
			this.setter((TField)((object)value));
		}

		// Token: 0x040001B9 RID: 441
		private readonly FieldInfo fieldInfo;

		// Token: 0x040001BA RID: 442
		private Func<TField> getter;

		// Token: 0x040001BB RID: 443
		private Action<TField> setter;

		// Token: 0x040001BC RID: 444
		private Type targetType;
	}
}
