using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollControl : MonoBehaviour {

	// 回転速度
	public int Speed;

	// ドラムの動作状態
	private int drumStatus;
	const int DRUM_SCROLL = 0;
	const int DRUM_STOP = 1;

	private List<Transform> itemAry;
	private float heightSum;
	private float underPosi;	// オブジェクトを上に移動させるボーダー


	// Use this for initialization
	void Start () {

		drumStatus = DRUM_SCROLL;

		Image[] image = this.GetComponentsInChildren<Image> ();
		itemAry = new List<Transform> ();
		float objPosiY = 0.0f;
		float objHeight = image[0].gameObject.GetComponent<RectTransform> ().sizeDelta.y;
		foreach (var im in image) {
			Transform t = im.gameObject.transform;
			Vector3 v = t.position;
			t.position = new Vector3(v.x,v.y+objPosiY,v.z);
			objPosiY -= objHeight;

			itemAry.Add( im.gameObject.transform);
		}

		heightSum = 0;
		foreach (var m in itemAry) {
			
			heightSum += m.gameObject.GetComponent<RectTransform>().sizeDelta.y;
		}

		// 図柄を上に移動させるボーダー座標
		underPosi = -200;//this.GetComponent<RectTransform>().position.y;	// 545.9
		//underPosi += this.GetComponent<RectTransform> ().sizeDelta.y; // 895.9
		//underPosi += itemAry [0].gameObject.GetComponent<RectTransform> ().sizeDelta.y;
		underPosi *= -1;

		Debug.Log ("UnderPosi : " + underPosi);
		Debug.Log ("HeightSum : " + heightSum);

		_FuncTime ();
	}

	void _FuncTime()
	{
		string s = "ABCDEFGHIJKLMNOPQRSTU";

		// 現在の経過時間を取得
		float check_time = Time.realtimeSinceStartup;

		for (int i = 0; i < 1000; i++) {
			stringRef (ref s);
		}
		 
		// 処理完了後の経過時間から、保存していた経過時間を引く＝処理時間
		check_time = Time.realtimeSinceStartup - check_time;
		 
		Debug.Log( "check time ref : " + check_time.ToString("0.00000") );

	
	
		// 現在の経過時間を取得
		check_time = Time.realtimeSinceStartup;

		for (int i = 0; i < 1000; i++) {
			stringRef (ref s);
		}

		// 処理完了後の経過時間から、保存していた経過時間を引く＝処理時間
		check_time = Time.realtimeSinceStartup - check_time;

		Debug.Log( "check time param: " + check_time.ToString("0.00000") );

	}

	private void stringRef(ref string s)
	{
		s = s + "A";
	}

	private void stringParam(string s)
	{
		s = s + "A";
	}

	// Update is called once per frame
	void Update () {
		switch(drumStatus)
		{
			case DRUM_SCROLL:
				UpdateScroll ();
			break;
		}
	}

	private void UpdateScroll()
	{
		foreach (var m in itemAry) {
			Vector3 v = m.position;

			// 合計を超えたら上に戻す
			if (v.y <= underPosi) {
				Debug.Log (v.y);
				v.y +=heightSum;
			}

			v.y -= Speed;
			m.position = v;

		}
	}
	public void PushButton()
	{
		drumStatus = (drumStatus == DRUM_SCROLL) ? DRUM_STOP : DRUM_SCROLL;
	}

}
