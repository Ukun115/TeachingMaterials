# TeachingMaterials
授業用教材集です。

【カタログ】
・UnityHub
・UnityEditor
・Github、Fork
・ビルボード
・MVP、R3

【候補】
1. Unity 基礎・コア機能
基本的な「動かす」ための知識です。
物理・移動: Rigidbody、Vector、InputSystem、Spline
オブジェクト管理: Prefab、LOD、カリング、Sprite Shape
地形・2D: TileMap、Terrain
アニメーション: Animation、IK、モーフィング、Clothシュミレーション

2. グラフィックス・演出
ゲームの見た目とクオリティを決定づける要素です。
レンダリング: マテリアル、シェーダー、ShaderGraph、Decal
エフェクト: ParticleSystem、VFX Graph、Trail(軌跡)、Wind/Shake(木々を風で揺らしたり...)
ライティング: 半球ライト、ライトハロー、レンズフレア
ポストプロセス: ColorGrading(LUT)、Bloom、Vignette、Tonemapping、Anti-aliasing(FXAA/SMAA/TAA)、AmbientOcclusion(SSAO)、Fog、AutoExposure(EyeAdaptation)、WhiteBalance、LiftGamma,Gain、Depth of Field(DoF)、MotionBlur、ChromaticAberration、LensDistortion、LensFlare、FilmGrain、PaniniProjection、Pixelate、Posterize、ColorSplash、Scanlines、GlitchEffect、EdgeDetection、Dithering、RadialBlur、ScreenSpaceReflections(SSR)、DepthBlur、Vortex/Twirl、ColorSplit/RGBShift、Invert、KuwaharaFilter、Sharpen、Letterbox

3. UI/UX・マルチデバイス
ユーザーとの接点を作る技術です。
uGUI: Canvas、TMPro、Slider、Image、InputField、Button、Toggle、ScrollView、Scrollbar、DropDown...、Anchor
マルチデバイス: モバイル対応(ノッチ/SafeAreaなど)、Androidビルド、Localization(多言語対応)、Transition、SplitScreen
カメラ: Chinemachine

4. プログラミング設計・アーキテクチャ
「保守性の高いコード」を書くためのエンジニアリング知識です。
設計パターン: Factoryパターン、Observerパターン、Commandパターン、Strategyパターン、ステートマシン、シングルトン、ObjectPool(Pooling)
プログラミング概念: オブジェクト指向、継承、コンポーネント指向、インタフェース、カプセル化、コンポジション、関心の分離 (Separation of Concerns)、疎結合と密結合
最適化・効率: 非同期処理(コルーチン、UniTask、async/await)、DOTween(イージング)、ScriptableObject、JobSystem、計算量の意識（Big O記法）

5. プロフェッショナル・プリンシプル
現場で評価される「綺麗なコード」の原則です。
基本原則: DRY、KISS、YAGNI、単一責任の原則 (SRP)、インターフェース分離の原則 (ISP)、デメテルの法則
実装テクニック: 早期リターン (Early Return)、マジックナンバーの排除、名前重要 (Naming)、「魔法の文字列」の定数化、不変性 (Immutability)
リファクタリング: ゴッドクラスの解体、依存関係の可視化

6. リソース管理・デプロイ
ゲームを製品として仕上げ、届けるためのフローです。
アセット管理: Addressable、AssetBundle、Resourcesとの違い！、Atlas化、SubModule
外部連携: Photon、Firebase、MLAPI、Unity Ads、アプリ内課金(Unity IAP)、Steam戦略からリリースまで
データ管理: マスターデータ(Json)、セーブデータの暗号化(PlayerPrefs vs AES)

7. 開発効率・品質管理
チーム開発とデバッグの技術です。
デバッグ・解析: デバッグ、Profiler、エラーハンドル、条件付きコンパイル
ワークフロー: CI/CD(自動ビルドGitHub Actions)、命名規則、ディレクトリ構成、コードレビューの作法、アノテーションコメント(NOTE、TODOなど)
運用・マインド: READMEの書き方、Changelog管理、ボーイスカウト・ルール、驚き最小の原則、ポートフォリオの作り方
法務・ツール: 利用規約・著作権表記をしっかりみてみる、Muse(画像・テクスチャ・コード生成)

8. 応用・先端技術
特定のジャンルや最新技術への挑戦です。
物理応用: Ragdoll、NavMesh
先端技術: AR、VR、ML-Agents
サウンド: CRI ADX2
