using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO.Sampled
{
	// Token: 0x020000DB RID: 219
	public class Agent : IAgent
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0003D894 File Offset: 0x0003BA94
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x0003D89C File Offset: 0x0003BA9C
		public Vector2 Position { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0003D8A5 File Offset: 0x0003BAA5
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0003D8AD File Offset: 0x0003BAAD
		public float ElevationCoordinate { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0003D8B6 File Offset: 0x0003BAB6
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x0003D8BE File Offset: 0x0003BABE
		public Vector2 CalculatedTargetPoint { get; private set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0003D8C7 File Offset: 0x0003BAC7
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0003D8CF File Offset: 0x0003BACF
		public float CalculatedSpeed { get; private set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0003D8D8 File Offset: 0x0003BAD8
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x0003D8E0 File Offset: 0x0003BAE0
		public bool Locked { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0003D8E9 File Offset: 0x0003BAE9
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0003D8F1 File Offset: 0x0003BAF1
		public float Radius { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0003D8FA File Offset: 0x0003BAFA
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x0003D902 File Offset: 0x0003BB02
		public float Height { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0003D90B File Offset: 0x0003BB0B
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0003D913 File Offset: 0x0003BB13
		public float AgentTimeHorizon { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0003D91C File Offset: 0x0003BB1C
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0003D924 File Offset: 0x0003BB24
		public float ObstacleTimeHorizon { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0003D92D File Offset: 0x0003BB2D
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0003D935 File Offset: 0x0003BB35
		public int MaxNeighbours { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0003D93E File Offset: 0x0003BB3E
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0003D946 File Offset: 0x0003BB46
		public int NeighbourCount { get; private set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0003D94F File Offset: 0x0003BB4F
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x0003D957 File Offset: 0x0003BB57
		public RVOLayer Layer { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0003D960 File Offset: 0x0003BB60
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x0003D968 File Offset: 0x0003BB68
		public RVOLayer CollidesWith { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0003D971 File Offset: 0x0003BB71
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0003D979 File Offset: 0x0003BB79
		public bool DebugDraw
		{
			get
			{
				return this.debugDraw;
			}
			set
			{
				this.debugDraw = value && this.simulator != null && !this.simulator.Multithreading;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0003D99D File Offset: 0x0003BB9D
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x0003D9A5 File Offset: 0x0003BBA5
		public float Priority { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0003D9AE File Offset: 0x0003BBAE
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x0003D9B6 File Offset: 0x0003BBB6
		public Action PreCalculationCallback { private get; set; }

		// Token: 0x06000985 RID: 2437 RVA: 0x0003D9BF File Offset: 0x0003BBBF
		public void SetTarget(Vector2 targetPoint, float desiredSpeed, float maxSpeed)
		{
			maxSpeed = Math.Max(maxSpeed, 0f);
			desiredSpeed = Math.Min(Math.Max(desiredSpeed, 0f), maxSpeed);
			this.nextTargetPoint = targetPoint;
			this.nextDesiredSpeed = desiredSpeed;
			this.nextMaxSpeed = maxSpeed;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0003D9F6 File Offset: 0x0003BBF6
		public void SetCollisionNormal(Vector2 normal)
		{
			this.collisionNormal = normal;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0003DA00 File Offset: 0x0003BC00
		public void ForceSetVelocity(Vector2 velocity)
		{
			this.nextTargetPoint = (this.CalculatedTargetPoint = this.position + velocity * 1000f);
			this.nextDesiredSpeed = (this.CalculatedSpeed = velocity.magnitude);
			this.manuallyControlled = true;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0003DA4F File Offset: 0x0003BC4F
		public List<ObstacleVertex> NeighbourObstacles
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0003DA54 File Offset: 0x0003BC54
		public Agent(Vector2 pos, float elevationCoordinate)
		{
			this.AgentTimeHorizon = 2f;
			this.ObstacleTimeHorizon = 2f;
			this.Height = 5f;
			this.Radius = 5f;
			this.MaxNeighbours = 10;
			this.Locked = false;
			this.Position = pos;
			this.ElevationCoordinate = elevationCoordinate;
			this.Layer = RVOLayer.DefaultAgent;
			this.CollidesWith = (RVOLayer)(-1);
			this.Priority = 0.5f;
			this.CalculatedTargetPoint = pos;
			this.CalculatedSpeed = 0f;
			this.SetTarget(pos, 0f, 0f);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0003DB18 File Offset: 0x0003BD18
		public void BufferSwitch()
		{
			this.radius = this.Radius;
			this.height = this.Height;
			this.maxSpeed = this.nextMaxSpeed;
			this.desiredSpeed = this.nextDesiredSpeed;
			this.agentTimeHorizon = this.AgentTimeHorizon;
			this.obstacleTimeHorizon = this.ObstacleTimeHorizon;
			this.maxNeighbours = this.MaxNeighbours;
			this.locked = this.Locked && !this.manuallyControlled;
			this.position = this.Position;
			this.elevationCoordinate = this.ElevationCoordinate;
			this.collidesWith = this.CollidesWith;
			this.layer = this.Layer;
			if (this.locked)
			{
				this.desiredTargetPointInVelocitySpace = this.position;
				this.desiredVelocity = (this.currentVelocity = Vector2.zero);
				return;
			}
			this.desiredTargetPointInVelocitySpace = this.nextTargetPoint - this.position;
			this.currentVelocity = (this.CalculatedTargetPoint - this.position).normalized * this.CalculatedSpeed;
			this.desiredVelocity = this.desiredTargetPointInVelocitySpace.normalized * this.desiredSpeed;
			if (this.collisionNormal != Vector2.zero)
			{
				this.collisionNormal.Normalize();
				float num = Vector2.Dot(this.currentVelocity, this.collisionNormal);
				if (num < 0f)
				{
					this.currentVelocity -= this.collisionNormal * num;
				}
				this.collisionNormal = Vector2.zero;
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0003DCA8 File Offset: 0x0003BEA8
		public void PreCalculation()
		{
			if (this.PreCalculationCallback != null)
			{
				this.PreCalculationCallback();
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0003DCC0 File Offset: 0x0003BEC0
		public void PostCalculation()
		{
			if (!this.manuallyControlled)
			{
				this.CalculatedTargetPoint = this.calculatedTargetPoint;
				this.CalculatedSpeed = this.calculatedSpeed;
			}
			List<ObstacleVertex> list = this.obstaclesBuffered;
			this.obstaclesBuffered = this.obstacles;
			this.obstacles = list;
			this.manuallyControlled = false;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0003DD10 File Offset: 0x0003BF10
		public void CalculateNeighbours()
		{
			this.neighbours.Clear();
			this.neighbourDists.Clear();
			if (this.MaxNeighbours > 0 && !this.locked)
			{
				this.simulator.Quadtree.Query(this.position, this.maxSpeed, this.agentTimeHorizon, this.radius, this);
			}
			this.NeighbourCount = this.neighbours.Count;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		private static float Sqr(float x)
		{
			return x * x;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0003DD84 File Offset: 0x0003BF84
		internal float InsertAgentNeighbour(Agent agent, float rangeSq)
		{
			if (this == agent || (agent.layer & this.collidesWith) == (RVOLayer)0)
			{
				return rangeSq;
			}
			float sqrMagnitude = (agent.position - this.position).sqrMagnitude;
			if (sqrMagnitude < rangeSq)
			{
				if (this.neighbours.Count < this.maxNeighbours)
				{
					this.neighbours.Add(null);
					this.neighbourDists.Add(float.PositiveInfinity);
				}
				int num = this.neighbours.Count - 1;
				if (sqrMagnitude < this.neighbourDists[num])
				{
					while (num != 0 && sqrMagnitude < this.neighbourDists[num - 1])
					{
						this.neighbours[num] = this.neighbours[num - 1];
						this.neighbourDists[num] = this.neighbourDists[num - 1];
						num--;
					}
					this.neighbours[num] = agent;
					this.neighbourDists[num] = sqrMagnitude;
				}
				if (this.neighbours.Count == this.maxNeighbours)
				{
					rangeSq = this.neighbourDists[this.neighbourDists.Count - 1];
				}
			}
			return rangeSq;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0003DEA9 File Offset: 0x0003C0A9
		private static Vector3 FromXZ(Vector2 p)
		{
			return new Vector3(p.x, 0f, p.y);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0003DEC1 File Offset: 0x0003C0C1
		private static Vector2 ToXZ(Vector3 p)
		{
			return new Vector2(p.x, p.z);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0003DED4 File Offset: 0x0003C0D4
		private Vector2 To2D(Vector3 p, out float elevation)
		{
			if (this.simulator.movementPlane == MovementPlane.XY)
			{
				elevation = -p.z;
				return new Vector2(p.x, p.y);
			}
			elevation = p.y;
			return new Vector2(p.x, p.z);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0003DF24 File Offset: 0x0003C124
		private static void DrawVO(Vector2 circleCenter, float radius, Vector2 origin)
		{
			float num = Mathf.Atan2((origin - circleCenter).y, (origin - circleCenter).x);
			float num2 = radius / (origin - circleCenter).magnitude;
			float num3 = ((num2 <= 1f) ? Mathf.Abs(Mathf.Acos(num2)) : 0f);
			Draw.Debug.CircleXZ(Agent.FromXZ(circleCenter), radius, Color.black, num - num3, num + num3);
			Vector2 vector = new Vector2(Mathf.Cos(num - num3), Mathf.Sin(num - num3)) * radius;
			Vector2 vector2 = new Vector2(Mathf.Cos(num + num3), Mathf.Sin(num + num3)) * radius;
			Vector2 vector3 = -new Vector2(-vector.y, vector.x);
			Vector2 vector4 = new Vector2(-vector2.y, vector2.x);
			vector += circleCenter;
			vector2 += circleCenter;
			Debug.DrawRay(Agent.FromXZ(vector), Agent.FromXZ(vector3).normalized * 100f, Color.black);
			Debug.DrawRay(Agent.FromXZ(vector2), Agent.FromXZ(vector4).normalized * 100f, Color.black);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0003E068 File Offset: 0x0003C268
		internal void CalculateVelocity(Simulator.WorkerContext context)
		{
			if (this.manuallyControlled)
			{
				return;
			}
			if (this.locked)
			{
				this.calculatedSpeed = 0f;
				this.calculatedTargetPoint = this.position;
				return;
			}
			Agent.VOBuffer vos = context.vos;
			vos.Clear();
			this.GenerateObstacleVOs(vos);
			this.GenerateNeighbourAgentVOs(vos);
			if (!Agent.BiasDesiredVelocity(vos, ref this.desiredVelocity, ref this.desiredTargetPointInVelocitySpace, this.simulator.symmetryBreakingBias))
			{
				this.calculatedTargetPoint = this.desiredTargetPointInVelocitySpace + this.position;
				this.calculatedSpeed = this.desiredSpeed;
				if (this.DebugDraw)
				{
					Draw.Debug.CrossXZ(Agent.FromXZ(this.calculatedTargetPoint), Color.white, 1f);
				}
				return;
			}
			Vector2 vector = Vector2.zero;
			vector = this.GradientDescent(vos, this.currentVelocity, this.desiredVelocity);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector + this.position), Color.white, 1f);
			}
			this.calculatedTargetPoint = this.position + vector;
			this.calculatedSpeed = Mathf.Min(vector.magnitude, this.maxSpeed);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0003E194 File Offset: 0x0003C394
		private static Color Rainbow(float v)
		{
			Color color = new Color(v, 0f, 0f);
			if (color.r > 1f)
			{
				color.g = color.r - 1f;
				color.r = 1f;
			}
			if (color.g > 1f)
			{
				color.b = color.g - 1f;
				color.g = 1f;
			}
			return color;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0003E20C File Offset: 0x0003C40C
		private void GenerateObstacleVOs(Agent.VOBuffer vos)
		{
			float num = this.maxSpeed * this.obstacleTimeHorizon;
			for (int i = 0; i < this.simulator.obstacles.Count; i++)
			{
				ObstacleVertex obstacleVertex = this.simulator.obstacles[i];
				ObstacleVertex obstacleVertex2 = obstacleVertex;
				do
				{
					if (obstacleVertex2.ignore || (obstacleVertex2.layer & this.collidesWith) == (RVOLayer)0)
					{
						obstacleVertex2 = obstacleVertex2.next;
					}
					else
					{
						float num2;
						Vector2 vector = this.To2D(obstacleVertex2.position, out num2);
						float num3;
						Vector2 vector2 = this.To2D(obstacleVertex2.next.position, out num3);
						Vector2 normalized = (vector2 - vector).normalized;
						float num4 = Agent.VO.SignedDistanceFromLine(vector, normalized, this.position);
						if (num4 >= -0.01f && num4 < num)
						{
							float num5 = Vector2.Dot(this.position - vector, vector2 - vector) / (vector2 - vector).sqrMagnitude;
							float num6 = Mathf.Lerp(num2, num3, num5);
							if ((Vector2.Lerp(vector, vector2, num5) - this.position).sqrMagnitude < num * num && (this.simulator.movementPlane == MovementPlane.XY || (this.elevationCoordinate <= num6 + obstacleVertex2.height && this.elevationCoordinate + this.height >= num6)))
							{
								vos.Add(Agent.VO.SegmentObstacle(vector2 - this.position, vector - this.position, Vector2.zero, this.radius * 0.01f, 1f / this.ObstacleTimeHorizon, 1f / this.simulator.DeltaTime));
							}
						}
						obstacleVertex2 = obstacleVertex2.next;
					}
				}
				while (obstacleVertex2 != obstacleVertex && obstacleVertex2 != null && obstacleVertex2.next != null);
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0003E3E0 File Offset: 0x0003C5E0
		private void GenerateNeighbourAgentVOs(Agent.VOBuffer vos)
		{
			float num = 1f / this.agentTimeHorizon;
			Vector2 vector = this.currentVelocity;
			for (int i = 0; i < this.neighbours.Count; i++)
			{
				Agent agent = this.neighbours[i];
				if (agent != this)
				{
					float num2 = Math.Min(this.elevationCoordinate + this.height, agent.elevationCoordinate + agent.height);
					float num3 = Math.Max(this.elevationCoordinate, agent.elevationCoordinate);
					if (num2 - num3 >= 0f)
					{
						float num4 = this.radius + agent.radius;
						Vector2 vector2 = agent.position - this.position;
						float num5;
						if (agent.locked || agent.manuallyControlled)
						{
							num5 = 1f;
						}
						else if (agent.Priority > 1E-05f || this.Priority > 1E-05f)
						{
							num5 = agent.Priority / (this.Priority + agent.Priority);
						}
						else
						{
							num5 = 0.5f;
						}
						Vector2 vector3 = Vector2.Lerp(agent.currentVelocity, agent.desiredVelocity, 2f * num5 - 1f);
						Vector2 vector4 = Vector2.Lerp(vector, vector3, num5);
						vos.Add(new Agent.VO(vector2, vector4, num4, num, 1f / this.simulator.DeltaTime));
						if (this.DebugDraw)
						{
							Agent.DrawVO(this.position + vector2 * num + vector4, num4 * num, this.position + vector4);
						}
					}
				}
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0003E56C File Offset: 0x0003C76C
		private Vector2 GradientDescent(Agent.VOBuffer vos, Vector2 sampleAround1, Vector2 sampleAround2)
		{
			float num;
			Vector2 vector = this.Trace(vos, sampleAround1, out num);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector + this.position), Color.yellow, 0.5f);
			}
			float num2;
			Vector2 vector2 = this.Trace(vos, sampleAround2, out num2);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector2 + this.position), Color.magenta, 0.5f);
			}
			if (num >= num2)
			{
				return vector2;
			}
			return vector;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0003E5F0 File Offset: 0x0003C7F0
		private static bool BiasDesiredVelocity(Agent.VOBuffer vos, ref Vector2 desiredVelocity, ref Vector2 targetPointInVelocitySpace, float maxBiasRadians)
		{
			float magnitude = desiredVelocity.magnitude;
			float num = 0f;
			for (int i = 0; i < vos.length; i++)
			{
				float num2;
				vos.buffer[i].Gradient(desiredVelocity, out num2);
				num = Mathf.Max(num, num2);
			}
			bool flag = num > 0f;
			if (magnitude < 0.001f)
			{
				return flag;
			}
			float num3 = Mathf.Min(maxBiasRadians, num / magnitude);
			desiredVelocity += new Vector2(desiredVelocity.y, -desiredVelocity.x) * num3;
			targetPointInVelocitySpace += new Vector2(targetPointInVelocitySpace.y, -targetPointInVelocitySpace.x) * num3;
			return flag;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0003E6B4 File Offset: 0x0003C8B4
		private Vector2 EvaluateGradient(Agent.VOBuffer vos, Vector2 p, out float value)
		{
			Vector2 vector = Vector2.zero;
			value = 0f;
			for (int i = 0; i < vos.length; i++)
			{
				float num;
				Vector2 vector2 = vos.buffer[i].ScaledGradient(p, out num);
				if (num > value)
				{
					value = num;
					vector = vector2;
				}
			}
			Vector2 vector3 = this.desiredVelocity - p;
			float magnitude = vector3.magnitude;
			if (magnitude > 0.0001f)
			{
				vector += vector3 * (0.1f / magnitude);
				value += magnitude * 0.1f;
			}
			float sqrMagnitude = p.sqrMagnitude;
			if (sqrMagnitude > this.desiredSpeed * this.desiredSpeed)
			{
				float num2 = Mathf.Sqrt(sqrMagnitude);
				if (num2 > this.maxSpeed)
				{
					value += 3f * (num2 - this.maxSpeed);
					vector -= 3f * (p / num2);
				}
				float num3 = 0.2f;
				value += num3 * (num2 - this.desiredSpeed);
				vector -= num3 * (p / num2);
			}
			return vector;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0003E7CC File Offset: 0x0003C9CC
		private Vector2 Trace(Agent.VOBuffer vos, Vector2 p, out float score)
		{
			float num = Mathf.Max(this.radius, 0.2f * this.desiredSpeed);
			float num2 = float.PositiveInfinity;
			Vector2 vector = p;
			for (int i = 0; i < 50; i++)
			{
				float num3 = 1f - (float)i / 50f;
				num3 = Agent.Sqr(num3) * num;
				float num4;
				Vector2 vector2 = this.EvaluateGradient(vos, p, out num4);
				if (num4 < num2)
				{
					num2 = num4;
					vector = p;
				}
				vector2.Normalize();
				vector2 *= num3;
				Vector2 vector3 = p;
				p += vector2;
				if (this.DebugDraw)
				{
					Debug.DrawLine(Agent.FromXZ(vector3 + this.position), Agent.FromXZ(p + this.position), Agent.Rainbow((float)i * 0.1f) * new Color(1f, 1f, 1f, 1f));
				}
			}
			score = num2;
			return vector;
		}

		// Token: 0x04000581 RID: 1409
		internal float radius;

		// Token: 0x04000582 RID: 1410
		internal float height;

		// Token: 0x04000583 RID: 1411
		internal float desiredSpeed;

		// Token: 0x04000584 RID: 1412
		internal float maxSpeed;

		// Token: 0x04000585 RID: 1413
		internal float agentTimeHorizon;

		// Token: 0x04000586 RID: 1414
		internal float obstacleTimeHorizon;

		// Token: 0x04000587 RID: 1415
		internal bool locked;

		// Token: 0x04000588 RID: 1416
		private RVOLayer layer;

		// Token: 0x04000589 RID: 1417
		private RVOLayer collidesWith;

		// Token: 0x0400058A RID: 1418
		private int maxNeighbours;

		// Token: 0x0400058B RID: 1419
		internal Vector2 position;

		// Token: 0x0400058C RID: 1420
		private float elevationCoordinate;

		// Token: 0x0400058D RID: 1421
		private Vector2 currentVelocity;

		// Token: 0x0400058E RID: 1422
		private Vector2 desiredTargetPointInVelocitySpace;

		// Token: 0x0400058F RID: 1423
		private Vector2 desiredVelocity;

		// Token: 0x04000590 RID: 1424
		private Vector2 nextTargetPoint;

		// Token: 0x04000591 RID: 1425
		private float nextDesiredSpeed;

		// Token: 0x04000592 RID: 1426
		private float nextMaxSpeed;

		// Token: 0x04000593 RID: 1427
		private Vector2 collisionNormal;

		// Token: 0x04000594 RID: 1428
		private bool manuallyControlled;

		// Token: 0x04000595 RID: 1429
		private bool debugDraw;

		// Token: 0x040005A5 RID: 1445
		internal Agent next;

		// Token: 0x040005A6 RID: 1446
		private float calculatedSpeed;

		// Token: 0x040005A7 RID: 1447
		private Vector2 calculatedTargetPoint;

		// Token: 0x040005A8 RID: 1448
		internal Simulator simulator;

		// Token: 0x040005A9 RID: 1449
		private List<Agent> neighbours = new List<Agent>();

		// Token: 0x040005AA RID: 1450
		private List<float> neighbourDists = new List<float>();

		// Token: 0x040005AB RID: 1451
		private List<ObstacleVertex> obstaclesBuffered = new List<ObstacleVertex>();

		// Token: 0x040005AC RID: 1452
		private List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

		// Token: 0x040005AD RID: 1453
		private const float DesiredVelocityWeight = 0.1f;

		// Token: 0x040005AE RID: 1454
		private const float WallWeight = 5f;

		// Token: 0x02000165 RID: 357
		internal struct VO
		{
			// Token: 0x06000B41 RID: 2881 RVA: 0x00046D24 File Offset: 0x00044F24
			public VO(Vector2 center, Vector2 offset, float radius, float inverseDt, float inverseDeltaTime)
			{
				this.weightFactor = 1f;
				this.weightBonus = 0f;
				this.circleCenter = center * inverseDt + offset;
				this.weightFactor = 4f * Mathf.Exp(-Agent.Sqr(center.sqrMagnitude / (radius * radius))) + 1f;
				if (center.magnitude < radius)
				{
					this.colliding = true;
					this.line1 = center.normalized * (center.magnitude - radius - 0.001f) * 0.3f * inverseDeltaTime;
					this.dir1 = new Vector2(this.line1.y, -this.line1.x).normalized;
					this.line1 += offset;
					this.cutoffDir = Vector2.zero;
					this.cutoffLine = Vector2.zero;
					this.dir2 = Vector2.zero;
					this.line2 = Vector2.zero;
					this.radius = 0f;
				}
				else
				{
					this.colliding = false;
					center *= inverseDt;
					radius *= inverseDt;
					Vector2 vector = center + offset;
					float num = center.magnitude - radius + 0.001f;
					this.cutoffLine = center.normalized * num;
					this.cutoffDir = new Vector2(-this.cutoffLine.y, this.cutoffLine.x).normalized;
					this.cutoffLine += offset;
					float num2 = Mathf.Atan2(-center.y, -center.x);
					float num3 = Mathf.Abs(Mathf.Acos(radius / center.magnitude));
					this.radius = radius;
					this.line1 = new Vector2(Mathf.Cos(num2 + num3), Mathf.Sin(num2 + num3));
					this.dir1 = new Vector2(this.line1.y, -this.line1.x);
					this.line2 = new Vector2(Mathf.Cos(num2 - num3), Mathf.Sin(num2 - num3));
					this.dir2 = new Vector2(this.line2.y, -this.line2.x);
					this.line1 = this.line1 * radius + vector;
					this.line2 = this.line2 * radius + vector;
				}
				this.segmentStart = Vector2.zero;
				this.segmentEnd = Vector2.zero;
				this.segment = false;
			}

			// Token: 0x06000B42 RID: 2882 RVA: 0x00046FBC File Offset: 0x000451BC
			public static Agent.VO SegmentObstacle(Vector2 segmentStart, Vector2 segmentEnd, Vector2 offset, float radius, float inverseDt, float inverseDeltaTime)
			{
				Agent.VO vo = default(Agent.VO);
				vo.weightFactor = 1f;
				vo.weightBonus = Mathf.Max(radius, 1f) * 40f;
				Vector3 vector = VectorMath.ClosestPointOnSegment(segmentStart, segmentEnd, Vector2.zero);
				if (vector.magnitude <= radius)
				{
					vo.colliding = true;
					vo.line1 = vector.normalized * (vector.magnitude - radius) * 0.3f * inverseDeltaTime;
					vo.dir1 = new Vector2(vo.line1.y, -vo.line1.x).normalized;
					vo.line1 += offset;
					vo.cutoffDir = Vector2.zero;
					vo.cutoffLine = Vector2.zero;
					vo.dir2 = Vector2.zero;
					vo.line2 = Vector2.zero;
					vo.radius = 0f;
					vo.segmentStart = Vector2.zero;
					vo.segmentEnd = Vector2.zero;
					vo.segment = false;
				}
				else
				{
					vo.colliding = false;
					segmentStart *= inverseDt;
					segmentEnd *= inverseDt;
					radius *= inverseDt;
					Vector2 normalized = (segmentEnd - segmentStart).normalized;
					vo.cutoffDir = normalized;
					vo.cutoffLine = segmentStart + new Vector2(-normalized.y, normalized.x) * radius;
					vo.cutoffLine += offset;
					float sqrMagnitude = segmentStart.sqrMagnitude;
					Vector2 vector2 = -VectorMath.ComplexMultiply(segmentStart, new Vector2(radius, Mathf.Sqrt(Mathf.Max(0f, sqrMagnitude - radius * radius)))) / sqrMagnitude;
					float sqrMagnitude2 = segmentEnd.sqrMagnitude;
					Vector2 vector3 = -VectorMath.ComplexMultiply(segmentEnd, new Vector2(radius, -Mathf.Sqrt(Mathf.Max(0f, sqrMagnitude2 - radius * radius)))) / sqrMagnitude2;
					vo.line1 = segmentStart + vector2 * radius + offset;
					vo.line2 = segmentEnd + vector3 * radius + offset;
					vo.dir1 = new Vector2(vector2.y, -vector2.x);
					vo.dir2 = new Vector2(vector3.y, -vector3.x);
					vo.segmentStart = segmentStart;
					vo.segmentEnd = segmentEnd;
					vo.radius = radius;
					vo.segment = true;
				}
				return vo;
			}

			// Token: 0x06000B43 RID: 2883 RVA: 0x00047271 File Offset: 0x00045471
			public static float SignedDistanceFromLine(Vector2 a, Vector2 dir, Vector2 p)
			{
				return (p.x - a.x) * dir.y - dir.x * (p.y - a.y);
			}

			// Token: 0x06000B44 RID: 2884 RVA: 0x0004729C File Offset: 0x0004549C
			public Vector2 ScaledGradient(Vector2 p, out float weight)
			{
				Vector2 vector = this.Gradient(p, out weight);
				if (weight > 0f)
				{
					vector *= 2f * this.weightFactor;
					weight *= 2f * this.weightFactor;
					weight += 1f + this.weightBonus;
				}
				return vector;
			}

			// Token: 0x06000B45 RID: 2885 RVA: 0x000472F4 File Offset: 0x000454F4
			public Vector2 Gradient(Vector2 p, out float weight)
			{
				if (this.colliding)
				{
					float num = Agent.VO.SignedDistanceFromLine(this.line1, this.dir1, p);
					if (num >= 0f)
					{
						weight = num;
						return new Vector2(-this.dir1.y, this.dir1.x);
					}
					weight = 0f;
					return new Vector2(0f, 0f);
				}
				else
				{
					float num2 = Agent.VO.SignedDistanceFromLine(this.cutoffLine, this.cutoffDir, p);
					if (num2 <= 0f)
					{
						weight = 0f;
						return Vector2.zero;
					}
					float num3 = Agent.VO.SignedDistanceFromLine(this.line1, this.dir1, p);
					float num4 = Agent.VO.SignedDistanceFromLine(this.line2, this.dir2, p);
					if (num3 < 0f || num4 < 0f)
					{
						weight = 0f;
						return Vector2.zero;
					}
					Vector2 vector;
					if (Vector2.Dot(p - this.line1, this.dir1) > 0f && Vector2.Dot(p - this.line2, this.dir2) < 0f)
					{
						if (!this.segment)
						{
							float num5;
							vector = VectorMath.Normalize(p - this.circleCenter, out num5);
							weight = this.radius - num5;
							return vector;
						}
						if (num2 < this.radius)
						{
							Vector2 vector2 = VectorMath.ClosestPointOnSegment(this.segmentStart, this.segmentEnd, p);
							float num6;
							vector = VectorMath.Normalize(p - vector2, out num6);
							weight = this.radius - num6;
							return vector;
						}
					}
					if (this.segment && num2 < num3 && num2 < num4)
					{
						weight = num2;
						vector = new Vector2(-this.cutoffDir.y, this.cutoffDir.x);
						return vector;
					}
					if (num3 < num4)
					{
						weight = num3;
						vector = new Vector2(-this.dir1.y, this.dir1.x);
					}
					else
					{
						weight = num4;
						vector = new Vector2(-this.dir2.y, this.dir2.x);
					}
					return vector;
				}
			}

			// Token: 0x040007EE RID: 2030
			private Vector2 line1;

			// Token: 0x040007EF RID: 2031
			private Vector2 line2;

			// Token: 0x040007F0 RID: 2032
			private Vector2 dir1;

			// Token: 0x040007F1 RID: 2033
			private Vector2 dir2;

			// Token: 0x040007F2 RID: 2034
			private Vector2 cutoffLine;

			// Token: 0x040007F3 RID: 2035
			private Vector2 cutoffDir;

			// Token: 0x040007F4 RID: 2036
			private Vector2 circleCenter;

			// Token: 0x040007F5 RID: 2037
			private bool colliding;

			// Token: 0x040007F6 RID: 2038
			private float radius;

			// Token: 0x040007F7 RID: 2039
			private float weightFactor;

			// Token: 0x040007F8 RID: 2040
			private float weightBonus;

			// Token: 0x040007F9 RID: 2041
			private Vector2 segmentStart;

			// Token: 0x040007FA RID: 2042
			private Vector2 segmentEnd;

			// Token: 0x040007FB RID: 2043
			private bool segment;
		}

		// Token: 0x02000166 RID: 358
		internal class VOBuffer
		{
			// Token: 0x06000B46 RID: 2886 RVA: 0x00047504 File Offset: 0x00045704
			public void Clear()
			{
				this.length = 0;
			}

			// Token: 0x06000B47 RID: 2887 RVA: 0x0004750D File Offset: 0x0004570D
			public VOBuffer(int n)
			{
				this.buffer = new Agent.VO[n];
				this.length = 0;
			}

			// Token: 0x06000B48 RID: 2888 RVA: 0x00047528 File Offset: 0x00045728
			public void Add(Agent.VO vo)
			{
				if (this.length >= this.buffer.Length)
				{
					Agent.VO[] array = new Agent.VO[this.buffer.Length * 2];
					this.buffer.CopyTo(array, 0);
					this.buffer = array;
				}
				Agent.VO[] array2 = this.buffer;
				int num = this.length;
				this.length = num + 1;
				array2[num] = vo;
			}

			// Token: 0x040007FC RID: 2044
			public Agent.VO[] buffer;

			// Token: 0x040007FD RID: 2045
			public int length;
		}
	}
}
