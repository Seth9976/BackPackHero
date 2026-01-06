using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200004A RID: 74
public class DigitalCursorInterface : MonoBehaviour
{
	// Token: 0x06000142 RID: 322 RVA: 0x00008583 File Offset: 0x00006783
	public bool IsEnabling()
	{
		return this.firstFrame;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0000858C File Offset: 0x0000678C
	private void OnEnable()
	{
		DigitalCursorInterface.digitalCursorInterfaces.Insert(0, this);
		this.firstFrame = true;
		if (DigitalCursor.main == null)
		{
			Console.Error.WriteLine("DigitalCursor has not assigned main, " + base.gameObject.name);
		}
		DigitalCursor.main.digitalCursorInterface = this;
		DigitalCursor.main.ToggleSprite(false);
		if (this.previousSelectable)
		{
			Selectable component = this.previousSelectable.GetComponent<Selectable>();
			if (component.interactable && component.gameObject.activeInHierarchy)
			{
				EventSystem.current.SetSelectedGameObject(component.gameObject);
			}
			this.previousSelectable = null;
		}
		if (this.canvasGroup)
		{
			this.canvasGroup.alpha = 1f;
			this.canvasGroup.interactable = true;
			this.canvasGroup.blocksRaycasts = true;
		}
		UnityEvent onEnableInterface = this.OnEnableInterface;
		if (onEnableInterface == null)
		{
			return;
		}
		onEnableInterface.Invoke();
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00008678 File Offset: 0x00006878
	private void OnDisable()
	{
		this.firstFrame = true;
		DigitalCursorInterface.digitalCursorInterfaces.Remove(this);
		if (DigitalCursor.main.digitalCursorInterface == this)
		{
			DigitalCursor.main.EndInteractionWithDigitalCursorInterface();
			if (EventSystem.current && EventSystem.current.currentSelectedGameObject && EventSystem.current.currentSelectedGameObject.transform.IsChildOf(base.transform))
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
		}
		this.ToggleOtherCanvasGroups(true);
		UnityEvent onCloseInterface = this.OnCloseInterface;
		if (onCloseInterface == null)
		{
			return;
		}
		onCloseInterface.Invoke();
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00008710 File Offset: 0x00006910
	public void Toggle()
	{
		base.enabled = !base.enabled;
		if (!base.enabled && DigitalCursor.main.digitalCursorInterface == this)
		{
			DigitalCursor.main.digitalCursorInterface = null;
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000875C File Offset: 0x0000695C
	public void SetCursorVisiblity(bool toggle)
	{
		this.showCursor = toggle;
		DigitalCursor.main.ToggleSprite(this.showCursor);
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00008775 File Offset: 0x00006975
	public void Toggle(bool toggle)
	{
		base.enabled = toggle;
		if (!base.enabled && DigitalCursor.main.digitalCursorInterface == this)
		{
			DigitalCursor.main.digitalCursorInterface = null;
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	// Token: 0x06000148 RID: 328 RVA: 0x000087B0 File Offset: 0x000069B0
	private void Setup()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		if (!this.canvasGroup)
		{
			Debug.LogError("DigitalCursorInterface: " + base.gameObject.name);
			Debug.LogError("DigitalCursorInterface: Missing canvas group");
			Object.Destroy(this);
			return;
		}
		this.canvasGroups.Clear();
		this.canvasGroups = Object.FindObjectsOfType<CanvasGroup>().ToList<CanvasGroup>();
		this.GetSelectables();
		this.ToggleOtherCanvasGroups(false);
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00008829 File Offset: 0x00006A29
	public void GetSelectables()
	{
		if (this.timeSinceLastGet > 0f)
		{
			this.timeSinceLastGet -= Time.deltaTime;
			return;
		}
		this.timeSinceLastGet = 0.1f;
		this.FoceGetSelectables();
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000885C File Offset: 0x00006A5C
	private void FoceGetSelectables()
	{
		this.selectables.Clear();
		this.selectables = base.GetComponentsInChildren<Selectable>().ToList<Selectable>();
		if (!this.defaultSelectable)
		{
			for (int i = 0; i < this.selectables.Count; i++)
			{
				if (this.selectables[i].interactable && this.selectables[i].gameObject.activeInHierarchy && this.selectables[i].navigation.mode != Navigation.Mode.None)
				{
					this.defaultSelectable = this.selectables[i];
					break;
				}
			}
		}
		for (int j = 0; j < this.selectables.Count; j++)
		{
			if (this.selectables[j].GetComponent<NavMoveCapturer>() == null)
			{
				this.selectables[j].gameObject.AddComponent<NavMoveCapturer>().cursorInterface = this;
			}
		}
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00008950 File Offset: 0x00006B50
	private void ToggleOtherCanvasGroups(bool toggle)
	{
		if (!this.disablesCanvasGroups)
		{
			return;
		}
		foreach (CanvasGroup canvasGroup in this.canvasGroups)
		{
			if (canvasGroup)
			{
				if (canvasGroup != this.canvasGroup && !canvasGroup.transform.IsChildOf(base.transform) && !base.transform.IsChildOf(canvasGroup.transform))
				{
					canvasGroup.interactable = toggle;
					canvasGroup.blocksRaycasts = toggle;
				}
				else if (canvasGroup == this.canvasGroup && canvasGroup.transform.IsChildOf(base.transform))
				{
					canvasGroup.interactable = !toggle;
					canvasGroup.blocksRaycasts = !toggle;
				}
			}
		}
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00008A2C File Offset: 0x00006C2C
	public Selectable GetClosestSelectable(Vector2 worldPos)
	{
		this.GetSelectables();
		Selectable selectable = null;
		float num = float.MaxValue;
		for (int i = 0; i < this.selectables.Count; i++)
		{
			if (this.selectables[i] && this.selectables[i].interactable && this.selectables[i].gameObject.activeInHierarchy && this.selectables[i].navigation.mode != Navigation.Mode.None)
			{
				float num2 = Vector2.Distance(worldPos, this.selectables[i].transform.position);
				if (num2 < num)
				{
					num = num2;
					selectable = this.selectables[i];
				}
			}
		}
		return selectable;
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00008AF4 File Offset: 0x00006CF4
	public void Interact()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.firstFrame)
		{
			this.Setup();
			DigitalCursor.main.ToggleSprite(false);
			this.firstFrame = false;
			return;
		}
		if (this.canvasGroup.alpha < 1f)
		{
			DigitalCursor.main.ToggleSprite(false);
			return;
		}
		if (!EventSystem.current)
		{
			return;
		}
		if (DigitalCursor.main.GetInputDown("cancel"))
		{
			this.OnCancelInput.Invoke();
		}
		if (EventSystem.current && (!EventSystem.current.currentSelectedGameObject || !EventSystem.current.currentSelectedGameObject.transform.IsChildOf(base.transform)))
		{
			if (this.defaultSelectable && this.defaultSelectable.gameObject.activeInHierarchy)
			{
				EventSystem.current.SetSelectedGameObject(this.defaultSelectable.gameObject);
			}
			else
			{
				this.GetSelectables();
				foreach (Selectable selectable in this.selectables)
				{
					if (selectable && selectable.gameObject.activeInHierarchy && selectable.interactable && selectable.navigation.mode != Navigation.Mode.None)
					{
						EventSystem.current.SetSelectedGameObject(selectable.gameObject);
					}
				}
			}
		}
		if (EventSystem.current && EventSystem.current.currentSelectedGameObject && EventSystem.current.currentSelectedGameObject.transform.IsChildOf(base.transform))
		{
			this.previousSelectable = EventSystem.current.currentSelectedGameObject.transform;
		}
		DigitalCursor.main.ToggleSprite(this.showCursor);
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00008CC8 File Offset: 0x00006EC8
	private void Update()
	{
		if (!DigitalCursor.main.digitalCursorInterface || DigitalCursorInterface.digitalCursorInterfaces.IndexOf(this) < DigitalCursorInterface.digitalCursorInterfaces.IndexOf(DigitalCursor.main.digitalCursorInterface))
		{
			DigitalCursor.main.digitalCursorInterface = this;
		}
		if (this.scrollbar && DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			if (DigitalCursor.main.GetInputHold("down") || DigitalCursor.main.moveFreeVector.y < -0.25f)
			{
				this.scrollbar.value -= 10f * Time.deltaTime;
				return;
			}
			if (DigitalCursor.main.GetInputHold("up") || DigitalCursor.main.moveFreeVector.y > 0.25f)
			{
				this.scrollbar.value += 10f * Time.deltaTime;
			}
		}
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00008DBC File Offset: 0x00006FBC
	public void OnMove(AxisEventData eventData, Selectable origin)
	{
		if (origin.navigation.mode != Navigation.Mode.Automatic || !this.navRaycastOverride)
		{
			return;
		}
		Vector3[] array = new Vector3[4];
		origin.GetComponent<RectTransform>().GetWorldCorners(array);
		array = DigitalCursorInterface.CalculateBoundingBox(array);
		Vector3[] array2 = array;
		int num = 0;
		array2[num].x = array2[num].x + 0.1f;
		Vector3[] array3 = array;
		int num2 = 0;
		array3[num2].y = array3[num2].y - 0.1f;
		Vector3[] array4 = array;
		int num3 = 1;
		array4[num3].x = array4[num3].x + 0.1f;
		Vector3[] array5 = array;
		int num4 = 1;
		array5[num4].y = array5[num4].y + 0.1f;
		Vector3[] array6 = array;
		int num5 = 2;
		array6[num5].x = array6[num5].x - 0.1f;
		Vector3[] array7 = array;
		int num6 = 2;
		array7[num6].y = array7[num6].y + 0.1f;
		Vector3[] array8 = array;
		int num7 = 3;
		array8[num7].x = array8[num7].x - 0.1f;
		Vector3[] array9 = array;
		int num8 = 3;
		array9[num8].y = array9[num8].y - 0.1f;
		float num9 = 10000f;
		switch (eventData.moveDir)
		{
		case MoveDirection.Left:
		{
			array[3] = array[0];
			array[2] = array[1];
			Vector3[] array10 = array;
			int num10 = 0;
			array10[num10].x = array10[num10].x - num9;
			Vector3[] array11 = array;
			int num11 = 1;
			array11[num11].x = array11[num11].x - num9;
			break;
		}
		case MoveDirection.Up:
		{
			array[1] = array[0];
			array[2] = array[3];
			Vector3[] array12 = array;
			int num12 = 0;
			array12[num12].y = array12[num12].y + num9;
			Vector3[] array13 = array;
			int num13 = 3;
			array13[num13].y = array13[num13].y + num9;
			break;
		}
		case MoveDirection.Right:
		{
			array[0] = array[3];
			array[1] = array[2];
			Vector3[] array14 = array;
			int num14 = 2;
			array14[num14].x = array14[num14].x + num9;
			Vector3[] array15 = array;
			int num15 = 3;
			array15[num15].x = array15[num15].x + num9;
			break;
		}
		case MoveDirection.Down:
		{
			array[0] = array[1];
			array[3] = array[2];
			Vector3[] array16 = array;
			int num16 = 1;
			array16[num16].y = array16[num16].y - num9;
			Vector3[] array17 = array;
			int num17 = 2;
			array17[num17].y = array17[num17].y - num9;
			break;
		}
		}
		Selectable[] componentsInChildren = origin.gameObject.GetComponentsInChildren<Selectable>();
		List<Selectable> list = new List<Selectable>();
		List<RectTransform> list2 = new List<RectTransform>();
		foreach (Selectable selectable in base.GetComponentsInChildren<Selectable>())
		{
			if (!(selectable == null) && !(selectable == origin) && selectable.enabled && selectable.navigation.mode != Navigation.Mode.None && selectable.gameObject.activeInHierarchy && selectable.interactable)
			{
				if (selectable.GetComponent<NavMoveCapturer>() == null)
				{
					selectable.gameObject.AddComponent<NavMoveCapturer>().cursorInterface = this;
				}
				if (componentsInChildren.Contains(selectable))
				{
					list.Add(selectable);
				}
				list2.Add(selectable.GetComponent<RectTransform>());
			}
		}
		List<GameObject> list3 = new List<GameObject>();
		foreach (Selectable selectable2 in list)
		{
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				if (DigitalCursor.main.transform.position.x > selectable2.transform.position.x)
				{
					list3.Add(selectable2.gameObject);
				}
				break;
			case MoveDirection.Up:
				if (DigitalCursor.main.transform.position.y > selectable2.transform.position.y)
				{
					list3.Add(selectable2.gameObject);
				}
				break;
			case MoveDirection.Right:
				if (DigitalCursor.main.transform.position.x < selectable2.transform.position.x)
				{
					list3.Add(selectable2.gameObject);
				}
				break;
			case MoveDirection.Down:
				if (DigitalCursor.main.transform.position.y < selectable2.transform.position.y)
				{
					list3.Add(selectable2.gameObject);
				}
				break;
			}
		}
		if (list3.Count == 0)
		{
			foreach (RectTransform rectTransform in list2)
			{
				Vector3[] array18 = new Vector3[4];
				rectTransform.GetWorldCorners(array18);
				array18 = DigitalCursorInterface.CalculateBoundingBox(array18);
				if (DigitalCursorInterface.DoRectanglesIntersect(array[1], array[3], array18[1], array18[3]))
				{
					if (DigitalCursorInterface.IsParent(rectTransform.gameObject, origin.gameObject))
					{
						Debug.Log("Colliding with parent");
						Vector3[] array19 = new Vector3[]
						{
							array[0],
							array[1],
							array[2],
							array[3]
						};
						float num18 = 0.5f;
						switch (eventData.moveDir)
						{
						case MoveDirection.Left:
						{
							Vector3[] array20 = array19;
							int num19 = 2;
							array20[num19].x = array20[num19].x - num18;
							Vector3[] array21 = array19;
							int num20 = 3;
							array21[num20].x = array21[num20].x - num18;
							break;
						}
						case MoveDirection.Up:
						{
							Vector3[] array22 = array19;
							int num21 = 0;
							array22[num21].y = array22[num21].y + num18;
							Vector3[] array23 = array19;
							int num22 = 3;
							array23[num22].y = array23[num22].y + num18;
							break;
						}
						case MoveDirection.Right:
						{
							Vector3[] array24 = array19;
							int num23 = 0;
							array24[num23].x = array24[num23].x + num18;
							Vector3[] array25 = array19;
							int num24 = 1;
							array25[num24].x = array25[num24].x + num18;
							break;
						}
						case MoveDirection.Down:
						{
							Vector3[] array26 = array19;
							int num25 = 1;
							array26[num25].y = array26[num25].y - num18;
							Vector3[] array27 = array19;
							int num26 = 2;
							array27[num26].y = array27[num26].y - num18;
							break;
						}
						}
						array19 = DigitalCursorInterface.CalculateBoundingBox(array19);
						if (DigitalCursorInterface.DoRectanglesIntersect(array19[1], array19[3], array18[1], array18[3]))
						{
							list3.Add(rectTransform.gameObject);
						}
					}
					else
					{
						list3.Add(rectTransform.gameObject);
					}
				}
			}
		}
		if (list3.Count > 0)
		{
			list3.Sort(delegate(GameObject a, GameObject b)
			{
				float num27 = Vector3.Distance(origin.transform.position, a.transform.position);
				float num28 = Vector3.Distance(origin.transform.position, b.transform.position);
				return num27.CompareTo(num28);
			});
			EventSystem.current.SetSelectedGameObject(list3[0].gameObject);
			eventData.Use();
			return;
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00009494 File Offset: 0x00007694
	private static Vector3[] CalculateBoundingBox(Vector3[] corners)
	{
		float num = float.MaxValue;
		float num2 = float.MaxValue;
		float num3 = float.MinValue;
		float num4 = float.MinValue;
		for (int i = 0; i < corners.Length; i++)
		{
			Vector2 vector = corners[i];
			num = Math.Min(num, vector.x);
			num2 = Math.Min(num2, vector.y);
			num3 = Math.Max(num3, vector.x);
			num4 = Math.Max(num4, vector.y);
		}
		return new Vector3[]
		{
			new Vector3(num, num4),
			new Vector3(num, num2),
			new Vector3(num3, num2),
			new Vector3(num3, num4)
		};
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00009555 File Offset: 0x00007755
	private static bool DoRectanglesIntersect(Vector2 rect1TopLeft, Vector2 rect1BottomRight, Vector2 rect2TopLeft, Vector2 rect2BottomRight)
	{
		return rect1BottomRight.x >= rect2TopLeft.x && rect2BottomRight.x >= rect1TopLeft.x && rect1BottomRight.y >= rect2TopLeft.y && rect2BottomRight.y >= rect1TopLeft.y;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00009594 File Offset: 0x00007794
	public static bool IsParent(GameObject parentObject, GameObject childObject)
	{
		if (parentObject == null || childObject == null)
		{
			Debug.LogError("Both parentObject and childObject must be valid GameObjects.");
			return false;
		}
		Transform transform = childObject.transform.parent;
		while (transform != null)
		{
			if (transform.gameObject == parentObject)
			{
				return true;
			}
			transform = transform.parent;
		}
		return false;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000095EE File Offset: 0x000077EE
	public void SelectGameWorld(GameObject x)
	{
		DigitalCursor.main.SetGameWorldDestinationTransform(x.transform);
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00009600 File Offset: 0x00007800
	public void FoceSelectElement()
	{
		this.FoceGetSelectables();
		DigitalCursor.main.SelectClosestInDigitalCursorInterface();
	}

	// Token: 0x040000D3 RID: 211
	[SerializeField]
	public UnityEvent OnEnableInterface;

	// Token: 0x040000D4 RID: 212
	public UnityEvent OnCloseInterface;

	// Token: 0x040000D5 RID: 213
	public UnityEvent OnCancelInput;

	// Token: 0x040000D6 RID: 214
	private static List<DigitalCursorInterface> digitalCursorInterfaces = new List<DigitalCursorInterface>();

	// Token: 0x040000D7 RID: 215
	[Header("--------Private References--------")]
	private CanvasGroup canvasGroup;

	// Token: 0x040000D8 RID: 216
	private List<Selectable> selectables = new List<Selectable>();

	// Token: 0x040000D9 RID: 217
	private List<CanvasGroup> canvasGroups = new List<CanvasGroup>();

	// Token: 0x040000DA RID: 218
	[SerializeField]
	private Transform previousSelectable;

	// Token: 0x040000DB RID: 219
	[Header("--------Properties--------")]
	private Selectable currentSelectable;

	// Token: 0x040000DC RID: 220
	[Header("--------Public References--------")]
	[SerializeField]
	public Selectable defaultSelectable;

	// Token: 0x040000DD RID: 221
	[SerializeField]
	public Scrollbar scrollbar;

	// Token: 0x040000DE RID: 222
	[Header("--------Options--------")]
	[SerializeField]
	public bool showCursor = true;

	// Token: 0x040000DF RID: 223
	[SerializeField]
	public bool disablesCanvasGroups = true;

	// Token: 0x040000E0 RID: 224
	[SerializeField]
	public bool navRaycastOverride = true;

	// Token: 0x040000E1 RID: 225
	[SerializeField]
	public bool reselectsPreviousOnReenable = true;

	// Token: 0x040000E2 RID: 226
	private bool firstFrame = true;

	// Token: 0x040000E3 RID: 227
	private float timeSinceLastGet;
}
