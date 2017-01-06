using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
    public float playerHealth;
    public float playerMaxHealth = 1000;
    public int playerHeightScore;
    public int playerCombatScore;
    public UnityEngine.UI.Text playerTotalScore;
    public UnityEngine.UI.Text playerHealthText;
    public UnityEngine.UI.Slider playerHealthSlider;

    private int playerYStart;

    void Start()
    {
        playerYStart = (int)transform.position.y;
        playerHealth = playerMaxHealth;
    }

    void Update()
    {
        int currentYPos = (int)transform.position.y;
        playerHeightScore = (int)Mathf.Max(playerHeightScore, (currentYPos - playerYStart));

        playerTotalScore.text = "Score: " + (playerHeightScore + playerCombatScore).ToString();

        playerHealthText.text = "Health: " + (playerHealth).ToString() + "/" + (playerMaxHealth).ToString();
        playerHealthSlider.value = (float)(playerHealth / playerMaxHealth);
    }

    public void Damage(int amount)
    {
        playerHealth = Mathf.Clamp(playerHealth - amount, 0, playerMaxHealth);
        if (playerHealth == 0)
            Death();
    }

    public void Death()
    {
    }
}
