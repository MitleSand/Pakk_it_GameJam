using UnityEngine;

public class P_PackboyButton : MonoBehaviour
{
    public StarterAssets.P_ZeroGMovement_TEST playerMovement; // Reference to the movement script
    public Vector3 direction; // Local direction this button sets
    public bool stopMovement = false; // If this button stops movement

    private void OnMouseDown()
    {
        if (playerMovement != null)
        {
            if (stopMovement)
            {
                playerMovement.StopMovement();
            }
            else
            {
                // Convert local direction to world direction based on the player's orientation
                Vector3 worldDirection = playerMovement.transform.TransformDirection(direction);
                playerMovement.SetDirection(worldDirection);
            }
        }
        else
        {
            Debug.LogWarning("Player movement script is not assigned!");
        }
    }
}
