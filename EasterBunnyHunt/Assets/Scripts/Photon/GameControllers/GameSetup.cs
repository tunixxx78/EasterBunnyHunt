using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public static GameSetup gS;
    public Transform[] spawnPoints;

    private void OnEnable()
    {
        if(GameSetup.gS == null)
        {
            GameSetup.gS = this;
        }
    }
}
