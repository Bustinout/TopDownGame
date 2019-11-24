using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    public class AISpawnAoEAttack : AIAction
    {
        public GameObject aoeProjectile;
        public bool FaceTarget = true;

        protected CharacterOrientation2D _orientation2D;
        protected Character _character;
        protected int _numberOfShoots = 0;
        protected bool _shooting = false;
        private float animationTime;

        protected override void Initialization()
        {
            _character = GetComponent<Character>();
            _orientation2D = GetComponent<CharacterOrientation2D>();
            animationTime = aoeProjectile.GetComponent<delayedAoE>().animationDelay;
        }


        public override void PerformAction()
        {
            TestFaceTarget();
            Shoot();
        }

        protected virtual void TestFaceTarget()
        {
            if (!FaceTarget)
            {
                return;
            }

            if (this.transform.position.x > _brain.Target.position.x)
            {
                _orientation2D.FaceDirection(-1);
            }
            else
            {
                _orientation2D.FaceDirection(1);
            }
        }

        public GameObject soundEffect;
        protected virtual void Shoot()
        {
            if (_numberOfShoots < 1)
            {
                Instantiate(soundEffect);
                GetComponentInParent<Character>().MovementState.ChangeState(CharacterStates.MovementStates.Attacking);
                StartCoroutine("endAnimation");

                Vector3 target = new Vector3(_brain.Target.position.x + UnityEngine.Random.Range(-1f, 1f), _brain.Target.position.y + UnityEngine.Random.Range(-1f, 1f), _brain.Target.position.z);
                Instantiate(aoeProjectile, target, Quaternion.identity, StaticManager.ObjectPool.transform);
                StaticManager.addObjectToPool();
                _numberOfShoots++;
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            _numberOfShoots = 0;
            _shooting = true;            
        }

        public override void OnExitState()
        {
            base.OnExitState();
            _shooting = false;
        }

        IEnumerator endAnimation()
        {
            yield return new WaitForSeconds(animationTime);
            GetComponentInParent<Character>().MovementState.ChangeState(CharacterStates.MovementStates.Idle);
        }

    }
}
