using System;
using System.Collections.Generic;
using Pathfinding.Drawing;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000060 RID: 96
	[HelpURL("https://arongranberg.com/astar/documentation/stable/animationlink.html")]
	public class AnimationLink : NodeLink2
	{
		// Token: 0x06000378 RID: 888 RVA: 0x00010AF8 File Offset: 0x0000ECF8
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

		// Token: 0x06000379 RID: 889 RVA: 0x00010B44 File Offset: 0x0000ED44
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

		// Token: 0x0600037A RID: 890 RVA: 0x00010E4C File Offset: 0x0000F04C
		public override void DrawGizmos()
		{
			base.DrawGizmos();
			List<Vector3> list = ListPool<Vector3>.Claim();
			Vector3 zero = Vector3.zero;
			this.CalculateOffsets(list, out zero);
			for (int i = 0; i < list.Count - 1; i++)
			{
				Draw.Line(list[i], list[i + 1], Color.blue);
			}
		}

		// Token: 0x04000212 RID: 530
		public string clip;

		// Token: 0x04000213 RID: 531
		public float animSpeed = 1f;

		// Token: 0x04000214 RID: 532
		public bool reverseAnim = true;

		// Token: 0x04000215 RID: 533
		public GameObject referenceMesh;

		// Token: 0x04000216 RID: 534
		public AnimationLink.LinkClip[] sequence;

		// Token: 0x04000217 RID: 535
		public string boneRoot = "bn_COG_Root";

		// Token: 0x02000061 RID: 97
		[Serializable]
		public class LinkClip
		{
			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x0600037C RID: 892 RVA: 0x00010EC6 File Offset: 0x0000F0C6
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

			// Token: 0x04000218 RID: 536
			public AnimationClip clip;

			// Token: 0x04000219 RID: 537
			public Vector3 velocity;

			// Token: 0x0400021A RID: 538
			public int loopCount = 1;
		}
	}
}
