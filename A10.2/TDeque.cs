// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// TDeque.cs
// A10.2: Implement a TDeque<T>
// Program to impleent enqueue or dequeue at both the ends at the head and the tail.
// This should allow to add and remove at the head & add and remove at the tail.
// Implement it using an array of T.
// ------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Text;

namespace GenericDeque;

#region class TDeque ------------------------------------------------------------------------------
public class TDeque<T> {

   #region Properties -----------------------------------------------
   /// <summary>Get the number of items</summary>
   public int Count { get; set; }

   /// <summary>Check if the deque is empty</summary>
   public bool IsEmpty => Count == 0;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Remove the first item</summary>
   public T DequeueFront () {
      if (IsEmpty) throw new Exception ("Queue empty");
      T value = mData[mHead];
      mData[mHead] = default!;
      mHead = (mHead + 1) % mData.Length;
      Count--;
      return value;
   }

   /// <summary>Remove the last item</summary>
   public T DequeueRear () {
      if (IsEmpty) throw new Exception ("Queue empty");
      mTail = (mTail - 1 + mData.Length) % mData.Length;
      T value = mData[mTail];
      mData[mTail] = default!;
      Count--;
      return value;
   }

   /// <summary>Add an item at front to the deque</summary>
   public void EnqueueFront (T a) {
      if (Count == mData.Length) Resize ();
      mHead = (mHead - 1 + mData.Length) % mData.Length;
      mData[mHead] = a;
      Count++;
   }

   /// <summary>Add an item at rear to the deque</summary>
   public void EnqueueRear (T a) {
      if (Count == mData.Length) Resize ();
      mData[mTail] = a;
      mTail = (mTail + 1) % mData.Length;
      Count++;
   }

   /// <summary>Get the first item added to deque</summary>
   public T PeekFront () {
      if (IsEmpty) throw new Exception ("Queue os empty");
      T value = DequeueFront ();
      EnqueueFront (value);
      return value;
   }
   /// <summary>Get the last item added to deque</summary>
   public T PeekRear () {
      if (IsEmpty) throw new Exception ("Queue os empty");
      T value = DequeueRear ();
      EnqueueRear (value);
      return value;
   }
   #endregion

   #region Implementation -------------------------------------------
   /// <summary>Increase the deque capacity when full</summary>
   void Resize () {
      T[] newData = new T[Count * 2];
      int firstPart = mData.Length - mHead;
      Array.Copy (mData, mHead, newData, 0, firstPart);
      Array.Copy (mData, 0, newData, firstPart, mHead);
      mData = newData;
      mHead = 0;
      mTail = Count;
   }
   #endregion

   #region Private --------------------------------------------------
   T[] mData = new T[CAPACITY];
   int mHead;
   int mTail;
   const int CAPACITY = 4;
   #endregion
}
#endregion
