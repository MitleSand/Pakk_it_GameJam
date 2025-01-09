using UnityEngine;

public class PackboyButton : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right, Stop }

    public Direction buttonDirection; // Assign this in the Inspector
    public StarterAssets.P_ZeroGMovement_TEST playerMovement; // Reference to the player's movement script

    private void OnMouseDown()
    {
        if (playerMovement == null) return;

        switch (buttonDirection)
        {
            case Direction.Up:
                playerMovement.SetDirection(Vector3.up);
                break;
            case Direction.Down:
                playerMovement.SetDirection(Vector3.down);
                break;
            case Direction.Left:
                playerMovement.SetDirection(Vector3.left);
                break;
            case Direction.Right:
                playerMovement.SetDirection(Vector3.right);
                break;
            case Direction.Stop:
                playerMovement.StopMovement();
                break;
        }
    }
}
