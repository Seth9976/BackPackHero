using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.VisualScripting
{
	// Token: 0x02000148 RID: 328
	public static class SceneSingleton<T> where T : MonoBehaviour, ISingleton
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x000266B4 File Offset: 0x000248B4
		static SceneSingleton()
		{
			if (SceneSingleton<T>.attribute == null)
			{
				throw new InvalidImplementationException(string.Format("Missing singleton attribute for '{0}'.", typeof(T)));
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00026701 File Offset: 0x00024901
		private static bool persistent
		{
			get
			{
				return SceneSingleton<T>.attribute.Persistent;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0002670D File Offset: 0x0002490D
		private static bool automatic
		{
			get
			{
				return SceneSingleton<T>.attribute.Automatic;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00026719 File Offset: 0x00024919
		private static string name
		{
			get
			{
				return SceneSingleton<T>.attribute.Name;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00026725 File Offset: 0x00024925
		private static HideFlags hideFlags
		{
			get
			{
				return SceneSingleton<T>.attribute.HideFlags;
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00026731 File Offset: 0x00024931
		private static void EnsureSceneValid(Scene scene)
		{
			if (!scene.IsValid())
			{
				throw new InvalidOperationException("Scene '" + scene.name + "' is invalid and cannot be used in singleton operations.");
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00026758 File Offset: 0x00024958
		public static bool InstantiatedIn(Scene scene)
		{
			SceneSingleton<T>.EnsureSceneValid(scene);
			if (Application.isPlaying)
			{
				return SceneSingleton<T>.instances.ContainsKey(scene);
			}
			return SceneSingleton<T>.FindInstances(scene).Length == 1;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002677E File Offset: 0x0002497E
		public static T InstanceIn(Scene scene)
		{
			SceneSingleton<T>.EnsureSceneValid(scene);
			if (!Application.isPlaying)
			{
				return SceneSingleton<T>.FindOrCreateInstance(scene);
			}
			if (SceneSingleton<T>.instances.ContainsKey(scene))
			{
				return SceneSingleton<T>.instances[scene];
			}
			return SceneSingleton<T>.FindOrCreateInstance(scene);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000267B4 File Offset: 0x000249B4
		private static T[] FindInstances(Scene scene)
		{
			SceneSingleton<T>.EnsureSceneValid(scene);
			return (from o in Object.FindObjectsOfType<T>()
				where o.gameObject.scene == scene
				select o).ToArray<T>();
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000267F4 File Offset: 0x000249F4
		private static T FindOrCreateInstance(Scene scene)
		{
			SceneSingleton<T>.EnsureSceneValid(scene);
			T[] array = SceneSingleton<T>.FindInstances(scene);
			if (array.Length == 1)
			{
				return array[0];
			}
			if (array.Length != 0)
			{
				throw new UnityException(string.Format("More than one '{0}' singleton in scene '{1}'.", typeof(T), scene.name));
			}
			if (!SceneSingleton<T>.automatic)
			{
				throw new UnityException(string.Format("Missing '{0}' singleton in scene '{1}'.", typeof(T), scene.name));
			}
			if (SceneSingleton<T>.persistent)
			{
				throw new UnityException("Scene singletons cannot be persistent.");
			}
			GameObject gameObject = new GameObject(SceneSingleton<T>.name ?? typeof(T).Name);
			gameObject.hideFlags = SceneSingleton<T>.hideFlags;
			SceneManager.MoveGameObjectToScene(gameObject, scene);
			T t = gameObject.AddComponent<T>();
			t.hideFlags = SceneSingleton<T>.hideFlags;
			return t;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000268C0 File Offset: 0x00024AC0
		public static void Awake(T instance)
		{
			Ensure.That("instance").IsNotNull<T>(instance);
			Scene scene = instance.gameObject.scene;
			SceneSingleton<T>.EnsureSceneValid(scene);
			if (SceneSingleton<T>.instances.ContainsKey(scene))
			{
				throw new UnityException(string.Format("More than one '{0}' singleton in scene '{1}'.", typeof(T), scene.name));
			}
			SceneSingleton<T>.instances.Add(scene, instance);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00026930 File Offset: 0x00024B30
		public static void OnDestroy(T instance)
		{
			Ensure.That("instance").IsNotNull<T>(instance);
			Scene scene = instance.gameObject.scene;
			if (!scene.IsValid())
			{
				foreach (KeyValuePair<Scene, T> keyValuePair in SceneSingleton<T>.instances)
				{
					if (keyValuePair.Value == instance)
					{
						SceneSingleton<T>.instances.Remove(keyValuePair.Key);
						break;
					}
				}
				return;
			}
			if (!SceneSingleton<T>.instances.ContainsKey(scene))
			{
				throw new UnityException(string.Format("Trying to destroy invalid instance of '{0}' singleton in scene '{1}'.", typeof(T), scene.name));
			}
			if (SceneSingleton<T>.instances[scene] == instance)
			{
				SceneSingleton<T>.instances.Remove(scene);
				return;
			}
			throw new UnityException(string.Format("Trying to destroy invalid instance of '{0}' singleton in scene '{1}'.", typeof(T), scene.name));
		}

		// Token: 0x04000219 RID: 537
		private static Dictionary<Scene, T> instances = new Dictionary<Scene, T>();

		// Token: 0x0400021A RID: 538
		private static readonly SingletonAttribute attribute = typeof(T).GetAttribute(true);
	}
}
