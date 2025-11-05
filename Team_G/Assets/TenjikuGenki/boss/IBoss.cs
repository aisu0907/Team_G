// IBossState.cs
public interface IBossState
{
    void Enter(g_boss boss);

    void Main(g_boss boss);
    
    void Exit(g_boss boss);
}