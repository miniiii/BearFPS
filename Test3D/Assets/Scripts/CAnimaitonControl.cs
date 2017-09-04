using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnimaitonControl : MonoBehaviour {


    private Animator anim = null;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

   
    public string AnimationCtrl(int _index)
    {
        switch(_index)
        {
            case 1:
                anim.SetInteger("animation", 1);    // Idle
                return "Idle";
                //break;
            case 2:
                anim.SetInteger("animation", 2);    // Alert
                return "Alert";
                //break;
            case 3:
                anim.SetInteger("animation", 3);    // Victory
                return "Victory";
                //break;
            case 4:
                anim.SetInteger("animation", 4);    // Block
                return "Block";
                //break;
            case 5:
                anim.SetTrigger("Damage");          // Damage
                return "Damage";
                //break;
            case 6:
                anim.SetTrigger("Die");             // Die1
                return "Die1";
                //break;
            case 7:
                anim.SetInteger("animation", 7);    // Die2
                return "Die2";
                //break;
            case 8:
                anim.SetInteger("animation", 8);    // Dash
                return "Dash";
                //break;
            case 9:
                anim.SetInteger("animation", 9);    // Jump
                return "Jump";
                //break;
            case 10:
                anim.SetInteger("animation", 10);   // Sit
                return "Sit";
                //break;
            case 11:
                anim.SetInteger("animation", 11);   // ATK1
                return "ATK1";
                //break;
            case 12:
                anim.SetInteger("animation", 12);   // ATK2
                return "ATK2";
                //break;
            case 13:
                anim.SetInteger("animation", 13);   // ATK3
                return "ATK3";
                //break;
            case 14:
                anim.SetInteger("animation", 14);   // ATK4
                return "ATK4";
                //break;
            case 15:
                anim.SetInteger("animation", 15);   // Move
                return "Move";
                //break;
            case 16:
                anim.SetInteger("animation", 16);   // Move_R
                return "Move_R";
                //break;
            case 17:
                anim.SetInteger("animation", 17);   // Move_L
                return "Move_L";
                //break;
            case 18:
                anim.SetInteger("animation", 18);   // Walk
                return "Walk";
                //break;
            case 19:
                anim.SetInteger("animation", 19);   // IdleA
                return "IdleA";
                //break;
            case 20:
                anim.SetTrigger("Attack");          // NormalATK
                return "NormalATK";
                //break;
        }
        return null;
    }

}
