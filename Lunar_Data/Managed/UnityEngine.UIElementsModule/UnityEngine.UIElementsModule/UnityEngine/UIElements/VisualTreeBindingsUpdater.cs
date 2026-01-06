using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x02000103 RID: 259
	internal class VisualTreeBindingsUpdater : BaseVisualTreeHierarchyTrackerUpdater
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001D16E File Offset: 0x0001B36E
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeBindingsUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0001D175 File Offset: 0x0001B375
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x0001D17C File Offset: 0x0001B37C
		public static bool disableBindingsThrottling { get; set; } = false;

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001D184 File Offset: 0x0001B384
		private IBinding GetBindingObjectFromElement(VisualElement ve)
		{
			IBindable bindable = ve as IBindable;
			bool flag = bindable != null;
			if (flag)
			{
				bool flag2 = bindable.binding != null;
				if (flag2)
				{
					return bindable.binding;
				}
			}
			return VisualTreeBindingsUpdater.GetAdditionalBinding(ve);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001D1C3 File Offset: 0x0001B3C3
		private void StartTracking(VisualElement ve)
		{
			this.m_ElementsToAdd.Add(ve);
			this.m_ElementsToRemove.Remove(ve);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001D1E0 File Offset: 0x0001B3E0
		private void StopTracking(VisualElement ve)
		{
			this.m_ElementsToRemove.Add(ve);
			this.m_ElementsToAdd.Remove(ve);
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0001D1FD File Offset: 0x0001B3FD
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0001D205 File Offset: 0x0001B405
		public Dictionary<object, object> temporaryObjectCache { get; private set; } = new Dictionary<object, object>();

		// Token: 0x060007FA RID: 2042 RVA: 0x0001D210 File Offset: 0x0001B410
		public static void SetAdditionalBinding(VisualElement ve, IBinding b)
		{
			IBinding additionalBinding = VisualTreeBindingsUpdater.GetAdditionalBinding(ve);
			if (additionalBinding != null)
			{
				additionalBinding.Release();
			}
			ve.SetProperty(VisualTreeBindingsUpdater.s_AdditionalBindingObjectVEPropertyName, b);
			ve.IncrementVersion(VersionChangeType.Bindings);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001D246 File Offset: 0x0001B446
		public static void ClearAdditionalBinding(VisualElement ve)
		{
			VisualTreeBindingsUpdater.SetAdditionalBinding(ve, null);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001D254 File Offset: 0x0001B454
		public static IBinding GetAdditionalBinding(VisualElement ve)
		{
			return ve.GetProperty(VisualTreeBindingsUpdater.s_AdditionalBindingObjectVEPropertyName) as IBinding;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001D278 File Offset: 0x0001B478
		public static void AddBindingRequest(VisualElement ve, IBindingRequest req)
		{
			List<IBindingRequest> list = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
			bool flag = list == null;
			if (flag)
			{
				list = ObjectListPool<IBindingRequest>.Get();
				ve.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, list);
			}
			list.Add(req);
			ve.IncrementVersion(VersionChangeType.Bindings);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001D2C4 File Offset: 0x0001B4C4
		public static void RemoveBindingRequest(VisualElement ve, IBindingRequest req)
		{
			List<IBindingRequest> list = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
			bool flag = list != null;
			if (flag)
			{
				req.Release();
				list.Remove(req);
				bool flag2 = list.Count == 0;
				if (flag2)
				{
					ObjectListPool<IBindingRequest>.Release(list);
					ve.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, null);
				}
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001D320 File Offset: 0x0001B520
		public static void ClearBindingRequests(VisualElement ve)
		{
			List<IBindingRequest> list = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
			bool flag = list != null;
			if (flag)
			{
				foreach (IBindingRequest bindingRequest in list)
				{
					bindingRequest.Release();
				}
				ObjectListPool<IBindingRequest>.Release(list);
				ve.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, null);
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001D3A4 File Offset: 0x0001B5A4
		private void StartTrackingRecursive(VisualElement ve)
		{
			IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(ve);
			bool flag = bindingObjectFromElement != null;
			if (flag)
			{
				this.StartTracking(ve);
			}
			object property = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
			bool flag2 = property != null;
			if (flag2)
			{
				this.m_ElementsToBind.Add(ve);
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				this.StartTrackingRecursive(visualElement);
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001D438 File Offset: 0x0001B638
		private void StopTrackingRecursive(VisualElement ve)
		{
			this.StopTracking(ve);
			object property = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
			bool flag = property != null;
			if (flag)
			{
				this.m_ElementsToBind.Remove(ve);
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				this.StopTrackingRecursive(visualElement);
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001D4B4 File Offset: 0x0001B6B4
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			base.OnVersionChanged(ve, versionChangeType);
			bool flag = (versionChangeType & VersionChangeType.Bindings) == VersionChangeType.Bindings;
			if (flag)
			{
				bool flag2 = this.GetBindingObjectFromElement(ve) != null;
				if (flag2)
				{
					this.StartTracking(ve);
				}
				else
				{
					this.StopTracking(ve);
				}
				object property = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
				bool flag3 = property != null;
				if (flag3)
				{
					this.m_ElementsToBind.Add(ve);
				}
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001D520 File Offset: 0x0001B720
		protected override void OnHierarchyChange(VisualElement ve, HierarchyChangeType type)
		{
			if (type != HierarchyChangeType.Add)
			{
				if (type == HierarchyChangeType.Remove)
				{
					this.StopTrackingRecursive(ve);
				}
			}
			else
			{
				this.StartTrackingRecursive(ve);
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001D554 File Offset: 0x0001B754
		private static long CurrentTime()
		{
			return Panel.TimeSinceStartupMs();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001D56C File Offset: 0x0001B76C
		public static bool ShouldThrottle(long startTime)
		{
			return !VisualTreeBindingsUpdater.disableBindingsThrottling && VisualTreeBindingsUpdater.CurrentTime() - startTime < 100L;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001D594 File Offset: 0x0001B794
		public void PerformTrackingOperations()
		{
			foreach (VisualElement visualElement in this.m_ElementsToAdd)
			{
				IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(visualElement);
				bool flag = bindingObjectFromElement != null;
				if (flag)
				{
					this.m_ElementsWithBindings.Add(visualElement);
				}
			}
			this.m_ElementsToAdd.Clear();
			foreach (VisualElement visualElement2 in this.m_ElementsToRemove)
			{
				this.m_ElementsWithBindings.Remove(visualElement2);
			}
			this.m_ElementsToRemove.Clear();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001D66C File Offset: 0x0001B86C
		public override void Update()
		{
			base.Update();
			bool flag = this.m_ElementsToBind.Count > 0;
			if (flag)
			{
				using (VisualTreeBindingsUpdater.s_ProfilerBindingRequestsMarker.Auto())
				{
					long num = VisualTreeBindingsUpdater.CurrentTime();
					while (this.m_ElementsToBind.Count > 0 && VisualTreeBindingsUpdater.CurrentTime() - num < 100L)
					{
						VisualElement visualElement = Enumerable.FirstOrDefault<VisualElement>(this.m_ElementsToBind);
						bool flag2 = visualElement != null;
						if (!flag2)
						{
							break;
						}
						this.m_ElementsToBind.Remove(visualElement);
						List<IBindingRequest> list = visualElement.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
						bool flag3 = list != null;
						if (flag3)
						{
							visualElement.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, null);
							foreach (IBindingRequest bindingRequest in list)
							{
								bindingRequest.Bind(visualElement);
							}
							ObjectListPool<IBindingRequest>.Release(list);
						}
					}
				}
			}
			this.PerformTrackingOperations();
			bool flag4 = this.m_ElementsWithBindings.Count > 0;
			if (flag4)
			{
				long num2 = VisualTreeBindingsUpdater.CurrentTime();
				bool flag5 = this.m_LastUpdateTime + 100L < num2;
				if (flag5)
				{
					this.UpdateBindings();
					this.m_LastUpdateTime = num2;
				}
			}
			bool flag6 = this.m_ElementsToBind.Count == 0;
			if (flag6)
			{
				this.temporaryObjectCache.Clear();
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001D814 File Offset: 0x0001BA14
		private void UpdateBindings()
		{
			VisualTreeBindingsUpdater.s_MarkerUpdate.Begin();
			foreach (VisualElement visualElement in this.m_ElementsWithBindings)
			{
				IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(visualElement);
				bool flag = bindingObjectFromElement == null || visualElement.elementPanel != base.panel;
				if (flag)
				{
					if (bindingObjectFromElement != null)
					{
						bindingObjectFromElement.Release();
					}
					this.StopTracking(visualElement);
				}
				else
				{
					this.updatedBindings.Add(bindingObjectFromElement);
				}
			}
			foreach (IBinding binding in this.updatedBindings)
			{
				binding.PreUpdate();
			}
			foreach (IBinding binding2 in this.updatedBindings)
			{
				binding2.Update();
			}
			this.updatedBindings.Clear();
			VisualTreeBindingsUpdater.s_MarkerUpdate.End();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001D964 File Offset: 0x0001BB64
		internal void PollElementsWithBindings(Action<VisualElement, IBinding> callback)
		{
			VisualTreeBindingsUpdater.s_MarkerPoll.Begin();
			this.PerformTrackingOperations();
			bool flag = this.m_ElementsWithBindings.Count > 0;
			if (flag)
			{
				foreach (VisualElement visualElement in this.m_ElementsWithBindings)
				{
					IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(visualElement);
					bool flag2 = bindingObjectFromElement == null || visualElement.elementPanel != base.panel;
					if (flag2)
					{
						if (bindingObjectFromElement != null)
						{
							bindingObjectFromElement.Release();
						}
						this.StopTracking(visualElement);
					}
					else
					{
						callback.Invoke(visualElement, bindingObjectFromElement);
					}
				}
			}
			VisualTreeBindingsUpdater.s_MarkerPoll.End();
		}

		// Token: 0x0400034C RID: 844
		private static readonly PropertyName s_BindingRequestObjectVEPropertyName = "__unity-binding-request-object";

		// Token: 0x0400034D RID: 845
		private static readonly PropertyName s_AdditionalBindingObjectVEPropertyName = "__unity-additional-binding-object";

		// Token: 0x0400034E RID: 846
		private static readonly string s_Description = "Update Bindings";

		// Token: 0x0400034F RID: 847
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeBindingsUpdater.s_Description);

		// Token: 0x04000350 RID: 848
		private static readonly ProfilerMarker s_ProfilerBindingRequestsMarker = new ProfilerMarker("Bindings.Requests");

		// Token: 0x04000351 RID: 849
		private static ProfilerMarker s_MarkerUpdate = new ProfilerMarker("Bindings.Update");

		// Token: 0x04000352 RID: 850
		private static ProfilerMarker s_MarkerPoll = new ProfilerMarker("Bindings.PollElementsWithBindings");

		// Token: 0x04000354 RID: 852
		private readonly HashSet<VisualElement> m_ElementsWithBindings = new HashSet<VisualElement>();

		// Token: 0x04000355 RID: 853
		private readonly HashSet<VisualElement> m_ElementsToAdd = new HashSet<VisualElement>();

		// Token: 0x04000356 RID: 854
		private readonly HashSet<VisualElement> m_ElementsToRemove = new HashSet<VisualElement>();

		// Token: 0x04000357 RID: 855
		private const int k_MinUpdateDelayMs = 100;

		// Token: 0x04000358 RID: 856
		private const int k_MaxBindingTimeMs = 100;

		// Token: 0x04000359 RID: 857
		private long m_LastUpdateTime = 0L;

		// Token: 0x0400035A RID: 858
		private HashSet<VisualElement> m_ElementsToBind = new HashSet<VisualElement>();

		// Token: 0x0400035C RID: 860
		private List<IBinding> updatedBindings = new List<IBinding>();

		// Token: 0x02000104 RID: 260
		private class RequestObjectListPool : ObjectListPool<IBindingRequest>
		{
		}
	}
}
