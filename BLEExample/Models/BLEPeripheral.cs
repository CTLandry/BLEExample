using BLEExample.Services.BLE.SharedImplementation;
using BLEExample.Services.BLE.SharedImplementation.Contracts;
using System.Collections;

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
        /// Relative indecator of signal strength
        /// </summary>
        public int Rssi { get; }

        public string ReadableRssi => Rssi.ToString();

        /// <summary>
        /// Relative indecator of signal strength
        /// </summary>
        public IList ServiceUUIDs { get; }

        /// <summary>
        /// Current state of connection to the peripheral
        /// </summary>
        public BLEPeripheralConnectionState ConnectionState { get; }

        public string ConnectionStateReadable => ConnectionState.ToString();

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

        public BLEPeripheral(Guid id, string name, object nativePeripheral, BLEPeripheralConnectionState connectionState, int rssi, IList serviceUUIDs)
        {
            Id = id;
            Name = name ?? "Unknown";
            NativePeripheral = nativePeripheral;
            ConnectionState = connectionState;
            Rssi = rssi;
            ServiceUUIDs = serviceUUIDs;
        }
    }
}
