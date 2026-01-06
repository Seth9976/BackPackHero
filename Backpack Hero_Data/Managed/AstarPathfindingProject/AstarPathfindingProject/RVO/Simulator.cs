using System;
using System.Collections.Generic;
using System.Threading;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000D3 RID: 211
	public class Simulator
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0003B448 File Offset: 0x00039648
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0003B450 File Offset: 0x00039650
		public RVOQuadtree Quadtree { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0003B459 File Offset: 0x00039659
		public float DeltaTime
		{
			get
			{
				return this.deltaTime;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0003B461 File Offset: 0x00039661
		public bool Multithreading
		{
			get
			{
				return this.workers != null && this.workers.Length != 0;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0003B477 File Offset: 0x00039677
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0003B47F File Offset: 0x0003967F
		public float DesiredDeltaTime
		{
			get
			{
				return this.desiredDeltaTime;
			}
			set
			{
				this.desiredDeltaTime = Math.Max(value, 0f);
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0003B492 File Offset: 0x00039692
		public List<Agent> GetAgents()
		{
			return this.agents;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0003B49A File Offset: 0x0003969A
		public List<ObstacleVertex> GetObstacles()
		{
			return this.obstacles;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0003B4A4 File Offset: 0x000396A4
		public Simulator(int workers, bool doubleBuffering, MovementPlane movementPlane)
		{
			this.workers = new Simulator.Worker[workers];
			this.doubleBuffering = doubleBuffering;
			this.DesiredDeltaTime = 1f;
			this.movementPlane = movementPlane;
			this.Quadtree = new RVOQuadtree();
			for (int i = 0; i < workers; i++)
			{
				this.workers[i] = new Simulator.Worker(this);
			}
			this.agents = new List<Agent>();
			this.obstacles = new List<ObstacleVertex>();
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0003B54C File Offset: 0x0003974C
		public void ClearAgents()
		{
			this.BlockUntilSimulationStepIsDone();
			for (int i = 0; i < this.agents.Count; i++)
			{
				this.agents[i].simulator = null;
			}
			this.agents.Clear();
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0003B594 File Offset: 0x00039794
		public void OnDestroy()
		{
			if (this.workers != null)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].Terminate();
				}
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0003B5CC File Offset: 0x000397CC
		public IAgent AddAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				string text = "The agent must be of type Agent. Agent was of type ";
				Type type = agent.GetType();
				throw new ArgumentException(text + ((type != null) ? type.ToString() : null));
			}
			if (agent2.simulator != null && agent2.simulator == this)
			{
				throw new ArgumentException("The agent is already in the simulation");
			}
			if (agent2.simulator != null)
			{
				throw new ArgumentException("The agent is already added to another simulation");
			}
			agent2.simulator = this;
			this.BlockUntilSimulationStepIsDone();
			this.agents.Add(agent2);
			return agent;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0003B65C File Offset: 0x0003985C
		[Obsolete("Use AddAgent(Vector2,float) instead")]
		public IAgent AddAgent(Vector3 position)
		{
			return this.AddAgent(new Vector2(position.x, position.z), position.y);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0003B67B File Offset: 0x0003987B
		public IAgent AddAgent(Vector2 position, float elevationCoordinate)
		{
			return this.AddAgent(new Agent(position, elevationCoordinate));
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0003B68C File Offset: 0x0003988C
		public void RemoveAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				string text = "The agent must be of type Agent. Agent was of type ";
				Type type = agent.GetType();
				throw new ArgumentException(text + ((type != null) ? type.ToString() : null));
			}
			if (agent2.simulator != this)
			{
				throw new ArgumentException("The agent is not added to this simulation");
			}
			this.BlockUntilSimulationStepIsDone();
			agent2.simulator = null;
			if (!this.agents.Remove(agent2))
			{
				throw new ArgumentException("Critical Bug! This should not happen. Please report this.");
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0003B70D File Offset: 0x0003990D
		public ObstacleVertex AddObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Add(v);
			this.UpdateObstacles();
			return v;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0003B736 File Offset: 0x00039936
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height, bool cycle = true)
		{
			return this.AddObstacle(vertices, height, Matrix4x4.identity, RVOLayer.DefaultObstacle, cycle);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0003B748 File Offset: 0x00039948
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height, Matrix4x4 matrix, RVOLayer layer = RVOLayer.DefaultObstacle, bool cycle = true)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			ObstacleVertex obstacleVertex = null;
			ObstacleVertex obstacleVertex2 = null;
			this.BlockUntilSimulationStepIsDone();
			for (int i = 0; i < vertices.Length; i++)
			{
				ObstacleVertex obstacleVertex3 = new ObstacleVertex
				{
					prev = obstacleVertex2,
					layer = layer,
					height = height
				};
				if (obstacleVertex == null)
				{
					obstacleVertex = obstacleVertex3;
				}
				else
				{
					obstacleVertex2.next = obstacleVertex3;
				}
				obstacleVertex2 = obstacleVertex3;
			}
			if (cycle)
			{
				obstacleVertex2.next = obstacleVertex;
				obstacleVertex.prev = obstacleVertex2;
			}
			this.UpdateObstacle(obstacleVertex, vertices, matrix);
			this.obstacles.Add(obstacleVertex);
			return obstacleVertex;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0003B7E0 File Offset: 0x000399E0
		public ObstacleVertex AddObstacle(Vector3 a, Vector3 b, float height)
		{
			ObstacleVertex obstacleVertex = new ObstacleVertex();
			ObstacleVertex obstacleVertex2 = new ObstacleVertex();
			obstacleVertex.layer = RVOLayer.DefaultObstacle;
			obstacleVertex2.layer = RVOLayer.DefaultObstacle;
			obstacleVertex.prev = obstacleVertex2;
			obstacleVertex2.prev = obstacleVertex;
			obstacleVertex.next = obstacleVertex2;
			obstacleVertex2.next = obstacleVertex;
			obstacleVertex.position = a;
			obstacleVertex2.position = b;
			obstacleVertex.height = height;
			obstacleVertex2.height = height;
			obstacleVertex2.ignore = true;
			obstacleVertex.dir = new Vector2(b.x - a.x, b.z - a.z).normalized;
			obstacleVertex2.dir = -obstacleVertex.dir;
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Add(obstacleVertex);
			this.UpdateObstacles();
			return obstacleVertex;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0003B8A0 File Offset: 0x00039AA0
		public void UpdateObstacle(ObstacleVertex obstacle, Vector3[] vertices, Matrix4x4 matrix)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (obstacle == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			bool flag = matrix == Matrix4x4.identity;
			this.BlockUntilSimulationStepIsDone();
			int i = 0;
			ObstacleVertex obstacleVertex = obstacle;
			while (i < vertices.Length)
			{
				obstacleVertex.position = (flag ? vertices[i] : matrix.MultiplyPoint3x4(vertices[i]));
				obstacleVertex = obstacleVertex.next;
				i++;
				if (obstacleVertex == obstacle || obstacleVertex == null)
				{
					obstacleVertex = obstacle;
					do
					{
						if (obstacleVertex.next == null)
						{
							obstacleVertex.dir = Vector2.zero;
						}
						else
						{
							Vector3 vector = obstacleVertex.next.position - obstacleVertex.position;
							obstacleVertex.dir = new Vector2(vector.x, vector.z).normalized;
						}
						obstacleVertex = obstacleVertex.next;
					}
					while (obstacleVertex != obstacle && obstacleVertex != null);
					this.ScheduleCleanObstacles();
					this.UpdateObstacles();
					return;
				}
			}
			Debug.DrawLine(obstacleVertex.prev.position, obstacleVertex.position, Color.red);
			throw new ArgumentException("Obstacle has more vertices than supplied for updating (" + vertices.Length.ToString() + " supplied)");
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0003B9CE File Offset: 0x00039BCE
		private void ScheduleCleanObstacles()
		{
			this.doCleanObstacles = true;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0003B9D7 File Offset: 0x00039BD7
		private void CleanObstacles()
		{
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0003B9D9 File Offset: 0x00039BD9
		public void RemoveObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Vertex must not be null");
			}
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Remove(v);
			this.UpdateObstacles();
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0003BA02 File Offset: 0x00039C02
		public void UpdateObstacles()
		{
			this.doUpdateObstacles = true;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0003BA0C File Offset: 0x00039C0C
		private void BuildQuadtree()
		{
			this.Quadtree.Clear();
			if (this.agents.Count > 0)
			{
				Rect rect = Rect.MinMaxRect(this.agents[0].position.x, this.agents[0].position.y, this.agents[0].position.x, this.agents[0].position.y);
				for (int i = 1; i < this.agents.Count; i++)
				{
					Vector2 position = this.agents[i].position;
					rect = Rect.MinMaxRect(Mathf.Min(rect.xMin, position.x), Mathf.Min(rect.yMin, position.y), Mathf.Max(rect.xMax, position.x), Mathf.Max(rect.yMax, position.y));
				}
				this.Quadtree.SetBounds(rect);
				for (int j = 0; j < this.agents.Count; j++)
				{
					this.Quadtree.Insert(this.agents[j]);
				}
			}
			this.Quadtree.CalculateSpeeds();
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0003BB50 File Offset: 0x00039D50
		private void BlockUntilSimulationStepIsDone()
		{
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0003BB90 File Offset: 0x00039D90
		private void PreCalculation()
		{
			for (int i = 0; i < this.agents.Count; i++)
			{
				this.agents[i].PreCalculation();
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0003BBC4 File Offset: 0x00039DC4
		private void CleanAndUpdateObstaclesIfNecessary()
		{
			if (this.doCleanObstacles)
			{
				this.CleanObstacles();
				this.doCleanObstacles = false;
				this.doUpdateObstacles = true;
			}
			if (this.doUpdateObstacles)
			{
				this.doUpdateObstacles = false;
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0003BBF4 File Offset: 0x00039DF4
		public void Update()
		{
			if (this.lastStep < 0f)
			{
				this.lastStep = Time.time;
				this.deltaTime = this.DesiredDeltaTime;
			}
			if (Time.time - this.lastStep >= this.DesiredDeltaTime)
			{
				this.deltaTime = Time.time - this.lastStep;
				this.lastStep = Time.time;
				this.deltaTime = Math.Max(this.deltaTime, 0.0005f);
				if (this.Multithreading)
				{
					if (this.doubleBuffering)
					{
						for (int i = 0; i < this.workers.Length; i++)
						{
							this.workers[i].WaitOne();
						}
						for (int j = 0; j < this.agents.Count; j++)
						{
							this.agents[j].PostCalculation();
						}
					}
					this.PreCalculation();
					this.CleanAndUpdateObstaclesIfNecessary();
					this.BuildQuadtree();
					for (int k = 0; k < this.workers.Length; k++)
					{
						this.workers[k].start = k * this.agents.Count / this.workers.Length;
						this.workers[k].end = (k + 1) * this.agents.Count / this.workers.Length;
					}
					for (int l = 0; l < this.workers.Length; l++)
					{
						this.workers[l].Execute(1);
					}
					for (int m = 0; m < this.workers.Length; m++)
					{
						this.workers[m].WaitOne();
					}
					for (int n = 0; n < this.workers.Length; n++)
					{
						this.workers[n].Execute(0);
					}
					if (!this.doubleBuffering)
					{
						for (int num = 0; num < this.workers.Length; num++)
						{
							this.workers[num].WaitOne();
						}
						for (int num2 = 0; num2 < this.agents.Count; num2++)
						{
							this.agents[num2].PostCalculation();
						}
						return;
					}
				}
				else
				{
					this.PreCalculation();
					this.CleanAndUpdateObstaclesIfNecessary();
					this.BuildQuadtree();
					for (int num3 = 0; num3 < this.agents.Count; num3++)
					{
						this.agents[num3].BufferSwitch();
					}
					for (int num4 = 0; num4 < this.agents.Count; num4++)
					{
						this.agents[num4].CalculateNeighbours();
						this.agents[num4].CalculateVelocity(this.coroutineWorkerContext);
					}
					for (int num5 = 0; num5 < this.agents.Count; num5++)
					{
						this.agents[num5].PostCalculation();
					}
				}
			}
		}

		// Token: 0x04000544 RID: 1348
		private readonly bool doubleBuffering = true;

		// Token: 0x04000545 RID: 1349
		private float desiredDeltaTime = 0.05f;

		// Token: 0x04000546 RID: 1350
		private readonly Simulator.Worker[] workers;

		// Token: 0x04000547 RID: 1351
		private List<Agent> agents;

		// Token: 0x04000548 RID: 1352
		public List<ObstacleVertex> obstacles;

		// Token: 0x0400054A RID: 1354
		private float deltaTime;

		// Token: 0x0400054B RID: 1355
		private float lastStep = -99999f;

		// Token: 0x0400054C RID: 1356
		private bool doUpdateObstacles;

		// Token: 0x0400054D RID: 1357
		private bool doCleanObstacles;

		// Token: 0x0400054E RID: 1358
		public float symmetryBreakingBias = 0.1f;

		// Token: 0x0400054F RID: 1359
		public readonly MovementPlane movementPlane;

		// Token: 0x04000550 RID: 1360
		private Simulator.WorkerContext coroutineWorkerContext = new Simulator.WorkerContext();

		// Token: 0x0200015E RID: 350
		internal class WorkerContext
		{
			// Token: 0x040007CC RID: 1996
			public Agent.VOBuffer vos = new Agent.VOBuffer(16);

			// Token: 0x040007CD RID: 1997
			public const int KeepCount = 3;

			// Token: 0x040007CE RID: 1998
			public Vector2[] bestPos = new Vector2[3];

			// Token: 0x040007CF RID: 1999
			public float[] bestSizes = new float[3];

			// Token: 0x040007D0 RID: 2000
			public float[] bestScores = new float[4];

			// Token: 0x040007D1 RID: 2001
			public Vector2[] samplePos = new Vector2[50];

			// Token: 0x040007D2 RID: 2002
			public float[] sampleSize = new float[50];
		}

		// Token: 0x0200015F RID: 351
		private class Worker
		{
			// Token: 0x06000B34 RID: 2868 RVA: 0x000466D0 File Offset: 0x000448D0
			public Worker(Simulator sim)
			{
				this.simulator = sim;
				new Thread(new ThreadStart(this.Run))
				{
					IsBackground = true,
					Name = "RVO Simulator Thread"
				}.Start();
			}

			// Token: 0x06000B35 RID: 2869 RVA: 0x00046735 File Offset: 0x00044935
			public void Execute(int task)
			{
				this.task = task;
				this.waitFlag.Reset();
				this.runFlag.Set();
			}

			// Token: 0x06000B36 RID: 2870 RVA: 0x00046754 File Offset: 0x00044954
			public void WaitOne()
			{
				if (!this.terminate)
				{
					this.waitFlag.Wait();
				}
			}

			// Token: 0x06000B37 RID: 2871 RVA: 0x00046769 File Offset: 0x00044969
			public void Terminate()
			{
				this.WaitOne();
				this.terminate = true;
				this.Execute(-1);
			}

			// Token: 0x06000B38 RID: 2872 RVA: 0x00046780 File Offset: 0x00044980
			public void Run()
			{
				this.runFlag.Wait();
				this.runFlag.Reset();
				while (!this.terminate)
				{
					try
					{
						List<Agent> agents = this.simulator.GetAgents();
						if (this.task == 0)
						{
							for (int i = this.start; i < this.end; i++)
							{
								agents[i].CalculateNeighbours();
								agents[i].CalculateVelocity(this.context);
							}
						}
						else if (this.task == 1)
						{
							for (int j = this.start; j < this.end; j++)
							{
								agents[j].BufferSwitch();
							}
						}
						else
						{
							if (this.task != 2)
							{
								Debug.LogError("Invalid Task Number: " + this.task.ToString());
								throw new Exception("Invalid Task Number: " + this.task.ToString());
							}
							this.simulator.BuildQuadtree();
						}
					}
					catch (Exception ex)
					{
						Debug.LogError(ex);
					}
					this.waitFlag.Set();
					this.runFlag.Wait();
					this.runFlag.Reset();
				}
			}

			// Token: 0x040007D3 RID: 2003
			public int start;

			// Token: 0x040007D4 RID: 2004
			public int end;

			// Token: 0x040007D5 RID: 2005
			private readonly ManualResetEventSlim runFlag = new ManualResetEventSlim(false);

			// Token: 0x040007D6 RID: 2006
			private readonly ManualResetEventSlim waitFlag = new ManualResetEventSlim(true);

			// Token: 0x040007D7 RID: 2007
			private readonly Simulator simulator;

			// Token: 0x040007D8 RID: 2008
			private int task;

			// Token: 0x040007D9 RID: 2009
			private bool terminate;

			// Token: 0x040007DA RID: 2010
			private Simulator.WorkerContext context = new Simulator.WorkerContext();
		}
	}
}
