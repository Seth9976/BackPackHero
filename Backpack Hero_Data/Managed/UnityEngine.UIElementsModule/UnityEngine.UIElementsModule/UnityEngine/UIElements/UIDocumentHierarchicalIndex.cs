using System;
using System.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024F RID: 591
	internal struct UIDocumentHierarchicalIndex : IComparable<UIDocumentHierarchicalIndex>
	{
		// Token: 0x060011B3 RID: 4531 RVA: 0x000445D8 File Offset: 0x000427D8
		public int CompareTo(UIDocumentHierarchicalIndex other)
		{
			bool flag = this.pathToParent == null;
			int num;
			if (flag)
			{
				bool flag2 = other.pathToParent == null;
				if (flag2)
				{
					num = 0;
				}
				else
				{
					num = 1;
				}
			}
			else
			{
				bool flag3 = other.pathToParent == null;
				if (flag3)
				{
					num = -1;
				}
				else
				{
					int num2 = this.pathToParent.Length;
					int num3 = other.pathToParent.Length;
					int num4 = 0;
					while (num4 < num2 && num4 < num3)
					{
						bool flag4 = this.pathToParent[num4] < other.pathToParent[num4];
						if (flag4)
						{
							return -1;
						}
						bool flag5 = this.pathToParent[num4] > other.pathToParent[num4];
						if (flag5)
						{
							return 1;
						}
						num4++;
					}
					bool flag6 = num2 > num3;
					if (flag6)
					{
						num = 1;
					}
					else
					{
						bool flag7 = num2 < num3;
						if (flag7)
						{
							num = -1;
						}
						else
						{
							num = 0;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000446C4 File Offset: 0x000428C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("pathToParent = [");
			bool flag = this.pathToParent != null;
			if (flag)
			{
				int num = this.pathToParent.Length;
				for (int i = 0; i < num; i++)
				{
					stringBuilder.Append(this.pathToParent[i]);
					bool flag2 = i < num - 1;
					if (flag2)
					{
						stringBuilder.Append(", ");
					}
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040007D7 RID: 2007
		internal int[] pathToParent;
	}
}
