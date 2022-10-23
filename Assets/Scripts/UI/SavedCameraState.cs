using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

