using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageComposer
{
    public abstract class Id : IComparable<Id>
    {
        private readonly Guid _guid;

        #region Constructors

        protected Id(string id)
        {
            this._guid = new Guid(id);
        }

        protected Id(Guid guid)
        {
            this._guid = guid;
        }

        #endregion  // Constructors

        #region Public Methods

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var target = obj as Id;
            if (target == null)
            {
                return false;
            }

            return this._guid == target._guid;
        }

        public int CompareTo(Id other)
        {
            if (other == null)
            {
                return 1;
            }

            return this._guid.CompareTo(other._guid);
        }

        public override string ToString()
        {
            return this._guid.ToString("D");
        }

        public override int GetHashCode()
        {
            return this._guid.GetHashCode();
        }

        public Guid ToGuid()
        {
            return this._guid;
        }

        #region Utility Methods

        public static bool operator ==(Id a, Id b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Id a, Id b)
        {
            return !(a == b);
        }

        #endregion // Utility Methods

        #endregion // Public Methods
    }
}
