using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotControl : MonoBehaviour {

	public GameObject leftScrollObj;
	public GameObject centerScrollObj;
	public GameObject rightScrollObj;

	public GameObject zugara;
	public int DrumSpeed;

	public List<GameObject> leftDrum;

	private bool stopFlag;

	enum EDRUMSTATUS{
		eRoll,
		eStop,
	};

	private EDRUMSTATUS drumStatus;

	// Use this for initialization
	void Start () {
		stopFlag = false;
		drumStatus = EDRUMSTATUS.eRoll;
		leftDrum = new List<GameObject> ();
		for (var i = 0; i < 10; i++)
			leftDrum.Add( _addChild (i));

	}

	private GameObject _addChild(int index)
	{
		GameObject gObj = Instantiate (zugara);
		Transform t = gObj.transform;
		t.parent = leftScrollObj.transform;

		t.position = new Vector3 (250, index * 250, 0);
		return gObj;
	}

	// Update is called once per frame
	void Update () {

		if (stopFlag)
			return;

		foreach (var n in leftDrum) {
			Vector3 t = n.transform.position;
			t.y -= DrumSpeed;
			if (t.y <= 0.0f)
				t.y = 250 * leftDrum.Count;
			n.transform.position = t;
		}
	}
	public void StopSlot()
	{
		stopFlag = !stopFlag;
	}
}
