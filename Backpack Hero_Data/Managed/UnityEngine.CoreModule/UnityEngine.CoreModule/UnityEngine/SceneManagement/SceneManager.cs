using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002E3 RID: 739
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/SceneManager/SceneManager.bindings.h")]
	public class SceneManager
	{
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001E2E RID: 7726
		public static extern int sceneCount
		{
			[NativeHeader("Runtime/SceneManager/SceneManager.h")]
			[StaticAccessor("GetSceneManager()", StaticAccessorType.Dot)]
			[NativeMethod("GetSceneCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x00030EDC File Offset: 0x0002F0DC
		public static int sceneCountInBuildSettings
		{
			get
			{
				return SceneManagerAPI.ActiveAPI.GetNumScenesInBuildSettings();
			}
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00030EF8 File Offset: 0x0002F0F8
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetActiveScene()
		{
			Scene scene;
			SceneManager.GetActiveScene_Injected(out scene);
			return scene;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00030F0D File Offset: 0x0002F10D
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static bool SetActiveScene(Scene scene)
		{
			return SceneManager.SetActiveScene_Injected(ref scene);
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00030F18 File Offset: 0x0002F118
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneByPath(string scenePath)
		{
			Scene scene;
			SceneManager.GetSceneByPath_Injected(scenePath, out scene);
			return scene;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00030F30 File Offset: 0x0002F130
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneByName(string name)
		{
			Scene scene;
			SceneManager.GetSceneByName_Injected(name, out scene);
			return scene;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00030F48 File Offset: 0x0002F148
		public static Scene GetSceneByBuildIndex(int buildIndex)
		{
			return SceneManagerAPI.ActiveAPI.GetSceneByBuildIndex(buildIndex);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x00030F68 File Offset: 0x0002F168
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneAt(int index)
		{
			Scene scene;
			SceneManager.GetSceneAt_Injected(index, out scene);
			return scene;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00030F80 File Offset: 0x0002F180
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene CreateScene([NotNull("ArgumentNullException")] string sceneName, CreateSceneParameters parameters)
		{
			Scene scene;
			SceneManager.CreateScene_Injected(sceneName, ref parameters, out scene);
			return scene;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00030F98 File Offset: 0x0002F198
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		private static bool UnloadSceneInternal(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneInternal_Injected(ref scene, options);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00030FA2 File Offset: 0x0002F1A2
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		private static AsyncOperation UnloadSceneAsyncInternal(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneAsyncInternal_Injected(ref scene, options);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00030FAC File Offset: 0x0002F1AC
		private static AsyncOperation LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			bool flag = !SceneManager.s_AllowLoadScene;
			AsyncOperation asyncOperation;
			if (flag)
			{
				asyncOperation = null;
			}
			else
			{
				asyncOperation = SceneManagerAPI.ActiveAPI.LoadSceneAsyncByNameOrIndex(sceneName, sceneBuildIndex, parameters, mustCompleteNextFrame);
			}
			return asyncOperation;
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00030FDC File Offset: 0x0002F1DC
		private static AsyncOperation UnloadSceneNameIndexInternal(string sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess)
		{
			bool flag = !SceneManager.s_AllowLoadScene;
			AsyncOperation asyncOperation;
			if (flag)
			{
				outSuccess = false;
				asyncOperation = null;
			}
			else
			{
				asyncOperation = SceneManagerAPI.ActiveAPI.UnloadSceneAsyncByNameOrIndex(sceneName, sceneBuildIndex, immediately, options, out outSuccess);
			}
			return asyncOperation;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x00031013 File Offset: 0x0002F213
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static void MergeScenes(Scene sourceScene, Scene destinationScene)
		{
			SceneManager.MergeScenes_Injected(ref sourceScene, ref destinationScene);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0003101E File Offset: 0x0002F21E
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static void MoveGameObjectToScene([NotNull("ArgumentNullException")] GameObject go, Scene scene)
		{
			SceneManager.MoveGameObjectToScene_Injected(go, ref scene);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x00031028 File Offset: 0x0002F228
		[RequiredByNativeCode]
		internal static AsyncOperation LoadFirstScene_Internal(bool async)
		{
			return SceneManagerAPI.ActiveAPI.LoadFirstScene(async);
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001E3E RID: 7742 RVA: 0x00031048 File Offset: 0x0002F248
		// (remove) Token: 0x06001E3F RID: 7743 RVA: 0x0003107C File Offset: 0x0002F27C
		[field: DebuggerBrowsable(0)]
		public static event UnityAction<Scene, LoadSceneMode> sceneLoaded;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001E40 RID: 7744 RVA: 0x000310B0 File Offset: 0x0002F2B0
		// (remove) Token: 0x06001E41 RID: 7745 RVA: 0x000310E4 File Offset: 0x0002F2E4
		[field: DebuggerBrowsable(0)]
		public static event UnityAction<Scene> sceneUnloaded;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001E42 RID: 7746 RVA: 0x00031118 File Offset: 0x0002F318
		// (remove) Token: 0x06001E43 RID: 7747 RVA: 0x0003114C File Offset: 0x0002F34C
		[field: DebuggerBrowsable(0)]
		public static event UnityAction<Scene, Scene> activeSceneChanged;

		// Token: 0x06001E44 RID: 7748 RVA: 0x00031180 File Offset: 0x0002F380
		[Obsolete("Use SceneManager.sceneCount and SceneManager.GetSceneAt(int index) to loop the all scenes instead.")]
		public static Scene[] GetAllScenes()
		{
			Scene[] array = new Scene[SceneManager.sceneCount];
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				array[i] = SceneManager.GetSceneAt(i);
			}
			return array;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000311C4 File Offset: 0x0002F3C4
		public static Scene CreateScene(string sceneName)
		{
			CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.None);
			return SceneManager.CreateScene(sceneName, createSceneParameters);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000311E8 File Offset: 0x0002F3E8
		public static void LoadScene(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			SceneManager.LoadScene(sceneName, loadSceneParameters);
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x00031208 File Offset: 0x0002F408
		[ExcludeFromDocs]
		public static void LoadScene(string sceneName)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			SceneManager.LoadScene(sceneName, loadSceneParameters);
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00031228 File Offset: 0x0002F428
		public static Scene LoadScene(string sceneName, LoadSceneParameters parameters)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, true);
			return SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00031250 File Offset: 0x0002F450
		public static void LoadScene(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			SceneManager.LoadScene(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00031270 File Offset: 0x0002F470
		[ExcludeFromDocs]
		public static void LoadScene(int sceneBuildIndex)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			SceneManager.LoadScene(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x00031290 File Offset: 0x0002F490
		public static Scene LoadScene(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, true);
			return SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x000312B8 File Offset: 0x0002F4B8
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			return SceneManager.LoadSceneAsync(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x000312DC File Offset: 0x0002F4DC
		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			return SceneManager.LoadSceneAsync(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00031300 File Offset: 0x0002F500
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, false);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0003131C File Offset: 0x0002F51C
		public static AsyncOperation LoadSceneAsync(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			return SceneManager.LoadSceneAsync(sceneName, loadSceneParameters);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00031340 File Offset: 0x0002F540
		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(string sceneName)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			return SceneManager.LoadSceneAsync(sceneName, loadSceneParameters);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00031364 File Offset: 0x0002F564
		public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, false);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x00031380 File Offset: 0x0002F580
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(Scene scene)
		{
			return SceneManager.UnloadSceneInternal(scene, UnloadSceneOptions.None);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0003139C File Offset: 0x0002F59C
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(int sceneBuildIndex)
		{
			bool flag;
			SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, true, UnloadSceneOptions.None, out flag);
			return flag;
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000313C0 File Offset: 0x0002F5C0
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(string sceneName)
		{
			bool flag;
			SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, true, UnloadSceneOptions.None, out flag);
			return flag;
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000313E0 File Offset: 0x0002F5E0
		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, false, UnloadSceneOptions.None, out flag);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x00031404 File Offset: 0x0002F604
		public static AsyncOperation UnloadSceneAsync(string sceneName)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, false, UnloadSceneOptions.None, out flag);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00031424 File Offset: 0x0002F624
		public static AsyncOperation UnloadSceneAsync(Scene scene)
		{
			return SceneManager.UnloadSceneAsyncInternal(scene, UnloadSceneOptions.None);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x00031440 File Offset: 0x0002F640
		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex, UnloadSceneOptions options)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, false, options, out flag);
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x00031464 File Offset: 0x0002F664
		public static AsyncOperation UnloadSceneAsync(string sceneName, UnloadSceneOptions options)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, false, options, out flag);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00031484 File Offset: 0x0002F684
		public static AsyncOperation UnloadSceneAsync(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneAsyncInternal(scene, options);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x000314A0 File Offset: 0x0002F6A0
		[RequiredByNativeCode]
		private static void Internal_SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			bool flag = SceneManager.sceneLoaded != null;
			if (flag)
			{
				SceneManager.sceneLoaded(scene, mode);
			}
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x000314CC File Offset: 0x0002F6CC
		[RequiredByNativeCode]
		private static void Internal_SceneUnloaded(Scene scene)
		{
			bool flag = SceneManager.sceneUnloaded != null;
			if (flag)
			{
				SceneManager.sceneUnloaded(scene);
			}
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x000314F4 File Offset: 0x0002F6F4
		[RequiredByNativeCode]
		private static void Internal_ActiveSceneChanged(Scene previousActiveScene, Scene newActiveScene)
		{
			bool flag = SceneManager.activeSceneChanged != null;
			if (flag)
			{
				SceneManager.activeSceneChanged(previousActiveScene, newActiveScene);
			}
		}

		// Token: 0x06001E60 RID: 7776
		[MethodImpl(4096)]
		private static extern void GetActiveScene_Injected(out Scene ret);

		// Token: 0x06001E61 RID: 7777
		[MethodImpl(4096)]
		private static extern bool SetActiveScene_Injected(ref Scene scene);

		// Token: 0x06001E62 RID: 7778
		[MethodImpl(4096)]
		private static extern void GetSceneByPath_Injected(string scenePath, out Scene ret);

		// Token: 0x06001E63 RID: 7779
		[MethodImpl(4096)]
		private static extern void GetSceneByName_Injected(string name, out Scene ret);

		// Token: 0x06001E64 RID: 7780
		[MethodImpl(4096)]
		private static extern void GetSceneAt_Injected(int index, out Scene ret);

		// Token: 0x06001E65 RID: 7781
		[MethodImpl(4096)]
		private static extern void CreateScene_Injected(string sceneName, ref CreateSceneParameters parameters, out Scene ret);

		// Token: 0x06001E66 RID: 7782
		[MethodImpl(4096)]
		private static extern bool UnloadSceneInternal_Injected(ref Scene scene, UnloadSceneOptions options);

		// Token: 0x06001E67 RID: 7783
		[MethodImpl(4096)]
		private static extern AsyncOperation UnloadSceneAsyncInternal_Injected(ref Scene scene, UnloadSceneOptions options);

		// Token: 0x06001E68 RID: 7784
		[MethodImpl(4096)]
		private static extern void MergeScenes_Injected(ref Scene sourceScene, ref Scene destinationScene);

		// Token: 0x06001E69 RID: 7785
		[MethodImpl(4096)]
		private static extern void MoveGameObjectToScene_Injected(GameObject go, ref Scene scene);

		// Token: 0x040009E0 RID: 2528
		internal static bool s_AllowLoadScene = true;
	}
}
