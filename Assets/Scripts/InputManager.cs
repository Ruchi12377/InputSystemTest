using UnityEngine;

public sealed class InputManager : MonoBehaviour {
    // �C���X�^���X�̊i�[�ꏊ
    private static InputManager _instance;

    // �C���X�^���X��getter
    public static InputManager Instance {
        get {
            // �C���X�^���X��null�łȂ��ꍇ�̓C���X�^���X��Ԃ�
            if (_instance is not null) {
                return _instance;
            }

            // �V�[��������C���X�^���X���������Ċi�[����
            _instance = FindObjectOfType<InputManager>();

            // �V�[�����ɃC���X�^���X�����݂��Ȃ������ꍇ�G���[���b�Z�[�W���o��
#if UNITY_EDITOR
            if (_instance is null) {
                Debug.LogError(typeof(InputManager) + " is not found");
            }
#endif

            return _instance;
        }
    }

    private GameControls controls;

    // �v���C���[�p
    public GameControls.PlayerActions Player => controls.Player;
    // �J�����p
    public GameControls.CameraActions Camera => controls.Camera;
    // UI�p
    public GameControls.UIActions UI => controls.UI;

    private void Awake() {
        /* ---�V���O���g���̃C���X�^���X�̏��������� ��������--- */
        if (_instance is null) {
            // �C���X�^���X��null�̏ꍇ�͎��g���i�[����
            _instance = this;
        } else if(_instance != this) {
            // ���Ɏ��g�ȊO�̃C���X�^���X�����݂���ꍇ�͎��g���폜���AAwake()�𔲂���
            Destroy(gameObject);
            return;
        }
        /* ---�V���O���g���̃C���X�^���X�̏��������� �����܂�--- */

        controls = new GameControls();
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDestroy() {
        if (controls is not null) {
            controls.Dispose();
        }
    }
}
