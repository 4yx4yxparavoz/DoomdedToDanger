using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 100f;
    public float regenRate = 10f;
    public float sprintDrain = 15f;

    public Slider staminaBar;

    private float currentStamina;
    public float SpeedMultiplier { get; private set; } = 1f;

    public float CurrentStamina => currentStamina;

    private void Start()
    {
        currentStamina = maxStamina;
        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = currentStamina;
        }
    }

    private void Update()
    {
        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0;
        bool hasStamina = currentStamina > 0f;

        if (wantsToSprint && hasStamina)
        {
            currentStamina -= sprintDrain * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0f);

            SpeedMultiplier = 1.4f;
        }
        else
        {
            currentStamina += regenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);

            SpeedMultiplier = 1f;
        }

        if (currentStamina <= 0f)
        {
            SpeedMultiplier = 1f;
        }

        if (staminaBar != null)
        {
            staminaBar.value = currentStamina;
        }
    }

    public bool HasStamina() => currentStamina > 0f;
}
