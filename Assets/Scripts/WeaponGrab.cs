using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponGrab : MonoBehaviour
{
    public Sprite weaponSprite; // Sprite for the weapon's icon
    public Image weaponImageUI; // UI element where the weapon icon will be displayed

    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Ensure that the grab event is hooked up
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        EquipWeapon();
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        UnequipWeapon();
    }

    private void EquipWeapon()
    {
        if (weaponImageUI != null && weaponSprite != null)
        {
            weaponImageUI.sprite = weaponSprite;
            weaponImageUI.enabled = true; // Show weapon icon in UI
        }
    }

    private void UnequipWeapon()
    {
        if (weaponImageUI != null)
        {
            weaponImageUI.enabled = false; // Hide weapon icon in UI when the weapon is released
        }
    }


    // Optionally, unsubscribe from the event on destroy to avoid memory leaks
    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.RemoveListener(OnGrab);
        }
    }
}
