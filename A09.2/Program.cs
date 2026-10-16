// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// A09.2: Expression Evaluator
// Program to implement unary minus for the expression evaluator
// ------------------------------------------------------------------------------------------------

namespace Eval;

class Program {
   static void Main () {
      var eval = new Evaluator ();
      for (; ; ) {
         Console.Write (">");
         string text = Console.ReadLine () ?? "";
         if (text == "exit") break;
         try {
            double result = eval.Evaluate (text);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine (result);
         } catch (Exception e) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine (e.Message);
         }
         Console.ResetColor ();
      }
   }
}
