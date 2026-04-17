using StudentEXE.Model.Entities;

namespace StudentEXE.Interfaces;

internal interface IPickable : IDrawable
{
    public bool OnPickUp(Player player);

    public string GetName();
}
