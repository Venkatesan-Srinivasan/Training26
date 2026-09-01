// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// A06: 8 Queens problem
// Program to Solve 8 Queens: place queens so that no two queen share same row, column, or diagonal
// Keep only distinct solutions (exclude rotations 90°, 180°, 270° and mirror images).
// ------------------------------------------------------------------------------------------------
using static System.Console;
using System.Text;

namespace A06;
#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      List<int[]> solutions = [];
      Solve (solutions);
      GetUniqueSolutions (solutions);
      for (int i = 0, count = solutions.Count (); i < count; i++) {
         WriteLine ($"\n{i + 1} of {count}");
         PrintChessBoard (solutions[i]);
      }
   }

   #region Implementations ------------------------------------------
   // Solve for distinct solutions
   static void GetUniqueSolutions (List<int[]> solutions) {
      List<int[]> unique = [];
      foreach (var sln in solutions) {
         var symmetries = new List<int[]> { ([.. sln.Reverse ()]) }; // Vertical mirror
         int[] rot90 = Rotate90 (sln), vFlipRot90 = [.. rot90.Reverse ()],
               rot180 = Rotate90 (rot90), vFlipRot180 = [.. rot180.Reverse ()],
               rot270 = Rotate90 (rot180), vFlipRot270 = [.. rot270.Reverse ()];
         symmetries.AddRange ([rot90, rot180, rot270, vFlipRot90, vFlipRot180, vFlipRot270]);
         if (unique.Any (u => symmetries.Any (a => u.SequenceEqual (a)))) continue;
         unique.Add (sln); // Adding rotations and its vertical mirror
      }
      solutions.Clear ();
      solutions.AddRange (unique);

      // Rotate by 90
      int[] Rotate90 (int[] arr) {
         int[] arr2 = new int[N];
         for (int i = 0; i < N; i++)
            arr2[arr[i]] = N - 1 - i;
         return arr2;
      }
   }

   // Prints each row to form a standard chessboard
   static void PrintChessBoard (int[] sln) {
      OutputEncoding = Encoding.UTF8;
      for (int row = 0, len = 2 * N + 1; row < len; row++) {
         if (row == 0) WriteLine ($"┌───{GetPattern ("┬───")}┐");
         else if (row % 2 == 0 && row != 2 * N) WriteLine ($"├───{GetPattern ("┼───")}┤");
         else if (row == 2 * N) WriteLine ($"└───{GetPattern ("┴───")}┘");
         else {
            for (int col = 0; col < N; col++) {
               string res = (col == sln[row / 2]) ? "│ ♕ " : "│   ";
               Write (res);
            }
            WriteLine ("│");
         }
      }

      // Print the given pattern for n times
      string GetPattern (string pattern) {
         string str = "";
         for (int i = 0, len = N - 1; i < len; i++) str += pattern;
         return str;
      }
   }

   // Solves for identical solutions
   static void Solve (List<int[]> solutions) {
      int[] sln = new int[N];
      Place (sln, 0, solutions);

      // Recursive backtracking method to place queens row by row on the board
      void Place (int[] sln, int r, List<int[]> solutions) {
         for (int c = 0; c < N; c++) { // Choices: one queen per column in each row
            if (IsValid (sln, r, c)) {
               sln[r] = c;
               Place (sln, r + 1, solutions); // Backtrack: Remove last placed queen
               if (r + 1 == N) solutions.Add ([.. sln]); // Base case: r == n --> valid solution
            }
         }
      }

      // No two queen can share column and diagonal
      bool IsValid (int[] sln, int row, int col) {
         for (int i = 0; i < row; i++) {
            int c = sln[i];
            if (c == col || Math.Abs (c - col) == Math.Abs (i - row)) return false;
         }
         return true;
      }
   }
   #endregion

   #region Private --------------------------------------------------
   const int N = 8; // Board size: 8X8
   #endregion
}
#endregion