using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000071 RID: 113
	[AddComponentMenu("Event/Physics Raycaster")]
	[RequireComponent(typeof(Camera))]
	public class PhysicsRaycaster : BaseRaycaster
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x0001B49F File Offset: 0x0001969F
		protected PhysicsRaycaster()
		{
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001B4B3 File Offset: 0x000196B3
		public override Camera eventCamera
		{
			get
			{
				if (this.m_EventCamera == null)
				{
					this.m_EventCamera = base.GetComponent<Camera>();
				}
				return this.m_EventCamera ?? Camera.main;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001B4DE File Offset: 0x000196DE
		public virtual int depth
		{
			get
			{
				if (!(this.eventCamera != null))
				{
					return 16777215;
				}
				return (int)this.eventCamera.depth;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001B500 File Offset: 0x00019700
		public int finalEventMask
		{
			get
			{
				if (!(this.eventCamera != null))
				{
					return -1;
				}
				return this.eventCamera.cullingMask & this.m_EventMask;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001B529 File Offset: 0x00019729
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0001B531 File Offset: 0x00019731
		public LayerMask eventMask
		{
			get
			{
				return this.m_EventMask;
			}
			set
			{
				this.m_EventMask = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001B53A File Offset: 0x0001973A
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x0001B542 File Offset: 0x00019742
		public int maxRayIntersections
		{
			get
			{
				return this.m_MaxRayIntersections;
			}
			set
			{
				this.m_MaxRayIntersections = value;
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001B54C File Offset: 0x0001974C
		protected bool ComputeRayAndDistance(PointerEventData eventData, ref Ray ray, ref int eventDisplayIndex, ref float distanceToClipPlane)
		{
			if (this.eventCamera == null)
			{
				return false;
			}
			Vector3 vector = MultipleDisplayUtilities.RelativeMouseAtScaled(eventData.position);
			if (vector != Vector3.zero)
			{
				eventDisplayIndex = (int)vector.z;
				if (eventDisplayIndex != this.eventCamera.targetDisplay)
				{
					return false;
				}
			}
			else
			{
				vector = eventData.position;
			}
			if (!this.eventCamera.pixelRect.Contains(vector))
			{
				return false;
			}
			ray = this.eventCamera.ScreenPointToRay(vector);
			float z = ray.direction.z;
			distanceToClipPlane = (Mathf.Approximately(0f, z) ? float.PositiveInfinity : Mathf.Abs((this.eventCamera.farClipPlane - this.eventCamera.nearClipPlane) / z));
			return true;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001B614 File Offset: 0x00019814
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			Ray ray = default(Ray);
			int num = 0;
			float num2 = 0f;
			if (!this.ComputeRayAndDistance(eventData, ref ray, ref num, ref num2))
			{
				return;
			}
			int num3;
			if (this.m_MaxRayIntersections == 0)
			{
				if (ReflectionMethodsCache.Singleton.raycast3DAll == null)
				{
					return;
				}
				this.m_Hits = ReflectionMethodsCache.Singleton.raycast3DAll(ray, num2, this.finalEventMask);
				num3 = this.m_Hits.Length;
			}
			else
			{
				if (ReflectionMethodsCache.Singleton.getRaycastNonAlloc == null)
				{
					return;
				}
				if (this.m_LastMaxRayIntersections != this.m_MaxRayIntersections)
				{
					this.m_Hits = new RaycastHit[this.m_MaxRayIntersections];
					this.m_LastMaxRayIntersections = this.m_MaxRayIntersections;
				}
				num3 = ReflectionMethodsCache.Singleton.getRaycastNonAlloc(ray, this.m_Hits, num2, this.finalEventMask);
			}
			if (num3 != 0)
			{
				if (num3 > 1)
				{
					Array.Sort<RaycastHit>(this.m_Hits, 0, num3, PhysicsRaycaster.RaycastHitComparer.instance);
				}
				int i = 0;
				int num4 = num3;
				while (i < num4)
				{
					RaycastResult raycastResult = new RaycastResult
					{
						gameObject = this.m_Hits[i].collider.gameObject,
						module = this,
						distance = this.m_Hits[i].distance,
						worldPosition = this.m_Hits[i].point,
						worldNormal = this.m_Hits[i].normal,
						screenPosition = eventData.position,
						displayIndex = num,
						index = (float)resultAppendList.Count,
						sortingLayer = 0,
						sortingOrder = 0
					};
					resultAppendList.Add(raycastResult);
					i++;
				}
			}
		}

		// Token: 0x0400022B RID: 555
		protected const int kNoEventMaskSet = -1;

		// Token: 0x0400022C RID: 556
		protected Camera m_EventCamera;

		// Token: 0x0400022D RID: 557
		[SerializeField]
		protected LayerMask m_EventMask = -1;

		// Token: 0x0400022E RID: 558
		[SerializeField]
		protected int m_MaxRayIntersections;

		// Token: 0x0400022F RID: 559
		protected int m_LastMaxRayIntersections;

		// Token: 0x04000230 RID: 560
		private RaycastHit[] m_Hits;

		// Token: 0x020000CB RID: 203
		private class RaycastHitComparer : IComparer<RaycastHit>
		{
			// Token: 0x06000757 RID: 1879 RVA: 0x0001C90C File Offset: 0x0001AB0C
			public int Compare(RaycastHit x, RaycastHit y)
			{
				return x.distance.CompareTo(y.distance);
			}

			// Token: 0x0400034F RID: 847
			public static PhysicsRaycaster.RaycastHitComparer instance = new PhysicsRaycaster.RaycastHitComparer();
		}
	}
}
