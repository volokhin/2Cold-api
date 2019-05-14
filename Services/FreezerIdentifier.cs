using System;
using Newtonsoft.Json;

namespace Dfreeze.Services
{
    public struct FreezerIdentifier
    {
        internal int Id { get; private set; }
        internal int Floor { get; private set; }

        public FreezerIdentifier(int floor, int id)
        {
            Floor = floor;
            Id = id;
        }

        public FreezerIdentifier(FreezerModel freezer)
        {
            Floor = freezer.Floor;
            Id = freezer.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is FreezerIdentifier)
            {
                var other = (FreezerIdentifier)obj;
                return Id == other.Id && Floor == other.Floor;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id ^ Floor;
        }

        public override string ToString()
        {
            return $"{Floor}/{Id}";
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