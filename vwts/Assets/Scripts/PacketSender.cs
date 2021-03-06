using UnityEngine;
using System.Collections;

public class PacketSender : MonoBehaviour {

	public GameObject packet;

	public GameObject targetObject;
	public GameObject targetObject2;

	public float spawnRate = 30f;
	public float packetDestroyTime = 1f;

	public float xPacketRotation = 0;
	public float yPacketRotation = 0;
	public float zPacketRotation = 90;

	public float xPacketOriginOffset = 0;
	public float yPacketOriginOffset = 0;
	public float zPacketOriginOffset = 0;

	public float xPacketTargetOffset = 0;
	public float yPacketTargetOffset = 0;
	public float zPacketTargetOffset = 0;
	public bool sendDlData = false;

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("movePackets", .0f, 1/spawnRate);
	}

	// Update is called once per frame
	void Update () {
	}

	public void StartSendingPackets(){
		InvokeRepeating ("sendPackets", 0.0f, 1 / spawnRate);
	}

	public void StartReceivingPackets(){
		InvokeRepeating ("receivePackets", 0.0f, 1 / spawnRate);
	}

	public void StopSendingPackets(){
		CancelInvoke ("sendPackets");
		CancelInvoke ("receivePackets");
	}

	private void sendPackets(){
		GameObject p = (GameObject)Instantiate (packet, 
			               new Vector3 (
				               gameObject.transform.position.x + xPacketOriginOffset, 
				               gameObject.transform.position.y + yPacketOriginOffset, 
				               gameObject.transform.position.z + zPacketOriginOffset), 
			               Quaternion.Euler (xPacketRotation, yPacketRotation, zPacketRotation));
		


		if (sendDlData == true) {
			iTween.MoveTo (p, 
				iTween.Hash (
					"x", targetObject2.transform.position.x + xPacketTargetOffset, 
					"y", targetObject2.transform.position.y + yPacketTargetOffset, 
					"z", targetObject2.transform.position.z + zPacketTargetOffset, 
					"easeType", "easeInOutSine", 
					"loopType", "loop", 
					"delay", .1));
		} else {
			iTween.MoveTo (p, 
				iTween.Hash(
					"x", targetObject.transform.position.x + xPacketTargetOffset, 
					"y", targetObject.transform.position.y + yPacketTargetOffset, 
					"z", targetObject.transform.position.z + zPacketTargetOffset, 
					"easeType", "easeInOutSine", 
					"loopType", "loop", 
					"delay", .1));
		}

		Destroy (p, packetDestroyTime);
	}

	private void receivePackets(){
		GameObject p = (GameObject)Instantiate (packet, 
			               new Vector3 (
				               targetObject.transform.position.x + xPacketTargetOffset,
				               targetObject.transform.position.y + yPacketTargetOffset,
				               targetObject.transform.position.z + zPacketTargetOffset),
			               Quaternion.Euler (xPacketRotation, yPacketRotation, zPacketRotation));

		iTween.MoveTo (p,
			iTween.Hash(
				"x", gameObject.transform.position.x + xPacketOriginOffset, 
				"y", gameObject.transform.position.y + yPacketOriginOffset,
				"z", gameObject.transform.position.z + zPacketOriginOffset, 
				"easeType", "easeInOutSine", 
				"loopType", "loop", 
				"delay", .1));
		
		Destroy (p, packetDestroyTime);
	}
}
