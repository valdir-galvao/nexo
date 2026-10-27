using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Nexo.Core;
using Nexo.UI;

namespace Nexo.Tests
{
    public sealed class NexoPlayModeTests
    {
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
