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
    private GameControls.PlayerActions _player;
    public GameControls.PlayerActions Player => _player;
    // �J�����p
    private GameControls.CameraActions _camera;
    public GameControls.CameraActions Camera => _camera;
    // UI�p
    private GameControls.UIActions _UI;
    public GameControls.UIActions UI => _UI;

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

        /* ---GameControls�֘A�̏��������� ��������--- */
        // GameControls�N���X�̃C���X�^���X�𐶐�����
        // ������ScriptableObject���p�������N���X�𗘗p���Ă���炵��
        // ���̂��߁AAwake()��Start()�ŃR���X�g���N�^���Ăԕ����ǂ����c�H
        controls = new GameControls();

        // controls.XXXActions��GameControls�N���X����new XXXActions(this)�ƒ�`����Ă���
        // �A��XXX��InputActionMap�̖��O
        // �܂�A���̃v���p�e�B���Ă΂��x��new�����悤�Ȃ̂ł��܂�ǂ��Ȃ�����
        _player = controls.Player;
        _camera = controls.Camera;
        _UI = controls.UI;
    }

    private void OnEnable() {
        // ����������GameControls����InputAction�S�����L���������炵���H
        controls.Enable();

        // ���̂悤�ɏ�����Player��ActionMap�̂ݗL�����ł������H
        //_player.Enable();
    }

    private void OnDestroy() {
        //�@IDisposable�Ȃ̂ŔO�̂���
        if (controls is not null) {
            controls.Dispose();
        }
    }
}
