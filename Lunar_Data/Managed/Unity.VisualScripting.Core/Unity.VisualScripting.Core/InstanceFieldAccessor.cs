using System;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000100 RID: 256
	public class InstanceFieldAccessor<TTarget, TField> : IOptimizedAccessor
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x0001F388 File Offset: 0x0001D588
		public InstanceFieldAccessor(FieldInfo fieldInfo)
		{
			if (OptimizedReflection.safeMode)
			{
				Ensure.That("fieldInfo").IsNotNull<FieldInfo>(fieldInfo);
				if (fieldInfo.DeclaringType != typeof(TTarget))
				{
					throw new ArgumentException("Declaring type of field info doesn't match generic type.", "fieldInfo");
				}
				if (fieldInfo.FieldType != typeof(TField))
				{
					throw new ArgumentException("Field type of field info doesn't match generic type.", "fieldInfo");
				}
				if (fieldInfo.IsStatic)
				{
					throw new ArgumentException("The field is static.", "fieldInfo");
				}
			}
			this.fieldInfo = fieldInfo;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001F420 File Offset: 0x0001D620
		public void Compile()
		{
			if (OptimizedReflection.useJit)
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(TTarget), "target");
				MemberExpression memberExpression = Expression.Field(parameterExpression, this.fieldInfo);
				this.getter = Expression.Lambda<Func<TTarget, TField>>(memberExpression, new ParameterExpression[] { parameterExpression }).Compile();
				if (!this.fieldInfo.CanWrite())
				{
					return;
				}
				try
				{
					ParameterExpression parameterExpression2 = Expression.Parameter(typeof(TField));
					BinaryExpression binaryExpression = Expression.Assign(memberExpression, parameterExpression2);
					this.setter = Expression.Lambda<Action<TTarget, TField>>(binaryExpression, new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile();
					return;
				}
				catch
				{
					string text = "Failed instance field: ";
					FieldInfo fieldInfo = this.fieldInfo;
					Debug.Log(text + ((fieldInfo != null) ? fieldInfo.ToString() : null));
					throw;
				}
			}
			this.getter = (TTarget instance) => (TField)((object)this.fieldInfo.GetValue(instance));
			if (this.fieldInfo.CanWrite())
			{
				this.setter = delegate(TTarget instance, TField value)
				{
					this.fieldInfo.SetValue(instance, value);
				};
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001F524 File Offset: 0x0001D724
		public object GetValue(object target)
		{
			if (OptimizedReflection.safeMode)
			{
				OptimizedReflection.VerifyInstanceTarget<TTarget>(target);
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

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001F578 File Offset: 0x0001D778
		private object GetValueUnsafe(object target)
		{
			return this.getter((TTarget)((object)target));
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001F590 File Offset: 0x0001D790
		public void SetValue(object target, object value)
		{
			if (OptimizedReflection.safeMode)
			{
				OptimizedReflection.VerifyInstanceTarget<TTarget>(target);
				if (this.setter == null)
				{
					throw new TargetException(string.Format("The field '{0}.{1}' cannot be assigned.", typeof(TTarget), this.fieldInfo.Name));
				}
				if (!typeof(TField).IsAssignableFrom(value))
				{
					string text = "The provided value for '{0}.{1}' does not match the field type.\nProvided: {2}\nExpected: {3}";
					object[] array = new object[4];
					array[0] = typeof(TTarget);
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

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001F688 File Offset: 0x0001D888
		private void SetValueUnsafe(object target, object value)
		{
			this.setter((TTarget)((object)target), (TField)((object)value));
		}

		// Token: 0x0400019B RID: 411
		private readonly FieldInfo fieldInfo;

		// Token: 0x0400019C RID: 412
		private Func<TTarget, TField> getter;

		// Token: 0x0400019D RID: 413
		private Action<TTarget, TField> setter;
	}
}
