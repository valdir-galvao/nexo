using System.Collections.Generic;
using UnityEngine;

namespace Nexo.UI
{
    /// <summary>
    /// Presentation layer for the vertical slice. It adds authored, mobile-safe
    /// shapes around the gameplay blockout without depending on external assets,
    /// physics primitives, URP-only shaders, or editor-only APIs.
    /// </summary>
    public sealed class NexoScenePresentation : MonoBehaviour
    {
        private readonly List<Transform> animatedDecor = new List<Transform>();
        private Transform explorer;
        private Transform explorerHead;
        private Transform explorerBackpack;
        private Transform leftLeg;
        private Transform rightLeg;
        private Transform portalRing;
        private Transform waterRibbon;
        private ParticleSystem sparkleFx;
        private Light portalLight;
        private Material visualMaterial;
        private float elapsed;
        private bool initialized;

        public void Initialize()
        {
            if (initialized) return;
            initialized = true;
            visualMaterial = CreateMaterial(new Color(0.16f, 0.64f, 0.95f, 1f));
            BuildEnvironmentAccents();
            BuildExplorerPresentation();
            BuildPortalPresentation();
            BuildWaterPresentation();
        }

        private void Update()
        {
            if (!initialized) return;
            elapsed += Time.unscaledDeltaTime;
            AnimateExplorer();
            AnimateEnvironment();
        }

        private void BuildEnvironmentAccents()
        {
            CreatePyramid("Nexo Mountain A", new Vector3(-7f, 0f, 5f), new Vector3(2.2f, 3.8f, 2.2f), new Color(0.12f, 0.28f, 0.38f, 1f));
            CreatePyramid("Nexo Mountain B", new Vector3(7f, 0f, 6f), new Vector3(2.8f, 4.8f, 2.8f), new Color(0.16f, 0.34f, 0.40f, 1f));
            CreatePyramid("Nexo Cliff", new Vector3(-7f, 0f, -5f), new Vector3(2.5f, 2.4f, 2.0f), new Color(0.25f, 0.34f, 0.42f, 1f));
            for (int i = 0; i < 5; i++)
            {
                float angle = i * Mathf.PI * 0.4f;
                Vector3 position = new Vector3(Mathf.Cos(angle) * 7.5f, 0.1f, Mathf.Sin(angle) * 6.2f + 1f);
                Transform crystal = CreateCrystal("Nexo Guide Crystal " + i, position, 0.55f + i * 0.08f, new Color(0.20f, 0.76f, 0.94f, 1f));
                if (crystal != null) animatedDecor.Add(crystal);
            }
        }

        private void BuildExplorerPresentation()
        {
            GameObject existing = GameObject.Find("Explorer");
            if (existing == null) return;
            explorer = existing.transform;
            explorerHead = FindChild(explorer, "Explorer Head");
            explorerBackpack = FindChild(explorer, "Explorer Backpack");
            leftLeg = FindChild(explorer, "Explorer Left Leg");
            rightLeg = FindChild(explorer, "Explorer Right Leg");
            Transform badge = CreateBadge(explorer, new Vector3(0f, 0.22f, -0.37f));
            if (badge != null) animatedDecor.Add(badge);
        }

        private void BuildPortalPresentation()
        {
            GameObject portal = GameObject.Find("GreenDoor");
            if (portal == null) portal = GameObject.Find("EnergyPortal");
            if (portal == null) portal = GameObject.Find("LogicPortal");
            if (portal == null) return;
            portalRing = CreateTorus("Nexo Portal Energy Ring", portal.transform, new Color(0.32f, 0.98f, 0.64f, 1f));
            if (portalRing != null) portalRing.localPosition = new Vector3(0f, 0f, -0.22f);
            GameObject lightObject = new GameObject("Nexo Portal Glow Light");
            if (lightObject != null)
            {
                lightObject.transform.SetParent(portal.transform, false);
                lightObject.transform.localPosition = new Vector3(0f, 0f, -0.6f);
                portalLight = lightObject.AddComponent<Light>();
                if (portalLight != null)
                {
                    portalLight.type = LightType.Point;
                    portalLight.range = 8f;
                    portalLight.intensity = 1.2f;
                    portalLight.color = new Color(0.24f, 1f, 0.56f, 1f);
                }
            }
            sparkleFx = CreateSparkles(portal.transform);
        }

