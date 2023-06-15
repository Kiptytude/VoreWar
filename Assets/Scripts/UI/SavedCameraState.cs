using OdinSerializer;
using UnityEngine;


class SavedCameraState
{
    [OdinSerialize]
    internal Vector2 StrategicPosition;
    [OdinSerialize]
    internal float StrategicZoom;
    [OdinSerialize]
    internal Vector2 TacticalPosition;
    [OdinSerialize]
    internal float TacticalZoom;
}

