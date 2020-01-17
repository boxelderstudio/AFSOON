using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Reflection_Movement : MonoBehaviour 
{

	// Object reflection and positioning
	[SerializeField] GameObject Obj_1;
	[SerializeField] Vector3 mirror_offset_;


	// Invisible mode for objects (shader and shadow casting)
	[SerializeField] Material normal_mat_ , invis_mat_;
	[SerializeField] bool main_obj_vis_ = true;
	[SerializeField] bool L_mirror_obj_vis = true;
	[SerializeField] bool R_mirror_obj_vis_ = true;

	// Material CollidePos_mat;
	Animator anim;   

	GameObject L_obj, R_obj , thisGO;
	
	
	void Awake () 
	{

		L_obj = GameObject.Instantiate(Obj_1, new Vector3 (5,0,5), Quaternion.identity);
		R_obj = GameObject.Instantiate(Obj_1, new Vector3 (-5,0,-5), Quaternion.identity);


		// CollidePos_mat = GetComponent<Renderer>().material;
		anim = GetComponent<Animator>();

		// Check for visibility of objects to change shadow and shader stats
		if (main_obj_vis_ == true)
		{
			GetComponent<Renderer>().material = normal_mat_;
		}
		else
		{
			GetComponent<MeshRenderer>().shadowCastingMode = 0;
			GetComponent<Renderer>().material = invis_mat_;
		}

		// Left mirror object
		if (L_mirror_obj_vis == true)
		{
			L_obj.GetComponent<Renderer>().material = normal_mat_;
		}
		else
		{
			L_obj.GetComponent<MeshRenderer>().shadowCastingMode = 0;
			L_obj.GetComponent<Renderer>().material = invis_mat_;
		}

		// Right mirror object
		if (L_mirror_obj_vis == true)
		{
			R_obj.GetComponent<Renderer>().material = normal_mat_;
		}
		else
		{
			R_obj.GetComponent<MeshRenderer>().shadowCastingMode = 0;
			R_obj.GetComponent<Renderer>().material = invis_mat_;
		}
	}
	
	void Update() 
	{
		Obj_Refl_Move(new Vector3(0,0,-1) , L_obj , new Vector3(1,1,-1));
		Obj_Refl_Move(new Vector3(-1,0,0) , R_obj , new Vector3(-1,1,1));
	}
	
	void Obj_Refl_Move(Vector3 ray_dir , 				// Direction vector from obj to the mirror
						GameObject Other_Player , 
						Vector3 Reflected_Rotation		// We use this vector for player rotation (in the mirror).
														// If the obj was Player, Then this helps to determine 
														// the reflected moving direction of the player. 
					  )
	{
		RaycastHit hit;
		Vector3 ray2mirror; // Vector from object to mirror
		int LayerMask = 1 << 9;

		Ray ray = new Ray(transform.position , ray_dir);
		if (Physics.Raycast(ray , out hit ,Mathf.Infinity, LayerMask))
		{
			// Only if ray hits the mirror do ..
			if(hit.collider.tag == "Mirror")
			{
				ray2mirror = hit.point - transform.position;
				Other_Player.transform.position = new Vector3 
				(
					transform.position.x + (ray2mirror.x*2) * Mathf.Abs(ray_dir.x) + mirror_offset_.x,
					transform.position.y + (ray2mirror.y*2) * Mathf.Abs(ray_dir.y) + mirror_offset_.y,
					transform.position.z + (ray2mirror.z*2) * Mathf.Abs(ray_dir.z) + mirror_offset_.z
				);

				// Rotating reflected player
				if (this.gameObject.tag == "Player")
				{
					Vector3 MainCharOrientation = GetComponent<Player_Movement_old>().movement;
					MainCharOrientation = new Vector3 (MainCharOrientation.x * Reflected_Rotation.x,
														MainCharOrientation.y * Reflected_Rotation.y,
														MainCharOrientation.z * Reflected_Rotation.z);

					if (MainCharOrientation != Vector3.zero)
					{
						Other_Player.transform.rotation = Quaternion.LookRotation(MainCharOrientation);
					}
				}

				Debug.DrawRay(transform.position , ray2mirror, Color.red);
				Debug.DrawRay(hit.point , ray2mirror , Color.blue);
			}
		}
	}


	// Invis shader effect
	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.gameObject.tag == "Player" || 
			other.collider.gameObject.tag == "Other_Player")
		{
			invis_mat_.SetVector("_pos" , other.contacts[0].point + new Vector3(0,1.6f,1));
			anim.Play("Box_invis_mode");
		}
	}
}
