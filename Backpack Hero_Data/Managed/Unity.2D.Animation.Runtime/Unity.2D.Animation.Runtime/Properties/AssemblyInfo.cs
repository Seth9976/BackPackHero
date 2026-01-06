using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Unity.Burst;
using UnityEngine.U2D.Animation;

[assembly: AssemblyVersion("0.0.0.0")]
[assembly: InternalsVisibleTo("Unity.2D.Psdimporter.Tests.EditorTests")]
[assembly: InternalsVisibleTo("Unity.2D.Animation.Tests.EditorTests")]
[assembly: InternalsVisibleTo("Unity.2D.Animation.Tests.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.2D.Animation.Tests.RuntimePerf")]
[assembly: InternalsVisibleTo("Unity.2D.Animation.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.PsdImporter.Editor")]
[assembly: BurstCompiler.StaticTypeReinitAttribute(typeof(BurstedSpriteSkinUtilities.ValidateBoneWeights_000000E1$BurstDirectCall))]
[assembly: SecurityPermission(8, SkipVerification = true)]
