using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200000B RID: 11
	public static class Cloning
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002740 File Offset: 0x00000940
		static Cloning()
		{
			Cloning.cloners.Add(Cloning.arrayCloner);
			Cloning.cloners.Add(Cloning.dictionaryCloner);
			Cloning.cloners.Add(Cloning.enumerableCloner);
			Cloning.cloners.Add(Cloning.listCloner);
			Cloning.cloners.Add(Cloning.animationCurveCloner);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000027F7 File Offset: 0x000009F7
		public static HashSet<ICloner> cloners { get; } = new HashSet<ICloner>();

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000027FE File Offset: 0x000009FE
		public static ArrayCloner arrayCloner { get; } = new ArrayCloner();

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002805 File Offset: 0x00000A05
		public static DictionaryCloner dictionaryCloner { get; } = new DictionaryCloner();

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000280C File Offset: 0x00000A0C
		public static EnumerableCloner enumerableCloner { get; } = new EnumerableCloner();

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002813 File Offset: 0x00000A13
		public static ListCloner listCloner { get; } = new ListCloner();

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000281A File Offset: 0x00000A1A
		public static AnimationCurveCloner animationCurveCloner { get; } = new AnimationCurveCloner();

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002821 File Offset: 0x00000A21
		public static FieldsCloner fieldsCloner { get; } = new FieldsCloner();

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002828 File Offset: 0x00000A28
		public static FakeSerializationCloner fakeSerializationCloner { get; } = new FakeSerializationCloner();

		// Token: 0x06000037 RID: 55 RVA: 0x00002830 File Offset: 0x00000A30
		public static object Clone(this object original, ICloner fallbackCloner, bool tryPreserveInstances)
		{
			object obj;
			using (CloningContext cloningContext = CloningContext.New(fallbackCloner, tryPreserveInstances))
			{
				obj = Cloning.Clone(cloningContext, original);
			}
			return obj;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000286C File Offset: 0x00000A6C
		public static T Clone<T>(this T original, ICloner fallbackCloner, bool tryPreserveInstances)
		{
			return (T)((object)original.Clone(fallbackCloner, tryPreserveInstances));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002880 File Offset: 0x00000A80
		public static object CloneViaFakeSerialization(this object original)
		{
			return original.Clone(Cloning.fakeSerializationCloner, true);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000288E File Offset: 0x00000A8E
		public static T CloneViaFakeSerialization<T>(this T original)
		{
			return (T)((object)original.CloneViaFakeSerialization());
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028A0 File Offset: 0x00000AA0
		internal static object Clone(CloningContext context, object original)
		{
			object obj = null;
			Cloning.CloneInto(context, ref obj, original);
			return obj;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028BC File Offset: 0x00000ABC
		internal static void CloneInto(CloningContext context, ref object clone, object original)
		{
			if (original == null)
			{
				clone = null;
				return;
			}
			Type type = original.GetType();
			if (Cloning.Skippable(type))
			{
				clone = original;
				return;
			}
			if (context.clonings.ContainsKey(original))
			{
				clone = context.clonings[original];
				return;
			}
			ICloner cloner = Cloning.GetCloner(original, type, context.fallbackCloner);
			if (clone == null)
			{
				clone = cloner.ConstructClone(type, original);
			}
			context.clonings.Add(original, clone);
			cloner.BeforeClone(type, original);
			cloner.FillClone(type, ref clone, original, context);
			cloner.AfterClone(type, clone);
			context.clonings[original] = clone;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002954 File Offset: 0x00000B54
		[CanBeNull]
		public static ICloner GetCloner(object original, Type type)
		{
			ISpecifiesCloner specifiesCloner = original as ISpecifiesCloner;
			if (specifiesCloner != null)
			{
				return specifiesCloner.cloner;
			}
			return Cloning.cloners.FirstOrDefault((ICloner cloner) => cloner.Handles(type));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002998 File Offset: 0x00000B98
		private static ICloner GetCloner(object original, Type type, ICloner fallbackCloner)
		{
			ICloner cloner = Cloning.GetCloner(original, type);
			if (cloner != null)
			{
				return cloner;
			}
			Ensure.That("fallbackCloner").IsNotNull<ICloner>(fallbackCloner);
			return fallbackCloner;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029C4 File Offset: 0x00000BC4
		private static bool Skippable(Type type)
		{
			bool flag;
			if (!Cloning.skippable.TryGetValue(type, out flag))
			{
				flag = type.IsValueType || type == typeof(string) || typeof(Type).IsAssignableFrom(type) || typeof(Object).IsAssignableFrom(type);
				Cloning.skippable.Add(type, flag);
			}
			return flag;
		}

		// Token: 0x04000005 RID: 5
		private static readonly Dictionary<Type, bool> skippable = new Dictionary<Type, bool>();
	}
}
