using System.Text;

namespace StudentEXE.View;

internal class InstructionView : RendererBase
{
    private static readonly string[] _logo =
    [
        "███████╗████████╗██╗   ██╗██████╗ ███████╗███╗   ██╗████████╗███████╗██╗  ██╗███████╗",
        "██╔════╝╚══██╔══╝██║   ██║██╔══██╗██╔════╝████╗  ██║╚══██╔══╝██╔════╝╚██╗██╔╝██╔════╝",
        "███████╗   ██║   ██║   ██║██║  ██║█████╗  ██╔██╗ ██║   ██║   █████╗   ╚███╔╝ █████╗  ",
        "╚════██║   ██║   ██║   ██║██║  ██║██╔══╝  ██║╚██╗██║   ██║   ██╔══╝   ██╔██╗ ██╔══╝  ",
        "███████║   ██║   ╚██████╔╝██████╔╝███████╗██║ ╚████║   ██║██╗███████╗██╔╝ ██╗███████╗",
        "╚══════╝   ╚═╝    ╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝   ╚═╝╚═╝╚══════╝╚═╝  ╚═╝╚══════╝"
    ];

    private const int _logoLine = 3;
    private int _instructionLine = _logoLine;
    private string[] _instruction;

    public InstructionView(string[] instruction)
    {
        _instruction = instruction;
    }

    public void Draw()
    {
        DrawLogo();
        DrawInstruction();
    }

    private void DrawLogo()
    {
        string[] coloredLogo = new string[_logo.Length];

        for (int i = 0; i < _logo.Length; i++)
        {
            StringBuilder sb = new();

            sb.Append("\x1b[93m");

            foreach (char c in _logo[i])
            {
                if (c != '█' && c != ' ')
                {
                    sb.Append("\x1b[33m");
                    sb.Append(c);         
                    sb.Append("\x1b[93m");
                }
                else
                {
                    sb.Append(c);
                }
            }

            sb.Append("\x1b[0m");

            coloredLogo[i] = sb.ToString();
        }

        for (int i = 0; i < _logo.Length; i++)
        {
            DrawCenter(coloredLogo[i], _logoLine + i);
        }

        _instructionLine = _logoLine + _logo.Length + 5;
    }

    private void DrawInstruction()
    {
        for (int i = 0; i < _instruction.Length; i++)
        {
            DrawCenter(_instruction[i], _instructionLine + i);
        }
    }
}
