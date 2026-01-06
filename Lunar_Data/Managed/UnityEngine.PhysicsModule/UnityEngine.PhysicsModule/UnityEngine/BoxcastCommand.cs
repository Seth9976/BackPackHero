using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003C RID: 60
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	[NativeHeader("Modules/Physics/BatchCommands/BoxcastCommand.h")]
	public struct BoxcastCommand
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00006568 File Offset: 0x00004768
		public BoxcastCommand(Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.center = center;
			this.halfExtents = halfExtents;
			this.orientation = orientation;
			this.direction = direction;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
			this.physicsScene = Physics.defaultPhysicsScene;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000065C0 File Offset: 0x000047C0
		public BoxcastCommand(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.center = center;
			this.halfExtents = halfExtents;
			this.orientation = orientation;
			this.direction = direction;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
			this.physicsScene = physicsScene;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00006612 File Offset: 0x00004812
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000661A File Offset: 0x0000481A
		public Vector3 center { readonly get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00006623 File Offset: 0x00004823
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0000662B File Offset: 0x0000482B
		public Vector3 halfExtents { readonly get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00006634 File Offset: 0x00004834
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x0000663C File Offset: 0x0000483C
		public Quaternion orientation { readonly get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00006645 File Offset: 0x00004845
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0000664D File Offset: 0x0000484D
		public Vector3 direction { readonly get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00006656 File Offset: 0x00004856
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0000665E File Offset: 0x0000485E
		public float distance { readonly get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00006667 File Offset: 0x00004867
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000666F File Offset: 0x0000486F
		public int layerMask { readonly get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00006678 File Offset: 0x00004878
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00006680 File Offset: 0x00004880
		internal int maxHits { readonly get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00006689 File Offset: 0x00004889
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00006691 File Offset: 0x00004891
		public PhysicsScene physicsScene { readonly get; set; }

		// Token: 0x0600047F RID: 1151 RVA: 0x0000669C File Offset: 0x0000489C
		public static JobHandle ScheduleBatch(NativeArray<BoxcastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<BoxcastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<BoxcastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<BoxcastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<BoxcastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return BoxcastCommand.ScheduleBoxcastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<BoxcastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000066F0 File Offset: 0x000048F0
		[FreeFunction("ScheduleBoxcastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleBoxcastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle jobHandle;
			BoxcastCommand.ScheduleBoxcastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000481 RID: 1153
		[MethodImpl(4096)]
		private unsafe static extern void ScheduleBoxcastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);
	}
}
