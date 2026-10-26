using System;

namespace Nexo.Core
{
    [Serializable]
    public sealed class ContentVersion : IContentVersion
    {
        public int major = 1;
        public int minor = 0;
        public string build = "p2";

        public int Major { get { return major; } }
        public int Minor { get { return minor; } }
        public string Build { get { return build ?? string.Empty; } }
    }

    public interface IContentVersion
    {
        int Major { get; }
        int Minor { get; }
        string Build { get; }
    }
}
