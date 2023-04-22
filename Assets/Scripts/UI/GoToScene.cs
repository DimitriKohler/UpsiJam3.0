using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private Button _yourButton;

    void Start ()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
