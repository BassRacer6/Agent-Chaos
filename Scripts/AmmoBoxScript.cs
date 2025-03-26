using UnityEngine;

public class AmmoBoxScript : Interactable
{
    private PlayerWeapon playerWeapon;
    private void Start()
    {
        playerWeapon = GameObject.Find("Player").GetComponent<PlayerWeapon>();
    }
    protected override void Interact()
    {
        playerWeapon.IncreaseAmmo(20);
        playerWeapon.UpdateCounter();
        Destroy(gameObject);
    }
}