        private void BuildWaterPresentation()
        {
            GameObject water = GameObject.Find("Water Surface");
            if (water == null) return;
            waterRibbon = CreateRibbon("Nexo Water Flow Ribbon", water.transform, new Color(0.18f, 0.80f, 1f, 0.92f));
            if (waterRibbon != null) waterRibbon.localPosition = new Vector3(0f, 0.54f, 0f);
            ParticleSystem waterFx = CreateWaterMist(water.transform);
            if (waterFx != null) animatedDecor.Add(waterFx.transform);
        }

        private void AnimateExplorer()
        {
            if (explorer == null) return;
            float walk = Mathf.Sin(elapsed * 4.5f);
            explorer.localPosition = new Vector3(explorer.localPosition.x, 1f + Mathf.Sin(elapsed * 2.2f) * 0.045f, explorer.localPosition.z);
            if (explorerHead != null) explorerHead.localRotation = Quaternion.Euler(0f, walk * 5f, 0f);
            if (explorerBackpack != null) explorerBackpack.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(elapsed * 3f) * 3f);
            if (leftLeg != null) leftLeg.localRotation = Quaternion.Euler(walk * 15f, 0f, 0f);
            if (rightLeg != null) rightLeg.localRotation = Quaternion.Euler(-walk * 15f, 0f, 0f);
        }

        private void AnimateEnvironment()
        {
            for (int i = 0; i < animatedDecor.Count; i++)
            {
                Transform item = animatedDecor[i];
                if (item == null) continue;
                item.Rotate(Vector3.up, (20f + i * 3f) * Time.unscaledDeltaTime, Space.Self);
                float lift = 1f + Mathf.Sin(elapsed * 2f + i) * 0.08f;
                item.localScale = new Vector3(lift, lift, lift);
            }
            if (portalRing != null)
            {
                portalRing.Rotate(Vector3.forward, 55f * Time.unscaledDeltaTime, Space.Self);
                float pulse = 1f + Mathf.Sin(elapsed * 4f) * 0.08f;
                portalRing.localScale = Vector3.one * pulse;
            }
            if (portalLight != null)
                portalLight.intensity = 1.1f + Mathf.Sin(elapsed * 4f) * 0.35f;
            if (waterRibbon != null)
            {
                float wave = 1f + Mathf.Sin(elapsed * 3.2f) * 0.06f;
                waterRibbon.localScale = new Vector3(wave, 1f, 1f);
            }
        }

        private Transform CreateCrystal(string name, Vector3 position, float scale, Color color)
        {
            GameObject crystal = CreateMeshObject(name, CreateDiamondMesh(), color);
            if (crystal == null) return null;
            crystal.transform.position = position;
            crystal.transform.localScale = Vector3.one * scale;
            return crystal.transform;
        }

        private void CreatePyramid(string name, Vector3 position, Vector3 scale, Color color)
        {
            GameObject pyramid = CreateMeshObject(name, CreatePyramidMesh(), color);
            if (pyramid == null) return;
            pyramid.transform.position = position;
            pyramid.transform.localScale = scale;
            animatedDecor.Add(pyramid.transform);
        }

        private Transform CreateBadge(Transform parent, Vector3 localPosition)
        {
            GameObject badge = CreateMeshObject("Explorer Mission Badge", CreateDiamondMesh(), new Color(1f, 0.82f, 0.24f, 1f));
            if (badge == null) return null;
            badge.transform.SetParent(parent, false);
            badge.transform.localPosition = localPosition;
            badge.transform.localScale = new Vector3(0.18f, 0.18f, 0.08f);
            return badge.transform;
        }

