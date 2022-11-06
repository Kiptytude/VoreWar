using OdinSerializer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITacticalAI {
    bool RunAI();
    bool ForeignTurn { get; set; }
    TacticalAI.RetreatConditions RetreatPlan { get; set; }
}
