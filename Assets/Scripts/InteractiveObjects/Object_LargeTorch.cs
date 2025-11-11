using UnityEngine;

public class Object_LargeTorch : MonoBehaviour
{
   private Animator anim;

   private void Awake()
   {
      anim = GetComponentInChildren<Animator>();
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      anim.SetBool("isActive", true);
   }
}
