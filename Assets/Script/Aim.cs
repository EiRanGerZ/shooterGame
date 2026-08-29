using UnityEngine;
using Invector.vCharacterController;

public class Aim : MonoBehaviour
{
    [Header("References")]
    public Animator playerAnimator;
    public vThirdPersonCamera playerCamera;
    public Transform aimTarget;
    public Transform spine;
    public vThirdPersonController cc;

    [Header("Camera Settings")]
    public float normalOffset = 0.2f;
    public float aimingOffset = 0.5f;
    public float normalDistance = 3f;
    public float aimingDistance = 1.5f;
    public float cameraSmoothSpeed = 10f;

    [Header("Spine Rotation Settings")]
    public Vector3 spineOffset = new Vector3(0, 0, 0); // Atur jika arah spine agak miring

    private bool wasAiming;
    private bool isAiming;

    private void Start()
    {
        if (cc == null)
            cc = GetComponent<vThirdPersonController>();
        
        if (playerAnimator == null)
            playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Cek jika game sedang pause
        if (PauseMenu.IsPaused) return;

        isAiming = Input.GetMouseButton(1);

        // Atur Strafe & Layer Weight Animasi
        if (isAiming)
        {
            playerAnimator.SetLayerWeight(1, Mathf.Lerp(playerAnimator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            if (!wasAiming && !cc.isStrafing)
            {
                cc.Strafe();
            }
        }
        else
        {
            playerAnimator.SetLayerWeight(1, Mathf.Lerp(playerAnimator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));

            if (wasAiming && cc.isStrafing)
            {
                cc.Strafe();
            }
        }

        // Transisi Kamera Halus (Smooth Lerp)
        float targetOffset = isAiming ? aimingOffset : normalOffset;
        float targetDistance = isAiming ? aimingDistance : normalDistance;

        playerCamera.rightOffset = Mathf.Lerp(playerCamera.rightOffset, targetOffset, Time.deltaTime * cameraSmoothSpeed);
        playerCamera.defaultDistance = Mathf.Lerp(playerCamera.defaultDistance, targetDistance, Time.deltaTime * cameraSmoothSpeed);

        wasAiming = isAiming;
    }

    // Eksekusi manipulasi tulang/spine SELALU di LateUpdate
    private void LateUpdate()
    {
        if (PauseMenu.IsPaused) return;

        if (isAiming && spine != null && aimTarget != null)
        {
            LookAtTarget();
        }
    }

    private void LookAtTarget()
    {
        Vector3 direction = aimTarget.position - spine.position;
        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            spine.rotation = targetRotation * Quaternion.Euler(spineOffset);
        }
    }
}