using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200013E RID: 318
	public abstract class SerializedPropertyProvider<T> : ScriptableObject, ISerializedPropertyProvider
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00026281 File Offset: 0x00024481
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0002628E File Offset: 0x0002448E
		object ISerializedPropertyProvider.item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = (T)((object)value);
			}
		}

		// Token: 0x0400020F RID: 527
		[SerializeField]
		protected T item;
	}
}
