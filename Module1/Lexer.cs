using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lexer
{

    public class LexerException : System.Exception
    {
        public LexerException(string msg)
            : base(msg)
        {
        }

    }

    public class Lexer
    {

        protected int position;
        protected char currentCh; // ��������� ��������� ������
        protected int currentCharValue; // ����� �������� ���������� ���������� �������
        protected System.IO.StringReader inputReader;
        protected string inputString;

        public Lexer(string input)
        {
            inputReader = new System.IO.StringReader(input);
            inputString = input;
        }

        public void Error()
        {
            System.Text.StringBuilder o = new System.Text.StringBuilder();
            o.Append(inputString + '\n');
            o.Append(new System.String(' ', position - 1) + "^\n");
            o.AppendFormat("Error in symbol {0}", currentCh);
            throw new LexerException(o.ToString());
        }

        protected void NextCh()
        {
            this.currentCharValue = this.inputReader.Read();
            this.currentCh = (char) currentCharValue;
            this.position += 1;
        }

        public virtual bool Parse()
        {
            return true;
        }
    }

    public class IntLexer : Lexer
    {

        protected System.Text.StringBuilder intString;
        public int parseResult = 0;
		Boolean minus = false;

        public IntLexer(string input)
            : base(input)
        {
            intString = new System.Text.StringBuilder();
        }

        public override bool Parse()
        {
            NextCh();
            if (currentCh == '+' || currentCh == '-')
            {
				if (currentCh == '-')
				{
					minus = true;
				}

                NextCh();
            }
        
            if (char.IsDigit(currentCh))
            {
				parseResult = parseResult * 10 + int.Parse(currentCh.ToString());
				NextCh();
            }
            else
            {
                Error();
            }

            while (char.IsDigit(currentCh))
            {
				parseResult = parseResult * 10 + int.Parse(currentCh.ToString());
				NextCh();
            }


            if (currentCharValue != -1)
            {
                Error();
            }

			if (minus == true)
			{
				parseResult *= -1;
			}

            return true;

        }
    }
    
    public class IdentLexer : Lexer
    {
        private string parseResult;
        protected StringBuilder builder;
    
        public string ParseResult
        {
            get { return parseResult; }
        }
    
        public IdentLexer(string input) : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        { 
            throw new NotImplementedException();
        }
       
    }

    public class IntNoZeroLexer : IntLexer
    {
        public IntNoZeroLexer(string input)
            : base(input)
        {
        }

        public override bool Parse()
        {
            throw new NotImplementedException();
        }
    }

    public class LetterDigitLexer : Lexer
    {
        protected StringBuilder builder;
        protected string parseResult;

        public string ParseResult
        {
            get { return parseResult; }
        }

        public LetterDigitLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        {
            throw new NotImplementedException();
        }
       
    }

    public class LetterListLexer : Lexer
    {
        protected List<char> parseResult;

        public List<char> ParseResult
        {
            get { return parseResult; }
        }

        public LetterListLexer(string input)
            : base(input)
        {
            parseResult = new List<char>();
        }

        public override bool Parse()
        {
            throw new NotImplementedException();
        }
    }

    public class DigitListLexer : Lexer
    {
        protected List<int> parseResult;

        public List<int> ParseResult
        {
            get { return parseResult; }
        }

        public DigitListLexer(string input)
            : base(input)
        {
            parseResult = new List<int>();
        }

        public override bool Parse()
        {
            throw new NotImplementedException();
        }
    }

    public class LetterDigitGroupLexer : Lexer
    {
        protected StringBuilder builder;
        protected string parseResult;

        public string ParseResult
        {
            get { return parseResult; }
        }
        
        public LetterDigitGroupLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        {
            throw new NotImplementedException();
        }
       
    }

    public class DoubleLexer : Lexer
    {
        private StringBuilder builder;
        private double parseResult;

		double res = 0;
		bool minus = false;
		int digits = 0;

		public double ParseResult
        {
            get { return parseResult; }

        }

        public DoubleLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        {
			NextCh();

			if (currentCharValue == -1)
				Error();

			if (!(char.IsDigit(currentCh) || currentCh == '+' || currentCh == '-'))
				Error();

			if (currentCh == '+' || currentCh == '-')
			{
				minus = currentCh == '-';
				NextCh();
			}

			while (char.IsDigit(currentCh))
			{
				res = res * 10 + (int)currentCh - (int)'0';
				NextCh();
			}

			if (currentCharValue == -1)
			{
				if (minus)
					res *= -1;
				parseResult = res;
				return true;
			}

			else if (currentCh != '.')
			{
				Error();
			}

			NextCh();
			digits++;

			if (currentCharValue == -1)
				Error();

			while (char.IsDigit(currentCh))
			{
				res += ((int)currentCh - (int)'0') / Math.Pow(10, digits);
				NextCh();
			}

			if (currentCharValue != -1)
			{
				Error();
			}

			if (minus)
				res *= -1;

			parseResult = res;


			return true;

		
	}
       
    }

    public class StringLexer : Lexer
    {
        private StringBuilder builder;
        private string parseResult;

        public string ParseResult
        {
            get { return parseResult; }

        }

        public StringLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        {
            throw new NotImplementedException();
        }
    }

    public class CommentLexer : Lexer
    {
        private StringBuilder builder;
        private string parseResult;

        public string ParseResult
        {
            get { return parseResult; }

        }

        public CommentLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        {
            throw new NotImplementedException();
        }
    }

    public class IdentChainLexer : Lexer
    {
        private StringBuilder builder;
        private List<string> parseResult;

        public List<string> ParseResult
        {
            get { return parseResult; }

        }

        public IdentChainLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
            parseResult = new List<string>();
        }

        public override bool Parse()
        {
            throw new NotImplementedException();
        }
    }

    public class Program
    {
        public static void Main()
        {
            string input = "154216";
            Lexer L = new IntLexer(input);
            try
            {
                L.Parse();
            }
            catch (LexerException e)
            {
                System.Console.WriteLine(e.Message);
            }

        }
    }
}