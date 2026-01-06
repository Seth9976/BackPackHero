using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000170 RID: 368
	public sealed class VariableDeclarationsCloner : Cloner<VariableDeclarations>
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x00029719 File Offset: 0x00027919
		public override bool Handles(Type type)
		{
			return type == typeof(VariableDeclarations);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002972B File Offset: 0x0002792B
		public override VariableDeclarations ConstructClone(Type type, VariableDeclarations original)
		{
			return new VariableDeclarations();
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00029734 File Offset: 0x00027934
		public override void FillClone(Type type, ref VariableDeclarations clone, VariableDeclarations original, CloningContext context)
		{
			foreach (VariableDeclaration variableDeclaration in original)
			{
				clone[variableDeclaration.name] = variableDeclaration.value.CloneViaFakeSerialization();
			}
		}

		// Token: 0x0400024F RID: 591
		public static readonly VariableDeclarationsCloner instance = new VariableDeclarationsCloner();
	}
}
