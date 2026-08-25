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
      Solve (sNum, solutions);
      EliminateIdenticalSlns (solutions);
      int count = 1;
      foreach (var solution in solutions) {
         WriteLine ($"\n{count++}");
         ChessBoard (sNum, solution);
      }
   }

   #region Implementations ------------------------------------------
   // Solve for distinct solutions
   static void EliminateIdenticalSlns (List<int[]> solutions) {
      List<int[]> unique = [];
      foreach (var sln in solutions) {
         var symmetries = new List<int[]> {
            ([.. sln.Reverse ()]) // Vertical mirror
         };
         // Rotations and its vertical mirror
         int[] rot90 = Rotate90 (sln);
         int[] flipRot90 = rot90.Reverse ().ToArray ();
         int[] rot180 = Rotate90 (rot90);
         int[] flipRot180 = rot180.Reverse ().ToArray ();
         int[] rot270 = Rotate90 (rot180);
         int[] flipRot270 = rot270.Reverse ().ToArray ();
         symmetries.AddRange (new[] { rot90, rot180, rot270, flipRot90, flipRot180, flipRot270 });
         bool isDuplicate = unique.Any (u => symmetries.Any (sym => u.SequenceEqual (sym)));
         if (!isDuplicate)
            unique.Add (sln);
      }
      solutions.Clear ();
      solutions.AddRange (unique);

      // Rotate by 90
      int[] Rotate90 (int[] arr) {
         int[] rotated = new int[sNum];
         for (int j = 0; j < sNum; j++)
            rotated[arr[j]] = sNum - 1 - j;
         return rotated;
      }
   }

   // Prints each row to form a standard chessboard
   static void ChessBoard (int n, int[] solution) {
      OutputEncoding = Encoding.UTF8;
      for (int row = 1; row <= 2 * n + 1; row++) {
         if (row == 1) // First line
            WriteLine ($"┌───{PrintPattern ("┬───", n - 1)}┐");
         // Middle lines which are odd lines and not last line
         else if (row % 2 != 0 && row != 2 * n + 1)
            WriteLine ($"├───{PrintPattern ("┼───", n - 1)}┤");
         else if (row == 2 * n + 1) // Last line
            WriteLine ($"└───{PrintPattern ("┴───", n - 1)}┘");
         // Queens line
         else {
            for (int col = 1; col <= n; col++) {
               string res = (col == solution[row / 2 - 1] + 1) ? "│ ♕ " : "│   ";
               Write (res);
            }
            Write ("│ \n");
         }
      }

      // Print the given pattern for n times
      string PrintPattern (string pattern, int n) {
         string str = "";
         for (int i = 0; i < n; i++) str += pattern;
         return str;
      }
   }

   //  Solves for identical solutions
   static void Solve (int n, List<int[]> solutions) {
      int[] board = new int[n];
      Place (board, 0, n, solutions);

      // Recursive backtracking method to place queens row by row on the board
      void Place (int[] board, int r, int n, List<int[]> solutions) {
         // Base case: if all rows are filled (r == n), we found a valid solution
         if (r == n) {
            solutions.Add ([.. board]);
            return;
         }
         // Choices: one queen per column in each row
         for (int col = 0; col < n; col++) {
            if (IsSafe (board, r, col)) {
               board[r] = col;
               Place (board, r + 1, n, solutions); // Backtrack: Remove last placed queen
            }
         }
         return;
      }

      // No two queen can share column and diagonal
      bool IsSafe (int[] board, int row, int col) {
         for (int i = 0; i < row; i++) {
            int c = board[i];
            if (c == col || Math.Abs (c - col) == Math.Abs (i - row))
               return false;
         }
         return true;
      }
   }
   #endregion

   #region Private --------------------------------------------------
   static int sNum = 8;
   #endregion
}
#endregion