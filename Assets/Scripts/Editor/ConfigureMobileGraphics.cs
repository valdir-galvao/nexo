using UnityEditor;
using UnityEngine.Rendering;

namespace Nexo.Editor
{
    /// <summary>Uses OpenGL ES 3 as the deterministic Android/emulator baseline and Metal on iOS.</summary>
    [InitializeOnLoad]
    internal static class ConfigureMobileGraphics
    {
        static ConfigureMobileGraphics()
        {
            EditorApplication.delayCall += Apply;
        }

        private static void Apply()
        {
            // SwiftShader and older AVD images advertise GLES 3 but can emit
            // EGL_BAD_CONFIG while probing newer/Vulkan paths. Keep the smoke
            // test deterministic; Vulkan remains an explicit future profile,
            // not an implicit fallback for this Android target.
            var androidApis = PlayerSettings.GetGraphicsAPIs(BuildTarget.Android);
            if (androidApis == null || androidApis.Length != 1 || androidApis[0] != GraphicsDeviceType.OpenGLES3)
                PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { GraphicsDeviceType.OpenGLES3 });

            PlayerSettings.SetGraphicsAPIs(BuildTarget.iOS, new[] { GraphicsDeviceType.Metal });
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel29;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel35;
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.X86_64;
            AssetDatabase.SaveAssets();
        }
    }
}
