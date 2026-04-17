namespace StudentEXE.Model;

internal class Logger
{
    public const int MaxEntries = 5;
    private static Logger? _instance = null;

    public event Action<string[]>? OnLog;
    public event Action<string>? OnMessage;

    private string _lastMessage = string.Empty;
    private List<string> _entries = [];
    public int Counter { get; private set; } = 0;
    public static Logger Instance => _instance ??= new Logger();
    public string[] Entries => _entries.ToArray();

    private Logger() { }

    public static void ResetLogger()
    {
        _instance = null;
    }

    public void Log(string text)
    {
        if (_entries.Count >= MaxEntries)
        {
            _entries.RemoveAt(_entries.Count - 1);
        }

        Counter++;
        _entries.Insert(0, text);
        OnLog?.Invoke(_entries.ToArray());
    }

    public void Message(string text)
    {
        _lastMessage = text;
        OnMessage?.Invoke(text);
    }
}
