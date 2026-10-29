using UnityEditor;

namespace Nexo.Editor.Build
{
    /// <summary>Ensures the vertical slice is present in the active Editor build scene list in CI and local Editor sessions.</summary>
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
