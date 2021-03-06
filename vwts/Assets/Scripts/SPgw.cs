using UnityEngine;
using System.Collections;
using System;

public class SPgw : MonoBehaviour {
	
	public GameObject S1uTunnel;
	private bool displayOnce = false;
	public bool spawned = false;
	private bool establishedS1u = true;
	float speed = 0.25f;

	public PacketSender sgwDataPacketSender;
	private PacketSender enbDataPacketSender;
	
	// Use this for initialization
	void Start () {
		transform.position = new Vector3(25f, -40.0f, -3.5f);
		print("Setup SPgw");
	}
	
	// Update is called once per frame
	void Update () {
		SpawnSPgw();
	}
	
	void SpawnSPgw(){
		if (!displayOnce)
		{
			print("Spawning SPgw");
			displayOnce = true;
		}

		if (transform.position.y < 0)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
		}

		if (Input.GetKeyDown ("space")) {
			try{
				print("space Get script");
				enbDataPacketSender = sgwDataPacketSender.GetComponent <PacketSender> ().targetObject.GetComponent<PacketSender> ();
				enbDataPacketSender.sendDlData = true;
				//test
				StartDLData();
			}
			catch(Exception){
				Debug.LogError ("Drag sGW PacketSender script to AttachProcedure script");
			}
			print("space key was pressed");
		}


	}

	public void StartDLData(){
		StartCoroutine (sendDlData ());
		print("StartDlData");
	}

	IEnumerator sendDlData()
	{
		print("sendDLData");
		sgwDataPacketSender.StartSendingPackets ();
		yield return new WaitForSeconds(1);
		enbDataPacketSender.StartSendingPackets ();
	}

	void CreateSPgwS1uLink(){
		print("SPgw: Creating S1u link");
		establishedS1u = true;
	}
}
