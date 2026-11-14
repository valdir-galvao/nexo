using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Nexo.Editor.Build
{
    public static class IosBuild
    {
        public static void PerformBuild()
        {
            // Keep iOS generation identical to Android so both artifacts audit
            // the same prefab/controller and clip mapping.
            CapBuddySetup.Build();
            if (!CapBuddySetup.LastBuildSucceeded)
                throw new BuildFailedException("[NEXO_CAPBUDDY] CapBuddy prefab/controller generation failed; see setup logs.");
            var output = Environment.GetEnvironmentVariable("BUILD_PATH");
            if (string.IsNullOrWhiteSpace(output)) output = Path.Combine("build", "iOS");
            Directory.CreateDirectory(output);
            const string scenePath = "Assets/Scenes/NexoVerticalSlice.unity";
            if (!File.Exists(scenePath)) throw new InvalidOperationException($"Required scene is missing: {scenePath}");
            var scenes = new[] { scenePath };
            PlayerSettings.bundleVersion = "1.0.0";
            PlayerSettings.iOS.buildNumber = "1";
            // Export a fresh Xcode project. AcceptExternalModificationsToPlayer
            // is append-only and fails on a clean runner with
            // "The build cannot be appended"; signing/archiving are separate
            // Apple/Xcode steps and are intentionally absent from this CI job.
            var report = BuildPipeline.BuildPlayer(
                scenes,
                output,
                BuildTarget.iOS,
                BuildOptions.None);
            if (report.summary.result != BuildResult.Succeeded)
                throw new BuildFailedException($"iOS build failed: {report.summary.result}");

            // GameCI v6 validates custom build methods by this exact marker.
            Console.WriteLine("Build succeeded!");
        }
    }
}
