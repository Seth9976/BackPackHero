using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000026 RID: 38
	public abstract class InputBindingComposite
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002C7 RID: 711
		public abstract Type valueType { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002C8 RID: 712
		public abstract int valueSizeInBytes { get; }

		// Token: 0x060002C9 RID: 713
		public unsafe abstract void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize);

		// Token: 0x060002CA RID: 714
		public abstract object ReadValueAsObject(ref InputBindingCompositeContext context);

		// Token: 0x060002CB RID: 715 RVA: 0x0000C464 File Offset: 0x0000A664
		public virtual float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return -1f;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000C46B File Offset: 0x0000A66B
		protected virtual void FinishSetup(ref InputBindingCompositeContext context)
		{
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000C46D File Offset: 0x0000A66D
		internal void CallFinishSetup(ref InputBindingCompositeContext context)
		{
			this.FinishSetup(ref context);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000C478 File Offset: 0x0000A678
		internal static Type GetValueType(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			Type type = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				return null;
			}
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(type, typeof(InputBindingComposite<>), 0);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
		public static string GetExpectedControlLayoutName(string composite, string part)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			if (string.IsNullOrEmpty(part))
			{
				throw new ArgumentNullException("part");
			}
			Type type = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				return null;
			}
			FieldInfo field = type.GetField(part, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			if (field == null)
			{
				return null;
			}
			InputControlAttribute customAttribute = field.GetCustomAttribute(false);
			if (customAttribute == null)
			{
				return null;
			}
			return customAttribute.layout;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000C531 File Offset: 0x0000A731
		internal static IEnumerable<string> GetPartNames(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			Type type = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				yield break;
			}
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				if (fieldInfo.GetCustomAttribute<InputControlAttribute>() != null)
				{
					yield return fieldInfo.Name;
				}
			}
			FieldInfo[] array = null;
			yield break;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000C544 File Offset: 0x0000A744
		internal static string GetDisplayFormatString(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			Type type = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				return null;
			}
			DisplayStringFormatAttribute customAttribute = type.GetCustomAttribute<DisplayStringFormatAttribute>();
			if (customAttribute == null)
			{
				return null;
			}
			return customAttribute.formatString;
		}

		// Token: 0x040000E4 RID: 228
		internal static TypeTable s_Composites;
	}
}
