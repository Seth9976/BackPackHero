using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x02000105 RID: 261
	internal class CollectionViewController
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600080D RID: 2061 RVA: 0x0001DB10 File Offset: 0x0001BD10
		// (remove) Token: 0x0600080E RID: 2062 RVA: 0x0001DB48 File Offset: 0x0001BD48
		[field: DebuggerBrowsable(0)]
		public event Action itemsSourceChanged;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600080F RID: 2063 RVA: 0x0001DB80 File Offset: 0x0001BD80
		// (remove) Token: 0x06000810 RID: 2064 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		[field: DebuggerBrowsable(0)]
		public event Action<int, int> itemIndexChanged;

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001DBED File Offset: 0x0001BDED
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		public IList itemsSource
		{
			get
			{
				return this.m_ItemsSource;
			}
			set
			{
				bool flag = this.m_ItemsSource == value;
				if (!flag)
				{
					this.m_ItemsSource = value;
					this.RaiseItemsSourceChanged();
				}
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001DC23 File Offset: 0x0001BE23
		protected void SetItemsSourceWithoutNotify(IList source)
		{
			this.m_ItemsSource = source;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001DC2D File Offset: 0x0001BE2D
		protected BaseVerticalCollectionView view
		{
			get
			{
				return this.m_View;
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001DC35 File Offset: 0x0001BE35
		public void SetView(BaseVerticalCollectionView view)
		{
			this.m_View = view;
			Assert.IsNotNull<BaseVerticalCollectionView>(this.m_View, "View must not be null.");
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001DC50 File Offset: 0x0001BE50
		public virtual int GetItemCount()
		{
			IList itemsSource = this.m_ItemsSource;
			return (itemsSource != null) ? itemsSource.Count : 0;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001DC74 File Offset: 0x0001BE74
		public virtual int GetIndexForId(int id)
		{
			return id;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001DC88 File Offset: 0x0001BE88
		public virtual int GetIdForIndex(int index)
		{
			Func<int, int> getItemId = this.m_View.getItemId;
			return (getItemId != null) ? getItemId.Invoke(index) : index;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001DCB4 File Offset: 0x0001BEB4
		public virtual object GetItemForIndex(int index)
		{
			bool flag = index < 0 || index >= this.m_ItemsSource.Count;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				obj = this.m_ItemsSource[index];
			}
			return obj;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001DCF2 File Offset: 0x0001BEF2
		internal virtual void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			reusableItem.Init(this.MakeItem());
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001DD02 File Offset: 0x0001BF02
		internal virtual void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			this.BindItem(reusableItem.bindableElement, index);
			reusableItem.SetSelected(Enumerable.Contains<int>(this.m_View.selectedIndices, index));
			reusableItem.rootElement.pseudoStates &= ~PseudoStates.Hover;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001DD40 File Offset: 0x0001BF40
		internal virtual void InvokeUnbindItem(ReusableCollectionItem reusableItem, int index)
		{
			this.UnbindItem(reusableItem.bindableElement, index);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001DD51 File Offset: 0x0001BF51
		internal virtual void InvokeDestroyItem(ReusableCollectionItem reusableItem)
		{
			this.DestroyItem(reusableItem.bindableElement);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001DD64 File Offset: 0x0001BF64
		public virtual VisualElement MakeItem()
		{
			bool flag = this.m_View.makeItem == null;
			VisualElement visualElement;
			if (flag)
			{
				bool flag2 = this.m_View.bindItem != null;
				if (flag2)
				{
					throw new NotImplementedException("You must specify makeItem if bindItem is specified.");
				}
				visualElement = new Label();
			}
			else
			{
				visualElement = this.m_View.makeItem.Invoke();
			}
			return visualElement;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001DDC0 File Offset: 0x0001BFC0
		public virtual void BindItem(VisualElement element, int index)
		{
			bool flag = this.m_View.bindItem == null;
			if (flag)
			{
				bool flag2 = this.m_View.makeItem != null;
				if (flag2)
				{
					throw new NotImplementedException("You must specify bindItem if makeItem is specified.");
				}
				Label label = (Label)element;
				object obj = this.m_ItemsSource[index];
				label.text = ((obj != null) ? obj.ToString() : null) ?? "null";
			}
			else
			{
				this.m_View.bindItem.Invoke(element, index);
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001DE43 File Offset: 0x0001C043
		public virtual void UnbindItem(VisualElement element, int index)
		{
			Action<VisualElement, int> unbindItem = this.m_View.unbindItem;
			if (unbindItem != null)
			{
				unbindItem.Invoke(element, index);
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001DE5F File Offset: 0x0001C05F
		public virtual void DestroyItem(VisualElement element)
		{
			Action<VisualElement> destroyItem = this.m_View.destroyItem;
			if (destroyItem != null)
			{
				destroyItem.Invoke(element);
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001DE7A File Offset: 0x0001C07A
		protected void RaiseItemsSourceChanged()
		{
			Action action = this.itemsSourceChanged;
			if (action != null)
			{
				action.Invoke();
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001DE8F File Offset: 0x0001C08F
		protected void RaiseItemIndexChanged(int srcIndex, int dstIndex)
		{
			Action<int, int> action = this.itemIndexChanged;
			if (action != null)
			{
				action.Invoke(srcIndex, dstIndex);
			}
		}

		// Token: 0x0400035D RID: 861
		private BaseVerticalCollectionView m_View;

		// Token: 0x0400035E RID: 862
		private IList m_ItemsSource;
	}
}
