using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000019 RID: 25
	internal static class InputInteraction
	{
		// Token: 0x0600012D RID: 301 RVA: 0x000032C4 File Offset: 0x000014C4
		public static Type GetValueType(Type interactionType)
		{
			if (interactionType == null)
			{
				throw new ArgumentNullException("interactionType");
			}
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(interactionType, typeof(IInputInteraction<>), 0);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000032EC File Offset: 0x000014EC
		public static string GetDisplayName(string interaction)
		{
			if (string.IsNullOrEmpty(interaction))
			{
				throw new ArgumentNullException("interaction");
			}
			Type type = InputInteraction.s_Interactions.LookupTypeRegistration(interaction);
			if (type == null)
			{
				return interaction;
			}
			return InputInteraction.GetDisplayName(type);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000332C File Offset: 0x0000152C
		public static string GetDisplayName(Type interactionType)
		{
			if (interactionType == null)
			{
				throw new ArgumentNullException("interactionType");
			}
			DisplayNameAttribute customAttribute = interactionType.GetCustomAttribute<DisplayNameAttribute>();
			if (customAttribute != null)
			{
				return customAttribute.DisplayName;
			}
			if (interactionType.Name.EndsWith("Interaction"))
			{
				return interactionType.Name.Substring(0, interactionType.Name.Length - "Interaction".Length);
			}
			return interactionType.Name;
		}

		// Token: 0x0400007F RID: 127
		public static TypeTable s_Interactions;
	}
}
