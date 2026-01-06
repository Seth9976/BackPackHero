using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000012 RID: 18
	[AddComponentMenu("Event/Graphic Raycaster")]
	[RequireComponent(typeof(Canvas))]
	public class GraphicRaycaster : BaseRaycaster
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005FB4 File Offset: 0x000041B4
		public override int sortOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.sortingOrder;
				}
				return base.sortOrderPriority;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005FD5 File Offset: 0x000041D5
		public override int renderOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.rootCanvas.renderOrder;
				}
				return base.renderOrderPriority;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005FFB File Offset: 0x000041FB
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00006003 File Offset: 0x00004203
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000600C File Offset: 0x0000420C
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00006014 File Offset: 0x00004214
		public GraphicRaycaster.BlockingObjects blockingObjects
		{
			get
			{
				return this.m_BlockingObjects;
			}
			set
			{
				this.m_BlockingObjects = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000601D File Offset: 0x0000421D
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00006025 File Offset: 0x00004225
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

		// Token: 0x060000FC RID: 252 RVA: 0x0000602E File Offset: 0x0000422E
		protected GraphicRaycaster()
		{
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006054 File Offset: 0x00004254
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

		// Token: 0x060000FE RID: 254 RVA: 0x00006080 File Offset: 0x00004280
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.canvas == null)
			{
				return;
			}
			IList<Graphic> raycastableGraphicsForCanvas = GraphicRegistry.GetRaycastableGraphicsForCanvas(this.canvas);
			if (raycastableGraphicsForCanvas == null || raycastableGraphicsForCanvas.Count == 0)
			{
				return;
			}
			Camera eventCamera = this.eventCamera;
			int num;
			if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay || eventCamera == null)
			{
				num = this.canvas.targetDisplay;
			}
			else
			{
				num = eventCamera.targetDisplay;
			}
			Vector3 vector = MultipleDisplayUtilities.RelativeMouseAtScaled(eventData.position);
			if (vector != Vector3.zero)
			{
				if ((int)vector.z != num)
				{
					return;
				}
			}
			else
			{
				vector = eventData.position;
			}
			Vector2 vector2;
			if (eventCamera == null)
			{
				float num2 = (float)Screen.width;
				float num3 = (float)Screen.height;
				if (num > 0 && num < Display.displays.Length)
				{
					num2 = (float)Display.displays[num].systemWidth;
					num3 = (float)Display.displays[num].systemHeight;
				}
				vector2 = new Vector2(vector.x / num2, vector.y / num3);
			}
			else
			{
				vector2 = eventCamera.ScreenToViewportPoint(vector);
			}
			if (vector2.x < 0f || vector2.x > 1f || vector2.y < 0f || vector2.y > 1f)
			{
				return;
			}
			float num4 = float.MaxValue;
			Ray ray = default(Ray);
			if (eventCamera != null)
			{
				ray = eventCamera.ScreenPointToRay(vector);
			}
			if (this.canvas.renderMode != RenderMode.ScreenSpaceOverlay && this.blockingObjects != GraphicRaycaster.BlockingObjects.None)
			{
				float num5 = 100f;
				if (eventCamera != null)
				{
					float z = ray.direction.z;
					num5 = (Mathf.Approximately(0f, z) ? float.PositiveInfinity : Mathf.Abs((eventCamera.farClipPlane - eventCamera.nearClipPlane) / z));
				}
				if ((this.blockingObjects == GraphicRaycaster.BlockingObjects.ThreeD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All) && ReflectionMethodsCache.Singleton.raycast3D != null)
				{
					RaycastHit[] array = ReflectionMethodsCache.Singleton.raycast3DAll(ray, num5, this.m_BlockingMask);
					if (array.Length != 0)
					{
						num4 = array[0].distance;
					}
				}
				if ((this.blockingObjects == GraphicRaycaster.BlockingObjects.TwoD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All) && ReflectionMethodsCache.Singleton.raycast2D != null)
				{
					RaycastHit2D[] array2 = ReflectionMethodsCache.Singleton.getRayIntersectionAll(ray, num5, this.m_BlockingMask);
					if (array2.Length != 0)
					{
						num4 = array2[0].distance;
					}
				}
			}
			this.m_RaycastResults.Clear();
			GraphicRaycaster.Raycast(this.canvas, eventCamera, vector, raycastableGraphicsForCanvas, this.m_RaycastResults);
			int count = this.m_RaycastResults.Count;
			for (int i = 0; i < count; i++)
			{
				GameObject gameObject = this.m_RaycastResults[i].gameObject;
				bool flag = true;
				if (this.ignoreReversedGraphics)
				{
					if (eventCamera == null)
					{
						Vector3 vector3 = gameObject.transform.rotation * Vector3.forward;
						flag = Vector3.Dot(Vector3.forward, vector3) > 0f;
					}
					else
					{
						Vector3 vector4 = eventCamera.transform.rotation * Vector3.forward * eventCamera.nearClipPlane;
						flag = Vector3.Dot(gameObject.transform.position - eventCamera.transform.position - vector4, gameObject.transform.forward) >= 0f;
					}
				}
				if (flag)
				{
					Transform transform = gameObject.transform;
					Vector3 forward = transform.forward;
					float num6;
					if (eventCamera == null || this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
					{
						num6 = 0f;
					}
					else
					{
						num6 = Vector3.Dot(forward, transform.position - ray.origin) / Vector3.Dot(forward, ray.direction);
						if (num6 < 0f)
						{
							goto IL_048B;
						}
					}
					if (num6 < num4)
					{
						RaycastResult raycastResult = new RaycastResult
						{
							gameObject = gameObject,
							module = this,
							distance = num6,
							screenPosition = vector,
							displayIndex = num,
							index = (float)resultAppendList.Count,
							depth = this.m_RaycastResults[i].depth,
							sortingLayer = this.canvas.sortingLayerID,
							sortingOrder = this.canvas.sortingOrder,
							worldPosition = ray.origin + ray.direction * num6,
							worldNormal = -forward
						};
						resultAppendList.Add(raycastResult);
					}
				}
				IL_048B:;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006528 File Offset: 0x00004728
		public override Camera eventCamera
		{
			get
			{
				Canvas canvas = this.canvas;
				RenderMode renderMode = canvas.renderMode;
				if (renderMode == RenderMode.ScreenSpaceOverlay || (renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null))
				{
					return null;
				}
				return canvas.worldCamera ?? Camera.main;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000656C File Offset: 0x0000476C
		private static void Raycast(Canvas canvas, Camera eventCamera, Vector2 pointerPosition, IList<Graphic> foundGraphics, List<Graphic> results)
		{
			int num = foundGraphics.Count;
			for (int i = 0; i < num; i++)
			{
				Graphic graphic = foundGraphics[i];
				if (graphic.raycastTarget && !graphic.canvasRenderer.cull && graphic.depth != -1 && RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, pointerPosition, eventCamera, graphic.raycastPadding) && (!(eventCamera != null) || eventCamera.WorldToScreenPoint(graphic.rectTransform.position).z <= eventCamera.farClipPlane) && graphic.Raycast(pointerPosition, eventCamera))
				{
					GraphicRaycaster.s_SortedGraphics.Add(graphic);
				}
			}
			GraphicRaycaster.s_SortedGraphics.Sort((Graphic g1, Graphic g2) => g2.depth.CompareTo(g1.depth));
			num = GraphicRaycaster.s_SortedGraphics.Count;
			for (int j = 0; j < num; j++)
			{
				results.Add(GraphicRaycaster.s_SortedGraphics[j]);
			}
			GraphicRaycaster.s_SortedGraphics.Clear();
		}

		// Token: 0x04000065 RID: 101
		protected const int kNoEventMaskSet = -1;

		// Token: 0x04000066 RID: 102
		[FormerlySerializedAs("ignoreReversedGraphics")]
		[SerializeField]
		private bool m_IgnoreReversedGraphics = true;

		// Token: 0x04000067 RID: 103
		[FormerlySerializedAs("blockingObjects")]
		[SerializeField]
		private GraphicRaycaster.BlockingObjects m_BlockingObjects;

		// Token: 0x04000068 RID: 104
		[SerializeField]
		protected LayerMask m_BlockingMask = -1;

		// Token: 0x04000069 RID: 105
		private Canvas m_Canvas;

		// Token: 0x0400006A RID: 106
		[NonSerialized]
		private List<Graphic> m_RaycastResults = new List<Graphic>();

		// Token: 0x0400006B RID: 107
		[NonSerialized]
		private static readonly List<Graphic> s_SortedGraphics = new List<Graphic>();

		// Token: 0x02000082 RID: 130
		public enum BlockingObjects
		{
			// Token: 0x0400025A RID: 602
			None,
			// Token: 0x0400025B RID: 603
			TwoD,
			// Token: 0x0400025C RID: 604
			ThreeD,
			// Token: 0x0400025D RID: 605
			All
		}
	}
}
