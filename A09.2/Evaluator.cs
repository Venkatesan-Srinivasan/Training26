using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Eval;

class EvalException : Exception {
   public EvalException (string message) : base (message) { }
}
class Evaluator {
   #region Properties -----------------------------------------------
   public int BasePriority { get; set; }
   #endregion

   #region Methods --------------------------------------------------
   public double Evaluate (string text) {
      Reset ();
      List<Token> tokens = []; //the number of tokens is  not known
      var tokenizer = new Tokenizer (this, text);
      for (; ; ) {
         var token = tokenizer.Next ();
         if (token is TEnd) break;
         if (token is TError err) throw new EvalException (err.Message);
         tokens.Add (token);
      }
      TVariable? tVariable = null;
      if (tokens.Count > 2 && tokens[0] is TVariable tvar && tokens[1] is TOpBinary { Op: '=' }) {
         tVariable = tvar;
         tokens.RemoveRange (0, 2);
      }
      TUnary? tUnary = null;
      if (tokens.Count > 1 && tokens[0] is TUnary tU && tokens[1] is TUnary { : '+' or '-' }) {
         tVariable = tvar;
         tokens.RemoveRange (0, 2);
      }
      foreach (var t in tokens) Process (t);
      while (mOperators.Count > 0) ApplyOperator ();
      if (BasePriority != 0) throw new EvalException ("Mismatched parentheses");
      if (mOperators.Count > 0) throw new EvalException ("Too many operators");
      if (mOperands.Count > 1) throw new EvalException ("Too many operators");
      double f = mOperands.Pop ();
      if (tVariable != null) mVars[tVariable.Name] = f;
      return f;
   }

   public double GetVariable (string name) {
      if (mVars.TryGetValue (name, out double value)) return value;
      throw new EvalException ($"Unknown variable: {name}");
   }
   #endregion

   #region Implementations ------------------------------------------
   void ApplyOperator () {
      var op = mOperators.Pop ();
      var f1 = mOperands.Pop ();
      if (op is TOpFunction func) {
         if (mOperands.Count < 1) throw new EvalException ("Too few operators");
         mOperands.Push (func.Apply (f1));
      } else if (op is TOpBinary bin) {
         if (mOperands.Count < 1) throw new EvalException ("Too few operands");
         var f2 = mOperands.Pop ();
         mOperands.Push (bin.Apply (f2, f1));
      }
   }
   bool OkToPush (TOperator op) {
      if (mOperators.Count == 0) return true;
      var top = mOperators.Peek ();
      return op.FinalPriority > top.FinalPriority;
   }
   void Process (Token token) {
      switch (token) {
         case TNumber num:
            mOperands.Push (num.Value);
            break;
         case TOperator op:
            op.FinalPriority = op.Priority + BasePriority;
            while (!OkToPush (op)) ApplyOperator ();
            mOperators.Push (op);
            break;
         case TPunctuation p:
            BasePriority += p.Punct == '(' ? 10 : -10;
            break;
         default: throw new EvalException ($"Unexpected token: {token}");
      }
   }
   void Reset () {
      mOperators.Clear ();
      mOperands.Clear ();
      BasePriority = 0;
   }
   #endregion

   #region Private --------------------------------------------------
   readonly Stack<double> mOperands = [];
   readonly Stack<TOperator> mOperators = [];
   readonly Dictionary<string, double> mVars = [];
   #endregion
}