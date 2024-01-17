using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestChangeScene : MonoBehaviour
{
    public void TestGoToLevel()
    {
        GameStateManager.GameStateManagerSingleton.ChangeGameState(GameStateManager.GameStates.Level1);
    }
}
