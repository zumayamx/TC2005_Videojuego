using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int playerHealth = 20; // Starting health of the player
    public TMP_Text healthText; // TMP Text element to display health
    public TMP_Text gameOverText; // TMP Text element to display game over message
    public Image gameOverImage; // Canvas Image to display game over screen
    public string playerType; // "b" for player one, "r" for player two

    void Start()
    {
        // Initialize the health display
        UpdateHealthText();
        // Ensure the game over image is initially off
        if (gameOverImage != null)
        {
            gameOverImage.gameObject.SetActive(false);
        }
    }

    // Method to reduce health and update the text
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        UpdateHealthText();

        // Check if the player is out of health
        if (playerHealth <= 0)
        {
            Debug.Log("Player is dead!");
            DisplayGameOverMessage();
        }
    }

    // Method to update the health display
    void UpdateHealthText()
    {
        healthText.text = "HP: " + playerHealth;
    }

    // Method to display game over message
    void DisplayGameOverMessage()
    {
        if (playerType == "b")
        {
            gameOverText.text = "Game Over! " + PersistentData.FirstPlayerName + " is defeated.";
        }
        else if (playerType == "r")
        {
            gameOverText.text = "Game Over! " + PersistentData.SecondPlayerName + " is defeated.";
        }
        else
        {
            gameOverText.text = "Game Over! Unknown player is defeated.";
        }

        // Enable the game over image
        if (gameOverImage != null)
        {
            gameOverImage.gameObject.SetActive(true);
        }
    }

    // Collision detection
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a projectile
        if (collision.collider.CompareTag("Projectile"))
        {
            TakeDamage(1); // Reduce health by 1 for each hit
            Destroy(collision.collider.gameObject); // Destroy the projectile upon collision
        }
    }
}