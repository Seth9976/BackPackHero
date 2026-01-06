using System;
using UnityEngine.SceneManagement;

namespace Unity.VisualScripting
{
	// Token: 0x02000149 RID: 329
	[UnitSurtitle("Scene")]
	public sealed class IsSceneVariableDefined : IsVariableDefinedUnit, ISceneVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x0600088C RID: 2188 RVA: 0x0000FD0B File Offset: 0x0000DF0B
		public IsSceneVariableDefined()
		{
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0000FD13 File Offset: 0x0000DF13
		public IsSceneVariableDefined(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			Scene? scene = flow.stack.scene;
			if (scene == null)
			{
				return null;
			}
			return Variables.Scene(new Scene?(scene.Value));
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0000FD51 File Offset: 0x0000DF51
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
