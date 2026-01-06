using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000045 RID: 69
	[InputControlLayout(isGenericTypeOfDevice = true)]
	public class Sensor : InputDevice
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0002C4D8 File Offset: 0x0002A6D8
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0002C510 File Offset: 0x0002A710
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
