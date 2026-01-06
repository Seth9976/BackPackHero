using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000105 RID: 261
public class ItemAnimationProxy : MonoBehaviour
{
	// Token: 0x060008EC RID: 2284 RVA: 0x0005D7F2 File Offset: 0x0005B9F2
	private void Awake()
	{
		ItemAnimationProxy.proxies.Add(this);
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x0005D7FF File Offset: 0x0005B9FF
	private void OnDestroy()
	{
		ItemAnimationProxy.proxies.Remove(this);
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x0005D80D File Offset: 0x0005BA0D
	private void Start()
	{
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x0005D80F File Offset: 0x0005BA0F
	private void Update()
	{
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x0005D814 File Offset: 0x0005BA14
	public static ItemAnimationProxy GetProxy(Transform parent)
	{
		foreach (ItemAnimationProxy itemAnimationProxy in ItemAnimationProxy.proxies)
		{
			if (itemAnimationProxy.parentItem == parent)
			{
				return itemAnimationProxy;
			}
		}
		return null;
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x0005D874 File Offset: 0x0005BA74
	public void ChangeSettings(Vector3 start, Vector3 end, Quaternion startingRotation, float _time)
	{
		this.startScale = start;
		this.endScale = end;
		this.startRotation = startingRotation;
		this.time = _time;
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x0005D893 File Offset: 0x0005BA93
	public void Reset()
	{
		base.StopAllCoroutines();
		this.isFlying = false;
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x0005D8A4 File Offset: 0x0005BAA4
	public void CopySprite(SpriteRenderer spriteRenderer)
	{
		this.parentItem = spriteRenderer.transform;
		SpriteRenderer componentInChildren = base.GetComponentInChildren<SpriteRenderer>();
		if (!componentInChildren)
		{
			Debug.LogError("No Sprite Renderer found");
			return;
		}
		componentInChildren.sprite = spriteRenderer.sprite;
		componentInChildren.color = spriteRenderer.color;
		componentInChildren.sortingOrder = spriteRenderer.sortingOrder;
		ParticleSystem.ShapeModule shape = base.GetComponentInChildren<ParticleSystem>().shape;
		shape.shapeType = ParticleSystemShapeType.Sprite;
		shape.sprite = spriteRenderer.sprite;
		shape.texture = spriteRenderer.sprite.texture;
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x0005D92F File Offset: 0x0005BB2F
	public void FlyTo(Vector2 start, Transform destination)
	{
		if (this.isFlying)
		{
			return;
		}
		base.transform.position = start;
		base.StartCoroutine(this.FlyToRoutine(destination));
		this.isFlying = true;
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0005D960 File Offset: 0x0005BB60
	private IEnumerator FlyToRoutine(Transform destination)
	{
		float t = 0f;
		Vector3 startPos = new Vector3(base.transform.position.x, base.transform.position.y, -4f);
		Vector3 destination2 = new Vector3(destination.position.x, destination.position.y, -4f);
		while (t < this.time)
		{
			base.transform.position = Vector3.Lerp(startPos, destination2, t / this.time);
			base.transform.localScale = Vector3.Lerp(this.startScale, this.endScale, t / this.time);
			if (this.rotate)
			{
				base.transform.rotation = Quaternion.Lerp(this.startRotation, Quaternion.Euler(0f, 0f, this.startRotation.eulerAngles.z + 180f), t / this.time);
			}
			t += Time.deltaTime;
			yield return null;
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x0005D976 File Offset: 0x0005BB76
	public void FlyTo(Vector2 start, Transform destination, Vector3 offset)
	{
		if (this.isFlying)
		{
			return;
		}
		base.transform.position = start;
		base.StartCoroutine(this.FlyToRoutine(destination, offset));
		this.isFlying = true;
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x0005D9A8 File Offset: 0x0005BBA8
	private IEnumerator FlyToRoutine(Transform destination, Vector3 offset)
	{
		float t = 0f;
		Vector3 startPos = new Vector3(base.transform.position.x, base.transform.position.y, -2f);
		while (t < this.time && destination)
		{
			base.transform.position = Vector3.Lerp(startPos, new Vector3(destination.position.x + offset.x, destination.position.y + offset.y, -2f + offset.z), t / this.time);
			base.transform.localScale = Vector3.Lerp(this.startScale, this.endScale, t / this.time);
			if (this.rotate)
			{
				base.transform.rotation = Quaternion.Lerp(this.startRotation, Quaternion.Euler(0f, 0f, this.startRotation.eulerAngles.z + 180f), t / this.time);
			}
			t += Time.deltaTime;
			yield return null;
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x04000710 RID: 1808
	private static List<ItemAnimationProxy> proxies = new List<ItemAnimationProxy>();

	// Token: 0x04000711 RID: 1809
	[Header("Settings")]
	[SerializeField]
	public bool rotate = true;

	// Token: 0x04000712 RID: 1810
	[SerializeField]
	private float time = 0.8f;

	// Token: 0x04000713 RID: 1811
	private bool isFlying;

	// Token: 0x04000714 RID: 1812
	private Vector3 startScale = Vector3.one;

	// Token: 0x04000715 RID: 1813
	private Vector3 endScale = Vector3.zero;

	// Token: 0x04000716 RID: 1814
	private Quaternion startRotation = Quaternion.identity;

	// Token: 0x04000717 RID: 1815
	private Transform parentItem;
}
