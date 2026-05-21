static bool IsPalindrome(string text)
{
    text = text.ToUpperInvariant();
    text = Regex.Replace(text, @"[^\p{L}\p{N}]", "");

    int start = 0;
    int end = text.Length - 1;

    while (start < end)
    {
        if (text[start] != text[end])
            return false;

        start++;
        end--;
    }

    return true;
}