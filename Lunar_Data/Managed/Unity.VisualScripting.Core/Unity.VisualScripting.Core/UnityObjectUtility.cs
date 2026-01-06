using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.VisualScripting
{
	// Token: 0x02000164 RID: 356
	public static class UnityObjectUtility
	{
		// Token: 0x06000988 RID: 2440 RVA: 0x00028D5B File Offset: 0x00026F5B
		public static bool IsDestroyed(this Object target)
		{
			return target != null && target == null;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00028D69 File Offset: 0x00026F69
		public static bool IsUnityNull(this object obj)
		{
			return obj == null || (obj is Object && (Object)obj == null);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00028D88 File Offset: 0x00026F88
		public static string ToSafeString(this Object uo)
		{
			if (uo == null)
			{
				return "(null)";
			}
			if (!UnityThread.allowsAPI)
			{
				return uo.GetType().Name;
			}
			if (uo == null)
			{
				return "(Destroyed)";
			}
			string text;
			try
			{
				text = uo.name;
			}
			catch (Exception ex)
			{
				text = string.Concat(new string[]
				{
					"(",
					ex.GetType().Name,
					" in ToString: ",
					ex.Message,
					")"
				});
			}
			return text;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00028E1C File Offset: 0x0002701C
		public static string ToSafeString(this object obj)
		{
			if (obj == null)
			{
				return "(null)";
			}
			Object @object = obj as Object;
			if (@object != null)
			{
				return @object.ToSafeString();
			}
			string text;
			try
			{
				text = obj.ToString();
			}
			catch (Exception ex)
			{
				text = string.Concat(new string[]
				{
					"(",
					ex.GetType().Name,
					" in ToString: ",
					ex.Message,
					")"
				});
			}
			return text;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00028E9C File Offset: 0x0002709C
		public static T AsUnityNull<T>(this T obj) where T : Object
		{
			if (obj == null)
			{
				return default(T);
			}
			return obj;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00028EC2 File Offset: 0x000270C2
		public static bool TrulyEqual(Object a, Object b)
		{
			return !(a != b) && a == null == (b == null);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00028EE2 File Offset: 0x000270E2
		public static IEnumerable<T> NotUnityNull<T>(this IEnumerable<T> enumerable) where T : Object
		{
			return enumerable.Where((T i) => i != null);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00028F09 File Offset: 0x00027109
		public static IEnumerable<T> FindObjectsOfTypeIncludingInactive<T>()
		{
			int num;
			for (int i = 0; i < SceneManager.sceneCount; i = num + 1)
			{
				Scene sceneAt = SceneManager.GetSceneAt(i);
				if (sceneAt.isLoaded)
				{
					foreach (GameObject gameObject in sceneAt.GetRootGameObjects())
					{
						foreach (T t in gameObject.GetComponentsInChildren<T>(true))
						{
							yield return t;
						}
						T[] array2 = null;
					}
					GameObject[] array = null;
				}
				num = i;
			}
			yield break;
		}
	}
}
