using System;
using System.Text;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000062 RID: 98
	[AddComponentMenu("Pathfinding/Pathfinding Debugger")]
	[ExecuteInEditMode]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/astardebugger.html")]
	public class AstarDebugger : VersionedMonoBehaviour
	{
		// Token: 0x0600037E RID: 894 RVA: 0x00010EF8 File Offset: 0x0000F0F8
		public void Start()
		{
			base.useGUILayout = false;
			this.fpsDrops = new float[this.fpsDropCounterSize];
			this.cam = base.GetComponent<Camera>();
			if (this.cam == null)
			{
				this.cam = Camera.main;
			}
			this.graph = new AstarDebugger.GraphPoint[this.graphBufferSize];
			if (Time.unscaledDeltaTime > 0f)
			{
				for (int i = 0; i < this.fpsDrops.Length; i++)
				{
					this.fpsDrops[i] = 1f / Time.unscaledDeltaTime;
				}
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00010F88 File Offset: 0x0000F188
		public void LateUpdate()
		{
			if (!this.show || (!Application.isPlaying && !this.showInEditor))
			{
				return;
			}
			if (Time.unscaledDeltaTime <= 0.0001f)
			{
				return;
			}
			int num = GC.CollectionCount(0);
			if (this.lastCollectNum != (float)num)
			{
				this.lastCollectNum = (float)num;
				this.delta = Time.realtimeSinceStartup - this.lastCollect;
				this.lastCollect = Time.realtimeSinceStartup;
				this.lastDeltaTime = Time.unscaledDeltaTime;
				this.collectAlloc = this.allocMem;
			}
			this.allocMem = (int)GC.GetTotalMemory(false);
			bool flag = this.allocMem < this.peakAlloc;
			this.peakAlloc = ((!flag) ? this.allocMem : this.peakAlloc);
			if (Time.realtimeSinceStartup - this.lastAllocSet > 0.3f || !Application.isPlaying)
			{
				int num2 = this.allocMem - this.lastAllocMemory;
				this.lastAllocMemory = this.allocMem;
				this.lastAllocSet = Time.realtimeSinceStartup;
				this.delayedDeltaTime = Time.unscaledDeltaTime;
				if (num2 >= 0)
				{
					this.allocRate = num2;
				}
			}
			if (Application.isPlaying)
			{
				this.fpsDrops[Time.frameCount % this.fpsDrops.Length] = ((Time.unscaledDeltaTime > 1E-05f) ? (1f / Time.unscaledDeltaTime) : 0f);
				int num3 = Time.frameCount % this.graph.Length;
				this.graph[num3].fps = ((Time.unscaledDeltaTime < 1E-05f) ? (1f / Time.unscaledDeltaTime) : 0f);
				this.graph[num3].collectEvent = flag;
				this.graph[num3].memory = (float)this.allocMem;
			}
			if (Application.isPlaying && this.cam != null && this.showGraph)
			{
				this.graphWidth = (float)this.cam.pixelWidth * 0.8f;
				float num4 = float.PositiveInfinity;
				float num5 = 0f;
				float num6 = float.PositiveInfinity;
				float num7 = 0f;
				for (int i = 0; i < this.graph.Length; i++)
				{
					num4 = Mathf.Min(this.graph[i].memory, num4);
					num5 = Mathf.Max(this.graph[i].memory, num5);
					num6 = Mathf.Min(this.graph[i].fps, num6);
					num7 = Mathf.Max(this.graph[i].fps, num7);
				}
				int num8 = Time.frameCount % this.graph.Length;
				Matrix4x4 matrix4x = Matrix4x4.TRS(new Vector3(((float)this.cam.pixelWidth - this.graphWidth) / 2f, this.graphOffset, 1f), Quaternion.identity, new Vector3(this.graphWidth, this.graphHeight, 1f));
				for (int j = 0; j < this.graph.Length - 1; j++)
				{
					if (j != num8)
					{
						this.DrawGraphLine(j, matrix4x, (float)j / (float)this.graph.Length, (float)(j + 1) / (float)this.graph.Length, Mathf.InverseLerp(num4, num5, this.graph[j].memory), Mathf.InverseLerp(num4, num5, this.graph[j + 1].memory), Color.blue);
						this.DrawGraphLine(j, matrix4x, (float)j / (float)this.graph.Length, (float)(j + 1) / (float)this.graph.Length, Mathf.InverseLerp(num6, num7, this.graph[j].fps), Mathf.InverseLerp(num6, num7, this.graph[j + 1].fps), Color.green);
					}
				}
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001135A File Offset: 0x0000F55A
		private void DrawGraphLine(int index, Matrix4x4 m, float x1, float x2, float y1, float y2, Color color)
		{
			Debug.DrawLine(this.cam.ScreenToWorldPoint(m.MultiplyPoint3x4(new Vector3(x1, y1))), this.cam.ScreenToWorldPoint(m.MultiplyPoint3x4(new Vector3(x2, y2))), color);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00011398 File Offset: 0x0000F598
		public void OnGUI()
		{
			if (!this.show || (!Application.isPlaying && !this.showInEditor))
			{
				return;
			}
			if (this.style == null)
			{
				this.style = new GUIStyle();
				this.style.normal.textColor = Color.white;
				this.style.padding = new RectOffset(5, 5, 5, 5);
			}
			if (Time.realtimeSinceStartup - this.lastUpdate > 0.5f || this.cachedText == null || !Application.isPlaying)
			{
				this.lastUpdate = Time.realtimeSinceStartup;
				this.boxRect = new Rect(5f, (float)this.yOffset, 310f, 40f);
				this.text.Length = 0;
				this.text.AppendLine("A* Pathfinding Project Debugger");
				this.text.Append("A* Version: ").Append(AstarPath.Version.ToString());
				if (this.showMemProfile)
				{
					this.boxRect.height = this.boxRect.height + 200f;
					this.text.AppendLine();
					this.text.AppendLine();
					this.text.Append("Currently allocated".PadRight(25));
					this.text.Append(((float)this.allocMem / 1000000f).ToString("0.0 MB"));
					this.text.AppendLine();
					this.text.Append("Peak allocated".PadRight(25));
					this.text.Append(((float)this.peakAlloc / 1000000f).ToString("0.0 MB")).AppendLine();
					this.text.Append("Last collect peak".PadRight(25));
					this.text.Append(((float)this.collectAlloc / 1000000f).ToString("0.0 MB")).AppendLine();
					this.text.Append("Allocation rate".PadRight(25));
					this.text.Append(((float)this.allocRate / 1000000f).ToString("0.0 MB")).AppendLine();
					this.text.Append("Collection frequency".PadRight(25));
					this.text.Append(this.delta.ToString("0.00"));
					this.text.Append("s\n");
					this.text.Append("Last collect fps".PadRight(25));
					this.text.Append((1f / this.lastDeltaTime).ToString("0.0 fps"));
					this.text.Append(" (");
					this.text.Append(this.lastDeltaTime.ToString("0.000 s"));
					this.text.Append(")");
				}
				if (this.showFPS)
				{
					this.text.AppendLine();
					this.text.AppendLine();
					float num = ((this.delayedDeltaTime > 1E-05f) ? (1f / this.delayedDeltaTime) : 0f);
					this.text.Append("FPS".PadRight(25)).Append(num.ToString("0.0 fps"));
					float num2 = float.PositiveInfinity;
					for (int i = 0; i < this.fpsDrops.Length; i++)
					{
						if (this.fpsDrops[i] < num2)
						{
							num2 = this.fpsDrops[i];
						}
					}
					this.text.AppendLine();
					this.text.Append(("Lowest fps (last " + this.fpsDrops.Length.ToString() + ")").PadRight(25)).Append(num2.ToString("0.0"));
				}
				if (this.showPathProfile)
				{
					Object active = AstarPath.active;
					this.text.AppendLine();
					if (active == null)
					{
						this.text.Append("\nNo AstarPath Object In The Scene");
					}
					else
					{
						if (ListPool<Vector3>.GetSize() > this.maxVecPool)
						{
							this.maxVecPool = ListPool<Vector3>.GetSize();
						}
						if (ListPool<GraphNode>.GetSize() > this.maxNodePool)
						{
							this.maxNodePool = ListPool<GraphNode>.GetSize();
						}
						this.text.Append("\nPool Sizes (size/total created)");
						for (int j = 0; j < this.debugTypes.Length; j++)
						{
							this.debugTypes[j].Print(this.text);
						}
					}
				}
				this.cachedText = this.text.ToString();
			}
			if (this.font != null)
			{
				this.style.font = this.font;
				this.style.fontSize = this.fontSize;
			}
			this.boxRect.height = this.style.CalcHeight(new GUIContent(this.cachedText), this.boxRect.width);
			GUI.Box(this.boxRect, "");
			GUI.Label(this.boxRect, this.cachedText, this.style);
			if (this.showGraph)
			{
				float num3 = float.PositiveInfinity;
				float num4 = 0f;
				float num5 = float.PositiveInfinity;
				float num6 = 0f;
				for (int k = 0; k < this.graph.Length; k++)
				{
					num3 = Mathf.Min(this.graph[k].memory, num3);
					num4 = Mathf.Max(this.graph[k].memory, num4);
					num5 = Mathf.Min(this.graph[k].fps, num5);
					num6 = Mathf.Max(this.graph[k].fps, num6);
				}
				GUI.color = Color.blue;
				float num7 = (float)Mathf.RoundToInt(num4 / 100000f);
				GUI.Label(new Rect(5f, (float)Screen.height - AstarMath.MapTo(num3, num4, 0f + this.graphOffset, this.graphHeight + this.graphOffset, num7 * 1000f * 100f) - 10f, 100f, 20f), (num7 / 10f).ToString("0.0 MB"));
				num7 = Mathf.Round(num3 / 100000f);
				GUI.Label(new Rect(5f, (float)Screen.height - AstarMath.MapTo(num3, num4, 0f + this.graphOffset, this.graphHeight + this.graphOffset, num7 * 1000f * 100f) - 10f, 100f, 20f), (num7 / 10f).ToString("0.0 MB"));
				GUI.color = Color.green;
				num7 = Mathf.Round(num6);
				GUI.Label(new Rect(55f, (float)Screen.height - AstarMath.MapTo(num5, num6, 0f + this.graphOffset, this.graphHeight + this.graphOffset, num7) - 10f, 100f, 20f), num7.ToString("0 FPS"));
				num7 = Mathf.Round(num5);
				GUI.Label(new Rect(55f, (float)Screen.height - AstarMath.MapTo(num5, num6, 0f + this.graphOffset, this.graphHeight + this.graphOffset, num7) - 10f, 100f, 20f), num7.ToString("0 FPS"));
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011B2C File Offset: 0x0000FD2C
		public AstarDebugger()
		{
			AstarDebugger.PathTypeDebug[] array = new AstarDebugger.PathTypeDebug[7];
			array[0] = new AstarDebugger.PathTypeDebug("ABPath", () => PathPool.GetSize(typeof(ABPath)), () => PathPool.GetTotalCreated(typeof(ABPath)));
			array[1] = new AstarDebugger.PathTypeDebug("MultiTargetPath", () => PathPool.GetSize(typeof(MultiTargetPath)), () => PathPool.GetTotalCreated(typeof(MultiTargetPath)));
			array[2] = new AstarDebugger.PathTypeDebug("RandomPath", () => PathPool.GetSize(typeof(RandomPath)), () => PathPool.GetTotalCreated(typeof(RandomPath)));
			array[3] = new AstarDebugger.PathTypeDebug("FleePath", () => PathPool.GetSize(typeof(FleePath)), () => PathPool.GetTotalCreated(typeof(FleePath)));
			array[4] = new AstarDebugger.PathTypeDebug("ConstantPath", () => PathPool.GetSize(typeof(ConstantPath)), () => PathPool.GetTotalCreated(typeof(ConstantPath)));
			array[5] = new AstarDebugger.PathTypeDebug("FloodPath", () => PathPool.GetSize(typeof(FloodPath)), () => PathPool.GetTotalCreated(typeof(FloodPath)));
			array[6] = new AstarDebugger.PathTypeDebug("FloodPathTracer", () => PathPool.GetSize(typeof(FloodPathTracer)), () => PathPool.GetTotalCreated(typeof(FloodPathTracer)));
			this.debugTypes = array;
			base..ctor();
		}

		// Token: 0x0400021B RID: 539
		public int yOffset = 5;

		// Token: 0x0400021C RID: 540
		public bool show = true;

		// Token: 0x0400021D RID: 541
		public bool showInEditor;

		// Token: 0x0400021E RID: 542
		public bool showFPS;

		// Token: 0x0400021F RID: 543
		public bool showPathProfile;

		// Token: 0x04000220 RID: 544
		public bool showMemProfile;

		// Token: 0x04000221 RID: 545
		public bool showGraph;

		// Token: 0x04000222 RID: 546
		public int graphBufferSize = 200;

		// Token: 0x04000223 RID: 547
		public Font font;

		// Token: 0x04000224 RID: 548
		public int fontSize = 12;

		// Token: 0x04000225 RID: 549
		private StringBuilder text = new StringBuilder();

		// Token: 0x04000226 RID: 550
		private string cachedText;

		// Token: 0x04000227 RID: 551
		private float lastUpdate = -999f;

		// Token: 0x04000228 RID: 552
		private AstarDebugger.GraphPoint[] graph;

		// Token: 0x04000229 RID: 553
		private float delayedDeltaTime = 1f;

		// Token: 0x0400022A RID: 554
		private float lastCollect;

		// Token: 0x0400022B RID: 555
		private float lastCollectNum;

		// Token: 0x0400022C RID: 556
		private float delta;

		// Token: 0x0400022D RID: 557
		private float lastDeltaTime;

		// Token: 0x0400022E RID: 558
		private int allocRate;

		// Token: 0x0400022F RID: 559
		private int lastAllocMemory;

		// Token: 0x04000230 RID: 560
		private float lastAllocSet = -9999f;

		// Token: 0x04000231 RID: 561
		private int allocMem;

		// Token: 0x04000232 RID: 562
		private int collectAlloc;

		// Token: 0x04000233 RID: 563
		private int peakAlloc;

		// Token: 0x04000234 RID: 564
		private int fpsDropCounterSize = 200;

		// Token: 0x04000235 RID: 565
		private float[] fpsDrops;

		// Token: 0x04000236 RID: 566
		private Rect boxRect;

		// Token: 0x04000237 RID: 567
		private GUIStyle style;

		// Token: 0x04000238 RID: 568
		private Camera cam;

		// Token: 0x04000239 RID: 569
		private float graphWidth = 100f;

		// Token: 0x0400023A RID: 570
		private float graphHeight = 100f;

		// Token: 0x0400023B RID: 571
		private float graphOffset = 50f;

		// Token: 0x0400023C RID: 572
		private int maxVecPool;

		// Token: 0x0400023D RID: 573
		private int maxNodePool;

		// Token: 0x0400023E RID: 574
		private AstarDebugger.PathTypeDebug[] debugTypes;

		// Token: 0x02000063 RID: 99
		private struct GraphPoint
		{
			// Token: 0x0400023F RID: 575
			public float fps;

			// Token: 0x04000240 RID: 576
			public float memory;

			// Token: 0x04000241 RID: 577
			public bool collectEvent;
		}

		// Token: 0x02000064 RID: 100
		private struct PathTypeDebug
		{
			// Token: 0x06000383 RID: 899 RVA: 0x00011DED File Offset: 0x0000FFED
			public PathTypeDebug(string name, Func<int> getSize, Func<int> getTotalCreated)
			{
				this.name = name;
				this.getSize = getSize;
				this.getTotalCreated = getTotalCreated;
			}

			// Token: 0x06000384 RID: 900 RVA: 0x00011E04 File Offset: 0x00010004
			public void Print(StringBuilder text)
			{
				int num = this.getTotalCreated();
				if (num > 0)
				{
					text.Append("\n").Append(("  " + this.name).PadRight(25)).Append(this.getSize())
						.Append("/")
						.Append(num);
				}
			}

			// Token: 0x04000242 RID: 578
			private string name;

			// Token: 0x04000243 RID: 579
			private Func<int> getSize;

			// Token: 0x04000244 RID: 580
			private Func<int> getTotalCreated;
		}
	}
}
