using System;
using System.Collections.Generic;

namespace TapTapTap.Core
{
    [Serializable]
    public class LevelDescription
    {
        public string id;
        public float positionScale;
        public List<LevelEntityData> levelEntityDatas;
    }
}