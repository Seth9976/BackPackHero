using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000097 RID: 151
	internal class AtlasAllocatorDynamic
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x00017170 File Offset: 0x00015370
		public AtlasAllocatorDynamic(int width, int height, int capacityAllocations)
		{
			int num = capacityAllocations * 2;
			this.m_Pool = new AtlasAllocatorDynamic.AtlasNodePool((short)num);
			this.m_NodeFromID = new Dictionary<int, short>(capacityAllocations);
			short num2 = -1;
			this.m_Root = this.m_Pool.AtlasNodeCreate(num2);
			this.m_Pool.m_Nodes[(int)this.m_Root].m_Rect.Set((float)width, (float)height, 0f, 0f);
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000171F0 File Offset: 0x000153F0
		public bool Allocate(out Vector4 result, int key, int width, int height)
		{
			short num = this.m_Pool.m_Nodes[(int)this.m_Root].Allocate(this.m_Pool, width, height);
			if (num >= 0)
			{
				result = this.m_Pool.m_Nodes[(int)num].m_Rect;
				this.m_NodeFromID.Add(key, num);
				return true;
			}
			result = Vector4.zero;
			return false;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00017260 File Offset: 0x00015460
		public void Release(int key)
		{
			short num;
			if (this.m_NodeFromID.TryGetValue(key, out num))
			{
				this.m_Pool.m_Nodes[(int)num].ReleaseAndMerge(this.m_Pool);
				this.m_NodeFromID.Remove(key);
				return;
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000172A8 File Offset: 0x000154A8
		public void Release()
		{
			this.m_Pool.Clear();
			this.m_Root = this.m_Pool.AtlasNodeCreate(-1);
			this.m_Pool.m_Nodes[(int)this.m_Root].m_Rect.Set((float)this.m_Width, (float)this.m_Height, 0f, 0f);
			this.m_NodeFromID.Clear();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00017318 File Offset: 0x00015518
		public string DebugStringFromRoot(int depthMax = -1)
		{
			string text = "";
			this.DebugStringFromNode(ref text, this.m_Root, 0, depthMax);
			return text;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001733C File Offset: 0x0001553C
		private void DebugStringFromNode(ref string res, short n, int depthCurrent = 0, int depthMax = -1)
		{
			res = string.Concat(new string[]
			{
				res,
				"{[",
				depthCurrent.ToString(),
				"], isOccupied = ",
				this.m_Pool.m_Nodes[(int)n].IsOccupied() ? "true" : "false",
				", self = ",
				this.m_Pool.m_Nodes[(int)n].m_Self.ToString(),
				", ",
				this.m_Pool.m_Nodes[(int)n].m_Rect.x.ToString(),
				",",
				this.m_Pool.m_Nodes[(int)n].m_Rect.y.ToString(),
				", ",
				this.m_Pool.m_Nodes[(int)n].m_Rect.z.ToString(),
				", ",
				this.m_Pool.m_Nodes[(int)n].m_Rect.w.ToString(),
				"}\n"
			});
			if (depthMax == -1 || depthCurrent < depthMax)
			{
				if (this.m_Pool.m_Nodes[(int)n].m_LeftChild >= 0)
				{
					this.DebugStringFromNode(ref res, this.m_Pool.m_Nodes[(int)n].m_LeftChild, depthCurrent + 1, depthMax);
				}
				if (this.m_Pool.m_Nodes[(int)n].m_RightChild >= 0)
				{
					this.DebugStringFromNode(ref res, this.m_Pool.m_Nodes[(int)n].m_RightChild, depthCurrent + 1, depthMax);
				}
			}
		}

		// Token: 0x0400031D RID: 797
		private int m_Width;

		// Token: 0x0400031E RID: 798
		private int m_Height;

		// Token: 0x0400031F RID: 799
		private AtlasAllocatorDynamic.AtlasNodePool m_Pool;

		// Token: 0x04000320 RID: 800
		private short m_Root;

		// Token: 0x04000321 RID: 801
		private Dictionary<int, short> m_NodeFromID;

		// Token: 0x0200016F RID: 367
		private class AtlasNodePool
		{
			// Token: 0x060008EB RID: 2283 RVA: 0x000243AB File Offset: 0x000225AB
			public AtlasNodePool(short capacity)
			{
				this.m_Nodes = new AtlasAllocatorDynamic.AtlasNode[(int)capacity];
				this.m_Next = 0;
				this.m_FreelistHead = -1;
			}

			// Token: 0x060008EC RID: 2284 RVA: 0x000243CD File Offset: 0x000225CD
			public void Dispose()
			{
				this.Clear();
				this.m_Nodes = null;
			}

			// Token: 0x060008ED RID: 2285 RVA: 0x000243DC File Offset: 0x000225DC
			public void Clear()
			{
				this.m_Next = 0;
				this.m_FreelistHead = -1;
			}

			// Token: 0x060008EE RID: 2286 RVA: 0x000243EC File Offset: 0x000225EC
			public short AtlasNodeCreate(short parent)
			{
				if (this.m_FreelistHead != -1)
				{
					short freelistNext = this.m_Nodes[(int)this.m_FreelistHead].m_FreelistNext;
					this.m_Nodes[(int)this.m_FreelistHead] = new AtlasAllocatorDynamic.AtlasNode(this.m_FreelistHead, parent);
					short freelistHead = this.m_FreelistHead;
					this.m_FreelistHead = freelistNext;
					return freelistHead;
				}
				this.m_Nodes[(int)this.m_Next] = new AtlasAllocatorDynamic.AtlasNode(this.m_Next, parent);
				short next = this.m_Next;
				this.m_Next = next + 1;
				return next;
			}

			// Token: 0x060008EF RID: 2287 RVA: 0x00024473 File Offset: 0x00022673
			public void AtlasNodeFree(short index)
			{
				this.m_Nodes[(int)index].m_FreelistNext = this.m_FreelistHead;
				this.m_FreelistHead = index;
			}

			// Token: 0x0400057D RID: 1405
			internal AtlasAllocatorDynamic.AtlasNode[] m_Nodes;

			// Token: 0x0400057E RID: 1406
			private short m_Next;

			// Token: 0x0400057F RID: 1407
			private short m_FreelistHead;
		}

		// Token: 0x02000170 RID: 368
		[StructLayout(LayoutKind.Explicit, Size = 32)]
		private struct AtlasNode
		{
			// Token: 0x060008F0 RID: 2288 RVA: 0x00024493 File Offset: 0x00022693
			public AtlasNode(short self, short parent)
			{
				this.m_Self = self;
				this.m_Parent = parent;
				this.m_LeftChild = -1;
				this.m_RightChild = -1;
				this.m_Flags = 0;
				this.m_FreelistNext = -1;
				this.m_Rect = Vector4.zero;
			}

			// Token: 0x060008F1 RID: 2289 RVA: 0x000244CA File Offset: 0x000226CA
			public bool IsOccupied()
			{
				return (this.m_Flags & 1) > 0;
			}

			// Token: 0x060008F2 RID: 2290 RVA: 0x000244D8 File Offset: 0x000226D8
			public void SetIsOccupied()
			{
				ushort num = 1;
				this.m_Flags |= num;
			}

			// Token: 0x060008F3 RID: 2291 RVA: 0x000244F8 File Offset: 0x000226F8
			public void ClearIsOccupied()
			{
				ushort num = 1;
				this.m_Flags &= ~num;
			}

			// Token: 0x060008F4 RID: 2292 RVA: 0x00024518 File Offset: 0x00022718
			public bool IsLeafNode()
			{
				return this.m_LeftChild == -1;
			}

			// Token: 0x060008F5 RID: 2293 RVA: 0x00024524 File Offset: 0x00022724
			public short Allocate(AtlasAllocatorDynamic.AtlasNodePool pool, int width, int height)
			{
				if (Mathf.Min(width, height) < 1)
				{
					return -1;
				}
				if (!this.IsLeafNode())
				{
					short num = pool.m_Nodes[(int)this.m_LeftChild].Allocate(pool, width, height);
					if (num == -1)
					{
						num = pool.m_Nodes[(int)this.m_RightChild].Allocate(pool, width, height);
					}
					return num;
				}
				if (this.IsOccupied())
				{
					return -1;
				}
				if ((float)width > this.m_Rect.x || (float)height > this.m_Rect.y)
				{
					return -1;
				}
				this.m_LeftChild = pool.AtlasNodeCreate(this.m_Self);
				this.m_RightChild = pool.AtlasNodeCreate(this.m_Self);
				float num2 = this.m_Rect.x - (float)width;
				float num3 = this.m_Rect.y - (float)height;
				if (num2 >= num3)
				{
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.x = (float)width;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.y = this.m_Rect.y;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.z = this.m_Rect.z;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.w = this.m_Rect.w;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.x = num2;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.y = this.m_Rect.y;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.z = this.m_Rect.z + (float)width;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.w = this.m_Rect.w;
					if (num3 < 1f)
					{
						pool.m_Nodes[(int)this.m_LeftChild].SetIsOccupied();
						return this.m_LeftChild;
					}
					short num4 = pool.m_Nodes[(int)this.m_LeftChild].Allocate(pool, width, height);
					if (num4 >= 0)
					{
						pool.m_Nodes[(int)num4].SetIsOccupied();
					}
					return num4;
				}
				else
				{
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.x = this.m_Rect.x;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.y = (float)height;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.z = this.m_Rect.z;
					pool.m_Nodes[(int)this.m_LeftChild].m_Rect.w = this.m_Rect.w;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.x = this.m_Rect.x;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.y = num3;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.z = this.m_Rect.z;
					pool.m_Nodes[(int)this.m_RightChild].m_Rect.w = this.m_Rect.w + (float)height;
					if (num2 < 1f)
					{
						pool.m_Nodes[(int)this.m_LeftChild].SetIsOccupied();
						return this.m_LeftChild;
					}
					short num5 = pool.m_Nodes[(int)this.m_LeftChild].Allocate(pool, width, height);
					if (num5 >= 0)
					{
						pool.m_Nodes[(int)num5].SetIsOccupied();
					}
					return num5;
				}
			}

			// Token: 0x060008F6 RID: 2294 RVA: 0x000248E4 File Offset: 0x00022AE4
			public void ReleaseChildren(AtlasAllocatorDynamic.AtlasNodePool pool)
			{
				if (this.IsLeafNode())
				{
					return;
				}
				pool.m_Nodes[(int)this.m_LeftChild].ReleaseChildren(pool);
				pool.m_Nodes[(int)this.m_RightChild].ReleaseChildren(pool);
				pool.AtlasNodeFree(this.m_LeftChild);
				pool.AtlasNodeFree(this.m_RightChild);
				this.m_LeftChild = -1;
				this.m_RightChild = -1;
			}

			// Token: 0x060008F7 RID: 2295 RVA: 0x00024950 File Offset: 0x00022B50
			public void ReleaseAndMerge(AtlasAllocatorDynamic.AtlasNodePool pool)
			{
				short num = this.m_Self;
				do
				{
					pool.m_Nodes[(int)num].ReleaseChildren(pool);
					pool.m_Nodes[(int)num].ClearIsOccupied();
					num = pool.m_Nodes[(int)num].m_Parent;
				}
				while (num >= 0 && pool.m_Nodes[(int)num].IsMergeNeeded(pool));
			}

			// Token: 0x060008F8 RID: 2296 RVA: 0x000249B4 File Offset: 0x00022BB4
			public bool IsMergeNeeded(AtlasAllocatorDynamic.AtlasNodePool pool)
			{
				return pool.m_Nodes[(int)this.m_LeftChild].IsLeafNode() && !pool.m_Nodes[(int)this.m_LeftChild].IsOccupied() && pool.m_Nodes[(int)this.m_RightChild].IsLeafNode() && !pool.m_Nodes[(int)this.m_RightChild].IsOccupied();
			}

			// Token: 0x04000580 RID: 1408
			[FieldOffset(0)]
			public short m_Self;

			// Token: 0x04000581 RID: 1409
			[FieldOffset(2)]
			public short m_Parent;

			// Token: 0x04000582 RID: 1410
			[FieldOffset(4)]
			public short m_LeftChild;

			// Token: 0x04000583 RID: 1411
			[FieldOffset(6)]
			public short m_RightChild;

			// Token: 0x04000584 RID: 1412
			[FieldOffset(8)]
			public short m_FreelistNext;

			// Token: 0x04000585 RID: 1413
			[FieldOffset(10)]
			public ushort m_Flags;

			// Token: 0x04000586 RID: 1414
			[FieldOffset(16)]
			public Vector4 m_Rect;

			// Token: 0x02000198 RID: 408
			private enum AtlasNodeFlags : uint
			{
				// Token: 0x040005E3 RID: 1507
				IsOccupied = 1U
			}
		}
	}
}
