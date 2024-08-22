using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameManager gameManager;
    public Timer timer;
    public TextMeshProUGUI textRemainingPartygoer;
    public TextMeshProUGUI textTimer;
    public TextMeshProUGUI textEscapedPartygoer;
    public TextMeshProUGUI textSlayedPartygoer;
    public TextMeshProUGUI textDaysSurvived;
    public TextMeshProUGUI textDaysSurvived2;
    public TextMeshProUGUI textDays;

    // Update is called once per frame
    void Update()
    {
        textRemainingPartygoer.text = $"Partygoers remaining: {gameManager.partygoersCount}";
        textTimer.text = $"Timer: {timer.minutes}:{timer.seconds}:{timer.miliseconds}";
        textEscapedPartygoer.text = $"Escaped partygoers: {gameManager.escapedPartygoersCount}";
        textDays.text = $"current day: {gameManager.daySystem.Day}\nrecord day: {gameManager.daySystem.RecordDay}";
        textSlayedPartygoer.text = $"Partygoers slayed: {gameManager.partygoersSlayedCound}";
        textDaysSurvived.text = $"Days survived: {gameManager.daySystem.Day}";
        textDaysSurvived2.text = $"Days survived: {gameManager.daySystem.Day}";

        if (gameManager.escapedPartygoersCount > 0)
            textEscapedPartygoer.color = Color.red;
    }
}
