
namespace BLEExample.Services.BLE.SharedImplementation.Exceptions
{
    /// <summary>
    /// An exception that is thrown whenever the connection to a device fails.
    /// </summary>
    public class BLEPeripheralConnectionException : Exception
    {
        /// <summary>
        /// The peripheral Id.
        /// </summary>
        public Guid PeripheralId { get; }
        /// <summary>
        /// The peripheral name.
        /// </summary>
        public string Name { get; }

 
        public BLEPeripheralConnectionException(Guid peripheralId, string name, string errorMessage) : base(errorMessage)
        {
            PeripheralId = peripheralId;
            Name = name;
        }
    }
}
