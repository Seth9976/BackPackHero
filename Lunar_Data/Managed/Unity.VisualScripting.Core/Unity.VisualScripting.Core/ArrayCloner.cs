using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000004 RID: 4
	public sealed class ArrayCloner : Cloner<Array>
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000213D File Offset: 0x0000033D
		public override bool Handles(Type type)
		{
			return type.IsArray;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002145 File Offset: 0x00000345
		public override Array ConstructClone(Type type, Array original)
		{
			return Array.CreateInstance(type.GetElementType(), 0);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002154 File Offset: 0x00000354
		public override void FillClone(Type type, ref Array clone, Array original, CloningContext context)
		{
			int length = original.GetLength(0);
			clone = Array.CreateInstance(type.GetElementType(), length);
			for (int i = 0; i < length; i++)
			{
				clone.SetValue(Cloning.Clone(context, original.GetValue(i)), i);
			}
		}
	}
}
