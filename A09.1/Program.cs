// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// A09.1: Implement a TQueue<T>
// Program to implement a generic queue of T, using an array as the underlying storage structure.
// ------------------------------------------------------------------------------------------------
using GenericQueue;

namespace genericQueue;

#region Program -----------------------------------------------------------------------------------

class Program {
   static void Main () {
      TQueueTest.TestBasicQueue ();
      TQueueTest.TestPeek ();
      TQueueTest.TestClear ();
      TQueueTest.TestCircularQueue ();
      TQueueTest.TestResize ();
      TQueueTest.TestEmptyQueue ();
   }
}
#endregion

