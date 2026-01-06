using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x0200007D RID: 125
	public struct InputUser : IEquatable<InputUser>
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00036B24 File Offset: 0x00034D24
		public bool valid
		{
			get
			{
				if (this.m_Id == 0U)
				{
					return false;
				}
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (InputUser.s_GlobalState.allUsers[i].m_Id == this.m_Id)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00036B70 File Offset: 0x00034D70
		public int index
		{
			get
			{
				if (this.m_Id == 0U)
				{
					throw new InvalidOperationException("Invalid user");
				}
				int num = InputUser.TryFindUserIndex(this.m_Id);
				if (num == -1)
				{
					throw new InvalidOperationException(string.Format("User with ID {0} is no longer valid", this.m_Id));
				}
				return num;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00036BAF File Offset: 0x00034DAF
		public uint id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00036BB7 File Offset: 0x00034DB7
		public InputUserAccountHandle? platformUserAccountHandle
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].platformUserAccountHandle;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00036BD3 File Offset: 0x00034DD3
		public string platformUserAccountName
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].platformUserAccountName;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00036BEF File Offset: 0x00034DEF
		public string platformUserAccountId
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].platformUserAccountId;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00036C0C File Offset: 0x00034E0C
		public ReadOnlyArray<InputDevice> pairedDevices
		{
			get
			{
				int index = this.index;
				return new ReadOnlyArray<InputDevice>(InputUser.s_GlobalState.allPairedDevices, InputUser.s_GlobalState.allUserData[index].deviceStartIndex, InputUser.s_GlobalState.allUserData[index].deviceCount);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00036C5C File Offset: 0x00034E5C
		public ReadOnlyArray<InputDevice> lostDevices
		{
			get
			{
				int index = this.index;
				return new ReadOnlyArray<InputDevice>(InputUser.s_GlobalState.allLostDevices, InputUser.s_GlobalState.allUserData[index].lostDeviceStartIndex, InputUser.s_GlobalState.allUserData[index].lostDeviceCount);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00036CA9 File Offset: 0x00034EA9
		public IInputActionCollection actions
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].actions;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00036CC5 File Offset: 0x00034EC5
		public InputControlScheme? controlScheme
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].controlScheme;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00036CE1 File Offset: 0x00034EE1
		public InputControlScheme.MatchResult controlSchemeMatch
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].controlSchemeMatch;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00036CFD File Offset: 0x00034EFD
		public bool hasMissingRequiredDevices
		{
			get
			{
				return InputUser.s_GlobalState.allUserData[this.index].controlSchemeMatch.hasMissingRequiredDevices;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00036D1E File Offset: 0x00034F1E
		public static ReadOnlyArray<InputUser> all
		{
			get
			{
				return new ReadOnlyArray<InputUser>(InputUser.s_GlobalState.allUsers, 0, InputUser.s_GlobalState.allUserCount);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000A3C RID: 2620 RVA: 0x00036D3A File Offset: 0x00034F3A
		// (remove) Token: 0x06000A3D RID: 2621 RVA: 0x00036D5A File Offset: 0x00034F5A
		public static event Action<InputUser, InputUserChange, InputDevice> onChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onChange.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onChange.RemoveCallback(value);
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000A3E RID: 2622 RVA: 0x00036D7A File Offset: 0x00034F7A
		// (remove) Token: 0x06000A3F RID: 2623 RVA: 0x00036DAC File Offset: 0x00034FAC
		public static event Action<InputControl, InputEventPtr> onUnpairedDeviceUsed
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onUnpairedDeviceUsed.AddCallback(value);
				if (InputUser.s_GlobalState.listenForUnpairedDeviceActivity > 0)
				{
					InputUser.HookIntoEvents();
				}
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onUnpairedDeviceUsed.RemoveCallback(value);
				if (InputUser.s_GlobalState.onUnpairedDeviceUsed.length == 0)
				{
					InputUser.UnhookFromDeviceStateChange();
				}
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000A40 RID: 2624 RVA: 0x00036DE2 File Offset: 0x00034FE2
		// (remove) Token: 0x06000A41 RID: 2625 RVA: 0x00036E02 File Offset: 0x00035002
		public static event Func<InputDevice, InputEventPtr, bool> onPrefilterUnpairedDeviceActivity
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onPreFilterUnpairedDeviceUsed.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputUser.s_GlobalState.onPreFilterUnpairedDeviceUsed.RemoveCallback(value);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00036E22 File Offset: 0x00035022
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x00036E30 File Offset: 0x00035030
		public static int listenForUnpairedDeviceActivity
		{
			get
			{
				return InputUser.s_GlobalState.listenForUnpairedDeviceActivity;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Cannot be negative");
				}
				if (value > 0 && InputUser.s_GlobalState.onUnpairedDeviceUsed.length > 0)
				{
					InputUser.HookIntoEvents();
				}
				else if (value == 0)
				{
					InputUser.UnhookFromDeviceStateChange();
				}
				InputUser.s_GlobalState.listenForUnpairedDeviceActivity = value;
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00036E84 File Offset: 0x00035084
		public override string ToString()
		{
			if (!this.valid)
			{
				return string.Format("<Invalid> (id: {0})", this.m_Id);
			}
			string text = string.Join<InputDevice>(",", this.pairedDevices);
			return string.Format("User #{0} (id: {1}, devices: {2}, actions: {3})", new object[] { this.index, this.m_Id, text, this.actions });
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00036F00 File Offset: 0x00035100
		public void AssociateActionsWithUser(IInputActionCollection actions)
		{
			int index = this.index;
			if (InputUser.s_GlobalState.allUserData[index].actions == actions)
			{
				return;
			}
			IInputActionCollection actions2 = InputUser.s_GlobalState.allUserData[index].actions;
			if (actions2 != null)
			{
				actions2.devices = null;
				actions2.bindingMask = null;
			}
			InputUser.s_GlobalState.allUserData[index].actions = actions;
			if (actions != null)
			{
				InputUser.HookIntoActionChange();
				actions.devices = new ReadOnlyArray<InputDevice>?(this.pairedDevices);
				if (InputUser.s_GlobalState.allUserData[index].controlScheme != null)
				{
					this.ActivateControlSchemeInternal(index, InputUser.s_GlobalState.allUserData[index].controlScheme.Value);
				}
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00036FD0 File Offset: 0x000351D0
		public InputUser.ControlSchemeChangeSyntax ActivateControlScheme(string schemeName)
		{
			if (!string.IsNullOrEmpty(schemeName))
			{
				InputControlScheme inputControlScheme;
				this.FindControlScheme(schemeName, out inputControlScheme);
				return this.ActivateControlScheme(inputControlScheme);
			}
			return this.ActivateControlScheme(default(InputControlScheme));
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00037008 File Offset: 0x00035208
		private bool TryFindControlScheme(string schemeName, out InputControlScheme scheme)
		{
			if (string.IsNullOrEmpty(schemeName))
			{
				scheme = default(InputControlScheme);
				return false;
			}
			if (InputUser.s_GlobalState.allUserData[this.index].actions == null)
			{
				throw new InvalidOperationException(string.Format("Cannot set control scheme '{0}' by name on user #{1} as not actions have been associated with the user yet (AssociateActionsWithUser)", schemeName, this.index));
			}
			ReadOnlyArray<InputControlScheme> controlSchemes = InputUser.s_GlobalState.allUserData[this.index].actions.controlSchemes;
			for (int i = 0; i < controlSchemes.Count; i++)
			{
				if (string.Compare(controlSchemes[i].name, schemeName, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					scheme = controlSchemes[i];
					return true;
				}
			}
			scheme = default(InputControlScheme);
			return false;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000370C1 File Offset: 0x000352C1
		internal void FindControlScheme(string schemeName, out InputControlScheme scheme)
		{
			if (this.TryFindControlScheme(schemeName, out scheme))
			{
				return;
			}
			throw new ArgumentException(string.Format("Cannot find control scheme '{0}' in actions '{1}'", schemeName, InputUser.s_GlobalState.allUserData[this.index].actions));
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000370F8 File Offset: 0x000352F8
		public InputUser.ControlSchemeChangeSyntax ActivateControlScheme(InputControlScheme scheme)
		{
			int index = this.index;
			InputControlScheme? controlScheme = InputUser.s_GlobalState.allUserData[index].controlScheme;
			if (controlScheme == null || (controlScheme != null && controlScheme.GetValueOrDefault() != scheme) || (scheme == default(InputControlScheme) && InputUser.s_GlobalState.allUserData[index].controlScheme != null))
			{
				this.ActivateControlSchemeInternal(index, scheme);
				InputUser.Notify(index, InputUserChange.ControlSchemeChanged, null);
			}
			return new InputUser.ControlSchemeChangeSyntax
			{
				m_UserIndex = index
			};
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0003719C File Offset: 0x0003539C
		private void ActivateControlSchemeInternal(int userIndex, InputControlScheme scheme)
		{
			bool flag = scheme == default(InputControlScheme);
			if (flag)
			{
				InputUser.s_GlobalState.allUserData[userIndex].controlScheme = null;
			}
			else
			{
				InputUser.s_GlobalState.allUserData[userIndex].controlScheme = new InputControlScheme?(scheme);
			}
			if (InputUser.s_GlobalState.allUserData[userIndex].actions != null)
			{
				if (flag)
				{
					InputUser.s_GlobalState.allUserData[userIndex].actions.bindingMask = null;
					InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
					InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch = default(InputControlScheme.MatchResult);
					return;
				}
				InputUser.s_GlobalState.allUserData[userIndex].actions.bindingMask = new InputBinding?(new InputBinding
				{
					groups = scheme.bindingGroup
				});
				InputUser.UpdateControlSchemeMatch(userIndex, false);
				if (InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.isSuccessfulMatch)
				{
					InputUser.RemoveLostDevicesForUser(userIndex);
				}
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000372CC File Offset: 0x000354CC
		public void UnpairDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			int index = this.index;
			if (!this.pairedDevices.ContainsReference(device))
			{
				return;
			}
			InputUser.RemoveDeviceFromUser(index, device, false);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00037308 File Offset: 0x00035508
		public void UnpairDevices()
		{
			int index = this.index;
			InputUser.RemoveLostDevicesForUser(index);
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				while (InputUser.s_GlobalState.allUserData[index].deviceCount > 0)
				{
					this.UnpairDevice(InputUser.s_GlobalState.allPairedDevices[InputUser.s_GlobalState.allUserData[index].deviceStartIndex + InputUser.s_GlobalState.allUserData[index].deviceCount - 1]);
				}
			}
			if (InputUser.s_GlobalState.allUserData[index].controlScheme != null)
			{
				InputUser.UpdateControlSchemeMatch(index, false);
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x000373C0 File Offset: 0x000355C0
		private static void RemoveLostDevicesForUser(int userIndex)
		{
			int lostDeviceCount = InputUser.s_GlobalState.allUserData[userIndex].lostDeviceCount;
			if (lostDeviceCount > 0)
			{
				int lostDeviceStartIndex = InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex;
				ArrayHelpers.EraseSliceWithCapacity<InputDevice>(ref InputUser.s_GlobalState.allLostDevices, ref InputUser.s_GlobalState.allLostDeviceCount, lostDeviceStartIndex, lostDeviceCount);
				InputUser.s_GlobalState.allUserData[userIndex].lostDeviceCount = 0;
				InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex = 0;
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex > lostDeviceStartIndex)
					{
						InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
						int num = i;
						allUserData[num].lostDeviceStartIndex = allUserData[num].lostDeviceStartIndex - lostDeviceCount;
					}
				}
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0003748E File Offset: 0x0003568E
		public void UnpairDevicesAndRemoveUser()
		{
			this.UnpairDevices();
			InputUser.RemoveUser(this.index);
			this.m_Id = 0U;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000374A8 File Offset: 0x000356A8
		public static InputControlList<InputDevice> GetUnpairedInputDevices()
		{
			InputControlList<InputDevice> inputControlList = new InputControlList<InputDevice>(Allocator.Temp, 0);
			InputUser.GetUnpairedInputDevices(ref inputControlList);
			return inputControlList;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000374C8 File Offset: 0x000356C8
		public static int GetUnpairedInputDevices(ref InputControlList<InputDevice> list)
		{
			int count = list.Count;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				if (!InputUser.s_GlobalState.allPairedDevices.ContainsReference(InputUser.s_GlobalState.allPairedDeviceCount, inputDevice))
				{
					list.Add(inputDevice);
				}
			}
			return list.Count - count;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00037548 File Offset: 0x00035748
		public static InputUser? FindUserPairedToDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			int num = InputUser.TryFindUserIndex(device);
			if (num == -1)
			{
				return null;
			}
			return new InputUser?(InputUser.s_GlobalState.allUsers[num]);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00037590 File Offset: 0x00035790
		public static InputUser? FindUserByAccount(InputUserAccountHandle platformUserAccountHandle)
		{
			if (platformUserAccountHandle == default(InputUserAccountHandle))
			{
				throw new ArgumentException("Empty platform user account handle", "platformUserAccountHandle");
			}
			int num = InputUser.TryFindUserIndex(platformUserAccountHandle);
			if (num == -1)
			{
				return null;
			}
			return new InputUser?(InputUser.s_GlobalState.allUsers[num]);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000375E8 File Offset: 0x000357E8
		public static InputUser CreateUserWithoutPairedDevices()
		{
			int num = InputUser.AddUser();
			return InputUser.s_GlobalState.allUsers[num];
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0003760C File Offset: 0x0003580C
		public static InputUser PerformPairingWithDevice(InputDevice device, InputUser user = default(InputUser), InputUserPairingOptions options = InputUserPairingOptions.None)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (user != default(InputUser) && !user.valid)
			{
				throw new ArgumentException("Invalid user", "user");
			}
			int num;
			if (user == default(InputUser))
			{
				num = InputUser.AddUser();
			}
			else
			{
				num = user.index;
				if ((options & InputUserPairingOptions.UnpairCurrentDevicesFromUser) != InputUserPairingOptions.None)
				{
					user.UnpairDevices();
				}
				if (user.pairedDevices.ContainsReference(device))
				{
					if ((options & InputUserPairingOptions.ForcePlatformUserAccountSelection) != InputUserPairingOptions.None)
					{
						InputUser.InitiateUserAccountSelection(num, device, options);
					}
					return user;
				}
			}
			if (!InputUser.InitiateUserAccountSelection(num, device, options))
			{
				InputUser.AddDeviceToUser(num, device, false, false);
			}
			return InputUser.s_GlobalState.allUsers[num];
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000376C0 File Offset: 0x000358C0
		private static bool InitiateUserAccountSelection(int userIndex, InputDevice device, InputUserPairingOptions options)
		{
			long num = (((options & InputUserPairingOptions.ForcePlatformUserAccountSelection) == InputUserPairingOptions.None) ? InputUser.UpdatePlatformUserAccount(userIndex, device) : 0L);
			if (((options & InputUserPairingOptions.ForcePlatformUserAccountSelection) != InputUserPairingOptions.None || (num != -1L && (num & 2L) == 0L && (options & InputUserPairingOptions.ForceNoPlatformUserAccountSelection) == InputUserPairingOptions.None)) && InputUser.InitiateUserAccountSelectionAtPlatformLevel(device))
			{
				InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
				allUserData[userIndex].flags = allUserData[userIndex].flags | InputUser.UserFlags.UserAccountSelectionInProgress;
				InputUser.s_GlobalState.ongoingAccountSelections.Append(new InputUser.OngoingAccountSelection
				{
					device = device,
					userId = InputUser.s_GlobalState.allUsers[userIndex].id
				});
				InputUser.HookIntoDeviceChange();
				InputUser.Notify(userIndex, InputUserChange.AccountSelectionInProgress, device);
				return true;
			}
			return false;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00037762 File Offset: 0x00035962
		public bool Equals(InputUser other)
		{
			return this.m_Id == other.m_Id;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00037772 File Offset: 0x00035972
		public override bool Equals(object obj)
		{
			return obj != null && obj is InputUser && this.Equals((InputUser)obj);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0003778F File Offset: 0x0003598F
		public override int GetHashCode()
		{
			return (int)this.m_Id;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00037797 File Offset: 0x00035997
		public static bool operator ==(InputUser left, InputUser right)
		{
			return left.m_Id == right.m_Id;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x000377A7 File Offset: 0x000359A7
		public static bool operator !=(InputUser left, InputUser right)
		{
			return left.m_Id != right.m_Id;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000377BC File Offset: 0x000359BC
		private static int AddUser()
		{
			uint num = InputUser.s_GlobalState.lastUserId + 1U;
			InputUser.s_GlobalState.lastUserId = num;
			uint num2 = num;
			int allUserCount = InputUser.s_GlobalState.allUserCount;
			ArrayHelpers.AppendWithCapacity<InputUser>(ref InputUser.s_GlobalState.allUsers, ref allUserCount, new InputUser
			{
				m_Id = num2
			}, 10);
			int num3 = ArrayHelpers.AppendWithCapacity<InputUser.UserData>(ref InputUser.s_GlobalState.allUserData, ref InputUser.s_GlobalState.allUserCount, default(InputUser.UserData), 10);
			InputUser.Notify(num3, InputUserChange.Added, null);
			return num3;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0003783C File Offset: 0x00035A3C
		private static void RemoveUser(int userIndex)
		{
			if (InputUser.s_GlobalState.allUserData[userIndex].controlScheme != null && InputUser.s_GlobalState.allUserData[userIndex].actions != null)
			{
				InputUser.s_GlobalState.allUserData[userIndex].actions.bindingMask = null;
			}
			InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
			InputUser.RemoveLostDevicesForUser(userIndex);
			for (int i = 0; i < InputUser.s_GlobalState.ongoingAccountSelections.length; i++)
			{
				if (InputUser.s_GlobalState.ongoingAccountSelections[i].userId == InputUser.s_GlobalState.allUsers[userIndex].id)
				{
					InputUser.s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(i);
					i--;
				}
			}
			InputUser.Notify(userIndex, InputUserChange.Removed, null);
			int allUserCount = InputUser.s_GlobalState.allUserCount;
			InputUser.s_GlobalState.allUsers.EraseAtWithCapacity(ref allUserCount, userIndex);
			InputUser.s_GlobalState.allUserData.EraseAtWithCapacity(ref InputUser.s_GlobalState.allUserCount, userIndex);
			if (InputUser.s_GlobalState.allUserCount == 0)
			{
				InputUser.UnhookFromDeviceChange();
				InputUser.UnhookFromActionChange();
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00037970 File Offset: 0x00035B70
		private static void Notify(int userIndex, InputUserChange change, InputDevice device)
		{
			if (InputUser.s_GlobalState.onChange.length == 0)
			{
				return;
			}
			InputUser.s_GlobalState.onChange.LockForChanges();
			for (int i = 0; i < InputUser.s_GlobalState.onChange.length; i++)
			{
				try
				{
					InputUser.s_GlobalState.onChange[i](InputUser.s_GlobalState.allUsers[userIndex], change, device);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.GetType().Name + " while executing 'InputUser.onChange' callbacks");
					Debug.LogException(ex);
				}
			}
			InputUser.s_GlobalState.onChange.UnlockForChanges();
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00037A24 File Offset: 0x00035C24
		private static int TryFindUserIndex(uint userId)
		{
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				if (InputUser.s_GlobalState.allUsers[i].m_Id == userId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00037A64 File Offset: 0x00035C64
		private static int TryFindUserIndex(InputUserAccountHandle platformHandle)
		{
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				InputUserAccountHandle? platformUserAccountHandle = InputUser.s_GlobalState.allUserData[i].platformUserAccountHandle;
				if (platformUserAccountHandle != null && (platformUserAccountHandle == null || platformUserAccountHandle.GetValueOrDefault() == platformHandle))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00037AC8 File Offset: 0x00035CC8
		private static int TryFindUserIndex(InputDevice device)
		{
			int num = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount);
			if (num == -1)
			{
				return -1;
			}
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				int deviceStartIndex = InputUser.s_GlobalState.allUserData[i].deviceStartIndex;
				if (deviceStartIndex <= num && num < deviceStartIndex + InputUser.s_GlobalState.allUserData[i].deviceCount)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00037B44 File Offset: 0x00035D44
		private static void AddDeviceToUser(int userIndex, InputDevice device, bool asLostDevice = false, bool dontUpdateControlScheme = false)
		{
			int num = (asLostDevice ? InputUser.s_GlobalState.allUserData[userIndex].lostDeviceCount : InputUser.s_GlobalState.allUserData[userIndex].deviceCount);
			int num2 = (asLostDevice ? InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex : InputUser.s_GlobalState.allUserData[userIndex].deviceStartIndex);
			InputUser.s_GlobalState.pairingStateVersion = InputUser.s_GlobalState.pairingStateVersion + 1;
			if (num > 0)
			{
				ArrayHelpers.MoveSlice<InputDevice>(asLostDevice ? InputUser.s_GlobalState.allLostDevices : InputUser.s_GlobalState.allPairedDevices, num2, asLostDevice ? (InputUser.s_GlobalState.allLostDeviceCount - num) : (InputUser.s_GlobalState.allPairedDeviceCount - num), num);
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (i != userIndex && (asLostDevice ? InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex : InputUser.s_GlobalState.allUserData[i].deviceStartIndex) > num2)
					{
						if (asLostDevice)
						{
							InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
							int num3 = i;
							allUserData[num3].lostDeviceStartIndex = allUserData[num3].lostDeviceStartIndex - num;
						}
						else
						{
							InputUser.UserData[] allUserData2 = InputUser.s_GlobalState.allUserData;
							int num4 = i;
							allUserData2[num4].deviceStartIndex = allUserData2[num4].deviceStartIndex - num;
						}
					}
				}
			}
			if (asLostDevice)
			{
				InputUser.s_GlobalState.allUserData[userIndex].lostDeviceStartIndex = InputUser.s_GlobalState.allLostDeviceCount - num;
				ArrayHelpers.AppendWithCapacity<InputDevice>(ref InputUser.s_GlobalState.allLostDevices, ref InputUser.s_GlobalState.allLostDeviceCount, device, 10);
				InputUser.UserData[] allUserData3 = InputUser.s_GlobalState.allUserData;
				allUserData3[userIndex].lostDeviceCount = allUserData3[userIndex].lostDeviceCount + 1;
			}
			else
			{
				InputUser.s_GlobalState.allUserData[userIndex].deviceStartIndex = InputUser.s_GlobalState.allPairedDeviceCount - num;
				ArrayHelpers.AppendWithCapacity<InputDevice>(ref InputUser.s_GlobalState.allPairedDevices, ref InputUser.s_GlobalState.allPairedDeviceCount, device, 10);
				InputUser.UserData[] allUserData4 = InputUser.s_GlobalState.allUserData;
				allUserData4[userIndex].deviceCount = allUserData4[userIndex].deviceCount + 1;
				IInputActionCollection actions = InputUser.s_GlobalState.allUserData[userIndex].actions;
				if (actions != null)
				{
					actions.devices = new ReadOnlyArray<InputDevice>?(InputUser.s_GlobalState.allUsers[userIndex].pairedDevices);
					if (!dontUpdateControlScheme && InputUser.s_GlobalState.allUserData[userIndex].controlScheme != null)
					{
						InputUser.UpdateControlSchemeMatch(userIndex, false);
					}
				}
			}
			InputUser.HookIntoDeviceChange();
			InputUser.Notify(userIndex, asLostDevice ? InputUserChange.DeviceLost : InputUserChange.DevicePaired, device);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00037DB4 File Offset: 0x00035FB4
		private static void RemoveDeviceFromUser(int userIndex, InputDevice device, bool asLostDevice = false)
		{
			int num = (asLostDevice ? InputUser.s_GlobalState.allLostDevices.IndexOfReference(device, InputUser.s_GlobalState.allLostDeviceCount) : InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allUserData[userIndex].deviceStartIndex, InputUser.s_GlobalState.allUserData[userIndex].deviceCount));
			if (num == -1)
			{
				return;
			}
			if (asLostDevice)
			{
				InputUser.s_GlobalState.allLostDevices.EraseAtWithCapacity(ref InputUser.s_GlobalState.allLostDeviceCount, num);
				InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
				allUserData[userIndex].lostDeviceCount = allUserData[userIndex].lostDeviceCount - 1;
			}
			else
			{
				InputUser.s_GlobalState.pairingStateVersion = InputUser.s_GlobalState.pairingStateVersion + 1;
				InputUser.s_GlobalState.allPairedDevices.EraseAtWithCapacity(ref InputUser.s_GlobalState.allPairedDeviceCount, num);
				InputUser.UserData[] allUserData2 = InputUser.s_GlobalState.allUserData;
				allUserData2[userIndex].deviceCount = allUserData2[userIndex].deviceCount - 1;
			}
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				if ((asLostDevice ? InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex : InputUser.s_GlobalState.allUserData[i].deviceStartIndex) > num)
				{
					if (asLostDevice)
					{
						InputUser.UserData[] allUserData3 = InputUser.s_GlobalState.allUserData;
						int num2 = i;
						allUserData3[num2].lostDeviceStartIndex = allUserData3[num2].lostDeviceStartIndex - 1;
					}
					else
					{
						InputUser.UserData[] allUserData4 = InputUser.s_GlobalState.allUserData;
						int num3 = i;
						allUserData4[num3].deviceStartIndex = allUserData4[num3].deviceStartIndex - 1;
					}
				}
			}
			if (!asLostDevice)
			{
				for (int j = 0; j < InputUser.s_GlobalState.ongoingAccountSelections.length; j++)
				{
					if (InputUser.s_GlobalState.ongoingAccountSelections[j].userId == InputUser.s_GlobalState.allUsers[userIndex].id && InputUser.s_GlobalState.ongoingAccountSelections[j].device == device)
					{
						InputUser.s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(j);
						j--;
					}
				}
				IInputActionCollection actions = InputUser.s_GlobalState.allUserData[userIndex].actions;
				if (actions != null)
				{
					actions.devices = new ReadOnlyArray<InputDevice>?(InputUser.s_GlobalState.allUsers[userIndex].pairedDevices);
					if (InputUser.s_GlobalState.allUsers[userIndex].controlScheme != null)
					{
						InputUser.UpdateControlSchemeMatch(userIndex, false);
					}
				}
				InputUser.Notify(userIndex, InputUserChange.DeviceUnpaired, device);
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00037FFC File Offset: 0x000361FC
		private static void UpdateControlSchemeMatch(int userIndex, bool autoPairMissing = false)
		{
			if (InputUser.s_GlobalState.allUserData[userIndex].controlScheme == null)
			{
				return;
			}
			InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch.Dispose();
			InputControlScheme.MatchResult matchResult = default(InputControlScheme.MatchResult);
			try
			{
				InputControlScheme value = InputUser.s_GlobalState.allUserData[userIndex].controlScheme.Value;
				if (value.deviceRequirements.Count > 0)
				{
					InputControlList<InputDevice> inputControlList = new InputControlList<InputDevice>(Allocator.Temp, 0);
					try
					{
						inputControlList.AddSlice<ReadOnlyArray<InputDevice>>(InputUser.s_GlobalState.allUsers[userIndex].pairedDevices, -1, -1, 0);
						if (autoPairMissing)
						{
							int count = inputControlList.Count;
							int unpairedInputDevices = InputUser.GetUnpairedInputDevices(ref inputControlList);
							if (InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle != null)
							{
								inputControlList.Sort<InputUser.CompareDevicesByUserAccount>(count, unpairedInputDevices, new InputUser.CompareDevicesByUserAccount
								{
									platformUserAccountHandle = InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle.Value
								});
							}
						}
						matchResult = value.PickDevicesFrom<InputControlList<InputDevice>>(inputControlList, null);
						if (matchResult.isSuccessfulMatch && autoPairMissing)
						{
							InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch = matchResult;
							foreach (InputDevice inputDevice in matchResult.devices)
							{
								if (!InputUser.s_GlobalState.allUsers[userIndex].pairedDevices.ContainsReference(inputDevice))
								{
									InputUser.AddDeviceToUser(userIndex, inputDevice, false, true);
								}
							}
						}
					}
					finally
					{
						inputControlList.Dispose();
					}
				}
				InputUser.s_GlobalState.allUserData[userIndex].controlSchemeMatch = matchResult;
			}
			catch (Exception)
			{
				matchResult.Dispose();
				throw;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00038208 File Offset: 0x00036408
		private static long UpdatePlatformUserAccount(int userIndex, InputDevice device)
		{
			InputUserAccountHandle? inputUserAccountHandle;
			string text;
			string text2;
			long num = InputUser.QueryPairedPlatformUserAccount(device, out inputUserAccountHandle, out text, out text2);
			if (num == -1L)
			{
				if ((InputUser.s_GlobalState.allUserData[userIndex].flags & InputUser.UserFlags.UserAccountSelectionInProgress) != (InputUser.UserFlags)0)
				{
					InputUser.Notify(userIndex, InputUserChange.AccountSelectionCanceled, null);
				}
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle = null;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName = null;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId = null;
				return num;
			}
			if ((InputUser.s_GlobalState.allUserData[userIndex].flags & InputUser.UserFlags.UserAccountSelectionInProgress) != (InputUser.UserFlags)0)
			{
				if ((num & 4L) == 0L)
				{
					if ((num & 16L) != 0L)
					{
						InputUser.Notify(userIndex, InputUserChange.AccountSelectionCanceled, device);
					}
					else
					{
						InputUser.UserData[] allUserData = InputUser.s_GlobalState.allUserData;
						allUserData[userIndex].flags = allUserData[userIndex].flags & ~InputUser.UserFlags.UserAccountSelectionInProgress;
						InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle = inputUserAccountHandle;
						InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName = text;
						InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId = text2;
						InputUser.Notify(userIndex, InputUserChange.AccountSelectionComplete, device);
					}
				}
			}
			else if (InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle != inputUserAccountHandle || InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId != text2)
			{
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountHandle = inputUserAccountHandle;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName = text;
				InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountId = text2;
				InputUser.Notify(userIndex, InputUserChange.AccountChanged, device);
			}
			else if (InputUser.s_GlobalState.allUserData[userIndex].platformUserAccountName != text)
			{
				InputUser.Notify(userIndex, InputUserChange.AccountNameChanged, device);
			}
			return num;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00038414 File Offset: 0x00036614
		private static long QueryPairedPlatformUserAccount(InputDevice device, out InputUserAccountHandle? platformAccountHandle, out string platformAccountName, out string platformAccountId)
		{
			QueryPairedUserAccountCommand queryPairedUserAccountCommand = QueryPairedUserAccountCommand.Create();
			long num = device.ExecuteCommand<QueryPairedUserAccountCommand>(ref queryPairedUserAccountCommand);
			if (num == -1L)
			{
				platformAccountHandle = null;
				platformAccountName = null;
				platformAccountId = null;
				return -1L;
			}
			if ((num & 2L) != 0L)
			{
				platformAccountHandle = new InputUserAccountHandle?(new InputUserAccountHandle(device.description.interfaceName ?? "<Unknown>", queryPairedUserAccountCommand.handle));
				platformAccountName = queryPairedUserAccountCommand.name;
				platformAccountId = queryPairedUserAccountCommand.id;
			}
			else
			{
				platformAccountHandle = null;
				platformAccountName = null;
				platformAccountId = null;
			}
			return num;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0003849C File Offset: 0x0003669C
		private static bool InitiateUserAccountSelectionAtPlatformLevel(InputDevice device)
		{
			InitiateUserAccountPairingCommand initiateUserAccountPairingCommand = InitiateUserAccountPairingCommand.Create();
			long num = device.ExecuteCommand<InitiateUserAccountPairingCommand>(ref initiateUserAccountPairingCommand);
			if (num == -2L)
			{
				throw new InvalidOperationException("User pairing already in progress");
			}
			return num == 1L;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000384CC File Offset: 0x000366CC
		private static void OnActionChange(object obj, InputActionChange change)
		{
			if (change == InputActionChange.BoundControlsChanged)
			{
				for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
				{
					if (InputUser.s_GlobalState.allUsers[i].actions == obj)
					{
						InputUser.Notify(i, InputUserChange.ControlsChanged, null);
					}
				}
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00038514 File Offset: 0x00036714
		private static void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (change == InputDeviceChange.Added)
			{
				for (int num = InputUser.FindLostDevice(device, 0); num != -1; num = InputUser.FindLostDevice(device, num))
				{
					int num2 = -1;
					for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
					{
						int lostDeviceStartIndex = InputUser.s_GlobalState.allUserData[i].lostDeviceStartIndex;
						if (lostDeviceStartIndex <= num && num < lostDeviceStartIndex + InputUser.s_GlobalState.allUserData[i].lostDeviceCount)
						{
							num2 = i;
							break;
						}
					}
					InputUser.RemoveDeviceFromUser(num2, InputUser.s_GlobalState.allLostDevices[num], true);
					InputUser.Notify(num2, InputUserChange.DeviceRegained, device);
					InputUser.AddDeviceToUser(num2, device, false, false);
				}
				return;
			}
			if (change == InputDeviceChange.Removed)
			{
				for (int num3 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount); num3 != -1; num3 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount))
				{
					int num4 = -1;
					for (int j = 0; j < InputUser.s_GlobalState.allUserCount; j++)
					{
						int deviceStartIndex = InputUser.s_GlobalState.allUserData[j].deviceStartIndex;
						if (deviceStartIndex <= num3 && num3 < deviceStartIndex + InputUser.s_GlobalState.allUserData[j].deviceCount)
						{
							num4 = j;
							break;
						}
					}
					InputUser.AddDeviceToUser(num4, device, true, false);
					InputUser.RemoveDeviceFromUser(num4, device, false);
				}
				return;
			}
			if (change != InputDeviceChange.ConfigurationChanged)
			{
				return;
			}
			bool flag = false;
			for (int k = 0; k < InputUser.s_GlobalState.ongoingAccountSelections.length; k++)
			{
				if (InputUser.s_GlobalState.ongoingAccountSelections[k].device == device)
				{
					InputUser inputUser = default(InputUser);
					inputUser.m_Id = InputUser.s_GlobalState.ongoingAccountSelections[k].userId;
					int index = inputUser.index;
					if ((InputUser.UpdatePlatformUserAccount(index, device) & 4L) == 0L)
					{
						flag = true;
						InputUser.s_GlobalState.ongoingAccountSelections.RemoveAtByMovingTailWithCapacity(k);
						k--;
						if (!InputUser.s_GlobalState.allUsers[index].pairedDevices.ContainsReference(device))
						{
							InputUser.AddDeviceToUser(index, device, false, false);
						}
					}
				}
			}
			if (!flag)
			{
				int num7;
				for (int num5 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, InputUser.s_GlobalState.allPairedDeviceCount); num5 != -1; num5 = InputUser.s_GlobalState.allPairedDevices.IndexOfReference(device, num7, InputUser.s_GlobalState.allPairedDeviceCount - num7))
				{
					int num6 = -1;
					for (int l = 0; l < InputUser.s_GlobalState.allUserCount; l++)
					{
						int deviceStartIndex2 = InputUser.s_GlobalState.allUserData[l].deviceStartIndex;
						if (deviceStartIndex2 <= num5 && num5 < deviceStartIndex2 + InputUser.s_GlobalState.allUserData[l].deviceCount)
						{
							num6 = l;
							break;
						}
					}
					InputUser.UpdatePlatformUserAccount(num6, device);
					num7 = num5 + Math.Max(1, InputUser.s_GlobalState.allUserData[num6].deviceCount);
				}
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0003880C File Offset: 0x00036A0C
		private static int FindLostDevice(InputDevice device, int startIndex = 0)
		{
			int deviceId = device.deviceId;
			for (int i = startIndex; i < InputUser.s_GlobalState.allLostDeviceCount; i++)
			{
				InputDevice inputDevice = InputUser.s_GlobalState.allLostDevices[i];
				if (device == inputDevice || inputDevice.deviceId == deviceId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00038854 File Offset: 0x00036A54
		private static void OnEvent(InputEventPtr eventPtr, InputDevice device)
		{
			if (InputUser.s_GlobalState.listenForUnpairedDeviceActivity == 0)
			{
				return;
			}
			FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				return;
			}
			if (!device.enabled)
			{
				return;
			}
			if (InputUser.s_GlobalState.allPairedDevices.ContainsReference(InputUser.s_GlobalState.allPairedDeviceCount, device))
			{
				return;
			}
			if (!DelegateHelpers.InvokeCallbacksSafe_AnyCallbackReturnsTrue<InputDevice, InputEventPtr>(ref InputUser.s_GlobalState.onPreFilterUnpairedDeviceUsed, device, eventPtr, "InputUser.onPreFilterUnpairedDeviceActivity", null))
			{
				return;
			}
			foreach (InputControl inputControl in eventPtr.EnumerateChangedControls(device, 0.0001f))
			{
				bool flag = false;
				InputUser.s_GlobalState.onUnpairedDeviceUsed.LockForChanges();
				for (int i = 0; i < InputUser.s_GlobalState.onUnpairedDeviceUsed.length; i++)
				{
					int pairingStateVersion = InputUser.s_GlobalState.pairingStateVersion;
					try
					{
						InputUser.s_GlobalState.onUnpairedDeviceUsed[i](inputControl, eventPtr);
					}
					catch (Exception ex)
					{
						Debug.LogError(ex.GetType().Name + " while executing 'InputUser.onUnpairedDeviceUsed' callbacks");
						Debug.LogException(ex);
					}
					if (pairingStateVersion != InputUser.s_GlobalState.pairingStateVersion && InputUser.FindUserPairedToDevice(device) != null)
					{
						flag = true;
						break;
					}
				}
				InputUser.s_GlobalState.onUnpairedDeviceUsed.UnlockForChanges();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000389E8 File Offset: 0x00036BE8
		internal static ISavedState SaveAndResetState()
		{
			ISavedState savedState = new SavedStructState<InputUser.GlobalState>(ref InputUser.s_GlobalState, delegate(ref InputUser.GlobalState state)
			{
				InputUser.s_GlobalState = state;
			}, delegate
			{
				InputUser.DisposeAndResetGlobalState();
			});
			InputUser.s_GlobalState = default(InputUser.GlobalState);
			return savedState;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00038A48 File Offset: 0x00036C48
		private static void HookIntoActionChange()
		{
			if (InputUser.s_GlobalState.onActionChangeHooked)
			{
				return;
			}
			if (InputUser.s_GlobalState.actionChangeDelegate == null)
			{
				InputUser.s_GlobalState.actionChangeDelegate = new Action<object, InputActionChange>(InputUser.OnActionChange);
			}
			InputSystem.onActionChange += InputUser.OnActionChange;
			InputUser.s_GlobalState.onActionChangeHooked = true;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00038AA0 File Offset: 0x00036CA0
		private static void UnhookFromActionChange()
		{
			if (!InputUser.s_GlobalState.onActionChangeHooked)
			{
				return;
			}
			InputSystem.onActionChange -= InputUser.OnActionChange;
			InputUser.s_GlobalState.onActionChangeHooked = false;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00038ACC File Offset: 0x00036CCC
		private static void HookIntoDeviceChange()
		{
			if (InputUser.s_GlobalState.onDeviceChangeHooked)
			{
				return;
			}
			if (InputUser.s_GlobalState.onDeviceChangeDelegate == null)
			{
				InputUser.s_GlobalState.onDeviceChangeDelegate = new Action<InputDevice, InputDeviceChange>(InputUser.OnDeviceChange);
			}
			InputSystem.onDeviceChange += InputUser.s_GlobalState.onDeviceChangeDelegate;
			InputUser.s_GlobalState.onDeviceChangeHooked = true;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00038B22 File Offset: 0x00036D22
		private static void UnhookFromDeviceChange()
		{
			if (!InputUser.s_GlobalState.onDeviceChangeHooked)
			{
				return;
			}
			InputSystem.onDeviceChange -= InputUser.s_GlobalState.onDeviceChangeDelegate;
			InputUser.s_GlobalState.onDeviceChangeHooked = false;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00038B4C File Offset: 0x00036D4C
		private static void HookIntoEvents()
		{
			if (InputUser.s_GlobalState.onEventHooked)
			{
				return;
			}
			if (InputUser.s_GlobalState.onEventDelegate == null)
			{
				InputUser.s_GlobalState.onEventDelegate = new Action<InputEventPtr, InputDevice>(InputUser.OnEvent);
			}
			InputSystem.onEvent += InputUser.s_GlobalState.onEventDelegate;
			InputUser.s_GlobalState.onEventHooked = true;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00038BAC File Offset: 0x00036DAC
		private static void UnhookFromDeviceStateChange()
		{
			if (!InputUser.s_GlobalState.onEventHooked)
			{
				return;
			}
			InputSystem.onEvent -= InputUser.s_GlobalState.onEventDelegate;
			InputUser.s_GlobalState.onEventHooked = false;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00038BE0 File Offset: 0x00036DE0
		private static void DisposeAndResetGlobalState()
		{
			for (int i = 0; i < InputUser.s_GlobalState.allUserCount; i++)
			{
				InputUser.s_GlobalState.allUserData[i].controlSchemeMatch.Dispose();
			}
			uint lastUserId = InputUser.s_GlobalState.lastUserId;
			InputUser.s_GlobalState = default(InputUser.GlobalState);
			InputUser.s_GlobalState.lastUserId = lastUserId;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00038C3D File Offset: 0x00036E3D
		internal static void ResetGlobals()
		{
			InputUser.UnhookFromActionChange();
			InputUser.UnhookFromDeviceChange();
			InputUser.UnhookFromDeviceStateChange();
			InputUser.DisposeAndResetGlobalState();
		}

		// Token: 0x0400037F RID: 895
		public const uint InvalidId = 0U;

		// Token: 0x04000380 RID: 896
		private uint m_Id;

		// Token: 0x04000381 RID: 897
		private static InputUser.GlobalState s_GlobalState;

		// Token: 0x020001C2 RID: 450
		public struct ControlSchemeChangeSyntax
		{
			// Token: 0x06001404 RID: 5124 RVA: 0x0005C744 File Offset: 0x0005A944
			public InputUser.ControlSchemeChangeSyntax AndPairRemainingDevices()
			{
				InputUser.UpdateControlSchemeMatch(this.m_UserIndex, true);
				return this;
			}

			// Token: 0x0400091A RID: 2330
			internal int m_UserIndex;
		}

		// Token: 0x020001C3 RID: 451
		[Flags]
		internal enum UserFlags
		{
			// Token: 0x0400091C RID: 2332
			BindToAllDevices = 1,
			// Token: 0x0400091D RID: 2333
			UserAccountSelectionInProgress = 2
		}

		// Token: 0x020001C4 RID: 452
		private struct UserData
		{
			// Token: 0x0400091E RID: 2334
			public InputUserAccountHandle? platformUserAccountHandle;

			// Token: 0x0400091F RID: 2335
			public string platformUserAccountName;

			// Token: 0x04000920 RID: 2336
			public string platformUserAccountId;

			// Token: 0x04000921 RID: 2337
			public int deviceCount;

			// Token: 0x04000922 RID: 2338
			public int deviceStartIndex;

			// Token: 0x04000923 RID: 2339
			public IInputActionCollection actions;

			// Token: 0x04000924 RID: 2340
			public InputControlScheme? controlScheme;

			// Token: 0x04000925 RID: 2341
			public InputControlScheme.MatchResult controlSchemeMatch;

			// Token: 0x04000926 RID: 2342
			public int lostDeviceCount;

			// Token: 0x04000927 RID: 2343
			public int lostDeviceStartIndex;

			// Token: 0x04000928 RID: 2344
			public InputUser.UserFlags flags;
		}

		// Token: 0x020001C5 RID: 453
		private struct CompareDevicesByUserAccount : IComparer<InputDevice>
		{
			// Token: 0x06001405 RID: 5125 RVA: 0x0005C758 File Offset: 0x0005A958
			public int Compare(InputDevice x, InputDevice y)
			{
				InputUserAccountHandle? userAccountHandleForDevice = InputUser.CompareDevicesByUserAccount.GetUserAccountHandleForDevice(x);
				InputUserAccountHandle? userAccountHandleForDevice2 = InputUser.CompareDevicesByUserAccount.GetUserAccountHandleForDevice(x);
				InputUserAccountHandle? inputUserAccountHandle = userAccountHandleForDevice;
				InputUserAccountHandle inputUserAccountHandle2 = this.platformUserAccountHandle;
				if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
				{
					inputUserAccountHandle = userAccountHandleForDevice2;
					inputUserAccountHandle2 = this.platformUserAccountHandle;
					if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
					{
						return 0;
					}
				}
				inputUserAccountHandle = userAccountHandleForDevice;
				inputUserAccountHandle2 = this.platformUserAccountHandle;
				if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
				{
					return -1;
				}
				inputUserAccountHandle = userAccountHandleForDevice2;
				inputUserAccountHandle2 = this.platformUserAccountHandle;
				if (inputUserAccountHandle != null && (inputUserAccountHandle == null || inputUserAccountHandle.GetValueOrDefault() == inputUserAccountHandle2))
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06001406 RID: 5126 RVA: 0x0005C83C File Offset: 0x0005AA3C
			private static InputUserAccountHandle? GetUserAccountHandleForDevice(InputDevice device)
			{
				return null;
			}

			// Token: 0x04000929 RID: 2345
			public InputUserAccountHandle platformUserAccountHandle;
		}

		// Token: 0x020001C6 RID: 454
		private struct OngoingAccountSelection
		{
			// Token: 0x0400092A RID: 2346
			public InputDevice device;

			// Token: 0x0400092B RID: 2347
			public uint userId;
		}

		// Token: 0x020001C7 RID: 455
		private struct GlobalState
		{
			// Token: 0x0400092C RID: 2348
			internal int pairingStateVersion;

			// Token: 0x0400092D RID: 2349
			internal uint lastUserId;

			// Token: 0x0400092E RID: 2350
			internal int allUserCount;

			// Token: 0x0400092F RID: 2351
			internal int allPairedDeviceCount;

			// Token: 0x04000930 RID: 2352
			internal int allLostDeviceCount;

			// Token: 0x04000931 RID: 2353
			internal InputUser[] allUsers;

			// Token: 0x04000932 RID: 2354
			internal InputUser.UserData[] allUserData;

			// Token: 0x04000933 RID: 2355
			internal InputDevice[] allPairedDevices;

			// Token: 0x04000934 RID: 2356
			internal InputDevice[] allLostDevices;

			// Token: 0x04000935 RID: 2357
			internal InlinedArray<InputUser.OngoingAccountSelection> ongoingAccountSelections;

			// Token: 0x04000936 RID: 2358
			internal CallbackArray<Action<InputUser, InputUserChange, InputDevice>> onChange;

			// Token: 0x04000937 RID: 2359
			internal CallbackArray<Action<InputControl, InputEventPtr>> onUnpairedDeviceUsed;

			// Token: 0x04000938 RID: 2360
			internal CallbackArray<Func<InputDevice, InputEventPtr, bool>> onPreFilterUnpairedDeviceUsed;

			// Token: 0x04000939 RID: 2361
			internal Action<object, InputActionChange> actionChangeDelegate;

			// Token: 0x0400093A RID: 2362
			internal Action<InputDevice, InputDeviceChange> onDeviceChangeDelegate;

			// Token: 0x0400093B RID: 2363
			internal Action<InputEventPtr, InputDevice> onEventDelegate;

			// Token: 0x0400093C RID: 2364
			internal bool onActionChangeHooked;

			// Token: 0x0400093D RID: 2365
			internal bool onDeviceChangeHooked;

			// Token: 0x0400093E RID: 2366
			internal bool onEventHooked;

			// Token: 0x0400093F RID: 2367
			internal int listenForUnpairedDeviceActivity;
		}
	}
}
