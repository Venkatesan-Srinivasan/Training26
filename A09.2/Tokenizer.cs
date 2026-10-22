// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Trumpf Metamation India.
// ------------------------------------------------------------------------------------------------
// Tokenizer.cs
// A09.2: Expression Evaluator
// Converts input text into tokens and identifies unary minus operators
// ------------------------------------------------------------------------------------------------

using Eval;

class Tokenizer {
   #region Constructor ----------------------------------------------
   public Tokenizer (Evaluator eval, string text) {
      _evaluator = eval;
      _position = 0;
      _text = text;
   }
   #endregion

   #region Method ---------------------------------------------------
   public Token Next () {
      while (_position < _text.Length) {
         char ch = char.ToLower (_text[_position++]);
         switch (ch) {
            case (>= '0' and <= '9') or '.': return Remember (GetNumber ());
            case '(' or ')': return Remember (new TPunctuation (ch));
            case '-': return GetMinus (ch);
            case '+' or '*' or '/' or '^' or '=':
               return Remember (new TOpBinary (_evaluator, ch.ToString ()));
            case >= 'a' and <= 'z': return Remember (GetIdentifier ());
            default: return new TError ($"Unknown symbol: {ch}");
         }
      }
      return new TEnd ();
   }
   #endregion

   #region Implementations ------------------------------------------
   Token GetIdentifier () {
      int start = _position - 1;
      while (_position < _text.Length) {
         char ch = char.ToLower (_text[_position++]);
         if (ch is >= 'a' and <= 'z') continue;
         _position--;
         break;
      }
      string sub = _text[start.._position];
      if (_functions.Contains (sub)) return new TOpFunction (_evaluator, sub);
      else return new TVariable (_evaluator, sub);
   }

   Token GetMinus (char ch) {
      Token token;
      token = (IsUnaryContext ()) ?
         new TUnary (_evaluator, ch.ToString ()) : new TOpBinary (_evaluator, ch.ToString ());
      return Remember (token);
   }

   Token GetNumber () {
      int start = _position - 1;
      while (_position < _text.Length) {
         char ch = _text[_position++];
         if (ch is (>= '0' and <= '9') or '.') continue;
         _position--; break;
      }
      string sub = _text[start.._position];
      if (double.TryParse (sub, out double number)) return new TLiteral (number);
      return new TError ($"Invalid number:{sub}");
   }

   bool IsUnaryContext () {
      return _previousToken == null ||
             _previousToken is TOperator ||
             _previousToken is TPunctuation { Punct: '(' };
   }
   Token Remember (Token token) => _previousToken = token;
   #endregion

   #region Private --------------------------------------------------
   readonly Evaluator _evaluator;
   readonly string[] _functions = ["sin", "cos", "tan", "asin", "acos", "atan", "log", "exp", "sqrt"];
   int _position;
   Token? _previousToken;
   readonly string _text;
   #endregion
}