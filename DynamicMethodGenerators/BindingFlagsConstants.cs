using System.Reflection;

namespace DynamicMethodHandleGenerators
{
    public static class BindingFlagsConstants
    {
        public const BindingFlags commonMethodFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;

        public const BindingFlags allLevelFlags
          = BindingFlags.FlattenHierarchy
          | BindingFlags.Instance
          | BindingFlags.Public
          | BindingFlags.NonPublic
          ;

        public const BindingFlags oneLevelFlags
          = BindingFlags.DeclaredOnly
          | BindingFlags.Instance
          | BindingFlags.Public
          | BindingFlags.NonPublic
          ;

        public const BindingFlags ctorFlags
          = BindingFlags.Instance
          | BindingFlags.Public
          | BindingFlags.NonPublic
          ;

        public const BindingFlags factoryFlags =
          BindingFlags.Static |
          BindingFlags.Public |
          BindingFlags.FlattenHierarchy;

        public const BindingFlags privateMethodFlags =
          BindingFlags.Public |
          BindingFlags.NonPublic |
          BindingFlags.Instance |
          BindingFlags.FlattenHierarchy;

        public const BindingFlags propertyFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
        public const BindingFlags fieldFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    }
}
