namespace TapTapTap.Core
{
    public interface IArchetypeProvider<TArchetype>
    {
        TArchetype GetArchetype(string name);
    }
}
