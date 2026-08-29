using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
    }
}