using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Linq;
public static class CapBuddySetup {
 private const string OutputDir = "Assets/Resources/Characters";
 private const string SourceModel = "Assets/External/FreeTestAssets/CapBuddy/CapBuddy.fbx";
 public static bool LastBuildSucceeded { get; private set; }

 [InitializeOnLoadMethod]
 private static void ConfigureInCiWhenRequested() {
  if (!string.Equals(System.Environment.GetEnvironmentVariable("NEXO_CONFIGURE_CAPBUDDY"), "1", System.StringComparison.Ordinal)) return;
  EditorApplication.delayCall += () => TryBuild(0);
 }

 private static void TryBuild(int attempt) {
  AssetDatabase.Refresh();
  var found = AssetDatabase.FindAssets("CapBuddy t:Model");
  if (found.Any(g => AssetDatabase.GUIDToAssetPath(g).EndsWith("CapBuddy.fbx"))) { Build(); return; }
  if (attempt < 12) EditorApplication.delayCall += () => TryBuild(attempt + 1);
  else Debug.LogError("[NEXO_CAPBUDDY] FBX was not imported after waiting for AssetDatabase.");
 }

 [MenuItem("Tools/CapBuddy/Create Prefab and Animator")]
 public static void Build() {
  LastBuildSucceeded = false;
  Debug.Log("[NEXO_CAPBUDDY] BEGIN setup");
  AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
  if (AssetDatabase.LoadAssetAtPath<GameObject>(SourceModel) == null && System.IO.File.Exists(SourceModel))
   AssetDatabase.ImportAsset(SourceModel, ImportAssetOptions.ForceUpdate);
  var guids=AssetDatabase.FindAssets("CapBuddy t:Model");
  var path=guids.Select(AssetDatabase.GUIDToAssetPath).FirstOrDefault(p=>p.EndsWith("CapBuddy.fbx"));
  if(path==null){ Debug.LogError("[NEXO_CAPBUDDY] FAIL source FBX not found: " + SourceModel); return; }
  var importer=(ModelImporter)AssetImporter.GetAtPath(path);
  if (importer == null) { Debug.LogError("[NEXO_CAPBUDDY] FAIL ModelImporter unavailable: " + path); return; }
  importer.animationType=ModelImporterAnimationType.Generic; importer.importAnimation=true;
  importer.SaveAndReimport();
  AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
  System.IO.Directory.CreateDirectory(OutputDir);
  var controllerPath=OutputDir+"/CapBuddy.controller";
  if (AssetDatabase.LoadAssetAtPath<Object>(controllerPath) != null)
   AssetDatabase.DeleteAsset(controllerPath);
  var ctrl=AnimatorController.CreateAnimatorControllerAtPath(controllerPath);
  if (ctrl == null) { Debug.LogError("[NEXO_CAPBUDDY] FAIL controller creation: " + controllerPath); return; }
  ctrl.AddParameter("Speed",AnimatorControllerParameterType.Float);
  ctrl.AddParameter("Investigate",AnimatorControllerParameterType.Trigger);
  ctrl.AddParameter("Success",AnimatorControllerParameterType.Trigger);
  var sm=ctrl.layers[0].stateMachine;
  string[] names={"Idle","Walk","Investigate","Success"};
  var clips=AssetDatabase.LoadAllAssetsAtPath(path).OfType<AnimationClip>().Where(c=>!c.name.StartsWith("__preview__")).ToArray();
  var states=names.ToDictionary(n=>n,n=>sm.AddState(n));
  bool clipsOk = true;
  foreach(var n in names){
   states[n].motion=clips.FirstOrDefault(c=>c.name.EndsWith(n)||c.name.Contains(n));
   if(states[n].motion==null){ clipsOk = false; Debug.LogError("[NEXO_CAPBUDDY] FAIL clip unmapped: " + n); }
   else Debug.Log("[NEXO_CAPBUDDY] clip mapped state=" + n + " asset=" + states[n].motion.name);
  }
  if (!clipsOk) { AssetDatabase.DeleteAsset(controllerPath); return; }
  sm.defaultState=states["Idle"];
  var walk=states["Idle"].AddTransition(states["Walk"]);walk.hasExitTime=false;walk.duration=.15f;walk.AddCondition(AnimatorConditionMode.Greater,.1f,"Speed");
  var idle=states["Walk"].AddTransition(states["Idle"]);idle.hasExitTime=false;idle.duration=.15f;idle.AddCondition(AnimatorConditionMode.Less,.1f,"Speed");
  foreach(var n in new[]{"Investigate","Success"}){var t=sm.AddAnyStateTransition(states[n]);t.hasExitTime=false;t.canTransitionToSelf=false;t.duration=.15f;t.AddCondition(AnimatorConditionMode.If,0,n);var back=states[n].AddTransition(states["Idle"]);back.hasExitTime=true;back.exitTime=1;back.duration=.15f;}
  var model=AssetDatabase.LoadAssetAtPath<GameObject>(path);
  if (model == null) { Debug.LogError("[NEXO_CAPBUDDY] FAIL model LoadAssetAtPath returned null: " + path); return; }
  var go=(GameObject)PrefabUtility.InstantiatePrefab(model);
  if (go == null) { Debug.LogError("[NEXO_CAPBUDDY] FAIL PrefabUtility.InstantiatePrefab returned null"); return; }
  var anim=go.GetComponentInChildren<Animator>(true);
  if (anim == null) anim=go.AddComponent<Animator>();
  if (anim == null) { Object.DestroyImmediate(go); Debug.LogError("[NEXO_CAPBUDDY] FAIL Animator component could not be created"); return; }
  anim.runtimeAnimatorController=ctrl;anim.applyRootMotion=false;
  var prefab=PrefabUtility.SaveAsPrefabAsset(go,OutputDir+"/CapBuddy.prefab");Object.DestroyImmediate(go);AssetDatabase.SaveAssets();AssetDatabase.Refresh();
  var savedPrefab=AssetDatabase.LoadAssetAtPath<GameObject>(OutputDir+"/CapBuddy.prefab");
  var savedController=AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);
  if (prefab == null || savedPrefab == null || savedController == null) { Debug.LogError("[NEXO_CAPBUDDY] FAIL output validation prefab=" + (savedPrefab != null) + " controller=" + (savedController != null)); return; }
  LastBuildSucceeded = true;
  Debug.Log("[NEXO_CAPBUDDY] SUCCESS prefab=" + OutputDir + "/CapBuddy.prefab controller=" + controllerPath + " clips=Idle,Walk,Investigate,Success");
 }

 public static void EnsureGeneratedForBuild() {
  Build();
 }
}
