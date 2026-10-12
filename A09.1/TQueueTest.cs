// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// TQueueTest.cs
// Tests the TQueue<T> implementation.
// ------------------------------------------------------------------------------------------------
using Xunit;
namespace GenericQueue;

#region class TQueueTest --------------------------------------------------------------------------
/// <summary>Tests the TQueue class</summary>
public class TQueueTest {
   #region Methods --------------------------------------------------
   /// <summary>Test clearing the queue</summary>
   [Fact]
   public void ClearRemovesAllElements () {
      TQueue<int> q = new ();
      q.Enqueue (10);
      q.Enqueue (20);
      q.Clear ();
      Assert.Equal (0, q.Count ());
      Assert.True (q.IsEmpty ());
   }

   /// <summary>Test circular queue behavior</summary>
   [Fact]
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
      Assert.Equal (30, q.Dequeue ());
      Assert.Equal (40, q.Dequeue ());
      Assert.Equal (50, q.Dequeue ());
      Assert.Equal (60, q.Dequeue ());
      Assert.True (q.IsEmpty ());
   }

   /// <summary>Test operations on an empty queue</summary>
   [Fact]
   public void DequeueThrowsOnEmptyQueue () {
      TQueue<int> q = new ();
      InvalidOperationException ex =
         Assert.Throws<InvalidOperationException> (() => q.Dequeue ());
      Assert.Equal ("Queue Empty", ex.Message);
   }

   /// <summary>Test basic queue operations</summary>
   [Fact]
   public void EnqueueAndDequeueMaintainFifoOrder () {
      TQueue<int> q = new ();
      q.Enqueue (10);
      q.Enqueue (20);
      q.Enqueue (30);
      Assert.Equal (10, q.Dequeue ());
      Assert.Equal (20, q.Dequeue ());
      Assert.Equal (30, q.Dequeue ());
      Assert.True (q.IsEmpty ());
   }

   /// <summary>Test peek and count</summary>
   [Fact]
   public void PeekReturnsFirstElement () {
      TQueue<int> q = new ();
      q.Enqueue (100);
      q.Enqueue (200);
      Assert.Equal (100, q.Peek ());
      Assert.Equal (2, q.Count ());
   }

   /// <summary>Test operations on an empty peek</summary>
   [Fact]
   public void PeekThrowsOnEmptyQueue () {
      TQueue<int> q = new ();
      InvalidOperationException ex =
         Assert.Throws<InvalidOperationException> (() => q.Peek ());
      Assert.Equal ("Queue Empty", ex.Message);
   }

   /// <summary>Test reference type</summary>
   [Fact]
   public void QueueSupportsStringValues () {
      TQueue<string> q = new ();
      q.Enqueue ("John");
      q.Enqueue ("Sam");
      Assert.Equal ("John", q.Dequeue ());
      Assert.Equal ("Sam", q.Dequeue ());
      Assert.True (q.IsEmpty ());
   }

   /// <summary>Test automatic queue resizing</summary>
   [Fact]
   public void ResizeMaintainsQueueOrder () {
      TQueue<int> q = new ();
      for (int i = 1; i <= 10; i++)
         q.Enqueue (i);
      Assert.Equal (10, q.Count ());
      for (int i = 1; i <= 10; i++)
         Assert.Equal (i, q.Dequeue ());
      Assert.True (q.IsEmpty ());
   }
   #endregion
}
#endregion
