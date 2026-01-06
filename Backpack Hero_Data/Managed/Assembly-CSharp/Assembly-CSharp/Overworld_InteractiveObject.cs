using System;
using UnityEngine;

// Token: 0x02000146 RID: 326
public class Overworld_InteractiveObject : CustomInputHandler
{
	// Token: 0x06000C81 RID: 3201 RVA: 0x0008030D File Offset: 0x0007E50D
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(base.transform.position, this.interactionRadius);
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x00080330 File Offset: 0x0007E530
	public static GameObject GetInteractiveObject(Vector2 position)
	{
		RaycastHit2D[] array = Physics2D.CircleCastAll(position, 0.25f, Vector2.zero);
		float num = float.MaxValue;
		GameObject gameObject = null;
		foreach (RaycastHit2D raycastHit2D in array)
		{
			Overworld_InteractiveObject component = raycastHit2D.collider.GetComponent<Overworld_InteractiveObject>();
			if (component != null && Vector2.Distance(position, component.transform.position) < num)
			{
				num = Vector2.Distance(position, component.transform.position);
				gameObject = component.gameObject;
			}
		}
		return gameObject;
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x000803C0 File Offset: 0x0007E5C0
	public virtual void WaitForInteraction()
	{
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x000803C2 File Offset: 0x0007E5C2
	public virtual void Interact()
	{
		Overworld_Manager.main.StartInteraction(this);
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x000803CF File Offset: 0x0007E5CF
	public virtual void EndInteraction()
	{
	}

	// Token: 0x04000A17 RID: 2583
	public float interactionRadius = 0.5f;
}
