using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200000F RID: 15
	[AddComponentMenu("Pathfinding/Seeker")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_seeker.php")]
	public class Seeker : VersionedMonoBehaviour
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00007C44 File Offset: 0x00005E44
		public Seeker()
		{
			this.onPathDelegate = new OnPathDelegate(this.OnPathComplete);
			this.onPartialPathDelegate = new OnPathDelegate(this.OnPartialPathComplete);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007CBE File Offset: 0x00005EBE
		protected override void Awake()
		{
			base.Awake();
			this.startEndModifier.Awake(this);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007CD2 File Offset: 0x00005ED2
		public Path GetCurrentPath()
		{
			return this.path;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007CDA File Offset: 0x00005EDA
		public void CancelCurrentPathRequest(bool pool = true)
		{
			if (!this.IsDone())
			{
				this.path.FailWithError("Canceled by script (Seeker.CancelCurrentPathRequest)");
				if (pool)
				{
					this.path.Claim(this.path);
					this.path.Release(this.path, false);
				}
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007D1A File Offset: 0x00005F1A
		public void OnDestroy()
		{
			this.ReleaseClaimedPath();
			this.startEndModifier.OnDestroy(this);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007D2E File Offset: 0x00005F2E
		private void ReleaseClaimedPath()
		{
			if (this.prevPath != null)
			{
				this.prevPath.Release(this, true);
				this.prevPath = null;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007D4C File Offset: 0x00005F4C
		public void RegisterModifier(IPathModifier modifier)
		{
			this.modifiers.Add(modifier);
			this.modifiers.Sort((IPathModifier a, IPathModifier b) => a.Order.CompareTo(b.Order));
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007D84 File Offset: 0x00005F84
		public void DeregisterModifier(IPathModifier modifier)
		{
			this.modifiers.Remove(modifier);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007D93 File Offset: 0x00005F93
		public void PostProcess(Path path)
		{
			this.RunModifiers(Seeker.ModifierPass.PostProcess, path);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007DA0 File Offset: 0x00005FA0
		public void RunModifiers(Seeker.ModifierPass pass, Path path)
		{
			if (pass == Seeker.ModifierPass.PreProcess)
			{
				if (this.preProcessPath != null)
				{
					this.preProcessPath(path);
				}
				for (int i = 0; i < this.modifiers.Count; i++)
				{
					this.modifiers[i].PreProcess(path);
				}
				return;
			}
			if (pass == Seeker.ModifierPass.PostProcess)
			{
				if (this.postProcessPath != null)
				{
					this.postProcessPath(path);
				}
				for (int j = 0; j < this.modifiers.Count; j++)
				{
					this.modifiers[j].Apply(path);
				}
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007E2D File Offset: 0x0000602D
		public bool IsDone()
		{
			return this.path == null || this.path.PipelineState >= PathState.Returned;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007E4A File Offset: 0x0000604A
		private void OnPathComplete(Path path)
		{
			this.OnPathComplete(path, true, true);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007E58 File Offset: 0x00006058
		private void OnPathComplete(Path p, bool runModifiers, bool sendCallbacks)
		{
			if (p != null && p != this.path && sendCallbacks)
			{
				return;
			}
			if (this == null || p == null || p != this.path)
			{
				return;
			}
			if (!this.path.error && runModifiers)
			{
				this.RunModifiers(Seeker.ModifierPass.PostProcess, this.path);
			}
			if (sendCallbacks)
			{
				p.Claim(this);
				this.lastCompletedNodePath = p.path;
				this.lastCompletedVectorPath = p.vectorPath;
				if (this.tmpPathCallback != null)
				{
					this.tmpPathCallback(p);
				}
				if (this.pathCallback != null)
				{
					this.pathCallback(p);
				}
				if (this.prevPath != null)
				{
					this.prevPath.Release(this, true);
				}
				this.prevPath = p;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007F17 File Offset: 0x00006117
		private void OnPartialPathComplete(Path p)
		{
			this.OnPathComplete(p, true, false);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007F22 File Offset: 0x00006122
		private void OnMultiPathComplete(Path p)
		{
			this.OnPathComplete(p, false, true);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007F2D File Offset: 0x0000612D
		[Obsolete("Use ABPath.Construct(start, end, null) instead")]
		public ABPath GetNewPath(Vector3 start, Vector3 end)
		{
			return ABPath.Construct(start, end, null);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007F37 File Offset: 0x00006137
		public Path StartPath(Vector3 start, Vector3 end)
		{
			return this.StartPath(start, end, null);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007F42 File Offset: 0x00006142
		public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback)
		{
			return this.StartPath(ABPath.Construct(start, end, null), callback);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007F53 File Offset: 0x00006153
		public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback, GraphMask graphMask)
		{
			return this.StartPath(ABPath.Construct(start, end, null), callback, graphMask);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007F66 File Offset: 0x00006166
		public Path StartPath(Path p, OnPathDelegate callback = null)
		{
			if (p.nnConstraint.graphMask == -1)
			{
				p.nnConstraint.graphMask = this.graphMask;
			}
			this.StartPathInternal(p, callback);
			return p;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007F95 File Offset: 0x00006195
		public Path StartPath(Path p, OnPathDelegate callback, GraphMask graphMask)
		{
			p.nnConstraint.graphMask = graphMask;
			this.StartPathInternal(p, callback);
			return p;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007FAC File Offset: 0x000061AC
		private void StartPathInternal(Path p, OnPathDelegate callback)
		{
			MultiTargetPath multiTargetPath = p as MultiTargetPath;
			if (multiTargetPath != null)
			{
				OnPathDelegate[] array = new OnPathDelegate[multiTargetPath.targetPoints.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.onPartialPathDelegate;
				}
				multiTargetPath.callbacks = array;
				p.callback = (OnPathDelegate)Delegate.Combine(p.callback, new OnPathDelegate(this.OnMultiPathComplete));
			}
			else
			{
				p.callback = (OnPathDelegate)Delegate.Combine(p.callback, this.onPathDelegate);
			}
			p.enabledTags = this.traversableTags;
			p.tagPenalties = this.tagPenalties;
			if (this.path != null && this.path.PipelineState <= PathState.Processing && this.path.CompleteState != PathCompleteState.Error && this.lastPathID == (uint)this.path.pathID)
			{
				this.path.FailWithError("Canceled path because a new one was requested.\nThis happens when a new path is requested from the seeker when one was already being calculated.\nFor example if a unit got a new order, you might request a new path directly instead of waiting for the now invalid path to be calculated. Which is probably what you want.\nIf you are getting this a lot, you might want to consider how you are scheduling path requests.");
			}
			this.path = p;
			this.tmpPathCallback = callback;
			this.lastPathID = (uint)this.path.pathID;
			this.RunModifiers(Seeker.ModifierPass.PreProcess, this.path);
			AstarPath.StartPath(this.path, false);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000080C8 File Offset: 0x000062C8
		public MultiTargetPath StartMultiTargetPath(Vector3 start, Vector3[] endPoints, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(start, endPoints, null, null);
			multiTargetPath.pathsForAll = pathsForAll;
			this.StartPath(multiTargetPath, callback, graphMask);
			return multiTargetPath;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000080F8 File Offset: 0x000062F8
		public MultiTargetPath StartMultiTargetPath(Vector3[] startPoints, Vector3 end, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(startPoints, end, null, null);
			multiTargetPath.pathsForAll = pathsForAll;
			this.StartPath(multiTargetPath, callback, graphMask);
			return multiTargetPath;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008128 File Offset: 0x00006328
		[Obsolete("You can use StartPath instead of this method now. It will behave identically.")]
		public MultiTargetPath StartMultiTargetPath(MultiTargetPath p, OnPathDelegate callback = null, int graphMask = -1)
		{
			this.StartPath(p, callback, graphMask);
			return p;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000813C File Offset: 0x0000633C
		public void OnDrawGizmos()
		{
			if (this.lastCompletedNodePath == null || !this.drawGizmos)
			{
				return;
			}
			if (this.detailedGizmos)
			{
				Gizmos.color = new Color(0.7f, 0.5f, 0.1f, 0.5f);
				if (this.lastCompletedNodePath != null)
				{
					for (int i = 0; i < this.lastCompletedNodePath.Count - 1; i++)
					{
						Gizmos.DrawLine((Vector3)this.lastCompletedNodePath[i].position, (Vector3)this.lastCompletedNodePath[i + 1].position);
					}
				}
			}
			Gizmos.color = new Color(0f, 1f, 0f, 1f);
			if (this.lastCompletedVectorPath != null)
			{
				for (int j = 0; j < this.lastCompletedVectorPath.Count - 1; j++)
				{
					Gizmos.DrawLine(this.lastCompletedVectorPath[j], this.lastCompletedVectorPath[j + 1]);
				}
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008230 File Offset: 0x00006430
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			if (this.graphMaskCompatibility != -1)
			{
				Debug.Log("Loaded " + this.graphMaskCompatibility.ToString() + " " + this.graphMask.value.ToString());
				this.graphMask = this.graphMaskCompatibility;
				this.graphMaskCompatibility = -1;
			}
			return base.OnUpgradeSerializedData(version, unityThread);
		}

		// Token: 0x040000C1 RID: 193
		public bool drawGizmos = true;

		// Token: 0x040000C2 RID: 194
		public bool detailedGizmos;

		// Token: 0x040000C3 RID: 195
		[HideInInspector]
		public StartEndModifier startEndModifier = new StartEndModifier();

		// Token: 0x040000C4 RID: 196
		[HideInInspector]
		public int traversableTags = -1;

		// Token: 0x040000C5 RID: 197
		[HideInInspector]
		public int[] tagPenalties = new int[32];

		// Token: 0x040000C6 RID: 198
		[HideInInspector]
		public GraphMask graphMask = GraphMask.everything;

		// Token: 0x040000C7 RID: 199
		[FormerlySerializedAs("graphMask")]
		private int graphMaskCompatibility = -1;

		// Token: 0x040000C8 RID: 200
		public OnPathDelegate pathCallback;

		// Token: 0x040000C9 RID: 201
		public OnPathDelegate preProcessPath;

		// Token: 0x040000CA RID: 202
		public OnPathDelegate postProcessPath;

		// Token: 0x040000CB RID: 203
		[NonSerialized]
		private List<Vector3> lastCompletedVectorPath;

		// Token: 0x040000CC RID: 204
		[NonSerialized]
		private List<GraphNode> lastCompletedNodePath;

		// Token: 0x040000CD RID: 205
		[NonSerialized]
		protected Path path;

		// Token: 0x040000CE RID: 206
		[NonSerialized]
		private Path prevPath;

		// Token: 0x040000CF RID: 207
		private readonly OnPathDelegate onPathDelegate;

		// Token: 0x040000D0 RID: 208
		private readonly OnPathDelegate onPartialPathDelegate;

		// Token: 0x040000D1 RID: 209
		private OnPathDelegate tmpPathCallback;

		// Token: 0x040000D2 RID: 210
		protected uint lastPathID;

		// Token: 0x040000D3 RID: 211
		private readonly List<IPathModifier> modifiers = new List<IPathModifier>();

		// Token: 0x020000F1 RID: 241
		public enum ModifierPass
		{
			// Token: 0x04000617 RID: 1559
			PreProcess,
			// Token: 0x04000618 RID: 1560
			PostProcess = 2
		}
	}
}
