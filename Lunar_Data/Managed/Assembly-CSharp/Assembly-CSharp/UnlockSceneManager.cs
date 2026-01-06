using System;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class UnlockSceneManager : MonoBehaviour
{
	// Token: 0x0600047C RID: 1148 RVA: 0x00016234 File Offset: 0x00014434
	private void OnEnable()
	{
		this.inputActions = new InputActions();
		this.inputActions.Enable();
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x0001624C File Offset: 0x0001444C
	private void OnDisable()
	{
		this.inputActions.Disable();
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00016259 File Offset: 0x00014459
	private void Start()
	{
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0001625C File Offset: 0x0001445C
	private void Update()
	{
		this.mainCamera.transform.position += new Vector3(this.movementVector.x, this.movementVector.y, 0f) * 10f * Time.deltaTime;
		if (Input.GetMouseButtonDown(0))
		{
			this.startingMousePos = Input.mousePosition;
			return;
		}
		if (Input.GetMouseButton(0))
		{
			Vector2 vector = Input.mousePosition - this.startingMousePos;
			this.mainCamera.transform.position -= new Vector3(vector.x, vector.y, 0f) * 5f * Time.deltaTime;
			this.startingMousePos = Input.mousePosition;
		}
	}

	// Token: 0x0400037B RID: 891
	[SerializeField]
	private Camera mainCamera;

	// Token: 0x0400037C RID: 892
	public InputActions inputActions;

	// Token: 0x0400037D RID: 893
	private Vector2 movementVector;

	// Token: 0x0400037E RID: 894
	private Vector2 startingMousePos;
}
