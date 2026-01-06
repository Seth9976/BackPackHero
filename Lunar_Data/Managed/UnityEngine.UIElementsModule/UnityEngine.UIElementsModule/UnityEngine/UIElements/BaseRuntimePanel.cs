using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005E RID: 94
	internal abstract class BaseRuntimePanel : Panel
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00009EA3 File Offset: 0x000080A3
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00009EAC File Offset: 0x000080AC
		public GameObject selectableGameObject
		{
			get
			{
				return this.m_SelectableGameObject;
			}
			set
			{
				bool flag = this.m_SelectableGameObject != value;
				if (flag)
				{
					this.AssignPanelToComponents(null);
					this.m_SelectableGameObject = value;
					this.AssignPanelToComponents(this);
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00009EE3 File Offset: 0x000080E3
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00009EEC File Offset: 0x000080EC
		public float sortingPriority
		{
			get
			{
				return this.m_SortingPriority;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_SortingPriority, value);
				if (flag)
				{
					this.m_SortingPriority = value;
					bool flag2 = this.contextType == ContextType.Player;
					if (flag2)
					{
						UIElementsRuntimeUtility.SetPanelOrderingDirty();
					}
				}
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060002A5 RID: 677 RVA: 0x00009F2C File Offset: 0x0000812C
		// (remove) Token: 0x060002A6 RID: 678 RVA: 0x00009F64 File Offset: 0x00008164
		[field: DebuggerBrowsable(0)]
		public event Action destroyed;

		// Token: 0x060002A7 RID: 679 RVA: 0x00009F9C File Offset: 0x0000819C
		protected BaseRuntimePanel(ScriptableObject ownerObject, EventDispatcher dispatcher = null)
			: base(ownerObject, ContextType.Player, dispatcher)
		{
			this.m_RuntimePanelCreationIndex = BaseRuntimePanel.s_CurrentRuntimePanelCounter++;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009FF0 File Offset: 0x000081F0
		protected override void Dispose(bool disposing)
		{
			bool disposed = base.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					Action action = this.destroyed;
					if (action != null)
					{
						action.Invoke();
					}
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000A02C File Offset: 0x0000822C
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000A044 File Offset: 0x00008244
		internal override Shader standardWorldSpaceShader
		{
			get
			{
				return this.m_StandardWorldSpaceShader;
			}
			set
			{
				bool flag = this.m_StandardWorldSpaceShader != value;
				if (flag)
				{
					this.m_StandardWorldSpaceShader = value;
					base.InvokeStandardWorldSpaceShaderChanged();
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A074 File Offset: 0x00008274
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000A08C File Offset: 0x0000828C
		internal bool drawToCameras
		{
			get
			{
				return this.m_DrawToCameras;
			}
			set
			{
				bool flag = this.m_DrawToCameras != value;
				if (flag)
				{
					this.m_DrawToCameras = value;
					UIRRepaintUpdater uirrepaintUpdater = this.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
					if (uirrepaintUpdater != null)
					{
						uirrepaintUpdater.DestroyRenderChain();
					}
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000A0CB File Offset: 0x000082CB
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000A0D3 File Offset: 0x000082D3
		internal int targetDisplay { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000A0DC File Offset: 0x000082DC
		internal int screenRenderingWidth
		{
			get
			{
				return (this.targetDisplay > 0 && this.targetDisplay < Display.displays.Length) ? Display.displays[this.targetDisplay].renderingWidth : Screen.width;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000A10E File Offset: 0x0000830E
		internal int screenRenderingHeight
		{
			get
			{
				return (this.targetDisplay > 0 && this.targetDisplay < Display.displays.Length) ? Display.displays[this.targetDisplay].renderingHeight : Screen.height;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A140 File Offset: 0x00008340
		public override void Repaint(Event e)
		{
			bool flag = this.targetTexture == null;
			if (flag)
			{
				RenderTexture active = RenderTexture.active;
				int num = ((active != null) ? active.width : this.screenRenderingWidth);
				int num2 = ((active != null) ? active.height : this.screenRenderingHeight);
				GL.Viewport(new Rect(0f, 0f, (float)num, (float)num2));
				base.Repaint(e);
			}
			else
			{
				RenderTexture active2 = RenderTexture.active;
				RenderTexture.active = this.targetTexture;
				GL.Viewport(new Rect(0f, 0f, (float)this.targetTexture.width, (float)this.targetTexture.height));
				base.Repaint(e);
				RenderTexture.active = active2;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000A208 File Offset: 0x00008408
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000A210 File Offset: 0x00008410
		public Func<Vector2, Vector2> screenToPanelSpace
		{
			get
			{
				return this.m_ScreenToPanelSpace;
			}
			set
			{
				this.m_ScreenToPanelSpace = value ?? BaseRuntimePanel.DefaultScreenToPanelSpace;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A224 File Offset: 0x00008424
		internal Vector2 ScreenToPanel(Vector2 screen)
		{
			return this.screenToPanelSpace.Invoke(screen) / base.scale;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A250 File Offset: 0x00008450
		internal bool ScreenToPanel(Vector2 screenPosition, Vector2 screenDelta, out Vector2 panelPosition, out Vector2 panelDelta, bool allowOutside = false)
		{
			panelPosition = this.ScreenToPanel(screenPosition);
			bool flag = !allowOutside;
			Vector2 vector;
			if (flag)
			{
				Rect layout = this.visualTree.layout;
				bool flag2 = !layout.Contains(panelPosition);
				if (flag2)
				{
					panelDelta = screenDelta;
					return false;
				}
				vector = this.ScreenToPanel(screenPosition - screenDelta);
				bool flag3 = !layout.Contains(vector);
				if (flag3)
				{
					panelDelta = screenDelta;
					return true;
				}
			}
			else
			{
				vector = this.ScreenToPanel(screenPosition - screenDelta);
			}
			panelDelta = panelPosition - vector;
			return true;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000A300 File Offset: 0x00008500
		private void AssignPanelToComponents(BaseRuntimePanel panel)
		{
			bool flag = this.selectableGameObject == null;
			if (!flag)
			{
				List<IRuntimePanelComponent> list = ObjectListPool<IRuntimePanelComponent>.Get();
				try
				{
					this.selectableGameObject.GetComponents<IRuntimePanelComponent>(list);
					foreach (IRuntimePanelComponent runtimePanelComponent in list)
					{
						runtimePanelComponent.panel = panel;
					}
				}
				finally
				{
					ObjectListPool<IRuntimePanelComponent>.Release(list);
				}
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000A394 File Offset: 0x00008594
		internal void PointerLeavesPanel(int pointerId, Vector2 position)
		{
			base.ClearCachedElementUnderPointer(pointerId, null);
			base.CommitElementUnderPointers();
			PointerDeviceState.SavePointerPosition(pointerId, position, null, this.contextType);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000A3B6 File Offset: 0x000085B6
		internal void PointerEntersPanel(int pointerId, Vector2 position)
		{
			PointerDeviceState.SavePointerPosition(pointerId, position, this, this.contextType);
		}

		// Token: 0x0400013B RID: 315
		private GameObject m_SelectableGameObject;

		// Token: 0x0400013C RID: 316
		private static int s_CurrentRuntimePanelCounter = 0;

		// Token: 0x0400013D RID: 317
		internal readonly int m_RuntimePanelCreationIndex;

		// Token: 0x0400013E RID: 318
		private float m_SortingPriority = 0f;

		// Token: 0x04000140 RID: 320
		private Shader m_StandardWorldSpaceShader;

		// Token: 0x04000141 RID: 321
		private bool m_DrawToCameras;

		// Token: 0x04000142 RID: 322
		internal RenderTexture targetTexture = null;

		// Token: 0x04000143 RID: 323
		internal Matrix4x4 panelToWorld = Matrix4x4.identity;

		// Token: 0x04000145 RID: 325
		internal static readonly Func<Vector2, Vector2> DefaultScreenToPanelSpace = (Vector2 p) => p;

		// Token: 0x04000146 RID: 326
		private Func<Vector2, Vector2> m_ScreenToPanelSpace = BaseRuntimePanel.DefaultScreenToPanelSpace;
	}
}
