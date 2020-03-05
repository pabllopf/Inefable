//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="BoardBox.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------

/// <summary>Define the board boxes of the dungeon.</summary>
public enum BoardBox
{
    /// <summary>The empty</summary>
    Empty,
   
    /// <summary>The floor</summary>
    Floor,
    
    /// <summary>The wall down</summary>
    WallDown,
    
    /// <summary>The wall left</summary>
    WallLeft,
    
    /// <summary>The wall right</summary>
    WallRight,
    
    /// <summary>The wall top</summary>
    WallTop,
   
    /// <summary>The corner left up</summary>
    CornerLeftUp,
   
    /// <summary>The corner right up</summary>
    CornerRightUp,
   
    /// <summary>The corner left down</summary>
    CornerLeftDown,
    
    /// <summary>The corner right down</summary>
    CornerRightDown,
    
    /// <summary>The corner internal left down</summary>
    CornerInternalLeftDown,
   
    /// <summary>The corner internal left up</summary>
    CornerInternalLeftUp,
    
    /// <summary>The corner internal right down</summary>
    CornerInternalRightDown,
    
    /// <summary>The corner internal right up</summary>
    CornerInternalRightUp
}