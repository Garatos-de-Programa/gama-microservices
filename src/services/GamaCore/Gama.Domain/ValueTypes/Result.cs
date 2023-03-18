namespace Gama.Domain.ValueTypes;

public struct Result<T>
{
    public bool IsFaulted { get; }

    private readonly T? _value;
    private Exception? _error;
    
    public Result(T value)
    {
        IsFaulted = false;
        _value = value;
        _error = null;
    }

    public Result(Exception error)
    {
        IsFaulted = true;
        _error = error;
    }

    public static implicit operator Result<T>(T value) =>
        new(value);

    public TR Match<TR>(Func<T, TR> success, Func<Exception, TR> fail) =>
        IsFaulted ? fail(_error) : success(_value);
}