namespace SoundConfig
{
    using R3;
    using UnityEngine;

    /// <summary>
    /// サウンド設定画面プレゼンター
    /// </summary>
    public class SoundConfigPresenter : MonoBehaviour
    {
        [SerializeField] private SoundConfigView _view;

        [Header("サウンド設定の初期設定")]
        [Range(0f, 1f)][SerializeField] private float _defaultMasterVolume = 1.0f;
        [Range(0f, 1f)][SerializeField] private float _defaultBgmVolume = 1.0f;
        [Range(0f, 1f)][SerializeField] private float _defaultSeVolume = 1.0f;
        [SerializeField] private bool _defaultMute = false;
        [Space(10)]

        private SoundConfigModel _model = new();

        private readonly CompositeDisposable _disposables = new();

        void Start()
        {
            // Let's Try モデルの初期値設定
            // _model.SetMasterVolume(_defaultMasterVolume);
            // _model.SetBgmVolume(_defaultBgmVolume);
            // _model.SetSeVolume(_defaultSeVolume);
            // _model.SetIsMute(_defaultMute);

            // モデルの値変更を購読してビューを更新
            Bind();

            // UIの操作を購読してモデルを更新
            SetupEvents();
        }

        private void OnDestroy() => _disposables.Dispose();

        /// <summary>
        /// モデルの値変更を購読してビューを更新
        /// </summary>
        private void Bind()
        {
            // Let's Try マスター、BGM、SEのスライダーとミュートトグルの値を各UIに反映


            // Let's Try ミュート設定の変更を反映
            // _model.IsMute.Subscribe(isMute => _view.SetMute(isMute)).AddTo(_disposables);

            // Let's Try LBGMとSEの最終的なボリュームはマスターボリュームを加味して計算して反映
            // _model.FinalBgmVolume.Subscribe(_view.SetBgmVolume).AddTo(_disposables);
            // _model.FinalSeVolume.Subscribe(_view.SetSeVolume).AddTo(_disposables);

            // Let's Try SEプレビュー再生
            // _model.SeVolume.Skip(1).Subscribe(_ => _view.PlaySe()).AddTo(_disposables);
        }

        /// <summary>
        /// UIの操作を購読してモデルを更新
        /// </summary>
        private void SetupEvents()
        {
            // Let's Try マスター、BGM、SEのスライダーとミュートトグル
            // NOTE: Skip(1)で初期値の変更をスキップしている。初期値も反映したい場合はSkip(1)を削除する。

        }

        /// <summary>
        /// Let's Try 各ボリューム設定(マスター、BGM、SE)
        /// </summary>
        // public void SetMasterVolume(float volume) => _model.SetMasterVolume(volume);
        // public void SetBgmVolume(float volume) => _model.SetBgmVolume(volume);
        // public void SetSeVolume(float volume) => _model.SetSeVolume(volume);
    }
}