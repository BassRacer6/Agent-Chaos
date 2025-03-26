using UnityEngine;

public class MedKitScript : Interactable
{
    private PlayerStats playerStats;
    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    protected override void Interact()
    {
        playerStats.Heal(40);
        Destroy(gameObject);
    }
}
