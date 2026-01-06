using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000149 RID: 329
	public static class Singleton<T> where T : MonoBehaviour, ISingleton
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x00026A4C File Offset: 0x00024C4C
		static Singleton()
		{
			if (Singleton<T>.attribute == null)
			{
				throw new InvalidImplementationException(string.Format("Missing singleton attribute for '{0}'.", typeof(T)));
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00026AA3 File Offset: 0x00024CA3
		private static bool persistent
		{
			get
			{
				return Singleton<T>.attribute.Persistent;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00026AAF File Offset: 0x00024CAF
		private static bool automatic
		{
			get
			{
				return Singleton<T>.attribute.Automatic;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00026ABB File Offset: 0x00024CBB
		private static string name
		{
			get
			{
				return Singleton<T>.attribute.Name;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00026AC7 File Offset: 0x00024CC7
		private static HideFlags hideFlags
		{
			get
			{
				return Singleton<T>.attribute.HideFlags;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00026AD4 File Offset: 0x00024CD4
		public static bool instantiated
		{
			get
			{
				object @lock = Singleton<T>._lock;
				bool flag2;
				lock (@lock)
				{
					if (Application.isPlaying)
					{
						flag2 = Singleton<T>._instance != null;
					}
					else
					{
						flag2 = Singleton<T>.FindInstances().Length == 1;
					}
				}
				return flag2;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00026B34 File Offset: 0x00024D34
		public static T instance
		{
			get
			{
				object @lock = Singleton<T>._lock;
				T t;
				lock (@lock)
				{
					if (Application.isPlaying)
					{
						if (Singleton<T>._instance == null)
						{
							Singleton<T>.Instantiate();
						}
						t = Singleton<T>._instance;
					}
					else
					{
						t = Singleton<T>.Instantiate();
					}
				}
				return t;
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00026B9C File Offset: 0x00024D9C
		private static T[] FindInstances()
		{
			return Object.FindObjectsOfType<T>();
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00026BA4 File Offset: 0x00024DA4
		public static T Instantiate()
		{
			object @lock = Singleton<T>._lock;
			T instance;
			lock (@lock)
			{
				T[] array = Singleton<T>.FindInstances();
				if (array.Length == 1)
				{
					Singleton<T>._instance = array[0];
				}
				else if (array.Length == 0)
				{
					if (!Singleton<T>.automatic)
					{
						throw new UnityException(string.Format("Missing '{0}' singleton in the scene.", typeof(T)));
					}
					GameObject gameObject = new GameObject(Singleton<T>.name ?? typeof(T).Name);
					gameObject.hideFlags = Singleton<T>.hideFlags;
					T t = gameObject.AddComponent<T>();
					t.hideFlags = Singleton<T>.hideFlags;
					Singleton<T>.Awake(t);
					if (Singleton<T>.persistent && Application.isPlaying)
					{
						Object.DontDestroyOnLoad(gameObject);
					}
				}
				else if (array.Length > 1)
				{
					throw new UnityException(string.Format("More than one '{0}' singleton in the scene.", typeof(T)));
				}
				instance = Singleton<T>._instance;
			}
			return instance;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00026CA4 File Offset: 0x00024EA4
		public static void Awake(T instance)
		{
			Ensure.That("instance").IsNotNull<T>(instance);
			if (Singleton<T>.awoken.Contains(instance))
			{
				return;
			}
			if (Singleton<T>._instance != null)
			{
				throw new UnityException(string.Format("More than one '{0}' singleton in the scene.", typeof(T)));
			}
			Singleton<T>._instance = instance;
			Singleton<T>.awoken.Add(instance);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00026D10 File Offset: 0x00024F10
		public static void OnDestroy(T instance)
		{
			Ensure.That("instance").IsNotNull<T>(instance);
			if (Singleton<T>._instance == instance)
			{
				Singleton<T>._instance = default(T);
				return;
			}
			throw new UnityException(string.Format("Trying to destroy invalid instance of '{0}' singleton.", typeof(T)));
		}

		// Token: 0x0400021B RID: 539
		private static readonly SingletonAttribute attribute = typeof(T).GetAttribute(true);

		// Token: 0x0400021C RID: 540
		private static readonly object _lock = new object();

		// Token: 0x0400021D RID: 541
		private static readonly HashSet<T> awoken = new HashSet<T>();

		// Token: 0x0400021E RID: 542
		private static T _instance;
	}
}
