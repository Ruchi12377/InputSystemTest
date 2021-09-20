using UnityEngine;

/// <summary>
/// 動作確認用　
/// Playerマップ内のJumpアクションのみ読み出せる
/// JumpアクションはQキーにバインドしている
/// </summary>
public class Test : MonoBehaviour
{
    private GameControls.PlayerActions input;

    void Start()
    {
        //　このタイミングでないと死ぬ危険性があるかも？
        input = InputManager.Instance.GetPlayerActions();
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
