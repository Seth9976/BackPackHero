using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000073 RID: 115
	public abstract class UIBehaviour : MonoBehaviour
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x0001B9D7 File Offset: 0x00019BD7
		protected virtual void Awake()
		{
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001B9D9 File Offset: 0x00019BD9
		protected virtual void OnEnable()
		{
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001B9DB File Offset: 0x00019BDB
		protected virtual void Start()
		{
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001B9DD File Offset: 0x00019BDD
		protected virtual void OnDisable()
		{
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001B9DF File Offset: 0x00019BDF
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001B9E1 File Offset: 0x00019BE1
		public virtual bool IsActive()
		{
			return base.isActiveAndEnabled;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001B9E9 File Offset: 0x00019BE9
		protected virtual void OnRectTransformDimensionsChange()
		{
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001B9EB File Offset: 0x00019BEB
		protected virtual void OnBeforeTransformParentChanged()
		{
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001B9ED File Offset: 0x00019BED
		protected virtual void OnTransformParentChanged()
		{
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001B9EF File Offset: 0x00019BEF
		protected virtual void OnDidApplyAnimationProperties()
		{
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001B9F1 File Offset: 0x00019BF1
		protected virtual void OnCanvasGroupChanged()
		{
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001B9F3 File Offset: 0x00019BF3
		protected virtual void OnCanvasHierarchyChanged()
		{
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001B9F5 File Offset: 0x00019BF5
		public bool IsDestroyed()
		{
			return this == null;
		}
	}
}
