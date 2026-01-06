using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.VisualScripting
{
	// Token: 0x0200016C RID: 364
	[Singleton(Name = "VisualScripting SceneVariables", Automatic = true, Persistent = false)]
	[RequireComponent(typeof(Variables))]
	[DisableAnnotation]
	[AddComponentMenu("")]
	[IncludeInSettings(false)]
	public sealed class SceneVariables : MonoBehaviour, ISingleton
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x0002946C File Offset: 0x0002766C
		public static SceneVariables Instance(Scene scene)
		{
			return SceneSingleton<SceneVariables>.InstanceIn(scene);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00029474 File Offset: 0x00027674
		public static bool InstantiatedIn(Scene scene)
		{
			return SceneSingleton<SceneVariables>.InstantiatedIn(scene);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002947C File Offset: 0x0002767C
		public static VariableDeclarations For(Scene? scene)
		{
			Ensure.That("scene").IsNotNull<Scene>(scene);
			return SceneVariables.Instance(scene.Value).variables.declarations;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000294A4 File Offset: 0x000276A4
		private void Awake()
		{
			SceneSingleton<SceneVariables>.Awake(this);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000294AC File Offset: 0x000276AC
		private void OnDestroy()
		{
			SceneSingleton<SceneVariables>.OnDestroy(this);
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x000294B4 File Offset: 0x000276B4
		public Variables variables
		{
			get
			{
				if (this._variables == null)
				{
					this._variables = base.gameObject.GetOrAddComponent<Variables>();
				}
				return this._variables;
			}
		}

		// Token: 0x04000248 RID: 584
		private Variables _variables;
	}
}
