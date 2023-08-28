using UnityEngine;

namespace TapTapTap.Core
{
    public interface ISpawner
    {
        TObject Spawn<TObject>(string name)
            where TObject : Component;

        TObject Spawn<TObject>(string name, Transform parent)
            where TObject : Component;

        TObject Spawn<TObject>(string name, Transform parent, Vector3 position) 
            where TObject : Component;
    }
}