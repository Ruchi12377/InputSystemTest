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

    // シングルプレイヤーならこれでよさそう
    // マルチプレイヤーを想定するならリストにするなどする必要がありそう
    private GameControls controls;


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

    // マルチプレイヤーの場合に拡張しやすいように敢えてメソッドにしておく


    // プレイヤー用の入力管理用の構造体を取得する
    public GameControls.PlayerActions GetPlayerActions() {
        return controls.Player;
    }

    // カメラ用の入力管理用の構造体を取得する
    public GameControls.CameraActions GetCameraActions() {
        return controls.Camera;
    }

    // UI用の入力管理用の構造体を取得する
    public GameControls.UIActions GetUIActions() {
        return controls.UI;
    }
}
