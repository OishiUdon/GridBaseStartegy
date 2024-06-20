using System;

/// <summary>
/// �O���b�h��̈ʒu�E���W��ݒ肷��N���X
/// </summary>
public struct GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }


    public static bool operator ==(GridPosition left, GridPosition right)
    {
        return left.x == right.x && left.z == right.z;
    }

    public static bool operator !=(GridPosition left, GridPosition right)
    {
        return !(left == right);
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z);
    }

    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }
}
