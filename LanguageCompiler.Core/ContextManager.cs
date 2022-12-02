namespace ClassLibrary1;

public static class ContextManager
{
    private static readonly List<Context> _contexts = new();
    private static readonly List<Context> _contextsForCodeGeneration = new();

    public static Context Push()
    {
        var context = new Context();
        _contexts.Add(context);
        _contextsForCodeGeneration.Add(context);
        return context;
    }

    public static Context Pop()
    {
        var lastContext =  _contexts.Last();
        _contexts.Remove(lastContext);
        return lastContext;
    }

    public static void Put(string lexeme, IdExpression id) => _contexts.Last().Put(lexeme, id);

    public static void UpdateSymbol(string lexeme, dynamic value)
    {
        for (var i = _contextsForCodeGeneration.Count - 1; i >= 0; i--)
        {
            var symbol = _contextsForCodeGeneration.ElementAt(i).Get(lexeme);
            if (symbol != null)
            {
                _contextsForCodeGeneration[i].UpdateSymbolValue(lexeme, value) ;
            }
        }
    }

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
    
    public static Symbol GetSymbolForInterpretation(string lexeme)
    {
        for (var i = _contextsForCodeGeneration.Count - 1; i >= 0; i--)
        {
            var symbol = _contextsForCodeGeneration.ElementAt(i).Get(lexeme);
            if (symbol != null)
            {
                return symbol;
            }
        }

        throw new ApplicationException($"Symbol {lexeme} was not found in current context");
    }

    public static IEnumerable<Symbol> GetSymbolsForCurrentContext()
    {
        if (!_contextsForCodeGeneration.Any())
        {
            return Enumerable.Empty<Symbol>();
        }

        var last = _contextsForCodeGeneration.First();
        var symbols = last.GetSymbolsForCurrentContext();
        _contextsForCodeGeneration.Remove(last);
        return symbols;
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

        if (id.Type is ArrayType arrayType)
        {
            _symbolTable.Add(lexeme, new Symbol(id, new dynamic[arrayType.Size]));
        }
        else
        {
            _symbolTable.Add(lexeme, new Symbol(id));
        }
    }

    public Symbol Get(string lexeme)
    {
        return _symbolTable.TryGetValue(lexeme, out var value) ? value : null;
    }

    public IEnumerable<Symbol> GetSymbolsForCurrentContext() =>
        this._symbolTable.Select(x => x.Value);

    public void UpdateSymbolValue(string lexeme, dynamic value)
    {
        var symbol = Get(lexeme);
        symbol.Value = value;
        _symbolTable[lexeme] = symbol;
    }
}