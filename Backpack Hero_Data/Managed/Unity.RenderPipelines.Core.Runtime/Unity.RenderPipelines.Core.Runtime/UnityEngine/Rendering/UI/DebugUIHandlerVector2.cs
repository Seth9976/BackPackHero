using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000103 RID: 259
	public class DebugUIHandlerVector2 : DebugUIHandlerWidget
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x000216D0 File Offset: 0x0001F8D0
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector2Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = () => this.m_Field.GetValue().x;
			this.fieldX.setter = delegate(float x)
			{
				this.SetValue(x, true, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = () => this.m_Field.GetValue().y;
			this.fieldY.setter = delegate(float x)
			{
				this.SetValue(x, false, true);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.SetupSettings(this.fieldY);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000217A8 File Offset: 0x0001F9A8
		private void SetValue(float v, bool x = false, bool y = false)
		{
			Vector2 value = this.m_Field.GetValue();
			if (x)
			{
				value.x = v;
			}
			if (y)
			{
				value.y = v;
			}
			this.m_Field.SetValue(value);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000217E4 File Offset: 0x0001F9E4
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00021834 File Offset: 0x0001FA34
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (fromNext || !this.valueToggle.isOn)
			{
				this.nameLabel.color = this.colorSelected;
			}
			else if (this.valueToggle.isOn)
			{
				if (this.m_Container.IsDirectChild(previous))
				{
					this.nameLabel.color = this.colorSelected;
				}
				else
				{
					DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
					DebugManager.instance.ChangeSelection(lastItem, false);
				}
			}
			return true;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000218AB File Offset: 0x0001FAAB
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000218BE File Offset: 0x0001FABE
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000218CC File Offset: 0x0001FACC
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000218DA File Offset: 0x0001FADA
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000218F8 File Offset: 0x0001FAF8
		public override DebugUIHandlerWidget Next()
		{
			if (!this.valueToggle.isOn || this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstItem = this.m_Container.GetFirstItem();
			if (firstItem == null)
			{
				return base.Next();
			}
			return firstItem;
		}

		// Token: 0x0400042F RID: 1071
		public Text nameLabel;

		// Token: 0x04000430 RID: 1072
		public UIFoldout valueToggle;

		// Token: 0x04000431 RID: 1073
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x04000432 RID: 1074
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x04000433 RID: 1075
		private DebugUI.Vector2Field m_Field;

		// Token: 0x04000434 RID: 1076
		private DebugUIHandlerContainer m_Container;
	}
}
