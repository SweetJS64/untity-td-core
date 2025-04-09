using UnityEngine;

public class ExitingTheGame : MonoBehaviour
{
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}