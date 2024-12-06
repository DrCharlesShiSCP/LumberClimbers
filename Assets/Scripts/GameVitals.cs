using UnityEngine;
using UnityEngine.SceneManagement;
public class GameVitals : MonoBehaviour
{
    public KeyCode KillSwitch = KeyCode.Escape;

    public string currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KillSwitch))
        {
            SceneManager.LoadScene(currentScene);
        }
    }
}
