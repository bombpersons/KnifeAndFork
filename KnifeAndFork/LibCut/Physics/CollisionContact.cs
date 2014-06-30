using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Box2D.XNA;

namespace LibCut.Physics
{
    public class CollisionContact : IContactListener
    {
        public void PreSolve(Contact _contact, ref Manifold _manifold)
        {
            // We can prevent collisions from occurring using this callback
            // Before collision is actually solved we have the chance to do
            // something with the contacts that will be created

            // We don't want bullets to collide with the shape they are associated with
            if (_contact.GetFixtureA().GetUserData() is Things.Bullet.BulletShooterProxy && _contact.GetFixtureB().GetUserData() is Shapes.PhysicsShape)
            {
                if ((_contact.GetFixtureA().GetUserData() as Things.Bullet.BulletShooterProxy).Shooter.Wearer
                    == (_contact.GetFixtureB().GetUserData() as Shapes.PhysicsShape))
                {
                    _contact.SetEnabled(false);
                }
            }
            if (_contact.GetFixtureB().GetUserData() is Things.Bullet.BulletShooterProxy && _contact.GetFixtureA().GetUserData() is Shapes.PhysicsShape)
            {
                if ((_contact.GetFixtureB().GetUserData() as Things.Bullet.BulletShooterProxy).Shooter.Wearer
                    == (_contact.GetFixtureA().GetUserData() as Shapes.PhysicsShape))
                {
                    _contact.SetEnabled(false);
                }
            }
        }

        public void PostSolve(Contact _contact, ref ContactImpulse _impulse)
        {
        }

        public void EndContact(Contact _contact)
        {
            Things.Thing thing1 = _contact.GetFixtureA().GetUserData() as Things.Thing;
            Things.Thing thing2 = _contact.GetFixtureB().GetUserData() as Things.Thing;

            if (thing1 != null)
            {
                thing1.UnHit = true;
                thing1.HitObject = _contact.GetFixtureB().GetUserData();
            }
            if (thing2 != null)
            {
                thing2.UnHit = true;
                thing2.HitObject = _contact.GetFixtureA().GetUserData();
            }
        }

        public void BeginContact(Contact _contact)
        {
            Things.Thing thing1 = _contact.GetFixtureA().GetUserData() as Things.Thing;
            Things.Thing thing2 = _contact.GetFixtureB().GetUserData() as Things.Thing;

            if (thing1 != null)
            {
                thing1.Hit = true;
                thing1.HitObject = _contact.GetFixtureB().GetUserData();
            }
            if (thing2 != null)
            {
                thing2.Hit = true;
                thing2.HitObject = _contact.GetFixtureA().GetUserData();
            }
        }
    }
}
