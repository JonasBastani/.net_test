using System.Text.RegularExpressions;
static string RemoveRepeatedQuestionAndExclamation(string text)
{
    text = Regex.Replace(text, @"\?+", "?");
    text = Regex.Replace(text, @"!+", "!");

    return text;
}