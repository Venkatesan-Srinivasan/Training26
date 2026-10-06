// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// TQueue.cs
// A09.1: Implement a TQueue<T>
// Implements a generic queue of T, using a circular arrayusing System;
namespace GenericQueue;

#region class TQueue ------------------------------------------------------------------------------
/// <summary>Generic first-in-first-out queue implemented using a circular array</summary>
class TQueue<T> {
   #region Methods --------------------------------------------------
   /// <summary>Clear the queue</summary>
   public void TClear () {
      mQueue = new T[CAPACITY];
      mHead = mTail = mUsed = 0;
   }

   /// <summary>Get the number of items</summary>
   public int TCount () => mUsed;

   /// <summary>Remove the first item</summary>
   public T TDequeue () {
      CheckEmpty ();
      T value = mQueue[mHead];
      mHead = (mHead + 1) % mQueue.Length;
      mUsed--;
      return value;
   }

   /// <summary>Add an item to the queue</summary>
   public void TEnqueue (T item) {
      if (mUsed == mQueue.Length)
         Resize ();
      mQueue[mTail] = item;
      mTail = (mTail + 1) % mQueue.Length;
      mUsed++;
   }

   /// <summary>Check if the queue is empty</summary>
   public bool TIsEmpty () => mUsed == 0;

   /// <summary>Get the first item</summary>
   public T TPeek () {
      CheckEmpty ();
      return mQueue[mHead];
   }
   #endregion

   #region Implementation -------------------------------------------
   /// <summary>Check if the queue is empty</summary>
   void CheckEmpty () {
      if (mUsed == 0)
         throw new Exception ("Queue Empty");
   }

   /// <summary>Increase the queue capacity when full</summary>
   void Resize () {
      int newSize = mQueue.Length == 0 ? CAPACITY : 2 * mQueue.Length;
      T[] newQueue = new T[newSize];
      for (int i = 0; i < mUsed; i++)
         newQueue[i] = mQueue[(mHead + i) % mQueue.Length];
      mQueue = newQueue;
      mHead = 0;
      mTail = mUsed;
   }
   #endregion

   #region Private --------------------------------------------------
   int mHead, mTail, mUsed;
   T[] mQueue = new T[CAPACITY];
   const int CAPACITY = 4;
   #endregion
}
#endregion
