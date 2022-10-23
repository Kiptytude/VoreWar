using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITacticalAI {
    bool RunAI();

    TacticalAI.RetreatConditions RetreatPlan { get; set; }
}
