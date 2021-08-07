using System;
using MySqlConnector;

namespace Core.Interfaces
{
    /// <summary>Interface to create a MySql connection.</summary>
    public interface IAppDb : IDisposable
    {
        MySqlConnection Connection { get; } 
        void Dispose();
    }
}

// TODO: Flytta från Core och lägg istället detta interface i Application så att man slipper att lägga till paketet MySqlConnector till Core?
