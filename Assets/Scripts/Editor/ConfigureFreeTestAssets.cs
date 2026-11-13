#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Nexo.EditorTools
{
    /// <summary>
    /// One-shot setup for the CC0 fixtures under Assets/External/FreeTestAssets.
    /// Run from Unity after the FBX importer has completed; it is intentionally
    /// not invoked automatically by builds.
    /// </summary>
    public static class ConfigureFreeTestAssets
    {
        private const string MannequinPath = "Assets/External/FreeTestAssets/Kinemiq/KinemIQ_Mannequin.fbx";
        private const string AnimationRoot = "Assets/External/FreeTestAssets/Kinemiq/FBX";
        // Runtime assets must live below Resources so the bootstrap can load
        // the authored character in a player build. The previous location
        // produced a prefab in the project but never connected it to runtime.
        private const string OutputRoot = "Assets/Resources/Characters";

        [InitializeOnLoadMethod]
        private static void ConfigureInCiWhenRequested()
        {
            if (!string.Equals(System.Environment.GetEnvironmentVariable("NEXO_CONFIGURE_FREE_ASSETS"), "1", System.StringComparison.Ordinal)) return;
            if (SessionState.GetBool("NEXO_FREE_ASSETS_CONFIGURED", false)) return;
            SessionState.SetBool("NEXO_FREE_ASSETS_CONFIGURED", true);
            EditorApplication.delayCall += Configure;
        }

        [MenuItem("NEXO/Free Test Assets/Configure Humanoid Character")]
        public static void Configure()
        {
            if (!File.Exists(MannequinPath))
            {
                Debug.LogError("[NEXO_ASSETS] KinemIQ mannequin FBX not found: " + MannequinPath);
                return;
            }

            AssetDatabase.ImportAsset(MannequinPath, ImportAssetOptions.ForceUpdate);
            var importer = AssetImporter.GetAtPath(MannequinPath) as ModelImporter;
            if (importer == null)
            {
                Debug.LogError("[NEXO_ASSETS] ModelImporter unavailable for mannequin.");
                return;
            }

            importer.animationType = ModelImporterAnimationType.Human;
            importer.avatarSetup = ModelImporterAvatarSetup.CreateFromThisModel;
            importer.importAnimation = true;
            importer.SaveAndReimport();

            Directory.CreateDirectory(OutputRoot);
            AssetDatabase.Refresh();

            var model = AssetDatabase.LoadAssetAtPath<GameObject>(MannequinPath);
            var avatar = AssetDatabase.LoadAllAssetsAtPath(MannequinPath).OfType<Avatar>().FirstOrDefault();
            if (model == null || avatar == null)
            {
                Debug.LogError("[NEXO_ASSETS] Imported mannequin or Avatar is missing.");
                return;
            }

            var controllerPath = OutputRoot + "/NexoExplorer.controller";
            var controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);
            if (controller == null)
            {
                controller = AnimatorController.CreateAnimatorControllerAtPath(controllerPath);
            }

            var layer = controller.layers[0];
            var root = layer.stateMachine;
            AddState(root, "Idle", FindClip("Idle_relaxed.fbx"), new Vector3(250f, 100f, 0f));
            AddState(root, "Walk", FindClip("Walk_forwards.fbx"), new Vector3(500f, 100f, 0f));
            AddState(root, "Investigate", FindClip("Action_interact.fbx"), new Vector3(250f, 250f, 0f));
            AddState(root, "Success", FindClip("Action_dance.fbx"), new Vector3(500f, 250f, 0f));
            EditorUtility.SetDirty(controller);
            AssetDatabase.SaveAssets();

            var instance = Object.Instantiate(model);
            instance.name = "NexoExplorer_FreeTest";
            var animator = instance.GetComponent<Animator>() ?? instance.AddComponent<Animator>();
            animator.avatar = avatar;
            animator.runtimeAnimatorController = controller;
            var prefabPath = OutputRoot + "/NexoExplorer_FreeTest.prefab";
            PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
            Object.DestroyImmediate(instance);

            Debug.Log("[NEXO_ASSETS] Configured humanoid avatar, AnimatorController and prefab: " + prefabPath);
        }

        private static AnimationClip FindClip(string fileName)
        {
            var path = AnimationRoot + "/" + fileName;
            return AssetDatabase.LoadAllAssetsAtPath(path).OfType<AnimationClip>().FirstOrDefault(c => !c.name.StartsWith("__preview__"));
        }

        private static void AddState(AnimatorStateMachine machine, string name, AnimationClip clip, Vector3 position)
        {
            if (machine == null || clip == null || machine.states.Any(s => s.state.name == name)) return;
            var state = machine.AddState(name, position);
            state.motion = clip;
            state.writeDefaultValues = true;
        }
    }
}
#endif
