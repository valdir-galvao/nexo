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
            // Equivalent to `-executeMethod CapBuddySetup.Build`, performed
            // in-process before the actual Android BuildPipeline invocation.
            CapBuddySetup.Build();
            if (!CapBuddySetup.LastBuildSucceeded)
                throw new BuildFailedException("[NEXO_CAPBUDDY] CapBuddy prefab/controller generation failed; see setup logs.");
            var architecture = Environment.GetEnvironmentVariable("NEXO_ANDROID_ARCH");
            PlayerSettings.Android.targetArchitectures = architecture switch
            {
                "ARM64" => AndroidArchitecture.ARM64,
                "X86_64" => AndroidArchitecture.X86_64,
                _ => AndroidArchitecture.ARM64 | AndroidArchitecture.X86_64
            };
            // Release builds must remain IL2CPP. The former CI fast-build
            // override produced a Mono APK that did not satisfy the product
            // architecture/stripping contract.
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            var output = Environment.GetEnvironmentVariable("BUILD_PATH");
            if (string.IsNullOrWhiteSpace(output)) output = Path.Combine("build", "Android", "Nexo.apk");
            // GameCI may pass buildsPath-derived directories (for example
            // build/Android/Android) through BUILD_PATH. Normalize those values
            // to an APK file before invoking BuildPipeline.
            if (!output.EndsWith(".apk", StringComparison.OrdinalIgnoreCase))
                output = Path.Combine(output, "Nexo.apk");
            Directory.CreateDirectory(Path.GetDirectoryName(output)!);
            const string scenePath = "Assets/Scenes/NexoVerticalSlice.unity";
            if (!File.Exists(scenePath)) throw new InvalidOperationException($"Required scene is missing: {scenePath}");
            var scenes = new[] { scenePath };
            var buildOptions = BuildOptions.None;
            if (string.Equals(Environment.GetEnvironmentVariable("NEXO_DEVELOPMENT_BUILD"), "1", StringComparison.OrdinalIgnoreCase))
                buildOptions = BuildOptions.Development | BuildOptions.AllowDebugging;
            var report = BuildPipeline.BuildPlayer(scenes, output, BuildTarget.Android, buildOptions);
            if (report.summary.result != BuildResult.Succeeded)
                throw new BuildFailedException($"Android build failed: {report.summary.result}");

            // GameCI v6 validates custom build methods by this exact marker.
            // Unity's BuildPipeline summary alone is not sufficient for the
            // wrapper's post-build validation step.
            Console.WriteLine("Build succeeded!");
        }
    }
}
