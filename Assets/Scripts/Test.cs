using UnityEngine;

//　動作確認用
public class Test : MonoBehaviour
{
    private GameControls.PlayerActions input;

    void Start()
    {
        //　このタイミングでないと死ぬ危険性があるかも？
        input = InputManager.Instance.Player;
    }

    private void FixedUpdate() {
        if (input.Jump.WasPressedThisFrame()) {
            Debug.Log("WasPressed");
        }

        if (input.Jump.WasReleasedThisFrame()) {
            Debug.Log("WasReleased");
        }

        if (input.Jump.IsPressed()) {
            Debug.Log("IsPressedNow");
        }
    }

    
}
