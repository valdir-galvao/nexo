using System.Collections;
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
        [UnityTest]
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

            NexoVerticalSliceBootstrap bootstrap = Object.FindObjectOfType<NexoVerticalSliceBootstrap>();
            Assert.IsNotNull(bootstrap);
            Assert.AreEqual(GameState.Intro, bootstrap.CurrentState);
        }
    }
}
