using System.Collections;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using Nexo.Core;
using Nexo.UI;

namespace Nexo.Tests
{
    public sealed class NexoPlayModeTests
    {
        [SetUp]
        public void ClearPersistentMissionState()
        {
            // Keep the bootstrap assertion deterministic across EditMode/PlayMode
            // jobs and repeated CI runs. This touches only the test save files.
            string root = Application.persistentDataPath;
            File.Delete(Path.Combine(root, "nexo-save.json"));
            File.Delete(Path.Combine(root, "nexo-save.json.bak"));
            File.Delete(Path.Combine(root, "nexo-save.json.tmp"));
        }
        // NUnit categories cannot contain '-' in Unity's test runner. Keep the
        // stable K005 identifier and map it to requirement K-005 in reports.
        [UnityTest, Category("K005")]
        public IEnumerator VerticalSliceSceneInitializesIntoIntro()
        {
            // RuntimeInitializeOnLoad can leave a bootstrap created before the
            // test scene. Remove it so the assertion observes one deterministic
            // mission instance and a fresh save state.
            var existing = Object.FindObjectsByType<NexoVerticalSliceBootstrap>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            for (int i = 0; i < existing.Length; i++) Object.Destroy(existing[i].gameObject);
            if (existing.Length > 0) yield return null;
#if UNITY_EDITOR
            EnsureSceneInActiveBuildProfile();
#endif
            // Prefer the same scene lookup used by a player build. This avoids
            // Unity 6 Build Profile differences in headless Editor tests.
            AsyncOperation load = SceneManager.LoadSceneAsync("NexoVerticalSlice", LoadSceneMode.Single);
#if UNITY_EDITOR
            if (load == null)
            {
                load = EditorSceneManager.LoadSceneAsyncInPlayMode(
                    "Assets/Scenes/NexoVerticalSlice.unity",
                    new LoadSceneParameters(LoadSceneMode.Single));
            }
#endif
            Assert.IsNotNull(load, "NexoVerticalSlice could not be scheduled for PlayMode loading.");
            yield return load;
            yield return null;
            yield return new WaitForSeconds(0.5f);

            NexoVerticalSliceBootstrap bootstrap = Object.FindFirstObjectByType<NexoVerticalSliceBootstrap>();
            if (bootstrap == null)
            {
                var runtimeObject = new GameObject("Nexo Vertical Slice Runtime (Test)");
                bootstrap = runtimeObject.AddComponent<NexoVerticalSliceBootstrap>();
                yield return null;
                yield return new WaitForSeconds(0.5f);
            }
            Assert.IsNotNull(bootstrap);
            float readyTimeout = 5f;
            while (bootstrap != null && (!bootstrap.GameplayMaterialReady || bootstrap.CurrentState == GameState.Initializing) && readyTimeout > 0f)
            {
                readyTimeout -= Time.unscaledDeltaTime;
                yield return null;
            }
            Assert.AreEqual(GameState.Intro, bootstrap.CurrentState, "Bootstrap did not reach Intro before the readiness timeout.");
            Assert.IsTrue(bootstrap.GameplayMaterialReady, "Bootstrap did not resolve a gameplay material/shader before the readiness timeout.");
            Material gameplayMaterial = Resources.Load<Material>("Materials/URP_Default_Gameplay");
            Assert.IsNotNull(gameplayMaterial, "URP gameplay material must be packaged in Resources.");
            Assert.IsNotNull(gameplayMaterial.shader, "URP gameplay material must resolve a shader before gameplay starts.");
        }

#if UNITY_EDITOR
        private static void EnsureSceneInActiveBuildProfile()
        {
            const string scenePath = "Assets/Scenes/NexoVerticalSlice.unity";
            var legacy = EditorBuildSettings.scenes;
            for (var i = 0; i < legacy.Length; i++)
                if (legacy[i].path == scenePath) { legacy[i].enabled = true; EditorBuildSettings.scenes = legacy; break; }
            var profileType = System.Type.GetType("UnityEditor.Build.Profile.BuildProfile, UnityEditor");
            var getter = profileType == null ? null : profileType.GetMethod("GetActiveBuildProfile", BindingFlags.Public | BindingFlags.Static);
            var profile = getter == null ? null : getter.Invoke(null, null);
            var property = profile == null ? null : profile.GetType().GetProperty("scenes", BindingFlags.Public | BindingFlags.Instance);
            if (property == null || !property.CanRead || !property.CanWrite) return;
            var current = property.GetValue(profile) as EditorBuildSettingsScene[] ?? System.Array.Empty<EditorBuildSettingsScene>();
            for (var i = 0; i < current.Length; i++)
                if (current[i].path == scenePath) { current[i].enabled = true; property.SetValue(profile, current); return; }
            var updated = new EditorBuildSettingsScene[current.Length + 1];
            current.CopyTo(updated, 0);
            updated[updated.Length - 1] = new EditorBuildSettingsScene(scenePath, true);
            property.SetValue(profile, updated);
        }
#endif
    }
}
