using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000097 RID: 151
	[UnitCategory("Events/Physics")]
	public sealed class OnParticleCollision : GameObjectEventUnit<GameObject>
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00009B63 File Offset: 0x00007D63
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnParticleCollisionMessageListener);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00009B6F File Offset: 0x00007D6F
		protected override string hookName
		{
			get
			{
				return "OnParticleCollision";
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00009B76 File Offset: 0x00007D76
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00009B7E File Offset: 0x00007D7E
		[DoNotSerialize]
		public ValueOutput other { get; private set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00009B87 File Offset: 0x00007D87
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00009B8F File Offset: 0x00007D8F
		[DoNotSerialize]
		public ValueOutput collisionEvents { get; private set; }

		// Token: 0x06000483 RID: 1155 RVA: 0x00009B98 File Offset: 0x00007D98
		protected override void Definition()
		{
			base.Definition();
			this.other = base.ValueOutput<GameObject>("other");
			this.collisionEvents = base.ValueOutput<List<ParticleCollisionEvent>>("collisionEvents");
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00009BC4 File Offset: 0x00007DC4
		protected override void AssignArguments(Flow flow, GameObject other)
		{
			flow.SetValue(this.other, other);
			List<ParticleCollisionEvent> list = new List<ParticleCollisionEvent>();
			flow.stack.GetElementData<GameObjectEventUnit<GameObject>.Data>(this).target.GetComponent<ParticleSystem>().GetCollisionEvents(other, list);
			flow.SetValue(this.collisionEvents, list);
		}
	}
}
