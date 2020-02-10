using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


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
    float p_main_velocity = 0;
    float[] p_ins_velocity = new float[1]; 
    int minIndex = 0;

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

    private void FixedUpdate()
    {    
        for (int i=0; i<mirrors_.Count; i++)
        {
            var mirror = mirrors_[i];

            // Move main player and instanced one seperately in this mirror
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");


            // player-main movement
            MoveInMirror( h, v, mirror.p_rb_ );

            // player-main velocity
            p_main_velocity = VelocityCalc( mirror.p_.transform.position, mirror.last_pos_main_ );
            
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

            // set array size
            Array.Resize<float>( ref p_ins_velocity, mirrors_.Count);
            // player-ins velocity
            p_ins_velocity[i] = VelocityCalc( mirrors_[i].p_ins_.transform.position , mirrors_[i].last_pos_ins_ );
            
            // find slowest member
            minIndex = Array.IndexOf(p_ins_velocity, p_ins_velocity.Min());
            Debug.Log(minIndex);
            
            // Repositioning base on slowest member
            // if player-main was slowest:
            if (p_main_velocity < p_ins_velocity[minIndex] * 0.9)
            {
                mirrors_[i].p_ins_.transform.position = Obj_Refl_Pos(mirrors_[i].mirror_direction_, mirror.p_.transform.position);
            }

            // if one of player-ins was slowest
            else if (p_ins_velocity[minIndex] < p_main_velocity * 0.9)
            {
                if (i != minIndex)
                {
                    mirror.p_.transform.position = Obj_Refl_Pos(-mirrors_[minIndex].mirror_direction_, mirrors_[minIndex].p_ins_.transform.position);
                    mirrors_[i].p_ins_.transform.position = Obj_Refl_Pos(mirrors_[i].mirror_direction_, mirror.p_.transform.position);
                                                            // Obj_Refl_Pos(-mirrors_[minIndex].mirror_direction_, mirrors_[minIndex].p_ins_.transform.position));
                }
            }
                  

            // set last-pos for player-main and player-ins (to calculate velocity)
            mirrors_[i].last_pos_ins_ = mirrors_[i].p_ins_.transform.position;
            mirror.last_pos_main_ = mirror.p_.transform.position;
        }
    } 

    // Object Reflection Position ////////////////////////////////////////////////////////////////////////////////////
    // return reletive position of an object, other side of the mirror
    public static Vector3 Obj_Refl_Pos (Vector3 mirror_direction , Vector3 p)
	{
		RaycastHit hit;
		Vector3 ray2mirror; // Vector from object to mirror
		int layer_mask = LayerMask.GetMask("Mirror");
        Vector3 ReflPos = Vector3.zero;
        Ray ray = new Ray(p, -mirror_direction);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
        {
            if (hit.collider != null)
            {
                ray2mirror = hit.point - p;
                ReflPos = new Vector3 (	p.x + (ray2mirror.x*2) * Mathf.Abs(-mirror_direction.x),
                                        p.y,
                                        p.z + (ray2mirror.z*2) * Mathf.Abs(-mirror_direction.z));
            }
        }

        return ReflPos;
	}

    // MoveInMirror ////////////////////////////////////////////////////////////////////////////////////////////////
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

    // Velocity Calculator ///////////////////////////////////////////////////////////////////////////////////////////
    private float VelocityCalc(Vector3 currentPosition, Vector3 LastPosition)
    {
        Vector3 moveVector;
        moveVector = new Vector3 ((currentPosition - LastPosition).x / Time.deltaTime, 
                                  (currentPosition - LastPosition).y / Time.deltaTime,
                                  (currentPosition - LastPosition).z / Time.deltaTime );

        return moveVector.magnitude;
    }
}
