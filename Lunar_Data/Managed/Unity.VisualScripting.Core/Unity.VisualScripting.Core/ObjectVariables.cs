using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200016A RID: 362
	public static class ObjectVariables
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x00029040 File Offset: 0x00027240
		public static VariableDeclarations Declarations(GameObject source, bool autoAddComponent, bool throwOnMissing)
		{
			Ensure.That("source").IsNotNull<GameObject>(source);
			Variables variables = source.GetComponent<Variables>();
			if (variables == null && autoAddComponent)
			{
				variables = source.AddComponent<Variables>();
			}
			if (variables != null)
			{
				return variables.declarations;
			}
			if (throwOnMissing)
			{
				throw new InvalidOperationException("Game object '" + source.name + "' does not have variables.");
			}
			return null;
		}
	}
}
