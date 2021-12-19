using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Holds information regarding the button's information.
/// The script is used as context for what gameMode the
/// player has selected.
/// </summary>
public class GameModeOption : MonoBehaviour, IPointerEnterHandler
{
    public GameManager.GameState gameMode;

    public string gameModeDescription;

    /// <summary>
    /// Invokes this function when the player's mouse
    /// hovers over the button.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Update the decription text to describe this gamemode
        UIManager.Instance.descriptionText.text = gameModeDescription;
    }
}