using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.VisualScripting
{
	// Token: 0x02000173 RID: 371
	[AddComponentMenu("Visual Scripting/Variables")]
	[DisableAnnotation]
	[IncludeInSettings(false)]
	public class Variables : LudiqBehaviour, IAotStubbable
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x000297BB File Offset: 0x000279BB
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x000297C3 File Offset: 0x000279C3
		[Serialize]
		[Inspectable]
		public VariableDeclarations declarations { get; internal set; } = new VariableDeclarations
		{
			Kind = VariableKind.Object
		};

		// Token: 0x060009E1 RID: 2529 RVA: 0x000297CC File Offset: 0x000279CC
		public static VariableDeclarations Graph(GraphPointer pointer)
		{
			Ensure.That("pointer").IsNotNull<GraphPointer>(pointer);
			if (pointer.hasData)
			{
				return Variables.GraphInstance(pointer);
			}
			return Variables.GraphDefinition(pointer);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000297F3 File Offset: 0x000279F3
		public static VariableDeclarations GraphInstance(GraphPointer pointer)
		{
			return pointer.GetGraphData<IGraphDataWithVariables>().variables;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00029800 File Offset: 0x00027A00
		public static VariableDeclarations GraphDefinition(GraphPointer pointer)
		{
			return Variables.GraphDefinition((IGraphWithVariables)pointer.graph);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00029812 File Offset: 0x00027A12
		public static VariableDeclarations GraphDefinition(IGraphWithVariables graph)
		{
			return graph.variables;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002981A File Offset: 0x00027A1A
		public static VariableDeclarations Object(GameObject go)
		{
			return go.GetOrAddComponent<Variables>().declarations;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00029827 File Offset: 0x00027A27
		public static VariableDeclarations Object(Component component)
		{
			return Variables.Object(component.gameObject);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00029834 File Offset: 0x00027A34
		public static VariableDeclarations Scene(Scene? scene)
		{
			return SceneVariables.For(scene);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002983C File Offset: 0x00027A3C
		public static VariableDeclarations Scene(GameObject go)
		{
			return Variables.Scene(new Scene?(go.scene));
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002984E File Offset: 0x00027A4E
		public static VariableDeclarations Scene(Component component)
		{
			return Variables.Scene(component.gameObject);
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002985B File Offset: 0x00027A5B
		public static VariableDeclarations ActiveScene
		{
			get
			{
				return Variables.Scene(new Scene?(SceneManager.GetActiveScene()));
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0002986C File Offset: 0x00027A6C
		public static VariableDeclarations Application
		{
			get
			{
				return ApplicationVariables.current;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00029873 File Offset: 0x00027A73
		public static VariableDeclarations Saved
		{
			get
			{
				return SavedVariables.current;
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002987A File Offset: 0x00027A7A
		public static bool ExistOnObject(GameObject go)
		{
			return go.GetComponent<Variables>() != null;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00029888 File Offset: 0x00027A88
		public static bool ExistOnObject(Component component)
		{
			return Variables.ExistOnObject(component.gameObject);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00029895 File Offset: 0x00027A95
		public static bool ExistInScene(Scene? scene)
		{
			return scene != null && SceneVariables.InstantiatedIn(scene.Value);
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000298AE File Offset: 0x00027AAE
		public static bool ExistInActiveScene
		{
			get
			{
				return Variables.ExistInScene(new Scene?(SceneManager.GetActiveScene()));
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000298BF File Offset: 0x00027ABF
		[ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x000298C7 File Offset: 0x00027AC7
		public IEnumerable<object> GetAotStubs(HashSet<object> visited)
		{
			foreach (VariableDeclaration variableDeclaration in this.declarations)
			{
				object value = variableDeclaration.value;
				ConstructorInfo constructorInfo = ((value != null) ? value.GetType().GetPublicDefaultConstructor() : null);
				if (constructorInfo != null)
				{
					yield return constructorInfo;
				}
			}
			IEnumerator<VariableDeclaration> enumerator = null;
			yield break;
			yield break;
		}
	}
}
