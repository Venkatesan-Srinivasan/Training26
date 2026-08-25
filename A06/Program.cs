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
   static void Main (string[] args) {
      List<int[]> Solutions = [];
      Solve (sNum, Solutions);
      EliminateIdenticalSlns (Solutions);
      int count = 1;
      foreach (var arr in Solutions) {
         WriteLine ($"\n{count++}");
         ChessBoard (sNum, arr);
      }
   }
   #region Private --------------------------------------------------
   static int sNum = 8;
   #endregion
   #region Implementations ------------------------------------------
   // Solve for distinct solutions
   static void EliminateIdenticalSlns (List<int[]> solutions) {
      List<int[]> unique = [];
      foreach (var sln in solutions) {
         var symmetries = new List<int[]> ();
         // Vertical mirror
         symmetries.Add (sln.Reverse ().ToArray ());
         // Rotations and its vertical mirror
         int[] rot90 = Rotate90 (sln);
         int[] flipRot90 = rot90.Reverse ().ToArray ();
         int[] rot180 = Rotate90 (rot90);
         int[] flipRot180 = rot180.Reverse ().ToArray();
         int[] rot270 = Rotate90 (rot180);
         int[] flipRot270 = rot270.Reverse ().ToArray ();
         symmetries.AddRange (new[] { rot90, rot180, rot270,
                              flipRot90, flipRot180, flipRot270 });
         bool isDuplicate = unique.Any (u => symmetries.Any
                            (sym => u.SequenceEqual (sym)));
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
   static void ChessBoard (int N, int[] boxPattern) {
      OutputEncoding = Encoding.UTF8;
      for (int row = 1; row <= 2 * N + 1; row++) {
         // First line
         if (row == 1)
            WriteLine ($"┌───{PrintPattern ("┬───", N - 1)}┐");
         // Middle lines which are odd lines and not last line
         else if (row % 2 != 0 && row != 2 * N + 1)
            WriteLine ($"├───{PrintPattern ("┼───", N - 1)}┤");
         // Last line
         else if (row == 2 * N + 1)
            WriteLine ($"└───{PrintPattern ("┴───", N - 1)}┘");
         // Queens line
         else {
            for (int col = 1; col <= N; col++) {
               string res = (col == boxPattern[row / 2 - 1] + 1) ?
                  "│ ♕ " : "│   ";
               Write (res);
            }
            Write ("│ \n");
         }
      }
      // Print the given pattern for n times
      string PrintPattern (string input, int n) {
         string result = "";
         for (int i = 0; i < n; i++) result += input;
         return result;
      }
   }

   //  Solves for identical solutions
   static void Solve (int n, List<int[]> solutions) {
      int[] board = new int[n];
      Place (board, 0, n, solutions);
      bool Place (int[] board, int r, int n, List<int[]> solutions) {
         if (r == n) {
            solutions.Add ((int[])board.ToArray ());
            return false;
         }
         // Choices: one queen per column in each row
         for (int col = 0; col < n; col++) {
            if (Safe (board, r, col)) {
               board[r] = col;
               // Backtrack: Remove last placed queen
               Place (board, r + 1, n, solutions);
            }
         }
         return false;
      }
      // No two queen can share column and diagonal
      bool Safe (int[] board, int row, int col) {
         for (int r = 0; r < row; r++) {
            int c = board[r];
            if (c == col || Math.Abs (c - col) == Math.Abs (r - row))
               return false;
         }
         return true;
      }
   }
   #endregion
}
#endregion