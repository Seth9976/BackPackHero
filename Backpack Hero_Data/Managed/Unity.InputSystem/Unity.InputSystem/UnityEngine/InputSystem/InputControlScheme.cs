using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Collections;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public struct InputControlScheme : IEquatable<InputControlScheme>
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000D725 File Offset: 0x0000B925
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000D72D File Offset: 0x0000B92D
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000D735 File Offset: 0x0000B935
		public string bindingGroup
		{
			get
			{
				return this.m_BindingGroup;
			}
			set
			{
				this.m_BindingGroup = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000D73E File Offset: 0x0000B93E
		public ReadOnlyArray<InputControlScheme.DeviceRequirement> deviceRequirements
		{
			get
			{
				return new ReadOnlyArray<InputControlScheme.DeviceRequirement>(this.m_DeviceRequirements);
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000D74C File Offset: 0x0000B94C
		public InputControlScheme(string name, IEnumerable<InputControlScheme.DeviceRequirement> devices = null, string bindingGroup = null)
		{
			this = default(InputControlScheme);
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			this.SetNameAndBindingGroup(name, bindingGroup);
			this.m_DeviceRequirements = null;
			if (devices != null)
			{
				this.m_DeviceRequirements = devices.ToArray<InputControlScheme.DeviceRequirement>();
				if (this.m_DeviceRequirements.Length == 0)
				{
					this.m_DeviceRequirements = null;
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000D7A1 File Offset: 0x0000B9A1
		internal void SetNameAndBindingGroup(string name, string bindingGroup = null)
		{
			this.m_Name = name;
			if (!string.IsNullOrEmpty(bindingGroup))
			{
				this.m_BindingGroup = bindingGroup;
				return;
			}
			this.m_BindingGroup = (name.Contains(';') ? name.Replace(";", "") : name);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		public static InputControlScheme? FindControlSchemeForDevices<TDevices, TSchemes>(TDevices devices, TSchemes schemes, InputDevice mustIncludeDevice = null, bool allowUnsuccesfulMatch = false) where TDevices : IReadOnlyList<InputDevice> where TSchemes : IEnumerable<InputControlScheme>
		{
			if (devices == null)
			{
				throw new ArgumentNullException("devices");
			}
			if (schemes == null)
			{
				throw new ArgumentNullException("schemes");
			}
			InputControlScheme inputControlScheme;
			InputControlScheme.MatchResult matchResult;
			if (!InputControlScheme.FindControlSchemeForDevices<TDevices, TSchemes>(devices, schemes, out inputControlScheme, out matchResult, mustIncludeDevice, allowUnsuccesfulMatch))
			{
				return null;
			}
			matchResult.Dispose();
			return new InputControlScheme?(inputControlScheme);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000D83C File Offset: 0x0000BA3C
		public static bool FindControlSchemeForDevices<TDevices, TSchemes>(TDevices devices, TSchemes schemes, out InputControlScheme controlScheme, out InputControlScheme.MatchResult matchResult, InputDevice mustIncludeDevice = null, bool allowUnsuccessfulMatch = false) where TDevices : IReadOnlyList<InputDevice> where TSchemes : IEnumerable<InputControlScheme>
		{
			if (devices == null)
			{
				throw new ArgumentNullException("devices");
			}
			if (schemes == null)
			{
				throw new ArgumentNullException("schemes");
			}
			InputControlScheme.MatchResult? matchResult2 = null;
			InputControlScheme? inputControlScheme = null;
			foreach (InputControlScheme inputControlScheme2 in schemes)
			{
				InputControlScheme.MatchResult matchResult3 = inputControlScheme2.PickDevicesFrom<TDevices>(devices, mustIncludeDevice);
				if (!matchResult3.isSuccessfulMatch && (!allowUnsuccessfulMatch || matchResult3.score <= 0f))
				{
					matchResult3.Dispose();
				}
				else if (mustIncludeDevice != null && !matchResult3.devices.Contains(mustIncludeDevice))
				{
					matchResult3.Dispose();
				}
				else if (matchResult2 != null && matchResult2.Value.score >= matchResult3.score)
				{
					matchResult3.Dispose();
				}
				else
				{
					if (matchResult2 != null)
					{
						matchResult2.GetValueOrDefault().Dispose();
					}
					matchResult2 = new InputControlScheme.MatchResult?(matchResult3);
					inputControlScheme = new InputControlScheme?(inputControlScheme2);
				}
			}
			matchResult = matchResult2.GetValueOrDefault();
			controlScheme = inputControlScheme.GetValueOrDefault();
			return matchResult2 != null;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000D988 File Offset: 0x0000BB88
		public static InputControlScheme? FindControlSchemeForDevice<TSchemes>(InputDevice device, TSchemes schemes) where TSchemes : IEnumerable<InputControlScheme>
		{
			if (schemes == null)
			{
				throw new ArgumentNullException("schemes");
			}
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			return InputControlScheme.FindControlSchemeForDevices<OneOrMore<InputDevice, ReadOnlyArray<InputDevice>>, TSchemes>(new OneOrMore<InputDevice, ReadOnlyArray<InputDevice>>(device), schemes, null, false);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000D9BC File Offset: 0x0000BBBC
		public bool SupportsDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			for (int i = 0; i < this.m_DeviceRequirements.Length; i++)
			{
				if (InputControlPath.TryFindControl(device, this.m_DeviceRequirements[i].controlPath, 0) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000DA08 File Offset: 0x0000BC08
		public InputControlScheme.MatchResult PickDevicesFrom<TDevices>(TDevices devices, InputDevice favorDevice = null) where TDevices : IReadOnlyList<InputDevice>
		{
			InputControlScheme.MatchResult matchResult;
			if (this.m_DeviceRequirements == null || this.m_DeviceRequirements.Length == 0)
			{
				matchResult = new InputControlScheme.MatchResult
				{
					m_Result = InputControlScheme.MatchResult.Result.AllSatisfied,
					m_Score = 0.5f
				};
				return matchResult;
			}
			bool flag = true;
			bool flag2 = true;
			int num = this.m_DeviceRequirements.Length;
			float num2 = 0f;
			InputControlList<InputControl> inputControlList = new InputControlList<InputControl>(Allocator.Persistent, num);
			try
			{
				bool flag3 = false;
				bool flag4 = false;
				for (int i = 0; i < num; i++)
				{
					bool isOR = this.m_DeviceRequirements[i].isOR;
					bool isOptional = this.m_DeviceRequirements[i].isOptional;
					if (isOR && flag3)
					{
						inputControlList.Add(null);
					}
					else
					{
						string controlPath = this.m_DeviceRequirements[i].controlPath;
						if (string.IsNullOrEmpty(controlPath))
						{
							num2 += 1f;
							inputControlList.Add(null);
						}
						else
						{
							InputControl inputControl = null;
							int j = 0;
							while (j < devices.Count)
							{
								InputDevice inputDevice = devices[j];
								if (favorDevice != null)
								{
									if (j == 0)
									{
										inputDevice = favorDevice;
									}
									else if (inputDevice == favorDevice)
									{
										inputDevice = devices[0];
									}
								}
								InputControl inputControl2 = InputControlPath.TryFindControl(inputDevice, controlPath, 0);
								if (inputControl2 != null && !inputControlList.Contains(inputControl2))
								{
									inputControl = inputControl2;
									InternedString internedString = new InternedString(InputControlPath.TryGetDeviceLayout(controlPath));
									if (internedString.IsEmpty())
									{
										num2 += 1f;
										break;
									}
									InternedString layout = inputControl2.device.m_Layout;
									int num3;
									if (InputControlLayout.s_Layouts.ComputeDistanceInInheritanceHierarchy(internedString, layout, out num3))
									{
										num2 += 1f + 1f / (float)(Math.Abs(num3) + 1);
										break;
									}
									num2 += 1f;
									break;
								}
								else
								{
									j++;
								}
							}
							if (i + 1 < num && this.m_DeviceRequirements[i + 1].isOR)
							{
								if (inputControl != null)
								{
									flag3 = true;
								}
								else if (!isOptional)
								{
									flag4 = true;
								}
							}
							else if (isOR && i == num - 1)
							{
								if (inputControl == null)
								{
									if (flag4)
									{
										flag = false;
									}
									else
									{
										flag2 = false;
									}
								}
							}
							else
							{
								if (inputControl == null)
								{
									if (isOptional)
									{
										flag2 = false;
									}
									else
									{
										flag = false;
									}
								}
								if (i > 0 && this.m_DeviceRequirements[i - 1].isOR)
								{
									if (!flag3)
									{
										if (flag4)
										{
											flag = false;
										}
										else
										{
											flag2 = false;
										}
									}
									flag3 = false;
								}
							}
							inputControlList.Add(inputControl);
						}
					}
				}
			}
			catch (Exception)
			{
				inputControlList.Dispose();
				throw;
			}
			matchResult = new InputControlScheme.MatchResult
			{
				m_Result = ((!flag) ? InputControlScheme.MatchResult.Result.MissingRequired : ((!flag2) ? InputControlScheme.MatchResult.Result.MissingOptional : InputControlScheme.MatchResult.Result.AllSatisfied)),
				m_Controls = inputControlList,
				m_Requirements = this.m_DeviceRequirements,
				m_Score = num2
			};
			return matchResult;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000DCBC File Offset: 0x0000BEBC
		public bool Equals(InputControlScheme other)
		{
			if (!string.Equals(this.m_Name, other.m_Name, StringComparison.InvariantCultureIgnoreCase) || !string.Equals(this.m_BindingGroup, other.m_BindingGroup, StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}
			if (this.m_DeviceRequirements == null || this.m_DeviceRequirements.Length == 0)
			{
				return other.m_DeviceRequirements == null || other.m_DeviceRequirements.Length == 0;
			}
			if (other.m_DeviceRequirements == null || this.m_DeviceRequirements.Length != other.m_DeviceRequirements.Length)
			{
				return false;
			}
			int num = this.m_DeviceRequirements.Length;
			for (int i = 0; i < num; i++)
			{
				InputControlScheme.DeviceRequirement deviceRequirement = this.m_DeviceRequirements[i];
				bool flag = false;
				for (int j = 0; j < num; j++)
				{
					if (other.m_DeviceRequirements[j] == deviceRequirement)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public override bool Equals(object obj)
		{
			return obj != null && obj is InputControlScheme && this.Equals((InputControlScheme)obj);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		public override int GetHashCode()
		{
			return (((((this.m_Name != null) ? this.m_Name.GetHashCode() : 0) * 397) ^ ((this.m_BindingGroup != null) ? this.m_BindingGroup.GetHashCode() : 0)) * 397) ^ ((this.m_DeviceRequirements != null) ? this.m_DeviceRequirements.GetHashCode() : 0);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000DE08 File Offset: 0x0000C008
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.m_Name))
			{
				return base.ToString();
			}
			if (this.m_DeviceRequirements == null)
			{
				return this.m_Name;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.m_Name);
			stringBuilder.Append('(');
			bool flag = true;
			foreach (InputControlScheme.DeviceRequirement deviceRequirement in this.m_DeviceRequirements)
			{
				if (!flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(deviceRequirement.controlPath);
				flag = false;
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000DEA9 File Offset: 0x0000C0A9
		public static bool operator ==(InputControlScheme left, InputControlScheme right)
		{
			return left.Equals(right);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000DEB3 File Offset: 0x0000C0B3
		public static bool operator !=(InputControlScheme left, InputControlScheme right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040000F3 RID: 243
		[SerializeField]
		internal string m_Name;

		// Token: 0x040000F4 RID: 244
		[SerializeField]
		internal string m_BindingGroup;

		// Token: 0x040000F5 RID: 245
		[SerializeField]
		internal InputControlScheme.DeviceRequirement[] m_DeviceRequirements;

		// Token: 0x0200017B RID: 379
		public struct MatchResult : IEnumerable<InputControlScheme.MatchResult.Match>, IEnumerable, IDisposable
		{
			// Token: 0x17000533 RID: 1331
			// (get) Token: 0x0600132B RID: 4907 RVA: 0x00058F2B File Offset: 0x0005712B
			public float score
			{
				get
				{
					return this.m_Score;
				}
			}

			// Token: 0x17000534 RID: 1332
			// (get) Token: 0x0600132C RID: 4908 RVA: 0x00058F33 File Offset: 0x00057133
			public bool isSuccessfulMatch
			{
				get
				{
					return this.m_Result != InputControlScheme.MatchResult.Result.MissingRequired;
				}
			}

			// Token: 0x17000535 RID: 1333
			// (get) Token: 0x0600132D RID: 4909 RVA: 0x00058F41 File Offset: 0x00057141
			public bool hasMissingRequiredDevices
			{
				get
				{
					return this.m_Result == InputControlScheme.MatchResult.Result.MissingRequired;
				}
			}

			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x0600132E RID: 4910 RVA: 0x00058F4C File Offset: 0x0005714C
			public bool hasMissingOptionalDevices
			{
				get
				{
					return this.m_Result == InputControlScheme.MatchResult.Result.MissingOptional;
				}
			}

			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x0600132F RID: 4911 RVA: 0x00058F58 File Offset: 0x00057158
			public InputControlList<InputDevice> devices
			{
				get
				{
					if (this.m_Devices.Count == 0 && !this.hasMissingRequiredDevices)
					{
						int count = this.m_Controls.Count;
						if (count != 0)
						{
							this.m_Devices.Capacity = count;
							for (int i = 0; i < count; i++)
							{
								InputControl inputControl = this.m_Controls[i];
								if (inputControl != null)
								{
									InputDevice device = inputControl.device;
									if (!this.m_Devices.Contains(device))
									{
										this.m_Devices.Add(device);
									}
								}
							}
						}
					}
					return this.m_Devices;
				}
			}

			// Token: 0x17000538 RID: 1336
			public InputControlScheme.MatchResult.Match this[int index]
			{
				get
				{
					if (index < 0 || this.m_Requirements == null || index >= this.m_Requirements.Length)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return new InputControlScheme.MatchResult.Match
					{
						m_RequirementIndex = index,
						m_Requirements = this.m_Requirements,
						m_Controls = this.m_Controls
					};
				}
			}

			// Token: 0x06001331 RID: 4913 RVA: 0x00059034 File Offset: 0x00057234
			public IEnumerator<InputControlScheme.MatchResult.Match> GetEnumerator()
			{
				return new InputControlScheme.MatchResult.Enumerator
				{
					m_Index = -1,
					m_Requirements = this.m_Requirements,
					m_Controls = this.m_Controls
				};
			}

			// Token: 0x06001332 RID: 4914 RVA: 0x00059071 File Offset: 0x00057271
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06001333 RID: 4915 RVA: 0x00059079 File Offset: 0x00057279
			public void Dispose()
			{
				this.m_Controls.Dispose();
				this.m_Devices.Dispose();
			}

			// Token: 0x04000821 RID: 2081
			internal InputControlScheme.MatchResult.Result m_Result;

			// Token: 0x04000822 RID: 2082
			internal float m_Score;

			// Token: 0x04000823 RID: 2083
			internal InputControlList<InputDevice> m_Devices;

			// Token: 0x04000824 RID: 2084
			internal InputControlList<InputControl> m_Controls;

			// Token: 0x04000825 RID: 2085
			internal InputControlScheme.DeviceRequirement[] m_Requirements;

			// Token: 0x0200025D RID: 605
			internal enum Result
			{
				// Token: 0x04000C65 RID: 3173
				AllSatisfied,
				// Token: 0x04000C66 RID: 3174
				MissingRequired,
				// Token: 0x04000C67 RID: 3175
				MissingOptional
			}

			// Token: 0x0200025E RID: 606
			public struct Match
			{
				// Token: 0x170005E6 RID: 1510
				// (get) Token: 0x060015F6 RID: 5622 RVA: 0x000641FB File Offset: 0x000623FB
				public InputControl control
				{
					get
					{
						return this.m_Controls[this.m_RequirementIndex];
					}
				}

				// Token: 0x170005E7 RID: 1511
				// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0006420E File Offset: 0x0006240E
				public InputDevice device
				{
					get
					{
						InputControl control = this.control;
						if (control == null)
						{
							return null;
						}
						return control.device;
					}
				}

				// Token: 0x170005E8 RID: 1512
				// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00064221 File Offset: 0x00062421
				public int requirementIndex
				{
					get
					{
						return this.m_RequirementIndex;
					}
				}

				// Token: 0x170005E9 RID: 1513
				// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00064229 File Offset: 0x00062429
				public InputControlScheme.DeviceRequirement requirement
				{
					get
					{
						return this.m_Requirements[this.m_RequirementIndex];
					}
				}

				// Token: 0x170005EA RID: 1514
				// (get) Token: 0x060015FA RID: 5626 RVA: 0x0006423C File Offset: 0x0006243C
				public bool isOptional
				{
					get
					{
						return this.requirement.isOptional;
					}
				}

				// Token: 0x04000C68 RID: 3176
				internal int m_RequirementIndex;

				// Token: 0x04000C69 RID: 3177
				internal InputControlScheme.DeviceRequirement[] m_Requirements;

				// Token: 0x04000C6A RID: 3178
				internal InputControlList<InputControl> m_Controls;
			}

			// Token: 0x0200025F RID: 607
			private struct Enumerator : IEnumerator<InputControlScheme.MatchResult.Match>, IEnumerator, IDisposable
			{
				// Token: 0x060015FB RID: 5627 RVA: 0x00064257 File Offset: 0x00062457
				public bool MoveNext()
				{
					this.m_Index++;
					return this.m_Requirements != null && this.m_Index < this.m_Requirements.Length;
				}

				// Token: 0x060015FC RID: 5628 RVA: 0x00064281 File Offset: 0x00062481
				public void Reset()
				{
					this.m_Index = -1;
				}

				// Token: 0x170005EB RID: 1515
				// (get) Token: 0x060015FD RID: 5629 RVA: 0x0006428C File Offset: 0x0006248C
				public InputControlScheme.MatchResult.Match Current
				{
					get
					{
						if (this.m_Requirements == null || this.m_Index < 0 || this.m_Index >= this.m_Requirements.Length)
						{
							throw new InvalidOperationException("Enumerator is not valid");
						}
						return new InputControlScheme.MatchResult.Match
						{
							m_RequirementIndex = this.m_Index,
							m_Requirements = this.m_Requirements,
							m_Controls = this.m_Controls
						};
					}
				}

				// Token: 0x170005EC RID: 1516
				// (get) Token: 0x060015FE RID: 5630 RVA: 0x000642F5 File Offset: 0x000624F5
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060015FF RID: 5631 RVA: 0x00064302 File Offset: 0x00062502
				public void Dispose()
				{
				}

				// Token: 0x04000C6B RID: 3179
				internal int m_Index;

				// Token: 0x04000C6C RID: 3180
				internal InputControlScheme.DeviceRequirement[] m_Requirements;

				// Token: 0x04000C6D RID: 3181
				internal InputControlList<InputControl> m_Controls;
			}
		}

		// Token: 0x0200017C RID: 380
		[Serializable]
		public struct DeviceRequirement : IEquatable<InputControlScheme.DeviceRequirement>
		{
			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x06001334 RID: 4916 RVA: 0x00059091 File Offset: 0x00057291
			// (set) Token: 0x06001335 RID: 4917 RVA: 0x00059099 File Offset: 0x00057299
			public string controlPath
			{
				get
				{
					return this.m_ControlPath;
				}
				set
				{
					this.m_ControlPath = value;
				}
			}

			// Token: 0x1700053A RID: 1338
			// (get) Token: 0x06001336 RID: 4918 RVA: 0x000590A2 File Offset: 0x000572A2
			// (set) Token: 0x06001337 RID: 4919 RVA: 0x000590AF File Offset: 0x000572AF
			public bool isOptional
			{
				get
				{
					return (this.m_Flags & InputControlScheme.DeviceRequirement.Flags.Optional) > InputControlScheme.DeviceRequirement.Flags.None;
				}
				set
				{
					if (value)
					{
						this.m_Flags |= InputControlScheme.DeviceRequirement.Flags.Optional;
						return;
					}
					this.m_Flags &= ~InputControlScheme.DeviceRequirement.Flags.Optional;
				}
			}

			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x06001338 RID: 4920 RVA: 0x000590D2 File Offset: 0x000572D2
			// (set) Token: 0x06001339 RID: 4921 RVA: 0x000590DD File Offset: 0x000572DD
			public bool isAND
			{
				get
				{
					return !this.isOR;
				}
				set
				{
					this.isOR = !value;
				}
			}

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x0600133A RID: 4922 RVA: 0x000590E9 File Offset: 0x000572E9
			// (set) Token: 0x0600133B RID: 4923 RVA: 0x000590F6 File Offset: 0x000572F6
			public bool isOR
			{
				get
				{
					return (this.m_Flags & InputControlScheme.DeviceRequirement.Flags.Or) > InputControlScheme.DeviceRequirement.Flags.None;
				}
				set
				{
					if (value)
					{
						this.m_Flags |= InputControlScheme.DeviceRequirement.Flags.Or;
						return;
					}
					this.m_Flags &= ~InputControlScheme.DeviceRequirement.Flags.Or;
				}
			}

			// Token: 0x0600133C RID: 4924 RVA: 0x0005911C File Offset: 0x0005731C
			public override string ToString()
			{
				if (string.IsNullOrEmpty(this.controlPath))
				{
					return base.ToString();
				}
				if (this.isOptional)
				{
					return this.controlPath + " (Optional)";
				}
				return this.controlPath + " (Required)";
			}

			// Token: 0x0600133D RID: 4925 RVA: 0x00059170 File Offset: 0x00057370
			public bool Equals(InputControlScheme.DeviceRequirement other)
			{
				return string.Equals(this.m_ControlPath, other.m_ControlPath) && this.m_Flags == other.m_Flags && string.Equals(this.controlPath, other.controlPath) && this.isOptional == other.isOptional;
			}

			// Token: 0x0600133E RID: 4926 RVA: 0x000591C3 File Offset: 0x000573C3
			public override bool Equals(object obj)
			{
				return obj != null && obj is InputControlScheme.DeviceRequirement && this.Equals((InputControlScheme.DeviceRequirement)obj);
			}

			// Token: 0x0600133F RID: 4927 RVA: 0x000591E0 File Offset: 0x000573E0
			public override int GetHashCode()
			{
				return (((((((this.m_ControlPath != null) ? this.m_ControlPath.GetHashCode() : 0) * 397) ^ this.m_Flags.GetHashCode()) * 397) ^ ((this.controlPath != null) ? this.controlPath.GetHashCode() : 0)) * 397) ^ this.isOptional.GetHashCode();
			}

			// Token: 0x06001340 RID: 4928 RVA: 0x0005924D File Offset: 0x0005744D
			public static bool operator ==(InputControlScheme.DeviceRequirement left, InputControlScheme.DeviceRequirement right)
			{
				return left.Equals(right);
			}

			// Token: 0x06001341 RID: 4929 RVA: 0x00059257 File Offset: 0x00057457
			public static bool operator !=(InputControlScheme.DeviceRequirement left, InputControlScheme.DeviceRequirement right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04000826 RID: 2086
			[SerializeField]
			internal string m_ControlPath;

			// Token: 0x04000827 RID: 2087
			[SerializeField]
			internal InputControlScheme.DeviceRequirement.Flags m_Flags;

			// Token: 0x02000260 RID: 608
			[Flags]
			internal enum Flags
			{
				// Token: 0x04000C6F RID: 3183
				None = 0,
				// Token: 0x04000C70 RID: 3184
				Optional = 1,
				// Token: 0x04000C71 RID: 3185
				Or = 2
			}
		}

		// Token: 0x0200017D RID: 381
		[Serializable]
		internal struct SchemeJson
		{
			// Token: 0x06001342 RID: 4930 RVA: 0x00059264 File Offset: 0x00057464
			public InputControlScheme ToScheme()
			{
				InputControlScheme.DeviceRequirement[] array = null;
				if (this.devices != null && this.devices.Length != 0)
				{
					int num = this.devices.Length;
					array = new InputControlScheme.DeviceRequirement[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = this.devices[i].ToDeviceEntry();
					}
				}
				return new InputControlScheme
				{
					m_Name = (string.IsNullOrEmpty(this.name) ? null : this.name),
					m_BindingGroup = (string.IsNullOrEmpty(this.bindingGroup) ? null : this.bindingGroup),
					m_DeviceRequirements = array
				};
			}

			// Token: 0x06001343 RID: 4931 RVA: 0x00059304 File Offset: 0x00057504
			public static InputControlScheme.SchemeJson ToJson(InputControlScheme scheme)
			{
				InputControlScheme.SchemeJson.DeviceJson[] array = null;
				if (scheme.m_DeviceRequirements != null && scheme.m_DeviceRequirements.Length != 0)
				{
					int num = scheme.m_DeviceRequirements.Length;
					array = new InputControlScheme.SchemeJson.DeviceJson[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = InputControlScheme.SchemeJson.DeviceJson.From(scheme.m_DeviceRequirements[i]);
					}
				}
				return new InputControlScheme.SchemeJson
				{
					name = scheme.m_Name,
					bindingGroup = scheme.m_BindingGroup,
					devices = array
				};
			}

			// Token: 0x06001344 RID: 4932 RVA: 0x00059384 File Offset: 0x00057584
			public static InputControlScheme.SchemeJson[] ToJson(InputControlScheme[] schemes)
			{
				if (schemes == null || schemes.Length == 0)
				{
					return null;
				}
				int num = schemes.Length;
				InputControlScheme.SchemeJson[] array = new InputControlScheme.SchemeJson[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = InputControlScheme.SchemeJson.ToJson(schemes[i]);
				}
				return array;
			}

			// Token: 0x06001345 RID: 4933 RVA: 0x000593C8 File Offset: 0x000575C8
			public static InputControlScheme[] ToSchemes(InputControlScheme.SchemeJson[] schemes)
			{
				if (schemes == null || schemes.Length == 0)
				{
					return null;
				}
				int num = schemes.Length;
				InputControlScheme[] array = new InputControlScheme[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = schemes[i].ToScheme();
				}
				return array;
			}

			// Token: 0x04000828 RID: 2088
			public string name;

			// Token: 0x04000829 RID: 2089
			public string bindingGroup;

			// Token: 0x0400082A RID: 2090
			public InputControlScheme.SchemeJson.DeviceJson[] devices;

			// Token: 0x02000261 RID: 609
			[Serializable]
			public struct DeviceJson
			{
				// Token: 0x06001600 RID: 5632 RVA: 0x00064304 File Offset: 0x00062504
				public InputControlScheme.DeviceRequirement ToDeviceEntry()
				{
					return new InputControlScheme.DeviceRequirement
					{
						controlPath = this.devicePath,
						isOptional = this.isOptional,
						isOR = this.isOR
					};
				}

				// Token: 0x06001601 RID: 5633 RVA: 0x00064344 File Offset: 0x00062544
				public static InputControlScheme.SchemeJson.DeviceJson From(InputControlScheme.DeviceRequirement requirement)
				{
					return new InputControlScheme.SchemeJson.DeviceJson
					{
						devicePath = requirement.controlPath,
						isOptional = requirement.isOptional,
						isOR = requirement.isOR
					};
				}

				// Token: 0x04000C72 RID: 3186
				public string devicePath;

				// Token: 0x04000C73 RID: 3187
				public bool isOptional;

				// Token: 0x04000C74 RID: 3188
				public bool isOR;
			}
		}
	}
}
