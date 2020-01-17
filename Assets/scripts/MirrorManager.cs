using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mirrorData
{
    public Vector3 mirror_direction_;
    public GameObject p_ins_, p_;
    public Rigidbody p_ins_rb_, p_rb_;
    public Vector3 last_pos_ins_, last_pos_main_;

    public mirrorData (GameObject p_ins, GameObject p, Rigidbody p_ins_rb, Rigidbody p_rb, Vector3 last_pos_ins, Vector3 last_pos_main, Vector3 mirror_direction)
    {
        p_ins_ = p_ins;
        p_ = p;
        p_ins_rb_ = p_ins_rb;
        p_rb_ = p_rb;
        last_pos_ins_ = last_pos_ins;
        last_pos_main_ = last_pos_main;
        mirror_direction_ = mirror_direction; 
    }

    public void DestroyPlayer()
    {
        GameObject.Destroy(p_ins_.gameObject);
    } 
}

public class MirrorManager : MonoBehaviour
{
    public static MirrorManager singletone;
    private List<mirrorData> mirrors_;

    private void Awake()
    {
        singletone = this;
        mirrors_ = new List<mirrorData>();
    }

    public void AddMirror(mirrorData mirror)
    {
        mirrors_.Add(mirror);
    }

    public void RemoveMirror(mirrorData mirror)
    {
        mirrors_.Remove(mirror);
    }

    private void Update()
    {    
        for (int i=0; i<mirrors_.Count; i++)
        {
            var mirror = mirrors_[i];

            // Move main player and instanced one seperately in this mirror
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");


            // player-main movement
            MoveInMirror( h, v, mirror.p_rb_ );

            // player-main speed
            float p_main_velocity = new Vector3(( mirror.p_.transform.position - mirror.last_pos_main_).x / Time.deltaTime ,
                                                ( mirror.p_.transform.position - mirror.last_pos_main_).y / Time.deltaTime ,
                                                ( mirror.p_.transform.position - mirror.last_pos_main_).z / Time.deltaTime ).magnitude;
            
            // change the movement orientation based on the mirror type
            if (mirror.mirror_direction_.z == 0)
            {
                h = -h;
            }
            else if (mirror.mirror_direction_.x == 0)
            {
                v = -v;
            }

            // player-ins movement
            MoveInMirror( h, v, mirror.p_ins_rb_ );

            // player-ins speed
            float p_ins_velocity = new Vector3 (( mirror.p_ins_.transform.position - mirror.last_pos_ins_).x / Time.deltaTime , 
                                                 (mirror.p_ins_.transform.position - mirror.last_pos_ins_).y / Time.deltaTime ,
                                                ( mirror.p_ins_.transform.position - mirror.last_pos_ins_).z / Time.deltaTime ).magnitude;


            // Syncing movement speed //////////////////////////////////////////////
            if (p_main_velocity * 0.9f > p_ins_velocity)
            {
                // min_velocity = p_ins_velocity;
                mirror.p_.transform.position = Obj_Refl_Pos(-mirror.mirror_direction_, mirror.p_ins_);
            }
            else if (p_ins_velocity * 0.9f > p_main_velocity)
            {
                // p_ins_velocity = min_velocity;
                mirror.p_ins_.transform.position = Obj_Refl_Pos(mirror.mirror_direction_, mirror.p_);
            }

            // set last-pos for player-main and player-ins (to calculate velocity)
            mirror.last_pos_ins_ = mirror.p_ins_.transform.position;
            mirror.last_pos_main_ = mirror.p_.transform.position;
        }
    } 


    // return reletive position of an object, other side of the mirror
    public static Vector3 Obj_Refl_Pos (Vector3 mirror_direction , GameObject p)
	{
		RaycastHit hit;
		Vector3 ray2mirror; // Vector from object to mirror
		int layer_mask = LayerMask.GetMask("Mirror");
        Vector3 ReflPos = Vector3.zero;
        Ray ray = new Ray(p.transform.position, -mirror_direction);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
        {
            if (hit.collider != null)
            {
                ray2mirror = hit.point - p.transform.position;
                ReflPos = new Vector3 (	p.transform.position.x + (ray2mirror.x*2) * Mathf.Abs(-mirror_direction.x),
                                        p.transform.position.y,
                                        p.transform.position.z + (ray2mirror.z*2) * Mathf.Abs(-mirror_direction.z));
            }
        }

        return ReflPos;
	}


    void MoveInMirror( float h, float v, Rigidbody rb )
    {
        Vector3 movement = new Vector3(h, 0, v);
        movement = movement.normalized * rb.gameObject.GetComponent<PlayerMovement>().Speed * Time.deltaTime;
        
        rb.velocity = movement;

        // change player orientation
        if (movement != Vector3.zero)
		{
			rb.gameObject.transform.rotation = Quaternion.LookRotation(movement);
		}
    }
}
