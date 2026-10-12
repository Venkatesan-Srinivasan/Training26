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
   #region Methods --------------------------------------------------
   /// <summary>Clear the queue</summary>
   public void Clear () {
      mQueue = new T[CAPACITY];
      mHead = mTail = mUsed = 0;
   }

   /// <summary>Get the number of items</summary>
   public int Count () => mUsed;

   /// <summary>Remove the first item</summary>
   public T Dequeue () {
      CheckEmpty ();
      T value = mQueue[mHead];
      mQueue[mHead] = default!;
      mHead = (mHead + 1) % mQueue.Length;
      mUsed--;
      return value;
   }

   /// <summary>Add an item to the queue</summary>
   public void Enqueue (T item) {
      if (mUsed == mQueue.Length)
         Resize ();
      mQueue[mTail] = item;
      mTail = (mTail + 1) % mQueue.Length;
      mUsed++;
   }

   /// <summary>Check if the queue is empty</summary>
   public bool IsEmpty () => mUsed == 0;

   /// <summary>Get the first item</summary>
   public T Peek () {
      CheckEmpty ();
      return mQueue[mHead];
   }
   #endregion

   #region Implementation -------------------------------------------
   /// <summary>Check if the queue is empty</summary>
   void CheckEmpty () {
      if (mUsed == 0)
         throw new InvalidOperationException ("Queue Empty");
   }

   /// <summary>Increase the queue capacity when full</summary>
   void Resize () {
      int newSize = 2 * mQueue.Length;
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
