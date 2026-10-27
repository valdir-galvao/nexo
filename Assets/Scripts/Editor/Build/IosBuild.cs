using System;
using System.IO;
using System.Linq;
using UnityEditor;
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
            var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
            if (scenes.Length == 0) throw new InvalidOperationException("No enabled scenes are configured.");
            var report = BuildPipeline.BuildPlayer(scenes, output, BuildTarget.iOS, BuildOptions.None);
            if (report.summary.result != BuildResult.Succeeded)
                throw new BuildFailedException($"iOS build failed: {report.summary.result}");
        }
    }
}
