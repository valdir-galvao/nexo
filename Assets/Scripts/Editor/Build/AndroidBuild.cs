using System;
using System.IO;
using System.Linq;
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
            var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
            if (scenes.Length == 0) throw new InvalidOperationException("No enabled scenes are configured.");
            var report = BuildPipeline.BuildPlayer(scenes, output, BuildTarget.Android, BuildOptions.None);
            if (report.summary.result != BuildResult.Succeeded)
                throw new BuildFailedException($"Android build failed: {report.summary.result}");
        }
    }
}
