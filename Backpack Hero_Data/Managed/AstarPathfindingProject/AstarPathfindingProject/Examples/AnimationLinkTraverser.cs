using System;
using System.Collections;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020000E3 RID: 227
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_examples_1_1_animation_link_traverser.php")]
	public class AnimationLinkTraverser : VersionedMonoBehaviour
	{
		// Token: 0x060009B2 RID: 2482 RVA: 0x0003F03C File Offset: 0x0003D23C
		private void OnEnable()
		{
			this.ai = base.GetComponent<RichAI>();
			if (this.ai != null)
			{
				RichAI richAI = this.ai;
				richAI.onTraverseOffMeshLink = (Func<RichSpecial, IEnumerator>)Delegate.Combine(richAI.onTraverseOffMeshLink, new Func<RichSpecial, IEnumerator>(this.TraverseOffMeshLink));
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0003F08B File Offset: 0x0003D28B
		private void OnDisable()
		{
			if (this.ai != null)
			{
				RichAI richAI = this.ai;
				richAI.onTraverseOffMeshLink = (Func<RichSpecial, IEnumerator>)Delegate.Remove(richAI.onTraverseOffMeshLink, new Func<RichSpecial, IEnumerator>(this.TraverseOffMeshLink));
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0003F0C3 File Offset: 0x0003D2C3
		protected virtual IEnumerator TraverseOffMeshLink(RichSpecial rs)
		{
			AnimationLink link = rs.nodeLink as AnimationLink;
			if (link == null)
			{
				Debug.LogError("Unhandled RichSpecial");
				yield break;
			}
			for (;;)
			{
				Quaternion rotation = this.ai.rotation;
				Quaternion quaternion = this.ai.SimulateRotationTowards(rs.first.forward, this.ai.rotationSpeed * Time.deltaTime);
				if (rotation == quaternion)
				{
					break;
				}
				this.ai.FinalizeMovement(this.ai.position, quaternion);
				yield return null;
			}
			base.transform.parent.position = base.transform.position;
			base.transform.parent.rotation = base.transform.rotation;
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
			if (rs.reverse && link.reverseAnim)
			{
				this.anim[link.clip].speed = -link.animSpeed;
				this.anim[link.clip].normalizedTime = 1f;
				this.anim.Play(link.clip);
				this.anim.Sample();
			}
			else
			{
				this.anim[link.clip].speed = link.animSpeed;
				this.anim.Rewind(link.clip);
				this.anim.Play(link.clip);
			}
			base.transform.parent.position -= base.transform.position - base.transform.parent.position;
			yield return new WaitForSeconds(Mathf.Abs(this.anim[link.clip].length / link.animSpeed));
			yield break;
		}

		// Token: 0x040005D8 RID: 1496
		public Animation anim;

		// Token: 0x040005D9 RID: 1497
		private RichAI ai;
	}
}
