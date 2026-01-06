using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x020000C8 RID: 200
	internal abstract class RBTree<K> : IEnumerable
	{
		// Token: 0x06000BA7 RID: 2983
		protected abstract int CompareNode(K record1, K record2);

		// Token: 0x06000BA8 RID: 2984
		protected abstract int CompareSateliteTreeNode(K record1, K record2);

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0003426D File Offset: 0x0003246D
		protected RBTree(TreeAccessMethod accessMethod)
		{
			this._accessMethod = accessMethod;
			this.InitTree();
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00034284 File Offset: 0x00032484
		private void InitTree()
		{
			this.root = 0;
			this._pageTable = new RBTree<K>.TreePage[32];
			this._pageTableMap = new int[(this._pageTable.Length + 32 - 1) / 32];
			this._inUsePageCount = 0;
			this._nextFreePageLine = 0;
			this.AllocPage(32);
			this._pageTable[0]._slots[0]._nodeColor = RBTree<K>.NodeColor.black;
			this._pageTable[0]._slotMap[0] = 1;
			this._pageTable[0].InUseCount = 1;
			this._inUseNodeCount = 1;
			this._inUseSatelliteTreeCount = 0;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0003431C File Offset: 0x0003251C
		private void FreePage(RBTree<K>.TreePage page)
		{
			this.MarkPageFree(page);
			this._pageTable[page.PageId] = null;
			this._inUsePageCount--;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00034344 File Offset: 0x00032544
		private RBTree<K>.TreePage AllocPage(int size)
		{
			int num = this.GetIndexOfPageWithFreeSlot(false);
			if (num != -1)
			{
				this._pageTable[num] = new RBTree<K>.TreePage(size);
				this._nextFreePageLine = num / 32;
			}
			else
			{
				RBTree<K>.TreePage[] array = new RBTree<K>.TreePage[this._pageTable.Length * 2];
				Array.Copy(this._pageTable, 0, array, 0, this._pageTable.Length);
				int[] array2 = new int[(array.Length + 32 - 1) / 32];
				Array.Copy(this._pageTableMap, 0, array2, 0, this._pageTableMap.Length);
				this._nextFreePageLine = this._pageTableMap.Length;
				num = this._pageTable.Length;
				this._pageTable = array;
				this._pageTableMap = array2;
				this._pageTable[num] = new RBTree<K>.TreePage(size);
			}
			this._pageTable[num].PageId = num;
			this._inUsePageCount++;
			return this._pageTable[num];
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0003441E File Offset: 0x0003261E
		private void MarkPageFull(RBTree<K>.TreePage page)
		{
			this._pageTableMap[page.PageId / 32] |= 1 << page.PageId % 32;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00034446 File Offset: 0x00032646
		private void MarkPageFree(RBTree<K>.TreePage page)
		{
			this._pageTableMap[page.PageId / 32] &= ~(1 << page.PageId % 32);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00034470 File Offset: 0x00032670
		private static int GetIntValueFromBitMap(uint bitMap)
		{
			int num = 0;
			if ((bitMap & 4294901760U) != 0U)
			{
				num += 16;
				bitMap >>= 16;
			}
			if ((bitMap & 65280U) != 0U)
			{
				num += 8;
				bitMap >>= 8;
			}
			if ((bitMap & 240U) != 0U)
			{
				num += 4;
				bitMap >>= 4;
			}
			if ((bitMap & 12U) != 0U)
			{
				num += 2;
				bitMap >>= 2;
			}
			if ((bitMap & 2U) != 0U)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000344D0 File Offset: 0x000326D0
		private void FreeNode(int nodeId)
		{
			RBTree<K>.TreePage treePage = this._pageTable[nodeId >> 16];
			int num = nodeId & 65535;
			treePage._slots[num] = default(RBTree<K>.Node);
			treePage._slotMap[num / 32] &= ~(1 << num % 32);
			RBTree<K>.TreePage treePage2 = treePage;
			int inUseCount = treePage2.InUseCount;
			treePage2.InUseCount = inUseCount - 1;
			this._inUseNodeCount--;
			if (treePage.InUseCount == 0)
			{
				this.FreePage(treePage);
				return;
			}
			if (treePage.InUseCount == treePage._slots.Length - 1)
			{
				this.MarkPageFree(treePage);
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00034568 File Offset: 0x00032768
		private int GetIndexOfPageWithFreeSlot(bool allocatedPage)
		{
			int i = this._nextFreePageLine;
			int num = -1;
			while (i < this._pageTableMap.Length)
			{
				if (this._pageTableMap[i] < -1)
				{
					uint num2 = (uint)this._pageTableMap[i];
					while ((num2 ^ 4294967295U) != 0U)
					{
						uint num3 = ~num2 & (num2 + 1U);
						if (((long)this._pageTableMap[i] & (long)((ulong)num3)) != 0L)
						{
							throw ExceptionBuilder.InternalRBTreeError(RBTreeError.PagePositionInSlotInUse);
						}
						num = i * 32 + RBTree<K>.GetIntValueFromBitMap(num3);
						if (allocatedPage)
						{
							if (this._pageTable[num] != null)
							{
								return num;
							}
						}
						else if (this._pageTable[num] == null)
						{
							return num;
						}
						num = -1;
						num2 |= num3;
					}
				}
				i++;
			}
			if (this._nextFreePageLine != 0)
			{
				this._nextFreePageLine = 0;
				num = this.GetIndexOfPageWithFreeSlot(allocatedPage);
			}
			return num;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0003460B File Offset: 0x0003280B
		public int Count
		{
			get
			{
				return this._inUseNodeCount - 1;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x00034615 File Offset: 0x00032815
		public bool HasDuplicates
		{
			get
			{
				return this._inUseSatelliteTreeCount != 0;
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00034620 File Offset: 0x00032820
		private int GetNewNode(K key)
		{
			int indexOfPageWithFreeSlot = this.GetIndexOfPageWithFreeSlot(true);
			RBTree<K>.TreePage treePage;
			if (indexOfPageWithFreeSlot != -1)
			{
				treePage = this._pageTable[indexOfPageWithFreeSlot];
			}
			else if (this._inUsePageCount < 4)
			{
				treePage = this.AllocPage(32);
			}
			else if (this._inUsePageCount < 32)
			{
				treePage = this.AllocPage(256);
			}
			else if (this._inUsePageCount < 128)
			{
				treePage = this.AllocPage(1024);
			}
			else if (this._inUsePageCount < 4096)
			{
				treePage = this.AllocPage(4096);
			}
			else if (this._inUsePageCount < 32768)
			{
				treePage = this.AllocPage(8192);
			}
			else
			{
				treePage = this.AllocPage(65536);
			}
			int num = treePage.AllocSlot(this);
			if (num == -1)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.NoFreeSlots);
			}
			treePage._slots[num]._selfId = (treePage.PageId << 16) | num;
			treePage._slots[num]._subTreeSize = 1;
			treePage._slots[num]._keyOfNode = key;
			return treePage._slots[num]._selfId;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00034738 File Offset: 0x00032938
		private int Successor(int x_id)
		{
			if (this.Right(x_id) != 0)
			{
				return this.Minimum(this.Right(x_id));
			}
			int num = this.Parent(x_id);
			while (num != 0 && x_id == this.Right(num))
			{
				x_id = num;
				num = this.Parent(num);
			}
			return num;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00034780 File Offset: 0x00032980
		private bool Successor(ref int nodeId, ref int mainTreeNodeId)
		{
			if (nodeId == 0)
			{
				nodeId = this.Minimum(mainTreeNodeId);
				mainTreeNodeId = 0;
			}
			else
			{
				nodeId = this.Successor(nodeId);
				if (nodeId == 0 && mainTreeNodeId != 0)
				{
					nodeId = this.Successor(mainTreeNodeId);
					mainTreeNodeId = 0;
				}
			}
			if (nodeId != 0)
			{
				if (this.Next(nodeId) != 0)
				{
					if (mainTreeNodeId != 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.NestedSatelliteTreeEnumerator);
					}
					mainTreeNodeId = nodeId;
					nodeId = this.Minimum(this.Next(nodeId));
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000347F0 File Offset: 0x000329F0
		private int Minimum(int x_id)
		{
			while (this.Left(x_id) != 0)
			{
				x_id = this.Left(x_id);
			}
			return x_id;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00034808 File Offset: 0x00032A08
		private int LeftRotate(int root_id, int x_id, int mainTreeNode)
		{
			int num = this.Right(x_id);
			this.SetRight(x_id, this.Left(num));
			if (this.Left(num) != 0)
			{
				this.SetParent(this.Left(num), x_id);
			}
			this.SetParent(num, this.Parent(x_id));
			if (this.Parent(x_id) == 0)
			{
				if (root_id == 0)
				{
					this.root = num;
				}
				else
				{
					this.SetNext(mainTreeNode, num);
					this.SetKey(mainTreeNode, this.Key(num));
					root_id = num;
				}
			}
			else if (x_id == this.Left(this.Parent(x_id)))
			{
				this.SetLeft(this.Parent(x_id), num);
			}
			else
			{
				this.SetRight(this.Parent(x_id), num);
			}
			this.SetLeft(num, x_id);
			this.SetParent(x_id, num);
			if (x_id != 0)
			{
				this.SetSubTreeSize(x_id, this.SubTreeSize(this.Left(x_id)) + this.SubTreeSize(this.Right(x_id)) + ((this.Next(x_id) == 0) ? 1 : this.SubTreeSize(this.Next(x_id))));
			}
			if (num != 0)
			{
				this.SetSubTreeSize(num, this.SubTreeSize(this.Left(num)) + this.SubTreeSize(this.Right(num)) + ((this.Next(num) == 0) ? 1 : this.SubTreeSize(this.Next(num))));
			}
			return root_id;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00034940 File Offset: 0x00032B40
		private int RightRotate(int root_id, int x_id, int mainTreeNode)
		{
			int num = this.Left(x_id);
			this.SetLeft(x_id, this.Right(num));
			if (this.Right(num) != 0)
			{
				this.SetParent(this.Right(num), x_id);
			}
			this.SetParent(num, this.Parent(x_id));
			if (this.Parent(x_id) == 0)
			{
				if (root_id == 0)
				{
					this.root = num;
				}
				else
				{
					this.SetNext(mainTreeNode, num);
					this.SetKey(mainTreeNode, this.Key(num));
					root_id = num;
				}
			}
			else if (x_id == this.Left(this.Parent(x_id)))
			{
				this.SetLeft(this.Parent(x_id), num);
			}
			else
			{
				this.SetRight(this.Parent(x_id), num);
			}
			this.SetRight(num, x_id);
			this.SetParent(x_id, num);
			if (x_id != 0)
			{
				this.SetSubTreeSize(x_id, this.SubTreeSize(this.Left(x_id)) + this.SubTreeSize(this.Right(x_id)) + ((this.Next(x_id) == 0) ? 1 : this.SubTreeSize(this.Next(x_id))));
			}
			if (num != 0)
			{
				this.SetSubTreeSize(num, this.SubTreeSize(this.Left(num)) + this.SubTreeSize(this.Right(num)) + ((this.Next(num) == 0) ? 1 : this.SubTreeSize(this.Next(num))));
			}
			return root_id;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00034A78 File Offset: 0x00032C78
		private int RBInsert(int root_id, int x_id, int mainTreeNodeID, int position, bool append)
		{
			this._version++;
			int num = 0;
			int num2 = ((root_id == 0) ? this.root : root_id);
			if (this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX && !append)
			{
				while (num2 != 0)
				{
					this.IncreaseSize(num2);
					num = num2;
					int num3 = ((root_id == 0) ? this.CompareNode(this.Key(x_id), this.Key(num2)) : this.CompareSateliteTreeNode(this.Key(x_id), this.Key(num2)));
					if (num3 < 0)
					{
						num2 = this.Left(num2);
					}
					else if (num3 > 0)
					{
						num2 = this.Right(num2);
					}
					else
					{
						if (root_id != 0)
						{
							throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinInsert);
						}
						if (this.Next(num2) != 0)
						{
							root_id = this.RBInsert(this.Next(num2), x_id, num2, -1, false);
							this.SetKey(num2, this.Key(this.Next(num2)));
						}
						else
						{
							int newNode = this.GetNewNode(this.Key(num2));
							this._inUseSatelliteTreeCount++;
							this.SetNext(newNode, num2);
							this.SetColor(newNode, this.color(num2));
							this.SetParent(newNode, this.Parent(num2));
							this.SetLeft(newNode, this.Left(num2));
							this.SetRight(newNode, this.Right(num2));
							if (this.Left(this.Parent(num2)) == num2)
							{
								this.SetLeft(this.Parent(num2), newNode);
							}
							else if (this.Right(this.Parent(num2)) == num2)
							{
								this.SetRight(this.Parent(num2), newNode);
							}
							if (this.Left(num2) != 0)
							{
								this.SetParent(this.Left(num2), newNode);
							}
							if (this.Right(num2) != 0)
							{
								this.SetParent(this.Right(num2), newNode);
							}
							if (this.root == num2)
							{
								this.root = newNode;
							}
							this.SetColor(num2, RBTree<K>.NodeColor.black);
							this.SetParent(num2, 0);
							this.SetLeft(num2, 0);
							this.SetRight(num2, 0);
							int num4 = this.SubTreeSize(num2);
							this.SetSubTreeSize(num2, 1);
							root_id = this.RBInsert(num2, x_id, newNode, -1, false);
							this.SetSubTreeSize(newNode, num4);
						}
						return root_id;
					}
				}
			}
			else
			{
				if (this._accessMethod != TreeAccessMethod.INDEX_ONLY && !append)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethod1);
				}
				if (position == -1)
				{
					position = this.SubTreeSize(this.root);
				}
				while (num2 != 0)
				{
					this.IncreaseSize(num2);
					num = num2;
					int num5 = position - this.SubTreeSize(this.Left(num));
					if (num5 <= 0)
					{
						num2 = this.Left(num2);
					}
					else
					{
						num2 = this.Right(num2);
						if (num2 != 0)
						{
							position = num5 - 1;
						}
					}
				}
			}
			this.SetParent(x_id, num);
			if (num == 0)
			{
				if (root_id == 0)
				{
					this.root = x_id;
				}
				else
				{
					this.SetNext(mainTreeNodeID, x_id);
					this.SetKey(mainTreeNodeID, this.Key(x_id));
					root_id = x_id;
				}
			}
			else
			{
				int num6;
				if (this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX)
				{
					num6 = ((root_id == 0) ? this.CompareNode(this.Key(x_id), this.Key(num)) : this.CompareSateliteTreeNode(this.Key(x_id), this.Key(num)));
				}
				else
				{
					if (this._accessMethod != TreeAccessMethod.INDEX_ONLY)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethod2);
					}
					num6 = ((position <= 0) ? (-1) : 1);
				}
				if (num6 < 0)
				{
					this.SetLeft(num, x_id);
				}
				else
				{
					this.SetRight(num, x_id);
				}
			}
			this.SetLeft(x_id, 0);
			this.SetRight(x_id, 0);
			this.SetColor(x_id, RBTree<K>.NodeColor.red);
			while (this.color(this.Parent(x_id)) == RBTree<K>.NodeColor.red)
			{
				if (this.Parent(x_id) == this.Left(this.Parent(this.Parent(x_id))))
				{
					num = this.Right(this.Parent(this.Parent(x_id)));
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						x_id = this.Parent(this.Parent(x_id));
					}
					else
					{
						if (x_id == this.Right(this.Parent(x_id)))
						{
							x_id = this.Parent(x_id);
							root_id = this.LeftRotate(root_id, x_id, mainTreeNodeID);
						}
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						root_id = this.RightRotate(root_id, this.Parent(this.Parent(x_id)), mainTreeNodeID);
					}
				}
				else
				{
					num = this.Left(this.Parent(this.Parent(x_id)));
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						x_id = this.Parent(this.Parent(x_id));
					}
					else
					{
						if (x_id == this.Left(this.Parent(x_id)))
						{
							x_id = this.Parent(x_id);
							root_id = this.RightRotate(root_id, x_id, mainTreeNodeID);
						}
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						root_id = this.LeftRotate(root_id, this.Parent(this.Parent(x_id)), mainTreeNodeID);
					}
				}
			}
			if (root_id == 0)
			{
				this.SetColor(this.root, RBTree<K>.NodeColor.black);
			}
			else
			{
				this.SetColor(root_id, RBTree<K>.NodeColor.black);
			}
			return root_id;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00034F6C File Offset: 0x0003316C
		public void UpdateNodeKey(K currentKey, K newKey)
		{
			RBTree<K>.NodePath nodeByKey = this.GetNodeByKey(currentKey);
			if (this.Parent(nodeByKey._nodeID) == 0 && nodeByKey._nodeID != this.root)
			{
				this.SetKey(nodeByKey._mainTreeNodeID, newKey);
			}
			this.SetKey(nodeByKey._nodeID, newKey);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00034FB8 File Offset: 0x000331B8
		public K DeleteByIndex(int i)
		{
			RBTree<K>.NodePath nodeByIndex = this.GetNodeByIndex(i);
			K k = this.Key(nodeByIndex._nodeID);
			this.RBDeleteX(0, nodeByIndex._nodeID, nodeByIndex._mainTreeNodeID);
			return k;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00034FED File Offset: 0x000331ED
		public int RBDelete(int z_id)
		{
			return this.RBDeleteX(0, z_id, 0);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00034FF8 File Offset: 0x000331F8
		private int RBDeleteX(int root_id, int z_id, int mainTreeNodeID)
		{
			if (this.Next(z_id) != 0)
			{
				return this.RBDeleteX(this.Next(z_id), this.Next(z_id), z_id);
			}
			bool flag = false;
			int num = ((this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX) ? mainTreeNodeID : z_id);
			if (this.Next(num) != 0)
			{
				root_id = this.Next(num);
			}
			if (this.SubTreeSize(this.Next(num)) == 2)
			{
				flag = true;
			}
			else if (this.SubTreeSize(this.Next(num)) == 1)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidNextSizeInDelete);
			}
			int num2;
			if (this.Left(z_id) == 0 || this.Right(z_id) == 0)
			{
				num2 = z_id;
			}
			else
			{
				num2 = this.Successor(z_id);
			}
			int num3;
			if (this.Left(num2) != 0)
			{
				num3 = this.Left(num2);
			}
			else
			{
				num3 = this.Right(num2);
			}
			int num4 = this.Parent(num2);
			if (num3 != 0)
			{
				this.SetParent(num3, num4);
			}
			if (num4 == 0)
			{
				if (root_id == 0)
				{
					this.root = num3;
				}
				else
				{
					root_id = num3;
				}
			}
			else if (num2 == this.Left(num4))
			{
				this.SetLeft(num4, num3);
			}
			else
			{
				this.SetRight(num4, num3);
			}
			if (num2 != z_id)
			{
				this.SetKey(z_id, this.Key(num2));
				this.SetNext(z_id, this.Next(num2));
			}
			if (this.Next(num) != 0)
			{
				if (root_id == 0 && z_id != num)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinDelete);
				}
				if (root_id != 0)
				{
					this.SetNext(num, root_id);
					this.SetKey(num, this.Key(root_id));
				}
			}
			for (int num5 = num4; num5 != 0; num5 = this.Parent(num5))
			{
				this.RecomputeSize(num5);
			}
			if (root_id != 0)
			{
				for (int num6 = num; num6 != 0; num6 = this.Parent(num6))
				{
					this.DecreaseSize(num6);
				}
			}
			if (this.color(num2) == RBTree<K>.NodeColor.black)
			{
				root_id = this.RBDeleteFixup(root_id, num3, num4, mainTreeNodeID);
			}
			if (flag)
			{
				if (num == 0 || this.SubTreeSize(this.Next(num)) != 1)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidNodeSizeinDelete);
				}
				this._inUseSatelliteTreeCount--;
				int num7 = this.Next(num);
				this.SetLeft(num7, this.Left(num));
				this.SetRight(num7, this.Right(num));
				this.SetSubTreeSize(num7, this.SubTreeSize(num));
				this.SetColor(num7, this.color(num));
				if (this.Parent(num) != 0)
				{
					this.SetParent(num7, this.Parent(num));
					if (this.Left(this.Parent(num)) == num)
					{
						this.SetLeft(this.Parent(num), num7);
					}
					else
					{
						this.SetRight(this.Parent(num), num7);
					}
				}
				if (this.Left(num) != 0)
				{
					this.SetParent(this.Left(num), num7);
				}
				if (this.Right(num) != 0)
				{
					this.SetParent(this.Right(num), num7);
				}
				if (this.root == num)
				{
					this.root = num7;
				}
				this.FreeNode(num);
				num = 0;
			}
			else if (this.Next(num) != 0)
			{
				if (root_id == 0 && z_id != num)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinEndDelete);
				}
				if (root_id != 0)
				{
					this.SetNext(num, root_id);
					this.SetKey(num, this.Key(root_id));
				}
			}
			if (num2 != z_id)
			{
				this.SetLeft(num2, this.Left(z_id));
				this.SetRight(num2, this.Right(z_id));
				this.SetColor(num2, this.color(z_id));
				this.SetSubTreeSize(num2, this.SubTreeSize(z_id));
				if (this.Parent(z_id) != 0)
				{
					this.SetParent(num2, this.Parent(z_id));
					if (this.Left(this.Parent(z_id)) == z_id)
					{
						this.SetLeft(this.Parent(z_id), num2);
					}
					else
					{
						this.SetRight(this.Parent(z_id), num2);
					}
				}
				else
				{
					this.SetParent(num2, 0);
				}
				if (this.Left(z_id) != 0)
				{
					this.SetParent(this.Left(z_id), num2);
				}
				if (this.Right(z_id) != 0)
				{
					this.SetParent(this.Right(z_id), num2);
				}
				if (this.root == z_id)
				{
					this.root = num2;
				}
				else if (root_id == z_id)
				{
					root_id = num2;
				}
				if (num != 0 && this.Next(num) == z_id)
				{
					this.SetNext(num, num2);
				}
			}
			this.FreeNode(z_id);
			this._version++;
			return z_id;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000353EC File Offset: 0x000335EC
		private int RBDeleteFixup(int root_id, int x_id, int px_id, int mainTreeNodeID)
		{
			if (x_id == 0 && px_id == 0)
			{
				return 0;
			}
			while (((root_id == 0) ? this.root : root_id) != x_id && this.color(x_id) == RBTree<K>.NodeColor.black)
			{
				if ((x_id != 0 && x_id == this.Left(this.Parent(x_id))) || (x_id == 0 && this.Left(px_id) == 0))
				{
					int num = ((x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id)));
					if (num == 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.RBDeleteFixup);
					}
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(px_id, RBTree<K>.NodeColor.red);
						root_id = this.LeftRotate(root_id, px_id, mainTreeNodeID);
						num = ((x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id)));
					}
					if (this.color(this.Left(num)) == RBTree<K>.NodeColor.black && this.color(this.Right(num)) == RBTree<K>.NodeColor.black)
					{
						this.SetColor(num, RBTree<K>.NodeColor.red);
						x_id = px_id;
						px_id = this.Parent(px_id);
					}
					else
					{
						if (this.color(this.Right(num)) == RBTree<K>.NodeColor.black)
						{
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							this.SetColor(num, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, num, mainTreeNodeID);
							num = ((x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id)));
						}
						this.SetColor(num, this.color(px_id));
						this.SetColor(px_id, RBTree<K>.NodeColor.black);
						this.SetColor(this.Right(num), RBTree<K>.NodeColor.black);
						root_id = this.LeftRotate(root_id, px_id, mainTreeNodeID);
						x_id = ((root_id == 0) ? this.root : root_id);
						px_id = this.Parent(x_id);
					}
				}
				else
				{
					int num = this.Left(px_id);
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(num, RBTree<K>.NodeColor.black);
						if (x_id != 0)
						{
							this.SetColor(px_id, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
						}
						else
						{
							this.SetColor(px_id, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
							if (num == 0)
							{
								throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CannotRotateInvalidsuccessorNodeinDelete);
							}
						}
					}
					if (this.color(this.Right(num)) == RBTree<K>.NodeColor.black && this.color(this.Left(num)) == RBTree<K>.NodeColor.black)
					{
						this.SetColor(num, RBTree<K>.NodeColor.red);
						x_id = px_id;
						px_id = this.Parent(px_id);
					}
					else
					{
						if (this.color(this.Left(num)) == RBTree<K>.NodeColor.black)
						{
							this.SetColor(this.Right(num), RBTree<K>.NodeColor.black);
							this.SetColor(num, RBTree<K>.NodeColor.red);
							root_id = this.LeftRotate(root_id, num, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
						}
						if (x_id != 0)
						{
							this.SetColor(num, this.color(px_id));
							this.SetColor(px_id, RBTree<K>.NodeColor.black);
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							x_id = ((root_id == 0) ? this.root : root_id);
							px_id = this.Parent(x_id);
						}
						else
						{
							this.SetColor(num, this.color(px_id));
							this.SetColor(px_id, RBTree<K>.NodeColor.black);
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							x_id = ((root_id == 0) ? this.root : root_id);
							px_id = this.Parent(x_id);
						}
					}
				}
			}
			this.SetColor(x_id, RBTree<K>.NodeColor.black);
			return root_id;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00035724 File Offset: 0x00033924
		private int SearchSubTree(int root_id, K key)
		{
			if (root_id != 0 && this._accessMethod != TreeAccessMethod.KEY_SEARCH_AND_INDEX)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethodInNonNillRootSubtree);
			}
			int num = ((root_id == 0) ? this.root : root_id);
			while (num != 0)
			{
				int num2 = ((root_id == 0) ? this.CompareNode(key, this.Key(num)) : this.CompareSateliteTreeNode(key, this.Key(num)));
				if (num2 == 0)
				{
					break;
				}
				if (num2 < 0)
				{
					num = this.Left(num);
				}
				else
				{
					num = this.Right(num);
				}
			}
			return num;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00035794 File Offset: 0x00033994
		public int Search(K key)
		{
			int num = this.root;
			while (num != 0)
			{
				int num2 = this.CompareNode(key, this.Key(num));
				if (num2 == 0)
				{
					break;
				}
				if (num2 < 0)
				{
					num = this.Left(num);
				}
				else
				{
					num = this.Right(num);
				}
			}
			return num;
		}

		// Token: 0x1700021B RID: 539
		public K this[int index]
		{
			get
			{
				return this.Key(this.GetNodeByIndex(index)._nodeID);
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000357EC File Offset: 0x000339EC
		private RBTree<K>.NodePath GetNodeByKey(K key)
		{
			int num = this.SearchSubTree(0, key);
			if (this.Next(num) != 0)
			{
				return new RBTree<K>.NodePath(this.SearchSubTree(this.Next(num), key), num);
			}
			K k = this.Key(num);
			if (!k.Equals(key))
			{
				num = 0;
			}
			return new RBTree<K>.NodePath(num, 0);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00035848 File Offset: 0x00033A48
		public int GetIndexByKey(K key)
		{
			int num = -1;
			RBTree<K>.NodePath nodeByKey = this.GetNodeByKey(key);
			if (nodeByKey._nodeID != 0)
			{
				num = this.GetIndexByNodePath(nodeByKey);
			}
			return num;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00035870 File Offset: 0x00033A70
		public int GetIndexByNode(int node)
		{
			if (this._inUseSatelliteTreeCount == 0)
			{
				return this.ComputeIndexByNode(node);
			}
			if (this.Next(node) != 0)
			{
				return this.ComputeIndexWithSatelliteByNode(node);
			}
			int num = this.SearchSubTree(0, this.Key(node));
			if (num == node)
			{
				return this.ComputeIndexWithSatelliteByNode(node);
			}
			return this.ComputeIndexWithSatelliteByNode(num) + this.ComputeIndexByNode(node);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000358C8 File Offset: 0x00033AC8
		private int GetIndexByNodePath(RBTree<K>.NodePath path)
		{
			if (this._inUseSatelliteTreeCount == 0)
			{
				return this.ComputeIndexByNode(path._nodeID);
			}
			if (path._mainTreeNodeID == 0)
			{
				return this.ComputeIndexWithSatelliteByNode(path._nodeID);
			}
			return this.ComputeIndexWithSatelliteByNode(path._mainTreeNodeID) + this.ComputeIndexByNode(path._nodeID);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00035918 File Offset: 0x00033B18
		private int ComputeIndexByNode(int nodeId)
		{
			int num = this.SubTreeSize(this.Left(nodeId));
			while (nodeId != 0)
			{
				int num2 = this.Parent(nodeId);
				if (nodeId == this.Right(num2))
				{
					num += this.SubTreeSize(this.Left(num2)) + 1;
				}
				nodeId = num2;
			}
			return num;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00035960 File Offset: 0x00033B60
		private int ComputeIndexWithSatelliteByNode(int nodeId)
		{
			int num = this.SubTreeSize(this.Left(nodeId));
			while (nodeId != 0)
			{
				int num2 = this.Parent(nodeId);
				if (nodeId == this.Right(num2))
				{
					num += this.SubTreeSize(this.Left(num2)) + ((this.Next(num2) == 0) ? 1 : this.SubTreeSize(this.Next(num2)));
				}
				nodeId = num2;
			}
			return num;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000359C0 File Offset: 0x00033BC0
		private RBTree<K>.NodePath GetNodeByIndex(int userIndex)
		{
			int num;
			int num2;
			if (this._inUseSatelliteTreeCount == 0)
			{
				num = this.ComputeNodeByIndex(this.root, userIndex + 1);
				num2 = 0;
			}
			else
			{
				num = this.ComputeNodeByIndex(userIndex, out num2);
			}
			if (num != 0)
			{
				return new RBTree<K>.NodePath(num, num2);
			}
			if (TreeAccessMethod.INDEX_ONLY == this._accessMethod)
			{
				throw ExceptionBuilder.RowOutOfRange(userIndex);
			}
			throw ExceptionBuilder.InternalRBTreeError(RBTreeError.IndexOutOFRangeinGetNodeByIndex);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00035A18 File Offset: 0x00033C18
		private int ComputeNodeByIndex(int index, out int satelliteRootId)
		{
			index++;
			satelliteRootId = 0;
			int num = this.root;
			int num2;
			while (num != 0 && ((num2 = this.SubTreeSize(this.Left(num)) + 1) != index || this.Next(num) != 0))
			{
				if (index < num2)
				{
					num = this.Left(num);
				}
				else
				{
					if (this.Next(num) != 0 && index >= num2 && index <= num2 + this.SubTreeSize(this.Next(num)) - 1)
					{
						satelliteRootId = num;
						index = index - num2 + 1;
						return this.ComputeNodeByIndex(this.Next(num), index);
					}
					if (this.Next(num) == 0)
					{
						index -= num2;
					}
					else
					{
						index -= num2 + this.SubTreeSize(this.Next(num)) - 1;
					}
					num = this.Right(num);
				}
			}
			return num;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00035AD4 File Offset: 0x00033CD4
		private int ComputeNodeByIndex(int x_id, int index)
		{
			while (x_id != 0)
			{
				int num = this.Left(x_id);
				int num2 = this.SubTreeSize(num) + 1;
				if (index < num2)
				{
					x_id = num;
				}
				else
				{
					if (num2 >= index)
					{
						break;
					}
					x_id = this.Right(x_id);
					index -= num2;
				}
			}
			return x_id;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00035B14 File Offset: 0x00033D14
		public int Insert(K item)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, -1, false);
			return newNode;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00035B38 File Offset: 0x00033D38
		public int Add(K item)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, -1, false);
			return newNode;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00035B5A File Offset: 0x00033D5A
		public IEnumerator GetEnumerator()
		{
			return new RBTree<K>.RBTreeEnumerator(this);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00035B68 File Offset: 0x00033D68
		public int IndexOf(int nodeId, K item)
		{
			int num = -1;
			if (nodeId == 0)
			{
				return num;
			}
			if (this.Key(nodeId) == item)
			{
				return this.GetIndexByNode(nodeId);
			}
			if ((num = this.IndexOf(this.Left(nodeId), item)) != -1)
			{
				return num;
			}
			return this.IndexOf(this.Right(nodeId), item);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00035BC1 File Offset: 0x00033DC1
		public int Insert(int position, K item)
		{
			return this.InsertAt(position, item, false);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00035BCC File Offset: 0x00033DCC
		public int InsertAt(int position, K item, bool append)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, position, append);
			return newNode;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00035BEE File Offset: 0x00033DEE
		public void RemoveAt(int position)
		{
			this.DeleteByIndex(position);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00035BF8 File Offset: 0x00033DF8
		public void Clear()
		{
			this.InitTree();
			this._version++;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00035C10 File Offset: 0x00033E10
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			int count = this.Count;
			if (array.Length - index < this.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			int num = this.Minimum(this.root);
			for (int i = 0; i < count; i++)
			{
				array.SetValue(this.Key(num), index + i);
				num = this.Successor(num);
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00035C90 File Offset: 0x00033E90
		public void CopyTo(K[] array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			int count = this.Count;
			if (array.Length - index < this.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			int num = this.Minimum(this.root);
			for (int i = 0; i < count; i++)
			{
				array[index + i] = this.Key(num);
				num = this.Successor(num);
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00035D05 File Offset: 0x00033F05
		private void SetRight(int nodeId, int rightNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._rightId = rightNodeId;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00035D29 File Offset: 0x00033F29
		private void SetLeft(int nodeId, int leftNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._leftId = leftNodeId;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00035D4D File Offset: 0x00033F4D
		private void SetParent(int nodeId, int parentNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._parentId = parentNodeId;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00035D71 File Offset: 0x00033F71
		private void SetColor(int nodeId, RBTree<K>.NodeColor color)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nodeColor = color;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00035D95 File Offset: 0x00033F95
		private void SetKey(int nodeId, K key)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._keyOfNode = key;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00035DB9 File Offset: 0x00033FB9
		private void SetNext(int nodeId, int nextNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nextId = nextNodeId;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00035DDD File Offset: 0x00033FDD
		private void SetSubTreeSize(int nodeId, int size)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._subTreeSize = size;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00035E01 File Offset: 0x00034001
		private void IncreaseSize(int nodeId)
		{
			RBTree<K>.Node[] slots = this._pageTable[nodeId >> 16]._slots;
			int num = nodeId & 65535;
			slots[num]._subTreeSize = slots[num]._subTreeSize + 1;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00035E2C File Offset: 0x0003402C
		private void RecomputeSize(int nodeId)
		{
			int num = this.SubTreeSize(this.Left(nodeId)) + this.SubTreeSize(this.Right(nodeId)) + ((this.Next(nodeId) == 0) ? 1 : this.SubTreeSize(this.Next(nodeId)));
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._subTreeSize = num;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00035E91 File Offset: 0x00034091
		private void DecreaseSize(int nodeId)
		{
			RBTree<K>.Node[] slots = this._pageTable[nodeId >> 16]._slots;
			int num = nodeId & 65535;
			slots[num]._subTreeSize = slots[num]._subTreeSize - 1;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00035EB9 File Offset: 0x000340B9
		[Conditional("DEBUG")]
		private void VerifySize(int nodeId, int size)
		{
			this.SubTreeSize(this.Left(nodeId));
			this.SubTreeSize(this.Right(nodeId));
			if (this.Next(nodeId) != 0)
			{
				this.SubTreeSize(this.Next(nodeId));
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00035EEE File Offset: 0x000340EE
		public int Right(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._rightId;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00035F11 File Offset: 0x00034111
		public int Left(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._leftId;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00035F34 File Offset: 0x00034134
		public int Parent(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._parentId;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00035F57 File Offset: 0x00034157
		private RBTree<K>.NodeColor color(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nodeColor;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00035F7A File Offset: 0x0003417A
		public int Next(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nextId;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00035F9D File Offset: 0x0003419D
		public int SubTreeSize(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._subTreeSize;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00035FC0 File Offset: 0x000341C0
		public K Key(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._keyOfNode;
		}

		// Token: 0x0400079E RID: 1950
		internal const int DefaultPageSize = 32;

		// Token: 0x0400079F RID: 1951
		internal const int NIL = 0;

		// Token: 0x040007A0 RID: 1952
		private RBTree<K>.TreePage[] _pageTable;

		// Token: 0x040007A1 RID: 1953
		private int[] _pageTableMap;

		// Token: 0x040007A2 RID: 1954
		private int _inUsePageCount;

		// Token: 0x040007A3 RID: 1955
		private int _nextFreePageLine;

		// Token: 0x040007A4 RID: 1956
		public int root;

		// Token: 0x040007A5 RID: 1957
		private int _version;

		// Token: 0x040007A6 RID: 1958
		private int _inUseNodeCount;

		// Token: 0x040007A7 RID: 1959
		private int _inUseSatelliteTreeCount;

		// Token: 0x040007A8 RID: 1960
		private readonly TreeAccessMethod _accessMethod;

		// Token: 0x020000C9 RID: 201
		private enum NodeColor
		{
			// Token: 0x040007AA RID: 1962
			red,
			// Token: 0x040007AB RID: 1963
			black
		}

		// Token: 0x020000CA RID: 202
		private struct Node
		{
			// Token: 0x040007AC RID: 1964
			internal int _selfId;

			// Token: 0x040007AD RID: 1965
			internal int _leftId;

			// Token: 0x040007AE RID: 1966
			internal int _rightId;

			// Token: 0x040007AF RID: 1967
			internal int _parentId;

			// Token: 0x040007B0 RID: 1968
			internal int _nextId;

			// Token: 0x040007B1 RID: 1969
			internal int _subTreeSize;

			// Token: 0x040007B2 RID: 1970
			internal K _keyOfNode;

			// Token: 0x040007B3 RID: 1971
			internal RBTree<K>.NodeColor _nodeColor;
		}

		// Token: 0x020000CB RID: 203
		private readonly struct NodePath
		{
			// Token: 0x06000BE8 RID: 3048 RVA: 0x00035FE3 File Offset: 0x000341E3
			internal NodePath(int nodeID, int mainTreeNodeID)
			{
				this._nodeID = nodeID;
				this._mainTreeNodeID = mainTreeNodeID;
			}

			// Token: 0x040007B4 RID: 1972
			internal readonly int _nodeID;

			// Token: 0x040007B5 RID: 1973
			internal readonly int _mainTreeNodeID;
		}

		// Token: 0x020000CC RID: 204
		private sealed class TreePage
		{
			// Token: 0x06000BE9 RID: 3049 RVA: 0x00035FF3 File Offset: 0x000341F3
			internal TreePage(int size)
			{
				if (size > 65536)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidPageSize);
				}
				this._slots = new RBTree<K>.Node[size];
				this._slotMap = new int[(size + 32 - 1) / 32];
			}

			// Token: 0x06000BEA RID: 3050 RVA: 0x0003602C File Offset: 0x0003422C
			internal int AllocSlot(RBTree<K> tree)
			{
				int num = -1;
				if (this._inUseCount < this._slots.Length)
				{
					for (int i = this._nextFreeSlotLine; i < this._slotMap.Length; i++)
					{
						if (this._slotMap[i] < -1)
						{
							int num2 = ~this._slotMap[i] & (this._slotMap[i] + 1);
							this._slotMap[i] |= num2;
							this._inUseCount++;
							if (this._inUseCount == this._slots.Length)
							{
								tree.MarkPageFull(this);
							}
							tree._inUseNodeCount++;
							num = RBTree<K>.GetIntValueFromBitMap((uint)num2);
							this._nextFreeSlotLine = i;
							num = i * 32 + num;
							break;
						}
					}
					if (num == -1 && this._nextFreeSlotLine != 0)
					{
						this._nextFreeSlotLine = 0;
						num = this.AllocSlot(tree);
					}
				}
				return num;
			}

			// Token: 0x1700021C RID: 540
			// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00036109 File Offset: 0x00034309
			// (set) Token: 0x06000BEC RID: 3052 RVA: 0x00036111 File Offset: 0x00034311
			internal int InUseCount
			{
				get
				{
					return this._inUseCount;
				}
				set
				{
					this._inUseCount = value;
				}
			}

			// Token: 0x1700021D RID: 541
			// (get) Token: 0x06000BED RID: 3053 RVA: 0x0003611A File Offset: 0x0003431A
			// (set) Token: 0x06000BEE RID: 3054 RVA: 0x00036122 File Offset: 0x00034322
			internal int PageId
			{
				get
				{
					return this._pageId;
				}
				set
				{
					this._pageId = value;
				}
			}

			// Token: 0x040007B6 RID: 1974
			public const int slotLineSize = 32;

			// Token: 0x040007B7 RID: 1975
			internal readonly RBTree<K>.Node[] _slots;

			// Token: 0x040007B8 RID: 1976
			internal readonly int[] _slotMap;

			// Token: 0x040007B9 RID: 1977
			private int _inUseCount;

			// Token: 0x040007BA RID: 1978
			private int _pageId;

			// Token: 0x040007BB RID: 1979
			private int _nextFreeSlotLine;
		}

		// Token: 0x020000CD RID: 205
		internal struct RBTreeEnumerator : IEnumerator<K>, IDisposable, IEnumerator
		{
			// Token: 0x06000BEF RID: 3055 RVA: 0x0003612B File Offset: 0x0003432B
			internal RBTreeEnumerator(RBTree<K> tree)
			{
				this._tree = tree;
				this._version = tree._version;
				this._index = 0;
				this._mainTreeNodeId = tree.root;
				this._current = default(K);
			}

			// Token: 0x06000BF0 RID: 3056 RVA: 0x00036160 File Offset: 0x00034360
			internal RBTreeEnumerator(RBTree<K> tree, int position)
			{
				this._tree = tree;
				this._version = tree._version;
				if (position == 0)
				{
					this._index = 0;
					this._mainTreeNodeId = tree.root;
				}
				else
				{
					this._index = tree.ComputeNodeByIndex(position - 1, out this._mainTreeNodeId);
					if (this._index == 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.IndexOutOFRangeinGetNodeByIndex);
					}
				}
				this._current = default(K);
			}

			// Token: 0x06000BF1 RID: 3057 RVA: 0x000094D4 File Offset: 0x000076D4
			public void Dispose()
			{
			}

			// Token: 0x06000BF2 RID: 3058 RVA: 0x000361CC File Offset: 0x000343CC
			public bool MoveNext()
			{
				if (this._version != this._tree._version)
				{
					throw ExceptionBuilder.EnumeratorModified();
				}
				bool flag = this._tree.Successor(ref this._index, ref this._mainTreeNodeId);
				this._current = this._tree.Key(this._index);
				return flag;
			}

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00036220 File Offset: 0x00034420
			public K Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00036228 File Offset: 0x00034428
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BF5 RID: 3061 RVA: 0x00036235 File Offset: 0x00034435
			void IEnumerator.Reset()
			{
				if (this._version != this._tree._version)
				{
					throw ExceptionBuilder.EnumeratorModified();
				}
				this._index = 0;
				this._mainTreeNodeId = this._tree.root;
				this._current = default(K);
			}

			// Token: 0x040007BC RID: 1980
			private readonly RBTree<K> _tree;

			// Token: 0x040007BD RID: 1981
			private readonly int _version;

			// Token: 0x040007BE RID: 1982
			private int _index;

			// Token: 0x040007BF RID: 1983
			private int _mainTreeNodeId;

			// Token: 0x040007C0 RID: 1984
			private K _current;
		}
	}
}
