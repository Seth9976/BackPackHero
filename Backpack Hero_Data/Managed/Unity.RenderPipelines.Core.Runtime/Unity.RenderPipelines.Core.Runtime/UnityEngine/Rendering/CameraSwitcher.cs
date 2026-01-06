using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200003F RID: 63
	public class CameraSwitcher : MonoBehaviour
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000C59C File Offset: 0x0000A79C
		private void OnEnable()
		{
			this.m_OriginalCamera = base.GetComponent<Camera>();
			this.m_CurrentCamera = this.m_OriginalCamera;
			if (this.m_OriginalCamera == null)
			{
				Debug.LogError("Camera Switcher needs a Camera component attached");
				return;
			}
			this.m_CurrentCameraIndex = this.GetCameraCount() - 1;
			this.m_CameraNames = new GUIContent[this.GetCameraCount()];
			this.m_CameraIndices = new int[this.GetCameraCount()];
			for (int i = 0; i < this.m_Cameras.Length; i++)
			{
				Camera camera = this.m_Cameras[i];
				if (camera != null)
				{
					this.m_CameraNames[i] = new GUIContent(camera.name);
				}
				else
				{
					this.m_CameraNames[i] = new GUIContent("null");
				}
				this.m_CameraIndices[i] = i;
			}
			this.m_CameraNames[this.GetCameraCount() - 1] = new GUIContent("Original Camera");
			this.m_CameraIndices[this.GetCameraCount() - 1] = this.GetCameraCount() - 1;
			this.m_DebugEntry = new DebugUI.EnumField
			{
				displayName = "Camera Switcher",
				getter = () => this.m_CurrentCameraIndex,
				setter = delegate(int value)
				{
					this.SetCameraIndex(value);
				},
				enumNames = this.m_CameraNames,
				enumValues = this.m_CameraIndices,
				getIndex = () => this.m_DebugEntryEnumIndex,
				setIndex = delegate(int value)
				{
					this.m_DebugEntryEnumIndex = value;
				}
			};
			DebugManager.instance.GetPanel("Camera", true, 0, false).children.Add(this.m_DebugEntry);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C727 File Offset: 0x0000A927
		private void OnDisable()
		{
			if (this.m_DebugEntry != null && this.m_DebugEntry.panel != null)
			{
				this.m_DebugEntry.panel.children.Remove(this.m_DebugEntry);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000C75A File Offset: 0x0000A95A
		private int GetCameraCount()
		{
			return this.m_Cameras.Length + 1;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000C766 File Offset: 0x0000A966
		private Camera GetNextCamera()
		{
			if (this.m_CurrentCameraIndex == this.m_Cameras.Length)
			{
				return this.m_OriginalCamera;
			}
			return this.m_Cameras[this.m_CurrentCameraIndex];
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C78C File Offset: 0x0000A98C
		private void SetCameraIndex(int index)
		{
			if (index > 0 && index < this.GetCameraCount())
			{
				this.m_CurrentCameraIndex = index;
				if (this.m_CurrentCamera == this.m_OriginalCamera)
				{
					this.m_OriginalCameraPosition = this.m_OriginalCamera.transform.position;
					this.m_OriginalCameraRotation = this.m_OriginalCamera.transform.rotation;
				}
				this.m_CurrentCamera = this.GetNextCamera();
				if (this.m_CurrentCamera != null)
				{
					if (this.m_CurrentCamera == this.m_OriginalCamera)
					{
						this.m_OriginalCamera.transform.position = this.m_OriginalCameraPosition;
						this.m_OriginalCamera.transform.rotation = this.m_OriginalCameraRotation;
					}
					base.transform.position = this.m_CurrentCamera.transform.position;
					base.transform.rotation = this.m_CurrentCamera.transform.rotation;
				}
			}
		}

		// Token: 0x04000183 RID: 387
		public Camera[] m_Cameras;

		// Token: 0x04000184 RID: 388
		private int m_CurrentCameraIndex = -1;

		// Token: 0x04000185 RID: 389
		private Camera m_OriginalCamera;

		// Token: 0x04000186 RID: 390
		private Vector3 m_OriginalCameraPosition;

		// Token: 0x04000187 RID: 391
		private Quaternion m_OriginalCameraRotation;

		// Token: 0x04000188 RID: 392
		private Camera m_CurrentCamera;

		// Token: 0x04000189 RID: 393
		private GUIContent[] m_CameraNames;

		// Token: 0x0400018A RID: 394
		private int[] m_CameraIndices;

		// Token: 0x0400018B RID: 395
		private DebugUI.EnumField m_DebugEntry;

		// Token: 0x0400018C RID: 396
		private int m_DebugEntryEnumIndex;
	}
}
