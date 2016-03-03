using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MessageComposer
{
    [JsonConverter(typeof(RepoIdJsonConverter))]
    public class RepoId : Id, IComparable<RepoId>
    {
        #region Constructors

        public RepoId(string id) : base(id)
        {
        }

        public RepoId(Guid guid) : base(guid)
        {
        }

        #endregion  // Constructors

        #region Public Methods

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(RepoId other)
        {
            return base.CompareTo(other);
        }

        #region Utility Methods

        public static bool TryParse(string id, out RepoId repositoryId)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(id, nameof(id));
            }

            repositoryId = null;
            try
            {
                repositoryId = new RepoId(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool operator ==(RepoId a, RepoId b)
        {
            return (Id)a == (Id)b;
        }

        public static bool operator !=(RepoId a, RepoId b)
        {
            return !(a == b);
        }

        public static explicit operator string(RepoId id)
        {
            return id == null ? null : id.ToString();
        }

        public static explicit operator RepoId(string id)
        {
            return string.IsNullOrEmpty(id) ? null : new RepoId(id);
        }

        #endregion // Utility Methods

        #endregion // Public Methods
    }
}