        private Transform CreateTorus(string name, Transform parent, Color color)
        {
            GameObject ring = CreateMeshObject(name, CreateTorusMesh(0.9f, 0.12f, 24), color);
            if (ring == null) return null;
            ring.transform.SetParent(parent, false);
            ring.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
            return ring.transform;
        }

        private Transform CreateRibbon(string name, Transform parent, Color color)
        {
            GameObject ribbon = CreateMeshObject(name, CreateRibbonMesh(), color);
            if (ribbon == null) return null;
            ribbon.transform.SetParent(parent, false);
            ribbon.transform.localScale = new Vector3(0.92f, 1f, 0.55f);
            return ribbon.transform;
        }

        private ParticleSystem CreateSparkles(Transform parent)
        {
            GameObject fxObject = new GameObject("Nexo Portal Sparkles");
            if (fxObject == null) return null;
            fxObject.transform.SetParent(parent, false);
            fxObject.transform.localPosition = new Vector3(0f, 0f, -0.5f);
            ParticleSystem fx = fxObject.AddComponent<ParticleSystem>();
            if (fx == null) return null;
            ParticleSystem.MainModule main = fx.main;
            main.loop = true;
            main.startLifetime = 1.8f;
            main.startSpeed = 0.35f;
            main.startSize = 0.09f;
            main.startColor = new Color(0.60f, 1f, 0.76f, 1f);
            main.maxParticles = 36;
            ParticleSystem.EmissionModule emission = fx.emission;
            emission.rateOverTime = 12f;
            ParticleSystem.ShapeModule shape = fx.shape;
            shape.shapeType = ParticleSystemShapeType.Circle;
            shape.radius = 0.9f;
            ParticleSystemRenderer renderer = fx.GetComponent<ParticleSystemRenderer>();
            if (renderer != null && visualMaterial != null) renderer.material = new Material(visualMaterial);
            fx.Play(true);
            return fx;
        }

        private ParticleSystem CreateWaterMist(Transform parent)
        {
            GameObject fxObject = new GameObject("Nexo Water Mist");
            if (fxObject == null) return null;
            fxObject.transform.SetParent(parent, false);
            fxObject.transform.localPosition = new Vector3(0f, 0.3f, 0f);
            ParticleSystem fx = fxObject.AddComponent<ParticleSystem>();
            if (fx == null) return null;
            ParticleSystem.MainModule main = fx.main;
            main.loop = true;
            main.startLifetime = 2.5f;
            main.startSpeed = 0.08f;
            main.startSize = 0.05f;
            main.startColor = new Color(0.45f, 0.92f, 1f, 0.55f);
            main.maxParticles = 18;
            ParticleSystem.EmissionModule emission = fx.emission;
            emission.rateOverTime = 4f;
            ParticleSystem.ShapeModule shape = fx.shape;
            shape.shapeType = ParticleSystemShapeType.Box;
            shape.scale = new Vector3(4f, 0.2f, 1.4f);
            ParticleSystemRenderer renderer = fx.GetComponent<ParticleSystemRenderer>();
            if (renderer != null && visualMaterial != null) renderer.material = new Material(visualMaterial);
            fx.Play(true);
            return fx;
        }

        private GameObject CreateMeshObject(string name, Mesh mesh, Color color)
        {
            if (mesh == null || visualMaterial == null) return null;
            GameObject obj = new GameObject(name);
            if (obj == null) return null;
            MeshFilter filter = obj.AddComponent<MeshFilter>();
            MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
            if (filter == null || renderer == null) return obj;
            filter.sharedMesh = mesh;
            renderer.material = new Material(visualMaterial);
            if (renderer.material != null) renderer.material.color = color;
            return obj;
        }

