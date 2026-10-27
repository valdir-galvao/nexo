using NUnit.Framework;
using Nexo.Safety;

namespace Nexo.Tests
{
    public sealed class SafetyPolicyTests
    {
        [Test] public void MvpDisablesExternalContactAndCommerce()
        {
            Assert.IsTrue(SafetyPolicy.IsMvpSafeConfiguration());
            Assert.IsFalse(SafetyPolicy.OpenChatEnabled);
            Assert.IsFalse(SafetyPolicy.PurchasesEnabled);
            Assert.IsFalse(SafetyPolicy.AdvertisingEnabled);
        }
    }
}
