using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


//setup an universal Pool Index that can be referenced based on what to Spawn
[Serializable]
public enum PoolIndex { //when a new pooldata is created please add in the data
    Nothing,
    Arrow,
    UpgradedArrow,
    Fireball,
    Roadblock,
    Roadblock_Blueprint,
    SlowSpell,
    SlowSpell_Blueprint,
    MagicTower_Blueprint,
    ArcherTower_Blueprint,
    EnemyFireball,
    UpgradedFireball,
    HealingTower_Blueprint,
}

