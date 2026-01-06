using System;

namespace System.Data
{
	// Token: 0x020000C6 RID: 198
	internal enum RBTreeError
	{
		// Token: 0x04000789 RID: 1929
		InvalidPageSize = 1,
		// Token: 0x0400078A RID: 1930
		PagePositionInSlotInUse = 3,
		// Token: 0x0400078B RID: 1931
		NoFreeSlots,
		// Token: 0x0400078C RID: 1932
		InvalidStateinInsert,
		// Token: 0x0400078D RID: 1933
		InvalidNextSizeInDelete = 7,
		// Token: 0x0400078E RID: 1934
		InvalidStateinDelete,
		// Token: 0x0400078F RID: 1935
		InvalidNodeSizeinDelete,
		// Token: 0x04000790 RID: 1936
		InvalidStateinEndDelete,
		// Token: 0x04000791 RID: 1937
		CannotRotateInvalidsuccessorNodeinDelete,
		// Token: 0x04000792 RID: 1938
		IndexOutOFRangeinGetNodeByIndex = 13,
		// Token: 0x04000793 RID: 1939
		RBDeleteFixup,
		// Token: 0x04000794 RID: 1940
		UnsupportedAccessMethod1,
		// Token: 0x04000795 RID: 1941
		UnsupportedAccessMethod2,
		// Token: 0x04000796 RID: 1942
		UnsupportedAccessMethodInNonNillRootSubtree,
		// Token: 0x04000797 RID: 1943
		AttachedNodeWithZerorbTreeNodeId,
		// Token: 0x04000798 RID: 1944
		CompareNodeInDataRowTree,
		// Token: 0x04000799 RID: 1945
		CompareSateliteTreeNodeInDataRowTree,
		// Token: 0x0400079A RID: 1946
		NestedSatelliteTreeEnumerator
	}
}
