using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000035 RID: 53
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_animation_link.php")]
	public class AnimationLink : NodeLink2
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		private static Transform SearchRec(Transform tr, string name)
		{
			int childCount = tr.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = tr.GetChild(i);
				if (child.name == name)
				{
					return child;
				}
				Transform transform = AnimationLink.SearchRec(child, name);
				if (transform != null)
				{
					return transform;
				}
			}
			return null;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000CF00 File Offset: 0x0000B100
		public void CalculateOffsets(List<Vector3> trace, out Vector3 endPosition)
		{
			endPosition = base.transform.position;
			if (this.referenceMesh == null)
			{
				return;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.referenceMesh, base.transform.position, base.transform.rotation);
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			Transform transform = AnimationLink.SearchRec(gameObject.transform, this.boneRoot);
			if (transform == null)
			{
				throw new Exception("Could not find root transform");
			}
			Animation animation = gameObject.GetComponent<Animation>();
			if (animation == null)
			{
				animation = gameObject.AddComponent<Animation>();
			}
			for (int i = 0; i < this.sequence.Length; i++)
			{
				animation.AddClip(this.sequence[i].clip, this.sequence[i].clip.name);
			}
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = base.transform.position;
			Vector3 vector3 = Vector3.zero;
			for (int j = 0; j < this.sequence.Length; j++)
			{
				AnimationLink.LinkClip linkClip = this.sequence[j];
				if (linkClip == null)
				{
					endPosition = vector2;
					return;
				}
				animation[linkClip.clip.name].enabled = true;
				animation[linkClip.clip.name].weight = 1f;
				for (int k = 0; k < linkClip.loopCount; k++)
				{
					animation[linkClip.clip.name].normalizedTime = 0f;
					animation.Sample();
					Vector3 vector4 = transform.position - base.transform.position;
					if (j > 0)
					{
						vector2 += vector - vector4;
					}
					else
					{
						vector3 = vector4;
					}
					for (int l = 0; l <= 20; l++)
					{
						float num = (float)l / 20f;
						animation[linkClip.clip.name].normalizedTime = num;
						animation.Sample();
						Vector3 vector5 = vector2 + (transform.position - base.transform.position) + linkClip.velocity * num * linkClip.clip.length;
						trace.Add(vector5);
					}
					vector2 += linkClip.velocity * 1f * linkClip.clip.length;
					animation[linkClip.clip.name].normalizedTime = 1f;
					animation.Sample();
					vector = transform.position - base.transform.position;
				}
				animation[linkClip.clip.name].enabled = false;
				animation[linkClip.clip.name].weight = 0f;
			}
			vector2 += vector - vector3;
			Object.DestroyImmediate(gameObject);
			endPosition = vector2;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000D208 File Offset: 0x0000B408
		public override void OnDrawGizmosSelected()
		{
			base.OnDrawGizmosSelected();
			List<Vector3> list = ListPool<Vector3>.Claim();
			Vector3 zero = Vector3.zero;
			this.CalculateOffsets(list, out zero);
			Gizmos.color = Color.blue;
			for (int i = 0; i < list.Count - 1; i++)
			{
				Gizmos.DrawLine(list[i], list[i + 1]);
			}
		}

		// Token: 0x04000183 RID: 387
		public string clip;

		// Token: 0x04000184 RID: 388
		public float animSpeed = 1f;

		// Token: 0x04000185 RID: 389
		public bool reverseAnim = true;

		// Token: 0x04000186 RID: 390
		public GameObject referenceMesh;

		// Token: 0x04000187 RID: 391
		public AnimationLink.LinkClip[] sequence;

		// Token: 0x04000188 RID: 392
		public string boneRoot = "bn_COG_Root";

		// Token: 0x020000FA RID: 250
		[Serializable]
		public class LinkClip
		{
			// Token: 0x17000174 RID: 372
			// (get) Token: 0x06000A0E RID: 2574 RVA: 0x000402BB File Offset: 0x0003E4BB
			public string name
			{
				get
				{
					if (!(this.clip != null))
					{
						return "";
					}
					return this.clip.name;
				}
			}

			// Token: 0x04000630 RID: 1584
			public AnimationClip clip;

			// Token: 0x04000631 RID: 1585
			public Vector3 velocity;

			// Token: 0x04000632 RID: 1586
			public int loopCount = 1;
		}
	}
}
