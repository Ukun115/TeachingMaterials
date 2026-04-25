using R3;
using UnityEngine;

namespace SoundConfig
{
    /// <summary>
    /// サウンド設定画面モデル
    /// </summary>
    public class SoundConfigModel
    {
        /// <summary>
        /// Let's Try マスターボリューム、BGMボリューム、SEボリュームは0〜1の範囲で管理
        /// ミュート設定はtrue/falseで管理
        /// </summary>


        /// <summary>
        /// Let's Try 外部への値の公開
        /// </summary>


        /// <summary>
        /// Let's Try LBGMとSEの最終的なボリュームはマスターボリュームを加味して計算して公開
        /// </summary>
        // public ReadOnlyReactiveProperty<float> FinalBgmVolume => Observable.CombineLatest(_masterVolume, _bgmVolume, (master, bgm) => master * bgm).ToReadOnlyReactiveProperty();
        // public ReadOnlyReactiveProperty<float> FinalSeVolume => Observable.CombineLatest(_masterVolume, _seVolume, (master, se) => master * se).ToReadOnlyReactiveProperty();

        /// <summary>
        /// Let's Try 各ボリューム値を0〜1の範囲にクランプしてから設定
        /// </summary>

    }
}