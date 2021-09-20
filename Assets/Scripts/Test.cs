using UnityEngine;

/// <summary>
/// ����m�F�p�@
/// Player�}�b�v����Jump�A�N�V�����̂ݓǂݏo����
/// Jump�A�N�V������Q�L�[�Ƀo�C���h���Ă���
/// </summary>
public class Test : MonoBehaviour
{
    private GameControls.PlayerActions input;

    void Start()
    {
        //�@���̃^�C�~���O�łȂ��Ǝ��ʊ댯�������邩���H
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
