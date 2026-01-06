using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000026 RID: 38
	public class LayoutRebuilder : ICanvasElement
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000EA71 File Offset: 0x0000CC71
		private void Initialize(RectTransform controller)
		{
			this.m_ToRebuild = controller;
			this.m_CachedHashFromTransform = controller.GetHashCode();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000EA86 File Offset: 0x0000CC86
		private void Clear()
		{
			this.m_ToRebuild = null;
			this.m_CachedHashFromTransform = 0;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000EA98 File Offset: 0x0000CC98
		static LayoutRebuilder()
		{
			RectTransform.reapplyDrivenProperties += LayoutRebuilder.ReapplyDrivenProperties;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000EAEA File Offset: 0x0000CCEA
		private static void ReapplyDrivenProperties(RectTransform driven)
		{
			LayoutRebuilder.MarkLayoutForRebuild(driven);
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000EAF2 File Offset: 0x0000CCF2
		public Transform transform
		{
			get
			{
				return this.m_ToRebuild;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000EAFA File Offset: 0x0000CCFA
		public bool IsDestroyed()
		{
			return this.m_ToRebuild == null;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000EB08 File Offset: 0x0000CD08
		private static void StripDisabledBehavioursFromList(List<Component> components)
		{
			components.RemoveAll((Component e) => e is Behaviour && !((Behaviour)e).isActiveAndEnabled);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public static void ForceRebuildLayoutImmediate(RectTransform layoutRoot)
		{
			LayoutRebuilder layoutRebuilder = LayoutRebuilder.s_Rebuilders.Get();
			layoutRebuilder.Initialize(layoutRoot);
			layoutRebuilder.Rebuild(CanvasUpdate.Layout);
			LayoutRebuilder.s_Rebuilders.Release(layoutRebuilder);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000EB64 File Offset: 0x0000CD64
		public void Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Layout)
			{
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputHorizontal();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutHorizontal();
				});
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputVertical();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutVertical();
				});
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000EC24 File Offset: 0x0000CE24
		private void PerformLayoutControl(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			rect.GetComponents(typeof(ILayoutController), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] is ILayoutSelfController)
					{
						action(list[i]);
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (!(list[j] is ILayoutSelfController))
					{
						Component component = list[j];
						if (component && component is ScrollRect)
						{
							if (((ScrollRect)component).content != rect)
							{
								action(list[j]);
							}
						}
						else
						{
							action(list[j]);
						}
					}
				}
				for (int k = 0; k < rect.childCount; k++)
				{
					this.PerformLayoutControl(rect.GetChild(k) as RectTransform, action);
				}
			}
			CollectionPool<List<Component>, Component>.Release(list);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000ED28 File Offset: 0x0000CF28
		private void PerformLayoutCalculation(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0 || rect.GetComponent(typeof(ILayoutGroup)))
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					this.PerformLayoutCalculation(rect.GetChild(i) as RectTransform, action);
				}
				for (int j = 0; j < list.Count; j++)
				{
					action(list[j]);
				}
			}
			CollectionPool<List<Component>, Component>.Release(list);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
		public static void MarkLayoutForRebuild(RectTransform rect)
		{
			if (rect == null || rect.gameObject == null)
			{
				return;
			}
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			bool flag = true;
			RectTransform rectTransform = rect;
			RectTransform rectTransform2 = rectTransform.parent as RectTransform;
			while (flag && !(rectTransform2 == null) && !(rectTransform2.gameObject == null))
			{
				flag = false;
				rectTransform2.GetComponents(typeof(ILayoutGroup), list);
				for (int i = 0; i < list.Count; i++)
				{
					Component component = list[i];
					if (component != null && component is Behaviour && ((Behaviour)component).isActiveAndEnabled)
					{
						flag = true;
						rectTransform = rectTransform2;
						break;
					}
				}
				rectTransform2 = rectTransform2.parent as RectTransform;
			}
			if (rectTransform == rect && !LayoutRebuilder.ValidController(rectTransform, list))
			{
				CollectionPool<List<Component>, Component>.Release(list);
				return;
			}
			LayoutRebuilder.MarkLayoutRootForRebuild(rectTransform);
			CollectionPool<List<Component>, Component>.Release(list);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
		private static bool ValidController(RectTransform layoutRoot, List<Component> comps)
		{
			if (layoutRoot == null || layoutRoot.gameObject == null)
			{
				return false;
			}
			layoutRoot.GetComponents(typeof(ILayoutController), comps);
			for (int i = 0; i < comps.Count; i++)
			{
				Component component = comps[i];
				if (component != null && component is Behaviour && ((Behaviour)component).isActiveAndEnabled)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000EF1C File Offset: 0x0000D11C
		private static void MarkLayoutRootForRebuild(RectTransform controller)
		{
			if (controller == null)
			{
				return;
			}
			LayoutRebuilder layoutRebuilder = LayoutRebuilder.s_Rebuilders.Get();
			layoutRebuilder.Initialize(controller);
			if (!CanvasUpdateRegistry.TryRegisterCanvasElementForLayoutRebuild(layoutRebuilder))
			{
				LayoutRebuilder.s_Rebuilders.Release(layoutRebuilder);
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000EF58 File Offset: 0x0000D158
		public void LayoutComplete()
		{
			LayoutRebuilder.s_Rebuilders.Release(this);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000EF65 File Offset: 0x0000D165
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000EF67 File Offset: 0x0000D167
		public override int GetHashCode()
		{
			return this.m_CachedHashFromTransform;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000EF6F File Offset: 0x0000D16F
		public override bool Equals(object obj)
		{
			return obj.GetHashCode() == this.GetHashCode();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000EF7F File Offset: 0x0000D17F
		public override string ToString()
		{
			string text = "(Layout Rebuilder for) ";
			RectTransform toRebuild = this.m_ToRebuild;
			return text + ((toRebuild != null) ? toRebuild.ToString() : null);
		}

		// Token: 0x040000F3 RID: 243
		private RectTransform m_ToRebuild;

		// Token: 0x040000F4 RID: 244
		private int m_CachedHashFromTransform;

		// Token: 0x040000F5 RID: 245
		private static ObjectPool<LayoutRebuilder> s_Rebuilders = new ObjectPool<LayoutRebuilder>(() => new LayoutRebuilder(), null, delegate(LayoutRebuilder x)
		{
			x.Clear();
		}, null, true, 10, 10000);
	}
}
