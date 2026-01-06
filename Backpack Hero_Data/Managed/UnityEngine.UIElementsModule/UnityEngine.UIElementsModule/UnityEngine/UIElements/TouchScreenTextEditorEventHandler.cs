using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000189 RID: 393
	internal class TouchScreenTextEditorEventHandler : TextEditorEventHandler
	{
		// Token: 0x06000C89 RID: 3209 RVA: 0x000331C6 File Offset: 0x000313C6
		public TouchScreenTextEditorEventHandler(TextEditorEngine editorEngine, ITextInputField textInputField)
			: base(editorEngine, textInputField)
		{
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000331DC File Offset: 0x000313DC
		private void PollTouchScreenKeyboard()
		{
			bool flag = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag)
			{
				bool flag2 = this.m_TouchKeyboardPoller == null;
				if (flag2)
				{
					VisualElement visualElement = base.textInputField as VisualElement;
					this.m_TouchKeyboardPoller = ((visualElement != null) ? visualElement.schedule.Execute(new Action(this.DoPollTouchScreenKeyboard)).Every(100L) : null);
				}
				else
				{
					this.m_TouchKeyboardPoller.Resume();
				}
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00033258 File Offset: 0x00031458
		private void DoPollTouchScreenKeyboard()
		{
			bool flag = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag)
			{
				bool flag2 = base.textInputField.editorEngine.keyboardOnScreen != null;
				if (flag2)
				{
					base.textInputField.UpdateText(base.textInputField.CullString(base.textInputField.editorEngine.keyboardOnScreen.text));
					bool flag3 = !base.textInputField.isDelayed;
					if (flag3)
					{
						base.textInputField.UpdateValueFromText();
					}
					bool flag4 = base.textInputField.editorEngine.keyboardOnScreen.status > TouchScreenKeyboard.Status.Visible;
					if (flag4)
					{
						base.textInputField.editorEngine.keyboardOnScreen = null;
						this.m_TouchKeyboardPoller.Pause();
						bool isDelayed = base.textInputField.isDelayed;
						if (isDelayed)
						{
							base.textInputField.UpdateValueFromText();
						}
					}
				}
			}
			else
			{
				base.textInputField.editorEngine.keyboardOnScreen.active = false;
				base.textInputField.editorEngine.keyboardOnScreen = null;
				this.m_TouchKeyboardPoller.Pause();
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0003337C File Offset: 0x0003157C
		public override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool flag = base.editorEngine.keyboardOnScreen != null;
			if (!flag)
			{
				bool flag2 = !base.textInputField.isReadOnly && evt.eventTypeId == EventBase<PointerDownEvent>.TypeId();
				if (flag2)
				{
					base.textInputField.CaptureMouse();
					this.m_LastPointerDownTarget = evt.target as VisualElement;
				}
				else
				{
					bool flag3 = !base.textInputField.isReadOnly && evt.eventTypeId == EventBase<PointerUpEvent>.TypeId();
					if (flag3)
					{
						base.textInputField.ReleaseMouse();
						bool flag4 = this.m_LastPointerDownTarget == null || !this.m_LastPointerDownTarget.worldBound.Contains(((PointerUpEvent)evt).position);
						if (!flag4)
						{
							this.m_LastPointerDownTarget = null;
							base.textInputField.SyncTextEngine();
							base.textInputField.UpdateText(base.editorEngine.text);
							base.editorEngine.keyboardOnScreen = TouchScreenKeyboard.Open(base.textInputField.text, TouchScreenKeyboardType.Default, true, base.editorEngine.multiline, base.textInputField.isPasswordField);
							bool flag5 = base.editorEngine.keyboardOnScreen != null;
							if (flag5)
							{
								this.PollTouchScreenKeyboard();
							}
							base.editorEngine.UpdateScrollOffset();
							evt.StopPropagation();
						}
					}
				}
			}
		}

		// Token: 0x040005C5 RID: 1477
		private IVisualElementScheduledItem m_TouchKeyboardPoller = null;

		// Token: 0x040005C6 RID: 1478
		private VisualElement m_LastPointerDownTarget;
	}
}
