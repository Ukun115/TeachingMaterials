namespace SoundConfig
{
    using R3;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// サウンド設定画面ビュー
    /// </summary>
    public class SampleSoundConfigView : MonoBehaviour
    {
        [Header("スライダーとトグル")]
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _bgmVolumeSlider;
        [SerializeField] private Slider _seVolumeSlider;
        [SerializeField] private Toggle _muteToggle;
        [Space(10)]

        [Header("BGMとSEのオーディオソース")]
        [SerializeField] private AudioSource _bgmAudioSource;
        [SerializeField] private AudioSource _seAudioSource;

        /// <summary>
        /// スライダーとトグル(マスター、BGM、SE、ミュート)の値変更イベント
        /// </summary>
        public Observable<float> OnMasterSliderChanged => _masterVolumeSlider.OnValueChangedAsObservable();
        public Observable<float> OnBgmSliderChanged => _bgmVolumeSlider.OnValueChangedAsObservable();
        public Observable<float> OnSeSliderChanged => _seVolumeSlider.OnValueChangedAsObservable();
        public Observable<bool> OnMuteToggleChanged => _muteToggle.OnValueChangedAsObservable();

        /// <summary>
        /// UI(スライダーとトグル)設定(通知なし)
        /// </summary>
        public void SetMasterSlider(float volume) => _masterVolumeSlider.SetValueWithoutNotify(volume);
        public void SetBgmSlider(float volume) => _bgmVolumeSlider.SetValueWithoutNotify(volume);
        public void SetSeSlider(float volume) => _seVolumeSlider.SetValueWithoutNotify(volume);
        public void SetMuteToggle(bool isMute) => _muteToggle.SetIsOnWithoutNotify(isMute);

        /// <summary>
        /// BGM,SEボリュームの変更(マスターボリュームを加味した最終値)
        /// </summary>
        public void SetBgmVolume(float finalVolume) => _bgmAudioSource.volume = finalVolume;
        public void SetSeVolume(float finalVolume) => _seAudioSource.volume = finalVolume;

        /// <summary>
        /// ミュート設定の変更
        /// </summary>
        public void SetMute(bool isMute)
        {
            _bgmAudioSource.mute = isMute;
            _seAudioSource.mute = isMute;
        }

        public void PlaySe() => _seAudioSource.Play();
    }
}