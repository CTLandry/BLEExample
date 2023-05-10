using BLEExample.Services.BLE.SharedImplementation;
using BLEExample.Services.BLE.SharedImplementation.Contracts;

namespace BLEExample.Models
{
    /// <summary>
    /// Representation of a BLE discovered or connected device
    /// </summary>
    public class BLEPeripheral : Model, IBLEPeripheral
    {
        /// <summary>
        /// GUID representation of the peripheral's unique id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Peripheral's name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Native object refernce for peripheral
        /// </summary>
        public object NativePeripheral { get; }

        /// <summary>
        /// Current state of connection to the peripheral
        /// </summary>
        public BLEPeripheralConnectionState ConnectionState { get; }

        /// <summary>
        /// How of ten to update the connection
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateConnectionInterval(BLEConnectionIntervals interval)
        {
            throw new NotImplementedException();
        }

        public BLEPeripheral(Guid id, string name, object nativePeripheral, BLEPeripheralConnectionState connectionState)
        {
            Id = id;
            Name = name;
            NativePeripheral = nativePeripheral;
            ConnectionState = connectionState;
        }
    }
}
