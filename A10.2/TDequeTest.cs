// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// TDequeTest.cs
// Tests the TDeque<T> implementation.
// ------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericDeque;

#region class TDequeTest --------------------------------------------------------------------------
/// <summary>Tests the TDeque class</summary>
[TestClass]
public class TDequeTest {

   #region Methods --------------------------------------------------
   /// <summary>Test  basic deque operations</summary>
   [TestMethod]
   public void DequeMaintainFifoLifoOrder () {
      TDeque<int> dk = new ();
      dk.EnqueueFront (2);
      dk.EnqueueFront (1);
      dk.EnqueueRear (3);
      dk.EnqueueRear (4);
      dk.EnqueueFront (6);
      dk.EnqueueFront (5);
      dk.EnqueueRear (7);
      dk.EnqueueRear (8);
      dk.DequeueFront ();
      dk.EnqueueFront (5);
      dk.DequeueRear ();
      dk.EnqueueRear (8);
      dk.DequeueRear ();
      dk.DequeueRear ();
      dk.DequeueFront ();
      dk.DequeueFront ();
      Assert.AreEqual (1, dk.DequeueFront ());
      Assert.AreEqual (4, dk.DequeueRear ());
      Assert.AreEqual (3, dk.DequeueRear ());
      Assert.AreEqual (2, dk.DequeueFront ());
   }

   /// <summary>Test operations on an empty deque</summary>
   [TestMethod]
   public void TestEmpty () {
      TDeque<int> dk = new ();
      dk.EnqueueFront (10);
      dk.DequeueFront ();
      dk.EnqueueRear (20);
      dk.DequeueRear ();
      dk.EnqueueFront (30);
      dk.EnqueueRear (40);
      dk.DequeueFront ();
      dk.DequeueRear ();
      Assert.IsTrue (dk.IsEmpty);
   }

   /// <summary>Test peek at front and rear</summary>
   [TestMethod]
   public void PeekReturnsFirsAndLastElement () {
      TDeque<int> dk = new ();
      dk.EnqueueFront (10);
      dk.EnqueueRear (20);
      dk.EnqueueFront (30);
      dk.EnqueueRear (40);
      dk.DequeueFront ();
      dk.DequeueRear ();
      Assert.AreEqual (20, dk.PeekRear ());
      Assert.AreEqual (10, dk.PeekFront ());
   }

   /// <summary>Test automatic deque resizing</summary>
   [TestMethod]
   public void ResizeMaintainsCorrectOrder () {
      TDeque<char> dk = new ();
      for (int i = 0; i < 5; i++)
         dk.EnqueueRear ((char)('A' + i));
      for (int i = 5; i < 10; i++)
         dk.EnqueueFront ((char)('A' + i));
      Assert.AreEqual (10, dk.Count);
      for (int i = 9; i >= 5; i--)
         Assert.AreEqual ((char)('A' + i), dk.DequeueFront ());
      for (int i = 0; i < 5; i++)
         Assert.AreEqual ((char)('A' + i), dk.DequeueFront ());
   }

   /// <summary>Test resizing while the head has wrapped around</summary>
   [TestMethod]
   public void ResizeWhileWrappedMaintainsQueueOrder () {
      TDeque<int> dk = new ();
      for (int i = 1; i <= 4; i++) dk.EnqueueRear (i);
      dk.DequeueFront ();
      dk.DequeueFront ();
      for (int i = 5; i <= 10; i++) dk.EnqueueFront (i);
      for (int i = 3; i <= 10; i++) dk.DequeueRear ();
      Assert.IsTrue (dk.IsEmpty);
   }
   #endregion
}
#endregion
