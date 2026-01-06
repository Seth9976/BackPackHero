using System;
using UnityEngine;

namespace CleverCrow.Fluid.Utilities
{
	// Token: 0x02000002 RID: 2
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool IsInstance
		{
			get
			{
				return Singleton<T>._instance != null;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002062 File Offset: 0x00000262
		private static string SingletonName
		{
			get
			{
				return typeof(T).Name;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		public static T Instance
		{
			get
			{
				if (Singleton<T>._instance != null)
				{
					return Singleton<T>._instance;
				}
				Object[] array = Object.FindObjectsOfType(typeof(T));
				if (array.Length >= 1)
				{
					bool isPlaying = Application.isPlaying;
					Singleton<T>._instance = (T)((object)array[0]);
					return Singleton<T>._instance;
				}
				GameObject gameObject = new GameObject(Singleton<T>.SingletonName);
				Singleton<T>._instance = gameObject.AddComponent<T>();
				if (Application.isPlaying)
				{
					Object.DontDestroyOnLoad(gameObject);
				}
				return Singleton<T>._instance;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F0 File Offset: 0x000002F0
		protected virtual void OnDestroy()
		{
			if (Singleton<T>._instance == this)
			{
				Singleton<T>._instance = default(T);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210F File Offset: 0x0000030F
		public static void ClearSingleton()
		{
			if (Application.isPlaying || Singleton<T>._instance == null)
			{
				return;
			}
			Object.DestroyImmediate(Singleton<T>._instance.gameObject);
			Singleton<T>._instance = default(T);
		}

		// Token: 0x04000001 RID: 1
		private static T _instance;
	}
}
