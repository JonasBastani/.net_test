static string GenerateFibonacciSequence(int quantity)
{
    string sequency = "";

    for (int i = 0; i < quantity; i++)
    {
        sequency += Fibonacci(i);

        if (i < quantity - 1)
            sequency += " ";
    }

    return sequency;
}

static int Fibonacci(int n)
{
    if (n == 0 || n == 1)
        return n;

    return Fibonacci(n - 1) + Fibonacci(n - 2);
}