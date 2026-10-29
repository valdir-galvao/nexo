using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Nexo.Core;
using Nexo.UI;

namespace Nexo.Tests
{
    public sealed class NexoPlayModeTests
    {
#if UNITY_EDITOR
        [SetUp]
        public void EnsureVerticalSliceSceneIsEnabled()
        {
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene("Assets/Scenes/NexoVerticalSlice.unity", true) };
        }
#endif
        [UnityTest]
        public IEnumerator VerticalSliceSceneInitializesIntoIntro()
        {
            AsyncOperation load = SceneManager.LoadSceneAsync("NexoVerticalSlice");
            yield return load;
            yield return null;

            NexoVerticalSliceBootstrap bootstrap = Object.FindObjectOfType<NexoVerticalSliceBootstrap>();
            Assert.IsNotNull(bootstrap);
            Assert.AreEqual(GameState.Intro, bootstrap.CurrentState);
        }
    }
}
