using System;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000081 RID: 129
	[DisableAnnotation]
	[AddComponentMenu("")]
	[IncludeInSettings(false)]
	public abstract class MessageListener : MonoBehaviour
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000944C File Offset: 0x0000764C
		[Obsolete("listenerTypes is deprecated", false)]
		public static Type[] listenerTypes
		{
			get
			{
				if (MessageListener._listenerTypes == null)
				{
					MessageListener._listenerTypes = RuntimeCodebase.types.Where((Type t) => typeof(MessageListener).IsAssignableFrom(t) && t.IsConcrete() && !Attribute.IsDefined(t, typeof(ObsoleteAttribute))).ToArray<Type>();
				}
				return MessageListener._listenerTypes;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00009498 File Offset: 0x00007698
		[Obsolete("Use the overload with a messageListenerType parameter instead", false)]
		public static void AddTo(GameObject gameObject)
		{
			foreach (Type type in MessageListener.listenerTypes)
			{
				if (gameObject.GetComponent(type) == null)
				{
					gameObject.AddComponent(type);
				}
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000094D4 File Offset: 0x000076D4
		public static void AddTo(Type messageListenerType, GameObject gameObject)
		{
			Component component;
			if (!gameObject.TryGetComponent(messageListenerType, out component))
			{
				gameObject.AddComponent(messageListenerType);
			}
		}

		// Token: 0x040000F6 RID: 246
		private static Type[] _listenerTypes;
	}
}