        private static Transform FindChild(Transform root, string name)
        {
            if (root == null) return null;
            Transform[] children = root.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < children.Length; i++)
                if (children[i] != null && children[i].name == name) return children[i];
            return null;
        }

        private static Material CreateMaterial(Color color)
        {
            Shader shader = Shader.Find("Unlit/Color");
            if (shader == null) shader = Shader.Find("Universal Render Pipeline/Unlit");
            if (shader == null) shader = Shader.Find("Sprites/Default");
            if (shader == null) return null;
            Material material = new Material(shader) { name = "Nexo Presentation Material" };
            material.color = color;
            return material;
        }

        private static Mesh CreatePyramidMesh()
        {
            Mesh mesh = new Mesh { name = "Nexo Pyramid Mesh" };
            mesh.vertices = new[] { new Vector3(-0.5f, 0f, -0.5f), new Vector3(0.5f, 0f, -0.5f), new Vector3(0.5f, 0f, 0.5f), new Vector3(-0.5f, 0f, 0.5f), new Vector3(0f, 1f, 0f) };
            mesh.triangles = new[] { 0, 1, 4, 1, 2, 4, 2, 3, 4, 3, 0, 4, 0, 3, 2, 0, 2, 1 };
            mesh.RecalculateNormals();
            return mesh;
        }

        private static Mesh CreateDiamondMesh()
        {
            Mesh mesh = new Mesh { name = "Nexo Diamond Mesh" };
            mesh.vertices = new[] { new Vector3(0f, 1f, 0f), new Vector3(1f, 0f, 0f), new Vector3(0f, 0f, 1f), new Vector3(-1f, 0f, 0f), new Vector3(0f, 0f, -1f), new Vector3(0f, -1f, 0f) };
            mesh.triangles = new[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 1, 5, 2, 1, 5, 3, 2, 5, 4, 3, 5, 1, 4 };
            mesh.RecalculateNormals();
            return mesh;
        }

        private static Mesh CreateTorusMesh(float major, float minor, int segments)
        {
            Mesh mesh = new Mesh { name = "Nexo Portal Ring Mesh" };
            Vector3[] vertices = new Vector3[segments * 8];
            int[] triangles = new int[segments * 8 * 6];
            for (int i = 0; i < segments; i++)
            {
                float a = i * Mathf.PI * 2f / segments;
                float next = (i + 1) * Mathf.PI * 2f / segments;
                for (int side = 0; side < 2; side++)
                {
                    float z = side == 0 ? -minor : minor;
                    int v = (i * 2 + side) * 4;
                    vertices[v] = new Vector3(Mathf.Cos(a) * (major - minor), Mathf.Sin(a) * (major - minor), z);
                    vertices[v + 1] = new Vector3(Mathf.Cos(a) * (major + minor), Mathf.Sin(a) * (major + minor), z);
                    vertices[v + 2] = new Vector3(Mathf.Cos(next) * (major + minor), Mathf.Sin(next) * (major + minor), z);
                    vertices[v + 3] = new Vector3(Mathf.Cos(next) * (major - minor), Mathf.Sin(next) * (major - minor), z);
                    int t = (i * 2 + side) * 12;
                    triangles[t] = v; triangles[t + 1] = v + 1; triangles[t + 2] = v + 2;
                    triangles[t + 3] = v; triangles[t + 4] = v + 2; triangles[t + 5] = v + 3;
                    triangles[t + 6] = v + 2; triangles[t + 7] = v + 1; triangles[t + 8] = v;
                    triangles[t + 9] = v + 3; triangles[t + 10] = v + 2; triangles[t + 11] = v;
                }
            }
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            return mesh;
        }

        private static Mesh CreateRibbonMesh()
        {
            Mesh mesh = new Mesh { name = "Nexo Water Ribbon Mesh" };
            mesh.vertices = new[] { new Vector3(-2f, 0f, -0.3f), new Vector3(2f, 0f, -0.3f), new Vector3(2f, 0f, 0.3f), new Vector3(-2f, 0f, 0.3f) };
            mesh.triangles = new[] { 0, 1, 2, 0, 2, 3 };
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}
