using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002E2 RID: 738
	public class SceneManagerAPI
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x00030E82 File Offset: 0x0002F082
		internal static SceneManagerAPI ActiveAPI
		{
			get
			{
				return SceneManagerAPI.overrideAPI ?? SceneManagerAPI.s_DefaultAPI;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x00030E92 File Offset: 0x0002F092
		// (set) Token: 0x06001E26 RID: 7718 RVA: 0x00030E99 File Offset: 0x0002F099
		public static SceneManagerAPI overrideAPI { get; set; }

		// Token: 0x06001E27 RID: 7719 RVA: 0x00008C2F File Offset: 0x00006E2F
		protected internal SceneManagerAPI()
		{
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00030EA1 File Offset: 0x0002F0A1
		protected internal virtual int GetNumScenesInBuildSettings()
		{
			return SceneManagerAPIInternal.GetNumScenesInBuildSettings();
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00030EA8 File Offset: 0x0002F0A8
		protected internal virtual Scene GetSceneByBuildIndex(int buildIndex)
		{
			return SceneManagerAPIInternal.GetSceneByBuildIndex(buildIndex);
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00030EB0 File Offset: 0x0002F0B0
		protected internal virtual AsyncOperation LoadSceneAsyncByNameOrIndex(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			return SceneManagerAPIInternal.LoadSceneAsyncNameIndexInternal(sceneName, sceneBuildIndex, parameters, mustCompleteNextFrame);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00030EBC File Offset: 0x0002F0BC
		protected internal virtual AsyncOperation UnloadSceneAsyncByNameOrIndex(string sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess)
		{
			return SceneManagerAPIInternal.UnloadSceneNameIndexInternal(sceneName, sceneBuildIndex, immediately, options, out outSuccess);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00030ECA File Offset: 0x0002F0CA
		protected internal virtual AsyncOperation LoadFirstScene(bool mustLoadAsync)
		{
			return null;
		}

		// Token: 0x040009DE RID: 2526
		private static SceneManagerAPI s_DefaultAPI = new SceneManagerAPI();
	}
}
