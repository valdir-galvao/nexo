using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Nexo.Editor
{
    /// <summary>
    /// Keeps the shaders required by the mobile vertical slice in Unity's
    /// Always Included Shaders list. Shader.Find is used instead of hard-coded
    /// GUIDs because URP and TextMeshPro shader GUIDs vary by package version.
    /// </summary>
    [InitializeOnLoad]
    internal static class EnsureIncludedShaders
    {
        private static readonly string[] RequiredShaderNames =
        {
            "Universal Render Pipeline/Lit",
            "Universal Render Pipeline/Unlit",
            "UI/Default",
            "TextMeshPro/Distance Field"
        };

        static EnsureIncludedShaders()
        {
            EditorApplication.delayCall += Ensure;
        }

        private static void Ensure()
        {
            var included = new List<Shader>(GraphicsSettings.alwaysIncludedShaders ?? new Shader[0]);
            var changed = false;
            foreach (var shaderName in RequiredShaderNames)
            {
                var shader = Shader.Find(shaderName);
                if (shader == null)
                {
                    Debug.LogWarning("NEXO shader not found during editor setup: " + shaderName);
                    continue;
                }

                if (!included.Contains(shader))
                {
                    included.Add(shader);
                    changed = true;
                }
            }

            if (!changed) return;
            GraphicsSettings.alwaysIncludedShaders = included.ToArray();
            AssetDatabase.SaveAssets();
            Debug.Log("NEXO mobile shaders registered in Always Included Shaders.");
        }
    }
}
