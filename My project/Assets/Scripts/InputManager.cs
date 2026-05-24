using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    // Event fired when any lane is pressed, passing the lane index (0-3)
    public static event Action<int> OnLanePressed;

    // Based on the workspace, the generated class name is likely InputSystem_Actions
    // (If you named it RhythmControls, just rename it below)
    private RhythmControls controls;

    private void Awake()
    {
        // Standard persistent singleton implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        controls = new RhythmControls();

        // Subscribe to input actions
        controls.Gameplay.LaneA.performed += OnLaneA;
        controls.Gameplay.LaneB.performed += OnLaneB;
        controls.Gameplay.LaneX.performed += OnLaneX;
        controls.Gameplay.LaneY.performed += OnLaneY;

        controls.Enable();
    }

    // Callbacks to route the specific lanes to the generic event
    private void OnLaneA(InputAction.CallbackContext ctx) => OnLanePressed?.Invoke(0);
    private void OnLaneB(InputAction.CallbackContext ctx) => OnLanePressed?.Invoke(1);
    private void OnLaneX(InputAction.CallbackContext ctx) => OnLanePressed?.Invoke(2);
    private void OnLaneY(InputAction.CallbackContext ctx) => OnLanePressed?.Invoke(3);

    private void OnDestroy()
    {
        // Clean up subscriptions to avoid memory leaks
        if (Instance == this && controls != null)
        {
            controls.Gameplay.LaneA.performed -= OnLaneA;
            controls.Gameplay.LaneB.performed -= OnLaneB;
            controls.Gameplay.LaneX.performed -= OnLaneX;
            controls.Gameplay.LaneY.performed -= OnLaneY;
            
            controls.Disable();
            controls.Dispose();
        }
    }
}