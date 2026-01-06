using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200003B RID: 59
	internal struct DependencyTreeInitializeOrderSorter
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00003277 File Offset: 0x00001477
		public DependencyTreeInitializeOrderSorter(DependencyTree tree, ICollection<int> target)
		{
			this.Tree = tree;
			this.Target = target;
			this.m_PackageTypeHashExplorationHistory = null;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003290 File Offset: 0x00001490
		public void SortRegisteredPackagesIntoTarget()
		{
			this.Target.Clear();
			this.RemoveUnprovidedOptionalDependenciesFromTree();
			IReadOnlyCollection<int> packageTypeHashes = this.GetPackageTypeHashes();
			this.m_PackageTypeHashExplorationHistory = new Dictionary<int, DependencyTreeInitializeOrderSorter.ExplorationMark>(packageTypeHashes.Count);
			try
			{
				foreach (int num in packageTypeHashes)
				{
					this.SortTreeThrough(num);
				}
			}
			catch (HashException ex)
			{
				throw new DependencyTreeSortFailedException(this.Tree, this.Target, ex);
			}
			this.m_PackageTypeHashExplorationHistory = null;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000332C File Offset: 0x0000152C
		private void RemoveUnprovidedOptionalDependenciesFromTree()
		{
			foreach (List<int> list in this.Tree.PackageTypeHashToComponentTypeHashDependencies.Values)
			{
				this.RemoveUnprovidedOptionalDependencies(list);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000338C File Offset: 0x0000158C
		private void RemoveUnprovidedOptionalDependencies(IList<int> dependencyTypeHashes)
		{
			for (int i = dependencyTypeHashes.Count - 1; i >= 0; i--)
			{
				int num = dependencyTypeHashes[i];
				if (this.Tree.IsOptional(num) && !this.Tree.IsProvided(num))
				{
					dependencyTypeHashes.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000033D8 File Offset: 0x000015D8
		private void SortTreeThrough(int packageTypeHash)
		{
			DependencyTreeInitializeOrderSorter.ExplorationMark explorationMark;
			this.m_PackageTypeHashExplorationHistory.TryGetValue(packageTypeHash, out explorationMark);
			if (explorationMark == DependencyTreeInitializeOrderSorter.ExplorationMark.Viewed)
			{
				throw new CircularDependencyException();
			}
			if (explorationMark != DependencyTreeInitializeOrderSorter.ExplorationMark.Sorted)
			{
				this.MarkPackage(packageTypeHash, DependencyTreeInitializeOrderSorter.ExplorationMark.Viewed);
				IEnumerable<int> dependencyTypeHashesFor = this.GetDependencyTypeHashesFor(packageTypeHash);
				try
				{
					this.SortTreeThrough(dependencyTypeHashesFor);
				}
				catch (DependencyTreeComponentHashException ex)
				{
					throw new DependencyTreePackageHashException(packageTypeHash, string.Format("Component with hash[{0}] threw exception when sorting package[{1}][{2}]", ex.Hash, packageTypeHash, this.Tree.PackageTypeHashToInstance[packageTypeHash].GetType().FullName), ex);
				}
				this.Target.Add(packageTypeHash);
				this.MarkPackage(packageTypeHash, DependencyTreeInitializeOrderSorter.ExplorationMark.Sorted);
				return;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003484 File Offset: 0x00001684
		private void SortTreeThrough(IEnumerable<int> dependencyTypeHashes)
		{
			foreach (int num in dependencyTypeHashes)
			{
				int packageTypeHashFor = this.GetPackageTypeHashFor(num);
				this.SortTreeThrough(packageTypeHashFor);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000034D4 File Offset: 0x000016D4
		private void MarkPackage(int packageTypeHash, DependencyTreeInitializeOrderSorter.ExplorationMark mark)
		{
			this.m_PackageTypeHashExplorationHistory[packageTypeHash] = mark;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000034E3 File Offset: 0x000016E3
		private IReadOnlyCollection<int> GetPackageTypeHashes()
		{
			return this.Tree.PackageTypeHashToInstance.Keys;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000034F8 File Offset: 0x000016F8
		private int GetPackageTypeHashFor(int componentTypeHash)
		{
			int num;
			if (!this.Tree.ComponentTypeHashToPackageTypeHash.TryGetValue(componentTypeHash, out num))
			{
				throw new DependencyTreeComponentHashException(componentTypeHash, string.Format("Component with hash[{0}] does not exist!", componentTypeHash));
			}
			return num;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003534 File Offset: 0x00001734
		private IEnumerable<int> GetDependencyTypeHashesFor(int packageTypeHash)
		{
			List<int> list;
			if (!this.Tree.PackageTypeHashToComponentTypeHashDependencies.TryGetValue(packageTypeHash, out list))
			{
				throw new DependencyTreePackageHashException(packageTypeHash, string.Format("Package with hash[{0}] does not exist!", packageTypeHash));
			}
			return list;
		}

		// Token: 0x04000043 RID: 67
		public readonly DependencyTree Tree;

		// Token: 0x04000044 RID: 68
		public readonly ICollection<int> Target;

		// Token: 0x04000045 RID: 69
		private Dictionary<int, DependencyTreeInitializeOrderSorter.ExplorationMark> m_PackageTypeHashExplorationHistory;

		// Token: 0x02000058 RID: 88
		private enum ExplorationMark
		{
			// Token: 0x04000080 RID: 128
			None,
			// Token: 0x04000081 RID: 129
			Viewed,
			// Token: 0x04000082 RID: 130
			Sorted
		}
	}
}
