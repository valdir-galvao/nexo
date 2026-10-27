using UnityEngine;

namespace Nexo.UI
{
    public static class NexoVisualTheme
    {
        public static readonly Color Sky = new Color(0.12f, 0.2f, 0.32f);
        public static readonly Color Stone = new Color(0.45f, 0.5f, 0.58f);
        public static readonly Color Wood = new Color(0.65f, 0.55f, 0.35f);
        public static readonly Color Water = new Color(0.1f, 0.55f, 0.9f);
        public static readonly Color Signal = new Color(0.95f, 0.82f, 0.3f);
        public static readonly Color SafeText = new Color(0.95f, 0.97f, 1f);

        public static void Apply(Renderer renderer, Color color)
        {
            if (renderer == null) return;
            renderer.material.color = color;
        }
    }
}
