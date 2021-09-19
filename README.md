# InputSystemTest
InputSystemのテスト  
GitHubにファイルをアップロードするのに慣れていないため必要そうなものだけ手動でアップロードしました  
恐らくプロジェクトとしては壊れていますが、強引に開けば動いてくれるはずです  
Unityのバージョンは2021.2.0bの系統を使っています  
InputSystem のバージョン1.1系ではポーリング方式で使用する際のAPIが追加されたようなのでこれを使いたいです  

## InputSystemフォルダ内のものについて
InputSystem.inputsettingsというアセットにInputSystemの設定が保存されています  
このアセット中のUpdate ModeをProcess Event in Fixed Updateに設定すると、  
InputSystemの更新タイミングがFixed Updateと同期されるようなのでこれを利用したいです  

デフォルトのバインディングなどの設定はInputActionAssetから行うつもりです  
ここではInputActionAssetにGameControlsという名前をつけて保存しています  
また、Ganerate C# classのオプションを用いることで同名クラスを自動生成しています  

自動生成されたクラス(以下、GameControlsクラス)はInputActionAssetで設定した内容を元にしたInputActionを作り、    
InputActionMapごとにそれらを纏めた構造体をGameControlsクラス内に生成するようです  
また、GameControlsクラスには生成された構造体のインスタンスを返すgetterが用意されていますが、  
このgetterはアクセスする度にインスタンスを生成するようになっており多用するのはまずそうです  

### 例:Player用のInputActionMapを作った場合   
GameControlsクラスにはPlayerActionsという構造体が生成され、Playerという名前のgetterからアクセスできます  
ですが、下記のようにPlayerというgetterを使う度にnewされてしまいます  

```csharp:Example.cs  
public class Example:MonoBehavior  
{  
  private GameControls controls;  
  private GameControls.PlayerActions player;
  
  private void Awake(){
    controls = new GameControls();
    // ここのgetterでnewされる 
    player = controls.Player;
  }
}  
```

ただ、生成された構造体にはそれぞれEnable()及びDisable()というメソッドが用意されており、  
これらのメソッドを利用することで特定のInputActionMapに属するInputActionを纏めてオンオフできそうです  

また、GameControlsクラス自身も複数インスタンスを作れるようになっています  
これは恐らくローカルマルチプレイなどの時に使うためのものだと思われます  
ですが、勝手にインスタンスを作ることができるのは管理上不便そうです  

## Scenesフォルダ内のものについて
Mainシーンのみしかありません

## Scriptsフォルダ内のものについて
管理用のシングルトンクラスとその利用クラスが一つずつあります  
InputManagerクラスは管理用のシングルトンなMonoBehaviorです  
当初は非MonoBehaviorで作るつもりだったのですが厳しそうだったため断念しました  
自動生成されたクラスのコンストラクタ呼び出しタイミングがAwake()かStart()でないとまずそうだったためです  
とりあえずで作ったためこのままで良いのか心配です

Testクラスはその利用クラスを想定しています  
このクラスを見れば分かるようにinput.Jump.WasPressedThisFrame()などと旧InputManagerに近い使い勝手で利用できます  
また、InputSystemの更新タイミングは上述したようにFixed Updateに合わせられるのでFixedUpdate内部で呼び出してもよさそうです

## その他（やりたいことや気になる点など）
機能や使い勝手、実装などを考慮したときに管理クラスがこれでいいのかが気になります

InputSystemの利点は生かしつつもスクリプトで利用するときは旧InputManagerに近いようにしたいです

同じ入力が別の意味になるときにも対応したいです  
例:マウス操作⇒「カーソルを動かして選択肢を選ぶ」「カメラを動かす」など  
（特定のInputActionMapのオンオフでいい？）

キーコンフィグ機能をつけられるなら対応したいです  
(バインドを変更する機能と変更後にデフォルトにリセットする機能があればよい)  
