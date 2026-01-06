using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024C RID: 588
	[DisallowMultipleComponent]
	[DefaultExecutionOrder(-100)]
	[ExecuteAlways]
	[AddComponentMenu("UI Toolkit/UI Document")]
	public sealed class UIDocument : MonoBehaviour
	{
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00043BE4 File Offset: 0x00041DE4
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x00043BFC File Offset: 0x00041DFC
		public PanelSettings panelSettings
		{
			get
			{
				return this.m_PanelSettings;
			}
			set
			{
				bool flag = this.parentUI == null;
				if (flag)
				{
					bool flag2 = this.m_PanelSettings == value;
					if (flag2)
					{
						this.m_PreviousPanelSettings = this.m_PanelSettings;
						return;
					}
					bool flag3 = this.m_PanelSettings != null;
					if (flag3)
					{
						this.m_PanelSettings.DetachUIDocument(this);
					}
					this.m_PanelSettings = value;
					bool flag4 = this.m_PanelSettings != null;
					if (flag4)
					{
						this.m_PanelSettings.AttachAndInsertUIDocumentToVisualTree(this);
					}
				}
				else
				{
					Assert.AreEqual<PanelSettings>(this.parentUI.m_PanelSettings, value);
					this.m_PanelSettings = this.parentUI.m_PanelSettings;
				}
				bool flag5 = this.m_ChildrenContent != null;
				if (flag5)
				{
					foreach (UIDocument uidocument in this.m_ChildrenContent.m_AttachedUIDocuments)
					{
						uidocument.panelSettings = this.m_PanelSettings;
					}
				}
				this.m_PreviousPanelSettings = this.m_PanelSettings;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00043D20 File Offset: 0x00041F20
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x00043D28 File Offset: 0x00041F28
		public UIDocument parentUI
		{
			get
			{
				return this.m_ParentUI;
			}
			private set
			{
				this.m_ParentUI = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00043D34 File Offset: 0x00041F34
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x00043D4C File Offset: 0x00041F4C
		public VisualTreeAsset visualTreeAsset
		{
			get
			{
				return this.sourceAsset;
			}
			set
			{
				this.sourceAsset = value;
				this.RecreateUI();
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00043D60 File Offset: 0x00041F60
		public VisualElement rootVisualElement
		{
			get
			{
				return this.m_RootVisualElement;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00043D78 File Offset: 0x00041F78
		internal int firstChildInserIndex
		{
			get
			{
				return this.m_FirstChildInsertIndex;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00043D80 File Offset: 0x00041F80
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00043D88 File Offset: 0x00041F88
		public float sortingOrder
		{
			get
			{
				return this.m_SortingOrder;
			}
			set
			{
				bool flag = this.m_SortingOrder == value;
				if (!flag)
				{
					this.m_SortingOrder = value;
					this.ApplySortingOrder();
				}
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00043DB4 File Offset: 0x00041FB4
		internal void ApplySortingOrder()
		{
			this.AddRootVisualElementToTree();
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00043DBE File Offset: 0x00041FBE
		private UIDocument()
		{
			this.m_UIDocumentCreationIndex = UIDocument.s_CurrentUIDocumentCounter++;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00043DFB File Offset: 0x00041FFB
		private void Awake()
		{
			this.SetupFromHierarchy();
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00043E08 File Offset: 0x00042008
		private void OnEnable()
		{
			bool flag = this.parentUI != null && this.m_PanelSettings == null;
			if (flag)
			{
				this.m_PanelSettings = this.parentUI.m_PanelSettings;
			}
			bool flag2 = this.m_RootVisualElement == null;
			if (flag2)
			{
				this.RecreateUI();
			}
			else
			{
				this.AddRootVisualElementToTree();
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00043E6C File Offset: 0x0004206C
		private void SetupFromHierarchy()
		{
			bool flag = this.parentUI != null;
			if (flag)
			{
				this.parentUI.RemoveChild(this);
			}
			this.parentUI = this.FindUIDocumentParent();
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00043EA8 File Offset: 0x000420A8
		private UIDocument FindUIDocumentParent()
		{
			Transform transform = base.transform;
			Transform parent = transform.parent;
			bool flag = parent != null;
			if (flag)
			{
				UIDocument[] componentsInParent = parent.GetComponentsInParent<UIDocument>(true);
				bool flag2 = componentsInParent != null && componentsInParent.Length != 0;
				if (flag2)
				{
					return componentsInParent[0];
				}
			}
			return null;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00043EFC File Offset: 0x000420FC
		internal void Reset()
		{
			bool flag = this.parentUI == null;
			if (flag)
			{
				PanelSettings previousPanelSettings = this.m_PreviousPanelSettings;
				if (previousPanelSettings != null)
				{
					previousPanelSettings.DetachUIDocument(this);
				}
				this.panelSettings = null;
			}
			this.SetupFromHierarchy();
			bool flag2 = this.parentUI != null;
			if (flag2)
			{
				this.m_PanelSettings = this.parentUI.m_PanelSettings;
				this.AddRootVisualElementToTree();
			}
			else
			{
				bool flag3 = this.m_PanelSettings != null;
				if (flag3)
				{
					this.AddRootVisualElementToTree();
				}
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00043F84 File Offset: 0x00042184
		private void AddChildAndInsertContentToVisualTree(UIDocument child)
		{
			bool flag = this.m_ChildrenContent == null;
			if (flag)
			{
				this.m_ChildrenContent = new UIDocumentList();
			}
			else
			{
				this.m_ChildrenContent.RemoveFromListAndFromVisualTree(child);
			}
			this.m_ChildrenContent.AddToListAndToVisualTree(child, this.m_RootVisualElement, this.m_FirstChildInsertIndex);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00043FD6 File Offset: 0x000421D6
		private void RemoveChild(UIDocument child)
		{
			UIDocumentList childrenContent = this.m_ChildrenContent;
			if (childrenContent != null)
			{
				childrenContent.RemoveFromListAndFromVisualTree(child);
			}
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00043FEC File Offset: 0x000421EC
		private void RecreateUI()
		{
			bool flag = this.m_RootVisualElement != null;
			if (flag)
			{
				this.RemoveFromHierarchy();
				this.m_RootVisualElement = null;
			}
			bool flag2 = this.sourceAsset != null;
			if (flag2)
			{
				this.m_RootVisualElement = this.sourceAsset.Instantiate();
				bool flag3 = this.m_RootVisualElement == null;
				if (flag3)
				{
					Debug.LogError("The UXML file set for the UIDocument could not be cloned.");
				}
			}
			bool flag4 = this.m_RootVisualElement == null;
			if (flag4)
			{
				this.m_RootVisualElement = new TemplateContainer
				{
					name = base.gameObject.name + "-container"
				};
			}
			else
			{
				this.m_RootVisualElement.name = base.gameObject.name + "-container";
			}
			this.m_RootVisualElement.pickingMode = PickingMode.Ignore;
			bool isActiveAndEnabled = base.isActiveAndEnabled;
			if (isActiveAndEnabled)
			{
				this.AddRootVisualElementToTree();
			}
			this.m_FirstChildInsertIndex = this.m_RootVisualElement.childCount;
			bool flag5 = this.m_ChildrenContent != null;
			if (flag5)
			{
				bool flag6 = this.m_ChildrenContentCopy == null;
				if (flag6)
				{
					this.m_ChildrenContentCopy = new List<UIDocument>(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				else
				{
					this.m_ChildrenContentCopy.AddRange(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				foreach (UIDocument uidocument in this.m_ChildrenContentCopy)
				{
					bool isActiveAndEnabled2 = uidocument.isActiveAndEnabled;
					if (isActiveAndEnabled2)
					{
						bool flag7 = uidocument.m_RootVisualElement == null;
						if (flag7)
						{
							uidocument.RecreateUI();
						}
						else
						{
							this.AddChildAndInsertContentToVisualTree(uidocument);
						}
					}
				}
				this.m_ChildrenContentCopy.Clear();
			}
			this.SetupRootClassList();
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000441C8 File Offset: 0x000423C8
		private void SetupRootClassList()
		{
			VisualElement rootVisualElement = this.m_RootVisualElement;
			if (rootVisualElement != null)
			{
				rootVisualElement.EnableInClassList("unity-ui-document__root", this.parentUI == null);
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000441F0 File Offset: 0x000423F0
		private void AddRootVisualElementToTree()
		{
			bool flag = !base.enabled;
			if (!flag)
			{
				bool flag2 = this.parentUI != null;
				if (flag2)
				{
					this.parentUI.AddChildAndInsertContentToVisualTree(this);
				}
				else
				{
					bool flag3 = this.m_PanelSettings != null;
					if (flag3)
					{
						this.m_PanelSettings.AttachAndInsertUIDocumentToVisualTree(this);
					}
				}
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00044250 File Offset: 0x00042450
		private void RemoveFromHierarchy()
		{
			bool flag = this.parentUI != null;
			if (flag)
			{
				this.parentUI.RemoveChild(this);
			}
			else
			{
				bool flag2 = this.m_PanelSettings != null;
				if (flag2)
				{
					this.m_PanelSettings.DetachUIDocument(this);
				}
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x000442A0 File Offset: 0x000424A0
		private void OnDisable()
		{
			bool flag = this.m_RootVisualElement != null;
			if (flag)
			{
				this.RemoveFromHierarchy();
				this.m_RootVisualElement = null;
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000442CC File Offset: 0x000424CC
		private void OnTransformChildrenChanged()
		{
			bool flag = this.m_ChildrenContent != null;
			if (flag)
			{
				bool flag2 = this.m_ChildrenContentCopy == null;
				if (flag2)
				{
					this.m_ChildrenContentCopy = new List<UIDocument>(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				else
				{
					this.m_ChildrenContentCopy.AddRange(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				foreach (UIDocument uidocument in this.m_ChildrenContentCopy)
				{
					uidocument.ReactToHierarchyChanged();
				}
				this.m_ChildrenContentCopy.Clear();
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00044384 File Offset: 0x00042584
		private void OnTransformParentChanged()
		{
			this.ReactToHierarchyChanged();
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00044390 File Offset: 0x00042590
		internal void ReactToHierarchyChanged()
		{
			this.SetupFromHierarchy();
			bool flag = this.parentUI != null;
			if (flag)
			{
				this.panelSettings = this.parentUI.m_PanelSettings;
			}
			VisualElement rootVisualElement = this.m_RootVisualElement;
			if (rootVisualElement != null)
			{
				rootVisualElement.RemoveFromHierarchy();
			}
			this.AddRootVisualElementToTree();
			this.SetupRootClassList();
		}

		// Token: 0x040007C8 RID: 1992
		internal const string k_RootStyleClassName = "unity-ui-document__root";

		// Token: 0x040007C9 RID: 1993
		internal const string k_VisualElementNameSuffix = "-container";

		// Token: 0x040007CA RID: 1994
		private const int k_DefaultSortingOrder = 0;

		// Token: 0x040007CB RID: 1995
		private static int s_CurrentUIDocumentCounter;

		// Token: 0x040007CC RID: 1996
		internal readonly int m_UIDocumentCreationIndex;

		// Token: 0x040007CD RID: 1997
		[SerializeField]
		private PanelSettings m_PanelSettings;

		// Token: 0x040007CE RID: 1998
		private PanelSettings m_PreviousPanelSettings = null;

		// Token: 0x040007CF RID: 1999
		[SerializeField]
		private UIDocument m_ParentUI;

		// Token: 0x040007D0 RID: 2000
		private UIDocumentList m_ChildrenContent = null;

		// Token: 0x040007D1 RID: 2001
		private List<UIDocument> m_ChildrenContentCopy = null;

		// Token: 0x040007D2 RID: 2002
		[SerializeField]
		private VisualTreeAsset sourceAsset;

		// Token: 0x040007D3 RID: 2003
		private VisualElement m_RootVisualElement;

		// Token: 0x040007D4 RID: 2004
		private int m_FirstChildInsertIndex;

		// Token: 0x040007D5 RID: 2005
		[SerializeField]
		private float m_SortingOrder = 0f;
	}
}
