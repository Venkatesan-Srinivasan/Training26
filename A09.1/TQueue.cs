// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// TQueue.cs
// A09.1: Implement a TQueue<T>
// Implements a generic queue of T, using a circular array
using System;
namespace GenericQueue;

#region class TQueue ------------------------------------------------------------------------------
/// <summary>Generic first-in-first-out queue implemented using a circular array</summary>
public class TQueue<T> {

   #region Properties -----------------------------------------------
   /// <summary>Get the number of items</summary>
   public int Count { get; private set; }

   /// <summary>Check if the queue is empty</summary>
   public bool IsEmpty => Count == 0;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Clear the queue</summary>
   public void Clear () {
      Array.Clear (mQueue);
      mHead = mTail = Count = 0;
   }

   /// <summary>Remove the first item</summary>
   public T Dequeue () {
      CheckEmpty ();
      T value = mQueue[mHead];
      mQueue[mHead] = default!;
      mHead = (mHead + 1) % mQueue.Length;
      Count--;
      return value;
   }

   /// <summary>Add an item to the queue</summary>
   public void Enqueue (T item) {
      if (Count == mQueue.Length)
         Resize ();
      mQueue[mTail] = item;
      mTail = (mTail + 1) % mQueue.Length;
      Count++;
   }

   /// <summary>Get the first item</summary>
   public T Peek () {
      CheckEmpty ();
      return mQueue[mHead];
   }
   #endregion

   #region Implementation -------------------------------------------
   /// <summary>Check if the queue is empty</summary>
   void CheckEmpty () {
      if (Count == 0)
         throw new InvalidOperationException ("Queue Empty");
   }

   /// <summary>Increase the queue capacity when full</summary>
   void Resize () {
      T[] newQueue = new T[2 * mQueue.Length];
      int firstPart = mQueue.Length - mHead;   // Called only when full, so Count == Length
      Array.Copy (mQueue, mHead, newQueue, 0, firstPart);
      Array.Copy (mQueue, 0, newQueue, firstPart, mHead);
      mQueue = newQueue;
      mHead = 0;
      mTail = Count;
   }
   #endregion

   #region Private --------------------------------------------------
   int mHead, mTail;
   T[] mQueue = new T[CAPACITY];
   const int CAPACITY = 4;
   #endregion
}
#endregion
