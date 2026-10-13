// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// TQueueTest.cs
// Tests the TQueue<T> implementation.
// ------------------------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace GenericQueue;

#region class TQueueTest --------------------------------------------------------------------------
/// <summary>Tests the TQueue class</summary>
[TestClass]
public class TQueueTest {
   #region Methods --------------------------------------------------
   /// <summary>Test clearing the queue</summary>
   [TestMethod]
   public void ClearRemovesAllElements () {
      TQueue<int> q = new ();
      q.Enqueue (10);
      q.Enqueue (20);
      q.Clear ();
      Assert.AreEqual (0, q.Count);
      Assert.IsTrue (q.IsEmpty);
   }

   /// <summary>Test circular queue behavior</summary>
   [TestMethod]
   public void CircularQueueMaintainsCorrectOrder () {
      TQueue<int> q = new ();
      q.Enqueue (10);
      q.Enqueue (20);
      q.Enqueue (30);
      q.Enqueue (40);
      q.Dequeue ();
      q.Dequeue ();
      q.Enqueue (50);
      q.Enqueue (60);
      Assert.AreEqual (30, q.Dequeue ());
      Assert.AreEqual (40, q.Dequeue ());
      Assert.AreEqual (50, q.Dequeue ());
      Assert.AreEqual (60, q.Dequeue ());
      Assert.IsTrue (q.IsEmpty);
   }

   /// <summary>Test operations on an empty queue</summary>
   [TestMethod]
   public void DequeueThrowsOnEmptyQueue () {
      TQueue<int> q = new ();
      InvalidOperationException ex =
         Assert.ThrowsExactly<InvalidOperationException> (() => q.Dequeue ());
      Assert.AreEqual ("Queue Empty", ex.Message);
   }

   /// <summary>Test basic queue operations</summary>
   [TestMethod]
   public void EnqueueAndDequeueMaintainFifoOrder () {
      TQueue<int> q = new ();
      q.Enqueue (10);
      q.Enqueue (20);
      q.Enqueue (30);
      Assert.AreEqual (10, q.Dequeue ());
      Assert.AreEqual (20, q.Dequeue ());
      Assert.AreEqual (30, q.Dequeue ());
      Assert.IsTrue (q.IsEmpty);
   }

   /// <summary>Test peek and count</summary>
   [TestMethod]
   public void PeekReturnsFirstElement () {
      TQueue<int> q = new ();
      q.Enqueue (100);
      q.Enqueue (200);
      Assert.AreEqual (100, q.Peek ());
      Assert.AreEqual (2, q.Count);
   }

   /// <summary>Test operations on an empty peek</summary>
   [TestMethod]
   public void PeekThrowsOnEmptyQueue () {
      TQueue<int> q = new ();
      InvalidOperationException ex =
         Assert.ThrowsExactly<InvalidOperationException> (() => q.Peek ());
      Assert.AreEqual ("Queue Empty", ex.Message);
   }

   /// <summary>Test reference type</summary>
   [TestMethod]
   public void QueueSupportsStringValues () {
      TQueue<string> q = new ();
      q.Enqueue ("John");
      q.Enqueue ("Sam");
      Assert.AreEqual ("John", q.Dequeue ());
      Assert.AreEqual ("Sam", q.Dequeue ());
      Assert.IsTrue (q.IsEmpty);
   }

   /// <summary>Test automatic queue resizing</summary>
   [TestMethod]
   public void ResizeMaintainsQueueOrder () {
      TQueue<int> q = new ();
      for (int i = 1; i <= 10; i++)
         q.Enqueue (i);
      Assert.AreEqual (10, q.Count);
      for (int i = 1; i <= 10; i++)
         Assert.AreEqual (i, q.Dequeue ());
      Assert.IsTrue (q.IsEmpty);
   }
   #endregion
}
#endregion
