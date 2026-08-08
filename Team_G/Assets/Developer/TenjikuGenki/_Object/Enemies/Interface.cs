
using UnityEngine;

public interface IReflectable
{
    public bool Hitting { get; }
    public COLOR Color { get; }

    public void Reflect(Vector2 ref_vec, bool hitting) { }
}

public interface IHitable
{
    public int Damage { get; }
    public virtual void Hit() { }
}