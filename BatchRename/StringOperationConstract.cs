using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class StringArgs
    {

    }

    public class ReplaceArgs : StringArgs
    {
        public string From { get; set; }
        public string To { get; set; }
    }

    public class NewCaseArgs : StringArgs
    {
    }

    public class FullnameNormalizeArg : StringArgs
    {

    }

    public class MoveArgs : StringArgs
    {
        public int Start { get; set; }
        public int End { get; set; }
        // variable to check user want to move cut string to before or end of stringInput
        public bool Before { get; set; }

    }


    public class UniqueName : StringArgs
    {

    }


    abstract class StringOperation
    {
        public StringArgs Args { get; set; }
        public abstract string Operate(string origin);
    }

    class ReplaceOperation : StringOperation
    {

        public override string Operate(string origin)
        {
            var args = Args as ReplaceArgs;
            var from = args.From;
            var to = args.To;

            return origin.Replace(from, to);
        }
    }

    class StringUpperOperation : StringOperation
    {
        public override string Operate(string origin)
        {
            var args = Args as NewCaseArgs;
            return origin.ToUpper();
        }
    }

    class StringLowerOperation : StringOperation
    {
        public override string Operate(string origin)
        {
            var args = Args as NewCaseArgs;
            return origin.ToLower();
        }
    }


    class StringNormalizeNewCaseOperation : StringOperation
    {
        public override string Operate(string origin)
        {
            var args = Args as NewCaseArgs;
            string result = "";
            result = origin.ToLower();

            char[] letters = result.ToCharArray();

            letters[0] = char.ToUpper(letters[0]);
            for (int i = 0; i < letters.Length - 1; i++)
            {
                if (letters[i] == ' ' && letters[i + 1] != ' ')
                {
                    letters[i + 1] = char.ToUpper(letters[i + 1]);
                }
            }

            result = new string(letters);
            return result;
        }
    }

    class FullnameNormalizeOperation : StringOperation
    {
        public override string Operate(string origin)
        {

            var args = Args as FullnameNormalizeArg;
            string result = "";
            string input = "";
            // remove space in first and last of string
            input = origin.Trim();

            // replace two space to one space
            while (input.IndexOf("  ") != -1)
            {
                input = input.Replace("  ", " ");
            }
            input = input.ToLower();

            string[] subName = input.Split(' ');
            for (int i = 0; i < subName.Length; i++)
            {
                string fistCharacter = subName[i].Substring(0, 1).ToUpper();
                string otherCharacter = subName[i].Substring(1);
                result += fistCharacter + otherCharacter + " ";
            }

            return result;
        }
    }


    /// <summary>
    /// example string have format ISBN FileName, move ISBN to before FileName or opposite 
    /// </summary>
    class MoveOperation : StringOperation
    {
        public override string Operate(string origin)
        {
            var args = Args as MoveArgs;

            string input = origin;
            var start = args.Start;
            var end = args.End;
            var before = args.Before;

            string firstString = input.Substring(start, end).Trim();
            string secondString = input.Substring(end).Trim();
            string result = "";

            if (before == true)
            {
                result = firstString + " " + secondString;
            }
            else
            {
                result += secondString + " " + firstString;
            }

            return result;
        }
    }

    class UniqueNameOperation : StringOperation
    {
        public override string Operate(string origin)
        {
            string result = "";
            Guid id;
            id = Guid.NewGuid();
            result += id;
            return result;
        }
    }


}
