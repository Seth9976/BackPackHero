using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.VisualScripting
{
	// Token: 0x02000137 RID: 311
	public sealed class GetVariable : UnifiedVariableUnit
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0000F656 File Offset: 0x0000D856
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x0000F65E File Offset: 0x0000D85E
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput value { get; private set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0000F667 File Offset: 0x0000D867
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x0000F66F File Offset: 0x0000D86F
		[DoNotSerialize]
		public ValueInput fallback { get; private set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0000F678 File Offset: 0x0000D878
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x0000F680 File Offset: 0x0000D880
		[Serialize]
		[Inspectable]
		[InspectorLabel("Fallback")]
		public bool specifyFallback { get; set; }

		// Token: 0x06000850 RID: 2128 RVA: 0x0000F68C File Offset: 0x0000D88C
		protected override void Definition()
		{
			base.Definition();
			this.value = base.ValueOutput<object>("value", new Func<Flow, object>(this.Get)).PredictableIf(new Func<Flow, bool>(this.IsDefined));
			base.Requirement(base.name, this.value);
			if (base.kind == VariableKind.Object)
			{
				base.Requirement(base.@object, this.value);
			}
			if (this.specifyFallback)
			{
				this.fallback = base.ValueInput<object>("fallback");
				base.Requirement(this.fallback, this.value);
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0000F728 File Offset: 0x0000D928
		private bool IsDefined(Flow flow)
		{
			string value = flow.GetValue<string>(base.name);
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			GameObject gameObject = null;
			if (base.kind == VariableKind.Object)
			{
				gameObject = flow.GetValue<GameObject>(base.@object);
				if (gameObject == null)
				{
					return false;
				}
			}
			Scene? scene = flow.stack.scene;
			if (base.kind == VariableKind.Scene && (scene == null || !scene.Value.IsValid() || !scene.Value.isLoaded || !Variables.ExistInScene(scene)))
			{
				return false;
			}
			switch (base.kind)
			{
			case VariableKind.Flow:
				return flow.variables.IsDefined(value);
			case VariableKind.Graph:
				return Variables.Graph(flow.stack).IsDefined(value);
			case VariableKind.Object:
				return Variables.Object(gameObject).IsDefined(value);
			case VariableKind.Scene:
				return Variables.Scene(new Scene?(scene.Value)).IsDefined(value);
			case VariableKind.Application:
				return Variables.Application.IsDefined(value);
			case VariableKind.Saved:
				return Variables.Saved.IsDefined(value);
			default:
				throw new UnexpectedEnumValueException<VariableKind>(base.kind);
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0000F84C File Offset: 0x0000DA4C
		private object Get(Flow flow)
		{
			string value = flow.GetValue<string>(base.name);
			VariableDeclarations variableDeclarations;
			switch (base.kind)
			{
			case VariableKind.Flow:
				variableDeclarations = flow.variables;
				break;
			case VariableKind.Graph:
				variableDeclarations = Variables.Graph(flow.stack);
				break;
			case VariableKind.Object:
				variableDeclarations = Variables.Object(flow.GetValue<GameObject>(base.@object));
				break;
			case VariableKind.Scene:
				variableDeclarations = Variables.Scene(flow.stack.scene);
				break;
			case VariableKind.Application:
				variableDeclarations = Variables.Application;
				break;
			case VariableKind.Saved:
				variableDeclarations = Variables.Saved;
				break;
			default:
				throw new UnexpectedEnumValueException<VariableKind>(base.kind);
			}
			if (this.specifyFallback && !variableDeclarations.IsDefined(value))
			{
				return flow.GetValue(this.fallback);
			}
			return variableDeclarations.Get(value);
		}
	}
}
