using System;
using System.Collections.Generic;
using System.Text;
using Eval;

class Tokenizer {
   #region Constructor ----------------------------------------------
   public Tokenizer (Evaluator eval, string text) {
      mText = text;
      mN = 0;
      mEval = eval;
   }
   #endregion

   #region Method ---------------------------------------------------
   public Token Next () {
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         switch (ch) {
            case ' ' or '\t': continue;
            case (>= '0' and <= '9') or '.': return GetNumber ();
            case '(' or ')': return new TPunctuation (ch);
            case '+' or '-' or '*' or '/' or '^' or '=': return new TOpBinary (mEval, ch);
            case >= 'a' and <= 'z': return GetIdentifier ();
            default: return new TError ($"Unknown symbol: {ch}");
         }
      }
      return new TEnd ();
   }
   #endregion

   #region Implementations ------------------------------------------
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
   #endregion

   #region Private --------------------------------------------------
   readonly Evaluator mEval;
   readonly string[] mFuncs = { "sin", "cos", "tan", "asin", "acos", "atan", "log", "exp", "sqrt" };
   readonly string mText;
   int mN;
   #endregion
}