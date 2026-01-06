using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014F RID: 335
	public class ListView : BaseVerticalCollectionView
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0002A6AD File Offset: 0x000288AD
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0002A6B8 File Offset: 0x000288B8
		public bool showBoundCollectionSize
		{
			get
			{
				return this.m_ShowBoundCollectionSize;
			}
			set
			{
				bool flag = this.m_ShowBoundCollectionSize == value;
				if (!flag)
				{
					this.m_ShowBoundCollectionSize = value;
					this.SetupArraySizeField();
				}
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0002A6E3 File Offset: 0x000288E3
		internal override bool sourceIncludesArraySize
		{
			get
			{
				return this.showBoundCollectionSize && base.binding != null && !this.showFoldoutHeader;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0002A701 File Offset: 0x00028901
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x0002A70C File Offset: 0x0002890C
		public bool showFoldoutHeader
		{
			get
			{
				return this.m_ShowFoldoutHeader;
			}
			set
			{
				bool flag = this.m_ShowFoldoutHeader == value;
				if (!flag)
				{
					this.m_ShowFoldoutHeader = value;
					base.EnableInClassList(ListView.listViewWithHeaderUssClassName, value);
					bool showFoldoutHeader = this.m_ShowFoldoutHeader;
					if (showFoldoutHeader)
					{
						bool flag2 = this.m_Foldout != null;
						if (flag2)
						{
							return;
						}
						this.m_Foldout = new Foldout
						{
							name = ListView.foldoutHeaderUssClassName,
							text = this.m_HeaderTitle
						};
						this.m_Foldout.AddToClassList(ListView.foldoutHeaderUssClassName);
						this.m_Foldout.tabIndex = 1;
						base.hierarchy.Add(this.m_Foldout);
						this.m_Foldout.Add(base.scrollView);
					}
					else
					{
						bool flag3 = this.m_Foldout != null;
						if (flag3)
						{
							Foldout foldout = this.m_Foldout;
							if (foldout != null)
							{
								foldout.RemoveFromHierarchy();
							}
							this.m_Foldout = null;
							base.hierarchy.Add(base.scrollView);
						}
					}
					this.SetupArraySizeField();
					this.UpdateEmpty();
					bool showAddRemoveFooter = this.showAddRemoveFooter;
					if (showAddRemoveFooter)
					{
						this.EnableFooter(true);
					}
				}
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002A82C File Offset: 0x00028A2C
		private void SetupArraySizeField()
		{
			bool flag = this.sourceIncludesArraySize || !this.showFoldoutHeader || !this.showBoundCollectionSize;
			if (flag)
			{
				TextField arraySizeField = this.m_ArraySizeField;
				if (arraySizeField != null)
				{
					arraySizeField.RemoveFromHierarchy();
				}
				this.m_ArraySizeField = null;
			}
			else
			{
				this.m_ArraySizeField = new TextField
				{
					name = ListView.arraySizeFieldUssClassName
				};
				this.m_ArraySizeField.AddToClassList(ListView.arraySizeFieldUssClassName);
				this.m_ArraySizeField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnArraySizeFieldChanged));
				this.m_ArraySizeField.isDelayed = true;
				this.m_ArraySizeField.focusable = true;
				base.hierarchy.Add(this.m_ArraySizeField);
				this.UpdateArraySizeField();
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0002A8EC File Offset: 0x00028AEC
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0002A8F4 File Offset: 0x00028AF4
		public string headerTitle
		{
			get
			{
				return this.m_HeaderTitle;
			}
			set
			{
				this.m_HeaderTitle = value;
				bool flag = this.m_Foldout != null;
				if (flag)
				{
					this.m_Foldout.text = this.m_HeaderTitle;
				}
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0002A928 File Offset: 0x00028B28
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x0002A933 File Offset: 0x00028B33
		public bool showAddRemoveFooter
		{
			get
			{
				return this.m_Footer != null;
			}
			set
			{
				this.EnableFooter(value);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0002A93D File Offset: 0x00028B3D
		internal Foldout headerFoldout
		{
			get
			{
				return this.m_Foldout;
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002A948 File Offset: 0x00028B48
		private void EnableFooter(bool enabled)
		{
			base.EnableInClassList(ListView.listViewWithFooterUssClassName, enabled);
			base.scrollView.EnableInClassList(ListView.scrollViewWithFooterUssClassName, enabled);
			if (enabled)
			{
				bool flag = this.m_Footer == null;
				if (flag)
				{
					this.m_Footer = new VisualElement
					{
						name = ListView.footerUssClassName
					};
					this.m_Footer.AddToClassList(ListView.footerUssClassName);
					this.m_RemoveButton = new Button(new Action(this.OnRemoveClicked))
					{
						name = ListView.footerRemoveButtonName,
						text = "-"
					};
					this.m_Footer.Add(this.m_RemoveButton);
					this.m_AddButton = new Button(new Action(this.OnAddClicked))
					{
						name = ListView.footerAddButtonName,
						text = "+"
					};
					this.m_Footer.Add(this.m_AddButton);
				}
				bool flag2 = this.m_Foldout != null;
				if (flag2)
				{
					this.m_Foldout.contentContainer.Add(this.m_Footer);
				}
				else
				{
					base.hierarchy.Add(this.m_Footer);
				}
			}
			else
			{
				Button removeButton = this.m_RemoveButton;
				if (removeButton != null)
				{
					removeButton.RemoveFromHierarchy();
				}
				Button addButton = this.m_AddButton;
				if (addButton != null)
				{
					addButton.RemoveFromHierarchy();
				}
				VisualElement footer = this.m_Footer;
				if (footer != null)
				{
					footer.RemoveFromHierarchy();
				}
				this.m_RemoveButton = null;
				this.m_AddButton = null;
				this.m_Footer = null;
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000AAB RID: 2731 RVA: 0x0002AAC8 File Offset: 0x00028CC8
		// (remove) Token: 0x06000AAC RID: 2732 RVA: 0x0002AB00 File Offset: 0x00028D00
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<int>> itemsAdded;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000AAD RID: 2733 RVA: 0x0002AB38 File Offset: 0x00028D38
		// (remove) Token: 0x06000AAE RID: 2734 RVA: 0x0002AB70 File Offset: 0x00028D70
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<int>> itemsRemoved;

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002ABA5 File Offset: 0x00028DA5
		private void AddItems(int itemCount)
		{
			this.viewController.AddItems(itemCount);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002ABB5 File Offset: 0x00028DB5
		private void RemoveItems(List<int> indices)
		{
			this.viewController.RemoveItems(indices);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002ABC8 File Offset: 0x00028DC8
		private void OnArraySizeFieldChanged(ChangeEvent<string> evt)
		{
			int num;
			bool flag = !int.TryParse(evt.newValue, ref num) || num < 0;
			if (flag)
			{
				this.m_ArraySizeField.SetValueWithoutNotify(evt.previousValue);
			}
			else
			{
				int itemCount = this.viewController.GetItemCount();
				bool flag2 = num > itemCount;
				if (flag2)
				{
					this.viewController.AddItems(num - itemCount);
				}
				else
				{
					bool flag3 = num < itemCount;
					if (flag3)
					{
						int num2 = itemCount;
						for (int i = num2 - 1; i >= num; i--)
						{
							this.viewController.RemoveItem(i);
						}
					}
				}
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002AC68 File Offset: 0x00028E68
		private void UpdateArraySizeField()
		{
			bool flag = !base.HasValidDataAndBindings();
			if (!flag)
			{
				TextField arraySizeField = this.m_ArraySizeField;
				if (arraySizeField != null)
				{
					arraySizeField.SetValueWithoutNotify(this.viewController.GetItemCount().ToString());
				}
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002ACAC File Offset: 0x00028EAC
		private void UpdateEmpty()
		{
			bool flag = !base.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = base.itemsSource.Count == 0 && !this.sourceIncludesArraySize;
				if (flag2)
				{
					bool flag3 = this.m_EmptyListLabel != null;
					if (!flag3)
					{
						this.m_EmptyListLabel = new Label("List is Empty");
						this.m_EmptyListLabel.AddToClassList(ListView.emptyLabelUssClassName);
						base.scrollView.contentViewport.Add(this.m_EmptyListLabel);
					}
				}
				else
				{
					Label emptyListLabel = this.m_EmptyListLabel;
					if (emptyListLabel != null)
					{
						emptyListLabel.RemoveFromHierarchy();
					}
					this.m_EmptyListLabel = null;
				}
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002AD50 File Offset: 0x00028F50
		private void OnAddClicked()
		{
			this.AddItems(1);
			bool flag = base.binding == null;
			if (flag)
			{
				base.SetSelection(base.itemsSource.Count - 1);
				base.ScrollToItem(-1);
			}
			else
			{
				base.schedule.Execute(delegate
				{
					base.SetSelection(base.itemsSource.Count - 1);
					base.ScrollToItem(-1);
				}).ExecuteLater(100L);
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002ADB8 File Offset: 0x00028FB8
		private void OnRemoveClicked()
		{
			bool flag = Enumerable.Any<int>(base.selectedIndices);
			if (flag)
			{
				this.viewController.RemoveItems(Enumerable.ToList<int>(base.selectedIndices));
				base.ClearSelection();
			}
			else
			{
				bool flag2 = base.itemsSource.Count > 0;
				if (flag2)
				{
					int num = base.itemsSource.Count - 1;
					this.viewController.RemoveItem(num);
				}
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002AE26 File Offset: 0x00029026
		internal new ListViewController viewController
		{
			get
			{
				return this.m_ListViewController;
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002AE2E File Offset: 0x0002902E
		private protected override void CreateVirtualizationController()
		{
			base.CreateVirtualizationController<ReusableListViewItem>();
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002AE38 File Offset: 0x00029038
		private protected override void CreateViewController()
		{
			this.SetViewController(new ListViewController());
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002AE48 File Offset: 0x00029048
		internal void SetViewController(ListViewController controller)
		{
			if (this.m_ItemAddedCallback == null)
			{
				this.m_ItemAddedCallback = new Action<IEnumerable<int>>(this.OnItemAdded);
			}
			if (this.m_ItemRemovedCallback == null)
			{
				this.m_ItemRemovedCallback = new Action<IEnumerable<int>>(this.OnItemsRemoved);
			}
			if (this.m_ItemsSourceSizeChangedCallback == null)
			{
				this.m_ItemsSourceSizeChangedCallback = new Action(this.OnItemsSourceSizeChanged);
			}
			bool flag = this.m_ListViewController != null;
			if (flag)
			{
				this.m_ListViewController.itemsAdded -= this.m_ItemAddedCallback;
				this.m_ListViewController.itemsRemoved -= this.m_ItemRemovedCallback;
				this.m_ListViewController.itemsSourceSizeChanged -= this.m_ItemsSourceSizeChangedCallback;
			}
			base.SetViewController(controller);
			this.m_ListViewController = controller;
			bool flag2 = this.m_ListViewController != null;
			if (flag2)
			{
				this.m_ListViewController.itemsAdded += this.m_ItemAddedCallback;
				this.m_ListViewController.itemsRemoved += this.m_ItemRemovedCallback;
				this.m_ListViewController.itemsSourceSizeChanged += this.m_ItemsSourceSizeChangedCallback;
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002AF3D File Offset: 0x0002913D
		private void OnItemAdded(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsAdded;
			if (action != null)
			{
				action.Invoke(indices);
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002AF53 File Offset: 0x00029153
		private void OnItemsRemoved(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsRemoved;
			if (action != null)
			{
				action.Invoke(indices);
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002672B File Offset: 0x0002492B
		private void OnItemsSourceSizeChanged()
		{
			base.RefreshItems();
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002AF69 File Offset: 0x00029169
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x0002AF74 File Offset: 0x00029174
		public ListViewReorderMode reorderMode
		{
			get
			{
				return this.m_ReorderMode;
			}
			set
			{
				bool flag = value != this.m_ReorderMode;
				if (flag)
				{
					this.m_ReorderMode = value;
					base.InitializeDragAndDropController();
					base.Rebuild();
				}
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002AFAC File Offset: 0x000291AC
		internal override ListViewDragger CreateDragger()
		{
			bool flag = this.m_ReorderMode == ListViewReorderMode.Simple;
			ListViewDragger listViewDragger;
			if (flag)
			{
				listViewDragger = new ListViewDragger(this);
			}
			else
			{
				listViewDragger = new ListViewDraggerAnimated(this);
			}
			return listViewDragger;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002AFDA File Offset: 0x000291DA
		internal override ICollectionDragAndDropController CreateDragAndDropController()
		{
			return new ListViewReorderableDragAndDropController(this);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002AFE2 File Offset: 0x000291E2
		public ListView()
		{
			base.AddToClassList(ListView.ussClassName);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002AFFF File Offset: 0x000291FF
		public ListView(IList itemsSource, float itemHeight = -1f, Func<VisualElement> makeItem = null, Action<VisualElement, int> bindItem = null)
			: base(itemsSource, itemHeight, makeItem, bindItem)
		{
			base.AddToClassList(ListView.ussClassName);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002B021 File Offset: 0x00029221
		private protected override void PostRefresh()
		{
			this.UpdateArraySizeField();
			this.UpdateEmpty();
			base.PostRefresh();
		}

		// Token: 0x040004A0 RID: 1184
		private bool m_ShowBoundCollectionSize = true;

		// Token: 0x040004A1 RID: 1185
		private bool m_ShowFoldoutHeader;

		// Token: 0x040004A2 RID: 1186
		private string m_HeaderTitle;

		// Token: 0x040004A5 RID: 1189
		private Label m_EmptyListLabel;

		// Token: 0x040004A6 RID: 1190
		private Foldout m_Foldout;

		// Token: 0x040004A7 RID: 1191
		private TextField m_ArraySizeField;

		// Token: 0x040004A8 RID: 1192
		private VisualElement m_Footer;

		// Token: 0x040004A9 RID: 1193
		private Button m_AddButton;

		// Token: 0x040004AA RID: 1194
		private Button m_RemoveButton;

		// Token: 0x040004AB RID: 1195
		private Action<IEnumerable<int>> m_ItemAddedCallback;

		// Token: 0x040004AC RID: 1196
		private Action<IEnumerable<int>> m_ItemRemovedCallback;

		// Token: 0x040004AD RID: 1197
		private Action m_ItemsSourceSizeChangedCallback;

		// Token: 0x040004AE RID: 1198
		private ListViewController m_ListViewController;

		// Token: 0x040004AF RID: 1199
		private ListViewReorderMode m_ReorderMode;

		// Token: 0x040004B0 RID: 1200
		public new static readonly string ussClassName = "unity-list-view";

		// Token: 0x040004B1 RID: 1201
		public new static readonly string itemUssClassName = ListView.ussClassName + "__item";

		// Token: 0x040004B2 RID: 1202
		public static readonly string emptyLabelUssClassName = ListView.ussClassName + "__empty-label";

		// Token: 0x040004B3 RID: 1203
		public static readonly string reorderableUssClassName = ListView.ussClassName + "__reorderable";

		// Token: 0x040004B4 RID: 1204
		public static readonly string reorderableItemUssClassName = ListView.reorderableUssClassName + "-item";

		// Token: 0x040004B5 RID: 1205
		public static readonly string reorderableItemContainerUssClassName = ListView.reorderableItemUssClassName + "__container";

		// Token: 0x040004B6 RID: 1206
		public static readonly string reorderableItemHandleUssClassName = ListView.reorderableUssClassName + "-handle";

		// Token: 0x040004B7 RID: 1207
		public static readonly string reorderableItemHandleBarUssClassName = ListView.reorderableItemHandleUssClassName + "-bar";

		// Token: 0x040004B8 RID: 1208
		public static readonly string footerUssClassName = ListView.ussClassName + "__footer";

		// Token: 0x040004B9 RID: 1209
		public static readonly string foldoutHeaderUssClassName = ListView.ussClassName + "__foldout-header";

		// Token: 0x040004BA RID: 1210
		public static readonly string arraySizeFieldUssClassName = ListView.ussClassName + "__size-field";

		// Token: 0x040004BB RID: 1211
		public static readonly string listViewWithHeaderUssClassName = ListView.ussClassName + "--with-header";

		// Token: 0x040004BC RID: 1212
		public static readonly string listViewWithFooterUssClassName = ListView.ussClassName + "--with-footer";

		// Token: 0x040004BD RID: 1213
		public static readonly string scrollViewWithFooterUssClassName = ListView.ussClassName + "__scroll-view--with-footer";

		// Token: 0x040004BE RID: 1214
		internal static readonly string footerAddButtonName = ListView.ussClassName + "__add-button";

		// Token: 0x040004BF RID: 1215
		internal static readonly string footerRemoveButtonName = ListView.ussClassName + "__remove-button";

		// Token: 0x02000150 RID: 336
		public new class UxmlFactory : UxmlFactory<ListView, ListView.UxmlTraits>
		{
		}

		// Token: 0x02000151 RID: 337
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x17000215 RID: 533
			// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002B1A8 File Offset: 0x000293A8
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000AC8 RID: 2760 RVA: 0x0002B1C8 File Offset: 0x000293C8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int num = 0;
				ListView listView = (ListView)ve;
				listView.reorderable = this.m_Reorderable.GetValueFromBag(bag, cc);
				bool flag = this.m_FixedItemHeight.TryGetValueFromBag(bag, cc, ref num);
				if (flag)
				{
					listView.fixedItemHeight = (float)num;
				}
				listView.reorderMode = this.m_ReorderMode.GetValueFromBag(bag, cc);
				listView.virtualizationMethod = this.m_VirtualizationMethod.GetValueFromBag(bag, cc);
				listView.showBorder = this.m_ShowBorder.GetValueFromBag(bag, cc);
				listView.selectionType = this.m_SelectionType.GetValueFromBag(bag, cc);
				listView.showAlternatingRowBackgrounds = this.m_ShowAlternatingRowBackgrounds.GetValueFromBag(bag, cc);
				listView.showFoldoutHeader = this.m_ShowFoldoutHeader.GetValueFromBag(bag, cc);
				listView.headerTitle = this.m_HeaderTitle.GetValueFromBag(bag, cc);
				listView.showAddRemoveFooter = this.m_ShowAddRemoveFooter.GetValueFromBag(bag, cc);
				listView.showBoundCollectionSize = this.m_ShowBoundCollectionSize.GetValueFromBag(bag, cc);
				listView.horizontalScrollingEnabled = this.m_HorizontalScrollingEnabled.GetValueFromBag(bag, cc);
			}

			// Token: 0x040004C0 RID: 1216
			private readonly UxmlIntAttributeDescription m_FixedItemHeight = new UxmlIntAttributeDescription
			{
				name = "fixed-item-height",
				obsoleteNames = new string[] { "itemHeight, item-height" },
				defaultValue = BaseVerticalCollectionView.s_DefaultItemHeight
			};

			// Token: 0x040004C1 RID: 1217
			private readonly UxmlEnumAttributeDescription<CollectionVirtualizationMethod> m_VirtualizationMethod = new UxmlEnumAttributeDescription<CollectionVirtualizationMethod>
			{
				name = "virtualization-method",
				defaultValue = CollectionVirtualizationMethod.FixedHeight
			};

			// Token: 0x040004C2 RID: 1218
			private readonly UxmlBoolAttributeDescription m_ShowBorder = new UxmlBoolAttributeDescription
			{
				name = "show-border",
				defaultValue = false
			};

			// Token: 0x040004C3 RID: 1219
			private readonly UxmlEnumAttributeDescription<SelectionType> m_SelectionType = new UxmlEnumAttributeDescription<SelectionType>
			{
				name = "selection-type",
				defaultValue = SelectionType.Single
			};

			// Token: 0x040004C4 RID: 1220
			private readonly UxmlEnumAttributeDescription<AlternatingRowBackground> m_ShowAlternatingRowBackgrounds = new UxmlEnumAttributeDescription<AlternatingRowBackground>
			{
				name = "show-alternating-row-backgrounds",
				defaultValue = AlternatingRowBackground.None
			};

			// Token: 0x040004C5 RID: 1221
			private readonly UxmlBoolAttributeDescription m_ShowFoldoutHeader = new UxmlBoolAttributeDescription
			{
				name = "show-foldout-header",
				defaultValue = false
			};

			// Token: 0x040004C6 RID: 1222
			private readonly UxmlStringAttributeDescription m_HeaderTitle = new UxmlStringAttributeDescription
			{
				name = "header-title",
				defaultValue = string.Empty
			};

			// Token: 0x040004C7 RID: 1223
			private readonly UxmlBoolAttributeDescription m_ShowAddRemoveFooter = new UxmlBoolAttributeDescription
			{
				name = "show-add-remove-footer",
				defaultValue = false
			};

			// Token: 0x040004C8 RID: 1224
			private readonly UxmlBoolAttributeDescription m_Reorderable = new UxmlBoolAttributeDescription
			{
				name = "reorderable",
				defaultValue = false
			};

			// Token: 0x040004C9 RID: 1225
			private readonly UxmlEnumAttributeDescription<ListViewReorderMode> m_ReorderMode = new UxmlEnumAttributeDescription<ListViewReorderMode>
			{
				name = "reorder-mode",
				defaultValue = ListViewReorderMode.Simple
			};

			// Token: 0x040004CA RID: 1226
			private readonly UxmlBoolAttributeDescription m_ShowBoundCollectionSize = new UxmlBoolAttributeDescription
			{
				name = "show-bound-collection-size",
				defaultValue = true
			};

			// Token: 0x040004CB RID: 1227
			private readonly UxmlBoolAttributeDescription m_HorizontalScrollingEnabled = new UxmlBoolAttributeDescription
			{
				name = "horizontal-scrolling",
				defaultValue = false
			};
		}
	}
}
