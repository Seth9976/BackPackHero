using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000DF RID: 223
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	public struct SortingLayer
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00005E10 File Offset: 0x00004010
		public int id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00005E28 File Offset: 0x00004028
		public string name
		{
			get
			{
				return SortingLayer.IDToName(this.m_Id);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00005E48 File Offset: 0x00004048
		public int value
		{
			get
			{
				return SortingLayer.GetLayerValueFromID(this.m_Id);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00005E68 File Offset: 0x00004068
		public static SortingLayer[] layers
		{
			get
			{
				int[] sortingLayerIDsInternal = SortingLayer.GetSortingLayerIDsInternal();
				SortingLayer[] array = new SortingLayer[sortingLayerIDsInternal.Length];
				for (int i = 0; i < sortingLayerIDsInternal.Length; i++)
				{
					array[i].m_Id = sortingLayerIDsInternal[i];
				}
				return array;
			}
		}

		// Token: 0x06000380 RID: 896
		[FreeFunction("GetTagManager().GetSortingLayerIDs")]
		[MethodImpl(4096)]
		private static extern int[] GetSortingLayerIDsInternal();

		// Token: 0x06000381 RID: 897
		[FreeFunction("GetTagManager().GetSortingLayerValueFromUniqueID")]
		[MethodImpl(4096)]
		public static extern int GetLayerValueFromID(int id);

		// Token: 0x06000382 RID: 898
		[FreeFunction("GetTagManager().GetSortingLayerValueFromName")]
		[MethodImpl(4096)]
		public static extern int GetLayerValueFromName(string name);

		// Token: 0x06000383 RID: 899
		[FreeFunction("GetTagManager().GetSortingLayerUniqueIDFromName")]
		[MethodImpl(4096)]
		public static extern int NameToID(string name);

		// Token: 0x06000384 RID: 900
		[FreeFunction("GetTagManager().GetSortingLayerNameFromUniqueID")]
		[MethodImpl(4096)]
		public static extern string IDToName(int id);

		// Token: 0x06000385 RID: 901
		[FreeFunction("GetTagManager().IsSortingLayerUniqueIDValid")]
		[MethodImpl(4096)]
		public static extern bool IsValid(int id);

		// Token: 0x040002DF RID: 735
		private int m_Id;
	}
}
