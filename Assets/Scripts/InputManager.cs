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
    public GameControls.PlayerActions Player => controls.Player;
    // カメラ用
    public GameControls.CameraActions Camera => controls.Camera;
    // UI用
    public GameControls.UIActions UI => controls.UI;

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
