using System;
using Pathfinding.Drawing;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000005 RID: 5
	public abstract class VersionedMonoBehaviour : MonoBehaviourGizmos, ISerializationCallbackReceiver, IVersionedMonoBehaviourInternal, IEntityIndex
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002060 File Offset: 0x00000260
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002068 File Offset: 0x00000268
		int IEntityIndex.EntityIndex { get; set; }

		// Token: 0x06000006 RID: 6 RVA: 0x00002074 File Offset: 0x00000274
		protected virtual void Awake()
		{
			if (Application.isPlaying)
			{
				if (this.version == 0)
				{
					Migrations migrations = new Migrations(int.MaxValue);
					this.OnUpgradeSerializedData(ref migrations, true);
					this.version = migrations.allMigrations;
					return;
				}
				((IVersionedMonoBehaviourInternal)this).UpgradeFromUnityThread();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020B8 File Offset: 0x000002B8
		protected virtual void Reset()
		{
			Migrations migrations = new Migrations(int.MaxValue);
			this.OnUpgradeSerializedData(ref migrations, true);
			this.version = migrations.allMigrations;
			this.DisableGizmosIcon();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020EC File Offset: 0x000002EC
		private void DisableGizmosIcon()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020EE File Offset: 0x000002EE
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020F0 File Offset: 0x000002F0
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.UpgradeSerializedData(false);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020FC File Offset: 0x000002FC
		protected void UpgradeSerializedData(bool isUnityThread)
		{
			Migrations migrations = new Migrations(this.version);
			this.OnUpgradeSerializedData(ref migrations, isUnityThread);
			if (migrations.ignore)
			{
				return;
			}
			if (migrations.IsLegacyFormat)
			{
				throw new Exception("Failed to migrate from the legacy format");
			}
			if ((migrations.finishedMigrations & ~(migrations.allMigrations != 0)) != 0)
			{
				throw new Exception("Run more migrations than there are migrations to run. Finished: " + migrations.finishedMigrations.ToString("X") + " all: " + migrations.allMigrations.ToString("X"));
			}
			if (isUnityThread && (migrations.allMigrations & ~(migrations.finishedMigrations != 0)) != 0)
			{
				throw new Exception("Some migrations were registered, but they did not run. Finished: " + migrations.finishedMigrations.ToString("X") + " all: " + migrations.allMigrations.ToString("X"));
			}
			this.version = migrations.finishedMigrations;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021DC File Offset: 0x000003DC
		protected virtual void OnUpgradeSerializedData(ref Migrations migrations, bool unityThread)
		{
			int num;
			if (migrations.TryMigrateFromLegacyFormat(out num) && num > 1)
			{
				throw new Exception("Reached base class without having migrated the legacy format, and the legacy version is not version 1.");
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002202 File Offset: 0x00000402
		void IVersionedMonoBehaviourInternal.UpgradeFromUnityThread()
		{
			this.UpgradeSerializedData(true);
		}

		// Token: 0x04000002 RID: 2
		[SerializeField]
		[HideInInspector]
		private int version;
	}
}
