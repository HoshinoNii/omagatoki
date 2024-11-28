using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "Tower_Defense/Debuff", order = 1)]
public class Debuff : ScriptableObject
{
   [Serializable]
    public struct _Debuff {

        //What type of enemy will spawn on that round?
        public enum DebuffType {
            Slow,

        }
        
        public DebuffType TypeOfDebuff;
        public float duration;
        public float Multiplier;

    }

    public _Debuff[] Debuffs;

}
