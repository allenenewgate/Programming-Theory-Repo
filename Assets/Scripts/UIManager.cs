using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject instructions;
    [SerializeField]
    private GameObject titleScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideInstructions();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowInstructions()
    {
        instructions.SetActive(true);
        titleScreen.SetActive(false);
    }

    private void HideInstructions()
    {
        instructions.SetActive(false);
        titleScreen.SetActive(true);
    }
}
