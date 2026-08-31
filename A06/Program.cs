// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// A06: 8 Queens problem
// Program to solve the 8 Queens problem. On a standard chessboard, place 8 queens so that no queen
// can attack another. Thus, no two queens may share the same row, column, or diagonal.
// The solution set must contain only distinct solutions,
// meaning no solution is identical to another by:
//   - 90°, 180°, or 270° rotation
//   - mirror image (horizontal or vertical)
// ------------------------------------------------------------------------------------------------
using static System.Console;
using System.Text;

namespace A06;
#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      List<int[]> solutions = [];
      Solve (solutions);
      EliminateIdentical (solutions);
      int count = 1;
      foreach (var sln in solutions) {
         WriteLine ($"\n{count++} of {solutions.Count}");
         PrintChessBoard (sln);
      }
   }

   #region Implementations ------------------------------------------
   // Solve for distinct solutions
   static void EliminateIdentical (List<int[]> solutions) {
      List<int[]> unique = [];
      foreach (var sln in solutions) {
         var symmetries = new List<int[]> {
            ([.. sln.Reverse ()]) // Vertical mirror
         };
         // Rotations and its vertical mirror
         int[] rot90 = Rotate90 (sln), vFlipRot90 = [.. rot90.Reverse ()],
               rot180 = Rotate90 (rot90), vFlipRot180 = [.. rot180.Reverse ()],
               rot270 = Rotate90 (rot180), vFlipRot270 = [.. rot270.Reverse ()];
         symmetries.AddRange ([rot90, rot180, rot270, vFlipRot90, vFlipRot180, vFlipRot270]);
         bool isDuplicate = unique.Any (u => symmetries.Any (a => u.SequenceEqual (a)));
         if (isDuplicate)
            continue;
         unique.Add (sln);
      }
      solutions.Clear ();
      solutions.AddRange (unique);

      // Rotate by 90
      int[] Rotate90 (int[] arr) {
         int[] rotated = new int[N];
         for (int i = 0; i < N; i++)
            rotated[arr[i]] = N - 1 - i;
         return rotated;
      }
   }

   // Prints each row to form a standard chessboard
   static void PrintChessBoard (int[] sln) {
      OutputEncoding = Encoding.UTF8;
      for (int row = 0, len = 2 * N + 1; row < len; row++) {
         if (row == 0) // First line
            WriteLine ($"┌───{GetPattern ("┬───")}┐");
         // Middle lines which are even lines and not last line
         else if (row % 2 == 0 && row != 2 * N)
            WriteLine ($"├───{GetPattern ("┼───")}┤");
         else if (row == 2 * N) // Last line
            WriteLine ($"└───{GetPattern ("┴───")}┘");
         // Queens line- odd lines [1,3,5,7..[/2 makes to access the array [0,1,2,3,..] of solution
         else {
            for (int col = 0; col < N; col++) {
               string res = (col == sln[row / 2]) ? "│ ♕ " : "│   ";
               Write (res);
            }
            Write ("│ \n");
         }
      }

      // Print the given pattern for n times
      string GetPattern (string pattern) {
         string str = "";
         for (int i = 0, len = N - 1; i < len; i++) str += pattern;
         return str;
      }
   }

   //  Solves for identical solutions
   static void Solve (List<int[]> solutions) {
      int[] sln = new int[N];
      Place (sln, 0, solutions);

      // Recursive backtracking method to place queens row by row on the board
      void Place (int[] sln, int r, List<int[]> solutions) {
         // Choices: one queen per column in each row
         for (int c = 0; c < N; c++) {
            if (IsSafe (sln, r, c)) {
               sln[r] = c;
               Place (sln, r + 1, solutions); // Backtrack: Remove last placed queen
               // Base case: if all rows are filled (r == n), we found a valid solution
               if (r + 1 == N) solutions.Add ([.. sln]);
            }
         }
      }

      // No two queen can share column and diagonal
      bool IsSafe (int[] sln, int row, int col) {
         for (int i = 0; i < row; i++) {
            int c = sln[i];
            if (c == col || Math.Abs (c - col) == Math.Abs (i - row))
               return false;
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