using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200014D RID: 333
	public static class ComponentHolderProtocol
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x00026E80 File Offset: 0x00025080
		public static bool IsComponentHolderType(Type type)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			return typeof(GameObject).IsAssignableFrom(type) || typeof(Component).IsAssignableFrom(type);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00026EB6 File Offset: 0x000250B6
		public static bool IsComponentHolder(this Object uo)
		{
			return uo is GameObject || uo is Component;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00026ECB File Offset: 0x000250CB
		public static GameObject GameObject(this Object uo)
		{
			if (uo is GameObject)
			{
				return (GameObject)uo;
			}
			if (uo is Component)
			{
				return ((Component)uo).gameObject;
			}
			return null;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00026EF1 File Offset: 0x000250F1
		public static T AddComponent<T>(this Object uo) where T : Component
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).AddComponent<T>();
			}
			if (uo is Component)
			{
				return ((Component)uo).gameObject.AddComponent<T>();
			}
			throw new NotSupportedException();
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00026F25 File Offset: 0x00025125
		public static T GetOrAddComponent<T>(this Object uo) where T : Component
		{
			T t;
			if ((t = uo.GetComponent<T>()) == null)
			{
				t = uo.AddComponent<T>();
			}
			return t;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00026F3C File Offset: 0x0002513C
		public static T GetComponent<T>(this Object uo)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponent<T>();
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponent<T>();
			}
			throw new NotSupportedException();
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00026F6B File Offset: 0x0002516B
		public static T GetComponentInChildren<T>(this Object uo)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponentInChildren<T>();
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponentInChildren<T>();
			}
			throw new NotSupportedException();
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00026F9A File Offset: 0x0002519A
		public static T GetComponentInParent<T>(this Object uo)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponentInParent<T>();
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponentInParent<T>();
			}
			throw new NotSupportedException();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00026FC9 File Offset: 0x000251C9
		public static T[] GetComponents<T>(this Object uo)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponents<T>();
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponents<T>();
			}
			throw new NotSupportedException();
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00026FF8 File Offset: 0x000251F8
		public static T[] GetComponentsInChildren<T>(this Object uo)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponentsInChildren<T>();
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponentsInChildren<T>();
			}
			throw new NotSupportedException();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00027027 File Offset: 0x00025227
		public static T[] GetComponentsInParent<T>(this Object uo)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponentsInParent<T>();
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponentsInParent<T>();
			}
			throw new NotSupportedException();
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00027056 File Offset: 0x00025256
		public static Component GetComponent(this Object uo, Type type)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponent(type);
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponent(type);
			}
			throw new NotSupportedException();
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00027087 File Offset: 0x00025287
		public static Component GetComponentInChildren(this Object uo, Type type)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponentInChildren(type);
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponentInChildren(type);
			}
			throw new NotSupportedException();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x000270B8 File Offset: 0x000252B8
		public static Component GetComponentInParent(this Object uo, Type type)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponentInParent(type);
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponentInParent(type);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x000270E9 File Offset: 0x000252E9
		public static Component[] GetComponents(this Object uo, Type type)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponents(type);
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponents(type);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002711A File Offset: 0x0002531A
		public static Component[] GetComponentsInChildren(this Object uo, Type type)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponentsInChildren(type);
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponentsInChildren(type);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002714B File Offset: 0x0002534B
		public static Component[] GetComponentsInParent(this Object uo, Type type)
		{
			if (uo is GameObject)
			{
				return ((GameObject)uo).GetComponentsInParent(type);
			}
			if (uo is Component)
			{
				return ((Component)uo).GetComponentsInParent(type);
			}
			throw new NotSupportedException();
		}
	}
}
