using System;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000015 RID: 21
	public sealed class FollowerEntity : VersionedMonoBehaviour
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00006C89 File Offset: 0x00004E89
		public void Start()
		{
			Debug.LogError("The FollowerEntity component requires at least version 1.0 of the 'Entities' package to be installed. You can install it using the Unity package manager.");
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006C95 File Offset: 0x00004E95
		protected override void OnUpgradeSerializedData(ref Migrations migrations, bool unityThread)
		{
			migrations.IgnoreMigrationAttempt();
		}
	}
}
