using System;
using System.Collections.Generic;
using Pathfinding.Util;

namespace Pathfinding.Voxels
{
	// Token: 0x020000AE RID: 174
	public class Utility
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x0003327B File Offset: 0x0003147B
		public static float Min(float a, float b, float c)
		{
			a = ((a < b) ? a : b);
			if (a >= c)
			{
				return c;
			}
			return a;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0003328E File Offset: 0x0003148E
		public static float Max(float a, float b, float c)
		{
			a = ((a > b) ? a : b);
			if (a <= c)
			{
				return c;
			}
			return a;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000332A4 File Offset: 0x000314A4
		public static Int3[] RemoveDuplicateVertices(Int3[] vertices, int[] triangles)
		{
			Dictionary<Int3, int> dictionary = ObjectPoolSimple<Dictionary<Int3, int>>.Claim();
			dictionary.Clear();
			int[] array = new int[vertices.Length];
			int num = 0;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (!dictionary.ContainsKey(vertices[i]))
				{
					dictionary.Add(vertices[i], num);
					array[i] = num;
					vertices[num] = vertices[i];
					num++;
				}
				else
				{
					array[i] = dictionary[vertices[i]];
				}
			}
			dictionary.Clear();
			ObjectPoolSimple<Dictionary<Int3, int>>.Release(ref dictionary);
			for (int j = 0; j < triangles.Length; j++)
			{
				triangles[j] = array[triangles[j]];
			}
			Int3[] array2 = new Int3[num];
			for (int k = 0; k < num; k++)
			{
				array2[k] = vertices[k];
			}
			return array2;
		}
	}
}
