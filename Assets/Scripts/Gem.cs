using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 宝石を制御するコンポーネント
public class Gem : MonoBehaviour
{
    public int m_exp; // 取得できる経験値
    public float m_brake = 0.9f; // 散らばる時の減速量、数値が小さいほどすぐ減速する

    private Vector3 m_direction; // 散らばる時の進行方向
    private float m_speed; // 散らばる時の速さ

    // 毎フレーム呼び出される関数
    private void Update()
    {
        // 散らばる速度を計算する
        var velocity = m_direction * m_speed;

        // 散らばる
        transform.localPosition += velocity;

        // だんだん減速する
        m_speed *= m_brake;

        // 宝石が画面外に出ないように位置を制限する
        transform.localPosition =
            Utils.ClampPosition(transform.localPosition);
    }

    // 宝石が出現する時に初期化する関数
    public void Init(int score, float speedMin, float speedMax)
    {
        // 宝石がどの方向に散らばるかランダムに決定する
        var angle = Random.Range(0, 360);

        // 進行方向をラジアン値に変換する
        var f = angle * Mathf.Deg2Rad;

        // 進行方向のベクトルを作成する
        m_direction = new Vector3(Mathf.Cos(f), Mathf.Sin(f), 0);

        // 宝石の散らばる速さをランダムに決定する
        m_speed = Mathf.Lerp(speedMin, speedMax, Random.value);

        // 8 秒後に宝石を削除する
        Destroy(gameObject, 8);
    }
}
