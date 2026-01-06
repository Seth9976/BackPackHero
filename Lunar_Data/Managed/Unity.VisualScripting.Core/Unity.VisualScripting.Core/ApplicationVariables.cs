using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000166 RID: 358
	public static class ApplicationVariables
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00028F8E File Offset: 0x0002718E
		public static VariablesAsset asset
		{
			get
			{
				if (ApplicationVariables._asset == null)
				{
					ApplicationVariables.Load();
				}
				return ApplicationVariables._asset;
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00028FA7 File Offset: 0x000271A7
		public static void Load()
		{
			ApplicationVariables._asset = Resources.Load<VariablesAsset>("ApplicationVariables") ?? ScriptableObject.CreateInstance<VariablesAsset>();
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00028FC1 File Offset: 0x000271C1
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x00028FC8 File Offset: 0x000271C8
		public static VariableDeclarations runtime { get; private set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00028FD0 File Offset: 0x000271D0
		public static VariableDeclarations initial
		{
			get
			{
				return ApplicationVariables.asset.declarations;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00028FDC File Offset: 0x000271DC
		public static VariableDeclarations current
		{
			get
			{
				if (!Application.isPlaying)
				{
					return ApplicationVariables.initial;
				}
				return ApplicationVariables.runtime;
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00028FF0 File Offset: 0x000271F0
		public static void OnEnterEditMode()
		{
			ApplicationVariables.DestroyRuntimeDeclarations();
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00028FF7 File Offset: 0x000271F7
		public static void OnExitEditMode()
		{
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00028FF9 File Offset: 0x000271F9
		internal static void OnEnterPlayMode()
		{
			ApplicationVariables.CreateRuntimeDeclarations();
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00029000 File Offset: 0x00027200
		internal static void OnExitPlayMode()
		{
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00029002 File Offset: 0x00027202
		private static void CreateRuntimeDeclarations()
		{
			ApplicationVariables.runtime = ApplicationVariables.asset.declarations.CloneViaFakeSerialization<VariableDeclarations>();
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00029018 File Offset: 0x00027218
		private static void DestroyRuntimeDeclarations()
		{
			ApplicationVariables.runtime = null;
		}

		// Token: 0x0400023F RID: 575
		public const string assetPath = "ApplicationVariables";

		// Token: 0x04000240 RID: 576
		private static VariablesAsset _asset;
	}
}
