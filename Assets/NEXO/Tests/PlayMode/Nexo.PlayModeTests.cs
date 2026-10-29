using System.Collections;
using System.IO;
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
#if UNITY_EDITOR
            AsyncOperation load = EditorSceneManager.LoadSceneAsyncInPlayMode(
                "Assets/Scenes/NexoVerticalSlice.unity",
                new LoadSceneParameters(LoadSceneMode.Single));
#else
            AsyncOperation load = SceneManager.LoadSceneAsync("NexoVerticalSlice");
#endif
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
            Assert.AreEqual(GameState.Intro, bootstrap.CurrentState);
        }
    }
}
