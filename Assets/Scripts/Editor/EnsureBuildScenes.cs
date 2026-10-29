using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Build.Profile;

namespace Nexo.Editor.Build
{
    /// <summary>Registers the vertical slice in the active Unity 6 Build Profile and legacy settings.</summary>
    [InitializeOnLoad]
    internal static class EnsureBuildScenes
    {
        private const string ScenePath = "Assets/Scenes/NexoVerticalSlice.unity";

        static EnsureBuildScenes()
        {
            EnsureSceneIsEnabled();
        }

        internal static void EnsureSceneIsEnabled()
        {
            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath) == null)
                return;

            EnsureActiveBuildProfile();
            EnsureLegacyBuildSettings();
        }

        private static void EnsureActiveBuildProfile()
        {
            BuildProfile profile = BuildProfile.GetActiveBuildProfile();
            if (profile == null)
                return;

            PropertyInfo scenesProperty = profile.GetType().GetProperty("scenes", BindingFlags.Instance | BindingFlags.Public);
            if (scenesProperty == null || !scenesProperty.CanRead || !scenesProperty.CanWrite)
                return;

            var current = scenesProperty.GetValue(profile) as EditorBuildSettingsScene[] ?? Array.Empty<EditorBuildSettingsScene>();
            for (int i = 0; i < current.Length; i++)
            {
                if (current[i].path == ScenePath)
                {
                    current[i].enabled = true;
                    scenesProperty.SetValue(profile, current);
                    EditorUtility.SetDirty(profile);
                    AssetDatabase.SaveAssets();
                    return;
                }
            }

            var updated = new EditorBuildSettingsScene[current.Length + 1];
            current.CopyTo(updated, 0);
            updated[updated.Length - 1] = new EditorBuildSettingsScene(ScenePath, true);
            scenesProperty.SetValue(profile, updated);
            EditorUtility.SetDirty(profile);
            AssetDatabase.SaveAssets();
        }

        private static void EnsureLegacyBuildSettings()
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i].path == ScenePath)
                {
                    scenes[i].enabled = true;
                    EditorBuildSettings.scenes = scenes;
                    return;
                }
            }

            var updated = new EditorBuildSettingsScene[scenes.Length + 1];
            scenes.CopyTo(updated, 0);
            updated[updated.Length - 1] = new EditorBuildSettingsScene(ScenePath, true);
            EditorBuildSettings.scenes = updated;
        }
    }
}