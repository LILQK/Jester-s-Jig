using UnityEngine;

public class FootController : MonoBehaviour
{
    private Animator animator;

    public bool foodOnPlace = false;
    public int foodPose = 0;
    public Vector3 foodPos = Vector3.zero;

    float timer = 0;
    float cooldown = 1f;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (timer <= 0)
        {
            foodOnPlace = false;
            timer = cooldown;
        }
        else {
            timer -= Time.deltaTime;
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (foodOnPlace) {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);

            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            switch (foodPose)
            {
                case 0:
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, foodPos);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

                    break;
                case 1:
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, foodPos);
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

                    break;
                case 2:
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, foodPos);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

                    break;
                case 3:
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, foodPos);
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

                    break;
            }
        }
    }
    public void SetFootOnPose(int pose, Vector3 pos) {
        foodPos = pos;
        foodPose = pose;
        foodOnPlace =true;
    }
}
