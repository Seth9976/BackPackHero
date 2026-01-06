using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x02000181 RID: 385
	public sealed class UnitPreservation : IPoolable
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x000127B1 File Offset: 0x000109B1
		void IPoolable.New()
		{
			this.disposed = false;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000127BC File Offset: 0x000109BC
		void IPoolable.Free()
		{
			this.disposed = true;
			foreach (KeyValuePair<string, List<UnitPreservation.UnitPortPreservation>> keyValuePair in this.inputConnections)
			{
				ListPool<UnitPreservation.UnitPortPreservation>.Free(keyValuePair.Value);
			}
			foreach (KeyValuePair<string, List<UnitPreservation.UnitPortPreservation>> keyValuePair2 in this.outputConnections)
			{
				ListPool<UnitPreservation.UnitPortPreservation>.Free(keyValuePair2.Value);
			}
			this.defaultValues.Clear();
			this.inputConnections.Clear();
			this.outputConnections.Clear();
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00012884 File Offset: 0x00010A84
		private UnitPreservation()
		{
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000128B0 File Offset: 0x00010AB0
		public static UnitPreservation Preserve(IUnit unit)
		{
			UnitPreservation unitPreservation = GenericPool<UnitPreservation>.New(() => new UnitPreservation());
			foreach (KeyValuePair<string, object> keyValuePair in unit.defaultValues)
			{
				unitPreservation.defaultValues.Add(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (IUnitInputPort unitInputPort in unit.inputs)
			{
				if (unitInputPort.hasAnyConnection)
				{
					unitPreservation.inputConnections.Add(unitInputPort.key, ListPool<UnitPreservation.UnitPortPreservation>.New());
					foreach (IUnitPort unitPort in unitInputPort.connectedPorts)
					{
						unitPreservation.inputConnections[unitInputPort.key].Add(new UnitPreservation.UnitPortPreservation(unitPort));
					}
				}
			}
			foreach (IUnitOutputPort unitOutputPort in unit.outputs)
			{
				if (unitOutputPort.hasAnyConnection)
				{
					unitPreservation.outputConnections.Add(unitOutputPort.key, ListPool<UnitPreservation.UnitPortPreservation>.New());
					foreach (IUnitPort unitPort2 in unitOutputPort.connectedPorts)
					{
						unitPreservation.outputConnections[unitOutputPort.key].Add(new UnitPreservation.UnitPortPreservation(unitPort2));
					}
				}
			}
			return unitPreservation;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00012AA0 File Offset: 0x00010CA0
		public void RestoreTo(IUnit unit)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.defaultValues)
			{
				if (unit.defaultValues.ContainsKey(keyValuePair.Key) && unit.valueInputs.Contains(keyValuePair.Key) && unit.valueInputs[keyValuePair.Key].type.IsAssignableFrom(keyValuePair.Value))
				{
					unit.defaultValues[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			foreach (KeyValuePair<string, List<UnitPreservation.UnitPortPreservation>> keyValuePair2 in this.inputConnections)
			{
				UnitPreservation.UnitPortPreservation unitPortPreservation = new UnitPreservation.UnitPortPreservation(unit, keyValuePair2.Key);
				foreach (UnitPreservation.UnitPortPreservation unitPortPreservation2 in keyValuePair2.Value)
				{
					this.RestoreConnection(unitPortPreservation2, unitPortPreservation);
				}
			}
			foreach (KeyValuePair<string, List<UnitPreservation.UnitPortPreservation>> keyValuePair3 in this.outputConnections)
			{
				UnitPreservation.UnitPortPreservation unitPortPreservation3 = new UnitPreservation.UnitPortPreservation(unit, keyValuePair3.Key);
				foreach (UnitPreservation.UnitPortPreservation unitPortPreservation4 in keyValuePair3.Value)
				{
					this.RestoreConnection(unitPortPreservation3, unitPortPreservation4);
				}
			}
			GenericPool<UnitPreservation>.Free(this);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00012C94 File Offset: 0x00010E94
		private void RestoreConnection(UnitPreservation.UnitPortPreservation sourcePreservation, UnitPreservation.UnitPortPreservation destinationPreservation)
		{
			InvalidOutput invalidOutput;
			IUnitPort orCreateOutput = sourcePreservation.GetOrCreateOutput(out invalidOutput);
			InvalidInput invalidInput;
			IUnitPort orCreateInput = destinationPreservation.GetOrCreateInput(out invalidInput);
			if (orCreateOutput.CanValidlyConnectTo(orCreateInput))
			{
				orCreateOutput.ValidlyConnectTo(orCreateInput);
				return;
			}
			if (orCreateOutput.CanInvalidlyConnectTo(orCreateInput))
			{
				orCreateOutput.InvalidlyConnectTo(orCreateInput);
				return;
			}
			if (invalidOutput != null)
			{
				sourcePreservation.unit.invalidOutputs.Remove(invalidOutput);
			}
			if (invalidInput != null)
			{
				destinationPreservation.unit.invalidInputs.Remove(invalidInput);
			}
		}

		// Token: 0x04000224 RID: 548
		private readonly Dictionary<string, object> defaultValues = new Dictionary<string, object>();

		// Token: 0x04000225 RID: 549
		private readonly Dictionary<string, List<UnitPreservation.UnitPortPreservation>> inputConnections = new Dictionary<string, List<UnitPreservation.UnitPortPreservation>>();

		// Token: 0x04000226 RID: 550
		private readonly Dictionary<string, List<UnitPreservation.UnitPortPreservation>> outputConnections = new Dictionary<string, List<UnitPreservation.UnitPortPreservation>>();

		// Token: 0x04000227 RID: 551
		private bool disposed;

		// Token: 0x020001E7 RID: 487
		private struct UnitPortPreservation
		{
			// Token: 0x06000C83 RID: 3203 RVA: 0x0001C323 File Offset: 0x0001A523
			public UnitPortPreservation(IUnitPort port)
			{
				this.unit = port.unit;
				this.key = port.key;
			}

			// Token: 0x06000C84 RID: 3204 RVA: 0x0001C33D File Offset: 0x0001A53D
			public UnitPortPreservation(IUnit unit, string key)
			{
				this.unit = unit;
				this.key = key;
			}

			// Token: 0x06000C85 RID: 3205 RVA: 0x0001C350 File Offset: 0x0001A550
			public IUnitPort GetOrCreateInput(out InvalidInput newInvalidInput)
			{
				string key = this.key;
				if (!this.unit.inputs.Any((IUnitInputPort p) => p.key == key))
				{
					newInvalidInput = new InvalidInput(key);
					this.unit.invalidInputs.Add(newInvalidInput);
				}
				else
				{
					newInvalidInput = null;
				}
				return this.unit.inputs.Single((IUnitInputPort p) => p.key == key);
			}

			// Token: 0x06000C86 RID: 3206 RVA: 0x0001C3D0 File Offset: 0x0001A5D0
			public IUnitPort GetOrCreateOutput(out InvalidOutput newInvalidOutput)
			{
				string key = this.key;
				if (!this.unit.outputs.Any((IUnitOutputPort p) => p.key == key))
				{
					newInvalidOutput = new InvalidOutput(key);
					this.unit.invalidOutputs.Add(newInvalidOutput);
				}
				else
				{
					newInvalidOutput = null;
				}
				return this.unit.outputs.Single((IUnitOutputPort p) => p.key == key);
			}

			// Token: 0x04000429 RID: 1065
			public readonly IUnit unit;

			// Token: 0x0400042A RID: 1066
			public readonly string key;
		}
	}
}
