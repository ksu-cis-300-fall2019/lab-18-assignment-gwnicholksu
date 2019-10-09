/* DictionaryTests.cs
 * Author: Rod Howell
 */
using NUnit.Framework;
using System;

namespace Ksu.Cis300.NameLookup.Tests
{
    /// <summary>
    /// Unit tests for the Dictionary class.
    /// </summary>
    [TestFixture]
    public class DictionaryTests
    {
        /// <summary>
        /// Gets a dictionary containing seven keys and values.
        /// </summary>
        private Dictionary<int, string> LoadDictionary()
        {
            Dictionary<int, string> d = new Dictionary<int, string>();
            d.Add(10, "Ten");
            d.Add(5, "Five");
            d.Add(15, "Fifteen");
            d.Add(3, "Three");
            d.Add(7, "Seven");
            d.Add(13, "Thirteen");
            d.Add(20, "Twenty");
            return d;
        }

        /// <summary>
        /// Tests that removing from an empty dictionary returns false.
        /// </summary>
        [Test]
        public void TestARemoveFromEmpty()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            Assert.That(d.Remove("x"), Is.False);
        }

        /// <summary>
        /// Tests that after adding one key, it can be removed.
        /// </summary>
        [Test]
        public void TestBAddOneRemoveIt()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            d.Add("one", 1);
            bool removeResult = d.Remove("one");
            int v;
            bool lookupResult = d.TryGetValue("one", out v);
            Assert.Multiple(() =>
            {
                Assert.That(removeResult, Is.True);
                Assert.That(lookupResult, Is.False);
            });
        }

        /// <summary>
        /// Builds a tree with 7 nodes, removes a leaf, then looks up several keys and the removed key.
        /// </summary>
        [Test]
        public void TestCRemoveLeaf()
        {
            Dictionary<int, string> d = LoadDictionary();
            bool removeResult = d.Remove(13);
            string v;
            bool lookup10Result = d.TryGetValue(10, out v);
            bool lookup13Result = d.TryGetValue(13, out v);
            bool lookup15Result = d.TryGetValue(15, out v);
            bool lookup20Result = d.TryGetValue(20, out v);
            Assert.Multiple(() =>
            {
                Assert.That(removeResult, Is.True);
                Assert.That(lookup10Result, Is.True);
                Assert.That(lookup13Result, Is.False);
                Assert.That(lookup15Result, Is.True);
                Assert.That(lookup20Result, Is.True);
            });
        }

        /// <summary>
        /// Builds a tree with 7 nodes, removes a leaf, then its parent, then looks up several keys
        /// and the removed key.
        /// </summary>
        [Test]
        public void TestDRemoveLeafThenItsParent()
        {
            Dictionary<int, string> d = LoadDictionary();
            d.Remove(13);
            bool removeResult = d.Remove(15);
            string v;
            bool lookup10Result = d.TryGetValue(10, out v);
            bool lookup13Result = d.TryGetValue(13, out v);
            bool lookup15Result = d.TryGetValue(15, out v);
            bool lookup20Result = d.TryGetValue(20, out v);
            Assert.Multiple(() =>
            {
                Assert.That(removeResult, Is.True);
                Assert.That(lookup10Result, Is.True);
                Assert.That(lookup13Result, Is.False);
                Assert.That(lookup15Result, Is.False);
                Assert.That(lookup20Result, Is.True);
            });
        }

        /// <summary>
        /// Builds a tree with 7 nodes, removes a leaf, then its parent, then looks up several keys
        /// and the removed key.
        /// </summary>
        [Test]
        public void TestDRemoveOtherLeafThenItsParent()
        {
            Dictionary<int, string> d = LoadDictionary();
            d.Remove(20);
            bool removeResult = d.Remove(15);
            string v;
            bool lookup10Result = d.TryGetValue(10, out v);
            bool lookup13Result = d.TryGetValue(13, out v);
            bool lookup15Result = d.TryGetValue(15, out v);
            bool lookup20Result = d.TryGetValue(20, out v);
            Assert.Multiple(() =>
            {
                Assert.That(removeResult, Is.True);
                Assert.That(lookup10Result, Is.True);
                Assert.That(lookup13Result, Is.True);
                Assert.That(lookup15Result, Is.False);
                Assert.That(lookup20Result, Is.False);
            });
        }

        /// <summary>
        /// Builds a tree with 7 nodes, removes the root, then looks up several keys
        /// and the removed key.
        /// </summary>
        [Test]
        public void TestERemoveRoot()
        {
            Dictionary<int, string> d = LoadDictionary();
            bool removeResult = d.Remove(10);
            string v;
            bool lookup3Result = d.TryGetValue(3, out v);
            bool lookup7Result = d.TryGetValue(7, out v);
            bool lookup10Result = d.TryGetValue(10, out v);
            bool lookup13Result = d.TryGetValue(13, out v);
            bool lookup20Result = d.TryGetValue(20, out v);
            Assert.Multiple(() =>
            {
                Assert.That(removeResult, Is.True);
                Assert.That(lookup3Result, Is.True);
                Assert.That(lookup7Result, Is.True);
                Assert.That(lookup10Result, Is.False);
                Assert.That(lookup13Result, Is.True);
                Assert.That(lookup20Result, Is.True);
            });
        }

        /// <summary>
        /// Builds a tree with 7 nodes, tries to remove a key that is not there, then
        /// looks up several keys.
        /// </summary>
        [Test]
        public void TestFRemoveMissing()
        {
            Dictionary<int, string> d = LoadDictionary();
            bool removeResult = d.Remove(9);
            string v;
            bool lookup3Result = d.TryGetValue(3, out v);
            bool lookup7Result = d.TryGetValue(7, out v);
            bool lookup10Result = d.TryGetValue(10, out v);
            bool lookup13Result = d.TryGetValue(13, out v);
            bool lookup20Result = d.TryGetValue(20, out v);
            Assert.Multiple(() =>
            {
                Assert.That(removeResult, Is.False);
                Assert.That(lookup3Result, Is.True);
                Assert.That(lookup7Result, Is.True);
                Assert.That(lookup10Result, Is.True);
                Assert.That(lookup13Result, Is.True);
                Assert.That(lookup20Result, Is.True);
            });
        }
    }
}