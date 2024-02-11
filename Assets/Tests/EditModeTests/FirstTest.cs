using NUnit.Framework;
using UnityEditor.SceneManagement;

namespace Tests
{
    public class FirstTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void FirstTestSimplePasses()
        {
            // Use the Assert class to test conditions
            Assert.IsTrue(true, "This test should always pass for now.");
        }

        // An EditMode test to check if the scene loads without errors
        [Test]
        public void SceneLoadTest()
        {
            // Load the scene
            string scenePath = "Assets/Scenes/Ranch.unity"; // Change this to the path of your scene
            var scene = EditorSceneManager.OpenScene(scenePath, UnityEditor.SceneManagement.OpenSceneMode.Single);

            // Check if the scene loaded without errors
            Assert.IsTrue(scene.isLoaded, "Scene did not load properly.");
        }

        // Additional tests can be added based on the functionality you want to test

        // A UnityTest behaves like a coroutine in Play Mode.
        // In Edit Mode, you can use `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator FirstTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}