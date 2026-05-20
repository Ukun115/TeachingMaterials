namespace IK
{
    using UnityEngine;

    /// <summary>
    /// 足IK
    /// </summary>
    public class FootIK : MonoBehaviour
    {
        // スウィング中と判定するキャラクター基準の足の高さ閾値
        private static readonly float SwingFadeHeight = 0.15f;

        [Header("足の位置から下に向けて飛ばすレイキャストの最大検出距離"), SerializeField]
        private float _raycastDistance = 1.5f;

        [Header("地面レイヤー"), SerializeField]
        private LayerMask _groundLayer;

        private Animator _animator;
        private float _footOffset;

        private void Start()
        {
            // 足の自然な高さから地面までの距離を計算してfootOffsetに保存
            // これをすることにより、階段などの段差がある地形でも足が地面にぴったりつくようになる
            _animator = GetComponent<Animator>();
            var leftFootPos = _animator.GetBoneTransform(HumanBodyBones.LeftFoot).position;
            if (Physics.Raycast(leftFootPos + Vector3.up * 0.5f, Vector3.down,
                out RaycastHit hit, _raycastDistance, _groundLayer))
            {
                _footOffset = leftFootPos.y - hit.point.y;
            }
        }

        /// <summary>
        /// IKの計算が行われるたびに呼ばれるコールバック関数
        /// NOTE: このメソッド内で最終的な足の位置を決定している
        /// </summary>
        private void OnAnimatorIK()
        {
            // 左右の足に対して地面に足を置く
            PlaceFootOnGround(AvatarIKGoal.LeftFoot);
            PlaceFootOnGround(AvatarIKGoal.RightFoot);
        }

        /// <summary>
        /// 地面に足を置くための処理
        /// </summary>
        /// <param name="goal"> IKで動かす目標部位 </param>
        private void PlaceFootOnGround(AvatarIKGoal goal)
        {
            // 足の位置
            var footPos = _animator.GetIKPosition(goal);
            // 下向きレイ
            var ray = new Ray(footPos + Vector3.up * 0.5f, Vector3.down);

            // レイキャストが地面に当たったか
            if (Physics.Raycast(ray, out RaycastHit hit, _raycastDistance, _groundLayer))
            {
                // 足のY座標に応じてIKの重みを計算
                // NOTE: IKの重みとはIKの影響力のことで、0だとIKの影響を全く受けず、1だとIKの影響を完全に受ける
                var weight = CalcIKWeightForFootPlacement(footPos.y);

                // IKの重みと位置・回転を設定
                SetIK(goal, weight, hit);
            }
            else
            {
                // 地面に当たらなかった場合はIKの影響をなくす
                _animator.SetIKPositionWeight(goal, 0f);
                _animator.SetIKRotationWeight(goal, 0f);
            }
        }

        /// <summary>
        /// 足の位置に応じてのIKの重み計算
        /// </summary>
        /// <param name="footY"> 足のY座標 </param>
        /// <returns> IKの重み </returns>
        private float CalcIKWeightForFootPlacement(float footY)
        {
            var expectedFootY = transform.position.y + _footOffset;
            var swingLift = Mathf.Max(0f, footY - expectedFootY);
            return Mathf.Clamp01(1f - swingLift / SwingFadeHeight);
        }

        /// <summary>
        /// IKの重みと位置・回転を設定
        /// </summary>
        /// <param name="goal"> IKで動かす目標部位 </param>
        /// <param name="weight"> IKの重み </param>
        /// <param name="hit"> レイキャストの結果 </param>
        private void SetIK(AvatarIKGoal goal, float weight, RaycastHit hit)
        {
            _animator.SetIKPositionWeight(goal, weight);
            _animator.SetIKRotationWeight(goal, weight);
            _animator.SetIKPosition(goal, hit.point + Vector3.up * _footOffset);
            _animator.SetIKRotation(goal, Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation);
        }
    }
}