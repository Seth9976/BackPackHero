using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class BackgroundController : MonoBehaviour
{
	// Token: 0x060008B0 RID: 2224 RVA: 0x0005B70B File Offset: 0x0005990B
	private void Start()
	{
		this.gameManager = GameManager.main;
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0005B718 File Offset: 0x00059918
	private void Update()
	{
		if (!this.running)
		{
			return;
		}
		if (this.playerTransform.position.x < 0f)
		{
			this.playerTransform.position = Vector3.MoveTowards(this.playerTransform.position, new Vector3(3f, this.playerTransform.position.y, this.playerTransform.position.z), 5.2f * Time.deltaTime);
			return;
		}
		this.ScrollBackground(this.speed);
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0005B7A4 File Offset: 0x000599A4
	public void UpdateAllSprites()
	{
		this.particlesParent.transform.position = new Vector3(0f, 0f, 0f);
		if (GameManager.main.dungeonLevel.zone == DungeonLevel.Zone.Chaos)
		{
			this.chaoseParticles.SetActive(true);
		}
		else
		{
			this.chaoseParticles.SetActive(false);
		}
		this.topSprites = null;
		this.middleSprites = null;
		this.floorSprites = null;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			this.ChangeSprites(transform);
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0005B864 File Offset: 0x00059A64
	private IEnumerator RandomizeAnimation(Animator animator)
	{
		animator.enabled = false;
		yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
		if (animator && animator.gameObject.activeInHierarchy)
		{
			animator.enabled = true;
		}
		yield break;
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0005B874 File Offset: 0x00059A74
	private void ChangeSprites(Transform trans)
	{
		if (!this.gameManager)
		{
			this.gameManager = GameManager.main;
		}
		if (trans.CompareTag("Scenery"))
		{
			Object.Destroy(trans.gameObject);
			return;
		}
		if ((Random.Range(0, 2) == 0 && this.gameManager.dungeonLevel.GetStageSprites().wholePieces.Count > 0) || this.gameManager.dungeonLevel.GetStageSprites().walls.Count == 0)
		{
			Animator animator = trans.GetChild(1).GetComponent<Animator>();
			if (!animator)
			{
				animator = trans.GetChild(1).gameObject.AddComponent<Animator>();
			}
			animator.enabled = false;
			trans.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
			int num = Random.Range(0, this.gameManager.dungeonLevel.GetStageSprites().wholePieces.Count);
			this.middleSpriteNum++;
			if (this.middleSpriteNum >= this.gameManager.dungeonLevel.GetStageSprites().wholePieces[num].spritesThatMustSpawnInOrder.Count)
			{
				this.middleSpriteNum = 0;
			}
			trans.GetChild(1).GetComponent<SpriteRenderer>().sprite = this.gameManager.dungeonLevel.GetStageSprites().wholePieces[num].spritesThatMustSpawnInOrder[this.middleSpriteNum];
			if (this.gameManager.dungeonLevel.GetStageSprites().wholePieces[num].animators.Count > 0)
			{
				trans.GetChild(1).gameObject.SetActive(true);
				if (animator && this.gameManager.dungeonLevel.GetStageSprites().wholePieces[num].animators != null && this.gameManager.dungeonLevel.GetStageSprites().wholePieces[num].animators.Count > 0)
				{
					animator.enabled = false;
					animator.runtimeAnimatorController = this.gameManager.dungeonLevel.GetStageSprites().wholePieces[num].animators[0];
					base.StartCoroutine(this.RandomizeAnimation(animator));
				}
			}
			trans.GetChild(2).GetComponent<SpriteRenderer>().sprite = null;
			return;
		}
		if (this.topSprites == null || this.topSpriteNum >= this.topSprites.spritesThatMustSpawnInOrder.Count)
		{
			this.topSpriteNum = 0;
			int num2 = Random.Range(0, this.gameManager.dungeonLevel.GetStageSprites().ceilings.Count);
			this.topSprites = this.gameManager.dungeonLevel.GetStageSprites().ceilings[num2];
			this.ConsiderSceneryPrefab(this.gameManager.dungeonLevel.GetStageSprites().ceilings[num2].sceneryPrefabs, trans);
		}
		trans.GetChild(0).GetComponent<SpriteRenderer>().sprite = this.topSprites.spritesThatMustSpawnInOrder[this.topSpriteNum];
		this.topSpriteNum++;
		if (this.middleSprites == null || this.middleSpriteNum >= this.middleSprites.spritesThatMustSpawnInOrder.Count)
		{
			this.middleSpriteNum = 0;
			int num2 = Random.Range(0, this.gameManager.dungeonLevel.GetStageSprites().walls.Count);
			this.middleSprites = this.gameManager.dungeonLevel.GetStageSprites().walls[num2];
			this.ConsiderSceneryPrefab(this.gameManager.dungeonLevel.GetStageSprites().walls[num2].sceneryPrefabs, trans);
		}
		trans.GetChild(1).GetComponent<SpriteRenderer>().sprite = this.middleSprites.spritesThatMustSpawnInOrder[this.middleSpriteNum];
		this.middleSpriteNum++;
		Animator component = trans.GetChild(1).gameObject.GetComponent<Animator>();
		if (component)
		{
			Object.Destroy(component);
		}
		if (this.floorSprites == null || this.floorSpriteNum >= this.floorSprites.spritesThatMustSpawnInOrder.Count)
		{
			this.floorSpriteNum = 0;
			int num2 = Random.Range(0, this.gameManager.dungeonLevel.GetStageSprites().floors.Count);
			this.floorSprites = this.gameManager.dungeonLevel.GetStageSprites().floors[num2];
			this.ConsiderSceneryPrefab(this.gameManager.dungeonLevel.GetStageSprites().floors[num2].sceneryPrefabs, trans);
		}
		trans.GetChild(2).GetComponent<SpriteRenderer>().sprite = this.floorSprites.spritesThatMustSpawnInOrder[this.floorSpriteNum];
		this.floorSpriteNum++;
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x0005BD30 File Offset: 0x00059F30
	private void ConsiderSceneryPrefab(List<GameObject> sceneryPrefabs, Transform trans)
	{
		Vector3 vector = trans.position + Vector3.back;
		if (sceneryPrefabs.Count > 0 && Random.Range(0, 2) == 0)
		{
			int num = Random.Range(0, sceneryPrefabs.Count);
			Object.Instantiate<GameObject>(sceneryPrefabs[num], new Vector3(vector.x + Random.Range(-0.5f, 0.5f), sceneryPrefabs[num].transform.position.y, vector.z), Quaternion.identity, trans.parent);
		}
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0005BDC0 File Offset: 0x00059FC0
	public void ScrollBackground(float speed)
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.localPosition.x < -12f)
			{
				if (transform.CompareTag("Scenery"))
				{
					Object.Destroy(transform.gameObject);
				}
				else
				{
					transform.localPosition += Vector3.right * 12f * 2f;
					this.ChangeSprites(transform);
				}
			}
		}
		foreach (object obj2 in base.transform)
		{
			((Transform)obj2).localPosition += Vector3.left * speed * Time.deltaTime;
		}
		this.particlesParent.transform.position += Vector3.left * speed * Time.deltaTime;
		foreach (object obj3 in this.visuals)
		{
			Transform transform2 = (Transform)obj3;
			if (!this.running || !(transform2 == this.playerTransform))
			{
				transform2.position += Vector3.left * speed * Time.deltaTime;
			}
		}
	}

	// Token: 0x040006D1 RID: 1745
	[SerializeField]
	private float speed;

	// Token: 0x040006D2 RID: 1746
	public bool running;

	// Token: 0x040006D3 RID: 1747
	[SerializeField]
	private Transform playerTransform;

	// Token: 0x040006D4 RID: 1748
	[SerializeField]
	private Transform visuals;

	// Token: 0x040006D5 RID: 1749
	[Header("Stage Effects")]
	[SerializeField]
	private GameObject particlesParent;

	// Token: 0x040006D6 RID: 1750
	[SerializeField]
	private GameObject chaoseParticles;

	// Token: 0x040006D7 RID: 1751
	private GameManager gameManager;

	// Token: 0x040006D8 RID: 1752
	private DungeonLevel.SpriteList topSprites;

	// Token: 0x040006D9 RID: 1753
	private int topSpriteNum;

	// Token: 0x040006DA RID: 1754
	private DungeonLevel.SpriteList middleSprites;

	// Token: 0x040006DB RID: 1755
	private int middleSpriteNum;

	// Token: 0x040006DC RID: 1756
	private DungeonLevel.SpriteList floorSprites;

	// Token: 0x040006DD RID: 1757
	private int floorSpriteNum;

	// Token: 0x02000376 RID: 886
	[Serializable]
	private class StageSprites
	{
		// Token: 0x040014F8 RID: 5368
		public Sprite[] ceilings;

		// Token: 0x040014F9 RID: 5369
		public Sprite[] walls;

		// Token: 0x040014FA RID: 5370
		public Sprite[] floors;

		// Token: 0x040014FB RID: 5371
		public GameObject[] sceneryPrefabs;
	}
}
