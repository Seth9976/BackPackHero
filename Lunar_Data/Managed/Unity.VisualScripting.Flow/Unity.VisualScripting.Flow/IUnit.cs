using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000158 RID: 344
	public interface IUnit : IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060008DE RID: 2270
		FlowGraph graph { get; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060008DF RID: 2271
		bool canDefine { get; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060008E0 RID: 2272
		bool isDefined { get; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060008E1 RID: 2273
		bool failedToDefine { get; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060008E2 RID: 2274
		Exception definitionException { get; }

		// Token: 0x060008E3 RID: 2275
		void Define();

		// Token: 0x060008E4 RID: 2276
		void EnsureDefined();

		// Token: 0x060008E5 RID: 2277
		void RemoveUnconnectedInvalidPorts();

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060008E6 RID: 2278
		Dictionary<string, object> defaultValues { get; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060008E7 RID: 2279
		IUnitPortCollection<ControlInput> controlInputs { get; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060008E8 RID: 2280
		IUnitPortCollection<ControlOutput> controlOutputs { get; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060008E9 RID: 2281
		IUnitPortCollection<ValueInput> valueInputs { get; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060008EA RID: 2282
		IUnitPortCollection<ValueOutput> valueOutputs { get; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060008EB RID: 2283
		IUnitPortCollection<InvalidInput> invalidInputs { get; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060008EC RID: 2284
		IUnitPortCollection<InvalidOutput> invalidOutputs { get; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060008ED RID: 2285
		IEnumerable<IUnitInputPort> inputs { get; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060008EE RID: 2286
		IEnumerable<IUnitOutputPort> outputs { get; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060008EF RID: 2287
		IEnumerable<IUnitInputPort> validInputs { get; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060008F0 RID: 2288
		IEnumerable<IUnitOutputPort> validOutputs { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060008F1 RID: 2289
		IEnumerable<IUnitPort> ports { get; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060008F2 RID: 2290
		IEnumerable<IUnitPort> invalidPorts { get; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060008F3 RID: 2291
		IEnumerable<IUnitPort> validPorts { get; }

		// Token: 0x060008F4 RID: 2292
		void PortsChanged();

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060008F5 RID: 2293
		// (remove) Token: 0x060008F6 RID: 2294
		event Action onPortsChanged;

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060008F7 RID: 2295
		IConnectionCollection<IUnitRelation, IUnitPort, IUnitPort> relations { get; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060008F8 RID: 2296
		IEnumerable<IUnitConnection> connections { get; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060008F9 RID: 2297
		bool isControlRoot { get; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060008FA RID: 2298
		// (set) Token: 0x060008FB RID: 2299
		Vector2 position { get; set; }
	}
}
