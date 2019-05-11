using System;
using Newtonsoft.Json;

namespace Dfreeze.Services
{
    public struct FreezerIdentifier
    {
        internal int Id { get; private set; }

        public FreezerIdentifier(int id)
        {
            Id = id;
        }

        public FreezerIdentifier(FreezerModel freezer)
        {
            Id = freezer.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is FreezerIdentifier)
            {
                var other = (FreezerIdentifier)obj;
                return Id == other.Id;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return $"{Id}";
        }

        public static bool operator ==(FreezerIdentifier left, FreezerIdentifier right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FreezerIdentifier left, FreezerIdentifier right)
        {
            return !left.Equals(right);
        }
    }
}