using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000131 RID: 305
	[UnitCategory("Time")]
	[UnitOrder(6)]
	[TypeIcon(typeof(WaitUnit))]
	public sealed class WaitForFlow : Unit, IGraphElementWithData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0000F274 File Offset: 0x0000D474
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0000F27C File Offset: 0x0000D47C
		[Serialize]
		[Inspectable]
		public bool resetOnExit { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0000F285 File Offset: 0x0000D485
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0000F28D File Offset: 0x0000D48D
		[DoNotSerialize]
		[Inspectable]
		[UnitHeaderInspectable("Inputs")]
		public int inputCount
		{
			get
			{
				return this._inputCount;
			}
			set
			{
				this._inputCount = Mathf.Clamp(value, 2, 10);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0000F29E File Offset: 0x0000D49E
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0000F2A6 File Offset: 0x0000D4A6
		[DoNotSerialize]
		public ReadOnlyCollection<ControlInput> awaitedInputs { get; private set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0000F2AF File Offset: 0x0000D4AF
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x0000F2B7 File Offset: 0x0000D4B7
		[DoNotSerialize]
		public ControlInput reset { get; private set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x06000829 RID: 2089 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		protected override void Definition()
		{
			List<ControlInput> list = new List<ControlInput>();
			this.awaitedInputs = list.AsReadOnly();
			this.exit = base.ControlOutput("exit");
			for (int i = 0; i < this.inputCount; i++)
			{
				int _i = i;
				ControlInput controlInput = base.ControlInputCoroutine(_i.ToString(), (Flow flow) => this.Enter(flow, _i), (Flow flow) => this.EnterCoroutine(flow, _i));
				list.Add(controlInput);
				base.Succession(controlInput, this.exit);
			}
			this.reset = base.ControlInput("reset", new Func<Flow, ControlOutput>(this.Reset));
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0000F384 File Offset: 0x0000D584
		public IGraphElementData CreateData()
		{
			return new WaitForFlow.Data
			{
				inputsActivated = new bool[this.inputCount]
			};
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0000F39C File Offset: 0x0000D59C
		private ControlOutput Enter(Flow flow, int index)
		{
			flow.stack.GetElementData<WaitForFlow.Data>(this).inputsActivated[index] = true;
			if (this.CheckActivated(flow))
			{
				if (this.resetOnExit)
				{
					this.Reset(flow);
				}
				return this.exit;
			}
			return null;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		private bool CheckActivated(Flow flow)
		{
			WaitForFlow.Data elementData = flow.stack.GetElementData<WaitForFlow.Data>(this);
			for (int i = 0; i < elementData.inputsActivated.Length; i++)
			{
				if (!elementData.inputsActivated[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0000F40E File Offset: 0x0000D60E
		private IEnumerator EnterCoroutine(Flow flow, int index)
		{
			WaitForFlow.Data data = flow.stack.GetElementData<WaitForFlow.Data>(this);
			data.inputsActivated[index] = true;
			if (data.isWaitingCoroutine)
			{
				yield break;
			}
			if (!this.CheckActivated(flow))
			{
				data.isWaitingCoroutine = true;
				yield return new WaitUntil(() => this.CheckActivated(flow));
				data.isWaitingCoroutine = false;
			}
			if (this.resetOnExit)
			{
				this.Reset(flow);
			}
			yield return this.exit;
			yield break;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0000F42C File Offset: 0x0000D62C
		private ControlOutput Reset(Flow flow)
		{
			WaitForFlow.Data elementData = flow.stack.GetElementData<WaitForFlow.Data>(this);
			for (int i = 0; i < elementData.inputsActivated.Length; i++)
			{
				elementData.inputsActivated[i] = false;
			}
			return null;
		}

		// Token: 0x040001DA RID: 474
		[SerializeAs("inputCount")]
		private int _inputCount = 2;

		// Token: 0x020001C5 RID: 453
		public sealed class Data : IGraphElementData
		{
			// Token: 0x040003CD RID: 973
			public bool[] inputsActivated;

			// Token: 0x040003CE RID: 974
			public bool isWaitingCoroutine;
		}
	}
}
