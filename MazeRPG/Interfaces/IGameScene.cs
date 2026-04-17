namespace StudentEXE.Interfaces;

internal interface IGameScene
{
    void Enter();
    void Pause();
    void Resume();
    void HandleInput(ConsoleKey key);
    void Exit();
}