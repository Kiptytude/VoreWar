using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal struct Vec2 : IComparable<Vec2>, IEquatable<Vec2>
{
    public int x, y;

    public Vec2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Vec2 operator +(Vec2 a, Vec2 b)
    {
        return new Vec2(a.x + b.x, a.y + b.y);
    }

    public static bool operator ==(Vec2 a, Vec2 b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(Vec2 a, Vec2 b)
    {
        return !(a == b);
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }

    public int CompareTo(Vec2 other)
    {
        if (x == other.x)
        {
            return y.CompareTo(other.y);
        }
        else
        {
            return x.CompareTo(other.x);
        }
    }

    public bool Equals(Vec2 other)
    {
        return other == this;
    }

    public override int GetHashCode()
    {
        return ((x.GetHashCode()
            ^ (y.GetHashCode() << 1)) >> 1);
    }

    public override bool Equals(object obj)
    {
        if (obj is Vec2)
        {
            return (Vec2)obj == this;
        }

        return false;
    }

    public static implicit operator Vec2(Vec2i obj)
    {
        if (obj == null)
        {
            Debug.Log("Vec2 Passed null comparison");
            return new Vec2(0, 0);
        }
        return new Vec2(obj.x, obj.y);
    }
}