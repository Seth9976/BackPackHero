using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000078 RID: 120
	public sealed class MergedGraphElementCollection : MergedKeyedCollection<Guid, IGraphElement>, INotifyCollectionChanged<IGraphElement>
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060003A4 RID: 932 RVA: 0x0000904C File Offset: 0x0000724C
		// (remove) Token: 0x060003A5 RID: 933 RVA: 0x00009084 File Offset: 0x00007284
		public event Action<IGraphElement> ItemAdded;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060003A6 RID: 934 RVA: 0x000090BC File Offset: 0x000072BC
		// (remove) Token: 0x060003A7 RID: 935 RVA: 0x000090F4 File Offset: 0x000072F4
		public event Action<IGraphElement> ItemRemoved;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060003A8 RID: 936 RVA: 0x0000912C File Offset: 0x0000732C
		// (remove) Token: 0x060003A9 RID: 937 RVA: 0x00009164 File Offset: 0x00007364
		public event Action CollectionChanged;

		// Token: 0x060003AA RID: 938 RVA: 0x0000919C File Offset: 0x0000739C
		public override void Include<TSubItem>(IKeyedCollection<Guid, TSubItem> collection)
		{
			base.Include<TSubItem>(collection);
			IGraphElementCollection<TSubItem> graphElementCollection = collection as IGraphElementCollection<TSubItem>;
			if (graphElementCollection != null)
			{
				graphElementCollection.ItemAdded += delegate(TSubItem element)
				{
					Action<IGraphElement> itemAdded = this.ItemAdded;
					if (itemAdded == null)
					{
						return;
					}
					itemAdded(element);
				};
				graphElementCollection.ItemRemoved += delegate(TSubItem element)
				{
					Action<IGraphElement> itemRemoved = this.ItemRemoved;
					if (itemRemoved == null)
					{
						return;
					}
					itemRemoved(element);
				};
				graphElementCollection.CollectionChanged += delegate
				{
					Action collectionChanged = this.CollectionChanged;
					if (collectionChanged == null)
					{
						return;
					}
					collectionChanged();
				};
			}
		}
	}
}
