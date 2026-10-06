// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// TQueueTest.cs
// Tests the TQueue<T> implementation.
// ------------------------------------------------------------------------------------------------
using System;

namespace GenericQueue;

#region class TQueueTest --------------------------------------------------------------------------
/// <summary>Tests the TQueue class</summary>
class TQueueTest {

   #region Methods --------------------------------------------------
   /// <summary>Test basic queue operations</summary>
   public static void TestBasicQueue () {
      TQueue<int> q = new ();
      q.TEnqueue (10);
      q.TEnqueue (20);
      q.TEnqueue (30);
      bool passed = q.TDequeue () == 10 &&
                    q.TDequeue () == 20 &&
                    q.TDequeue () == 30 &&
                    q.TIsEmpty ();
      PrintResult ("Basic Queue", passed);
   }

   /// <summary>Test clearing the queue</summary>
   public static void TestClear () {
      TQueue<int> q = new ();
      q.TEnqueue (10);
      q.TEnqueue (20);
      q.TClear ();
      bool passed = q.TCount () == 0 && q.TIsEmpty ();
      PrintResult ("Clear", passed);
   }

   /// <summary>Test circular queue behavior</summary>
   public static void TestCircularQueue () {
      TQueue<int> q = new ();
      q.TEnqueue (10);
      q.TEnqueue (20);
      q.TEnqueue (30);
      q.TEnqueue (40);
      q.TDequeue ();
      q.TDequeue ();
      q.TEnqueue (50);
      q.TEnqueue (60);
      bool passed = q.TDequeue () == 30 &&
                    q.TDequeue () == 40 &&
                    q.TDequeue () == 50 &&
                    q.TDequeue () == 60 &&
                    q.TIsEmpty ();
      PrintResult ("Circular Queue", passed);
   }

   /// <summary>Test operations on an empty queue</summary>
   public static void TestEmptyQueue () {
      TQueue<int> q = new ();
      bool dequeuePassed = false;
      bool peekPassed = false;
      try {
         q.TDequeue ();
      } catch (Exception e) {
         dequeuePassed = e.Message == "Queue Empty";
      }
      try {
         q.TPeek ();
      } catch (Exception e) {
         peekPassed = e.Message == "Queue Empty";
      }
      PrintResult ("Empty Queue", dequeuePassed && peekPassed);
   }

   /// <summary>Test peek and count</summary>
   public static void TestPeek () {
      TQueue<int> q = new ();
      q.TEnqueue (100);
      q.TEnqueue (200);
      bool passed = q.TPeek () == 100 &&
                    q.TCount () == 2;
      PrintResult ("Peek and Count", passed);
   }

   /// <summary>Test automatic queue resizing</summary>
   public static void TestResize () {
      TQueue<int> q = new ();
      for (int i = 1; i <= 10; i++)
         q.TEnqueue (i);
      bool passed = q.TCount () == 10;
      for (int i = 1; i <= 10; i++)
         if (q.TDequeue () != i)
            passed = false;
      passed = passed && q.TIsEmpty ();
      PrintResult ("Resize", passed);
   }
   #endregion

   #region Implementation -------------------------------------------
   /// <summary>Print the test result</summary>
   static void PrintResult (string test, bool passed) =>
      Console.WriteLine ($"{test}: {(passed ? "PASSED" : "FAILED")}");
   #endregion
}
#endregion
