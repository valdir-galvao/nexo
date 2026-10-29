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
            var output = Environment.GetEnvironmentVariable("BUILD_PATH");
            if (string.IsNullOrWhiteSpace(output)) output = Path.Combine("build", "iOS");
            Directory.CreateDirectory(output);
            const string scenePath = "Assets/Scenes/NexoVerticalSlice.unity";
            if (!File.Exists(scenePath)) throw new InvalidOperationException($"Required scene is missing: {scenePath}");
            var scenes = new[] { scenePath };
            PlayerSettings.bundleVersion = "1.0.0";
            PlayerSettings.iOS.buildNumber = "1";
            // Export the Xcode project only. Signing and archiving are separate
            // Apple/Xcode steps and are intentionally absent from this CI job.
            var report = BuildPipeline.BuildPlayer(
                scenes,
                output,
                BuildTarget.iOS,
                BuildOptions.AcceptExternalModificationsToPlayer);
            if (report.summary.result != BuildResult.Succeeded)
                throw new BuildFailedException($"iOS build failed: {report.summary.result}");
        }
    }
}
