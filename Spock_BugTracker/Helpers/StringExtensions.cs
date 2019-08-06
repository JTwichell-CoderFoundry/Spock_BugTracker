using System;

namespace Spock_BugTracker.Helpers
{
    public static class StringExtensions
    {
        public static string SomeCoolNewStringFeature(this String myString)
        {
            return "My Feature was hit";
        }

        public static void TestStringExtension()
        {
            var newString = "Foo";
            var message = newString.SomeCoolNewStringFeature();
        }
    }
}