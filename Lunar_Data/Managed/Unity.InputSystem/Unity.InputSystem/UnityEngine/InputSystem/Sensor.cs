using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000045 RID: 69
	[InputControlLayout(isGenericTypeOfDevice = true)]
	public class Sensor : InputDevice
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0002C49C File Offset: 0x0002A69C
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x0002C4D4 File Offset: 0x0002A6D4
		public float samplingFrequency
		{
			get
			{
				QuerySamplingFrequencyCommand querySamplingFrequencyCommand = QuerySamplingFrequencyCommand.Create();
				if (base.ExecuteCommand<QuerySamplingFrequencyCommand>(ref querySamplingFrequencyCommand) >= 0L)
				{
					return querySamplingFrequencyCommand.frequency;
				}
				throw new NotSupportedException(string.Format("Device '{0}' does not support querying sampling frequency", this));
			}
			set
			{
				SetSamplingFrequencyCommand setSamplingFrequencyCommand = SetSamplingFrequencyCommand.Create(value);
				base.ExecuteCommand<SetSamplingFrequencyCommand>(ref setSamplingFrequencyCommand);
			}
		}
	}
}
