using UnityEngine;
using System.Collections;

public class MG_7_Target : MonoBehaviour {

	public UILabel label;
	public string word;

	private Rigidbody rb;

	public Vector3 moveDir;
	private float speed = 0.3f;

	private Collider lastHitCollider;

	void Awake(){
		label = this.GetComponentInChildren<UILabel>();
		rb = this.rigidbody;
	}

	// Use this for initialization
	void Start () {

		

		//rb.velocity = new Vector3(0.5f,0.2f,0f);

		//moveDir = new Vector3(0.25f,0.1f,0f).normalized;

		print("START:" + moveDir);
		
		moveDir= new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0f).normalized;
	}
	
	// Update is called once per frame
	void Update () {
		MoveFish();
	}

	void MoveFish(){
		transform.Translate(moveDir * Time.deltaTime * speed);

		Ray ray = new Ray(transform.position, moveDir);
		RaycastHit hit;



		if ( Physics.Raycast(ray, out hit, Time.deltaTime * speed *5) ){

			if(hit.collider.tag == "FishBounceWall"){

				
				if(hit.collider !=lastHitCollider ){

					lastHitCollider = hit.collider;

					Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);

					Debug.DrawLine (transform.position, hit.point, Color.cyan);
					print("YES:" + reflectDir.normalized);

					moveDir = reflectDir.normalized;
				}
			}

		}




		
	}

	public void CaughtWord(){
		this.gameObject.SetActive(false);
	}


	public void SetWord(string w){
		word = w;

		SetLabel();
	}

	public string GetWord(){
		return word;
	}

	void SetLabel(){
		label.text = word;
	}

	public void ResetPosition(){

		//Random.Range()
		this.gameObject.SetActive(true);
		this.transform.position = new Vector3(0f,0f,0f);
	}

	void OnTriggerExit(Collider other) {
        //Destroy(other.gameObject);
       // print("BOUNCE");

        //rb.velocity = new Vector3(Random.Range(-0.3f,0.3f),Random.Range(-0.3f,0.3f),0f);
	
    }
}
