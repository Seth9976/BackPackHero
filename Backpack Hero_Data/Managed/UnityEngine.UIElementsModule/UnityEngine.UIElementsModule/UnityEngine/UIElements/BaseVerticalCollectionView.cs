using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000120 RID: 288
	public abstract class BaseVerticalCollectionView : BindableElement, ISerializationCallbackReceiver
	{
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000952 RID: 2386 RVA: 0x0002481C File Offset: 0x00022A1C
		// (remove) Token: 0x06000953 RID: 2387 RVA: 0x00024854 File Offset: 0x00022A54
		[Obsolete("onItemChosen is deprecated, use onItemsChosen instead", true)]
		[field: DebuggerBrowsable(0)]
		public event Action<object> onItemChosen;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000954 RID: 2388 RVA: 0x0002488C File Offset: 0x00022A8C
		// (remove) Token: 0x06000955 RID: 2389 RVA: 0x000248C4 File Offset: 0x00022AC4
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<object>> onItemsChosen;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000956 RID: 2390 RVA: 0x000248FC File Offset: 0x00022AFC
		// (remove) Token: 0x06000957 RID: 2391 RVA: 0x00024934 File Offset: 0x00022B34
		[Obsolete("onSelectionChanged is deprecated, use onSelectionChange instead", true)]
		[field: DebuggerBrowsable(0)]
		public event Action<List<object>> onSelectionChanged;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000958 RID: 2392 RVA: 0x0002496C File Offset: 0x00022B6C
		// (remove) Token: 0x06000959 RID: 2393 RVA: 0x000249A4 File Offset: 0x00022BA4
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<object>> onSelectionChange;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600095A RID: 2394 RVA: 0x000249DC File Offset: 0x00022BDC
		// (remove) Token: 0x0600095B RID: 2395 RVA: 0x00024A14 File Offset: 0x00022C14
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<int>> onSelectedIndicesChange;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600095C RID: 2396 RVA: 0x00024A4C File Offset: 0x00022C4C
		// (remove) Token: 0x0600095D RID: 2397 RVA: 0x00024A84 File Offset: 0x00022C84
		[field: DebuggerBrowsable(0)]
		public event Action<int, int> itemIndexChanged;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600095E RID: 2398 RVA: 0x00024ABC File Offset: 0x00022CBC
		// (remove) Token: 0x0600095F RID: 2399 RVA: 0x00024AF4 File Offset: 0x00022CF4
		[field: DebuggerBrowsable(0)]
		public event Action itemsSourceChanged;

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00024B29 File Offset: 0x00022D29
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x00024B31 File Offset: 0x00022D31
		internal Func<int, int> getItemId
		{
			get
			{
				return this.m_GetItemId;
			}
			set
			{
				this.m_GetItemId = value;
				this.RefreshItems();
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00024B42 File Offset: 0x00022D42
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00024B56 File Offset: 0x00022D56
		public IList itemsSource
		{
			get
			{
				CollectionViewController viewController = this.viewController;
				return (viewController != null) ? viewController.itemsSource : null;
			}
			set
			{
				this.GetOrCreateViewController().itemsSource = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00004D72 File Offset: 0x00002F72
		internal virtual bool sourceIncludesArraySize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00024B65 File Offset: 0x00022D65
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x00024B6D File Offset: 0x00022D6D
		public Func<VisualElement> makeItem
		{
			get
			{
				return this.m_MakeItem;
			}
			set
			{
				this.m_MakeItem = value;
				this.Rebuild();
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00024B7E File Offset: 0x00022D7E
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x00024B86 File Offset: 0x00022D86
		public Action<VisualElement, int> bindItem
		{
			get
			{
				return this.m_BindItem;
			}
			set
			{
				this.m_BindItem = value;
				this.RefreshItems();
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00024B97 File Offset: 0x00022D97
		internal void SetMakeItemWithoutNotify(Func<VisualElement> func)
		{
			this.m_MakeItem = func;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00024BA1 File Offset: 0x00022DA1
		internal void SetBindItemWithoutNotify(Action<VisualElement, int> callback)
		{
			this.m_BindItem = callback;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00024BAB File Offset: 0x00022DAB
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00024BB3 File Offset: 0x00022DB3
		public Action<VisualElement, int> unbindItem { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00024BBC File Offset: 0x00022DBC
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x00024BC4 File Offset: 0x00022DC4
		public Action<VisualElement> destroyItem { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00024BCD File Offset: 0x00022DCD
		public override VisualElement contentContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00024BD0 File Offset: 0x00022DD0
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x00024BE8 File Offset: 0x00022DE8
		public SelectionType selectionType
		{
			get
			{
				return this.m_SelectionType;
			}
			set
			{
				this.m_SelectionType = value;
				bool flag = this.m_SelectionType == SelectionType.None;
				if (flag)
				{
					this.ClearSelection();
				}
				else
				{
					bool flag2 = this.m_SelectionType == SelectionType.Single;
					if (flag2)
					{
						bool flag3 = this.m_SelectedIndices.Count > 1;
						if (flag3)
						{
							this.SetSelection(Enumerable.First<int>(this.m_SelectedIndices));
						}
					}
				}
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00024C4A File Offset: 0x00022E4A
		public object selectedItem
		{
			get
			{
				return (this.m_SelectedItems.Count == 0) ? null : Enumerable.First<object>(this.m_SelectedItems);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00024C67 File Offset: 0x00022E67
		public IEnumerable<object> selectedItems
		{
			get
			{
				return this.m_SelectedItems;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00024C70 File Offset: 0x00022E70
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x00024C9D File Offset: 0x00022E9D
		public int selectedIndex
		{
			get
			{
				return (this.m_SelectedIndices.Count == 0) ? (-1) : Enumerable.First<int>(this.m_SelectedIndices);
			}
			set
			{
				this.SetSelection(value);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00024CA8 File Offset: 0x00022EA8
		public IEnumerable<int> selectedIndices
		{
			get
			{
				return this.m_SelectedIndices;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00024CB0 File Offset: 0x00022EB0
		internal List<int> currentSelectionIds
		{
			get
			{
				return this.m_SelectedIds;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00024CB8 File Offset: 0x00022EB8
		internal IEnumerable<ReusableCollectionItem> activeItems
		{
			get
			{
				CollectionVirtualizationController virtualizationController = this.m_VirtualizationController;
				return ((virtualizationController != null) ? virtualizationController.activeItems : null) ?? BaseVerticalCollectionView.k_EmptyItems;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00024CD5 File Offset: 0x00022ED5
		internal ScrollView scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00024CDD File Offset: 0x00022EDD
		internal ListViewDragger dragger
		{
			get
			{
				return this.m_Dragger;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00024CE5 File Offset: 0x00022EE5
		internal CollectionViewController viewController
		{
			get
			{
				return this.m_ViewController;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00024CED File Offset: 0x00022EED
		internal CollectionVirtualizationController virtualizationController
		{
			get
			{
				return this.GetOrCreateVirtualizationController();
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00024CF5 File Offset: 0x00022EF5
		[Obsolete("resolvedItemHeight is deprecated and will be removed from the API.", false)]
		public float resolvedItemHeight
		{
			get
			{
				return this.ResolveItemHeight(-1f);
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00024D04 File Offset: 0x00022F04
		internal float ResolveItemHeight(float height = -1f)
		{
			float scaledPixelsPerPoint = base.scaledPixelsPerPoint;
			height = ((height < 0f) ? this.fixedItemHeight : height);
			return Mathf.Round(height * scaledPixelsPerPoint) / scaledPixelsPerPoint;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00024D3A File Offset: 0x00022F3A
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x00024D4C File Offset: 0x00022F4C
		public bool showBorder
		{
			get
			{
				return this.m_ScrollView.ClassListContains(BaseVerticalCollectionView.borderUssClassName);
			}
			set
			{
				this.m_ScrollView.EnableInClassList(BaseVerticalCollectionView.borderUssClassName, value);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00024D60 File Offset: 0x00022F60
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x00024DA8 File Offset: 0x00022FA8
		public bool reorderable
		{
			get
			{
				ListViewDragger dragger = this.m_Dragger;
				bool? flag;
				if (dragger == null)
				{
					flag = default(bool?);
				}
				else
				{
					ICollectionDragAndDropController dragAndDropController = dragger.dragAndDropController;
					flag = ((dragAndDropController != null) ? new bool?(dragAndDropController.enableReordering) : default(bool?));
				}
				bool? flag2 = flag;
				return flag2.GetValueOrDefault();
			}
			set
			{
				ListViewDragger dragger = this.m_Dragger;
				bool flag = ((dragger != null) ? dragger.dragAndDropController : null) == null;
				if (flag)
				{
					if (value)
					{
						this.InitializeDragAndDropController();
					}
				}
				else
				{
					ICollectionDragAndDropController dragAndDropController = this.m_Dragger.dragAndDropController;
					bool flag2 = dragAndDropController != null && dragAndDropController.enableReordering != value;
					if (flag2)
					{
						dragAndDropController.enableReordering = value;
						this.Rebuild();
					}
				}
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00024E14 File Offset: 0x00023014
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x00024E2C File Offset: 0x0002302C
		public bool horizontalScrollingEnabled
		{
			get
			{
				return this.m_HorizontalScrollingEnabled;
			}
			set
			{
				bool flag = this.m_HorizontalScrollingEnabled == value;
				if (!flag)
				{
					this.m_HorizontalScrollingEnabled = value;
					this.m_ScrollView.mode = (value ? ScrollViewMode.VerticalAndHorizontal : ScrollViewMode.Vertical);
				}
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00024E64 File Offset: 0x00023064
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00024E7C File Offset: 0x0002307C
		public AlternatingRowBackground showAlternatingRowBackgrounds
		{
			get
			{
				return this.m_ShowAlternatingRowBackgrounds;
			}
			set
			{
				bool flag = this.m_ShowAlternatingRowBackgrounds == value;
				if (!flag)
				{
					this.m_ShowAlternatingRowBackgrounds = value;
					this.RefreshItems();
				}
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00024EA7 File Offset: 0x000230A7
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00024EB0 File Offset: 0x000230B0
		public CollectionVirtualizationMethod virtualizationMethod
		{
			get
			{
				return this.m_VirtualizationMethod;
			}
			set
			{
				CollectionVirtualizationMethod virtualizationMethod = this.m_VirtualizationMethod;
				this.m_VirtualizationMethod = value;
				bool flag = virtualizationMethod != value;
				if (flag)
				{
					this.CreateVirtualizationController();
					this.Rebuild();
				}
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00024EE7 File Offset: 0x000230E7
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x00024EF0 File Offset: 0x000230F0
		[Obsolete("itemHeight is deprecated, use fixedItemHeight instead.", false)]
		public int itemHeight
		{
			get
			{
				return (int)this.fixedItemHeight;
			}
			set
			{
				this.fixedItemHeight = (float)value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00024EFB File Offset: 0x000230FB
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00024F04 File Offset: 0x00023104
		public float fixedItemHeight
		{
			get
			{
				return this.m_FixedItemHeight;
			}
			set
			{
				bool flag = value < 0f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("fixedItemHeight", "Value needs to be positive for virtualization.");
				}
				this.m_ItemHeightIsInline = true;
				bool flag2 = Math.Abs(this.m_FixedItemHeight - value) > float.Epsilon;
				if (flag2)
				{
					this.m_FixedItemHeight = value;
					this.RefreshItems();
				}
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00024F5D File Offset: 0x0002315D
		internal float lastHeight
		{
			get
			{
				return this.m_LastHeight;
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00024F65 File Offset: 0x00023165
		private protected virtual void CreateVirtualizationController()
		{
			this.CreateVirtualizationController<ReusableCollectionItem>();
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00024F70 File Offset: 0x00023170
		internal CollectionVirtualizationController GetOrCreateVirtualizationController()
		{
			bool flag = this.m_VirtualizationController == null;
			if (flag)
			{
				this.CreateVirtualizationController();
			}
			return this.m_VirtualizationController;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00024F9C File Offset: 0x0002319C
		internal void CreateVirtualizationController<T>() where T : ReusableCollectionItem, new()
		{
			CollectionVirtualizationMethod virtualizationMethod = this.virtualizationMethod;
			CollectionVirtualizationMethod collectionVirtualizationMethod = virtualizationMethod;
			if (collectionVirtualizationMethod != CollectionVirtualizationMethod.FixedHeight)
			{
				if (collectionVirtualizationMethod != CollectionVirtualizationMethod.DynamicHeight)
				{
					throw new ArgumentOutOfRangeException("virtualizationMethod", this.virtualizationMethod, "Unsupported virtualizationMethod virtualization");
				}
				this.m_VirtualizationController = new DynamicHeightVirtualizationController<T>(this);
			}
			else
			{
				this.m_VirtualizationController = new FixedHeightVirtualizationController<T>(this);
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00024FF8 File Offset: 0x000231F8
		internal CollectionViewController GetOrCreateViewController()
		{
			bool flag = this.m_ViewController == null;
			if (flag)
			{
				this.CreateViewController();
			}
			return this.m_ViewController;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00025026 File Offset: 0x00023226
		private protected virtual void CreateViewController()
		{
			this.SetViewController(new CollectionViewController());
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00025038 File Offset: 0x00023238
		internal void SetViewController(CollectionViewController controller)
		{
			bool flag = this.m_ViewController != null;
			if (flag)
			{
				this.m_ViewController.itemIndexChanged -= this.m_ItemIndexChangedCallback;
				this.m_ViewController.itemsSourceChanged -= this.m_ItemsSourceChangedCallback;
			}
			this.m_ViewController = controller;
			bool flag2 = this.m_ViewController != null;
			if (flag2)
			{
				this.m_ViewController.SetView(this);
				this.m_ViewController.itemIndexChanged += this.m_ItemIndexChangedCallback;
				this.m_ViewController.itemsSourceChanged += this.m_ItemsSourceChangedCallback;
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000250C0 File Offset: 0x000232C0
		internal virtual ListViewDragger CreateDragger()
		{
			return new ListViewDragger(this);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000250D8 File Offset: 0x000232D8
		internal void InitializeDragAndDropController()
		{
			bool flag = this.m_Dragger != null;
			if (flag)
			{
				this.m_Dragger.UnregisterCallbacksFromTarget(true);
				this.m_Dragger.dragAndDropController = null;
				this.m_Dragger = null;
			}
			this.m_Dragger = this.CreateDragger();
			this.m_Dragger.dragAndDropController = this.CreateDragAndDropController();
		}

		// Token: 0x06000996 RID: 2454
		internal abstract ICollectionDragAndDropController CreateDragAndDropController();

		// Token: 0x06000997 RID: 2455 RVA: 0x00025134 File Offset: 0x00023334
		internal void SetDragAndDropController(ICollectionDragAndDropController dragAndDropController)
		{
			if (this.m_Dragger == null)
			{
				this.m_Dragger = this.CreateDragger();
			}
			this.m_Dragger.dragAndDropController = dragAndDropController;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00025158 File Offset: 0x00023358
		internal ICollectionDragAndDropController GetDragAndDropController()
		{
			ListViewDragger dragger = this.m_Dragger;
			return (dragger != null) ? dragger.dragAndDropController : null;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0002517C File Offset: 0x0002337C
		public BaseVerticalCollectionView()
		{
			base.AddToClassList(BaseVerticalCollectionView.ussClassName);
			this.selectionType = SelectionType.Single;
			this.m_ScrollOffset = Vector2.zero;
			this.m_ScrollView = new ScrollView();
			this.m_ScrollView.viewDataKey = "list-view__scroll-view";
			this.m_ScrollView.AddToClassList(BaseVerticalCollectionView.listScrollViewUssClassName);
			this.m_ScrollView.verticalScroller.valueChanged += delegate(float v)
			{
				this.OnScroll(new Vector2(0f, v));
			};
			base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnSizeChanged), TrickleDown.NoTrickleDown);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
			this.m_ScrollView.contentContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_ScrollView.contentContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			base.hierarchy.Add(this.m_ScrollView);
			this.m_ScrollView.contentContainer.focusable = true;
			this.m_ScrollView.contentContainer.usageHints &= ~UsageHints.GroupTransform;
			base.focusable = true;
			base.isCompositeRoot = true;
			base.delegatesFocus = true;
			this.m_ItemIndexChangedCallback = new Action<int, int>(this.OnItemIndexChanged);
			this.m_ItemsSourceChangedCallback = new Action(this.OnItemsSourceChanged);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00025310 File Offset: 0x00023510
		public BaseVerticalCollectionView(IList itemsSource, float itemHeight = -1f, Func<VisualElement> makeItem = null, Action<VisualElement, int> bindItem = null)
			: this()
		{
			bool flag = Math.Abs(itemHeight - -1f) > float.Epsilon;
			if (flag)
			{
				this.m_FixedItemHeight = itemHeight;
				this.m_ItemHeightIsInline = true;
			}
			this.itemsSource = itemsSource;
			this.makeItem = makeItem;
			this.bindItem = bindItem;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00025368 File Offset: 0x00023568
		public VisualElement GetRootElementForId(int id)
		{
			ReusableCollectionItem reusableCollectionItem = Enumerable.FirstOrDefault<ReusableCollectionItem>(this.activeItems, (ReusableCollectionItem t) => t.id == id);
			return (reusableCollectionItem != null) ? reusableCollectionItem.GetRootElement() : null;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000253AC File Offset: 0x000235AC
		public VisualElement GetRootElementForIndex(int index)
		{
			return this.GetRootElementForId(this.viewController.GetIdForIndex(index));
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000253D0 File Offset: 0x000235D0
		internal bool HasValidDataAndBindings()
		{
			return this.m_ViewController != null && this.itemsSource != null && this.makeItem != null == (this.bindItem != null);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002540C File Offset: 0x0002360C
		private void OnItemIndexChanged(int srcIndex, int dstIndex)
		{
			Action<int, int> action = this.itemIndexChanged;
			if (action != null)
			{
				action.Invoke(srcIndex, dstIndex);
			}
			bool flag = !(base.binding is IInternalListViewBinding);
			if (flag)
			{
				this.RefreshItems();
			}
			else
			{
				base.schedule.Execute(new Action(this.RefreshItems)).ExecuteLater(100L);
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002546C File Offset: 0x0002366C
		private void OnItemsSourceChanged()
		{
			Action action = this.itemsSourceChanged;
			if (action != null)
			{
				action.Invoke();
			}
			bool flag = !(base.binding is IInternalListViewBinding);
			if (flag)
			{
				this.RefreshItems();
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000254A8 File Offset: 0x000236A8
		public void RefreshItem(int index)
		{
			foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
			{
				bool flag = reusableCollectionItem.index == index;
				if (flag)
				{
					this.viewController.InvokeBindItem(reusableCollectionItem, reusableCollectionItem.index);
					break;
				}
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00025518 File Offset: 0x00023718
		public void RefreshItems()
		{
			bool flag = this.m_ViewController == null;
			if (!flag)
			{
				this.RefreshSelection();
				this.virtualizationController.Refresh(false);
				this.PostRefresh();
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00025550 File Offset: 0x00023750
		[Obsolete("Refresh() has been deprecated. Use Rebuild() instead. (UnityUpgradable) -> Rebuild()", false)]
		public void Refresh()
		{
			this.Rebuild();
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002555C File Offset: 0x0002375C
		public void Rebuild()
		{
			bool flag = this.m_ViewController == null;
			if (!flag)
			{
				this.RefreshSelection();
				this.virtualizationController.Refresh(true);
				this.PostRefresh();
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00025594 File Offset: 0x00023794
		private void RefreshSelection()
		{
			this.m_SelectedIndices.Clear();
			this.m_SelectedItems.Clear();
			CollectionViewController viewController = this.viewController;
			bool flag = ((viewController != null) ? viewController.itemsSource : null) == null;
			if (!flag)
			{
				bool flag2 = this.m_SelectedIds.Count > 0;
				if (flag2)
				{
					int count = this.viewController.itemsSource.Count;
					for (int i = 0; i < count; i++)
					{
						bool flag3 = !this.m_SelectedIds.Contains(this.viewController.GetIdForIndex(i));
						if (!flag3)
						{
							this.m_SelectedIndices.Add(i);
							this.m_SelectedItems.Add(this.viewController.GetItemForIndex(i));
						}
					}
				}
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00025658 File Offset: 0x00023858
		private protected virtual void PostRefresh()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.m_LastHeight = this.m_ScrollView.layout.height;
				bool flag2 = float.IsNaN(this.m_ScrollView.layout.height);
				if (!flag2)
				{
					this.Resize(this.m_ScrollView.layout.size, -1);
				}
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000256C7 File Offset: 0x000238C7
		public void ScrollTo(VisualElement visualElement)
		{
			this.m_ScrollView.ScrollTo(visualElement);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000256D8 File Offset: 0x000238D8
		public void ScrollToItem(int index)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.virtualizationController.ScrollToItem(index);
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00025704 File Offset: 0x00023904
		public void ScrollToId(int id)
		{
			int indexForId = this.viewController.GetIndexForId(id);
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.virtualizationController.ScrollToItem(indexForId);
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002573C File Offset: 0x0002393C
		private void OnScroll(Vector2 offset)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.virtualizationController.OnScroll(offset);
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00025766 File Offset: 0x00023966
		private void Resize(Vector2 size, int layoutPass = -1)
		{
			this.virtualizationController.Resize(size, layoutPass);
			this.m_LastHeight = size.y;
			this.virtualizationController.UpdateBackground();
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00025790 File Offset: 0x00023990
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.m_ScrollView.contentContainer.AddManipulator(this.m_NavigationManipulator = new KeyboardNavigationManipulator(new Action<KeyboardNavigationOperation, EventBase>(this.Apply)));
				this.m_ScrollView.contentContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00025854 File Offset: 0x00023A54
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				this.m_ScrollView.contentContainer.RemoveManipulator(this.m_NavigationManipulator);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00025903 File Offset: 0x00023B03
		[Obsolete("OnKeyDown is obsolete and will be removed from ListView. Use the event system instead, i.e. SendEvent(EventBase e).", false)]
		public void OnKeyDown(KeyDownEvent evt)
		{
			this.m_NavigationManipulator.OnKeyDown(evt);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00025914 File Offset: 0x00023B14
		private bool Apply(KeyboardNavigationOperation op, bool shiftKey)
		{
			BaseVerticalCollectionView.<>c__DisplayClass164_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.shiftKey = shiftKey;
			bool flag = this.selectionType == SelectionType.None || !this.HasValidDataAndBindings();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				switch (op)
				{
				case KeyboardNavigationOperation.SelectAll:
					this.SelectAll();
					return true;
				case KeyboardNavigationOperation.Cancel:
					this.ClearSelection();
					return true;
				case KeyboardNavigationOperation.Submit:
				{
					Action<IEnumerable<object>> action = this.onItemsChosen;
					if (action != null)
					{
						action.Invoke(this.m_SelectedItems);
					}
					this.ScrollToItem(this.selectedIndex);
					return true;
				}
				case KeyboardNavigationOperation.Previous:
				{
					bool flag3 = this.selectedIndex > 0;
					if (flag3)
					{
						this.<Apply>g__HandleSelectionAndScroll|164_0(this.selectedIndex - 1, ref CS$<>8__locals1);
						return true;
					}
					break;
				}
				case KeyboardNavigationOperation.Next:
				{
					bool flag4 = this.selectedIndex + 1 < this.m_ViewController.itemsSource.Count;
					if (flag4)
					{
						this.<Apply>g__HandleSelectionAndScroll|164_0(this.selectedIndex + 1, ref CS$<>8__locals1);
						return true;
					}
					break;
				}
				case KeyboardNavigationOperation.PageUp:
				{
					bool flag5 = this.m_SelectedIndices.Count > 0;
					if (flag5)
					{
						int num = (this.m_IsRangeSelectionDirectionUp ? Enumerable.Min(this.m_SelectedIndices) : Enumerable.Max(this.m_SelectedIndices));
						this.<Apply>g__HandleSelectionAndScroll|164_0(Mathf.Max(0, num - (this.virtualizationController.visibleItemCount - 1)), ref CS$<>8__locals1);
					}
					return true;
				}
				case KeyboardNavigationOperation.PageDown:
				{
					bool flag6 = this.m_SelectedIndices.Count > 0;
					if (flag6)
					{
						int num2 = (this.m_IsRangeSelectionDirectionUp ? Enumerable.Min(this.m_SelectedIndices) : Enumerable.Max(this.m_SelectedIndices));
						this.<Apply>g__HandleSelectionAndScroll|164_0(Mathf.Min(this.viewController.itemsSource.Count - 1, num2 + (this.virtualizationController.visibleItemCount - 1)), ref CS$<>8__locals1);
					}
					return true;
				}
				case KeyboardNavigationOperation.Begin:
					this.<Apply>g__HandleSelectionAndScroll|164_0(0, ref CS$<>8__locals1);
					return true;
				case KeyboardNavigationOperation.End:
					this.<Apply>g__HandleSelectionAndScroll|164_0(this.m_ViewController.itemsSource.Count - 1, ref CS$<>8__locals1);
					return true;
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00025B3C File Offset: 0x00023D3C
		private void Apply(KeyboardNavigationOperation op, EventBase sourceEvent)
		{
			KeyDownEvent keyDownEvent = sourceEvent as KeyDownEvent;
			bool flag = keyDownEvent != null && keyDownEvent.shiftKey;
			bool flag2 = this.Apply(op, flag);
			if (flag2)
			{
				sourceEvent.StopPropagation();
				sourceEvent.PreventDefault();
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00025B7C File Offset: 0x00023D7C
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = (evt.pressedButtons & 1) == 0;
				if (flag2)
				{
					this.ProcessPointerUp(evt);
				}
				else
				{
					this.ProcessPointerDown(evt);
				}
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00025BC0 File Offset: 0x00023DC0
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = evt.pointerType != PointerType.mouse;
			if (flag)
			{
				this.ProcessPointerDown(evt);
				base.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			else
			{
				this.ProcessPointerDown(evt);
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00025C0C File Offset: 0x00023E0C
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					this.ClearSelection();
				}
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00025C40 File Offset: 0x00023E40
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = evt.pointerType != PointerType.mouse;
			if (flag)
			{
				this.ProcessPointerUp(evt);
				base.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			else
			{
				this.ProcessPointerUp(evt);
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00025C8C File Offset: 0x00023E8C
		private void ProcessPointerDown(IPointerEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					bool flag3 = evt.button != 0;
					if (!flag3)
					{
						bool flag4 = evt.pointerType != PointerType.mouse;
						if (flag4)
						{
							this.m_TouchDownPosition = evt.position;
						}
						else
						{
							this.DoSelect(evt.localPosition, evt.clickCount, evt.actionKey, evt.shiftKey);
						}
					}
				}
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00025D10 File Offset: 0x00023F10
		private void ProcessPointerUp(IPointerEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					bool flag3 = evt.button != 0;
					if (!flag3)
					{
						bool flag4 = evt.pointerType != PointerType.mouse;
						if (flag4)
						{
							bool flag5 = (evt.position - this.m_TouchDownPosition).sqrMagnitude <= 100f;
							if (flag5)
							{
								this.DoSelect(evt.localPosition, evt.clickCount, evt.actionKey, evt.shiftKey);
							}
						}
						else
						{
							int indexFromPosition = this.virtualizationController.GetIndexFromPosition(evt.localPosition);
							bool flag6 = this.selectionType == SelectionType.Multiple && !evt.shiftKey && !evt.actionKey && this.m_SelectedIndices.Count > 1 && this.m_SelectedIndices.Contains(indexFromPosition);
							if (flag6)
							{
								this.ProcessSingleClick(indexFromPosition);
							}
						}
					}
				}
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00025E20 File Offset: 0x00024020
		private void DoSelect(Vector2 localPosition, int clickCount, bool actionKey, bool shiftKey)
		{
			int indexFromPosition = this.virtualizationController.GetIndexFromPosition(localPosition);
			bool flag = indexFromPosition > this.viewController.itemsSource.Count - 1;
			if (!flag)
			{
				bool flag2 = this.selectionType == SelectionType.None;
				if (!flag2)
				{
					int idForIndex = this.viewController.GetIdForIndex(indexFromPosition);
					if (clickCount != 1)
					{
						if (clickCount == 2)
						{
							bool flag3 = this.onItemsChosen != null;
							if (flag3)
							{
								this.ProcessSingleClick(indexFromPosition);
							}
							Action<IEnumerable<object>> action = this.onItemsChosen;
							if (action != null)
							{
								action.Invoke(this.m_SelectedItems);
							}
						}
					}
					else
					{
						bool flag4 = this.selectionType == SelectionType.Multiple && actionKey;
						if (flag4)
						{
							bool flag5 = this.m_SelectedIds.Contains(idForIndex);
							if (flag5)
							{
								this.RemoveFromSelection(indexFromPosition);
							}
							else
							{
								this.AddToSelection(indexFromPosition);
							}
						}
						else
						{
							bool flag6 = this.selectionType == SelectionType.Multiple && shiftKey;
							if (flag6)
							{
								bool flag7 = this.m_SelectedIndices.Count == 0;
								if (flag7)
								{
									this.SetSelection(indexFromPosition);
								}
								else
								{
									this.DoRangeSelection(indexFromPosition);
								}
							}
							else
							{
								bool flag8 = this.selectionType == SelectionType.Multiple && this.m_SelectedIndices.Contains(indexFromPosition);
								if (!flag8)
								{
									this.SetSelection(indexFromPosition);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00025F70 File Offset: 0x00024170
		private void DoRangeSelection(int rangeSelectionFinalIndex)
		{
			int num = (this.m_IsRangeSelectionDirectionUp ? Enumerable.Max(this.m_SelectedIndices) : Enumerable.Min(this.m_SelectedIndices));
			this.ClearSelectionWithoutValidation();
			List<int> list = new List<int>();
			this.m_IsRangeSelectionDirectionUp = rangeSelectionFinalIndex < num;
			bool isRangeSelectionDirectionUp = this.m_IsRangeSelectionDirectionUp;
			if (isRangeSelectionDirectionUp)
			{
				for (int i = rangeSelectionFinalIndex; i <= num; i++)
				{
					list.Add(i);
				}
			}
			else
			{
				for (int j = rangeSelectionFinalIndex; j >= num; j--)
				{
					list.Add(j);
				}
			}
			this.AddToSelection(list);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00024C9D File Offset: 0x00022E9D
		private void ProcessSingleClick(int clickedIndex)
		{
			this.SetSelection(clickedIndex);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00026010 File Offset: 0x00024210
		internal void SelectAll()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = this.selectionType != SelectionType.Multiple;
				if (!flag2)
				{
					for (int i = 0; i < this.m_ViewController.itemsSource.Count; i++)
					{
						int idForIndex = this.viewController.GetIdForIndex(i);
						object itemForIndex = this.viewController.GetItemForIndex(i);
						foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
						{
							bool flag3 = reusableCollectionItem.id == idForIndex;
							if (flag3)
							{
								reusableCollectionItem.SetSelected(true);
							}
						}
						bool flag4 = !this.m_SelectedIds.Contains(idForIndex);
						if (flag4)
						{
							this.m_SelectedIds.Add(idForIndex);
							this.m_SelectedIndices.Add(i);
							this.m_SelectedItems.Add(itemForIndex);
						}
					}
					this.NotifyOfSelectionChange();
					base.SaveViewData();
				}
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00026134 File Offset: 0x00024334
		public void AddToSelection(int index)
		{
			this.AddToSelection(new int[] { index });
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00026148 File Offset: 0x00024348
		internal void AddToSelection(IList<int> indexes)
		{
			bool flag = !this.HasValidDataAndBindings() || indexes == null || indexes.Count == 0;
			if (!flag)
			{
				foreach (int num in indexes)
				{
					this.AddToSelectionWithoutValidation(num);
				}
				this.NotifyOfSelectionChange();
				base.SaveViewData();
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000261C4 File Offset: 0x000243C4
		private void AddToSelectionWithoutValidation(int index)
		{
			bool flag = this.m_SelectedIndices.Contains(index);
			if (!flag)
			{
				int idForIndex = this.viewController.GetIdForIndex(index);
				object itemForIndex = this.viewController.GetItemForIndex(index);
				foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
				{
					bool flag2 = reusableCollectionItem.id == idForIndex;
					if (flag2)
					{
						reusableCollectionItem.SetSelected(true);
					}
				}
				this.m_SelectedIds.Add(idForIndex);
				this.m_SelectedIndices.Add(index);
				this.m_SelectedItems.Add(itemForIndex);
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00026280 File Offset: 0x00024480
		public void RemoveFromSelection(int index)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.RemoveFromSelectionWithoutValidation(index);
				this.NotifyOfSelectionChange();
				base.SaveViewData();
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000262B4 File Offset: 0x000244B4
		private void RemoveFromSelectionWithoutValidation(int index)
		{
			bool flag = !this.m_SelectedIndices.Contains(index);
			if (!flag)
			{
				int idForIndex = this.viewController.GetIdForIndex(index);
				object itemForIndex = this.viewController.GetItemForIndex(index);
				foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
				{
					bool flag2 = reusableCollectionItem.id == idForIndex;
					if (flag2)
					{
						reusableCollectionItem.SetSelected(false);
					}
				}
				this.m_SelectedIds.Remove(idForIndex);
				this.m_SelectedIndices.Remove(index);
				this.m_SelectedItems.Remove(itemForIndex);
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00026370 File Offset: 0x00024570
		public void SetSelection(int index)
		{
			bool flag = index < 0;
			if (flag)
			{
				this.ClearSelection();
			}
			else
			{
				this.SetSelection(new int[] { index });
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000263A1 File Offset: 0x000245A1
		public void SetSelection(IEnumerable<int> indices)
		{
			this.SetSelectionInternal(indices, true);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000263AD File Offset: 0x000245AD
		public void SetSelectionWithoutNotify(IEnumerable<int> indices)
		{
			this.SetSelectionInternal(indices, false);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000263BC File Offset: 0x000245BC
		internal void SetSelectionInternal(IEnumerable<int> indices, bool sendNotification)
		{
			bool flag = !this.HasValidDataAndBindings() || indices == null;
			if (!flag)
			{
				this.ClearSelectionWithoutValidation();
				foreach (int num in indices)
				{
					this.AddToSelectionWithoutValidation(num);
				}
				if (sendNotification)
				{
					this.NotifyOfSelectionChange();
				}
				base.SaveViewData();
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00026438 File Offset: 0x00024638
		private void NotifyOfSelectionChange()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				Action<IEnumerable<object>> action = this.onSelectionChange;
				if (action != null)
				{
					action.Invoke(this.m_SelectedItems);
				}
				Action<IEnumerable<int>> action2 = this.onSelectedIndicesChange;
				if (action2 != null)
				{
					action2.Invoke(this.m_SelectedIndices);
				}
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00026488 File Offset: 0x00024688
		public void ClearSelection()
		{
			bool flag = !this.HasValidDataAndBindings() || this.m_SelectedIds.Count == 0;
			if (!flag)
			{
				this.ClearSelectionWithoutValidation();
				this.NotifyOfSelectionChange();
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x000264C4 File Offset: 0x000246C4
		private void ClearSelectionWithoutValidation()
		{
			foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
			{
				reusableCollectionItem.SetSelected(false);
			}
			this.m_SelectedIds.Clear();
			this.m_SelectedIndices.Clear();
			this.m_SelectedItems.Clear();
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002653C File Offset: 0x0002473C
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00026564 File Offset: 0x00024764
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			bool flag = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId();
			if (flag)
			{
				ListViewDragger dragger = this.m_Dragger;
				if (dragger != null)
				{
					dragger.OnPointerUpEvent((PointerUpEvent)evt);
				}
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
				if (flag2)
				{
					this.m_VirtualizationController.OnFocus(evt.leafTarget as VisualElement);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
					if (flag3)
					{
						BlurEvent blurEvent = evt as BlurEvent;
						this.m_VirtualizationController.OnBlur(((blurEvent != null) ? blurEvent.relatedTarget : null) as VisualElement);
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<NavigationSubmitEvent>.TypeId();
						if (flag4)
						{
							bool flag5 = evt.target == this;
							if (flag5)
							{
								this.m_ScrollView.contentContainer.Focus();
							}
						}
					}
				}
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00026648 File Offset: 0x00024848
		private void OnSizeChanged(GeometryChangedEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = Mathf.Approximately(evt.newRect.width, evt.oldRect.width) && Mathf.Approximately(evt.newRect.height, evt.oldRect.height);
				if (!flag2)
				{
					this.Resize(evt.newRect.size, evt.layoutPass);
				}
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x000266D0 File Offset: 0x000248D0
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			int num;
			bool flag = !this.m_ItemHeightIsInline && e.customStyle.TryGetValue(BaseVerticalCollectionView.s_ItemHeightProperty, out num);
			if (flag)
			{
				bool flag2 = Math.Abs(this.m_FixedItemHeight - (float)num) > float.Epsilon;
				if (flag2)
				{
					this.m_FixedItemHeight = (float)num;
					this.RefreshItems();
				}
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000020E6 File Offset: 0x000002E6
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002672B File Offset: 0x0002492B
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.RefreshItems();
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00026824 File Offset: 0x00024A24
		[CompilerGenerated]
		private void <Apply>g__HandleSelectionAndScroll|164_0(int index, ref BaseVerticalCollectionView.<>c__DisplayClass164_0 A_2)
		{
			bool flag = index < 0 || index >= this.m_ViewController.itemsSource.Count;
			if (!flag)
			{
				bool flag2 = ((this.selectionType == SelectionType.Multiple) & A_2.shiftKey) && this.m_SelectedIndices.Count != 0;
				if (flag2)
				{
					this.DoRangeSelection(index);
				}
				else
				{
					this.selectedIndex = index;
				}
				this.ScrollToItem(index);
			}
		}

		// Token: 0x040003FE RID: 1022
		private Func<int, int> m_GetItemId;

		// Token: 0x040003FF RID: 1023
		private Func<VisualElement> m_MakeItem;

		// Token: 0x04000400 RID: 1024
		private Action<VisualElement, int> m_BindItem;

		// Token: 0x04000403 RID: 1027
		private SelectionType m_SelectionType;

		// Token: 0x04000404 RID: 1028
		private static readonly List<ReusableCollectionItem> k_EmptyItems = new List<ReusableCollectionItem>();

		// Token: 0x04000405 RID: 1029
		private bool m_HorizontalScrollingEnabled;

		// Token: 0x04000406 RID: 1030
		[SerializeField]
		private AlternatingRowBackground m_ShowAlternatingRowBackgrounds = AlternatingRowBackground.None;

		// Token: 0x04000407 RID: 1031
		internal static readonly int s_DefaultItemHeight = 30;

		// Token: 0x04000408 RID: 1032
		internal float m_FixedItemHeight = (float)BaseVerticalCollectionView.s_DefaultItemHeight;

		// Token: 0x04000409 RID: 1033
		internal bool m_ItemHeightIsInline;

		// Token: 0x0400040A RID: 1034
		private CollectionVirtualizationMethod m_VirtualizationMethod;

		// Token: 0x0400040B RID: 1035
		private readonly ScrollView m_ScrollView;

		// Token: 0x0400040C RID: 1036
		private CollectionViewController m_ViewController;

		// Token: 0x0400040D RID: 1037
		private CollectionVirtualizationController m_VirtualizationController;

		// Token: 0x0400040E RID: 1038
		private KeyboardNavigationManipulator m_NavigationManipulator;

		// Token: 0x0400040F RID: 1039
		[SerializeField]
		internal Vector2 m_ScrollOffset;

		// Token: 0x04000410 RID: 1040
		[SerializeField]
		private readonly List<int> m_SelectedIds = new List<int>();

		// Token: 0x04000411 RID: 1041
		private readonly List<int> m_SelectedIndices = new List<int>();

		// Token: 0x04000412 RID: 1042
		private readonly List<object> m_SelectedItems = new List<object>();

		// Token: 0x04000413 RID: 1043
		private float m_LastHeight;

		// Token: 0x04000414 RID: 1044
		private bool m_IsRangeSelectionDirectionUp;

		// Token: 0x04000415 RID: 1045
		private ListViewDragger m_Dragger;

		// Token: 0x04000416 RID: 1046
		internal const float ItemHeightUnset = -1f;

		// Token: 0x04000417 RID: 1047
		internal static CustomStyleProperty<int> s_ItemHeightProperty = new CustomStyleProperty<int>("--unity-item-height");

		// Token: 0x04000418 RID: 1048
		private Action<int, int> m_ItemIndexChangedCallback;

		// Token: 0x04000419 RID: 1049
		private Action m_ItemsSourceChangedCallback;

		// Token: 0x0400041A RID: 1050
		public static readonly string ussClassName = "unity-collection-view";

		// Token: 0x0400041B RID: 1051
		public static readonly string borderUssClassName = BaseVerticalCollectionView.ussClassName + "--with-border";

		// Token: 0x0400041C RID: 1052
		public static readonly string itemUssClassName = BaseVerticalCollectionView.ussClassName + "__item";

		// Token: 0x0400041D RID: 1053
		public static readonly string dragHoverBarUssClassName = BaseVerticalCollectionView.ussClassName + "__drag-hover-bar";

		// Token: 0x0400041E RID: 1054
		public static readonly string itemDragHoverUssClassName = BaseVerticalCollectionView.itemUssClassName + "--drag-hover";

		// Token: 0x0400041F RID: 1055
		public static readonly string itemSelectedVariantUssClassName = BaseVerticalCollectionView.itemUssClassName + "--selected";

		// Token: 0x04000420 RID: 1056
		public static readonly string itemAlternativeBackgroundUssClassName = BaseVerticalCollectionView.itemUssClassName + "--alternative-background";

		// Token: 0x04000421 RID: 1057
		public static readonly string listScrollViewUssClassName = BaseVerticalCollectionView.ussClassName + "__scroll-view";

		// Token: 0x04000422 RID: 1058
		internal static readonly string backgroundFillUssClassName = BaseVerticalCollectionView.ussClassName + "__background";

		// Token: 0x04000423 RID: 1059
		private Vector3 m_TouchDownPosition;
	}
}
