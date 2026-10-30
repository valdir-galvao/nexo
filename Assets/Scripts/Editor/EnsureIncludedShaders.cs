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
            "Unlit/Color",
            "UI/Default",
            "TextMeshPro/Distance Field"
        };
        private static int retryCount;

        static EnsureIncludedShaders()
        {
            EditorApplication.delayCall += Ensure;
        }

        private static void Ensure()
        {
            var changed = false;
            var settingsAsset = AssetDatabase.LoadAssetAtPath<Object>("ProjectSettings/GraphicsSettings.asset");
            SerializedObject serialized = settingsAsset == null ? null : new SerializedObject(settingsAsset);
            var included = serialized == null ? null : serialized.FindProperty("m_AlwaysIncludedShaders");
            if (settingsAsset != null && included == null)
                Debug.LogWarning("NEXO GraphicsSettings nao possui m_AlwaysIncludedShaders; material sera resolvido independentemente.");

            foreach (var shaderName in RequiredShaderNames)
            {
                var shader = Shader.Find(shaderName);
                if (shader == null)
                {
                    Debug.LogWarning("NEXO shader not found during editor setup: " + shaderName);
                    continue;
                }
                if (included == null) continue;

                var alreadyIncluded = false;
                for (var i = 0; i < included.arraySize; i++)
                {
                    if (included.GetArrayElementAtIndex(i).objectReferenceValue == shader)
                    {
                        alreadyIncluded = true;
                        break;
                    }
                }

                if (!alreadyIncluded)
                {
                    included.InsertArrayElementAtIndex(included.arraySize);
                    included.GetArrayElementAtIndex(included.arraySize - 1).objectReferenceValue = shader;
                    changed = true;
                }
            }

            var gameplayMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/Materials/URP_Default_Gameplay.mat");
            var urpActive = GraphicsSettings.currentRenderPipeline != null;
            var gameplayShader = urpActive ? Shader.Find("Universal Render Pipeline/Unlit") : Shader.Find("Unlit/Color");
            if (gameplayShader == null) gameplayShader = urpActive ? Shader.Find("Unlit/Color") : Shader.Find("Universal Render Pipeline/Unlit");
            if (gameplayShader == null) gameplayShader = Shader.Find("UI/Default");
            if (gameplayMaterial != null && gameplayShader != null && gameplayMaterial.shader != gameplayShader)
            {
                gameplayMaterial.shader = gameplayShader;
                EditorUtility.SetDirty(gameplayMaterial);
                changed = true;
            }

            if (!changed) return;
            if (serialized != null) serialized.ApplyModifiedPropertiesWithoutUndo();
            AssetDatabase.SaveAssets();
            Debug.Log("NEXO mobile shaders registered in Always Included Shaders.");
        }
    }
}
