using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class FirstPlayModeTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void FirstPlayModeTestSimplePasses()
        {
            // Create a GameObject for testing
            GameObject testObject = new GameObject("TestObject");

            // Add a Rigidbody component (assuming you have a movement script that relies on Rigidbody)
            Rigidbody rigidbody = testObject.AddComponent<Rigidbody>();

            // Perform some action (e.g., simulate player input or trigger an event)
            // For example, let's simulate moving the object to the right
            rigidbody.AddForce(Vector3.right * 10f, ForceMode.Force);

            // Use the Assert class to test conditions
            Assert.AreEqual(Vector3.right * 10f, rigidbody.velocity, "Object did not move as expected.");
        }

        // A UnityTest behaves like a coroutine in Play Mode.
        // In Edit Mode, you can use `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator FirstPlayModeTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}