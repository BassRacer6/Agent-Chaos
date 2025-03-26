using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private new Camera camera;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    void Start()
    {
        camera = GetComponent<PlayerLook>().camera;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }
    void Update()
    {
        playerUI.UpdatePromptText(string.Empty);
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast (ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.gameObject.GetComponent<Interactable>();
                playerUI.UpdatePromptText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        };
    }
}
