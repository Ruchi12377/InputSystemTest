using UnityEngine;

//�@����m�F�p
public class Test : MonoBehaviour
{
    private GameControls.PlayerActions input;

    void Start()
    {
        //�@���̃^�C�~���O�łȂ��Ǝ��ʊ댯�������邩���H
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
