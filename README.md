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

自動生成されたクラス(以下、GameControlsクラス)はInputActionAssetの設定内容を元にInputActionを作り、    
InputActionMapごとにそれらを纏めた構造体をGameControlsクラス内に生成するようです  
これらの構造体にはそれぞれEnable()及びDisable()というメソッドが用意されており、  
これを利用することで特定のInputActionMapに属するInputActionを纏めてオンオフできそうです  
また、GameControlsクラスには生成された構造体のインスタンスを返すgetterがあり、  
ここからアクセスできそうです  

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

* InputSystemの利点は生かしつつも旧InputManagerに近い使い勝手にする
* 同じ入力が別の意味になるときに対応する  
  * 例:マウス操作⇒「カーソルを動かして選択肢を選ぶ」「カメラを動かす」など  
  * 特定のInputActionMapのオンオフでいい？  
* キーコンフィグ機能を付ける  
  * バインドを変更する機能  
  * 変更後にデフォルトにリセットする機能  
* そもそもGameControlsクラスのラッパーを作った方が良いかどうか
* 管理クラスに改善できそうなところはないか


