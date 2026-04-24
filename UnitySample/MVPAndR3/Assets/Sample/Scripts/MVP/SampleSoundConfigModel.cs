using R3;
using UnityEngine;

namespace SoundConfig
{
    /// <summary>
    /// サウンド設定画面モデル
    /// </summary>
    public class SampleSoundConfigModel
    {
        /// <summary>
        /// マスターボリューム、BGMボリューム、SEボリュームは0〜1の範囲で管理
        /// ミュート設定はtrue/falseで管理
        /// </summary>
        private readonly ReactiveProperty<float> _masterVolume = new();
        private readonly ReactiveProperty<float> _bgmVolume = new();
        private readonly ReactiveProperty<float> _seVolume = new();
        private readonly ReactiveProperty<bool> _isMute = new();

        /// <summary>
        /// 外部への値の公開
        /// </summary>
        public ReadOnlyReactiveProperty<float> MasterVolume => _masterVolume;
        public ReadOnlyReactiveProperty<float> BgmVolume => _bgmVolume;
        public ReadOnlyReactiveProperty<float> SeVolume => _seVolume;
        public ReadOnlyReactiveProperty<bool> IsMute => _isMute;

        /// <summary>
        /// BGMとSEの最終的なボリュームはマスターボリュームを加味して計算して公開
        /// </summary>
        public ReadOnlyReactiveProperty<float> FinalBgmVolume => Observable.CombineLatest(_masterVolume, _bgmVolume, (master, bgm) => master * bgm).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<float> FinalSeVolume => Observable.CombineLatest(_masterVolume, _seVolume, (master, se) => master * se).ToReadOnlyReactiveProperty();

        /// <summary>
        /// 各ボリューム値を0〜1の範囲にクランプしてから設定
        /// </summary>
        public void SetMasterVolume(float volume) => _masterVolume.Value = Mathf.Clamp01(volume);
        public void SetBgmVolume(float volume) => _bgmVolume.Value = Mathf.Clamp01(volume);
        public void SetSeVolume(float volume) => _seVolume.Value = Mathf.Clamp01(volume);
        public void SetIsMute(bool isMute) => _isMute.Value = isMute;
    }
}