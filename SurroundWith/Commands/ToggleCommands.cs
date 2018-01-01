namespace SurroundWith.Commands
{
    internal class ToggleCommands
    {
        public string ToggleCurlyBraces(string textToToggleAround) => ToggleDelimiter(textToToggleAround, "{", "}");

        public string ToggleParenthesis(string textToToggleAround) => ToggleDelimiter(textToToggleAround, "(", ")");

        public string ToggleSquareBrackets(string textToToggleAround) => ToggleDelimiter(textToToggleAround, "[", "]");

        public string ToggleAngleBrackets(string textToToggleAround) => ToggleDelimiter(textToToggleAround, "<", ">");

        public string ToggleSingleQuotes(string textToToggleAround) => ToggleDelimiter(textToToggleAround, "'", "'");

        public string ToggleDelimiter(string textToToggleAround, string startingDelimiter, string endingDelimiter)
        {
            if (string.IsNullOrEmpty(textToToggleAround)) return textToToggleAround;

            //todo: can be made smarter, perhaps use snippets if you can
            if (textToToggleAround.StartsWith(startingDelimiter) && textToToggleAround.EndsWith(endingDelimiter))
            {
                textToToggleAround = textToToggleAround.Substring(1, textToToggleAround.Length - 2);
            }
            else
            {
                textToToggleAround = $"{startingDelimiter}{textToToggleAround}{endingDelimiter}";
            }
            return textToToggleAround;
        }
    }
}