using System;
using System.Collections.Generic;
using NFSScript.Math;

namespace NFSScript
{
    /// <summary>
    /// A class that represents a dynamic (physics) game object in the game world.
    /// </summary>
    public abstract class EAGLPhysicsObject
    {
        /// <summary>
        /// Dynamic game object gravity values.
        /// </summary>
        public abstract Vector3 GravityValues { get; set; }

        /// <summary>
        /// The position of the defined dynamic object ID.
        /// </summary>
        public abstract Vector3 Position { get; set; }

        /// <summary>
        /// The rotation of the defined dynamic object ID.
        /// </summary>
        public abstract Quaternion Rotation { get; set; }

        /// <summary>
        /// The rotation axis of the dynamic object.
        /// </summary>
        public abstract Vector3 RotationAxis { get; set; }

        /// <summary>
        /// The ID of the <see cref="EAGLPhysicsObject"/>.
        /// </summary>
        public abstract byte ID { get; set; }
    }

    /// <summary>
    /// A <seealso cref="Listable{T}"/> class from NFS: World.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Listable<T> : IListable, IDisposable where T : IListable
    {
        private static List<IListable> List = new List<IListable>();
        private bool disposed;

        /// <summary>
        /// Constructs the <see cref="Listable{T}"/> instance.
        /// </summary>
        public Listable()
        {
            List.Add(this);
        }

        /// <summary>
        /// Deconstructs the <seealso cref="Listable{T}"/> instance.
        /// </summary>
        ~Listable()
        {
            this.Dispose(false);
        }

        /// <summary/>
        public static void ForEach(Action<T> action)
        {
            foreach (IListable listable in List)
                action((T)listable);
        }

        /// <summary>
        /// Disposes the <see cref="Listable{T}"/> instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary/>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;
            int num = disposing ? 1 : 0;
            List.Remove(this);
            this.disposed = true;
        }
    }
}
