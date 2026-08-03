// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------
// Program.cs
// Program to implement a simple guessing game. The computer thinks of a random number between 1 and 100, and the user has to guess it.
// The user can enter an number, and the computer will respond with one of these:
//  - Your guess is too high
//  - Your guess is too low
//  - You guessed correctly.
// --------------------------------------------------------------------------------------------
namespace A01;
#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main (string[] args) {
      int randomNumber = new Random ().Next (1, 101);
      // To prompt the user to guess the Random number
      int guessNumber = GetNumber ("Enter a number from 1 to 100: ");
      // Check the guess accuracy
      for (int i = 6; i >= 0; i--) {
         if (guessNumber < randomNumber && i != 0) {
            Console.WriteLine ("Your guess is too low");
            guessNumber = GetNumber ($"You have {i} try. Guess correctly:  ");
         } else if (guessNumber > randomNumber && i != 0) {
            Console.WriteLine ("Your guess is too high");
            guessNumber = GetNumber ($"You have {i} try. Guess correctly:  ");
         } else if (guessNumber == randomNumber) {
            Console.WriteLine ("You guessed correctly");
            break;
         } else Console.WriteLine ($"The game is over and the number is {randomNumber}");
      }
      #region Method ----------------------------------------------
      // Function to get a valid number from 1 to 100
      int GetNumber (string str) {
         for (; ; ) {
            Console.Write (str);
            if (int.TryParse (Console.ReadLine (), out int result) && (result <= 100 && result > 0)) return result;
            else Console.WriteLine ($"INVALID!");
         }
      }
      #endregion
   }
}
#endregion