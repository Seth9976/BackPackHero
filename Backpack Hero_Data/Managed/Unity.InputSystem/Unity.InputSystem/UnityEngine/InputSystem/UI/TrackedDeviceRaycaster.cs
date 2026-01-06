using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x0200008B RID: 139
	[AddComponentMenu("Event/Tracked Device Raycaster")]
	[RequireComponent(typeof(Canvas))]
	public class TrackedDeviceRaycaster : BaseRaycaster
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0003BBBC File Offset: 0x00039DBC
		public override Camera eventCamera
		{
			get
			{
				Canvas canvas = this.canvas;
				if (!(canvas != null))
				{
					return null;
				}
				return canvas.worldCamera;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0003BBE1 File Offset: 0x00039DE1
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x0003BBE9 File Offset: 0x00039DE9
		public LayerMask blockingMask
		{
			get
			{
				return this.m_BlockingMask;
			}
			set
			{
				this.m_BlockingMask = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0003BBF2 File Offset: 0x00039DF2
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x0003BBFA File Offset: 0x00039DFA
		public bool checkFor3DOcclusion
		{
			get
			{
				return this.m_CheckFor3DOcclusion;
			}
			set
			{
				this.m_CheckFor3DOcclusion = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0003BC03 File Offset: 0x00039E03
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x0003BC0B File Offset: 0x00039E0B
		public bool checkFor2DOcclusion
		{
			get
			{
				return this.m_CheckFor2DOcclusion;
			}
			set
			{
				this.m_CheckFor2DOcclusion = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0003BC14 File Offset: 0x00039E14
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0003BC1C File Offset: 0x00039E1C
		public bool ignoreReversedGraphics
		{
			get
			{
				return this.m_IgnoreReversedGraphics;
			}
			set
			{
				this.m_IgnoreReversedGraphics = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0003BC25 File Offset: 0x00039E25
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x0003BC2D File Offset: 0x00039E2D
		public float maxDistance
		{
			get
			{
				return this.m_MaxDistance;
			}
			set
			{
				this.m_MaxDistance = value;
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0003BC36 File Offset: 0x00039E36
		protected override void OnEnable()
		{
			base.OnEnable();
			TrackedDeviceRaycaster.s_Instances.AppendWithCapacity(this, 10);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0003BC4C File Offset: 0x00039E4C
		protected override void OnDisable()
		{
			int num = TrackedDeviceRaycaster.s_Instances.IndexOfReference(this);
			if (num != -1)
			{
				TrackedDeviceRaycaster.s_Instances.RemoveAtByMovingTailWithCapacity(num);
			}
			base.OnDisable();
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0003BC7C File Offset: 0x00039E7C
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			ExtendedPointerEventData extendedPointerEventData = eventData as ExtendedPointerEventData;
			if (extendedPointerEventData != null && extendedPointerEventData.pointerType == UIPointerType.Tracked)
			{
				this.PerformRaycast(extendedPointerEventData, resultAppendList);
			}
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0003BCA4 File Offset: 0x00039EA4
		internal void PerformRaycast(ExtendedPointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.canvas == null)
			{
				return;
			}
			if (this.eventCamera == null)
			{
				return;
			}
			Ray ray = new Ray(eventData.trackedDevicePosition, eventData.trackedDeviceOrientation * Vector3.forward);
			float num = this.m_MaxDistance;
			RaycastHit raycastHit;
			if (this.m_CheckFor3DOcclusion && Physics.Raycast(ray, out raycastHit, num, this.m_BlockingMask))
			{
				num = raycastHit.distance;
			}
			if (this.m_CheckFor2DOcclusion)
			{
				float num2 = num;
				RaycastHit2D rayIntersection = Physics2D.GetRayIntersection(ray, num2, this.m_BlockingMask);
				if (rayIntersection.collider != null)
				{
					num = rayIntersection.distance;
				}
			}
			this.m_RaycastResultsCache.Clear();
			this.SortedRaycastGraphics(this.canvas, ray, this.m_RaycastResultsCache);
			for (int i = 0; i < this.m_RaycastResultsCache.Count; i++)
			{
				bool flag = true;
				TrackedDeviceRaycaster.RaycastHitData raycastHitData = this.m_RaycastResultsCache[i];
				GameObject gameObject = raycastHitData.graphic.gameObject;
				if (this.m_IgnoreReversedGraphics)
				{
					Vector3 direction = ray.direction;
					Vector3 vector = gameObject.transform.rotation * Vector3.forward;
					flag = Vector3.Dot(direction, vector) > 0f;
				}
				flag &= raycastHitData.distance < num;
				if (flag)
				{
					RaycastResult raycastResult = new RaycastResult
					{
						gameObject = gameObject,
						module = this,
						distance = raycastHitData.distance,
						index = (float)resultAppendList.Count,
						depth = raycastHitData.graphic.depth,
						worldPosition = raycastHitData.worldHitPosition,
						screenPosition = raycastHitData.screenPosition
					};
					resultAppendList.Add(raycastResult);
				}
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0003BE68 File Offset: 0x0003A068
		private void SortedRaycastGraphics(Canvas canvas, Ray ray, List<TrackedDeviceRaycaster.RaycastHitData> results)
		{
			IList<Graphic> graphicsForCanvas = GraphicRegistry.GetGraphicsForCanvas(canvas);
			TrackedDeviceRaycaster.s_SortedGraphics.Clear();
			for (int i = 0; i < graphicsForCanvas.Count; i++)
			{
				Graphic graphic = graphicsForCanvas[i];
				Vector3 vector;
				float num;
				if (graphic.depth != -1 && TrackedDeviceRaycaster.RayIntersectsRectTransform(graphic.rectTransform, ray, out vector, out num))
				{
					Vector2 vector2 = this.eventCamera.WorldToScreenPoint(vector);
					if (graphic.Raycast(vector2, this.eventCamera))
					{
						TrackedDeviceRaycaster.s_SortedGraphics.Add(new TrackedDeviceRaycaster.RaycastHitData(graphic, vector, vector2, num));
					}
				}
			}
			TrackedDeviceRaycaster.s_SortedGraphics.Sort((TrackedDeviceRaycaster.RaycastHitData g1, TrackedDeviceRaycaster.RaycastHitData g2) => g2.graphic.depth.CompareTo(g1.graphic.depth));
			results.AddRange(TrackedDeviceRaycaster.s_SortedGraphics);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0003BF28 File Offset: 0x0003A128
		private static bool RayIntersectsRectTransform(RectTransform transform, Ray ray, out Vector3 worldPosition, out float distance)
		{
			Vector3[] array = new Vector3[4];
			transform.GetWorldCorners(array);
			Plane plane = new Plane(array[0], array[1], array[2]);
			float num;
			if (plane.Raycast(ray, out num))
			{
				Vector3 point = ray.GetPoint(num);
				Vector3 vector = array[3] - array[0];
				Vector3 vector2 = array[1] - array[0];
				float num2 = Vector3.Dot(point - array[0], vector);
				if (Vector3.Dot(point - array[0], vector2) >= 0f && num2 >= 0f)
				{
					Vector3 vector3 = array[1] - array[2];
					Vector3 vector4 = array[3] - array[2];
					float num3 = Vector3.Dot(point - array[2], vector3);
					float num4 = Vector3.Dot(point - array[2], vector4);
					if (num3 >= 0f && num4 >= 0f)
					{
						worldPosition = point;
						distance = num;
						return true;
					}
				}
			}
			worldPosition = Vector3.zero;
			distance = 0f;
			return false;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0003C05E File Offset: 0x0003A25E
		private Canvas canvas
		{
			get
			{
				if (this.m_Canvas != null)
				{
					return this.m_Canvas;
				}
				this.m_Canvas = base.GetComponent<Canvas>();
				return this.m_Canvas;
			}
		}

		// Token: 0x040003F1 RID: 1009
		[NonSerialized]
		private List<TrackedDeviceRaycaster.RaycastHitData> m_RaycastResultsCache = new List<TrackedDeviceRaycaster.RaycastHitData>();

		// Token: 0x040003F2 RID: 1010
		internal static InlinedArray<TrackedDeviceRaycaster> s_Instances;

		// Token: 0x040003F3 RID: 1011
		private static readonly List<TrackedDeviceRaycaster.RaycastHitData> s_SortedGraphics = new List<TrackedDeviceRaycaster.RaycastHitData>();

		// Token: 0x040003F4 RID: 1012
		[FormerlySerializedAs("ignoreReversedGraphics")]
		[SerializeField]
		private bool m_IgnoreReversedGraphics;

		// Token: 0x040003F5 RID: 1013
		[FormerlySerializedAs("checkFor2DOcclusion")]
		[SerializeField]
		private bool m_CheckFor2DOcclusion;

		// Token: 0x040003F6 RID: 1014
		[FormerlySerializedAs("checkFor3DOcclusion")]
		[SerializeField]
		private bool m_CheckFor3DOcclusion;

		// Token: 0x040003F7 RID: 1015
		[Tooltip("Maximum distance (in 3D world space) that rays are traced to find a hit.")]
		[SerializeField]
		private float m_MaxDistance = 1000f;

		// Token: 0x040003F8 RID: 1016
		[SerializeField]
		private LayerMask m_BlockingMask;

		// Token: 0x040003F9 RID: 1017
		[NonSerialized]
		private Canvas m_Canvas;

		// Token: 0x020001CC RID: 460
		private struct RaycastHitData
		{
			// Token: 0x06001418 RID: 5144 RVA: 0x0005CA46 File Offset: 0x0005AC46
			public RaycastHitData(Graphic graphic, Vector3 worldHitPosition, Vector2 screenPosition, float distance)
			{
				this.graphic = graphic;
				this.worldHitPosition = worldHitPosition;
				this.screenPosition = screenPosition;
				this.distance = distance;
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x06001419 RID: 5145 RVA: 0x0005CA65 File Offset: 0x0005AC65
			public readonly Graphic graphic { get; }

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x0600141A RID: 5146 RVA: 0x0005CA6D File Offset: 0x0005AC6D
			public readonly Vector3 worldHitPosition { get; }

			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x0600141B RID: 5147 RVA: 0x0005CA75 File Offset: 0x0005AC75
			public readonly Vector2 screenPosition { get; }

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x0600141C RID: 5148 RVA: 0x0005CA7D File Offset: 0x0005AC7D
			public readonly float distance { get; }
		}
	}
}
