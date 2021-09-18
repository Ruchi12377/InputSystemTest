using UnityEngine;

public sealed class InputManager : MonoBehaviour {
    // インスタンスの格納場所
    private static InputManager _instance;

    // インスタンスのgetter
    public static InputManager Instance {
        get {
            // インスタンスがnullでない場合はインスタンスを返す
            if (_instance is not null) {
                return _instance;
            }

            // シーン内からインスタンスを検索して格納する
            _instance = FindObjectOfType<InputManager>();

            // シーン内にインスタンスが存在しなかった場合エラーメッセージを出す
#if UNITY_EDITOR
            if (_instance is null) {
                Debug.LogError(typeof(InputManager) + " is not found");
            }
#endif

            return _instance;
        }
    }

    private GameControls controls;

    // プレイヤー用
    private GameControls.PlayerActions _player;
    public GameControls.PlayerActions Player => _player;
    // カメラ用
    private GameControls.CameraActions _camera;
    public GameControls.CameraActions Camera => _camera;
    // UI用
    private GameControls.UIActions _UI;
    public GameControls.UIActions UI => _UI;

    private void Awake() {
        /* ---シングルトンのインスタンスの初期化処理 ここから--- */
        if (_instance is null) {
            // インスタンスがnullの場合は自身を格納する
            _instance = this;
        } else if(_instance != this) {
            // 既に自身以外のインスタンスが存在する場合は自身を削除し、Awake()を抜ける
            Destroy(gameObject);
            return;
        }
        /* ---シングルトンのインスタンスの初期化処理 ここまで--- */

        /* ---GameControls関連の初期化処理 ここから--- */
        // GameControlsクラスのインスタンスを生成する
        // 内部でScriptableObjectを継承したクラスを利用しているらしい
        // そのため、Awake()かStart()でコンストラクタを呼ぶ方が良さげ…？
        controls = new GameControls();

        // controls.XXXActionsはGameControlsクラス内でnew XXXActions(this)と定義されていた
        // 但しXXXはInputActionMapの名前
        // つまり、このプロパティが呼ばれる度にnewされるようなのであまり良くなさそう
        _player = controls.Player;
        _camera = controls.Camera;
        _UI = controls.UI;
    }

    private void OnEnable() {
        // こう書けばGameControls内のInputAction全部が有効化されるらしい？
        controls.Enable();

        // 下のように書けばPlayerのActionMapのみ有効化できそう？
        //_player.Enable();
    }

    private void OnDestroy() {
        //　IDisposableなので念のため
        if (controls is not null) {
            controls.Dispose();
        }
    }
}
