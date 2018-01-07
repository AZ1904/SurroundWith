using System;
using System.Text;

namespace SurroundWith.Commands
{
    internal class ToggleCommands
    {
        private readonly string _newLine = Environment.NewLine;

        public string ToggleCurlyBraces(string textToToggleAround) => ToggleDelimiter(textToToggleAround, "{", "}");

        public string ToggleParenthesis(string textToToggleAround) => ToggleDelimiter(textToToggleAround, "(", ")");

        public string ToggleSquareBrackets(string textToToggleAround) => ToggleDelimiter(textToToggleAround, "[", "]");

        public string ToggleAngleBrackets(string textToToggleAround) => ToggleDelimiter(textToToggleAround, "<", ">");

        public string ToggleSingleQuotes(string textToToggleAround) => ToggleDelimiter(textToToggleAround, "'", "'");

        public string ToggleDelimiter(string textToToggleAround, string startingDelimiter, string endingDelimiter, bool newLineAfterDelimiter = false)
        {
            if (string.IsNullOrEmpty(textToToggleAround)) return textToToggleAround;

            //todo: can be made smarter, perhaps use snippets if you can
            if (textToToggleAround.StartsWith(startingDelimiter) && textToToggleAround.EndsWith(endingDelimiter))
            {
                textToToggleAround = textToToggleAround.Substring(1, textToToggleAround.Length - 2);
            }
            else
            {
                textToToggleAround = newLineAfterDelimiter ?
                    ApplySeparateLineDelimiter(textToToggleAround, startingDelimiter, endingDelimiter) :
                    $"{startingDelimiter}{textToToggleAround}{endingDelimiter}";
            }
            return textToToggleAround;
        }

        private string ApplySeparateLineDelimiter(string textToToggleAround, string startingDelimiter, string endingDelimiter)
        {
            var code = new StringBuilder(textToToggleAround);
            code.Replace(_newLine, _newLine + '\t');
            return $"{startingDelimiter}{_newLine}{code}{_newLine}{endingDelimiter}";
        }
    }
}