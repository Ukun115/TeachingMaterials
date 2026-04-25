namespace SoundConfig
{
    using R3;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// サウンド設定画面ビュー
    /// </summary>
    public class SoundConfigView : MonoBehaviour
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
        /// Let's Try スライダーとトグル(マスター、BGM、SE、ミュート)の値変更イベント
        /// </summary>


        /// <summary>
        /// Let's Try UI(スライダーとトグル)設定(通知なし)
        /// </summary>


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