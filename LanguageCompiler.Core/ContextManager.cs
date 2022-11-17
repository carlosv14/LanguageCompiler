namespace ClassLibrary1;

public static class ContextManager
{
    private static readonly List<Context> _contexts = new();

    public static Context Push()
    {
        var context = new Context();
        _contexts.Add(context);
        return context;
    }

    public static Context Pop()
    {
        var lastContext =  _contexts.Last();
        _contexts.Remove(lastContext);
        return lastContext;
    }

    public static void Put(string lexeme, IdExpression id) => _contexts.Last().Put(lexeme, id);

    public static Symbol Get(string lexeme)
    {
        for (var i = _contexts.Count - 1; i >= 0; i--)
        {
            var symbol = _contexts.ElementAt(i).Get(lexeme);
            if (symbol != null)
            {
                return symbol;
            }
        }

        throw new ApplicationException($"Symbol {lexeme} was not found in current context");
    }
}

public class Context
{
    private readonly Dictionary<string, Symbol> _symbolTable;

    public Context()
    {
        this._symbolTable = new();
    }

    public void Put(string lexeme, IdExpression id)
    {
        if (_symbolTable.ContainsKey(lexeme))
        {
            throw new ApplicationException(
                $"A local symbol named '{lexeme}' cannot be declared in this scope because a symbol with the same already exists");
        }
        _symbolTable.Add(lexeme, new Symbol(id));
    }

    public Symbol Get(string lexeme)
    {
        return _symbolTable.TryGetValue(lexeme, out var value) ? value : null;
    }
}