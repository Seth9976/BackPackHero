using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000009 RID: 9
	public sealed class ListCloner : Cloner<IList>
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000023DA File Offset: 0x000005DA
		public override bool Handles(Type type)
		{
			return typeof(IList).IsAssignableFrom(type);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023EC File Offset: 0x000005EC
		public override void FillClone(Type type, ref IList clone, IList original, CloningContext context)
		{
			if (context.tryPreserveInstances)
			{
				for (int i = 0; i < original.Count; i++)
				{
					object obj = original[i];
					if (i < clone.Count)
					{
						object obj2 = clone[i];
						Cloning.CloneInto(context, ref obj2, obj);
						clone[i] = obj2;
					}
					else
					{
						clone.Add(Cloning.Clone(context, obj));
					}
				}
				return;
			}
			for (int j = 0; j < original.Count; j++)
			{
				object obj3 = original[j];
				clone.Add(Cloning.Clone(context, obj3));
			}
		}
	}
}
