using System;
using UnityEngine.SceneManagement;

namespace Unity.VisualScripting
{
	// Token: 0x02000150 RID: 336
	[UnitSurtitle("Scene")]
	public sealed class SetSceneVariable : SetVariableUnit, ISceneVariableUnit, IVariableUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x060008AA RID: 2218 RVA: 0x0000FEAF File Offset: 0x0000E0AF
		public SetSceneVariable()
		{
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0000FEB7 File Offset: 0x0000E0B7
		public SetSceneVariable(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		protected override VariableDeclarations GetDeclarations(Flow flow)
		{
			Scene? scene = flow.stack.scene;
			if (scene == null)
			{
				return null;
			}
			return Variables.Scene(new Scene?(scene.Value));
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0000FEF5 File Offset: 0x0000E0F5
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
