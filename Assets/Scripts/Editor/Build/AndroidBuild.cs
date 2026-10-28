using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Nexo.Editor.Build
{
    public static class AndroidBuild
    {
        public static void PerformBuild()
        {
            var output = Environment.GetEnvironmentVariable("BUILD_PATH");
            if (string.IsNullOrWhiteSpace(output)) output = Path.Combine("build", "Android", "Nexo.apk");
            Directory.CreateDirectory(Path.GetDirectoryName(output)!);
            const string scenePath = "Assets/Scenes/NexoVerticalSlice.unity";
            if (!File.Exists(scenePath)) throw new InvalidOperationException($"Required scene is missing: {scenePath}");
            var scenes = new[] { scenePath };
            var report = BuildPipeline.BuildPlayer(scenes, output, BuildTarget.Android, BuildOptions.None);
            if (report.summary.result != BuildResult.Succeeded)
                throw new BuildFailedException($"Android build failed: {report.summary.result}");
        }
    }
}
