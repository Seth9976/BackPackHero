using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200003A RID: 58
	internal static class DependencyTreeExtensions
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00002E88 File Offset: 0x00001088
		internal static string ToJson(this DependencyTree tree, ICollection<int> order = null)
		{
			JArray jarray = new JArray();
			JProperty jproperty = new JProperty("ordered", jarray);
			if (order != null)
			{
				foreach (int num in order)
				{
					JObject packageJObject = DependencyTreeExtensions.GetPackageJObject(tree, num);
					jarray.Add(new JObject(packageJObject));
				}
			}
			JArray jarray2 = new JArray();
			JProperty jproperty2 = new JProperty("packages", jarray2);
			foreach (int num2 in tree.PackageTypeHashToInstance.Keys)
			{
				JObject packageJObject2 = DependencyTreeExtensions.GetPackageJObject(tree, num2);
				jarray2.Add(packageJObject2);
			}
			JArray jarray3 = new JArray();
			JProperty jproperty3 = new JProperty("components", jarray3);
			foreach (int num3 in tree.ComponentTypeHashToInstance.Keys)
			{
				JObject componentJObject = DependencyTreeExtensions.GetComponentJObject(tree, num3);
				jarray3.Add(componentJObject);
			}
			return new JObject(new object[] { jproperty, jproperty2, jproperty3 }).ToString();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002FE8 File Offset: 0x000011E8
		internal static bool IsOptional(this DependencyTree tree, int componentTypeHash)
		{
			IServiceComponent serviceComponent;
			return tree.ComponentTypeHashToInstance.TryGetValue(componentTypeHash, out serviceComponent) && serviceComponent == null;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000300B File Offset: 0x0000120B
		internal static bool IsProvided(this DependencyTree tree, int componentTypeHash)
		{
			return tree.ComponentTypeHashToPackageTypeHash.ContainsKey(componentTypeHash);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000301C File Offset: 0x0000121C
		private static JObject GetPackageJObject(DependencyTree tree, int packageHash)
		{
			JProperty jproperty = new JProperty("packageHash", packageHash);
			IInitializablePackage initializablePackage;
			tree.PackageTypeHashToInstance.TryGetValue(packageHash, out initializablePackage);
			JProperty jproperty2 = new JProperty("packageProvider", (initializablePackage != null) ? initializablePackage.GetType().Name : "null");
			JArray jarray = new JArray();
			JProperty jproperty3 = new JProperty("packageDependencies", jarray);
			List<int> list;
			if (tree.PackageTypeHashToComponentTypeHashDependencies.TryGetValue(packageHash, out list))
			{
				foreach (int num in list)
				{
					JProperty jproperty4 = new JProperty("dependencyHash", num);
					IServiceComponent serviceComponent;
					tree.ComponentTypeHashToInstance.TryGetValue(num, out serviceComponent);
					JProperty jproperty5 = new JProperty("dependencyComponent", DependencyTreeExtensions.GetComponentIdentifier(serviceComponent));
					JProperty jproperty6 = new JProperty("dependencyProvided", tree.IsProvided(num) ? "true" : "false");
					JProperty jproperty7 = new JProperty("dependencyOptional", tree.IsOptional(num) ? "true" : "false");
					JObject jobject = new JObject(new object[] { jproperty4, jproperty5, jproperty6, jproperty7 });
					jarray.Add(jobject);
				}
			}
			return new JObject(new object[] { jproperty, jproperty2, jproperty3 });
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003190 File Offset: 0x00001390
		private static JObject GetComponentJObject(DependencyTree tree, int componentHash)
		{
			JProperty jproperty = new JProperty("componentHash", componentHash);
			IServiceComponent serviceComponent;
			tree.ComponentTypeHashToInstance.TryGetValue(componentHash, out serviceComponent);
			JProperty jproperty2 = new JProperty("component", DependencyTreeExtensions.GetComponentIdentifier(serviceComponent));
			int num;
			tree.ComponentTypeHashToPackageTypeHash.TryGetValue(componentHash, out num);
			JProperty jproperty3 = new JProperty("componentPackageHash", num);
			IInitializablePackage initializablePackage;
			bool flag = tree.PackageTypeHashToInstance.TryGetValue(num, out initializablePackage);
			JProperty jproperty4 = new JProperty("componentPackage", flag ? initializablePackage.GetType().Name : "null");
			return new JObject(new object[] { jproperty, jproperty2, jproperty3, jproperty4 });
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003240 File Offset: 0x00001440
		private static string GetComponentIdentifier(IServiceComponent component)
		{
			if (component == null)
			{
				return "null";
			}
			MissingComponent missingComponent = component as MissingComponent;
			if (missingComponent != null)
			{
				return missingComponent.IntendedType.Name;
			}
			return component.GetType().Name;
		}
	}
}
