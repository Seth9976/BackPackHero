using System;
using System.Diagnostics;
using UnityEngine.Pool;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000112 RID: 274
	internal class ReusableTreeViewItem : ReusableCollectionItem
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00021754 File Offset: 0x0001F954
		public override VisualElement rootElement
		{
			get
			{
				return this.m_Container ?? base.bindableElement;
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060008CA RID: 2250 RVA: 0x00021768 File Offset: 0x0001F968
		// (remove) Token: 0x060008CB RID: 2251 RVA: 0x000217A0 File Offset: 0x0001F9A0
		[field: DebuggerBrowsable(0)]
		public event Action<PointerUpEvent> onPointerUp;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060008CC RID: 2252 RVA: 0x000217D8 File Offset: 0x0001F9D8
		// (remove) Token: 0x060008CD RID: 2253 RVA: 0x00021810 File Offset: 0x0001FA10
		[field: DebuggerBrowsable(0)]
		public event Action<ChangeEvent<bool>> onToggleValueChanged;

		// Token: 0x060008CE RID: 2254 RVA: 0x00021848 File Offset: 0x0001FA48
		public ReusableTreeViewItem()
		{
			this.m_PointerUpCallback = new EventCallback<PointerUpEvent>(this.OnPointerUp);
			this.m_ToggleValueChangedCallback = new EventCallback<ChangeEvent<bool>>(this.OnToggleValueChanged);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000218B8 File Offset: 0x0001FAB8
		public override void Init(VisualElement item)
		{
			base.Init(item);
			this.m_Container = new VisualElement
			{
				name = TreeView.itemUssClassName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			this.m_Container.AddToClassList(TreeView.itemUssClassName);
			this.m_IndentContainer = new VisualElement
			{
				name = TreeView.itemIndentsContainerUssClassName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			this.m_IndentContainer.AddToClassList(TreeView.itemIndentsContainerUssClassName);
			this.m_Container.hierarchy.Add(this.m_IndentContainer);
			this.m_Toggle = new Toggle
			{
				name = TreeView.itemToggleUssClassName
			};
			this.m_Toggle.userData = this;
			this.m_Toggle.AddToClassList(Foldout.toggleUssClassName);
			this.m_Toggle.visualInput.AddToClassList(Foldout.inputUssClassName);
			this.m_Toggle.visualInput.Q(null, Toggle.checkmarkUssClassName).AddToClassList(Foldout.checkmarkUssClassName);
			this.m_Container.hierarchy.Add(this.m_Toggle);
			this.m_BindableContainer = new VisualElement
			{
				name = TreeView.itemContentContainerUssClassName,
				style = 
				{
					flexGrow = 1f
				}
			};
			this.m_BindableContainer.AddToClassList(TreeView.itemContentContainerUssClassName);
			this.m_Container.Add(this.m_BindableContainer);
			this.m_BindableContainer.Add(item);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00021A3F File Offset: 0x0001FC3F
		public override void PreAttachElement()
		{
			base.PreAttachElement();
			this.rootElement.AddToClassList(TreeView.itemUssClassName);
			this.m_Container.RegisterCallback<PointerUpEvent>(this.m_PointerUpCallback, TrickleDown.NoTrickleDown);
			this.m_Toggle.RegisterValueChangedCallback(this.m_ToggleValueChangedCallback);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00021A7F File Offset: 0x0001FC7F
		public override void DetachElement()
		{
			base.DetachElement();
			this.rootElement.RemoveFromClassList(TreeView.itemUssClassName);
			this.m_Container.UnregisterCallback<PointerUpEvent>(this.m_PointerUpCallback, TrickleDown.NoTrickleDown);
			this.m_Toggle.UnregisterValueChangedCallback(this.m_ToggleValueChangedCallback);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00021AC0 File Offset: 0x0001FCC0
		public void Indent(int depth)
		{
			for (int i = 0; i < this.m_IndentContainer.childCount; i++)
			{
				this.m_IndentPool.Release(this.m_IndentContainer[i]);
			}
			this.m_IndentContainer.Clear();
			for (int j = 0; j < depth; j++)
			{
				VisualElement visualElement = this.m_IndentPool.Get();
				this.m_IndentContainer.Add(visualElement);
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00021B3B File Offset: 0x0001FD3B
		public void SetExpandedWithoutNotify(bool expanded)
		{
			this.m_Toggle.SetValueWithoutNotify(expanded);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00021B4B File Offset: 0x0001FD4B
		public void SetToggleVisibility(bool visible)
		{
			this.m_Toggle.visible = visible;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00021B5B File Offset: 0x0001FD5B
		private void OnPointerUp(PointerUpEvent evt)
		{
			Action<PointerUpEvent> action = this.onPointerUp;
			if (action != null)
			{
				action.Invoke(evt);
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00021B71 File Offset: 0x0001FD71
		private void OnToggleValueChanged(ChangeEvent<bool> evt)
		{
			Action<ChangeEvent<bool>> action = this.onToggleValueChanged;
			if (action != null)
			{
				action.Invoke(evt);
			}
		}

		// Token: 0x04000395 RID: 917
		private Toggle m_Toggle;

		// Token: 0x04000396 RID: 918
		private VisualElement m_Container;

		// Token: 0x04000397 RID: 919
		private VisualElement m_IndentContainer;

		// Token: 0x04000398 RID: 920
		private VisualElement m_BindableContainer;

		// Token: 0x0400039B RID: 923
		private ObjectPool<VisualElement> m_IndentPool = new ObjectPool<VisualElement>(delegate
		{
			VisualElement visualElement = new VisualElement();
			visualElement.AddToClassList(TreeView.itemIndentUssClassName);
			return visualElement;
		}, null, null, null, true, 10, 10000);

		// Token: 0x0400039C RID: 924
		protected EventCallback<PointerUpEvent> m_PointerUpCallback;

		// Token: 0x0400039D RID: 925
		protected EventCallback<ChangeEvent<bool>> m_ToggleValueChangedCallback;
	}
}
