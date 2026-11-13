using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Linq;
public static class CapBuddySetup {
 private const string OutputDir = "Assets/Resources/Characters";

 [InitializeOnLoadMethod]
 private static void ConfigureInCiWhenRequested() {
  if (!string.Equals(System.Environment.GetEnvironmentVariable("NEXO_CONFIGURE_CAPBUDDY"), "1", System.StringComparison.Ordinal)) return;
  EditorApplication.delayCall += Build;
 }

 [MenuItem("Tools/CapBuddy/Create Prefab and Animator")]
 public static void Build() {
  var guids=AssetDatabase.FindAssets("CapBuddy t:Model");
  var path=guids.Select(AssetDatabase.GUIDToAssetPath).FirstOrDefault(p=>p.EndsWith("CapBuddy.fbx"));
  if(path==null){ Debug.LogError("Copy CapBuddy.fbx and Textures into Assets/CapBuddy first."); return; }
  var importer=(ModelImporter)AssetImporter.GetAtPath(path);
  importer.animationType=ModelImporterAnimationType.Generic; importer.importAnimation=true;
  importer.SaveAndReimport();
  System.IO.Directory.CreateDirectory(OutputDir);
  var controllerPath=OutputDir+"/CapBuddy.controller";
  var ctrl=AnimatorController.CreateAnimatorControllerAtPath(controllerPath);
  ctrl.AddParameter("Speed",AnimatorControllerParameterType.Float);
  ctrl.AddParameter("Investigate",AnimatorControllerParameterType.Trigger);
  ctrl.AddParameter("Success",AnimatorControllerParameterType.Trigger);
  var sm=ctrl.layers[0].stateMachine;
  string[] names={"Idle","Walk","Investigate","Success"};
  var clips=AssetDatabase.LoadAllAssetsAtPath(path).OfType<AnimationClip>().Where(c=>!c.name.StartsWith("__preview__")).ToArray();
  var states=names.ToDictionary(n=>n,n=>sm.AddState(n));
  foreach(var n in names){states[n].motion=clips.FirstOrDefault(c=>c.name.EndsWith(n)||c.name.Contains(n)); if(states[n].motion==null)Debug.LogWarning("Missing clip: "+n);}
  sm.defaultState=states["Idle"];
  var walk=states["Idle"].AddTransition(states["Walk"]);walk.hasExitTime=false;walk.duration=.15f;walk.AddCondition(AnimatorConditionMode.Greater,.1f,"Speed");
  var idle=states["Walk"].AddTransition(states["Idle"]);idle.hasExitTime=false;idle.duration=.15f;idle.AddCondition(AnimatorConditionMode.Less,.1f,"Speed");
  foreach(var n in new[]{"Investigate","Success"}){var t=sm.AddAnyStateTransition(states[n]);t.hasExitTime=false;t.canTransitionToSelf=false;t.duration=.15f;t.AddCondition(AnimatorConditionMode.If,0,n);var back=states[n].AddTransition(states["Idle"]);back.hasExitTime=true;back.exitTime=1;back.duration=.15f;}
  var go=(GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(path));
  var anim=go.GetComponent<Animator>()??go.AddComponent<Animator>();anim.runtimeAnimatorController=ctrl;anim.applyRootMotion=false;
  PrefabUtility.SaveAsPrefabAsset(go,OutputDir+"/CapBuddy.prefab");Object.DestroyImmediate(go);AssetDatabase.SaveAssets();
  Debug.Log("[NEXO_CAPBUDDY] prefab/controller generated at Resources/Characters");
 }
}
