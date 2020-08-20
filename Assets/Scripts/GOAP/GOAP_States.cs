﻿using System;

[Serializable]
public enum GOAP_States {
    MeeleInSite,
    HasMeele,
    InMeeleRange,
    RangeInSite,
    HasRange,
    InFireRange,
    PlayerInSite,
    DamagePlayer,
    HasPatrolEnergy,
    Patrolling,
    Resting,
    Awake
}
