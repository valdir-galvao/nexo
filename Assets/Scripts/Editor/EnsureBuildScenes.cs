using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Nexo.Editor.Build
{
    [InitializeOnLoad]
    internal static class EnsureBuildScenes
    {
        private const string ScenePath = "Assets/Scenes/NexoVerticalSlice.unity";

        static EnsureBuildScenes() { EditorApplication.delayCall += EnsureSceneIsEnabled; }

        private static void EnsureSceneIsEnabled()
        {
            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath) == null) return;
            var legacy = EditorBuildSettings.scenes;
            for (var i = 0; i < legacy.Length; i++)
                if (legacy[i].path == ScenePath) { legacy[i].enabled = true; EditorBuildSettings.scenes = legacy; break; }

            var profileType = Type.GetType("UnityEditor.Build.Profile.BuildProfile, UnityEditor");
            var getter = profileType == null ? null : profileType.GetMethod("GetActiveBuildProfile", BindingFlags.Public | BindingFlags.Static);
            var profile = getter == null ? null : getter.Invoke(null, null);
            var property = profile == null ? null : profile.GetType().GetProperty("scenes", BindingFlags.Public | BindingFlags.Instance);
            if (property == null || !property.CanRead || !property.CanWrite) return;
            var current = property.GetValue(profile) as EditorBuildSettingsScene[] ?? Array.Empty<EditorBuildSettingsScene>();
            for (var i = 0; i < current.Length; i++)
                if (current[i].path == ScenePath) { current[i].enabled = true; property.SetValue(profile, current); EditorUtility.SetDirty((UnityEngine.Object)profile); AssetDatabase.SaveAssets(); return; }
            var updated = new EditorBuildSettingsScene[current.Length + 1];
            current.CopyTo(updated, 0);
            updated[updated.Length - 1] = new EditorBuildSettingsScene(ScenePath, true);
            property.SetValue(profile, updated);
            EditorUtility.SetDirty((UnityEngine.Object)profile);
            AssetDatabase.SaveAssets();
        }
    }
}
