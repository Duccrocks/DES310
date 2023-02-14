/// <summary>
/// All interactable objects must inherit from this.
/// </summary>
public interface IInteractable
{
    /// <summary>
    ///     This method is ran whenever a gameobject has a
    ///     collision with a raytrace and the player presses E.
    /// </summary>
    void Interact();
}