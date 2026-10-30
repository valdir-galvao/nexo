using UnityEditor;
using UnityEngine;

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
        private static int retryCount;

        static EnsureIncludedShaders()
        {
            EditorApplication.delayCall += Ensure;
        }

        private static void Ensure()
        {
            var settingsAsset = AssetDatabase.LoadAssetAtPath<Object>("ProjectSettings/GraphicsSettings.asset");
            if (settingsAsset == null)
            {
                if (retryCount++ < 20) EditorApplication.delayCall += Ensure;
                else Debug.LogWarning("NEXO GraphicsSettings.asset was not available after editor retries; shader inclusion remains pending.");
                return;
            }

            var serialized = new SerializedObject(settingsAsset);
            var included = serialized.FindProperty("m_AlwaysIncludedShaders");
            if (included == null)
            {
                Debug.LogError("NEXO GraphicsSettings has no m_AlwaysIncludedShaders property; URP shader inclusion cannot be verified.");
                return;
            }

            var changed = false;
            foreach (var shaderName in RequiredShaderNames)
            {
                var shader = Shader.Find(shaderName);
                if (shader == null)
                {
                    Debug.LogWarning("NEXO shader not found during editor setup: " + shaderName);
                    continue;
                }

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

            if (!changed) return;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            AssetDatabase.SaveAssets();
            Debug.Log("NEXO mobile shaders registered in Always Included Shaders.");
        }
    }
}
