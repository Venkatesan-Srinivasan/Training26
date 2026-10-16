using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Eval;

class EvalException : Exception {
   public EvalException (string message) : base (message) { }
}
class Evaluator {
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
      if (tokens.Count > 2 && tokens[0] is TVariable tvar && tokens[1] is TOpArithmetic { Op: '=' }) {
         tVariable = tvar;
         tokens.RemoveRange (0, 2);
      }
      foreach (var t in tokens) Process (t);
      while (mOperators.Count > 0) ApplyOperator ();
      double f = mOperands.Pop ();
      if (tVariable != null) mVars[tVariable.Name] = f;
      return f;
   }
   public int BasePriority { get; set; }
   public void Reset () {
      mOperators.Clear ();
      mOperands.Clear ();
      BasePriority = 0;
   }
   public double GetVariable (string name) {
      if (mVars.TryGetValue (name, out double value)) return value;
      throw new EvalException ($"Unknown variable: {name}");
   }
   readonly Dictionary<string, double> mVars = [];

   void Process (Token token) {
      switch (token) {
         case TNumber num:
            mOperands.Push (num.Value);
            break;
         case TOperator op:
            while (mOperators.Count > 0 && mOperators.Peek ().Priority > op.Priority) ApplyOperator ();
            mOperators.Push (op);
            break;
         case TPunctuation p:
            BasePriority += p.Punct == '(' ? 10 : -10;
            break;
         default: throw new EvalException ($"Unexpected token: {token}");
      }
   }
   readonly Stack<double> mOperands = [];
   readonly Stack<TOperator> mOperators = [];
   void ApplyOperator () {
      var op = mOperators.Pop ();
      var f1 = mOperands.Pop ();
      if (op is TOpFunction func) mOperands.Push (func.Evaluate (f1));
      else if (op is TOpArithmetic arith) {
         var f2 = mOperands.Pop ();
         mOperands.Push (arith.Evaluate (f2, f1));
      }
   }
}
class Tokenizer {
   public Tokenizer (Evaluator eval, string text) {
      mText = text;
      mN = 0;
      mEval = eval;

      // Tokenization logic goes here
   }
   readonly Evaluator mEval;  // The evaluator that owns this
   readonly string mText;     // The input text we're parsing through
   int mN;

   public Token Next () {
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         switch (ch) {
            case ' ' or '\t': continue;
            case (>= '0' and <= '9') or '.': return GetNumber ();
            case '(' or ')': return new TPunctuation (ch);
            case '+' or '-' or '*' or '/' or '^' or '=': return new TOpArithmetic (mEval, ch);
            case >= 'a' and <= 'z': return GetIdentifier ();
            default: return new TError ($"Unknown symbol: {ch}");
         }
      }
      return new TEnd ();
   }

   Token GetIdentifier () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         if (ch is >= 'a' and <= 'z' or >= '0' and <= '9') continue;
         mN--;
         break;
      }
      string sub = mText[start..mN];
      if (mFuncs.Contains (sub)) return new TOpFunction (mEval, sub);
      else return new TVariable (mEval, sub);
   }
   readonly string[] mFuncs = { "sin", "cos", "tan", "asin", "acos", "atan", "log", "exp", "sqrt" };
   Token GetNumber () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = mText[mN++];
         if (ch is (>= '0' and <= '9') or '.') continue;
         mN--; break;
      }
      string sub = mText[start..mN];
      if (double.TryParse (sub, out double number)) return new TLiteral (number);
      return new TError ($"Invalid number:{sub}");
   }

}

