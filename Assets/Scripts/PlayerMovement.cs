using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity;
using System;
using Scaling;
using UnityEngine.UI;

public class CharacterController3D : MonoBehaviour
{
    private Camera camera;

    public float playerSpeed = 5f;
    public float lookSpeed = 2f;
    public float maxLookAngle = 75f;
    public float dashMultiplier = 3f;
    public float dashLengthSeconds = 0.6f;
    public float dashAffectionRadius = 10f;

    public float abilityCooldownSeconds = 2f;
    public float abilityEffectTime = 2f;

    [SerializeField] private float fallingMultiplier = 2f;
    [SerializeField] private float jumpHeight = 1.0f;

    [SerializeField] private Slider abilityCooldown;
    [SerializeField] private Image alt_icon;

    [SerializeField] private Color sliderBackground;
    [SerializeField] private Color sliderForegroundEffect;
    [SerializeField] private Color sliderForegroundCooldown;
    [SerializeField] private Color sliderComplete;

    private bool isGrounded;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;

    private CharacterController controller;

    private bool _currentlyDashing = false;
    private bool _abilityOnCooldown = false;
    public bool AbilityOnCooldown => _abilityOnCooldown;

    void Start()
    {
        camera = Camera.main;
        controller = gameObject.AddComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        Move();
        Rotate();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = 0f;
            Jump();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (!_abilityOnCooldown)
            {
                alt_icon.enabled = false;

                abilityCooldown.maxValue = abilityEffectTime;
                abilityCooldown.value = abilityEffectTime;

                abilityCooldown.GetComponentsInChildren<Image>()[0].color = sliderBackground;
                abilityCooldown.GetComponentsInChildren<Image>()[1].color = sliderForegroundEffect;

                StartCoroutine(DecreaseSlider(abilityCooldown, abilityEffectTime, () => {
                    abilityCooldown.GetComponentsInChildren<Image>()[0].color = sliderBackground;
                    abilityCooldown.GetComponentsInChildren<Image>()[1].color = sliderForegroundCooldown;
                    MonsterObject[] enemies = FindObjectsOfType<MonsterObject>();
                    foreach (MonsterObject e in enemies)
                    {
                        e.Bigger();
                    }
                    abilityCooldown.maxValue = abilityCooldownSeconds;
                    abilityCooldown.value = abilityCooldownSeconds;
                    StartCoroutine(DecreaseSlider(abilityCooldown, abilityCooldownSeconds, () =>
                    {
                        abilityCooldown.GetComponentsInChildren<Image>()[0].color = sliderComplete;
                        abilityCooldown.GetComponentsInChildren<Image>()[1].color = sliderComplete;
                    }));
                }));

                MonsterObject[] enemies = FindObjectsOfType<MonsterObject>();
                foreach (MonsterObject e in enemies)
                {
                    e.Smaller();
                }
                _abilityOnCooldown = true;

                

                StartCoroutine(Wait(abilityEffectTime + abilityCooldownSeconds, () => { _abilityOnCooldown = false; alt_icon.enabled = true; }));
            }
        }

        if (Input.GetButtonDown("Fire3") && isGrounded)
        {
            if (_currentlyDashing)
                return;
            StartCoroutine(Dash());
        }

        playerVelocity.y += gravityValue * fallingMultiplier * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    IEnumerator DecreaseSlider(Slider slider, float seconds, Action followup = null)
    {
        if (slider != null)
        {
            float timeSlice = (slider.value / seconds / 10);
            while (slider.value >= 0)
            {
                Debug.Log(timeSlice + ", " + slider.value);
                slider.value -= timeSlice;
                yield return new WaitForSeconds(0.1f);
                if (slider.value <= 0)
                    break;
            }
        }
        yield return null;
        if (followup != null)
            followup();
    }

    private IEnumerator Dash()
    {
        this._currentlyDashing = true;
        playerSpeed *= dashMultiplier;
        Collider[] hitColliders = Physics.OverlapSphere(camera.transform.position, dashAffectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            
            MonsterObject monster = hitCollider.gameObject.GetComponent<MonsterObject>();

            if (monster != null)
            {
                Debug.Log("Monster was nearby during dash!");

                monster.Bigger();
            }
            
        }
        yield return new WaitForSeconds(dashLengthSeconds);
        playerSpeed /= dashMultiplier;
        this._currentlyDashing = false;
    }

    private IEnumerator Wait(float waitSeconds, Action followUp)
    {
        yield return new WaitForSeconds(waitSeconds);
        followUp();
    }

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = camera.transform.TransformDirection(move);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    private void Rotate()
    {
        float yRotationInput = Input.GetAxis("Mouse X");
        float xRotationInput = Input.GetAxis("Mouse Y") * lookSpeed;

        xRotation -= xRotationInput;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        yRotation += yRotationInput * lookSpeed;

        camera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void Jump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    private void LateUpdate()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}