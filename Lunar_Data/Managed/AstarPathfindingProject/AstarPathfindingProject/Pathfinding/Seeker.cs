using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200001F RID: 31
	[AddComponentMenu("Pathfinding/Seeker")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/seeker.html")]
	public class Seeker : VersionedMonoBehaviour
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00009090 File Offset: 0x00007290
		public Seeker()
		{
			this.onPathDelegate = new OnPathDelegate(this.OnPathComplete);
			this.onPartialPathDelegate = new OnPathDelegate(this.OnPartialPathComplete);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000910A File Offset: 0x0000730A
		protected override void Awake()
		{
			base.Awake();
			this.startEndModifier.Awake(this);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000911E File Offset: 0x0000731E
		public Path GetCurrentPath()
		{
			return this.path;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009126 File Offset: 0x00007326
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

		// Token: 0x060001B0 RID: 432 RVA: 0x00009166 File Offset: 0x00007366
		private void OnDestroy()
		{
			this.ReleaseClaimedPath();
			this.startEndModifier.OnDestroy(this);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000917A File Offset: 0x0000737A
		private void ReleaseClaimedPath()
		{
			if (this.prevPath != null)
			{
				this.prevPath.Release(this, true);
				this.prevPath = null;
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00009198 File Offset: 0x00007398
		public void RegisterModifier(IPathModifier modifier)
		{
			this.modifiers.Add(modifier);
			this.modifiers.Sort((IPathModifier a, IPathModifier b) => a.Order.CompareTo(b.Order));
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000091D0 File Offset: 0x000073D0
		public void DeregisterModifier(IPathModifier modifier)
		{
			this.modifiers.Remove(modifier);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000091DF File Offset: 0x000073DF
		public void PostProcess(Path path)
		{
			this.RunModifiers(Seeker.ModifierPass.PostProcess, path);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000091EC File Offset: 0x000073EC
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

		// Token: 0x060001B6 RID: 438 RVA: 0x00009279 File Offset: 0x00007479
		public bool IsDone()
		{
			return this.path == null || this.path.PipelineState >= PathState.Returning;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009296 File Offset: 0x00007496
		private void OnPathComplete(Path path)
		{
			this.OnPathComplete(path, true, true);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000092A4 File Offset: 0x000074A4
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
				if (this.tmpPathCallback != null || this.pathCallback != null)
				{
					if (this.tmpPathCallback != null)
					{
						this.tmpPathCallback(p);
					}
					if (this.pathCallback != null)
					{
						this.pathCallback(p);
					}
				}
				if (this.prevPath != null)
				{
					this.prevPath.Release(this, true);
				}
				this.prevPath = p;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000935B File Offset: 0x0000755B
		private void OnPartialPathComplete(Path p)
		{
			this.OnPathComplete(p, true, false);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009366 File Offset: 0x00007566
		private void OnMultiPathComplete(Path p)
		{
			this.OnPathComplete(p, false, true);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009371 File Offset: 0x00007571
		[Obsolete("Use the overload that takes a callback instead")]
		public Path StartPath(Vector3 start, Vector3 end)
		{
			return this.StartPath(start, end, null);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000937C File Offset: 0x0000757C
		public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback)
		{
			return this.StartPath(ABPath.Construct(start, end, null), callback);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000938D File Offset: 0x0000758D
		public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback, GraphMask graphMask)
		{
			return this.StartPath(ABPath.Construct(start, end, null), callback, graphMask);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000093A0 File Offset: 0x000075A0
		public Path StartPath(Path p)
		{
			return this.StartPath(p, null);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000093AA File Offset: 0x000075AA
		public Path StartPath(Path p, OnPathDelegate callback)
		{
			if (p.nnConstraint.graphMask == -1)
			{
				p.nnConstraint.graphMask = this.graphMask;
			}
			this.StartPathInternal(p, callback);
			return p;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000093D9 File Offset: 0x000075D9
		public Path StartPath(Path p, OnPathDelegate callback, GraphMask graphMask)
		{
			p.nnConstraint.graphMask = graphMask;
			this.StartPathInternal(p, callback);
			return p;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000093F0 File Offset: 0x000075F0
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
			if (this.traversalProvider != null)
			{
				p.traversalProvider = this.traversalProvider;
			}
			if (this.path != null && this.path.PipelineState <= PathState.Processing && this.path.CompleteState != PathCompleteState.Error && this.lastPathID == (uint)this.path.pathID)
			{
				this.path.FailWithError("Canceled path because a new one was requested.\nThis happens when a new path is requested from the seeker when one was already being calculated.\nFor example if a unit got a new order, you might request a new path directly instead of waiting for the now invalid path to be calculated. Which is probably what you want.\nIf you are getting this a lot, you might want to consider how you are scheduling path requests.");
			}
			this.path = p;
			this.tmpPathCallback = callback;
			this.lastPathID = (uint)this.path.pathID;
			this.RunModifiers(Seeker.ModifierPass.PreProcess, this.path);
			AstarPath.StartPath(this.path, false, false);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009520 File Offset: 0x00007720
		public MultiTargetPath StartMultiTargetPath(Vector3 start, Vector3[] endPoints, bool pathsForAll, OnPathDelegate callback, int graphMask = -1)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(start, endPoints, null, null);
			multiTargetPath.pathsForAll = pathsForAll;
			this.StartPath(multiTargetPath, callback, graphMask);
			return multiTargetPath;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009550 File Offset: 0x00007750
		public MultiTargetPath StartMultiTargetPath(Vector3[] startPoints, Vector3 end, bool pathsForAll, OnPathDelegate callback, int graphMask = -1)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(startPoints, end, null, null);
			multiTargetPath.pathsForAll = pathsForAll;
			this.StartPath(multiTargetPath, callback, graphMask);
			return multiTargetPath;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009580 File Offset: 0x00007780
		protected override void OnUpgradeSerializedData(ref Migrations migrations, bool unityThread)
		{
			if (this.graphMaskCompatibility != -1)
			{
				this.graphMask = this.graphMaskCompatibility;
				this.graphMaskCompatibility = -1;
			}
			base.OnUpgradeSerializedData(ref migrations, unityThread);
		}

		// Token: 0x04000105 RID: 261
		public bool drawGizmos = true;

		// Token: 0x04000106 RID: 262
		public bool detailedGizmos;

		// Token: 0x04000107 RID: 263
		[HideInInspector]
		public StartEndModifier startEndModifier = new StartEndModifier();

		// Token: 0x04000108 RID: 264
		[HideInInspector]
		public int traversableTags = -1;

		// Token: 0x04000109 RID: 265
		[HideInInspector]
		public int[] tagPenalties = new int[32];

		// Token: 0x0400010A RID: 266
		[HideInInspector]
		public GraphMask graphMask = GraphMask.everything;

		// Token: 0x0400010B RID: 267
		public ITraversalProvider traversalProvider;

		// Token: 0x0400010C RID: 268
		[FormerlySerializedAs("graphMask")]
		private int graphMaskCompatibility = -1;

		// Token: 0x0400010D RID: 269
		[Obsolete("Pass a callback every time to the StartPath method instead, or use ai.SetPath+ai.pathPending on the movement script. You can cache it in your own script if you want to avoid the GC allocation of creating a new delegate.")]
		public OnPathDelegate pathCallback;

		// Token: 0x0400010E RID: 270
		public OnPathDelegate preProcessPath;

		// Token: 0x0400010F RID: 271
		public OnPathDelegate postProcessPath;

		// Token: 0x04000110 RID: 272
		[NonSerialized]
		protected Path path;

		// Token: 0x04000111 RID: 273
		[NonSerialized]
		private Path prevPath;

		// Token: 0x04000112 RID: 274
		private readonly OnPathDelegate onPathDelegate;

		// Token: 0x04000113 RID: 275
		private readonly OnPathDelegate onPartialPathDelegate;

		// Token: 0x04000114 RID: 276
		private OnPathDelegate tmpPathCallback;

		// Token: 0x04000115 RID: 277
		protected uint lastPathID;

		// Token: 0x04000116 RID: 278
		private readonly List<IPathModifier> modifiers = new List<IPathModifier>();

		// Token: 0x02000020 RID: 32
		public enum ModifierPass
		{
			// Token: 0x04000118 RID: 280
			PreProcess,
			// Token: 0x04000119 RID: 281
			PostProcess = 2
		}
	}
}
