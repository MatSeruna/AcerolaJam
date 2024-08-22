using TMPro;
using UnityEngine;

public class PanelIntro : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI textDay;

    public void IntroEnded()
    {
        gameManager.StartGame();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        textDay.text = $"Day: {gameManager.daySystem.Day}";
    }
}
