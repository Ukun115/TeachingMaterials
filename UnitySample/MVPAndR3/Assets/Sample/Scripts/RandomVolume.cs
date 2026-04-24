namespace SoundConfig
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ランダムボリューム設定
    /// </summary>
    public class RandomAudioVolume : MonoBehaviour
    {
        [Header("マスター,BGM,SEのランダムボタン")]
        [SerializeField] private Button _masterVolumeRandomButton;
        [SerializeField] private Button _bgmVolumeRandomButton;
        [SerializeField] private Button _seVolumeRandomButton;
        [Space(10)]

        [SerializeField] private SampleSoundConfigPresenter _presenter;

        private void Start()
        {
            SetupEvents();
        }

        /// <summary>
        /// マスター,BGM,SEのランダムボタンのクリックイベントを設定
        /// </summary>
        private void SetupEvents()
        {
            _masterVolumeRandomButton.onClick.AddListener(() => _presenter.SetMasterVolume(Random.Range(0f, 1f))); ;
            _bgmVolumeRandomButton.onClick.AddListener(() => _presenter.SetBgmVolume(Random.Range(0f, 1f)));
            _seVolumeRandomButton.onClick.AddListener(() => _presenter.SetSeVolume(Random.Range(0f, 1f)));
        }
    }
}