using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{
    [Header("External references")]
    public new Camera camera;
    public GameObject menuSlider;
    private Slider sensitivitySlider;

    [Header("General settings")]
    private float xRotation = 0f;
    public float mouseSensitivity = 25f;

    private void Start()
    {
        sensitivitySlider = menuSlider.GetComponent<Slider>();
        sensitivitySlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -=(mouseY * Time.deltaTime) * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * mouseSensitivity);
    }
    private void OnDestroy()
    {
        sensitivitySlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
    private void OnSliderValueChanged(float value)
    {
        mouseSensitivity = sensitivitySlider.value;
    }
}
