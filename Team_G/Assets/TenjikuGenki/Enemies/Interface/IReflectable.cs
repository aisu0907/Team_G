public interface IReflectable
{
    int timer { get; set; }
    public void SpinLimit(Enemy e)
    {
        timer++;
        if (timer > 180)
        {
            e.Delete();
        }
    }
}