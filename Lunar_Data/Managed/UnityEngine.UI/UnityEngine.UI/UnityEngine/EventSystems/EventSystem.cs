using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000064 RID: 100
	[AddComponentMenu("Event/Event System")]
	[DisallowMultipleComponent]
	public class EventSystem : UIBehaviour
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001832E File Offset: 0x0001652E
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0001834C File Offset: 0x0001654C
		public static EventSystem current
		{
			get
			{
				if (EventSystem.m_EventSystems.Count <= 0)
				{
					return null;
				}
				return EventSystem.m_EventSystems[0];
			}
			set
			{
				int num = EventSystem.m_EventSystems.IndexOf(value);
				if (num > 0)
				{
					EventSystem.m_EventSystems.RemoveAt(num);
					EventSystem.m_EventSystems.Insert(0, value);
					return;
				}
				if (num < 0)
				{
					Debug.LogError("Failed setting EventSystem.current to unknown EventSystem " + ((value != null) ? value.ToString() : null));
				}
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000183A1 File Offset: 0x000165A1
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x000183A9 File Offset: 0x000165A9
		public bool sendNavigationEvents
		{
			get
			{
				return this.m_sendNavigationEvents;
			}
			set
			{
				this.m_sendNavigationEvents = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x000183B2 File Offset: 0x000165B2
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x000183BA File Offset: 0x000165BA
		public int pixelDragThreshold
		{
			get
			{
				return this.m_DragThreshold;
			}
			set
			{
				this.m_DragThreshold = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x000183C3 File Offset: 0x000165C3
		public BaseInputModule currentInputModule
		{
			get
			{
				return this.m_CurrentInputModule;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x000183CB File Offset: 0x000165CB
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x000183D3 File Offset: 0x000165D3
		public GameObject firstSelectedGameObject
		{
			get
			{
				return this.m_FirstSelected;
			}
			set
			{
				this.m_FirstSelected = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x000183DC File Offset: 0x000165DC
		public GameObject currentSelectedGameObject
		{
			get
			{
				return this.m_CurrentSelected;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x000183E4 File Offset: 0x000165E4
		[Obsolete("lastSelectedGameObject is no longer supported")]
		public GameObject lastSelectedGameObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x000183E7 File Offset: 0x000165E7
		public bool isFocused
		{
			get
			{
				return this.m_HasFocus;
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000183EF File Offset: 0x000165EF
		protected EventSystem()
		{
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00018418 File Offset: 0x00016618
		public void UpdateModules()
		{
			base.GetComponents<BaseInputModule>(this.m_SystemInputModules);
			for (int i = this.m_SystemInputModules.Count - 1; i >= 0; i--)
			{
				if (!this.m_SystemInputModules[i] || !this.m_SystemInputModules[i].IsActive())
				{
					this.m_SystemInputModules.RemoveAt(i);
				}
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001847B File Offset: 0x0001667B
		public bool alreadySelecting
		{
			get
			{
				return this.m_SelectionGuard;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00018484 File Offset: 0x00016684
		public void SetSelectedGameObject(GameObject selected, BaseEventData pointer)
		{
			if (this.m_SelectionGuard)
			{
				Debug.LogError("Attempting to select " + ((selected != null) ? selected.ToString() : null) + "while already selecting an object.");
				return;
			}
			this.m_SelectionGuard = true;
			if (selected == this.m_CurrentSelected)
			{
				this.m_SelectionGuard = false;
				return;
			}
			ExecuteEvents.Execute<IDeselectHandler>(this.m_CurrentSelected, pointer, ExecuteEvents.deselectHandler);
			this.m_CurrentSelected = selected;
			ExecuteEvents.Execute<ISelectHandler>(this.m_CurrentSelected, pointer, ExecuteEvents.selectHandler);
			this.m_SelectionGuard = false;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001850A File Offset: 0x0001670A
		private BaseEventData baseEventDataCache
		{
			get
			{
				if (this.m_DummyData == null)
				{
					this.m_DummyData = new BaseEventData(this);
				}
				return this.m_DummyData;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00018526 File Offset: 0x00016726
		public void SetSelectedGameObject(GameObject selected)
		{
			this.SetSelectedGameObject(selected, this.baseEventDataCache);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00018538 File Offset: 0x00016738
		private static int RaycastComparer(RaycastResult lhs, RaycastResult rhs)
		{
			if (lhs.module != rhs.module)
			{
				Camera eventCamera = lhs.module.eventCamera;
				Camera eventCamera2 = rhs.module.eventCamera;
				if (eventCamera != null && eventCamera2 != null && eventCamera.depth != eventCamera2.depth)
				{
					if (eventCamera.depth < eventCamera2.depth)
					{
						return 1;
					}
					if (eventCamera.depth == eventCamera2.depth)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (lhs.module.sortOrderPriority != rhs.module.sortOrderPriority)
					{
						return rhs.module.sortOrderPriority.CompareTo(lhs.module.sortOrderPriority);
					}
					if (lhs.module.renderOrderPriority != rhs.module.renderOrderPriority)
					{
						return rhs.module.renderOrderPriority.CompareTo(lhs.module.renderOrderPriority);
					}
				}
			}
			if (lhs.sortingLayer != rhs.sortingLayer)
			{
				int layerValueFromID = SortingLayer.GetLayerValueFromID(rhs.sortingLayer);
				int layerValueFromID2 = SortingLayer.GetLayerValueFromID(lhs.sortingLayer);
				return layerValueFromID.CompareTo(layerValueFromID2);
			}
			if (lhs.sortingOrder != rhs.sortingOrder)
			{
				return rhs.sortingOrder.CompareTo(lhs.sortingOrder);
			}
			if (lhs.depth != rhs.depth && lhs.module.rootRaycaster == rhs.module.rootRaycaster)
			{
				return rhs.depth.CompareTo(lhs.depth);
			}
			if (lhs.distance != rhs.distance)
			{
				return lhs.distance.CompareTo(rhs.distance);
			}
			if (lhs.sortingGroupID != SortingGroup.invalidSortingGroupID && rhs.sortingGroupID != SortingGroup.invalidSortingGroupID)
			{
				if (lhs.sortingGroupID != rhs.sortingGroupID)
				{
					return lhs.sortingGroupID.CompareTo(rhs.sortingGroupID);
				}
				if (lhs.sortingGroupOrder != rhs.sortingGroupOrder)
				{
					return rhs.sortingGroupOrder.CompareTo(lhs.sortingGroupOrder);
				}
			}
			return lhs.index.CompareTo(rhs.index);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00018744 File Offset: 0x00016944
		public void RaycastAll(PointerEventData eventData, List<RaycastResult> raycastResults)
		{
			raycastResults.Clear();
			List<BaseRaycaster> raycasters = RaycasterManager.GetRaycasters();
			int count = raycasters.Count;
			for (int i = 0; i < count; i++)
			{
				BaseRaycaster baseRaycaster = raycasters[i];
				if (!(baseRaycaster == null) && baseRaycaster.IsActive())
				{
					baseRaycaster.Raycast(eventData, raycastResults);
				}
			}
			raycastResults.Sort(EventSystem.s_RaycastComparer);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001879C File Offset: 0x0001699C
		public bool IsPointerOverGameObject()
		{
			return this.IsPointerOverGameObject(-1);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000187A5 File Offset: 0x000169A5
		public bool IsPointerOverGameObject(int pointerId)
		{
			return this.m_CurrentInputModule != null && this.m_CurrentInputModule.IsPointerOverGameObject(pointerId);
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000187C3 File Offset: 0x000169C3
		private bool isUIToolkitActiveEventSystem
		{
			get
			{
				return EventSystem.s_UIToolkitOverride.activeEventSystem == this || EventSystem.s_UIToolkitOverride.activeEventSystem == null;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x000187E9 File Offset: 0x000169E9
		private bool sendUIToolkitEvents
		{
			get
			{
				return EventSystem.s_UIToolkitOverride.sendEvents && this.isUIToolkitActiveEventSystem;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000187FF File Offset: 0x000169FF
		private bool createUIToolkitPanelGameObjectsOnStart
		{
			get
			{
				return EventSystem.s_UIToolkitOverride.createPanelGameObjectsOnStart && this.isUIToolkitActiveEventSystem;
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00018818 File Offset: 0x00016A18
		public static void SetUITookitEventSystemOverride(EventSystem activeEventSystem, bool sendEvents = true, bool createPanelGameObjectsOnStart = true)
		{
			UIElementsRuntimeUtility.UnregisterEventSystem(UIElementsRuntimeUtility.activeEventSystem);
			EventSystem.s_UIToolkitOverride = new EventSystem.UIToolkitOverrideConfig
			{
				activeEventSystem = activeEventSystem,
				sendEvents = sendEvents,
				createPanelGameObjectsOnStart = createPanelGameObjectsOnStart
			};
			if (sendEvents && ((activeEventSystem != null) ? activeEventSystem : EventSystem.current).isActiveAndEnabled)
			{
				UIElementsRuntimeUtility.RegisterEventSystem(activeEventSystem);
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00018878 File Offset: 0x00016A78
		private void CreateUIToolkitPanelGameObject(BaseRuntimePanel panel)
		{
			if (panel.selectableGameObject == null)
			{
				GameObject go = new GameObject(panel.name, new Type[]
				{
					typeof(PanelEventHandler),
					typeof(PanelRaycaster)
				});
				go.transform.SetParent(base.transform);
				panel.selectableGameObject = go;
				panel.destroyed += delegate
				{
					Object.DestroyImmediate(go);
				};
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00018900 File Offset: 0x00016B00
		protected override void Start()
		{
			base.Start();
			if (this.createUIToolkitPanelGameObjectsOnStart)
			{
				foreach (Panel panel in UIElementsRuntimeUtility.GetSortedPlayerPanels())
				{
					BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)panel;
					this.CreateUIToolkitPanelGameObject(baseRuntimePanel);
				}
				UIElementsRuntimeUtility.onCreatePanel += this.CreateUIToolkitPanelGameObject;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00018978 File Offset: 0x00016B78
		protected override void OnDestroy()
		{
			UIElementsRuntimeUtility.onCreatePanel -= this.CreateUIToolkitPanelGameObject;
			base.OnDestroy();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00018991 File Offset: 0x00016B91
		protected override void OnEnable()
		{
			base.OnEnable();
			EventSystem.m_EventSystems.Add(this);
			if (this.sendUIToolkitEvents)
			{
				UIElementsRuntimeUtility.RegisterEventSystem(this);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000189B2 File Offset: 0x00016BB2
		protected override void OnDisable()
		{
			UIElementsRuntimeUtility.UnregisterEventSystem(this);
			if (this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.DeactivateModule();
				this.m_CurrentInputModule = null;
			}
			EventSystem.m_EventSystems.Remove(this);
			base.OnDisable();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000189EC File Offset: 0x00016BEC
		private void TickModules()
		{
			int count = this.m_SystemInputModules.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.m_SystemInputModules[i] != null)
				{
					this.m_SystemInputModules[i].UpdateModule();
				}
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00018A36 File Offset: 0x00016C36
		protected virtual void OnApplicationFocus(bool hasFocus)
		{
			this.m_HasFocus = hasFocus;
			if (!this.m_HasFocus)
			{
				this.TickModules();
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00018A50 File Offset: 0x00016C50
		protected virtual void Update()
		{
			if (EventSystem.current != this)
			{
				return;
			}
			this.TickModules();
			bool flag = false;
			int count = this.m_SystemInputModules.Count;
			int i = 0;
			while (i < count)
			{
				BaseInputModule baseInputModule = this.m_SystemInputModules[i];
				if (baseInputModule.IsModuleSupported() && baseInputModule.ShouldActivateModule())
				{
					if (this.m_CurrentInputModule != baseInputModule)
					{
						this.ChangeEventModule(baseInputModule);
						flag = true;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			if (this.m_CurrentInputModule == null)
			{
				for (int j = 0; j < count; j++)
				{
					BaseInputModule baseInputModule2 = this.m_SystemInputModules[j];
					if (baseInputModule2.IsModuleSupported())
					{
						this.ChangeEventModule(baseInputModule2);
						flag = true;
						break;
					}
				}
			}
			if (!flag && this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.Process();
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00018B1F File Offset: 0x00016D1F
		private void ChangeEventModule(BaseInputModule module)
		{
			if (this.m_CurrentInputModule == module)
			{
				return;
			}
			if (this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.DeactivateModule();
			}
			if (module != null)
			{
				module.ActivateModule();
			}
			this.m_CurrentInputModule = module;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00018B60 File Offset: 0x00016D60
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "<b>Selected:</b>";
			GameObject currentSelectedGameObject = this.currentSelectedGameObject;
			stringBuilder.AppendLine(text + ((currentSelectedGameObject != null) ? currentSelectedGameObject.ToString() : null));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine((this.m_CurrentInputModule != null) ? this.m_CurrentInputModule.ToString() : "No module");
			return stringBuilder.ToString();
		}

		// Token: 0x040001D1 RID: 465
		private List<BaseInputModule> m_SystemInputModules = new List<BaseInputModule>();

		// Token: 0x040001D2 RID: 466
		private BaseInputModule m_CurrentInputModule;

		// Token: 0x040001D3 RID: 467
		private static List<EventSystem> m_EventSystems = new List<EventSystem>();

		// Token: 0x040001D4 RID: 468
		[SerializeField]
		[FormerlySerializedAs("m_Selected")]
		private GameObject m_FirstSelected;

		// Token: 0x040001D5 RID: 469
		[SerializeField]
		private bool m_sendNavigationEvents = true;

		// Token: 0x040001D6 RID: 470
		[SerializeField]
		private int m_DragThreshold = 10;

		// Token: 0x040001D7 RID: 471
		private GameObject m_CurrentSelected;

		// Token: 0x040001D8 RID: 472
		private bool m_HasFocus = true;

		// Token: 0x040001D9 RID: 473
		private bool m_SelectionGuard;

		// Token: 0x040001DA RID: 474
		private BaseEventData m_DummyData;

		// Token: 0x040001DB RID: 475
		private static readonly Comparison<RaycastResult> s_RaycastComparer = new Comparison<RaycastResult>(EventSystem.RaycastComparer);

		// Token: 0x040001DC RID: 476
		private static EventSystem.UIToolkitOverrideConfig s_UIToolkitOverride = new EventSystem.UIToolkitOverrideConfig
		{
			activeEventSystem = null,
			sendEvents = true,
			createPanelGameObjectsOnStart = true
		};

		// Token: 0x020000C2 RID: 194
		private struct UIToolkitOverrideConfig
		{
			// Token: 0x04000341 RID: 833
			public EventSystem activeEventSystem;

			// Token: 0x04000342 RID: 834
			public bool sendEvents;

			// Token: 0x04000343 RID: 835
			public bool createPanelGameObjectsOnStart;
		}
	}
}
