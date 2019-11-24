using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    public class AIChainAoEAttack : AIAction
    {
        public float waitIntervalMax;
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

        public int totalExplosions = 1;
        public float delayBetweenExplosions = 0;
        private int explosions;
        private Vector3 currentLocation;
        private Vector3 targetLocation;
        private Vector3 distanceBetweenExplosions;

        protected virtual void Shoot()
        {
            if (_numberOfShoots < 1)
            {
                StartCoroutine(waitBeforeShooting(Random.Range(0, waitIntervalMax)));
                _numberOfShoots++;
            }
        }

        public virtual IEnumerator waitBeforeShooting(float x)
        {
            yield return new WaitForSecondsRealtime(x);
            //play animation
            GetComponentInParent<Character>().MovementState.ChangeState(CharacterStates.MovementStates.Attacking);
            StartCoroutine("endAnimation");

            explosions = 0;
            currentLocation = transform.position;


            float mult = (10 / (_brain.Target.position - currentLocation).magnitude);

            targetLocation = currentLocation + (_brain.Target.position - currentLocation) * mult;

            distanceBetweenExplosions = (targetLocation - transform.position) / totalExplosions;
            StartCoroutine("chainExplosion");

            StaticManager.addObjectToPool();
        }

        IEnumerator chainExplosion()
        {
            while (explosions < totalExplosions)
            {
                Instantiate(aoeProjectile, currentLocation + distanceBetweenExplosions * (explosions + 1), Quaternion.identity, StaticManager.ObjectPool.transform);
                explosions++;
                yield return new WaitForSeconds(delayBetweenExplosions);
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
