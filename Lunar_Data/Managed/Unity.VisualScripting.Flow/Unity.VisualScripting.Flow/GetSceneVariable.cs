using System;
using UnityEngine.SceneManagement;

namespace Unity.VisualScripting
{
	// Token: 0x0200013E RID: 318
	[UnitSurtitle("Scene")]
	public sealed class GetSceneVariable : GetVariableUnit, ISceneVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x0000FB16 File Offset: 0x0000DD16
		public GetSceneVariable()
		{
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0000FB1E File Offset: 0x0000DD1E
		public GetSceneVariable(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0000FB28 File Offset: 0x0000DD28
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			Scene? scene = flow.stack.scene;
			if (scene == null)
			{
				return null;
			}
			return Variables.Scene(new Scene?(scene.Value));
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0000FB5D File Offset: 0x0000DD5D
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
