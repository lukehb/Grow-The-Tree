using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace AssemblyCSharp
{
		//Interface used by objects that fall from the sky
	public interface IFallingItem
	{
		
		//Objects that fall from the sky must specify how they handle collisions with things
		bool CollisionHandler(FSFixture fixA, FSFixture fixB, Contact contact);
		
		//Falling objects must be able to be removed
		void RemoveSelf();
	}
}

