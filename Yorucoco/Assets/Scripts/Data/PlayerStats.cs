using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money; //Carried over
    public int startMoney = 20;

    public static int Lives;
    public int startLives = 100;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }

    private void Update() {
        //if player loses all his lifes
        if(Lives <= 0) { 
            //Placeholder print function for now to allow the implementation of perhaps UI?
            print("PEW PEW YOUR MOTHER FLY SIA U DIE");
            
        }
            
    }


}
