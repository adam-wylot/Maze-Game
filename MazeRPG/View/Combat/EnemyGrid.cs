using StudentEXE.Model.Entities.Enemies;

namespace StudentEXE.View.CombatBoard;

internal class EnemyGrid : RendererBase
{
    public int StartY { get; set; }

    private static readonly string[] _defaultArt =
    [
        @"   ,    ,    /\   /\    ",
        @"  /( /\ )\  _\ \_/ /_   ",
        @"  |\_||_/| < \_   _/ >  ",
        @"  \______/  \|0   0|/   ",
        @"    _\/_   _(_  ^  _)_  ",
        @"   ( () ) /`\|V'''V|/`\ ",
        @"     {}   \  \_____/  / ",
        @"     ()   /\   )=(   /\ ",
        @"     {}  /  \_/\=/\_/  \"
    ];

    private string[] GetEnemyArt(string enemyName)
    {
        return enemyName switch
        {
            "Academic Guard" =>
            [
                @"          ╭──────╮          ",
                @"          ┴──────┴────      ",
                @"         ╱  ~  ~  ╲         ",
                @"         │ (o)(o) │         ",
                @"         │   <>   │         ",
                @"      ╭-╱│ ╱────╲ │╲-╮      ",
                @"      │  ╰────────╯  │      ",
                @"      │   ☆  •  PW   │      ",
                @"      │  │   •    │  │      ",
                @"      │  │   •    │  │      ",
                @"      ├──┤   •    ├──┤      ",
                @"      ╰──┴────────┴──╯      "
            ],
            "Wild Dog" =>
            [
                @"    _          __        ",
                @"   ╱ ▏        ▕  ╲       ",
                @" ╱   ╲________╱    ╲     ",
                @"│   ╲          /    │    ",
                @" ╲  ◥◣   ││   ◢◤   ╱     ",
                @"  │      ◉◉       │      ",
                @"  │     ╱▽▽╲      │      ",
                @"   ╲_  ╰△△△△╯   _╱      /",
                @"  ╭──╯▔▔▔▔▔▔▔▔▔▔╰──╮   ╱ ",
                @"  │                │▔▔▔╲ ",
                @"  │  │ ________ │  │    │ ",
                @"  │  ├╯────────╰┤  ├──┤ │ ",
                @"  ╰──╯          ╰──╯  ╰─╯ "
            ],
            "Hangover" =>
            [
            @"               ...                            ",        
            @"             ;::::;                           ",            
            @"           ;::::; :;                          ",                
            @"         ;:::::'   :;                         ",                            
            @"        ;:::::;     ;.                        ",                            
            @"       ,:::::'       ;           OOO\         ",                                        
            @"       ::::::;       ;          OOOOO\        ",                            
            @"       ;:::::;       ;         OOOOOOOO       ",                                
            @"      ,;::::::;     ;'         / OOOOOOO      ",                
            @"    ;:::::::::`. ,,,;.        /  / DOOOOOO    ",                
            @"  .';:::::::::::::::::;,     /  /     DOOOO   ",    
            @" ,::::::;::::::;;;;::::;,   /  /        DOOO  ",            
            @";`::::::`'::::::;;;::::: ,#/  /          DOOO ",    
            @":`:::::::`;::::::;;::: ;::#  /            DOOO",        
            @"::`:::::::`;:::::::: ;::::# /              DOO",        
            @"`:`:::::::`;:::::: ;::::::#/               DOO",        
            @" :::`:::::::`;; ;:::::::::##                OO",        
            @" ::::`:::::::`;::::::::;:::#                OO",        
            @" `:::::`::::::::::::;'`:;::#                O ",    
            @"  `:::::`::::::::;' /  / `:#                  ",    
            @"   ::::::`:::::;'  /  /   `#                  "    
            ],
            "Homeless" =>
            [
                @"      ╭──────╮              ",
                @"     ╱ ~~~    ╲             ",
                @"    │ ─o─  ─o─ │            ",
                @"    │    u     │            ",
                @"    │  ╭───╮   │            ",
                @"     ╲ ╰───╯  ╱             ",
                @"    _╱▔▔▔▔▔▔▔▔╲_            ",
                @"   ╱            ╲           ",
                @"  ╱  ┌────────┐  ╲          ",
                @" ╱   │ DROP A │   ╲    │   │",
                @"│    │  COIN  │    │   \ ¢ /",
                @"└────┴────────┴────┘   └-─-┘"
            ],
            _ => _defaultArt
        };
    }

    public void Update(Enemy enemy)
    {
        // Enemy's name
        DrawCenter($"\x1b[31m=== {enemy.Name.ToUpper()} ===\x1b[0m", StartY);

        // HP bar
        string hpBar = GenerateHealthBar(enemy.Hp, 100, 40);
        DrawCenter($"HP: {hpBar} ({enemy.Hp})", StartY + 2);

        // ASCII Art
        string[] currentArt = GetEnemyArt(enemy.Name);
        int artStartY = StartY + 5;
        for (int i = 0; i < currentArt.Length; i++)
        {
            DrawCenter($"\x1b[35m{currentArt[i]}\x1b[0m", artStartY + i);
        }
    }

    private string GenerateHealthBar(int current, int max, int length)
    {
        if (max <= 0) return new string('░', length);

        int filledLength = (int)Math.Round((double)current / max * length);
        filledLength = Math.Max(0, Math.Min(length, filledLength));

        string filled = new string('█', filledLength);
        string empty = new string('░', length - filledLength);

        double percentage = (double)current / max;
        string color = "\x1b[32m";
        if (percentage < 0.5) color = "\x1b[33m";
        if (percentage < 0.25) color = "\x1b[31m";

        return $"{color}{filled}\x1b[90m{empty}\x1b[0m";
    }
}