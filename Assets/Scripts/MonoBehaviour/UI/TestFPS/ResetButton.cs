using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public void ReastartLevel()
    {
        SceneManager.LoadScene(0);
    }
}