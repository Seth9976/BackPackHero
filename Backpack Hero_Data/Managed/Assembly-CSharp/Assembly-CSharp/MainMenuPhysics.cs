using System;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class MainMenuPhysics : MonoBehaviour
{
	// Token: 0x06000920 RID: 2336 RVA: 0x0005EE10 File Offset: 0x0005D010
	private void Start()
	{
		if (EventManager.instance)
		{
			for (int i = 0; i < this.spritesByEvent.Length; i++)
			{
				if (EventManager.instance.eventType == this.spritesByEvent[i].eventType)
				{
					this.sprites = this.spritesByEvent[i].sprites;
					break;
				}
			}
		}
		MainMenuPhysics.num = 0;
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x0005EE70 File Offset: 0x0005D070
	private void Update()
	{
		if (!this.started)
		{
			return;
		}
		this.time += Time.deltaTime;
		if (this.time > this.timeToSpawn)
		{
			this.time = 0f;
			Vector2 vector = base.transform.position + Vector3.right * (float)Random.Range(0, 24);
			if (this.leftSpawnArea && this.rightSpawnArea)
			{
				vector = Vector2.Lerp(this.leftSpawnArea.position, this.rightSpawnArea.position, Random.Range(0f, 1f));
			}
			Sprite sprite = this.sprites[Random.Range(0, this.sprites.Length)];
			if (this.drawSpritesFromDiscovered && MetaProgressSaveManager.main && MainMenuPhysics.num >= MetaProgressSaveManager.main.itemsDiscovered.Count)
			{
				MainMenuPhysics.num = 0;
			}
			if (this.drawSpritesFromDiscovered && MetaProgressSaveManager.main && MainMenuPhysics.num < MetaProgressSaveManager.main.itemsDiscovered.Count && DebugItemManager.main)
			{
				Item2 item2ByName = DebugItemManager.main.GetItem2ByName(MetaProgressSaveManager.main.itemsDiscovered[MainMenuPhysics.num]);
				if (item2ByName)
				{
					SpriteRenderer component = item2ByName.GetComponent<SpriteRenderer>();
					if (component)
					{
						sprite = component.sprite;
					}
				}
				MainMenuPhysics.num++;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.physicsPrefab, vector, Quaternion.Euler(0f, 0f, (float)Random.Range(0, 360)), base.transform);
			SpriteRenderer component2 = gameObject.GetComponent<SpriteRenderer>();
			component2.sprite = sprite;
			Vector2 vector2 = component2.sprite.bounds.size * 0.65f;
			gameObject.GetComponent<CapsuleCollider2D>().size = vector2;
			gameObject.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-200f, 200f);
		}
	}

	// Token: 0x04000737 RID: 1847
	[SerializeField]
	private MainMenuPhysics.Sprites[] spritesByEvent;

	// Token: 0x04000738 RID: 1848
	private static int num;

	// Token: 0x04000739 RID: 1849
	[SerializeField]
	private bool drawSpritesFromDiscovered;

	// Token: 0x0400073A RID: 1850
	[SerializeField]
	private GameObject physicsPrefab;

	// Token: 0x0400073B RID: 1851
	[SerializeField]
	private float timeToSpawn;

	// Token: 0x0400073C RID: 1852
	[SerializeField]
	private Sprite[] sprites;

	// Token: 0x0400073D RID: 1853
	public bool started;

	// Token: 0x0400073E RID: 1854
	private float time;

	// Token: 0x0400073F RID: 1855
	[SerializeField]
	private Transform leftSpawnArea;

	// Token: 0x04000740 RID: 1856
	[SerializeField]
	private Transform rightSpawnArea;

	// Token: 0x02000382 RID: 898
	[Serializable]
	private class Sprites
	{
		// Token: 0x04001535 RID: 5429
		public Sprite[] sprites;

		// Token: 0x04001536 RID: 5430
		public EventManager.EventType eventType;
	}
}
